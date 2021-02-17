namespace sc2i.win32.common
{
	partial class C2iProgressBar
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
			this.m_progressBar = new System.Windows.Forms.ProgressBar();
			this.m_labelText = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// m_progressBar
			// 
			this.m_progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.m_progressBar.Location = new System.Drawing.Point(3, 3);
			this.m_progressBar.Name = "m_progressBar";
			this.m_progressBar.Size = new System.Drawing.Size(398, 14);
			this.m_progressBar.TabIndex = 0;
			// 
			// m_labelText
			// 
			this.m_labelText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.m_labelText.Location = new System.Drawing.Point(3, 20);
			this.m_labelText.Name = "m_labelText";
			this.m_labelText.Size = new System.Drawing.Size(398, 32);
			this.m_labelText.TabIndex = 1;
			// 
			// C2iProgressBar
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.m_labelText);
			this.Controls.Add(this.m_progressBar);
			this.Name = "C2iProgressBar";
			this.Size = new System.Drawing.Size(404, 52);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ProgressBar m_progressBar;
		private System.Windows.Forms.Label m_labelText;
	}
}
