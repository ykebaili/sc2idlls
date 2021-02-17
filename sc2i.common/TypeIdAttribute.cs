using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace sc2i.common
{
    [AttributeUsage(AttributeTargets.Class)]
    [AutoExec("Autoexec")]
    public class TypeIdAttribute : Attribute
    {
        private static Dictionary<Type, string> m_dicTypeToTypeId = new Dictionary<Type, string>();
        private static Dictionary<string, Type> m_dicTypeIdToType = new Dictionary<string, Type>();


        public readonly string IdDeType;


        //-----------------------------------------------------
        public TypeIdAttribute(string strTypeId)
        {
            IdDeType = strTypeId;
        }

        ///Ces fonctions sont internal car utilisées uniquement par CActivatorSurChaine
        //-----------------------------------------------------
        internal static string GetTypeId(Type tp)
        {
            string strId = "";
            if (!m_dicTypeToTypeId.TryGetValue(tp, out strId))//s'il n'y est pas, c'est qu'il n'existe pas car le dic est toujours à jours
            {
                return null;
            }
            return strId;
        }

        //-----------------------------------------------------
        internal static Type GetType(string strId)
        {
            Type tp = null;
            m_dicTypeIdToType.TryGetValue(strId, out tp);//s'il n'y est pas, c'est qu'il n'existe pas car le dic est toujours à jours
            if (tp == null)
                return CActivatorSurChaine.GetType(strId, false, false);
            return tp;
        }

        //-----------------------------------------------------
        public  static void Autoexec()
        {
            LoadTypedsId();
        }

        //-----------------------------------------------------
        private static bool m_bIsInit = false;
        private static void LoadTypedsId()
        {
            if ( m_bIsInit )
                return;
            m_bIsInit = true;
            foreach (Assembly ass in CGestionnaireAssemblies.GetAssemblies())
            {
                LoadTypesId(ass);
            }
            AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(CurrentDomain_AssemblyLoad);

        }

        //-----------------------------------------------------
        private static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            LoadTypesId(args.LoadedAssembly);
        }

        //-----------------------------------------------------
        private static void LoadTypesId(Assembly ass)
        {
            foreach ( Type tp in ass.GetTypes() )
            {
                object[] atts = tp.GetCustomAttributes(typeof(TypeIdAttribute), false);
                string strId = "";
                if (atts.Length > 0)
                {
                    strId = ((TypeIdAttribute)atts[0]).IdDeType;
                    Type tpExistant = null;
                    if (m_dicTypeIdToType.TryGetValue(strId, out tpExistant))
                    {
                        if (tpExistant != tp)
                            throw new Exception("Type Id " + strId + " is either for type " + tpExistant.ToString() + " and " + tp.ToString());
                    }
                    m_dicTypeToTypeId[tp] = strId;
                    m_dicTypeIdToType[strId] = tp;
                }
            }
        }
                
    }
}
