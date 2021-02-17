using System;

using sc2i.common;
using sc2i.data;
using sc2i.data.serveur;
using sc2i.data.dynamic;

namespace sc2i.documents.serveur
{
    /// <summary>
    /// Description résumée de CRelationDocumentGED_CategorieServeur.
    /// </summary>
    public class CRelationDocumentGED_CategorieServeur : CObjetServeur
    {
        /// //////////////////////////////////////////////////
        public CRelationDocumentGED_CategorieServeur(int nIdSession)
            : base(nIdSession)
        {

        }

        //////////////////////////////////////////////////////////////////////
        public override string GetNomTable()
        {
            return CRelationDocumentGED_Categorie.c_nomTable;
        }

        //////////////////////////////////////////////////////////////////////
        public override Type GetTypeObjets()
        {
            return typeof(CRelationDocumentGED_Categorie);
        }

        //////////////////////////////////////////////////////////////////////
        public override CResultAErreur VerifieDonnees(CObjetDonnee objet)
        {
            CResultAErreur result = CResultAErreur.True;
            try
            {
                CRelationDocumentGED_Categorie rel = (CRelationDocumentGED_Categorie)objet;

                return result;
            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
            }
            return result;

        }


    }
}
