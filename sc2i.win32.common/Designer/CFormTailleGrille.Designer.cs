namespace sc2i.win32.common
{
    partial class CFormTailleGrille
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
            this.m_lblLargeur = new System.Windows.Forms.Label();
            this.m_txtLargeur = new sc2i.win32.common.C2iTextBoxNumerique();
            this.m_txtHauteur = new sc2i.win32.common.C2iTextBoxNumerique();
            this.m_lblHauteur = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_lblLargeur
            // 
            this.m_lblLargeur.AutoSize = true;
            this.m_lblLargeur.Location = new System.Drawing.Point(9, 9);
            this.m_lblLargeur.Name = "m_lblLargeur";
            this.m_lblLargeur.Size = new System.Drawing.Size(43, 13);
            this.m_lblLargeur.TabIndex = 2;
            this.m_lblLargeur.Text = "Width|10009";
            // 
            // m_txtLargeur
            // 
            this.m_txtLargeur.Arrondi = 0;
            this.m_txtLargeur.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_txtLargeur.DecimalAutorise = true;
            this.m_txtLargeur.DoubleValue = 0;
            this.m_txtLargeur.IntValue = 0;
            this.m_txtLargeur.Location = new System.Drawing.Point(60, 6);
            this.m_txtLargeur.LockEdition = false;
            this.m_txtLargeur.Name = "m_txtLargeur";
            this.m_txtLargeur.NullAutorise = false;
            this.m_txtLargeur.SelectAllOnEnter = true;
            this.m_txtLargeur.Size = new System.Drawing.Size(71, 20);
            this.m_txtLargeur.TabIndex = 3;
            this.m_txtLargeur.Text = "0";
            // 
            // m_txtHauteur
            // 
            this.m_txtHauteur.Arrondi = 0;
            this.m_txtHauteur.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_txtHauteur.DecimalAutorise = true;
            this.m_txtHauteur.DoubleValue = 0;
            this.m_txtHauteur.IntValue = 0;
            this.m_txtHauteur.Location = new System.Drawing.Point(60, 32);
            this.m_txtHauteur.LockEdition = false;
            this.m_txtHauteur.Name = "m_txtHauteur";
            this.m_txtHauteur.NullAutorise = false;
            this.m_txtHauteur.SelectAllOnEnter = true;
            this.m_txtHauteur.Size = new System.Drawing.Size(71, 20);
            this.m_txtHauteur.TabIndex = 3;
            this.m_txtHauteur.Text = "0";
            // 
            // m_lblHauteur
            // 
            this.m_lblHauteur.AutoSize = true;
            this.m_lblHauteur.Location = new System.Drawing.Point(9, 35);
            this.m_lblHauteur.Name = "m_lblHauteur";
            this.m_lblHauteur.Size = new System.Drawing.Size(45, 13);
            this.m_lblHauteur.TabIndex = 2;
            this.m_lblHauteur.Text = "Height|10010";
            // 
            // CFormTailleGrille
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(143, 89);
            this.Controls.Add(this.m_lblLargeur);
            this.Controls.Add(this.m_txtLargeur);
            this.Controls.Add(this.m_lblHauteur);
            this.Controls.Add(this.m_txtHauteur);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "CFormTailleGrille";
            this.Opacity = 1;
            this.Text = "Grid Size|10011";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CFormTailleGrille_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CFormTailleGrille_FormClosing);
            this.Controls.SetChildIndex(this.m_txtHauteur, 0);
            this.Controls.SetChildIndex(this.m_lblHauteur, 0);
            this.Controls.SetChildIndex(this.m_txtLargeur, 0);
            this.Controls.SetChildIndex(this.m_lblLargeur, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_lblLargeur;
        private C2iTextBoxNumerique m_txtLargeur;
        private C2iTextBoxNumerique m_txtHauteur;
        private System.Windows.Forms.Label m_lblHauteur;
    }
}