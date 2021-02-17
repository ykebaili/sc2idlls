namespace sc2i.win32.data.dynamic.formulesglobale
{
    partial class CPanelEditeFormulesGlobales
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
            this.m_panelControls = new System.Windows.Forms.Panel();
            this.m_extModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.SuspendLayout();
            // 
            // m_panelControls
            // 
            this.m_panelControls.AutoScroll = true;
            this.m_panelControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelControls.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_panelControls, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelControls.Name = "m_panelControls";
            this.m_panelControls.Size = new System.Drawing.Size(356, 305);
            this.m_panelControls.TabIndex = 0;
            // 
            // m_extModeEdition
            // 
            this.m_extModeEdition.ModeEdition = true;
            // 
            // CPanelEditeFormulesGlobales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_panelControls);
            this.m_extModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CPanelEditeFormulesGlobales";
            this.Size = new System.Drawing.Size(356, 305);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_panelControls;
        private sc2i.win32.common.CExtModeEdition m_extModeEdition;

    }
}
