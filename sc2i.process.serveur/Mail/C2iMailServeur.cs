using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.data.serveur;
using sc2i.process.Mail;
using sc2i.common;
using System.Data;
using System.Collections;
using sc2i.data;

namespace sc2i.process.serveur.Mail
{
    public class C2iMailServeur : CObjetServeur, I2iMailServeur
    {
        public C2iMailServeur(int nIdSession)
            :base(nIdSession)
        {

        }

        public override string GetNomTable()
        {
            return C2iMail.c_nomTable;
        }

        public override CResultAErreur VerifieDonnees(sc2i.data.CObjetDonnee objet)
        {
            CResultAErreur result = CResultAErreur.True;

            try
            {
                C2iMail mail = objet as C2iMail;

            }
            catch (Exception e)
            {
                result.EmpileErreur(e.Message);
            }

            return result;
        }

        public override Type GetTypeObjets()
        {
            return typeof(C2iMail);
        }

        public override CResultAErreur TraitementAvantSauvegarde(sc2i.data.CContexteDonnee contexte)
        {
            CResultAErreur result = base.TraitementAvantSauvegarde(contexte);
            if (!result)
                return result;
            DataTable table = contexte.Tables[GetNomTable()];
            if (table == null)
                return result;
            ArrayList lst = new ArrayList(table.Rows);
            foreach (DataRow row in new ArrayList(table.Rows))
            {
                if (row.RowState == DataRowState.Added)
                {
                    C2iMail mail = new C2iMail(row);
                    CTracabiliteMail traceMail = new CTracabiliteMail(contexte);
                    if (!traceMail.ReadIfExists(new CFiltreData(
                        CTracabiliteMail.c_champMessageUid + " = @1", mail.MessageId)))
                    {
                        traceMail.CreateNewInCurrentContexte();
                        traceMail.MessageUid = mail.MessageId;
                        traceMail.DateReception = mail.DateReception;
                        traceMail.CompteMail = mail.CompteMail;
                    }
                }
            }

            return result;
        }

        public CResultAErreur RetrieveMessageComplet(int nIdMail)
        {
            CResultAErreur result = CResultAErreur.True;
            using (CContexteDonnee contexteDeReception = new CContexteDonnee(IdSession, true, false))
            {
                C2iMail mail = new C2iMail(contexteDeReception);
                if (mail.ReadIfExists(nIdMail))
                {
                    CRecepteurMails recepteur = new CRecepteurMails();
                    result = recepteur.RetrieveMail(mail);
                }
            }
            return result;
        }

    }
}
