namespace futurocom.win32.easyquery
{
    partial class CFormEditeProprietesFiltreCAML
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
            this.m_chkUseCache = new System.Windows.Forms.CheckBox();
            this.m_txtNomTable = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_exStyle = new sc2i.win32.common.CExtStyle();
            this.m_tabControl = new sc2i.win32.common.C2iTabControl(this.components);
            this.tabPage1 = new Crownwood.Magic.Controls.TabPage();
            this.m_panelCAML = new futurocom.win32.easyquery.CPanelFiltreCAML();
            this.tabPage2 = new Crownwood.Magic.Controls.TabPage();
            this.m_ctrlFormulesNommees = new sc2i.win32.expression.CControlEditListeFormulesNommees();
            this.m_panelFormules = new sc2i.win32.common.C2iPanel(this.components);
            this.tabPage3 = new Crownwood.Magic.Controls.TabPage();
            this.m_panelPostFilter = new futurocom.win32.easyquery.postFilter.CPanelPostFilter();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.m_tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.m_ctrlFormulesNommees.SuspendLayout();
            this.tabPage3.SuspendLayout();
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
            this.m_chkUseCache.Location = new System.Drawing.Point(130, 32);
            this.m_chkUseCache.Name = "m_chkUseCache";
            this.m_chkUseCache.Size = new System.Drawing.Size(134, 17);
            this.m_exStyle.SetStyleBackColor(this.m_chkUseCache, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_chkUseCache, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkUseCache.TabIndex = 5;
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
            this.m_tabControl.SelectedIndex = 2;
            this.m_tabControl.SelectedTab = this.tabPage3;
            this.m_tabControl.Size = new System.Drawing.Size(592, 311);
            this.m_exStyle.SetStyleBackColor(this.m_tabControl, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_exStyle.SetStyleForeColor(this.m_tabControl, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.m_tabControl.TabIndex = 5;
            this.m_tabControl.TabPages.AddRange(new Crownwood.Magic.Controls.TabPage[] {
            this.tabPage1,
            this.tabPage2,
            this.tabPage3});
            this.m_tabControl.TextColor = System.Drawing.Color.Black;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_panelCAML);
            this.tabPage1.Location = new System.Drawing.Point(0, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Selected = false;
            this.tabPage1.Size = new System.Drawing.Size(576, 270);
            this.m_exStyle.SetStyleBackColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage1.TabIndex = 10;
            this.tabPage1.Title = "CAML filter|20024";
            // 
            // m_panelCAML
            // 
            this.m_panelCAML.BackColor = System.Drawing.Color.White;
            this.m_panelCAML.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelCAML.ForeColor = System.Drawing.Color.Black;
            this.m_panelCAML.Location = new System.Drawing.Point(0, 0);
            this.m_panelCAML.Name = "m_panelCAML";
            this.m_panelCAML.Size = new System.Drawing.Size(576, 270);
            this.m_exStyle.SetStyleBackColor(this.m_panelCAML, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_panelCAML, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelCAML.TabIndex = 0;
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
            this.tabPage2.TabIndex = 11;
            this.tabPage2.Title = "Calculated columns|20022";
            // 
            // m_ctrlFormulesNommees
            // 
            this.m_ctrlFormulesNommees.AutoScroll = true;
            this.m_ctrlFormulesNommees.Controls.Add(this.m_panelFormules);
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
            this.m_ctrlFormulesNommees.TabIndex = 1;
            this.m_ctrlFormulesNommees.TypeFormuleNomme = typeof(sc2i.expression.CFormuleNommee);
            // 
            // m_panelFormules
            // 
            this.m_panelFormules.AutoScroll = true;
            this.m_panelFormules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelFormules.Location = new System.Drawing.Point(0, 0);
            this.m_panelFormules.LockEdition = true;
            this.m_panelFormules.Name = "m_panelFormules";
            this.m_panelFormules.Size = new System.Drawing.Size(576, 270);
            this.m_exStyle.SetStyleBackColor(this.m_panelFormules, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.m_panelFormules, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelFormules.TabIndex = 2;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.m_panelPostFilter);
            this.tabPage3.Location = new System.Drawing.Point(0, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(576, 270);
            this.m_exStyle.SetStyleBackColor(this.tabPage3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_exStyle.SetStyleForeColor(this.tabPage3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage3.TabIndex = 12;
            this.tabPage3.Title = "Post filter|20074";
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
            // CFormEditeProprietesFiltreCAML
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
            this.Name = "CFormEditeProprietesFiltreCAML";
            this.m_exStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.m_exStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Filter|20024";
            this.Load += new System.EventHandler(this.CFormEditeProprietesFiltreCAML_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.m_tabControl.ResumeLayout(false);
            this.m_tabControl.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.m_ctrlFormulesNommees.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
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
        private Crownwood.Magic.Controls.TabPage tabPage2;
        private sc2i.win32.expression.CControlEditListeFormulesNommees m_ctrlFormulesNommees;
        private sc2i.win32.common.C2iPanel m_panelFormules;
        private CPanelFiltreCAML m_panelCAML;
        private System.Windows.Forms.CheckBox m_chkUseCache;
        private Crownwood.Magic.Controls.TabPage tabPage3;
        private postFilter.CPanelPostFilter m_panelPostFilter;
    }
}