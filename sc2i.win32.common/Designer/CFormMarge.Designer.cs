namespace sc2i.win32.common
{
    partial class CFormMarge
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
            this.m_txtMarge = new sc2i.win32.common.C2iTextBoxNumerique();
            this.m_lblMarge = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_txtMarge
            // 
            this.m_txtMarge.Arrondi = 0;
            this.m_txtMarge.DecimalAutorise = true;
            this.m_txtMarge.DoubleValue = 0;
            this.m_txtMarge.IntValue = 0;
            this.m_txtMarge.Location = new System.Drawing.Point(47, 10);
            this.m_txtMarge.LockEdition = false;
            this.m_txtMarge.Name = "m_txtMarge";
            this.m_txtMarge.NullAutorise = false;
            this.m_txtMarge.SelectAllOnEnter = true;
            this.m_txtMarge.Size = new System.Drawing.Size(100, 20);
            this.m_txtMarge.TabIndex = 2;
            this.m_txtMarge.Text = "0";
            // 
            // m_lblMarge
            // 
            this.m_lblMarge.AutoSize = true;
            this.m_lblMarge.Location = new System.Drawing.Point(0, 13);
            this.m_lblMarge.Name = "m_lblMarge";
            this.m_lblMarge.Size = new System.Drawing.Size(37, 13);
            this.m_lblMarge.TabIndex = 3;
            this.m_lblMarge.Text = "Margin|10007";
            // 
            // CFormMarge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(151, 73);
            this.Controls.Add(this.m_lblMarge);
            this.Controls.Add(this.m_txtMarge);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "CFormMarge";
            this.Opacity = 1;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CFormMarge_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CFormMarge_FormClosing);
            this.Controls.SetChildIndex(this.m_txtMarge, 0);
            this.Controls.SetChildIndex(this.m_lblMarge, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C2iTextBoxNumerique m_txtMarge;
        private System.Windows.Forms.Label m_lblMarge;
    }
}