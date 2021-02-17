using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.Drawing;

namespace futurocom.chart.ChartArea
{
    [Serializable]
    public class CChartAreaStyle : I2iSerializable
    {
        private Color m_backColor = Color.Transparent;
        private EGradientStyle m_backGradientStyle = EGradientStyle.None;
        private EChartHatchStyle m_hatchStyle = EChartHatchStyle.None;
        private Bitmap m_backImage = null;
        private Color m_backImageTransparentColor = Color.White;
        private EChartImageAlignmentStyle m_backImageAlignment = EChartImageAlignmentStyle.TopLeft;
        private EChartImageWrapMode m_backImageWrapMode = EChartImageWrapMode.Scaled;
        private Color m_backSecondaryColor = Color.White;
        private Color m_borderColor = Color.Black;
        private EChartDashStyle m_borderDashStyle = EChartDashStyle.NotSet;
        private int m_nBorderWidth = 1;
        private bool m_bIsSameFontSizeForAllAxes = false;
        private Color m_shadowColor = Color.Gray;
        private int m_nShadowOffset = 0;

        public CChartAreaStyle()
        {
        }
        //-----------------------------------------------------------------
        public Color BackColor
        {
            get
            {
                return m_backColor;
            }
            set
            {
                m_backColor = value;
            }
        }
        //-----------------------------------------------------------------
        public EGradientStyle BackGradientStyle
        {
            get
            {
                return m_backGradientStyle;
            }
            set
            {
                m_backGradientStyle = value;
            }
        }
        //-----------------------------------------------------------------
        public EChartHatchStyle HatchStyle
        {
            get
            {
                return m_hatchStyle;
            }
            set
            {
                m_hatchStyle = value;
            }
        }
        //-----------------------------------------------------------------
        public Bitmap BackImage
        {
            get
            {
                return m_backImage;
            }
            set
            {
                m_backImage = value;
            }
        }
        //-----------------------------------------------------------------
        public Color BackImageTransparentColor
        {
            get
            {
                return m_backImageTransparentColor;
            }
            set
            {
                m_backImageTransparentColor = value;
            }
        }
        //-----------------------------------------------------------------
        public EChartImageAlignmentStyle BackImageAlignment
        {
            get
            {
                return m_backImageAlignment;
            }
            set
            {
                m_backImageAlignment = value;
            }
        }
        //-----------------------------------------------------------------
        public EChartImageWrapMode BackImageWrapMode
        {
            get
            {
                return m_backImageWrapMode;
            }
            set
            {
                m_backImageWrapMode = value;
            }
        }
        //-----------------------------------------------------------------
        public Color BackSecondaryColor
        {
            get
            {
                return m_backSecondaryColor;
            }
            set
            {
                m_backSecondaryColor = value;
            }
        }
        //-----------------------------------------------------------------
        public Color BorderColor
        {
            get
            {
                return m_borderColor;
            }
            set
            {
                m_borderColor = value;
            }
        }
        //-----------------------------------------------------------------
        public EChartDashStyle BorderDashStyle
        {
            get
            {
                return m_borderDashStyle;
            }
            set
            {
                m_borderDashStyle = value;
            }
        }
        //-----------------------------------------------------------------
        public int BorderWidth
        {
            get
            {
                return m_nBorderWidth;
            }
            set
            {
                m_nBorderWidth = value;
            }
        }
        //-----------------------------------------------------------------
        public bool IsSameFontSizeForAllAxes
        {
            get
            {
                return m_bIsSameFontSizeForAllAxes;
            }
            set
            {
                m_bIsSameFontSizeForAllAxes = value;
            }
        }
        //-----------------------------------------------------------------
        public Color ShadowColor
        {
            get
            {
                return m_shadowColor;
            }
            set
            {
                m_shadowColor = value;
            }
        }
        //-----------------------------------------------------------------
        public int ShadowOffset
        {
            get
            {
                return m_nShadowOffset;
            }
            set
            {
                m_nShadowOffset = value;
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
            if (!result)
                return result;
            serializer.TraiteColor(ref m_backColor);
            serializer.TraiteEnum<EGradientStyle>(ref m_backGradientStyle);
            serializer.TraiteEnum<EChartHatchStyle>(ref m_hatchStyle);
            serializer.TraiteBitmap(ref m_backImage, true);
            serializer.TraiteColor(ref m_backImageTransparentColor);
            serializer.TraiteEnum<EChartImageAlignmentStyle>(ref m_backImageAlignment);
            serializer.TraiteEnum<EChartImageWrapMode>(ref m_backImageWrapMode);
            serializer.TraiteColor(ref m_backSecondaryColor);
            serializer.TraiteColor(ref m_borderColor);
            serializer.TraiteEnum<EChartDashStyle>(ref m_borderDashStyle);
            serializer.TraiteInt(ref m_nBorderWidth);
            serializer.TraiteBool(ref m_bIsSameFontSizeForAllAxes);
            serializer.TraiteColor(ref m_shadowColor);
            serializer.TraiteInt(ref m_nShadowOffset);
            return result;
        }
    }

    public class CChartAreaStyleConvertor : CGenericObjectConverter<CChartAreaStyle>
    {
        public override string GetString(CChartAreaStyle value)
        {
            return "Area style";
        }
    }
}
