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
    public class CFournisseurInventaireRelationTypeId : IFournisseurInventaireObjetDonneeLies
    {
        public static void Autoexec()
        {
            CFournisseurInventaireObjetDonneeLies.RegisterFournisseur(new CFournisseurInventaireRelationTypeId());
        }

        public void FillInventaireNonRecursif(CObjetDonnee objet, sc2i.common.inventaire.CInventaire inventaire, CFournisseurInventaireObjetDonneeLies.EModeInventaireObjetDonneeLies mode)
        {
            if ((mode & CFournisseurInventaireObjetDonneeLies.EModeInventaireObjetDonneeLies.TousLesFils) != CFournisseurInventaireObjetDonneeLies.EModeInventaireObjetDonneeLies.Aucuns)
            {
                CObjetDonneeAIdNumerique objId = objet as CObjetDonneeAIdNumerique;
                if (objId != null)
                {
                    if (objId.GetType().GetCustomAttributes(typeof(NoRelationTypeIdAttribute), true).Length == 0)
                    {
                        foreach (RelationTypeIdAttribute rel in CContexteDonnee.RelationsTypeIds)
                        {
                            if (rel.Composition && (mode & CFournisseurInventaireObjetDonneeLies.EModeInventaireObjetDonneeLies.FilsCompositions) == CFournisseurInventaireObjetDonneeLies.EModeInventaireObjetDonneeLies.FilsCompositions ||
                                !rel.Composition && (mode & CFournisseurInventaireObjetDonneeLies.EModeInventaireObjetDonneeLies.FilsNonCompositions) == CFournisseurInventaireObjetDonneeLies.EModeInventaireObjetDonneeLies.FilsNonCompositions)
                            {
                                if (rel.NomConvivialPourParent != "" && rel.IsAppliqueToType(objId.GetType()))
                                {
                                    foreach (CObjetDonnee obj in objId.GetDependancesRelationTypeId(
                                        rel.TableFille,
                                        rel.ChampType,
                                        rel.ChampId,
                                        false))
                                        inventaire.AddObject(obj);
                                }
                            }
                        }
                    }
                }
            }
                    
        }
    }*/
}
