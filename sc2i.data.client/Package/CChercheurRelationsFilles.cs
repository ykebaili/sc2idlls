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
    public class CChercheurRelationsFilles : IFournisseurDependancesObjetDonnee
    {
        //----------------------------------------------------------------------------
        public static void Autoexec()
        {
            CEntitiesManager.RegisterFournisseurDependances(typeof(CChercheurRelationsFilles));
        }

        //----------------------------------------------------------------------------
        public List<CReferenceObjetDependant> GetDependances(CEntitiesManager manager, CObjetDonnee objet)
        {
            List<CReferenceObjetDependant> lst = new List<CReferenceObjetDependant>();
            if (objet == null)
                return lst;
            CStructureTable structure = CStructureTable.GetStructure(objet.GetType());
            foreach (CInfoRelation relation in structure.RelationsFilles)
            {
                Type tpFille = CContexteDonnee.GetTypeForTable(relation.TableFille);
                if (manager.ConfigurationRecherche.IsIgnore(tpFille))
                    return lst;
                C2iRequeteAvancee requete = new C2iRequeteAvancee();
                requete.FiltreAAppliquer = CFiltreData.CreateFiltreAndSurValeurs(relation.ChampsFille, objet.GetValeursCles());
                requete.TableInterrogee = relation.TableFille;
                bool bHasUniversalId = tpFille.GetCustomAttribute<NoIdUniverselAttribute>(true) == null;
                if (bHasUniversalId)
                    requete.ListeChamps.Add(new C2iChampDeRequete(CObjetDonnee.c_champIdUniversel,
                        new CSourceDeChampDeRequete(CObjetDonnee.c_champIdUniversel),
                        typeof(string),
                        OperationsAgregation.None,
                        true));
                else
                {
                    CStructureTable sFille = CStructureTable.GetStructure(tpFille);
                    foreach (CInfoChampTable infoChamp in sFille.ChampsId)
                        requete.ListeChamps.Add(new C2iChampDeRequete(infoChamp.NomChamp,
                            new CSourceDeChampDeRequete(infoChamp.NomChamp),
                            infoChamp.TypeDonnee,
                            OperationsAgregation.None,
                            true));
                }
                string strNomProp = relation.NomConvivial;
                if (strNomProp.Length == 0)
                    strNomProp = "List of " + DynamicClassAttribute.GetNomConvivial(tpFille);
                CResultAErreur result = requete.ExecuteRequete(objet.ContexteDonnee.IdSession);
                if (result && result.Data is DataTable)
                {
                    DataTable table = result.Data as DataTable;
                    foreach (DataRow row in table.Rows)
                    {
                        if (bHasUniversalId)
                            lst.Add(new CReferenceObjetDependant(strNomProp, tpFille, CDbKey.GetNewDbKeyOnUniversalIdANePasUtiliserEnDehorsDeCDbKeyAddOn((string)row[0])));
                        else
                        {
                            lst.Add(new CReferenceObjetDependant(strNomProp, tpFille,
                                row.ItemArray));
                        }
                    }

                }
            }
            return lst;
        }
    }
}
