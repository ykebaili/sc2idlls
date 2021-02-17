using System;

using sc2i.multitiers.client;
using sc2i.common;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CDonneeNotificationBesoinIntervention.
	/// </summary>
	[Serializable]
	public class CDonneeNotificationBesoinIntervention : IDonneeNotification
	{
		private int m_nIdSessionEnvoyeur = -1;
        private CDbKey m_keyUtilisateurConcerne = null;
        private string m_strLibelle = "";
        private int m_nIdBesoinUtilisateur = -1;
        private bool m_bIsDelete = false;

		/// //////////////////////////////////////////////////////////
		public CDonneeNotificationBesoinIntervention( 
            int nIdSession, 
            CDbKey keyUtilisateurIntervention,
            int nIdBesoinUtilisateur,
            string strLibelle,
            bool bIsDelete)
		{
            //TESTDBKEYTODO
			m_nIdSessionEnvoyeur = nIdSession;
            m_keyUtilisateurConcerne = keyUtilisateurIntervention;
            m_strLibelle = strLibelle;
            m_nIdBesoinUtilisateur = nIdBesoinUtilisateur;
            m_bIsDelete = bIsDelete;
		
        }

        /// //////////////////////////////////////////////////////////
        public bool IsDelete
        {
            get
            {
                return m_bIsDelete;
            }
        }

        /// //////////////////////////////////////////////////////////
        public string Libelle
        {
            get
            {
                return m_strLibelle;
            }
        }

        /// //////////////////////////////////////////////////////////
        public int IdBesoinUtilisateur
        {
            get
            {
                return m_nIdBesoinUtilisateur;
            }
        }

		/// //////////////////////////////////////////////////////////
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

		/// ////////////////////////////////////////////////////////////////////
		public int PrioriteNotification
		{
			get
			{
				return 100;
			}
		}

		/// //////////////////////////////////////////////////////////
		public CDbKey KeyUtilisateurConcerne
		{
			get
			{
				return m_keyUtilisateurConcerne;
			}
		}

	}
}
