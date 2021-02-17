namespace sc2i.win32.data.dynamic
{
    partial class CControlPanorama
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
            this.m_panelTop = new System.Windows.Forms.Panel();
            this.m_lblTitreGroupe = new System.Windows.Forms.Label();
            this.m_panelGlobal = new System.Windows.Forms.Panel();
            this.m_gestionnaireModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panelTop
            // 
            this.m_panelTop.Controls.Add(this.m_lblTitreGroupe);
            this.m_panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelTop.Location = new System.Drawing.Point(0, 0);
            this.m_panelTop.Name = "m_panelTop";
            this.m_panelTop.Size = new System.Drawing.Size(416, 20);
            this.m_panelTop.TabIndex = 0;
            // 
            // m_lblTitreGroupe
            // 
            this.m_lblTitreGroupe.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lblTitreGroupe.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblTitreGroupe.Location = new System.Drawing.Point(0, 0);
            this.m_lblTitreGroupe.Name = "m_lblTitreGroupe";
            this.m_lblTitreGroupe.Size = new System.Drawing.Size(233, 20);
            this.m_lblTitreGroupe.TabIndex = 0;
            this.m_lblTitreGroupe.Text = "Dynamic Title";
            // 
            // m_panelGlobal
            // 
            this.m_panelGlobal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelGlobal.Location = new System.Drawing.Point(0, 20);
            this.m_panelGlobal.Name = "m_panelGlobal";
            this.m_panelGlobal.Size = new System.Drawing.Size(416, 142);
            this.m_panelGlobal.TabIndex = 1;
            this.m_panelGlobal.Paint += new System.Windows.Forms.PaintEventHandler(this.m_panelGlobal_Paint);
            // 
            // m_gestionnaireModeEdition
            // 
            this.m_gestionnaireModeEdition.ModeEdition = true;
            // 
            // CControlPanorama
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_panelGlobal);
            this.Controls.Add(this.m_panelTop);
            this.m_gestionnaireModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CControlPanorama";
            this.Size = new System.Drawing.Size(416, 162);
            this.SizeChanged += new System.EventHandler(this.CControlPanorama_SizeChanged);
            this.m_panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        

        #endregion

        private System.Windows.Forms.Panel m_panelTop;
        private System.Windows.Forms.Panel m_panelGlobal;
        private System.Windows.Forms.Label m_lblTitreGroupe;
        private sc2i.win32.common.CExtModeEdition m_gestionnaireModeEdition;
    }
}
