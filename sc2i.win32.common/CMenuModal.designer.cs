namespace sc2i.win32.common
{
    partial class CMenuModal
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
            this.m_menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // m_menu
            // 
            this.m_menu.Name = "m_menu";
            this.m_menu.Size = new System.Drawing.Size(61, 4);
            this.m_menu.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.m_menu_Closed);
            // 
            // m_timer
            // 
            this.m_timer.Enabled = true;
            this.m_timer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // CMenuModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(49, 25);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CMenuModal";
            this.Opacity = 0.5;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CMenuModal";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.CMenuModal_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip m_menu;
        private System.Windows.Forms.Timer m_timer;
    }
}