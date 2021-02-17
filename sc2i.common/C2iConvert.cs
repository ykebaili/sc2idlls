using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace sc2i.common
{
    public interface I2iConvertible
    {
        bool CanConvertTo(Type tp);
        object ConvertTo(Type tp);
    }


    public class C2iConvert
    {

        public static T ChangeType<T>(object obj)
        {
            return (T)ChangeType(obj, typeof(T));
        }

        private static Dictionary<string, MethodInfo> m_dicMethodsToConvert = new Dictionary<string, MethodInfo>();

        public static object ChangeType(object obj, Type nouveauType) 
        {
            if (obj == null)
                return null;
            if (nouveauType.IsAssignableFrom(obj.GetType()))
                return obj;

            string strKey = obj.GetType() + "_" + nouveauType.GetType();
            try
            {
                I2iConvertible convertible = obj as I2iConvertible;
                if (convertible != null && convertible.CanConvertTo(nouveauType))
                    return convertible.ConvertTo(nouveauType);

                MethodInfo method = null;
                object retour = null;
                if ( m_dicMethodsToConvert.TryGetValue(strKey, out method ) )
                {
                    retour = method.Invoke(null, new object[] { obj });
                    if (retour != null && nouveauType.IsAssignableFrom(retour.GetType()))
                        return retour;
                }

                retour = Convert.ChangeType(obj, nouveauType);
                if (nouveauType.IsAssignableFrom ( retour.GetType() ))
                    return retour;
            }
            catch ( Exception e )
            {
                try
                {
                    //Ne marche pas par méthode .Net
                    //tente de trouve un opérateur implicit
                    MethodInfo method = FindConvertMethod(obj.GetType(), obj.GetType(), nouveauType);
                    if (method == null)
                        method = FindConvertMethod(nouveauType, obj.GetType(), nouveauType);
                    if (method != null)
                    {
                        m_dicMethodsToConvert[strKey] = method;
                        object retour = method.Invoke(null, new object[] { obj });
                        if ( nouveauType.IsAssignableFrom(retour.GetType()))
                            return retour;
                    }
                }
                catch
                {
                    throw e;
                }
                    
            }
            throw new Exception("can not convert from " + obj.GetType().ToString() + " to " + nouveauType.ToString());

        }


        public static MethodInfo FindConvertMethod(Type typePortantLaMethode, Type typeSource, Type typeDesire)
        {
            foreach ( MethodInfo method in typePortantLaMethode.GetMethods() )
            {
                if ( method.IsStatic && method.Name == "op_Implicit" )
                {
                    ParameterInfo[] parameters = method.GetParameters();
                    if ( parameters.Length == 1 && 
                        parameters[0].ParameterType == typeSource &&
                        method.ReturnType == typeDesire )
                        return method;
                }
            }
            return null;
        }

    }

}
