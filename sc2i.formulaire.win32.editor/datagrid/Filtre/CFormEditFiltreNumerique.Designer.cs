namespace sc2i.formulaire.win32.datagrid.Filtre
{
    partial class CFormEditFiltreNumerique
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditFiltreNumerique));
            this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.m_lblTypeFiltre = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_txtValeurNumerique = new sc2i.win32.common.C2iTextBoxNumerique();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_panelForBetween = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.m_valeur2 = new sc2i.win32.common.C2iTextBoxNumerique();
            this.panel1.SuspendLayout();
            this.m_panelForBetween.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_lblTypeFiltre
            // 
            this.m_lblTypeFiltre.Location = new System.Drawing.Point(13, 6);
            this.m_lblTypeFiltre.Name = "m_lblTypeFiltre";
            this.m_lblTypeFiltre.Size = new System.Drawing.Size(133, 23);
            this.cExtStyle1.SetStyleBackColor(this.m_lblTypeFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_lblTypeFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lblTypeFiltre.TabIndex = 0;
            this.m_lblTypeFiltre.Text = "Filter";
            this.m_lblTypeFiltre.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(277, 48);
            this.cExtStyle1.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 2;
            // 
            // m_txtValeurNumerique
            // 
            this.m_txtValeurNumerique.Arrondi = 4;
            this.m_txtValeurNumerique.DecimalAutorise = true;
            this.m_txtValeurNumerique.DoubleValue = null;
            this.m_txtValeurNumerique.EmptyText = "";
            this.m_txtValeurNumerique.IntValue = null;
            this.m_txtValeurNumerique.Location = new System.Drawing.Point(160, 8);
            this.m_txtValeurNumerique.LockEdition = false;
            this.m_txtValeurNumerique.Name = "m_txtValeurNumerique";
            this.m_txtValeurNumerique.NullAutorise = true;
            this.m_txtValeurNumerique.SelectAllOnEnter = true;
            this.m_txtValeurNumerique.SeparateurMilliers = "";
            this.m_txtValeurNumerique.Size = new System.Drawing.Size(100, 20);
            this.cExtStyle1.SetStyleBackColor(this.m_txtValeurNumerique, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtValeurNumerique, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtValeurNumerique.TabIndex = 0;
            this.m_txtValeurNumerique.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnOk.ForeColor = System.Drawing.Color.White;
            this.m_btnOk.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOk.Image")));
            this.m_btnOk.Location = new System.Drawing.Point(91, 2);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(40, 40);
            this.cExtStyle1.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 0;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnAnnuler.ForeColor = System.Drawing.Color.White;
            this.m_btnAnnuler.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAnnuler.Image")));
            this.m_btnAnnuler.Location = new System.Drawing.Point(145, 2);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(40, 40);
            this.cExtStyle1.SetStyleBackColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAnnuler.TabIndex = 1;
            this.m_btnAnnuler.Click += new System.EventHandler(this.m_btnAnnuler_Click);
            // 
            // m_panelForBetween
            // 
            this.m_panelForBetween.Controls.Add(this.m_valeur2);
            this.m_panelForBetween.Controls.Add(this.label1);
            this.m_panelForBetween.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_panelForBetween.Location = new System.Drawing.Point(0, 31);
            this.m_panelForBetween.Name = "m_panelForBetween";
            this.m_panelForBetween.Size = new System.Drawing.Size(277, 23);
            this.cExtStyle1.SetStyleBackColor(this.m_panelForBetween, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelForBetween, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelForBetween.TabIndex = 5;
            this.m_panelForBetween.Visible = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(22, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 23);
            this.cExtStyle1.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 1;
            this.label1.Text = "and|20038";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_valeur2
            // 
            this.m_valeur2.Arrondi = 4;
            this.m_valeur2.DecimalAutorise = true;
            this.m_valeur2.DoubleValue = null;
            this.m_valeur2.EmptyText = "";
            this.m_valeur2.IntValue = null;
            this.m_valeur2.Location = new System.Drawing.Point(160, 1);
            this.m_valeur2.LockEdition = false;
            this.m_valeur2.Name = "m_valeur2";
            this.m_valeur2.NullAutorise = true;
            this.m_valeur2.SelectAllOnEnter = true;
            this.m_valeur2.SeparateurMilliers = "";
            this.m_valeur2.Size = new System.Drawing.Size(100, 20);
            this.cExtStyle1.SetStyleBackColor(this.m_valeur2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_valeur2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_valeur2.TabIndex = 2;
            this.m_valeur2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CFormEditFiltreNumerique
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(277, 102);
            this.ControlBox = false;
            this.Controls.Add(this.m_panelForBetween);
            this.Controls.Add(this.m_txtValeurNumerique);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_lblTypeFiltre);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CFormEditFiltreNumerique";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Numeric filter|20035";
            this.Load += new System.EventHandler(this.CFormEditFiltreNumerique_Load);
            this.panel1.ResumeLayout(false);
            this.m_panelForBetween.ResumeLayout(false);
            this.m_panelForBetween.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private sc2i.win32.common.CExtStyle cExtStyle1;
        private System.Windows.Forms.Label m_lblTypeFiltre;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button m_btnAnnuler;
        private System.Windows.Forms.Button m_btnOk;
        private sc2i.win32.common.C2iTextBoxNumerique m_txtValeurNumerique;
        private System.Windows.Forms.Panel m_panelForBetween;
        private sc2i.win32.common.C2iTextBoxNumerique m_valeur2;
        private System.Windows.Forms.Label label1;
    }
}