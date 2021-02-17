using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Design;
using sc2i.common.drawing;

namespace futurocom.chart.LegendArea
{
    [Serializable]
    public class CLegendArea : I2iSerializable
    {
        private EStringAlignment m_alignment = EStringAlignment.Near;
        private string m_strIdDockedArea = "";
        private EDocking m_docking = EDocking.Right;
        private bool m_bIsDockedInsideChartArea = true;
        private int m_nMaximumAutoSize = 50;//0->100
        private CChartLegendStyle m_legendStyle = new CChartLegendStyle();

        private string m_strTitle = "";
        private EStringAlignment m_titleAlignment = EStringAlignment.Center;
        private Font m_titleFont = null;
        private Color m_titleBackColor = Color.White;
        private Color m_titleForeColor = Color.Black;
        private ELegendSeparatorStyle m_titleSeparator = ELegendSeparatorStyle.None;
        private Color m_titleSeparatorColor = Color.Black;

        private string m_strId = "";
        private string m_strLegendName = "";

        //------------------------------------------------------
        public CLegendArea()
        {
            m_strId = Guid.NewGuid().ToString();
        }


        //-----------------------------------------------------------------
        [Category("Position")]
        public EStringAlignment Alignment
        {
            get
            {
                return m_alignment;
            }
            set
            {
                m_alignment = value;
            }
        }
        //-----------------------------------------------------------------
        [TypeConverter(typeof(CChartAreaFromStringConvertor))]
        [Editor(typeof(CProprieteSelectChartAreaEditor), typeof(UITypeEditor))]
        [Category("Position")]
        public string DockedArea
        {
            get
            {
                return m_strIdDockedArea;
            }
            set
            {
                m_strIdDockedArea = value;
            }
        }
        //-----------------------------------------------------------------
        [Category("Position")]
        public EDocking Docking
        {
            get
            {
                return m_docking;
            }
            set
            {
                m_docking = value;
            }
        }
        //-----------------------------------------------------------------
        [Category("Position")]
        public bool IsDockedInsideChartArea
        {
            get
            {
                return m_bIsDockedInsideChartArea;
            }
            set
            {
                m_bIsDockedInsideChartArea = value;
            }
        }
        //-----------------------------------------------------------------
        [Category("Position")]
        public int MaximumAutoSize
        {
            get
            {
                return m_nMaximumAutoSize;
            }
            set
            {
                m_nMaximumAutoSize = value;
            }
        }
        
        //-----------------------------------------------------------------
        [TypeConverter(typeof(CChartLegendStyleConvertor))]
        [Category("Appearance")]
        public CChartLegendStyle LegendStyle
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
        [Category("Title")]
        public string Title
        {
            get
            {
                return m_strTitle;
            }
            set
            {
                m_strTitle = value;
            }
        }
        //-----------------------------------------------------------------
        [Category("Title")]
        public EStringAlignment TitleAlignment
        {
            get
            {
                return m_titleAlignment;
            }
            set
            {
                m_titleAlignment = value;
            }
        }
        //-----------------------------------------------------------------
        [Category("Title")]
        public Font TitleFont
        {
            get
            {
                return m_titleFont;
            }
            set
            {
                m_titleFont = value;
            }
        }
        //-----------------------------------------------------------------
        [Category("Title")]
        public Color TitleBackColor
        {
            get
            {
                return m_titleBackColor;
            }
            set
            {
                m_titleBackColor = value;
            }
        }

        //-----------------------------------------------------------------
        [Category("Title")]
        public Color TitleForeColor
        {
            get
            {
                return m_titleForeColor;
            }
            set
            {
                m_titleForeColor = value;
            }
        }
        //-----------------------------------------------------------------
        [Category("Title")]
        public ELegendSeparatorStyle TitleSeparator
        {
            get
            {
                return m_titleSeparator;
            }
            set
            {
                m_titleSeparator = value;
            }
        }
        //-----------------------------------------------------------------
        [Category("Title")]
        public Color TitleSeparatorColor
        {
            get
            {
                return m_titleSeparatorColor;
            }
            set
            {
                m_titleSeparatorColor = value;
            }
        }

        //-----------------------------------------------------------------
        public string LegendName
        {
            get
            {
                return m_strLegendName;
            }
            set
            {
                m_strLegendName = value;
            }
        }

        //-----------------------------------------------------------------
        [Browsable(false)]
        public string LegendId
        {
            get
            {
                return m_strId;
            }
            set
            {
                m_strId = value;
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
            serializer.TraiteEnum<EStringAlignment>(ref m_alignment);
            serializer.TraiteString(ref m_strIdDockedArea);
            serializer.TraiteEnum<EDocking>(ref m_docking);
            serializer.TraiteBool(ref m_bIsDockedInsideChartArea);
            serializer.TraiteInt(ref m_nMaximumAutoSize);
            result = serializer.TraiteObject<CChartLegendStyle>(ref m_legendStyle);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strTitle);
            serializer.TraiteEnum<EStringAlignment>(ref m_titleAlignment);
            CUtilFont.SerializeFont(serializer, ref m_titleFont);
            serializer.TraiteColor(ref m_titleBackColor);
            serializer.TraiteColor(ref m_titleForeColor);
            serializer.TraiteEnum<ELegendSeparatorStyle>(ref m_titleSeparator);
            serializer.TraiteColor(ref m_titleSeparatorColor);
            serializer.TraiteString(ref m_strLegendName);
            serializer.TraiteString(ref m_strId);

            if ( serializer.IsForClone)
                m_strId = Guid.NewGuid().ToString();
            return result;
        }
    }

    


}
