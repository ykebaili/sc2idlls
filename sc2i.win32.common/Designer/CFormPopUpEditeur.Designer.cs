namespace sc2i.win32.common
{
    partial class CFormPopUpEditeur
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
			this.m_btnOk = new System.Windows.Forms.Button();
			this.m_panBottom = new System.Windows.Forms.Panel();
			this.m_effetFondu = new sc2i.win32.common.CEffetFonduPourForm();
			this.m_panBottom.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_btnOk
			// 
			this.m_btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.m_btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.m_btnOk.Location = new System.Drawing.Point(12, 3);
			this.m_btnOk.Name = "m_btnOk";
			this.m_btnOk.Size = new System.Drawing.Size(234, 22);
			this.m_btnOk.TabIndex = 0;
			this.m_btnOk.Text = "Ok|10";
			this.m_btnOk.UseVisualStyleBackColor = true;
			this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
			// 
			// m_panBottom
			// 
			this.m_panBottom.Controls.Add(this.m_btnOk);
			this.m_panBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.m_panBottom.Location = new System.Drawing.Point(0, 89);
			this.m_panBottom.Name = "m_panBottom";
			this.m_panBottom.Size = new System.Drawing.Size(258, 28);
			this.m_panBottom.TabIndex = 1;
			// 
			// m_effetFondu
			// 
			this.m_effetFondu.AuDessusDesAutresFenetres = true;
			this.m_effetFondu.EffetFonduFermeture = true;
			this.m_effetFondu.EffetFonduOuverture = true;
			this.m_effetFondu.Formulaire = this;
			this.m_effetFondu.IntervalImages = 15;
			this.m_effetFondu.NombreImage = 10;
			// 
			// CFormPopUpEditeur
			// 
			this.AcceptButton = this.m_btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(258, 117);
			this.ControlBox = false;
			this.Controls.Add(this.m_panBottom);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "CFormPopUpEditeur";
			this.Opacity = 0;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.m_panBottom.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.Panel m_panBottom;
        private CEffetFonduPourForm m_effetFondu;
    }
}