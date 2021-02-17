using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using System.Runtime.Remoting.Services;
using System.Runtime.Remoting;

using sc2i.common;
using sc2i.multitiers.client;
using sc2i.multitiers.server;
using System.Threading;
using System.Text;
using System.Runtime.Remoting.Lifetime;

namespace sc2i.multitiers.server
{
	/// <summary>
	/// Description résumée de CGestionnaireSessionsCafel.
	/// </summary>
	[AutoExec("StartRemoteTracking", AutoExecAttribute.BackGroundService)]
	public abstract class CGestionnaireSessions : MarshalByRefObject, IGestionnaireSessions
	{
        private class CLockerListeSessions { }
		private static Hashtable m_listeSessions = new Hashtable();

		private static int m_nNumNextSession = 0;

        private static Timer m_timerNettoyageSessions = null;

        private const int c_nFrequenceNettoyage = 1000 * 60 ;



		/// ///////////////////////////////////////////////////
		public CGestionnaireSessions()
		{
		}

		/// ///////////////////////////////////////////////////
		public static void StartRemoteTracking()
		{
			TrackingServices.RegisterTrackingHandler(new CSessionTracker());
            if ( m_timerNettoyageSessions == null )
            {
                m_timerNettoyageSessions = new Timer(NettoieSessions, null, c_nFrequenceNettoyage, c_nFrequenceNettoyage);
            }
		}

