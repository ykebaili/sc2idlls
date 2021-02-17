using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.data.serveur;
using sc2i.process.workflow;
using sc2i.common;

namespace sc2i.process.serveur.workflow
{
    public class CTypeWorkflowServeur : CObjetServeurAvecBlob
    {

        /// //////////////////////////////////////////////////
        public CTypeWorkflowServeur(int nIdSession)
			:base(nIdSession)
		{
		}

        /// //////////////////////////////////////////////////
        public override string GetNomTable()
        {
            return CTypeWorkflow.c_nomTable;
        }

        /// //////////////////////////////////////////////////
        public override CResultAErreur VerifieDonnees(sc2i.data.CObjetDonnee objet)
        {
            CResultAErreur result = CResultAErreur.True;
            CTypeWorkflow typeWorkflow = objet as CTypeWorkflow;
            if ( typeWorkflow != null )
            {
                if ( typeWorkflow.Libelle.Length == 0 )
                {
                    result.EmpileErreur(I.T("Workflow type label should be set|20003"));
                }
                if (typeWorkflow.Etapes.Count > 0 && typeWorkflow.EtapeDemarrageDefaut == null)
                {
                    result.EmpileErreur(I.T("You have to select a start step for this workflow|20009"));
                }

            }
            return result;
        }

        /// //////////////////////////////////////////////////
        public override Type GetTypeObjets()
        {
            return typeof(CTypeWorkflow);
        }
    }
}
