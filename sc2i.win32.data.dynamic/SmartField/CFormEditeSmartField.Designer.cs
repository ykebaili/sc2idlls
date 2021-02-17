namespace sc2i.win32.data.dynamic.SmartField
{
    partial class CFormEditeSmartField
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditeSmartField));
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_extStyle = new sc2i.win32.common.CExtStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtNomChamp = new System.Windows.Forms.TextBox();
            this.m_lblOriginal = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_txtCategorie = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 129);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(588, 48);
            this.m_extStyle.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 5;
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnAnnuler.ForeColor = System.Drawing.Color.White;
            this.m_btnAnnuler.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAnnuler.Image")));
            this.m_btnAnnuler.Location = new System.Drawing.Point(301, 2);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(40, 40);
            this.m_extStyle.SetStyleBackColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAnnuler.TabIndex = 1;
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnOk.ForeColor = System.Drawing.Color.White;
            this.m_btnOk.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOk.Image")));
            this.m_btnOk.Location = new System.Drawing.Point(247, 2);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(40, 40);
            this.m_extStyle.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 0;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 18);
            this.m_extStyle.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 6;
            this.label1.Text = "Field name|20080";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtNomChamp
            // 
            this.m_txtNomChamp.Location = new System.Drawing.Point(187, 12);
            this.m_txtNomChamp.Name = "m_txtNomChamp";
            this.m_txtNomChamp.Size = new System.Drawing.Size(389, 20);
            this.m_extStyle.SetStyleBackColor(this.m_txtNomChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtNomChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtNomChamp.TabIndex = 0;
            // 
            // m_lblOriginal
            // 
            this.m_lblOriginal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.m_lblOriginal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblOriginal.Location = new System.Drawing.Point(187, 66);
            this.m_lblOriginal.Name = "m_lblOriginal";
            this.m_lblOriginal.Size = new System.Drawing.Size(389, 50);
            this.m_extStyle.SetStyleBackColor(this.m_lblOriginal, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_lblOriginal, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lblOriginal.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(13, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 18);
            this.m_extStyle.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 6;
            this.label2.Text = "Original field|20081";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtCategorie
            // 
            this.m_txtCategorie.Location = new System.Drawing.Point(187, 37);
            this.m_txtCategorie.Name = "m_txtCategorie";
            this.m_txtCategorie.Size = new System.Drawing.Size(389, 20);
            this.m_extStyle.SetStyleBackColor(this.m_txtCategorie, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtCategorie, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtCategorie.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(13, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(166, 18);
            this.m_extStyle.SetStyleBackColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label3.TabIndex = 9;
            this.label3.Text = "Category|20086";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CFormEditeSmartField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 177);
            this.ControlBox = false;
            this.Controls.Add(this.m_txtCategorie);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_lblOriginal);
            this.Controls.Add(this.m_txtNomChamp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CFormEditeSmartField";
            this.m_extStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_extStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.Text = "Smart field|20079";
            this.Load += new System.EventHandler(this.CFormEditeSmartField_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button m_btnAnnuler;
        private System.Windows.Forms.Button m_btnOk;
        private sc2i.win32.common.CExtStyle m_extStyle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_txtNomChamp;
        private System.Windows.Forms.Label m_lblOriginal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_txtCategorie;
        private System.Windows.Forms.Label label3;
    }
}