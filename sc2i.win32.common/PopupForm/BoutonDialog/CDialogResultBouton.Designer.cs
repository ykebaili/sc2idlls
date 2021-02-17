namespace sc2i.win32.common
{
	partial class CDialogResultBouton
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
			this.m_btn = new System.Windows.Forms.Button();
			this.m_dialogResultProviderForBouton = new sc2i.win32.common.CDialogResultProviderForBouton();
			this.SuspendLayout();
			// 
			// m_btn
			// 
			this.m_btn.BackColor = System.Drawing.Color.White;
			this.m_btn.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.m_btn.Location = new System.Drawing.Point(0, 0);
			this.m_btn.Name = "m_btn";
			this.m_btn.Size = new System.Drawing.Size(107, 37);
			this.m_btn.TabIndex = 0;
			this.m_btn.UseVisualStyleBackColor = false;
			// 
			// m_dialogResultProviderForBouton
			// 
			this.m_dialogResultProviderForBouton.Bouton = this.m_btn;
			this.m_dialogResultProviderForBouton.ClicBouton += new sc2i.win32.common.ClicBoutonDialogResult(this.m_dialogResultProviderForBouton_ClicBouton);
			// 
			// CDialogResultBouton
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.m_btn);
			this.Name = "CDialogResultBouton";
			this.Size = new System.Drawing.Size(107, 37);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button m_btn;
		private CDialogResultProviderForBouton m_dialogResultProviderForBouton;
	}
}
