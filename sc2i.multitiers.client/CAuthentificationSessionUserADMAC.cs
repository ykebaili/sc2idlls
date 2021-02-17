using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using System.Threading;
using System.Security.Principal;

namespace sc2i.multitiers.client
{
#if PDA
#else
    [Serializable]
#endif
    public class CAuthentificationSessionUserADMAC : CAuthentificationSessionUserAD
    {
        private string [] m_MAC;

        public CAuthentificationSessionUserADMAC()
        {
        }

        public CAuthentificationSessionUserADMAC(IIdentity id, string [] strMAC)
            :base( id)
        {
            m_MAC = strMAC;
		
		}

        public CAuthentificationSessionUserADMAC(string [] strMAC)
            : base()
        {
            m_MAC = strMAC;

        }

        public string [] MAC
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
