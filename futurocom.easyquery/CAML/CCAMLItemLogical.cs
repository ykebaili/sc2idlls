using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace futurocom.easyquery.CAML
{
    //--------------------------------------------
    [Serializable]
    public abstract class CCAMLItemLogical : CCAMLItem
    {
        private List<CCAMLItem> m_listeChilds = new List<CCAMLItem>();

        //--------------------------------------------
        protected abstract string BaliseXml { get; }

        //--------------------------------------------
        protected abstract string BaliseRowFilter { get; }

        //----------------------------------------
        public IEnumerable<CCAMLItem> Childs
        {
            get
            {
                return m_listeChilds.AsReadOnly();
            }
            set
            {
                m_listeChilds = new List<CCAMLItem>();
                if (value != null)
                    m_listeChilds.AddRange(value);
            }
        }

        //----------------------------------------
        public void AddChild(CCAMLItem item)
        {
            m_listeChilds.Add(item);
        }

        //----------------------------------------
        public void RemoveChild(CCAMLItem item)
        {
            m_listeChilds.Remove(item);
        }

        //----------------------------------------
        public void ClearChilds()
        {
            m_listeChilds.Clear();
        }

        //----------------------------------------
        protected override void MyGetXmlText(CEasyQuery query, StringBuilder bl, int nIndent)
        {
            StringBuilder blTmp = new StringBuilder();
            foreach (CCAMLItem item in Childs)
            {
                item.GetXmlText(query, blTmp, nIndent + 1);
            }
            if (blTmp.Length > 0)
            {
                AddIndent(bl, nIndent);
                bl.Append("<");
                bl.Append(BaliseXml);
                bl.Append(">");
                bl.Append(Environment.NewLine);
                bl.Append(blTmp);
                bl.Append("</");
                bl.Append(BaliseXml);
                bl.Append(">");
                bl.Append(Environment.NewLine);
            }
        }

        //--------------------------------------------
        protected override void MyGetRowFilter(CEasyQuery query, StringBuilder bl)
        {
            bool bHasAdd = false;
            foreach (CCAMLItem item in Childs)
            {
                StringBuilder blTmp = new StringBuilder();
                item.GetRowFilter(query, blTmp);
                if (blTmp.Length > 0)
                {
                    bl.Append("(");
                    bl.Append(blTmp);
                    bl.Append(") ");
                    bl.Append(BaliseRowFilter);
                    bl.Append(" ");
                    bHasAdd = true;
                }
            }
            if (bHasAdd)
                bl.Remove(bl.Length - BaliseRowFilter.Length - 2, BaliseRowFilter.Length + 2);
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
            result = serializer.TraiteListe<CCAMLItem>(m_listeChilds);
            return result;
        }
    }

    //--------------------------------------------
    [Serializable]
    public class CCAMLItemAnd : CCAMLItemLogical
    {
        //--------------------------------------------
        public override string Libelle
        {
            get
            {
                return "And";
            }
        }

        //--------------------------------------------
        protected override string BaliseXml
        {
            get { return "And"; }
        }

        //--------------------------------------------
        protected override string BaliseRowFilter
        {
            get { return "And"; }
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
            return result;
        }
    }

    //--------------------------------------------
    [Serializable]
    public class CCAMLItemOr : CCAMLItemLogical
    {
        public override string Libelle
        {
            get
            {
                return "Or";
            }
        }

        //--------------------------------------------
        protected override string BaliseXml
        {
            get { return "Or"; }
        }

        //--------------------------------------------
        protected override string BaliseRowFilter
        {
            get { return "Or"; }
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
            return result;
        }
    }
}
