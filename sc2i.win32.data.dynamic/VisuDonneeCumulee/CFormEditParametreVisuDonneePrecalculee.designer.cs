namespace sc2i.win32.data.dynamic
{
    partial class CFormEditParametreVisuDonneePrecalculee
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditParametreVisuDonneePrecalculee));
            this.m_panelParametres = new sc2i.win32.data.dynamic.CPanelEditParametreVisuDonneePrecalculee();
            this.m_panelBas = new System.Windows.Forms.Panel();
            this.m_lnkTester = new System.Windows.Forms.LinkLabel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_panelBas.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panelParametres
            // 
            this.m_panelParametres.BackColor = System.Drawing.Color.White;
            this.m_panelParametres.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelParametres.ForeColor = System.Drawing.Color.Black;
            this.m_panelParametres.Location = new System.Drawing.Point(0, 0);
            this.m_panelParametres.LockEdition = false;
            this.m_panelParametres.Name = "m_panelParametres";
            this.m_panelParametres.Size = new System.Drawing.Size(881, 397);
            this.m_panelParametres.TabIndex = 0;
            // 
            // m_panelBas
            // 
            this.m_panelBas.Controls.Add(this.m_lnkTester);
            this.m_panelBas.Controls.Add(this.m_btnAnnuler);
            this.m_panelBas.Controls.Add(this.m_btnOk);
            this.m_panelBas.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_panelBas.Location = new System.Drawing.Point(0, 397);
            this.m_panelBas.Name = "m_panelBas";
            this.m_panelBas.Size = new System.Drawing.Size(881, 48);
            this.m_panelBas.TabIndex = 1;
            // 
            // m_lnkTester
            // 
            this.m_lnkTester.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lnkTester.AutoSize = true;
            this.m_lnkTester.Location = new System.Drawing.Point(513, 18);
            this.m_lnkTester.Name = "m_lnkTester";
            this.m_lnkTester.Size = new System.Drawing.Size(60, 13);
            this.m_lnkTester.TabIndex = 6;
            this.m_lnkTester.TabStop = true;
            this.m_lnkTester.Text = "Test|20034";
            this.m_lnkTester.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkTester_LinkClicked);
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnAnnuler.ForeColor = System.Drawing.Color.White;
            this.m_btnAnnuler.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAnnuler.Image")));
            this.m_btnAnnuler.Location = new System.Drawing.Point(447, 4);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(40, 40);
            this.m_btnAnnuler.TabIndex = 5;
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnOk.ForeColor = System.Drawing.Color.White;
            this.m_btnOk.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOk.Image")));
            this.m_btnOk.Location = new System.Drawing.Point(393, 4);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(40, 40);
            this.m_btnOk.TabIndex = 4;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // CFormEditParametreVisuDonneePrecalculee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(881, 445);
            this.Controls.Add(this.m_panelParametres);
            this.Controls.Add(this.m_panelBas);
            this.Name = "CFormEditParametreVisuDonneePrecalculee";
            this.Text = "Visualization Parameters|20035";
            this.Load += new System.EventHandler(this.CFormEditParametreVisuDonneePrecalculee_Load);
            this.m_panelBas.ResumeLayout(false);
            this.m_panelBas.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CPanelEditParametreVisuDonneePrecalculee m_panelParametres;
        private System.Windows.Forms.Panel m_panelBas;
        private System.Windows.Forms.Button m_btnAnnuler;
        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.LinkLabel m_lnkTester;
    }
}