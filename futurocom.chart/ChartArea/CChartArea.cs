using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.ComponentModel;
using System.Drawing.Design;

namespace futurocom.chart.ChartArea
{
    [Serializable]
    public class CChartArea : I2iSerializable
    {
        /// 3D
        private CChart3DStyle m_3DStyle = new CChart3DStyle();

        //Alignment
        private EAreaAlignmentOrientations m_AlignmentOrientation = EAreaAlignmentOrientations.Vertical;
        private EAreaAlignmentStyles m_AlignmentStyle = EAreaAlignmentStyles.All;
        private string m_strIdAlignWithArea = "";

        //Apparence
        private CChartAreaStyle m_areaStyle = new CChartAreaStyle();
        private CChartElementPosition m_position = new CChartElementPosition();

        //Axes
        private CChartAxis m_primaryXAxis = new CChartAxis();
        private CChartAxis m_primaryYAxis = new CChartAxis();
        private CChartAxis m_secondaryXAxis = new CChartAxis();
        private CChartAxis m_secondaryYAxis = new CChartAxis();

        //Cursor
        private CAxisCursor m_cursorX = new CAxisCursor();
        private CAxisCursor m_cursorY = new CAxisCursor();

        private string m_strAreaName = "";

        private string m_strAreaId = "";

        //-----------------------------------------------------------
        public CChartArea()
        {
            m_strAreaId = Guid.NewGuid().ToString();
        }

        //-----------------------------------------------------------------
        [TypeConverter(typeof(CChart3DStyleConvertor))]
        [Category("3D")]
        public CChart3DStyle Area3DStyle
        {
            get
            {
                return m_3DStyle;
            }
            set
            {
                m_3DStyle = value;
            }
        }
        //-----------------------------------------------------------------
        [Category("Alignment")]
        public EAreaAlignmentOrientations AlignmentOrientation
        {
            get
            {
                return m_AlignmentOrientation;
            }
            set
            {
                m_AlignmentOrientation = value;
            }
        }
        //-----------------------------------------------------------------
        [Category("Alignment")]
        public EAreaAlignmentStyles AlignmentStyle
        {
            get
            {
                return m_AlignmentStyle;
            }
            set
            {
                m_AlignmentStyle = value;
            }
        }
        //-----------------------------------------------------------------
        [TypeConverter(typeof(CChartAreaFromStringConvertor))]
        [Editor(typeof(CProprieteSelectChartAreaEditor), typeof(UITypeEditor))]
        [Category("Alignment")]
        public string AlignmentArea
        {
            get
            {
                return m_strIdAlignWithArea;
            }
            set
            {
                m_strIdAlignWithArea = value;
            }
        }
        
        //-----------------------------------------------------------------
        [TypeConverter(typeof(CChartAreaStyleConvertor))]
        [Category("Appearance")]
        public CChartAreaStyle AreaStyle
        {
            get
            {
                return m_areaStyle;
            }
            set
            {
                m_areaStyle = value;
            }
        }
        //-----------------------------------------------------------------
        [TypeConverter(typeof(CChartElementPositionConvertor))]
        [Category("Appearance")]
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
        [TypeConverter(typeof(CChartAxisConvertor))]
        [Category("Axis")]
        public CChartAxis PrimaryXAxis
        {
            get
            {
                return m_primaryXAxis;
            }
            set
            {
                m_primaryXAxis = value;
            }
        }
        //-----------------------------------------------------------------
        [TypeConverter(typeof(CChartAxisConvertor))]
        [Category("Axis")]
        public CChartAxis PrimaryYAxis
        {
            get
            {
                return m_primaryYAxis;
            }
            set
            {
                m_primaryYAxis = value;
            }
        }
        //-----------------------------------------------------------------
        [TypeConverter(typeof(CChartAxisConvertor))]
        [Category("Axis")]
        public CChartAxis SecondaryXAxis
        {
            get
            {
                return m_secondaryXAxis;
            }
            set
            {
                m_secondaryXAxis = value;
            }
        }
        //-----------------------------------------------------------------
        [TypeConverter(typeof(CChartAxisConvertor))]
        [Category("Axis")]
        public CChartAxis SecondaryYAxis
        {
            get
            {
                return m_secondaryYAxis;
            }
            set
            {
                m_secondaryYAxis = value;
            }
        }
        //-----------------------------------------------------------------
        [TypeConverter(typeof(CAxisCursorConvertor))]
        [Category("Axis")]
        public CAxisCursor CursorX
        {
            get
            {
                return m_cursorX;
            }
            set
            {
                m_cursorX = value;
            }
        }
        //-----------------------------------------------------------------
        [TypeConverter(typeof(CAxisCursorConvertor))]
        [Category("Axis")]
        public CAxisCursor CursorY
        {
            get
            {
                return m_cursorY;
            }
            set
            {
                m_cursorY = value;
            }
        }
        //-----------------------------------------------------------------
        [Category("Misc")]
        public string AreaName
        {
            get
            {
                return m_strAreaName;
            }
            set
            {
                m_strAreaName = value;
            }
        }
        //-----------------------------------------------------------------
        [Browsable(false)]
        public string AreaId
        {
            get
            {
                return m_strAreaId;
            }
            set
            {
                m_strAreaId = value;
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
            result = serializer.TraiteObject<CChart3DStyle>(ref m_3DStyle);
            if (!result)
                return result;

            serializer.TraiteEnum<EAreaAlignmentOrientations>(ref m_AlignmentOrientation);
            serializer.TraiteEnum<EAreaAlignmentStyles>(ref m_AlignmentStyle);
            serializer.TraiteString(ref m_strIdAlignWithArea);

            result = serializer.TraiteObject<CChartAreaStyle>(ref m_areaStyle);
            if (!result)
                return result;

            result = serializer.TraiteObject<CChartElementPosition>(ref m_position);
            if (!result)
                return result;

            result = serializer.TraiteObject<CChartAxis>(ref m_primaryXAxis);
            if (result)
                result = serializer.TraiteObject<CChartAxis>(ref m_primaryYAxis);
            if (result)
                result = serializer.TraiteObject<CChartAxis>(ref m_secondaryXAxis);
            if (result)
                result = serializer.TraiteObject<CChartAxis>(ref m_secondaryYAxis);

            if (result)
                result = serializer.TraiteObject<CAxisCursor>(ref m_cursorX);
            if (result)
                result = serializer.TraiteObject<CAxisCursor>(ref m_cursorY);

            if (!result)
                return result;

            serializer.TraiteString(ref m_strAreaName);
            serializer.TraiteString(ref m_strAreaId);

            if ( serializer.IsForClone)
                m_strAreaId = Guid.NewGuid().ToString();

            return result;
        }

        public override string ToString()
        {
            return m_strAreaName;
        }



    }
}
