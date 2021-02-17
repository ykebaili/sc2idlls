namespace sc2i.win32.data.dynamic.EasyQuery
{
    partial class CFormEditJeuDeDonneesEasyQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditJeuDeDonneesEasyQuery));
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_extStyle = new sc2i.win32.common.CExtStyle();
            this.m_panelHeader = new sc2i.win32.common.C2iPanelOmbre();
            this.m_txtLibelleElement = new System.Windows.Forms.TextBox();
            this.m_txtPrefix = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_panelQuery = new futurocom.win32.easyquery.CEditeurEasyQuery();
            this.c2iPanelOmbre2 = new sc2i.win32.common.C2iPanelOmbre();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel3 = new System.Windows.Forms.Panel();
            this.m_wndListeTablesAExporter = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.m_panelHeader.SuspendLayout();
            this.c2iPanelOmbre2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 383);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(729, 48);
            this.m_extStyle.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 4051;
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnAnnuler.ForeColor = System.Drawing.Color.White;
            this.m_btnAnnuler.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAnnuler.Image")));
            this.m_btnAnnuler.Location = new System.Drawing.Point(371, 2);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(40, 40);
            this.m_extStyle.SetStyleBackColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAnnuler.TabIndex = 3;
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnOk.ForeColor = System.Drawing.Color.White;
            this.m_btnOk.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOk.Image")));
            this.m_btnOk.Location = new System.Drawing.Point(317, 2);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(40, 40);
            this.m_extStyle.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 2;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_panelHeader
            // 
            this.m_panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_panelHeader.Controls.Add(this.m_txtLibelleElement);
            this.m_panelHeader.Controls.Add(this.m_txtPrefix);
            this.m_panelHeader.Controls.Add(this.label2);
            this.m_panelHeader.Controls.Add(this.label1);
            this.m_panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelHeader.Location = new System.Drawing.Point(0, 0);
            this.m_panelHeader.LockEdition = false;
            this.m_panelHeader.Name = "m_panelHeader";
            this.m_panelHeader.Size = new System.Drawing.Size(729, 48);
            this.m_extStyle.SetStyleBackColor(this.m_panelHeader, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelHeader, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelHeader.TabIndex = 4052;
            // 
            // m_txtLibelleElement
            // 
            this.m_txtLibelleElement.Location = new System.Drawing.Point(112, 8);
            this.m_txtLibelleElement.Name = "m_txtLibelleElement";
            this.m_txtLibelleElement.Size = new System.Drawing.Size(256, 20);
            this.m_extStyle.SetStyleBackColor(this.m_txtLibelleElement, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtLibelleElement, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtLibelleElement.TabIndex = 3;
            // 
            // m_txtPrefix
            // 
            this.m_txtPrefix.Location = new System.Drawing.Point(480, 8);
            this.m_txtPrefix.Name = "m_txtPrefix";
            this.m_txtPrefix.Size = new System.Drawing.Size(96, 20);
            this.m_extStyle.SetStyleBackColor(this.m_txtPrefix, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtPrefix, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtPrefix.TabIndex = 5;
            this.m_txtPrefix.Text = "textBox1";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(374, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 16);
            this.m_extStyle.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 4;
            this.label2.Text = "Table prefix|137";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 16);
            this.m_extStyle.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 0;
            this.label1.Text = "Label|136";
            // 
            // m_panelQuery
            // 
            this.m_panelQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelQuery.Location = new System.Drawing.Point(0, 0);
            this.m_panelQuery.LockEdition = false;
            this.m_panelQuery.Name = "m_panelQuery";
            this.m_panelQuery.Size = new System.Drawing.Size(567, 320);
            this.m_extStyle.SetStyleBackColor(this.m_panelQuery, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelQuery, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelQuery.TabIndex = 4053;
            this.m_panelQuery.AfterRemoveElement += new System.EventHandler(this.m_panelQuery_AfterRemoveElement);
            this.m_panelQuery.AfterAddElements += new System.EventHandler(this.m_panelQuery_AfterAddElements);
            this.m_panelQuery.ElementPropertiesChanged += new System.EventHandler(this.m_panelQuery_ElementPropertiesChanged);
            // 
            // c2iPanelOmbre2
            // 
            this.c2iPanelOmbre2.Controls.Add(this.panel2);
            this.c2iPanelOmbre2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c2iPanelOmbre2.Location = new System.Drawing.Point(0, 48);
            this.c2iPanelOmbre2.LockEdition = false;
            this.c2iPanelOmbre2.Name = "c2iPanelOmbre2";
            this.c2iPanelOmbre2.Size = new System.Drawing.Size(729, 335);
            this.m_extStyle.SetStyleBackColor(this.c2iPanelOmbre2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.c2iPanelOmbre2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iPanelOmbre2.TabIndex = 4054;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.m_panelQuery);
            this.panel2.Controls.Add(this.splitter1);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(717, 320);
            this.m_extStyle.SetStyleBackColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel2.TabIndex = 4054;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(567, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 320);
            this.m_extStyle.SetStyleBackColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.splitter1.TabIndex = 4055;
            this.splitter1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.m_wndListeTablesAExporter);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(570, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(147, 320);
            this.m_extStyle.SetStyleBackColor(this.panel3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.panel3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel3.TabIndex = 4054;
            // 
            // m_wndListeTablesAExporter
            // 
            this.m_wndListeTablesAExporter.CheckBoxes = true;
            this.m_wndListeTablesAExporter.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.m_wndListeTablesAExporter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeTablesAExporter.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_wndListeTablesAExporter.Location = new System.Drawing.Point(0, 23);
            this.m_wndListeTablesAExporter.MultiSelect = false;
            this.m_wndListeTablesAExporter.Name = "m_wndListeTablesAExporter";
            this.m_wndListeTablesAExporter.Size = new System.Drawing.Size(147, 297);
            this.m_extStyle.SetStyleBackColor(this.m_wndListeTablesAExporter, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_wndListeTablesAExporter, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListeTablesAExporter.TabIndex = 1;
            this.m_wndListeTablesAExporter.UseCompatibleStateImageBehavior = false;
            this.m_wndListeTablesAExporter.View = System.Windows.Forms.View.Details;
            this.m_wndListeTablesAExporter.SizeChanged += new System.EventHandler(this.m_wndListeTablesAExporter_SizeChanged);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 23);
            this.m_extStyle.SetStyleBackColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label3.TabIndex = 0;
            this.label3.Text = "Tables to export|20114";
            // 
            // CFormEditJeuDeDonneesEasyQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 431);
            this.Controls.Add(this.c2iPanelOmbre2);
            this.Controls.Add(this.m_panelHeader);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CFormEditJeuDeDonneesEasyQuery";
            this.m_extStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.m_extStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Easy query|20113";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CFormEditJeuDeDonneesEasyQuery_Load);
            this.panel1.ResumeLayout(false);
            this.m_panelHeader.ResumeLayout(false);
            this.m_panelHeader.PerformLayout();
            this.c2iPanelOmbre2.ResumeLayout(false);
            this.c2iPanelOmbre2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button m_btnAnnuler;
        private System.Windows.Forms.Button m_btnOk;
        private sc2i.win32.common.CExtStyle m_extStyle;
        private sc2i.win32.common.C2iPanelOmbre m_panelHeader;
        private System.Windows.Forms.TextBox m_txtLibelleElement;
        private System.Windows.Forms.TextBox m_txtPrefix;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private futurocom.win32.easyquery.CEditeurEasyQuery m_panelQuery;
        private sc2i.win32.common.C2iPanelOmbre c2iPanelOmbre2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView m_wndListeTablesAExporter;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}