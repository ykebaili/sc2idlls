namespace sc2i.win32.data.navigation
{
	partial class CArbreObjetHierarchique
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CArbreObjetHierarchique));
            this.m_arbre = new System.Windows.Forms.TreeView();
            this.m_tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.m_btnEdit = new System.Windows.Forms.Button();
            this.m_btnUp = new System.Windows.Forms.Button();
            this.m_btnReaffecter = new System.Windows.Forms.Button();
            this.m_extModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.SuspendLayout();
            // 
            // m_arbre
            // 
            this.m_arbre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_arbre.Location = new System.Drawing.Point(28, 0);
            this.m_extModeEdition.SetModeEdition(this.m_arbre, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_arbre.Name = "m_arbre";
            this.m_arbre.Size = new System.Drawing.Size(295, 216);
            this.m_arbre.TabIndex = 0;
            this.m_arbre.DoubleClick += new System.EventHandler(this.m_arbre_DoubleClick);
            // 
            // m_btnEdit
            // 
            this.m_btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("m_btnEdit.Image")));
            this.m_btnEdit.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_btnEdit, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnEdit.Name = "m_btnEdit";
            this.m_btnEdit.Size = new System.Drawing.Size(30, 24);
            this.m_btnEdit.TabIndex = 4;
            this.m_tooltip.SetToolTip(this.m_btnEdit, "Reach the selected element sheet|30061");
            this.m_btnEdit.UseVisualStyleBackColor = true;
            this.m_btnEdit.Click += new System.EventHandler(this.m_btnEdit_Click);
            // 
            // m_btnUp
            // 
            this.m_btnUp.Image = ((System.Drawing.Image)(resources.GetObject("m_btnUp.Image")));
            this.m_btnUp.Location = new System.Drawing.Point(0, 23);
            this.m_extModeEdition.SetModeEdition(this.m_btnUp, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnUp.Name = "m_btnUp";
            this.m_btnUp.Size = new System.Drawing.Size(30, 24);
            this.m_btnUp.TabIndex = 5;
            this.m_tooltip.SetToolTip(this.m_btnUp, "Reach the parent element sheet|30062");
            this.m_btnUp.UseVisualStyleBackColor = true;
            this.m_btnUp.Click += new System.EventHandler(this.m_btnUp_Click);
            // 
            // m_btnReaffecter
            // 
            this.m_btnReaffecter.Image = ((System.Drawing.Image)(resources.GetObject("m_btnReaffecter.Image")));
            this.m_btnReaffecter.Location = new System.Drawing.Point(0, 46);
            this.m_extModeEdition.SetModeEdition(this.m_btnReaffecter, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnReaffecter.Name = "m_btnReaffecter";
            this.m_btnReaffecter.Size = new System.Drawing.Size(30, 24);
            this.m_btnReaffecter.TabIndex = 6;
            this.m_tooltip.SetToolTip(this.m_btnReaffecter, "Reaffect|30063");
            this.m_btnReaffecter.UseVisualStyleBackColor = true;
            this.m_btnReaffecter.Click += new System.EventHandler(this.m_btnReaffecter_Click);
            // 
            // CArbreObjetHierarchique
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_btnReaffecter);
            this.Controls.Add(this.m_btnUp);
            this.Controls.Add(this.m_btnEdit);
            this.Controls.Add(this.m_arbre);
            this.m_extModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CArbreObjetHierarchique";
            this.Size = new System.Drawing.Size(323, 216);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView m_arbre;
        private System.Windows.Forms.ToolTip m_tooltip;
        private sc2i.win32.common.CExtModeEdition m_extModeEdition;
        private System.Windows.Forms.Button m_btnEdit;
        private System.Windows.Forms.Button m_btnUp;
        private System.Windows.Forms.Button m_btnReaffecter;
    }
}
