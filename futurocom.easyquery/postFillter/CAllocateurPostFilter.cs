using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace futurocom.easyquery.postFillter
{
    public class CDefPostFilter
    {
        public string Libelle{get;set;}
        public string Id{get;set;}
        public Type TypePostFilter{get;set;}

        //----------------------------------------------------------------
        public CDefPostFilter ( string strId, string strLibelle, Type typePostFilter )
        {
            Libelle = strLibelle;
            Id = strId;
            TypePostFilter = typePostFilter;
        }

        //----------------------------------------------------------------
        public override bool Equals(object obj)
        {
            CDefPostFilter def = obj as CDefPostFilter;
            if (def != null)
                return def.Id == Id;
            return false;
        }


    }
    public static class CAllocateurPostFilter
    {
        private static Dictionary<string, CDefPostFilter> m_dicIdTypePostFilterToType = new Dictionary<string, CDefPostFilter>();

        //----------------------------------------------------------------
        public static void RegisterType ( CDefPostFilter def )
        {
            if ( def != null )
                m_dicIdTypePostFilterToType[def.Id] = def;
        }

        //----------------------------------------------------------------
        public static IEnumerable<CDefPostFilter> GetDefs()
        {
            List<CDefPostFilter> lstDefs = new List<CDefPostFilter>(m_dicIdTypePostFilterToType.Values);
            lstDefs.Sort((x, y) => x.Libelle.CompareTo(y.Libelle));
            return lstDefs.AsReadOnly();
        }

        //----------------------------------------------------------------
        public static CDefPostFilter GetDef ( string strId )
        {
            CDefPostFilter def = null;
            m_dicIdTypePostFilterToType.TryGetValue(strId, out def);
            return def;
        }

        //----------------------------------------------------------------
        public static CDefPostFilter GetDef ( Type typePostFilter )
        {
            foreach (CDefPostFilter def in m_dicIdTypePostFilterToType.Values)
                if (def.TypePostFilter == typePostFilter)
                    return def;
            return null;
        }

        //----------------------------------------------------------------
        public static IPostFilter GetPostFilter ( string strId )
        {
            CDefPostFilter def = GetDef(strId);
            if (def != null)
            {
                IPostFilter filter = Activator.CreateInstance(def.TypePostFilter, new object[0]) as IPostFilter;
                return filter;
            }
            return null;
        }
    }
}
