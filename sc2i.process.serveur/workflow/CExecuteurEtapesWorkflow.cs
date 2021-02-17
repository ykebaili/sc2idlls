using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using sc2i.process.workflow;
using System.Threading;
using sc2i.data;

namespace sc2i.process.serveur.workflow
{
    public static class CExecuteurEtapesWorkflow
    {
        /*//-------------------------------------------------------
        private class CParametresEtapeAsynchrone
        {
            public readonly int IdSession;
            public readonly int IdEtape;

            public CParametresEtapeAsynchrone(int nIdSession, int nIdEtape)
            {
                IdEtape = nIdEtape;
                IdSession = nIdSession;
            }
        }

        //--------------------------------------------------------
        public static CResultAErreur ExecuteEtape(CEtapeWorkflow etape)
        {
            CResultAErreur result = CResultAErreur.True;
            if (etape.DateDebut == null ||
                etape.DateFin != null ||
                etape.Etat != (int)EEtatEtapeWorkflow.EnAttente)
            {
                result.EmpileErreur(I.T("Step @ 1 can not be executed, it does not check the conditions (Pending, non-zero start date and end date null)|20007"));
                return result;
            }

            Thread th = new Thread(StartEtape);
            th.Start ( new CParametresEtapeAsynchrone ( etape.ContexteDonnee.IdSession, etape.Id ));
            return result;
        }

        //--------------------------------------------------------
        private static void StartEtape(object parametreNonCast)
        {
            CParametresEtapeAsynchrone parametre = parametreNonCast as CParametresEtapeAsynchrone;
            if ( parametre == null )
                return;
            using (CContexteDonnee ctx = new CContexteDonnee(parametre.IdSession, true, false))
            {
                CEtapeWorkflow etape = new CEtapeWorkflow(ctx);
                if (!etape.ReadIfExists(parametre.IdEtape))
                    return;
                CResultAErreur result = etape.StartEtape();
            }

        }*/

    }
}
