using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace futurocom.chart.ChartArea
{
    [Serializable]
    public class CChartElementPosition : I2iSerializable
    {
        private bool m_bAuto = true;
        private float m_fX = 0;
        private float m_fY = 0;
        private float m_fWidth = 100;
        private float m_fHeight = 100;


        //-----------------------------------------
        public CChartElementPosition()
        {
        }
        //-----------------------------------------------------------------
        public bool Auto
        {
            get
            {
                return m_bAuto;
            }
            set
            {
                m_bAuto = value;
            }
        }
        //-----------------------------------------------------------------
        public float X
        {
            get
            {
                return m_fX;
            }
            set
            {
                m_fX = value;
            }
        }
        //-----------------------------------------------------------------
        public float Y
        {
            get
            {
                return m_fY;
            }
            set
            {
                m_fY = value;
            }
        }
        //-----------------------------------------------------------------
        public float Width
        {
            get
            {
                return m_fWidth;
            }
            set
            {
                m_fWidth = value;
            }
        }
        //-----------------------------------------------------------------
        public float Height
        {
            get
            {
                return m_fHeight;
            }
            set
            {
                m_fHeight = value;
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
            serializer.TraiteBool(ref m_bAuto);
            serializer.TraiteFloat(ref m_fX);
            serializer.TraiteFloat(ref m_fY);
            serializer.TraiteFloat(ref m_fWidth);
            serializer.TraiteFloat(ref m_fHeight);
            return result;
        }


    }

    public class CChartElementPositionConvertor : CGenericObjectConverter<CChartElementPosition>
    {
        public override string GetString(CChartElementPosition value)
        {
            return "Position";
        }
    }
}
