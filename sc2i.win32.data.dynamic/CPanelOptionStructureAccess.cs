using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;

using System.Text;

using sc2i.common;
using sc2i.data.dynamic;

namespace sc2i.win32.data.dynamic
{
	public class CPanelOptionStructureAccess : sc2i.win32.data.dynamic.CPanelOptionStructure
	{
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox m_cmbFormat;
		private System.ComponentModel.IContainer components = null;

		public CPanelOptionStructureAccess()
		{
			// Cet appel est requis par le Concepteur Windows Form.
			InitializeComponent();
		}

		public CPanelOptionStructureAccess(CExporteurDatasetAccess exporteur)
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
            this.label5 = new System.Windows.Forms.Label();
            this.m_cmbFormat = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(24, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "Format|212";
            // 
            // m_cmbFormat
            // 
            this.m_cmbFormat.DisplayMember = "Libelle";
            this.m_cmbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbFormat.Location = new System.Drawing.Point(80, 96);
            this.m_cmbFormat.Name = "m_cmbFormat";
            this.m_cmbFormat.Size = new System.Drawing.Size(184, 21);
            this.m_cmbFormat.TabIndex = 12;
            this.m_cmbFormat.ValueMember = "Valeur";
            // 
            // CPanelOptionStructureAccess
            // 
            this.Controls.Add(this.m_cmbFormat);
            this.Controls.Add(this.label5);
            this.Name = "CPanelOptionStructureAccess";
            this.Size = new System.Drawing.Size(596, 224);
            this.Load += new System.EventHandler(this.CPanelOptionStructure_Load);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.m_cmbFormat, 0);
            this.ResumeLayout(false);

		}
		#endregion

		

		private void RestoreDefaultValue()
		{
			if (m_exporteur==null)
				m_exporteur = new CExporteurDatasetAccess();
			AffecteControlsWithExporteurValues();
		}

		private void AffecteControlsWithExporteurValues()
		{
			if (m_exporteur==null)
				m_exporteur = new CExporteurDatasetAccess();

			CExporteurDatasetAccess exp = (CExporteurDatasetAccess) m_exporteur;

			m_cmbFormat.DataSource = CUtilSurEnum.GetCouplesFromEnum ( typeof ( VersionAccessExport ) );
			foreach ( CUtilSurEnum.CCoupleEnumLibelle couple in (IList)m_cmbFormat.DataSource )
				if ( couple.Valeur == (int)exp.VersionAccess )
					m_cmbFormat.SelectedItem = couple;
			if ( m_cmbFormat.SelectedItem == null )
				m_cmbFormat.SelectedIndex = 0;
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
			m_frmSave.Filter = "MS Access (*.mdb)|*.mdb|All files|*.*"; 
			m_frmSave.OverwritePrompt = true;
			if (m_frmSave.ShowDialog() == DialogResult.OK)
				strOldFileName = m_frmSave.FileName;

			return strOldFileName;
		}



		public override IExporteurDataset Exporteur
		{
			get
			{
				CExporteurDatasetAccess exp = new CExporteurDatasetAccess();

				m_exporteur.ExporteStructureOnly = StructureOnlyChecked;

				exp.VersionAccess = (VersionAccessExport)((CUtilSurEnum.CCoupleEnumLibelle)m_cmbFormat.SelectedItem).Valeur;

				m_exporteur = exp;

				return m_exporteur;
			}
			set
			{
				if (value is CExporteurDatasetAccess)
				{
					base.Exporteur = value;
					AffecteControlsWithExporteurValues();
				}
			}
		}
	}
}

