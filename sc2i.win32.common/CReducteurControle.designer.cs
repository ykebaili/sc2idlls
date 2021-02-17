namespace sc2i.win32.common
{
	partial class CReducteurControle
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CReducteurControle));
			this.m_images = new System.Windows.Forms.ImageList(this.components);
			this.m_btnChanger = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.m_btnChanger)).BeginInit();
			this.SuspendLayout();
			// 
			// m_images
			// 
			this.m_images.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_images.ImageStream")));
			this.m_images.TransparentColor = System.Drawing.Color.Transparent;
			this.m_images.Images.SetKeyName(0, "haut.bmp");
			this.m_images.Images.SetKeyName(1, "bas.bmp");
			// 
			// m_btnChanger
			// 
			this.m_btnChanger.Cursor = System.Windows.Forms.Cursors.Hand;
			this.m_btnChanger.Image = ((System.Drawing.Image)(resources.GetObject("m_btnChanger.Image")));
			this.m_btnChanger.Location = new System.Drawing.Point(0, 0);
			this.m_btnChanger.Name = "m_btnChanger";
			this.m_btnChanger.Size = new System.Drawing.Size(9, 8);
			this.m_btnChanger.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.m_btnChanger.TabIndex = 0;
			this.m_btnChanger.TabStop = false;
			this.m_btnChanger.Click += new System.EventHandler(this.m_btnChanger_Click);
			// 
			// CReducteurControle
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.m_btnChanger);
			this.Name = "CReducteurControle";
			this.Size = new System.Drawing.Size(9, 8);
			this.Move += new System.EventHandler(this.CReducteurControle_Move);
			this.Resize += new System.EventHandler(this.CReducteurControle_Resize);
			((System.ComponentModel.ISupportInitialize)(this.m_btnChanger)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ImageList m_images;
		private System.Windows.Forms.PictureBox m_btnChanger;
	}
}
