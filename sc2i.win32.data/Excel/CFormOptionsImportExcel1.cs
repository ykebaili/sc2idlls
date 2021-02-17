using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

using sc2i.data;
using sc2i.data.Excel;
using sc2i.common;
using sc2i.win32.common;
using CarlosAg.ExcelXmlWriter;
using System.Text.RegularExpressions;
using System.Data;

namespace sc2i.win32.data
{
	public class CFormOptionsImportExcel1 : System.Windows.Forms.Form
	{
		#region Code généré par le Concepteur Windows Form

        private System.Windows.Forms.Button m_btnAnnuler;
		private System.Windows.Forms.Button m_btnSuivant;
        private System.Windows.Forms.CheckBox m_chkNomChampsSurPremiereLigne;
        private Label label1;
        private ListBox m_listeFeuilles;
        private Label label2;
        private C2iTextBox m_txtPlageDonnees;
        private ErrorProvider m_errorProvider;
        private Label label3;
        private DataGridView m_grilleApercu;
        private IContainer components;



		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.m_chkNomChampsSurPremiereLigne = new System.Windows.Forms.CheckBox();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnSuivant = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.m_listeFeuilles = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_txtPlageDonnees = new sc2i.win32.common.C2iTextBox();
            this.m_errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.m_grilleApercu = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_grilleApercu)).BeginInit();
            this.SuspendLayout();
            // 
            // m_chkNomChampsSurPremiereLigne
            // 
            this.m_chkNomChampsSurPremiereLigne.Location = new System.Drawing.Point(316, 29);
            this.m_chkNomChampsSurPremiereLigne.Name = "m_chkNomChampsSurPremiereLigne";
            this.m_chkNomChampsSurPremiereLigne.Size = new System.Drawing.Size(244, 24);
            this.m_chkNomChampsSurPremiereLigne.TabIndex = 0;
            this.m_chkNomChampsSurPremiereLigne.Text = "The first line is the field names|114";
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(497, 388);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(75, 23);
            this.m_btnAnnuler.TabIndex = 8;
            this.m_btnAnnuler.Text = "Cancel|11";
            // 
            // m_btnSuivant
            // 
            this.m_btnSuivant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnSuivant.Location = new System.Drawing.Point(577, 388);
            this.m_btnSuivant.Name = "m_btnSuivant";
            this.m_btnSuivant.Size = new System.Drawing.Size(75, 23);
            this.m_btnSuivant.TabIndex = 9;
            this.m_btnSuivant.Text = "Next >|21";
            this.m_btnSuivant.Click += new System.EventHandler(this.m_btnSuivant_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Select Excel Sheet to import|141";
            // 
            // m_listeFeuilles
            // 
            this.m_listeFeuilles.FormattingEnabled = true;
            this.m_listeFeuilles.Location = new System.Drawing.Point(8, 29);
            this.m_listeFeuilles.Name = "m_listeFeuilles";
            this.m_listeFeuilles.Size = new System.Drawing.Size(281, 82);
            this.m_listeFeuilles.TabIndex = 11;
            this.m_listeFeuilles.SelectedIndexChanged += new System.EventHandler(this.m_listeFeuilles_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(313, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Data Range|142";
            // 
            // m_txtPlageDonnees
            // 
            this.m_txtPlageDonnees.Location = new System.Drawing.Point(405, 63);
            this.m_txtPlageDonnees.LockEdition = false;
            this.m_txtPlageDonnees.Name = "m_txtPlageDonnees";
            this.m_txtPlageDonnees.Size = new System.Drawing.Size(114, 20);
            this.m_txtPlageDonnees.TabIndex = 12;
            this.m_txtPlageDonnees.TextChanged += new System.EventHandler(this.m_txtPlageDonnees_TextChanged);
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(407, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "(A1:K77)";
            // 
            // m_grilleApercu
            // 
            this.m_grilleApercu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_grilleApercu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_grilleApercu.Location = new System.Drawing.Point(8, 117);
            this.m_grilleApercu.Name = "m_grilleApercu";
            this.m_grilleApercu.Size = new System.Drawing.Size(644, 265);
            this.m_grilleApercu.TabIndex = 14;
            // 
            // CFormOptionsImportExcel1
            // 
            this.AcceptButton = this.m_btnSuivant;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(666, 418);
            this.Controls.Add(this.m_grilleApercu);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_txtPlageDonnees);
            this.Controls.Add(this.m_listeFeuilles);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_btnSuivant);
            this.Controls.Add(this.m_btnAnnuler);
            this.Controls.Add(this.m_chkNomChampsSurPremiereLigne);
            this.Name = "CFormOptionsImportExcel1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import Options 1/2|107";
            this.Load += new System.EventHandler(this.CFormOptionsCSV1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_grilleApercu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		public CFormOptionsImportExcel1()
		{
			InitializeComponent();
		}
		
        private string m_strFichierExemple = "";
        private string m_strSheetExemple = "";
		private CParametreLectureExcel m_parametre = null;

		public static bool FillOptions ( CParametreLectureExcel parametre, string strFichierExemple )
		{
			CFormOptionsImportExcel1 form = new CFormOptionsImportExcel1();
			form.m_parametre = parametre;
            form.m_strFichierExemple = strFichierExemple;


			DialogResult result = DialogResult.Retry;
			while ( result == DialogResult.Retry )
			{
				result = form.ShowDialog();
                if (result == DialogResult.OK)
                    result = CFormOptionsImportExcel2.FillOptions(parametre, strFichierExemple);
			}
			form.Dispose();
			return result == DialogResult.OK;
		}


		//----------------------------------------------------------------------
		private void CFormOptionsCSV1_Load(object sender, System.EventArgs e)
		{
			if (m_parametre == null)
				return;

			// Lance la traduction du formulaire
			CWin32Traducteur.Translate(this);

			m_chkNomChampsSurPremiereLigne.Checked = m_parametre.NomChampsSurPremiereLigne;
            InitListeFeuilles();
            
		}

		//----------------------------------------------------------------------
		private void m_btnSuivant_Click(object sender, System.EventArgs e)
		{
			m_parametre.NomChampsSurPremiereLigne = m_chkNomChampsSurPremiereLigne.Checked;
            m_parametre.SheetName = m_strSheetExemple;
            m_parametre.PlageDonnees = m_txtPlageDonnees.Text.ToUpper();
            
            DialogResult = DialogResult.OK;
			Close();
		}



        //----------------------------------------------------------------------
        private void InitListeFeuilles()
        {
            m_listeFeuilles.Items.Clear();

            if (m_strFichierExemple.Trim() != "")
            {
                CLecteurFichierExcel lecteur = new CLecteurFichierExcel(m_strFichierExemple, "");
                string[] tableNomsFeuilles = lecteur.GetExcelSheetNames();
                if(tableNomsFeuilles != null)
                    m_listeFeuilles.Items.AddRange(tableNomsFeuilles);
                lecteur.Close();
            }
        }

        //----------------------------------------------------------------------
        private void m_listeFeuilles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(m_listeFeuilles.SelectedItems.Count ==1)
                UpdateVisuApercu();
        }

        //-----------------------------------------------------------------------
        private void UpdateVisuApercu()
        {

            string strNomFeuille = m_listeFeuilles.SelectedItem as string;
            string strPlage = m_txtPlageDonnees.Text;
            if (strPlage != "" && !PlageDonneesIsValide(strPlage))
            {
                CFormAlerte.Afficher(I.T("Invalid data range|143"));
                return;
            }

            if (strNomFeuille != null && m_strFichierExemple != "" && strNomFeuille != "")
            {
                m_strSheetExemple = strNomFeuille;
                CLecteurFichierExcel reader = null;
                try
                {
                    // Utiliser ici le lecteur de fichier Excel 
                    reader = new CLecteurFichierExcel(m_strFichierExemple, strNomFeuille, strPlage);
                    DataTable dtApercu = reader.GetTable(m_chkNomChampsSurPremiereLigne.Checked);

                    if (dtApercu == null)
                        m_grilleApercu.Visible = false;
                    else
                    {
                        m_grilleApercu.Visible = true;
                        m_grilleApercu.DataSource = dtApercu;
                    }
                }
                catch (Exception ex)
                {
                    CFormAlerte.Afficher(ex.Message);
                }
                finally
                {
                    reader.Close();
                }
            }

        }

        private void m_txtPlageDonnees_TextChanged(object sender, EventArgs e)
        {
            int nPositioncurseur = m_txtPlageDonnees.SelectionStart;
            m_txtPlageDonnees.Text = m_txtPlageDonnees.Text.ToUpper();
            m_txtPlageDonnees.SelectionStart = nPositioncurseur;
            if (m_txtPlageDonnees.Text != "" && PlageDonneesIsValide(m_txtPlageDonnees.Text))
                m_errorProvider.SetError(m_txtPlageDonnees, "");
            else
                m_errorProvider.SetError(m_txtPlageDonnees, I.T("Invalid Data Range|143"));
        }

        private bool PlageDonneesIsValide(string strPlage)
        {
            Regex expPlageExcel = new Regex("[A-Z]+[0-9]:[A-Z]+[0-9]");

            return expPlageExcel.IsMatch(strPlage);
        }
	}
}
