using System;
using System.Collections;

using sc2i.data;
using sc2i.data.serveur;
using sc2i.data.dynamic;
using sc2i.common;
using sc2i.multitiers.client;
using sc2i.documents;

namespace sc2i.documents.serveur
{
    /// <summary>
    /// Description résumée de CCategorieGEDServeur.
    /// </summary>
    public class CCategorieGEDServeur : CObjetHierarchiqueServeur
    {

        //-------------------------------------------------------------------
        public CCategorieGEDServeur(int nIdSession)
            : base(nIdSession)
        {
        }
        //-------------------------------------------------------------------
        public override string GetNomTable()
        {
            return CCategorieGED.c_nomTable;
        }


        //-------------------------------------------------------------------
        public override CResultAErreur VerifieDonnees(CObjetDonnee objet)
        {
            CResultAErreur result = CResultAErreur.True;
            try
            {
                CCategorieGED cat = (CCategorieGED)objet;

                if (cat.Libelle == "")
                    result.EmpileErreur(I.T("The category label cannot be empty|105"));

				if (!CObjetDonneeAIdNumerique.IsUnique(cat, CCategorieGED.c_champLibelle, cat.Libelle))
                    result.EmpileErreur(I.T("Another category with this label already exists|106"));

            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
            }
            return result;
        }
        //-------------------------------------------------------------------
        public override Type GetTypeObjets()
        {
            return typeof(CCategorieGED);
        }
        //-------------------------------------------------------------------
    }
}
