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
	/// Description résumée de CPanelOptionStructure.
	/// </summary>
	public class CPanelOptionStructure : System.Windows.Forms.UserControl
	{
		private bool m_bSansFichier =  false;
		protected IExporteurDataset m_exporteur = new CExporteurDatasetText();
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label m_txtFilename;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button m_btnChoixFile;
		private System.Windows.Forms.Panel panel1;
		protected System.Windows.Forms.SaveFileDialog m_frmSave;
		private System.Windows.Forms.CheckBox m_chkStructureOnly;
		private System.Windows.Forms.Panel m_panelFichier;
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CPanelOptionStructure()
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();
		}

		public CPanelOptionStructure(IExporteurDataset exporteur)
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			m_exporteur = exporteur;
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

		#region Component Designer generated code
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnChoixFile = new System.Windows.Forms.Button();
            this.m_txtFilename = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_frmSave = new System.Windows.Forms.SaveFileDialog();
            this.m_chkStructureOnly = new System.Windows.Forms.CheckBox();
            this.m_panelFichier = new System.Windows.Forms.Panel();
            this.m_panelFichier.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(20, 88);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(560, 4);
            this.panel1.TabIndex = 9;
            // 
            // m_btnChoixFile
            // 
            this.m_btnChoixFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnChoixFile.Location = new System.Drawing.Point(536, 0);
            this.m_btnChoixFile.Name = "m_btnChoixFile";
            this.m_btnChoixFile.Size = new System.Drawing.Size(24, 23);
            this.m_btnChoixFile.TabIndex = 8;
            this.m_btnChoixFile.Text = "...";
            this.m_btnChoixFile.Click += new System.EventHandler(this.m_btnChoixFile_Click);
            // 
            // m_txtFilename
            // 
            this.m_txtFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFilename.BackColor = System.Drawing.Color.White;
            this.m_txtFilename.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_txtFilename.Location = new System.Drawing.Point(141, 0);
            this.m_txtFilename.Name = "m_txtFilename";
            this.m_txtFilename.Size = new System.Drawing.Size(395, 20);
            this.m_txtFilename.TabIndex = 7;
            this.m_txtFilename.DoubleClick += new System.EventHandler(this.m_btnChoixFile_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Export file|202";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 21);
            this.label1.TabIndex = 5;
            this.label1.Text = "Export options|201";
            // 
            // m_frmSave
            // 
            this.m_frmSave.FileName = "export";
            // 
            // m_chkStructureOnly
            // 
            this.m_chkStructureOnly.Location = new System.Drawing.Point(11, 58);
            this.m_chkStructureOnly.Name = "m_chkStructureOnly";
            this.m_chkStructureOnly.Size = new System.Drawing.Size(393, 24);
            this.m_chkStructureOnly.TabIndex = 10;
            this.m_chkStructureOnly.Text = "Export structure only (without records) |233";
            // 
            // m_panelFichier
            // 
            this.m_panelFichier.Controls.Add(this.label2);
            this.m_panelFichier.Controls.Add(this.m_txtFilename);
            this.m_panelFichier.Controls.Add(this.m_btnChoixFile);
            this.m_panelFichier.Location = new System.Drawing.Point(8, 32);
            this.m_panelFichier.Name = "m_panelFichier";
            this.m_panelFichier.Size = new System.Drawing.Size(584, 24);
            this.m_panelFichier.TabIndex = 11;
            // 
            // CPanelOptionStructure
            // 
            this.Controls.Add(this.m_panelFichier);
            this.Controls.Add(this.m_chkStructureOnly);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "CPanelOptionStructure";
            this.Size = new System.Drawing.Size(616, 228);
            this.Load += new System.EventHandler(this.CPanelOptionStructure_Load);
            this.m_panelFichier.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		public bool SansFichier
		{
			get
			{
				return m_bSansFichier;
			}
			set
			{
				m_bSansFichier = value;
				m_panelFichier.Visible = !m_bSansFichier;
			}
		}

		private void m_btnChoixFile_Click(object sender, System.EventArgs e)
		{
			m_txtFilename.Text = GetFileName();
		}

		protected virtual string GetFileName()
		{
			string strOldFileName = m_txtFilename.Text;
			m_frmSave.DefaultExt = "xml";
			m_frmSave.AddExtension = true;
			string strFilter = "";
			if ( Exporteur is CExporteurDatasetXML )
				strFilter = "XML files (*.xml)|*.xml"; 
			else if ( Exporteur is CExporteurDatasetAccess )
				strFilter = "Access files (*.mdb)|*.mdb"; 
			if ( strFilter.Length > 0 )
				strFilter += "|";
			strFilter += "All files (*.*)|*.*"; 
			m_frmSave.Filter = strFilter;
			m_frmSave.OverwritePrompt = true;
			if (m_frmSave.ShowDialog() == DialogResult.OK)
				strOldFileName = m_frmSave.FileName;

			return strOldFileName;
		}

        //------------------------------------------------------------------------------------
		protected virtual void CPanelOptionStructure_Load(object sender, System.EventArgs e)
		{
            sc2i.win32.common.CWin32Traducteur.Translate(this);
		}

		public string FileName
		{
			get
			{
				return m_txtFilename.Text;
			}
		}

		protected bool StructureOnlyChecked
		{
			get
			{
				return m_chkStructureOnly.Checked;
			}
			set
			{
				m_chkStructureOnly.Checked = value;
			}
		}

		public virtual IExporteurDataset Exporteur
		{
			get
			{
				if (m_exporteur == null)
					m_exporteur = new CExporteurDatasetXML();

				m_exporteur.ExporteStructureOnly = StructureOnlyChecked;
	
				return m_exporteur;
			}
			set
			{
				m_exporteur = value;
				if ( value != null )
					StructureOnlyChecked = m_exporteur.ExporteStructureOnly;
			}
		}
	}
}
