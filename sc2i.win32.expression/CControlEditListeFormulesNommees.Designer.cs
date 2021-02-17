namespace sc2i.win32.expression
{
    partial class CControlEditListeFormulesNommees
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
            this.m_panelTop = new System.Windows.Forms.Panel();
            this.m_lnkSupprimer = new sc2i.win32.common.CWndLinkStd();
            this.m_lnkAjouter = new sc2i.win32.common.CWndLinkStd();
            this.m_gestionnaireModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_panelFormules = new sc2i.win32.common.C2iPanel(this.components);
            this.m_panelHeader = new System.Windows.Forms.Panel();
            this.m_lblFormule = new System.Windows.Forms.Label();
            this.m_lblLibelle = new System.Windows.Forms.Label();
            this.m_btnCopy = new System.Windows.Forms.PictureBox();
            this.m_btnPaste = new System.Windows.Forms.PictureBox();
            this.m_panelTop.SuspendLayout();
            this.m_panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnCopy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnPaste)).BeginInit();
            this.SuspendLayout();
            // 
            // m_panelTop
            // 
            this.m_panelTop.Controls.Add(this.m_btnCopy);
            this.m_panelTop.Controls.Add(this.m_btnPaste);
            this.m_panelTop.Controls.Add(this.m_lnkSupprimer);
            this.m_panelTop.Controls.Add(this.m_lnkAjouter);
            this.m_panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelTop.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelTop, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelTop.Name = "m_panelTop";
            this.m_panelTop.Size = new System.Drawing.Size(512, 28);
            this.m_panelTop.TabIndex = 1;
            // 
            // m_lnkSupprimer
            // 
            this.m_lnkSupprimer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkSupprimer.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkSupprimer.Location = new System.Drawing.Point(200, 2);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkSupprimer, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_lnkSupprimer.Name = "m_lnkSupprimer";
            this.m_lnkSupprimer.Size = new System.Drawing.Size(112, 24);
            this.m_lnkSupprimer.TabIndex = 1;
            this.m_lnkSupprimer.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_lnkSupprimer.LinkClicked += new System.EventHandler(this.m_lnkSupprimer_LinkClicked);
            // 
            // m_lnkAjouter
            // 
            this.m_lnkAjouter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkAjouter.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkAjouter.Location = new System.Drawing.Point(14, 4);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkAjouter, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_lnkAjouter.Name = "m_lnkAjouter";
            this.m_lnkAjouter.Size = new System.Drawing.Size(112, 24);
            this.m_lnkAjouter.TabIndex = 0;
            this.m_lnkAjouter.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_lnkAjouter.LinkClicked += new System.EventHandler(this.m_lnkAjouter_LinkClicked);
            // 
            // m_gestionnaireModeEdition
            // 
            this.m_gestionnaireModeEdition.ModeEdition = true;
            // 
            // m_panelFormules
            // 
            this.m_panelFormules.AutoScroll = true;
            this.m_panelFormules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelFormules.Location = new System.Drawing.Point(0, 56);
            this.m_panelFormules.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelFormules, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_panelFormules.Name = "m_panelFormules";
            this.m_panelFormules.Size = new System.Drawing.Size(512, 193);
            this.m_panelFormules.TabIndex = 2;
            // 
            // m_panelHeader
            // 
            this.m_panelHeader.Controls.Add(this.m_lblFormule);
            this.m_panelHeader.Controls.Add(this.m_lblLibelle);
            this.m_panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelHeader.Location = new System.Drawing.Point(0, 28);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelHeader, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelHeader.Name = "m_panelHeader";
            this.m_panelHeader.Size = new System.Drawing.Size(512, 28);
            this.m_panelHeader.TabIndex = 0;
            this.m_panelHeader.Visible = false;
            // 
            // m_lblFormule
            // 
            this.m_lblFormule.BackColor = System.Drawing.Color.White;
            this.m_lblFormule.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lblFormule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblFormule.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblFormule.Location = new System.Drawing.Point(160, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lblFormule, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblFormule.Name = "m_lblFormule";
            this.m_lblFormule.Size = new System.Drawing.Size(352, 28);
            this.m_lblFormule.TabIndex = 1;
            this.m_lblFormule.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblLibelle
            // 
            this.m_lblLibelle.BackColor = System.Drawing.Color.White;
            this.m_lblLibelle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lblLibelle.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lblLibelle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblLibelle.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lblLibelle, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblLibelle.Name = "m_lblLibelle";
            this.m_lblLibelle.Size = new System.Drawing.Size(160, 28);
            this.m_lblLibelle.TabIndex = 0;
            this.m_lblLibelle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_btnCopy
            // 
            this.m_btnCopy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnCopy.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnCopy.Image = global::sc2i.win32.expression.Resource1.copy;
            this.m_btnCopy.Location = new System.Drawing.Point(454, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnCopy, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnCopy.Name = "m_btnCopy";
            this.m_btnCopy.Size = new System.Drawing.Size(29, 28);
            this.m_btnCopy.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.m_btnCopy.TabIndex = 9;
            this.m_btnCopy.TabStop = false;
            this.m_btnCopy.Click += new System.EventHandler(this.m_btnCopy_Click);
            // 
            // m_btnPaste
            // 
            this.m_btnPaste.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnPaste.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnPaste.Image = global::sc2i.win32.expression.Resource1.paste;
            this.m_btnPaste.Location = new System.Drawing.Point(483, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnPaste, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnPaste.Name = "m_btnPaste";
            this.m_btnPaste.Size = new System.Drawing.Size(29, 28);
            this.m_btnPaste.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.m_btnPaste.TabIndex = 10;
            this.m_btnPaste.TabStop = false;
            this.m_btnPaste.Click += new System.EventHandler(this.m_btnPaste_Click);
            // 
            // CControlEditListeFormulesNommees
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.Controls.Add(this.m_panelFormules);
            this.Controls.Add(this.m_panelHeader);
            this.Controls.Add(this.m_panelTop);
            this.m_gestionnaireModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CControlEditListeFormulesNommees";
            this.Size = new System.Drawing.Size(512, 249);
            this.m_panelTop.ResumeLayout(false);
            this.m_panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_btnCopy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnPaste)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_panelTop;
        private sc2i.win32.common.CWndLinkStd m_lnkSupprimer;
        private sc2i.win32.common.CWndLinkStd m_lnkAjouter;
        private sc2i.win32.common.CExtModeEdition m_gestionnaireModeEdition;
        private sc2i.win32.common.C2iPanel m_panelFormules;
        private System.Windows.Forms.Panel m_panelHeader;
        private System.Windows.Forms.Label m_lblFormule;
        private System.Windows.Forms.Label m_lblLibelle;
        private System.Windows.Forms.PictureBox m_btnCopy;
        private System.Windows.Forms.PictureBox m_btnPaste;
    }
}
