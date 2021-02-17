namespace futurocom.win32.easyquery
{
    partial class CFormEditeProprietesSort
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditeProprietesSort));
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_chkUseCache = new System.Windows.Forms.CheckBox();
            this.m_txtNomTable = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_exStyle = new sc2i.win32.common.CExtStyle();
            this.m_tabControl = new sc2i.win32.common.C2iTabControl(this.components);
            this.tabPage1 = new Crownwood.Magic.Controls.TabPage();
            this.m_wndListeSort = new sc2i.win32.common.customizableList.CCustomizableList();
            this.panel4 = new System.Windows.Forms.Panel();
            this.m_lnkRemoveSort = new sc2i.win32.common.CWndLinkStd();
            this.m_lnkAddSort = new sc2i.win32.common.CWndLinkStd();
            this.panel5 = new System.Windows.Forms.Panel();
            this.m_btnUpSort = new System.Windows.Forms.Button();
            this.m_btnDownSort = new System.Windows.Forms.Button();
            this.tabPage3 = new Crownwood.Magic.Controls.TabPage();
            this.m_wndListeColonnesFromSource = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel3 = new System.Windows.Forms.Panel();
            this.m_btnUp = new System.Windows.Forms.Button();
            this.m_btnDown = new System.Windows.Forms.Button();
            this.tabPage2 = new Crownwood.Magic.Controls.TabPage();
            this.m_ctrlFormulesNommees = new sc2i.win32.expression.CControlEditListeFormulesNommees();
            this.m_panelFormules = new sc2i.win32.common.C2iPanel(this.components);
            this.c2iPanel1 = new sc2i.win32.common.C2iPanel(this.components);
            this.tabPage4 = new Crownwood.Magic.Controls.TabPage();
            this.m_panelPostFilter = new futurocom.win32.easyquery.postFilter.CPanelPostFilter();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.m_tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.m_ctrlFormulesNommees.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 363);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(595, 35);
            this.m_exStyle.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 2;
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Location = new System.Drawing.Point(252, 6);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(75, 23);
            this.m_exStyle.SetStyleBackColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAnnuler.TabIndex = 7;
            this.m_btnAnnuler.Text = "Cancel|2";
            this.m_btnAnnuler.UseVisualStyleBackColor = true;
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // m_btnOk
            // 
            this.m_btnOk.Location = new System.Drawing.Point(157, 6);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_exStyle.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 6;
            this.m_btnOk.Text = "Ok|1";
            this.m_btnOk.UseVisualStyleBackColor = true;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(592, 52);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 311);
            this.m_exStyle.SetStyleBackColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_chkUseCache);
            this.panel2.Controls.Add(this.m_txtNomTable);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(595, 52);
            this.m_exStyle.SetStyleBackColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel2.TabIndex = 4;
            // 
            // m_chkUseCache
            // 
            this.m_chkUseCache.AutoSize = true;
            this.m_chkUseCache.Location = new System.Drawing.Point(130, 29);
            this.m_chkUseCache.Name = "m_chkUseCache";
            this.m_chkUseCache.Size = new System.Drawing.Size(134, 17);
            this.m_exStyle.SetStyleBackColor(this.m_chkUseCache, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_chkUseCache, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkUseCache.TabIndex = 7;
            this.m_chkUseCache.Text = "Use data cache|20063";
            this.m_chkUseCache.UseVisualStyleBackColor = true;
            // 
            // m_txtNomTable
            // 
            this.m_txtNomTable.Location = new System.Drawing.Point(130, 5);
            this.m_txtNomTable.Name = "m_txtNomTable";
            this.m_txtNomTable.Size = new System.Drawing.Size(352, 20);
            this.m_exStyle.SetStyleBackColor(this.m_txtNomTable, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_txtNomTable, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtNomTable.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 17);
            this.m_exStyle.SetStyleBackColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label3.TabIndex = 3;
            this.label3.Text = "Table name|20001";
            // 
            // m_tabControl
            // 
            this.m_tabControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_tabControl.BoldSelectedPage = true;
            this.m_tabControl.ControlBottomOffset = 16;
            this.m_tabControl.ControlRightOffset = 16;
            this.m_tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tabControl.ForeColor = System.Drawing.Color.Black;
            this.m_tabControl.IDEPixelArea = false;
            this.m_tabControl.Location = new System.Drawing.Point(0, 52);
            this.m_tabControl.Name = "m_tabControl";
            this.m_tabControl.Ombre = true;
            this.m_tabControl.PositionTop = true;
            this.m_tabControl.SelectedIndex = 3;
            this.m_tabControl.SelectedTab = this.tabPage4;
            this.m_tabControl.Size = new System.Drawing.Size(592, 311);
            this.m_exStyle.SetStyleBackColor(this.m_tabControl, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_exStyle.SetStyleForeColor(this.m_tabControl, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.m_tabControl.TabIndex = 5;
            this.m_tabControl.TabPages.AddRange(new Crownwood.Magic.Controls.TabPage[] {
            this.tabPage1,
            this.tabPage3,
            this.tabPage2,
            this.tabPage4});
            this.m_tabControl.TextColor = System.Drawing.Color.Black;
            this.m_tabControl.SelectionChanged += new System.EventHandler(this.m_tabControl_SelectionChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_wndListeSort);
            this.tabPage1.Controls.Add(this.panel4);
            this.tabPage1.Controls.Add(this.panel5);
            this.tabPage1.Location = new System.Drawing.Point(0, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Selected = false;
            this.tabPage1.Size = new System.Drawing.Size(576, 270);
            this.m_exStyle.SetStyleBackColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage1.TabIndex = 13;
            this.tabPage1.Title = "Data sort|20058";
            // 
            // m_wndListeSort
            // 
            this.m_wndListeSort.AllowDrop = true;
            this.m_wndListeSort.CurrentItemIndex = null;
            this.m_wndListeSort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeSort.ItemControl = null;
            this.m_wndListeSort.Items = new sc2i.win32.common.customizableList.CCustomizableListItem[0];
            this.m_wndListeSort.Location = new System.Drawing.Point(0, 26);
            this.m_wndListeSort.LockEdition = false;
            this.m_wndListeSort.Name = "m_wndListeSort";
            this.m_wndListeSort.Size = new System.Drawing.Size(576, 215);
            this.m_exStyle.SetStyleBackColor(this.m_wndListeSort, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_wndListeSort, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListeSort.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.m_lnkRemoveSort);
            this.panel4.Controls.Add(this.m_lnkAddSort);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(576, 26);
            this.m_exStyle.SetStyleBackColor(this.panel4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.panel4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel4.TabIndex = 1;
            // 
            // m_lnkRemoveSort
            // 
            this.m_lnkRemoveSort.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkRemoveSort.CustomImage = ((System.Drawing.Image)(resources.GetObject("m_lnkRemoveSort.CustomImage")));
            this.m_lnkRemoveSort.CustomText = "Remove";
            this.m_lnkRemoveSort.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkRemoveSort.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkRemoveSort.Location = new System.Drawing.Point(112, 0);
            this.m_lnkRemoveSort.Name = "m_lnkRemoveSort";
            this.m_lnkRemoveSort.ShortMode = false;
            this.m_lnkRemoveSort.Size = new System.Drawing.Size(112, 26);
            this.m_exStyle.SetStyleBackColor(this.m_lnkRemoveSort, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_lnkRemoveSort, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lnkRemoveSort.TabIndex = 1;
            this.m_lnkRemoveSort.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_lnkRemoveSort.LinkClicked += new System.EventHandler(this.m_lnkRemoveSort_LinkClicked);
            // 
            // m_lnkAddSort
            // 
            this.m_lnkAddSort.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkAddSort.CustomImage = ((System.Drawing.Image)(resources.GetObject("m_lnkAddSort.CustomImage")));
            this.m_lnkAddSort.CustomText = "Add";
            this.m_lnkAddSort.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkAddSort.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkAddSort.Location = new System.Drawing.Point(0, 0);
            this.m_lnkAddSort.Name = "m_lnkAddSort";
            this.m_lnkAddSort.ShortMode = false;
            this.m_lnkAddSort.Size = new System.Drawing.Size(112, 26);
            this.m_exStyle.SetStyleBackColor(this.m_lnkAddSort, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_lnkAddSort, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lnkAddSort.TabIndex = 0;
            this.m_lnkAddSort.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_lnkAddSort.LinkClicked += new System.EventHandler(this.m_lnkAddSort_LinkClicked);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.m_btnUpSort);
            this.panel5.Controls.Add(this.m_btnDownSort);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 241);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(576, 29);
            this.m_exStyle.SetStyleBackColor(this.panel5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.panel5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel5.TabIndex = 10;
            // 
            // m_btnUpSort
            // 
            this.m_btnUpSort.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_btnUpSort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnUpSort.Image = global::futurocom.win32.easyquery.Properties.Resources.up_blue;
            this.m_btnUpSort.Location = new System.Drawing.Point(29, 0);
            this.m_btnUpSort.Name = "m_btnUpSort";
            this.m_btnUpSort.Size = new System.Drawing.Size(29, 29);
            this.m_exStyle.SetStyleBackColor(this.m_btnUpSort, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_btnUpSort, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnUpSort.TabIndex = 1;
            this.m_btnUpSort.UseVisualStyleBackColor = true;
            this.m_btnUpSort.Click += new System.EventHandler(this.m_btnUpSort_Click);
            // 
            // m_btnDownSort
            // 
            this.m_btnDownSort.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_btnDownSort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnDownSort.Image = global::futurocom.win32.easyquery.Properties.Resources.down_blue;
            this.m_btnDownSort.Location = new System.Drawing.Point(0, 0);
            this.m_btnDownSort.Name = "m_btnDownSort";
            this.m_btnDownSort.Size = new System.Drawing.Size(29, 29);
            this.m_exStyle.SetStyleBackColor(this.m_btnDownSort, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_btnDownSort, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnDownSort.TabIndex = 0;
            this.m_btnDownSort.UseVisualStyleBackColor = true;
            this.m_btnDownSort.Click += new System.EventHandler(this.m_btnDownSort_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.m_wndListeColonnesFromSource);
            this.tabPage3.Controls.Add(this.panel3);
            this.tabPage3.Location = new System.Drawing.Point(0, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Selected = false;
            this.tabPage3.Size = new System.Drawing.Size(576, 270);
            this.m_exStyle.SetStyleBackColor(this.tabPage3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.tabPage3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage3.TabIndex = 12;
            this.tabPage3.Title = "Columns order|20059";
            // 
            // m_wndListeColonnesFromSource
            // 
            this.m_wndListeColonnesFromSource.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.m_wndListeColonnesFromSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeColonnesFromSource.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_wndListeColonnesFromSource.HideSelection = false;
            this.m_wndListeColonnesFromSource.Location = new System.Drawing.Point(0, 0);
            this.m_wndListeColonnesFromSource.MultiSelect = false;
            this.m_wndListeColonnesFromSource.Name = "m_wndListeColonnesFromSource";
            this.m_wndListeColonnesFromSource.Size = new System.Drawing.Size(576, 241);
            this.m_exStyle.SetStyleBackColor(this.m_wndListeColonnesFromSource, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_wndListeColonnesFromSource, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListeColonnesFromSource.TabIndex = 8;
            this.m_wndListeColonnesFromSource.UseCompatibleStateImageBehavior = false;
            this.m_wndListeColonnesFromSource.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name|20007";
            this.columnHeader1.Width = 330;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.m_btnUp);
            this.panel3.Controls.Add(this.m_btnDown);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 241);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(576, 29);
            this.m_exStyle.SetStyleBackColor(this.panel3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.panel3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel3.TabIndex = 9;
            // 
            // m_btnUp
            // 
            this.m_btnUp.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnUp.Image = global::futurocom.win32.easyquery.Properties.Resources.up_blue;
            this.m_btnUp.Location = new System.Drawing.Point(29, 0);
            this.m_btnUp.Name = "m_btnUp";
            this.m_btnUp.Size = new System.Drawing.Size(29, 29);
            this.m_exStyle.SetStyleBackColor(this.m_btnUp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_btnUp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnUp.TabIndex = 1;
            this.m_btnUp.UseVisualStyleBackColor = true;
            this.m_btnUp.Click += new System.EventHandler(this.m_btnUp_Click);
            // 
            // m_btnDown
            // 
            this.m_btnDown.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnDown.Image = global::futurocom.win32.easyquery.Properties.Resources.down_blue;
            this.m_btnDown.Location = new System.Drawing.Point(0, 0);
            this.m_btnDown.Name = "m_btnDown";
            this.m_btnDown.Size = new System.Drawing.Size(29, 29);
            this.m_exStyle.SetStyleBackColor(this.m_btnDown, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_btnDown, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnDown.TabIndex = 0;
            this.m_btnDown.UseVisualStyleBackColor = true;
            this.m_btnDown.Click += new System.EventHandler(this.m_btnDown_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.m_ctrlFormulesNommees);
            this.tabPage2.Location = new System.Drawing.Point(0, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Selected = false;
            this.tabPage2.Size = new System.Drawing.Size(576, 270);
            this.m_exStyle.SetStyleBackColor(this.tabPage2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.tabPage2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage2.TabIndex = 14;
            this.tabPage2.Title = "Calculated columns|20022";
            // 
            // m_ctrlFormulesNommees
            // 
            this.m_ctrlFormulesNommees.AutoScroll = true;
            this.m_ctrlFormulesNommees.Controls.Add(this.m_panelFormules);
            this.m_ctrlFormulesNommees.Controls.Add(this.c2iPanel1);
            this.m_ctrlFormulesNommees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_ctrlFormulesNommees.HasDeleteButton = true;
            this.m_ctrlFormulesNommees.HasHadButton = true;
            this.m_ctrlFormulesNommees.HeaderTextForFormula = "";
            this.m_ctrlFormulesNommees.HeaderTextForName = "";
            this.m_ctrlFormulesNommees.HideNomFormule = false;
            this.m_ctrlFormulesNommees.Location = new System.Drawing.Point(0, 0);
            this.m_ctrlFormulesNommees.LockEdition = false;
            this.m_ctrlFormulesNommees.Name = "m_ctrlFormulesNommees";
            this.m_ctrlFormulesNommees.Size = new System.Drawing.Size(576, 270);
            this.m_exStyle.SetStyleBackColor(this.m_ctrlFormulesNommees, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_ctrlFormulesNommees, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ctrlFormulesNommees.TabIndex = 2;
            this.m_ctrlFormulesNommees.TypeFormuleNomme = typeof(sc2i.expression.CFormuleNommee);
            // 
            // m_panelFormules
            // 
            this.m_panelFormules.AutoScroll = true;
            this.m_panelFormules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelFormules.Location = new System.Drawing.Point(0, 0);
            this.m_panelFormules.LockEdition = false;
            this.m_panelFormules.Name = "m_panelFormules";
            this.m_panelFormules.Size = new System.Drawing.Size(576, 270);
            this.m_exStyle.SetStyleBackColor(this.m_panelFormules, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_panelFormules, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelFormules.TabIndex = 2;
            // 
            // c2iPanel1
            // 
            this.c2iPanel1.AutoScroll = true;
            this.c2iPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c2iPanel1.Location = new System.Drawing.Point(0, 0);
            this.c2iPanel1.LockEdition = true;
            this.c2iPanel1.Name = "c2iPanel1";
            this.c2iPanel1.Size = new System.Drawing.Size(576, 270);
            this.m_exStyle.SetStyleBackColor(this.c2iPanel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.c2iPanel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iPanel1.TabIndex = 2;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.m_panelPostFilter);
            this.tabPage4.Location = new System.Drawing.Point(0, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(576, 270);
            this.m_exStyle.SetStyleBackColor(this.tabPage4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.tabPage4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage4.TabIndex = 15;
            this.tabPage4.Title = "Post filter|20074";
            // 
            // m_panelPostFilter
            // 
            this.m_panelPostFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelPostFilter.Location = new System.Drawing.Point(0, 0);
            this.m_panelPostFilter.Name = "m_panelPostFilter";
            this.m_panelPostFilter.Size = new System.Drawing.Size(576, 270);
            this.m_exStyle.SetStyleBackColor(this.m_panelPostFilter, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_panelPostFilter, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelPostFilter.TabIndex = 1;
            // 
            // CFormEditeProprietesSort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 398);
            this.ControlBox = false;
            this.Controls.Add(this.m_tabControl);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CFormEditeProprietesSort";
            this.m_exStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.m_exStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Formula filter|20010";
            this.Load += new System.EventHandler(this.CFormEditeProprietesSort_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.m_tabControl.ResumeLayout(false);
            this.m_tabControl.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.m_ctrlFormulesNommees.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox m_txtNomTable;
        private sc2i.win32.common.CExtStyle m_exStyle;
        private System.Windows.Forms.Button m_btnAnnuler;
        private System.Windows.Forms.Button m_btnOk;
        private sc2i.win32.common.C2iTabControl m_tabControl;
        private Crownwood.Magic.Controls.TabPage tabPage3;
        private System.Windows.Forms.ListView m_wndListeColonnesFromSource;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button m_btnDown;
        private System.Windows.Forms.Button m_btnUp;
        private Crownwood.Magic.Controls.TabPage tabPage1;
        private sc2i.win32.common.customizableList.CCustomizableList m_wndListeSort;
        private System.Windows.Forms.Panel panel4;
        private sc2i.win32.common.CWndLinkStd m_lnkRemoveSort;
        private sc2i.win32.common.CWndLinkStd m_lnkAddSort;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button m_btnUpSort;
        private System.Windows.Forms.Button m_btnDownSort;
        private System.Windows.Forms.CheckBox m_chkUseCache;
        private Crownwood.Magic.Controls.TabPage tabPage2;
        private sc2i.win32.expression.CControlEditListeFormulesNommees m_ctrlFormulesNommees;
        private sc2i.win32.common.C2iPanel m_panelFormules;
        private sc2i.win32.common.C2iPanel c2iPanel1;
        private Crownwood.Magic.Controls.TabPage tabPage4;
        private postFilter.CPanelPostFilter m_panelPostFilter;
    }
}