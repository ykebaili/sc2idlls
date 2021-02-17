using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace futurocom.chart
{
    public class CValeurPourChartAction
    {
        private object m_valeur = null;

        [DynamicField("Value for action")]
        public object ValueForAction
        {
            get
            {
                return m_valeur;
            }
            set
            {
                m_valeur = value;
            }
        }
    }
}
