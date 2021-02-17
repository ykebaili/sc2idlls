using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace sc2i.common
{
    public class CUtilType
    {
        /// <summary>
        /// Trouve la méthode correspondant aux types. Contrairement à 
        /// GetMethod sur Type, types peut contenir un type à null,
        /// qui dans ce cas sert de joker pour n'importe quel type
        /// </summary>
        /// <param name="strNomMethode"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public static MethodInfo FindMethod(Type tp, string strNomMethode, Type[] types)
        {
            IEnumerable<MethodInfo> methods = from m in tp.GetMethods()
                                              where m.Name == strNomMethode &&
                                              MatchTypes(m, types)
                                              select m;
            if (methods.Count() == 0)
            {
                foreach (Type tpInt in tp.GetInterfaces())
                {
                    methods = from m in tpInt.GetMethods()
                              where m.Name == strNomMethode && 
                              MatchTypes ( m, types )
                              select m;
                    if ( methods.Count() > 0 )
                        break;
                }
            }
            if (methods.Count() == 1)
                return methods.ElementAt(0);
            return null;
        }

        public static MethodInfo FindMethodFromParametres(Type tp, string strNomMethode, object[] parametres)
        {
            List<Type> lst = new List<Type>();
            foreach (object parametre in parametres)
                if (parametre == null)
                    lst.Add(null);
                else
                    lst.Add(parametre.GetType());
            return FindMethod(tp, strNomMethode, lst.ToArray());
        }

        private static bool MatchTypes(MethodInfo m, Type[] types)
        {
            ParameterInfo[] parametres = m.GetParameters();
            System.Console.WriteLine(m.Name);
            if (parametres.Length != types.Length)
                return false;
            for (int nParam = 0; nParam < types.Length; nParam++)
            {
                if (types[nParam] != null)
                {
                    if (!parametres[nParam].ParameterType.IsAssignableFrom(types[nParam]))
                        return false;
                }
            }
            return true;
        }
    }
}
