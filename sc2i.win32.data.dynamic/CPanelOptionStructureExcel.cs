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
	public class CPanelOptionStructureExcel : sc2i.win32.data.dynamic.CPanelOptionStructure
	{
		private System.Windows.Forms.CheckBox m_chkMasquerIdAuto;
		private System.ComponentModel.IContainer components = null;

		public CPanelOptionStructureExcel()
		{
			// Cet appel est requis par le Concepteur Windows Form.
			InitializeComponent();
		}

		public CPanelOptionStructureExcel(CExporteurDatasetExcelXml exporteur)
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
            this.m_chkMasquerIdAuto = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // m_chkMasquerIdAuto
            // 
            this.m_chkMasquerIdAuto.Location = new System.Drawing.Point(24, 96);
            this.m_chkMasquerIdAuto.Name = "m_chkMasquerIdAuto";
            this.m_chkMasquerIdAuto.Size = new System.Drawing.Size(400, 24);
            this.m_chkMasquerIdAuto.TabIndex = 12;
            this.m_chkMasquerIdAuto.Text = "Mask automatic identifiers|211";
            // 
            // CPanelOptionStructureExcel
            // 
            this.Controls.Add(this.m_chkMasquerIdAuto);
            this.Name = "CPanelOptionStructureExcel";
            this.Size = new System.Drawing.Size(596, 224);
            this.Load += new System.EventHandler(this.CPanelOptionStructure_Load);
            this.Controls.SetChildIndex(this.m_chkMasquerIdAuto, 0);
            this.ResumeLayout(false);

		}
		#endregion

		

		private void RestoreDefaultValue()
		{
			if (m_exporteur==null)
				m_exporteur = new CExporteurDatasetExcelXml();
			AffecteControlsWithExporteurValues();
		}

		private void AffecteControlsWithExporteurValues()
		{
			if (m_exporteur==null)
				m_exporteur = new CExporteurDatasetExcelXml();
			m_chkMasquerIdAuto.Checked = ((CExporteurDatasetExcelXml)m_exporteur).MasquerIdsAuto;
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
			m_frmSave.Filter = "Excel file (*.xls)|*.xls|All files|*.*"; 
			m_frmSave.OverwritePrompt = true;
			if (m_frmSave.ShowDialog() == DialogResult.OK)
				strOldFileName = m_frmSave.FileName;

			return strOldFileName;
		}



		public override IExporteurDataset Exporteur
		{
			get
			{
				CExporteurDatasetExcelXml exp = new CExporteurDatasetExcelXml();

				exp.ExporteStructureOnly = StructureOnlyChecked;
				exp.MasquerIdsAuto = m_chkMasquerIdAuto.Checked;

				m_exporteur = exp;

				return m_exporteur;
			}
			set
			{
				if (value is CExporteurDatasetExcelXml)
				{
					base.Exporteur = value;
					AffecteControlsWithExporteurValues();
				}
			}
		}
	}
}

