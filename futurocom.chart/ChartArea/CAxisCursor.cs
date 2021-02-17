using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using sc2i.common;
using System.ComponentModel;

namespace futurocom.chart.ChartArea
{
    public class CAxisCursor : I2iSerializable
    {
        private bool m_bAutoScroll = true;
        private EAxisType m_axisType = EAxisType.Primary;
        private double m_fInterval = 1;
        private double m_fIntervalOffset = 0;
        private EDateTimeIntervalType m_intervalOffsetType = EDateTimeIntervalType.Auto;
        private EDateTimeIntervalType m_intervalType = EDateTimeIntervalType.Auto;
        private bool m_bIsUserEnabled = false;
        private bool m_bIsUserSelectionEnabled = false;
        private Color m_lineColor = Color.Red;
        private EChartDashStyle m_lineDashStyle = EChartDashStyle.Solid;
        private int m_nLineWith = 1;
        private Color m_selectionColor = Color.LightGray;

        //--------------------------------------------------
        public CAxisCursor()
        { }
        //-----------------------------------------------------------------
        public bool AutoScroll
        {
            get
            {
                return m_bAutoScroll;
            }
            set
            {
                m_bAutoScroll = value;
            }
        }
        //-----------------------------------------------------------------
        public EAxisType AxisType
        {
            get
            {
                return m_axisType;
            }
            set
            {
                m_axisType = value;
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
                return m_intervalOffsetType;
            }
            set
            {
                m_intervalOffsetType = value;
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
        public bool IsUserEnabled
        {
            get
            {
                return m_bIsUserEnabled;
            }
            set
            {
                m_bIsUserEnabled = value;
            }
        }

        //-----------------------------------------------------------------
        /// <summary>
        /// Masqué car geré par la fenêtre de visu
        /// </summary>
        [Browsable(false)]
        public bool IsUserSelectionEnabled
        {
            get
            {
                return m_bIsUserSelectionEnabled;
            }
            set
            {
                m_bIsUserSelectionEnabled = value;
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
        public int LineWith
        {
            get
            {
                return m_nLineWith;
            }
            set
            {
                m_nLineWith = value;
            }
        }
        //-----------------------------------------------------------------
        public Color SelectionColor
        {
            get
            {
                return m_selectionColor;
            }
            set
            {
                m_selectionColor = value;
            }
        }

        //-----------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }


        //-----------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteBool(ref m_bAutoScroll);
            serializer.TraiteEnum<EAxisType>(ref m_axisType);
            serializer.TraiteDouble(ref m_fInterval);
            serializer.TraiteDouble(ref m_fIntervalOffset);
            serializer.TraiteEnum<EDateTimeIntervalType>(ref m_intervalOffsetType);
            serializer.TraiteEnum<EDateTimeIntervalType>(ref m_intervalType);
            serializer.TraiteBool(ref m_bIsUserEnabled);
            serializer.TraiteBool(ref m_bIsUserSelectionEnabled);
            serializer.TraiteColor(ref m_lineColor);
            serializer.TraiteEnum<EChartDashStyle>(ref m_lineDashStyle);
            serializer.TraiteInt(ref m_nLineWith);
            serializer.TraiteColor(ref m_selectionColor);
            return result;
        }
    }

    //--------------------------------------------------------------------------
    public class CAxisCursorConvertor : CGenericObjectConverter<CAxisCursor>
    {
        public override string GetString(CAxisCursor value)
        {
            return "Axis Cursor";
        }
    }
}
