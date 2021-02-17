namespace sc2i.win32.data.dynamic
{
    partial class CFormTestVisuTableauCroise
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
            this.m_grid = new CPanelVisuDonneePrecalculee();
            this.SuspendLayout();
            // 
            // m_grid
            // 
            this.m_grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_grid.Location = new System.Drawing.Point(0, 0);
            this.m_grid.Name = "m_grid";
            this.m_grid.Size = new System.Drawing.Size(442, 326);
            this.m_grid.TabIndex = 0;
            // 
            // CFormTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(442, 326);
            this.Controls.Add(this.m_grid);
            this.Name = "CFormTest";
            this.Text = "Visualization test|20033";
            this.Load += new System.EventHandler(this.CFormTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CPanelVisuDonneePrecalculee m_grid;

    }
}