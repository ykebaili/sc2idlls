using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using sc2i.common;
using sc2i.data.dynamic;
//using sc2i.data.Inventaire;

namespace sc2i.data.dynamic.Inventaire
{
    /*[AutoExec("Autoexec")]
    public class CFournisseurInventaireObjetDonneeLieChampCustom : IFournisseurInventaireObjetDonneeLies
    {
        public static void Autoexec()
        {
            CFournisseurInventaireObjetDonneeLies.RegisterFournisseur(new CFournisseurInventaireObjetDonneeLieChampCustom());
        }

        public void FillInventaireNonRecursif(CObjetDonnee objet, sc2i.common.inventaire.CInventaire inventaire, CFournisseurInventaireObjetDonneeLies.EModeInventaireObjetDonneeLies mode)
        {
            IObjetDonneeAChamps objAChamp = objet as IObjetDonneeAChamps;
            if (objAChamp == null)
                return;

            //recherche les parents
            if ((mode & CFournisseurInventaireObjetDonneeLies.EModeInventaireObjetDonneeLies.Parents) == CFournisseurInventaireObjetDonneeLies.EModeInventaireObjetDonneeLies.Parents)
            {
                CListeObjetsDonnees lst = objAChamp.RelationsChampsCustom;
                foreach (CRelationElementAChamp_ChampCustom rel in lst)
                {
                    if (rel.ChampCustom.TypeDonneeChamp.TypeDonnee == TypeDonnee.tObjetDonneeAIdNumeriqueAuto)
                    {
                        object obj = rel.Valeur as CObjetDonnee;
                        inventaire.AddObject(obj);
                    }
                }
            }
            //Relations filles
            if ((mode & CFournisseurInventaireObjetDonneeLies.EModeInventaireObjetDonneeLies.FilsNonCompositions) == CFournisseurInventaireObjetDonneeLies.EModeInventaireObjetDonneeLies.FilsNonCompositions)
            {
                CListeObjetsDonnees lst = new CListeObjetsDonnees(objet.ContexteDonnee, typeof(CChampCustom));
                lst.Filtre = new CFiltreData(CChampCustom.c_champTypeObjetDonnee + "=@1",
                    objet.GetType().ToString());
                foreach (CChampCustom champ in lst)
                {
                    CRoleChampCustom role = champ.Role;
                    Type tpParent = role.TypeAssocie;
                    IObjetDonneeAChamps elt = Activator.CreateInstance(tpParent, new object[] { objet.ContexteDonnee }) as IObjetDonneeAChamps;
                    if (elt != null)
                    {
                        CRelationElementAChamp_ChampCustom rel = elt.GetNewRelationToChamp();
                        CListeObjetsDonnees liste = new CListeObjetsDonnees(elt.ContexteDonnee, elt.GetType());
                        liste.Filtre = new CFiltreDataAvance(
                            elt.GetNomTable(),
                            rel.GetNomTable() + "." +
                            CRelationElementAChamp_ChampCustom.c_champValeurString + "=@1 and " +
                            rel.GetNomTable() + "." +
                            CRelationElementAChamp_ChampCustom.c_champValeurInt + "=@2 and " +
                            rel.GetNomTable() + "." +
                            CChampCustom.c_champId + "=@3",
                            objet.GetType().ToString(),
                            ((CObjetDonneeAIdNumerique)objet).Id,
                            champ.Id);
                        foreach (CObjetDonnee fils in liste)
                            inventaire.AddObject(fils);
                    }
                }
            }
                    
        }
    }*/
}
