using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.Drawing;
using futurocom.chart.ChartArea;
using System.ComponentModel;
using sc2i.common.drawing;

namespace futurocom.chart.LegendArea
{
    [Serializable]
    public class CChartLegendStyle : I2iSerializable
    {
        private bool m_bEnabled = true;
        private ELegendStyle m_legendStyle = ELegendStyle.Table;
        private ELegendTableStyle m_tableStyle = ELegendTableStyle.Auto;
        private int m_nAutoFitMinFontSize = 7;
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
        private Font m_font = null;
        private Color m_foreColor = Color.Black;
        private bool m_bInterlacedRows = false;
        private Color m_interlacedRowsColor = Color.Transparent;
        private bool m_bIsEquallySpacedItems = false;
        private bool m_bIsTextAutoFit = true;
        private ELegendItemOrder m_legendItemOrder = ELegendItemOrder.Auto;
        private CChartElementPosition m_position = new CChartElementPosition();
        private Color m_shadowColor = Color.Gray;
        private int m_nShadowOffset = 0;
        private int m_nTextWrapThreshold = 25;

        public CChartLegendStyle()
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
        public ELegendStyle LegendStyle
        {
            get
            {
                return m_legendStyle;
            }
            set
            {
                m_legendStyle = value;
            }
        }

        //-----------------------------------------------------------------
        public ELegendTableStyle TableStyle
        {
            get
            {
                return m_tableStyle;
            }
            set
            {
                m_tableStyle = value;
            }
        }

        //-----------------------------------------------------------------
        public int AutoFitMinFontSize
        {
            get
            {
                return m_nAutoFitMinFontSize;
            }
            set
            {
                m_nAutoFitMinFontSize = value;
            }
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
        public Font Font
        {
            get{
                return m_font;
            }
            set{
                m_font = value;
            }
        }

        //-----------------------------------------------------------------
        public Color ForeColor
        {
            get
            {
                return m_foreColor;
            }
            set
            {
                m_foreColor = value;
            }
        }

        //-----------------------------------------------------------------
        public bool InterlacedRows
        {
            get
            {
                return m_bInterlacedRows;
            }
            set
            {
                m_bInterlacedRows = value;
            }
        }

        //-----------------------------------------------------------------
        public Color InterlacedRowsColor
        {
            get
            {
                return m_interlacedRowsColor;
            }
            set
            {
                m_interlacedRowsColor = value;
            }
        }

        //-----------------------------------------------------------------
        public bool IsEquallySpacedItems
        {
            get
            {
                return m_bIsEquallySpacedItems;
            }
            set
            {
                m_bIsEquallySpacedItems = value;
            }
        }
        //-----------------------------------------------------------------
        public bool IsTextAutoFit
        {
            get
            {
                return m_bIsTextAutoFit;
            }
            set
            {
                m_bIsTextAutoFit = value;
            }
        }
        //-----------------------------------------------------------------
        public ELegendItemOrder LegendItemOrder
        {
            get
            {
                return m_legendItemOrder;
            }
            set
            {
                m_legendItemOrder = value;
            }
        }

        //-----------------------------------------------------------------
        [TypeConverter(typeof(CChartElementPositionConvertor))]
        public CChartElementPosition Position
        {
            get
            {
                return m_position;
            }
            set
            {
                m_position = value;
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
        public int TextWrapThreshold
        {
            get
            {
                return m_nTextWrapThreshold;
            }
            set
            {
                m_nTextWrapThreshold = value;
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
            serializer.TraiteBool(ref m_bEnabled);
            serializer.TraiteEnum<ELegendStyle>(ref m_legendStyle);
            serializer.TraiteEnum<ELegendTableStyle>(ref m_tableStyle);
            serializer.TraiteInt(ref m_nAutoFitMinFontSize);
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
            CUtilFont.SerializeFont(serializer, ref m_font);
            serializer.TraiteColor(ref m_foreColor);
            serializer.TraiteBool(ref m_bInterlacedRows);
            serializer.TraiteColor(ref m_interlacedRowsColor);
            serializer.TraiteBool(ref m_bIsEquallySpacedItems);
            serializer.TraiteBool(ref m_bIsTextAutoFit);
            serializer.TraiteEnum<ELegendItemOrder>(ref m_legendItemOrder);
            serializer.TraiteObject<CChartElementPosition>(ref m_position);
            serializer.TraiteColor(ref m_shadowColor);
            serializer.TraiteInt(ref m_nShadowOffset);
            serializer.TraiteInt(ref m_nTextWrapThreshold);
            return result;
        }
    }

    public class CChartLegendStyleConvertor : CGenericObjectConverter<CChartAreaStyle>
    {
        public override string GetString(CChartAreaStyle value)
        {
            return "Legend style";
        }
    }
}
