using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.data.serveur;
using sc2i.process.Mail;
using sc2i.common;

namespace sc2i.process.serveur.Mail
{
    public class CDossierMailServeur : CObjetHierarchiqueServeur
    {
        public CDossierMailServeur(int nIdSession)
            :base(nIdSession)
        {

        }

        public override string GetNomTable()
        {
            return CDossierMail.c_nomTable;
        }

        public override CResultAErreur VerifieDonnees(sc2i.data.CObjetDonnee objet)
        {
            CResultAErreur result = CResultAErreur.True;
            try
            {
                CDossierMail dossier = objet as CDossierMail;


            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
            }
            return result;
        }

        public override Type GetTypeObjets()
        {
            return typeof(CDossierMail);
        }
    }
}
