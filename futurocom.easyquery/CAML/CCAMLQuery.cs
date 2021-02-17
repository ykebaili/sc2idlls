using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;
using sc2i.common;

namespace futurocom.easyquery.CAML
{
    //--------------------------------------------
    [Serializable]
    public class CCAMLQuery : I2iSerializable
    {
        private CCAMLItem m_root = null;

        public string GetXmlText(CEasyQuery query)
        {
            StringBuilder bl = new StringBuilder();
            if (m_root != null)
                m_root.GetXmlText(query, bl, 0);
            return bl.ToString();
        }

        //--------------------------------------------
        public CCAMLItem RootItem
        {
            get
            {
                return m_root;
            }
            set
            {
                m_root = value;
            }
        }

        //--------------------------------------------
        public string GetRowFilter(CEasyQuery query)
        {
            StringBuilder bl = new StringBuilder();
            if (m_root != null)
                m_root.GetRowFilter(query, bl);
            return bl.ToString();
        }

        //--------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //--------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = serializer.TraiteObject<CCAMLItem>(ref m_root);
            return result;
        }

    }

    

    

    

    
    

    

}