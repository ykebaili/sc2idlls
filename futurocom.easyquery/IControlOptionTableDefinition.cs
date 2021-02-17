using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace futurocom.easyquery
{
    /// <summary>
    /// contrôle présentant des options pour une table source
    /// </summary>
    public interface IControlOptionTableDefinition : IDisposable
    {
        void FillFromTable(ITableDefinition table);
        CResultAErreur MajChamps();
    }

    public static class CAllocateurControleOptionTableDefinition
    {
        private static Dictionary<Type, Type> m_dicTypeTableToTypeControle = new Dictionary<Type, Type>();

        public static void RegistrerControleOption(Type typeTable, Type typeControle)
        {
            m_dicTypeTableToTypeControle[typeTable] = typeControle;
        }

        public static IControlOptionTableDefinition GetControleOptions(ITableDefinition table)
        {
            if ( table == null )
                return null;
            Type tp = null;
            if ( m_dicTypeTableToTypeControle.TryGetValue ( table.GetType(), out tp ) )
            {
                if ( tp != null )
                {
                    IControlOptionTableDefinition ctrl = Activator.CreateInstance ( tp, new object[0] ) as IControlOptionTableDefinition;
                    return ctrl;
                }
            }
            return null;
        }
    }

}
