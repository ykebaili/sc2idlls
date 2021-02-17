using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.Drawing;

namespace futurocom.chart.ChartArea
{
    [Serializable]
    public class CChartGridStyle : I2iSerializable
    {

        private bool m_bEnabled = true;
        private double m_fInterval = 0;//0 = auto
        private double m_fIntervalOffset = 0;//0= auto
        private EDateTimeIntervalType m_IntervalOffsetType = EDateTimeIntervalType.NotSet;
        private EDateTimeIntervalType m_intervalType = EDateTimeIntervalType.NotSet;
        private Color m_lineColor = Color.Black;
        private EChartDashStyle m_lineDashStyle = EChartDashStyle.Solid;
        private int m_nLineWidth = 1;

        //---------------------------------------------------
        public CChartGridStyle()
        {
        }


        //-----------------------------------------------------------------
        public bool Enabled
        {
            get
            {
                return m_bEnabled;
            }
            set
            {
                m_bEnabled = value;
            }
        }
        //-----------------------------------------------------------------
        public double Interval
        {
            get
            {
                return m_fInterval;
            }
            set
            {
                m_fInterval = value;
            }
        }
        //-----------------------------------------------------------------
        public double IntervalOffset
        {
            get
            {
                return m_fIntervalOffset;
            }
            set
            {
                m_fIntervalOffset = value;
            }
        }
        //-----------------------------------------------------------------
        public EDateTimeIntervalType IntervalOffsetType
        {
            get
            {
                return m_IntervalOffsetType;
            }
            set
            {
                m_IntervalOffsetType = value;
            }
        }
        //-----------------------------------------------------------------
        public EDateTimeIntervalType IntervalType
        {
            get
            {
                return m_intervalType;
            }
            set
            {
                m_intervalType = value;
            }
        }
        //-----------------------------------------------------------------
        public Color LineColor
        {
            get
            {
                return m_lineColor;
            }
            set
            {
                m_lineColor = value;
            }
        }
        //-----------------------------------------------------------------
        public EChartDashStyle LineDashStyle
        {
            get
            {
                return m_lineDashStyle;
            }
            set
            {
                m_lineDashStyle = value;
            }
        }
        //-----------------------------------------------------------------
        public int LineWidth
        {
            get
            {
                return m_nLineWidth;
            }
            set
            {
                m_nLineWidth = value;
            }
        }

        //-----------------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //-----------------------------------------------------------------
        public virtual CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);

            serializer.TraiteBool(ref m_bEnabled);
            serializer.TraiteDouble(ref m_fInterval);
            serializer.TraiteDouble(ref m_fIntervalOffset);
            serializer.TraiteEnum<EDateTimeIntervalType>(ref m_IntervalOffsetType);
            serializer.TraiteEnum<EDateTimeIntervalType>(ref m_intervalType);
            serializer.TraiteColor(ref m_lineColor);
            serializer.TraiteEnum<EChartDashStyle>(ref m_lineDashStyle);
            serializer.TraiteInt(ref m_nLineWidth);
            return result;
        }

    }

    public class CChartGridStyleConvertor : CGenericObjectConverter<CChartGridStyle>
    {
        public override string GetString(CChartGridStyle value)
        {
            return "Grid style";
        }
    }

}
