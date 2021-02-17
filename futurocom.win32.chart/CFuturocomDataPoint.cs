using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace futurocom.win32.chart
{
    public class CFuturocomDataPoint : DataPoint
    {
        private object m_valeurPourAction = null;
        private object m_valeurSort = null;
        private string m_strCustomTooltip = "";

        //--------------------------------------------
        public CFuturocomDataPoint()
            :base()
        {
        }

        //--------------------------------------------
        public object ValeurPourAction
        {
            get
            {
                return m_valeurPourAction;
            }
            set
            {
                m_valeurPourAction = value;
            }
        }

        //--------------------------------------------
        public object ValeurSort
        {
            get
            {
                return m_valeurSort;
            }
            set
            {
                m_valeurSort = value;
            }
        }


        //--------------------------------------------
        public string CustomToolTip
        {
            get
            {
                return m_strCustomTooltip;
            }
            set
            {
                m_strCustomTooltip = value;
            }
        }
    }
}
