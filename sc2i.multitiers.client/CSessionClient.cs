using System;
using System.Runtime.Remoting.Messaging;
using System.Collections;
using System.Collections.Generic;

using sc2i.common;
using System.Runtime.Remoting;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Gestion des sessions client
	/// pour fonctionner,le fichier de config du remoting doit déclarer
	/// un objet de type IGestionnaireSessions
	/// exemple : 
	/// <wellknown mode="SingleCall" type="Cafel.serveur.CGestionnaireSessionsCafel, Cafel.serveur" objectUri="IGestionnaireSessions"/>
	/// </summary>
	public enum ETypeApplicationCliente
	{
		Windows = 0,
		Web,
		Service,
		Process
	}

	


	public delegate void SessionEventHandler ( CSessionClient session );
	
	public class CSessionClient : 
		MarshalByRefObject, 
		IDisposable, 
		IServicesClientManager, 
		I2iMarshalObjectDeSession, 
		IInfoSession,
		IServiceTransactions
	{
		//Id->SessionClient
		private static Hashtable m_tableSessionParId = new Hashtable();

		//Table des sessions allouées localement (pas remote) (id session -> true );
		private static Hashtable m_tableSessionsLocales = new Hashtable();

		//Table StringIdService->service
		private static Hashtable m_tableServicesSurClient = new Hashtable();

		private static int m_nFirstSessionOuverte = -1;

		//Liste des listes de recepteurs de notification par type de donnée de notification
		//enregistré
		private Hashtable m_tableRecepteursParType = new Hashtable();

		private static int m_nProchainIdRecepteur = 0;

        private CThreadSemaphore m_semaphorePourThreads = new CThreadSemaphore();



#if PDA
#else
		private static C2iSponsor m_sponsor = new C2iSponsor();
#endif

		private int		m_nIdSession=0;
		private ISessionClientSurServeur m_sessionSurServeur = null;
		private CCacheInfoUtilisateurSurClient m_cacheInfosUtilisateur = null;
        private bool m_bUseSessionUtilisateurData = true;

		private Type m_typeGestionnaireSessions = typeof(IGestionnaireSessions);

		private IAuthentificationSession m_authentification;


		public event EventHandler OnCloseSession;

		private string m_strDescription = "";
		private ETypeApplicationCliente m_typeApplication = ETypeApplicationCliente.Windows;
		private DateTime m_dateHeureConnexion = DateTime.Now;

		//Contient des propriétés spécifiques à la session
		private Hashtable m_tableProprietes = new Hashtable();

		private CConfigurationsImpression m_configurationsImpression = new CConfigurationsImpression();

		private bool m_bIsOpen = false;

        
        private static bool m_bGlobalUseSessionUtilisateurData = true;


        /// <summary>
        /// Permet d'empecher l'utilisation de IdSessionUtilisateurData (pour les sites WEB)
        /// </summary>
        public static bool GlobalUseSessionUtilisateurData
        {
            get
            {
                return m_bGlobalUseSessionUtilisateurData;
            }
            set
            {
                m_bGlobalUseSessionUtilisateurData = value;
            }
        }

		///////////////////////////////////////////////
		protected CSessionClient(  )
		{
            m_bUseSessionUtilisateurData = m_bGlobalUseSessionUtilisateurData;
		}

        ///////////////////////////////////////////////
        /// <summary>
        /// Mettre à false pour les sessions WEB
        /// </summary>
        public bool UseSessionUtilisateurData
        {
            get
            {
                return m_bUseSessionUtilisateurData;
            }
            set
            {
                m_bUseSessionUtilisateurData = value;
            }
        }

		public static event SessionEventHandler AfterOpenSession;
		public static event SessionEventHandler BeforeClosingSession;

		///////////////////////////////////////////////
		public static CSessionClient CreateInstance()
		{
			return new CSessionClient();
		}

		///////////////////////////////////////////////
		public virtual IAuthentificationSession Authentification
		{
			get
			{
				return m_authentification;
			}
			set
			{
				m_authentification = value;
			}
		}

        ///////////////////////////////////////////////
        public DateTime DateHeureDerniereActivite
        {
            get
            {
                try
                {
                    ISessionClientSurServeur session = SessionSurServeur;
                    return session.DateHeureDerniereActivite;
                }
                catch { }
                return DateHeureConnexion;
            }
            set
            {
                ISessionClientSurServeur session = SessionSurServeur;
                SessionSurServeur.DateHeureDerniereActivite = value;
            }
        }

		///////////////////////////////////////////////
		public virtual void SetPropriete ( string strPropriete, object valeur )
		{
			m_tableProprietes[strPropriete] = valeur;
		}

		///////////////////////////////////////////////
		public virtual object GetPropriete(string strPropriete)
		{
			return m_tableProprietes[strPropriete];
		}

		///////////////////////////////////////////////
		public string[] GetProprietes()
		{
			List<string> lst = new List<string>();
			foreach (string strProp in m_tableProprietes.Keys)
				lst.Add(strProp);
			return lst.ToArray();
		}

		///////////////////////////////////////////////
		public virtual CConfigurationsImpression ConfigurationsImpression
		{
			get
			{
				/*CConfigurationsImpression config = (CConfigurationsImpression)CallContext.GetData ( CConfigurationsImpression.c_idDataThreadConfigurationImpression );
				if ( config != null )
					return config;
				if ( m_configurationsImpression == null )
					m_configurationsImpression = new CConfigurationsImpression();*/
				return m_configurationsImpression;
			}
			set
			{
				m_configurationsImpression = value;

			}
		}

		///////////////////////////////////////////////
		public CResultAErreur OpenSession(IAuthentificationSession authentification)
		{
			return OpenSession ( authentification, "Inconnu", ETypeApplicationCliente.Web);
		}

		///////////////////////////////////////////////
		public Type TypeGestionnaireSessions
		{
			set
			{
				m_typeGestionnaireSessions = value;
			}
		}

		///////////////////////////////////////////////
		public static bool IsSessionLocale ( CSessionClient session )
		{
			try
			{
				return session.GetType() == typeof(CSessionClient);
			}
			catch { }
			return false;
		}

		///////////////////////////////////////////////
		public static bool IsUserConnected(CDbKey keyUtilisateur)
		{
			IGestionnaireSessions gestionnaire = GestionnaireSessions;
			try
			{
                //TESTDBKEYOK
				return gestionnaire.IsConnected(keyUtilisateur);
			}
			catch
			{
			}
			return false;
		}

		///////////////////////////////////////////////
		public static IInfoUtilisateur[] GetUtilisateursConnecte()
		{
			IGestionnaireSessions gestionnaire = GestionnaireSessions;
			try
			{
				return gestionnaire.GetUtilisateursConnectes();
			}
			catch
			{
			}
			return new IInfoUtilisateur[0];
		}


		///////////////////////////////////////////////
		public CResultAErreur OpenSession(IAuthentificationSession authentification, string strDescriptionApplication, ETypeApplicationCliente typeApplication)
		{
			CResultAErreur result = CResultAErreur.True;
			IGestionnaireSessions gestionnaire = null;
			try
			{
				//Ne pas utiliser GestionnaireSessions car on a besoin du type de gestionnaire
				//Si l'appli utilise plusieurs gestionnaires de sessions
				gestionnaire = (IGestionnaireSessions)C2iFactory.GetNewObject(m_typeGestionnaireSessions);
			}
			catch ( Exception e )
			{
				result.EmpileErreur( new CErreurException(e));
				result.EmpileErreur(I.T("Error during the allowance of sessions manager|107"));
				return result;
			}
			
			m_authentification = authentification;
			m_strDescription = strDescriptionApplication;
			m_typeApplication = typeApplication;
			try
			{
				result = gestionnaire.OpenSession ( this );
				if ( result )
				{
					m_tableSessionParId[IdSession] = this;
					m_tableSessionsLocales[IdSession] = true;
					if ( AfterOpenSession != null )
						AfterOpenSession ( this );
					if ( m_nFirstSessionOuverte == -1 )
					{
						m_nFirstSessionOuverte = IdSession;
					}
					GetDataThread(m_bUseSessionUtilisateurData).PushIdSession(IdSession);
					m_bIsOpen = true;
				}
			}
			catch ( Exception e )
			{
				result.EmpileErreur( new CErreurException(e));
				result.EmpileErreur(I.T("Error while opening session|108"));
				return result;
			}
			return result;
		}

		/*///////////////////////////////////////////////
		public void AttachToThread ( )
		{
			if ( CallContext.GetData ( typeof(IdSessionUtilisateurData).ToString() ) == null )
				CallContext.SetData ( typeof(IdSessionUtilisateurData).ToString(), new IdSessionUtilisateurData(IdSession) );
		}*/

		///////////////////////////////////////////////
		public virtual string DescriptionApplicationCliente
		{
			get
			{
				return m_strDescription;
			}
		}

		///////////////////////////////////////////////
		public virtual ETypeApplicationCliente TypeApplicationCliente
		{
			get
			{
				return m_typeApplication;
			}
		}

		///////////////////////////////////////////////
		public virtual DateTime DateHeureConnexion
		{
			get
			{
				return m_dateHeureConnexion;
			}
		}

		///////////////////////////////////////////////
		public virtual void Dispose()
		{
			CloseSession();
		}

        ///////////////////////////////////////////////
        public virtual void CloseSession()
        {
            if (!m_bIsOpen)
                return;
            DateTime dt = DateTime.Now;
            if (BeforeClosingSession != null)
                BeforeClosingSession(this);
            TimeSpan sp = DateTime.Now - dt;
            //Console.WriteLine("Closing " + m_nIdSession + " : 1-" + sp.TotalMilliseconds);
            CGestionnaireObjetsAttachesASession.OnCloseSession(IdSession);
            sp = DateTime.Now - dt;
            //Console.WriteLine("Closing " + m_nIdSession + " : 2-" + sp.TotalMilliseconds);
            if (OnCloseSession != null)
                OnCloseSession(this, new EventArgs());
            sp = DateTime.Now - dt;
            //Console.WriteLine("Closing " + m_nIdSession + " : 3-" + sp.TotalMilliseconds);
            m_tableSessionParId.Remove(IdSession);
            sp = DateTime.Now - dt;
            //Console.WriteLine("Closing " + m_nIdSession + " : 4-" + sp.TotalMilliseconds);
            m_tableSessionsLocales.Remove(IdSession);
            sp = DateTime.Now - dt;
            //Console.WriteLine("Closing " + m_nIdSession + " : 5-" + sp.TotalMilliseconds);
            if (m_sessionSurServeur != null)
                m_sessionSurServeur.CloseSession();
            sp = DateTime.Now - dt;
            //Console.WriteLine("Closing " + m_nIdSession + " : 6-" + sp.TotalMilliseconds);
            m_sessionSurServeur = null;
            if (m_cacheInfosUtilisateur != null)
                m_cacheInfosUtilisateur.Dispose();
            sp = DateTime.Now - dt;
            //Console.WriteLine("Closing " + m_nIdSession + " : 7-" + sp.TotalMilliseconds);
            m_cacheInfosUtilisateur = null;
            //Nettoie les recepteurs de notification
            m_tableRecepteursParType.Clear();
            GetDataThread(m_bUseSessionUtilisateurData).PopIdSession();
            sp = DateTime.Now - dt;
            //Console.WriteLine("Closing " + m_nIdSession + " : 8-" + sp.TotalMilliseconds);
            m_bIsOpen = false;

            m_sponsor.Unregister(m_sessionSurServeur);
            sp = DateTime.Now - dt;
            //Console.WriteLine("Closing " + m_nIdSession + " : 9-" + sp.TotalMilliseconds);
            m_sessionSurServeur = null;

        }

		///////////////////////////////////////////////
		public int IdSession
		{
			get 
			{  
				return m_nIdSession;
			}
			set
			{
				m_nIdSession = value;
			}
		}

		///////////////////////////////////////////////
		public bool IsConnected
		{
			get
			{
				if ( SessionSurServeur == null )
					return false;
				try
				{
					//Se demande soi-même au gestionnaire de session
					//distant pour vérifier qu'elle est tjrs valides
					m_tableSessionParId[IdSession] = null;
					CSessionClient session =  GetSessionForIdSession ( IdSession );
					return session.IdSession == IdSession;
				}
				catch
				{
					return false;
				}
				
			}
		}

		///////////////////////////////////////////////
		public ISessionClientSurServeur SessionSurServeur
		{
			get
			{
				ISessionClientSurServeur sessionSurServeur = m_sessionSurServeur;
				try
				{
					sessionSurServeur.Ping();
					return sessionSurServeur;
				}
				catch (Exception)
				{
					CResultAErreur result = CResultAErreur.True;
					//Tente de récuperer la session sur le serveur
					m_sessionSurServeur = GestionnaireSessions.GetSessionClientSurServeur(IdSession);
					try
					{
						m_sessionSurServeur.Ping();
					}
					catch
					{
						m_sessionSurServeur = null;
					}
					if (m_sessionSurServeur == null)
					{
						result = GestionnaireSessions.ReconnecteSession(this);
						if (!result)
							throw new DisconnectedSessionException();
						try
						{
							m_sessionSurServeur.Ping();
							if (OnReconnexionAutomatique != null)
								OnReconnexionAutomatique(this, null);
						}
						catch
						{
							throw new DisconnectedSessionException();
						}
					}
					RefreshUtilisateur();
					return m_sessionSurServeur;					
				}				
			}
			set
			{
#if PDA
#else
				m_sponsor.Unregister(m_sessionSurServeur);
#endif
				m_sessionSurServeur = value;
#if PDA
#else

				m_sponsor.Register(m_sessionSurServeur);
#endif
			}
		}

		
		///////////////////////////////////////////////
		public virtual void RefreshUtilisateur()
		{
            if (m_cacheInfosUtilisateur != null)
                m_cacheInfosUtilisateur.Dispose();
			m_cacheInfosUtilisateur = null;
		}


		///////////////////////////////////////////////
		public virtual IInfoUtilisateur GetInfoUtilisateur()
		{
			if ( m_cacheInfosUtilisateur == null )
			{
				if ( m_sessionSurServeur == null )
					return null;
				m_cacheInfosUtilisateur = new CCacheInfoUtilisateurSurClient(IdSession,m_sessionSurServeur.GetInfoUtilisateur());
			}
			return m_cacheInfosUtilisateur;
		}

		///////////////////////////////////////////////
		public virtual void ChangeUtilisateur ( CDbKey keyUtilisateur )
		{
            //TESTDBKEYOK
			SessionSurServeur.ChangeUtilisateur ( keyUtilisateur );
            RefreshUtilisateur();
		}


		///////////////////////////////////////////////
		public virtual IFournisseurServicePourSessionClient GetFournisseur ( string strService )
		{
			if ( m_sessionSurServeur == null )
				return null;
			return m_sessionSurServeur.GetFournisseur ( strService );
		}

		///////////////////////////////////////////////
		public virtual IServicePourSessionClient GetService ( string strService )
		{
			return GetService (strService, m_nIdSession);
		}

		///////////////////////////////////////////////
		public virtual IServicePourSessionClient GetService ( string strService, int nIdSession )
		{
			if ( m_sessionSurServeur == null )
				return null;
			return m_sessionSurServeur.GetService ( strService, m_nIdSession );
		}

		///////////////////////////////////////////////
		public static IGestionnaireSessions GestionnaireSessions
		{
			get
			{
				IGestionnaireSessions gestionnaire = (IGestionnaireSessions)C2iFactory.GetNewObject(typeof(IGestionnaireSessions));
				return gestionnaire;
			}
		}

		///////////////////////////////////////////////
		/// <summary>
		/// Appelé par le gestionnaire de sessions
		/// </summary>
		/// <param name="nIdSession"></param>
		public static void OnCloseSessionServeur(int nIdSession)
		{
			m_tableSessionParId.Remove(nIdSession);
		}


		///////////////////////////////////////////////
		public static CSessionClient GetSessionForIdSession ( int nIdSession )
		{
			CSessionClient session = (CSessionClient)m_tableSessionParId[nIdSession];
			if ( session != null )
				return session;
			IGestionnaireSessions gestionnaire = GestionnaireSessions;
			session = gestionnaire.GetSessionClient ( nIdSession );
			m_tableSessionParId[nIdSession] = session;
			return session;
		}
		
		///////////////////////////////////////////////
		/// <summary>
		/// Retourne la session unique d'une application n'hébergeant qu'une seule Session
		/// </summary>
		/// <returns></returns>
		public static CSessionClient GetSessionUnique ()
		{
			int nIdSession = m_nFirstSessionOuverte;
			try
			{
				IdSessionUtilisateurData data = GetDataThread(GlobalUseSessionUtilisateurData);
				if ( data.HasIdSession )
					nIdSession = data.IdSession;
			}
			catch
			{
			}
			CSessionClient session = (CSessionClient)m_tableSessionParId[nIdSession];
			if ( session == null )
				session = GetSessionForIdSession ( nIdSession );
			return session;
		}

		///////////////////////////////////////////////
		public void RenouvelleBailParAppel()
		{
		}

        ///////////////////////////////////////////////
        private string m_strIdUnique = "";
        public string UniqueId
        {
            get
            {
                if (m_strIdUnique.Length == 0)
                    m_strIdUnique = CUniqueIdentifier.GetNew();
                return m_strIdUnique;
            }
        }

		///////////////////////////////////////////////
		public static void RegisterServiceSurClient ( CServiceSurClient service )
		{
			m_tableServicesSurClient[service.IdService] = service;
		}

		///////////////////////////////////////////////
		public virtual CServiceSurClient GetServiceSurClient ( string strService )
		{
			return ( CServiceSurClient )m_tableServicesSurClient[strService];
		}
		 
		///////////////////////////////////////////////
		public virtual CResultAErreur BeginTrans()
		{
			return SessionSurServeur.BeginTrans();
		}

		///////////////////////////////////////////////
		public virtual CResultAErreur BeginTrans(System.Data.IsolationLevel isolationLevel)
		{
			return SessionSurServeur.BeginTrans( isolationLevel );
		}

        ///////////////////////////////////////////////
        public virtual bool IsInTransaction()
        {
            return SessionSurServeur.IsInTransaction();
        }
	
		///////////////////////////////////////////////
		public virtual CResultAErreur RollbackTrans()
		{
			return SessionSurServeur.RollbackTrans();
		}
		
		///////////////////////////////////////////////
		public virtual CResultAErreur CommitTrans()
		{
			return SessionSurServeur.CommitTrans();
		}

	
		///////////////////////////////////////////////
		public virtual bool AccepteTransactionsImbriquees
		{
			get
			{
				return true;
			}
		}

		///////////////////////////////////////////////
		/// <summary>
		/// Retourne true si la session est on objet alloué localement ou false si c'est un proxy
		/// </summary>
		public static bool IsSessionLocale ( int nIdSession )
		{
			return m_tableSessionsLocales.Contains ( nIdSession );
		}

		///////////////////////////////////////////////
		public override string ToString()
		{
			return "Session n° " + IdSession;
		}

		///////////////////////////////////////////////
		private static IdSessionUtilisateurData GetDataThread(bool bUseSessionUtilisateurData)
		{
            IdSessionUtilisateurData data = null;
            if (bUseSessionUtilisateurData)
            {
                data = (IdSessionUtilisateurData)CallContext.GetData(typeof(IdSessionUtilisateurData).ToString());
                if (data == null)
                {
                    data = new IdSessionUtilisateurData();
                    CallContext.SetData(typeof(IdSessionUtilisateurData).ToString(), data);
                }
            }
            else
            {
                data = new IdSessionUtilisateurData();
            }
			return data;
		}

		///////////////////////////////////////////////
		public void DetacheFromThread()
		{
			GetDataThread(m_bUseSessionUtilisateurData).PopIdSession();
		}

		///////////////////////////////////////////////
		public void AttacheToThread()
		{
			GetDataThread(m_bUseSessionUtilisateurData).PushIdSession ( IdSession );
		}


		private class CLockerListeRecepteurs
		{ }
		
		/// //////////////////////////////////////////////////////////////////////////
		public void RegisterRecepteurNotification(CRecepteurNotification recepteur, Type typeDeNotificationGeree)
		{
			lock (typeof(CLockerListeRecepteurs))
			{
				recepteur.IdRecepteur = m_nProchainIdRecepteur;
				m_nProchainIdRecepteur++;
				ArrayList lst = (ArrayList)m_tableRecepteursParType[typeDeNotificationGeree];
				if (lst == null)
				{
					lst = new ArrayList();
					m_tableRecepteursParType[typeDeNotificationGeree] = lst;
				}
				if (!lst.Contains(recepteur))
				{
					lst.Add(recepteur);
				}
			}
		}

		/// //////////////////////////////////////////////////////////////////////////
		public  void UnregisterRecepteurNotification(CRecepteurNotification recepteur)
		{
			lock (typeof(CLockerListeRecepteurs))
			{
				foreach (ArrayList lst in m_tableRecepteursParType.Values)
				{
					lst.Remove(recepteur);
				}
			}
		}

		/// //////////////////////////////////////////////////////////////////////////
		public CResultAErreur OnNotification(IDonneeNotification[] donnees)
		{
			CResultAErreur result = CResultAErreur.True;

            foreach (IDonneeNotification donnee in donnees)
            {
                ArrayList lst = null;
                lock (typeof(CLockerListeRecepteurs))
                {
                    ArrayList tmp = (ArrayList)m_tableRecepteursParType[donnee.GetType()];
                    if (tmp != null)
                        lst = new ArrayList(tmp);
                }
                if (lst != null)
                {
                    foreach (CRecepteurNotification recepteur in lst)
                    {
                        try
                        {
                            recepteur.RecoitNotification(donnee);
                        }
                        catch
                        { }
                    }
                }
            }
			return result;
		}

        /// //////////////////////////////////////////////////////////////////////////
        public CJetonThread GetJeton(string strSemaphore)
        {
            if (IsSessionLocale(IdSession))
            {
                return new CJetonThread(m_semaphorePourThreads, strSemaphore);
            }
            else
                return new CJetonThread(null, "");
        }

        /// //////////////////////////////////////////////////////////////////////////
        public void WaitOne(string strSemaphore)
        {
            m_semaphorePourThreads.WaitOne(strSemaphore);
        }

        /// //////////////////////////////////////////////////////////////////////////
        public void Release(string strSemaphore)
        {
            m_semaphorePourThreads.Release(strSemaphore);
        }

		public event EventHandler OnReconnexionAutomatique;
 	}
}
