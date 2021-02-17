using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.Drawing;
using System.ComponentModel;

namespace futurocom.chart
{
    public class CSerieStyle : I2iSerializable
    {
        //Back
        private EGradientStyle m_backGradientStyle = EGradientStyle.None;
        private EChartHatchStyle m_backHatchStyle = EChartHatchStyle.None;
        private Bitmap m_backImage = null;
        private EChartImageAlignmentStyle m_backImageAlignement = EChartImageAlignmentStyle.TopLeft;
        private Color m_backImageTransparentColor = Color.Transparent;
        private EChartImageWrapMode m_backImageWrapMode = EChartImageWrapMode.Tile;
        private Color m_backSecondaryColor = Color.White;

        //Border
        private Color m_borderColor = Color.White;
        private EChartDashStyle m_borderDashStyle = EChartDashStyle.Solid;
        private int m_nBorderWidth = 1;

        //Couleur nulle->c'est le chart qui décide à partir de sa palette
        private Color m_serieColor = Color.FromArgb(0);
        private int m_serieOpacity = 100;
        private Color m_shadowColor = Color.FromArgb(0);
        private int m_nShadowOffset = 4;

        //------------------------------------------------------
        public CSerieStyle()
        {
        }

        //-------------------------------------------------------
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

        //-------------------------------------------------------
        public EChartHatchStyle BackHatchStyle
        {
            get
            {
                return m_backHatchStyle;
            }
            set
            {
                m_backHatchStyle = value;
            }
        }

        //-------------------------------------------------------
        public Bitmap BackImage
        {
            get
            {
                return m_backImage;
            }
            set
            {
                if (m_backImage != null)
                    m_backImage.Dispose();
                m_backImage = value;
            }
        }

        //-------------------------------------------------------
        public EChartImageAlignmentStyle BackImageAlignment
        {
            get
            {
                return m_backImageAlignement;
            }
            set
            {
                m_backImageAlignement = value;
            }
        }

        //-------------------------------------------------------
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

        //-------------------------------------------------------
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

        //-------------------------------------------------------
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

        //-------------------------------------------------------
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

        //-------------------------------------------------------
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

        //-------------------------------------------------------
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

        //-------------------------------------------------------
        public Color SerieColor
        {
            get
            {
                return m_serieColor;
            }
            set
            {

                m_serieColor = value;
            }
        }

        //-------------------------------------------------------
        public int SerieOpacity
        {
            get
            {
                return m_serieOpacity;
            }
            set
            {
                m_serieOpacity = Math.Min(Math.Max(value, 0), 100);
            }
        }


        //----------------------------------------------------------
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

        //----------------------------------------------------------
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

        //----------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //----------------------------------------------------------
        public CResultAErreur Serialize ( C2iSerializer serializer )
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
            if ( !result )
                return result;
            serializer.TraiteEnum<EGradientStyle>(ref m_backGradientStyle);
            serializer.TraiteEnum<EChartHatchStyle>(ref m_backHatchStyle);
            serializer.TraiteBitmap( ref m_backImage, true );
            serializer.TraiteEnum<EChartImageAlignmentStyle>(ref m_backImageAlignement);
            serializer.TraiteColor ( ref m_backImageTransparentColor);
            serializer.TraiteEnum<EChartImageWrapMode>(ref m_backImageWrapMode);
            serializer.TraiteColor ( ref m_backSecondaryColor);

            serializer.TraiteColor ( ref m_borderColor );
            serializer.TraiteEnum<EChartDashStyle>(ref m_borderDashStyle);
            serializer.TraiteInt ( ref m_nBorderWidth );

            serializer.TraiteColor ( ref m_serieColor );
            serializer.TraiteInt(ref m_serieOpacity);
            serializer.TraiteColor ( ref m_shadowColor );
            serializer.TraiteInt ( ref m_nShadowOffset );

            return result;
        }
    }

    public class CSerieStyleOptionsConverter : ExpandableObjectConverter
    {
        //----------------------------------------------------------------------------------------
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(CSerieStyle))
                return true;
            return base.CanConvertFrom(context, destinationType);
        }

        //-----------------------------------------------------------------
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is CSerieStyle)
                return "style";
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}