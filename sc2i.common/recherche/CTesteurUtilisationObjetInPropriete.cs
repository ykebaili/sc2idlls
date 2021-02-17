using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace sc2i.common.recherche
{
    /// <summary>
    /// Teste si un objet a une propriété qui référence l'objet demandé
    /// </summary>
    [AutoExec("Autoexec")]
    public class CTesteurUtilisationObjetInPropriete : ITesteurUtilisationObjet
    {
        /// <summary>
        /// Testeurs testant récursivement certains types de propriétés
        /// </summary>
        private static Dictionary<Type, List<ITesteurUtilisationObjet>> m_dicTesteursSupplementaires = new Dictionary<Type, List<ITesteurUtilisationObjet>>();

        public static void RegisterTesteurSupplementaire(Type typePropriete, ITesteurUtilisationObjet testeurAUtiliser)
        {
            List<ITesteurUtilisationObjet> lst = null;
            if (!m_dicTesteursSupplementaires.TryGetValue(typePropriete, out lst))
            {
                lst = new List<ITesteurUtilisationObjet>();
                m_dicTesteursSupplementaires[typePropriete] = lst;
            }
            foreach (ITesteurUtilisationObjet test in lst)
                if (test.GetType() == testeurAUtiliser.GetType())
                    return;
            lst.Add(testeurAUtiliser);
        }
        
        //------------------------------------------------------
        public static void Autoexec()
        {
            CTesteurUtilisationObjet.RegisterTesteur(new CTesteurUtilisationObjetInPropriete());
        }

        //------------------------------------------------------
        public bool DoesUse(object objetUtilisateur, object objetCherche)
        {
            Type tp = objetCherche.GetType();
            PropertyInfo[] props = objetUtilisateur.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            //Récupère toutes les propriétés qui retournent une formule
            foreach (PropertyInfo info in props)
            {
                MethodInfo getMethode = info.GetGetMethod();
                if (getMethode != null && info.PropertyType.IsAssignableFrom(tp))
                {
                    try
                    {
                        object valeur = getMethode.Invoke(objetUtilisateur, new object[0]);
                        if (valeur != null && valeur.Equals(objetCherche))
                            return true;
                    }
                    catch { }
                }
            }
            //Teste les testeurs supplémentaires
            foreach (KeyValuePair<Type, List<ITesteurUtilisationObjet>> kv in m_dicTesteursSupplementaires)
            {
                foreach (PropertyInfo prop in props)
                {
                    MethodInfo getMethode = prop.GetGetMethod();
                    if (getMethode != null && kv.Key.IsAssignableFrom(prop.PropertyType))
                    {
                        try
                        {
                            object valeur = getMethode.Invoke(objetUtilisateur, new object[0]);
                            if (valeur != null)
                            {
                                foreach (ITesteurUtilisationObjet testeur in kv.Value)
                                {
                                    if (testeur.DoesUse(valeur, objetCherche))
                                        return true;
                                }
                            }
                        }
                        catch { }
                    }
                }
            }
            return false;
        }
    }
}
