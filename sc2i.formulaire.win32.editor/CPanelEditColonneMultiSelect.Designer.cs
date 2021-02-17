namespace sc2i.formulaire.win32
{
    partial class CPanelEditColonneMultiSelect
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
            this.m_txtNom = new System.Windows.Forms.TextBox();
            this.m_txtLargeur = new sc2i.win32.common.C2iTextBoxNumerique();
            this.m_txtFormule = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.m_btnDelete = new sc2i.win32.common.CWndLinkStd();
            this.SuspendLayout();
            // 
            // m_txtNom
            // 
            this.m_txtNom.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_txtNom.Location = new System.Drawing.Point(48, 0);
            this.m_txtNom.Name = "m_txtNom";
            this.m_txtNom.Size = new System.Drawing.Size(150, 20);
            this.m_txtNom.TabIndex = 0;
            // 
            // m_txtLargeur
            // 
            this.m_txtLargeur.Arrondi = 0;
            this.m_txtLargeur.DecimalAutorise = false;
            this.m_txtLargeur.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_txtLargeur.EmptyText = "";
            this.m_txtLargeur.IntValue = 0;
            this.m_txtLargeur.Location = new System.Drawing.Point(0, 0);
            this.m_txtLargeur.LockEdition = false;
            this.m_txtLargeur.Name = "m_txtLargeur";
            this.m_txtLargeur.NullAutorise = false;
            this.m_txtLargeur.SelectAllOnEnter = true;
            this.m_txtLargeur.SeparateurMilliers = "";
            this.m_txtLargeur.Size = new System.Drawing.Size(48, 20);
            this.m_txtLargeur.TabIndex = 1;
            this.m_txtLargeur.Text = "0";
            // 
            // m_txtFormule
            // 
            this.m_txtFormule.AllowGraphic = true;
            this.m_txtFormule.AllowNullFormula = false;
            this.m_txtFormule.AllowSaisieTexte = true;
            this.m_txtFormule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtFormule.Formule = null;
            this.m_txtFormule.Location = new System.Drawing.Point(198, 0);
            this.m_txtFormule.LockEdition = false;
            this.m_txtFormule.LockZoneTexte = false;
            this.m_txtFormule.Name = "m_txtFormule";
            this.m_txtFormule.Size = new System.Drawing.Size(219, 21);
            this.m_txtFormule.TabIndex = 2;
            // 
            // m_btnDelete
            // 
            this.m_btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnDelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnDelete.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_btnDelete.Location = new System.Drawing.Point(417, 0);
            this.m_btnDelete.Name = "m_btnDelete";
            this.m_btnDelete.ShortMode = false;
            this.m_btnDelete.Size = new System.Drawing.Size(23, 21);
            this.m_btnDelete.TabIndex = 3;
            this.m_btnDelete.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_btnDelete.LinkClicked += new System.EventHandler(this.m_btnDelete_LinkClicked);
            // 
            // CPanelEditColonneMultiSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_txtFormule);
            this.Controls.Add(this.m_txtNom);
            this.Controls.Add(this.m_txtLargeur);
            this.Controls.Add(this.m_btnDelete);
            this.Name = "CPanelEditColonneMultiSelect";
            this.Size = new System.Drawing.Size(440, 21);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_txtNom;
        private sc2i.win32.common.C2iTextBoxNumerique m_txtLargeur;
        private sc2i.win32.expression.CTextBoxZoomFormule m_txtFormule;
        private sc2i.win32.common.CWndLinkStd m_btnDelete;
    }
}
