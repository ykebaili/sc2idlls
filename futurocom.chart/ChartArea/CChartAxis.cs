using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.Drawing;
using System.ComponentModel;
using sc2i.common.drawing;

namespace futurocom.chart.ChartArea
{
    [Serializable]
    public class CChartAxis : I2iSerializable
    {
        private CChartAxisStyle m_style = new CChartAxisStyle();

        private EAxisEnabled m_enabled = EAxisEnabled.Auto;

        //Scale
        private bool m_bIsLogarithmic = false;
        private double m_fLogarithmBase = 10;
        private bool m_bIsMarginVisible = true;
        private bool m_bIsReversed = false;
        private bool m_bIsStartedFromZero = true;
        
        //Label
        private bool m_bIsLabelAutoFit = true;
        private int m_nLabelAutoFitMaxFontSize = 10;
        private int m_nLabelAutoFitMinFontSize = 6;
        private ELabelAutoFitStyles m_labelAutoFitStyle = ELabelAutoFitStyles.IncreaseFont |
            ELabelAutoFitStyles.DecreaseFont | 
            ELabelAutoFitStyles.StaggeredLabels | 
            ELabelAutoFitStyles.LabelsAngleStep30 | 
            ELabelAutoFitStyles.WordWrap;
        private CAxisLabelStyle m_labelStyle = new CAxisLabelStyle();

        private CChartGridStyle m_majorGridStyle = new CChartGridStyle();
        private CChartTickMark m_majorTickMark = new CChartTickMark();
        private CChartGridStyle m_minorGridStyle = new CChartGridStyle();
        private CChartTickMark m_minorTickMark = new CChartTickMark();

        private double m_fInterval = 0;//0 = auto;
        private EIntervalAutoMode m_intervalAutoMode = EIntervalAutoMode.FixedCount;
        private double m_fIntervalOffset = 0;//0 = auto;
        private EDateTimeIntervalType m_intervalOffsetType = EDateTimeIntervalType.Auto;
        private EDateTimeIntervalType m_intervalType = EDateTimeIntervalType.Auto;

        private ETextOrientation m_titleOrientation = ETextOrientation.Auto;
        private string m_strTitle = "";
        private EStringAlignment m_titleAlignment = EStringAlignment.Center;
        private Font m_titleFont = null;
        private Color m_titleForeColor = Color.Black;

        private CScrollBarStyle m_scrollBarStyle = new CScrollBarStyle();


        //-----------------------------------------------------------------
        public CChartAxis()
        {
        }


        //-----------------------------------------------------------------
        [TypeConverter(typeof(CChartAxisStyleConvertor))]
        [Category("Appearance")]
        public CChartAxisStyle Style
        {
            get
            {
                return m_style;
            }
            set
            {
                m_style = value;
            }
        }
        //-----------------------------------------------------------------
        [Category("Appearance")]
        public EAxisEnabled Enabled
        {
            get
            {
                return m_enabled;
            }
            set
            {
                m_enabled = value;
            }
        }
        //-----------------------------------------------------------------
        [Category("Scale")]
        public bool IsLogarithmic
        {
            get
            {
                return m_bIsLogarithmic;
            }
            set
            {
                m_bIsLogarithmic = value;
            }
        }
        //-----------------------------------------------------------------
        [Category("Scale")]
        public double LogarithmBase
        {
            get
            {
                return m_fLogarithmBase;
            }
            set
            {
                m_fLogarithmBase = value;
            }
        }
        //-----------------------------------------------------------------
        [Category("Scale")]
        public bool IsMarginVisible
        {
            get
            {
                return m_bIsMarginVisible;
            }
            set
            {
                m_bIsMarginVisible = value;
            }
        }
        //-----------------------------------------------------------------
        [Category("Scale")]
        public bool IsReversed
        {
            get
            {
                return m_bIsReversed;
            }
            set
            {
                m_bIsReversed = value;
            }
        }
        //-----------------------------------------------------------------
        [Category("Scale")]
        public bool IsStartedFromZero
        {
            get
            {
                return m_bIsStartedFromZero;
            }
            set
            {
                m_bIsStartedFromZero = value;
            }
        }

        //-----------------------------------------------------------------
        [Category("Labels")]
        public bool IsLabelAutoFit
        {
            get
            {
                return m_bIsLabelAutoFit;
            }
            set
            {
                m_bIsLabelAutoFit = value;
            }
        }

