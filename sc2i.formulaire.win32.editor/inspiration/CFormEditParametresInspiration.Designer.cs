namespace sc2i.formulaire.win32.inspiration
{
    partial class CFormEditParametresInspiration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditParametresInspiration));
            this.m_panelBas = new System.Windows.Forms.Panel();
            this.m_btnValiderModifications = new System.Windows.Forms.Button();
            this.m_btnAnnulerModifications = new System.Windows.Forms.Button();
            this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.m_panelParametres = new sc2i.formulaire.win32.inspiration.CPanelEditParametresInspiration();
            this.m_panelBas.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panelBas
            // 
            this.m_panelBas.BackColor = System.Drawing.Color.White;
            this.m_panelBas.Controls.Add(this.m_btnValiderModifications);
            this.m_panelBas.Controls.Add(this.m_btnAnnulerModifications);
            this.m_panelBas.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_panelBas.Location = new System.Drawing.Point(0, 287);
            this.m_panelBas.Name = "m_panelBas";
            this.m_panelBas.Size = new System.Drawing.Size(611, 43);
            this.cExtStyle1.SetStyleBackColor(this.m_panelBas, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelBas, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelBas.TabIndex = 4;
            // 
            // m_btnValiderModifications
            // 
            this.m_btnValiderModifications.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnValiderModifications.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnValiderModifications.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnValiderModifications.ForeColor = System.Drawing.Color.White;
            this.m_btnValiderModifications.Image = ((System.Drawing.Image)(resources.GetObject("m_btnValiderModifications.Image")));
            this.m_btnValiderModifications.Location = new System.Drawing.Point(270, 6);
            this.m_btnValiderModifications.Name = "m_btnValiderModifications";
            this.m_btnValiderModifications.Size = new System.Drawing.Size(32, 32);
            this.cExtStyle1.SetStyleBackColor(this.m_btnValiderModifications, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnValiderModifications, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnValiderModifications.TabIndex = 4;
            this.m_btnValiderModifications.TabStop = false;
            this.m_btnValiderModifications.Click += new System.EventHandler(this.m_btnValiderModifications_Click);
            // 
            // m_btnAnnulerModifications
            // 
            this.m_btnAnnulerModifications.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnAnnulerModifications.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnAnnulerModifications.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnulerModifications.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnAnnulerModifications.ForeColor = System.Drawing.Color.White;
            this.m_btnAnnulerModifications.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAnnulerModifications.Image")));
            this.m_btnAnnulerModifications.Location = new System.Drawing.Point(308, 6);
            this.m_btnAnnulerModifications.Name = "m_btnAnnulerModifications";
            this.m_btnAnnulerModifications.Size = new System.Drawing.Size(32, 32);
            this.cExtStyle1.SetStyleBackColor(this.m_btnAnnulerModifications, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnAnnulerModifications, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAnnulerModifications.TabIndex = 5;
            this.m_btnAnnulerModifications.TabStop = false;
            this.m_btnAnnulerModifications.Click += new System.EventHandler(this.m_btnAnnulerModifications_Click);
            // 
            // m_panelParametres
            // 
            this.m_panelParametres.CurrentItemIndex = null;
            this.m_panelParametres.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelParametres.Items = new sc2i.win32.common.customizableList.CCustomizableListItem[0];
            this.m_panelParametres.Location = new System.Drawing.Point(0, 0);
            this.m_panelParametres.LockEdition = false;
            this.m_panelParametres.Name = "m_panelParametres";
            this.m_panelParametres.Size = new System.Drawing.Size(611, 287);
            this.cExtStyle1.SetStyleBackColor(this.m_panelParametres, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelParametres, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelParametres.TabIndex = 5;
            // 
            // CFormEditParametresInspiration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 330);
            this.Controls.Add(this.m_panelParametres);
            this.Controls.Add(this.m_panelBas);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CFormEditParametresInspiration";
            this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.Text = "Inspiration parameters|20027";
            this.Load += new System.EventHandler(this.CFormEditParametresInspiration_Load);
            this.m_panelBas.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_panelBas;
        protected System.Windows.Forms.Button m_btnValiderModifications;
        protected System.Windows.Forms.Button m_btnAnnulerModifications;
        private sc2i.win32.common.CExtStyle cExtStyle1;
        private CPanelEditParametresInspiration m_panelParametres;
    }
}