using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using sc2i.common;
using sc2i.data.dynamic;
using sc2i.win32.common;

namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Description résumée de CFormVisualisationDataSet.
	/// </summary>
	public class CFormVisualisationDataSet : System.Windows.Forms.Form
	{
		private System.Windows.Forms.LinkLabel m_lnkExporter;
		private System.Windows.Forms.DataGrid m_dataGrid;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CFormVisualisationDataSet()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormVisualisationDataSet));
            this.m_dataGrid = new System.Windows.Forms.DataGrid();
            this.m_lnkExporter = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // m_dataGrid
            // 
            this.m_dataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_dataGrid.DataMember = "";
            this.m_dataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.m_dataGrid.Location = new System.Drawing.Point(0, 0);
            this.m_dataGrid.Name = "m_dataGrid";
            this.m_dataGrid.Size = new System.Drawing.Size(608, 344);
            this.m_dataGrid.TabIndex = 0;
            // 
            // m_lnkExporter
            // 
            this.m_lnkExporter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lnkExporter.Location = new System.Drawing.Point(504, 344);
            this.m_lnkExporter.Name = "m_lnkExporter";
            this.m_lnkExporter.Size = new System.Drawing.Size(100, 16);
            this.m_lnkExporter.TabIndex = 1;
            this.m_lnkExporter.TabStop = true;
            this.m_lnkExporter.Text = "Export|26";
            this.m_lnkExporter.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.m_lnkExporter.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkExporter_LinkClicked);
            // 
            // CFormVisualisationDataSet
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(608, 358);
            this.Controls.Add(this.m_lnkExporter);
            this.Controls.Add(this.m_dataGrid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CFormVisualisationDataSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data visualization|173";
            this.Load += new System.EventHandler(this.CFormVisualisationDataSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGrid)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		public static void AfficheDonnees ( DataSet ds )
		{
			CFormVisualisationDataSet form = new CFormVisualisationDataSet ( );
			form.m_dataGrid.DataSource = ds;
			form.ShowDialog();
			form.Dispose();
		}

		private void m_lnkExporter_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if ( !(m_dataGrid.DataSource is DataSet))
			{
				CFormAlerte.Afficher(I.T("This function cannot be used with this data set|30036"), EFormAlerteType.Erreur);
				return;
			}
			IExporteurDataset exporteur = null;
			IDestinationExport destination = null;
			if ( CFormOptionsExport.EditeOptions ( ref destination, ref exporteur ) )
			{
				if ( exporteur != null )
				{
					CResultAErreur result = exporteur.Export ( (DataSet)m_dataGrid.DataSource, destination );
					if ( !result )
					{
						CFormAlerte.Afficher ( result);
						return;
					}
					else
					{
						CFormAlerte.Afficher(I.T("Export finished|30037"));
					}
				}
			}
		}

        private void CFormVisualisationDataSet_Load(object sender, EventArgs e)
        {
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);

        }
	}
}
