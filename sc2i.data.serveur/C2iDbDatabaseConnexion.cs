using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
	/// Classe de base pour une connexion via System.data.IDbConnection ou autre;
	/// </summary>

	/////////////////////////////////////////////////////////////////////////////////
	//Encapsule les accès à la base de données
	public abstract class C2iDbDatabaseConnexion : C2iObjetServeur, IDatabaseConnexion, IObjetAttacheASession
	{

        private string m_strPrefixeTables = "";

		private const string c_strAttributMotDePasseCrypte = "PWCR";
		private const int c_nTailleBufferBlob = 2048;
		private string m_strConnexionString = "";
		private IDbTransaction	m_transaction = null;
		private int	m_nNbTransaction = 0;

		private IDbConnection m_connexion = null;
		private int m_nTimeOut = 60;
		public abstract IDataBaseCreator GetDataBaseCreator();
        private object m_lockerAdapter = new object();

        //Nom de table dans le contexte de donnée->Prefixe de la table
        private static Dictionary<string, string> m_dicPrefixesDeTables = new Dictionary<string, string>();

		//////////////////////////////////////////////////////////////////
		public C2iDbDatabaseConnexion( int nIdSession )
			:base ( nIdSession )
		{
			m_connexion = null;
			CGestionnaireObjetsAttachesASession.AttacheObjet(nIdSession, this);
		}

		/// /////////////////////////////////////////////////////
		public virtual void Dispose()
		{
			CGestionnaireObjetsAttachesASession.DetacheObjet(IdSession, this);
			if ( m_transaction != null )
			{
				m_transaction.Rollback();
			}
			if ( m_connexion != null && (m_connexion.State & ConnectionState.Open)!=0 )
				m_connexion.Close();
			m_connexion = null;
			m_transaction = null;
		}

		/// /////////////////////////////////////////////////////
		public virtual void OnCloseSession()
		{
			Dispose();
		}

		/// /////////////////////////////////////////////////////
		public string DescriptifObjetAttacheASession
		{
			get
			{
				return I.T("Database connection : @1|104",m_strConnexionString);
			}
		}

        /// /////////////////////////////////////////////////////
        public string GetPrefixeForTable(string strNomTableInContexte)
        {
            string strPrefixe = "";
            if (!m_dicPrefixesDeTables.TryGetValue(strNomTableInContexte, out strPrefixe))
            {
                IObjetServeur objServeur = CContexteDonnee.GetTableLoader(strNomTableInContexte, null, IdSession);
                if (objServeur != null)
                {
                    strPrefixe = CSc2iDataServer.GetInstance().GetPrefixeForType(objServeur.GetType());
                    if (strPrefixe == null)
                        strPrefixe = "";
                    m_dicPrefixesDeTables[strNomTableInContexte] = strPrefixe;
                }
                m_dicPrefixesDeTables[strNomTableInContexte] = strPrefixe;
            }
            return strPrefixe;
        }

		/////////////////////////////////////////////////////////
		public virtual string ConnexionString
		{
			get
			{
				return m_strConnexionString;
			}
			set
			{
				if (m_strConnexionString != value)
				{
					if(value.ToUpper().Contains("PWCR="))
						m_strConnexionString = DecryptConnexionString(value);
					else
						m_strConnexionString = value;
				}
			}
		}

        /////////////////////////////////////////////////////////
        public string PrefixeTables
        {
            get
            {
                return m_strPrefixeTables;
            }
            set
            {
                m_strPrefixeTables = value;
            }
        }

		private string DecryptConnexionString(string strConnexionString)
		{
			string strConnectDecrypt = strConnexionString;
			string[] eles = strConnectDecrypt.Split(';');
			strConnectDecrypt = "";
			foreach (string strEle in eles)
				if (strEle.Substring(0, c_strAttributMotDePasseCrypte.Length).ToUpper() == c_strAttributMotDePasseCrypte)
				{
					if(strEle.Contains("\""))
						strConnectDecrypt += "PASSWORD=\"" + C2iCrypto.Decrypte(strEle.Replace("\"","").Substring(c_strAttributMotDePasseCrypte.Length + 1)) + "\";";
					else if (strEle.Contains("'"))
						strConnectDecrypt += "PASSWORD='" + C2iCrypto.Decrypte(strEle.Replace("'","").Substring(c_strAttributMotDePasseCrypte.Length + 1)) + "';";
					else
						strConnectDecrypt += "PASSWORD=" + C2iCrypto.Decrypte(strEle.Substring(c_strAttributMotDePasseCrypte.Length + 1)) + ";";
				}
				else
					strConnectDecrypt += strEle + ";";
			strConnectDecrypt = strConnectDecrypt.Replace(";;", ";");
			return strConnectDecrypt;
		}

		/// /////////////////////////////////////////////////////
		public virtual IDbConnection GetConnexion()
		{
			return GetConnexion ( false );
		}

		/////////////////////////////////////////////////////////
		///Renvoie la connexion à la base de données
		protected virtual IDbConnection GetConnexion(bool bOpenIfClose)
		{
			if ( m_transaction == null || m_transaction.Connection == null )
            {
                m_connexion = GetNewConnexion(bOpenIfClose);
            }
			else if ( m_connexion == null )
			{
				m_connexion = GetNewConnexion(bOpenIfClose);
			}
			
			return m_connexion;
		}

		/// /////////////////////////////////////////////////////
		public virtual int CommandTimeOut
		{
			get
			{
				return m_nTimeOut;
			}
			set
			{
				m_nTimeOut = value;
			}
		}

		/////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne une nouvelle connnexion
		/// </summary>
		/// <param name="bOpenIfClose"></param>
		/// <returns></returns>
		protected abstract IDbConnection GetNewConnexion ( bool bOpenIfClose );
		


		/// /////////////////////////////////////////////////////
		public virtual CResultAErreur IsConnexionValide()
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				IDbConnection con = GetConnexion(true);
                if (!IsInTrans())
                    con.Close();
			}
			catch ( Exception e )
			{
				result.EmpileErreur(new CErreurException(e));
			}
			return result;
		}

		/////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne le niveau d'isolation pour les transactios
		/// </summary>
		/// <returns></returns>
		protected virtual IsolationLevel GetIsolationLevel()
		{
			return IsolationLevel.ReadUncommitted;
		}

		/////////////////////////////////////////////////////////
		public virtual CResultAErreur BeginTrans ( )
		{
			return BeginTrans ( GetIsolationLevel() );
		}

        protected virtual CResultAErreur JusteBeforeTrans(IDbConnection connexion)
        {
            return CResultAErreur.True;
        }

		/////////////////////////////////////////////////////////
		public virtual CResultAErreur BeginTrans( IsolationLevel isolationLevel )
		{
			lock(m_lockerAdapter)
			{
				CResultAErreur result = CResultAErreur.True;
				try
				{
					if ( m_nNbTransaction == 0 )
					{
						IDbConnection connexion = GetConnexion(true);
                        result = JusteBeforeTrans(connexion);
                        if (!result)
                            return result;
                        m_transaction = connexion.BeginTransaction(isolationLevel);
					}
					m_nNbTransaction++;
                }
				catch ( Exception e )
				{
					result.EmpileErreur ( new CErreurException ( e ) );
				}
				return result;
			}
		}

		/////////////////////////////////////////////////////////
		public event OnCommitTransEventHandler OnCommitTrans;
		public virtual CResultAErreur CommitTrans()
		{
			CResultAErreur result = CResultAErreur.True;
			m_nNbTransaction--;
            if ( m_transaction != null && m_nNbTransaction<1)
			{
				try
				{
                    IDbConnection connection = m_transaction.Connection;
					m_transaction.Commit();
                    connection.Close();
					m_transaction = null;
					if ( result  && OnCommitTrans != null )
						result = OnCommitTrans();
				}
				catch ( Exception e )
				{
					result.EmpileErreur ( new CErreurException ( e ) );
				}
			}
			return result;
		}

		/////////////////////////////////////////////////////////
		public IDbTransaction Transaction
		{
			get
			{
				return m_transaction;
			}
		}

		/////////////////////////////////////////////////////////
		public bool IsInTrans()
		{
			return ( m_transaction != null );
		}

		/////////////////////////////////////////////////////////
		public virtual CResultAErreur RollbackTrans()
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				m_nNbTransaction=0;
				new CDatabaseRegistre ( IdSession ).ResetCache();
				if ( m_transaction != null && m_nNbTransaction<1)
				{
                    IDbConnection connection = m_transaction.Connection;
					m_transaction.Rollback();
                    connection.Close();
					m_transaction = null;
				}
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
			}
			return result;
		}

		/////////////////////////////////////////////////////////
		public virtual bool AccepteTransactionsImbriquees
		{
			get
			{
				return true;
			}
		}



		////////////////////////////////////////////////////////
		/// <summary>
		/// les noms des parametres sont @1, @2, ... dans la requete
		/// </summary>
		/// <param name="strRequete"></param>
		/// <param name="parametres"></param>
		/// <returns></returns>
		public virtual IDataAdapter GetAdapterForRequete(string strRequete, object[] parametres)
		{
			IDbCommand command = GetConnexion().CreateCommand();
			command.Transaction = m_transaction;
			command.CommandText = strRequete;
			command.CommandTimeout = m_nTimeOut;
			int nIndex = 1;
			foreach ( object obj in parametres )
			{
				IDbDataParameter parametre = command.CreateParameter();
				parametre.ParameterName = GetNomParametre(nIndex.ToString());
				parametre.Value = obj;
				command.Parameters.Add ( parametre );
				nIndex++;
			}
			IDbDataAdapter adapter = GetNewAdapter(command);
			return adapter;
		}
			
		

		////////////////////////////////////////////////////////
		public virtual CResultAErreur RunStatement(String strStatement)
		{
			CResultAErreur result = CResultAErreur.False;
			try
			{
				IDbConnection con = GetConnexion(true);
				if ( con == null )
					return result;
				IDbCommand command = con.CreateCommand();
				if ( m_transaction!=null )
					command.Transaction = m_transaction;
				command.CommandTimeout = m_nTimeOut;
				command.CommandText = strStatement;
                lock (m_lockerAdapter)
                {
                    command.ExecuteNonQuery();
                }
				
				//Ferme la connexion
				if ( !IsInTrans())
					con.Close();
                command.Dispose();

				result.Result = true;
				return result;
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
				result.EmpileErreur(I.T("Error in the request '@1'|105",strStatement));
			}
			return result;
		}

		////////////////////////////////////////////////////////
		public virtual CResultAErreur ExecuteScalar(string strSelectClause, string strNomTableInDb, CFiltreData filtre)
		{
			CResultAErreur result = CResultAErreur.False;
			string strSql = "select "+strSelectClause+" from "+strNomTableInDb;
			try
			{
				
				string strWhere = "";
				bool bDistinct = false;
				IDbCommand command = GetConnexion().CreateCommand();
				command.CommandText = "";
				command.Transaction = m_transaction;
				string strJoin = "";
                string strPrefixeFrom = "";
				result = InitCommandForFiltre ( filtre, command, ref strWhere, ref strJoin, ref bDistinct, ref strPrefixeFrom );
				if ( !result )
				{
					result.EmpileErreur(I.T("Error in the filter|106"));
					throw new CExceptionErreur(result.Erreur);
				}

				strSql = GetSql(strSql, strPrefixeFrom, strJoin, strWhere);
			
				if ( bDistinct )
				{
					int nPos = strSql.ToLower().IndexOf("select");
					if ( nPos != 0)
						throw new Exception(I.T("'Select' was not found in the request @1|108",strSql));
					strSql = "select distinct "+strSql.Substring(nPos+6);
				}
				if ( filtre != null && filtre.SortOrder.Trim() != "" )
					strSql+=" order by "+filtre.SortOrder;

				command.CommandText = strSql;
				if ( command.Connection.State != ConnectionState.Open )
				{
					try
					{
						command.Connection.Open();
					}
					catch
					{
						result.EmpileErreur(I.T("Connection opening error|107"));
					}
				}

				command.CommandTimeout = m_nTimeOut;

                object obj = null;
                lock (m_lockerAdapter)
                {
                    obj = command.ExecuteScalar();
                }
				object objVal = obj==DBNull.Value?0:obj;

				//Ferme la connexion
				if ( !IsInTrans())
					command.Connection.Close();
                command.Dispose();

				result.Result = true;
				result.Data = objVal;
				return result;
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
				result.EmpileErreur(I.T("Error in the request '@1'|105",strSql));
			}
			return result;
		}

		////////////////////////////////////////////////////////
		public virtual CResultAErreur ExecuteScalar(string strStatement)
		{
			CResultAErreur result = CResultAErreur.False;
			try
			{
				IDbConnection con = GetConnexion(true);
				if ( con == null )
					return result;
				IDbCommand command = con.CreateCommand();
				if ( m_transaction!=null )
					command.Transaction = m_transaction;
				command.CommandText = strStatement;

				command.CommandTimeout = m_nTimeOut;

                object obj = null;
                lock (m_lockerAdapter)
                {
                    obj = command.ExecuteScalar();
                }
				object objVal = obj==DBNull.Value?0:obj;

				//Ferme la connexion
				if ( !IsInTrans())
					con.Close();
                command.Dispose();

				result.Result = true;
				result.Data = objVal;
				return result;
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
				result.EmpileErreur(I.T("Error in the request '@1'|105",strStatement));
			}
			return result;
		}

		////////////////////////////////////////////////////////
		public abstract int GetMaxIdentity ( string strNomTableInDb );


		/// /////////////////////////////////////////////////////
		protected abstract IDbDataAdapter GetNewAdapter( IDbCommand selectCommand );

		/// /////////////////////////////////////////////////////
		protected virtual IDbDataAdapter GetNewAdapter(string strSelectCommand)
		{
			IDbCommand command = GetConnexion().CreateCommand();
			command.CommandText = strSelectCommand;
			command.Transaction = m_transaction;
			command.CommandTimeout = m_nTimeOut;
			return GetNewAdapter ( command );
		}

		/// /////////////////////////////////////////////////////
		
		/// <summary>
		/// Retourne un nom de table qui passe dans une requête SQL (typiquement, en ajoutant
		/// des crochets [] autour du nom de table
		/// </summary>
		/// <param name="strNomTable"></param>
		/// <returns></returns>
		public virtual string GetNomTableForRequete ( string strNomTableInDb )
		{
			return "["+strNomTableInDb+"]";
		}

		/// /////////////////////////////////////////////////////
		public virtual IDataAdapter GetTableAdapter ( string strNomTableIndb )
		{
			IDbCommand command = GetConnexion().CreateCommand();
			command.CommandText = "Select * from "+ strNomTableIndb;
			if ( m_transaction != null )
				command.Transaction = m_transaction;
			IDbDataAdapter adapter = GetNewAdapter ( command );
			return adapter;
		}


		/// /////////////////////////////////////////////////////
		public virtual IDataAdapter GetSimpleReadAdapter(string strSql)
		{
			return GetSimpleReadAdapter ( strSql, null );
		}

		/// /////////////////////////////////////////////////////
		protected virtual string OptimiseRequete ( string strRequete )
		{
			return strRequete;
		}

		/// /////////////////////////////////////////////////////
		public virtual IDataAdapter GetSimpleReadAdapter(string strSql, CFiltreData filtre)
		{
			string strWhere = "";
			bool bDistinct = false;
			IDbCommand command = GetConnexion().CreateCommand();
			command.CommandText = "";
			command.Transaction = m_transaction;
			string strJoin = "";
            string strPrefixeFrom = "";
			CResultAErreur result = InitCommandForFiltre ( filtre, command, ref strWhere, ref strJoin, ref bDistinct, ref strPrefixeFrom );
			if ( !result )
			{
				result.EmpileErreur(I.T("Error in the filter|106"));
				throw new CExceptionErreur(result.Erreur);
			}

			 strSql = GetSql(strSql, strPrefixeFrom, strJoin, strWhere);
			
			/*strSql += strJoin + " ";

			if (strWhere.Trim() != "")
				if (strJoin.Trim() != "")
					strSql += " AND (" + strWhere + ")";
				else
					strSql += " WHERE (" + strWhere + ")";*/


			if ( bDistinct )
			{
				int nPos = strSql.ToLower().IndexOf("select");
				if ( nPos != 0 )
					throw new Exception(I.T("'Select' was not found in the request @1|108", strSql));
				strSql = "select distinct "+strSql.Substring(nPos+6);
			}
			if ( filtre != null && filtre.SortOrder.Trim() != "" )
				strSql+=" order by "+filtre.SortOrder;

			strSql = OptimiseRequete ( strSql );
			command.CommandText = strSql;

			
			command.CommandTimeout = m_nTimeOut;

			
			IDbDataAdapter adapter = GetNewAdapter ( command );
			if ( m_transaction != null )
			{
				adapter.SelectCommand.Transaction = m_transaction;
				if ( adapter.DeleteCommand != null )
					adapter.DeleteCommand.Transaction = m_transaction;
				if ( adapter.UpdateCommand != null )
					adapter.UpdateCommand.Transaction = m_transaction;
				if ( adapter.InsertCommand != null )
					adapter.InsertCommand.Transaction = m_transaction;
			}
			return adapter;
		}

		/// /////////////////////////////////////////////////////
		protected abstract IAdapterBuilder GetBuilder(string strNomTableInDb);

		/// /////////////////////////////////////////////////////
		protected abstract IAdapterBuilder GetBuilder ( Type tp );
		
		
		/// /////////////////////////////////////////////////////
		public virtual void PrepareTableToWriteDatabase ( DataTable table )
		{
		}

		/// /////////////////////////////////////////////////////
		public abstract void DesactiverIdAuto ( string strNomTable, bool bDesactiver );

		/// /////////////////////////////////////////////////////
		public abstract void DesactiverContraintes (  bool bDesactiver );

		/// /////////////////////////////////////////////////////
		public IDataAdapter GetNewAdapterForUpdate ( string strNomTableInDb )
		{
			IAdapterBuilder builder = GetBuilder ( strNomTableInDb );
			IDbDataAdapter adapter = (IDbDataAdapter)builder.GetNewAdapter(DataRowState.Added | DataRowState.Deleted | DataRowState.Modified, false );
			if ( m_transaction != null )
			{
				adapter.SelectCommand.Transaction = m_transaction;
				if ( adapter.DeleteCommand != null )
					adapter.DeleteCommand.Transaction = m_transaction;
				if ( adapter.UpdateCommand != null )
					adapter.UpdateCommand.Transaction = m_transaction;
				if ( adapter.InsertCommand != null )
					adapter.InsertCommand.Transaction = m_transaction;
			}
			return adapter;
		}

		protected abstract bool IsIdAutoDesactive ( string strNomTable );
		/// /////////////////////////////////////////////////////
		public virtual IDataAdapter GetNewAdapterForType(Type tp, DataRowState etatsAPrendreEnCompte, bool bDisableIdAuto, params string[] champsExclus)
		{
			IAdapterBuilder builder = GetBuilder ( tp);
			string strNomTable = CContexteDonnee.GetNomTableForType ( tp );
			bDisableIdAuto |= IsIdAutoDesactive ( strNomTable );
			IDataAdapter adapter = builder.GetNewAdapter( etatsAPrendreEnCompte, bDisableIdAuto , champsExclus);
			return adapter;
		}

		
		/// /////////////////////////////////////////////////////
		public virtual CResultAErreur ExecuteUpdate(string strNomTable, string strExpressionCle, Hashtable tableChamps)
		{
			CResultAErreur result = CResultAErreur.True;
			string strQuery = "update "+strNomTable+" set ";
			int nNbMAJ = 0;
			Hashtable tableCles = new Hashtable();
			int nPos = strExpressionCle.LastIndexOf('=');
			string strTest = strExpressionCle;
			string strNewCle ;
			while ( nPos > 0)
			{
				strTest = strTest.Substring(0, nPos ).Trim();
				nPos = strTest.LastIndexOf(' ');
				strNewCle = strTest.Substring ( nPos+1 ).Trim();
				tableCles[strNewCle.ToUpper()] = "";
				nPos = strTest.LastIndexOf ( '=' );
			}

			foreach ( String strCle in tableChamps.Keys )
			{
				if ( !tableCles.ContainsKey ( strCle.ToUpper() ))
				{
					strQuery+= strCle+"="+GetNomParametre(strCle)+",";
					nNbMAJ += 1;
				}
			}
			if ( nNbMAJ == 0 )
				//Rien à mettre à jour, tout va bien
				return CResultAErreur.True;
			strQuery = strQuery.Substring(0, strQuery.Length-1);
			strQuery += " where "+strExpressionCle;
			IDbConnection con = GetConnexion();
			if ( con == null )
				return result.SetFalse();
			IDbCommand command = con.CreateCommand();
			command.CommandText = strQuery;

			foreach ( string strCle in tableChamps.Keys )
			{
				if ( !tableCles.ContainsKey ( strCle.ToUpper() ) )
				{
					IDbDataParameter parametre = command.CreateParameter();
					parametre.Value = tableChamps[strCle];
					parametre.ParameterName = GetNomParametre(strCle);
					command.Parameters.Add ( parametre );
				}
			}
            try
            {
                int nNbLignes = 0;
                if (m_transaction != null)
                    command.Transaction = m_transaction;
                lock (m_lockerAdapter)
                {
                    nNbLignes = command.ExecuteNonQuery();
                }
                nNbLignes = nNbLignes + 1;
            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
                result.EmpileErreur(I.T("Error in the request '@1'|105", strQuery));
            }
            finally
            {
                command.Dispose();
            }
			return result;
		}


		public virtual CResultAErreur ExecuteInsert(string strNomTableInDb, Hashtable tableChamps)
		{
			CResultAErreur result = CResultAErreur.True;
			string strQuery = "insert into "+strNomTableInDb+"(";
			foreach ( String strCle in tableChamps.Keys )
				strQuery += strCle+",";
			strQuery = strQuery.Substring(0, strQuery.Length-1);
			strQuery += ") values(";
			foreach ( String strCle in tableChamps.Keys )
				strQuery += GetNomParametre(strCle)+",";
			strQuery = strQuery.Substring(0, strQuery.Length-1);
			strQuery += ")";
			IDbConnection con = GetConnexion();
			if ( con == null )
				return result.SetFalse();
			IDbCommand command = con.CreateCommand();
			command.CommandText = strQuery;

			ArrayList listeNulls = new ArrayList();
			foreach ( string strCle in tableChamps.Keys )
			{
				IDbDataParameter parametre = command.CreateParameter();
				parametre.ParameterName = GetNomParametre(strCle);
				parametre.Value = tableChamps[strCle];
				command.Parameters.Add ( parametre );
			}
            try
            {
                if (m_transaction != null)
                    command.Transaction = m_transaction;
                lock (m_lockerAdapter)
                {
                    command.ExecuteNonQuery();
                }
                if (listeNulls.Count > 0)
                {
                    strQuery = "Update " + strNomTableInDb + " set ";
                    foreach (string strNull in listeNulls)
                        strQuery += strNull + "=NULL,";
                    strQuery = strQuery.Substring(0, strQuery.Length - 1);
                    result = RunStatement(strQuery);
                }
            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
                result.EmpileErreur(I.T("Error in the request '@1'|105", strQuery));
            }
            finally
            {
                command.Dispose();
            }
			return result;
		}
		//-----------------------------------------------------------------------------------
		/// <summary>
		/// Renvoie un tableau contenant les noms des tables de la base de données.
		/// </summary>
		public abstract string[] TablesNames{get;}

		//-----------------------------------------------------------------------------------
		private Dictionary<string, string> m_dicTableInContexteFromTableInDb = new Dictionary<string,string>();
		public string GetNomTableInContexteFromNomTableInDb(string strNomTableInDb)
		{
			string strTableInContexte = "";
			if (m_dicTableInContexteFromTableInDb.TryGetValue(strNomTableInDb, out strTableInContexte))
				return strTableInContexte;
			List<string> listeMesTables = new List<string>(TablesNames);
			foreach (string strTmp in CContexteDonnee.GetNomTableForNomTableInDb(strNomTableInDb))
			{
				if (listeMesTables.Contains(strNomTableInDb))
				{
                    IObjetServeur tmpServeur = CContexteDonnee.GetTableLoader ( strTmp, null, IdSession );
                    IDatabaseConnexion cnx = CSc2iDataServer.GetInstance().GetDatabaseConnexion(IdSession, tmpServeur.GetType());
                    if (cnx != null && cnx.ConnexionString == ConnexionString)
                    {
                        m_dicTableInContexteFromTableInDb[strNomTableInDb] = strTmp;
                        return strTmp;
                    }
				}
			}
			return null;
		}

		public virtual string[] GetColonnesDeTable(string strNomTableInDb)
		{
			IDataAdapter adapter = GetTableAdapter(strNomTableInDb);
			DataSet ds = new DataSet();
			adapter.FillSchema(ds, SchemaType.Source);
            CUtilDataAdapter.DisposeAdapter(adapter);
			if (ds.Tables.Count > 0)
			{
				DataTable table = ds.Tables[0];
				List<string> strCols = new List<string>();
				foreach (DataColumn col in table.Columns)
					strCols.Add(col.ColumnName);
				return strCols.ToArray();
			}
			return new string[0];
		}
		
		/////////////////////////////////////////////////////////////////////////////////////
		public virtual CResultAErreur ReadBlob(string strNomTableInDb, string strChamp, CFiltreData filtre)
		{
			
			CResultAErreur result = CResultAErreur.True;
			try
			{
				string strWhere = "";
				bool bDistinct = false;
				IDbCommand command = GetConnexion(true).CreateCommand();
				command.Transaction = m_transaction;
				string strJoin = "";
                string strPrefixeFrom = "";
				result = InitCommandForFiltre ( filtre, command, ref strWhere, ref strJoin, ref bDistinct, ref strPrefixeFrom );
				if ( !result )
					return result;
				string strSql = "Select ";
				if ( bDistinct )
					strSql += "Distinct ";
				strSql += strNomTableInDb+"."+strChamp+" ";
				strSql += "from "+strNomTableInDb;

				strSql = GetSql(strSql, strPrefixeFrom, strJoin, strWhere);
				/*strSql += " "+strJoin;

				if(  strWhere.Trim() != "" )
					if (strJoin.Trim() != "")
						strSql += " AND (" + strWhere + ")";
					else
						strSql += " WHERE (" + strWhere + ")";*/



				command.CommandText = strSql;

				IDbDataAdapter adapter = GetNewAdapter ( command );
				adapter.SelectCommand.Transaction = m_transaction;
				DataSet ds = new DataSet();
                lock (m_lockerAdapter)
                {
                    adapter.Fill(ds);
                }
				DataTable table = ds.Tables["Table"];
				if ( table.Rows.Count != 0 )
				{
					if ( table.Rows[0][strChamp] == DBNull.Value )
						result.Data = null;
					else
					{
						byte[] data = (byte[])table.Rows[0][strChamp];
						if ( data != null && data.Length == 0 )
							result.Data = null;
						else
							result.Data = data;
					}
				}
				else
					result.Data = null;
                command.Dispose();
                CUtilDataAdapter.DisposeAdapter(adapter);
			}
			catch ( Exception e )
			{
				result.EmpileErreur(new CErreurException(e));
				result.EmpileErreur(I.T("Error during the blob reading|109"));
				return result;
			}
			return result;
		}

		/////////////////////////////////////////////////////////////////////////////////////
		public virtual CResultAErreur SaveBlob(string strNomTableInDb, string strChamp, CFiltreData filtre, byte[] data)
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				string strWhere = "";
				bool bDistinct = false;
				IDbCommand command = GetConnexion(true).CreateCommand();
				command.Transaction = m_transaction;
				
				string strJoin = "";
                string strPrefixeFrom = "";
				InitCommandForFiltre ( filtre, command, ref strWhere, ref strJoin, ref bDistinct, ref strPrefixeFrom );
				string strSql = "update ";
				if ( bDistinct || strJoin != "")
				{
					result.EmpileErreur(I.T("Save blob called with a filter which requires distinct or several tables|110"));
					return result;
				}
				strSql += " "+strNomTableInDb;
				if ( data == null )
					strSql += " set "+strChamp+"=null where ";
				else
					strSql += " set "+strChamp+"="+GetNomParametre("BLOBVAL")+" where ";
				strSql += strWhere;

				command.CommandText = strSql;
				if ( data != null )
				{
					IDbDataParameter parametre = command.CreateParameter();
					parametre.ParameterName = GetNomParametre("BLOBVAL");
                    parametre.DbType = DbType.Binary;
					parametre.Value = data;
					command.Parameters.Insert (0, parametre );
				}
                int nNb = 0;
                lock (m_lockerAdapter)
                {
                    nNb = command.ExecuteNonQuery();
                }
				result.Data = nNb;
                command.Dispose();

			}
			catch ( Exception e )
			{
				result.EmpileErreur(new CErreurException(e));
				result.EmpileErreur(I.T("Error during the BLOB @1 saving of the table @2|111",strChamp,strNomTableInDb));
			}
			return result;
		}

		/////////////////////////////////////////////////////////////////////////////////////
		public virtual string GetSqlForAliasDecl(string strAlias)
		{
			return " as " + strAlias;
		}

		/////////////////////////////////////////////////////////////////////////////////////
		public virtual void CreateJoinPourLiens (
			CFiltreData filtre,
			CArbreTable arbreTables, 
			CComposantFiltre composantFiltre, 
			ref bool bDistinct,
			ref string strJoin,
			ref string strWhere,
            ref string strPrefixeFrom)
		{
			strJoin = "";
            arbreTables.SortTablesLiees();
			foreach ( CArbreTableFille arbreFils in arbreTables.TablesLiees )
			{
				if ( arbreFils.IsLeftOuter )
					strJoin += " LEFT OUTER JOIN ";
				else
					strJoin += " INNER JOIN ";
				if ( arbreFils.Relation.IsRelationFille )
					bDistinct = true;
				string strTable;
				strTable = CContexteDonnee.GetNomTableInDbForNomTable(arbreFils.NomTable)+
					GetSqlForAliasDecl(arbreFils.Alias);
				string strSuiteFrom = "";
				string strSuiteWhere = "";
				CreateJoinPourLiens ( filtre, arbreFils, composantFiltre, ref bDistinct, ref strSuiteFrom, ref strSuiteWhere, ref strPrefixeFrom );
				if ( strSuiteFrom.Trim() != "" )
					strTable = "("+strTable;
				strTable+=strSuiteFrom;
				if ( strSuiteFrom.Trim() != "" )
					strTable +=")";
				strJoin += strTable;
				strJoin += " ON (";
				CInfoRelationComposantFiltre relation = arbreFils.Relation;
				string strAliasParent, strAliasFille;
				string strTableDependante = "";
				if ( relation.IsRelationFille)
				{
					strAliasParent = arbreTables.Alias;
					strAliasFille = arbreFils.Alias;
					strTableDependante = arbreFils.NomTable;
				}
				else
				{
					strAliasParent = arbreFils.Alias;
					strAliasFille = arbreTables.Alias;
					strTableDependante = arbreTables.NomTable;
				}
				string strJointure = relation.GetJoinClause(strAliasParent, "", strAliasFille, "");

				string strComplementVersion = "";
				if (EstCeQueLaTableGereLesVersions(strTableDependante) && !filtre.IntegrerLesElementsSupprimes)
				{
					strComplementVersion = "(" + strAliasFille + "." + CSc2iDataConst.c_champIsDeleted + "=0 or " +
						strAliasFille+"."+CSc2iDataConst.c_champIsDeleted + " is null)";
				}
				string strIdsVersionsALire = filtre.GetStringListeIdsVersionsALire(',');
				if (EstCeQueLaTableGereLesVersions(strTableDependante))
				{
					if (strComplementVersion != "")
						strComplementVersion += " and ";
					if (strIdsVersionsALire == null)
						strComplementVersion += strAliasFille + "." + CSc2iDataConst.c_champIdVersion + " is null";
					else 
						strComplementVersion += "(" + strAliasFille + "." + CSc2iDataConst.c_champIdVersion + " in (" + strIdsVersionsALire + ") or " +
							strAliasFille+"."+CSc2iDataConst.c_champIdVersion + " is null)";
				}
				if (strComplementVersion != "")
				{
					if (strJoin != "")
						strJoin += "(" + strJointure + ") and (";
					else
                        strJoin += "(";
					strJoin += strComplementVersion + "))";
				}
				else
					strJoin += strJointure + ")";
				if ( composantFiltre != null )
					composantFiltre.DefinitAlias ( arbreFils.CheminRelations, arbreFils.Alias );
                if (filtre is CFiltreDataAvance)
                {
                    foreach (CComposantFiltreChamp champ in ((CFiltreDataAvance)filtre).ChampsAAjouterAArbreTable)
                        champ.DefinitAlias(arbreFils.CheminRelations, arbreFils.Alias);
                }
			}
		}

		/// <summary>
		/// Nom de la table->Est-ce que cette table sait gérer les suppressions
		/// </summary>
		private static Dictionary<string, bool> m_tableAvecFlagSuppression = new Dictionary<string,bool>();
		protected bool EstCeQueLaTableGereLesVersions(string strNomTable)
		{
			bool bVal;
			if (m_tableAvecFlagSuppression.TryGetValue(strNomTable, out bVal))
				return bVal;
			try
			{
				Type tp = CContexteDonnee.GetTypeForTable(strNomTable);
				if (tp != null)
				{
					CStructureTable structure = CStructureTable.GetStructure(tp);
					if (structure != null)
					{
						foreach (CInfoChampTable info in structure.Champs)
						{
							if (info.NomChamp == CSc2iDataConst.c_champIsDeleted)
							{
								m_tableAvecFlagSuppression[strNomTable] = true;
								return true;
							}
						}
					}

				}
			}
			catch
			{
			}
			m_tableAvecFlagSuppression[strNomTable] = false;
			return false;
		}

		/////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne le nom du paramètre. Pour sql, ajoute @ devant, pour oracle, ajoute :
		/// etc...
		/// </summary>
		/// <param name="strParametre"></param>
		/// <returns></returns>
		public abstract string GetNomParametre ( string strParametre );			

		/////////////////////////////////////////////////////////////////////////////////////
		public abstract IFormatteurFiltreDataToString GetFormatteurFiltre();


        /////////////////////////////////////////////////////////////////////////////////////
        public class CParametreRequeteDatabase
        {
            public readonly string NomParametre = "";
            public readonly object Valeur = null;

            public CParametreRequeteDatabase(
                string strNomParametre,
                object valeur)
            {
                NomParametre = strNomParametre;
                Valeur = valeur;
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////
        public virtual CResultAErreur PrepareRequeteFromFiltre(CFiltreData filtre,
            ref string strWhere,
            ref string strJoin,
            ref bool bDistinct,
            ref string strPrefixeFrom,
            ref IEnumerable<CParametreRequeteDatabase> parametres)
        {
            CResultAErreur result = CResultAErreur.True;
            Hashtable tablesRelation = new Hashtable();
            bDistinct = false;
            strJoin = "";
            strWhere = "";
            string strAliasPrincipal = "";
            if (filtre != null && filtre is CFiltreDataAvance)
            {
                CFiltreDataAvance filtreAvance = (CFiltreDataAvance)filtre;


                result = filtreAvance.GetArbreTables(); ;
                if (!result)
                    return result;
                CArbreTable arbre = (CArbreTable)result.Data;
                string strNomTableIndb = CContexteDonnee.GetNomTableInDbForNomTable(filtreAvance.TablePrincipale);
                filtreAvance.ComposantPrincipal.DefinitAlias(new CInfoRelationComposantFiltre[0], strNomTableIndb);
                strAliasPrincipal = strNomTableIndb;
                CreateJoinPourLiens(
                    filtre,
                    arbre,
                    filtreAvance.ComposantPrincipal,
                    ref bDistinct,
                    ref strJoin,
                    ref strWhere,
                    ref strPrefixeFrom);
            }
            if (filtre != null)
            {
                if (strWhere.Trim() != "")
                    strWhere = "(" + strWhere + ") and (" + GetFormatteurFiltre().GetString(filtre) + ")";
                else
                    strWhere = GetFormatteurFiltre().GetString(filtre);
            }
            if (filtre != null && filtre.HasFiltre)
            {
                List<CParametreRequeteDatabase> lstParametres = new List<CParametreRequeteDatabase>();
                parametres = lstParametres;
                int nNbParametres = 1;
                foreach (object param in filtre.Parametres)
                {
                    string strName =  GetNomParametre("PARAM" + nNbParametres.ToString());
                    object value = null;
                    if (param is CDateTimeEx)
                        value = ((CDateTimeEx)param).DateTimeValue;
                    else
                        value = param;
                    lstParametres.Add ( new CParametreRequeteDatabase(strName, value ) );
                    nNbParametres++;
                }
            }
            //Order by
            if (filtre != null && filtre is CFiltreDataAvance && filtre.SortOrder.Trim() != "")
            {
                string[] strOrders = filtre.SortOrder.Split(',');
                string strNewOrder = "";
                if (strAliasPrincipal != "")
                    strAliasPrincipal = strAliasPrincipal + ".";
                foreach (string strOrder in strOrders)
                    strNewOrder += strAliasPrincipal + strOrder.TrimStart() + ",";
                if (strNewOrder.Length > 0)
                    strNewOrder = strNewOrder.Substring(0, strNewOrder.Length - 1);
                filtre.SortOrder = strNewOrder;
            }
            return result;
        }

		/////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Ajoute les paramètres à la commande, retourne 
		/// -la clause where
		/// -la liste des tables à
		///		mettre dans la clause from (liées au filtre, la table principale du filtre
		///		n'est pas ajoutée) 
		/// -un bool indiquant si la mention distincte est conseillée
		///		La mention distincte est conseillée si une table fille de la table principale
		///		d'un filtre est appelée
		/// </summary>
		/// <param name="command"></param>
		/// <param name="strWhere"></param>
		/// <param name="strFromForFiltre"></param>
		/// <param name="bDistinct"></param>
		/// <returns></returns>
		protected virtual CResultAErreur InitCommandForFiltre( 
			CFiltreData filtre, 
			IDbCommand command, 
			ref string strWhere, 
			ref string strJoin,
			ref bool bDistinct,
            ref string strPrefixeFrom )
		{
			CResultAErreur result = CResultAErreur.True;
			bDistinct = false;
			strJoin = "";
			strWhere = "";
            IEnumerable<CParametreRequeteDatabase> lstParametres = null;

            PrepareRequeteFromFiltre(filtre,
                ref strWhere,
                ref strJoin,
                ref bDistinct,
                ref strPrefixeFrom,
                ref lstParametres);

            if ( lstParametres != null )
            {
                foreach ( CParametreRequeteDatabase p in lstParametres )
                {
					IDbDataParameter parametre = command.CreateParameter();
					parametre.ParameterName = p.NomParametre;
                    parametre.Value = p.Valeur;
                    InitParameterType(parametre, p.Valeur);
					command.Parameters.Add ( parametre );
				}
			}
			return result;
		}

        /////////////////////////////////////////////////////////////////////////////////////
        public virtual void InitParameterType(IDbDataParameter parametre, object valeur)
        {
        }

		/////////////////////////////////////////////////////////////////////////////////////
		public virtual DbType GetDbType ( Type tp )
		{
			if ( tp == typeof(String) )
				return DbType.String;
			if (tp == typeof(int) || tp == typeof(int?))
				return DbType.Int32;
			if (tp == typeof(double) || tp == typeof(double?))
				return DbType.Double;
			if ( tp == typeof(DateTime) || tp == typeof(CDateTimeEx) || tp == typeof(DateTime?))
				return DbType.DateTime;
			if (tp == typeof(bool) || tp == typeof(bool?))
				return DbType.Boolean;
			if ( tp == typeof(CDonneeBinaireInRow) )
				return DbType.Binary;
			if ( tp == typeof(byte[]) )
				return DbType.Binary;
			if (tp == typeof(Decimal) || tp == typeof(Decimal?))
				return DbType.Decimal;
			if (tp == typeof(Single) || tp == typeof(Single?))
				return DbType.Single;
			if (tp == typeof(Int16) || tp == typeof(Int16?))
				return DbType.Int16;
			if (tp == typeof(Byte) || tp == typeof(Byte?))
				return DbType.Byte;
			if ( tp.IsEnum )
				return DbType.Int32;
			Console.WriteLine(I.T("Data type without Sql conversion : @1|112",tp.ToString()));
			return DbType.String;
		}

		/////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Initialise une clause set d'un update pour les champs demandés
		/// Retour contient les paramètres créés dans l'ordre de strChamps
		/// </summary>
		/// <param name="command"></param>
		/// <param name="strChamps"></param>
		/// <param name="valeurs"></param>
		/// <returns></returns>
		protected virtual IDbDataParameter[] InitUpdateSetClause ( IDbCommand command, ref string strUpdateClause, string[] strChamps, object[] valeurs )
		{
			IDbDataParameter[] retour = new IDbDataParameter[strChamps.Length];
			int nChamp;
			for ( nChamp = 0; nChamp < strChamps.Length; nChamp++ )
			{
				IDbDataParameter parametre = command.CreateParameter();
				parametre.ParameterName = GetNomParametre("PARAM"+(command.Parameters.Count+1).ToString());
				parametre.DbType = GetDbType ( valeurs[nChamp].GetType() );
				retour[nChamp] = parametre;
				command.Parameters.Add ( parametre );
				strUpdateClause += strChamps[nChamp]+"="+retour[nChamp].ParameterName+" ";
			}
			if ( strUpdateClause.Length > 0 )
				strUpdateClause = strUpdateClause.Substring(0, strUpdateClause.Length-1);
			return retour;
		}

		/////////////////////////////////////////////////////////////////////////////////////
		public virtual CResultAErreur SetValeurChamp( string strNomTableInDb, string[] strChamps, object[] valeurs, CFiltreData filtre )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				if ( filtre == null || !filtre.HasFiltre)
				{
					//Ca peut être dangereux !
					result.EmpileErreur(I.T("'SetValeurChamp' call without definite filter|113"));
					return result;
				}
				if ( filtre is CFiltreDataAvance )
				{
					result.EmpileErreur(I.T("'SetValeurChamp' function not implemented with advanced filter|114"));
					return result;
				}
				IDbCommand command = GetConnexion().CreateCommand();
				command.Transaction = m_transaction;
				string strWhere = "";
				bool bDistinct = false;
				string strJoin = "";
                string strPrefixeFrom = "";
				result = InitCommandForFiltre ( filtre, command, ref strWhere, ref strJoin, ref bDistinct, ref strPrefixeFrom );
				if ( !result )
					return result;
				if ( bDistinct || strJoin!="")
				{
					result.EmpileErreur(I.T("Multiple tables in a 'SetValeurChamp' request|115"));
					return result;
				}
				if ( strWhere == "" )
				{
					result.EmpileErreur(I.T("'SetValeurChamps' attempt on a table in where clause|116"));
					return result;
				}
				string strUpdateClause = "";
				IDbDataParameter[] parametres = InitUpdateSetClause ( command, ref strUpdateClause, strChamps, valeurs );
				string strSql = "Update "+strNomTableInDb+" set "+strUpdateClause+" where "+strWhere;

				command.CommandText = strSql;
				for ( int nChamp = 0; nChamp < strChamps.Length; nChamp++ )
				{
					parametres[nChamp].Value = valeurs[nChamp];
					
				}
                lock (m_lockerAdapter)
                {
                    command.ExecuteNonQuery();
                }
                command.Dispose();
				

			}
			catch ( Exception e )
			{
				result.EmpileErreur(new CErreurException(e));
			}
			return result;
		}	
		
		/////////////////////////////////////////////////////////////////////////////////////
		public abstract string GetStringForRequete ( object valeur );


		/////////////////////////////////////////////////////////////////////////////////////
		public abstract int CountRecords ( string strNomTableInDb, CFiltreData filtre );

		/// /////////////////////////////////////////////////
		protected virtual string GetStringAgregation(OperationsAgregation operation, string strChamp)
		{
			switch ( operation )
			{
				case OperationsAgregation.Max :
					return "max("+strChamp+")";
				case OperationsAgregation.Min :
					return "min("+strChamp+")";
				case OperationsAgregation.Average :
					return "avg("+strChamp+")";
				case OperationsAgregation.Number :
					return "count("+strChamp+")";
				case OperationsAgregation.DistinctNumber:
					return "count(distinct("+strChamp+"))";
				case OperationsAgregation.Sum :
					return "sum("+strChamp+")";
			}
			return strChamp;
		}

		/////////////////////////////////////////////////////////////////////////////////////
		public virtual string GetSql(string strSelect, string strPrefixeFrom, string strJoin, string strWhere)
		{
            if (strPrefixeFrom.Trim().Length > 0)
            {
                int nPos = strSelect.ToUpper().IndexOf("FROM");
                if (nPos > 0)
                {
                    nPos += 4;
                    string strTmp = strSelect.Substring(0, nPos);
                    strTmp += " " + strPrefixeFrom + " ";
                    strTmp += strSelect.Substring(nPos);
                    strSelect = strTmp;
                }
            }
			string strSql = strSelect + " " + strJoin;
			if (strWhere.Trim() != "")
				strSql += " WHERE " + strWhere;
            
			return strSql;
		}

		/////////////////////////////////////////////////////////////////////////////////////
		public virtual CResultAErreur ExecuteRequeteComplexe(C2iChampDeRequete[] champs, CArbreTable arbreTables, CFiltreData filtre)
		{
			CResultAErreur result = CResultAErreur.True;

			//En requête imbriquée pour le filtre
			IDbDataAdapter adapter = null;
			
			//Crée la requête principale
			string strSelect = "";
			string strJoin = "";
			string strGroupBy = "";
            bool bAgregate = false;//Indique s'il faut utiliser le group by
            //On fait de l'agrégation si l'utilisateur a indiqué un group by ou
            //si au moins un champ a une opération d'agrégation
			foreach ( C2iChampDeRequete champ in champs )
			{
				result = champ.GetStringSql();
				if ( !result )
					return result;
				string strReqChamp = GetStringAgregation ( champ.OperationAgregation, result.Data.ToString() );
				/*if ( strReqChamp != "" )
					strReqChamp += "("+result.Data.ToString()+")";
				else
					strReqChamp = result.Data.ToString();*/

				if ( champ.OperationAgregation == OperationsAgregation.None || champ.GroupBy )
					strGroupBy += strReqChamp+",";
                
                if (champ.GroupBy || champ.OperationAgregation != OperationsAgregation.None)
                    bAgregate = true;
				
				strReqChamp += GetSqlForAliasDecl ( champ.NomChamp );
				strSelect += strReqChamp+",";
			}
			strSelect = strSelect.Substring(0, strSelect.Length-1);
			if ( strGroupBy.Length > 0 )
				strGroupBy = strGroupBy.Substring(0, strGroupBy.Length-1);

			string strWhere = "";
			bool bDistinct = false;
			IDbCommand command = GetConnexion().CreateCommand();
			command.CommandText = "";
			command.Transaction = m_transaction;
            string strPrefixeFrom = "";
			if ( filtre != null )
			{
				filtre.SortOrder = "";
				InitCommandForFiltre ( filtre, command, ref strWhere, ref strJoin, ref bDistinct, ref strPrefixeFrom );
			}
			string strWhereDeJoin = "";
			CreateJoinPourLiens ( filtre, arbreTables, null, ref bDistinct, ref strJoin, ref strWhereDeJoin, ref strPrefixeFrom );
			if (strWhereDeJoin.Trim() != "")
				strWhere = "(" + strWhereDeJoin + ") and (" + strWhere + ")";

            //SC 04/09/2016 : le bdistinct n'était pas utilisé !!!
			string strRequete = GetSql("SELECT " +
                (bDistinct?"DISTINCT ":"")+
                strSelect + 
                " FROM " +arbreTables.NomTable, strPrefixeFrom, strJoin, strWhere);

			/*string strRequete = "SELECT " + strSelect + " FROM " + arbreTables.NomTable + strJoin + " ";

			if (strWhere.Trim() != "")
				strRequete += " WHERE (" + strWhere + ")";*/
			
			
			if ( strGroupBy != "" && bAgregate)
				strRequete += " GROUP BY " + strGroupBy;

			command.CommandText = strRequete;

			adapter = GetNewAdapter( command );

			DataSet ds = new DataSet();
            try
            {
                //adapter.MissingSchemaAction = MissingSchemaAction.
                //DataTable
                //ITableMapping mapping = adapter.TableMappings.Add("","").
                //ds.Tables[3].Map
                FillAdapter(adapter, ds);
            }

            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
                return result;
            }
            finally
            {
                command.Dispose();
                CUtilDataAdapter.DisposeAdapter(adapter);
            }

			DataTable tableRetour = ds.Tables[0];
            if ( tableRetour.Columns.Count == 0 )
            {
                foreach ( C2iChampDeRequete champ in champs )
                {
                    Type tp = typeof(string);
                    if (champ.TypeDonneeFinalForce != null)
                        tp = champ.TypeDonneeFinalForce;
                    else
                        if (champ.TypeDonnee != null)
                            tp = champ.TypeDonnee;
                    if (tp.IsGenericType && tp.GetGenericTypeDefinition() == typeof(Nullable<>))
                        tp = tp.GenericTypeArguments[0];
                    try
                    {
                        DataColumn col = new DataColumn(champ.NomChamp, tp);
                        col.AllowDBNull = true;
                        tableRetour.Columns.Add(col);
                    }
                    catch { }
                }
            }
			tableRetour.TableName = arbreTables.NomTable;
			result.Data = new CDataTableFastSerialize( tableRetour );
			return result;
		}

		//public virtual DataTable PrepareDataTableForRequeteComplexe(C2iChampDeRequete[] champs, CArbreTable arbreTables, CFiltreData filtre)
		//{ 

		//}

        //-------------------------------------------------------------------------
        /// <summary>
        /// Permet de vérouiller l'appel d'un seul DataAdapter à la fois par connexion
        /// </summary>
        /// <param name="adapter"></param>
        /// <param name="ds"></param>
        public virtual void FillAdapter(IDataAdapter adapter, DataSet ds)
        {
            lock (m_lockerAdapter)
            {
                adapter.Fill(ds);
            }
        }
        //--------------------------------------------------------------------------
        public virtual void FillAdapter(DbDataAdapter adapter, DataTable dt)
        {
            lock (m_lockerAdapter)
            {
                adapter.Fill(dt);
            }
        }
        //--------------------------------------------------------------------------
        public virtual void FillAdapter(DbDataAdapter adapter, DataSet ds, int startRecord, int maxRecords, string srcTable)
        {
            lock (m_lockerAdapter)
            {
               adapter.Fill(ds, startRecord, maxRecords, srcTable);
            }
        }
        //--------------------------------------------------------------------------
        public virtual void FillAdapter(IDataAdapterARemplissagePartiel adapter, DataSet ds, int startRecord, int maxRecords, string srcTable)
        {
            lock (m_lockerAdapter)
            {
                adapter.Fill(ds, startRecord, maxRecords, srcTable);
            }
        }


	}
}
