namespace sc2i.win32.data.navigation
{
	partial class CExtendeurFormEditionStandardTabPage
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
			this.m_extModeEdition = new sc2i.win32.common.CExtModeEdition();
			this.m_extLinkField = new sc2i.win32.common.CExtLinkField();
			this.m_extStyle = new sc2i.win32.common.CExtStyle();
			this.SuspendLayout();
			// 
			// CExtendeurFormEditionStandardTabPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ForeColor = System.Drawing.Color.Black;
			this.m_extLinkField.SetLinkField(this, "");
			this.m_extModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
			this.Name = "CExtendeurFormEditionStandardTabPage";
			this.m_extStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
			this.m_extStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
			this.Load += new System.EventHandler(this.CExtendeurFormEditionStandardTabPage_Load);
			this.ResumeLayout(false);

		}

		#endregion

		protected sc2i.win32.common.CExtModeEdition m_extModeEdition;
		protected sc2i.win32.common.CExtLinkField m_extLinkField;
		protected sc2i.win32.common.CExtStyle m_extStyle;

	}
}
