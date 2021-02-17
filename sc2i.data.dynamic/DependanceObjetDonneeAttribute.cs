using sc2i.common;
using sc2i.data.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.data.dynamic
{
    /// <summary>
    /// Indique que la propriete représente une dépendance à un objet donnée
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    [AutoExec("Autoexec")]
    public class DependanceObjetDonneeAttribute : Attribute, IFournisseurDependancesObjetDonnee
    {

        //----------------------------------------------------------------------
        public static void Autoexec()
        {
            CEntitiesManager.RegisterFournisseurDependances(typeof(DependanceObjetDonneeAttribute));
        }

        //----------------------------------------------------------------------
        public List<CReferenceObjetDependant> GetDependances(CEntitiesManager manager, CObjetDonnee objet)
        {
            List<CReferenceObjetDependant> lstRefs = new List<CReferenceObjetDependant>();
            if (objet == null)
                return lstRefs;
            Type tpObjet = objet.GetType();
            List<MethodInfo> lstMethodes = new List<MethodInfo>();
            foreach ( MemberInfo member in from m in tpObjet.GetMembers() where
                                           m.GetCustomAttribute<DependanceObjetDonneeAttribute>(true) != null
                                           select m )
                                           {
                MethodInfo method = member as MethodInfo;
                if ( method == null && member is PropertyInfo )
                    method = ((PropertyInfo)member).GetGetMethod();
                string strNom = member.Name;
                DynamicFieldAttribute fieldAtt = member.GetCustomAttribute<DynamicFieldAttribute>(true);
                if ( fieldAtt != null )
                    strNom = fieldAtt.NomConvivial;
                if ( method != null )
                {
                try
                {
                    CObjetDonnee dep = method.Invoke(objet, new object[0]) as CObjetDonnee;
                    if ( dep != null )
                    {
                        lstRefs.Add(new CReferenceObjetDependant(strNom, dep));
                    }
                }
                    catch{}
                }
            }
            return lstRefs;
        }
    }
}
