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
    public class CChercheurRelationsTypeId : IFournisseurDependancesObjetDonnee
    {
        //----------------------------------------------------------------------------
        public static void Autoexec()
        {
            CEntitiesManager.RegisterFournisseurDependances(typeof(CChercheurRelationsTypeId));
        }

        //----------------------------------------------------------------------------
        public List<CReferenceObjetDependant> GetDependances(CEntitiesManager manager, CObjetDonnee objet)
        {
            List<CReferenceObjetDependant> lst = new List<CReferenceObjetDependant>();
            if (objet == null)
                return lst;
            if (!(objet is CObjetDonneeAIdNumerique))
                return lst;
            foreach ( RelationTypeIdAttribute rt in  CContexteDonnee.RelationsTypeIds )
            {
                if ( rt.IsAppliqueToType(objet.GetType()) && objet.GetType().GetCustomAttribute<NoRelationTypeIdAttribute>() == null)
                {
                    Type tpRelId = CContexteDonnee.GetTypeForTable(rt.TableFille);
                    if ( tpRelId != null && !manager.ConfigurationRecherche.IsIgnore(tpRelId))
                    {
                        CListeObjetsDonnees lstRT = new CListeObjetsDonnees(objet.ContexteDonnee, tpRelId);
                        lstRT.Filtre = new CFiltreData ( rt.ChampType +"=@1 and "+
                            rt.ChampId+"=@2",
                            objet.GetType().ToString(),
                            ((CObjetDonneeAIdNumerique)objet).Id );
                        foreach ( CObjetDonnee objetRT in lstRT )
                        {
                            lst.Add(new CReferenceObjetDependant(rt.NomConvivialPourParent, objetRT));
                        }
                    }
                }
            }
            return lst;
        }
    }
}
