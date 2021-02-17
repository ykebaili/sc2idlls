using sc2i.win32.data.dynamic;
namespace sc2i.win32.data.navigation
{
    partial class C2iTextBoxSelectionne
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

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_txtSelect = new sc2i.win32.data.dynamic.C2iTextBoxFiltreRapide();
            this.m_link = new System.Windows.Forms.LinkLabel();
            this.m_panelLink = new System.Windows.Forms.Panel();
            this.m_panelImage = new System.Windows.Forms.Panel();
            this.m_picType = new System.Windows.Forms.PictureBox();
            this.m_panelLink.SuspendLayout();
            this.m_panelImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_picType)).BeginInit();
            this.SuspendLayout();
            // 
            // m_txtSelect
            // 
            this.m_txtSelect.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_txtSelect.ElementSelectionne = null;
            this.m_txtSelect.FonctionTextNull = null;
            this.m_txtSelect.ImageDisplayMode = sc2i.win32.data.dynamic.EModeAffichageImageTextBoxRapide.Always;
            this.m_txtSelect.Location = new System.Drawing.Point(0, 0);
            this.m_txtSelect.LockEdition = false;
            this.m_txtSelect.Name = "m_txtSelect";
            this.m_txtSelect.SelectedObject = null;
            this.m_txtSelect.SelectionLength = 0;
            this.m_txtSelect.SelectionStart = 0;
            this.m_txtSelect.Size = new System.Drawing.Size(332, 20);
            this.m_txtSelect.SpecificImage = null;
            this.m_txtSelect.TabIndex = 0;
            this.m_txtSelect.TextNull = "";
            this.m_txtSelect.UseIntellisense = true;
            this.m_txtSelect.ElementSelectionneChanged += new System.EventHandler(this.m_txtSelect_ElementSelectionneChanged);
            // 
            // m_link
            // 
            this.m_link.BackColor = System.Drawing.Color.White;
            this.m_link.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_link.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_link.Location = new System.Drawing.Point(20, 0);
            this.m_link.Name = "m_link";
            this.m_link.Size = new System.Drawing.Size(312, 61);
            this.m_link.TabIndex = 1;
            this.m_link.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_link.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_linkControl_LinkClicked);
            // 
            // m_panelLink
            // 
            this.m_panelLink.Controls.Add(this.m_link);
            this.m_panelLink.Controls.Add(this.m_panelImage);
            this.m_panelLink.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelLink.Location = new System.Drawing.Point(0, 20);
            this.m_panelLink.Name = "m_panelLink";
            this.m_panelLink.Size = new System.Drawing.Size(332, 61);
            this.m_panelLink.TabIndex = 2;
            // 
            // m_panelImage
            // 
            this.m_panelImage.Controls.Add(this.m_picType);
            this.m_panelImage.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_panelImage.Location = new System.Drawing.Point(0, 0);
            this.m_panelImage.Name = "m_panelImage";
            this.m_panelImage.Size = new System.Drawing.Size(20, 61);
            this.m_panelImage.TabIndex = 2;
            // 
            // m_picType
            // 
            this.m_picType.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_picType.Location = new System.Drawing.Point(0, 0);
            this.m_picType.Name = "m_picType";
            this.m_picType.Size = new System.Drawing.Size(20, 20);
            this.m_picType.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.m_picType.TabIndex = 4;
            this.m_picType.TabStop = false;
            this.m_picType.MouseMove += new System.Windows.Forms.MouseEventHandler(this.m_picType_MouseMove);
            this.m_picType.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_picType_MouseDown);
            this.m_picType.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_picType_MouseUp);
            // 
            // C2iTextBoxSelectionne
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_panelLink);
            this.Controls.Add(this.m_txtSelect);
            this.Name = "C2iTextBoxSelectionne";
            this.Size = new System.Drawing.Size(332, 81);
            this.m_panelLink.ResumeLayout(false);
            this.m_panelImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_picType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C2iTextBoxFiltreRapide m_txtSelect;
        private System.Windows.Forms.LinkLabel m_link;
        private System.Windows.Forms.Panel m_panelLink;
        private System.Windows.Forms.Panel m_panelImage;
        private System.Windows.Forms.PictureBox m_picType;
    }
}
