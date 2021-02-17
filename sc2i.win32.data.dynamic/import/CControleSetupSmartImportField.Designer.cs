namespace sc2i.win32.data.dynamic.import
{
    partial class CControleSetupSmartImportField
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
            this.m_lblChamp = new System.Windows.Forms.Label();
            this.m_lblValeur = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_btnExpand = new System.Windows.Forms.Button();
            this.m_lblMarge = new System.Windows.Forms.Label();
            this.m_controleSource = new sc2i.win32.data.dynamic.import.sources.CControleEditeSourceImport();
            this.m_wndOptionCreation = new sc2i.win32.data.dynamic.import.CControleOptionCreation();
            this.m_panelKey = new System.Windows.Forms.Panel();
            this.m_imageSearchKey = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.m_panelKey.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_imageSearchKey)).BeginInit();
            this.SuspendLayout();
            // 
            // m_lblChamp
            // 
            this.m_lblChamp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lblChamp.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lblChamp.Location = new System.Drawing.Point(48, 0);
            this.m_extModeEdition.SetModeEdition(this.m_lblChamp, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblChamp.Name = "m_lblChamp";
            this.m_lblChamp.Size = new System.Drawing.Size(119, 21);
            this.m_lblChamp.TabIndex = 0;
            this.m_lblChamp.Text = "Field";
            this.m_lblChamp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblValeur
            // 
            this.m_lblValeur.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.m_lblValeur.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lblValeur.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lblValeur.Location = new System.Drawing.Point(167, 0);
            this.m_extModeEdition.SetModeEdition(this.m_lblValeur, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblValeur.Name = "m_lblValeur";
            this.m_lblValeur.Size = new System.Drawing.Size(210, 21);
            this.m_lblValeur.TabIndex = 1;
            this.m_lblValeur.Text = "Value";
            this.m_lblValeur.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_btnExpand);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(21, 0);
            this.m_extModeEdition.SetModeEdition(this.panel1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(27, 21);
            this.panel1.TabIndex = 2;
            // 
            // m_btnExpand
            // 
            this.m_btnExpand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_btnExpand.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_btnExpand, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnExpand.Name = "m_btnExpand";
            this.m_btnExpand.Size = new System.Drawing.Size(27, 21);
            this.m_btnExpand.TabIndex = 0;
            this.m_btnExpand.UseVisualStyleBackColor = true;
            this.m_btnExpand.Click += new System.EventHandler(this.m_btnExpand_Click);
            // 
            // m_lblMarge
            // 
            this.m_lblMarge.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.m_lblMarge.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lblMarge.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_lblMarge, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblMarge.Name = "m_lblMarge";
            this.m_lblMarge.Size = new System.Drawing.Size(21, 21);
            this.m_lblMarge.TabIndex = 3;
            // 
            // m_controleSource
            // 
            this.m_controleSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_controleSource.DefaultValue = null;
            this.m_controleSource.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_controleSource.Location = new System.Drawing.Point(422, 0);
            this.m_extModeEdition.SetModeEdition(this.m_controleSource, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_controleSource.Name = "m_controleSource";
            this.m_controleSource.Size = new System.Drawing.Size(202, 21);
            this.m_controleSource.SourceTable = null;
            this.m_controleSource.TabIndex = 5;
            this.m_controleSource.TypesSourcesValides = null;
            this.m_controleSource.ValueChanged += new System.EventHandler(this.m_controleSource_ValueChanged);
            this.m_controleSource.SourceChanged += new System.EventHandler(this.m_controleSource_SourceChanged);
            // 
            // m_wndOptionCreation
            // 
            this.m_wndOptionCreation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_wndOptionCreation.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_wndOptionCreation.IsApplicable = false;
            this.m_wndOptionCreation.Location = new System.Drawing.Point(398, 0);
            this.m_extModeEdition.SetModeEdition(this.m_wndOptionCreation, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_wndOptionCreation.Name = "m_wndOptionCreation";
            this.m_wndOptionCreation.Option = sc2i.data.dynamic.StructureImport.SmartImport.EOptionCreationElementImport.None;
            this.m_wndOptionCreation.Size = new System.Drawing.Size(24, 21);
            this.m_wndOptionCreation.TabIndex = 4;
            this.m_wndOptionCreation.ValueChanged += new System.EventHandler(this.m_wndOptionCreation_ValueChanged);
            // 
            // m_panelKey
            // 
            this.m_panelKey.Controls.Add(this.m_imageSearchKey);
            this.m_panelKey.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_panelKey.Location = new System.Drawing.Point(377, 0);
            this.m_extModeEdition.SetModeEdition(this.m_panelKey, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelKey.Name = "m_panelKey";
            this.m_panelKey.Size = new System.Drawing.Size(21, 21);
            this.m_panelKey.TabIndex = 6;
            // 
            // m_imageSearchKey
            // 
            this.m_imageSearchKey.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_imageSearchKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_imageSearchKey.Image = global::sc2i.win32.data.dynamic.Properties.Resources.SearchKey_No;
            this.m_imageSearchKey.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_imageSearchKey, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_imageSearchKey.Name = "m_imageSearchKey";
            this.m_imageSearchKey.Size = new System.Drawing.Size(21, 21);
            this.m_imageSearchKey.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.m_imageSearchKey.TabIndex = 0;
            this.m_imageSearchKey.TabStop = false;
            this.m_imageSearchKey.Click += new System.EventHandler(this.m_imageSearchKey_Click);
            // 
            // CControleSetupSmartImportField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_controleSource);
            this.Controls.Add(this.m_wndOptionCreation);
            this.Controls.Add(this.m_panelKey);
            this.Controls.Add(this.m_lblValeur);
            this.Controls.Add(this.m_lblChamp);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_lblMarge);
            this.m_extModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CControleSetupSmartImportField";
            this.Size = new System.Drawing.Size(684, 21);
            this.panel1.ResumeLayout(false);
            this.m_panelKey.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_imageSearchKey)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label m_lblChamp;
        private System.Windows.Forms.Label m_lblValeur;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button m_btnExpand;
        private System.Windows.Forms.Label m_lblMarge;
        private CControleOptionCreation m_wndOptionCreation;
        private sources.CControleEditeSourceImport m_controleSource;
        private System.Windows.Forms.Panel m_panelKey;
        private System.Windows.Forms.PictureBox m_imageSearchKey;
    }
}
