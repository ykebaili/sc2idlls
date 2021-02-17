namespace sc2i.win32.common
{
    partial class CVEarthCtrl
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
            this.m_browser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // m_browser
            // 
            this.m_browser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_browser.Location = new System.Drawing.Point(0, 0);
            this.m_browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.m_browser.Name = "m_browser";
            this.m_browser.Size = new System.Drawing.Size(499, 379);
            this.m_browser.TabIndex = 0;
            this.m_browser.SizeChanged += new System.EventHandler(this.m_browser_SizeChanged);
            // 
            // VEarthCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.m_browser);
            this.Name = "VEarthCtrl";
            this.Size = new System.Drawing.Size(499, 379);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser m_browser;
    }
}
