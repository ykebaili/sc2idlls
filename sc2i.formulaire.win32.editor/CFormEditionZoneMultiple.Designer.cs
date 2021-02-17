namespace sc2i.formulaire.win32
{
	partial class CFormEditionZoneMultiple
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

		#region Code généré par le Concepteur Windows Form

		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.m_panelSousFormulaire = new sc2i.formulaire.win32.editor.CPanelEditionFullFormulaire();
            this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.SuspendLayout();
            // 
            // m_panelSousFormulaire
            // 
            this.m_panelSousFormulaire.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_panelSousFormulaire.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelSousFormulaire.EntiteEditee = null;
            this.m_panelSousFormulaire.ForeColor = System.Drawing.Color.Black;
            this.m_panelSousFormulaire.FournisseurProprietes = null;
            this.m_panelSousFormulaire.Location = new System.Drawing.Point(0, 0);
            this.m_panelSousFormulaire.LockEdition = false;
            this.m_panelSousFormulaire.Name = "m_panelSousFormulaire";
            this.m_panelSousFormulaire.Size = new System.Drawing.Size(792, 566);
            this.cExtStyle1.SetStyleBackColor(this.m_panelSousFormulaire, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.m_panelSousFormulaire, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.m_panelSousFormulaire.TabIndex = 0;
            this.m_panelSousFormulaire.TypeEdite = null;
            this.m_panelSousFormulaire.WndEditee = null;
            // 
            // CFormEditionZoneMultiple
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.m_panelSousFormulaire);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CFormEditionZoneMultiple";
            this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Sub form|20001";
            this.Load += new System.EventHandler(this.CFormEditionZoneMultiple_Load);
            this.ResumeLayout(false);

		}

		#endregion

		private sc2i.formulaire.win32.editor.CPanelEditionFullFormulaire m_panelSousFormulaire;
		private sc2i.win32.common.CExtStyle cExtStyle1;
	}
}