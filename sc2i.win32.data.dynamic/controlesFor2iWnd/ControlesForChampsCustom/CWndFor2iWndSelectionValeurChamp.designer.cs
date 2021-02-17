namespace sc2i.win32.data.dynamic
{
    partial class CWndFor2iWndSelectionValeurChamp
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
            this.m_panorama = new sc2i.win32.common.CControlPanorama();
            this.m_gestionnaireModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.SuspendLayout();
            // 
            // m_panorama
            // 
            this.m_panorama.ButtonColor = System.Drawing.Color.LightGreen;
            this.m_panorama.ButtonHeight = 70;
            this.m_panorama.ButtonHorizontalMargin = 3;
            this.m_panorama.ButtonVerticalMargin = 3;
            this.m_panorama.ButtonWidth = 100;
            this.m_panorama.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panorama.Location = new System.Drawing.Point(0, 0);
            this.m_panorama.LockEdition = false;
            this.m_panorama.MaxLineCount = 1;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panorama, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_panorama.Name = "m_panorama";
            this.m_panorama.Size = new System.Drawing.Size(468, 142);
            this.m_panorama.TabIndex = 0;
            this.m_panorama.OnSelectObject += new sc2i.win32.common.OnSelectObjectEventHandler(this.m_panorama_OnSelectObject);
            this.m_panorama.OnCalcButtonText += new sc2i.win32.common.OnCalcButtonTextEventHandler(this.m_panorama_OnCalcButtonText);
            // 
            // m_gestionnaireModeEdition
            // 
            this.m_gestionnaireModeEdition.ModeEdition = true;
            // 
            // CWndFor2iWndSelectionValeurChamp
            // 
            this.Controls.Add(this.m_panorama);
            this.m_gestionnaireModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CWndFor2iWndSelectionValeurChamp";
            this.Size = new System.Drawing.Size(468, 142);
            this.ResumeLayout(false);

        }

        #endregion

        private sc2i.win32.common.CControlPanorama m_panorama;
        private sc2i.win32.common.CExtModeEdition m_gestionnaireModeEdition;




    }
}
