using System;
using System.Collections;
using System.Data.OleDb;
using System.Data;
using System.Threading;
using System.Text;
using System.IO;

using sc2i.common;
using sc2i.data;
using System.Reflection;
using sc2i.multitiers.server;
using sc2i.multitiers.client;



namespace sc2i.data.serveur
{
	/// <summary>
	/// Retour de fonction contenant un dataset / une erreur
	/// </summary>

	/////////////////////////////////////////////////////////////////////////////////
	//Encapsule les accès à la base de données
	public class COleDbDatabaseConnexion : C2iDbDatabaseConnexion, IDatabaseConnexion, IObjetAttacheASession
	{

		private ArrayList m_listeTables = null;

		//////////////////////////////////////////////////////////////////
		public COleDbDatabaseConnexion( int nIdSession )
			:base ( nIdSession )
		{
		}

		public override IDataBaseCreator GetDataBaseCreator()
		{
			return null;
		}

		/////////////////////////////////////////////////////////
		protected override IDbConnection GetNewConnexion ( bool bOpenIfClose )
		{
			OleDbConnection connexion = new OleDbConnection(ConnexionString);

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
			string strSql = "select max(IDENTITYCOL) from "+GetNomTableForRequete(strNomTableInDb);
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
			return new OleDbDataAdapter ( (OleDbCommand)selectCommand );
		}

		/// /////////////////////////////////////////////////////
		public override string GetNomParametre(string strNomParametre )
		{
			return "@"+strNomParametre;
		}
		
		/// /////////////////////////////////////////////////////
		protected override IAdapterBuilder GetBuilder(string strNomTableInDb)
		{
			return new C2iOleDbAdapterBuilder ( strNomTableInDb, this );
		}

		/// /////////////////////////////////////////////////////
		protected override IAdapterBuilder GetBuilder ( Type tp )
		{
			return new C2iOleDbAdapterBuilderForType ( tp, this );
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
				throw new Exception (I.T("Attempt to DataBase Id auto deactivation outside transaction|183"));
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
			string strReq = @"SELECT 'ALTER TABLE ' + QUOTENAME(TABLE_SCHEMA) + '.' + QUOTENAME(TABLE_NAME) + ' ";
			if ( bDesactiver )
				strReq += "NO";
			strReq += @"CHECK CONSTRAINT ALL'
								FROM INFORMATION_SCHEMA.TABLES
								WHERE 
								OBJECTPROPERTY(OBJECT_ID(QUOTENAME(TABLE_SCHEMA) + '.' + QUOTENAME(TABLE_NAME)), 'IsMSShipped') = 0
								AND TABLE_TYPE = 'BASE TABLE'";
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
				OleDbConnection con = (OleDbConnection)GetConnexion(true);
				DataTable tableTables = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,null);
				m_listeTables = new ArrayList();
				foreach ( DataRow row in tableTables.Rows )
					m_listeTables.Add ( row["TABLE_NAME"].ToString() );
				return (string[]) m_listeTables.ToArray(typeof(string));
			}
		}
		
		/////////////////////////////////////////////////////////////////////////////////////
		public override string GetStringForRequete ( object valeur )
		{
			return CConvertisseurObjetToOleDb.ConvertToOleDb ( valeur );
		}


		/////////////////////////////////////////////////////////////////////////////////////
		public override int CountRecords ( string strNomTableInDb, CFiltreData filtre )
		{
			filtre.SortOrder = "";
			string strNomTableInContexte = GetNomTableInContexteFromNomTableInDb(strNomTableInDb);
			Type tp = CContexteDonnee.GetTypeForTable ( strNomTableInContexte );
			string strRequete = "";
			
			if( strRequete == "" )
				strRequete = "select count (*) as COMBIEN from "+GetNomTableForRequete(strNomTableInDb);

			IDataAdapter adapter = GetSimpleReadAdapter( strRequete, filtre );
			if ( tp != null && adapter is IDbDataAdapter)
			{
				CStructureTable structure = CStructureTable.GetStructure(tp);
				strRequete = ((IDbDataAdapter)adapter).SelectCommand.CommandText.Replace("distinct","");
				strRequete = "select count(*) as COMBIEN from (\n"+
					strRequete+" group by \n";
				foreach ( CInfoChampTable info in structure.ChampsId )
				{
					strRequete += strNomTableInDb+"."+info.NomChamp+",";
				}
				strRequete = strRequete.Substring(0, strRequete.Length-1);
				strRequete += ")";
				((IDbDataAdapter)adapter).SelectCommand.CommandText = strRequete;
			}
			DataSet ds = new DataSet();
            try
            {
                this.FillAdapter(adapter, ds);
            }
            catch (Exception e)
            {
                Console.WriteLine(I.T("Error in 'CountRecords' : @1|182", e.ToString()));
                throw e;
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
			return new CFormatteurFiltreDataToStringOleDb();
		}

        /////////////////////////////////////////////////////////////////////////////////////
        public override void InitParameterType(IDbDataParameter parametre, object valeur)
        {
            if (valeur is DateTime || valeur is CDateTimeEx)
            {
                OleDbParameter paramOleDb = parametre as OleDbParameter;
                if (paramOleDb != null)
                {
                    paramOleDb.OleDbType = OleDbType.Date;
                }
            }
        }


				

	}
}
