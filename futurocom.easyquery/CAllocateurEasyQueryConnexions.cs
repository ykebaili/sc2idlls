using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace futurocom.easyquery
{
    public static class CAllocateurEasyQueryConnexions
    {
        private static Dictionary<string, Type> m_dicIdTypeConnexionToTypeConnexion = new Dictionary<string, Type>();

        //-----------------------------------------------
        public static void RegisterTypeConnexion(string strIdType, Type typeconnexion)
        {
            m_dicIdTypeConnexionToTypeConnexion[strIdType] = typeconnexion;
        }

       
        //-----------------------------------------------
        public static IEasyQueryConnexion GetNewConnexion(string strIdTypeConnexion)
        {
            Type tp = null;
            if ( m_dicIdTypeConnexionToTypeConnexion.TryGetValue ( strIdTypeConnexion, out tp ) )
            {
                try{
                    IEasyQueryConnexion cnx = Activator.CreateInstance ( tp, new object[0] ) as IEasyQueryConnexion;
                    return cnx;
                }
                catch{
                }
            }
            return null;
        }

        //-----------------------------------------------
        public static IEnumerable<IEasyQueryConnexion> GetConnexionsPossibles()
        {
            List<IEasyQueryConnexion> lstCnx = new List<IEasyQueryConnexion>();
            foreach (string strIdType in m_dicIdTypeConnexionToTypeConnexion.Keys)
            {
                IEasyQueryConnexion cnx = GetNewConnexion(strIdType);
                if (cnx != null)
                    lstCnx.Add(cnx);
            }
            lstCnx.Sort((x, y) => x.ConnexionTypeName.CompareTo(y.ConnexionTypeName));
            return lstCnx.AsReadOnly();
        }
    }
}
