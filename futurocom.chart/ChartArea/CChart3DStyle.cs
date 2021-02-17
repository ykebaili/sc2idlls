using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace futurocom.chart.ChartArea
{
    [Serializable]
    public class CChart3DStyle : I2iSerializable
    {
        private bool m_bEnable3D = false;
        private int m_nInclination = 30;
        private bool m_bIsClustered = false;
        private bool m_bIsRightAngleAxes = true;
        private ELightStyle m_lightStyle = ELightStyle.Simplistic;
        private int m_nPerspective = 0;
        private int m_nPointDepth = 100;
        private int m_nPointGapDepth = 100;
        private int m_nRotation = 30;
        private int m_nWallWidth = 7;

        //-------------------------------------------------
        public CChart3DStyle()
        {
        }
        //-----------------------------------------------------------------
        public bool Enable3D
        {
            get
            {
                return m_bEnable3D;
            }
            set
            {
                m_bEnable3D = value;
            }
        }
        //-----------------------------------------------------------------
        public int Inclination
        {
            get
            {
                return m_nInclination;
            }
            set
            {
                m_nInclination = value;
            }
        }
        //-----------------------------------------------------------------
        public bool IsClustered
        {
            get
            {
                return m_bIsClustered;
            }
            set
            {
                m_bIsClustered = value;
            }
        }
        //-----------------------------------------------------------------
        public bool IsRightAngleAxes
        {
            get
            {
                return m_bIsRightAngleAxes;
            }
            set
            {
                m_bIsRightAngleAxes = value;
            }
        }
        //-----------------------------------------------------------------
        public ELightStyle LightStyle
        {
            get
            {
                return m_lightStyle;
            }
            set
            {
                m_lightStyle = value;
            }
        }
        //-----------------------------------------------------------------
        public int Perspective
        {
            get
            {
                return m_nPerspective;
            }
            set
            {
                m_nPerspective = value;
            }
        }
        //-----------------------------------------------------------------
        public int PointDepth
        {
            get
            {
                return m_nPointDepth;
            }
            set
            {
                m_nPointDepth = value;
            }
        }
        //-----------------------------------------------------------------
        public int PointGapDepth
        {
            get
            {
                return m_nPointGapDepth;
            }
            set
            {
                m_nPointGapDepth = value;
            }
        }
        //-----------------------------------------------------------------
        public int Rotation
        {
            get
            {
                return m_nRotation;
            }
            set
            {
                m_nRotation = value;
            }
        }
        //-----------------------------------------------------------------
        public int WallWidth
        {
            get
            {
                return m_nWallWidth;
            }
            set
            {
                m_nWallWidth = value;
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
            serializer.TraiteBool(ref m_bEnable3D);
            serializer.TraiteInt(ref m_nInclination);
            serializer.TraiteBool(ref m_bIsClustered);
            serializer.TraiteBool(ref m_bIsRightAngleAxes);
            serializer.TraiteEnum<ELightStyle>(ref m_lightStyle);
            serializer.TraiteInt(ref m_nPerspective);
            serializer.TraiteInt(ref m_nPointDepth);
            serializer.TraiteInt(ref m_nPointGapDepth);
            serializer.TraiteInt(ref m_nRotation);
            serializer.TraiteInt(ref m_nWallWidth);
            return result;
        }
    }

    //-----------------------------------------------------------------
    public class C3DStyleConverter : CGenericObjectConverter<CChart3DStyle>
    {
        public override string GetString(CChart3DStyle value)
        {
            return "3D style";
        }
    }

    //-----------------------------------------------------------------
    public class CChart3DStyleConvertor : CGenericObjectConverter<CChart3DStyle>
    {
        public override string GetString(CChart3DStyle value)
        {
            return "3D style";
        }
    }
    
}
