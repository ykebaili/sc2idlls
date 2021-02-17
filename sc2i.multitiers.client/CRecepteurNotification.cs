using System;
using System.Diagnostics;
using sc2i.common;

namespace sc2i.multitiers.client
{
	
	/// <summary>
	/// Donnée de notification
	/// </summary>
	public interface IDonneeNotification
	{
        int IdSessionEnvoyeur { get;set;}

		/// <summary>
		/// Les notifications de priorité supérieur sont envoyées avant les autres 
		/// Dans une transaction de notifications
		/// </summary>
		int PrioriteNotification{get;}
	}

	/// <summary>
	/// Handler de message de notification
	/// </summary>
	public delegate void NotificationEventHandler(IDonneeNotification donnee);


	
	/// ///////////////////////////////////////////////////////
	/// <summary>
	/// //Objet recevant des notifications de la part d'un serveur de notifications
	/// </summary>
	public class CRecepteurNotification : IDisposable
	{
		private bool m_bEnabled = true;
		/// ///////////////////////////////////////////////////////
		/// <summary>
		/// Evenement se produisant lorsque le récepteur reçoit une notification
		/// </summary>
		public event NotificationEventHandler OnReceiveNotification;

		private int m_nIdSession;
		
		private int m_nIdRecepteur = -1;
		
		/// ///////////////////////////////////////////////////////
		/// Déclare le nouveau recepteur et l'enregistre auprès du gestionnaire
		/// de notification du système.
		public CRecepteurNotification( int nIdSession, Type typeDeNotificationGeree)
		{
			m_nIdSession = nIdSession;
			CSessionClient session = CSessionClient.GetSessionForIdSession(nIdSession);
			if (session == null)
				throw (new Exception(I.T("The @1 session doesn't exist|104",nIdSession.ToString())));
			try
			{
				session.RegisterRecepteurNotification(this, typeDeNotificationGeree);
			}
			catch (Exception e)
			{
				System.Console.WriteLine("Erreur enregistrement de notification " + e.ToString());
			}
			/*IGestionnaireNotification gestionnaire = (IGestionnaireNotification)C2iFactory.GetNewObjetForSession("CGestionnaireNotification", typeof(IGestionnaireNotification), m_nIdSession);
			if ( gestionnaire != null )
				gestionnaire.RegisterRecepteur(this, typeDeNotificationGeree);
			CGestionnaireObjetsAttachesASession.AttacheObjet(nIdSession, this);*/
		}

		/// ///////////////////////////////////////////////////////
		~CRecepteurNotification()
		{
			Dispose();
		}

		/// ///////////////////////////////////////////////////////
		public int IdRecepteur
		{
			get
			{
				return m_nIdRecepteur;
			}
			set
			{
				m_nIdRecepteur = value;
			}
		}


		/// ///////////////////////////////////////////////////////
		private bool m_bDisposed = false;
		public void Dispose()
		{
			if ( m_bDisposed ) return;
			try
			{
				CSessionClient session = CSessionClient.GetSessionForIdSession(IdSession);
				if (session != null)
					session.UnregisterRecepteurNotification(this);
				/*CGestionnaireObjetsAttachesASession.DetacheObjet ( IdSession, this );
				IGestionnaireNotification gestionnaire = (IGestionnaireNotification)C2iFactory.GetNewObjetForSession("CGestionnaireNotification", typeof(IGestionnaireNotification), m_nIdSession);
				if ( gestionnaire != null )
					gestionnaire.UnregisterRecepteur ( this );*/
				m_bDisposed = true;
			}
			catch{}
			
		}


		/// ///////////////////////////////////////////////////////
		public void OnCloseSession ()
		{
			Dispose();
		}

		/// ///////////////////////////////////////////////////////
		public void RecoitNotification ( IDonneeNotification donnee )
		{
			if ( Enabled && OnReceiveNotification != null )
				OnReceiveNotification ( donnee );
		}

		/// ///////////////////////////////////////////////////////
		public bool Enabled
		{
			get
			{
				return m_bEnabled;
			}
			set
			{
				m_bEnabled = value;
			}
		}

		/// ///////////////////////////////////////////////////////
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
		public void RenouvelleBailParAppel()
		{
		}

		

	}
}
