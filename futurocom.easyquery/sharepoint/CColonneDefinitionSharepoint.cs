using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using futurocom.easyquery;
using sc2i.common;

namespace futurocom.easyquery.sharepoint30
{
    public class CColonneDefinitionSharepoint : CColumnDefinitionSimple
    {
        private string m_strSharepointId = "";
        private string m_strDescription = "";

        public CColonneDefinitionSharepoint()
        {
        }

        public string SharepointId
        {
            get
            {
                return m_strSharepointId;
            }
            set
            {
                m_strSharepointId = value;
            }
        }

        public string Description
        {
            get
            {
                return m_strDescription;
            }
            set
            {
                m_strDescription = value;
            }
        }

        private int GetNumVersion()
        {
            return 0;
        }

        //----------------------------------------------
        public override sc2i.common.CResultAErreur Serialize(sc2i.common.C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
            if (!result )
                return result;
            result = base.Serialize(serializer);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strSharepointId);
            serializer.TraiteString (ref m_strDescription);
            return result;
        }
    }
}
