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
    /// <summary>
    /// Description résumée de CCauseGelServeur.
    /// </summary>
    public class CCauseGelServeur : CObjetDonneeServeurAvecCache
    {
        //-------------------------------------------------------------------
        public CCauseGelServeur(int nIdSession)
            : base(nIdSession)
        {
        }

        //-------------------------------------------------------------------
        public override string GetNomTable()
        {
            return CCauseGel.c_nomTable;
        }

        //-------------------------------------------------------------------
        public override CResultAErreur VerifieDonnees(CObjetDonnee objet)
        {
            CResultAErreur result = CResultAErreur.True;
            try
            {
                CCauseGel cause = (CCauseGel)objet;

                if (cause.Libelle == "")
                    result.EmpileErreur(I.T("The label of the freezing cause cannot be empty|10000"));
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
            return typeof(CCauseGel);
        }

    }
}
