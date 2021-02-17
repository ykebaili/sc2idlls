using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.Drawing;
using System.ComponentModel;

namespace futurocom.chart
{

    /// <summary>
    /// Apparence d'un point de série (marker)
    /// </summary>
    public class CMarkerStyle :  I2iSerializable
    {
        private Color m_borderColor = Color.Black;
        private int m_nBorderWidth = 1;

        private Color m_backColor = Color.White;
        private Bitmap m_markerImage = null;
        private Color m_markerImageTransparentColor = Color.Transparent;

        private int m_nMarkerSize = 14;
        private int m_nMarkerStep = 1;

        private EMarkerStyle m_markerStyle = EMarkerStyle.None;

        //-----------------------------------------------------
        public CMarkerStyle()
        {
        }

        //-----------------------------------------------------
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

        //-----------------------------------------------------
        public int BorderWidth
        {
            get
            {
                return m_nBorderWidth; ;
            }
            set
            {
                m_nBorderWidth = value;
            }
        }

        //-----------------------------------------------------
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

        //-----------------------------------------------------
        public Bitmap MarkerImage
        {
            get
            {
                return m_markerImage;
            }
            set
            {
                if (m_markerImage != null)
                    m_markerImage.Dispose();
                m_markerImage = value;
            }
        }

        //-----------------------------------------------------
        public Color MarkerImageTransparentColor
        {
            get
            {
                return m_markerImageTransparentColor;
            }
            set
            {
                m_markerImageTransparentColor = value;
            }
        }

        //-----------------------------------------------------
        public int MarkerSize
        {
            get
            {
                return m_nMarkerSize;
            }
            set
            {
                m_nMarkerSize = value;
            }
        }

        //-----------------------------------------------------
        public int MarkerStep
        {
            get
            {
                return m_nMarkerStep;
            }
            set
            {
                m_nMarkerStep = value;
            }
        }

        //-----------------------------------------------------
        public EMarkerStyle MarkerStyle
        {
            get
            {
                return m_markerStyle;
            }
            set
            {
                m_markerStyle = value;
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
            serializer.TraiteColor(ref m_borderColor);
            serializer.TraiteInt(ref m_nBorderWidth);
            serializer.TraiteColor(ref m_backColor);
            serializer.TraiteBitmap(ref m_markerImage, true);
            serializer.TraiteColor(ref m_markerImageTransparentColor);
            serializer.TraiteInt(ref m_nMarkerSize);
            serializer.TraiteInt(ref m_nMarkerStep);
            serializer.TraiteEnum<EMarkerStyle>(ref m_markerStyle);
            return result;
        }
    }

    //----------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------
    public class CMarkerStyleOptionsConverter : ExpandableObjectConverter
    {
        //----------------------------------------------------------------------------------------
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(CMarkerStyle))
                return true;
            return base.CanConvertFrom(context, destinationType);
        }

        //-----------------------------------------------------------------
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is CMarkerStyle)
                return "style";
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
