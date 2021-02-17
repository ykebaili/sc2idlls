using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace sc2i.process.workflow
{

    public enum ETypeActionExterneOnWorkflowStep
    {
        End=0,
        Cancel
    }

    public class CTypeActionExterneOnWorkflowStep : CEnumALibelle<ETypeActionExterneOnWorkflowStep>
    {
        public CTypeActionExterneOnWorkflowStep(ETypeActionExterneOnWorkflowStep code)
            : base(code)
        {
        }

        [DynamicField("Label")]
        public override string Libelle
        {
            get
            {
                switch (Code)
                {
                    case ETypeActionExterneOnWorkflowStep.End:
                        return I.T("End step|20103");
                        break;
                    case ETypeActionExterneOnWorkflowStep.Cancel:
                        return I.T("Cancel step|20104");
                        break;

                }
                return "?";
            }
        }
    }
}
