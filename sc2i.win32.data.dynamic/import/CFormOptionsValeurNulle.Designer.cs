namespace sc2i.win32.data.dynamic.import
{
    partial class CFormOptionsValeursNulle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormOptionsValeursNulle));
            this.m_chkNullSiErreurConversion = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtValeursNulles = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_chkNullSiErreurConversion
            // 
            this.m_chkNullSiErreurConversion.AutoSize = true;
            this.m_chkNullSiErreurConversion.Location = new System.Drawing.Point(126, 3);
            this.m_chkNullSiErreurConversion.Name = "m_chkNullSiErreurConversion";
            this.m_chkNullSiErreurConversion.Size = new System.Drawing.Size(198, 17);
            this.cExtStyle1.SetStyleBackColor(this.m_chkNullSiErreurConversion, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_chkNullSiErreurConversion, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkNullSiErreurConversion.TabIndex = 0;
            this.m_chkNullSiErreurConversion.Text = "Set null value on version error|20160";
            this.m_chkNullSiErreurConversion.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 171);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(336, 48);
            this.cExtStyle1.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 12;
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnAnnuler.ForeColor = System.Drawing.Color.White;
            this.m_btnAnnuler.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAnnuler.Image")));
            this.m_btnAnnuler.Location = new System.Drawing.Point(175, 2);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(40, 40);
            this.cExtStyle1.SetStyleBackColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAnnuler.TabIndex = 3;
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnOk.ForeColor = System.Drawing.Color.White;
            this.m_btnOk.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOk.Image")));
            this.m_btnOk.Location = new System.Drawing.Point(121, 2);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(40, 40);
            this.cExtStyle1.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 2;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 20);
            this.cExtStyle1.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 5;
            this.label1.Text = "Null values|20161";
            // 
            // m_txtValeursNulles
            // 
            this.m_txtValeursNulles.AcceptsReturn = true;
            this.m_txtValeursNulles.Location = new System.Drawing.Point(126, 27);
            this.m_txtValeursNulles.Multiline = true;
            this.m_txtValeursNulles.Name = "m_txtValeursNulles";
            this.m_txtValeursNulles.Size = new System.Drawing.Size(198, 138);
            this.cExtStyle1.SetStyleBackColor(this.m_txtValeursNulles, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtValeursNulles, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtValeursNulles.TabIndex = 1;
            // 
            // CFormOptionsValeursNulle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 219);
            this.ControlBox = false;
            this.Controls.Add(this.m_txtValeursNulles);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_chkNullSiErreurConversion);
            this.ForeColor = System.Drawing.Color.Black;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CFormOptionsValeursNulle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Null values options|20162";
            this.Load += new System.EventHandler(this.CFormOptionsValeursNulle_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox m_chkNullSiErreurConversion;
        private common.CExtStyle cExtStyle1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button m_btnAnnuler;
        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_txtValeursNulles;
    }
}