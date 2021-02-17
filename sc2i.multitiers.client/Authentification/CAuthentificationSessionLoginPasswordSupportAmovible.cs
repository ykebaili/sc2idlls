using System;
using System.Collections.Generic;
using System.Text;


namespace sc2i.multitiers.client
{
#if PDA
#else
    [Serializable]
#endif
    public class CAuthentificationSessionLoginPasswordSupportAmovible : CAuthentificationSessionLoginPassword
    {
        private string m_strIdSupport;

        public CAuthentificationSessionLoginPasswordSupportAmovible()
		{
		}

		public CAuthentificationSessionLoginPasswordSupportAmovible(string strLogin, string strPassword, string strIdSupport)
            :base( strLogin,  strPassword)
        {
            m_strIdSupport = strIdSupport;
		
		}

        public string IdSupportAmovible
        {
            get
            {
                return m_strIdSupport;
            }
            set
            {
                m_strIdSupport = value;
            }
        }
    }
}
