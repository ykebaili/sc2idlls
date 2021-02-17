using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.data
{
    /// <summary>
    /// Indexs qui ne sont pas liés à une relation ou à un seul champ
    /// </summary>
    [Serializable]
    public class CInfoIndexTable
    {
        private string[] m_strChamps = new string[0];
        private bool m_bIsCluster = false;

        public CInfoIndexTable(params string[] strChamps)
        {
            m_strChamps = strChamps;
        }

        public string[] Champs
        {
            get
            {
                return m_strChamps;
            }
        }

        public bool IsCluster
        {
            get
            {
                return m_bIsCluster;
            }
            set
            {
                m_bIsCluster = value;
            }
        }
    }
}
