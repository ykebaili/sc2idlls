namespace sc2i.win32.data.Package
{
    partial class CFormParametresRecherche
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
            this.m_panelOptions = new sc2i.win32.data.Package.CPanelOptionRechercheDependances();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnSaveConfig = new System.Windows.Forms.Button();
            this.m_btnLoadConfig = new System.Windows.Forms.Button();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnValider = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panelOptions
            // 
            this.m_panelOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelOptions.Location = new System.Drawing.Point(0, 0);
            this.m_panelOptions.Name = "m_panelOptions";
            this.m_panelOptions.Size = new System.Drawing.Size(663, 458);
            this.m_panelOptions.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnSaveConfig);
            this.panel1.Controls.Add(this.m_btnLoadConfig);
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnValider);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 458);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(663, 38);
            this.panel1.TabIndex = 1;
            // 
            // m_btnSaveConfig
            // 
            this.m_btnSaveConfig.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.m_btnSaveConfig.Image = global::sc2i.win32.data.Properties.Resources._1421186080_save_24;
            this.m_btnSaveConfig.Location = new System.Drawing.Point(605, 3);
            this.m_btnSaveConfig.Name = "m_btnSaveConfig";
            this.m_btnSaveConfig.Size = new System.Drawing.Size(50, 32);
            this.m_btnSaveConfig.TabIndex = 3;
            this.m_btnSaveConfig.UseVisualStyleBackColor = true;
            this.m_btnSaveConfig.Click += new System.EventHandler(this.m_btnSaveConfig_Click);
            // 
            // m_btnLoadConfig
            // 
            this.m_btnLoadConfig.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.m_btnLoadConfig.Image = global::sc2i.win32.data.Properties.Resources._1421185988_Open_file;
            this.m_btnLoadConfig.Location = new System.Drawing.Point(549, 3);
            this.m_btnLoadConfig.Name = "m_btnLoadConfig";
            this.m_btnLoadConfig.Size = new System.Drawing.Size(50, 32);
            this.m_btnLoadConfig.TabIndex = 2;
            this.m_btnLoadConfig.UseVisualStyleBackColor = true;
            this.m_btnLoadConfig.Click += new System.EventHandler(this.m_btnLoadConfig_Click);
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_btnAnnuler.Image = global::sc2i.win32.data.Properties.Resources.Ignorer;
            this.m_btnAnnuler.Location = new System.Drawing.Point(350, 3);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(50, 32);
            this.m_btnAnnuler.TabIndex = 1;
            this.m_btnAnnuler.UseVisualStyleBackColor = true;
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // m_btnValider
            // 
            this.m_btnValider.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_btnValider.Image = global::sc2i.win32.data.Properties.Resources.Forcer;
            this.m_btnValider.Location = new System.Drawing.Point(263, 3);
            this.m_btnValider.Name = "m_btnValider";
            this.m_btnValider.Size = new System.Drawing.Size(50, 32);
            this.m_btnValider.TabIndex = 0;
            this.m_btnValider.UseVisualStyleBackColor = true;
            this.m_btnValider.Click += new System.EventHandler(this.m_btnValider_Click);
            // 
            // CFormParametresRecherche
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 496);
            this.Controls.Add(this.m_panelOptions);
            this.Controls.Add(this.panel1);
            this.Name = "CFormParametresRecherche";
            this.Text = "Search parameters|20010";
            this.Load += new System.EventHandler(this.CFormParametresRecherche_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CPanelOptionRechercheDependances m_panelOptions;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button m_btnAnnuler;
        private System.Windows.Forms.Button m_btnValider;
        private System.Windows.Forms.Button m_btnSaveConfig;
        private System.Windows.Forms.Button m_btnLoadConfig;
    }
}