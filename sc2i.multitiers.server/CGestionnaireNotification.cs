using System;
using System.Collections;

using sc2i.common;
using sc2i.multitiers.client;

namespace sc2i.multitiers.server
{
	/// <summary>
	/// Description résumée de CGestionnaireNotification.
	/// </summary>
	public class CGestionnaireNotification : C2iObjetServeur,IGestionnaireNotification
	{
		protected enum TypeGestionSessionEnvoyeur
		{
			SeulementASessionEnvoyeur,
			SaufASessionEnvoyeur,
			ToutesSessions
		}

		//Gestionnaires de transaction de notification par id de session
		private static Hashtable m_tableGestionnairesTransaction = new Hashtable();

        //Liste des serveurs auxquels il faut envoyer les notifications
        private static Hashtable m_tableServeursAnnexes = new Hashtable();

		///Fournisseur de service de transaction : CFournisseurTransactionsNotifications
		/// //////////////////////////////////////////////////////////////////////////
		[AutoExec("Autoexec")]
		public  class CFournisseurTransactionsNotifications : IFournisseurServiceTransactionPourSession
		{
			public IServiceTransactions GetServiceTransaction ( int nIdSession )
			{
				return CGestionnaireNotification.GetGestionnaireTransaction ( nIdSession );
			}

			/// //////////////////////////////////////////////////////////////////////////
			public int PrioriteTransaction
			{
				get
				{
					return 100;
				}
			}

			/// //////////////////////////////////////////////////////////////////////////
			public static void Autoexec()
			{
				CSessionClientSurServeur.RegisterFournisseurTransactions ( new CFournisseurTransactionsNotifications() );
			}
		}

        /// //////////////////////////////////////////////////////////////////////////
        public CGestionnaireNotification()
        {
            m_nIdSession = -1;
        }


		/// //////////////////////////////////////////////////////////////////////////
		public CGestionnaireNotification( int nIdSession )
			:base ( nIdSession )
		{
			
		}

        /// //////////////////////////////////////////////////////////////////////////
        public static void RegisterServeurAnnexe(string strUrl)
        {
            m_tableServeursAnnexes[strUrl] = true;
        }


		/// //////////////////////////////////////////////////////////////////////////
		protected delegate void EnvoieNotificationCallback ( IDonneeNotification[] donnees, TypeGestionSessionEnvoyeur typeGestion );

		/// //////////////////////////////////////////////////////////////////////////
		protected delegate void EnvoieNotificationsToSessionCallBack(CSessionClient session, IDonneeNotification[] donnees);

        private static int m_nNbErreursNotif = 0;

		/// //////////////////////////////////////////////////////////////////////////
		protected void EnvoieNotificationToSessionThread(CSessionClient session, IDonneeNotification[] donnees)
		{
			try
			{
				session.OnNotification(donnees);
			}
			catch ( Exception e )
			{
                try
                {

                    m_nNbErreursNotif++;
                    if (m_nNbErreursNotif > 20)
                    {
                        C2iEventLog.WriteErreur("Error sending notification \r\n" + e.ToString());
                        m_nNbErreursNotif = 0;
                    }
                }
                catch { }
			}
		}

		/// //////////////////////////////////////////////////////////////////////////
		protected void EnvoieNotificationsThread ( IDonneeNotification[] donnees, TypeGestionSessionEnvoyeur typeGestion )
		{
			foreach ( CSessionClientSurServeur session in CGestionnaireSessions.ListeSessionsServeur )
			{
				try
				{
					EnvoieNotificationsToSessionCallBack sender = new EnvoieNotificationsToSessionCallBack(EnvoieNotificationToSessionThread);
					sender.BeginInvoke(session.SessionClient, donnees, null, null);
				}
				catch
				{
					C2iEventLog.WriteErreur(I.T("Sending notifiations to session @1 error|104", session.IdSession.ToString()));
				}

			}

            //Transfert de la notification aux serveurs annexes
            foreach (string strURI in m_tableServeursAnnexes.Keys)
            {
                if ( strURI != "" )
                try
                {
                    IGestionnaireNotification gestionnaireAnnexe = (IGestionnaireNotification)Activator.GetObject(typeof(IGestionnaireNotification), strURI + "/IGestionnaireNotification");
                    gestionnaireAnnexe.RelaieNotifications(donnees);
                }
                catch  ( Exception e )
                {
                    C2iEventLog.WriteErreur(I.T("Secondary server error @1\r\n@2|103", strURI,e.ToString()));
                }
            }
			
		}

