using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace sc2i.process.workflow
{
    [Flags]
    public enum EModeGestionErreurEtapeWorkflow
    {
        DoNothing = 1,
        EndStep = 2,
        CancelStep = 4,
        SetError = 8
    }

    public class CModeGestionErreurEtapeWorkflow : CEnumALibelle<EModeGestionErreurEtapeWorkflow>
    {
        public CModeGestionErreurEtapeWorkflow(EModeGestionErreurEtapeWorkflow mode)
            :base(mode)
        {
        }


        /// <summary>
        /// Retourne vrai s'il n'y a qu'un mode possible dans la valeur du mode
        /// (pas de combinaison de flags)
        /// </summary>
        /// <returns></returns>
        public bool IsSingleChoice()
        {
            return Code == EModeGestionErreurEtapeWorkflow.SetError ||
                Code == EModeGestionErreurEtapeWorkflow.EndStep ||
                Code == EModeGestionErreurEtapeWorkflow.DoNothing ||
                Code == EModeGestionErreurEtapeWorkflow.CancelStep;
        }

        public override string Libelle
        {
            get
            {
                StringBuilder bl = new StringBuilder();
                if ((Code & EModeGestionErreurEtapeWorkflow.DoNothing) != 0)
                {
                    bl.Append(I.T("Do nothing|20097"));
                    bl.Append(", ");
                }
                if ((Code & EModeGestionErreurEtapeWorkflow.EndStep) != 0)
                {
                    bl.Append(I.T("End step|20094"));
                    bl.Append(", ");
                }
                if ((Code & EModeGestionErreurEtapeWorkflow.CancelStep) != 0)
                {
                    bl.Append(I.T("Cancel step|20095"));
                    bl.Append(", ");
                }
                if ((Code & EModeGestionErreurEtapeWorkflow.SetError) != 0)
                {
                    bl.Append(I.T("Store error|20096"));
                    bl.Append(", ");
                }
                if (bl.Length > 0)
                    bl.Remove(bl.Length - 2, 2);
                else
                    bl.Append(I.T("Do noting|20097"));
                return bl.ToString();                
            }
        }
    }
}
