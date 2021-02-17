using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace futurocom.chart.ChartArea
{
    [Serializable]
    public class CChartTickMark : CChartGridStyle
    {
        private float m_fSize = 1;
        private ETickMarkStyle m_tickMarkStyle = ETickMarkStyle.OutsideArea;

        //-------------------------------------------------------
        public CChartTickMark()
            : base()
        {
            
        }
        //-----------------------------------------------------------------
        public float Size
        {
            get
            {
                return m_fSize;
            }
            set
            {
                m_fSize = value;
            }
        }
        //-----------------------------------------------------------------
        public ETickMarkStyle TickMarkStyle
        {
            get
            {
                return m_tickMarkStyle;
            }
            set
            {
                m_tickMarkStyle = value;
            }
        }

        //-----------------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //-----------------------------------------------------------------
        public override CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = base.Serialize(serializer);
            if (!result)
                return result;
            serializer.TraiteFloat(ref m_fSize);
            serializer.TraiteEnum<ETickMarkStyle>(ref m_tickMarkStyle);
            return result;
        }

    }

    public class CChartTickMarkConvertor : CGenericObjectConverter<CChartTickMark>
    {
        public override string GetString(CChartTickMark value)
        {
            return "Tick mark style";
        }
    }

    
}
