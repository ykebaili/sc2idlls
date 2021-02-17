using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace sc2i.common.inventaire
{
    /// <summary>
    /// Permet de stocker et d'ajouter des éléments dans un inventaire
    /// </summary>
    public class CInventaire
    {
        /// <summary>
        /// Le premier paramètre contient un objet, le second indique s'il a été inventorié
        /// </summary>
        private Dictionary<object, bool?> m_dicInventaire = new Dictionary<object, bool?>();
        private Dictionary<object, bool?> m_dicHorsInventaire = new Dictionary<object, bool?>();
        private HashSet<Type> m_listeDesTypesANePasAjouter = new HashSet<Type>();


        public CInventaire()
        {
        }

        public CInventaire(IEnumerable<Type> listeTypesQuOnVeutPas)
        {
            ListeDesTypesAPasExplorer = listeTypesQuOnVeutPas;
        }

        public IEnumerable<Type> ListeDesTypesAPasExplorer
        {
            get
            {
                return m_listeDesTypesANePasAjouter;
            }
            set
            {
                if (value != null)
                    m_listeDesTypesANePasAjouter = new HashSet<Type>(value);
            }
        }

        public void AddObject(object obj)
        {
            AddObject(obj, false);
        }

        public void AddObject(object obj, bool bHorsInventaire)
        {
            AddObject(obj, bHorsInventaire, false);
        }
        
        public void AddObject(object obj, bool bHorsInventaire, bool bNePasExplorer)
        {
            if (obj != null && !m_listeDesTypesANePasAjouter.Contains(obj.GetType()))
            {
                bool? bDejaExplore;
                if (bHorsInventaire)
                {
                    if (m_dicInventaire.TryGetValue(obj, out bDejaExplore))
                        m_dicInventaire[obj] = bDejaExplore.Value || bNePasExplorer;
                    else if (!m_dicHorsInventaire.ContainsKey(obj))
                        m_dicHorsInventaire.Add(obj, bNePasExplorer);
                }
                else
                {
                    if (m_dicInventaire.TryGetValue(obj, out bDejaExplore))
                        m_dicInventaire[obj] = bDejaExplore.Value || bNePasExplorer;
                    else
                    {
                        // Si l'objet est déjà Hors inventaire, on le supprime du "Hors inventaire
                        if (m_dicHorsInventaire.TryGetValue(obj, out bDejaExplore))
                        {
                            m_dicInventaire.Add(obj, bDejaExplore.Value || bNePasExplorer);
                            m_dicHorsInventaire.Remove(obj);
                        }
                        else
                        {
                            m_dicInventaire.Add(obj, bNePasExplorer);
                        }
                    }
                }
            }
        }

        public IEnumerable<object> GetObjects()
        {
            return GetObjects(false);
        }

        public IEnumerable<object> GetObjects(bool bHorsInventaire)
        {
            List<object> lst = new List<object>();
            if (bHorsInventaire)
                lst.AddRange(m_dicHorsInventaire.Keys);
            else
                lst.AddRange(m_dicInventaire.Keys);
            
            return lst;
        }

        public IEnumerable<object> GetObjectsAInventorier()
        {
            IEnumerable<object> lst = from kv in m_dicInventaire
                                      where kv.Value == null || !kv.Value.Value
                                      select kv.Key;
            
            IEnumerable<object> lstHors = from kv in m_dicHorsInventaire
                                      where kv.Value == null || !kv.Value.Value
                                      select kv.Key;

            List<object> listeObjets = new List<object>();
            listeObjets.AddRange(lst);
            listeObjets.AddRange(lstHors);

            return (IEnumerable<object>) listeObjets;
        }

        public bool ShouldExplore(object obj)
        {
            if (obj == null)
                return false;
            bool? bExploré = false;
            if (!m_dicInventaire.TryGetValue(obj, out bExploré))
                m_dicHorsInventaire.TryGetValue(obj, out bExploré);

            return bExploré == null || !bExploré.Value;
        }

        public void SetExploré(object obj)
        {
            if (obj != null)
            {
                if (m_dicInventaire.ContainsKey(obj))
                    m_dicInventaire[obj] = true;
                if (m_dicHorsInventaire.ContainsKey(obj))
                    m_dicHorsInventaire[obj] = true;
            }
        }
    }
            
    /// <summary>
    /// element capable de calculer un inventaire sur un objet
    /// Chaque fournisseur est spécialisé dans certains modes d'inventoriation des objets
    /// </summary>
    public interface IFournisseurInventaire
    {
        /// <summary>
        /// Remplit la liste passée en paramètre avec l'inventaire et retourne la liste des nouveaux éléments
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="liste"></param>
        /// <returns></returns>
        void FillInventaireNonRecursif(object obj, CInventaire inventaire);
    }

    /// <summary>
    /// Indique que l'objet inventorié fait référence à des Objets qui doivent être ajoutés à la liste
    /// de l'inventaire
    /// </summary>
    /// <typeparam name="TypeObjets"></typeparam>
    public interface IUtilisateurObjetsPourInventaire<TypeObjets, TypeParametre>
    {
        CResultAErreur GetObjetsUtilises(TypeParametre parametre);
    }


    /// <summary>
    /// Permet de calculer l'inventaire d'un élément
    /// Assure la récursivité des éléments à invantorier
    /// </summary>
    public  class CCalculeurInventaire
    {
        public static CInventaire GetInventaire(object obj, params IFournisseurInventaire[] fournisseurs)
        {
            return GetInventaire(obj, null, fournisseurs);
        }

        public static CInventaire GetInventaire(object obj, IEnumerable<Type> listeTypesQuOnVeutPas, params IFournisseurInventaire[] fournisseurs)
        {
            CInventaire inventaire = new CInventaire(listeTypesQuOnVeutPas);
            FillInventaire(obj, inventaire, fournisseurs);
            return inventaire;
        }

        private static void FillInventaire(object obj, CInventaire inventaire, IFournisseurInventaire[] fournisseurs)
        {
            if (inventaire.ShouldExplore(obj))
            {
                foreach (IFournisseurInventaire fournisseur in fournisseurs)
                {
                    fournisseur.FillInventaireNonRecursif(obj, inventaire);
                }
                inventaire.SetExploré ( obj );
                ArrayList lst = new ArrayList(inventaire.GetObjectsAInventorier().ToArray());
                foreach (object objInv in lst)
                    FillInventaire(objInv, inventaire, fournisseurs);
            }
        }
        
    }
}
