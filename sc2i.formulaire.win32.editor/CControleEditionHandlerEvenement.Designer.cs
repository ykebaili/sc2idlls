namespace sc2i.formulaire.win32
{
	partial class CControleEditionHandlerEvenement
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
            this.components = new System.ComponentModel.Container();
            this.m_lblNomEvenement = new System.Windows.Forms.Label();
            this.m_txtEditFormule = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.m_séparateur = new System.Windows.Forms.PictureBox();
            this.m_tooltip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.m_séparateur)).BeginInit();
            this.SuspendLayout();
            // 
            // m_lblNomEvenement
            // 
            this.m_lblNomEvenement.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lblNomEvenement.Location = new System.Drawing.Point(0, 1);
            this.m_lblNomEvenement.Name = "m_lblNomEvenement";
            this.m_lblNomEvenement.Size = new System.Drawing.Size(92, 53);
            this.m_lblNomEvenement.TabIndex = 0;
            this.m_lblNomEvenement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_txtEditFormule
            // 
            this.m_txtEditFormule.AllowGraphic = true;
            this.m_txtEditFormule.AllowNullFormula = false;
            this.m_txtEditFormule.AllowSaisieTexte = true;
            this.m_txtEditFormule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtEditFormule.Formule = null;
            this.m_txtEditFormule.Location = new System.Drawing.Point(92, 1);
            this.m_txtEditFormule.LockEdition = false;
            this.m_txtEditFormule.LockZoneTexte = false;
            this.m_txtEditFormule.Name = "m_txtEditFormule";
            this.m_txtEditFormule.Size = new System.Drawing.Size(182, 53);
            this.m_txtEditFormule.TabIndex = 1;
            this.m_txtEditFormule.Leave += new System.EventHandler(this.m_txtEditFormule_Leave);
            // 
            // m_séparateur
            // 
            this.m_séparateur.BackColor = System.Drawing.Color.Black;
            this.m_séparateur.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_séparateur.Location = new System.Drawing.Point(0, 0);
            this.m_séparateur.Name = "m_séparateur";
            this.m_séparateur.Size = new System.Drawing.Size(274, 1);
            this.m_séparateur.TabIndex = 2;
            this.m_séparateur.TabStop = false;
            // 
            // CControleEditionHandlerEvenement
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.m_txtEditFormule);
            this.Controls.Add(this.m_lblNomEvenement);
            this.Controls.Add(this.m_séparateur);
            this.Name = "CControleEditionHandlerEvenement";
            this.Size = new System.Drawing.Size(274, 54);
            ((System.ComponentModel.ISupportInitialize)(this.m_séparateur)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label m_lblNomEvenement;
		private sc2i.win32.expression.CTextBoxZoomFormule m_txtEditFormule;
		private System.Windows.Forms.PictureBox m_séparateur;
		private System.Windows.Forms.ToolTip m_tooltip;
	}
}
