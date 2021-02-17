namespace sc2i.win32.data.dynamic.StructureImport
{
    partial class CAssistantImporterDonnees
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CAssistantImporterDonnees));
            this.m_panelStructure = new sc2i.win32.data.dynamic.StructureImport.CPanelEditStructureImport();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_lnkDemarrer = new System.Windows.Forms.LinkLabel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panelStructure
            // 
            this.m_panelStructure.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_panelStructure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelStructure.ForeColor = System.Drawing.Color.Black;
            this.m_panelStructure.Location = new System.Drawing.Point(0, 0);
            this.m_panelStructure.Name = "m_panelStructure";
            this.m_panelStructure.Size = new System.Drawing.Size(632, 402);
            this.cExtStyle1.SetStyleBackColor(this.m_panelStructure, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelStructure, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelStructure.TabIndex = 0;
            this.m_panelStructure.Load += new System.EventHandler(this.m_panelStructure_Load);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.m_lnkDemarrer);
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(0, 402);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(632, 41);
            this.cExtStyle1.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.cExtStyle1.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.panel1.TabIndex = 1;
            // 
            // m_lnkDemarrer
            // 
            this.m_lnkDemarrer.AutoSize = true;
            this.m_lnkDemarrer.Location = new System.Drawing.Point(12, 3);
            this.m_lnkDemarrer.Name = "m_lnkDemarrer";
            this.m_lnkDemarrer.Size = new System.Drawing.Size(92, 13);
            this.cExtStyle1.SetStyleBackColor(this.m_lnkDemarrer, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_lnkDemarrer, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lnkDemarrer.TabIndex = 6;
            this.m_lnkDemarrer.TabStop = true;
            this.m_lnkDemarrer.Text = "Start import|20047";
            this.m_lnkDemarrer.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkDemarrer_LinkClicked);
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnAnnuler.ForeColor = System.Drawing.Color.White;
            this.m_btnAnnuler.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAnnuler.Image")));
            this.m_btnAnnuler.Location = new System.Drawing.Point(580, 0);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(40, 40);
            this.cExtStyle1.SetStyleBackColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAnnuler.TabIndex = 5;
            // 
            // CAssistantImporterDonnees
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(632, 443);
            this.Controls.Add(this.m_panelStructure);
            this.Controls.Add(this.panel1);
            this.Name = "CAssistantImporterDonnees";
            this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.Text = "Import data|20046";
            this.Load += new System.EventHandler(this.CAssistantImporterDeDonnees_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CPanelEditStructureImport m_panelStructure;
        private sc2i.win32.common.CExtStyle cExtStyle1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button m_btnAnnuler;
        private System.Windows.Forms.LinkLabel m_lnkDemarrer;
    }
}