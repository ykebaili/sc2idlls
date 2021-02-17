namespace sc2i.win32.data.dynamic.unites
{
    partial class CFormEditeClasseUniteInDb
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditeClasseUniteInDb));
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_extStyle = new sc2i.win32.common.CExtStyle();
            this.m_panelClasse = new sc2i.win32.common.C2iPanel(this.components);
            this.m_txtUniteDeBase = new sc2i.win32.common.C2iTextBox();
            this.m_txtIdClasse = new sc2i.win32.common.C2iTextBox();
            this.m_txtLibelleClasse = new sc2i.win32.common.C2iTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.m_panelClasse.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 114);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(377, 48);
            this.m_extStyle.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 9;
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnAnnuler.ForeColor = System.Drawing.Color.White;
            this.m_btnAnnuler.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAnnuler.Image")));
            this.m_btnAnnuler.Location = new System.Drawing.Point(195, 2);
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
            this.m_btnOk.Location = new System.Drawing.Point(141, 2);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(40, 40);
            this.m_extStyle.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 2;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_panelClasse
            // 
            this.m_panelClasse.Controls.Add(this.m_txtUniteDeBase);
            this.m_panelClasse.Controls.Add(this.m_txtIdClasse);
            this.m_panelClasse.Controls.Add(this.m_txtLibelleClasse);
            this.m_panelClasse.Controls.Add(this.label3);
            this.m_panelClasse.Controls.Add(this.label1);
            this.m_panelClasse.Controls.Add(this.label2);
            this.m_panelClasse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelClasse.Location = new System.Drawing.Point(0, 0);
            this.m_panelClasse.LockEdition = false;
            this.m_panelClasse.Name = "m_panelClasse";
            this.m_panelClasse.Size = new System.Drawing.Size(377, 114);
            this.m_extStyle.SetStyleBackColor(this.m_panelClasse, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelClasse, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelClasse.TabIndex = 10;
            // 
            // m_txtUniteDeBase
            // 
            this.m_txtUniteDeBase.Location = new System.Drawing.Point(109, 76);
            this.m_txtUniteDeBase.LockEdition = false;
            this.m_txtUniteDeBase.Name = "m_txtUniteDeBase";
            this.m_txtUniteDeBase.Size = new System.Drawing.Size(248, 20);
            this.m_extStyle.SetStyleBackColor(this.m_txtUniteDeBase, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtUniteDeBase, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtUniteDeBase.TabIndex = 5;
            // 
            // m_txtIdClasse
            // 
            this.m_txtIdClasse.Location = new System.Drawing.Point(109, 50);
            this.m_txtIdClasse.LockEdition = false;
            this.m_txtIdClasse.Name = "m_txtIdClasse";
            this.m_txtIdClasse.Size = new System.Drawing.Size(248, 20);
            this.m_extStyle.SetStyleBackColor(this.m_txtIdClasse, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtIdClasse, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtIdClasse.TabIndex = 4;
            // 
            // m_txtLibelleClasse
            // 
            this.m_txtLibelleClasse.Location = new System.Drawing.Point(109, 24);
            this.m_txtLibelleClasse.LockEdition = false;
            this.m_txtLibelleClasse.Name = "m_txtLibelleClasse";
            this.m_txtLibelleClasse.Size = new System.Drawing.Size(248, 20);
            this.m_extStyle.SetStyleBackColor(this.m_txtLibelleClasse, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_txtLibelleClasse, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtLibelleClasse.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 18);
            this.m_extStyle.SetStyleBackColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label3.TabIndex = 2;
            this.label3.Text = "BaseUnit|20065";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 18);
            this.m_extStyle.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 0;
            this.label1.Text = "Class Label|20063";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 18);
            this.m_extStyle.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 1;
            this.label2.Text = "Id|20064";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CFormEditeClasseUniteInDb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 162);
            this.ControlBox = false;
            this.Controls.Add(this.m_panelClasse);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CFormEditeClasseUniteInDb";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.m_extStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.m_extStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Edit Unit|20072";
            this.Load += new System.EventHandler(this.CFormEditeClasseUniteInDb_Load);
            this.panel1.ResumeLayout(false);
            this.m_panelClasse.ResumeLayout(false);
            this.m_panelClasse.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button m_btnAnnuler;
        private System.Windows.Forms.Button m_btnOk;
        private sc2i.win32.common.CExtStyle m_extStyle;
        private sc2i.win32.common.C2iPanel m_panelClasse;
        private sc2i.win32.common.C2iTextBox m_txtUniteDeBase;
        private sc2i.win32.common.C2iTextBox m_txtIdClasse;
        private sc2i.win32.common.C2iTextBox m_txtLibelleClasse;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}