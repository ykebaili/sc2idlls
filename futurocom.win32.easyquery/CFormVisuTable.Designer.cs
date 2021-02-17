namespace futurocom.win32.easyquery
{
    partial class CFormVisuTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormVisuTable));
            this.m_grid = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_lblNbRecs = new System.Windows.Forms.Label();
            this.m_btnCopy = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.m_grid)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_grid
            // 
            this.m_grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_grid.Location = new System.Drawing.Point(0, 0);
            this.m_grid.Name = "m_grid";
            this.m_grid.Size = new System.Drawing.Size(432, 348);
            this.m_grid.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnCopy);
            this.panel1.Controls.Add(this.m_lblNbRecs);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 348);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(432, 24);
            this.panel1.TabIndex = 1;
            // 
            // m_lblNbRecs
            // 
            this.m_lblNbRecs.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lblNbRecs.Location = new System.Drawing.Point(0, 0);
            this.m_lblNbRecs.Name = "m_lblNbRecs";
            this.m_lblNbRecs.Size = new System.Drawing.Size(179, 24);
            this.m_lblNbRecs.TabIndex = 0;
            // 
            // m_btnCopy
            // 
            this.m_btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("m_btnCopy.Image")));
            this.m_btnCopy.Location = new System.Drawing.Point(405, 1);
            this.m_btnCopy.Name = "m_btnCopy";
            this.m_btnCopy.Size = new System.Drawing.Size(24, 23);
            this.m_btnCopy.TabIndex = 18;
            this.m_btnCopy.Click += new System.EventHandler(this.m_btnCopy_Click);
            // 
            // CFormVisuTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 372);
            this.Controls.Add(this.m_grid);
            this.Controls.Add(this.panel1);
            this.Name = "CFormVisuTable";
            this.Text = "Table view|20056";
            this.Load += new System.EventHandler(this.CFormVisuTable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_grid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView m_grid;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label m_lblNbRecs;
        private System.Windows.Forms.Button m_btnCopy;
    }
}