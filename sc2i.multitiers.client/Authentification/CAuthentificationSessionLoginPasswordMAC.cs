using System;
using System.Collections.Generic;
using System.Text;


namespace sc2i.multitiers.client
{
#if PDA
#else
    [Serializable]
#endif
    public class CAuthentificationSessionLoginPasswordMAC : CAuthentificationSessionLoginPassword
    {
        private string [] m_MAC;

        public CAuthentificationSessionLoginPasswordMAC()
		{
		}

        public CAuthentificationSessionLoginPasswordMAC(string strLogin, string strPassword, string[] strMAC)
            :base( strLogin,  strPassword)
        {
            m_MAC = strMAC;
		
		}

        public string[] MAC
        {
            get
            {
                return m_MAC;
            }
            set
            {
                m_MAC = value;
            }
        }
    }
}
