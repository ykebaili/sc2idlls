namespace sc2i.win32.data.dynamic.import
{
    partial class CControleOptionCreation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CControleOptionCreation));
            this.m_image = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_image)).BeginInit();
            this.SuspendLayout();
            // 
            // m_image
            // 
            this.m_image.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_image.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_image.Image = ((System.Drawing.Image)(resources.GetObject("m_image.Image")));
            this.m_image.Location = new System.Drawing.Point(0, 0);
            this.m_image.Name = "m_image";
            this.m_image.Size = new System.Drawing.Size(32, 32);
            this.m_image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.m_image.TabIndex = 0;
            this.m_image.TabStop = false;
            this.m_image.Click += new System.EventHandler(this.m_image_Click);
            // 
            // CControleOptionCreation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_image);
            this.Name = "CControleOptionCreation";
            this.Size = new System.Drawing.Size(32, 32);
            ((System.ComponentModel.ISupportInitialize)(this.m_image)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox m_image;
    }
}
