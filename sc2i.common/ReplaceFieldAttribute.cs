using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace sc2i.common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ReplaceFieldAttribute : Attribute
    {
        
        private class COldFieldsToPropertyInfos : Dictionary<string, PropertyInfo>
        {
        }

        private static Dictionary<Type, COldFieldsToPropertyInfos> m_dicTypeToReplaceFieldAttributes = new Dictionary<Type, COldFieldsToPropertyInfos>();



        public readonly string OldCSharpName = "";
        public readonly string OldDynamicField = "";

        public ReplaceFieldAttribute(string strOldCSharpName,
            string strOldDynamicField)
        {
            OldCSharpName = strOldCSharpName;
            OldDynamicField = strOldDynamicField;
        }

        public static PropertyInfo FindFieldObsolete ( Type tp, string strNomObsolete )
        {
            if ( tp == null || strNomObsolete.Length == 0 || strNomObsolete == null )
                return null;
            COldFieldsToPropertyInfos mapOld = null;
            if ( !m_dicTypeToReplaceFieldAttributes.TryGetValue(tp, out mapOld ) )
            {
                mapOld = new COldFieldsToPropertyInfos();
                foreach ( PropertyInfo info in tp.GetProperties( ))
                {
                    object[] atts = info.GetCustomAttributes(typeof(ReplaceFieldAttribute), true);
                    if ( atts != null && atts.Length > 0 )
                    {
                        foreach ( ReplaceFieldAttribute att in atts )
                            mapOld.Add ( att.OldCSharpName, info );
                    }
                }
                m_dicTypeToReplaceFieldAttributes[tp] = mapOld;
            }
            PropertyInfo propTrouvée = null;
            if ( mapOld != null )
            {
                mapOld.TryGetValue(strNomObsolete, out propTrouvée);
            }
            return propTrouvée;
        }

    }
}
