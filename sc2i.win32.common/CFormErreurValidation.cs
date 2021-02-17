using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using sc2i.common;

namespace sc2i.win32.common
{
	/// <summary>
	/// Description résumée de CFormErreurValidation.
	/// </summary>
	//
	public class CFormErreurValidation : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView m_listeErreurs;
		private System.Windows.Forms.LinkLabel m_btnCorriger;
		private System.Windows.Forms.LinkLabel m_btnIgnorer;
		private System.Windows.Forms.ImageList m_images;
		private System.Windows.Forms.ColumnHeader colErreur;
		private System.ComponentModel.IContainer components;

		private CPileErreur m_erreurs = null;

		private Decisions m_decision = Decisions.Corriger;

		public enum Decisions
		{
			Corriger = 0,
			Ignorer = 1
		}


		public CFormErreurValidation()
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormErreurValidation));
            this.m_listeErreurs = new System.Windows.Forms.ListView();
            this.colErreur = new System.Windows.Forms.ColumnHeader();
            this.m_images = new System.Windows.Forms.ImageList(this.components);
            this.m_btnCorriger = new System.Windows.Forms.LinkLabel();
            this.m_btnIgnorer = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // m_listeErreurs
            // 
            this.m_listeErreurs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_listeErreurs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colErreur});
            this.m_listeErreurs.Location = new System.Drawing.Point(0, 0);
            this.m_listeErreurs.Name = "m_listeErreurs";
            this.m_listeErreurs.Size = new System.Drawing.Size(488, 248);
            this.m_listeErreurs.SmallImageList = this.m_images;
            this.m_listeErreurs.TabIndex = 0;
            this.m_listeErreurs.UseCompatibleStateImageBehavior = false;
            this.m_listeErreurs.View = System.Windows.Forms.View.Details;
            this.m_listeErreurs.Resize += new System.EventHandler(this.m_listeErreurs_Resize);
            this.m_listeErreurs.SelectedIndexChanged += new System.EventHandler(this.m_listeErreurs_SelectedIndexChanged);
            // 
            // colErreur
            // 
            this.colErreur.Text = "Erreur";
            this.colErreur.Width = 471;
            // 
            // m_images
            // 
            this.m_images.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_images.ImageStream")));
            this.m_images.TransparentColor = System.Drawing.Color.Transparent;
            this.m_images.Images.SetKeyName(0, "");
            this.m_images.Images.SetKeyName(1, "");
            // 
            // m_btnCorriger
            // 
            this.m_btnCorriger.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnCorriger.Location = new System.Drawing.Point(136, 256);
            this.m_btnCorriger.Name = "m_btnCorriger";
            this.m_btnCorriger.Size = new System.Drawing.Size(88, 24);
            this.m_btnCorriger.TabIndex = 1;
            this.m_btnCorriger.TabStop = true;
            this.m_btnCorriger.Text = "Modify|13";
            this.m_btnCorriger.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_btnCorriger_LinkClicked);
            // 
            // m_btnIgnorer
            // 
            this.m_btnIgnorer.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnIgnorer.Location = new System.Drawing.Point(240, 256);
            this.m_btnIgnorer.Name = "m_btnIgnorer";
            this.m_btnIgnorer.Size = new System.Drawing.Size(112, 24);
            this.m_btnIgnorer.TabIndex = 2;
            this.m_btnIgnorer.TabStop = true;
            this.m_btnIgnorer.Text = "Ignore and validate|14";
            this.m_btnIgnorer.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_btnIgnorer_LinkClicked);
            // 
            // CFormErreurValidation
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(488, 286);
            this.Controls.Add(this.m_btnIgnorer);
            this.Controls.Add(this.m_btnCorriger);
            this.Controls.Add(this.m_listeErreurs);
            this.MinimizeBox = false;
            this.Name = "CFormErreurValidation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Erreurs dans les données|105";
            this.Load += new System.EventHandler(this.CFormErreurValidation_Load);
            this.ResumeLayout(false);

		}
		#endregion

		private void m_listeErreurs_Resize(object sender, System.EventArgs e)
		{
			m_listeErreurs.Columns[colErreur.Index].Width = Math.Max(m_listeErreurs.Width-16,128);
		}

		public static Decisions AfficheErreurs ( CPileErreur pile )
		{
			CFormErreurValidation form = new CFormErreurValidation();
			form.m_erreurs = pile;
			Decisions decision = Decisions.Corriger;
			if ( form.ShowDialog() == DialogResult.OK )
			{
				decision = form.m_decision;
			}
			form.Dispose();
			return decision;
		}

		private void m_listeErreurs_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		}

		private void CFormErreurValidation_Load(object sender, System.EventArgs e)
		{
            // Lance la traduction du formulaire
            CWin32Traducteur.Translate(this);
            
            if (m_erreurs == null)
			{
				m_btnIgnorer.Visible = false;
				return;
			}
			bool bCanIgnorer = true;
			foreach ( IErreur erreur in m_erreurs )
			{
				bool bIsAlert = false;
				if ( erreur is CErreurValidation )
				{
					if ( !((CErreurValidation)erreur).IsAvertissement )
						bCanIgnorer = false;
					else bIsAlert = true;
				}
				else
					bCanIgnorer = false;
				ListViewItem item = new ListViewItem(erreur.Message, bIsAlert?0:1);
				m_listeErreurs.Items.Add ( item );

			}
			m_btnIgnorer.Visible = bCanIgnorer;
		}

		private void m_btnCorriger_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			m_decision = Decisions.Corriger;
			DialogResult = DialogResult.OK;
			Close();

		}

		private void m_btnIgnorer_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			m_decision = Decisions.Ignorer;
			DialogResult = DialogResult.OK;
			Close();
		}


	}
}
