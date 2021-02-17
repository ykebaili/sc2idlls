using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.data;
using sc2i.common;
using sc2i.data.serveur;
using sc2i.process.Mail;

namespace sc2i.process.serveur.Mail
{
    public class CRegleMailServeur : CObjetServeur
    {
        public CRegleMailServeur(int nIdSession)
            :base(nIdSession)
        {

        }

        public override string GetNomTable()
        {
            return CRegleMail.c_nomTable;
        }

        public override CResultAErreur VerifieDonnees(CObjetDonnee objet)
        {
            CResultAErreur result = CResultAErreur.True;
            try
            {

            }
            catch (Exception e)
            {
                result.EmpileErreur(e.Message);
            }


            return result;
        }

        public override Type GetTypeObjets()
        {
            return typeof(CRegleMail);
        }
    }
}