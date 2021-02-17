namespace sc2i.formulaire.win32.controles2iWnd
{
	partial class CPanelChildElement
	{
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Nettoyage des ressources utilisées.
		/// </summary>
		/// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Code généré par le Concepteur de composants

		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.m_lnkDelete = new sc2i.win32.common.CWndLinkStd();
            this.m_panelDelete = new System.Windows.Forms.Panel();
            this.m_gestionnaireModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_panelSousFormulaire = new sc2i.formulaire.win32.controles2iWnd.CPanelSousFormulaire();
            this.m_panelDelete.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_lnkDelete
            // 
            this.m_lnkDelete.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_lnkDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkDelete.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkDelete.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkDelete, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lnkDelete.Name = "m_lnkDelete";
            this.m_lnkDelete.Size = new System.Drawing.Size(23, 22);
            this.m_lnkDelete.TabIndex = 1;
            this.m_lnkDelete.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_lnkDelete.LinkClicked += new System.EventHandler(this.m_lnkDelete_LinkClicked);
            // 
            // m_panelDelete
            // 
            this.m_panelDelete.Controls.Add(this.m_lnkDelete);
            this.m_panelDelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_panelDelete.Location = new System.Drawing.Point(310, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelDelete, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_panelDelete.Name = "m_panelDelete";
            this.m_panelDelete.Size = new System.Drawing.Size(25, 128);
            this.m_panelDelete.TabIndex = 2;
            this.m_panelDelete.Visible = false;
            // 
            // m_panelSousFormulaire
            // 
            this.m_panelSousFormulaire.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelSousFormulaire.Location = new System.Drawing.Point(0, 0);
            this.m_panelSousFormulaire.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelSousFormulaire, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelSousFormulaire.Name = "m_panelSousFormulaire";
            this.m_panelSousFormulaire.Size = new System.Drawing.Size(310, 128);
            this.m_panelSousFormulaire.TabIndex = 0;
            m_panelSousFormulaire.AdjustSizeToFormulaire = true;
            this.m_panelSousFormulaire.EnabledChanged += new System.EventHandler(this.m_panelSousFormulaire_EnabledChanged);
            this.m_panelSousFormulaire.SizeChanged += new System.EventHandler(this.m_panelSousFormulaire_SizeChanged);
            // 
            // CPanelChildElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.m_panelSousFormulaire);
            this.Controls.Add(this.m_panelDelete);
            this.m_gestionnaireModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CPanelChildElement";
            this.Size = new System.Drawing.Size(335, 128);
            this.m_panelDelete.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private CPanelSousFormulaire m_panelSousFormulaire;
		private sc2i.win32.common.CWndLinkStd m_lnkDelete;
		private System.Windows.Forms.Panel m_panelDelete;
		private sc2i.win32.common.CExtModeEdition m_gestionnaireModeEdition;
	}
}
