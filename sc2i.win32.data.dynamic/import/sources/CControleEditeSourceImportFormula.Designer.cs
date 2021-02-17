namespace sc2i.win32.data.dynamic.import.sources
{
    partial class CControleEditeSourceImportFormula
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
            this.m_txtFormule = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.m_lblFormule = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_txtFormule
            // 
            this.m_txtFormule.AllowGraphic = true;
            this.m_txtFormule.AllowNullFormula = false;
            this.m_txtFormule.AllowSaisieTexte = true;
            this.m_txtFormule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtFormule.Formule = null;
            this.m_txtFormule.Location = new System.Drawing.Point(56, 0);
            this.m_txtFormule.LockEdition = false;
            this.m_txtFormule.LockZoneTexte = false;
            this.m_txtFormule.Name = "m_txtFormule";
            this.m_txtFormule.Size = new System.Drawing.Size(158, 27);
            this.m_txtFormule.TabIndex = 0;
            this.m_txtFormule.OnChangeTexteFormule += new System.EventHandler(this.m_txtFormule_OnChangeTexteFormule);
            // 
            // m_lblFormule
            // 
            this.m_lblFormule.BackColor = System.Drawing.Color.White;
            this.m_lblFormule.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblFormule.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lblFormule.Location = new System.Drawing.Point(0, 0);
            this.m_lblFormule.Name = "m_lblFormule";
            this.m_lblFormule.Size = new System.Drawing.Size(56, 27);
            this.m_lblFormule.TabIndex = 1;
            this.m_lblFormule.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CControleEditeSourceImportFormula
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_txtFormule);
            this.Controls.Add(this.m_lblFormule);
            this.Name = "CControleEditeSourceImportFormula";
            this.Size = new System.Drawing.Size(214, 27);
            this.ResumeLayout(false);

        }

        #endregion

        private expression.CTextBoxZoomFormule m_txtFormule;
        private System.Windows.Forms.Label m_lblFormule;
    }
}
