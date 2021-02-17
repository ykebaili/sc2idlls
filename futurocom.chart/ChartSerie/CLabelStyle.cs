using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.Drawing;
using sc2i.common.drawing;
using System.ComponentModel;

namespace futurocom.chart
{
    [Browsable(true)]
    public class CLabelStyle : I2iSerializable
    {
        public Font m_font;
        private int m_nAngle = 0;
        private Color m_backColor = Color.White;
        private Color m_borderColor = Color.Black;
        private EChartDashStyle m_borderDash = EChartDashStyle.Solid;
        private int m_nBorderWidth = 1;
        private Color m_foreColor = Color.Black;
        private string m_strFormat = "";

        //----------------------------------------------
        public CLabelStyle()
        {
        }

        //----------------------------------------------
        public Font Font
        {
            get
            {
                return m_font;
            }
            set
            {
                m_font = value;
            }
        }

        //----------------------------------------------
        public int Angle
        {
            get
            {
                return m_nAngle;
            }
            set
            {
                m_nAngle = value;
            }
        }

        //----------------------------------------------
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

        //----------------------------------------------
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

        //----------------------------------------------
        public EChartDashStyle BorderDash
        {
            get
            {
                return m_borderDash;
            }
            set
            {
                m_borderDash = value;
            }
        }

        //----------------------------------------------
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

        //----------------------------------------------
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

        //----------------------------------------------
        public string Format
        {
            get
            {
                return m_strFormat;
            }
            set
            {
                m_strFormat = value;
            }
        }

        //----------------------------------------------
        private int GetNumVersion()
        {
            return 1;
        }

        //----------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = CUtilFont.SerializeFont(serializer, ref m_font);
            if (!result)
                return result;
            serializer.TraiteInt(ref m_nAngle);
            serializer.TraiteColor(ref m_backColor);
            serializer.TraiteColor(ref m_borderColor);
            serializer.TraiteEnum<EChartDashStyle>(ref m_borderDash);
            serializer.TraiteInt(ref m_nBorderWidth);
            serializer.TraiteColor(ref m_foreColor);
            if (nVersion >= 1)
                serializer.TraiteString(ref m_strFormat);
            return result;
        }
    }


    //----------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------
    public class CLabelStyleOptionsConverter : ExpandableObjectConverter
    {
        //----------------------------------------------------------------------------------------
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(CLabelStyle))
                return true;
            return base.CanConvertFrom(context, destinationType);
        }

        //-----------------------------------------------------------------
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is CLabelStyle)
                return "style";
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}