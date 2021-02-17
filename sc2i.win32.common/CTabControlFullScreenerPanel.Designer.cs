namespace sc2i.win32.common
{
    partial class CTabControlFullScreenerPanel
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_panelTop = new System.Windows.Forms.Panel();
            this.m_btnFullScreen = new System.Windows.Forms.PictureBox();
            this.m_lblTitle = new System.Windows.Forms.Label();
            this.m_btnNoFullScreen = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_panelControles = new System.Windows.Forms.Panel();
            this.m_panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnFullScreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnNoFullScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // m_panelTop
            // 
            this.m_panelTop.Controls.Add(this.m_btnFullScreen);
            this.m_panelTop.Controls.Add(this.m_lblTitle);
            this.m_panelTop.Controls.Add(this.m_btnNoFullScreen);
            this.m_panelTop.Controls.Add(this.label1);
            this.m_panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelTop.Location = new System.Drawing.Point(0, 0);
            this.m_panelTop.Name = "m_panelTop";
            this.m_panelTop.Size = new System.Drawing.Size(508, 33);
            this.m_panelTop.TabIndex = 0;
            // 
            // m_btnFullScreen
            // 
            this.m_btnFullScreen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_btnFullScreen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnFullScreen.Image = global::sc2i.win32.common.Properties.Resources.FullScreen_Icon;
            this.m_btnFullScreen.Location = new System.Drawing.Point(379, 17);
            this.m_btnFullScreen.Name = "m_btnFullScreen";
            this.m_btnFullScreen.Size = new System.Drawing.Size(12, 12);
            this.m_btnFullScreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.m_btnFullScreen.TabIndex = 1;
            this.m_btnFullScreen.TabStop = false;
            this.m_btnFullScreen.Click += new System.EventHandler(this.m_btnFullScreen_Click);
            this.m_btnFullScreen.MouseEnter += new System.EventHandler(this.m_btnFullScreen_MouseEnter);
            this.m_btnFullScreen.MouseLeave += new System.EventHandler(this.m_btnFullScreen_MouseLeave);
            this.m_btnFullScreen.ParentChanged += new System.EventHandler(this.m_btnFullScreen_ParentChanged);
            // 
            // m_lblTitle
            // 
            this.m_lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblTitle.Location = new System.Drawing.Point(0, 0);
            this.m_lblTitle.Name = "m_lblTitle";
            this.m_lblTitle.Size = new System.Drawing.Size(476, 32);
            this.m_lblTitle.TabIndex = 1;
            this.m_lblTitle.Text = "Title";
            this.m_lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_btnNoFullScreen
            // 
            this.m_btnNoFullScreen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnNoFullScreen.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnNoFullScreen.Image = global::sc2i.win32.common.Properties.Resources.NoFullScreen_Icon;
            this.m_btnNoFullScreen.Location = new System.Drawing.Point(476, 0);
            this.m_btnNoFullScreen.Name = "m_btnNoFullScreen";
            this.m_btnNoFullScreen.Size = new System.Drawing.Size(32, 32);
            this.m_btnNoFullScreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.m_btnNoFullScreen.TabIndex = 0;
            this.m_btnNoFullScreen.TabStop = false;
            this.m_btnNoFullScreen.Click += new System.EventHandler(this.m_btnNoFullScreen_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(0, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(508, 1);
            this.label1.TabIndex = 2;
            // 
            // m_panelControles
            // 
            this.m_panelControles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelControles.Location = new System.Drawing.Point(0, 33);
            this.m_panelControles.Name = "m_panelControles";
            this.m_panelControles.Size = new System.Drawing.Size(508, 273);
            this.m_panelControles.TabIndex = 1;
            // 
            // CTabControlFullScreenerPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_panelControles);
            this.Controls.Add(this.m_panelTop);
            this.Name = "CTabControlFullScreenerPanel";
            this.Size = new System.Drawing.Size(508, 306);
            this.m_panelTop.ResumeLayout(false);
            this.m_panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnFullScreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnNoFullScreen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_panelTop;
        private System.Windows.Forms.Label m_lblTitle;
        private System.Windows.Forms.PictureBox m_btnNoFullScreen;
        private System.Windows.Forms.Panel m_panelControles;
        private System.Windows.Forms.PictureBox m_btnFullScreen;
        private System.Windows.Forms.Label label1;
    }
}
