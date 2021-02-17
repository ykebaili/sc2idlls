using sc2i.common;
using sc2i.data;
using sc2i.data.serveur;
using sc2i.process.workflow.gels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.process.serveur.workflow.gels
{
    public class CGelEtapeWorkflowServeur : CObjetDonneeServeurAvecCache
    {

        //-------------------------------------------------------------------
        public CGelEtapeWorkflowServeur(int nIdSession)
            : base(nIdSession)
        {
        }

        //-------------------------------------------------------------------
        public override string GetNomTable()
        {
            return CGelEtapeWorkflow.c_nomTable;
        }

        //-------------------------------------------------------------------
        public override CResultAErreur VerifieDonnees(CObjetDonnee objet)
        {
            CResultAErreur result = CResultAErreur.True;
            try
            {
                CGelEtapeWorkflow gel = (CGelEtapeWorkflow)objet;

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
            return typeof(CGelEtapeWorkflow);
        }
    }
}
