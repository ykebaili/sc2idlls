using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common.inventaire;
using sc2i.common;
using System.Reflection;

namespace sc2i.data.Inventaire
{

/*    public interface IFournisseurInventaireObjetDonneeLies
    {
        void FillInventaireNonRecursif(CObjetDonnee objet, CInventaire inventaire, CFournisseurInventaireObjetDonneeLies.EModeInventaireObjetDonneeLies mode);
    }

    public interface IUtilisateurObjetsDonnee : IUtilisateurObjetsPourInventaire<CObjetDonnee, CContexteDonnee>
    {
    }

    /// <summary>
    /// fournit la liste des compositions d'un objet donnée
    /// </summary>
    public class CFournisseurInventaireObjetDonneeLies : IFournisseurInventaire
    {
        private static List<IFournisseurInventaireObjetDonneeLies> m_listeFournisseursSupplementaires = new List<IFournisseurInventaireObjetDonneeLies>();
        public enum EModeInventaireObjetDonneeLies
        {
            Aucuns = 0,
            Parents = 1,
            FilsNonCompositions = 2,
            FilsCompositions = 4,
            TousLesFils = 6,
            Tous = 255
        }



        private EModeInventaireObjetDonneeLies m_mode = EModeInventaireObjetDonneeLies.Tous;
        private CContexteDonnee m_contexte = null;

        public static void RegisterFournisseur ( IFournisseurInventaireObjetDonneeLies fournisseur )
        {
            if ( m_listeFournisseursSupplementaires.FirstOrDefault ( f=>f.GetType() == fournisseur.GetType() ) == null )
                m_listeFournisseursSupplementaires.Add ( fournisseur );
        }

        //---------------------------------------------------------------------------------
        public CFournisseurInventaireObjetDonneeLies(CContexteDonnee contexte, EModeInventaireObjetDonneeLies mode)
        {
            m_contexte = contexte;
            m_mode = mode;
        }

        //---------------------------------------------------------------------------------
        public void FillInventaireNonRecursif(object obj, CInventaire inventaire)
        {
            CObjetDonnee objetDonnee = obj as CObjetDonnee;
            if (objetDonnee != null)
            {

                CStructureTable structure = CStructureTable.GetStructure(objetDonnee.GetType());
                if ((m_mode & EModeInventaireObjetDonneeLies.TousLesFils) != EModeInventaireObjetDonneeLies.Aucuns)
                {
                    foreach (CInfoRelation relation in structure.RelationsFilles)
                    {
                        if (relation.Composition || (m_mode & EModeInventaireObjetDonneeLies.FilsCompositions) != EModeInventaireObjetDonneeLies.FilsCompositions)
                        {
                            CListeObjetsDonnees lst = objetDonnee.GetDependancesListe(relation.TableFille, relation.ChampsFille);
                            foreach (CObjetDonnee fille in lst)
                                inventaire.AddObject(fille);
                        }
                    }
                }
                if ((m_mode & EModeInventaireObjetDonneeLies.Parents) == EModeInventaireObjetDonneeLies.Parents)
                {
                    foreach (CInfoRelation relation in structure.RelationsParentes)
                    {
                        CObjetDonnee parent = objetDonnee.GetParent(relation.ChampsFille, CContexteDonnee.GetTypeForTable(relation.TableParente));
                        if (parent != null)
                            inventaire.AddObject(parent);
                    }
                }
                foreach (IFournisseurInventaireObjetDonneeLies fournisseur in m_listeFournisseursSupplementaires)
                    fournisseur.FillInventaireNonRecursif(objetDonnee, inventaire, m_mode);
            }
            else
            {
                if (obj is IUtilisateurObjetsDonnee)
                {
                    CResultAErreur result = ((IUtilisateurObjetsDonnee) obj).GetObjetsUtilises(m_contexte);
                    if (result)
                    {
                        foreach (CObjetDonnee objet in (CObjetDonnee[])result.Data)
                        {
                            inventaire.AddObject(objet);
                        }

                    }
                }
            }
            
        }
    }*/
}
