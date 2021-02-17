namespace sc2i.win32.data.dynamic.import.sources
{
    partial class CControleEditeSourceImportObjetRef
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
            this.m_cmbObjet = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // m_cmbObjet
            // 
            this.m_cmbObjet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_cmbObjet.FormattingEnabled = true;
            this.m_cmbObjet.Location = new System.Drawing.Point(0, 0);
            this.m_cmbObjet.Name = "m_cmbObjet";
            this.m_cmbObjet.Size = new System.Drawing.Size(237, 21);
            this.m_cmbObjet.TabIndex = 0;
            this.m_cmbObjet.SelectionChangeCommitted += new System.EventHandler(this.m_cmbField_SelectionChangeCommitted);
            // 
            // CControleEditeSourceImportObjetRef
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_cmbObjet);
            this.Name = "CControleEditeSourceImportObjetRef";
            this.Size = new System.Drawing.Size(237, 25);
            this.Load += new System.EventHandler(this.CControleEditeSourceImportField_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox m_cmbObjet;
    }
}
