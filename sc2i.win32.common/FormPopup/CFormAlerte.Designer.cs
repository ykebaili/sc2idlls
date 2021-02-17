namespace sc2i.win32.common
{
    partial class CFormAlerte
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormAlerte));
            sc2i.win32.common.CProfilEffetFondu cProfilEffetFondu1 = new sc2i.win32.common.CProfilEffetFondu();
            this.m_panBoutons = new System.Windows.Forms.Panel();
            this.m_tt = new System.Windows.Forms.ToolTip(this.components);
            this.m_panImageHaut = new System.Windows.Forms.Panel();
            this.m_txtMessage = new System.Windows.Forms.TextBox();
            this.m_timerAutoClose = new System.Windows.Forms.Timer(this.components);
            this.m_btnGauche = new sc2i.win32.common.CDialogResultBouton();
            this.m_btnDroit = new sc2i.win32.common.CDialogResultBouton();
            this.m_btnCentre = new sc2i.win32.common.CDialogResultBouton();
            this.m_ctrlErreurs = new sc2i.win32.common.CCtrlShowListeErreurs();
            this.m_draggeurForm = new sc2i.win32.common.CDraggeurDeControl();
            this.m_effetFondu = new sc2i.win32.common.CEffetFonduPourForm();
            this.m_panBoutons.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panBoutons
            // 
            this.m_panBoutons.BackColor = System.Drawing.Color.Transparent;
            this.m_panBoutons.Controls.Add(this.m_btnGauche);
            this.m_panBoutons.Controls.Add(this.m_btnDroit);
            this.m_panBoutons.Controls.Add(this.m_btnCentre);
            this.m_panBoutons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_panBoutons.Location = new System.Drawing.Point(0, 224);
            this.m_panBoutons.Name = "m_panBoutons";
            this.m_panBoutons.Size = new System.Drawing.Size(280, 39);
            this.m_panBoutons.TabIndex = 1;
            // 
            // m_tt
            // 
            this.m_tt.AutomaticDelay = 2000;
            this.m_tt.AutoPopDelay = 5000;
            this.m_tt.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.m_tt.InitialDelay = 2000;
            this.m_tt.ReshowDelay = 400;
            this.m_tt.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Error;
            // 
            // m_panImageHaut
            // 
            this.m_panImageHaut.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_panImageHaut.BackgroundImage")));
            this.m_panImageHaut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.m_panImageHaut.Location = new System.Drawing.Point(0, 0);
            this.m_panImageHaut.Name = "m_panImageHaut";
            this.m_panImageHaut.Size = new System.Drawing.Size(43, 38);
            this.m_panImageHaut.TabIndex = 3;
            // 
            // m_txtMessage
            // 
            this.m_txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtMessage.BackColor = System.Drawing.Color.White;
            this.m_txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_txtMessage.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtMessage.Location = new System.Drawing.Point(12, 71);
            this.m_txtMessage.Multiline = true;
            this.m_txtMessage.Name = "m_txtMessage";
            this.m_txtMessage.ReadOnly = true;
            this.m_txtMessage.Size = new System.Drawing.Size(256, 147);
            this.m_txtMessage.TabIndex = 4;
            // 
            // m_timerAutoClose
            // 
            this.m_timerAutoClose.Interval = 1000;
            this.m_timerAutoClose.Tick += new System.EventHandler(this.m_timerAutoClose_Tick);
            // 
            // m_btnGauche
            // 
            this.m_btnGauche.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.m_btnGauche.Location = new System.Drawing.Point(13, 6);
            this.m_btnGauche.Name = "m_btnGauche";
            this.m_btnGauche.ResultatAssocie = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnGauche.Size = new System.Drawing.Size(75, 24);
            this.m_btnGauche.TabIndex = 5;
            this.m_btnGauche.TexteAffiche = "OK";
            // 
            // m_btnDroit
            // 
            this.m_btnDroit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.m_btnDroit.Location = new System.Drawing.Point(193, 6);
            this.m_btnDroit.Name = "m_btnDroit";
            this.m_btnDroit.ResultatAssocie = System.Windows.Forms.DialogResult.Ignore;
            this.m_btnDroit.Size = new System.Drawing.Size(75, 24);
            this.m_btnDroit.TabIndex = 5;
            this.m_btnDroit.TexteAffiche = "OK";
            // 
            // m_btnCentre
            // 
            this.m_btnCentre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.m_btnCentre.Location = new System.Drawing.Point(103, 6);
            this.m_btnCentre.Name = "m_btnCentre";
            this.m_btnCentre.ResultatAssocie = System.Windows.Forms.DialogResult.OK;
            this.m_btnCentre.Size = new System.Drawing.Size(75, 24);
            this.m_btnCentre.TabIndex = 5;
            this.m_btnCentre.TexteAffiche = "OK";
            // 
            // m_ctrlErreurs
            // 
            this.m_ctrlErreurs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ctrlErreurs.AutoScroll = true;
            this.m_ctrlErreurs.BackColor = System.Drawing.Color.Transparent;
            this.m_ctrlErreurs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_ctrlErreurs.Location = new System.Drawing.Point(12, 71);
            this.m_ctrlErreurs.Name = "m_ctrlErreurs";
            this.m_ctrlErreurs.Size = new System.Drawing.Size(256, 147);
            this.m_ctrlErreurs.TabIndex = 0;
            // 
            // m_draggeurForm
            // 
            this.m_draggeurForm.Controle = this;
            // 
            // m_effetFondu
            // 
            this.m_effetFondu.AuDessusDesAutresFenetres = false;
            this.m_effetFondu.EffetFonduFermeture = true;
            this.m_effetFondu.EffetFonduOuverture = true;
            this.m_effetFondu.Formulaire = this;
            this.m_effetFondu.IntervalImages = 5;
            this.m_effetFondu.NombreImage = 5;
            cProfilEffetFondu1.EffetActif = true;
            cProfilEffetFondu1.EffetFermeture = true;
            cProfilEffetFondu1.EffetOuverture = true;
            cProfilEffetFondu1.IntervalImages = 5;
            cProfilEffetFondu1.NombreImages = 5;
            this.m_effetFondu.Profil = cProfilEffetFondu1;
            // 
            // CFormAlerte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(280, 263);
            this.ControlBox = false;
            this.Controls.Add(this.m_panBoutons);
            this.Controls.Add(this.m_ctrlErreurs);
            this.Controls.Add(this.m_txtMessage);
            this.Controls.Add(this.m_panImageHaut);
            this.Name = "CFormAlerte";
            this.Opacity = 0;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.CFormAlerte_Load);
            this.m_panBoutons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCtrlShowListeErreurs m_ctrlErreurs;
        private System.Windows.Forms.Panel m_panBoutons;
		private CDraggeurDeControl m_draggeurForm;
		private System.Windows.Forms.ToolTip m_tt;
		private System.Windows.Forms.TextBox m_txtMessage;
		private System.Windows.Forms.Panel m_panImageHaut;
		private CDialogResultBouton m_btnCentre;
		private CDialogResultBouton m_btnGauche;
		private CDialogResultBouton m_btnDroit;
		private CEffetFonduPourForm m_effetFondu;
        private System.Windows.Forms.Timer m_timerAutoClose;

    }
}