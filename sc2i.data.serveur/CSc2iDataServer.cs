using System;
using System.Collections;
#if PDA
#else
using System.Runtime.Remoting;
#endif

using sc2i.data;
using sc2i.multitiers.server;
using sc2i.common;
using sc2i.multitiers.client;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Constantes
	/// </summary>
	[AutoExec("Autoexec")]
	public class CSc2iDataServer : MarshalByRefObject, IDisposable, IFournisseurServiceTransactionPourSession
	{
		//Table IDConnexion->CDefinitionConnexionDataSource
		private static Hashtable m_tableDefinitionsConnexions = new Hashtable();
		
		//Stock pour chaque type d'objet serveur l'id de connexion à appeler
		//si le type n'est pas trouvé dans la table, la connexion par défaut est utilisée
		private static Hashtable m_tableTypeObjetServeurToIdConnexion = new Hashtable();
		
		//Hashtable de Nom assembly (string complet) vers l'id de connexion à utiliser.
		//Si l'assembly n'est pas trouvé dans cette table, la connexion par défaut est utilisée
		private static Hashtable m_tableAssemblyToIdConnexion = new Hashtable();
		
		private static CSc2iDataServer m_instance = null;

		private static string m_strIdConnexionParDefaut = "";


		//private Type m_typeConnexion = null;
		//private string m_strDatabaseConnexionString = "";

		private Hashtable	 m_tableSessions = new Hashtable();

		
		/// <summary>
		/// Stocke les connexions pour une session
		/// </summary>
		private class CConnexionsParSession : 
            C2iObjetServeur, 
            IDisposable, 
            IServiceTransactions,
            IObjetAttacheASession
		{
			//Nombre d'appels à BeginTrans appelés sans commit ni rollback.
			//cette variable est utile si on ouvre une connexion alors qu'une autre
			//est déjà ouverte et en transaction. Il est important que les deux bases
			//aient le même nombre de transactions ouvertes pour que le système
			//se comporte correctement
			private int m_nNbTransactionsOuvertes = 0;


			//Hashtable IdConnexion->class IDatabaseConnexion
			private Hashtable m_connexions = new Hashtable();
			
			////////////////////////////////////////////////////////////////////////
			public  CConnexionsParSession ( int nIdSession )
				:base(nIdSession)
			{
                CGestionnaireObjetsAttachesASession.AttacheObjet(nIdSession, this);
			}

			////////////////////////////////////////////////////////////////////////
			public void Dispose()
			{
                CGestionnaireObjetsAttachesASession.DetacheObjet(m_nIdSession, this);
				foreach ( IDatabaseConnexion connexion in m_connexions.Values )
				{
					try
					{
						connexion.Dispose();
					}
					catch{}
				}
                m_connexions.Clear();
			}

			////////////////////////////////////////////////////////////////////////
			public IDatabaseConnexion GetConnexion ( string strIdConnexion )
			{
				if ( strIdConnexion == "" )
					strIdConnexion = m_strIdConnexionParDefaut;
				IDatabaseConnexion connexion = (IDatabaseConnexion)m_connexions[strIdConnexion];
				if ( connexion == null )
				{
					if ( !CGestionnaireSessions.IsSessionOpenStatic ( IdSession ) )
						throw new Exception (I.T("The session has been closed|198"));
					CDefinitionConnexionDataSource def = (CDefinitionConnexionDataSource)m_tableDefinitionsConnexions[strIdConnexion];
					connexion = (IDatabaseConnexion)Activator.CreateInstance(def.TypeConnexion, new object[] {m_nIdSession});
					m_connexions[strIdConnexion] = connexion;
					connexion.ConnexionString = def.ConnexionString;
                    connexion.PrefixeTables = def.PrefixeTables;

					//Se met en phase avec les transactions des autres connexions
					for ( int nTrans = 0; nTrans < m_nNbTransactionsOuvertes; nTrans++ )
					{
						connexion.BeginTrans( m_lastIsolationLevel );
					}
				}
				return connexion;
			}

			////////////////////////////////////////////////////////////////////////
			public void SetConnexion ( string strIdConnexion, IDatabaseConnexion connexion )
			{
				m_connexions[strIdConnexion] = connexion;
			}

			private System.Data.IsolationLevel m_lastIsolationLevel = System.Data.IsolationLevel.ReadUncommitted;

			////////////////////////////////////////////////////////////////////////
			public CResultAErreur BeginTrans()
			{
				return BeginTrans ( System.Data.IsolationLevel.ReadUncommitted );
			}

			////////////////////////////////////////////////////////////////////////
			public CResultAErreur BeginTrans( System.Data.IsolationLevel isolationLevel )
			{
				CResultAErreur result = CResultAErreur.True;
				m_lastIsolationLevel = isolationLevel;
				foreach ( IDatabaseConnexion connexion in m_connexions.Values )
				{
					result = connexion.BeginTrans( isolationLevel );
					if ( !result )
						return result;
				}
				m_nNbTransactionsOuvertes++;
				return result;
			}

			////////////////////////////////////////////////////////////////////////
			public CResultAErreur RollbackTrans()
			{
				CResultAErreur result = CResultAErreur.True;
				foreach ( IDatabaseConnexion connexion in m_connexions.Values )
				{
					result = connexion.RollbackTrans();
					if ( !result )
						return result;
				}
				m_nNbTransactionsOuvertes--;
				return result;
			}

			////////////////////////////////////////////////////////////////////////
			public CResultAErreur CommitTrans()
			{
				CResultAErreur result = CResultAErreur.True;
				foreach ( IDatabaseConnexion connexion in m_connexions.Values )
				{
					result = connexion.CommitTrans();
					if ( !result )
					{
						C2iEventLog.WriteErreur(I.T("ERROR WHILE TRANSATION CLOSING :\r\nConnection: @1\r\nSession: @2\r\nConnection number for this session: @3|199",connexion.ConnexionString,IdSession.ToString(), m_connexions.Count.ToString()));
						return result;
					}
				}
				m_nNbTransactionsOuvertes--;
				return result;
			}

			////////////////////////////////////////////////////////////////////////
			public bool AccepteTransactionsImbriquees
			{
				get
				{
					return true;
				}
			}



			////////////////////////////////////////////////////////////////////////
			public void ResetConnexion ( string strIdConnexion )
			{
				IDatabaseConnexion connexion = (IDatabaseConnexion)m_tableDefinitionsConnexions[strIdConnexion];
				if ( connexion != null )
					try
					{
						connexion.Dispose();
					}
					catch
					{
					}
				m_tableDefinitionsConnexions[strIdConnexion] = null;
			}

            //---------------------------------
            public string DescriptifObjetAttacheASession
            {
                get { return "Connection for session cache"; }
            }

            //---------------------------------
            public void OnCloseSession()
            {
                CSc2iDataServer.OnCloseSession ( IdSession );
                Dispose();
            }

        }
                
		///////////////////////////////////////////
		private CSc2iDataServer( CDefinitionConnexionDataSource defConnexionParDefaut )
		{
			m_tableDefinitionsConnexions[defConnexionParDefaut.IdConnexion] = defConnexionParDefaut;
			m_strIdConnexionParDefaut = defConnexionParDefaut.IdConnexion;
		}

		////////////////////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CSessionClientSurServeur.RegisterFournisseurTransactions ( GetInstance() );
		}

		////////////////////////////////////////////////////////////////////////
		public static string IdConnexionParDefaut
		{
			get
			{
				return m_strIdConnexionParDefaut;
			}
		}

		///////////////////////////////////////////
		public void Dispose()
		{
			//Ferme toutes les connexions
			foreach ( CConnexionsParSession connexions in m_tableSessions.Values )
			{
				connexions.Dispose();
			}
            m_tableSessions.Clear();
		}

		///////////////////////////////////////////
		public static CResultAErreur Init ( CDefinitionConnexionDataSource defConnexionParDefaut )
		{
			if ( m_instance != null )
			{
				m_instance.Dispose();
				m_instance = null;
			}
			CResultAErreur result = CResultAErreur.True;
			m_instance = new CSc2iDataServer ( defConnexionParDefaut );
			return result;
		}

		////////////////////////////////////////////////////////////////////////
		public IServiceTransactions GetServiceTransaction ( int nIdSession )
		{

			return GetConnexions( GetIdSessionForDb ( nIdSession ) );
		}

		/// //////////////////////////////////////////////////////////////////////////
		public int PrioriteTransaction
		{
			get
			{
				return 1000;
			}
		}

		///////////////////////////////////////////
		public static void AddDefinitionConnexion ( CDefinitionConnexionDataSource defConnexionParDefaut )
		{
			m_tableDefinitionsConnexions[defConnexionParDefaut.IdConnexion] = defConnexionParDefaut;
		}

		///////////////////////////////////////////
		/// <summary>
		/// Retourne true si l'id de connexion existe.
		/// </summary>
		/// <param name="strIdConnexion"></param>
		public static bool ExitsConnexion ( string strIdConnexion )
		{
			return m_tableDefinitionsConnexions[strIdConnexion] != null;
		}

		////////////////////////////////////////////////////////////////////////
		public static void SetIdConnexionForClasse ( Type classe, string strIdConnexion )
		{
			m_tableTypeObjetServeurToIdConnexion[classe] = strIdConnexion;
		}

		////////////////////////////////////////////////////////////////////////
		public static void SetIdConnexionForAssembly ( string strAssembly, string strIdConnexion )
		{
			m_tableAssemblyToIdConnexion[strAssembly] = strIdConnexion;
		}

		///////////////////////////////////////////
		public static CSc2iDataServer GetInstance()
		{
			if ( m_instance == null )
				throw new Exception(I.T("CSc2iDataServer has not been initialized|200"));
			return m_instance;
		}

		///////////////////////////////////////////
		private CConnexionsParSession GetConnexions ( int nIdSession )
		{
			nIdSession = GetIdSessionForDb(nIdSession);
			CConnexionsParSession connexions = (CConnexionsParSession)m_tableSessions[nIdSession];
			if ( connexions == null )
			{
				connexions = new CConnexionsParSession(nIdSession);
				m_tableSessions[nIdSession] = connexions;
			}
			return connexions;
		}

		///////////////////////////////////////////
		/// <summary>
		/// Retourne l'id de la session qui gère les transaction pour cette session
		/// </summary>
		/// <param name="nIdSession"></param>
		/// <returns></returns>
		private int GetIdSessionForDb(int nIdSession)
		{
			CSessionClient session = CSessionClient.GetSessionForIdSession(nIdSession);
			CSousSessionClient sousSession = session as CSousSessionClient;
			if (sousSession != null)
				return sousSession.RootSession.IdSession;
			return nIdSession;
		}

        ///////////////////////////////////////////
        public string GetIdConnexionForType(Type classeObjetServeur)
        {
            string strIdConnexion = m_strIdConnexionParDefaut;
            object idCon = m_tableTypeObjetServeurToIdConnexion[classeObjetServeur];
            if (idCon == null)
            {
                string strNomClasse = classeObjetServeur.ToString();
                strNomClasse = strNomClasse.Substring(0, strNomClasse.LastIndexOf('.'));
                idCon = m_tableAssemblyToIdConnexion[strNomClasse];
                if (idCon != null)
                {
                    strIdConnexion = idCon.ToString();
                    m_tableTypeObjetServeurToIdConnexion[classeObjetServeur] = idCon;
                }
            }
            else
                strIdConnexion = idCon.ToString();
            return strIdConnexion;
        }

		///////////////////////////////////////////
		/// <summary>
		/// Retourne la connexion pour l'objet serveur donné et la session donnée.
		/// </summary>
		/// <param name="nIdSession"></param>
		/// <param name="classeObjetServeur"></param>
		/// <returns></returns>
		public IDatabaseConnexion GetDatabaseConnexion ( int nIdSession, Type classeObjetServeur )
		{
			nIdSession = GetIdSessionForDb(nIdSession);
            string strIdConnexion = GetIdConnexionForType ( classeObjetServeur );
			return GetConnexions(nIdSession).GetConnexion ( strIdConnexion );
		}

		///////////////////////////////////////////
		public void ResetConnexion ( int nIdSession, string strIdConnexion )
		{
			nIdSession = GetIdSessionForDb(nIdSession);
			if ( strIdConnexion == null || strIdConnexion == "")
				strIdConnexion = m_strIdConnexionParDefaut;
			GetConnexions(nIdSession).ResetConnexion ( strIdConnexion );
		}

        ///////////////////////////////////////////
        internal static void OnCloseSession(int nIdSession)
        {
            CConnexionsParSession cnx = GetInstance().m_tableSessions[nIdSession] as CConnexionsParSession;
            GetInstance().m_tableSessions.Remove(nIdSession);
            if (cnx != null)
                cnx.Dispose();
        }
        
        ///////////////////////////////////////////
		/// <summary>
		/// Retourne la connexion pour l'objet serveur donné et la session donnée.
		/// </summary>
		/// <param name="nIdSession"></param>
		/// <param name="classeObjetServeur"></param>
		/// <returns></returns>
		public IDatabaseConnexion GetDatabaseConnexion ( int nIdSession, string strIdConnexion )
		{
			nIdSession = GetIdSessionForDb(nIdSession);
			if ( strIdConnexion == null || strIdConnexion == "")
				strIdConnexion = m_strIdConnexionParDefaut;
			return GetConnexions(nIdSession).GetConnexion ( strIdConnexion );
		}

		///////////////////////////////////////////
		public void SetDatabaseConnexion ( int nIdSession, string strIdConnexion, IDatabaseConnexion connexion )
		{
			nIdSession = GetIdSessionForDb(nIdSession);
			GetConnexions ( nIdSession ).SetConnexion ( strIdConnexion, connexion );
		}




        ///////////////////////////////////////////
        internal string GetPrefixeForType(Type tpLoader)
        {
            string strIdConnexion = GetIdConnexionForType(tpLoader);
            if (strIdConnexion != null)
            {
                CDefinitionConnexionDataSource def = (CDefinitionConnexionDataSource)m_tableDefinitionsConnexions[strIdConnexion];
                if (def != null)
                    return def.PrefixeTables;
            }
            return "";
        }
    }
}
