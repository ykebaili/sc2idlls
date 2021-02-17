using System;
using System.Collections;
using System.Data;

using sc2i.data;
using sc2i.data.dynamic;
using sc2i.data.serveur;
using sc2i.common;

namespace sc2i.data.dynamic.loader
{
    /// <summary>
    /// Description résumée de CEntiteOrganisationnelleServeur.
    /// </summary>
    public class CModuleParametrageServeur : CObjetHierarchiqueServeur
    {
        //-------------------------------------------------------------------
        public CModuleParametrageServeur(int nIdSession)
            : base(nIdSession)
        {
        }
        //-------------------------------------------------------------------
        public override string GetNomTable()
        {
            return CModuleParametrage.c_nomTable;
        }
        //-------------------------------------------------------------------
        public override CResultAErreur VerifieDonnees(CObjetDonnee objet)
        {
            CResultAErreur result = CResultAErreur.True;
            try
            {
                CModuleParametrage module = (CModuleParametrage)objet;

                if (module.Libelle == "")
                    result.EmpileErreur(I.T("The label of the Setting Module cannot be empty|10000"));
                
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
            return typeof(CModuleParametrage);
        }

    }
}
