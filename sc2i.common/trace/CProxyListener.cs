using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.trace
{
    public class CProxyListener : MarshalByRefObject, IFuturocomTraceListener, IEquatable<IFuturocomTraceListener>
    {
        private IFuturocomTraceListener m_listener = null;
        private string[] m_strCategoriesEcoutees = new string[0];
        private C2iSponsor m_sponsor = new C2iSponsor();
        private int m_nHashCode = 0;

        //-----------------------------------------------------------
        public CProxyListener(IFuturocomTraceListener listener)
        {
            m_listener = listener;
            m_sponsor.Register(listener);
            if (m_listener != null)
            {
                m_strCategoriesEcoutees = listener.CategoriesEcoutees;
                m_nHashCode = m_listener.GetHashCode();
            }
        }

        //-----------------------------------------------------------
        public void WriteTrace(string strMessage, params string[] strCategories)
        {
            if ( m_listener != null )
                m_listener.WriteTrace(strMessage, strCategories);
        }

        //-----------------------------------------------------------
        public string[] CategoriesEcoutees
        {
            get { return m_strCategoriesEcoutees; }
        }

        //-----------------------------------------------------------
        public void Dispose()
        {
            if ( m_sponsor != null )
                m_sponsor.Dispose();
            m_sponsor = null;
            if (m_listener != null)
                m_listener.Dispose();
            m_listener = null;
        }

        //-----------------------------------------------------------
        public override int GetHashCode()
        {
            return m_nHashCode;
        }

        //-----------------------------------------------------------
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            return GetHashCode().Equals(obj.GetHashCode());
        }

        //-----------------------------------------------------------
        public bool Equals(IFuturocomTraceListener other)
        {
            return other.GetHashCode().Equals(GetHashCode());
        }


    }
}