        /// //////////////////////////////////////////////////////////////////////////
        public void RelaieNotifications(IDonneeNotification[] donnees)
        {
			foreach( IDonneeNotification donnee in donnees )
				donnee.IdSessionEnvoyeur = -1;
            EnvoieNotificationsHorsTransaction(donnees);
        }
		
		/// //////////////////////////////////////////////////////////////////////////
		internal void EnvoieNotificationsHorsTransaction ( IDonneeNotification[] donnees )
		{
			//EnvoieNotificationThread ( donnee, TypeGestionSessionEnvoyeur.SeulementASessionEnvoyeur );
			EnvoieNotificationCallback sender = new EnvoieNotificationCallback(EnvoieNotificationsThread);
			//Envoie la notification aux recepteurs de la session

			sender.BeginInvoke ( donnees, TypeGestionSessionEnvoyeur.ToutesSessions, null, null );
		}

		/// //////////////////////////////////////////////////////////////////////////
		internal static CGestionnaireTransactionsNotification GetGestionnaireTransaction( int nIdSession )
		{
			CGestionnaireTransactionsNotification gestionnaire = (CGestionnaireTransactionsNotification)m_tableGestionnairesTransaction[nIdSession];
			if ( gestionnaire == null )
			{
				//Stef, le 03102008, gestion des sous sessions
				CSousSessionClient sousSession = CSessionClient.GetSessionForIdSession(nIdSession) as CSousSessionClient;
				if (sousSession != null)//C'est une sous session, donc utilise le gestionnaire de transactions de sa session principale
				{
					gestionnaire = GetGestionnaireTransaction(sousSession.RootSession.IdSession);
                    //Ne stocke pas dans le cache, car
                    //le gestionnaire n'est pas attaché à la sous session,
                    //donc, il ne sera pas supprimé par le OnCloseSession
					//m_tableGestionnairesTransaction[nIdSession] = gestionnaire;
				}
				else
				{
					gestionnaire = new CGestionnaireTransactionsNotification(nIdSession);
					m_tableGestionnairesTransaction[nIdSession] = gestionnaire;
				}
			}
			return gestionnaire;
		}

		/// //////////////////////////////////////////////////////////////////////////
		public void BeginTrans (  )
		{
			GetGestionnaireTransaction (IdSession ).BeginTrans();
		}

		/// //////////////////////////////////////////////////////////////////////////
		public void CommitTrans (  )
		{
			GetGestionnaireTransaction ( IdSession ).CommitTrans();
		}

		/// //////////////////////////////////////////////////////////////////////////
		public void RollbackTrans (  )
		{
			GetGestionnaireTransaction(IdSession).RollbackTrans();
		}



		/// //////////////////////////////////////////////////////////////////////////
		public void EnvoieNotifications (IDonneeNotification[] donnees )
		{
			if (donnees.Length == 0)
				return;
			//STEF 03102008 : Utilise la fonction GetGestionnaireTransactions
			CGestionnaireTransactionsNotification gestionnaire = GetGestionnaireTransaction(donnees[0].IdSessionEnvoyeur);
			gestionnaire.EnvoieNotifications(donnees);
		}

		/// //////////////////////////////////////////////////////////////////////////
		public static void ReleaseGestionnaire(CGestionnaireTransactionsNotification gestionnaire)
		{
			m_tableGestionnairesTransaction.Remove(gestionnaire.IdSession);
		}

		
		#region Membres de IFournisseurServiceTransactionPourSession

		public IServiceTransactions GetServiceTransaction(int nIdSession)
		{
			// TODO : ajoutez l'implémentation de CGestionnaireNotification.GetServiceTransaction
			return null;
		}

		#endregion
	}
}
