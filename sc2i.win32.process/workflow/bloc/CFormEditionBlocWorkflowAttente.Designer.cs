namespace sc2i.win32.process.workflow.bloc
{
    partial class CFormEditionBlocWorkflowAttente
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditionBlocWorkflowAttente));
            this.m_extStyle = new sc2i.win32.common.CExtStyle();
            this.m_txtFormuleElementEdite = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.label1 = new System.Windows.Forms.Label();
            this.c2iPanelOmbre1 = new sc2i.win32.common.C2iPanelOmbre();
            this.m_panelParametreDeclenchement = new sc2i.win32.process.CPanelEditParametreDeclencheur();
            this.m_panelGauche = new sc2i.win32.common.C2iPanel(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_menuFormulaires = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.c2iPanelOmbre1.SuspendLayout();
            this.m_panelParametreDeclenchement.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_txtFormuleElementEdite
            // 
            this.m_txtFormuleElementEdite.AllowGraphic = true;
            this.m_txtFormuleElementEdite.AllowNullFormula = false;
            this.m_txtFormuleElementEdite.AllowSaisieTexte = true;
            this.m_txtFormuleElementEdite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormuleElementEdite.Formule = null;
            this.m_txtFormuleElementEdite.Location = new System.Drawing.Point(186, 0);
            this.m_txtFormuleElementEdite.LockEdition = false;
            this.m_txtFormuleElementEdite.LockZoneTexte = false;
            this.m_txtFormuleElementEdite.Name = "m_txtFormuleElementEdite";
            this.m_txtFormuleElementEdite.Size = new System.Drawing.Size(612, 20);
            this.m_extStyle.SetStyleBackColor(this.m_txtFormuleElementEdite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtFormuleElementEdite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtFormuleElementEdite.TabIndex = 0;
            this.m_txtFormuleElementEdite.Validated += new System.EventHandler(this.m_txtFormuleElementEdite_Validated);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 16);
            this.m_extStyle.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 1;
            this.label1.Text = "target element|20116";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // c2iPanelOmbre1
            // 
            this.c2iPanelOmbre1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iPanelOmbre1.Controls.Add(this.m_panelParametreDeclenchement);
            this.c2iPanelOmbre1.Controls.Add(this.panel3);
            this.c2iPanelOmbre1.Dock = System.Windows.Forms.DockStyle.Top;
            this.c2iPanelOmbre1.ForeColor = System.Drawing.Color.Black;
            this.c2iPanelOmbre1.Location = new System.Drawing.Point(0, 0);
            this.c2iPanelOmbre1.LockEdition = false;
            this.c2iPanelOmbre1.Name = "c2iPanelOmbre1";
            this.c2iPanelOmbre1.Size = new System.Drawing.Size(824, 362);
            this.m_extStyle.SetStyleBackColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_extStyle.SetStyleForeColor(this.c2iPanelOmbre1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.c2iPanelOmbre1.TabIndex = 3;
            // 
            // m_panelParametreDeclenchement
            // 
            this.m_panelParametreDeclenchement.AutoriseSurCreation = false;
            this.m_panelParametreDeclenchement.AutoriseSurDate = true;
            this.m_panelParametreDeclenchement.AutoriseSurManuel = true;
            this.m_panelParametreDeclenchement.AutoriseSurModification = true;
            this.m_panelParametreDeclenchement.AutoriseSurSuppression = false;
            this.m_panelParametreDeclenchement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(187)))), ((int)(((byte)(255)))));
            this.m_panelParametreDeclenchement.Controls.Add(this.m_panelGauche);
            this.m_panelParametreDeclenchement.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelParametreDeclenchement.Location = new System.Drawing.Point(0, 25);
            this.m_panelParametreDeclenchement.LockEdition = false;
            this.m_panelParametreDeclenchement.Name = "m_panelParametreDeclenchement";
            this.m_panelParametreDeclenchement.Size = new System.Drawing.Size(824, 280);
            this.m_extStyle.SetStyleBackColor(this.m_panelParametreDeclenchement, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelParametreDeclenchement, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelParametreDeclenchement.TabIndex = 7;
            this.m_panelParametreDeclenchement.TypeCible = typeof(string);
            // 
            // m_panelGauche
            // 
            this.m_panelGauche.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(187)))), ((int)(((byte)(255)))));
            this.m_panelGauche.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelGauche.Location = new System.Drawing.Point(0, 0);
            this.m_panelGauche.LockEdition = false;
            this.m_panelGauche.Name = "m_panelGauche";
            this.m_panelGauche.Size = new System.Drawing.Size(824, 280);
            this.m_extStyle.SetStyleBackColor(this.m_panelGauche, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelGauche, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelGauche.TabIndex = 4004;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.m_txtFormuleElementEdite);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(824, 25);
            this.m_extStyle.SetStyleBackColor(this.panel3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.panel3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel3.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 361);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(824, 48);
            this.m_extStyle.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 4;
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnAnnuler.ForeColor = System.Drawing.Color.White;
            this.m_btnAnnuler.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAnnuler.Image")));
            this.m_btnAnnuler.Location = new System.Drawing.Point(419, 2);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(40, 40);
            this.m_extStyle.SetStyleBackColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAnnuler.TabIndex = 3;
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnOk.ForeColor = System.Drawing.Color.White;
            this.m_btnOk.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOk.Image")));
            this.m_btnOk.Location = new System.Drawing.Point(365, 2);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(40, 40);
            this.m_extStyle.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 2;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_menuFormulaires
            // 
            this.m_menuFormulaires.Name = "m_menuSousTypes";
            this.m_menuFormulaires.Size = new System.Drawing.Size(61, 4);
            this.m_extStyle.SetStyleBackColor(this.m_menuFormulaires, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_menuFormulaires, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            // 
            // CFormEditionBlocWorkflowAttente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 409);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.c2iPanelOmbre1);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "CFormEditionBlocWorkflowAttente";
            this.m_extStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.m_extStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Wait event|20117";
            this.Load += new System.EventHandler(this.CFormEditionBlocWorkflowAttente_Load);
            this.c2iPanelOmbre1.ResumeLayout(false);
            this.c2iPanelOmbre1.PerformLayout();
            this.m_panelParametreDeclenchement.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private sc2i.win32.common.CExtStyle m_extStyle;
        private sc2i.win32.expression.CTextBoxZoomFormule m_txtFormuleElementEdite;
        private System.Windows.Forms.Label label1;
        private sc2i.win32.common.C2iPanelOmbre c2iPanelOmbre1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button m_btnAnnuler;
        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ContextMenuStrip m_menuFormulaires;
        private CPanelEditParametreDeclencheur m_panelParametreDeclenchement;
        private sc2i.win32.common.C2iPanel m_panelGauche;
    }
}