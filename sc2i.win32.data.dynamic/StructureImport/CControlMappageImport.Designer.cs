namespace sc2i.win32.data.dynamic.StructureImport
{
    partial class CControlMappageImport
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CControlMappageImport));
            this.m_lblChampSource = new System.Windows.Forms.Label();
            this.m_container = new System.Windows.Forms.SplitContainer();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.m_cmbChampDest = new sc2i.win32.common.CComboboxAutoFilled();
            this.m_panelKey = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.m_chkKey = new System.Windows.Forms.CheckBox();
            this.m_tooltip = new sc2i.win32.common.CToolTipTraductible(this.components);
            this.m_container.Panel1.SuspendLayout();
            this.m_container.Panel2.SuspendLayout();
            this.m_container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.m_panelKey.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // m_lblChampSource
            // 
            this.m_lblChampSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblChampSource.Location = new System.Drawing.Point(0, 0);
            this.m_lblChampSource.Name = "m_lblChampSource";
            this.m_lblChampSource.Size = new System.Drawing.Size(87, 23);
            this.m_lblChampSource.TabIndex = 0;
            this.m_lblChampSource.Text = "label1";
            this.m_lblChampSource.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_container
            // 
            this.m_container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_container.Location = new System.Drawing.Point(0, 0);
            this.m_container.Name = "m_container";
            // 
            // m_container.Panel1
            // 
            this.m_container.Panel1.Controls.Add(this.m_lblChampSource);
            this.m_container.Panel1.Controls.Add(this.pictureBox1);
            // 
            // m_container.Panel2
            // 
            this.m_container.Panel2.Controls.Add(this.m_cmbChampDest);
            this.m_container.Panel2.Controls.Add(this.m_panelKey);
            this.m_container.Size = new System.Drawing.Size(319, 23);
            this.m_container.SplitterDistance = 106;
            this.m_container.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(87, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(19, 23);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // m_cmbChampDest
            // 
            this.m_cmbChampDest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbChampDest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbChampDest.FormattingEnabled = true;
            this.m_cmbChampDest.IsLink = false;
            this.m_cmbChampDest.ListDonnees = null;
            this.m_cmbChampDest.Location = new System.Drawing.Point(0, 0);
            this.m_cmbChampDest.LockEdition = false;
            this.m_cmbChampDest.Name = "m_cmbChampDest";
            this.m_cmbChampDest.NullAutorise = true;
            this.m_cmbChampDest.ProprieteAffichee = "Nom";
            this.m_cmbChampDest.Size = new System.Drawing.Size(176, 21);
            this.m_cmbChampDest.TabIndex = 0;
            this.m_cmbChampDest.TextNull = "(empty)";
            this.m_cmbChampDest.Tri = true;
            this.m_cmbChampDest.SelectedValueChanged += new System.EventHandler(this.m_cmbChampDest_SelectedValueChanged);
            // 
            // m_panelKey
            // 
            this.m_panelKey.Controls.Add(this.pictureBox2);
            this.m_panelKey.Controls.Add(this.m_chkKey);
            this.m_panelKey.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_panelKey.Location = new System.Drawing.Point(176, 0);
            this.m_panelKey.Name = "m_panelKey";
            this.m_panelKey.Size = new System.Drawing.Size(33, 23);
            this.m_panelKey.TabIndex = 1;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(17, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 23);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            this.m_tooltip.SetToolTip(this.pictureBox2, "use as identification field|");
            // 
            // m_chkKey
            // 
            this.m_chkKey.AutoSize = true;
            this.m_chkKey.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_chkKey.Location = new System.Drawing.Point(0, 0);
            this.m_chkKey.Name = "m_chkKey";
            this.m_chkKey.Size = new System.Drawing.Size(15, 23);
            this.m_chkKey.TabIndex = 0;
            this.m_chkKey.UseVisualStyleBackColor = true;
            // 
            // CControlMappageImport
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.m_container);
            this.Name = "CControlMappageImport";
            this.Size = new System.Drawing.Size(319, 23);
            this.m_container.Panel1.ResumeLayout(false);
            this.m_container.Panel2.ResumeLayout(false);
            this.m_container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.m_panelKey.ResumeLayout(false);
            this.m_panelKey.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label m_lblChampSource;
        private System.Windows.Forms.SplitContainer m_container;
        private System.Windows.Forms.PictureBox pictureBox1;
        private sc2i.win32.common.CComboboxAutoFilled m_cmbChampDest;
        private System.Windows.Forms.Panel m_panelKey;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.CheckBox m_chkKey;
        private sc2i.win32.common.CToolTipTraductible m_tooltip;
    }
}
