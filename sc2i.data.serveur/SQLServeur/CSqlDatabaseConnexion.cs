using System;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Threading;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;

using sc2i.common;
using sc2i.data;
using sc2i.multitiers.server;
using sc2i.multitiers.client;
using System.Data.Common;
using sc2i.data.serveur.SQLServeur;

namespace sc2i.data.serveur
{
	/////////////////////////////////////////////////////////////////////////////////
	//Encapsule les accès à la base de données
	public class CSqlDatabaseConnexion : C2iDbDatabaseConnexion, IObjetAttacheASession
	{

		private ArrayList m_listeTables = null;

		//////////////////////////////////////////////////////////////////
		public CSqlDatabaseConnexion( int nIdSession )
			:base ( nIdSession )
		{
		}


		public override IDataBaseCreator GetDataBaseCreator()
		{
			return (IDataBaseCreator)new CSQLServeurDataBaseCreator(this);
		}

		/////////////////////////////////////////////////////////
		protected override IDbConnection GetNewConnexion ( bool bOpenIfClose )
		{
			SqlConnection connexion = new SqlConnection(ConnexionString);

			if ( bOpenIfClose )
			{
				switch ( connexion.State )
				{
					case ConnectionState.Broken :
						connexion = null;
						return GetConnexion(bOpenIfClose);
					case ConnectionState.Closed :
						connexion.Open();
						break;
				}
			}
			return connexion;
		}


		////////////////////////////////////////////////////////
		public override int GetMaxIdentity ( string strNomTableInDb )
		{
			string strSql = "select max(IDENTITYCOL) from "+strNomTableInDb;
			IDbConnection con = GetConnexion (true);
			IDbCommand command = con.CreateCommand();
			command.CommandType = CommandType.Text;
			if (IsInTrans())
				command.Transaction = Transaction;
			command.CommandText = strSql;
			object obj = command.ExecuteScalar();
			int nVal = obj==DBNull.Value?0:(int)obj;
			if ( !IsInTrans() )
				con.Close();
            command.Dispose();
			return nVal;
		}

		/// /////////////////////////////////////////////////////
		protected override IDbDataAdapter GetNewAdapter( IDbCommand selectCommand )
		{
            CSpeedSqlDataAdapter adapter = new CSpeedSqlDataAdapter((SqlCommand)selectCommand);
            return adapter;
			/*SqlDataAdapter adapter = new SqlDataAdapter ( (SqlCommand)selectCommand );
            return adapter;*/
		}
		
		/// /////////////////////////////////////////////////////
		public override string GetNomParametre(string strNomParametre )
		{
			return "@"+strNomParametre;
		}

		/// /////////////////////////////////////////////////////
		protected override IAdapterBuilder GetBuilder(string strNomTable)
		{
			return new C2iSqlAdapterBuilder ( strNomTable, this );
		}

		/// /////////////////////////////////////////////////////
		protected override IAdapterBuilder GetBuilder ( Type tp )
		{
			return new C2iSqlAdapterBuilderForType ( tp, this );
		}

		/// /////////////////////////////////////////////////////
		public override void PrepareTableToWriteDatabase ( DataTable table )
		{
		}

		//Non table->True si desactivé, null sinon
		private Hashtable m_tableEtatIdAuto = new Hashtable();

		
		protected override bool IsIdAutoDesactive ( string strNomTable )
		{
			return (m_tableEtatIdAuto[strNomTable]is bool) && (bool)m_tableEtatIdAuto[strNomTable];
		}


		/// /////////////////////////////////////////////////////
		public override void DesactiverIdAuto ( string strNomTableInDb, bool bDesactiver )
		{
			if ( !IsInTrans() )
				throw new Exception(I.T("Attempt to DataBase Id auto deactivation outside transaction|183"));
			string strReq = "SET IDENTITY_INSERT "+strNomTableInDb+" ";
			m_tableEtatIdAuto[strNomTableInDb] = bDesactiver;
			if ( bDesactiver )
			{
				strReq += "on";
			}
			else
			{
				strReq += "off";
			}
			RunStatement ( strReq );
		}

		/// /////////////////////////////////////////////////////
		public override void DesactiverContraintes (  bool bDesactiver )
		{
			if ( !IsInTrans() )
				throw new Exception(I.T("Attempt to DataBase contrains deactivation outside transaction|184"));
			//Récupère toutes les requetes à éxecuter
			string strReq = "SELECT 'ALTER TABLE ' + QUOTENAME(TABLE_SCHEMA) + '.' + QUOTENAME(TABLE_NAME) + ' ";
			if ( bDesactiver )
				strReq += "NO";
			strReq += "CHECK CONSTRAINT ALL' " +
			"FROM INFORMATION_SCHEMA.TABLES " +
			"WHERE " +
			"OBJECTPROPERTY(OBJECT_ID(QUOTENAME(TABLE_SCHEMA) + '.' + QUOTENAME(TABLE_NAME)), 'IsMSShipped') = 0 " +
			"AND TABLE_TYPE = 'BASE TABLE'";
			IDataAdapter adapter = GetSimpleReadAdapter(strReq);
			DataSet ds = new DataSet();
            this.FillAdapter(adapter, ds);
            CUtilDataAdapter.DisposeAdapter(adapter);
			foreach ( string strNomTable in TablesNames )
				m_tableEtatIdAuto[strNomTable] = bDesactiver;

			strReq = "";
			//Fabrique la requete globale
			foreach ( DataRow row in ds.Tables["Table"].Rows )
				strReq += row[0].ToString()+";\n";
			RunStatement ( strReq );
		}

		

