using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using sc2i.common;
using System.Data;

namespace sc2i.data.Package
{
    [AutoExec("Autoexec")]
    public class CChercheurRelationsParentes : IFournisseurDependancesObjetDonnee
    {
        //----------------------------------------------------------------------------
        public static void Autoexec()
        {
            CEntitiesManager.RegisterFournisseurDependances(typeof(CChercheurRelationsParentes));
        }

        //----------------------------------------------------------------------------
        public List<CReferenceObjetDependant> GetDependances(CEntitiesManager manager, CObjetDonnee objet)
        {
            List<CReferenceObjetDependant> lst = new List<CReferenceObjetDependant>();
            if (objet == null)
                return lst;
            CStructureTable structure = CStructureTable.GetStructure(objet.GetType());
            foreach (CInfoRelation relation in structure.RelationsParentes)
            {
                Type tpParent = CContexteDonnee.GetTypeForTable(relation.TableParente);
                if (manager.ConfigurationRecherche.IsIgnore(tpParent))
                    return lst;
                CObjetDonnee parent = objet.GetParent(relation.ChampsFille, tpParent);
                bool bHasUniversalId = tpParent.GetCustomAttribute<NoIdUniverselAttribute>(true) == null;
                string strNomProp = relation.NomConvivial;
                if (strNomProp.Length == 0)
                    strNomProp = DynamicClassAttribute.GetNomConvivial(tpParent);
                if (parent != null)
                {
                    if (bHasUniversalId)
                        lst.Add(new CReferenceObjetDependant(strNomProp, tpParent, parent.DbKey));
                    else
                    {
                        lst.Add(new CReferenceObjetDependant(strNomProp, tpParent,
                            parent.GetValeursCles()));
                    }

                }
            }
            return lst;
        }
    }
}
