using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.data.serveur;
using sc2i.process.workflow;
using sc2i.common;
using System.Data;
using System.Collections;

namespace sc2i.process.serveur.workflow
{
    public class CWorkflowServeur : CObjetServeur
    {

        /// //////////////////////////////////////////////////
        public CWorkflowServeur(int nIdSession)
			:base(nIdSession)
		{
		}

        /// //////////////////////////////////////////////////
        public override string GetNomTable()
        {
            return CWorkflow.c_nomTable;
        }

        /// //////////////////////////////////////////////////
        public override CResultAErreur VerifieDonnees(sc2i.data.CObjetDonnee objet)
        {
            CResultAErreur result = CResultAErreur.True;
            CWorkflow workflow = objet as CWorkflow;
            if ( workflow != null )
            {
                if (workflow.TypeWorkflow == null)
                    result.EmpileErreur(I.T("Select a workflow type for the workflow @1|20005", workflow.Libelle));
                
            }
            return result;
        }

        /// //////////////////////////////////////////////////
        public override Type GetTypeObjets()
        {
            return typeof(CWorkflow);
        }

        /*/// //////////////////////////////////////////////////
        public override CResultAErreur TraitementAvantSauvegarde(sc2i.data.CContexteDonnee contexte)
        {
            CResultAErreur result =  base.TraitementAvantSauvegarde(contexte);
            if (!result)
                return result;
            DataTable table = contexte.Tables[GetNomTable()];
            if (table == null)
                return result;
            ArrayList lstRows = new ArrayList(table.Rows);
            foreach (DataRow row in lstRows)
            {
                if (row.RowState == DataRowState.Modified)
                {
                    bool bOldRunning = (bool)row[CWorkflow.c_champIsRunning, DataRowVersion.Original];
                    bool bNewRunning = (bool)row[CWorkflow.c_champIsRunning];
                    if (bOldRunning && !bNewRunning)
                    {
                        //Le workflow vient de s'arrêter
                        CWorkflow workflow = new CWorkflow(row);
                        CEtapeWorkflow etapeAppelante = workflow.EtapeAppelante;
                        if (etapeAppelante != null && etapeAppelante.EtatCode == (int)EEtatEtapeWorkflow.Démarrée)
                        {
                            etapeAppelante.InternalSetInfosTerminéeInCurrentContexte();
                        }
                    }
                }
            }
            return result;
        }*/
    }
}
