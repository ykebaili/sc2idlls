namespace sc2i.formulaire.win32
{
	partial class CPanelAffectationPropriete
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
            this.m_libelle = new System.Windows.Forms.Label();
            this.m_txtFormule = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.SuspendLayout();
            // 
            // m_libelle
            // 
            this.m_libelle.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_libelle.Location = new System.Drawing.Point(0, 0);
            this.m_libelle.Name = "m_libelle";
            this.m_libelle.Size = new System.Drawing.Size(139, 22);
            this.m_libelle.TabIndex = 0;
            // 
            // m_txtFormule
            // 
            this.m_txtFormule.AllowGraphic = true;
            this.m_txtFormule.AllowNullFormula = false;
            this.m_txtFormule.AllowSaisieTexte = true;
            this.m_txtFormule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtFormule.Formule = null;
            this.m_txtFormule.Location = new System.Drawing.Point(142, 0);
            this.m_txtFormule.LockEdition = false;
            this.m_txtFormule.LockZoneTexte = false;
            this.m_txtFormule.Name = "m_txtFormule";
            this.m_txtFormule.Size = new System.Drawing.Size(219, 22);
            this.m_txtFormule.TabIndex = 1;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(139, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 22);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // CPanelAffectationPropriete
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.m_txtFormule);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.m_libelle);
            this.Name = "CPanelAffectationPropriete";
            this.Size = new System.Drawing.Size(361, 22);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label m_libelle;
		private sc2i.win32.expression.CTextBoxZoomFormule m_txtFormule;
        private System.Windows.Forms.Splitter splitter1;
	}
}
