using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

using sc2i.data;
using sc2i.common;
using sc2i.win32.common;

namespace sc2i.win32.data
{
	public class CFormOptionsCSV1 : System.Windows.Forms.Form
	{
		#region Code généré par le Concepteur Windows Form

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton m_chkTabulation;
		private System.Windows.Forms.RadioButton m_chkPointVirgule;
		private System.Windows.Forms.RadioButton m_chkVirgule;
		private System.Windows.Forms.RadioButton m_chkEspace;
		private System.Windows.Forms.RadioButton m_chkAutre;
		private System.Windows.Forms.TextBox m_txtAutreSep;
		private System.Windows.Forms.TextBox m_txtApercu;
		private System.Windows.Forms.Button m_btnAnnuler;
		private System.Windows.Forms.Button m_btnSuivant;
		private System.Windows.Forms.CheckBox m_chkNomChampsSurPremiereLigne;
		private System.Windows.Forms.CheckBox m_chkIndicateurTexte;
		private System.Windows.Forms.ComboBox m_cmbIndicateurTexte;
        private Label label1;
        private C2iComboBox m_cmbEncoding;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;



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
            this.m_chkNomChampsSurPremiereLigne = new System.Windows.Forms.CheckBox();
            this.m_chkIndicateurTexte = new System.Windows.Forms.CheckBox();
            this.m_cmbIndicateurTexte = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_txtAutreSep = new System.Windows.Forms.TextBox();
            this.m_chkAutre = new System.Windows.Forms.RadioButton();
            this.m_chkEspace = new System.Windows.Forms.RadioButton();
            this.m_chkVirgule = new System.Windows.Forms.RadioButton();
            this.m_chkPointVirgule = new System.Windows.Forms.RadioButton();
            this.m_chkTabulation = new System.Windows.Forms.RadioButton();
            this.m_txtApercu = new System.Windows.Forms.TextBox();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnSuivant = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.m_cmbEncoding = new sc2i.win32.common.C2iComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_chkNomChampsSurPremiereLigne
            // 
            this.m_chkNomChampsSurPremiereLigne.Location = new System.Drawing.Point(272, 8);
            this.m_chkNomChampsSurPremiereLigne.Name = "m_chkNomChampsSurPremiereLigne";
            this.m_chkNomChampsSurPremiereLigne.Size = new System.Drawing.Size(264, 24);
            this.m_chkNomChampsSurPremiereLigne.TabIndex = 0;
            this.m_chkNomChampsSurPremiereLigne.Text = "The first line is the field names|114";
            // 
            // m_chkIndicateurTexte
            // 
            this.m_chkIndicateurTexte.Location = new System.Drawing.Point(272, 32);
            this.m_chkIndicateurTexte.Name = "m_chkIndicateurTexte";
            this.m_chkIndicateurTexte.Size = new System.Drawing.Size(238, 24);
            this.m_chkIndicateurTexte.TabIndex = 1;
            this.m_chkIndicateurTexte.Text = "Text identificator|115";
            // 
            // m_cmbIndicateurTexte
            // 
            this.m_cmbIndicateurTexte.Items.AddRange(new object[] {
            "\"",
            "\'"});
            this.m_cmbIndicateurTexte.Location = new System.Drawing.Point(416, 32);
            this.m_cmbIndicateurTexte.MaxLength = 1;
            this.m_cmbIndicateurTexte.Name = "m_cmbIndicateurTexte";
            this.m_cmbIndicateurTexte.Size = new System.Drawing.Size(56, 21);
            this.m_cmbIndicateurTexte.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_txtAutreSep);
            this.groupBox1.Controls.Add(this.m_chkAutre);
            this.groupBox1.Controls.Add(this.m_chkEspace);
            this.groupBox1.Controls.Add(this.m_chkVirgule);
            this.groupBox1.Controls.Add(this.m_chkPointVirgule);
            this.groupBox1.Controls.Add(this.m_chkTabulation);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 64);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Separator|108";
            // 
            // m_txtAutreSep
            // 
            this.m_txtAutreSep.Location = new System.Drawing.Point(144, 40);
            this.m_txtAutreSep.MaxLength = 1;
            this.m_txtAutreSep.Name = "m_txtAutreSep";
            this.m_txtAutreSep.Size = new System.Drawing.Size(24, 20);
            this.m_txtAutreSep.TabIndex = 5;
            // 
            // m_chkAutre
            // 
            this.m_chkAutre.Location = new System.Drawing.Point(88, 41);
            this.m_chkAutre.Name = "m_chkAutre";
            this.m_chkAutre.Size = new System.Drawing.Size(56, 16);
            this.m_chkAutre.TabIndex = 4;
            this.m_chkAutre.Text = "Other|113";
            // 
            // m_chkEspace
            // 
            this.m_chkEspace.Location = new System.Drawing.Point(8, 38);
            this.m_chkEspace.Name = "m_chkEspace";
            this.m_chkEspace.Size = new System.Drawing.Size(64, 22);
            this.m_chkEspace.TabIndex = 3;
            this.m_chkEspace.Text = "Space|112";
            // 
            // m_chkVirgule
            // 
            this.m_chkVirgule.Location = new System.Drawing.Point(182, 10);
            this.m_chkVirgule.Name = "m_chkVirgule";
            this.m_chkVirgule.Size = new System.Drawing.Size(85, 29);
            this.m_chkVirgule.TabIndex = 2;
            this.m_chkVirgule.Text = "Comma|111";
            // 
            // m_chkPointVirgule
            // 
            this.m_chkPointVirgule.Location = new System.Drawing.Point(88, 13);
            this.m_chkPointVirgule.Name = "m_chkPointVirgule";
            this.m_chkPointVirgule.Size = new System.Drawing.Size(103, 23);
            this.m_chkPointVirgule.TabIndex = 1;
            this.m_chkPointVirgule.Text = "Semicolon|110";
            // 
            // m_chkTabulation
            // 
            this.m_chkTabulation.Location = new System.Drawing.Point(8, 16);
            this.m_chkTabulation.Name = "m_chkTabulation";
            this.m_chkTabulation.Size = new System.Drawing.Size(80, 16);
            this.m_chkTabulation.TabIndex = 0;
            this.m_chkTabulation.Text = "Tabulation|109";
            // 
            // m_txtApercu
            // 
            this.m_txtApercu.Location = new System.Drawing.Point(8, 80);
            this.m_txtApercu.Multiline = true;
            this.m_txtApercu.Name = "m_txtApercu";
            this.m_txtApercu.ReadOnly = true;
            this.m_txtApercu.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.m_txtApercu.Size = new System.Drawing.Size(536, 208);
            this.m_txtApercu.TabIndex = 5;
            this.m_txtApercu.Text = "textBox1";
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(384, 288);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(75, 23);
            this.m_btnAnnuler.TabIndex = 8;
            this.m_btnAnnuler.Text = "Cancel|11";
            // 
            // m_btnSuivant
            // 
            this.m_btnSuivant.Location = new System.Drawing.Point(464, 288);
            this.m_btnSuivant.Name = "m_btnSuivant";
            this.m_btnSuivant.Size = new System.Drawing.Size(75, 23);
            this.m_btnSuivant.TabIndex = 9;
            this.m_btnSuivant.Text = "Next >|21";
            this.m_btnSuivant.Click += new System.EventHandler(this.m_btnSuivant_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(272, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 10;
            this.label1.Text = "Encoding|20004";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_cmbEncoding
            // 
            this.m_cmbEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbEncoding.FormattingEnabled = true;
            this.m_cmbEncoding.IsLink = false;
            this.m_cmbEncoding.Location = new System.Drawing.Point(378, 55);
            this.m_cmbEncoding.LockEdition = false;
            this.m_cmbEncoding.Name = "m_cmbEncoding";
            this.m_cmbEncoding.Size = new System.Drawing.Size(166, 21);
            this.m_cmbEncoding.TabIndex = 11;
            this.m_cmbEncoding.SelectionChangeCommitted += new System.EventHandler(this.m_cmbEncoding_SelectionChangeCommitted);
            // 
            // CFormOptionsCSV1
            // 
            this.AcceptButton = this.m_btnSuivant;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(552, 318);
            this.Controls.Add(this.m_cmbEncoding);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_btnSuivant);
            this.Controls.Add(this.m_btnAnnuler);
            this.Controls.Add(this.m_txtApercu);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.m_cmbIndicateurTexte);
            this.Controls.Add(this.m_chkIndicateurTexte);
            this.Controls.Add(this.m_chkNomChampsSurPremiereLigne);
            this.Name = "CFormOptionsCSV1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import Options 1/2|107";
            this.Load += new System.EventHandler(this.CFormOptionsCSV1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		public CFormOptionsCSV1()
		{
			InitializeComponent();
		}
		

		private string m_strTexteExemple = "";
        private string m_strFichierExemple = "";
		private CParametreLectureCSV m_parametre = null;

        private string GetTexteExemple()
        {
            string strTexteExemple = "";
            if (m_strFichierExemple != "")
            {
                StreamReader reader = null;
                try
                {
                    reader = new StreamReader(m_strFichierExemple, m_parametre.GetEncoding());
                    int nLigne = 0;
                    string strLigne = reader.ReadLine();
                    while (strLigne != null && nLigne < 100)
                    {
                        strTexteExemple += strLigne + "\r\n";
                        strLigne = reader.ReadLine();
                        nLigne++;
                    }
                    reader.Close();
                }
                catch
                {
                }
                finally
                {
                    try
                    {
                        reader.Close();
                    }

                    catch { }
                }
            }
            return strTexteExemple;
        }


		public static bool FillOptions ( CParametreLectureCSV parametre, string strFichierExemple )
		{
			CFormOptionsCSV1 form = new CFormOptionsCSV1();
			form.m_parametre = parametre;
            form.m_strFichierExemple = strFichierExemple;

			DialogResult result = DialogResult.Retry;
			while ( result == DialogResult.Retry )
			{
				result = form.ShowDialog();
				if ( result == DialogResult.OK )
					result = CFormOptionsCSV2.FillOptions ( parametre, strFichierExemple );
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

			switch (m_parametre.Separateur)
			{
				case '\t':
					m_chkTabulation.Checked = true;
					break;
				case ',':
					m_chkVirgule.Checked = true;
					break;
				case ';':
					m_chkPointVirgule.Checked = true;
					break;
				case ' ':
					m_chkEspace.Checked = true;
					break;
				default:
					m_chkAutre.Checked = true;
					m_txtAutreSep.Text = m_parametre.Separateur + "";
					break;
			}

            m_cmbEncoding.DataSource = CEncoding.GetValeursEnumPossibleInEnumALibelle(typeof(CEncoding));
            m_cmbEncoding.DisplayMember = "Libelle";
            m_cmbEncoding.ValueMember = "Code";
            m_cmbEncoding.SelectedValue = m_parametre.Encodage;

			m_chkNomChampsSurPremiereLigne.Checked = m_parametre.NomChampsSurPremiereLigne;

			if (m_parametre.IndicateurTexte != "")
			{
				m_chkIndicateurTexte.Checked = true;
				m_cmbIndicateurTexte.Text = m_parametre.IndicateurTexte;
			}

            UpdateTexteExemple();
		}

        //----------------------------------------------------------------------
        private void UpdateTexteExemple()
        {
            m_strTexteExemple = GetTexteExemple();

            if (m_strTexteExemple.Trim() == "")
                m_txtApercu.Visible = false;
            else
            {
                m_txtApercu.Visible = true;
                m_txtApercu.Text = m_strTexteExemple;
            }
        }

        //----------------------------------------------------------------------
		private void m_btnSuivant_Click(object sender, System.EventArgs e)
		{
			if ( m_chkTabulation.Checked )
				m_parametre.Separateur = '\t';
			else if ( m_chkVirgule.Checked )
				m_parametre.Separateur = ',';
			else if ( m_chkEspace.Checked )
				m_parametre.Separateur = ' ';
			else if ( m_chkPointVirgule.Checked )
				m_parametre.Separateur = ';';
			else if ( m_chkAutre.Checked )
			{
				if ( m_txtAutreSep.Text.Length < 1 )
				{
					CFormAlerte.Afficher(I.T("Indicate a separator|120"), EFormAlerteType.Exclamation);
					return;
				}
				m_parametre.Separateur = m_txtAutreSep.Text[0];
			}
            if ( m_cmbEncoding.SelectedValue is CEncoding )
                m_parametre.Encodage = (EEncoding)m_cmbEncoding.SelectedValue; ;
			m_parametre.NomChampsSurPremiereLigne = m_chkNomChampsSurPremiereLigne.Checked;
			if ( m_chkIndicateurTexte.Checked )
			{
				if ( m_cmbIndicateurTexte.Text.Length < 1 )
				{
					CFormAlerte.Afficher(I.T("Indicate a text separator|121"), EFormAlerteType.Exclamation);
					return;
				}
				m_parametre.IndicateurTexte = m_cmbIndicateurTexte.Text;
			}
			else
				m_parametre.IndicateurTexte= "";
			DialogResult = DialogResult.OK;
			Close();
		}

        //----------------------------------------------------------------------
        private void m_cmbEncoding_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateTexteExemple();
        }
	}
}
