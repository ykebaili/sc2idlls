using System;
using System.Collections.Generic;
using System.Collections;

using sc2i.multitiers.client;
using sc2i.common;


namespace sc2i.multitiers.server
{
	/// <summary>
	/// Description résumée de CSessionClientSurServeur.
	/// </summary>
	public abstract class  CSessionClientSurServeur : C2iObjetServeur, ISessionClientSurServeur
	{
		private static Hashtable	m_tableFournisseursServicesPourClient = new Hashtable();
				
		/// <summary>
		/// Liste des fournisseurs de service de transaction connus
		/// </summary>
		private static ArrayList m_listeFournisseursTransaction = new ArrayList();

		
		private static C2iSponsor m_sponsor = new C2iSponsor();

		private CSessionClient m_sessionSurClient = null;

        private List<ISessionClientSurServeur> m_listeSousSessions = new List<ISessionClientSurServeur>();

		private string m_strDescription = "";
		private ETypeApplicationCliente m_typeApplication = ETypeApplicationCliente.Windows;
		private DateTime m_dateHeureConnexion;
        private DateTime m_dateHeureDerniereActivite;

		private int m_nNbTransactions = 0;

        private DateTime m_lastDateTesteClientSuccess = DateTime.Now;

        private IAuthentificationSession m_authentification = null;

		private CSessionClientSurServeur m_sessionPourTransactions = null;

		///////////////////////////////////////////////
		public CSessionClientSurServeur( CSessionClient sessionSurClient )
		{
			m_sessionSurClient = sessionSurClient;
			IdSession = sessionSurClient.IdSession;
			m_dateHeureConnexion = sessionSurClient.DateHeureConnexion;
            m_dateHeureDerniereActivite = DateTime.Now;
			m_sponsor.Register ( m_sessionSurClient );
			m_strDescription = sessionSurClient.DescriptionApplicationCliente;
			m_typeApplication = sessionSurClient.TypeApplicationCliente;
            m_authentification = sessionSurClient.Authentification;
            CAuthentificationSessionSousSession autSousSession = m_authentification as CAuthentificationSessionSousSession;
            if (autSousSession != null)
            {
                ISessionClientSurServeur sessionPrincipale = CGestionnaireSessions.GetSessionClientSurServeurStatic(autSousSession.IdSessionPrincipale);
                if (sessionPrincipale != null)
                    sessionPrincipale.RegisterSousSession(this);
            }
		}

        ///////////////////////////////////////////////
        public void RegisterSousSession(ISessionClientSurServeur sousSession)
        {
            if (!m_listeSousSessions.Contains(sousSession))
                m_listeSousSessions.Add(sousSession);
        }

        ///////////////////////////////////////////////
        public void RemoveSousSession ( ISessionClientSurServeur sousSession )
        {
            if ( m_listeSousSessions.Contains ( sousSession ))
                m_listeSousSessions.Remove ( sousSession );
        }

        ///////////////////////////////////////////////
        public IEnumerable<ISessionClientSurServeur> GetSousSessions()
        {
            return m_listeSousSessions.AsReadOnly();
        }

        ///////////////////////////////////////////////
        public DateTime DateHeureLastTestSessionClientSuccess
        {
            get
            {
                return m_lastDateTesteClientSuccess;
            }
            set
            {
                m_lastDateTesteClientSuccess = value;
            }
        }


        ///////////////////////////////////////////////
        public IEnumerable<ISessionClientSurServeur> GetAllSousSession()
        {
            List<ISessionClientSurServeur> lst = new List<ISessionClientSurServeur>();
            FillListeSousSessions(lst);
            return lst.AsReadOnly();
        }

        ///////////////////////////////////////////////
        public void FillListeSousSessions(List<ISessionClientSurServeur> lst)
        {
            lst.AddRange(GetSousSessions());
            foreach (ISessionClientSurServeur session in m_listeSousSessions)
            {
                session.FillListeSousSessions(lst);
            }
        }

        ///////////////////////////////////////////////
        public ISessionClientSurServeur GetSessionPrincipale()
        {
            CAuthentificationSessionSousSession auth = m_authentification as CAuthentificationSessionSousSession;
            if (auth != null)
            {
                ISessionClientSurServeur session = CGestionnaireSessions.GetSessionClientSurServeurStatic(auth.IdSessionPrincipale);
                if (session != null)
                    return session.GetSessionPrincipale();
            }
            return this;
        }

