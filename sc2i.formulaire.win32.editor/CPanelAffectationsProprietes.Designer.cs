namespace sc2i.formulaire.win32
{
	partial class CPanelAffectationsProprietes
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
			this.m_panelControles = new sc2i.win32.common.C2iPanel(this.components);
			this.SuspendLayout();
			// 
			// m_panelControles
			// 
			this.m_panelControles.AutoScroll = true;
			this.m_panelControles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_panelControles.Location = new System.Drawing.Point(0, 0);
			this.m_panelControles.LockEdition = false;
			this.m_panelControles.Name = "m_panelControles";
			this.m_panelControles.Size = new System.Drawing.Size(432, 273);
			this.m_panelControles.TabIndex = 0;
			// 
			// CPanelAffectationsProprietes
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.m_panelControles);
			this.Name = "CPanelAffectationsProprietes";
			this.Size = new System.Drawing.Size(432, 273);
			this.ResumeLayout(false);

		}

		#endregion

		private sc2i.win32.common.C2iPanel m_panelControles;
	}
}
