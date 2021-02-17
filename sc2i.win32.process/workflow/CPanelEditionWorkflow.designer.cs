namespace sc2i.win32.process.workflow
{
	partial class CPanelEditionWorkflow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPanelEditionWorkflow));
            sc2i.win32.common.CGrilleEditeurObjetGraphique cGrilleEditeurObjetGraphique1 = new sc2i.win32.common.CGrilleEditeurObjetGraphique();
            sc2i.win32.common.CProfilEditeurObjetGraphique cProfilEditeurObjetGraphique1 = new sc2i.win32.common.CProfilEditeurObjetGraphique();
            this.m_gridProprietes = new System.Windows.Forms.PropertyGrid();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.m_gestionnaireModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.m_panelInfoSelection = new System.Windows.Forms.Panel();
            this.m_lblElementSelectionne = new System.Windows.Forms.Label();
            this.m_imageElementSelectionne = new System.Windows.Forms.PictureBox();
            this.m_btnDetailEtape = new System.Windows.Forms.Button();
            this.m_pageAffectations = new System.Windows.Forms.TabPage();
            this.m_wndListeAffectations = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.m_imagesAffectations = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_lnkDeleteModeleAffectation = new sc2i.win32.common.CWndLinkStd();
            this.m_lnkEditModeleAffectation = new sc2i.win32.common.CWndLinkStd();
            this.m_lnkAddModeleAffectation = new sc2i.win32.common.CWndLinkStd();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_txtFiltreAffectations = new sc2i.win32.common.C2iTextBox();
            this.m_btnSearchAffectation = new System.Windows.Forms.PictureBox();
            this.m_panelToolBar = new System.Windows.Forms.Panel();
            this.m_chkLien = new System.Windows.Forms.RadioButton();
            this.m_btnModeZoom = new System.Windows.Forms.RadioButton();
            this.m_btnModeSelection = new System.Windows.Forms.RadioButton();
            this.m_panelObjets = new sc2i.win32.common.C2iPanel(this.components);
            this.m_btnBlocs = new System.Windows.Forms.Button();
            this.m_menuBlocs = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_panelUndrawns = new System.Windows.Forms.Panel();
            this.m_wndListeUndrawn = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.m_panelBasEditeur = new System.Windows.Forms.Panel();
            this.m_trackZoom = new System.Windows.Forms.TrackBar();
            this.m_lblZoom = new System.Windows.Forms.Label();
            this.m_panelWorkflow = new sc2i.win32.process.workflow.CControlEditeWorkflow();
            this.m_ToolTipTraductible1 = new sc2i.win32.common.CToolTipTraductible(this.components);
            this.m_tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.m_panelInfoSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_imageElementSelectionne)).BeginInit();
            this.m_pageAffectations.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnSearchAffectation)).BeginInit();
            this.m_panelToolBar.SuspendLayout();
            this.m_panelObjets.SuspendLayout();
            this.m_panelUndrawns.SuspendLayout();
            this.m_panelBasEditeur.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_trackZoom)).BeginInit();
            this.SuspendLayout();
            // 
            // m_gridProprietes
            // 
            this.m_gridProprietes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridProprietes.Location = new System.Drawing.Point(3, 37);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_gridProprietes, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_gridProprietes.Name = "m_gridProprietes";
            this.m_gridProprietes.Size = new System.Drawing.Size(191, 360);
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
            this.splitter2.Location = new System.Drawing.Point(203, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.splitter2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 426);
            this.splitter2.TabIndex = 3;
            this.splitter2.TabStop = false;
            // 
            // m_tabControl
            // 
            this.m_tabControl.Controls.Add(this.tabPage1);
            this.m_tabControl.Controls.Add(this.m_pageAffectations);
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
            this.tabPage1.Controls.Add(this.m_panelInfoSelection);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.m_gestionnaireModeEdition.SetModeEdition(this.tabPage1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(197, 400);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Properties|198";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // m_panelInfoSelection
            // 
            this.m_panelInfoSelection.Controls.Add(this.m_lblElementSelectionne);
            this.m_panelInfoSelection.Controls.Add(this.m_imageElementSelectionne);
            this.m_panelInfoSelection.Controls.Add(this.m_btnDetailEtape);
            this.m_panelInfoSelection.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelInfoSelection.Location = new System.Drawing.Point(3, 3);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelInfoSelection, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelInfoSelection.Name = "m_panelInfoSelection";
            this.m_panelInfoSelection.Size = new System.Drawing.Size(191, 34);
            this.m_panelInfoSelection.TabIndex = 1;
            // 
            // m_lblElementSelectionne
            // 
            this.m_lblElementSelectionne.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblElementSelectionne.Location = new System.Drawing.Point(38, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lblElementSelectionne, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblElementSelectionne.Name = "m_lblElementSelectionne";
            this.m_lblElementSelectionne.Size = new System.Drawing.Size(115, 34);
            this.m_lblElementSelectionne.TabIndex = 1;
            // 
            // m_imageElementSelectionne
            // 
            this.m_imageElementSelectionne.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_imageElementSelectionne.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_imageElementSelectionne, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_imageElementSelectionne.Name = "m_imageElementSelectionne";
            this.m_imageElementSelectionne.Size = new System.Drawing.Size(38, 34);
            this.m_imageElementSelectionne.TabIndex = 0;
            this.m_imageElementSelectionne.TabStop = false;
            // 
            // m_btnDetailEtape
            // 
            this.m_btnDetailEtape.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnDetailEtape.Image = global::sc2i.win32.process.Properties.Resources._32px_Crystal_Clear_app_kedit;
            this.m_btnDetailEtape.Location = new System.Drawing.Point(153, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnDetailEtape, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnDetailEtape.Name = "m_btnDetailEtape";
            this.m_btnDetailEtape.Size = new System.Drawing.Size(38, 34);
            this.m_btnDetailEtape.TabIndex = 2;
            this.m_btnDetailEtape.UseVisualStyleBackColor = true;
            this.m_btnDetailEtape.Click += new System.EventHandler(this.m_btnDetailEtape_Click);
            // 
            // m_pageAffectations
            // 
            this.m_pageAffectations.Controls.Add(this.m_wndListeAffectations);
            this.m_pageAffectations.Controls.Add(this.panel2);
            this.m_pageAffectations.Controls.Add(this.panel1);
            this.m_pageAffectations.Location = new System.Drawing.Point(4, 22);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_pageAffectations, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_pageAffectations.Name = "m_pageAffectations";
            this.m_pageAffectations.Size = new System.Drawing.Size(197, 400);
            this.m_pageAffectations.TabIndex = 1;
            this.m_pageAffectations.Text = "Assignments|20080";
            this.m_pageAffectations.UseVisualStyleBackColor = true;
            // 
            // m_wndListeAffectations
            // 
            this.m_wndListeAffectations.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.m_wndListeAffectations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeAffectations.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_wndListeAffectations.Location = new System.Drawing.Point(0, 46);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_wndListeAffectations, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_wndListeAffectations.MultiSelect = false;
            this.m_wndListeAffectations.Name = "m_wndListeAffectations";
            this.m_wndListeAffectations.Size = new System.Drawing.Size(197, 354);
            this.m_wndListeAffectations.SmallImageList = this.m_imagesAffectations;
            this.m_wndListeAffectations.StateImageList = this.m_imagesAffectations;
            this.m_wndListeAffectations.TabIndex = 1;
            this.m_wndListeAffectations.UseCompatibleStateImageBehavior = false;
            this.m_wndListeAffectations.View = System.Windows.Forms.View.Details;
            this.m_wndListeAffectations.Resize += new System.EventHandler(this.m_wndListeAffectations_Resize);
            this.m_wndListeAffectations.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.m_wndListeAffectations_ItemDrag);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 171;
            // 
            // m_imagesAffectations
            // 
            this.m_imagesAffectations.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imagesAffectations.ImageStream")));
            this.m_imagesAffectations.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imagesAffectations.Images.SetKeyName(0, "1346459174_group.png");
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_lnkDeleteModeleAffectation);
            this.panel2.Controls.Add(this.m_lnkEditModeleAffectation);
            this.panel2.Controls.Add(this.m_lnkAddModeleAffectation);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 20);
            this.m_gestionnaireModeEdition.SetModeEdition(this.panel2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(197, 26);
            this.panel2.TabIndex = 2;
            // 
            // m_lnkDeleteModeleAffectation
            // 
            this.m_lnkDeleteModeleAffectation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkDeleteModeleAffectation.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkDeleteModeleAffectation.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkDeleteModeleAffectation.Location = new System.Drawing.Point(62, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkDeleteModeleAffectation, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lnkDeleteModeleAffectation.Name = "m_lnkDeleteModeleAffectation";
            this.m_lnkDeleteModeleAffectation.Size = new System.Drawing.Size(31, 26);
            this.m_lnkDeleteModeleAffectation.TabIndex = 2;
            this.m_lnkDeleteModeleAffectation.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_lnkDeleteModeleAffectation.LinkClicked += new System.EventHandler(this.m_lnkDeleteModeleAffectation_LinkClicked);
            // 
            // m_lnkEditModeleAffectation
            // 
            this.m_lnkEditModeleAffectation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkEditModeleAffectation.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkEditModeleAffectation.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkEditModeleAffectation.Location = new System.Drawing.Point(31, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkEditModeleAffectation, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lnkEditModeleAffectation.Name = "m_lnkEditModeleAffectation";
            this.m_lnkEditModeleAffectation.Size = new System.Drawing.Size(31, 26);
            this.m_lnkEditModeleAffectation.TabIndex = 1;
            this.m_lnkEditModeleAffectation.TypeLien = sc2i.win32.common.TypeLinkStd.Modification;
            this.m_lnkEditModeleAffectation.LinkClicked += new System.EventHandler(this.m_lnkEditModeleAffectation_LinkClicked);
            // 
            // m_lnkAddModeleAffectation
            // 
            this.m_lnkAddModeleAffectation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkAddModeleAffectation.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkAddModeleAffectation.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkAddModeleAffectation.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkAddModeleAffectation, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lnkAddModeleAffectation.Name = "m_lnkAddModeleAffectation";
            this.m_lnkAddModeleAffectation.Size = new System.Drawing.Size(31, 26);
            this.m_lnkAddModeleAffectation.TabIndex = 0;
            this.m_lnkAddModeleAffectation.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_lnkAddModeleAffectation.LinkClicked += new System.EventHandler(this.m_lnkAddModeleAffectation_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_txtFiltreAffectations);
            this.panel1.Controls.Add(this.m_btnSearchAffectation);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.panel1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(197, 20);
            this.panel1.TabIndex = 0;
            // 
            // m_txtFiltreAffectations
            // 
            this.m_txtFiltreAffectations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtFiltreAffectations.EmptyText = "";
            this.m_txtFiltreAffectations.Location = new System.Drawing.Point(0, 0);
            this.m_txtFiltreAffectations.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_txtFiltreAffectations, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_txtFiltreAffectations.Name = "m_txtFiltreAffectations";
            this.m_txtFiltreAffectations.Size = new System.Drawing.Size(171, 20);
            this.m_txtFiltreAffectations.TabIndex = 0;
            // 
            // m_btnSearchAffectation
            // 
            this.m_btnSearchAffectation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnSearchAffectation.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnSearchAffectation.Image = global::sc2i.win32.process.Properties.Resources.loupe;
            this.m_btnSearchAffectation.Location = new System.Drawing.Point(171, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnSearchAffectation, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnSearchAffectation.Name = "m_btnSearchAffectation";
            this.m_btnSearchAffectation.Size = new System.Drawing.Size(26, 20);
            this.m_btnSearchAffectation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.m_btnSearchAffectation.TabIndex = 1;
            this.m_btnSearchAffectation.TabStop = false;
            this.m_btnSearchAffectation.Click += new System.EventHandler(this.m_btnSearchAffectation_Click);
            // 
            // m_panelToolBar
            // 
            this.m_panelToolBar.Controls.Add(this.m_chkLien);
            this.m_panelToolBar.Controls.Add(this.m_btnModeZoom);
            this.m_panelToolBar.Controls.Add(this.m_btnModeSelection);
            this.m_panelToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelToolBar.Location = new System.Drawing.Point(206, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelToolBar, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelToolBar.Name = "m_panelToolBar";
            this.m_panelToolBar.Size = new System.Drawing.Size(325, 30);
            this.m_panelToolBar.TabIndex = 1;
            // 
            // m_chkLien
            // 
            this.m_chkLien.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_chkLien.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_chkLien.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.m_chkLien.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_chkLien.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.m_chkLien.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.m_chkLien.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_chkLien.Image = global::sc2i.win32.process.Properties.Resources.Link;
            this.m_chkLien.Location = new System.Drawing.Point(56, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_chkLien, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_chkLien.Name = "m_chkLien";
            this.m_chkLien.Size = new System.Drawing.Size(32, 30);
            this.m_chkLien.TabIndex = 5;
            this.m_chkLien.CheckedChanged += new System.EventHandler(this.m_chkLien_CheckedChanged);
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
            this.m_btnModeZoom.CheckedChanged += new System.EventHandler(this.m_btnModeZoom_CheckedChanged);
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
            // m_panelObjets
            // 
            this.m_panelObjets.Controls.Add(this.m_btnBlocs);
            this.m_panelObjets.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_panelObjets.Location = new System.Drawing.Point(206, 30);
            this.m_panelObjets.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelObjets, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_panelObjets.Name = "m_panelObjets";
            this.m_panelObjets.Size = new System.Drawing.Size(43, 396);
            this.m_panelObjets.TabIndex = 6;
            // 
            // m_btnBlocs
            // 
            this.m_btnBlocs.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_btnBlocs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnBlocs.Image = global::sc2i.win32.process.Properties.Resources.icones_workflow;
            this.m_btnBlocs.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnBlocs, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnBlocs.Name = "m_btnBlocs";
            this.m_btnBlocs.Size = new System.Drawing.Size(43, 43);
            this.m_btnBlocs.TabIndex = 7;
            this.m_btnBlocs.UseVisualStyleBackColor = true;
            this.m_btnBlocs.Click += new System.EventHandler(this.m_btnBlocs_Click);
            // 
            // m_menuBlocs
            // 
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_menuBlocs, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_menuBlocs.Name = "m_menuBlocs";
            this.m_menuBlocs.Size = new System.Drawing.Size(61, 4);
            this.m_menuBlocs.Opening += new System.ComponentModel.CancelEventHandler(this.m_menuBlocs_Opening);
            // 
            // m_panelUndrawns
            // 
            this.m_panelUndrawns.Controls.Add(this.m_wndListeUndrawn);
            this.m_panelUndrawns.Controls.Add(this.label1);
            this.m_panelUndrawns.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_panelUndrawns.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelUndrawns, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelUndrawns.Name = "m_panelUndrawns";
            this.m_panelUndrawns.Size = new System.Drawing.Size(200, 426);
            this.m_panelUndrawns.TabIndex = 1;
            // 
            // m_wndListeUndrawn
            // 
            this.m_wndListeUndrawn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeUndrawn.Location = new System.Drawing.Point(0, 23);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_wndListeUndrawn, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_wndListeUndrawn.Name = "m_wndListeUndrawn";
            this.m_wndListeUndrawn.Size = new System.Drawing.Size(200, 403);
            this.m_wndListeUndrawn.TabIndex = 1;
            this.m_wndListeUndrawn.UseCompatibleStateImageBehavior = false;
            this.m_wndListeUndrawn.View = System.Windows.Forms.View.List;
            this.m_wndListeUndrawn.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.m_wndListeUndrawn_ItemDrag);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Undrawn elements|20045";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitter3
            // 
            this.splitter3.Location = new System.Drawing.Point(200, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.splitter3, sc2i.win32.common.TypeModeEdition.Autonome);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(3, 426);
            this.splitter3.TabIndex = 8;
            this.splitter3.TabStop = false;
            // 
            // m_panelBasEditeur
            // 
            this.m_panelBasEditeur.Controls.Add(this.m_trackZoom);
            this.m_panelBasEditeur.Controls.Add(this.m_lblZoom);
            this.m_panelBasEditeur.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_panelBasEditeur.Location = new System.Drawing.Point(249, 397);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelBasEditeur, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelBasEditeur.Name = "m_panelBasEditeur";
            this.m_panelBasEditeur.Size = new System.Drawing.Size(282, 29);
            this.m_panelBasEditeur.TabIndex = 2;
            // 
            // m_trackZoom
            // 
            this.m_trackZoom.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_trackZoom.Location = new System.Drawing.Point(149, 0);
            this.m_trackZoom.Maximum = 30;
            this.m_trackZoom.Minimum = 1;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_trackZoom, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_trackZoom.Name = "m_trackZoom";
            this.m_trackZoom.Size = new System.Drawing.Size(104, 29);
            this.m_trackZoom.TabIndex = 0;
            this.m_trackZoom.Value = 1;
            this.m_trackZoom.ValueChanged += new System.EventHandler(this.m_trackZoom_ValueChanged);
            this.m_trackZoom.Scroll += new System.EventHandler(this.m_trackZoom_Scroll);
            // 
            // m_lblZoom
            // 
            this.m_lblZoom.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_lblZoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblZoom.Location = new System.Drawing.Point(253, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lblZoom, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblZoom.Name = "m_lblZoom";
            this.m_lblZoom.Size = new System.Drawing.Size(29, 29);
            this.m_lblZoom.TabIndex = 1;
            this.m_lblZoom.Text = "x1";
            this.m_lblZoom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_panelWorkflow
            // 
            this.m_panelWorkflow.AllowDrop = true;
            this.m_panelWorkflow.AutoScroll = true;
            this.m_panelWorkflow.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.m_panelWorkflow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelWorkflow.Echelle = 1F;
            this.m_panelWorkflow.EffetAjoutSuppression = false;
            this.m_panelWorkflow.EffetFonduMenu = true;
            this.m_panelWorkflow.EnDeplacement = false;
            this.m_panelWorkflow.FormesDesPoignees = sc2i.win32.common.EFormePoignee.Carre;
            cGrilleEditeurObjetGraphique1.Couleur = System.Drawing.Color.LightGray;
            cGrilleEditeurObjetGraphique1.HauteurCarreau = 20;
            cGrilleEditeurObjetGraphique1.LargeurCarreau = 20;
            cGrilleEditeurObjetGraphique1.Representation = sc2i.win32.common.ERepresentationGrille.LignesContinues;
            cGrilleEditeurObjetGraphique1.TailleCarreau = new System.Drawing.Size(20, 20);
            this.m_panelWorkflow.GrilleAlignement = cGrilleEditeurObjetGraphique1;
            this.m_panelWorkflow.HauteurMinimaleDesObjets = 10;
            this.m_panelWorkflow.HistorisationActive = true;
            this.m_panelWorkflow.LargeurMinimaleDesObjets = 10;
            this.m_panelWorkflow.Location = new System.Drawing.Point(249, 30);
            this.m_panelWorkflow.LockEdition = false;
            this.m_panelWorkflow.Marge = 10;
            this.m_panelWorkflow.MaxZoom = 6F;
            this.m_panelWorkflow.MinZoom = 0.2F;
            this.m_panelWorkflow.ModeAffichageGrille = sc2i.win32.common.EModeAffichageGrille.AuDeplacement;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelWorkflow, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_panelWorkflow.ModeRepresentationGrille = sc2i.win32.common.ERepresentationGrille.LignesContinues;
            this.m_panelWorkflow.ModeSouris = sc2i.win32.common.CPanelEditionObjetGraphique.EModeSouris.Selection;
            this.m_panelWorkflow.ModeSourisCustom = sc2i.win32.process.workflow.CControlEditeWorkflow.EModeSourisCustom.LienWorkflow;
            this.m_panelWorkflow.Name = "m_panelWorkflow";
            this.m_panelWorkflow.NoClipboard = false;
            this.m_panelWorkflow.NoDelete = false;
            this.m_panelWorkflow.NoDoubleClick = false;
            this.m_panelWorkflow.NombreHistorisation = 10;
            this.m_panelWorkflow.NoMenu = false;
            this.m_panelWorkflow.ObjetEdite = null;
            cProfilEditeurObjetGraphique1.FormeDesPoignees = sc2i.win32.common.EFormePoignee.Carre;
            cProfilEditeurObjetGraphique1.Grille = cGrilleEditeurObjetGraphique1;
            cProfilEditeurObjetGraphique1.HistorisationActive = true;
            cProfilEditeurObjetGraphique1.Marge = 10;
            cProfilEditeurObjetGraphique1.ModeAffichageGrille = sc2i.win32.common.EModeAffichageGrille.AuDeplacement;
            cProfilEditeurObjetGraphique1.NombreHistorisation = 10;
            cProfilEditeurObjetGraphique1.ToujoursAlignerSurLaGrille = false;
            this.m_panelWorkflow.Profil = cProfilEditeurObjetGraphique1;
            this.m_panelWorkflow.RefreshSelectionChanged = true;
            this.m_panelWorkflow.SelectionVisible = true;
            this.m_panelWorkflow.Size = new System.Drawing.Size(282, 367);
            this.m_panelWorkflow.TabIndex = 4;
            this.m_panelWorkflow.ToujoursAlignerSelonLesControles = true;
            this.m_panelWorkflow.ToujoursAlignerSurLaGrille = false;
            this.m_panelWorkflow.Load += new System.EventHandler(this.m_panelWorkflow_Load);
            this.m_panelWorkflow.FrontBackChanged += new System.EventHandler(this.m_panelFormulaire_FrontBackChanged);
            this.m_panelWorkflow.EchelleChanged += new System.EventHandler(this.m_panelWorkflow_EchelleChanged);
            this.m_panelWorkflow.SelectionChanged += new System.EventHandler(this.m_panelFormulaire_SelectionChanged);
            this.m_panelWorkflow.DoubleClicSurElement += new System.EventHandler(this.m_panelWorkflow_DoubleClicSurElement);
            this.m_panelWorkflow.ElementMovedOrResized += new System.EventHandler(this.m_panelFormulaire_ElementMovedOrResized);
            this.m_panelWorkflow.AfterRemoveObjetGraphique += new System.EventHandler(this.m_panelFormulaire_AfterRemoveObjetGraphique);
            // 
            // CPanelEditionWorkflow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.m_panelWorkflow);
            this.Controls.Add(this.m_panelBasEditeur);
            this.Controls.Add(this.m_panelObjets);
            this.Controls.Add(this.m_panelToolBar);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.m_tabControl);
            this.Controls.Add(this.splitter3);
            this.Controls.Add(this.m_panelUndrawns);
            this.m_gestionnaireModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CPanelEditionWorkflow";
            this.Size = new System.Drawing.Size(739, 426);
            this.Load += new System.EventHandler(this.CPanelEditionWorkflow_Load);
            this.m_tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.m_panelInfoSelection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_imageElementSelectionne)).EndInit();
            this.m_pageAffectations.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnSearchAffectation)).EndInit();
            this.m_panelToolBar.ResumeLayout(false);
            this.m_panelToolBar.PerformLayout();
            this.m_panelObjets.ResumeLayout(false);
            this.m_panelUndrawns.ResumeLayout(false);
            this.m_panelBasEditeur.ResumeLayout(false);
            this.m_panelBasEditeur.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_trackZoom)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PropertyGrid m_gridProprietes;
        private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Splitter splitter2;
		private sc2i.win32.process.workflow.CControlEditeWorkflow m_panelWorkflow;
		private sc2i.win32.common.CExtModeEdition m_gestionnaireModeEdition;
		private System.Windows.Forms.TabControl m_tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel m_panelToolBar;
        private System.Windows.Forms.RadioButton m_btnModeZoom;
        private System.Windows.Forms.RadioButton m_btnModeSelection;
        private sc2i.win32.common.CToolTipTraductible m_ToolTipTraductible1;
        private System.Windows.Forms.RadioButton m_chkLien;
        private sc2i.win32.common.C2iPanel m_panelObjets;
        private System.Windows.Forms.Button m_btnBlocs;
        private System.Windows.Forms.ContextMenuStrip m_menuBlocs;
        private System.Windows.Forms.Panel m_panelUndrawns;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel m_panelInfoSelection;
        private System.Windows.Forms.Label m_lblElementSelectionne;
        private System.Windows.Forms.PictureBox m_imageElementSelectionne;
        private System.Windows.Forms.ListView m_wndListeUndrawn;
        private System.Windows.Forms.Panel m_panelBasEditeur;
        private System.Windows.Forms.TrackBar m_trackZoom;
        private System.Windows.Forms.Label m_lblZoom;
        private System.Windows.Forms.Button m_btnDetailEtape;
        private System.Windows.Forms.TabPage m_pageAffectations;
        private System.Windows.Forms.ListView m_wndListeAffectations;
        private System.Windows.Forms.Panel panel1;
        private sc2i.win32.common.C2iTextBox m_txtFiltreAffectations;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ImageList m_imagesAffectations;
        private System.Windows.Forms.PictureBox m_btnSearchAffectation;
        private System.Windows.Forms.Panel panel2;
        private sc2i.win32.common.CWndLinkStd m_lnkDeleteModeleAffectation;
        private sc2i.win32.common.CWndLinkStd m_lnkEditModeleAffectation;
        private sc2i.win32.common.CWndLinkStd m_lnkAddModeleAffectation;
	}
}
