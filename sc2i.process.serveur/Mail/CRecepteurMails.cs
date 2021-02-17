using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.process.Mail;
using sc2i.common;
using sc2i.multitiers.server;

using sc2i.data;
using OpenPop.Pop3;
using OpenPop.Mime;
using System.Net.Mail;
using OpenPop.Mime.Header;

namespace sc2i.process.serveur.Mail
{
    public class CRecepteurMails : C2iObjetServeur
    {
        CCompteMail m_compteMail = null;

        public CRecepteurMails(int nIdSession)
            : base(nIdSession)
        {

        }

        public CRecepteurMails()
        {
        }

        /// <summary>
        /// Récupère les nouveaux Messaegs du serveur de Mails POP3 dont les informations de
        /// connextion sont indiqués dans le paramètre
        /// </summary>
        /// <param name="parametre">Parametres de connexion au serveur de mails POP3</param>
        /// <returns>Le Data du Result contient un liste de MailMessageEx</returns>
        public CResultAErreur RetrieveMails(CCompteMail compteMail)
        {
            CResultAErreur result = CResultAErreur.True;
            if (compteMail == null)
            {
                result.EmpileErreur("Erreur dans CRecepteurMail: compteMail est nul");
            }

            m_compteMail = compteMail;
            m_compteMail.BeginEdit();
            m_compteMail.DateDernierReleve = DateTime.Now;

            CParametreCompteMail parametre = compteMail.ParametreCompteMail;
            if (parametre == null)
            {
                result.EmpileErreur("Erreur dans CRecepteurMail: Le ¨Parametre de compte est nul");
                return result;
            }
            using (Pop3Client client = new Pop3Client())
            {
                try
                {
                    client.Connect(parametre.PopServer, parametre.PopPort, parametre.UseSsl);
                    // Authenticate ourselves towards the server
                    client.Authenticate(parametre.User, parametre.Password);
                    // Get the number of messages in the inbox
                    int messageCount = client.GetMessageCount();

                    // Messages are numbered in the interval: [1, messageCount]
                    // Ergo: message numbers are 1-based.
                    for (int i = 1; i <= messageCount; i++)
                    {
                        MessageHeader messageHead = client.GetMessageHeaders(i);
                        Message messageRecu = null;
                        CTracabiliteMail traceMail = new CTracabiliteMail(m_compteMail.ContexteDonnee);
                        CFiltreData filtreIfExist = new CFiltreData(
                            CTracabiliteMail.c_champMessageUid + " = @1 and " +
                            CCompteMail.c_champId + " = @2 ",
                            messageHead.MessageId,
                            compteMail.Id);
                        if (!traceMail.ReadIfExists(filtreIfExist))
                        {
                            try
                            {
                                C2iMail mail = new C2iMail(compteMail.ContexteDonnee);
                                mail.CreateNewInCurrentContexte();
                                mail.CompteMail = m_compteMail;
                                if (parametre.HeaderOnly)
                                {
                                    mail.FillFromHeader(messageHead);
                                }
                                else
                                {
                                    messageRecu = client.GetMessage(i);
                                    mail.FillFromMessage(messageRecu);
                                }
                            }
                            catch (Exception ex)
                            {
                                result.EmpileErreur(ex.Message);
                            }
                        }
                        if (parametre.SupprimerDuServeur)
                        {
                            if (parametre.DelaiSuppression <= 0)
                                client.DeleteMessage(i);
                            else
                            {
                                DateTime dateMessage = messageHead.DateSent;
                                TimeSpan dureeDeVie = DateTime.Now - dateMessage;
                                if (dureeDeVie.Days >= parametre.DelaiSuppression)
                                    client.DeleteMessage(i);
                            }
                        }

                    }
                }
                catch (Exception exception)
                {
                    result.EmpileErreur(exception.Message);
                }
                finally
                {
                    client.Disconnect();
                }
            }

            if (!result)
                m_compteMail.LastErreur = result.MessageErreur;
            else
                m_compteMail.LastErreur = I.T("Mails succesfully retrived|10000");

            result += m_compteMail.CommitEdit();

            return result;
        }

        //-------------------------------------------------------------------
        public CResultAErreur RetrieveMail(C2iMail mail)
        {
            CResultAErreur result = CResultAErreur.True;
            if (mail.CompteMail == null)
            {
                result.EmpileErreur("Erreur dans CRecepteurMail: mail est nul");
            }
            CParametreCompteMail parametre = mail.CompteMail.ParametreCompteMail;
            if (parametre == null)
            {
                result.EmpileErreur("Erreur dans CRecepteurMail: Le ¨Parametre de compte est nul");
                return result;
            }
            mail.BeginEdit();

            using (Pop3Client client = new Pop3Client())
            {

                try
                {
                    client.Connect(parametre.PopServer, parametre.PopPort, parametre.UseSsl);
                    // Authenticate ourselves towards the server
                    client.Authenticate(parametre.User, parametre.Password);
                    // Get the number of messages in the inbox
                    int messageCount = client.GetMessageCount();

                    // Messages are numbered in the interval: [1, messageCount]
                    // Ergo: message numbers are 1-based.
                    string strIdRecherche = mail.MessageId;
                    for (int i = 1; i <= messageCount; i++)
                    {
                        MessageHeader messageHead = client.GetMessageHeaders(i);
                        if (messageHead.MessageId == strIdRecherche)
                        // Bingo ! C'est lui que je cherche
                        {
                            Message messageComplet = client.GetMessage(i);
                            mail.FillFromMessage(messageComplet);
                        }
                    }
                }
                catch (Exception exception)
                {
                    result.EmpileErreur(exception.Message);
                }
                finally
                {
                    client.Disconnect();
                }
            }

            result = mail.CommitEdit();

            return result;
        }

    }
}
