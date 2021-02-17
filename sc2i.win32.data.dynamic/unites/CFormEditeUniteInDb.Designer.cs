namespace sc2i.win32.data.dynamic.unites
{
    partial class CFormEditeUniteInDb
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditeUniteInDb));
            this.m_panelUnite = new sc2i.win32.common.C2iPanel(this.components);
            this.m_cmbClasse = new sc2i.win32.common.CComboboxAutoFilled();
            this.m_txtOffsetConversion = new sc2i.win32.common.C2iTextBoxNumerique();
            this.m_txtFacteurConversion = new sc2i.win32.common.C2iTextBoxNumerique();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.m_lblConversion = new System.Windows.Forms.Label();
            this.m_txtLibelleLongUnite = new sc2i.win32.common.C2iTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.m_txtLibellCourtUnite = new sc2i.win32.common.C2iTextBox();
            this.m_txtIdUnite = new sc2i.win32.common.C2iTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_extStyle = new sc2i.win32.common.CExtStyle();
            this.m_panelUnite.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panelUnite
            // 
            this.m_panelUnite.Controls.Add(this.m_cmbClasse);
            this.m_panelUnite.Controls.Add(this.m_txtOffsetConversion);
            this.m_panelUnite.Controls.Add(this.m_txtFacteurConversion);
            this.m_panelUnite.Controls.Add(this.label9);
            this.m_panelUnite.Controls.Add(this.label8);
            this.m_panelUnite.Controls.Add(this.m_lblConversion);
            this.m_panelUnite.Controls.Add(this.m_txtLibelleLongUnite);
            this.m_panelUnite.Controls.Add(this.label7);
            this.m_panelUnite.Controls.Add(this.m_txtLibellCourtUnite);
            this.m_panelUnite.Controls.Add(this.m_txtIdUnite);
            this.m_panelUnite.Controls.Add(this.label4);
            this.m_panelUnite.Controls.Add(this.label5);
            this.m_panelUnite.Controls.Add(this.label6);
            this.m_panelUnite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelUnite.Location = new System.Drawing.Point(0, 0);
            this.m_panelUnite.LockEdition = false;
            this.m_panelUnite.Name = "m_panelUnite";
            this.m_panelUnite.Size = new System.Drawing.Size(377, 274);
            this.m_extStyle.SetStyleBackColor(this.m_panelUnite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelUnite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelUnite.TabIndex = 8;
            // 
            // m_cmbClasse
            // 
            this.m_cmbClasse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbClasse.FormattingEnabled = true;
            this.m_cmbClasse.IsLink = false;
            this.m_cmbClasse.ListDonnees = null;
            this.m_cmbClasse.Location = new System.Drawing.Point(109, 24);
            this.m_cmbClasse.LockEdition = false;
            this.m_cmbClasse.Name = "m_cmbClasse";
            this.m_cmbClasse.NullAutorise = false;
            this.m_cmbClasse.ProprieteAffichee = null;
            this.m_cmbClasse.Size = new System.Drawing.Size(248, 21);
            this.m_extStyle.SetStyleBackColor(this.m_cmbClasse, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_cmbClasse, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_cmbClasse.TabIndex = 14;
            this.m_cmbClasse.TextNull = "(empty)";
            this.m_cmbClasse.Tri = true;
            this.m_cmbClasse.SelectionChangeCommitted += new System.EventHandler(this.m_cmbClasse_SelectionChangeCommitted);
            // 
            // m_txtOffsetConversion
            // 
            this.m_txtOffsetConversion.Arrondi = 12;
            this.m_txtOffsetConversion.DecimalAutorise = true;
            this.m_txtOffsetConversion.IntValue = 0;
            this.m_txtOffsetConversion.Location = new System.Drawing.Point(112, 190);
            this.m_txtOffsetConversion.LockEdition = false;
            this.m_txtOffsetConversion.Name = "m_txtOffsetConversion";
            this.m_txtOffsetConversion.NullAutorise = false;
            this.m_txtOffsetConversion.SelectAllOnEnter = true;
            this.m_txtOffsetConversion.SeparateurMilliers = "";
            this.m_txtOffsetConversion.Size = new System.Drawing.Size(182, 20);
            this.m_extStyle.SetStyleBackColor(this.m_txtOffsetConversion, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtOffsetConversion, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtOffsetConversion.TabIndex = 13;
            this.m_txtOffsetConversion.Text = "0";
            this.m_txtOffsetConversion.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // m_txtFacteurConversion
            // 
            this.m_txtFacteurConversion.Arrondi = 12;
            this.m_txtFacteurConversion.DecimalAutorise = true;
            this.m_txtFacteurConversion.IntValue = 0;
            this.m_txtFacteurConversion.Location = new System.Drawing.Point(112, 161);
            this.m_txtFacteurConversion.LockEdition = false;
            this.m_txtFacteurConversion.Name = "m_txtFacteurConversion";
            this.m_txtFacteurConversion.NullAutorise = false;
            this.m_txtFacteurConversion.SelectAllOnEnter = true;
            this.m_txtFacteurConversion.SeparateurMilliers = "";
            this.m_txtFacteurConversion.Size = new System.Drawing.Size(182, 20);
            this.m_extStyle.SetStyleBackColor(this.m_txtFacteurConversion, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtFacteurConversion, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtFacteurConversion.TabIndex = 12;
            this.m_txtFacteurConversion.Text = "0";
            this.m_txtFacteurConversion.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(3, 190);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 18);
            this.m_extStyle.SetStyleBackColor(this.label9, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label9, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label9.TabIndex = 11;
            this.label9.Text = "B=";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(3, 163);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 18);
            this.m_extStyle.SetStyleBackColor(this.label8, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label8, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label8.TabIndex = 10;
            this.label8.Text = "A=";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblConversion
            // 
            this.m_lblConversion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_lblConversion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblConversion.Location = new System.Drawing.Point(18, 134);
            this.m_lblConversion.Name = "m_lblConversion";
            this.m_lblConversion.Size = new System.Drawing.Size(339, 21);
            this.m_extStyle.SetStyleBackColor(this.m_lblConversion, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_lblConversion, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lblConversion.TabIndex = 9;
            // 
            // m_txtLibelleLongUnite
            // 
            this.m_txtLibelleLongUnite.Location = new System.Drawing.Point(109, 102);
            this.m_txtLibelleLongUnite.LockEdition = false;
            this.m_txtLibelleLongUnite.Name = "m_txtLibelleLongUnite";
            this.m_txtLibelleLongUnite.Size = new System.Drawing.Size(248, 20);
            this.m_extStyle.SetStyleBackColor(this.m_txtLibelleLongUnite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtLibelleLongUnite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtLibelleLongUnite.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(3, 102);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 18);
            this.m_extStyle.SetStyleBackColor(this.label7, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label7, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label7.TabIndex = 7;
            this.label7.Text = "Long label|20068";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtLibellCourtUnite
            // 
            this.m_txtLibellCourtUnite.Location = new System.Drawing.Point(109, 76);
            this.m_txtLibellCourtUnite.LockEdition = false;
            this.m_txtLibellCourtUnite.Name = "m_txtLibellCourtUnite";
            this.m_txtLibellCourtUnite.Size = new System.Drawing.Size(248, 20);
            this.m_extStyle.SetStyleBackColor(this.m_txtLibellCourtUnite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtLibellCourtUnite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtLibellCourtUnite.TabIndex = 5;
            // 
            // m_txtIdUnite
            // 
            this.m_txtIdUnite.Location = new System.Drawing.Point(109, 50);
            this.m_txtIdUnite.LockEdition = false;
            this.m_txtIdUnite.Name = "m_txtIdUnite";
            this.m_txtIdUnite.Size = new System.Drawing.Size(248, 20);
            this.m_extStyle.SetStyleBackColor(this.m_txtIdUnite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtIdUnite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtIdUnite.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 18);
            this.m_extStyle.SetStyleBackColor(this.label4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label4.TabIndex = 2;
            this.label4.Text = "Short label|20067";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 18);
            this.m_extStyle.SetStyleBackColor(this.label5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label5.TabIndex = 0;
            this.label5.Text = "Class|20066";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 18);
            this.m_extStyle.SetStyleBackColor(this.label6, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label6, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label6.TabIndex = 1;
            this.label6.Text = "Id|20064";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 226);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(377, 48);
            this.m_extStyle.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 9;
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnAnnuler.ForeColor = System.Drawing.Color.White;
            this.m_btnAnnuler.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAnnuler.Image")));
            this.m_btnAnnuler.Location = new System.Drawing.Point(195, 2);
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
            this.m_btnOk.Location = new System.Drawing.Point(141, 2);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(40, 40);
            this.m_extStyle.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 2;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // CFormEditeUniteInDb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 274);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_panelUnite);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CFormEditeUniteInDb";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.m_extStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.m_extStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Edit Unit|20072";
            this.Load += new System.EventHandler(this.CFormEditeUniteInDb_Load);
            this.m_panelUnite.ResumeLayout(false);
            this.m_panelUnite.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private sc2i.win32.common.C2iPanel m_panelUnite;
        private sc2i.win32.common.CComboboxAutoFilled m_cmbClasse;
        private sc2i.win32.common.C2iTextBoxNumerique m_txtOffsetConversion;
        private sc2i.win32.common.C2iTextBoxNumerique m_txtFacteurConversion;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label m_lblConversion;
        private sc2i.win32.common.C2iTextBox m_txtLibelleLongUnite;
        private System.Windows.Forms.Label label7;
        private sc2i.win32.common.C2iTextBox m_txtLibellCourtUnite;
        private sc2i.win32.common.C2iTextBox m_txtIdUnite;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button m_btnAnnuler;
        private System.Windows.Forms.Button m_btnOk;
        private sc2i.win32.common.CExtStyle m_extStyle;
    }
}