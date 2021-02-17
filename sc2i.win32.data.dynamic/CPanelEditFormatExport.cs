using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using sc2i.data.dynamic;

namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Description résumée de CPanelEditFormatExport.
	/// </summary>
	public class CPanelEditFormatExport : System.Windows.Forms.UserControl
	{
		private bool m_bSansFichier = false;
		private CPanelOptionStructure m_panelOptions = new CPanelOptionStructure( new CExporteurDatasetXML() );
		private System.Windows.Forms.Panel m_panelContainerOption;
		private System.Windows.Forms.Panel m_panelTypeExport;
		private System.Windows.Forms.RadioButton m_rbtnAccess;
		private System.Windows.Forms.RadioButton m_rbtnText;
		private System.Windows.Forms.RadioButton m_rbtnXML;
		private System.Windows.Forms.RadioButton m_rbtnExcel;
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CPanelEditFormatExport()
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			// TODO : ajoutez les initialisations après l'appel à InitializeComponent

		}

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

		#region Code généré par le Concepteur de composants
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.m_panelContainerOption = new System.Windows.Forms.Panel();
            this.m_panelTypeExport = new System.Windows.Forms.Panel();
            this.m_rbtnExcel = new System.Windows.Forms.RadioButton();
            this.m_rbtnAccess = new System.Windows.Forms.RadioButton();
            this.m_rbtnText = new System.Windows.Forms.RadioButton();
            this.m_rbtnXML = new System.Windows.Forms.RadioButton();
            this.m_panelTypeExport.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panelContainerOption
            // 
            this.m_panelContainerOption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelContainerOption.Location = new System.Drawing.Point(0, 32);
            this.m_panelContainerOption.Name = "m_panelContainerOption";
            this.m_panelContainerOption.Size = new System.Drawing.Size(576, 280);
            this.m_panelContainerOption.TabIndex = 7;
            // 
            // m_panelTypeExport
            // 
            this.m_panelTypeExport.Controls.Add(this.m_rbtnExcel);
            this.m_panelTypeExport.Controls.Add(this.m_rbtnAccess);
            this.m_panelTypeExport.Controls.Add(this.m_rbtnText);
            this.m_panelTypeExport.Controls.Add(this.m_rbtnXML);
            this.m_panelTypeExport.Location = new System.Drawing.Point(-8, 0);
            this.m_panelTypeExport.Name = "m_panelTypeExport";
            this.m_panelTypeExport.Size = new System.Drawing.Size(568, 32);
            this.m_panelTypeExport.TabIndex = 6;
            // 
            // m_rbtnExcel
            // 
            this.m_rbtnExcel.Location = new System.Drawing.Point(400, 4);
            this.m_rbtnExcel.Name = "m_rbtnExcel";
            this.m_rbtnExcel.Size = new System.Drawing.Size(160, 24);
            this.m_rbtnExcel.TabIndex = 3;
            this.m_rbtnExcel.Text = " Excel Export|181";
            this.m_rbtnExcel.CheckedChanged += new System.EventHandler(this.m_rbtnExcel_CheckedChanged);
            // 
            // m_rbtnAccess
            // 
            this.m_rbtnAccess.Location = new System.Drawing.Point(256, 4);
            this.m_rbtnAccess.Name = "m_rbtnAccess";
            this.m_rbtnAccess.Size = new System.Drawing.Size(160, 24);
            this.m_rbtnAccess.TabIndex = 2;
            this.m_rbtnAccess.Text = " MS-Access Export|180";
            this.m_rbtnAccess.CheckedChanged += new System.EventHandler(this.m_rbtnAccess_CheckedChanged);
            // 
            // m_rbtnText
            // 
            this.m_rbtnText.Location = new System.Drawing.Point(136, 4);
            this.m_rbtnText.Name = "m_rbtnText";
            this.m_rbtnText.Size = new System.Drawing.Size(116, 24);
            this.m_rbtnText.TabIndex = 1;
            this.m_rbtnText.Text = " Text Export|179";
            this.m_rbtnText.CheckedChanged += new System.EventHandler(this.m_rbtnText_CheckedChanged);
            // 
            // m_rbtnXML
            // 
            this.m_rbtnXML.Checked = true;
            this.m_rbtnXML.Location = new System.Drawing.Point(16, 4);
            this.m_rbtnXML.Name = "m_rbtnXML";
            this.m_rbtnXML.Size = new System.Drawing.Size(116, 24);
            this.m_rbtnXML.TabIndex = 0;
            this.m_rbtnXML.TabStop = true;
            this.m_rbtnXML.Text = "XML Export|178";
            this.m_rbtnXML.CheckedChanged += new System.EventHandler(this.m_rbtnXML_CheckedChanged);
            // 
            // CPanelEditFormatExport
            // 
            this.Controls.Add(this.m_panelContainerOption);
            this.Controls.Add(this.m_panelTypeExport);
            this.Name = "CPanelEditFormatExport";
            this.Size = new System.Drawing.Size(576, 312);
            this.Load += new System.EventHandler(this.CPanelEditFormatExport_Load);
            this.m_panelTypeExport.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		private void UpdatePanelOptions()
		{
			Control oldPanel = m_panelOptions;

			IExporteurDataset exporteur = null;
			if ( m_panelOptions != null )
				exporteur = m_panelOptions.Exporteur;

			if (m_rbtnXML.Checked && !(m_panelOptions is CPanelOptionStructure) )
				m_panelOptions = new CPanelOptionStructure( !(exporteur is CExporteurDatasetXML)?new CExporteurDatasetXML():(CExporteurDatasetXML)exporteur );
			else if ( m_rbtnAccess.Checked && !(m_panelOptions is CPanelOptionStructureAccess))
				m_panelOptions = new CPanelOptionStructureAccess ( !(exporteur is CExporteurDatasetAccess)?new CExporteurDatasetAccess():(CExporteurDatasetAccess)exporteur );
			else if ( m_rbtnText.Checked && !(m_panelOptions is CPanelOptionStructureTexte) )
				m_panelOptions = new CPanelOptionStructureTexte( !(exporteur is CExporteurDatasetText)?new CExporteurDatasetText():(CExporteurDatasetText)exporteur );
			if (m_rbtnExcel.Checked && !(m_panelOptions is CPanelOptionStructureExcel) )
				m_panelOptions = new CPanelOptionStructureExcel( !(exporteur is CExporteurDatasetExcelXml)?new CExporteurDatasetExcelXml():(CExporteurDatasetExcelXml)exporteur );
			

			if (oldPanel!=null)
			{
				oldPanel.Hide();
				oldPanel = null;
			}
			m_panelOptions.Parent = m_panelContainerOption;
			m_panelOptions.Dock = DockStyle.Fill;
			m_panelOptions.SansFichier = m_bSansFichier;
			m_panelOptions.Show();

            sc2i.win32.common.CWin32Traducteur.Translate(this);

		}

		//-----------------------------------------------------------------------------
		private void m_rbtnXML_CheckedChanged(object sender, System.EventArgs e)
		{
			UpdatePanelOptions();
		}

		//-----------------------------------------------------------------------------
		private void m_rbtnText_CheckedChanged(object sender, System.EventArgs e)
		{
			UpdatePanelOptions();
		}

		//-----------------------------------------------------------------------------
		private void m_rbtnAccess_CheckedChanged(object sender, System.EventArgs e)
		{
			UpdatePanelOptions();
		}

		//-----------------------------------------------------------------------------
		private void CPanelEditFormatExport_Load(object sender, System.EventArgs e)
		{
			UpdatePanelOptions();
		}

		//-----------------------------------------------------------------------------
		private void m_rbtnExcel_CheckedChanged(object sender, System.EventArgs e)
		{
			UpdatePanelOptions();
		}

		//-----------------------------------------------------------------------------
		public bool SansFichier
		{
			get
			{
				return m_bSansFichier;
			}
			set
			{
				m_bSansFichier = value;
				if ( m_panelOptions != null )
					m_panelOptions.SansFichier = m_bSansFichier;
			}
		}

		//-----------------------------------------------------------------------------
		public virtual IExporteurDataset Exporteur
		{
			get
			{
				if ( m_panelOptions == null )
					return null;
				return m_panelOptions.Exporteur;
			}
			set
			{
                if (value != null)
                {
                    if (value.GetType() == typeof(CExporteurDatasetAccess))
                        m_rbtnAccess.Checked = true;
                    else if (value.GetType() == typeof(CExporteurDatasetText))
                        m_rbtnText.Checked = true;
                    else if (value.GetType() == typeof(CExporteurDatasetXML))
                        m_rbtnXML.Checked = true;
                    else if (value.GetType() == typeof(CExporteurDatasetExcelXml))
                        m_rbtnExcel.Checked = true;
                }
				UpdatePanelOptions();
				m_panelOptions.Exporteur = value;
			}
		}

		//-----------------------------------------------------------------------------
		public virtual IDestinationExport DestinationExport
		{
			get
			{
				return new CDestinationExportFile ( m_panelOptions.FileName );
			}
		}
		
	}
}
