namespace sc2i.win32.data.dynamic.import
{
    partial class CFormResultatSmartImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormResultatSmartImport));
            this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.m_tab = new sc2i.win32.common.C2iTabControl(this.components);
            this.tabPage1 = new Crownwood.Magic.Controls.TabPage();
            this.m_wndViewItem = new sc2i.win32.data.dynamic.import.CControleViewSmartImportResult();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_wndListeItems = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_imagesItems = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_lblTypeEntite = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel3 = new System.Windows.Forms.Panel();
            this.m_wndListeResumée = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage3 = new Crownwood.Magic.Controls.TabPage();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.m_wndListeLog = new System.Windows.Forms.ListView();
            this.m_colIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_colField = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_colCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_colMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_imagesLog = new System.Windows.Forms.ImageList(this.components);
            this.m_ctrlSetup = new sc2i.win32.data.dynamic.import.CControleSetupSmartImport();
            this.tabPage2 = new Crownwood.Magic.Controls.TabPage();
            this.m_wndListeComparaison = new System.Windows.Forms.ListView();
            this.m_colNumLigneCompare = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_colColumnCompare = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_colSourceValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_colDestValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel4 = new System.Windows.Forms.Panel();
            this.m_btnValider = new System.Windows.Forms.Button();
            this.m_tab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_tab
            // 
            this.m_tab.BoldSelectedPage = true;
            this.m_tab.ControlBottomOffset = 16;
            this.m_tab.ControlRightOffset = 16;
            this.m_tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tab.IDEPixelArea = false;
            this.m_tab.Location = new System.Drawing.Point(0, 0);
            this.m_tab.Name = "m_tab";
            this.m_tab.Ombre = true;
            this.m_tab.PositionTop = true;
            this.m_tab.SelectedIndex = 1;
            this.m_tab.SelectedTab = this.tabPage3;
            this.m_tab.Size = new System.Drawing.Size(806, 396);
            this.cExtStyle1.SetStyleBackColor(this.m_tab, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_tab, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_tab.TabIndex = 0;
            this.m_tab.TabPages.AddRange(new Crownwood.Magic.Controls.TabPage[] {
            this.tabPage1,
            this.tabPage3,
            this.tabPage2});
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_wndViewItem);
            this.tabPage1.Controls.Add(this.splitter2);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.splitter1);
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Location = new System.Drawing.Point(0, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Selected = false;
            this.tabPage1.Size = new System.Drawing.Size(790, 355);
            this.cExtStyle1.SetStyleBackColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage1.TabIndex = 10;
            this.tabPage1.Title = "Import summary|20220";
            // 
            // m_wndViewItem
            // 
            this.m_wndViewItem.CurrentItemIndex = null;
            this.m_wndViewItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndViewItem.Items = new sc2i.win32.common.customizableList.CCustomizableListItem[0];
            this.m_wndViewItem.Location = new System.Drawing.Point(504, 0);
            this.m_wndViewItem.LockEdition = false;
            this.m_wndViewItem.Name = "m_wndViewItem";
            this.m_wndViewItem.Size = new System.Drawing.Size(286, 355);
            this.cExtStyle1.SetStyleBackColor(this.m_wndViewItem, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_wndViewItem, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndViewItem.TabIndex = 5;
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(501, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 355);
            this.cExtStyle1.SetStyleBackColor(this.splitter2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.splitter2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.splitter2.TabIndex = 6;
            this.splitter2.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_wndListeItems);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(230, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(271, 355);
            this.cExtStyle1.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 3;
            // 
            // m_wndListeItems
            // 
            this.m_wndListeItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.m_wndListeItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeItems.Location = new System.Drawing.Point(0, 28);
            this.m_wndListeItems.MultiSelect = false;
            this.m_wndListeItems.Name = "m_wndListeItems";
            this.m_wndListeItems.Size = new System.Drawing.Size(271, 327);
            this.m_wndListeItems.SmallImageList = this.m_imagesItems;
            this.cExtStyle1.SetStyleBackColor(this.m_wndListeItems, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_wndListeItems, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListeItems.TabIndex = 2;
            this.m_wndListeItems.UseCompatibleStateImageBehavior = false;
            this.m_wndListeItems.View = System.Windows.Forms.View.Details;
            this.m_wndListeItems.SelectedIndexChanged += new System.EventHandler(this.m_wndListeItems_SelectedIndexChanged);
            this.m_wndListeItems.SizeChanged += new System.EventHandler(this.m_wndListeItems_SizeChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Entities|20222";
            this.columnHeader1.Width = 251;
            // 
            // m_imagesItems
            // 
            this.m_imagesItems.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imagesItems.ImageStream")));
            this.m_imagesItems.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imagesItems.Images.SetKeyName(0, "1402941834_Create.png");
            this.m_imagesItems.Images.SetKeyName(1, "1402941874_Modify.png");
            this.m_imagesItems.Images.SetKeyName(2, "1402941905_checkbox_no.png");
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_lblTypeEntite);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(271, 28);
            this.cExtStyle1.SetStyleBackColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel2.TabIndex = 3;
            // 
            // m_lblTypeEntite
            // 
            this.m_lblTypeEntite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblTypeEntite.Location = new System.Drawing.Point(0, 0);
            this.m_lblTypeEntite.Name = "m_lblTypeEntite";
            this.m_lblTypeEntite.Size = new System.Drawing.Size(271, 28);
            this.cExtStyle1.SetStyleBackColor(this.m_lblTypeEntite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_lblTypeEntite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lblTypeEntite.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(227, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 355);
            this.cExtStyle1.SetStyleBackColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.m_wndListeResumée);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(227, 355);
            this.cExtStyle1.SetStyleBackColor(this.panel3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel3.TabIndex = 4;
            // 
            // m_wndListeResumée
            // 
            this.m_wndListeResumée.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.m_wndListeResumée.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeResumée.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_wndListeResumée.Location = new System.Drawing.Point(0, 28);
            this.m_wndListeResumée.MultiSelect = false;
            this.m_wndListeResumée.Name = "m_wndListeResumée";
            this.m_wndListeResumée.Size = new System.Drawing.Size(227, 327);
            this.cExtStyle1.SetStyleBackColor(this.m_wndListeResumée, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_wndListeResumée, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListeResumée.TabIndex = 0;
            this.m_wndListeResumée.UseCompatibleStateImageBehavior = false;
            this.m_wndListeResumée.View = System.Windows.Forms.View.Details;
            this.m_wndListeResumée.SelectedIndexChanged += new System.EventHandler(this.m_wndListeResumée_SelectedIndexChanged);
            this.m_wndListeResumée.SizeChanged += new System.EventHandler(this.m_wndListeResumée_SizeChanged);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(227, 28);
            this.cExtStyle1.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 1;
            this.label1.Text = "item (total / modifed / added)|20223";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.splitter3);
            this.tabPage3.Controls.Add(this.m_wndListeLog);
            this.tabPage3.Controls.Add(this.m_ctrlSetup);
            this.tabPage3.Location = new System.Drawing.Point(0, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(790, 355);
            this.cExtStyle1.SetStyleBackColor(this.tabPage3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.tabPage3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage3.TabIndex = 12;
            this.tabPage3.Title = "Import log|20221";
            // 
            // splitter3
            // 
            this.splitter3.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter3.Location = new System.Drawing.Point(533, 0);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(3, 355);
            this.cExtStyle1.SetStyleBackColor(this.splitter3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.splitter3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.splitter3.TabIndex = 3;
            this.splitter3.TabStop = false;
            // 
            // m_wndListeLog
            // 
            this.m_wndListeLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_colIndex,
            this.m_colField,
            this.m_colCol,
            this.m_colMessage});
            this.m_wndListeLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeLog.FullRowSelect = true;
            this.m_wndListeLog.Location = new System.Drawing.Point(0, 0);
            this.m_wndListeLog.Name = "m_wndListeLog";
            this.m_wndListeLog.Size = new System.Drawing.Size(536, 355);
            this.m_wndListeLog.SmallImageList = this.m_imagesLog;
            this.cExtStyle1.SetStyleBackColor(this.m_wndListeLog, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_wndListeLog, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListeLog.TabIndex = 0;
            this.m_wndListeLog.UseCompatibleStateImageBehavior = false;
            this.m_wndListeLog.View = System.Windows.Forms.View.Details;
            // 
            // m_colIndex
            // 
            this.m_colIndex.Text = "Line n°|20231";
            // 
            // m_colField
            // 
            this.m_colField.Text = "Dest.|20227";
            this.m_colField.Width = 158;
            // 
            // m_colCol
            // 
            this.m_colCol.Text = "Source|20228";
            this.m_colCol.Width = 148;
            // 
            // m_colMessage
            // 
            this.m_colMessage.Text = "Message|20229";
            this.m_colMessage.Width = 715;
            // 
            // m_imagesLog
            // 
            this.m_imagesLog.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imagesLog.ImageStream")));
            this.m_imagesLog.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imagesLog.Images.SetKeyName(0, "1403617439_Info.png");
            this.m_imagesLog.Images.SetKeyName(1, "1403617511_Warning.png");
            this.m_imagesLog.Images.SetKeyName(2, "1403617473_Error.png");
            // 
            // m_ctrlSetup
            // 
            this.m_ctrlSetup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.m_ctrlSetup.CurrentItemIndex = null;
            this.m_ctrlSetup.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_ctrlSetup.HideNullSources = true;
            this.m_ctrlSetup.Items = new sc2i.win32.common.customizableList.CCustomizableListItem[0];
            this.m_ctrlSetup.Location = new System.Drawing.Point(536, 0);
            this.m_ctrlSetup.LockEdition = true;
            this.m_ctrlSetup.Name = "m_ctrlSetup";
            this.m_ctrlSetup.Size = new System.Drawing.Size(254, 355);
            this.cExtStyle1.SetStyleBackColor(this.m_ctrlSetup, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_ctrlSetup, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ctrlSetup.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.m_wndListeComparaison);
            this.tabPage2.Location = new System.Drawing.Point(0, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Selected = false;
            this.tabPage2.Size = new System.Drawing.Size(790, 355);
            this.cExtStyle1.SetStyleBackColor(this.tabPage2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.tabPage2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage2.TabIndex = 13;
            this.tabPage2.Title = "Comparison|20234";
            // 
            // m_wndListeComparaison
            // 
            this.m_wndListeComparaison.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_colNumLigneCompare,
            this.m_colColumnCompare,
            this.m_colSourceValue,
            this.m_colDestValue});
            this.m_wndListeComparaison.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeComparaison.Location = new System.Drawing.Point(0, 0);
            this.m_wndListeComparaison.Name = "m_wndListeComparaison";
            this.m_wndListeComparaison.Size = new System.Drawing.Size(790, 355);
            this.cExtStyle1.SetStyleBackColor(this.m_wndListeComparaison, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_wndListeComparaison, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListeComparaison.TabIndex = 0;
            this.m_wndListeComparaison.UseCompatibleStateImageBehavior = false;
            this.m_wndListeComparaison.View = System.Windows.Forms.View.Details;
            this.m_wndListeComparaison.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_wndListeComparaison_MouseUp);
            // 
            // m_colNumLigneCompare
            // 
            this.m_colNumLigneCompare.Text = "Line n°|20231";
            // 
            // m_colColumnCompare
            // 
            this.m_colColumnCompare.Text = "Column|20232";
            this.m_colColumnCompare.Width = 149;
            // 
            // m_colSourceValue
            // 
            this.m_colSourceValue.Text = "Source value|20233";
            this.m_colSourceValue.Width = 252;
            // 
            // m_colDestValue
            // 
            this.m_colDestValue.Text = "Import value|20235";
            this.m_colDestValue.Width = 296;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.m_btnValider);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 396);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(806, 35);
            this.cExtStyle1.SetStyleBackColor(this.panel4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel4.TabIndex = 4;
            // 
            // m_btnValider
            // 
            this.m_btnValider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_btnValider.Location = new System.Drawing.Point(12, 3);
            this.m_btnValider.Name = "m_btnValider";
            this.m_btnValider.Size = new System.Drawing.Size(154, 29);
            this.cExtStyle1.SetStyleBackColor(this.m_btnValider, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnValider, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnValider.TabIndex = 0;
            this.m_btnValider.Text = "Close window|20224";
            this.m_btnValider.UseVisualStyleBackColor = true;
            this.m_btnValider.Click += new System.EventHandler(this.m_btnValider_Click);
            // 
            // CFormResultatSmartImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(806, 431);
            this.Controls.Add(this.m_tab);
            this.Controls.Add(this.panel4);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CFormResultatSmartImport";
            this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Smart import preview|20219";
            this.m_tab.ResumeLayout(false);
            this.m_tab.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private common.CExtStyle cExtStyle1;
        private common.C2iTabControl m_tab;
        private Crownwood.Magic.Controls.TabPage tabPage3;
        private Crownwood.Magic.Controls.TabPage tabPage1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView m_wndListeItems;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Label m_lblTypeEntite;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListView m_wndListeResumée;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList m_imagesItems;
        private CControleViewSmartImportResult m_wndViewItem;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button m_btnValider;
        private System.Windows.Forms.ListView m_wndListeLog;
        private System.Windows.Forms.ColumnHeader m_colField;
        private System.Windows.Forms.ColumnHeader m_colCol;
        private System.Windows.Forms.ColumnHeader m_colMessage;
        private System.Windows.Forms.ImageList m_imagesLog;
        private System.Windows.Forms.ColumnHeader m_colIndex;
        private Crownwood.Magic.Controls.TabPage tabPage2;
        private System.Windows.Forms.ListView m_wndListeComparaison;
        private System.Windows.Forms.ColumnHeader m_colNumLigneCompare;
        private System.Windows.Forms.ColumnHeader m_colColumnCompare;
        private System.Windows.Forms.ColumnHeader m_colSourceValue;
        private System.Windows.Forms.ColumnHeader m_colDestValue;
        private System.Windows.Forms.Splitter splitter3;
        private CControleSetupSmartImport m_ctrlSetup;
    }
}