namespace sc2i.formulaire.win32
{
    partial class CFormSelect2iWndReference
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
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_panelPreview = new System.Windows.Forms.Panel();
            this.m_wndPreview = new sc2i.win32.common.C2iImageViewer();
            this.m_lblForm = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.m_wndListeForms = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.m_txtSearch = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.m_timerSearch = new System.Windows.Forms.Timer(this.components);
            this.m_timerPreview = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.m_panelPreview.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnCancel);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 275);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(542, 53);
            this.cExtStyle1.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 0;
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_btnCancel.Image = global::sc2i.formulaire.win32.Properties.Resources.cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(283, 6);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(35, 37);
            this.cExtStyle1.SetStyleBackColor(this.m_btnCancel, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnCancel, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnCancel.TabIndex = 1;
            this.m_btnCancel.UseVisualStyleBackColor = true;
            this.m_btnCancel.Click += new System.EventHandler(this.m_btnCancel_Click);
            // 
            // m_btnOk
            // 
            this.m_btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_btnOk.Image = global::sc2i.formulaire.win32.Properties.Resources.check;
            this.m_btnOk.Location = new System.Drawing.Point(225, 6);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(35, 37);
            this.cExtStyle1.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 0;
            this.m_btnOk.UseVisualStyleBackColor = true;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_panelPreview);
            this.panel2.Controls.Add(this.splitter1);
            this.panel2.Controls.Add(this.m_wndListeForms);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(542, 275);
            this.cExtStyle1.SetStyleBackColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel2.TabIndex = 1;
            // 
            // m_panelPreview
            // 
            this.m_panelPreview.Controls.Add(this.m_wndPreview);
            this.m_panelPreview.Controls.Add(this.m_lblForm);
            this.m_panelPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelPreview.Location = new System.Drawing.Point(337, 21);
            this.m_panelPreview.Name = "m_panelPreview";
            this.m_panelPreview.Size = new System.Drawing.Size(205, 254);
            this.cExtStyle1.SetStyleBackColor(this.m_panelPreview, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelPreview, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelPreview.TabIndex = 1;
            // 
            // m_wndPreview
            // 
            this.m_wndPreview.BackColor = System.Drawing.Color.White;
            this.m_wndPreview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_wndPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndPreview.Image = null;
            this.m_wndPreview.Location = new System.Drawing.Point(0, 18);
            this.m_wndPreview.Name = "m_wndPreview";
            this.m_wndPreview.Size = new System.Drawing.Size(205, 236);
            this.cExtStyle1.SetStyleBackColor(this.m_wndPreview, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_wndPreview, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndPreview.TabIndex = 2;
            this.m_wndPreview.Zoom = 1;
            // 
            // m_lblForm
            // 
            this.m_lblForm.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_lblForm.Location = new System.Drawing.Point(0, 0);
            this.m_lblForm.Name = "m_lblForm";
            this.m_lblForm.Size = new System.Drawing.Size(205, 18);
            this.cExtStyle1.SetStyleBackColor(this.m_lblForm, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_lblForm, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lblForm.TabIndex = 3;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(334, 21);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 254);
            this.cExtStyle1.SetStyleBackColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // m_wndListeForms
            // 
            this.m_wndListeForms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.m_wndListeForms.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_wndListeForms.HideSelection = false;
            this.m_wndListeForms.Location = new System.Drawing.Point(0, 21);
            this.m_wndListeForms.MultiSelect = false;
            this.m_wndListeForms.Name = "m_wndListeForms";
            this.m_wndListeForms.Size = new System.Drawing.Size(334, 254);
            this.cExtStyle1.SetStyleBackColor(this.m_wndListeForms, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_wndListeForms, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListeForms.TabIndex = 0;
            this.m_wndListeForms.UseCompatibleStateImageBehavior = false;
            this.m_wndListeForms.View = System.Windows.Forms.View.Details;
            this.m_wndListeForms.SelectedIndexChanged += new System.EventHandler(this.m_wndListeForms_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 313;
            // 
            // m_txtSearch
            // 
            this.m_txtSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.m_txtSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.m_txtSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_txtSearch.Location = new System.Drawing.Point(73, 0);
            this.m_txtSearch.Name = "m_txtSearch";
            this.m_txtSearch.Size = new System.Drawing.Size(187, 20);
            this.cExtStyle1.SetStyleBackColor(this.m_txtSearch, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtSearch, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtSearch.TabIndex = 3;
            this.m_txtSearch.TextChanged += new System.EventHandler(this.m_txtSearch_TextChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.m_txtSearch);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(542, 21);
            this.cExtStyle1.SetStyleBackColor(this.panel4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel4.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 21);
            this.cExtStyle1.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 4;
            this.label1.Text = "Search|20041";
            // 
            // m_timerSearch
            // 
            this.m_timerSearch.Interval = 200;
            this.m_timerSearch.Tick += new System.EventHandler(this.m_timerSearch_Tick);
            // 
            // m_timerPreview
            // 
            this.m_timerPreview.Interval = 200;
            this.m_timerPreview.Tick += new System.EventHandler(this.m_timerPreview_Tick);
            // 
            // CFormSelect2iWndReference
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 328);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CFormSelect2iWndReference";
            this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Select sub form|20040";
            this.Load += new System.EventHandler(this.CFormSelect2iWndReference_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.m_panelPreview.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private sc2i.win32.common.CExtStyle cExtStyle1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button m_btnCancel;
        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.Panel m_panelPreview;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ListView m_wndListeForms;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private sc2i.win32.common.C2iImageViewer m_wndPreview;
        private System.Windows.Forms.Label m_lblForm;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_txtSearch;
        private System.Windows.Forms.Timer m_timerSearch;
        private System.Windows.Forms.Timer m_timerPreview;
    }
}