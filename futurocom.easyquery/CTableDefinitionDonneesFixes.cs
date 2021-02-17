using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace futurocom.easyquery
{
    [Serializable]
    public class CTableDefinitionDonneesFixes : CTableDefinitionBaseDonneesFixes, I2iCloneableAvecTraitementApresClonage
    {
        private string m_strId = "";
        private string m_strName = "";

        public CTableDefinitionDonneesFixes()
        {
            m_strId = Guid.NewGuid().ToString();
        }

        //----------------------------------------
        public override string Id
        {
            get { return m_strId;  }
        }

        //----------------------------------------
        public override string TableName
        {
            get
            {
                return m_strName;
            }
            set
            {
                m_strName = value;
            }
        }

        //----------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //----------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (result)
                result = base.Serialize(serializer);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strId);
            serializer.TraiteString(ref m_strName);
            if ( serializer.IsForClone)
                m_strId = Guid.NewGuid().ToString();
            return result;
        }

        //-------------------------------------------------------------
        public void TraiteApresClonage(I2iSerializable source)
        {
            m_strId = Guid.NewGuid().ToString();
        }
    }
}
