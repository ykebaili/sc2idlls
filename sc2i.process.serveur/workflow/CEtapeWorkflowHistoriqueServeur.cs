using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.data.serveur;
using sc2i.common;
using sc2i.process.workflow;
using System.Data;
using System.Collections;
using sc2i.multitiers.client;

namespace sc2i.process.serveur.EtapeWorkflow
{
    public class CEtapeWorkflowHistoriqueServeur : CObjetServeur
    {

        /// //////////////////////////////////////////////////
        public CEtapeWorkflowHistoriqueServeur(int nIdSession)
			:base(nIdSession)
		{
		}

        /// //////////////////////////////////////////////////
        public override string GetNomTable()
        {
            return CEtapeWorkflowHistorique.c_nomTable;
        }

        /// //////////////////////////////////////////////////
        public override CResultAErreur VerifieDonnees(sc2i.data.CObjetDonnee objet)
        {
            CResultAErreur result = CResultAErreur.True;
            CEtapeWorkflowHistorique etapeWorkflow = objet as CEtapeWorkflowHistorique;
            if ( etapeWorkflow != null )
            {
                
            }
            return result;
        }

        /// //////////////////////////////////////////////////
        public override Type GetTypeObjets()
        {
            return typeof(CEtapeWorkflowHistorique);
        }

        
    }
}
