using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.SimpleMatch
{
    public class CSimpleMatchPlageMinMax : ISimpleMatch
    {
        private string m_strMin = "";
        private string m_strMax = "";
        private int? m_nMin = null;
        private int? m_nMax = null;


        //-----------------------------------------
        public CSimpleMatchPlageMinMax()
        {
        }

        //-----------------------------------------
        public CSimpleMatchPlageMinMax(string strMin, string strMax)
        {
            Max = strMax;
            Min = strMin;
        }

        //-----------------------------------------
        public string Min
        {
            get
            {
                return m_strMin;
            }
            set
            {
                m_strMin = value;
                m_nMin = null;
                try
                {
                    m_nMin = Int32.Parse(value);
                }
                catch { }
            }
        }

        //-----------------------------------------
        public string Max
        {
            get
            {
                return m_strMax;
            }
            set
            {
                m_strMax = value;
                m_nMax = null;
                try
                {
                    m_nMax = Int32.Parse(value);
                }
                catch { }
            }
        }


        //-----------------------------------------
        public bool Match(string strChaine)
        {
            if (m_nMin != null && m_nMax != null)
            {
                try
                {
                    int nVal = Int32.Parse(strChaine);
                    return nVal >= m_nMin.Value && nVal <= m_nMax.Value;
                }
                catch { }
            }
            bool bResult =
                (m_strMin != "" ? m_strMin.CompareTo(strChaine) <= 0 : true) &&
                (m_strMin != "" ? m_strMax.CompareTo(strChaine) >= 0 : true);
            return bResult;
        }

        //-----------------------------------------
        public string GetString()
        {
            return m_strMin + "-" + m_strMax;            
        }

        //-----------------------------------------
        public bool IsValide()
        {
            return true;
        }


    }
}
