using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.data.serveur;
using sc2i.process.workflow;
using sc2i.common;

namespace sc2i.process.serveur.workflow
{
    public class CLienEtapesWorkflowServeur : CObjetServeur
    {

        /// //////////////////////////////////////////////////
        public CLienEtapesWorkflowServeur(int nIdSession)
			:base(nIdSession)
		{
		}

        /// //////////////////////////////////////////////////
        public override string GetNomTable()
        {
            return CLienEtapesWorkflow.c_nomTable;
        }

        /// //////////////////////////////////////////////////
        public override CResultAErreur VerifieDonnees(sc2i.data.CObjetDonnee objet)
        {
            CResultAErreur result = CResultAErreur.True;
            CLienEtapesWorkflow lien = objet as CLienEtapesWorkflow;
            if ( lien != null )
            {
            }
            return result;
        }

        /// //////////////////////////////////////////////////
        public override Type GetTypeObjets()
        {
            return typeof(CLienEtapesWorkflow);
        }
    }
}
