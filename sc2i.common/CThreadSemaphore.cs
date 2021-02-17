using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace sc2i.common
{
    /// <summary>
    /// Un sémaphore qui assure que deux thread différents
    /// n'accèdent pas à la même ressource. Par contre, un même
    /// thread peut accéder tant qu'il veut à la ressource
    /// </summary>
    public class CThreadSemaphore : MarshalByRefObject
    {
        private string m_strId = Guid.NewGuid().ToString();

        private Dictionary<string, Semaphore> m_dicSemaphores = new Dictionary<string, Semaphore>();

        private string GetKey(string strSemaphore)
        {
            return m_strId + strSemaphore.ToUpper();
        }

        //-------------------------------------------------
        public void WaitOne(string strSemaphore)
        {
            int nThreadId = Thread.CurrentThread.ManagedThreadId;
            object obj = Thread.GetData(Thread.GetNamedDataSlot(GetKey(strSemaphore)));
            if (obj is int)
            {
                int nVal = (int)obj;
                if (nVal > 0)
                {
                    nVal++;
                    Thread.SetData(Thread.GetNamedDataSlot(GetKey(strSemaphore)), nVal);
                    return;
                }
            }

            Semaphore sem = null;
            lock (m_dicSemaphores)
            {
                if (!m_dicSemaphores.TryGetValue(strSemaphore.ToUpper(), out sem))
                {
                    sem = new Semaphore(1, 1);
                    m_dicSemaphores[strSemaphore.ToUpper()] = sem;
                }
            }
            sem.WaitOne();
            Thread.SetData(Thread.GetNamedDataSlot(GetKey(strSemaphore)), 1);
        }

        //-------------------------------------------------
        public void Release(string strSemaphore)
        {
            int nThreadId = Thread.CurrentThread.ManagedThreadId;
            object obj = Thread.GetData(Thread.GetNamedDataSlot(GetKey(strSemaphore)));
            if (obj is int)
            {
                int nVal = (int)obj;
                nVal--;
                Thread.SetData(Thread.GetNamedDataSlot(GetKey(strSemaphore)), nVal);
                if (nVal == 0)
                {
                    Semaphore sem = null;
                    if (m_dicSemaphores.TryGetValue(strSemaphore.ToUpper(), out sem))
                        sem.Release();
                }
            }
        }

        //-------------------------------------------------
        public CJetonThread GetJeton(string strSemaphore)
        {
            return new CJetonThread(this, strSemaphore);
        }
    }

    //-------------------------------------------------
    public class CJetonThread : MarshalByRefObject, IDisposable
    {
        private CThreadSemaphore m_semaphore = null;
        private string m_strSemaphore = "";


        public CJetonThread(CThreadSemaphore sem, string strSemaphore)
        {
            m_semaphore = sem;
            m_strSemaphore = strSemaphore;
            if ( m_semaphore != null )
                m_semaphore.WaitOne(strSemaphore);
        }

        #region IDisposable Membres

        public virtual void Dispose()
        {
            if ( m_semaphore != null )
                m_semaphore.Release(m_strSemaphore);
        }

        #endregion
    }
}