		///////////////////////////////////////////////
		public abstract IInfoUtilisateur GetInfoUtilisateur(  );
		
		///////////////////////////////////////////////
		public abstract void MyCloseSession();

		///////////////////////////////////////////////
		public void CloseSession()
		{
			string strMessage = I.T("Closing session @1|110", IdSession.ToString());
			try
			{
				strMessage += " "+GetInfoUtilisateur().NomUtilisateur;
			}
			catch
			{
			}
            CAuthentificationSessionSousSession authSousSession = m_authentification as CAuthentificationSessionSousSession;
            if (authSousSession != null)
            {
                ISessionClientSurServeur sessionPrinc = CGestionnaireSessions.GetSessionClientSurServeurStatic(authSousSession.IdSessionPrincipale);
                if (sessionPrinc != null)
                    sessionPrinc.RemoveSousSession(this);
            }
			
            C2iEventLog.WriteInfo(strMessage, NiveauBavardage.VraiPiplette);
			int nIdSession = IdSession;
            DateTime dt = DateTime.Now;
			MyCloseSession();
            TimeSpan sp = DateTime.Now - dt;
            Console.WriteLine("SessionServeur closing " + nIdSession + " / 1 : " + sp.TotalMilliseconds);
			CGestionnaireObjetsAttachesASession.OnCloseSession(IdSession);
            sp = DateTime.Now - dt;
            Console.WriteLine("SessionServeur closing " + nIdSession + " / 2 : " + sp.TotalMilliseconds);
			CGestionnaireSessions.CloseSession ( this );
            sp = DateTime.Now - dt;
            Console.WriteLine("SessionServeur closing " + nIdSession + " / 3 : " + sp.TotalMilliseconds);
			CGestionnaireObjetsAttachesASession.OnCloseSession(IdSession);//Si jamais le close sessino a réaloué des éléments
            sp = DateTime.Now - dt;
            Console.WriteLine("SessionServeur closing " + nIdSession + " / 4 : " + sp.TotalMilliseconds);
			m_sponsor.Unregister(m_sessionSurClient);
            sp = DateTime.Now - dt;
            Console.WriteLine("SessionServeur closing " + nIdSession + " / 5 : " + sp.TotalMilliseconds);
			m_sessionSurClient = null;
		}

		///////////////////////////////////////////////
		public abstract void ChangeUtilisateur( CDbKey keyUtilisateur);

		///////////////////////////////////////////////
		public static void RegisterFournisseur ( IFournisseurServicePourSessionClient fournisseur )
		{
			m_tableFournisseursServicesPourClient[fournisseur.TypeService] = fournisseur;
		}
		///////////////////////////////////////////////
		public IFournisseurServicePourSessionClient GetFournisseur ( string strService )
		{
			return (IFournisseurServicePourSessionClient)m_tableFournisseursServicesPourClient[strService];
		}

		///////////////////////////////////////////////
		public IServicePourSessionClient GetService ( string strTypeService, int nIdSession )
		{
			IFournisseurServicePourSessionClient fournisseur = (IFournisseurServicePourSessionClient)m_tableFournisseursServicesPourClient[strTypeService];
			if ( fournisseur != null )
				return fournisseur.GetService ( nIdSession );
			return null;
		}

		///////////////////////////////////////////////
		public CSessionClient SessionClient
		{
			get
			{
				return m_sessionSurClient;
			}
		}

		///////////////////////////////////////////////
		public string DescriptionApplicationCliente
		{
			get
			{
				return m_strDescription;
			}
		}

		///////////////////////////////////////////////
		public ETypeApplicationCliente TypeApplicationCliente
		{
			get
			{
				return m_typeApplication;
			}
		}

		///////////////////////////////////////////////
		public DateTime DateHeureConnexion
		{
			get
			{
				return m_dateHeureConnexion;
			}
		}

        ///////////////////////////////////////////////
        public DateTime DateHeureDerniereActivite
        {
            get
            {
                return m_dateHeureDerniereActivite;
            }
            set
            {
                m_dateHeureDerniereActivite = value;
            }
        }

