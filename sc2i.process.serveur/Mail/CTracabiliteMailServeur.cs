using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.data.serveur;
using sc2i.process.Mail;
using sc2i.common;

namespace sc2i.process.serveur.Mail
{
    public class CTracabiliteMailServeur : CObjetServeur
    {
        public CTracabiliteMailServeur(int nIdSession)
            :base(nIdSession)
        {

        }

        public override string GetNomTable()
        {
            return CTracabiliteMail.c_nomTable;
        }

        public override CResultAErreur VerifieDonnees(sc2i.data.CObjetDonnee objet)
        {
            CResultAErreur result = CResultAErreur.True;

            try
            {
                CTracabiliteMail mail = objet as CTracabiliteMail;

            }
            catch (Exception e)
            {
                result.EmpileErreur(e.Message);
            }

            return result;
        }

        public override Type GetTypeObjets()
        {
            return typeof(CTracabiliteMail);
        }
    }
}