        //-----------------------------------------------------------------
        [Category("Labels")]
        public int LabelAutoFitMaxFontSize
        {
            get
            {
                return m_nLabelAutoFitMaxFontSize;
            }
            set
            {
                m_nLabelAutoFitMaxFontSize = value;
            }
        }
        //-----------------------------------------------------------------
        [Category("Labels")]
        public int LabelAutoFitMinFontSize
        {
            get
            {
                return m_nLabelAutoFitMinFontSize;
            }
            set
            {
                m_nLabelAutoFitMinFontSize = value;
            }
        }
        //-----------------------------------------------------------------
        [Category("Labels")]
        public ELabelAutoFitStyles LabelAutoFitStyle
        {
            get
            {
                return m_labelAutoFitStyle;
            }
            set
            {
                m_labelAutoFitStyle = value;
            }
        }
        //-----------------------------------------------------------------
        [TypeConverter(typeof(CAxisLabelStyleConvertor))]
        [Category("Labels")]
        public CAxisLabelStyle LabelStyle
        {
            get
            {
                return m_labelStyle;
            }
            set
            {
                m_labelStyle = value;
            }
        }
        //-----------------------------------------------------------------
        [TypeConverter(typeof(CChartGridStyleConvertor))]
        [Category("Graduation")]
        public CChartGridStyle MajorGridStyle
        {
            get
            {
                return m_majorGridStyle;
            }
            set
            {
                m_majorGridStyle = value;
            }
        }
        //-----------------------------------------------------------------
        [TypeConverter(typeof(CChartTickMarkConvertor))]
        [Category("Graduation")]
        public CChartTickMark MajorTickMark
        {
            get
            {
                return m_majorTickMark;
            }
            set
            {
                m_majorTickMark = value;
            }
        }
        
        //-----------------------------------------------------------------
        [TypeConverter(typeof(CChartGridStyleConvertor))]
        [Category("Graduation")]
        public CChartGridStyle MinorGridStyle
        {
            get
            {
                return m_minorGridStyle;
            }
            set
            {
                m_minorGridStyle = value;
            }
        }
        //-----------------------------------------------------------------
        [TypeConverter(typeof(CChartTickMarkConvertor))]
        [Category("Graduation")]
        public CChartTickMark MinorTickMark
        {
            get
            {
                return m_minorTickMark;
            }
            set
            {
                m_minorTickMark = value;
            }
        }
        //-----------------------------------------------------------------
        [Category("Interval")]
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
        [Category("Interval")]
        public EIntervalAutoMode IntervalAutoMode
        {
            get
            {
                return m_intervalAutoMode;
            }
            set
            {
                m_intervalAutoMode = value;
            }
        }
        //-----------------------------------------------------------------
        [Category("Interval")]
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
        [Category("Interval")]
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
        [Category("Interval")]
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
        [Category("Title")]
        public ETextOrientation TitleOrientation
        {
            get
            {
                return m_titleOrientation;
            }
            set
            {
                m_titleOrientation = value;
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
        [TypeConverter(typeof(CScrollBarStyleConvertor))]
        public CScrollBarStyle ScrollBar
        {
            get
            {
                return m_scrollBarStyle;
            }
            set
            {
                m_scrollBarStyle = value;
            }
        }

        //-----------------------------------------------------------------
        private int GetNumVersion()
        {
            return 1;
            //1 : ajout des scrollbar
        }

        //-----------------------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = serializer.TraiteObject<CChartAxisStyle>(ref m_style);
            if (!result)
                return result;
            serializer.TraiteBool(ref m_bIsLogarithmic);
            serializer.TraiteDouble(ref m_fLogarithmBase);
            serializer.TraiteBool(ref m_bIsMarginVisible);
            serializer.TraiteBool(ref m_bIsReversed);
            serializer.TraiteBool(ref m_bIsStartedFromZero);

            serializer.TraiteBool(ref m_bIsLabelAutoFit);
            serializer.TraiteInt(ref m_nLabelAutoFitMaxFontSize);
            serializer.TraiteInt(ref m_nLabelAutoFitMinFontSize);
            serializer.TraiteEnum<ELabelAutoFitStyles>(ref m_labelAutoFitStyle);
            result = serializer.TraiteObject<CAxisLabelStyle>(ref m_labelStyle);
            if (result)
                result = serializer.TraiteObject<CChartGridStyle>(ref m_majorGridStyle);
            if (result)
                result = serializer.TraiteObject<CChartTickMark>(ref m_majorTickMark);
            if (result)
                result = serializer.TraiteObject<CChartGridStyle>(ref m_minorGridStyle);
            if (result)
                result = serializer.TraiteObject<CChartTickMark>(ref m_minorTickMark);
            if (!result)
                return result;

            serializer.TraiteDouble(ref m_fInterval);
            serializer.TraiteEnum<EIntervalAutoMode>(ref m_intervalAutoMode);
            serializer.TraiteDouble(ref m_fIntervalOffset);
            serializer.TraiteEnum<EDateTimeIntervalType>(ref m_intervalOffsetType);
            serializer.TraiteEnum<EDateTimeIntervalType>(ref m_intervalType);

            serializer.TraiteEnum<ETextOrientation>(ref m_titleOrientation);
            serializer.TraiteString(ref m_strTitle);
            serializer.TraiteEnum<EStringAlignment>(ref m_titleAlignment);
            result = CUtilFont.SerializeFont(serializer, ref m_titleFont);
            if (!result)
                return result;
            serializer.TraiteColor(ref m_titleForeColor);

            if (nVersion >= 1)
                serializer.TraiteObject<CScrollBarStyle>(ref m_scrollBarStyle);

            return result;
        }
    }

    public class CChartAxisConvertor : CGenericObjectConverter<CChartAxis>
    {
        public override string GetString(CChartAxis value)
        {
            return "Axis";
        }
    }

}
