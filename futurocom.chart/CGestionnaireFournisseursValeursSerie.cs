using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace futurocom.chart
{
    public static class CGestionnaireFournisseursValeursSerie
    {
        private static HashSet<Type> m_setTypesConnus = new HashSet<Type>();

        //--------------------------------------------------------
        public static void RegisterFournisseur ( Type tp )
        {
            m_setTypesConnus.Add ( tp );
        }

        //--------------------------------------------------------
        public static IEnumerable<Type> GetTypesFournisseursConnus()
        {
            return m_setTypesConnus.ToArray();
        }



    }
}
