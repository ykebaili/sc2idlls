using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using sc2i.formulaire.datagrid;
using sc2i.formulaire.win32.controles2iWnd.datagrid;
using sc2i.expression;
using sc2i.common;

namespace sc2i.formulaire.win32.datagrid
{
    public class CGridDataCache : IDisposable
    {
        private const int c_nMaxThread = 10;

        
        private class CObjetDataCache
        {
            public object Objet { get; set; }
            private Dictionary<int, CCoupleValeurEtValeurDisplay> m_dicValeurs = new Dictionary<int, CCoupleValeurEtValeurDisplay>();

            public CObjetDataCache(object objet)
            {
                Objet = objet;
            }

            public CCoupleValeurEtValeurDisplay GetValeur(int nCol)
            {
                CCoupleValeurEtValeurDisplay data = null;
                m_dicValeurs.TryGetValue(nCol, out data);
                return data;
            }


            public void SetValeur(int nCol, string strVal, object valeur)
            {
                if (strVal == null & m_dicValeurs.ContainsKey(nCol))
                {
                    m_dicValeurs.Remove(nCol);
                }
                else
                {
                    m_dicValeurs[nCol] = new CCoupleValeurEtValeurDisplay(strVal, valeur);
                }
            }
        }

        private Dictionary<object, CObjetDataCache> m_cache = new Dictionary<object, CObjetDataCache>();

        private List<CThreadData> m_listeThreadToStart = new List<CThreadData>();
        private List<Thread> m_listeThreadStarted = new List<Thread>();

        public const string c_strWaitingData = "##€__|`[{#";

        private Timer m_timer = null;

        private Dictionary<int, IWndIncluableDansDataGrid> m_dicControles = new Dictionary<int, IWndIncluableDansDataGrid>();

        private CDataGridForFormulaire m_grid = null;

        private Dictionary<int, C2iExpression> m_dicNumColToFormuleElementEdite = new Dictionary<int, C2iExpression>();
        
        public CGridDataCache(CDataGridForFormulaire grid)
        {
            m_grid = grid;
            m_timer = new Timer(new TimerCallback(StartThreadTask), null, 100, 100);
        }

        public void RegisterControle(int nCol, IWndIncluableDansDataGrid ctrl)
        {
            m_dicControles[nCol] = ctrl;
        }

        private class CLockThread { }
        public void Dispose()
        {
            lock (typeof(CLockThread))
            {
                foreach (Thread th in m_listeThreadStarted)
                {
                    try
                    {
                        th.Abort();
                    }
                    catch { }
                }
            }
            m_listeThreadToStart.Clear();
        }

        private class CThreadData
        {
            public object Objet{get;set;}
            public int ColumnIndex{get;set;}
            public CObjetDataCache Cache { get; set; }

            public CThreadData(object ob, int nCol, CObjetDataCache cache)
            {
                Objet = ob;
                ColumnIndex = nCol;
                Cache = cache;
            }
        }

        public CCoupleValeurEtValeurDisplay GetValeur(object objet, int nColonne, bool bMultiThread)
        {
            if (objet == null)
                return new CCoupleValeurEtValeurDisplay("", null);
            CObjetDataCache cache = null;
            try
            {
                if (!m_cache.TryGetValue(objet, out cache))
                {
                    cache = new CObjetDataCache(objet);
                    m_cache[objet] = cache;
                }
            }
            finally
            {
                if (cache == null)
                {
                    cache = new CObjetDataCache(objet);
                    m_cache[objet] = cache;
                }
            }
            CCoupleValeurEtValeurDisplay data = cache.GetValeur(nColonne);

            if (data != null)
                return data;

            if (bMultiThread)
            {
                cache.SetValeur(nColonne, c_strWaitingData, null);
                CThreadData thread = new CThreadData(objet, nColonne, cache);
                lock (typeof(CLockThread))
                {
                    m_listeThreadToStart.Add(thread);
                }
                return new CCoupleValeurEtValeurDisplay(c_strWaitingData, null);
            }
            else
            {
                data = GetValeurHorsCache(objet, nColonne, cache);
                return data;
            }

        }

