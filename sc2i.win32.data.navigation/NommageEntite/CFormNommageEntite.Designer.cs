namespace sc2i.win32.data.navigation
{
    partial class CFormNommageEntite
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
            this.c2iPanelOmbre1 = new sc2i.win32.common.C2iPanelOmbre();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_lnkAjouter = new sc2i.win32.common.CWndLinkStd();
            this.m_panelControlsSaisie = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_lblEntite = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_panelBoutons = new System.Windows.Forms.Panel();
            this.m_btnOK = new System.Windows.Forms.Button();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.c2iPanelOmbre1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.m_panelBoutons.SuspendLayout();
            this.SuspendLayout();
            // 
            // c2iPanelOmbre1
            // 
            this.c2iPanelOmbre1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.c2iPanelOmbre1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iPanelOmbre1.Controls.Add(this.panel2);
            this.c2iPanelOmbre1.ForeColor = System.Drawing.Color.Black;
            this.c2iPanelOmbre1.Location = new System.Drawing.Point(7, 8);
            this.c2iPanelOmbre1.LockEdition = false;
            this.c2iPanelOmbre1.Name = "c2iPanelOmbre1";
            this.c2iPanelOmbre1.Size = new System.Drawing.Size(432, 305);
            this.c2iPanelOmbre1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.m_lnkAjouter);
            this.panel2.Controls.Add(this.m_panelControlsSaisie);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(410, 286);
            this.panel2.TabIndex = 12;
            // 
            // m_lnkAjouter
            // 
            this.m_lnkAjouter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkAjouter.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkAjouter.Location = new System.Drawing.Point(5, 38);
            this.m_lnkAjouter.Name = "m_lnkAjouter";
            this.m_lnkAjouter.Size = new System.Drawing.Size(112, 24);
            this.m_lnkAjouter.TabIndex = 13;
            this.m_lnkAjouter.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_lnkAjouter.LinkClicked += new System.EventHandler(this.m_lnkAjouter_LinkClicked);
            // 
            // m_panelControlsSaisie
            // 
            this.m_panelControlsSaisie.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelControlsSaisie.Location = new System.Drawing.Point(19, 68);
            this.m_panelControlsSaisie.Name = "m_panelControlsSaisie";
            this.m_panelControlsSaisie.Size = new System.Drawing.Size(385, 215);
            this.m_panelControlsSaisie.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_lblEntite);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(410, 32);
            this.panel1.TabIndex = 11;
            // 
            // m_lblEntite
            // 
            this.m_lblEntite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblEntite.BackColor = System.Drawing.Color.White;
            this.m_lblEntite.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblEntite.Location = new System.Drawing.Point(103, 4);
            this.m_lblEntite.Name = "m_lblEntite";
            this.m_lblEntite.Size = new System.Drawing.Size(304, 23);
            this.m_lblEntite.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(2, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Named Entity|123";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_panelBoutons
            // 
            this.m_panelBoutons.Controls.Add(this.m_btnOK);
            this.m_panelBoutons.Controls.Add(this.m_btnAnnuler);
            this.m_panelBoutons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_panelBoutons.Location = new System.Drawing.Point(0, 313);
            this.m_panelBoutons.Name = "m_panelBoutons";
            this.m_panelBoutons.Size = new System.Drawing.Size(442, 41);
            this.m_panelBoutons.TabIndex = 4;
            // 
            // m_btnOK
            // 
            this.m_btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.m_btnOK.FlatAppearance.BorderSize = 0;
            this.m_btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.m_btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.m_btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnOK.Image = global::sc2i.win32.data.navigation.Properties.Resources.Valider;
            this.m_btnOK.Location = new System.Drawing.Point(187, 6);
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.Size = new System.Drawing.Size(26, 26);
            this.m_btnOK.TabIndex = 1;
            this.m_btnOK.UseVisualStyleBackColor = true;
            this.m_btnOK.Click += new System.EventHandler(this.m_btnOK_Click);
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnAnnuler.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.FlatAppearance.BorderSize = 0;
            this.m_btnAnnuler.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.m_btnAnnuler.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.m_btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnAnnuler.Image = global::sc2i.win32.data.navigation.Properties.Resources.Annuler;
            this.m_btnAnnuler.Location = new System.Drawing.Point(228, 6);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(26, 26);
            this.m_btnAnnuler.TabIndex = 1;
            this.m_btnAnnuler.UseVisualStyleBackColor = true;
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // CFormNommageEntite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(442, 354);
            this.Controls.Add(this.m_panelBoutons);
            this.Controls.Add(this.c2iPanelOmbre1);
            this.Name = "CFormNommageEntite";
            this.Text = "Naming Entity|10112";
            this.Load += new System.EventHandler(this.CFormNommageEntite_Load);
            this.c2iPanelOmbre1.ResumeLayout(false);
            this.c2iPanelOmbre1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.m_panelBoutons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private sc2i.win32.common.C2iPanelOmbre c2iPanelOmbre1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label m_lblEntite;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel m_panelBoutons;
        private System.Windows.Forms.Button m_btnOK;
        private System.Windows.Forms.Button m_btnAnnuler;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel m_panelControlsSaisie;
        private sc2i.win32.common.CWndLinkStd m_lnkAjouter;
    }
}