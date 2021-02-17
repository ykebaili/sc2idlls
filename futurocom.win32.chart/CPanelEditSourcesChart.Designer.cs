namespace futurocom.win32.chart
{
    partial class CPanelEditSourcesChart
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_lnkDeleteSource = new sc2i.win32.common.CWndLinkStd();
            this.m_lnkDetailSource = new sc2i.win32.common.CWndLinkStd();
            this.m_lnkAddSource = new sc2i.win32.common.CWndLinkStd();
            this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.m_wndListeSources = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.m_menuTypesSources = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_lnkDeleteSource);
            this.panel1.Controls.Add(this.m_lnkDetailSource);
            this.panel1.Controls.Add(this.m_lnkAddSource);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(369, 21);
            this.cExtStyle1.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 0;
            // 
            // m_lnkDeleteSource
            // 
            this.m_lnkDeleteSource.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkDeleteSource.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkDeleteSource.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkDeleteSource.Location = new System.Drawing.Point(200, 0);
            this.m_lnkDeleteSource.Name = "m_lnkDeleteSource";
            this.m_lnkDeleteSource.ShortMode = false;
            this.m_lnkDeleteSource.Size = new System.Drawing.Size(100, 21);
            this.cExtStyle1.SetStyleBackColor(this.m_lnkDeleteSource, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_lnkDeleteSource, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lnkDeleteSource.TabIndex = 2;
            this.m_lnkDeleteSource.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_lnkDeleteSource.LinkClicked += new System.EventHandler(this.m_lnkDeleteSource_LinkClicked);
            // 
            // m_lnkDetailSource
            // 
            this.m_lnkDetailSource.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkDetailSource.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkDetailSource.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkDetailSource.Location = new System.Drawing.Point(100, 0);
            this.m_lnkDetailSource.Name = "m_lnkDetailSource";
            this.m_lnkDetailSource.ShortMode = false;
            this.m_lnkDetailSource.Size = new System.Drawing.Size(100, 21);
            this.cExtStyle1.SetStyleBackColor(this.m_lnkDetailSource, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_lnkDetailSource, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lnkDetailSource.TabIndex = 1;
            this.m_lnkDetailSource.TypeLien = sc2i.win32.common.TypeLinkStd.Modification;
            this.m_lnkDetailSource.LinkClicked += new System.EventHandler(this.m_lnkDetailSource_LinkClicked);
            // 
            // m_lnkAddSource
            // 
            this.m_lnkAddSource.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkAddSource.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkAddSource.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkAddSource.Location = new System.Drawing.Point(0, 0);
            this.m_lnkAddSource.Name = "m_lnkAddSource";
            this.m_lnkAddSource.ShortMode = false;
            this.m_lnkAddSource.Size = new System.Drawing.Size(100, 21);
            this.cExtStyle1.SetStyleBackColor(this.m_lnkAddSource, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_lnkAddSource, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lnkAddSource.TabIndex = 0;
            this.m_lnkAddSource.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_lnkAddSource.LinkClicked += new System.EventHandler(this.m_lnkAddSource_LinkClicked);
            // 
            // m_wndListeSources
            // 
            this.m_wndListeSources.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.m_wndListeSources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeSources.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_wndListeSources.Location = new System.Drawing.Point(0, 21);
            this.m_wndListeSources.MultiSelect = false;
            this.m_wndListeSources.Name = "m_wndListeSources";
            this.m_wndListeSources.Size = new System.Drawing.Size(369, 127);
            this.cExtStyle1.SetStyleBackColor(this.m_wndListeSources, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_wndListeSources, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListeSources.TabIndex = 1;
            this.m_wndListeSources.UseCompatibleStateImageBehavior = false;
            this.m_wndListeSources.View = System.Windows.Forms.View.Details;
            this.m_wndListeSources.SizeChanged += new System.EventHandler(this.m_wndListeSources_SizeChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 339;
            // 
            // m_menuTypesSources
            // 
            this.m_menuTypesSources.Name = "m_menuTypesSources";
            this.m_menuTypesSources.Size = new System.Drawing.Size(61, 4);
            this.cExtStyle1.SetStyleBackColor(this.m_menuTypesSources, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_menuTypesSources, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            // 
            // CPanelEditSourcesChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_wndListeSources);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CPanelEditSourcesChart";
            this.Size = new System.Drawing.Size(369, 148);
            this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private sc2i.win32.common.CWndLinkStd m_lnkDeleteSource;
        private sc2i.win32.common.CExtStyle cExtStyle1;
        private sc2i.win32.common.CWndLinkStd m_lnkDetailSource;
        private sc2i.win32.common.CWndLinkStd m_lnkAddSource;
        private System.Windows.Forms.ListView m_wndListeSources;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ContextMenuStrip m_menuTypesSources;
    }
}
