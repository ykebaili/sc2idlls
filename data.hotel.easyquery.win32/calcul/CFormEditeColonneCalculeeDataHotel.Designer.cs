namespace data.hotel.easyquery.win32.calcul
{
    partial class CFormEditeColonneCalculeeDataHotel
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
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtColumnName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_cmbTypeCalcul = new sc2i.win32.common.CComboboxAutoFilled();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_panelCalcul = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Column name|20045";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtColumnName
            // 
            this.m_txtColumnName.Location = new System.Drawing.Point(142, 11);
            this.m_txtColumnName.Name = "m_txtColumnName";
            this.m_txtColumnName.Size = new System.Drawing.Size(209, 20);
            this.m_txtColumnName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(13, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Calculation|20026";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_cmbTypeCalcul
            // 
            this.m_cmbTypeCalcul.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbTypeCalcul.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_cmbTypeCalcul.FormattingEnabled = true;
            this.m_cmbTypeCalcul.IsLink = false;
            this.m_cmbTypeCalcul.ListDonnees = null;
            this.m_cmbTypeCalcul.Location = new System.Drawing.Point(142, 36);
            this.m_cmbTypeCalcul.LockEdition = false;
            this.m_cmbTypeCalcul.Name = "m_cmbTypeCalcul";
            this.m_cmbTypeCalcul.NullAutorise = false;
            this.m_cmbTypeCalcul.ProprieteAffichee = null;
            this.m_cmbTypeCalcul.Size = new System.Drawing.Size(209, 21);
            this.m_cmbTypeCalcul.TabIndex = 2;
            this.m_cmbTypeCalcul.TextNull = "(empty)";
            this.m_cmbTypeCalcul.Tri = true;
            this.m_cmbTypeCalcul.SelectionChangeCommitted += new System.EventHandler(this.m_cmbTypeCalcul_SelectionChangeCommitted);
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Location = new System.Drawing.Point(263, 3);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(75, 23);
            this.m_btnAnnuler.TabIndex = 7;
            this.m_btnAnnuler.Text = "Cancel|2";
            this.m_btnAnnuler.UseVisualStyleBackColor = true;
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // m_btnOk
            // 
            this.m_btnOk.Location = new System.Drawing.Point(166, 3);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 6;
            this.m_btnOk.Text = "Ok|1";
            this.m_btnOk.UseVisualStyleBackColor = true;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.m_txtColumnName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.m_cmbTypeCalcul);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(505, 64);
            this.panel1.TabIndex = 8;
            // 
            // m_panelCalcul
            // 
            this.m_panelCalcul.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelCalcul.Location = new System.Drawing.Point(0, 64);
            this.m_panelCalcul.Name = "m_panelCalcul";
            this.m_panelCalcul.Size = new System.Drawing.Size(505, 210);
            this.m_panelCalcul.TabIndex = 9;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_btnOk);
            this.panel2.Controls.Add(this.m_btnAnnuler);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 274);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(505, 29);
            this.panel2.TabIndex = 10;
            // 
            // CFormEditeColonneCalculeeDataHotel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 303);
            this.Controls.Add(this.m_panelCalcul);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "CFormEditeColonneCalculeeDataHotel";
            this.Text = "Hotel calculated column|20044";
            this.Load += new System.EventHandler(this.CFormEditeColonneCalculeeDataHotel_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_txtColumnName;
        private System.Windows.Forms.Label label2;
        private sc2i.win32.common.CComboboxAutoFilled m_cmbTypeCalcul;
        private System.Windows.Forms.Button m_btnAnnuler;
        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel m_panelCalcul;
        private System.Windows.Forms.Panel panel2;
    }
}