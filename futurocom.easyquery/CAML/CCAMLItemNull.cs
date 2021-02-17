using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace futurocom.easyquery.CAML
{
    //--------------------------------------------
    [Serializable]
    public class CCAMLItemNull : CCAMLItem
    {
        private CCAMLItemField m_champ;
        private bool m_bIsNull = true;

        //--------------------------------------------
        public bool IsNull
        {
            get
            {
                return m_bIsNull;
            }
            set
            {
                m_bIsNull = value;
            }
        }

        //--------------------------------------------
        public CCAMLItemField Field
        {
            get
            {
                return m_champ;
            }
            set
            {
                m_champ = value;
            }
        }

        //--------------------------------------------
        public override string Libelle
        {
            get
            {
                return (Field != null ? Field.Libelle : "?") + " " +
                    (IsNull ? " is null" : "is not null");
            }
        }

        //--------------------------------------------
        private string BaliseXml
        {
            get
            {
                if (m_bIsNull)
                    return "IsNull";
                return "IsNotNull";
            }
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
            serializer.TraiteBool(ref m_bIsNull);
            result = serializer.TraiteObject<CCAMLItemField>(ref m_champ);
            return result;
        }

        //--------------------------------------------
        protected override void MyGetXmlText(CEasyQuery query, StringBuilder bl, int nIndent)
        {
            if (m_champ != null)
            {
                AddIndent(bl, nIndent);
                bl.Append("<");
                bl.Append(BaliseXml);
                bl.Append(">");
                m_champ.GetXmlText(query, bl, nIndent + 1);
                bl.Append("</");
                bl.Append(BaliseXml);
                bl.Append(">");
                bl.Append(Environment.NewLine);
            }
        }

        //--------------------------------------------
        protected override void MyGetRowFilter(CEasyQuery query, StringBuilder bl)
        {
            if (m_champ != null)
            {
                m_champ.GetRowFilter(query, bl);
                bl.Append(" ");
                if (m_bIsNull)
                    bl.Append("is null ");
                else
                    bl.Append("is not null ");
            }
        }
    }
}