        /// ///////////////////////////////////////////////////
        public static void NettoieSessions(object state)
        {
            try
            {
                StringBuilder blInfo = new StringBuilder();
                blInfo.Append("Sessions cleaning : " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n");
                foreach (DictionaryEntry entry in new ArrayList(m_listeSessions))
                {
                    try
                    {
                        int nIdSession = (int)entry.Key;
                        blInfo.Append("Session ");
                        blInfo.Append(nIdSession);
                        CSessionClientSurServeur sessionServeur = (CSessionClientSurServeur)entry.Value;
                        CSessionClient session = CSessionClient.GetSessionForIdSession(nIdSession);
                        bool bIsConnected = false;
                        try
                        {
                            bIsConnected = session.IsConnected;
                            if ( bIsConnected )
                                sessionServeur.DateHeureLastTestSessionClientSuccess = DateTime.Now;
                        }
                        catch
                        {
                            blInfo.Append("/Disconnected from client/");
                        }
                        if (session == null || !bIsConnected)
                        {
                            TimeSpan sp = DateTime.Now - sessionServeur.DateHeureLastTestSessionClientSuccess;
                            if (sp.TotalMinutes > LifetimeServices.SponsorshipTimeout.Minutes)
                            {
                                blInfo.Append("/Closing/");
                                C2iEventLog.WriteInfo("Loose " + nIdSession + " Automatic disconnect", NiveauBavardage.PetiteCausette);
                                try
                                {
                                    sessionServeur.CloseSession();
                                }
                                catch
                                {
                                    CloseSession(sessionServeur);
                                }
                                
                                blInfo.Append("/Closed/");
                            }
                        }
                        else
                            blInfo.Append("/active/");
                        blInfo.Append(Environment.NewLine);
                    }
                    catch {
                        blInfo.Append("ERROR \r\n");
                    }
                }
                C2iEventLog.WriteInfo(blInfo.ToString(), NiveauBavardage.VraiPiplette);
            }
            catch{}
        }

		/// ///////////////////////////////////////////////////
		/// Le data du result doit être passé à la fonction GetNewSessionSurServeur
		protected abstract CResultAErreur CanOpenSession ( CSessionClient session );

		/// ///////////////////////////////////////////////////
		/// Data est le result.data du CanOpenSession
		protected abstract CSessionClientSurServeur GetNewSessionSurServeur ( CSessionClient session, object data );

		/// ///////////////////////////////////////////////////
		public abstract string GetNomUtilisateurFromKeyUtilisateur ( CDbKey keyUtilisateur );

		/// ///////////////////////////////////////////////////
		public CResultAErreur OpenSession ( CSessionClient sessionSurClient )
		{

			CResultAErreur result = CResultAErreur.True;
			result = CanOpenSession ( sessionSurClient );
			if ( result )
			{
				sessionSurClient.IdSession = m_nNumNextSession++;
				CSessionClientSurServeur sessionSurServeur = GetNewSessionSurServeur ( sessionSurClient, result.Data );
				result.Data = null;
				if ( sessionSurServeur == null )
				{
					result.EmpileErreur(I.T("Server session allocation impossible|105"));
				}
				else
				{
					sessionSurClient.SessionSurServeur = sessionSurServeur;
                    lock (typeof(CLockerListeSessions))
                    {
                        m_listeSessions[sessionSurClient.IdSession] = sessionSurServeur;
                    }
					string strMessage = I.T("Session Connection number @1|106",sessionSurClient.IdSession.ToString())+
						I.T("\r\n|108") + I.T("Type|107") +sessionSurClient.TypeApplicationCliente.ToString();
					strMessage += I.T("\r\n|108") + sessionSurServeur.DescriptionApplicationCliente;
					try
					{
						
						strMessage += I.T("\r\n|108") + sessionSurClient.GetInfoUtilisateur().NomUtilisateur;
					}
					catch
					{}
					C2iEventLog.WriteInfo( strMessage, NiveauBavardage.VraiPiplette );
					CDonneeNotificationConnection donnee = new CDonneeNotificationConnection(sessionSurClient.IdSession, true);
					CEnvoyeurNotification.EnvoieNotifications(new IDonneeNotification[] { donnee });
				}
			}
			return result;
		}

		/// ///////////////////////////////////////////////////
		public CResultAErreur ReconnecteSession(CSessionClient sessionSurClient)
		{
			CResultAErreur result = CResultAErreur.True;
			ISessionClientSurServeur sessionSurServeur = GetSessionClientSurServeur(sessionSurClient.IdSession);
			if (sessionSurServeur != null)
			{
				sessionSurClient.SessionSurServeur = sessionSurServeur;
				return result;
			}
			result = CanOpenSession(sessionSurClient);
			if (result)
			{
				sessionSurServeur = GetNewSessionSurServeur(sessionSurClient, result.Data);
				result.Data = null;
				if (sessionSurServeur == null)
				{
					result.EmpileErreur(I.T("Server session allocation impossible|105"));
				}
				else
				{
					sessionSurClient.SessionSurServeur = sessionSurServeur;
                    lock (typeof(CLockerListeSessions))
                    {
                        m_listeSessions[sessionSurClient.IdSession] = sessionSurServeur;
                    }
					string strMessage = I.T("Session Reconnection n°|111") + sessionSurClient.IdSession +
						"\r\n" + I.T("Type|107") + sessionSurClient.TypeApplicationCliente.ToString();
					strMessage += I.T("\r\n|108") + sessionSurServeur.DescriptionApplicationCliente;
					try
					{

						strMessage += I.T("\r\n|108") + sessionSurClient.GetInfoUtilisateur().NomUtilisateur;
					}
					catch
					{ }
					C2iEventLog.WriteInfo(strMessage, NiveauBavardage.VraiPiplette);
					CDonneeNotificationConnection donnee = new CDonneeNotificationConnection(sessionSurClient.IdSession, true);
					CEnvoyeurNotification.EnvoieNotifications(new IDonneeNotification[] { donnee });
				}
			}
			return result;
		}

		/// ///////////////////////////////////////////////////
		public static void CloseSession( CSessionClientSurServeur session )
		{
			CDonneeNotificationConnection donnee = new CDonneeNotificationConnection(session.IdSession, false);
			CEnvoyeurNotification.EnvoieNotifications(new IDonneeNotification[] { donnee });

            lock (typeof(CLockerListeSessions))
            {
                m_listeSessions.Remove(session.IdSession);
            }

			CSessionClient.OnCloseSessionServeur(session.IdSession);
		}

		/// ///////////////////////////////////////////////////
		public IInfoUtilisateur[] GetUtilisateursConnectes()
		{
			List<IInfoUtilisateur> lst = new List<IInfoUtilisateur>();
            lock (typeof(CLockerListeSessions))
            {
                foreach (CSessionClientSurServeur session in m_listeSessions.Values)
                {
                    try
                    {
                        IInfoUtilisateur user = session.GetInfoUtilisateur();
                        if (user != null && user.KeyUtilisateur != null)
                            lst.Add(user);
                    }
                    catch
                    {
                    }
                }
            }
			return lst.ToArray();
		}

		/// ///////////////////////////////////////////////////
		public CSessionClient GetSessionClient ( int nIdSession )
		{
			CSessionClientSurServeur sessionServeur = (CSessionClientSurServeur)m_listeSessions[nIdSession];
			if ( sessionServeur != null )
				return sessionServeur.SessionClient;
			return null;
		}

        /// ///////////////////////////////////////////////////
        public static ISessionClientSurServeur GetSessionClientSurServeurStatic(int nIdSession)
        {
            CSessionClientSurServeur sessionServeur = (CSessionClientSurServeur)m_listeSessions[nIdSession];
            return sessionServeur;
        }

		/// ///////////////////////////////////////////////////
		public ISessionClientSurServeur GetSessionClientSurServeur ( int nIdSession )
		{
            return GetSessionClientSurServeurStatic(nIdSession);
		}

		/// ///////////////////////////////////////////////////
		public static CSessionClientSurServeur[] ListeSessionsServeur
		{
			get
			{
				List<CSessionClientSurServeur> lst = new List<CSessionClientSurServeur>();
                lock (typeof(CLockerListeSessions))
                {
                    foreach (CSessionClientSurServeur session in m_listeSessions.Values)
                        lst.Add(session);
                    return lst.ToArray();
                }
			}
		}

		/// ///////////////////////////////////////////////////
		public int[]GetListeIdSessionsConnectees()
		{
			ArrayList lstRetour = new ArrayList();
			foreach ( int nIdSession in m_listeSessions.Keys )
			{
				if ( m_listeSessions[nIdSession] != null )
					lstRetour.Add ( nIdSession );
			}
			return (int[])lstRetour.ToArray(typeof(int));
			
		}

		//////////////////////////////////////////////////////////////////:
		public bool IsSessionOpen ( int nIdSession )
		{
			return IsSessionOpenStatic ( nIdSession );
		}

		//////////////////////////////////////////////////////////////////:
		public static bool IsSessionOpenStatic ( int nIdSession )
		{
			return m_listeSessions[nIdSession] != null;
		}

		//-------------------------------------------------------------
		public bool IsConnected(CDbKey keyUtilisateur)
		{
			foreach (CSessionClientSurServeur session in m_listeSessions.Values)
			{
				try
				{
					if (session.GetInfoUtilisateur().KeyUtilisateur == keyUtilisateur)
						return true;
				}
				catch
				{ }
			}
			return false;
		}



        #region IGestionnaireSessions Membres


        public CInfoSessionAsDynamicClass[] GetInfosSessionsActives()
        {
            List<CInfoSessionAsDynamicClass> lst = new List<CInfoSessionAsDynamicClass>();
            int[] listeIdSessions = GetListeIdSessionsConnectees();
            foreach (int nId in listeIdSessions)
            {
                CInfoSessionAsDynamicClass infoSession = new CInfoSessionAsDynamicClass();
                infoSession.IdSession = nId;

                CDbKey keyUtilisateur = null;
                IInfoSession session = GetSessionClient(nId);
                try
                {
                    session.GetInfoUtilisateur();
                }
                catch
                {
                    session = GetSessionClientSurServeur(nId);
                    infoSession.Invalide = true;
                    session = GetSessionClientSurServeur(nId);
                }
                if (session == null)
                {
                    infoSession.Invalide = true;
                    session = GetSessionClientSurServeur(nId);
                    infoSession.NomUtilisateur = I.T("ACCESS ERROR|20000");
                }

                try
                {
                    infoSession.NomUtilisateur = session.GetInfoUtilisateur().NomUtilisateur;
                    IInfoUtilisateur info = session.GetInfoUtilisateur();
                    //TESTDBKEYOK
                    keyUtilisateur = info.KeyUtilisateur;
                    infoSession.KeyUtilisateur = info.KeyUtilisateur;
                    DateTime dt = session.DateHeureConnexion;
                    infoSession.DateDebutConnexion = dt;
                    infoSession.DateDerniereActivité = session.DateHeureDerniereActivite;
                    infoSession.LibelleSession = session.DescriptionApplicationCliente;
                }
                catch
                {
                    if (nId == 0)
                        infoSession.NomUtilisateur = I.T("Server|20001");
                    else
                    {
                        infoSession.Invalide = true;
                        infoSession.NomUtilisateur = I.T("ACCESS ERROR|20000");
                    }
                }
                infoSession.IsSystem = keyUtilisateur == null;
                lst.Add(infoSession);
            }
            return lst.ToArray();
        }

        #endregion
    }

