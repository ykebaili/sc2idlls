namespace sc2i.win32.expression
{
    partial class CFormEditeListeFonctionsDynamiques
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditeListeFonctionsDynamiques));
            this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnRemove = new sc2i.win32.common.CWndLinkStd();
            this.m_btnEdit = new sc2i.win32.common.CWndLinkStd();
            this.m_btnAdd = new sc2i.win32.common.CWndLinkStd();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.m_wndListeFonctions = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.m_btnRemove);
            this.panel1.Controls.Add(this.m_btnEdit);
            this.panel1.Controls.Add(this.m_btnAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(327, 19);
            this.cExtStyle1.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.panel1.TabIndex = 0;
            // 
            // m_btnRemove
            // 
            this.m_btnRemove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnRemove.CustomImage = ((System.Drawing.Image)(resources.GetObject("m_btnRemove.CustomImage")));
            this.m_btnRemove.CustomText = "Remove";
            this.m_btnRemove.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_btnRemove.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_btnRemove.Location = new System.Drawing.Point(152, 0);
            this.m_btnRemove.Name = "m_btnRemove";
            this.m_btnRemove.ShortMode = false;
            this.m_btnRemove.Size = new System.Drawing.Size(76, 19);
            this.cExtStyle1.SetStyleBackColor(this.m_btnRemove, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnRemove, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnRemove.TabIndex = 2;
            this.m_btnRemove.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_btnRemove.LinkClicked += new System.EventHandler(this.m_btnRemove_LinkClicked);
            // 
            // m_btnEdit
            // 
            this.m_btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnEdit.CustomImage = ((System.Drawing.Image)(resources.GetObject("m_btnEdit.CustomImage")));
            this.m_btnEdit.CustomText = "Detail";
            this.m_btnEdit.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_btnEdit.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_btnEdit.Location = new System.Drawing.Point(76, 0);
            this.m_btnEdit.Name = "m_btnEdit";
            this.m_btnEdit.ShortMode = false;
            this.m_btnEdit.Size = new System.Drawing.Size(76, 19);
            this.cExtStyle1.SetStyleBackColor(this.m_btnEdit, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnEdit, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnEdit.TabIndex = 1;
            this.m_btnEdit.TypeLien = sc2i.win32.common.TypeLinkStd.Modification;
            this.m_btnEdit.LinkClicked += new System.EventHandler(this.m_btnEdit_LinkClicked);
            // 
            // m_btnAdd
            // 
            this.m_btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnAdd.CustomImage = ((System.Drawing.Image)(resources.GetObject("m_btnAdd.CustomImage")));
            this.m_btnAdd.CustomText = "Add";
            this.m_btnAdd.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_btnAdd.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_btnAdd.Location = new System.Drawing.Point(0, 0);
            this.m_btnAdd.Name = "m_btnAdd";
            this.m_btnAdd.ShortMode = false;
            this.m_btnAdd.Size = new System.Drawing.Size(76, 19);
            this.cExtStyle1.SetStyleBackColor(this.m_btnAdd, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnAdd, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAdd.TabIndex = 0;
            this.m_btnAdd.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_btnAdd.LinkClicked += new System.EventHandler(this.m_btnAdd_LinkClicked);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_btnCancel);
            this.panel2.Controls.Add(this.m_btnOk);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 261);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(327, 37);
            this.cExtStyle1.SetStyleBackColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel2.TabIndex = 1;
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnCancel.Image = global::sc2i.win32.expression.Resource1.cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(175, 0);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(48, 33);
            this.cExtStyle1.SetStyleBackColor(this.m_btnCancel, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnCancel, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnCancel.TabIndex = 0;
            this.m_btnCancel.UseVisualStyleBackColor = true;
            this.m_btnCancel.Click += new System.EventHandler(this.m_btnCancel_Click);
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnOk.Image = global::sc2i.win32.expression.Resource1.check;
            this.m_btnOk.Location = new System.Drawing.Point(103, 0);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(48, 32);
            this.cExtStyle1.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 0;
            this.m_btnOk.UseVisualStyleBackColor = true;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.panel3.Controls.Add(this.m_wndListeFonctions);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.ForeColor = System.Drawing.Color.Black;
            this.panel3.Location = new System.Drawing.Point(0, 19);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(327, 242);
            this.cExtStyle1.SetStyleBackColor(this.panel3, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.panel3, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.panel3.TabIndex = 2;
            // 
            // m_wndListeFonctions
            // 
            this.m_wndListeFonctions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.m_wndListeFonctions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeFonctions.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_wndListeFonctions.Location = new System.Drawing.Point(0, 0);
            this.m_wndListeFonctions.MultiSelect = false;
            this.m_wndListeFonctions.Name = "m_wndListeFonctions";
            this.m_wndListeFonctions.Size = new System.Drawing.Size(327, 242);
            this.cExtStyle1.SetStyleBackColor(this.m_wndListeFonctions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_wndListeFonctions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListeFonctions.TabIndex = 0;
            this.m_wndListeFonctions.UseCompatibleStateImageBehavior = false;
            this.m_wndListeFonctions.View = System.Windows.Forms.View.Details;
            this.m_wndListeFonctions.Resize += new System.EventHandler(this.m_wndListeFonctions_Resize);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 307;
            // 
            // CFormEditeListeFonctionsDynamiques
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 298);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CFormEditeListeFonctionsDynamiques";
            this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Functions|20035";
            this.Load += new System.EventHandler(this.CFormEditeListeFonctionsDynamiques_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private sc2i.win32.common.CExtStyle cExtStyle1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button m_btnCancel;
        private sc2i.win32.common.CWndLinkStd m_btnRemove;
        private sc2i.win32.common.CWndLinkStd m_btnEdit;
        private sc2i.win32.common.CWndLinkStd m_btnAdd;
        private System.Windows.Forms.ListView m_wndListeFonctions;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}