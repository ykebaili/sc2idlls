namespace sc2i.formulaire.win32.datagrid
{
    partial class CFormFiltreDataGrid
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormFiltreDataGrid));
            this.m_txtFiltre = new System.Windows.Forms.TextBox();
            this.m_panelTop = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.m_lblNomCol = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_txtFiltre
            // 
            this.m_txtFiltre.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_txtFiltre.Location = new System.Drawing.Point(0, 0);
            this.m_txtFiltre.Name = "m_txtFiltre";
            this.m_txtFiltre.Size = new System.Drawing.Size(150, 20);
            this.m_txtFiltre.TabIndex = 0;
            this.m_txtFiltre.KeyUp += new System.Windows.Forms.KeyEventHandler(this.m_txtFiltre_KeyUp);
            // 
            // m_panelTop
            // 
            this.m_panelTop.Controls.Add(this.m_lblNomCol);
            this.m_panelTop.Controls.Add(this.pictureBox1);
            this.m_panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelTop.Location = new System.Drawing.Point(0, 0);
            this.m_panelTop.Name = "m_panelTop";
            this.m_panelTop.Size = new System.Drawing.Size(150, 22);
            this.m_panelTop.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 22);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // m_lblNomCol
            // 
            this.m_lblNomCol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblNomCol.Location = new System.Drawing.Point(16, 0);
            this.m_lblNomCol.Name = "m_lblNomCol";
            this.m_lblNomCol.Size = new System.Drawing.Size(134, 22);
            this.m_lblNomCol.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.m_panelTop);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(152, 44);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_txtFiltre);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 22);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(150, 21);
            this.panel2.TabIndex = 3;
            // 
            // CFormFiltreDataGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(152, 144);
            this.Controls.Add(this.panel1);
            this.Name = "CFormFiltreDataGrid";
            this.Text = "CFormFiltreDataGrid";
            this.Load += new System.EventHandler(this.CFormFiltreDataGrid_Load);
            this.m_panelTop.ResumeLayout(false);
            this.m_panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox m_txtFiltre;
        private System.Windows.Forms.Panel m_panelTop;
        private System.Windows.Forms.Label m_lblNomCol;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}