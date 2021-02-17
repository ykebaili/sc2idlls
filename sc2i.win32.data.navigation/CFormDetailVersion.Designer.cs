namespace sc2i.win32.data.navigation
{
	partial class CFormDetailVersion
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
			this.m_panelBas = new System.Windows.Forms.Panel();
			this.m_btnFermer = new System.Windows.Forms.Button();
			this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
			this.m_panelDetail = new sc2i.win32.data.navigation.CControleDetailVersion();
			this.m_panelBas.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_panelBas
			// 
			this.m_panelBas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
			this.m_panelBas.Controls.Add(this.m_btnFermer);
			this.m_panelBas.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.m_panelBas.ForeColor = System.Drawing.Color.Black;
			this.m_panelBas.Location = new System.Drawing.Point(0, 407);
			this.m_panelBas.Name = "m_panelBas";
			this.m_panelBas.Size = new System.Drawing.Size(666, 33);
			this.cExtStyle1.SetStyleBackColor(this.m_panelBas, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
			this.cExtStyle1.SetStyleForeColor(this.m_panelBas, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
			this.m_panelBas.TabIndex = 2;
			// 
			// m_btnFermer
			// 
			this.m_btnFermer.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.m_btnFermer.Location = new System.Drawing.Point(296, 7);
			this.m_btnFermer.Name = "m_btnFermer";
			this.m_btnFermer.Size = new System.Drawing.Size(75, 23);
			this.cExtStyle1.SetStyleBackColor(this.m_btnFermer, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
			this.cExtStyle1.SetStyleForeColor(this.m_btnFermer, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
			this.m_btnFermer.TabIndex = 0;
			this.m_btnFermer.Text = "Close|12";
			this.m_btnFermer.UseVisualStyleBackColor = true;
			this.m_btnFermer.Click += new System.EventHandler(this.m_btnFermer_Click);
			// 
			// m_panelDetail
			// 
			this.m_panelDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
			this.m_panelDetail.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_panelDetail.ForeColor = System.Drawing.Color.Black;
			this.m_panelDetail.Location = new System.Drawing.Point(0, 0);
			this.m_panelDetail.Name = "m_panelDetail";
			this.m_panelDetail.Size = new System.Drawing.Size(666, 407);
			this.cExtStyle1.SetStyleBackColor(this.m_panelDetail, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
			this.cExtStyle1.SetStyleForeColor(this.m_panelDetail, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
			this.m_panelDetail.TabIndex = 3;
			// 
			// CFormDetailVersion
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(666, 440);
			this.Controls.Add(this.m_panelDetail);
			this.Controls.Add(this.m_panelBas);
			this.ForeColor = System.Drawing.Color.Black;
			this.Name = "CFormDetailVersion";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
			this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
			this.Text = "Version detail|106";
			this.Load += new System.EventHandler(this.CFormDetailVersion_Load);
			this.m_panelBas.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel m_panelBas;
		private System.Windows.Forms.Button m_btnFermer;
		private sc2i.win32.common.CExtStyle cExtStyle1;
		private CControleDetailVersion m_panelDetail;
	}
}