using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.trace
{
    public delegate void OnTraceMessageEventHander ( string strMessage, params string[] strCategories );

    public class CFuturocomTraceListenerAMessage : IFuturocomTraceListener
    {
        private string[] m_strCategories = new string[0];

        public CFuturocomTraceListenerAMessage(params string[] strCategories)
        {
            m_strCategories = strCategories;
        }

        public event OnTraceMessageEventHander OnTraceMessage;



        public void WriteTrace(string strMessage, params string[] strCategories)
        {
            if (OnTraceMessage != null)
                OnTraceMessage(strMessage, strCategories);
        }

        public string[] CategoriesEcoutees
        {
            get { return m_strCategories; }
        }

        public void Dispose()
        {
        }
    }
}
