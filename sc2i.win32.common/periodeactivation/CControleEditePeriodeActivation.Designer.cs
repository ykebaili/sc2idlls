namespace sc2i.win32.common.periodeactivation
{
    partial class CControleEditePeriodeActivation
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
            this.m_arbre = new System.Windows.Forms.TreeView();
            this.m_panelEditPeriode = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.m_menuArbre = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_menuAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuInsert = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuDeleteElement = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuDeleteAndMoveToTop = new System.Windows.Forms.ToolStripMenuItem();
            this.m_extStyle = new sc2i.win32.common.CExtStyle();
            this.m_extModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_menuArbre.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_arbre
            // 
            this.m_arbre.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_arbre.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_arbre, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_arbre.Name = "m_arbre";
            this.m_arbre.Size = new System.Drawing.Size(216, 306);
            this.m_extStyle.SetStyleBackColor(this.m_arbre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_arbre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_arbre.TabIndex = 0;
            this.m_arbre.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_arbre_MouseUp);
            this.m_arbre.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.m_arbre_AfterSelect);
            // 
            // m_panelEditPeriode
            // 
            this.m_panelEditPeriode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelEditPeriode.Location = new System.Drawing.Point(219, 0);
            this.m_extModeEdition.SetModeEdition(this.m_panelEditPeriode, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelEditPeriode.Name = "m_panelEditPeriode";
            this.m_panelEditPeriode.Size = new System.Drawing.Size(385, 306);
            this.m_extStyle.SetStyleBackColor(this.m_panelEditPeriode, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelEditPeriode, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelEditPeriode.TabIndex = 1;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(216, 0);
            this.m_extModeEdition.SetModeEdition(this.splitter1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 306);
            this.m_extStyle.SetStyleBackColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // m_menuArbre
            // 
            this.m_menuArbre.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_menuAdd,
            this.m_menuInsert,
            this.m_menuDelete});
            this.m_extModeEdition.SetModeEdition(this.m_menuArbre, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_menuArbre.Name = "contextMenuStrip1";
            this.m_menuArbre.Size = new System.Drawing.Size(145, 70);
            this.m_extStyle.SetStyleBackColor(this.m_menuArbre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_menuArbre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_menuArbre.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // m_menuAdd
            // 
            this.m_menuAdd.Name = "m_menuAdd";
            this.m_menuAdd.Size = new System.Drawing.Size(144, 22);
            this.m_menuAdd.Text = "Add|2003";
            // 
            // m_menuInsert
            // 
            this.m_menuInsert.Name = "m_menuInsert";
            this.m_menuInsert.Size = new System.Drawing.Size(144, 22);
            this.m_menuInsert.Text = "Insert|2004";
            // 
            // m_menuDelete
            // 
            this.m_menuDelete.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_menuDeleteElement,
            this.m_menuDeleteAndMoveToTop});
            this.m_menuDelete.Name = "m_menuDelete";
            this.m_menuDelete.Size = new System.Drawing.Size(144, 22);
            this.m_menuDelete.Text = "Delete|2005";
            // 
            // m_menuDeleteElement
            // 
            this.m_menuDeleteElement.Name = "m_menuDeleteElement";
            this.m_menuDeleteElement.Size = new System.Drawing.Size(218, 22);
            this.m_menuDeleteElement.Text = "Element and children|20008";
            this.m_menuDeleteElement.Click += new System.EventHandler(this.m_menuDeleteElement_Click);
            // 
            // m_menuDeleteAndMoveToTop
            // 
            this.m_menuDeleteAndMoveToTop.Name = "m_menuDeleteAndMoveToTop";
            this.m_menuDeleteAndMoveToTop.Size = new System.Drawing.Size(218, 22);
            this.m_menuDeleteAndMoveToTop.Text = "Move children to top|20009";
            this.m_menuDeleteAndMoveToTop.Click += new System.EventHandler(this.m_menuDeleteAndMoveToTop_Click);
            // 
            // CControleEditePeriodeActivation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_panelEditPeriode);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.m_arbre);
            this.ForeColor = System.Drawing.Color.Black;
            this.m_extModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CControleEditePeriodeActivation";
            this.Size = new System.Drawing.Size(604, 306);
            this.m_extStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_extStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.m_menuArbre.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView m_arbre;
        private System.Windows.Forms.Panel m_panelEditPeriode;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ContextMenuStrip m_menuArbre;
        private System.Windows.Forms.ToolStripMenuItem m_menuAdd;
        private System.Windows.Forms.ToolStripMenuItem m_menuInsert;
        private System.Windows.Forms.ToolStripMenuItem m_menuDelete;
        private CExtStyle m_extStyle;
        private System.Windows.Forms.ToolStripMenuItem m_menuDeleteElement;
        private System.Windows.Forms.ToolStripMenuItem m_menuDeleteAndMoveToTop;
        private CExtModeEdition m_extModeEdition;
    }
}
