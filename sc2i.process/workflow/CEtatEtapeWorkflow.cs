using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace sc2i.process.workflow
{

    public enum EEtatEtapeWorkflow
    {
        EnAttente = 0,
        ADemarrer,
        Démarrée,
        Terminée,
        Erreur,
        Annulée
    }

    [Serializable]
    public class CEtatEtapeWorkflow : CEnumALibelle<EEtatEtapeWorkflow>
    {
        //----------------------------------------------------------
        public CEtatEtapeWorkflow(EEtatEtapeWorkflow code)
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
                    case EEtatEtapeWorkflow.EnAttente:
                        return I.T("Waiting|20079");
                    case EEtatEtapeWorkflow.ADemarrer:
                        return I.T("Starting|20085");
                    case EEtatEtapeWorkflow.Démarrée:
                        return I.T("Started|20080");
                    case EEtatEtapeWorkflow.Terminée:
                        return I.T("Ended|20081");
                    case EEtatEtapeWorkflow.Erreur:
                        return I.T("Error|20089");
                    case EEtatEtapeWorkflow.Annulée:
                        return I.T("Canceled|20093");
                }
                return "";
            }
        }


    }
}
