namespace futurocom.win32.easyquery
{
    partial class CFormEditeProprietesTableManuelle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditeProprietesTableManuelle));
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_txtNomTable = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_exStyle = new sc2i.win32.common.CExtStyle();
            this.m_tabControl = new sc2i.win32.common.C2iTabControl(this.components);
            this.tabPage1 = new Crownwood.Magic.Controls.TabPage();
            this.m_wndListeColumns = new sc2i.win32.common.customizableList.CCustomizableList();
            this.panel4 = new System.Windows.Forms.Panel();
            this.m_lnkRemoveColumn = new sc2i.win32.common.CWndLinkStd();
            this.m_lnkAddColumn = new sc2i.win32.common.CWndLinkStd();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.m_tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel4.SuspendLayout();
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
            this.m_btnAnnuler.Location = new System.Drawing.Point(307, 6);
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
            this.m_btnOk.Location = new System.Drawing.Point(212, 6);
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
            this.splitter1.Location = new System.Drawing.Point(592, 39);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 324);
            this.m_exStyle.SetStyleBackColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_txtNomTable);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(595, 39);
            this.m_exStyle.SetStyleBackColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel2.TabIndex = 4;
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
            this.m_tabControl.Location = new System.Drawing.Point(0, 39);
            this.m_tabControl.Name = "m_tabControl";
            this.m_tabControl.Ombre = true;
            this.m_tabControl.PositionTop = true;
            this.m_tabControl.SelectedIndex = 0;
            this.m_tabControl.SelectedTab = this.tabPage1;
            this.m_tabControl.Size = new System.Drawing.Size(592, 324);
            this.m_exStyle.SetStyleBackColor(this.m_tabControl, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_exStyle.SetStyleForeColor(this.m_tabControl, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.m_tabControl.TabIndex = 5;
            this.m_tabControl.TabPages.AddRange(new Crownwood.Magic.Controls.TabPage[] {
            this.tabPage1});
            this.m_tabControl.TextColor = System.Drawing.Color.Black;
            this.m_tabControl.SelectionChanged += new System.EventHandler(this.m_tabControl_SelectionChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_wndListeColumns);
            this.tabPage1.Controls.Add(this.panel4);
            this.tabPage1.Location = new System.Drawing.Point(0, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(576, 283);
            this.m_exStyle.SetStyleBackColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage1.TabIndex = 13;
            this.tabPage1.Title = "Columns|20070";
            // 
            // m_wndListeColumns
            // 
            this.m_wndListeColumns.AllowDrop = true;
            this.m_wndListeColumns.CurrentItemIndex = null;
            this.m_wndListeColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeColumns.ItemControl = null;
            this.m_wndListeColumns.Items = new sc2i.win32.common.customizableList.CCustomizableListItem[0];
            this.m_wndListeColumns.Location = new System.Drawing.Point(0, 26);
            this.m_wndListeColumns.LockEdition = false;
            this.m_wndListeColumns.Name = "m_wndListeColumns";
            this.m_wndListeColumns.Size = new System.Drawing.Size(576, 257);
            this.m_exStyle.SetStyleBackColor(this.m_wndListeColumns, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_wndListeColumns, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListeColumns.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.m_lnkRemoveColumn);
            this.panel4.Controls.Add(this.m_lnkAddColumn);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(576, 26);
            this.m_exStyle.SetStyleBackColor(this.panel4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.panel4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel4.TabIndex = 1;
            // 
            // m_lnkRemoveColumn
            // 
            this.m_lnkRemoveColumn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkRemoveColumn.CustomImage = ((System.Drawing.Image)(resources.GetObject("m_lnkRemoveColumn.CustomImage")));
            this.m_lnkRemoveColumn.CustomText = "Remove";
            this.m_lnkRemoveColumn.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkRemoveColumn.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkRemoveColumn.Location = new System.Drawing.Point(112, 0);
            this.m_lnkRemoveColumn.Name = "m_lnkRemoveColumn";
            this.m_lnkRemoveColumn.ShortMode = false;
            this.m_lnkRemoveColumn.Size = new System.Drawing.Size(112, 26);
            this.m_exStyle.SetStyleBackColor(this.m_lnkRemoveColumn, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_lnkRemoveColumn, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lnkRemoveColumn.TabIndex = 1;
            this.m_lnkRemoveColumn.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_lnkRemoveColumn.LinkClicked += new System.EventHandler(this.m_lnkRemoveSort_LinkClicked);
            // 
            // m_lnkAddColumn
            // 
            this.m_lnkAddColumn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkAddColumn.CustomImage = ((System.Drawing.Image)(resources.GetObject("m_lnkAddColumn.CustomImage")));
            this.m_lnkAddColumn.CustomText = "Add";
            this.m_lnkAddColumn.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkAddColumn.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkAddColumn.Location = new System.Drawing.Point(0, 0);
            this.m_lnkAddColumn.Name = "m_lnkAddColumn";
            this.m_lnkAddColumn.ShortMode = false;
            this.m_lnkAddColumn.Size = new System.Drawing.Size(112, 26);
            this.m_exStyle.SetStyleBackColor(this.m_lnkAddColumn, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_lnkAddColumn, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lnkAddColumn.TabIndex = 0;
            this.m_lnkAddColumn.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_lnkAddColumn.LinkClicked += new System.EventHandler(this.m_lnkAddColumn_LinkClicked);
            // 
            // CFormEditeProprietesTableManuelle
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
            this.Name = "CFormEditeProprietesTableManuelle";
            this.m_exStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.m_exStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Manual table|20069";
            this.Load += new System.EventHandler(this.CFormEditeProprietesTableManuelle_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.m_tabControl.ResumeLayout(false);
            this.m_tabControl.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
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
        private Crownwood.Magic.Controls.TabPage tabPage1;
        private sc2i.win32.common.customizableList.CCustomizableList m_wndListeColumns;
        private System.Windows.Forms.Panel panel4;
        private sc2i.win32.common.CWndLinkStd m_lnkRemoveColumn;
        private sc2i.win32.common.CWndLinkStd m_lnkAddColumn;
    }
}