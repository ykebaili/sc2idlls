using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.multitiers.client;

namespace sc2i.process.workflow
{
    [Serializable]
    public class CDonneeNotificationWorkflow : IDonneeNotification
    {
        private int m_nIdSessionEnvoyeur = -1;
        private int m_nIdEtapeSource = -1;
        private string m_strCodeAffectations = "";
        private bool m_bAutoexec = false;
        private string m_strLibelle = "";

        /// //////////////////////////////////////////////////////////
        public CDonneeNotificationWorkflow(
            int nIdSession, 
            int nIdEtapeSource,
            string strLibelle, 
            string strCodesAffectations,
            bool bAutoexec)
        {
            m_nIdSessionEnvoyeur = nIdSession;
            m_nIdEtapeSource = nIdEtapeSource;
            m_strLibelle = strLibelle;
            m_strCodeAffectations = strCodesAffectations;
            m_bAutoexec = bAutoexec;
        }

        /// //////////////////////////////////////////////////////////
        public bool ExecutionAutomatique
        {
            get
            {
                return m_bAutoexec;
            }
            set
            {
                m_bAutoexec = value;
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

        /// ////////////////////////////////////////////////////////////////////
        public int IdEtapeSource
        {
            get
            {
                return m_nIdEtapeSource;
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

        /// ////////////////////////////////////////////////////////////////////
        public string CodesAffectations
        {
            get
            {
                return m_strCodeAffectations;
            }
        }

    }
}
