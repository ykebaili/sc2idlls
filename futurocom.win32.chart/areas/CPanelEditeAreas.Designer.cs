namespace futurocom.win32.chart.Areas
{
    partial class CPanelEditeAreas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPanelEditeAreas));
            this.m_panelListeAreas = new System.Windows.Forms.Panel();
            this.m_wndListeAreas = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_lnkRemoveSerie = new sc2i.win32.common.CWndLinkStd();
            this.m_lnkAddSerie = new sc2i.win32.common.CWndLinkStd();
            this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.m_panelDetailSerie = new System.Windows.Forms.Panel();
            this.m_gridProprietes = new System.Windows.Forms.PropertyGrid();
            this.m_menuArea = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_menuCopyArea = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuPasteArea = new System.Windows.Forms.ToolStripMenuItem();
            this.m_panelListeAreas.SuspendLayout();
            this.panel2.SuspendLayout();
            this.m_panelDetailSerie.SuspendLayout();
            this.m_menuArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panelListeAreas
            // 
            this.m_panelListeAreas.Controls.Add(this.m_wndListeAreas);
            this.m_panelListeAreas.Controls.Add(this.panel2);
            this.m_panelListeAreas.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_panelListeAreas.Location = new System.Drawing.Point(0, 0);
            this.m_panelListeAreas.Name = "m_panelListeAreas";
            this.m_panelListeAreas.Size = new System.Drawing.Size(242, 326);
            this.cExtStyle1.SetStyleBackColor(this.m_panelListeAreas, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelListeAreas, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelListeAreas.TabIndex = 1;
            // 
            // m_wndListeAreas
            // 
            this.m_wndListeAreas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.m_wndListeAreas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeAreas.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_wndListeAreas.HideSelection = false;
            this.m_wndListeAreas.Location = new System.Drawing.Point(0, 25);
            this.m_wndListeAreas.Name = "m_wndListeAreas";
            this.m_wndListeAreas.Size = new System.Drawing.Size(242, 301);
            this.cExtStyle1.SetStyleBackColor(this.m_wndListeAreas, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_wndListeAreas, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListeAreas.TabIndex = 1;
            this.m_wndListeAreas.UseCompatibleStateImageBehavior = false;
            this.m_wndListeAreas.View = System.Windows.Forms.View.Details;
            this.m_wndListeAreas.SelectedIndexChanged += new System.EventHandler(this.m_wndListeAreas_SelectedIndexChanged);
            this.m_wndListeAreas.SizeChanged += new System.EventHandler(this.m_wndListeAreas_SizeChanged);
            this.m_wndListeAreas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_wndListeAreas_MouseUp);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_lnkRemoveSerie);
            this.panel2.Controls.Add(this.m_lnkAddSerie);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(242, 25);
            this.cExtStyle1.SetStyleBackColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel2.TabIndex = 0;
            // 
            // m_lnkRemoveSerie
            // 
            this.m_lnkRemoveSerie.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkRemoveSerie.CustomImage = ((System.Drawing.Image)(resources.GetObject("m_lnkRemoveSerie.CustomImage")));
            this.m_lnkRemoveSerie.CustomText = "Remove";
            this.m_lnkRemoveSerie.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkRemoveSerie.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkRemoveSerie.Location = new System.Drawing.Point(94, 0);
            this.m_lnkRemoveSerie.Name = "m_lnkRemoveSerie";
            this.m_lnkRemoveSerie.ShortMode = false;
            this.m_lnkRemoveSerie.Size = new System.Drawing.Size(94, 25);
            this.cExtStyle1.SetStyleBackColor(this.m_lnkRemoveSerie, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_lnkRemoveSerie, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lnkRemoveSerie.TabIndex = 2;
            this.m_lnkRemoveSerie.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_lnkRemoveSerie.LinkClicked += new System.EventHandler(this.m_lnkRemoveSerie_LinkClicked);
            // 
            // m_lnkAddSerie
            // 
            this.m_lnkAddSerie.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkAddSerie.CustomImage = ((System.Drawing.Image)(resources.GetObject("m_lnkAddSerie.CustomImage")));
            this.m_lnkAddSerie.CustomText = "Add";
            this.m_lnkAddSerie.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkAddSerie.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkAddSerie.Location = new System.Drawing.Point(0, 0);
            this.m_lnkAddSerie.Name = "m_lnkAddSerie";
            this.m_lnkAddSerie.ShortMode = false;
            this.m_lnkAddSerie.Size = new System.Drawing.Size(94, 25);
            this.cExtStyle1.SetStyleBackColor(this.m_lnkAddSerie, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_lnkAddSerie, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lnkAddSerie.TabIndex = 0;
            this.m_lnkAddSerie.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_lnkAddSerie.LinkClicked += new System.EventHandler(this.m_lnkAddSerie_LinkClicked);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(242, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 326);
            this.cExtStyle1.SetStyleBackColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // m_panelDetailSerie
            // 
            this.m_panelDetailSerie.Controls.Add(this.m_gridProprietes);
            this.m_panelDetailSerie.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelDetailSerie.Location = new System.Drawing.Point(245, 0);
            this.m_panelDetailSerie.Name = "m_panelDetailSerie";
            this.m_panelDetailSerie.Size = new System.Drawing.Size(255, 326);
            this.cExtStyle1.SetStyleBackColor(this.m_panelDetailSerie, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelDetailSerie, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelDetailSerie.TabIndex = 3;
            // 
            // m_gridProprietes
            // 
            this.m_gridProprietes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_gridProprietes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridProprietes.ForeColor = System.Drawing.Color.Black;
            this.m_gridProprietes.Location = new System.Drawing.Point(0, 0);
            this.m_gridProprietes.Name = "m_gridProprietes";
            this.m_gridProprietes.Size = new System.Drawing.Size(255, 326);
            this.cExtStyle1.SetStyleBackColor(this.m_gridProprietes, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.m_gridProprietes, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.m_gridProprietes.TabIndex = 1;
            this.m_gridProprietes.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.m_gridProprietes_PropertyValueChanged);
            // 
            // m_menuArea
            // 
            this.m_menuArea.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_menuCopyArea,
            this.m_menuPasteArea});
            this.m_menuArea.Name = "m_menuArea";
            this.m_menuArea.Size = new System.Drawing.Size(153, 70);
            this.cExtStyle1.SetStyleBackColor(this.m_menuArea, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_menuArea, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            // 
            // m_menuCopyArea
            // 
            this.m_menuCopyArea.Name = "m_menuCopyArea";
            this.m_menuCopyArea.Size = new System.Drawing.Size(152, 22);
            this.m_menuCopyArea.Text = "Copy|20051";
            this.m_menuCopyArea.Click += new System.EventHandler(this.m_menuCopyArea_Click);
            // 
            // m_menuPasteArea
            // 
            this.m_menuPasteArea.Name = "m_menuPasteArea";
            this.m_menuPasteArea.Size = new System.Drawing.Size(152, 22);
            this.m_menuPasteArea.Text = "Paste|20052";
            this.m_menuPasteArea.Click += new System.EventHandler(this.m_menuPasteArea_Click);
            // 
            // CPanelEditeAreas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_panelDetailSerie);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.m_panelListeAreas);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CPanelEditeAreas";
            this.Size = new System.Drawing.Size(500, 326);
            this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.m_panelListeAreas.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.m_panelDetailSerie.ResumeLayout(false);
            this.m_menuArea.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_panelListeAreas;
        private System.Windows.Forms.ListView m_wndListeAreas;
        private sc2i.win32.common.CExtStyle cExtStyle1;
        private System.Windows.Forms.Panel panel2;
        private sc2i.win32.common.CWndLinkStd m_lnkRemoveSerie;
        private sc2i.win32.common.CWndLinkStd m_lnkAddSerie;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel m_panelDetailSerie;
        private System.Windows.Forms.PropertyGrid m_gridProprietes;
        private System.Windows.Forms.ContextMenuStrip m_menuArea;
        private System.Windows.Forms.ToolStripMenuItem m_menuCopyArea;
        private System.Windows.Forms.ToolStripMenuItem m_menuPasteArea;
    }
}
