namespace sc2i.test.metier
{
    partial class CFormTEstGMap
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_vEarth = new sc2i.win32.common.CVEarthCtrl();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(921, 125);
            this.panel1.TabIndex = 1;
            // 
            // m_vEarth
            // 
            this.m_vEarth.CenterLatitude = 0;
            this.m_vEarth.CenterLongitude = 0;
            this.m_vEarth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_vEarth.Location = new System.Drawing.Point(0, 125);
            this.m_vEarth.MapStyle = sc2i.win32.common.EMapStyle.Road;
            this.m_vEarth.Name = "m_vEarth";
            this.m_vEarth.Size = new System.Drawing.Size(921, 409);
            this.m_vEarth.TabIndex = 0;
            this.m_vEarth.Zoom = 5;
            // 
            // CFormTEstGMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 534);
            this.Controls.Add(this.m_vEarth);
            this.Controls.Add(this.panel1);
            this.Name = "CFormTEstGMap";
            this.Text = "Test GMap control";
            this.Load += new System.EventHandler(this.CFormTEstGMap_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private sc2i.win32.common.CVEarthCtrl m_vEarth;
        private System.Windows.Forms.Panel panel1;
    }
}