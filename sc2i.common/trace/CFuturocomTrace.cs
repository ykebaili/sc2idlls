using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.trace
{
    public interface IFuturocomTraceListener : IDisposable
    {
        void WriteTrace(string strMessage, params string[] strCategories);

        string[] CategoriesEcoutees { get; }
    }

    public class CFuturocomTrace : IDisposable
    {
        private List<IFuturocomTraceListener> m_listeListeners = new List<IFuturocomTraceListener>();
        private Dictionary<string, List<IFuturocomTraceListener>> m_dicCategorieToListener = new Dictionary<string, List<IFuturocomTraceListener>>();
        private C2iSponsor m_sponsor = new C2iSponsor();

        private class CLockerListeListeners { }

        //--------------------------------------------------------------
        public void RegisterListener(IFuturocomTraceListener listener)
        {
            lock (typeof(CLockerListeListeners))
            {
                m_listeListeners.Add(listener);
                m_sponsor.Register(listener);
                foreach (string strCat in listener.CategoriesEcoutees)
                {
                    List<IFuturocomTraceListener> lst = null;
                    if (!m_dicCategorieToListener.TryGetValue(strCat, out lst))
                    {
                        lst = new List<IFuturocomTraceListener>();
                        m_dicCategorieToListener[strCat] = lst;
                    }
                    lst.Add(listener);
                }
            }

        }

        //--------------------------------------------------------------
        public void UnregistrerListener(IFuturocomTraceListener listener)
        {
            for (int nTry = 0; nTry < 2; nTry++)
            {
                try
                {
                    lock (typeof(CLockerListeListeners))
                    {
                        m_listeListeners.Remove(listener);
                        m_sponsor.Unregister(listener);
                        foreach (string strCat in listener.CategoriesEcoutees)
                        {
                            List<IFuturocomTraceListener> lst = null;
                            if (m_dicCategorieToListener.TryGetValue(strCat, out lst))
                                lst.Remove(listener);
                        }
                        return;
                    }
                }
                catch
                {
                    DeleteListenersEnErreur();
                }
            }

        }

        //--------------------------------------------------------------
        public void Write(string strMessage, params string[] strCategories)
        {
            List<IFuturocomTraceListener> lst = null;
            HashSet<IFuturocomTraceListener> listenerFaits = new HashSet<IFuturocomTraceListener>();
            foreach (string strCategorie in strCategories)
            {
                if (m_dicCategorieToListener.TryGetValue(strCategorie, out lst))
                {
                    for (int nTry = 0; nTry < 2; nTry++)
                    {
                        try
                        {
                            foreach (IFuturocomTraceListener listener in lst)
                            {

                                if (!listenerFaits.Contains(listener))
                                {
                                    listener.WriteTrace(strMessage, strCategories);
                                }
                                listenerFaits.Add(listener);
                            }
                        }
                        catch
                        {
                            DeleteListenersEnErreur();
                        }
                    }
                }
            }
        }

        //--------------------------------------------------------------
        private void DeleteListenersEnErreur()
        {
            lock (typeof(CLockerListeListeners))
            {
                C2iEventLog.WriteErreur("Error on trace system. Cleaning up");
                DeleteListenersEnErreur(m_listeListeners);
                foreach (KeyValuePair<string, List<IFuturocomTraceListener>> kv in m_dicCategorieToListener)
                {
                    DeleteListenersEnErreur(kv.Value);
                }
            }
        }

        //-----------------------------------------------------------------------------
        private void DeleteListenersEnErreur(List<IFuturocomTraceListener> lst)
        {
            List<int> lstToDestroy = new List<int>();
            for (int n = lst.Count - 1; n >= 0; n--)
            {
                IFuturocomTraceListener listener = lst[n];
                try
                {
                    listener.ToString();
                }
                catch
                {
                    lstToDestroy.Add(n);
                }
            }
            foreach (int nToDestroy in lstToDestroy)
                lst.RemoveAt(nToDestroy);
        }

        //--------------------------------------------------------------
        public void Dispose()
        {
            if (m_sponsor != null)
                m_sponsor.Dispose();
        }

        //--------------------------------------------------------------
    }
}
