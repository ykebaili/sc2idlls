using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.Drawing;

namespace futurocom.chart.ChartArea
{
    [Serializable]
    public class CScrollBarStyle : I2iSerializable
    {
        private Color m_backColor = Color.FromArgb(0);
        private Color m_buttonColor = Color.FromArgb(0);
        private EScrollBarButtonStyles m_buttonStyle = EScrollBarButtonStyles.All;
        private bool m_bEnabled = true;
        private bool m_bIsPositionedInside = true;
        private Color m_lineColor = Color.FromArgb(0);
        private int m_nSize = 14;

        public CScrollBarStyle()
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
        public Color ButtonColor
        {
            get
            {
                return m_buttonColor;
            }
            set
            {
                m_buttonColor = value;
            }
        }
        //-----------------------------------------------------------------
        public EScrollBarButtonStyles ButtonStyle
        {
            get
            {
                return m_buttonStyle;
            }
            set
            {
                m_buttonStyle = value;
            }
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
        public bool IsPositionedInside
        {
            get
            {
                return m_bIsPositionedInside;
            }
            set
            {
                m_bIsPositionedInside = value;
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
        public int Size
        {
            get
            {
                return m_nSize;
            }
            set
            {
                m_nSize = value;
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
            serializer.TraiteColor(ref m_buttonColor);
            serializer.TraiteEnum<EScrollBarButtonStyles>(ref m_buttonStyle);
            serializer.TraiteBool(ref m_bEnabled);
            serializer.TraiteBool(ref m_bIsPositionedInside);
            serializer.TraiteColor(ref m_lineColor);
            serializer.TraiteInt(ref m_nSize);
            return result;
        }



    }

    public class CScrollBarStyleConvertor : CGenericObjectConverter<CScrollBarStyle>
    {
        public override string GetString(CScrollBarStyle value)
        {
            return "ScrollBar";
        }
    }

}
