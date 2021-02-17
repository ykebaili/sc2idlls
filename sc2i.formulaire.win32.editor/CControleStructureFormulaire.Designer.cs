namespace sc2i.formulaire.win32
{
    partial class CControleStructureFormulaire
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CControleStructureFormulaire));
            this.m_arbre = new System.Windows.Forms.TreeView();
            this.m_images = new System.Windows.Forms.ImageList(this.components);
            this.m_panelBottom = new System.Windows.Forms.Panel();
            this.m_btnMonter = new System.Windows.Forms.Button();
            this.m_btnDescendre = new System.Windows.Forms.Button();
            this.m_extModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_arbre
            // 
            this.m_arbre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_arbre.HideSelection = false;
            this.m_arbre.ImageIndex = 0;
            this.m_arbre.ImageList = this.m_images;
            this.m_arbre.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_arbre, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_arbre.Name = "m_arbre";
            this.m_arbre.SelectedImageIndex = 0;
            this.m_arbre.Size = new System.Drawing.Size(361, 256);
            this.m_arbre.TabIndex = 0;
            this.m_arbre.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.m_arbre_AfterSelect);
            // 
            // m_images
            // 
            this.m_images.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_images.ImageStream")));
            this.m_images.TransparentColor = System.Drawing.Color.Transparent;
            this.m_images.Images.SetKeyName(0, "paramétrage.gif");
            // 
            // m_panelBottom
            // 
            this.m_panelBottom.Controls.Add(this.m_btnMonter);
            this.m_panelBottom.Controls.Add(this.m_btnDescendre);
            this.m_panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_panelBottom.Location = new System.Drawing.Point(0, 256);
            this.m_extModeEdition.SetModeEdition(this.m_panelBottom, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelBottom.Name = "m_panelBottom";
            this.m_panelBottom.Size = new System.Drawing.Size(361, 28);
            this.m_panelBottom.TabIndex = 1;
            // 
            // m_btnMonter
            // 
            this.m_btnMonter.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnMonter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnMonter.Image = global::sc2i.formulaire.win32.Properties.Resources.haut;
            this.m_btnMonter.Location = new System.Drawing.Point(291, 0);
            this.m_extModeEdition.SetModeEdition(this.m_btnMonter, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnMonter.Name = "m_btnMonter";
            this.m_btnMonter.Size = new System.Drawing.Size(35, 28);
            this.m_btnMonter.TabIndex = 0;
            this.m_btnMonter.UseVisualStyleBackColor = true;
            this.m_btnMonter.Click += new System.EventHandler(this.m_btnMonter_Click);
            // 
            // m_btnDescendre
            // 
            this.m_btnDescendre.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnDescendre.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnDescendre.Image = global::sc2i.formulaire.win32.Properties.Resources.bas;
            this.m_btnDescendre.Location = new System.Drawing.Point(326, 0);
            this.m_extModeEdition.SetModeEdition(this.m_btnDescendre, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnDescendre.Name = "m_btnDescendre";
            this.m_btnDescendre.Size = new System.Drawing.Size(35, 28);
            this.m_btnDescendre.TabIndex = 1;
            this.m_btnDescendre.UseVisualStyleBackColor = true;
            this.m_btnDescendre.Click += new System.EventHandler(this.m_btnDescendre_Click);
            // 
            // CControleStructureFormulaire
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_arbre);
            this.Controls.Add(this.m_panelBottom);
            this.m_extModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CControleStructureFormulaire";
            this.Size = new System.Drawing.Size(361, 284);
            this.m_panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView m_arbre;
        private System.Windows.Forms.ImageList m_images;
        private System.Windows.Forms.Panel m_panelBottom;
        private System.Windows.Forms.Button m_btnMonter;
        private System.Windows.Forms.Button m_btnDescendre;
        private sc2i.win32.common.CExtModeEdition m_extModeEdition;
    }
}
