using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.common
{
    /// <summary>
    /// Sérializée dans le thread pour suivre les session en cours
    /// </summary>
    [Serializable]
    public class IdSessionUtilisateurData : ILogicalThreadAffinative
    {
        public Stack m_IdSessions = new Stack();

        public IdSessionUtilisateurData()
        {
        }

        public int IdSession
        {
            get
            {
                return (int)m_IdSessions.Peek();
            }

        }

        public void PushIdSession(int nIdSession)
        {
            m_IdSessions.Push(nIdSession);
        }

        public void PopIdSession()
        {
            if (m_IdSessions.Count > 0)
                m_IdSessions.Pop();
        }

        public bool HasIdSession
        {
            get
            {
                return m_IdSessions.ToArray().Length > 0;
            }
        }
    }
}
