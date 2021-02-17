namespace sc2i.win32.common
{
	partial class CWndSaisieHeure
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
            this.m_txtHeure = new sc2i.win32.common.C2iTextBox();
            this.SuspendLayout();
            // 
            // m_txtHeure
            // 
            this.m_txtHeure.AutoCompleteCustomSource.AddRange(new string[] {
            "tRUC",
            "bIDULE",
            "mACHIN"});
            this.m_txtHeure.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.m_txtHeure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtHeure.Location = new System.Drawing.Point(0, 0);
            this.m_txtHeure.LockEdition = false;
            this.m_txtHeure.MaxLength = 5;
            this.m_txtHeure.Name = "m_txtHeure";
            this.m_txtHeure.Size = new System.Drawing.Size(275, 20);
            this.m_txtHeure.TabIndex = 0;
            this.m_txtHeure.Validated += new System.EventHandler(this.m_txtHeure_Validated);
            // 
            // CWndSaisieHeure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.m_txtHeure);
            this.Name = "CWndSaisieHeure";
            this.Size = new System.Drawing.Size(275, 21);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private C2iTextBox m_txtHeure;
	}
}
