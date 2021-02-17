namespace sc2i.win32.data.dynamic.import
{
    partial class CFormOptionsImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormOptionsImport));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_txtStartLine = new sc2i.win32.common.C2iTextBoxNumerique();
            this.m_txtNbLines = new sc2i.win32.common.C2iTextBoxNumerique();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.m_chkBestEffort = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_txtTaillePaquets = new sc2i.win32.common.C2iTextBoxNumerique();
            this.label4 = new System.Windows.Forms.Label();
            this.m_txtSaveFile = new System.Windows.Forms.TextBox();
            this.m_btnSave = new System.Windows.Forms.Button();
            this.m_chkCreerVersionDonnees = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 23);
            this.cExtStyle1.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sart at line|20239";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(13, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(178, 23);
            this.cExtStyle1.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 0;
            this.label2.Text = "Number of line to import|20240";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtStartLine
            // 
            this.m_txtStartLine.Arrondi = 0;
            this.m_txtStartLine.DecimalAutorise = false;
            this.m_txtStartLine.DoubleValue = null;
            this.m_txtStartLine.EmptyText = "Start of table";
            this.m_txtStartLine.IntValue = null;
            this.m_txtStartLine.Location = new System.Drawing.Point(197, 12);
            this.m_txtStartLine.LockEdition = false;
            this.m_txtStartLine.Name = "m_txtStartLine";
            this.m_txtStartLine.NullAutorise = true;
            this.m_txtStartLine.SelectAllOnEnter = true;
            this.m_txtStartLine.SeparateurMilliers = "";
            this.m_txtStartLine.Size = new System.Drawing.Size(100, 20);
            this.cExtStyle1.SetStyleBackColor(this.m_txtStartLine, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtStartLine, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtStartLine.TabIndex = 0;
            // 
            // m_txtNbLines
            // 
            this.m_txtNbLines.Arrondi = 0;
            this.m_txtNbLines.DecimalAutorise = false;
            this.m_txtNbLines.DoubleValue = null;
            this.m_txtNbLines.EmptyText = "To the end";
            this.m_txtNbLines.IntValue = null;
            this.m_txtNbLines.Location = new System.Drawing.Point(197, 39);
            this.m_txtNbLines.LockEdition = false;
            this.m_txtNbLines.Name = "m_txtNbLines";
            this.m_txtNbLines.NullAutorise = true;
            this.m_txtNbLines.SelectAllOnEnter = true;
            this.m_txtNbLines.SeparateurMilliers = "";
            this.m_txtNbLines.Size = new System.Drawing.Size(100, 20);
            this.cExtStyle1.SetStyleBackColor(this.m_txtNbLines, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtNbLines, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtNbLines.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 188);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(438, 48);
            this.cExtStyle1.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 40;
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnAnnuler.ForeColor = System.Drawing.Color.White;
            this.m_btnAnnuler.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAnnuler.Image")));
            this.m_btnAnnuler.Location = new System.Drawing.Point(226, 2);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(40, 40);
            this.cExtStyle1.SetStyleBackColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAnnuler.TabIndex = 3;
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnOk.ForeColor = System.Drawing.Color.White;
            this.m_btnOk.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOk.Image")));
            this.m_btnOk.Location = new System.Drawing.Point(172, 2);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(40, 40);
            this.cExtStyle1.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 2;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_chkBestEffort
            // 
            this.m_chkBestEffort.AutoSize = true;
            this.m_chkBestEffort.Location = new System.Drawing.Point(162, 91);
            this.m_chkBestEffort.Name = "m_chkBestEffort";
            this.m_chkBestEffort.Size = new System.Drawing.Size(135, 17);
            this.cExtStyle1.SetStyleBackColor(this.m_chkBestEffort, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_chkBestEffort, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkBestEffort.TabIndex = 30;
            this.m_chkBestEffort.Text = "Best effort mode|20243";
            this.m_chkBestEffort.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(13, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 23);
            this.cExtStyle1.SetStyleBackColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label3.TabIndex = 0;
            this.label3.Text = "Packet size|20244";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtTaillePaquets
            // 
            this.m_txtTaillePaquets.Arrondi = 0;
            this.m_txtTaillePaquets.DecimalAutorise = false;
            this.m_txtTaillePaquets.DoubleValue = null;
            this.m_txtTaillePaquets.EmptyText = "No paquet";
            this.m_txtTaillePaquets.IntValue = null;
            this.m_txtTaillePaquets.Location = new System.Drawing.Point(197, 65);
            this.m_txtTaillePaquets.LockEdition = false;
            this.m_txtTaillePaquets.Name = "m_txtTaillePaquets";
            this.m_txtTaillePaquets.NullAutorise = true;
            this.m_txtTaillePaquets.SelectAllOnEnter = true;
            this.m_txtTaillePaquets.SeparateurMilliers = "";
            this.m_txtTaillePaquets.Size = new System.Drawing.Size(100, 20);
            this.cExtStyle1.SetStyleBackColor(this.m_txtTaillePaquets, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtTaillePaquets, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtTaillePaquets.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(178, 23);
            this.cExtStyle1.SetStyleBackColor(this.label4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label4.TabIndex = 0;
            this.label4.Text = "Save session as|20149";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtSaveFile
            // 
            this.m_txtSaveFile.Location = new System.Drawing.Point(196, 134);
            this.m_txtSaveFile.Multiline = true;
            this.m_txtSaveFile.Name = "m_txtSaveFile";
            this.m_txtSaveFile.Size = new System.Drawing.Size(217, 43);
            this.cExtStyle1.SetStyleBackColor(this.m_txtSaveFile, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtSaveFile, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtSaveFile.TabIndex = 41;
            // 
            // m_btnSave
            // 
            this.m_btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnSave.Image = ((System.Drawing.Image)(resources.GetObject("m_btnSave.Image")));
            this.m_btnSave.Location = new System.Drawing.Point(414, 132);
            this.m_btnSave.Name = "m_btnSave";
            this.m_btnSave.Size = new System.Drawing.Size(24, 23);
            this.cExtStyle1.SetStyleBackColor(this.m_btnSave, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnSave, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnSave.TabIndex = 42;
            this.m_btnSave.Click += new System.EventHandler(this.m_btnSave_Click);
            // 
            // m_chkCreerVersionDonnees
            // 
            this.m_chkCreerVersionDonnees.AutoSize = true;
            this.m_chkCreerVersionDonnees.Location = new System.Drawing.Point(162, 112);
            this.m_chkCreerVersionDonnees.Name = "m_chkCreerVersionDonnees";
            this.m_chkCreerVersionDonnees.Size = new System.Drawing.Size(159, 17);
            this.cExtStyle1.SetStyleBackColor(this.m_chkCreerVersionDonnees, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_chkCreerVersionDonnees, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkCreerVersionDonnees.TabIndex = 30;
            this.m_chkCreerVersionDonnees.Text = "Create a data version|20159";
            this.m_chkCreerVersionDonnees.UseVisualStyleBackColor = true;
            // 
            // CFormOptionsImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 236);
            this.ControlBox = false;
            this.Controls.Add(this.m_btnSave);
            this.Controls.Add(this.m_txtSaveFile);
            this.Controls.Add(this.m_chkCreerVersionDonnees);
            this.Controls.Add(this.m_chkBestEffort);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_txtTaillePaquets);
            this.Controls.Add(this.m_txtNbLines);
            this.Controls.Add(this.m_txtStartLine);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.Black;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CFormOptionsImport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Import options|20242";
            this.Load += new System.EventHandler(this.CFormOptionsImport_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private common.C2iTextBoxNumerique m_txtStartLine;
        private common.C2iTextBoxNumerique m_txtNbLines;
        private common.CExtStyle cExtStyle1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button m_btnAnnuler;
        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.CheckBox m_chkBestEffort;
        private System.Windows.Forms.Label label3;
        private common.C2iTextBoxNumerique m_txtTaillePaquets;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox m_txtSaveFile;
        private System.Windows.Forms.Button m_btnSave;
        private System.Windows.Forms.CheckBox m_chkCreerVersionDonnees;
    }
}