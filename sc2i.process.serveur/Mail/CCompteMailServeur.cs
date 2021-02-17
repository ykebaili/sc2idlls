using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.data.serveur;
using sc2i.process.Mail;
using sc2i.common;
using sc2i.multitiers.client;
using System.Threading;
using sc2i.multitiers.server;
using sc2i.data;
using System.Collections;

namespace sc2i.process.serveur.Mail
{
    [AutoExec("Autoexec", AutoExecAttribute.BackGroundService)]
    public class CCompteMailServeur : CObjetServeurAvecBlob, ICompteMailServeur
    {
        private static int c_nDelaiEnvoi = 1000 * 30;//Toutes les 30 secondes
        private static Timer m_timer = null;
        private static CSessionClient m_sessionRecherche = null;

        public CCompteMailServeur(int nIdSession)
            :base(nIdSession)
        {

        }

        //-------------------------------------------------------------------
        public static void Autoexec()
        {
            m_timer = new Timer(new TimerCallback(OnRetrieveMails), null, c_nDelaiEnvoi, 1000 * 60);
            C2iEventLog.WriteInfo(I.T("Receive Mails Service started|10000"));
        }

        public override string GetNomTable()
        {
            return CCompteMail.c_nomTable;
        }

        public override CResultAErreur VerifieDonnees(sc2i.data.CObjetDonnee objet)
        {
            CResultAErreur result = CResultAErreur.True;
            try
            {
                CCompteMail compteMail = objet as CCompteMail;
                if (compteMail != null)
                {
                    if(string.IsNullOrEmpty(compteMail.Libelle))
                        result.EmpileErreur(I.T("Mail Account Lable cannot be empty|10007"));
                }
            }
            catch (Exception e)
            {
                result.EmpileErreur(e.Message);
            }


            return result;
        }

        public override Type GetTypeObjets()
        {
            return typeof(CCompteMail);
        }

        //-------------------------------------------------------------------------
        private static bool m_bTraitementEnCours = false;
        private static void OnRetrieveMails(object state)
        {
            if (m_bTraitementEnCours)
                return;
            m_bTraitementEnCours = true;

            try
            {
                System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Lowest;
                CResultAErreur result;
                if (m_sessionRecherche == null || !m_sessionRecherche.IsConnected)
                {
                    m_sessionRecherche = CSessionClient.CreateInstance();
                    result = m_sessionRecherche.OpenSession(new CAuthentificationSessionServer(),
                        I.T("MAIL ACCOUNT SERVICE|10001"),
                        ETypeApplicationCliente.Service);
                    if (!result)
                    {
                        C2iEventLog.WriteErreur(I.T("Session Opening error for Mail Account Management Service|10003"));
                        return;
                    }
                }
                try
                {
                    CFiltreData filtre = new CFiltreData(
                        CCompteMail.c_champIsActive + " = @1",
                        true);
                    if (new CCompteMailServeur(m_sessionRecherche.IdSession).CountRecords(
                        CCompteMail.c_nomTable, filtre) > 0)
                    {
                        CSessionClient sessionTravail = new CSessionProcessServeurSuivi();
                        result = sessionTravail.OpenSession(new CAuthentificationSessionServer(),
                            I.T("RECEIVE MAILS SERVICE|10004"),
                            ETypeApplicationCliente.Service);
                        if (!result)
                        {
                            C2iEventLog.WriteErreur(I.T("Working session openning error for Receive Mails Service|10005"));
                            return;
                        }
                        try
                        {
                            using (CContexteDonnee contexteTravail = new CContexteDonnee(sessionTravail.IdSession, true, false))
                            {
                                //CCompteMailServeur compteServeur = new CCompteMailServeur(sessionTravail.IdSession);
                                CListeObjetsDonnees liste = new CListeObjetsDonnees(contexteTravail, typeof(CCompteMail));
                                liste.Filtre = filtre;
                                ArrayList lstLock = liste.ToArrayList();

                                foreach (CCompteMail compteMail in lstLock)
                                {
                                    if (compteMail.DateDernierReleve == null ||
                                        compteMail.DateDernierReleve.Value.AddMinutes(compteMail.PeriodeReleve) < DateTime.Now)
                                    {
                                        result += compteMail.RetrieveMails();
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            C2iEventLog.WriteErreur(I.T("Error while receiving Mail : @1|10006", e.ToString()));
                        }
                        finally
                        {
                            try
                            {
                                sessionTravail.CloseSession();
                            }
                            catch { }
                        }
                    }
                }
                catch (Exception e)
                {
                    C2iEventLog.WriteErreur(I.T("Error while receiving Mail : @1|10006", e.ToString()));
                }
            }
            catch (Exception e)
            {
                {
                    C2iEventLog.WriteErreur(I.T("Error while receiving Mail : @1|10006", e.ToString()));
                }
            }
            finally
            {
                m_bTraitementEnCours = false;
            }
        }

        //----------------------------------------------------------------------------
        public CResultAErreur RetrieveMails(int nIdCompteMail)
        {
            CResultAErreur result = CResultAErreur.True;

            using (CContexteDonnee contexteDeReception = new CContexteDonnee(IdSession, true, false))
            {
                CCompteMail compteMail = new CCompteMail(contexteDeReception);
                if (compteMail.ReadIfExists(nIdCompteMail))
                {
                    CRecepteurMails recepteur = new CRecepteurMails();
                    result = compteMail.EnregistreEvenement(CCompteMail.c_strIdEvenementBeforeRetrieve, true);
                    if (result)
                    {
                        result = recepteur.RetrieveMails(compteMail);
                        if(result)
                            result = compteMail.EnregistreEvenement(CCompteMail.c_strIdEvenementAfterRetrieve, true);
                    }
                }
            }
            return result;
        }


    }
}
