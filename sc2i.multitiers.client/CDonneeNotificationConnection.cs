using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.multitiers.client
{
	//---------------------------------------------
	[Serializable]
	public class CDonneeNotificationConnection : IDonneeNotification
	{
		private int m_nIdSessionEnvoyeur = -1;
		private bool m_bIsConnexion = true;

		//---------------------------------------------
		public CDonneeNotificationConnection ( int nIdSession, bool bIsConnexion )
		{
			m_nIdSessionEnvoyeur = nIdSession ;
			m_bIsConnexion = bIsConnexion;
		}


		//---------------------------------------------
		public int IdSessionEnvoyeur
		{
			get
			{
				return m_nIdSessionEnvoyeur;
			}
			set
			{
				m_nIdSessionEnvoyeur = value;
			}
		}

		//---------------------------------------------
		//Si false, c'est une déconnexion
		public bool IsConnexion
		{
			get
			{
				return m_bIsConnexion;
			}
			set
			{
				m_bIsConnexion = value;
			}
		}

		//---------------------------------------------
		public int PrioriteNotification
		{
			get { return 0; }
		}
	}
}
