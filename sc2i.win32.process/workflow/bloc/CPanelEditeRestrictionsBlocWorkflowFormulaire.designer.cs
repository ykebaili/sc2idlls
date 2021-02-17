namespace sc2i.win32.process.workflow.bloc
{
    partial class CPanelEditeRestrictionsBlocWorkflowFormulaire
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPanelEditeRestrictionsBlocWorkflowFormulaire));
            this.m_cmbType = new sc2i.win32.common.C2iComboSelectDynamicClass(this.components);
            this.m_panelTypes = new sc2i.win32.common.C2iPanel(this.components);
            this.m_wndListeTypes = new System.Windows.Forms.ListView();
            this.c2iPanel1 = new sc2i.win32.common.C2iPanel(this.components);
            this.m_wndAddType = new sc2i.win32.common.CWndLinkStd();
            this.m_wndSupprimer = new sc2i.win32.common.CWndLinkStd();
            this.m_panelDetailType = new System.Windows.Forms.Panel();
            this.m_wndListeChamps = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.m_imagesDroits = new System.Windows.Forms.ImageList(this.components);
            this.m_panelGlobal = new System.Windows.Forms.Panel();
            this.m_imageNoDelete = new System.Windows.Forms.PictureBox();
            this.m_imageNoAdd = new System.Windows.Forms.PictureBox();
            this.m_lblRestrictionGlobale = new System.Windows.Forms.Label();
            this.m_imageRestrictionGlobale = new System.Windows.Forms.PictureBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.m_extModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_panelTypes.SuspendLayout();
            this.c2iPanel1.SuspendLayout();
            this.m_panelDetailType.SuspendLayout();
            this.m_panelGlobal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_imageNoDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_imageNoAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_imageRestrictionGlobale)).BeginInit();
            this.SuspendLayout();
            // 
            // m_cmbType
            // 
            this.m_cmbType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_cmbType.FormattingEnabled = true;
            this.m_cmbType.IsLink = false;
            this.m_cmbType.Location = new System.Drawing.Point(0, 0);
            this.m_cmbType.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_cmbType, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_cmbType.Name = "m_cmbType";
            this.m_cmbType.Size = new System.Drawing.Size(228, 21);
            this.m_cmbType.TabIndex = 0;
            this.m_cmbType.TypeSelectionne = null;
            // 
            // m_panelTypes
            // 
            this.m_panelTypes.Controls.Add(this.m_wndListeTypes);
            this.m_panelTypes.Controls.Add(this.c2iPanel1);
            this.m_panelTypes.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_panelTypes.Location = new System.Drawing.Point(0, 0);
            this.m_panelTypes.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_panelTypes, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelTypes.Name = "m_panelTypes";
            this.m_panelTypes.Size = new System.Drawing.Size(280, 261);
            this.m_panelTypes.TabIndex = 1;
            // 
            // m_wndListeTypes
            // 
            this.m_wndListeTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeTypes.HideSelection = false;
            this.m_wndListeTypes.Location = new System.Drawing.Point(0, 23);
            this.m_extModeEdition.SetModeEdition(this.m_wndListeTypes, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_wndListeTypes.MultiSelect = false;
            this.m_wndListeTypes.Name = "m_wndListeTypes";
            this.m_wndListeTypes.Size = new System.Drawing.Size(280, 238);
            this.m_wndListeTypes.TabIndex = 2;
            this.m_wndListeTypes.UseCompatibleStateImageBehavior = false;
            this.m_wndListeTypes.View = System.Windows.Forms.View.List;
            this.m_wndListeTypes.SelectedIndexChanged += new System.EventHandler(this.m_wndListeTypes_SelectedIndexChanged);
            // 
            // c2iPanel1
            // 
            this.c2iPanel1.Controls.Add(this.m_cmbType);
            this.c2iPanel1.Controls.Add(this.m_wndAddType);
            this.c2iPanel1.Controls.Add(this.m_wndSupprimer);
            this.c2iPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.c2iPanel1.Location = new System.Drawing.Point(0, 0);
            this.c2iPanel1.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.c2iPanel1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.c2iPanel1.Name = "c2iPanel1";
            this.c2iPanel1.Size = new System.Drawing.Size(280, 23);
            this.c2iPanel1.TabIndex = 2;
            // 
            // m_wndAddType
            // 
            this.m_wndAddType.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_wndAddType.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_wndAddType.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_wndAddType.Location = new System.Drawing.Point(228, 0);
            this.m_extModeEdition.SetModeEdition(this.m_wndAddType, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_wndAddType.Name = "m_wndAddType";
            this.m_wndAddType.ShortMode = false;
            this.m_wndAddType.Size = new System.Drawing.Size(26, 23);
            this.m_wndAddType.TabIndex = 2;
            this.m_wndAddType.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_wndAddType.LinkClicked += new System.EventHandler(this.m_wndAddType_LinkClicked);
            // 
            // m_wndSupprimer
            // 
            this.m_wndSupprimer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_wndSupprimer.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_wndSupprimer.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_wndSupprimer.Location = new System.Drawing.Point(254, 0);
            this.m_extModeEdition.SetModeEdition(this.m_wndSupprimer, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_wndSupprimer.Name = "m_wndSupprimer";
            this.m_wndSupprimer.ShortMode = false;
            this.m_wndSupprimer.Size = new System.Drawing.Size(26, 23);
            this.m_wndSupprimer.TabIndex = 3;
            this.m_wndSupprimer.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_wndSupprimer.LinkClicked += new System.EventHandler(this.m_wndSupprimer_LinkClicked);
            // 
            // m_panelDetailType
            // 
            this.m_panelDetailType.Controls.Add(this.m_wndListeChamps);
            this.m_panelDetailType.Controls.Add(this.m_panelGlobal);
            this.m_panelDetailType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelDetailType.Location = new System.Drawing.Point(283, 0);
            this.m_extModeEdition.SetModeEdition(this.m_panelDetailType, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelDetailType.Name = "m_panelDetailType";
            this.m_panelDetailType.Size = new System.Drawing.Size(198, 261);
            this.m_panelDetailType.TabIndex = 2;
            // 
            // m_wndListeChamps
            // 
            this.m_wndListeChamps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.m_wndListeChamps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeChamps.Location = new System.Drawing.Point(0, 44);
            this.m_extModeEdition.SetModeEdition(this.m_wndListeChamps, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_wndListeChamps.Name = "m_wndListeChamps";
            this.m_wndListeChamps.Size = new System.Drawing.Size(198, 217);
            this.m_wndListeChamps.SmallImageList = this.m_imagesDroits;
            this.m_wndListeChamps.TabIndex = 2;
            this.m_wndListeChamps.UseCompatibleStateImageBehavior = false;
            this.m_wndListeChamps.View = System.Windows.Forms.View.Details;
            this.m_wndListeChamps.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_wndListeChamps_KeyDown);
            this.m_wndListeChamps.Click += new System.EventHandler(this.m_wndListeChamps_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Field|85";
            this.columnHeader1.Width = 279;
            // 
            // m_imagesDroits
            // 
            this.m_imagesDroits.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imagesDroits.ImageStream")));
            this.m_imagesDroits.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imagesDroits.Images.SetKeyName(0, "");
            this.m_imagesDroits.Images.SetKeyName(1, "");
            this.m_imagesDroits.Images.SetKeyName(2, "");
            this.m_imagesDroits.Images.SetKeyName(3, "");
            this.m_imagesDroits.Images.SetKeyName(4, "");
            this.m_imagesDroits.Images.SetKeyName(5, "");
            this.m_imagesDroits.Images.SetKeyName(6, "");
            // 
            // m_panelGlobal
            // 
            this.m_panelGlobal.Controls.Add(this.m_imageNoDelete);
            this.m_panelGlobal.Controls.Add(this.m_imageNoAdd);
            this.m_panelGlobal.Controls.Add(this.m_lblRestrictionGlobale);
            this.m_panelGlobal.Controls.Add(this.m_imageRestrictionGlobale);
            this.m_panelGlobal.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelGlobal.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_panelGlobal, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelGlobal.Name = "m_panelGlobal";
            this.m_panelGlobal.Size = new System.Drawing.Size(198, 44);
            this.m_panelGlobal.TabIndex = 3;
            // 
            // m_imageNoDelete
            // 
            this.m_imageNoDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_imageNoDelete.Location = new System.Drawing.Point(39, 5);
            this.m_extModeEdition.SetModeEdition(this.m_imageNoDelete, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_imageNoDelete.Name = "m_imageNoDelete";
            this.m_imageNoDelete.Size = new System.Drawing.Size(16, 16);
            this.m_imageNoDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.m_imageNoDelete.TabIndex = 4014;
            this.m_imageNoDelete.TabStop = false;
            this.m_imageNoDelete.Click += new System.EventHandler(this.m_imageNoDelete_Click);
            // 
            // m_imageNoAdd
            // 
            this.m_imageNoAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_imageNoAdd.Location = new System.Drawing.Point(23, 5);
            this.m_extModeEdition.SetModeEdition(this.m_imageNoAdd, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_imageNoAdd.Name = "m_imageNoAdd";
            this.m_imageNoAdd.Size = new System.Drawing.Size(16, 16);
            this.m_imageNoAdd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.m_imageNoAdd.TabIndex = 4013;
            this.m_imageNoAdd.TabStop = false;
            this.m_imageNoAdd.Click += new System.EventHandler(this.m_imageNoAdd_Click);
            // 
            // m_lblRestrictionGlobale
            // 
            this.m_lblRestrictionGlobale.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblRestrictionGlobale.Location = new System.Drawing.Point(63, 5);
            this.m_extModeEdition.SetModeEdition(this.m_lblRestrictionGlobale, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblRestrictionGlobale.Name = "m_lblRestrictionGlobale";
            this.m_lblRestrictionGlobale.Size = new System.Drawing.Size(132, 16);
            this.m_lblRestrictionGlobale.TabIndex = 4012;
            this.m_lblRestrictionGlobale.Text = "[]";
            // 
            // m_imageRestrictionGlobale
            // 
            this.m_imageRestrictionGlobale.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_imageRestrictionGlobale.Location = new System.Drawing.Point(7, 5);
            this.m_extModeEdition.SetModeEdition(this.m_imageRestrictionGlobale, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_imageRestrictionGlobale.Name = "m_imageRestrictionGlobale";
            this.m_imageRestrictionGlobale.Size = new System.Drawing.Size(16, 16);
            this.m_imageRestrictionGlobale.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.m_imageRestrictionGlobale.TabIndex = 4011;
            this.m_imageRestrictionGlobale.TabStop = false;
            this.m_imageRestrictionGlobale.Click += new System.EventHandler(this.m_imageRestrictionGlobale_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(280, 0);
            this.m_extModeEdition.SetModeEdition(this.splitter1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 261);
            this.splitter1.TabIndex = 0;
            this.splitter1.TabStop = false;
            // 
            // CPanelEditeRestrictionsBlocWorkflowFormulaire
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_panelDetailType);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.m_panelTypes);
            this.m_extModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CPanelEditeRestrictionsBlocWorkflowFormulaire";
            this.Size = new System.Drawing.Size(481, 261);
            this.m_panelTypes.ResumeLayout(false);
            this.c2iPanel1.ResumeLayout(false);
            this.m_panelDetailType.ResumeLayout(false);
            this.m_panelGlobal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_imageNoDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_imageNoAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_imageRestrictionGlobale)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private sc2i.win32.common.C2iComboSelectDynamicClass m_cmbType;
        private sc2i.win32.common.C2iPanel m_panelTypes;
        private System.Windows.Forms.ListView m_wndListeTypes;
        private sc2i.win32.common.C2iPanel c2iPanel1;
        private sc2i.win32.common.CWndLinkStd m_wndAddType;
        private System.Windows.Forms.Panel m_panelDetailType;
        private System.Windows.Forms.Splitter splitter1;
        private sc2i.win32.common.CExtModeEdition m_extModeEdition;
        private System.Windows.Forms.ListView m_wndListeChamps;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ImageList m_imagesDroits;
        private System.Windows.Forms.Panel m_panelGlobal;
        private System.Windows.Forms.PictureBox m_imageNoDelete;
        private System.Windows.Forms.PictureBox m_imageNoAdd;
        private System.Windows.Forms.Label m_lblRestrictionGlobale;
        private System.Windows.Forms.PictureBox m_imageRestrictionGlobale;
        private sc2i.win32.common.CWndLinkStd m_wndSupprimer;
    }
}
