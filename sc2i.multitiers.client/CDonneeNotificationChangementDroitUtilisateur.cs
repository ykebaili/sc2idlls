using System;
using sc2i.common;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Description résumée de CDonneeNotificationChangementDroitUtilisateur.
	/// </summary>
	[Serializable]
	public class CDonneeNotificationChangementDroitUtilisateur : IDonneeNotification
	{
		public readonly CDbKey KeyUtilisateur;
		private int m_nIdSessionEnvoyeur;
		
		public CDonneeNotificationChangementDroitUtilisateur( int nIdSession, CDbKey keyUtilisateur )
		{
            //TESTDBKEYOK
			m_nIdSessionEnvoyeur = nIdSession;
			KeyUtilisateur = keyUtilisateur;
		}

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

		public int PrioriteNotification
		{
			get
			{
				return 100;
			}
		}

		public override bool Equals ( object obj )
		{
			if ( !(obj is CDonneeNotificationChangementDroitUtilisateur) )
				return false;
			CDonneeNotificationChangementDroitUtilisateur dd = (CDonneeNotificationChangementDroitUtilisateur)obj;
			return dd.KeyUtilisateur == KeyUtilisateur && dd.IdSessionEnvoyeur==IdSessionEnvoyeur;
		}

		public override int GetHashCode()
		{
            return (KeyUtilisateur.StringValue+ "_" + IdSessionEnvoyeur.GetHashCode()).GetHashCode();
		}


	}
}
