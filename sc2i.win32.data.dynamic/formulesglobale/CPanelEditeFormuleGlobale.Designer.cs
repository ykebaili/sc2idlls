namespace sc2i.win32.data.dynamic.formulesglobale
{
    partial class CPanelEditeFormuleGlobale
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
            this.m_lblLibelle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtFormule = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.m_extModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.SuspendLayout();
            // 
            // m_lblLibelle
            // 
            this.m_lblLibelle.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lblLibelle.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_lblLibelle, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblLibelle.Name = "m_lblLibelle";
            this.m_lblLibelle.Size = new System.Drawing.Size(234, 32);
            this.m_lblLibelle.TabIndex = 0;
            this.m_lblLibelle.Text = "label1";
            this.m_lblLibelle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(0, 32);
            this.m_extModeEdition.SetModeEdition(this.label1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(540, 1);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // m_txtFormule
            // 
            this.m_txtFormule.AllowGraphic = true;
            this.m_txtFormule.AllowNullFormula = true;
            this.m_txtFormule.AllowSaisieTexte = true;
            this.m_txtFormule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtFormule.Formule = null;
            this.m_txtFormule.Location = new System.Drawing.Point(234, 0);
            this.m_txtFormule.LockEdition = false;
            this.m_txtFormule.LockZoneTexte = false;
            this.m_extModeEdition.SetModeEdition(this.m_txtFormule, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_txtFormule.Name = "m_txtFormule";
            this.m_txtFormule.Size = new System.Drawing.Size(306, 32);
            this.m_txtFormule.TabIndex = 2;
            // 
            // m_extModeEdition
            // 
            this.m_extModeEdition.ModeEdition = true;
            // 
            // CPanelEditeFormuleGlobale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_txtFormule);
            this.Controls.Add(this.m_lblLibelle);
            this.Controls.Add(this.label1);
            this.m_extModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CPanelEditeFormuleGlobale";
            this.Size = new System.Drawing.Size(540, 33);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label m_lblLibelle;
        private sc2i.win32.common.CExtModeEdition m_extModeEdition;
        private System.Windows.Forms.Label label1;
        private sc2i.win32.expression.CTextBoxZoomFormule m_txtFormule;
    }
}
