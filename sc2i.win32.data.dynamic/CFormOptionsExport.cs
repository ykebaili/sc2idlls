using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using sc2i.common;
using sc2i.data.dynamic;
using sc2i.win32.common;

using sc2i.data;
using sc2i.formulaire;

namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Description résumée de CFormOptionsExport.
	/// </summary>
	public class CFormOptionsExport : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button m_btnCancel;
		private System.Windows.Forms.Button m_btnOK;
		private sc2i.win32.data.dynamic.CPanelEditFormatExport m_panelFormatExport;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CFormOptionsExport()
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();

			//
			// TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
			//
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

		#region Code généré par le Concepteur Windows Form
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            sc2i.data.dynamic.CExporteurDatasetXML cExporteurDatasetXML4 = new sc2i.data.dynamic.CExporteurDatasetXML();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_btnOK = new System.Windows.Forms.Button();
            this.m_panelFormatExport = new sc2i.win32.data.dynamic.CPanelEditFormatExport();
            this.SuspendLayout();
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(320, 296);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 7;
            this.m_btnCancel.Text = "Cancel|11";
            this.m_btnCancel.Click += new System.EventHandler(this.m_btnCancel_Click);
            // 
            // m_btnOK
            // 
            this.m_btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnOK.Location = new System.Drawing.Point(240, 296);
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.Size = new System.Drawing.Size(75, 23);
            this.m_btnOK.TabIndex = 6;
            this.m_btnOK.Text = "Ok|10";
            this.m_btnOK.Click += new System.EventHandler(this.m_btnOK_Click);
            // 
            // m_panelFormatExport
            // 
            cExporteurDatasetXML4.ExporteStructureOnly = false;
            this.m_panelFormatExport.Exporteur = cExporteurDatasetXML4;
            this.m_panelFormatExport.Location = new System.Drawing.Point(0, 0);
            this.m_panelFormatExport.Name = "m_panelFormatExport";
            this.m_panelFormatExport.SansFichier = false;
            this.m_panelFormatExport.Size = new System.Drawing.Size(648, 296);
            this.m_panelFormatExport.TabIndex = 8;
            // 
            // CFormOptionsExport
            // 
            this.AcceptButton = this.m_btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(648, 318);
            this.ControlBox = false;
            this.Controls.Add(this.m_panelFormatExport);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_btnOK);
            this.Name = "CFormOptionsExport";
            this.Text = "Export options|249";
            this.Load += new System.EventHandler(this.CFormOptionsExport_Load);
            this.ResumeLayout(false);

		}
		#endregion

		public virtual IExporteurDataset Exporteur
		{
			get
			{
				return m_panelFormatExport.Exporteur;
			}
			set
			{
				m_panelFormatExport.Exporteur = value;
			}
		}

		private void m_btnOK_Click(object sender, System.EventArgs e)
		{
			CResultAErreur result = CResultAErreur.True;

            //if ( m_panelOptions == null )
            //    return;

			if ( Exporteur == null )
				result.EmpileErreur(I.T("Incorrect export options|30034"));

			if ( !result )
			{
				CFormAlerte.Afficher(result);
				return;
			}

			DialogResult = DialogResult.OK;
			Close();
		}

		private void m_btnCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		public static bool EditeOptions ( ref IDestinationExport destination, ref IExporteurDataset exporteur )
		{
			CFormOptionsExport form = new CFormOptionsExport();
			bool bOk = form.ShowDialog() == DialogResult.OK;
			form.Exporteur = exporteur;
			if ( bOk )
			{
                //destination = new CDestinationExportFile(form.m_panelOptions.FileName);
                destination = form.m_panelFormatExport.DestinationExport; 
				exporteur = form.Exporteur;
			}
			form.Dispose();
			return bOk;
		}

        private void CFormOptionsExport_Load(object sender, EventArgs e)
        {
            sc2i.win32.common.CWin32Traducteur.Translate(this);

        }

	}
}
