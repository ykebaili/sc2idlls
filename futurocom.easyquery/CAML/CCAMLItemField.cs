using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace futurocom.easyquery.CAML
{
    //--------------------------------------------
    [Serializable]
    public class CCAMLItemField : CCAMLItem
    {
        private string m_strXmlAttrib;
        private string m_strXmlAttribValue;
        private string m_strNomChamp;

        //------------------------------------
        public CCAMLItemField()
        {
        }

        //------------------------------------
        public CCAMLItemField(string strNomChamp,
            string strXmlAttrib,
            string strXmlAttribValue)
        {
            m_strNomChamp = strNomChamp;
            m_strXmlAttrib = strXmlAttrib;
            m_strXmlAttribValue = strXmlAttribValue;
        }

        //------------------------------------
        protected override bool ShouldApply(CEasyQuery query)
        {
            return true;
        }


        //------------------------------------
        public override string Libelle
        {
            get
            {
                return m_strNomChamp;
            }
        }

        //------------------------------------
        public string NomChamp
        {
            get
            {
                return m_strNomChamp;
            }
            set
            {
                m_strNomChamp = value;
            }
        }

        //------------------------------------
        protected override void MyGetRowFilter(CEasyQuery query, StringBuilder bl)
        {
            bl.Append(" [");
            bl.Append(NomChamp);
            bl.Append("] ");
        }

        //------------------------------------
        public string XmlAttrib
        {
            get
            {
                return m_strXmlAttrib;
            }
            set
            {
                m_strXmlAttrib = value;
            }
        }

        //------------------------------------
        public string XmlAttribValue
        {
            get
            {
                return m_strXmlAttribValue;
            }
            set
            {
                m_strXmlAttribValue = value;
            }
        }


        //------------------------------------
        protected override void MyGetXmlText(CEasyQuery query, StringBuilder bl, int nIndent)
        {
            AddIndent(bl, nIndent);
            bl.Append("<FieldRef ");
            bl.Append(m_strXmlAttrib);
            bl.Append("='");
            bl.Append(m_strXmlAttribValue);
            bl.Append("'/>");
        }

        //--------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //--------------------------------------------
        public override CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (result)
                result = base.Serialize(serializer);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strXmlAttrib);
            serializer.TraiteString(ref m_strXmlAttribValue);
            serializer.TraiteString(ref m_strNomChamp);
            return result;
        }

    }
}
