namespace sc2i.formulaire.win32.controles2iWnd.datagrid
{
    partial class CDataGridForFormulaire
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
            this.m_panelTop = new System.Windows.Forms.Panel();
            this.m_lnkDelete = new sc2i.win32.common.CWndLinkStd();
            this.m_lnkAdd = new sc2i.win32.common.CWndLinkStd();
            this.m_grid = new System.Windows.Forms.DataGridView();
            this.m_rclickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_menuSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuCopier = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuFiltre = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_menuFiltreSpecial = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuNoFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_grid)).BeginInit();
            this.m_rclickMenu.SuspendLayout();
            this.m_menuFiltre.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panelTop
            // 
            this.m_panelTop.Controls.Add(this.m_lnkDelete);
            this.m_panelTop.Controls.Add(this.m_lnkAdd);
            this.m_panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelTop.Location = new System.Drawing.Point(0, 0);
            this.m_panelTop.Name = "m_panelTop";
            this.m_panelTop.Size = new System.Drawing.Size(435, 21);
            this.m_panelTop.TabIndex = 0;
            // 
            // m_lnkDelete
            // 
            this.m_lnkDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkDelete.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkDelete.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkDelete.Location = new System.Drawing.Point(112, 0);
            this.m_lnkDelete.Name = "m_lnkDelete";
            this.m_lnkDelete.ShortMode = false;
            this.m_lnkDelete.Size = new System.Drawing.Size(112, 21);
            this.m_lnkDelete.TabIndex = 1;
            this.m_lnkDelete.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_lnkDelete.LinkClicked += new System.EventHandler(this.m_lnkDelete_LinkClicked);
            // 
            // m_lnkAdd
            // 
            this.m_lnkAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkAdd.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkAdd.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkAdd.Location = new System.Drawing.Point(0, 0);
            this.m_lnkAdd.Name = "m_lnkAdd";
            this.m_lnkAdd.ShortMode = false;
            this.m_lnkAdd.Size = new System.Drawing.Size(112, 21);
            this.m_lnkAdd.TabIndex = 0;
            this.m_lnkAdd.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_lnkAdd.LinkClicked += new System.EventHandler(this.m_lnkAdd_LinkClicked);
            // 
            // m_grid
            // 
            this.m_grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_grid.Location = new System.Drawing.Point(0, 21);
            this.m_grid.Name = "m_grid";
            this.m_grid.Size = new System.Drawing.Size(435, 191);
            this.m_grid.TabIndex = 1;
            this.m_grid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.m_grid_RowEnter);
            this.m_grid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.m_grid_ColumnHeaderMouseClick);
            this.m_grid.CellStyleChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.m_grid_CellStyleChanged);
            this.m_grid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_grid_MouseUp);
            this.m_grid.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.m_grid_CellPainting);
            this.m_grid.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.m_grid_DataBindingComplete);
            this.m_grid.DataSourceChanged += new System.EventHandler(this.m_grid_DataSourceChanged);
            this.m_grid.SelectionChanged += new System.EventHandler(this.m_grid_SelectionChanged);
            // 
            // m_rclickMenu
            // 
            this.m_rclickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_menuSelectAll,
            this.m_menuCopier});
            this.m_rclickMenu.Name = "m_rclickMenu";
            this.m_rclickMenu.Size = new System.Drawing.Size(162, 48);
            this.m_rclickMenu.Text = "Select all|20021";
            // 
            // m_menuSelectAll
            // 
            this.m_menuSelectAll.Name = "m_menuSelectAll";
            this.m_menuSelectAll.Size = new System.Drawing.Size(161, 22);
            this.m_menuSelectAll.Text = "Select all|20020";
            this.m_menuSelectAll.Click += new System.EventHandler(this.m_menuSelectAll_Click);
            // 
            // m_menuCopier
            // 
            this.m_menuCopier.Name = "m_menuCopier";
            this.m_menuCopier.Size = new System.Drawing.Size(161, 22);
            this.m_menuCopier.Text = "Copy|20021";
            this.m_menuCopier.Click += new System.EventHandler(this.m_menuCopier_Click);
            // 
            // m_menuFiltre
            // 
            this.m_menuFiltre.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_menuFiltreSpecial,
            this.m_menuNoFilter,
            this.toolStripMenuItem1});
            this.m_menuFiltre.Name = "m_menuFiltre";
            this.m_menuFiltre.Size = new System.Drawing.Size(158, 76);
            // 
            // m_menuFiltreSpecial
            // 
            this.m_menuFiltreSpecial.Name = "m_menuFiltreSpecial";
            this.m_menuFiltreSpecial.Size = new System.Drawing.Size(157, 22);
            this.m_menuFiltreSpecial.Text = "Filter|20032";
            // 
            // m_menuNoFilter
            // 
            this.m_menuNoFilter.Name = "m_menuNoFilter";
            this.m_menuNoFilter.Size = new System.Drawing.Size(157, 22);
            this.m_menuNoFilter.Text = "No filter|20036";
            this.m_menuNoFilter.Click += new System.EventHandler(this.m_menuNoFilter_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(154, 6);
            // 
            // CDataGridForFormulaire
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_grid);
            this.Controls.Add(this.m_panelTop);
            this.Name = "CDataGridForFormulaire";
            this.Size = new System.Drawing.Size(435, 212);
            this.m_panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_grid)).EndInit();
            this.m_rclickMenu.ResumeLayout(false);
            this.m_menuFiltre.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_panelTop;
        private sc2i.win32.common.CWndLinkStd m_lnkAdd;
        private sc2i.win32.common.CWndLinkStd m_lnkDelete;
        private System.Windows.Forms.DataGridView m_grid;
        private System.Windows.Forms.ContextMenuStrip m_rclickMenu;
        private System.Windows.Forms.ToolStripMenuItem m_menuSelectAll;
        private System.Windows.Forms.ToolStripMenuItem m_menuCopier;
        private System.Windows.Forms.ContextMenuStrip m_menuFiltre;
        private System.Windows.Forms.ToolStripMenuItem m_menuFiltreSpecial;
        private System.Windows.Forms.ToolStripMenuItem m_menuNoFilter;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    }
}