        public void SetValeur(object objet, int nColonne, string strValeur, object objValeur)
        {
            if (objet == null)
                return;
            CObjetDataCache cache = null;
            if (!m_cache.TryGetValue(objet, out cache))
            {
                cache = new CObjetDataCache(objet);
                m_cache[objet] = cache;
            }
            if (strValeur == null)
                cache.SetValeur(nColonne, null, null);
            else 
                cache.SetValeur(nColonne, strValeur, objValeur);
        }




        private bool m_bInStartThreadTask = false;
        private void StartThreadTask(object state)
        {
            if (m_bInStartThreadTask)
                return;
            m_bInStartThreadTask = true;
            try
            {
                lock (typeof(CLockThread))
                {
                    IEnumerable<Thread> threads = from t in m_listeThreadStarted
                                                  where
                                                      t.ThreadState != ThreadState.Running
                                                  select t;
                    foreach (Thread thToDelete in threads.ToArray())
                        m_listeThreadStarted.Remove(thToDelete);


                    if (m_listeThreadToStart.Count == 0)
                        return;
                    while (m_listeThreadToStart.Count > 0 && m_listeThreadStarted.Count < c_nMaxThread)
                    {
                        CThreadData data = m_listeThreadToStart[0];
                        m_listeThreadToStart.RemoveAt(0);
                        Thread th = new Thread(GetDataThread);
                        th.Priority = ThreadPriority.BelowNormal;
                        m_listeThreadStarted.Add(th);
                        th.Start(data);
                    }
                }
            }
            finally
            {
                m_bInStartThreadTask = false;
            }
        }

        private Dictionary<int, Dictionary<object, object>> m_dicColToObjetsEdites = new Dictionary<int, Dictionary<object, object>>();
        public object GetElementEdite(object source, int nColumnIndex)
        {
            if ( source == null )
                return null;
            Dictionary<object, object> dicObjetToElementEdite = null;
            C2iExpression formule = null;
            if (!m_dicNumColToFormuleElementEdite.TryGetValue(nColumnIndex, out formule))
            {
                C2iWndDataGridColumn col = m_grid.WndGrid.GetColumn(nColumnIndex);
                if (col != null)
                    formule = col.AlternativeEditedElement;
                m_dicNumColToFormuleElementEdite[nColumnIndex] = formule;
            }
            if (!m_dicColToObjetsEdites.TryGetValue(nColumnIndex, out dicObjetToElementEdite))
            {
                if (formule != null)
                {
                    dicObjetToElementEdite = new Dictionary<object, object>();
                    m_dicColToObjetsEdites[nColumnIndex] = dicObjetToElementEdite;
                }
            }
            object retour = source;
            if (dicObjetToElementEdite != null)
            {
                retour = null;
                if (!dicObjetToElementEdite.TryGetValue(source, out retour))
                {
                    CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(source);
                    try
                    {
                        CResultAErreur result = formule.Eval(ctx);
                        if (result)
                            retour = result.Data;
                    }
                    catch { }
                    dicObjetToElementEdite[source] = retour;
                }
            }
            return retour;
        }

        private CCoupleValeurEtValeurDisplay GetValeurHorsCache(object objet, int nColumnIndex, CObjetDataCache cache)
        {
            IWndIncluableDansDataGrid ctrl = null;
            if (m_dicControles.TryGetValue(nColumnIndex, out ctrl))
            {
                try
                {
                    object elementEdite = GetElementEdite(objet, nColumnIndex);
                    object val = ctrl.GetObjectValueForGrid(elementEdite);
                    string strVal = ctrl.ConvertObjectValueToStringForGrid(val);
                    cache.SetValeur(nColumnIndex, strVal, val);
                    return new CCoupleValeurEtValeurDisplay(strVal, val);
                }
                catch
                {
                    cache.SetValeur(nColumnIndex, "", null);
                }
            }
            return new CCoupleValeurEtValeurDisplay("", null);
        }

        private void GetDataThread ( object data )
        {
            CThreadData d = data as CThreadData;
            if (d != null)
            {

                IWndIncluableDansDataGrid ctrl = null;
                if (m_dicControles.TryGetValue(d.ColumnIndex, out ctrl))
                {
                    CCoupleValeurEtValeurDisplay dTmp = GetValeurHorsCache(d.Objet, d.ColumnIndex, d.Cache);
                    m_grid.OnDataArrivé(d.Objet, d.ColumnIndex);
                }
            }
        }







    }
}


