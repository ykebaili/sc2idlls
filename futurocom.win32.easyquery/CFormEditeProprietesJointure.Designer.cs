namespace futurocom.win32.easyquery
{
    partial class CFormEditeProprietesJointure
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_txtNomTable = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_exStyle = new sc2i.win32.common.CExtStyle();
            this.m_panelJointure = new System.Windows.Forms.Panel();
            this.m_lnkAddParametre = new sc2i.win32.common.CWndLinkStd();
            this.m_cmbTypeJointure = new System.Windows.Forms.ComboBox();
            this.c2iTabControl1 = new sc2i.win32.common.C2iTabControl(this.components);
            this.tabPage2 = new Crownwood.Magic.Controls.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_wndListeColonnes1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.m_lblTable1 = new System.Windows.Forms.Label();
            this.m_wndListeColonnes2 = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.m_lblTable2 = new System.Windows.Forms.Label();
            this.tabPage1 = new Crownwood.Magic.Controls.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.m_chkUseCache = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.c2iTabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 291);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(566, 35);
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
            this.splitter1.Location = new System.Drawing.Point(563, 50);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 241);
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
            this.panel2.Size = new System.Drawing.Size(566, 50);
            this.m_exStyle.SetStyleBackColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel2.TabIndex = 4;
            // 
            // m_txtNomTable
            // 
            this.m_txtNomTable.Location = new System.Drawing.Point(130, 5);
            this.m_txtNomTable.Name = "m_txtNomTable";
            this.m_txtNomTable.Size = new System.Drawing.Size(433, 20);
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
            // m_panelJointure
            // 
            this.m_panelJointure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelJointure.Location = new System.Drawing.Point(0, 47);
            this.m_panelJointure.Name = "m_panelJointure";
            this.m_panelJointure.Size = new System.Drawing.Size(563, 169);
            this.m_exStyle.SetStyleBackColor(this.m_panelJointure, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_panelJointure, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelJointure.TabIndex = 5;
            // 
            // m_lnkAddParametre
            // 
            this.m_lnkAddParametre.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkAddParametre.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkAddParametre.Location = new System.Drawing.Point(3, 22);
            this.m_lnkAddParametre.Name = "m_lnkAddParametre";
            this.m_lnkAddParametre.ShortMode = false;
            this.m_lnkAddParametre.Size = new System.Drawing.Size(112, 22);
            this.m_exStyle.SetStyleBackColor(this.m_lnkAddParametre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_lnkAddParametre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lnkAddParametre.TabIndex = 5;
            this.m_lnkAddParametre.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_lnkAddParametre.LinkClicked += new System.EventHandler(this.m_lnkAddParametre_LinkClicked);
            // 
            // m_cmbTypeJointure
            // 
            this.m_cmbTypeJointure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbTypeJointure.FormattingEnabled = true;
            this.m_cmbTypeJointure.Location = new System.Drawing.Point(100, 3);
            this.m_cmbTypeJointure.Name = "m_cmbTypeJointure";
            this.m_cmbTypeJointure.Size = new System.Drawing.Size(315, 21);
            this.m_exStyle.SetStyleBackColor(this.m_cmbTypeJointure, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_cmbTypeJointure, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_cmbTypeJointure.TabIndex = 7;
            // 
            // c2iTabControl1
            // 
            this.c2iTabControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iTabControl1.BoldSelectedPage = true;
            this.c2iTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c2iTabControl1.ForeColor = System.Drawing.Color.Black;
            this.c2iTabControl1.IDEPixelArea = false;
            this.c2iTabControl1.Location = new System.Drawing.Point(0, 50);
            this.c2iTabControl1.Name = "c2iTabControl1";
            this.c2iTabControl1.Ombre = false;
            this.c2iTabControl1.PositionTop = true;
            this.c2iTabControl1.SelectedIndex = 0;
            this.c2iTabControl1.SelectedTab = this.tabPage1;
            this.c2iTabControl1.Size = new System.Drawing.Size(563, 241);
            this.m_exStyle.SetStyleBackColor(this.c2iTabControl1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_exStyle.SetStyleForeColor(this.c2iTabControl1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.c2iTabControl1.TabIndex = 0;
            this.c2iTabControl1.TabPages.AddRange(new Crownwood.Magic.Controls.TabPage[] {
            this.tabPage1,
            this.tabPage2});
            this.c2iTabControl1.TextColor = System.Drawing.Color.Black;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(0, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Selected = false;
            this.tabPage2.Size = new System.Drawing.Size(563, 216);
            this.m_exStyle.SetStyleBackColor(this.tabPage2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.tabPage2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage2.TabIndex = 11;
            this.tabPage2.Title = "Columns|20005";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.m_wndListeColonnes1);
            this.splitContainer1.Panel1.Controls.Add(this.m_lblTable1);
            this.m_exStyle.SetStyleBackColor(this.splitContainer1.Panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.splitContainer1.Panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.m_wndListeColonnes2);
            this.splitContainer1.Panel2.Controls.Add(this.m_lblTable2);
            this.m_exStyle.SetStyleBackColor(this.splitContainer1.Panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.splitContainer1.Panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.splitContainer1.Size = new System.Drawing.Size(563, 216);
            this.splitContainer1.SplitterDistance = 278;
            this.m_exStyle.SetStyleBackColor(this.splitContainer1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.splitContainer1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.splitContainer1.TabIndex = 0;
            // 
            // m_wndListeColonnes1
            // 
            this.m_wndListeColonnes1.CheckBoxes = true;
            this.m_wndListeColonnes1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.m_wndListeColonnes1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeColonnes1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_wndListeColonnes1.LabelEdit = true;
            this.m_wndListeColonnes1.Location = new System.Drawing.Point(0, 21);
            this.m_wndListeColonnes1.Name = "m_wndListeColonnes1";
            this.m_wndListeColonnes1.Size = new System.Drawing.Size(278, 195);
            this.m_exStyle.SetStyleBackColor(this.m_wndListeColonnes1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_wndListeColonnes1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListeColonnes1.TabIndex = 7;
            this.m_wndListeColonnes1.UseCompatibleStateImageBehavior = false;
            this.m_wndListeColonnes1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name|20007";
            this.columnHeader1.Width = 153;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Source|20008";
            this.columnHeader2.Width = 98;
            // 
            // m_lblTable1
            // 
            this.m_lblTable1.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_lblTable1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblTable1.Location = new System.Drawing.Point(0, 0);
            this.m_lblTable1.Name = "m_lblTable1";
            this.m_lblTable1.Size = new System.Drawing.Size(278, 21);
            this.m_exStyle.SetStyleBackColor(this.m_lblTable1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_lblTable1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lblTable1.TabIndex = 0;
            // 
            // m_wndListeColonnes2
            // 
            this.m_wndListeColonnes2.CheckBoxes = true;
            this.m_wndListeColonnes2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.m_wndListeColonnes2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeColonnes2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_wndListeColonnes2.LabelEdit = true;
            this.m_wndListeColonnes2.Location = new System.Drawing.Point(0, 21);
            this.m_wndListeColonnes2.Name = "m_wndListeColonnes2";
            this.m_wndListeColonnes2.Size = new System.Drawing.Size(281, 195);
            this.m_exStyle.SetStyleBackColor(this.m_wndListeColonnes2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_wndListeColonnes2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListeColonnes2.TabIndex = 8;
            this.m_wndListeColonnes2.UseCompatibleStateImageBehavior = false;
            this.m_wndListeColonnes2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Name|20007";
            this.columnHeader3.Width = 153;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Source|20008";
            this.columnHeader4.Width = 98;
            // 
            // m_lblTable2
            // 
            this.m_lblTable2.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_lblTable2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblTable2.Location = new System.Drawing.Point(0, 0);
            this.m_lblTable2.Name = "m_lblTable2";
            this.m_lblTable2.Size = new System.Drawing.Size(281, 21);
            this.m_exStyle.SetStyleBackColor(this.m_lblTable2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_lblTable2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lblTable2.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_panelJointure);
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Location = new System.Drawing.Point(0, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(563, 216);
            this.m_exStyle.SetStyleBackColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage1.TabIndex = 10;
            this.tabPage1.Title = "Join parameters|20015";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.m_cmbTypeJointure);
            this.panel3.Controls.Add(this.m_lnkAddParametre);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(563, 47);
            this.m_exStyle.SetStyleBackColor(this.panel3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.panel3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel3.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(4, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 16);
            this.m_exStyle.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 8;
            this.label2.Text = " Mode|20019";
            // 
            // m_chkUseCache
            // 
            this.m_chkUseCache.AutoSize = true;
            this.m_chkUseCache.Location = new System.Drawing.Point(130, 30);
            this.m_chkUseCache.Name = "m_chkUseCache";
            this.m_chkUseCache.Size = new System.Drawing.Size(134, 17);
            this.m_exStyle.SetStyleBackColor(this.m_chkUseCache, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_chkUseCache, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkUseCache.TabIndex = 7;
            this.m_chkUseCache.Text = "Use data cache|20063";
            this.m_chkUseCache.UseVisualStyleBackColor = true;
            // 
            // CFormEditeProprietesJointure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 326);
            this.ControlBox = false;
            this.Controls.Add(this.c2iTabControl1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CFormEditeProprietesJointure";
            this.m_exStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.m_exStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Join|20014";
            this.Load += new System.EventHandler(this.CFormEditeProprietesJointure_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.c2iTabControl1.ResumeLayout(false);
            this.c2iTabControl1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
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
        private System.Windows.Forms.Panel m_panelJointure;
        private sc2i.win32.common.CWndLinkStd m_lnkAddParametre;
        private System.Windows.Forms.ComboBox m_cmbTypeJointure;
        private sc2i.win32.common.C2iTabControl c2iTabControl1;
        private Crownwood.Magic.Controls.TabPage tabPage1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private Crownwood.Magic.Controls.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label m_lblTable1;
        private System.Windows.Forms.Label m_lblTable2;
        private System.Windows.Forms.ListView m_wndListeColonnes1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView m_wndListeColonnes2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.CheckBox m_chkUseCache;
    }
}