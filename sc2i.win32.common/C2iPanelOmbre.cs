using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace sc2i.win32.common
{
	/// <summary>
	/// Description résumée de C2iPanelOmbre.
	/// </summary>
	public class C2iPanelOmbre : /*UserControl*/sc2i.win32.common.C2iPanel
	{
		private System.Windows.Forms.PictureBox m_pictDroite;
		private System.Windows.Forms.PictureBox m_pictBasDroite;
		private System.Windows.Forms.PictureBox m_pictBas;
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public C2iPanelOmbre()
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();


			// TODO : ajoutez les initialisations après l'appel à InitForm

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(C2iPanelOmbre));
			this.m_pictDroite = new System.Windows.Forms.PictureBox();
			this.m_pictBasDroite = new System.Windows.Forms.PictureBox();
			this.m_pictBas = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.m_pictDroite)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.m_pictBasDroite)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.m_pictBas)).BeginInit();
			this.SuspendLayout();
			// 
			// m_pictDroite
			// 
			this.m_pictDroite.Image = ((System.Drawing.Image)(resources.GetObject("m_pictDroite.Image")));
			this.m_pictDroite.Location = new System.Drawing.Point(30, 130);
			this.m_pictDroite.Name = "m_pictDroite";
			this.m_pictDroite.Size = new System.Drawing.Size(16, 800);
			this.m_pictDroite.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.m_pictDroite.TabIndex = 0;
			this.m_pictDroite.TabStop = false;
			// 
			// m_pictBasDroite
			// 
			this.m_pictBasDroite.Image = ((System.Drawing.Image)(resources.GetObject("m_pictBasDroite.Image")));
			this.m_pictBasDroite.Location = new System.Drawing.Point(17, 17);
			this.m_pictBasDroite.Name = "m_pictBasDroite";
			this.m_pictBasDroite.Size = new System.Drawing.Size(16, 16);
			this.m_pictBasDroite.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.m_pictBasDroite.TabIndex = 2;
			this.m_pictBasDroite.TabStop = false;
			// 
			// m_pictBas
			// 
			this.m_pictBas.Image = ((System.Drawing.Image)(resources.GetObject("m_pictBas.Image")));
			this.m_pictBas.Location = new System.Drawing.Point(132, 17);
			this.m_pictBas.Name = "m_pictBas";
			this.m_pictBas.Size = new System.Drawing.Size(1800, 16);
			this.m_pictBas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.m_pictBas.TabIndex = 2;
			this.m_pictBas.TabStop = false;
			// 
			// C2iPanelOmbre
			// 
			this.Controls.Add(this.m_pictBasDroite);
			this.Controls.Add(this.m_pictBas);
			this.Controls.Add(this.m_pictDroite);
			this.Size = new System.Drawing.Size(224, 192);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.C2iPanelOmbre_Paint);
			this.Resize += new System.EventHandler(this.C2iPanelOmbre_SizeChanged);
			this.SizeChanged += new System.EventHandler(this.C2iPanelOmbre_SizeChanged);
			((System.ComponentModel.ISupportInitialize)(this.m_pictDroite)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_pictBasDroite)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_pictBas)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion


		/////////////////////////////////////////////////////
		private void AjusteImages()
		{
		}

        //--------------------------------------------------------------------------
        /// <summary>
		/// /////////////////////
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void C2iPanelOmbre_SizeChanged(object sender, System.EventArgs e)
		{
            UpdateOmbres();
		}

        //--------------------------------------------------------------------------
        private void C2iPanelOmbre_Paint(object sender, PaintEventArgs e)
        {
            UpdateOmbres();
        }

        //--------------------------------------------------------------------------
        private void UpdateOmbres()
        {
            if (m_pictBas != null)
            {
                m_pictDroite.Top = 0;
                m_pictDroite.Left = ClientSize.Width - m_pictDroite.Width;
            }
            if (m_pictBasDroite != null)
            {
                m_pictBasDroite.Top = ClientSize.Height - m_pictBasDroite.Height;
                m_pictBasDroite.Left = ClientSize.Width - m_pictBasDroite.Width;
            }
            if (m_pictBas != null)
            {
                m_pictBas.Top = ClientSize.Height - m_pictBas.Height;
                m_pictBas.Left = 0;
            }
        }

        //--------------------------------------------------------------------------
        public void SaveImages()
		{
			m_pictBas.Image.Save ( "c:\\Bas.bmp", System.Drawing.Imaging.ImageFormat.Bmp );
			m_pictDroite.Image.Save ( "c:\\Droite.bmp", System.Drawing.Imaging.ImageFormat.Bmp );
			m_pictBasDroite.Image.Save ( "c:\\BasDroite.bmp", System.Drawing.Imaging.ImageFormat.Bmp );
		}


	}
}
