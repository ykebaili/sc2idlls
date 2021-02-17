using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.data.serveur;
using sc2i.process.workflow;
using sc2i.common;

namespace sc2i.process.serveur.workflow
{
    public class CTypeEtapeWorkflowServeur : CObjetServeurAvecBlob
    {

        /// //////////////////////////////////////////////////
        public CTypeEtapeWorkflowServeur(int nIdSession)
			:base(nIdSession)
		{
		}

        /// //////////////////////////////////////////////////
        public override string GetNomTable()
        {
            return CTypeEtapeWorkflow.c_nomTable;
        }

        /// //////////////////////////////////////////////////
        public override CResultAErreur VerifieDonnees(sc2i.data.CObjetDonnee objet)
        {
            CResultAErreur result = CResultAErreur.True;
            CTypeEtapeWorkflow TypeEtapeWorkflow = objet as CTypeEtapeWorkflow;
            if ( TypeEtapeWorkflow != null )
            {
                if ( TypeEtapeWorkflow.Libelle.Length == 0 )
                {
                    result.EmpileErreur(I.T("Workflow step type label should be set|20004"));
                }
            }
            return result;
        }

        /// //////////////////////////////////////////////////
        public override Type GetTypeObjets()
        {
            return typeof(CTypeEtapeWorkflow);
        }
    }
}
