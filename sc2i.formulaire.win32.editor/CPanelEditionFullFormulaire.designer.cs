namespace sc2i.formulaire.win32.editor
{
	partial class CPanelEditionFullFormulaire
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
            sc2i.win32.common.CGrilleEditeurObjetGraphique cGrilleEditeurObjetGraphique1 = new sc2i.win32.common.CGrilleEditeurObjetGraphique();
            sc2i.win32.common.CProfilEditeurObjetGraphique cProfilEditeurObjetGraphique1 = new sc2i.win32.common.CProfilEditeurObjetGraphique();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPanelEditionFullFormulaire));
            this.m_gridProprietes = new System.Windows.Forms.PropertyGrid();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.m_gestionnaireModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.m_panelEvenements = new sc2i.formulaire.win32.CControlEditionHandlersEvenements();
            this.m_pageStructure = new System.Windows.Forms.TabPage();
            this.m_ctrlStructure = new sc2i.formulaire.win32.CControleStructureFormulaire();
            this.m_panelFormulaire = new sc2i.formulaire.win32.editor.CPanelEditionFormulaire();
            this.m_wndListeControles = new sc2i.formulaire.win32.editor.CPanelListe2iWnd();
            this.m_panelToolBar = new System.Windows.Forms.Panel();
            this.m_btnSave = new System.Windows.Forms.PictureBox();
            this.m_btnLoad = new System.Windows.Forms.PictureBox();
            this.m_btnModeZoom = new System.Windows.Forms.RadioButton();
            this.m_btnModeSelection = new System.Windows.Forms.RadioButton();
            this.m_ToolTipTraductible1 = new sc2i.win32.common.CToolTipTraductible(this.components);
            this.m_tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.m_pageStructure.SuspendLayout();
            this.m_panelToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnLoad)).BeginInit();
            this.SuspendLayout();
            // 
            // m_gridProprietes
            // 
            this.m_gridProprietes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridProprietes.Location = new System.Drawing.Point(3, 3);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_gridProprietes, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_gridProprietes.Name = "m_gridProprietes";
            this.m_gridProprietes.Size = new System.Drawing.Size(191, 394);
            this.m_gridProprietes.TabIndex = 0;
            this.m_gridProprietes.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.m_gridProprietes_PropertyValueChanged);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(531, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.splitter1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 426);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(162, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.splitter2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 426);
            this.splitter2.TabIndex = 3;
            this.splitter2.TabStop = false;
            // 
            // m_tabControl
            // 
            this.m_tabControl.Controls.Add(this.tabPage1);
            this.m_tabControl.Controls.Add(this.tabPage2);
            this.m_tabControl.Controls.Add(this.m_pageStructure);
            this.m_tabControl.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_tabControl.Location = new System.Drawing.Point(534, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_tabControl, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_tabControl.Name = "m_tabControl";
            this.m_tabControl.SelectedIndex = 0;
            this.m_tabControl.Size = new System.Drawing.Size(205, 426);
            this.m_tabControl.TabIndex = 5;
            this.m_tabControl.SelectedIndexChanged += new System.EventHandler(this.m_tabControl_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_gridProprietes);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.m_gestionnaireModeEdition.SetModeEdition(this.tabPage1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(197, 400);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Properties|20006";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.m_panelEvenements);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.m_gestionnaireModeEdition.SetModeEdition(this.tabPage2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(197, 400);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Events|20007";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // m_panelEvenements
            // 
            this.m_panelEvenements.AutoScroll = true;
            this.m_panelEvenements.BackColor = System.Drawing.Color.White;
            this.m_panelEvenements.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_panelEvenements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelEvenements.Location = new System.Drawing.Point(3, 3);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelEvenements, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_panelEvenements.Name = "m_panelEvenements";
            this.m_panelEvenements.Size = new System.Drawing.Size(191, 394);
            this.m_panelEvenements.TabIndex = 0;
            // 
            // m_pageStructure
            // 
            this.m_pageStructure.Controls.Add(this.m_ctrlStructure);
            this.m_pageStructure.Location = new System.Drawing.Point(4, 22);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_pageStructure, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_pageStructure.Name = "m_pageStructure";
            this.m_pageStructure.Size = new System.Drawing.Size(197, 400);
            this.m_pageStructure.TabIndex = 2;
            this.m_pageStructure.Text = "Structure|20016";
            this.m_pageStructure.UseVisualStyleBackColor = true;
            // 
            // m_ctrlStructure
            // 
            this.m_ctrlStructure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_ctrlStructure.Editeur = null;
            this.m_ctrlStructure.Location = new System.Drawing.Point(0, 0);
            this.m_ctrlStructure.LockEdition = true;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_ctrlStructure, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_ctrlStructure.Name = "m_ctrlStructure";
            this.m_ctrlStructure.Size = new System.Drawing.Size(197, 400);
            this.m_ctrlStructure.TabIndex = 0;
            // 
            // m_panelFormulaire
            // 
            this.m_panelFormulaire.AllowDrop = true;
            this.m_panelFormulaire.AutoScroll = true;
            this.m_panelFormulaire.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.m_panelFormulaire.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelFormulaire.Echelle = 1F;
            this.m_panelFormulaire.EffetAjoutSuppression = false;
            this.m_panelFormulaire.EffetFonduMenu = true;
            this.m_panelFormulaire.EnDeplacement = false;
            this.m_panelFormulaire.EntiteEditee = null;
            this.m_panelFormulaire.FormesDesPoignees = sc2i.win32.common.EFormePoignee.Carre;
            this.m_panelFormulaire.FournisseurProprietes = null;
            cGrilleEditeurObjetGraphique1.Couleur = System.Drawing.Color.LightGray;
            cGrilleEditeurObjetGraphique1.HauteurCarreau = 20;
            cGrilleEditeurObjetGraphique1.LargeurCarreau = 20;
            cGrilleEditeurObjetGraphique1.Representation = sc2i.win32.common.ERepresentationGrille.LignesContinues;
            cGrilleEditeurObjetGraphique1.TailleCarreau = new System.Drawing.Size(20, 20);
            this.m_panelFormulaire.GrilleAlignement = cGrilleEditeurObjetGraphique1;
            this.m_panelFormulaire.HauteurMinimaleDesObjets = 10;
            this.m_panelFormulaire.HistorisationActive = true;
            this.m_panelFormulaire.LargeurMinimaleDesObjets = 10;
            this.m_panelFormulaire.Location = new System.Drawing.Point(165, 30);
            this.m_panelFormulaire.LockEdition = false;
            this.m_panelFormulaire.Marge = 10;
            this.m_panelFormulaire.ModeAffichageGrille = sc2i.win32.common.EModeAffichageGrille.AuDeplacement;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelFormulaire, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_panelFormulaire.ModeRepresentationGrille = sc2i.win32.common.ERepresentationGrille.LignesContinues;
            this.m_panelFormulaire.ModeSouris = sc2i.win32.common.CPanelEditionObjetGraphique.EModeSouris.Selection;
            this.m_panelFormulaire.Name = "m_panelFormulaire";
            this.m_panelFormulaire.NoClipboard = false;
            this.m_panelFormulaire.NoDelete = false;
            this.m_panelFormulaire.NoDoubleClick = false;
            this.m_panelFormulaire.NombreHistorisation = 10;
            this.m_panelFormulaire.NoMenu = false;
            cProfilEditeurObjetGraphique1.FormeDesPoignees = sc2i.win32.common.EFormePoignee.Carre;
            cProfilEditeurObjetGraphique1.Grille = cGrilleEditeurObjetGraphique1;
            cProfilEditeurObjetGraphique1.HistorisationActive = true;
            cProfilEditeurObjetGraphique1.Marge = 10;
            cProfilEditeurObjetGraphique1.ModeAffichageGrille = sc2i.win32.common.EModeAffichageGrille.AuDeplacement;
            cProfilEditeurObjetGraphique1.NombreHistorisation = 10;
            cProfilEditeurObjetGraphique1.ToujoursAlignerSurLaGrille = false;
            this.m_panelFormulaire.Profil = cProfilEditeurObjetGraphique1;
            this.m_panelFormulaire.RefreshSelectionChanged = true;
            this.m_panelFormulaire.SelectionVisible = true;
            this.m_panelFormulaire.Size = new System.Drawing.Size(366, 396);
            this.m_panelFormulaire.TabIndex = 4;
            this.m_panelFormulaire.ToujoursAlignerSelonLesControles = true;
            this.m_panelFormulaire.ToujoursAlignerSurLaGrille = false;
            this.m_panelFormulaire.TypeEdite = null;
            this.m_panelFormulaire.FrontBackChanged += new System.EventHandler(this.m_panelFormulaire_FrontBackChanged);
            this.m_panelFormulaire.SelectionChanged += new System.EventHandler(this.m_panelFormulaire_SelectionChanged);
            this.m_panelFormulaire.AfterAddElements += new sc2i.win32.common.EventHandlerPanelEditionGraphiqueSuppression(this.m_panelFormulaire_AfterAddElements);
            this.m_panelFormulaire.ElementMovedOrResized += new System.EventHandler(this.m_panelFormulaire_ElementMovedOrResized);
            this.m_panelFormulaire.AfterRemoveObjetGraphique += new System.EventHandler(this.m_panelFormulaire_AfterRemoveObjetGraphique);
            // 
            // m_wndListeControles
            // 
            this.m_wndListeControles.AutoScroll = true;
            this.m_wndListeControles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.m_wndListeControles.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_wndListeControles.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_wndListeControles, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_wndListeControles.Name = "m_wndListeControles";
            this.m_wndListeControles.Size = new System.Drawing.Size(162, 426);
            this.m_wndListeControles.TabIndex = 2;
            // 
            // m_panelToolBar
            // 
            this.m_panelToolBar.Controls.Add(this.m_btnSave);
            this.m_panelToolBar.Controls.Add(this.m_btnLoad);
            this.m_panelToolBar.Controls.Add(this.m_btnModeZoom);
            this.m_panelToolBar.Controls.Add(this.m_btnModeSelection);
            this.m_panelToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelToolBar.Location = new System.Drawing.Point(165, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelToolBar, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelToolBar.Name = "m_panelToolBar";
            this.m_panelToolBar.Size = new System.Drawing.Size(366, 30);
            this.m_panelToolBar.TabIndex = 1;
            // 
            // m_btnSave
            // 
            this.m_btnSave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnSave.Image = ((System.Drawing.Image)(resources.GetObject("m_btnSave.Image")));
            this.m_btnSave.Location = new System.Drawing.Point(310, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnSave, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnSave.Name = "m_btnSave";
            this.m_btnSave.Size = new System.Drawing.Size(28, 30);
            this.m_btnSave.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.m_btnSave.TabIndex = 3;
            this.m_btnSave.TabStop = false;
            this.m_btnSave.Click += new System.EventHandler(this.m_btnSave_Click);
            // 
            // m_btnLoad
            // 
            this.m_btnLoad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_btnLoad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnLoad.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnLoad.Image = ((System.Drawing.Image)(resources.GetObject("m_btnLoad.Image")));
            this.m_btnLoad.Location = new System.Drawing.Point(338, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnLoad, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnLoad.Name = "m_btnLoad";
            this.m_btnLoad.Size = new System.Drawing.Size(28, 30);
            this.m_btnLoad.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.m_btnLoad.TabIndex = 2;
            this.m_btnLoad.TabStop = false;
            this.m_btnLoad.Click += new System.EventHandler(this.m_btnLoad_Click);
            // 
            // m_btnModeZoom
            // 
            this.m_btnModeZoom.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_btnModeZoom.AutoSize = true;
            this.m_btnModeZoom.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_btnModeZoom.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_btnModeZoom.Image = ((System.Drawing.Image)(resources.GetObject("m_btnModeZoom.Image")));
            this.m_btnModeZoom.Location = new System.Drawing.Point(28, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnModeZoom, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnModeZoom.Name = "m_btnModeZoom";
            this.m_btnModeZoom.Size = new System.Drawing.Size(28, 30);
            this.m_btnModeZoom.TabIndex = 1;
            this.m_ToolTipTraductible1.SetToolTip(this.m_btnModeZoom, "Zoom|30002");
            this.m_btnModeZoom.UseVisualStyleBackColor = true;
            // 
            // m_btnModeSelection
            // 
            this.m_btnModeSelection.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_btnModeSelection.AutoSize = true;
            this.m_btnModeSelection.Checked = true;
            this.m_btnModeSelection.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_btnModeSelection.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_btnModeSelection.Image = ((System.Drawing.Image)(resources.GetObject("m_btnModeSelection.Image")));
            this.m_btnModeSelection.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnModeSelection, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnModeSelection.Name = "m_btnModeSelection";
            this.m_btnModeSelection.Size = new System.Drawing.Size(28, 30);
            this.m_btnModeSelection.TabIndex = 0;
            this.m_btnModeSelection.TabStop = true;
            this.m_ToolTipTraductible1.SetToolTip(this.m_btnModeSelection, "Selection|30001");
            this.m_btnModeSelection.UseVisualStyleBackColor = true;
            this.m_btnModeSelection.CheckedChanged += new System.EventHandler(this.m_btnModeSelection_CheckedChanged);
            // 
            // CPanelEditionFullFormulaire
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.m_panelFormulaire);
            this.Controls.Add(this.m_panelToolBar);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.m_wndListeControles);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.m_tabControl);
            this.m_gestionnaireModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CPanelEditionFullFormulaire";
            this.Size = new System.Drawing.Size(739, 426);
            this.Load += new System.EventHandler(this.CPanelEditionFullFormulaire_Load);
            this.m_tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.m_pageStructure.ResumeLayout(false);
            this.m_panelToolBar.ResumeLayout(false);
            this.m_panelToolBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnLoad)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PropertyGrid m_gridProprietes;
		private System.Windows.Forms.Splitter splitter1;
		private sc2i.formulaire.win32.editor.CPanelListe2iWnd m_wndListeControles;
		private System.Windows.Forms.Splitter splitter2;
		private sc2i.formulaire.win32.editor.CPanelEditionFormulaire m_panelFormulaire;
		private sc2i.win32.common.CExtModeEdition m_gestionnaireModeEdition;
		private System.Windows.Forms.TabControl m_tabControl;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private CControlEditionHandlersEvenements m_panelEvenements;
        private System.Windows.Forms.Panel m_panelToolBar;
        private System.Windows.Forms.RadioButton m_btnModeZoom;
        private System.Windows.Forms.RadioButton m_btnModeSelection;
        private sc2i.win32.common.CToolTipTraductible m_ToolTipTraductible1;
        private System.Windows.Forms.TabPage m_pageStructure;
        private CControleStructureFormulaire m_ctrlStructure;
        private System.Windows.Forms.PictureBox m_btnSave;
        private System.Windows.Forms.PictureBox m_btnLoad;
	}
}
