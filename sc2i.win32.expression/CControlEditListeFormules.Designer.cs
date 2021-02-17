namespace sc2i.win32.expression
{
    partial class CControlEditListeFormules
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
            this.m_panelFormules = new System.Windows.Forms.Panel();
            this.m_panelTop = new System.Windows.Forms.Panel();
            this.m_lnkAjouter = new sc2i.win32.common.CWndLinkStd();
            this.m_lnkSupprimer = new sc2i.win32.common.CWndLinkStd();
            this.m_panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panelFormules
            // 
            this.m_panelFormules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelFormules.Location = new System.Drawing.Point(0, 28);
            this.m_panelFormules.Name = "m_panelFormules";
            this.m_panelFormules.Size = new System.Drawing.Size(512, 221);
            this.m_panelFormules.TabIndex = 0;
            // 
            // m_panelTop
            // 
            this.m_panelTop.Controls.Add(this.m_lnkSupprimer);
            this.m_panelTop.Controls.Add(this.m_lnkAjouter);
            this.m_panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelTop.Location = new System.Drawing.Point(0, 0);
            this.m_panelTop.Name = "m_panelTop";
            this.m_panelTop.Size = new System.Drawing.Size(512, 28);
            this.m_panelTop.TabIndex = 1;
            // 
            // m_lnkAjouter
            // 
            this.m_lnkAjouter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkAjouter.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkAjouter.Location = new System.Drawing.Point(14, 4);
            this.m_lnkAjouter.Name = "m_lnkAjouter";
            this.m_lnkAjouter.Size = new System.Drawing.Size(112, 24);
            this.m_lnkAjouter.TabIndex = 0;
            this.m_lnkAjouter.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_lnkAjouter.LinkClicked += new System.EventHandler(this.m_lnkAjouter_LinkClicked);
            // 
            // m_lnkSupprimer
            // 
            this.m_lnkSupprimer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkSupprimer.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkSupprimer.Location = new System.Drawing.Point(200, 2);
            this.m_lnkSupprimer.Name = "m_lnkSupprimer";
            this.m_lnkSupprimer.Size = new System.Drawing.Size(112, 24);
            this.m_lnkSupprimer.TabIndex = 1;
            this.m_lnkSupprimer.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_lnkSupprimer.LinkClicked += new System.EventHandler(this.m_lnkSupprimer_LinkClicked);
            // 
            // CControlEditListeFormules
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.Controls.Add(this.m_panelFormules);
            this.Controls.Add(this.m_panelTop);
            this.Name = "CControlEditListeFormules";
            this.Size = new System.Drawing.Size(512, 249);
            this.m_panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_panelFormules;
        private System.Windows.Forms.Panel m_panelTop;
        private sc2i.win32.common.CWndLinkStd m_lnkSupprimer;
        private sc2i.win32.common.CWndLinkStd m_lnkAjouter;
    }
}
