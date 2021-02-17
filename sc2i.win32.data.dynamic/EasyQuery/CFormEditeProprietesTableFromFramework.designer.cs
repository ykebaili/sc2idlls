namespace sc2i.win32.data.dynamic.easyquery
{
    partial class CFormEditeProprietesTableFromFramework
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditeProprietesTableFromFramework));
            this.label1 = new System.Windows.Forms.Label();
            this.m_lblSource = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_txtNomTable = new System.Windows.Forms.TextBox();
            this.m_exStyle = new sc2i.win32.common.CExtStyle();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_wndListeColonnes = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_tabControl = new sc2i.win32.common.C2iTabControl(this.components);
            this.tabPage1 = new Crownwood.Magic.Controls.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnRemoveColumn = new sc2i.win32.common.CWndLinkStd();
            this.m_btnEditColumn = new sc2i.win32.common.CWndLinkStd();
            this.m_wndAddColumn = new sc2i.win32.common.CWndLinkStd();
            this.tabPage2 = new Crownwood.Magic.Controls.TabPage();
            this.m_ctrlFormulesNommees = new sc2i.win32.expression.CControlEditListeFormulesNommees();
            this.m_pageOptions = new Crownwood.Magic.Controls.TabPage();
            this.m_pageFiltre = new Crownwood.Magic.Controls.TabPage();
            this.m_panelFiltre = new sc2i.win32.data.dynamic.CPanelEditFiltreDynamique();
            this.m_chkUseCache = new System.Windows.Forms.CheckBox();
            this.m_pagePostFiltre = new Crownwood.Magic.Controls.TabPage();
            this.m_panelPostFilter = new futurocom.win32.easyquery.postFilter.CPanelPostFilter();
            this.m_tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.m_pageFiltre.SuspendLayout();
            this.m_pagePostFiltre.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 22);
            this.m_exStyle.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source table|20111";
            // 
            // m_lblSource
            // 
            this.m_lblSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblSource.BackColor = System.Drawing.Color.White;
            this.m_lblSource.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblSource.Location = new System.Drawing.Point(139, 9);
            this.m_lblSource.Name = "m_lblSource";
            this.m_lblSource.Size = new System.Drawing.Size(580, 23);
            this.m_exStyle.SetStyleBackColor(this.m_lblSource, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_lblSource, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lblSource.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 17);
            this.m_exStyle.SetStyleBackColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label3.TabIndex = 2;
            this.label3.Text = "Table name|20112";
            // 
            // m_txtNomTable
            // 
            this.m_txtNomTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtNomTable.Location = new System.Drawing.Point(139, 36);
            this.m_txtNomTable.Name = "m_txtNomTable";
            this.m_txtNomTable.Size = new System.Drawing.Size(580, 20);
            this.m_exStyle.SetStyleBackColor(this.m_txtNomTable, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_txtNomTable, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtNomTable.TabIndex = 3;
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnOk.Location = new System.Drawing.Point(280, 396);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_exStyle.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 4;
            this.m_btnOk.Text = "Ok|10";
            this.m_btnOk.UseVisualStyleBackColor = true;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnAnnuler.Location = new System.Drawing.Point(375, 396);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(75, 23);
            this.m_exStyle.SetStyleBackColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAnnuler.TabIndex = 5;
            this.m_btnAnnuler.Text = "Cancel|11";
            this.m_btnAnnuler.UseVisualStyleBackColor = true;
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // m_wndListeColonnes
            // 
            this.m_wndListeColonnes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.m_wndListeColonnes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeColonnes.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_wndListeColonnes.HideSelection = false;
            this.m_wndListeColonnes.LabelEdit = true;
            this.m_wndListeColonnes.Location = new System.Drawing.Point(0, 24);
            this.m_wndListeColonnes.Name = "m_wndListeColonnes";
            this.m_wndListeColonnes.Size = new System.Drawing.Size(711, 256);
            this.m_exStyle.SetStyleBackColor(this.m_wndListeColonnes, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_wndListeColonnes, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListeColonnes.TabIndex = 6;
            this.m_wndListeColonnes.UseCompatibleStateImageBehavior = false;
            this.m_wndListeColonnes.View = System.Windows.Forms.View.Details;
            this.m_wndListeColonnes.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.m_wndListeColonnes_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name|20105";
            this.columnHeader1.Width = 462;
            // 
            // m_tabControl
            // 
            this.m_tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tabControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_tabControl.BoldSelectedPage = true;
            this.m_tabControl.ForeColor = System.Drawing.Color.Black;
            this.m_tabControl.IDEPixelArea = false;
            this.m_tabControl.Location = new System.Drawing.Point(12, 85);
            this.m_tabControl.Name = "m_tabControl";
            this.m_tabControl.Ombre = false;
            this.m_tabControl.PositionTop = true;
            this.m_tabControl.SelectedIndex = 4;
            this.m_tabControl.SelectedTab = this.m_pagePostFiltre;
            this.m_tabControl.Size = new System.Drawing.Size(711, 305);
            this.m_exStyle.SetStyleBackColor(this.m_tabControl, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_exStyle.SetStyleForeColor(this.m_tabControl, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.m_tabControl.TabIndex = 8;
            this.m_tabControl.TabPages.AddRange(new Crownwood.Magic.Controls.TabPage[] {
            this.tabPage1,
            this.tabPage2,
            this.m_pageOptions,
            this.m_pageFiltre,
            this.m_pagePostFiltre});
            this.m_tabControl.TextColor = System.Drawing.Color.Black;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_wndListeColonnes);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(0, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Selected = false;
            this.tabPage1.Size = new System.Drawing.Size(711, 280);
            this.m_exStyle.SetStyleBackColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage1.TabIndex = 10;
            this.tabPage1.Title = "Columns|20106";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnRemoveColumn);
            this.panel1.Controls.Add(this.m_btnEditColumn);
            this.panel1.Controls.Add(this.m_wndAddColumn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(711, 24);
            this.m_exStyle.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 7;
            // 
            // m_btnRemoveColumn
            // 
            this.m_btnRemoveColumn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnRemoveColumn.CustomImage = ((System.Drawing.Image)(resources.GetObject("m_btnRemoveColumn.CustomImage")));
            this.m_btnRemoveColumn.CustomText = "Remove";
            this.m_btnRemoveColumn.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_btnRemoveColumn.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_btnRemoveColumn.Location = new System.Drawing.Point(224, 0);
            this.m_btnRemoveColumn.Name = "m_btnRemoveColumn";
            this.m_btnRemoveColumn.ShortMode = false;
            this.m_btnRemoveColumn.Size = new System.Drawing.Size(112, 24);
            this.m_exStyle.SetStyleBackColor(this.m_btnRemoveColumn, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_btnRemoveColumn, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnRemoveColumn.TabIndex = 1;
            this.m_btnRemoveColumn.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_btnRemoveColumn.LinkClicked += new System.EventHandler(this.m_btnRemoveColumn_LinkClicked);
            // 
            // m_btnEditColumn
            // 
            this.m_btnEditColumn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnEditColumn.CustomImage = ((System.Drawing.Image)(resources.GetObject("m_btnEditColumn.CustomImage")));
            this.m_btnEditColumn.CustomText = "Detail";
            this.m_btnEditColumn.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_btnEditColumn.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_btnEditColumn.Location = new System.Drawing.Point(112, 0);
            this.m_btnEditColumn.Name = "m_btnEditColumn";
            this.m_btnEditColumn.ShortMode = false;
            this.m_btnEditColumn.Size = new System.Drawing.Size(112, 24);
            this.m_exStyle.SetStyleBackColor(this.m_btnEditColumn, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_btnEditColumn, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnEditColumn.TabIndex = 2;
            this.m_btnEditColumn.TypeLien = sc2i.win32.common.TypeLinkStd.Modification;
            this.m_btnEditColumn.LinkClicked += new System.EventHandler(this.m_btnEditColumn_LinkClicked);
            // 
            // m_wndAddColumn
            // 
            this.m_wndAddColumn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_wndAddColumn.CustomImage = ((System.Drawing.Image)(resources.GetObject("m_wndAddColumn.CustomImage")));
            this.m_wndAddColumn.CustomText = "Add";
            this.m_wndAddColumn.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_wndAddColumn.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_wndAddColumn.Location = new System.Drawing.Point(0, 0);
            this.m_wndAddColumn.Name = "m_wndAddColumn";
            this.m_wndAddColumn.ShortMode = false;
            this.m_wndAddColumn.Size = new System.Drawing.Size(112, 24);
            this.m_exStyle.SetStyleBackColor(this.m_wndAddColumn, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_wndAddColumn, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndAddColumn.TabIndex = 0;
            this.m_wndAddColumn.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_wndAddColumn.LinkClicked += new System.EventHandler(this.m_wndAddColumn_LinkClicked);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.m_ctrlFormulesNommees);
            this.tabPage2.Location = new System.Drawing.Point(0, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Selected = false;
            this.tabPage2.Size = new System.Drawing.Size(711, 280);
            this.m_exStyle.SetStyleBackColor(this.tabPage2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.tabPage2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage2.TabIndex = 11;
            this.tabPage2.Title = "Calculated columns|20107";
            // 
            // m_ctrlFormulesNommees
            // 
            this.m_ctrlFormulesNommees.AutoScroll = true;
            this.m_ctrlFormulesNommees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_ctrlFormulesNommees.HasDeleteButton = true;
            this.m_ctrlFormulesNommees.HasHadButton = true;
            this.m_ctrlFormulesNommees.HeaderTextForFormula = "";
            this.m_ctrlFormulesNommees.HeaderTextForName = "";
            this.m_ctrlFormulesNommees.HideNomFormule = false;
            this.m_ctrlFormulesNommees.Location = new System.Drawing.Point(0, 0);
            this.m_ctrlFormulesNommees.LockEdition = false;
            this.m_ctrlFormulesNommees.Name = "m_ctrlFormulesNommees";
            this.m_ctrlFormulesNommees.Size = new System.Drawing.Size(711, 280);
            this.m_exStyle.SetStyleBackColor(this.m_ctrlFormulesNommees, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_ctrlFormulesNommees, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ctrlFormulesNommees.TabIndex = 0;
            this.m_ctrlFormulesNommees.TypeFormuleNomme = typeof(sc2i.expression.CFormuleNommee);
            // 
            // m_pageOptions
            // 
            this.m_pageOptions.Location = new System.Drawing.Point(0, 25);
            this.m_pageOptions.Name = "m_pageOptions";
            this.m_pageOptions.Selected = false;
            this.m_pageOptions.Size = new System.Drawing.Size(711, 280);
            this.m_exStyle.SetStyleBackColor(this.m_pageOptions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_pageOptions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_pageOptions.TabIndex = 12;
            this.m_pageOptions.Title = "Options|20108";
            // 
            // m_pageFiltre
            // 
            this.m_pageFiltre.Controls.Add(this.m_panelFiltre);
            this.m_pageFiltre.Location = new System.Drawing.Point(0, 25);
            this.m_pageFiltre.Name = "m_pageFiltre";
            this.m_pageFiltre.Selected = false;
            this.m_pageFiltre.Size = new System.Drawing.Size(711, 280);
            this.m_exStyle.SetStyleBackColor(this.m_pageFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_pageFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_pageFiltre.TabIndex = 13;
            this.m_pageFiltre.Title = "Filter|20109";
            // 
            // m_panelFiltre
            // 
            this.m_panelFiltre.BackColor = System.Drawing.Color.White;
            this.m_panelFiltre.DefinitionRacineDeChampsFiltres = null;
            this.m_panelFiltre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelFiltre.FiltreDynamique = null;
            this.m_panelFiltre.Location = new System.Drawing.Point(0, 0);
            this.m_panelFiltre.LockEdition = false;
            this.m_panelFiltre.ModeFiltreExpression = false;
            this.m_panelFiltre.ModeSansType = true;
            this.m_panelFiltre.Name = "m_panelFiltre";
            this.m_panelFiltre.Size = new System.Drawing.Size(711, 280);
            this.m_exStyle.SetStyleBackColor(this.m_panelFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_panelFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelFiltre.TabIndex = 0;
            // 
            // m_chkUseCache
            // 
            this.m_chkUseCache.AutoSize = true;
            this.m_chkUseCache.Location = new System.Drawing.Point(139, 62);
            this.m_chkUseCache.Name = "m_chkUseCache";
            this.m_chkUseCache.Size = new System.Drawing.Size(134, 17);
            this.m_exStyle.SetStyleBackColor(this.m_chkUseCache, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_chkUseCache, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkUseCache.TabIndex = 14;
            this.m_chkUseCache.Text = "Use data cache|20063";
            this.m_chkUseCache.UseVisualStyleBackColor = true;
            // 
            // m_pagePostFiltre
            // 
            this.m_pagePostFiltre.Controls.Add(this.m_panelPostFilter);
            this.m_pagePostFiltre.Location = new System.Drawing.Point(0, 25);
            this.m_pagePostFiltre.Name = "m_pagePostFiltre";
            this.m_pagePostFiltre.Size = new System.Drawing.Size(711, 280);
            this.m_exStyle.SetStyleBackColor(this.m_pagePostFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_pagePostFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_pagePostFiltre.TabIndex = 14;
            this.m_pagePostFiltre.Title = "Post filter|20164";
            // 
            // m_panelPostFilter
            // 
            this.m_panelPostFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelPostFilter.Location = new System.Drawing.Point(0, 0);
            this.m_panelPostFilter.Name = "m_panelPostFilter";
            this.m_panelPostFilter.Size = new System.Drawing.Size(711, 280);
            this.m_exStyle.SetStyleBackColor(this.m_panelPostFilter, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_panelPostFilter, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelPostFilter.TabIndex = 0;
            // 
            // CFormEditeProprietesTableFromFramework
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 432);
            this.ControlBox = false;
            this.Controls.Add(this.m_chkUseCache);
            this.Controls.Add(this.m_tabControl);
            this.Controls.Add(this.m_btnAnnuler);
            this.Controls.Add(this.m_btnOk);
            this.Controls.Add(this.m_txtNomTable);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_lblSource);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CFormEditeProprietesTableFromFramework";
            this.m_exStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.m_exStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Table properties|20110";
            this.Load += new System.EventHandler(this.CFormEditeProprietesTableFromFramework_Load);
            this.m_tabControl.ResumeLayout(false);
            this.m_tabControl.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.m_pageFiltre.ResumeLayout(false);
            this.m_pagePostFiltre.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label m_lblSource;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox m_txtNomTable;
        private sc2i.win32.common.CExtStyle m_exStyle;
        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.Button m_btnAnnuler;
        private System.Windows.Forms.ListView m_wndListeColonnes;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private sc2i.win32.common.C2iTabControl m_tabControl;
        private Crownwood.Magic.Controls.TabPage tabPage2;
        private Crownwood.Magic.Controls.TabPage tabPage1;
        private sc2i.win32.expression.CControlEditListeFormulesNommees m_ctrlFormulesNommees;
        private Crownwood.Magic.Controls.TabPage m_pageOptions;
        private Crownwood.Magic.Controls.TabPage m_pageFiltre;
        private CPanelEditFiltreDynamique m_panelFiltre;
        private System.Windows.Forms.Panel panel1;
        private sc2i.win32.common.CWndLinkStd m_btnRemoveColumn;
        private sc2i.win32.common.CWndLinkStd m_btnEditColumn;
        private sc2i.win32.common.CWndLinkStd m_wndAddColumn;
        private System.Windows.Forms.CheckBox m_chkUseCache;
        private Crownwood.Magic.Controls.TabPage m_pagePostFiltre;
        private futurocom.win32.easyquery.postFilter.CPanelPostFilter m_panelPostFilter;
    }
}