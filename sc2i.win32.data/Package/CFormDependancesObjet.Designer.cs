namespace sc2i.win32.data.Package
{
    partial class CFormDependancesObjet
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
            this.m_arbre = new sc2i.win32.data.Package.CArbreDependancesObjet();
            this.SuspendLayout();
            // 
            // m_arbre
            // 
            this.m_arbre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_arbre.Location = new System.Drawing.Point(0, 0);
            this.m_arbre.Name = "m_arbre";
            this.m_arbre.Size = new System.Drawing.Size(494, 368);
            this.m_arbre.TabIndex = 0;
            this.m_arbre.Load += new System.EventHandler(this.m_arbre_Load);
            // 
            // CFormDependancesObjet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 368);
            this.Controls.Add(this.m_arbre);
            this.Name = "CFormDependancesObjet";
            this.Text = "CFormDependancesObjet";
            this.ResumeLayout(false);

        }

        #endregion

        private CArbreDependancesObjet m_arbre;
    }
}