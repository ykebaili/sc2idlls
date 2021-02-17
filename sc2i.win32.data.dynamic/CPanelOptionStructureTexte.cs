using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;

using System.Text;

using sc2i.data.dynamic;

namespace sc2i.win32.data.dynamic
{
	public class CPanelOptionStructureTexte : sc2i.win32.data.dynamic.CPanelOptionStructure
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox m_cmbSeparateurDecimal;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox m_cmbIndicateurTexte;
		private System.Windows.Forms.CheckBox m_chkIndicateurTexte;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.RadioButton m_btnMonoFile;
		private System.Windows.Forms.RadioButton m_btnMultiFiles;
		private System.Windows.Forms.ComboBox m_cmbEncodage;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox m_cmbSeparateurChamp;
		private System.Windows.Forms.CheckBox m_chkLigneEntete;
		private System.Windows.Forms.CheckBox m_chkMasquerIdsAuto;
		private System.ComponentModel.IContainer components = null;

		public CPanelOptionStructureTexte()
		{
			// Cet appel est requis par le Concepteur Windows Form.
			InitializeComponent();
		}

		public CPanelOptionStructureTexte(CExporteurDatasetText exporteur)
			:base(exporteur)
		{
			// Cet appel est requis par le Concepteur Windows Form.
			InitializeComponent();
		}

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_cmbSeparateurDecimal = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_cmbIndicateurTexte = new System.Windows.Forms.ComboBox();
            this.m_chkIndicateurTexte = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnMultiFiles = new System.Windows.Forms.RadioButton();
            this.m_btnMonoFile = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.m_cmbEncodage = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.m_cmbSeparateurChamp = new System.Windows.Forms.ComboBox();
            this.m_chkLigneEntete = new System.Windows.Forms.CheckBox();
            this.m_chkMasquerIdsAuto = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "Filed separator|203";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(24, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Decimal separator|204";
            // 
            // m_cmbSeparateurDecimal
            // 
            this.m_cmbSeparateurDecimal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbSeparateurDecimal.Items.AddRange(new object[] {
            ".",
            ","});
            this.m_cmbSeparateurDecimal.Location = new System.Drawing.Point(176, 128);
            this.m_cmbSeparateurDecimal.Name = "m_cmbSeparateurDecimal";
            this.m_cmbSeparateurDecimal.Size = new System.Drawing.Size(80, 21);
            this.m_cmbSeparateurDecimal.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(24, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Text indicator|205";
            // 
            // m_cmbIndicateurTexte
            // 
            this.m_cmbIndicateurTexte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbIndicateurTexte.Enabled = false;
            this.m_cmbIndicateurTexte.Items.AddRange(new object[] {
            "\"",
            "\'",
            ""});
            this.m_cmbIndicateurTexte.Location = new System.Drawing.Point(176, 152);
            this.m_cmbIndicateurTexte.Name = "m_cmbIndicateurTexte";
            this.m_cmbIndicateurTexte.Size = new System.Drawing.Size(80, 21);
            this.m_cmbIndicateurTexte.TabIndex = 12;
            // 
            // m_chkIndicateurTexte
            // 
            this.m_chkIndicateurTexte.Location = new System.Drawing.Point(156, 154);
            this.m_chkIndicateurTexte.Name = "m_chkIndicateurTexte";
            this.m_chkIndicateurTexte.Size = new System.Drawing.Size(16, 16);
            this.m_chkIndicateurTexte.TabIndex = 11;
            this.m_chkIndicateurTexte.CheckedChanged += new System.EventHandler(this.m_chkIndicateurTexte_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.m_btnMultiFiles);
            this.panel1.Controls.Add(this.m_btnMonoFile);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(264, 104);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 68);
            this.panel1.TabIndex = 1;
            // 
            // m_btnMultiFiles
            // 
            this.m_btnMultiFiles.Location = new System.Drawing.Point(8, 40);
            this.m_btnMultiFiles.Name = "m_btnMultiFiles";
            this.m_btnMultiFiles.Size = new System.Drawing.Size(180, 24);
            this.m_btnMultiFiles.TabIndex = 15;
            this.m_btnMultiFiles.Text = "Export in multiple files|210";
            // 
            // m_btnMonoFile
            // 
            this.m_btnMonoFile.Checked = true;
            this.m_btnMonoFile.Location = new System.Drawing.Point(8, 20);
            this.m_btnMonoFile.Name = "m_btnMonoFile";
            this.m_btnMonoFile.Size = new System.Drawing.Size(168, 24);
            this.m_btnMonoFile.TabIndex = 14;
            this.m_btnMonoFile.TabStop = true;
            this.m_btnMonoFile.Text = "Export in one file|209";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(191, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Multiple table export|208";
            // 
            // m_cmbEncodage
            // 
            this.m_cmbEncodage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbEncodage.Items.AddRange(new object[] {
            "ANSI",
            "Unicode",
            "ASCII"});
            this.m_cmbEncodage.Location = new System.Drawing.Point(92, 176);
            this.m_cmbEncodage.Name = "m_cmbEncodage";
            this.m_cmbEncodage.Size = new System.Drawing.Size(164, 21);
            this.m_cmbEncodage.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(24, 178);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "Encoding|206";
            // 
            // m_cmbSeparateurChamp
            // 
            this.m_cmbSeparateurChamp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbSeparateurChamp.Items.AddRange(new object[] {
            "Tab",
            ";",
            "/",
            "|"});
            this.m_cmbSeparateurChamp.Location = new System.Drawing.Point(176, 104);
            this.m_cmbSeparateurChamp.Name = "m_cmbSeparateurChamp";
            this.m_cmbSeparateurChamp.Size = new System.Drawing.Size(80, 21);
            this.m_cmbSeparateurChamp.TabIndex = 9;
            // 
            // m_chkLigneEntete
            // 
            this.m_chkLigneEntete.Location = new System.Drawing.Point(256, 178);
            this.m_chkLigneEntete.Name = "m_chkLigneEntete";
            this.m_chkLigneEntete.Size = new System.Drawing.Size(272, 16);
            this.m_chkLigneEntete.TabIndex = 14;
            this.m_chkLigneEntete.Text = "Use the first line for column names|255";
            // 
            // m_chkMasquerIdsAuto
            // 
            this.m_chkMasquerIdsAuto.Location = new System.Drawing.Point(92, 200);
            this.m_chkMasquerIdsAuto.Name = "m_chkMasquerIdsAuto";
            this.m_chkMasquerIdsAuto.Size = new System.Drawing.Size(257, 16);
            this.m_chkMasquerIdsAuto.TabIndex = 15;
            this.m_chkMasquerIdsAuto.Text = "Mask automatic identifiers|207";
            // 
            // CPanelOptionStructureTexte
            // 
            this.Controls.Add(this.m_chkMasquerIdsAuto);
            this.Controls.Add(this.m_chkLigneEntete);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_cmbSeparateurChamp);
            this.Controls.Add(this.m_cmbSeparateurDecimal);
            this.Controls.Add(this.m_chkIndicateurTexte);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_cmbEncodage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_cmbIndicateurTexte);
            this.Controls.Add(this.label5);
            this.Name = "CPanelOptionStructureTexte";
            this.Size = new System.Drawing.Size(596, 224);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.m_cmbIndicateurTexte, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.m_cmbEncodage, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.m_chkIndicateurTexte, 0);
            this.Controls.SetChildIndex(this.m_cmbSeparateurDecimal, 0);
            this.Controls.SetChildIndex(this.m_cmbSeparateurChamp, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.m_chkLigneEntete, 0);
            this.Controls.SetChildIndex(this.m_chkMasquerIdsAuto, 0);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void m_chkIndicateurTexte_CheckedChanged(object sender, System.EventArgs e)
		{
			m_cmbIndicateurTexte.Enabled = m_chkIndicateurTexte.Checked;
		}

		private void RestoreDefaultValue()
		{
			/*if (m_exporteur==null)
				m_exporteur = new CExporteurDatasetText();*/
			AffecteControlsWithExporteurValues();
		}

		private void AffecteControlsWithExporteurValues()
		{
			if (m_exporteur==null)
				m_exporteur = new CExporteurDatasetText();

			CExporteurDatasetText exp = (CExporteurDatasetText) m_exporteur;

			// Encodage
			if (exp.Encodage == Encoding.ASCII)
			{
				m_cmbEncodage.Text = "ASCII";
				//m_cmbEncodage.SelectedText = "ASCII";
			}
			else if (exp.Encodage == Encoding.Unicode)
			{
				m_cmbEncodage.Text = "Unicode";
				//m_cmbEncodage.SelectedText = "Unicode";
			}
			else 
			{
				m_cmbEncodage.Text = "ANSI";
				//m_cmbEncodage.SelectedValue = "ANSI";
			}

			// Indicateur de texte
			if (exp.IndicateurTexte == "")
				m_chkIndicateurTexte.Checked = false;
			else
			{
				m_chkIndicateurTexte.Checked = true;
				m_cmbIndicateurTexte.Text = exp.IndicateurTexte;
				m_cmbIndicateurTexte.SelectedText = exp.IndicateurTexte;
			}

			// Multifichiers
			m_btnMultiFiles.Checked = exp.Multifichier;

			// Séparateur de champs
			if (exp.SeparateurChamp == "\t")
			{
				m_cmbSeparateurChamp.Text = "Tab";
				m_cmbSeparateurChamp.SelectedText = "Tab";
			}
			else 
			{
				m_cmbSeparateurChamp.Text = exp.SeparateurChamp;
				m_cmbSeparateurChamp.SelectedText = exp.SeparateurChamp;
			}

			// Séparateur décimal
			m_cmbSeparateurDecimal.Text = exp.SeparateurDecimal;
			m_cmbSeparateurDecimal.SelectedText = exp.SeparateurDecimal;

			m_chkLigneEntete.Checked = exp.LigneEntete;
			m_chkMasquerIdsAuto.Checked = exp.MasquerClesAuto;
		}

		protected override void CPanelOptionStructure_Load(object sender, System.EventArgs e)
		{
			RestoreDefaultValue();
		}

		protected override string GetFileName()
		{
			string strOldFileName = base.FileName;
			m_frmSave.DefaultExt = "txt";
			m_frmSave.AddExtension = true;
			m_frmSave.Filter = "Text files (*.txt)|*.txt|Csv files(*.csv)|*.csv|All files (*.*)|*.*"; 
			m_frmSave.OverwritePrompt = true;
			if (m_frmSave.ShowDialog() == DialogResult.OK)
				strOldFileName = m_frmSave.FileName;

			return strOldFileName;
		}

		public override IExporteurDataset Exporteur
		{
			get
			{
				CExporteurDatasetText exp = new CExporteurDatasetText();

				// Encodage
				if (m_cmbEncodage.Text == "ASCII")
					exp.Encodage = Encoding.ASCII;
				else if (m_cmbEncodage.Text == "Unicode")
					exp.Encodage = Encoding.Unicode;
				else 
					exp.Encodage = Encoding.Default;

				// Indicateur de texte
				exp.IndicateurTexte = m_chkIndicateurTexte.Checked ? (string)(m_cmbIndicateurTexte.Text) : "";

				// Multifichiers
				exp.Multifichier = m_btnMultiFiles.Checked;

				// Séparateur de champs
				if (m_cmbSeparateurChamp.Text == "Tab")
					exp.SeparateurChamp = "\t";
				else 
					exp.SeparateurChamp = m_cmbSeparateurChamp.Text;

				// Séparateur décimal
				exp.SeparateurDecimal = m_cmbSeparateurDecimal.Text;

				exp.MasquerClesAuto = m_chkMasquerIdsAuto.Checked;

				exp.LigneEntete = m_chkLigneEntete.Checked;

				m_exporteur.ExporteStructureOnly = StructureOnlyChecked;

				m_exporteur = exp;

				return m_exporteur;
			}
			set
			{
				if (value is CExporteurDatasetText)
				{
					base.Exporteur = value;
					AffecteControlsWithExporteurValues();
				}
			}
        }
	}
}

