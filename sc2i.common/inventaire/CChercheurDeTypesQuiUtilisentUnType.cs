using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;

namespace sc2i.common.inventaire
{
    /// <summary>
    /// Permet de rechercher tous les Types qui utilisent un Type Donné (typeRecherche)
    /// en tant que Type retourné directement par une propriété ou une méthode
    /// ou indirectement par un Type retourné qui utilise le Type recherché
    /// </summary>
    public class CChercheurDeTypesQuiUtilisentUnType<TypeRecherche>
    {
        private HashSet<Type> m_typesQuiUtilisent = new HashSet<Type>();
        private HashSet<Type> m_typesQuinUtilisentPas = new HashSet<Type>();
        private HashSet<Type> m_typesQuonVeutPasExplorer = new HashSet<Type>();


        public CChercheurDeTypesQuiUtilisentUnType(IEnumerable<Type> typesAPasExplorer)
        {
            TypesAPasExplorer = typesAPasExplorer;

            foreach (Assembly ass in CGestionnaireAssemblies.GetAssemblies())
            {
                foreach (Type tp in ass.GetTypes())
                {
                    IEnumerable<Type> lst = from t in m_typesQuonVeutPasExplorer
                                            where t.IsAssignableFrom(tp)
                                            select t;
                    if (lst.Count() > 0)
                        continue;

                    // Cherche dans les Méthodes directes
                    foreach (MethodInfo info in tp.GetMethods())
                    {
                        if (info.IsStatic)
                            continue;
                        if (info.GetParameters().Count() > 0)
                            continue;

                        Type tpTest = info.ReturnType;
                        if (tpTest.IsArray)
                            tpTest = tpTest.GetElementType();
                        if (tpTest != typeof(object))
                        {
                            lst = from t in m_typesQuonVeutPasExplorer
                                    where t.IsAssignableFrom(tpTest)
                                    select t;
                            if (lst.Count() > 0)
                                continue;

                            if (typeof(TypeRecherche).IsAssignableFrom(tpTest) || tpTest.IsAssignableFrom(typeof(TypeRecherche)))
                            {
                                Type tpToAdd = tp;
                                while (tpToAdd != typeof(object) && tpToAdd != null)
                                {
                                    m_typesQuiUtilisent.Add(tpToAdd);
                                    tpToAdd = tpToAdd.BaseType;
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }

        public IEnumerable<Type> TypesAPasExplorer
        {
            get
            {
                return m_typesQuonVeutPasExplorer;
            }
            set
            {
                if(value != null)
                    m_typesQuonVeutPasExplorer = new HashSet<Type>(value);
            }
        }

        public bool IsDansLaListe ( Type tp )
        {

            if (m_typesQuiUtilisent.Contains ( tp ) )
                return true;
            HashSet<Type> typesFaits = new HashSet<Type>();

            return IsDansLaListe ( tp, typesFaits );
        }

        private bool IsDansLaListe(Type tp, HashSet<Type> typeFaits)
        {
            if (m_typesQuinUtilisentPas.Contains(tp))
                return false;

            IEnumerable<Type> lst = from t in m_typesQuonVeutPasExplorer
                                    where t.IsAssignableFrom(tp)
                                    select t;
            if (lst.Count() > 0)
                return false;

            List<Type> lstAFaire = new List<Type>();
            typeFaits.Add(tp);
            lstAFaire.Add(tp);

            while (lstAFaire.Count > 0)
            {
                List<Type> lstNewAFaire = new List<Type>();
                foreach (Type tpAFaire in lstAFaire)
                {
                    typeFaits.Add(tpAFaire);
                    // Recherche dans les Méthodes filles
                    foreach (MethodInfo info in tpAFaire.GetMethods())
                    {
                        if (info.IsStatic)
                            continue;
                        if (info.GetParameters().Count() > 0)
                            continue;

                        Type tpTest = info.ReturnType;
                        if (tpTest.IsArray)
                            tpTest = tpTest.GetElementType();

                        lst = from t in m_typesQuonVeutPasExplorer
                              where t.IsAssignableFrom(tpTest)
                              select t;
                        if (lst.Count() > 0)
                            continue;

                        if (m_typesQuiUtilisent.Contains(tpTest))
                        {
                            m_typesQuiUtilisent.Add(tp);
                            return true;
                        }
                        if (!typeFaits.Contains(tpTest))
                            lstNewAFaire.Add(tpTest);
                    }
                }
                lstAFaire = lstNewAFaire;
            }
            m_typesQuinUtilisentPas.Add(tp);

            return false;
        }
    }
}
