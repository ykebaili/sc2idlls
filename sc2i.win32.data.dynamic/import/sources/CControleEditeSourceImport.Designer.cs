namespace sc2i.win32.data.dynamic.import.sources
{
    partial class CControleEditeSourceImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CControleEditeSourceImport));
            this.m_imageType = new System.Windows.Forms.PictureBox();
            this.m_panelEditeur = new System.Windows.Forms.Panel();
            this.m_panelOnlyOnCreate = new System.Windows.Forms.Panel();
            this.m_imageOnlyOnCreate = new System.Windows.Forms.PictureBox();
            this.m_imageFormula = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_imageType)).BeginInit();
            this.m_panelOnlyOnCreate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_imageOnlyOnCreate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_imageFormula)).BeginInit();
            this.SuspendLayout();
            // 
            // m_imageType
            // 
            this.m_imageType.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_imageType.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_imageType.Image = ((System.Drawing.Image)(resources.GetObject("m_imageType.Image")));
            this.m_imageType.Location = new System.Drawing.Point(0, 0);
            this.m_imageType.Name = "m_imageType";
            this.m_imageType.Size = new System.Drawing.Size(64, 32);
            this.m_imageType.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.m_imageType.TabIndex = 0;
            this.m_imageType.TabStop = false;
            this.m_imageType.Click += new System.EventHandler(this.m_imageType_Click);
            // 
            // m_panelEditeur
            // 
            this.m_panelEditeur.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelEditeur.Location = new System.Drawing.Point(96, 0);
            this.m_panelEditeur.Name = "m_panelEditeur";
            this.m_panelEditeur.Size = new System.Drawing.Size(306, 32);
            this.m_panelEditeur.TabIndex = 1;
            this.m_panelEditeur.Paint += new System.Windows.Forms.PaintEventHandler(this.m_panelEditeur_Paint);
            // 
            // m_panelOnlyOnCreate
            // 
            this.m_panelOnlyOnCreate.Controls.Add(this.m_imageFormula);
            this.m_panelOnlyOnCreate.Controls.Add(this.m_imageOnlyOnCreate);
            this.m_panelOnlyOnCreate.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_panelOnlyOnCreate.Location = new System.Drawing.Point(64, 0);
            this.m_panelOnlyOnCreate.Name = "m_panelOnlyOnCreate";
            this.m_panelOnlyOnCreate.Size = new System.Drawing.Size(32, 32);
            this.m_panelOnlyOnCreate.TabIndex = 0;
            // 
            // m_imageOnlyOnCreate
            // 
            this.m_imageOnlyOnCreate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_imageOnlyOnCreate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_imageOnlyOnCreate.Image = ((System.Drawing.Image)(resources.GetObject("m_imageOnlyOnCreate.Image")));
            this.m_imageOnlyOnCreate.Location = new System.Drawing.Point(0, 0);
            this.m_imageOnlyOnCreate.Name = "m_imageOnlyOnCreate";
            this.m_imageOnlyOnCreate.Size = new System.Drawing.Size(32, 32);
            this.m_imageOnlyOnCreate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.m_imageOnlyOnCreate.TabIndex = 0;
            this.m_imageOnlyOnCreate.TabStop = false;
            this.m_imageOnlyOnCreate.Click += new System.EventHandler(this.m_imageOnlyOnCreate_Click);
            // 
            // m_imageFormula
            // 
            this.m_imageFormula.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_imageFormula.Image = global::sc2i.win32.data.dynamic.Properties.Resources.Formula_16;
            this.m_imageFormula.Location = new System.Drawing.Point(15, 15);
            this.m_imageFormula.Name = "m_imageFormula";
            this.m_imageFormula.Size = new System.Drawing.Size(16, 16);
            this.m_imageFormula.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.m_imageFormula.TabIndex = 1;
            this.m_imageFormula.TabStop = false;
            this.m_imageFormula.Click += new System.EventHandler(this.m_imageOnlyOnCreate_Click);
            // 
            // CControleEditeSourceImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_panelEditeur);
            this.Controls.Add(this.m_panelOnlyOnCreate);
            this.Controls.Add(this.m_imageType);
            this.Name = "CControleEditeSourceImport";
            this.Size = new System.Drawing.Size(402, 32);
            this.SizeChanged += new System.EventHandler(this.CControleEditeSourceImport_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.m_imageType)).EndInit();
            this.m_panelOnlyOnCreate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_imageOnlyOnCreate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_imageFormula)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox m_imageType;
        private System.Windows.Forms.Panel m_panelEditeur;
        private System.Windows.Forms.Panel m_panelOnlyOnCreate;
        private System.Windows.Forms.PictureBox m_imageOnlyOnCreate;
        private System.Windows.Forms.PictureBox m_imageFormula;
    }
}
