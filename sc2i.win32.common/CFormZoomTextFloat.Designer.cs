namespace sc2i.win32.common
{
	partial class CFormZoomTextFloat
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

		#region Code généré par le Concepteur Windows Form

		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.m_txtBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// m_txtBox
			// 
			this.m_txtBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_txtBox.Location = new System.Drawing.Point(0, 0);
			this.m_txtBox.Multiline = true;
			this.m_txtBox.Name = "m_txtBox";
			this.m_txtBox.Size = new System.Drawing.Size(152, 144);
			this.m_txtBox.TabIndex = 0;
			this.m_txtBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtBox_KeyDown);
			this.m_txtBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_txtBox_KeyPress);
			// 
			// CFormZoomTextFloat
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(152, 144);
			this.Controls.Add(this.m_txtBox);
			this.Name = "CFormZoomTextFloat";
			this.Text = "CFormZoomText";
			this.Load += new System.EventHandler(this.CFormZoomTextFloat_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox m_txtBox;
	}
}