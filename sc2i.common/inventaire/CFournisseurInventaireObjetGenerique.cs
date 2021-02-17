using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using sc2i.common;
using System.Reflection;
using System.Collections;

namespace sc2i.common.inventaire
{
    /// <summary>
    /// 
    /// </summary>
    public class CFournisseurInventaireObjetGenerique<TypeRecherche> : IFournisseurInventaire
    {
        private Type m_typeRecherche;
        private bool m_bNePasExplorerLeTypeRecherche = false;

        public CFournisseurInventaireObjetGenerique()
        {
            m_typeRecherche = typeof(TypeRecherche);
        }

        public CFournisseurInventaireObjetGenerique(bool bNePasExplorerLeTypeRecherche)
        {
            m_typeRecherche = typeof(TypeRecherche);
            m_bNePasExplorerLeTypeRecherche = bNePasExplorerLeTypeRecherche;
        }

        public void FillInventaireNonRecursif(object obj, CInventaire inventaire)
        {
            CChercheurDeTypesQuiUtilisentUnType<TypeRecherche> chercheur = new CChercheurDeTypesQuiUtilisentUnType<TypeRecherche>(inventaire.ListeDesTypesAPasExplorer);
            

            #region Recherche dans les Propriétés
            /* // Ca ne sert à rien de parcourir les Propriétés car elles sont traitée en tant que Methode Get_method et Set_method
            foreach (PropertyInfo infoProp in obj.GetType().GetProperties())
            {
                bool bIsEnumerable = false;
                Type tpProp = infoProp.PropertyType;
                //if (tpProp.IsArray)
                //    tpProp = tpProp.GetElementType();
                if(typeof(IEnumerable).IsAssignableFrom(tpProp))
                    bIsEnumerable = true;

                //object valeurEvaluee = infoProp.GetValue(obj, null);
                //ICollection collection = valeurEvaluee as ICollection;

                if (tpProp != typeof(object))
                {
                    if (bIsEnumerable)
                    {
                        try
                        {
                            IEnumerable objetEnumerable = infoProp.GetValue(obj, null) as IEnumerable;
                            if (objetEnumerable != null)
                            {
                                // Ici il faut évaluer chaque objet de la collection et l'ajouter à l'inventaire s'il est du type recherché
                                bool bDuBonType = false;
                                foreach (object item in objetEnumerable)
                                {
                                    if(bDuBonType)
                                        inventaire.AddObject(item, false);
                                    else if (m_typeRecherche.IsAssignableFrom(item.GetType()) || item.GetType().IsAssignableFrom(m_typeRecherche))
                                    {
                                        // Pour le premier item, on test forcément s'il est du bon type. Si oui, on flag bDuBonType pour nepas reteser chaque item de la collection
                                        bDuBonType = true;
                                        inventaire.AddObject(item, false);
                                    }
                                    else if (chercheur.IsDansLaListe(item.GetType()))
                                    {
                                        bDuBonType = true;
                                        inventaire.AddObject(item, true);
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Problème dans l'évaluation de l'objetEnumerable");
                            Console.WriteLine(e.Message);
                        }
                    }
                    // Sinon, ce n'est pas une collection
                    else if (m_typeRecherche.IsAssignableFrom(tpProp) || tpProp.IsAssignableFrom(m_typeRecherche))
                    {
                        // PROPRIETE DIRECTE: Ajoute directement à l'inventaire
                        try
                        {
                            object val = infoProp.GetValue(obj, null);
                            inventaire.AddObject(val, false);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Impossible d'évaluer la Propriété :" + infoProp.Name + " sur l'objet : " + obj.ToString());
                            Console.WriteLine(e.Message);
                        }
                        
                    }
                    else
                    {
                        if (chercheur.IsDansLaListe(tpProp))
                        {
                            // PROPRIETE INDIRECTE: Ajoute le valeur de l'objet qui utilise le type recherché
                            try
                            {
                                object valeurProp = infoProp.GetValue(obj, null);
                                inventaire.AddObject(valeurProp, true);
                            }
                            catch
                            {
                                Console.WriteLine("Impossible d'évaluer la Propriété :" + infoProp.Name + " sur l'objet : " + obj.ToString());
                            }
                        }
                    }
                }

            } */
            #endregion

            #region Recherche dans les Methodes
            foreach (MethodInfo infoMethod in obj.GetType().GetMethods())
            {
                if (infoMethod.IsStatic)
                    continue;
                if (infoMethod.GetParameters().Count() > 0)
                    continue;
                
                bool bIsEnumerable = false;
                Type tpMethod = infoMethod.ReturnType;
                if (typeof(IEnumerable).IsAssignableFrom(tpMethod) && tpMethod != typeof(string)) // on élimine les String, car le string est un IEnumerable et du coup ça fait parcourir chaque caractère de la chaine pour rien
                    bIsEnumerable = true;
                
                if (tpMethod != typeof(object))
                {
                    if (bIsEnumerable)
                    {
                        try
                        {
                            IEnumerable objetEnumerable = infoMethod.Invoke(obj, null) as IEnumerable;
                            if (objetEnumerable != null)
                            {
                                // Ici il faut évaluer chaque objet de la collection et l'ajouter à l'inventaire s'il est du type recherché
                                bool bDuBonType = false;
                                bool bHorsInventaire = false;
                                bool bNePasExplorer = m_bNePasExplorerLeTypeRecherche;
                                foreach (object item in objetEnumerable)
                                {
                                    if (bDuBonType)
                                        inventaire.AddObject(item, bHorsInventaire, bNePasExplorer);
                                    else
                                    {
                                        // Pour le premier item, on test forcément s'il est du bon type. Si oui, on flag bDuBonType pour ne pas reteser chaque item de la collection
                                        if (m_typeRecherche.IsAssignableFrom(item.GetType()) || item.GetType().IsAssignableFrom(m_typeRecherche))
                                        {
                                            bDuBonType = true;
                                            bHorsInventaire = false;
                                            bNePasExplorer = m_bNePasExplorerLeTypeRecherche;
                                        }
                                        else if (chercheur.IsDansLaListe(item.GetType()))
                                        {
                                            bDuBonType = true;
                                            bHorsInventaire = true;
                                            bNePasExplorer = false;
                                        }
                                        if(bDuBonType)
                                            inventaire.AddObject(item, bHorsInventaire, bNePasExplorer);
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Problème dans l'évaluation de l'objetEnumerable");
                            Console.WriteLine(e.Message);
                        }
                    }
                    // Sinon, ce n'est pas une collection
                    else if (m_typeRecherche.IsAssignableFrom(tpMethod) || tpMethod.IsAssignableFrom(m_typeRecherche))
                    {
                        // PROPRIETE DIRECTE: Ajoute directement à l'inventaire
                        try
                        {
                            object val = infoMethod.Invoke(obj, null);
                            inventaire.AddObject(val, false, m_bNePasExplorerLeTypeRecherche);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Impossible d'évaluer la Propriété :" + infoMethod.Name + " sur l'objet : " + obj.ToString());
                            Console.WriteLine(e.Message);
                        }

                    }
                    else
                    {
                        if (chercheur.IsDansLaListe(tpMethod))
                        {
                            // PROPRIETE INDIRECTE: Ajoute le valeur de l'objet qui utilise le type recherché
                            try
                            {
                                object valeurProp = infoMethod.Invoke(obj, null);
                                inventaire.AddObject(valeurProp, true, false);
                            }
                            catch
                            {
                                Console.WriteLine("Impossible d'évaluer la Propriété :" + infoMethod.Name + " sur l'objet : " + obj.ToString());
                            }
                        }
                    }
                }

            }
            #endregion

        }

    }
}
