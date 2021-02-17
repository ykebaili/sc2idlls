using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.Drawing;
using sc2i.common.drawing;

namespace futurocom.chart.ChartArea
{
    [Serializable]
    public class CAxisLabelStyle : I2iSerializable
    {
        private int m_nAngle = 0;
        private bool m_bEnabled = true;
        private Font m_font = null;
        private Color m_foreColor = Color.Black;
        private double m_fInterval = double.NaN;
        private double m_fIntervalOffset = double.NaN;
        private EDateTimeIntervalType m_intervalOffsetType = EDateTimeIntervalType.Auto;
        private EDateTimeIntervalType m_intervalType = EDateTimeIntervalType.Auto;
        private bool m_bIsEndLabelVisible = true;
        private bool m_bIsStaggered = false;
        private bool m_bTruncatedLabels = false;
        private string m_strFormat = "";

        //-----------------------------------------
        public CAxisLabelStyle()
        {
        }
        //---------------------7--------------------------------------------
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
        public bool IsEndLabelVisible
        {
            get
            {
                return m_bIsEndLabelVisible;
            }
            set
            {
                m_bIsEndLabelVisible = value;
            }
        }
        //-----------------------------------------------------------------
        public bool IsStaggered
        {
            get
            {
                return m_bIsStaggered;
            }
            set
            {
                m_bIsStaggered = value;
            }
        }
        //-----------------------------------------------------------------
        public bool TruncatedLabels
        {
            get
            {
                return m_bTruncatedLabels;
            }
            set
            {
                m_bTruncatedLabels = value;
            }
        }

        //---------------------7--------------------------------------------
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

        //---------------------------------------------------
        private int GetNumVersion()
        {
            return 1;
        }

        //---------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteInt(ref m_nAngle);
            serializer.TraiteBool(ref m_bEnabled);
            CUtilFont.SerializeFont(serializer, ref m_font);
            serializer.TraiteColor(ref m_foreColor);
            serializer.TraiteDouble(ref m_fInterval);
            serializer.TraiteDouble(ref m_fIntervalOffset);
            serializer.TraiteEnum<EDateTimeIntervalType>(ref m_intervalOffsetType);
            serializer.TraiteEnum<EDateTimeIntervalType>(ref m_intervalType);
            serializer.TraiteBool(ref m_bIsEndLabelVisible);
            serializer.TraiteBool(ref m_bIsStaggered);
            serializer.TraiteBool(ref m_bTruncatedLabels);
            if (nVersion >= 1)
                serializer.TraiteString(ref m_strFormat);
            return result;
        }
    }

    public class CAxisLabelStyleConvertor : CGenericObjectConverter<CAxisLabelStyle>
    {
        public override string GetString(CAxisLabelStyle value)
        {
            return "Style";
        }
    }



}