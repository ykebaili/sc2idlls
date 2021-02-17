namespace sc2i.win32.process
{
    partial class CFormEditionDefinisseurEvenementPopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditionDefinisseurEvenementPopup));
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_panelEvenement = new sc2i.win32.process.CPanelDefinisseurEvenements();
            this.m_extStyle = new sc2i.win32.common.CExtStyle();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 457);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(735, 48);
            this.m_extStyle.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 2;
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnAnnuler.ForeColor = System.Drawing.Color.White;
            this.m_btnAnnuler.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAnnuler.Image")));
            this.m_btnAnnuler.Location = new System.Drawing.Point(374, 2);
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
            this.m_btnOk.Location = new System.Drawing.Point(320, 2);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(40, 40);
            this.m_extStyle.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 2;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_panelEvenement
            // 
            this.m_panelEvenement.BackColor = System.Drawing.Color.White;
            this.m_panelEvenement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelEvenement.ForeColor = System.Drawing.Color.Black;
            this.m_panelEvenement.Location = new System.Drawing.Point(0, 0);
            this.m_panelEvenement.LockEdition = false;
            this.m_panelEvenement.Name = "m_panelEvenement";
            this.m_panelEvenement.Size = new System.Drawing.Size(735, 457);
            this.m_extStyle.SetStyleBackColor(this.m_panelEvenement, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelEvenement, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelEvenement.TabIndex = 3;
            // 
            // CFormEditionDefinisseurEvenementPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 505);
            this.Controls.Add(this.m_panelEvenement);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CFormEditionDefinisseurEvenementPopup";
            this.m_extStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.m_extStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Event|20099";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CFormEditionDefinisseurEvenementPopup_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button m_btnAnnuler;
        private sc2i.win32.common.CExtStyle m_extStyle;
        private System.Windows.Forms.Button m_btnOk;
        private sc2i.win32.process.CPanelDefinisseurEvenements m_panelEvenement;
    }
}