namespace sc2i.formulaire.win32
{
    partial class CDelayedButton
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
            if (m_timer != null)
                m_timer.Stop();
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            m_timer = null;
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
            this.m_button = new System.Windows.Forms.Button();
            this.m_panelFondProgress = new System.Windows.Forms.Panel();
            this.m_lblProgress = new System.Windows.Forms.Label();
            this.m_timer = new System.Windows.Forms.Timer(this.components);
            this.m_panelFondProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_button
            // 
            this.m_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_button.FlatAppearance.BorderSize = 0;
            this.m_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_button.Location = new System.Drawing.Point(0, 0);
            this.m_button.Name = "m_button";
            this.m_button.Size = new System.Drawing.Size(448, 27);
            this.m_button.TabIndex = 1;
            this.m_button.Text = "button1";
            this.m_button.UseVisualStyleBackColor = true;
            this.m_button.Click += new System.EventHandler(this.m_button_Click);
            // 
            // m_panelFondProgress
            // 
            this.m_panelFondProgress.BackColor = System.Drawing.Color.Red;
            this.m_panelFondProgress.Controls.Add(this.m_lblProgress);
            this.m_panelFondProgress.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_panelFondProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_panelFondProgress.Location = new System.Drawing.Point(0, 27);
            this.m_panelFondProgress.Name = "m_panelFondProgress";
            this.m_panelFondProgress.Size = new System.Drawing.Size(448, 4);
            this.m_panelFondProgress.TabIndex = 2;
            this.m_panelFondProgress.Click += new System.EventHandler(this.m_panelFondProgress_Click);
            // 
            // m_lblProgress
            // 
            this.m_lblProgress.BackColor = System.Drawing.Color.Lime;
            this.m_lblProgress.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lblProgress.Location = new System.Drawing.Point(0, 0);
            this.m_lblProgress.Name = "m_lblProgress";
            this.m_lblProgress.Size = new System.Drawing.Size(35, 4);
            this.m_lblProgress.TabIndex = 0;
            this.m_lblProgress.Click += new System.EventHandler(this.m_panelFondProgress_Click);
            // 
            // m_timer
            // 
            this.m_timer.Interval = 1000;
            this.m_timer.Tick += new System.EventHandler(this.m_timer_Tick);
            // 
            // CDelayedButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_button);
            this.Controls.Add(this.m_panelFondProgress);
            this.Name = "CDelayedButton";
            this.Size = new System.Drawing.Size(448, 31);
            this.BackColorChanged += new System.EventHandler(this.CDelayedButton_BackColorChanged);
            this.EnabledChanged += new System.EventHandler(this.CDelayedButton_EnabledChanged);
            this.FontChanged += new System.EventHandler(this.CDelayedButton_FontChanged);
            this.ForeColorChanged += new System.EventHandler(this.CDelayedButton_ForeColorChanged);
            this.m_panelFondProgress.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button m_button;
        private System.Windows.Forms.Panel m_panelFondProgress;
        private System.Windows.Forms.Label m_lblProgress;
        private System.Windows.Forms.Timer m_timer;
    }
}
