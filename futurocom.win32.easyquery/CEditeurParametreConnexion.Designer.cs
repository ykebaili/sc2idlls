namespace futurocom.win32.easyquery
{
    partial class CEditeurParametreConnexion
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
            this.m_lblNomParametre = new System.Windows.Forms.Label();
            this.m_txtValeur = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // m_extModeEdition
            // 
            this.m_extModeEdition.ModeEdition = true;
            // 
            // m_lblNomParametre
            // 
            this.m_lblNomParametre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.m_lblNomParametre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lblNomParametre.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lblNomParametre.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_lblNomParametre, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblNomParametre.Name = "m_lblNomParametre";
            this.m_lblNomParametre.Size = new System.Drawing.Size(120, 21);
            this.m_lblNomParametre.TabIndex = 0;
            this.m_lblNomParametre.Text = "label1";
            this.m_lblNomParametre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_txtValeur
            // 
            this.m_txtValeur.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtValeur.Location = new System.Drawing.Point(120, 0);
            this.m_extModeEdition.SetModeEdition(this.m_txtValeur, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_txtValeur.Name = "m_txtValeur";
            this.m_txtValeur.Size = new System.Drawing.Size(208, 20);
            this.m_txtValeur.TabIndex = 1;
            // 
            // CEditeurParametreConnexion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_txtValeur);
            this.Controls.Add(this.m_lblNomParametre);
            this.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CEditeurParametreConnexion";
            this.Size = new System.Drawing.Size(328, 21);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_lblNomParametre;
        private System.Windows.Forms.TextBox m_txtValeur;
    }
}