	//Surveille que les connexions ne se deconnectent pas !!!
	//Si c'est le cas, fait le nécéssaire
	//////////////////////////////////////////////////////////////////:
	internal class CSessionTracker : ITrackingHandler
	{
		// Notify a handler that an object has been marshaled.
		public void MarshaledObject(Object obj, ObjRef or)
		{
		}

		// Notify a handler that an object has been unmarshaled.
		public void UnmarshaledObject(Object obj, ObjRef or)
		{
		}

		// Notify a handler that an object has been disconnected.
		public void DisconnectedObject(Object obj)
		{
			if ( obj is CSessionClientSurServeur )
			{
                bool bConnected = false;
				CSessionClientSurServeur session = (CSessionClientSurServeur)obj;
				try
				{
                    CSessionClientSurServeur sc = CGestionnaireSessions.GetSessionClientSurServeurStatic(session.IdSession) as CSessionClientSurServeur;
					if (CGestionnaireSessions.IsSessionOpenStatic(session.IdSession))
					{
                        
                        //
                        try
                        {
                            //Tente de contacter la session cliente
                            bConnected = sc == null || sc.SessionClient.IsConnected;
                        }
                        catch
                        {
                        }
                        if( !bConnected )
                        {
                            string strMessage = I.T("Auto disconnection session number @1|109", session.IdSession.ToString()) +
                                I.T("\r\n|108") + I.T("Type|107") + session.TypeApplicationCliente.ToString();
                            strMessage += I.T("\r\n|108") + session.DescriptionApplicationCliente;
                            try
                            {
                                strMessage += I.T("\r\n|108") + session.GetInfoUtilisateur().NomUtilisateur;
                            }
                            catch
                            { }
                            C2iEventLog.WriteErreur(strMessage);
                        }
					}
				}
				catch{}
                if( !bConnected )
				    session.CloseSession();
				
			}
		}

		
	}
}
