namespace sc2i.win32.data.dynamic
{
	partial class CPanelEditTableExportCumulee
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPanelEditTableExportCumulee));
            this.m_btnBas = new System.Windows.Forms.PictureBox();
            this.m_btnHaut = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_wndListeChamps = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.m_btnSupprimer = new sc2i.win32.common.CWndLinkStd();
            this.m_btnDetail = new sc2i.win32.common.CWndLinkStd();
            this.m_btnAjouter = new sc2i.win32.common.CWndLinkStd();
            this.m_panelNomTableEtFiltre = new System.Windows.Forms.Panel();
            this.m_panelFiltreOuFormule = new System.Windows.Forms.Panel();
            this.m_imageFiltre = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_txtNomTable = new sc2i.win32.common.C2iTextBox();
            this.m_imagesFiltre = new System.Windows.Forms.ImageList(this.components);
            this.m_gestionnaireModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_panelTableauCroise = new System.Windows.Forms.Panel();
            this.m_chkCroiser = new System.Windows.Forms.CheckBox();
            this.m_lnkTableauCroise = new System.Windows.Forms.LinkLabel();
            this.m_imagesChamps = new System.Windows.Forms.ImageList(this.components);
            this.m_tooltip = new sc2i.win32.common.CToolTipTraductible(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.m_btnBas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnHaut)).BeginInit();
            this.m_panelNomTableEtFiltre.SuspendLayout();
            this.m_panelFiltreOuFormule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_imageFiltre)).BeginInit();
            this.m_panelTableauCroise.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_btnBas
            // 
            this.m_btnBas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnBas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnBas.Image = ((System.Drawing.Image)(resources.GetObject("m_btnBas.Image")));
            this.m_btnBas.Location = new System.Drawing.Point(493, 31);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnBas, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnBas.Name = "m_btnBas";
            this.m_btnBas.Size = new System.Drawing.Size(15, 15);
            this.m_btnBas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.m_btnBas.TabIndex = 10;
            this.m_btnBas.TabStop = false;
            this.m_btnBas.Click += new System.EventHandler(this.m_btnBas_Click);
            // 
            // m_btnHaut
            // 
            this.m_btnHaut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnHaut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnHaut.Image = ((System.Drawing.Image)(resources.GetObject("m_btnHaut.Image")));
            this.m_btnHaut.Location = new System.Drawing.Point(472, 31);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnHaut, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnHaut.Name = "m_btnHaut";
            this.m_btnHaut.Size = new System.Drawing.Size(15, 15);
            this.m_btnHaut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.m_btnHaut.TabIndex = 9;
            this.m_btnHaut.TabStop = false;
            this.m_btnHaut.Click += new System.EventHandler(this.m_btnHaut_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 28);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label4, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Table fields|186";
            // 
            // m_wndListeChamps
            // 
            this.m_wndListeChamps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_wndListeChamps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.m_wndListeChamps.FullRowSelect = true;
            this.m_wndListeChamps.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_wndListeChamps.HideSelection = false;
            this.m_wndListeChamps.LabelEdit = true;
            this.m_wndListeChamps.Location = new System.Drawing.Point(6, 47);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_wndListeChamps, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_wndListeChamps.MultiSelect = false;
            this.m_wndListeChamps.Name = "m_wndListeChamps";
            this.m_wndListeChamps.Size = new System.Drawing.Size(502, 174);
            this.m_wndListeChamps.SmallImageList = this.m_imagesChamps;
            this.m_wndListeChamps.TabIndex = 2;
            this.m_wndListeChamps.UseCompatibleStateImageBehavior = false;
            this.m_wndListeChamps.View = System.Windows.Forms.View.Details;
            this.m_wndListeChamps.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.m_wndListeChamps_AfterLabelEdit);
            this.m_wndListeChamps.MouseMove += new System.Windows.Forms.MouseEventHandler(this.m_wndListeChamps_MouseMove);
            this.m_wndListeChamps.BeforeLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.m_wndListeChamps_BeforeLabelEdit);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 500;
            // 
            // m_btnSupprimer
            // 
            this.m_btnSupprimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnSupprimer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnSupprimer.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_btnSupprimer.Location = new System.Drawing.Point(146, 227);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnSupprimer, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnSupprimer.Name = "m_btnSupprimer";
            this.m_btnSupprimer.Size = new System.Drawing.Size(80, 16);
            this.m_btnSupprimer.TabIndex = 6;
            this.m_btnSupprimer.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_btnSupprimer.LinkClicked += new System.EventHandler(this.m_btnSupprimer_LinkClicked);
            // 
            // m_btnDetail
            // 
            this.m_btnDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnDetail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnDetail.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_btnDetail.Location = new System.Drawing.Point(76, 227);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnDetail, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnDetail.Name = "m_btnDetail";
            this.m_btnDetail.Size = new System.Drawing.Size(64, 16);
            this.m_btnDetail.TabIndex = 5;
            this.m_btnDetail.TypeLien = sc2i.win32.common.TypeLinkStd.Modification;
            this.m_btnDetail.LinkClicked += new System.EventHandler(this.m_btnDetail_LinkClicked);
            // 
            // m_btnAjouter
            // 
            this.m_btnAjouter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnAjouter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnAjouter.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_btnAjouter.Location = new System.Drawing.Point(6, 227);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnAjouter, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnAjouter.Name = "m_btnAjouter";
            this.m_btnAjouter.Size = new System.Drawing.Size(64, 16);
            this.m_btnAjouter.TabIndex = 4;
            this.m_btnAjouter.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_btnAjouter.LinkClicked += new System.EventHandler(this.m_btnAjouter_LinkClicked);
            // 
            // m_panelNomTableEtFiltre
            // 
            this.m_panelNomTableEtFiltre.Controls.Add(this.m_panelFiltreOuFormule);
            this.m_panelNomTableEtFiltre.Controls.Add(this.label3);
            this.m_panelNomTableEtFiltre.Controls.Add(this.m_txtNomTable);
            this.m_panelNomTableEtFiltre.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelNomTableEtFiltre.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelNomTableEtFiltre, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelNomTableEtFiltre.Name = "m_panelNomTableEtFiltre";
            this.m_panelNomTableEtFiltre.Size = new System.Drawing.Size(520, 25);
            this.m_panelNomTableEtFiltre.TabIndex = 13;
            // 
            // m_panelFiltreOuFormule
            // 
            this.m_panelFiltreOuFormule.Controls.Add(this.m_imageFiltre);
            this.m_panelFiltreOuFormule.Location = new System.Drawing.Point(90, 3);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelFiltreOuFormule, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelFiltreOuFormule.Name = "m_panelFiltreOuFormule";
            this.m_panelFiltreOuFormule.Size = new System.Drawing.Size(24, 17);
            this.m_panelFiltreOuFormule.TabIndex = 12;
            // 
            // m_imageFiltre
            // 
            this.m_imageFiltre.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_imageFiltre.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_imageFiltre.Image = ((System.Drawing.Image)(resources.GetObject("m_imageFiltre.Image")));
            this.m_imageFiltre.Location = new System.Drawing.Point(8, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_imageFiltre, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_imageFiltre.Name = "m_imageFiltre";
            this.m_imageFiltre.Size = new System.Drawing.Size(16, 17);
            this.m_imageFiltre.TabIndex = 8;
            this.m_imageFiltre.TabStop = false;
            this.m_imageFiltre.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_imageFiltre_MouseUp);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 2);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label3, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 18);
            this.label3.TabIndex = 1;
            this.label3.Text = "Table|165";
            // 
            // m_txtNomTable
            // 
            this.m_txtNomTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtNomTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtNomTable.Location = new System.Drawing.Point(120, 0);
            this.m_txtNomTable.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_txtNomTable, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_txtNomTable.Name = "m_txtNomTable";
            this.m_txtNomTable.Size = new System.Drawing.Size(397, 22);
            this.m_txtNomTable.TabIndex = 0;
            // 
            // m_imagesFiltre
            // 
            this.m_imagesFiltre.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imagesFiltre.ImageStream")));
            this.m_imagesFiltre.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imagesFiltre.Images.SetKeyName(0, "");
            this.m_imagesFiltre.Images.SetKeyName(1, "");
            // 
            // m_panelTableauCroise
            // 
            this.m_panelTableauCroise.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelTableauCroise.Controls.Add(this.m_chkCroiser);
            this.m_panelTableauCroise.Controls.Add(this.m_lnkTableauCroise);
            this.m_panelTableauCroise.Location = new System.Drawing.Point(373, 222);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelTableauCroise, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelTableauCroise.Name = "m_panelTableauCroise";
            this.m_panelTableauCroise.Size = new System.Drawing.Size(135, 24);
            this.m_panelTableauCroise.TabIndex = 14;
            // 
            // m_chkCroiser
            // 
            this.m_chkCroiser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_chkCroiser.Location = new System.Drawing.Point(3, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_chkCroiser, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_chkCroiser.Name = "m_chkCroiser";
            this.m_chkCroiser.Size = new System.Drawing.Size(16, 24);
            this.m_chkCroiser.TabIndex = 8;
            this.m_chkCroiser.CheckedChanged += new System.EventHandler(this.m_chkCroiser_CheckedChanged);
            // 
            // m_lnkTableauCroise
            // 
            this.m_lnkTableauCroise.Location = new System.Drawing.Point(17, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkTableauCroise, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_lnkTableauCroise.Name = "m_lnkTableauCroise";
            this.m_lnkTableauCroise.Size = new System.Drawing.Size(98, 23);
            this.m_lnkTableauCroise.TabIndex = 7;
            this.m_lnkTableauCroise.TabStop = true;
            this.m_lnkTableauCroise.Text = "Cross Table|187";
            this.m_lnkTableauCroise.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_lnkTableauCroise.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkTableauCroise_LinkClicked);
            // 
            // m_imagesChamps
            // 
            this.m_imagesChamps.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imagesChamps.ImageStream")));
            this.m_imagesChamps.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imagesChamps.Images.SetKeyName(0, "");
            this.m_imagesChamps.Images.SetKeyName(1, "");
            this.m_imagesChamps.Images.SetKeyName(2, "");
            this.m_imagesChamps.Images.SetKeyName(3, "");
            // 
            // CPanelEditTableExportCumulee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.m_panelTableauCroise);
            this.Controls.Add(this.m_panelNomTableEtFiltre);
            this.Controls.Add(this.m_btnAjouter);
            this.Controls.Add(this.m_btnDetail);
            this.Controls.Add(this.m_btnSupprimer);
            this.Controls.Add(this.m_wndListeChamps);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_btnHaut);
            this.Controls.Add(this.m_btnBas);
            this.m_gestionnaireModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CPanelEditTableExportCumulee";
            this.Size = new System.Drawing.Size(520, 246);
            this.Load += new System.EventHandler(this.CPanelEditTableExportCumulee_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_btnBas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnHaut)).EndInit();
            this.m_panelNomTableEtFiltre.ResumeLayout(false);
            this.m_panelNomTableEtFiltre.PerformLayout();
            this.m_panelFiltreOuFormule.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_imageFiltre)).EndInit();
            this.m_panelTableauCroise.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox m_btnBas;
		private System.Windows.Forms.PictureBox m_btnHaut;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ListView m_wndListeChamps;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private sc2i.win32.common.CWndLinkStd m_btnSupprimer;
		private sc2i.win32.common.CWndLinkStd m_btnDetail;
		private sc2i.win32.common.CWndLinkStd m_btnAjouter;
		private System.Windows.Forms.Panel m_panelNomTableEtFiltre;
		private System.Windows.Forms.Label label3;
		private sc2i.win32.common.C2iTextBox m_txtNomTable;
		private System.Windows.Forms.ImageList m_imagesFiltre;
        private System.Windows.Forms.Panel m_panelFiltreOuFormule;
        private System.Windows.Forms.PictureBox m_imageFiltre;
		private sc2i.win32.common.CExtModeEdition m_gestionnaireModeEdition;
        private System.Windows.Forms.Panel m_panelTableauCroise;
        private System.Windows.Forms.CheckBox m_chkCroiser;
        private System.Windows.Forms.LinkLabel m_lnkTableauCroise;
        private System.Windows.Forms.ImageList m_imagesChamps;
        private sc2i.win32.common.CToolTipTraductible m_tooltip;
	}
}
