namespace sc2i.win32.data.dynamic.import
{
    partial class CFormEditionSessionImport
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
            this.m_tabControl = new sc2i.win32.common.C2iTabControl(this.components);
            this.tabPage1 = new Crownwood.Magic.Controls.TabPage();
            this.m_gridTableSource = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new Crownwood.Magic.Controls.TabPage();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_wndListeLogForLigne = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_btnMarkLines = new System.Windows.Forms.Button();
            this.m_lblLigneSel = new System.Windows.Forms.Label();
            this.m_gridTableLignesTraitees = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.m_chkAfficheNonImportees = new System.Windows.Forms.RadioButton();
            this.m_chkAfficherImportées = new System.Windows.Forms.RadioButton();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panel4 = new System.Windows.Forms.Panel();
            this.m_gridMarkedRecords = new System.Windows.Forms.DataGridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.m_btnMarkedRecordsToSourceTable = new System.Windows.Forms.Button();
            this.tabPage3 = new Crownwood.Magic.Controls.TabPage();
            this.m_wndListeLog = new System.Windows.Forms.ListView();
            this.m_colIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_colField = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_colCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_colMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel7 = new System.Windows.Forms.Panel();
            this.m_lnkNbLignesSource = new System.Windows.Forms.Label();
            this.m_lblNbLignesTableImport = new System.Windows.Forms.Label();
            this.m_tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTableSource)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTableLignesTraitees)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridMarkedRecords)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_tabControl
            // 
            this.m_tabControl.BoldSelectedPage = true;
            this.m_tabControl.ControlBottomOffset = 16;
            this.m_tabControl.ControlRightOffset = 16;
            this.m_tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tabControl.IDEPixelArea = false;
            this.m_tabControl.Location = new System.Drawing.Point(0, 0);
            this.m_tabControl.Name = "m_tabControl";
            this.m_tabControl.Ombre = true;
            this.m_tabControl.PositionTop = true;
            this.m_tabControl.SelectedIndex = 0;
            this.m_tabControl.SelectedTab = this.tabPage1;
            this.m_tabControl.Size = new System.Drawing.Size(749, 463);
            this.m_tabControl.TabIndex = 0;
            this.m_tabControl.TabPages.AddRange(new Crownwood.Magic.Controls.TabPage[] {
            this.tabPage1,
            this.tabPage2,
            this.tabPage3});
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_gridTableSource);
            this.tabPage1.Controls.Add(this.panel7);
            this.tabPage1.Location = new System.Drawing.Point(0, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(733, 422);
            this.tabPage1.TabIndex = 10;
            this.tabPage1.Title = "Source table|20151";
            // 
            // m_gridTableSource
            // 
            this.m_gridTableSource.AllowUserToAddRows = false;
            this.m_gridTableSource.AllowUserToDeleteRows = false;
            this.m_gridTableSource.AllowUserToOrderColumns = true;
            this.m_gridTableSource.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_gridTableSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridTableSource.Location = new System.Drawing.Point(0, 23);
            this.m_gridTableSource.Name = "m_gridTableSource";
            this.m_gridTableSource.Size = new System.Drawing.Size(733, 399);
            this.m_gridTableSource.TabIndex = 0;
            this.m_gridTableSource.DataSourceChanged += new System.EventHandler(this.m_gridTableSource_DataSourceChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.m_gridTableLignesTraitees);
            this.tabPage2.Controls.Add(this.splitter1);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Controls.Add(this.splitter2);
            this.tabPage2.Controls.Add(this.panel4);
            this.tabPage2.Location = new System.Drawing.Point(0, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(733, 422);
            this.tabPage2.TabIndex = 11;
            this.tabPage2.Title = "Processed lines|20152";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 269);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(506, 3);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_wndListeLogForLigne);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 272);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(506, 150);
            this.panel1.TabIndex = 5;
            // 
            // m_wndListeLogForLigne
            // 
            this.m_wndListeLogForLigne.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.m_wndListeLogForLigne.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeLogForLigne.FullRowSelect = true;
            this.m_wndListeLogForLigne.Location = new System.Drawing.Point(0, 29);
            this.m_wndListeLogForLigne.Name = "m_wndListeLogForLigne";
            this.m_wndListeLogForLigne.ShowItemToolTips = true;
            this.m_wndListeLogForLigne.Size = new System.Drawing.Size(506, 121);
            this.m_wndListeLogForLigne.TabIndex = 3;
            this.m_wndListeLogForLigne.UseCompatibleStateImageBehavior = false;
            this.m_wndListeLogForLigne.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Line n°|20231";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Dest.|20227";
            this.columnHeader2.Width = 158;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Source|20228";
            this.columnHeader3.Width = 148;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Message|20229";
            this.columnHeader4.Width = 715;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_btnMarkLines);
            this.panel2.Controls.Add(this.m_lblLigneSel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(506, 29);
            this.panel2.TabIndex = 4;
            // 
            // m_btnMarkLines
            // 
            this.m_btnMarkLines.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnMarkLines.Location = new System.Drawing.Point(466, 0);
            this.m_btnMarkLines.Name = "m_btnMarkLines";
            this.m_btnMarkLines.Size = new System.Drawing.Size(40, 29);
            this.m_btnMarkLines.TabIndex = 2;
            this.m_btnMarkLines.Text = "==>";
            this.m_btnMarkLines.UseVisualStyleBackColor = true;
            this.m_btnMarkLines.Click += new System.EventHandler(this.m_btnMarkLines_Click);
            // 
            // m_lblLigneSel
            // 
            this.m_lblLigneSel.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lblLigneSel.Location = new System.Drawing.Point(0, 0);
            this.m_lblLigneSel.Name = "m_lblLigneSel";
            this.m_lblLigneSel.Size = new System.Drawing.Size(204, 29);
            this.m_lblLigneSel.TabIndex = 0;
            // 
            // m_gridTableLignesTraitees
            // 
            this.m_gridTableLignesTraitees.AllowUserToAddRows = false;
            this.m_gridTableLignesTraitees.AllowUserToOrderColumns = true;
            this.m_gridTableLignesTraitees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_gridTableLignesTraitees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridTableLignesTraitees.Location = new System.Drawing.Point(0, 24);
            this.m_gridTableLignesTraitees.Name = "m_gridTableLignesTraitees";
            this.m_gridTableLignesTraitees.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.m_gridTableLignesTraitees.Size = new System.Drawing.Size(506, 245);
            this.m_gridTableLignesTraitees.TabIndex = 1;
            this.m_gridTableLignesTraitees.DataSourceChanged += new System.EventHandler(this.m_gridTableLignesTraitees_DataSourceChanged);
            this.m_gridTableLignesTraitees.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.m_gridTableLignesTraitees_CellClick);
            this.m_gridTableLignesTraitees.SelectionChanged += new System.EventHandler(this.m_gridTableNonImporte_SelectionChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.m_lblNbLignesTableImport);
            this.panel3.Controls.Add(this.m_chkAfficheNonImportees);
            this.panel3.Controls.Add(this.m_chkAfficherImportées);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(506, 24);
            this.panel3.TabIndex = 6;
            // 
            // m_chkAfficheNonImportees
            // 
            this.m_chkAfficheNonImportees.AutoSize = true;
            this.m_chkAfficheNonImportees.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_chkAfficheNonImportees.Location = new System.Drawing.Point(134, 0);
            this.m_chkAfficheNonImportees.Name = "m_chkAfficheNonImportees";
            this.m_chkAfficheNonImportees.Size = new System.Drawing.Size(157, 24);
            this.m_chkAfficheNonImportees.TabIndex = 1;
            this.m_chkAfficheNonImportees.TabStop = true;
            this.m_chkAfficheNonImportees.Text = "Not imported lines|20157";
            this.m_chkAfficheNonImportees.UseVisualStyleBackColor = true;
            this.m_chkAfficheNonImportees.CheckedChanged += new System.EventHandler(this.m_chkAfficheNonImportees_CheckedChanged);
            // 
            // m_chkAfficherImportées
            // 
            this.m_chkAfficherImportées.AutoSize = true;
            this.m_chkAfficherImportées.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_chkAfficherImportées.Location = new System.Drawing.Point(0, 0);
            this.m_chkAfficherImportées.Name = "m_chkAfficherImportées";
            this.m_chkAfficherImportées.Size = new System.Drawing.Size(134, 24);
            this.m_chkAfficherImportées.TabIndex = 0;
            this.m_chkAfficherImportées.TabStop = true;
            this.m_chkAfficherImportées.Text = "Imported lines|20156";
            this.m_chkAfficherImportées.UseVisualStyleBackColor = true;
            this.m_chkAfficherImportées.CheckedChanged += new System.EventHandler(this.m_chkAfficherImportées_CheckedChanged);
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(506, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 422);
            this.splitter2.TabIndex = 8;
            this.splitter2.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.m_gridMarkedRecords);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(509, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(224, 422);
            this.panel4.TabIndex = 7;
            // 
            // m_gridMarkedRecords
            // 
            this.m_gridMarkedRecords.AllowUserToAddRows = false;
            this.m_gridMarkedRecords.AllowUserToOrderColumns = true;
            this.m_gridMarkedRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_gridMarkedRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridMarkedRecords.Location = new System.Drawing.Point(0, 24);
            this.m_gridMarkedRecords.Name = "m_gridMarkedRecords";
            this.m_gridMarkedRecords.Size = new System.Drawing.Size(224, 335);
            this.m_gridMarkedRecords.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(224, 24);
            this.panel5.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Marked records|20157";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.m_btnMarkedRecordsToSourceTable);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 359);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(224, 63);
            this.panel6.TabIndex = 3;
            // 
            // m_btnMarkedRecordsToSourceTable
            // 
            this.m_btnMarkedRecordsToSourceTable.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_btnMarkedRecordsToSourceTable.Location = new System.Drawing.Point(0, 0);
            this.m_btnMarkedRecordsToSourceTable.Name = "m_btnMarkedRecordsToSourceTable";
            this.m_btnMarkedRecordsToSourceTable.Size = new System.Drawing.Size(224, 45);
            this.m_btnMarkedRecordsToSourceTable.TabIndex = 0;
            this.m_btnMarkedRecordsToSourceTable.Text = "Replace source table by marked records|20158";
            this.m_btnMarkedRecordsToSourceTable.UseVisualStyleBackColor = true;
            this.m_btnMarkedRecordsToSourceTable.Click += new System.EventHandler(this.m_btnMarkedRecordsToSourceTable_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.m_wndListeLog);
            this.tabPage3.Location = new System.Drawing.Point(0, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Selected = false;
            this.tabPage3.Size = new System.Drawing.Size(733, 422);
            this.tabPage3.TabIndex = 12;
            this.tabPage3.Title = "Import log|20221";
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
            this.m_wndListeLog.Size = new System.Drawing.Size(733, 422);
            this.m_wndListeLog.TabIndex = 2;
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
            // panel7
            // 
            this.panel7.Controls.Add(this.m_lnkNbLignesSource);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(733, 23);
            this.panel7.TabIndex = 1;
            // 
            // m_lnkNbLignesSource
            // 
            this.m_lnkNbLignesSource.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_lnkNbLignesSource.Location = new System.Drawing.Point(614, 0);
            this.m_lnkNbLignesSource.Name = "m_lnkNbLignesSource";
            this.m_lnkNbLignesSource.Size = new System.Drawing.Size(119, 23);
            this.m_lnkNbLignesSource.TabIndex = 0;
            // 
            // m_lblNbLignesTableImport
            // 
            this.m_lblNbLignesTableImport.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_lblNbLignesTableImport.Location = new System.Drawing.Point(387, 0);
            this.m_lblNbLignesTableImport.Name = "m_lblNbLignesTableImport";
            this.m_lblNbLignesTableImport.Size = new System.Drawing.Size(119, 24);
            this.m_lblNbLignesTableImport.TabIndex = 2;
            // 
            // CFormEditionSessionImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 463);
            this.Controls.Add(this.m_tabControl);
            this.Name = "CFormEditionSessionImport";
            this.Text = "Import session|20247";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CFormEditionSessionImport_Load);
            this.m_tabControl.ResumeLayout(false);
            this.m_tabControl.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTableSource)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTableLignesTraitees)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridMarkedRecords)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private common.C2iTabControl m_tabControl;
        private Crownwood.Magic.Controls.TabPage tabPage1;
        private Crownwood.Magic.Controls.TabPage tabPage2;
        private System.Windows.Forms.DataGridView m_gridTableLignesTraitees;
        private System.Windows.Forms.DataGridView m_gridTableSource;
        private Crownwood.Magic.Controls.TabPage tabPage3;
        private System.Windows.Forms.ListView m_wndListeLog;
        private System.Windows.Forms.ColumnHeader m_colIndex;
        private System.Windows.Forms.ColumnHeader m_colField;
        private System.Windows.Forms.ColumnHeader m_colCol;
        private System.Windows.Forms.ColumnHeader m_colMessage;
        private System.Windows.Forms.ListView m_wndListeLogForLigne;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label m_lblLigneSel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton m_chkAfficherImportées;
        private System.Windows.Forms.RadioButton m_chkAfficheNonImportees;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button m_btnMarkLines;
        private System.Windows.Forms.DataGridView m_gridMarkedRecords;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button m_btnMarkedRecordsToSourceTable;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label m_lnkNbLignesSource;
        private System.Windows.Forms.Label m_lblNbLignesTableImport;
    }
}