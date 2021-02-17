namespace sc2i.win32.common
{
    partial class CSlidingZone
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
            this.m_titleBar = new sc2i.win32.common.CDegradeTitleBar();
            this.m_gestionnaireModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_timerAutoResize = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // m_titleBar
            // 
            this.m_titleBar.BackColorGradient = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(210)))), ((int)(((byte)(224)))));
            this.m_titleBar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_titleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_titleBar.IsCollapse = false;
            this.m_titleBar.Location = new System.Drawing.Point(0, 0);
            this.m_titleBar.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_titleBar, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_titleBar.Name = "m_titleBar";
            this.m_titleBar.Size = new System.Drawing.Size(237, 18);
            this.m_titleBar.TabIndex = 0;
            this.m_titleBar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_titleBar.Click += new System.EventHandler(this.m_titleBar_Click);
            // 
            // m_gestionnaireModeEdition
            // 
            this.m_gestionnaireModeEdition.ModeEdition = true;
            // 
            // m_timerAutoResize
            // 
            this.m_timerAutoResize.Tick += new System.EventHandler(this.m_timerAutoResize_Tick);
            // 
            // CSlidingZone
            // 
            this.AllowDrop = true;
            this.Controls.Add(this.m_titleBar);
            this.m_gestionnaireModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CSlidingZone";
            this.Size = new System.Drawing.Size(237, 198);
            this.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.CSlidingZone_ControlAdded);
            this.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.CSlidingZone_ControlRemoved);
            this.ResumeLayout(false);

        }

        #endregion

        private CDegradeTitleBar m_titleBar;
		private CExtModeEdition m_gestionnaireModeEdition;
        private System.Windows.Forms.Timer m_timerAutoResize;
    }
}
