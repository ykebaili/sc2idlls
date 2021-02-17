namespace futurocom.win32.chart.series
{
    partial class CControleEditeFournisseurValeursSerieFormule
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_chkForEach = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_txtFormule
            // 
            this.m_txtFormule.AllowGraphic = true;
            this.m_txtFormule.AllowNullFormula = false;
            this.m_txtFormule.AllowSaisieTexte = true;
            this.m_txtFormule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtFormule.Formule = null;
            this.m_txtFormule.Location = new System.Drawing.Point(0, 21);
            this.m_txtFormule.LockEdition = false;
            this.m_txtFormule.LockZoneTexte = false;
            this.m_txtFormule.Name = "m_txtFormule";
            this.m_txtFormule.Size = new System.Drawing.Size(517, 52);
            this.m_txtFormule.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_chkForEach);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(517, 21);
            this.panel1.TabIndex = 1;
            // 
            // m_chkForEach
            // 
            this.m_chkForEach.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_chkForEach.Location = new System.Drawing.Point(0, 0);
            this.m_chkForEach.Name = "m_chkForEach";
            this.m_chkForEach.Size = new System.Drawing.Size(370, 21);
            this.m_chkForEach.TabIndex = 0;
            this.m_chkForEach.Text = "Evaluate for each source element|20013";
            this.m_chkForEach.UseVisualStyleBackColor = true;
            // 
            // CControleEditeFournisseurValeursSerieFormule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_txtFormule);
            this.Controls.Add(this.panel1);
            this.Name = "CControleEditeFournisseurValeursSerieFormule";
            this.Size = new System.Drawing.Size(517, 73);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private sc2i.win32.expression.CTextBoxZoomFormule m_txtFormule;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox m_chkForEach;
    }
}
