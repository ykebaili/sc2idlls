namespace sc2i.formulaire.win32.inspiration
{
    partial class CPanelEditParametreInspiration
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
            this.m_cmbType = new sc2i.win32.common.C2iComboSelectDynamicClass(this.components);
            this.m_cmbChamp = new sc2i.win32.common.CComboboxAutoFilled();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // m_cmbType
            // 
            this.m_cmbType.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_cmbType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_cmbType.FormattingEnabled = true;
            this.m_cmbType.IsLink = false;
            this.m_cmbType.Location = new System.Drawing.Point(0, 0);
            this.m_cmbType.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_cmbType, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_cmbType.Name = "m_cmbType";
            this.m_cmbType.Size = new System.Drawing.Size(232, 21);
            this.m_cmbType.TabIndex = 0;
            this.m_cmbType.TypeSelectionne = null;
            this.m_cmbType.SelectionChangeCommitted += new System.EventHandler(this.m_cmbType_SelectionChangeCommitted);
            // 
            // m_cmbChamp
            // 
            this.m_cmbChamp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_cmbChamp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_cmbChamp.FormattingEnabled = true;
            this.m_cmbChamp.IsLink = false;
            this.m_cmbChamp.ListDonnees = null;
            this.m_cmbChamp.Location = new System.Drawing.Point(247, 0);
            this.m_cmbChamp.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_cmbChamp, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_cmbChamp.Name = "m_cmbChamp";
            this.m_cmbChamp.NullAutorise = false;
            this.m_cmbChamp.ProprieteAffichee = null;
            this.m_cmbChamp.Size = new System.Drawing.Size(233, 21);
            this.m_cmbChamp.TabIndex = 1;
            this.m_cmbChamp.Text = "(empty)";
            this.m_cmbChamp.TextNull = "(empty)";
            this.m_cmbChamp.Tri = true;
            this.m_cmbChamp.SelectionChangeCommitted += new System.EventHandler(this.m_cmbType_SelectionChangeCommitted);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(232, 0);
            this.m_extModeEdition.SetModeEdition(this.panel1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(15, 24);
            this.panel1.TabIndex = 2;
            // 
            // CPanelEditParametreInspiration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Controls.Add(this.m_cmbChamp);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_cmbType);
            this.m_extModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CPanelEditParametreInspiration";
            this.Size = new System.Drawing.Size(480, 24);
            this.ResumeLayout(false);

        }

        #endregion

        private sc2i.win32.common.C2iComboSelectDynamicClass m_cmbType;
        private sc2i.win32.common.CComboboxAutoFilled m_cmbChamp;
        private System.Windows.Forms.Panel panel1;
    }
}
