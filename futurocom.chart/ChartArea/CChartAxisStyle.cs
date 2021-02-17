using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.Drawing;

namespace futurocom.chart.ChartArea
{
    [Serializable]
    public class CChartAxisStyle : I2iSerializable
    {
        //Apparence
        private EAxisArrowStyle m_arrowStyle = EAxisArrowStyle.None;
        private Color m_interlacedColor = Color.White;
        private bool m_bIsInterlaced = false;
        private bool m_bIsMarksNextToAxis = true;
        private Color m_lineColor = Color.Black;
        private EChartDashStyle m_lineDashStyle = EChartDashStyle.Solid;
        private int m_nLineWidth = 1;

        //-------------------------------------------------------------
        public CChartAxisStyle()
        { 
        }


        //-----------------------------------------------------------------
        public EAxisArrowStyle ArrowStyle
        {
            get
            {
                return m_arrowStyle;
            }
            set
            {
                m_arrowStyle = value;
            }
        }
        //-----------------------------------------------------------------
        public Color InterlacedColor
        {
            get
            {
                return m_interlacedColor;
            }
            set
            {
                m_interlacedColor = value;
            }
        }
        //-----------------------------------------------------------------
        public bool IsInterlaced
        {
            get
            {
                return m_bIsInterlaced;
            }
            set
            {
                m_bIsInterlaced = value;
            }
        }
        //-----------------------------------------------------------------
        public bool IsMarksNextToAxis
        {
            get
            {
                return m_bIsMarksNextToAxis;
            }
            set
            {
                m_bIsMarksNextToAxis = value;
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
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            serializer.TraiteEnum<EAxisArrowStyle>(ref m_arrowStyle);
            serializer.TraiteColor(ref m_interlacedColor);
            serializer.TraiteBool(ref m_bIsInterlaced);
            serializer.TraiteBool(ref m_bIsMarksNextToAxis);
            serializer.TraiteColor(ref m_lineColor);
            serializer.TraiteEnum<EChartDashStyle>(ref m_lineDashStyle);
            serializer.TraiteInt(ref m_nLineWidth);
            return result;
        }


    }

    //-----------------------------------------------------------------
    public class CChartAxisStyleConvertor : CGenericObjectConverter<CChartAxisStyle>
    {
        public override string GetString(CChartAxisStyle value)
        {
            return "Style";
        }
    }
}
