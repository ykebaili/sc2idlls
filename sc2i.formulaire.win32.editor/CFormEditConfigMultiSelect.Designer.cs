namespace sc2i.formulaire.win32
{
    partial class CFormEditConfigMultiSelect
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.m_extstyle = new sc2i.win32.common.CExtStyle();
            this.m_txtZoomFormule = new sc2i.win32.expression.CTextBoxZoomFormule();
            this.label2 = new System.Windows.Forms.Label();
            this.m_panelColonnes = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.m_btnAdd = new sc2i.win32.common.CWndLinkStd();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_txtZoomFormule);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(574, 30);
            this.m_extstyle.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extstyle.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 30);
            this.m_extstyle.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extstyle.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 0;
            this.label1.Text = "Checked value formula|20029";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_txtZoomFormule
            // 
            this.m_txtZoomFormule.AllowGraphic = true;
            this.m_txtZoomFormule.AllowNullFormula = false;
            this.m_txtZoomFormule.AllowSaisieTexte = true;
            this.m_txtZoomFormule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtZoomFormule.Formule = null;
            this.m_txtZoomFormule.Location = new System.Drawing.Point(148, 0);
            this.m_txtZoomFormule.LockEdition = false;
            this.m_txtZoomFormule.LockZoneTexte = false;
            this.m_txtZoomFormule.Name = "m_txtZoomFormule";
            this.m_txtZoomFormule.Size = new System.Drawing.Size(426, 30);
            this.m_extstyle.SetStyleBackColor(this.m_txtZoomFormule, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extstyle.SetStyleForeColor(this.m_txtZoomFormule, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtZoomFormule.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(574, 23);
            this.m_extstyle.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extstyle.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 1;
            this.label2.Text = "Columns|20030";
            // 
            // m_panelColonnes
            // 
            this.m_panelColonnes.AutoScroll = true;
            this.m_panelColonnes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelColonnes.Location = new System.Drawing.Point(0, 80);
            this.m_panelColonnes.Name = "m_panelColonnes";
            this.m_panelColonnes.Size = new System.Drawing.Size(574, 191);
            this.m_extstyle.SetStyleBackColor(this.m_panelColonnes, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extstyle.SetStyleForeColor(this.m_panelColonnes, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelColonnes.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.m_btnOk);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 271);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(574, 62);
            this.m_extstyle.SetStyleBackColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extstyle.SetStyleForeColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel2.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(298, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.m_extstyle.SetStyleBackColor(this.button1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extstyle.SetStyleForeColor(this.button1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.button1.TabIndex = 3;
            this.button1.Text = "Cancel|2";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // m_btnOk
            // 
            this.m_btnOk.Location = new System.Drawing.Point(201, 20);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_extstyle.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extstyle.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 2;
            this.m_btnOk.Text = "Ok|1";
            this.m_btnOk.UseVisualStyleBackColor = true;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.m_btnAdd);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 53);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(574, 27);
            this.m_extstyle.SetStyleBackColor(this.panel3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extstyle.SetStyleForeColor(this.panel3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel3.TabIndex = 0;
            // 
            // m_btnAdd
            // 
            this.m_btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnAdd.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_btnAdd.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_btnAdd.Location = new System.Drawing.Point(0, 0);
            this.m_btnAdd.Name = "m_btnAdd";
            this.m_btnAdd.ShortMode = false;
            this.m_btnAdd.Size = new System.Drawing.Size(78, 27);
            this.m_extstyle.SetStyleBackColor(this.m_btnAdd, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extstyle.SetStyleForeColor(this.m_btnAdd, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAdd.TabIndex = 0;
            this.m_btnAdd.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_btnAdd.LinkClicked += new System.EventHandler(this.m_btnAdd_LinkClicked);
            // 
            // CFormEditConfigMultiSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 333);
            this.Controls.Add(this.m_panelColonnes);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CFormEditConfigMultiSelect";
            this.m_extstyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.m_extstyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Multi select setup|20028";
            this.Load += new System.EventHandler(this.CFormEditConfigMultiSelect_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private sc2i.win32.expression.CTextBoxZoomFormule m_txtZoomFormule;
        private sc2i.win32.common.CExtStyle m_extstyle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel m_panelColonnes;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.Panel panel3;
        private sc2i.win32.common.CWndLinkStd m_btnAdd;
    }
}