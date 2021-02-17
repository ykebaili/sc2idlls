namespace futurocom.win32.chart.series
{
    partial class CPanelEditeSeries
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPanelEditeSeries));
            this.m_panelListeSeries = new System.Windows.Forms.Panel();
            this.m_wndListeSeries = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_lnkRemoveSerie = new sc2i.win32.common.CWndLinkStd();
            this.m_lnkAddSerie = new sc2i.win32.common.CWndLinkStd();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnDown = new System.Windows.Forms.Button();
            this.m_btnUp = new System.Windows.Forms.Button();
            this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.m_panelDetailSerie = new System.Windows.Forms.Panel();
            this.m_gridProprietes = new System.Windows.Forms.PropertyGrid();
            this.m_menuSerie = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_menuCopieSerie = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuPasteSerie = new System.Windows.Forms.ToolStripMenuItem();
            this.m_panelListeSeries.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.m_panelDetailSerie.SuspendLayout();
            this.m_menuSerie.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panelListeSeries
            // 
            this.m_panelListeSeries.Controls.Add(this.m_wndListeSeries);
            this.m_panelListeSeries.Controls.Add(this.panel2);
            this.m_panelListeSeries.Controls.Add(this.panel1);
            this.m_panelListeSeries.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_panelListeSeries.Location = new System.Drawing.Point(0, 0);
            this.m_panelListeSeries.Name = "m_panelListeSeries";
            this.m_panelListeSeries.Size = new System.Drawing.Size(242, 326);
            this.cExtStyle1.SetStyleBackColor(this.m_panelListeSeries, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelListeSeries, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelListeSeries.TabIndex = 1;
            // 
            // m_wndListeSeries
            // 
            this.m_wndListeSeries.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.m_wndListeSeries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeSeries.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_wndListeSeries.HideSelection = false;
            this.m_wndListeSeries.Location = new System.Drawing.Point(0, 25);
            this.m_wndListeSeries.Name = "m_wndListeSeries";
            this.m_wndListeSeries.Size = new System.Drawing.Size(242, 272);
            this.cExtStyle1.SetStyleBackColor(this.m_wndListeSeries, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_wndListeSeries, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListeSeries.TabIndex = 1;
            this.m_wndListeSeries.UseCompatibleStateImageBehavior = false;
            this.m_wndListeSeries.View = System.Windows.Forms.View.Details;
            this.m_wndListeSeries.SelectedIndexChanged += new System.EventHandler(this.m_wndListeSeries_SelectedIndexChanged);
            this.m_wndListeSeries.SizeChanged += new System.EventHandler(this.m_wndListeSeries_SizeChanged);
            this.m_wndListeSeries.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_wndListeSeries_MouseUp);
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
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnDown);
            this.panel1.Controls.Add(this.m_btnUp);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 297);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(242, 29);
            this.cExtStyle1.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 2;
            // 
            // m_btnDown
            // 
            this.m_btnDown.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnDown.Image = global::futurocom.win32.chart.Resource1.down_blue;
            this.m_btnDown.Location = new System.Drawing.Point(34, 0);
            this.m_btnDown.Name = "m_btnDown";
            this.m_btnDown.Size = new System.Drawing.Size(34, 29);
            this.cExtStyle1.SetStyleBackColor(this.m_btnDown, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnDown, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnDown.TabIndex = 1;
            this.m_btnDown.UseVisualStyleBackColor = true;
            this.m_btnDown.Click += new System.EventHandler(this.m_btnDown_Click);
            // 
            // m_btnUp
            // 
            this.m_btnUp.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnUp.Image = global::futurocom.win32.chart.Resource1.up_blue;
            this.m_btnUp.Location = new System.Drawing.Point(0, 0);
            this.m_btnUp.Name = "m_btnUp";
            this.m_btnUp.Size = new System.Drawing.Size(34, 29);
            this.cExtStyle1.SetStyleBackColor(this.m_btnUp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnUp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnUp.TabIndex = 0;
            this.m_btnUp.UseVisualStyleBackColor = true;
            this.m_btnUp.Click += new System.EventHandler(this.m_btnUp_Click);
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
            // m_menuSerie
            // 
            this.m_menuSerie.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_menuCopieSerie,
            this.m_menuPasteSerie});
            this.m_menuSerie.Name = "m_menuSerie";
            this.m_menuSerie.Size = new System.Drawing.Size(136, 48);
            this.cExtStyle1.SetStyleBackColor(this.m_menuSerie, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_menuSerie, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            // 
            // m_menuCopieSerie
            // 
            this.m_menuCopieSerie.Name = "m_menuCopieSerie";
            this.m_menuCopieSerie.Size = new System.Drawing.Size(135, 22);
            this.m_menuCopieSerie.Text = "Copy|20051";
            this.m_menuCopieSerie.Click += new System.EventHandler(this.m_menuCopySeries_Click);
            // 
            // m_menuPasteSerie
            // 
            this.m_menuPasteSerie.Name = "m_menuPasteSerie";
            this.m_menuPasteSerie.Size = new System.Drawing.Size(135, 22);
            this.m_menuPasteSerie.Text = "Paste|20052";
            this.m_menuPasteSerie.Click += new System.EventHandler(this.m_menuPasteSeries_Click);
            // 
            // CPanelEditeSeries
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_panelDetailSerie);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.m_panelListeSeries);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CPanelEditeSeries";
            this.Size = new System.Drawing.Size(500, 326);
            this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.m_panelListeSeries.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.m_panelDetailSerie.ResumeLayout(false);
            this.m_menuSerie.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_panelListeSeries;
        private System.Windows.Forms.ListView m_wndListeSeries;
        private sc2i.win32.common.CExtStyle cExtStyle1;
        private System.Windows.Forms.Panel panel2;
        private sc2i.win32.common.CWndLinkStd m_lnkRemoveSerie;
        private sc2i.win32.common.CWndLinkStd m_lnkAddSerie;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel m_panelDetailSerie;
        private System.Windows.Forms.PropertyGrid m_gridProprietes;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button m_btnDown;
        private System.Windows.Forms.Button m_btnUp;
        private System.Windows.Forms.ContextMenuStrip m_menuSerie;
        private System.Windows.Forms.ToolStripMenuItem m_menuCopieSerie;
        private System.Windows.Forms.ToolStripMenuItem m_menuPasteSerie;
    }
}
