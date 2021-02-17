namespace sc2i.win32.data.dynamic.formulesglobale
{
    partial class CFormEditeFormulesGlobales
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditeFormulesGlobales));
            this.m_panelFormules = new sc2i.win32.data.dynamic.formulesglobale.CPanelEditeFormulesGlobales();
            this.m_panelBas = new System.Windows.Forms.Panel();
            this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_panelBas.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panelFormules
            // 
            this.m_panelFormules.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_panelFormules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelFormules.ForeColor = System.Drawing.Color.Black;
            this.m_panelFormules.Location = new System.Drawing.Point(0, 0);
            this.m_panelFormules.LockEdition = false;
            this.m_panelFormules.Name = "m_panelFormules";
            this.m_panelFormules.Size = new System.Drawing.Size(500, 197);
            this.cExtStyle1.SetStyleBackColor(this.m_panelFormules, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.m_panelFormules, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.m_panelFormules.TabIndex = 0;
            // 
            // m_panelBas
            // 
            this.m_panelBas.Controls.Add(this.m_btnAnnuler);
            this.m_panelBas.Controls.Add(this.m_btnOk);
            this.m_panelBas.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_panelBas.Location = new System.Drawing.Point(0, 197);
            this.m_panelBas.Name = "m_panelBas";
            this.m_panelBas.Size = new System.Drawing.Size(500, 55);
            this.cExtStyle1.SetStyleBackColor(this.m_panelBas, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelBas, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelBas.TabIndex = 1;
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnAnnuler.ForeColor = System.Drawing.Color.White;
            this.m_btnAnnuler.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAnnuler.Image")));
            this.m_btnAnnuler.Location = new System.Drawing.Point(254, 6);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(40, 40);
            this.cExtStyle1.SetStyleBackColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAnnuler.TabIndex = 5;
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnOk.ForeColor = System.Drawing.Color.White;
            this.m_btnOk.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOk.Image")));
            this.m_btnOk.Location = new System.Drawing.Point(200, 6);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(40, 40);
            this.cExtStyle1.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 4;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // CFormEditeFormulesGlobales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 252);
            this.Controls.Add(this.m_panelFormules);
            this.Controls.Add(this.m_panelBas);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CFormEditeFormulesGlobales";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Global formulas|20062";
            this.m_panelBas.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CPanelEditeFormulesGlobales m_panelFormules;
        private sc2i.win32.common.CExtStyle cExtStyle1;
        private System.Windows.Forms.Panel m_panelBas;
        private System.Windows.Forms.Button m_btnAnnuler;
        private System.Windows.Forms.Button m_btnOk;
    }
}