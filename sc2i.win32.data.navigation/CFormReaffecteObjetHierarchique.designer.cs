namespace sc2i.win32.data.navigation
{
	partial class CFormReaffecteObjetHierarchique
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
			this.m_arbre = new System.Windows.Forms.TreeView();
			this.m_btnOk = new System.Windows.Forms.Button();
			this.m_btnAnnuler = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// m_arbre
			// 
			this.m_arbre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.m_arbre.Location = new System.Drawing.Point(0, 0);
			this.m_arbre.Name = "m_arbre";
			this.m_arbre.Size = new System.Drawing.Size(639, 339);
			this.m_arbre.TabIndex = 0;
			this.m_arbre.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.m_arbre_AfterExpand);
			// 
			// m_btnOk
			// 
			this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.m_btnOk.Location = new System.Drawing.Point(137, 345);
			this.m_btnOk.Name = "m_btnOk";
			this.m_btnOk.Size = new System.Drawing.Size(151, 29);
			this.m_btnOk.TabIndex = 1;
			this.m_btnOk.Text = "Ok|10";
			this.m_btnOk.UseVisualStyleBackColor = true;
			this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
			// 
			// m_btnAnnuler
			// 
			this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.m_btnAnnuler.Location = new System.Drawing.Point(352, 345);
			this.m_btnAnnuler.Name = "m_btnAnnuler";
			this.m_btnAnnuler.Size = new System.Drawing.Size(151, 29);
			this.m_btnAnnuler.TabIndex = 2;
			this.m_btnAnnuler.Text = "Cancel|11";
			this.m_btnAnnuler.UseVisualStyleBackColor = true;
			this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
			// 
			// CFormReaffecteObjetHierarchique
			// 
			this.AcceptButton = this.m_btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.CancelButton = this.m_btnAnnuler;
			this.ClientSize = new System.Drawing.Size(640, 386);
			this.Controls.Add(this.m_btnAnnuler);
			this.Controls.Add(this.m_btnOk);
			this.Controls.Add(this.m_arbre);
			this.Name = "CFormReaffecteObjetHierarchique";
			this.Text = "Moving an element|129";
			this.Load += new System.EventHandler(this.CFormReaffecteObjetHierarchique_Load);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView m_arbre;
        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.Button m_btnAnnuler;
    }
}