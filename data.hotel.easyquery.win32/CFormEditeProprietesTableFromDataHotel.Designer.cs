namespace futurocom.win32.easyquery
{
    partial class CFormEditeProprietesTableFromDataHotel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditeProprietesTableFromDataHotel));
            this.label1 = new System.Windows.Forms.Label();
            this.m_lblSource = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_txtNomTable = new System.Windows.Forms.TextBox();
            this.m_exStyle = new sc2i.win32.common.CExtStyle();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_wndListeColonnes = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_menuChamp = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_menuSupprimerChamp = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuProprietes = new System.Windows.Forms.ToolStripMenuItem();
            this.m_tabControl = new sc2i.win32.common.C2iTabControl(this.components);
            this.tabPage1 = new Crownwood.Magic.Controls.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAddField = new sc2i.win32.common.CWndLinkStd();
            this.tabPage2 = new Crownwood.Magic.Controls.TabPage();
            this.m_ctrlFormulesNommees = new sc2i.win32.expression.CControlEditListeFormulesNommees();
            this.m_pageOptions = new Crownwood.Magic.Controls.TabPage();
            this.m_chkUseCache = new System.Windows.Forms.CheckBox();
            this.m_panelOptions = new data.hotel.easyquery.win32.CControleOptionsTableFromDataHotel();
            this.m_menuChamp.SuspendLayout();
            this.m_tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.m_pageOptions.SuspendLayout();
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
            this.label1.Text = "Source table|20006";
            // 
            // m_lblSource
            // 
            this.m_lblSource.BackColor = System.Drawing.Color.White;
            this.m_lblSource.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblSource.Location = new System.Drawing.Point(139, 9);
            this.m_lblSource.Name = "m_lblSource";
            this.m_lblSource.Size = new System.Drawing.Size(352, 23);
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
            this.label3.Text = "Table name|20007";
            // 
            // m_txtNomTable
            // 
            this.m_txtNomTable.Location = new System.Drawing.Point(139, 36);
            this.m_txtNomTable.Name = "m_txtNomTable";
            this.m_txtNomTable.Size = new System.Drawing.Size(352, 20);
            this.m_exStyle.SetStyleBackColor(this.m_txtNomTable, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_txtNomTable, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtNomTable.TabIndex = 3;
            // 
            // m_btnOk
            // 
            this.m_btnOk.Location = new System.Drawing.Point(165, 429);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_exStyle.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 4;
            this.m_btnOk.Text = "Ok|1";
            this.m_btnOk.UseVisualStyleBackColor = true;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Location = new System.Drawing.Point(260, 429);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(75, 23);
            this.m_exStyle.SetStyleBackColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAnnuler.TabIndex = 5;
            this.m_btnAnnuler.Text = "Cancel|2";
            this.m_btnAnnuler.UseVisualStyleBackColor = true;
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // m_wndListeColonnes
            // 
            this.m_wndListeColonnes.CheckBoxes = true;
            this.m_wndListeColonnes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.m_wndListeColonnes.ContextMenuStrip = this.m_menuChamp;
            this.m_wndListeColonnes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeColonnes.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_wndListeColonnes.LabelEdit = true;
            this.m_wndListeColonnes.Location = new System.Drawing.Point(0, 25);
            this.m_wndListeColonnes.Name = "m_wndListeColonnes";
            this.m_wndListeColonnes.Size = new System.Drawing.Size(545, 297);
            this.m_exStyle.SetStyleBackColor(this.m_wndListeColonnes, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_wndListeColonnes, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListeColonnes.TabIndex = 6;
            this.m_wndListeColonnes.UseCompatibleStateImageBehavior = false;
            this.m_wndListeColonnes.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name|20012";
            this.columnHeader1.Width = 316;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Source|20013";
            this.columnHeader2.Width = 137;
            // 
            // m_menuChamp
            // 
            this.m_menuChamp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_menuSupprimerChamp,
            this.m_menuProprietes});
            this.m_menuChamp.Name = "m_menuChamp";
            this.m_menuChamp.Size = new System.Drawing.Size(161, 70);
            this.m_exStyle.SetStyleBackColor(this.m_menuChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_menuChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_menuChamp.Opening += new System.ComponentModel.CancelEventHandler(this.m_menuChamp_Opening);
            // 
            // m_menuSupprimerChamp
            // 
            this.m_menuSupprimerChamp.Name = "m_menuSupprimerChamp";
            this.m_menuSupprimerChamp.Size = new System.Drawing.Size(160, 22);
            this.m_menuSupprimerChamp.Text = "Remove|20042";
            this.m_menuSupprimerChamp.Click += new System.EventHandler(this.m_menuSupprimerChamp_Click);
            // 
            // m_menuProprietes
            // 
            this.m_menuProprietes.Name = "m_menuProprietes";
            this.m_menuProprietes.Size = new System.Drawing.Size(160, 22);
            this.m_menuProprietes.Text = "Properties|20043";
            this.m_menuProprietes.Click += new System.EventHandler(this.m_menuProprietes_Click);
            // 
            // m_tabControl
            // 
            this.m_tabControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_tabControl.BoldSelectedPage = true;
            this.m_tabControl.ForeColor = System.Drawing.Color.Black;
            this.m_tabControl.IDEPixelArea = false;
            this.m_tabControl.Location = new System.Drawing.Point(12, 76);
            this.m_tabControl.Name = "m_tabControl";
            this.m_tabControl.Ombre = false;
            this.m_tabControl.PositionTop = true;
            this.m_tabControl.SelectedIndex = 0;
            this.m_tabControl.SelectedTab = this.tabPage1;
            this.m_tabControl.Size = new System.Drawing.Size(545, 347);
            this.m_exStyle.SetStyleBackColor(this.m_tabControl, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_exStyle.SetStyleForeColor(this.m_tabControl, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.m_tabControl.TabIndex = 8;
            this.m_tabControl.TabPages.AddRange(new Crownwood.Magic.Controls.TabPage[] {
            this.tabPage1,
            this.tabPage2,
            this.m_pageOptions});
            this.m_tabControl.TextColor = System.Drawing.Color.Black;
            this.m_tabControl.SelectionChanged += new System.EventHandler(this.m_tabControl_SelectionChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_wndListeColonnes);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(0, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(545, 322);
            this.m_exStyle.SetStyleBackColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage1.TabIndex = 10;
            this.tabPage1.Title = "Columns|20009";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnAddField);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(545, 25);
            this.m_exStyle.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 7;
            // 
            // m_btnAddField
            // 
            this.m_btnAddField.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnAddField.CustomImage = ((System.Drawing.Image)(resources.GetObject("m_btnAddField.CustomImage")));
            this.m_btnAddField.CustomText = "Add";
            this.m_btnAddField.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_btnAddField.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_btnAddField.Location = new System.Drawing.Point(0, 0);
            this.m_btnAddField.Name = "m_btnAddField";
            this.m_btnAddField.ShortMode = false;
            this.m_btnAddField.Size = new System.Drawing.Size(112, 25);
            this.m_exStyle.SetStyleBackColor(this.m_btnAddField, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_btnAddField, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAddField.TabIndex = 0;
            this.m_btnAddField.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_btnAddField.LinkClicked += new System.EventHandler(this.m_btnAddField_LinkClicked);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.m_ctrlFormulesNommees);
            this.tabPage2.Location = new System.Drawing.Point(0, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Selected = false;
            this.tabPage2.Size = new System.Drawing.Size(545, 322);
            this.m_exStyle.SetStyleBackColor(this.tabPage2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.tabPage2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage2.TabIndex = 11;
            this.tabPage2.Title = "Calculated columns|20010";
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
            this.m_ctrlFormulesNommees.Size = new System.Drawing.Size(545, 322);
            this.m_exStyle.SetStyleBackColor(this.m_ctrlFormulesNommees, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_ctrlFormulesNommees, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ctrlFormulesNommees.TabIndex = 0;
            this.m_ctrlFormulesNommees.TypeFormuleNomme = typeof(sc2i.expression.CFormuleNommee);
            // 
            // m_pageOptions
            // 
            this.m_pageOptions.Controls.Add(this.m_panelOptions);
            this.m_pageOptions.Location = new System.Drawing.Point(0, 25);
            this.m_pageOptions.Name = "m_pageOptions";
            this.m_pageOptions.Selected = false;
            this.m_pageOptions.Size = new System.Drawing.Size(545, 322);
            this.m_exStyle.SetStyleBackColor(this.m_pageOptions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_pageOptions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_pageOptions.TabIndex = 12;
            this.m_pageOptions.Title = "Query options|20011";
            // 
            // m_chkUseCache
            // 
            this.m_chkUseCache.AutoSize = true;
            this.m_chkUseCache.Location = new System.Drawing.Point(139, 58);
            this.m_chkUseCache.Name = "m_chkUseCache";
            this.m_chkUseCache.Size = new System.Drawing.Size(134, 17);
            this.m_exStyle.SetStyleBackColor(this.m_chkUseCache, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_chkUseCache, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkUseCache.TabIndex = 9;
            this.m_chkUseCache.Text = "Use data cache|20008";
            this.m_chkUseCache.UseVisualStyleBackColor = true;
            // 
            // m_panelOptions
            // 
            this.m_panelOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelOptions.Location = new System.Drawing.Point(0, 0);
            this.m_panelOptions.Name = "m_panelOptions";
            this.m_panelOptions.Size = new System.Drawing.Size(545, 322);
            this.m_exStyle.SetStyleBackColor(this.m_panelOptions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_panelOptions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelOptions.TabIndex = 0;
            // 
            // CFormEditeProprietesTableFromDataHotel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 464);
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
            this.Name = "CFormEditeProprietesTableFromDataHotel";
            this.m_exStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.m_exStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Table properties|20005";
            this.Load += new System.EventHandler(this.CFormEditeProprietesTableFromDataHotel_Load);
            this.m_menuChamp.ResumeLayout(false);
            this.m_tabControl.ResumeLayout(false);
            this.m_tabControl.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.m_pageOptions.ResumeLayout(false);
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
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private sc2i.win32.common.C2iTabControl m_tabControl;
        private Crownwood.Magic.Controls.TabPage tabPage2;
        private Crownwood.Magic.Controls.TabPage tabPage1;
        private sc2i.win32.expression.CControlEditListeFormulesNommees m_ctrlFormulesNommees;
        private Crownwood.Magic.Controls.TabPage m_pageOptions;
        private System.Windows.Forms.CheckBox m_chkUseCache;
        private data.hotel.easyquery.win32.CControleOptionsTableFromDataHotel m_panelOptions;
        private System.Windows.Forms.Panel panel1;
        private sc2i.win32.common.CWndLinkStd m_btnAddField;
        private System.Windows.Forms.ContextMenuStrip m_menuChamp;
        private System.Windows.Forms.ToolStripMenuItem m_menuSupprimerChamp;
        private System.Windows.Forms.ToolStripMenuItem m_menuProprietes;
    }
}