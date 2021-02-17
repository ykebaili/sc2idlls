namespace sc2i.win32.data.navigation.ControlesForCWnd
{
    partial class CControleSelectFormEdition
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
			this.m_listBox = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// m_listBox
			// 
			this.m_listBox.DisplayMember = "Libelle";
			this.m_listBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_listBox.FormattingEnabled = true;
			this.m_listBox.Location = new System.Drawing.Point(0, 0);
			this.m_listBox.Name = "m_listBox";
			this.m_listBox.Size = new System.Drawing.Size(230, 147);
			this.m_listBox.TabIndex = 1;
			this.m_listBox.SelectedValueChanged += new System.EventHandler(this.m_listBox_SelectedValueChanged);
			// 
			// CControleSelectFormEdition
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.m_listBox);
			this.Name = "CControleSelectFormEdition";
			this.Size = new System.Drawing.Size(230, 150);
			this.Load += new System.EventHandler(this.CControleSelectFormEdition_Load);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox m_listBox;
    }
}