		//-----------------------------------------------------------------------------------
		/// <summary>
		/// Renvoie un tableau contenant les noms des tables de la base de données.
		/// </summary>
		public override string[] TablesNames
		{
			get
			{
				string strRequete = "";
				if(m_listeTables != null)
				{
					strRequete = "select count(*) from sys.tables where [type] = 'U'";
					CResultAErreur result = ExecuteScalar(strRequete);
					if (result && (int)result.Data == m_listeTables.Count)
					{
						return (string[])m_listeTables.ToArray(typeof(string));
					}
				}
				m_listeTables = new ArrayList();

				strRequete = "select [name] as TABLE_NAME from sys.tables where [type] = 'U'";
				SqlDataAdapter adapter = new SqlDataAdapter(strRequete, (SqlConnection)this.GetConnexion());
				adapter.SelectCommand.Transaction = (SqlTransaction)Transaction;
				DataTable tableTablesNames = new DataTable();
                this.FillAdapter(adapter, tableTablesNames);
				foreach(DataRow row in tableTablesNames.Rows)
				{
					m_listeTables.Add(row["TABLE_NAME"].ToString());
				}
				return (string[]) m_listeTables.ToArray(typeof(string));
			}
		}
		
		/////////////////////////////////////////////////////////////////////////////////////
		public override string GetStringForRequete ( object valeur )
		{
			return CConvertisseurObjetToSqlServeur.ConvertToSql ( valeur );
		}


		/////////////////////////////////////////////////////////////////////////////////////
		public override int CountRecords ( string strNomTableInDb, CFiltreData filtre )
		{
			filtre.SortOrder = "";
			string strNomTableInContexte = GetNomTableInContexteFromNomTableInDb(strNomTableInDb);
			Type tp = CContexteDonnee.GetTypeForTable ( strNomTableInContexte );
			string strRequete = "";
			if ( tp != null )
			{
				CStructureTable structure = CStructureTable.GetStructure(tp);
				if ( structure.ChampsId.Length == 1 )
					strRequete = "select count ( distinct "+strNomTableInDb+"."+structure.ChampsId[0].NomChamp+") as COMBIEN from "+strNomTableInDb ;
			}
			if( strRequete == "" )
				strRequete = "select count (*) as COMBIEN from "+strNomTableInDb ;

			IDataAdapter adapter = GetSimpleReadAdapter( strRequete, filtre );

			DataSet ds = new DataSet();
            try
            {
                /*SqlCommand cmd = ((SqlDataAdapter)adapter).SelectCommand;
                cmd.Connection = (SqlConnection)GetConnexion(true);
                cmd.Transaction = (SqlTransaction)Transaction;
                object val = ((SqlDataAdapter)adapter).SelectCommand.ExecuteScalar();*/
                this.FillAdapter(adapter, ds);
            }
            catch (Exception e)
            {
                string strTmp = e.ToString();
                return 0;
            }
            finally
            {
                CUtilDataAdapter.DisposeAdapter(adapter);
            }
			return ((int)ds.Tables["Table"].Rows[0]["COMBIEN"]);
		}

		/////////////////////////////////////////////////////////////////////////////////////
		public override IFormatteurFiltreDataToString GetFormatteurFiltre()
		{
			return new CFormatteurFiltreDataToStringSqlServer(this);
		}

		/////////////////////////////////////////////////////////////////////////////////////
		protected override string OptimiseRequete(string strRequete)
		{
			//En fait ça sert à rien (après chrono, c'est sur!)
			/* string strUp = strRequete.ToUpper();
			int nPos = strUp.IndexOf("FROM");
			if ( nPos < 0 )
				return strUp;
			string strSelect = strRequete.Substring(0, nPos-1);
			string strFrom = strRequete.Substring(nPos+4).Trim();
			string strOrdre = "";
			strUp = strFrom.ToUpper();
			nPos = strUp.LastIndexOf("ORDER BY");
			if ( nPos >= 0 )
			{
				strOrdre = strFrom.Substring(nPos);
				strFrom = strFrom.Substring(0, nPos-1);
			}
			Regex ex = new Regex ( "^((?:\\[[0-9a-zA-Z_]+\\])|(?:[0-9a-zA-Z_]+)) +(?i:where) +((?:\\[[0-9a-zA-Z_]+\\])|(?:[0-9a-zA-Z_]+)) +(?i:IN) +\\(((?:[0-9]+,)*[0-9]+)\\)$" );
			if ( ex.IsMatch(strFrom) )
			{
				Match match = ex.Match ( strFrom );
				string strTable = match.Groups[1].Value;
				string strChamp = match.Groups[2].Value;
				string strVals = match.Groups[3].Value;
				string strTableTmp = "#"+CGenerateurStringUnique.GetNewNumero(m_nIdSession);
				string strCommande = "CREATE TABLE "+strTableTmp+
					"( TMP_ID int not null );";
				string[] strIds = strVals.Split(',');
				foreach (string strId in strIds )
				{
					strCommande += "INSERT INTO "+strTableTmp+" values ("+strId+");";
				}
				strCommande += strSelect + " FROM "+strTable+" inner join "+strTableTmp+" as TMPTMP on "+
					strChamp+"=TMPTMP.TMP_ID "+strOrdre+";";
				strCommande += "DROP TABLE "+strTableTmp;
				return strCommande;				
			}*/
			return strRequete;
		}

        

        

				

	}
}