		///////////////////////////////////////////////
		private class CFournisseurServiceTransactionComparer : IComparer
		{
			public int Compare(object x, object y)
			{
				return ((IFournisseurServiceTransactionPourSession)y).PrioriteTransaction.CompareTo (
					((IFournisseurServiceTransactionPourSession)x).PrioriteTransaction );
			}
		}


		///////////////////////////////////////////////
		/// <summary>
		/// </summary>
		/// <param name="service"></param>
		public static void RegisterFournisseurTransactions ( IFournisseurServiceTransactionPourSession fournisseur )
		{
			m_listeFournisseursTransaction.Add ( fournisseur );
			m_listeFournisseursTransaction.Sort ( new CFournisseurServiceTransactionComparer() );
		}

		///////////////////////////////////////////////
		public CResultAErreur BeginTrans()
		{
			return BeginTrans ( System.Data.IsolationLevel.ReadUncommitted );
		}

        ///////////////////////////////////////////////
        public bool IsInTransaction()
        {
            return m_nNbTransactions > 0;
        }

		///////////////////////////////////////////////
		/// <summary>
		/// Retourne le n° de session gérant les transactions
		/// </summary>
		/// <param name="nIdSession"></param>
		/// <returns></returns>
		private CSessionClientSurServeur SessionPourTransactions
		{
			get
			{
				if (m_sessionPourTransactions == null)
				{
					CSessionClient session = CSessionClient.GetSessionForIdSession(IdSession);
					CSousSessionClient sousSession = session as CSousSessionClient;
					if (sousSession != null)
						m_sessionPourTransactions = (CSessionClientSurServeur)sousSession.RootSession.SessionSurServeur;
					else
						m_sessionPourTransactions = this;
				}
				return m_sessionPourTransactions;
			}
		}

		///////////////////////////////////////////////
		public CResultAErreur BeginTrans( System.Data.IsolationLevel isolationLevel )
		{
			ArrayList lstStarted = new ArrayList();
			CResultAErreur result = CResultAErreur.True;
			if (SessionPourTransactions != this)
				return SessionPourTransactions.BeginTrans(isolationLevel);
			foreach ( IFournisseurServiceTransactionPourSession fournisseur in m_listeFournisseursTransaction )
			{
				IServiceTransactions service = fournisseur.GetServiceTransaction(IdSession);
				if ( service.AccepteTransactionsImbriquees || m_nNbTransactions == 0 )
				{
					result = service.BeginTrans( isolationLevel );
					if ( !result )
					{
						foreach ( IServiceTransactions serviceFait in lstStarted )
							serviceFait.RollbackTrans();
						return result;
					}
					else
						lstStarted.Add ( service );
				}
			}
			m_nNbTransactions++;
			return result;
		}



		///////////////////////////////////////////////
		public CResultAErreur RollbackTrans()
		{
			CResultAErreur result = CResultAErreur.True;
			if (SessionPourTransactions != this)
				return SessionPourTransactions.RollbackTrans();
			foreach ( IFournisseurServiceTransactionPourSession fournisseur in m_listeFournisseursTransaction )
			{
				IServiceTransactions service = fournisseur.GetServiceTransaction(IdSession);
				if ( m_nNbTransactions == 1 || service.AccepteTransactionsImbriquees )
					result = service.RollbackTrans();
				if ( !result )
					return result;
			}
			m_nNbTransactions--;
			return result;
		}

		///////////////////////////////////////////////
		public CResultAErreur CommitTrans()
		{
			CResultAErreur result = CResultAErreur.True;
			if (SessionPourTransactions != this)
				return SessionPourTransactions.CommitTrans();
			foreach ( IFournisseurServiceTransactionPourSession fournisseur in m_listeFournisseursTransaction )
			{
				IServiceTransactions service = fournisseur.GetServiceTransaction(IdSession);
				if ( m_nNbTransactions == 1 || service.AccepteTransactionsImbriquees )
					result = service.CommitTrans();
				if ( !result )
				{
					return result;
				}
			}
			m_nNbTransactions--;
			return result;
		}

		///////////////////////////////////////////////
		public bool AccepteTransactionsImbriquees
		{
			get
			{
				return true;
			}
		}

		///////////////////////////////////////////////
		public bool Ping()
		{
			return true;
		}


		public abstract string GetVersionServeur();
		

	}
}
