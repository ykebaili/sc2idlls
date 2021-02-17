using sc2i.win32.common.folderbrowser;
namespace sc2i.win32.process
{
    partial class CFormAddMultipleToGed
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormAddMultipleToGed));
            this.m_DummyImageList = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_panelCategories = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.panel3 = new System.Windows.Forms.Panel();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_browser = new CFileExplorer();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_DummyImageList
            // 
            this.m_DummyImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.m_DummyImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.m_DummyImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.panel2.Controls.Add(this.m_panelCategories);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.ForeColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(551, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(334, 375);
            this.cExtStyle1.SetStyleBackColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.panel2.TabIndex = 4;
            // 
            // m_panelCategories
            // 
            this.m_panelCategories.AutoScroll = true;
            this.m_panelCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelCategories.Location = new System.Drawing.Point(0, 38);
            this.m_panelCategories.Name = "m_panelCategories";
            this.m_panelCategories.Size = new System.Drawing.Size(334, 337);
            this.cExtStyle1.SetStyleBackColor(this.m_panelCategories, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelCategories, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelCategories.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(334, 38);
            this.cExtStyle1.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 0;
            this.label2.Text = "Drag files to EDM categories|20143";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(551, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 375);
            this.cExtStyle1.SetStyleBackColor(this.splitter2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.splitter2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.splitter2.TabIndex = 5;
            this.splitter2.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.m_btnAnnuler);
            this.panel3.Controls.Add(this.m_btnOk);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 375);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(885, 48);
            this.cExtStyle1.SetStyleBackColor(this.panel3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel3.TabIndex = 3;
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnAnnuler.ForeColor = System.Drawing.Color.White;
            this.m_btnAnnuler.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAnnuler.Image")));
            this.m_btnAnnuler.Location = new System.Drawing.Point(449, 2);
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
            this.m_btnOk.Location = new System.Drawing.Point(395, 2);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(40, 40);
            this.cExtStyle1.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 2;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_browser
            // 
            this.m_browser.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_browser.Location = new System.Drawing.Point(0, 0);
            this.m_browser.Name = "m_browser";
            this.m_browser.Size = new System.Drawing.Size(551, 375);
            this.cExtStyle1.SetStyleBackColor(this.m_browser, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_browser, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_browser.TabIndex = 0;
            // 
            // CFormAddMultipleToGed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 423);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.m_browser);
            this.Controls.Add(this.panel3);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CFormAddMultipleToGed";
            this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondFenetre);
            this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.Text = "Add documents to EDM|20145";
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList m_DummyImageList;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Splitter splitter2;
        private sc2i.win32.common.CExtStyle cExtStyle1;
        private System.Windows.Forms.Panel m_panelCategories;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button m_btnAnnuler;
        private System.Windows.Forms.Button m_btnOk;
        private CFileExplorer m_browser;
    }
}

