namespace sc2i.win32.expression
{
    partial class CEditeurParametresFormule
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
            this.m_panelControls = new System.Windows.Forms.Panel();
            this.m_panelTop = new System.Windows.Forms.Panel();
            this.m_txtFormule = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.m_timerDelayUpdate = new System.Windows.Forms.Timer(this.components);
            this.m_timerDelayInterprete = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // m_panelControls
            // 
            this.m_panelControls.AutoScroll = true;
            this.m_panelControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelControls.Location = new System.Drawing.Point(0, 90);
            this.m_panelControls.Name = "m_panelControls";
            this.m_panelControls.Size = new System.Drawing.Size(342, 123);
            this.m_panelControls.TabIndex = 0;
            // 
            // m_panelTop
            // 
            this.m_panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelTop.Location = new System.Drawing.Point(0, 0);
            this.m_panelTop.Name = "m_panelTop";
            this.m_panelTop.Size = new System.Drawing.Size(342, 25);
            this.m_panelTop.TabIndex = 0;
            this.m_panelTop.Visible = false;
            // 
            // m_txtFormule
            // 
            this.m_txtFormule.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_txtFormule.Formule = null;
            this.m_txtFormule.Location = new System.Drawing.Point(0, 25);
            this.m_txtFormule.LockEdition = false;
            this.m_txtFormule.LockZoneTexte = false;
            this.m_txtFormule.Name = "m_txtFormule";
            this.m_txtFormule.Size = new System.Drawing.Size(342, 62);
            this.m_txtFormule.TabIndex = 1;
            this.m_txtFormule.OnChangeTexteFormule += new System.EventHandler(this.m_txtFormule_OnChangeTexteFormule);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 87);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(342, 3);
            this.splitter1.TabIndex = 0;
            this.splitter1.TabStop = false;
            // 
            // m_timerDelayUpdate
            // 
            this.m_timerDelayUpdate.Interval = 500;
            this.m_timerDelayUpdate.Tick += new System.EventHandler(this.m_timerDelayUpdate_Tick);
            // 
            // m_timerDelayInterprete
            // 
            this.m_timerDelayInterprete.Interval = 1000;
            this.m_timerDelayInterprete.Tick += new System.EventHandler(this.m_timerDelayInterprete_Tick);
            // 
            // CEditeurParametresFormule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_panelControls);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.m_txtFormule);
            this.Controls.Add(this.m_panelTop);
            this.Name = "CEditeurParametresFormule";
            this.Size = new System.Drawing.Size(342, 213);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_panelControls;
        private System.Windows.Forms.Panel m_panelTop;
        private sc2i.win32.expression.CTextBoxZoomFormule m_txtFormule;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Timer m_timerDelayUpdate;
        private System.Windows.Forms.Timer m_timerDelayInterprete;
    }
}
