using System;
using System.Collections;

using sc2i.common;
using sc2i.multitiers.client;

namespace sc2i.multitiers.server
{
	/// <summary>
	/// Description résumée de CGestionnaireTransactionsNotification.
	/// </summary>
	public class CGestionnaireTransactionsNotification: 
        IServiceTransactions,
        IObjetAttacheASession,
        IDisposable
	{
		private int m_nNbTransactions = 0;
		private int m_nIdSession = -1;
		private ArrayList m_liste = new ArrayList();
		
		/// /////////////////////////////////////////////////////////////
		public CGestionnaireTransactionsNotification( int nIdSession)
		{
			m_nIdSession = nIdSession;
			CGestionnaireObjetsAttachesASession.AttacheObjet(nIdSession, this);
		}

        #region IDisposable Membres

        public void Dispose()
        {
            CGestionnaireObjetsAttachesASession.DetacheObjet(m_nIdSession, this);
        }

        #endregion
        
        /// /////////////////////////////////////////////////////////////
		public int IdSession
		{
			get
			{
				return m_nIdSession;
			}
		}

		/// /////////////////////////////////////////////////////////////
		public CResultAErreur BeginTrans()
		{
			CResultAErreur result = CResultAErreur.True;
			m_nNbTransactions++;
			return result;
		}

		/// /////////////////////////////////////////////////////////////
		public CResultAErreur BeginTrans(System.Data.IsolationLevel isolationLevel)
		{
			return BeginTrans();
		}

		/// /////////////////////////////////////////////////////////////
		private class NotificationSorter : IComparer
		{
			#region Membres de IComparer

			public int Compare(object x, object y)
			{
				return ( ((IDonneeNotification)y).PrioriteNotification.CompareTo(
					((IDonneeNotification)x).PrioriteNotification ) );
			}

			#endregion

		}


		/// /////////////////////////////////////////////////////////////
		public CResultAErreur CommitTrans()
		{
			CResultAErreur result = CResultAErreur.True;
			m_nNbTransactions--;
			if ( m_nNbTransactions <= 0 )
			{
				m_nNbTransactions = 0;
				m_liste.Sort ( new NotificationSorter() );
				EnvoieNotifications ( (IDonneeNotification[])m_liste.ToArray ( typeof(IDonneeNotification) ));
				m_liste = new ArrayList();
			}
			return result;
		}

		/// /////////////////////////////////////////////////////////////
		public CResultAErreur RollbackTrans()
		{
			CResultAErreur result = CResultAErreur.True;
			m_nNbTransactions--;
			if ( m_nNbTransactions <= 0 )
			{
				m_nNbTransactions = 0;
				m_liste = new ArrayList();
			}
			return result;
		}

		/// /////////////////////////////////////////////////////////////
		public bool AccepteTransactionsImbriquees
		{
			get
			{
				return true;
			}
		}

		


		/// /////////////////////////////////////////////////////////////
		public void EnvoieNotifications ( IDonneeNotification[] donnees )
		{
			if (donnees.Length == 0)
				return;
			if ( m_nNbTransactions > 0 )
			{
				foreach ( IDonneeNotification donnee in donnees )
					if ( !m_liste.Contains(donnee))
						m_liste.Add (donnee);
			}
			else
			{
				new CGestionnaireNotification(donnees[0].IdSessionEnvoyeur).EnvoieNotificationsHorsTransaction(donnees);
			}

		}



		#region IObjetAttacheASession Membres

		public string DescriptifObjetAttacheASession
		{
			get { return "Transaction"; }
		}

		public void OnCloseSession()
		{
			CGestionnaireNotification.ReleaseGestionnaire(this);	
		}

		#endregion

    }
}
