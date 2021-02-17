using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.common;

using sc2i.win32.common;
using System.Net.Mail;
using System.IO;
using sc2i.process.Mail;


namespace sc2i.test.metier
{
    public partial class CFormTestClientMailPop3 : Form
    {
        public CFormTestClientMailPop3()
        {
            InitializeComponent();
        }

        //List<Message> m_listeMessages = new List<MailMessageEx>();

        private void m_btnRetrieveList_Click(object sender, EventArgs e)
        {
            FillListMails(CreateParam());
           
        }

        private void FillListMails(CParametreCompteMail parametre)
        {
            CResultAErreur result = CResultAErreur.True;
            /*
            try
            {
                using (Pop3Client client = new Pop3Client(
                    parametre.PopServer,
                    parametre.PopPort,
                    parametre.UseSsl,
                    parametre.User,
                    parametre.Password))
                {
                    client.Trace += new Action<string>(Console.WriteLine);

                    //connects to Pop3 Server, Executes POP3 USER and PASS
                    client.Authenticate();
                    //
                    Stat statistiques = client.Stat();
                    m_glListeMails.ListeSource = null;
                    List<Pop3ListItem> listMAils = client.List();

                    if(listMAils.Count == 0)
                        MessageBox.Show("Vous n'avez pas de nouveaux messages");

                    foreach (Pop3ListItem item in listMAils)
                    {
                        try
                        {
                            MailMessageEx message = client.RetrMailMessageEx(item);
                            //MailMessageEx message = client.Top(item.MessageId, 10);

                            Console.WriteLine("Children.Count: {0}", message.Children.Count);
                            Console.WriteLine("message-id: {0}", message.MessageId);
                            Console.WriteLine("subject: {0}", message.Subject);
                            Console.WriteLine("Attachments.Count: {0}", message.Attachments.Count);
                            
                            m_listeMessages.Add(message);
                        }
                        catch (Exception e)
                        {
                            result.EmpileErreur(e.Message);
                            Console.WriteLine(e.Message);
                        }
                        
                    }
                    m_glListeMails.ListeSource = m_listeMessages;

                    client.Noop();
                    client.Rset();
                    client.Quit();
                }

                
            }
            catch (Exception ex)
            {
                result.EmpileErreur(ex.Message);
                CFormAlerte.Afficher(result.Erreur);
            }*/
        }

        private CParametreCompteMail CreateParam()
        {
            CParametreCompteMail param = new CParametreCompteMail();

            param.PopServer = m_txtServeur.Text;
            param.PopPort = (int)m_numPort.Value;
            param.User = m_txtUser.Text;
            param.Password = m_txtPass.Text;
            param.UseSsl = m_chkSSL.Checked;

            return param;
        }

        private void m_glListeMails_DoubleClick(object sender, EventArgs e)
        {
            /*MailMessageEx message = m_glListeMails.SelectedItems[0] as MailMessageEx;
            if (message != null)
            {
                byte[] octets = message.BodyEncoding.GetBytes(message.Body);
                string strBody = Encoding.UTF8.GetString(octets);
                m_webBrowser.DocumentText = strBody;
                m_txtBody.Text = strBody;

                m_lblFrom.Text = message.From.Address.ToString();

                m_lblTo.Text = "";
                foreach (MailAddress to in message.To)
                {
                    m_lblTo.Text += to.ToString() + "; ";
                }
                m_lblDate.Text = message.DeliveryDate.ToString("dd/MM/yyyy HH:mm");

                if (message.Attachments.Count > 0)
                {
                    Attachment pj = message.Attachments[0];
                    m_lnkPièceJointe.Text = pj.Name;
                    m_lnkPièceJointe.Tag = pj;
                }
                else
                    m_lnkPièceJointe.Text = "";

            }*/
        }

        private void m_lnkPièceJointe_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Attachment pj = m_lnkPièceJointe.Tag as Attachment;
            if (pj != null)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.RestoreDirectory = true;
                dlg.FileName = pj.Name;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string strChemin = dlg.FileName;
                    Stream fluxPJ = pj.ContentStream;
                    BinaryReader reader = new BinaryReader(fluxPJ);
                    Stream fluxFichier = File.Open(strChemin, FileMode.Create);
                    BinaryWriter binWriter = new BinaryWriter(fluxFichier);
                    try
                    {
                        int nSizeBuffer = 1024 * 5;
                        byte[] buffer = new byte[nSizeBuffer];
                        int nRead = 0;
                        while ((nRead = reader.Read(buffer, 0, nSizeBuffer)) > 0)
                        {
                            binWriter.Write(buffer, 0, nRead);
                        }
                    }
                    catch
                    {}
                    finally
                    {
                        reader.Close();
                        binWriter.Close();
                        fluxPJ.Close();
                        fluxFichier.Close();
                    }

                }
            }
        }

        private void CFormTestClientMailPop3_Load(object sender, EventArgs e)
        {
            m_lnkPièceJointe.Text = "";
        }


    }
}
