using sc2i.common;
using sc2i.expression;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.data.dynamic.StructureImport.SmartImport
{
    [Serializable]
    public class CMappageEntiteFille : I2iSerializable
    {
        private CSourceSmartImport m_source = null;
        private CConfigMappagesSmartImport m_config = new CConfigMappagesSmartImport();

        //------------------------------------------------
        public CMappageEntiteFille()
        {

        }

        //------------------------------------------------
        public CConfigMappagesSmartImport ConfigMappage
        {
            get
            {
                return m_config;
            }
            set
            {
                m_config = value;
            }

        }

        //------------------------------------------------
        public CSourceSmartImport Source
        {
            get
            {
                return m_source;
            }
            set
            {
                m_source = value;
            }
        }

        //------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = serializer.TraiteObject<CSourceSmartImport>(ref m_source);
            if (result)
                result = serializer.TraiteObject<CConfigMappagesSmartImport>(ref m_config);
            return result;
        }

    }
}
