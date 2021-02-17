namespace sc2i.win32.expression
{
    partial class CEditeurExpressionGraphique
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CEditeurExpressionGraphique));
            sc2i.win32.common.CGrilleEditeurObjetGraphique cGrilleEditeurObjetGraphique1 = new sc2i.win32.common.CGrilleEditeurObjetGraphique();
            sc2i.win32.common.CProfilEditeurObjetGraphique cProfilEditeurObjetGraphique1 = new sc2i.win32.common.CProfilEditeurObjetGraphique();
            this.m_btnLink = new System.Windows.Forms.Panel();
            this.m_btnLienParametre = new System.Windows.Forms.RadioButton();
            this.m_chkLien = new System.Windows.Forms.RadioButton();
            this.m_chkZoom = new System.Windows.Forms.RadioButton();
            this.m_btnSelect = new System.Windows.Forms.RadioButton();
            this.m_arbreFormules = new System.Windows.Forms.TreeView();
            this.m_panelDetailFormule = new System.Windows.Forms.Panel();
            this.m_editeurParametresStandard = new sc2i.win32.expression.CEditeurParametresFormule();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.m_panelVariables = new System.Windows.Forms.Panel();
            this.m_wndListeVariables = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_lnkRemoveVar = new sc2i.win32.common.CWndLinkStd();
            this.m_lnkAddVar = new sc2i.win32.common.CWndLinkStd();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_chkActivateDebug = new System.Windows.Forms.CheckBox();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.m_panelSaveLoadEtc = new System.Windows.Forms.Panel();
            this.m_dragSetVar = new sc2i.win32.common.CPictureBoxDraggable();
            this.m_dragConstante = new sc2i.win32.common.CPictureBoxDraggable();
            this.m_btnDragIf = new sc2i.win32.common.CPictureBoxDraggable();
            this.m_DragAddNew = new sc2i.win32.common.CPictureBoxDraggable();
            this.m_btnCopy = new System.Windows.Forms.Button();
            this.m_btnPaste = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.m_btnSave = new System.Windows.Forms.Button();
            this.m_btnLoad = new System.Windows.Forms.Button();
            this.m_menuAddFormule = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_tooltip = new sc2i.win32.common.CToolTipTraductible(this.components);
            this.m_editeur = new sc2i.win32.expression.CPanelEditionRepresentationExpressionGraphique();
            this.m_timerSelection = new System.Windows.Forms.Timer(this.components);
            this.m_btnLink.SuspendLayout();
            this.m_panelDetailFormule.SuspendLayout();
            this.m_panelVariables.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.m_panelSaveLoadEtc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dragSetVar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dragConstante)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnDragIf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_DragAddNew)).BeginInit();
            this.SuspendLayout();
            // 
            // m_btnLink
            // 
            this.m_btnLink.Controls.Add(this.m_btnLienParametre);
            this.m_btnLink.Controls.Add(this.m_chkLien);
            this.m_btnLink.Controls.Add(this.m_chkZoom);
            this.m_btnLink.Controls.Add(this.m_btnSelect);
            this.m_btnLink.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_btnLink.Location = new System.Drawing.Point(231, 0);
            this.m_btnLink.Name = "m_btnLink";
            this.m_btnLink.Size = new System.Drawing.Size(534, 32);
            this.m_btnLink.TabIndex = 1;
            // 
            // m_btnLienParametre
            // 
            this.m_btnLienParametre.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_btnLienParametre.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_btnLienParametre.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.m_btnLienParametre.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_btnLienParametre.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.m_btnLienParametre.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.m_btnLienParametre.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_btnLienParametre.Image = global::sc2i.win32.expression.Resource1.Source;
            this.m_btnLienParametre.Location = new System.Drawing.Point(99, 0);
            this.m_btnLienParametre.Name = "m_btnLienParametre";
            this.m_btnLienParametre.Size = new System.Drawing.Size(32, 32);
            this.m_btnLienParametre.TabIndex = 6;
            this.m_btnLienParametre.CheckedChanged += new System.EventHandler(this.m_btnLienParametre_CheckedChanged);
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
            this.m_chkLien.Image = global::sc2i.win32.expression.Resource1.Sequence;
            this.m_chkLien.Location = new System.Drawing.Point(67, 0);
            this.m_chkLien.Name = "m_chkLien";
            this.m_chkLien.Size = new System.Drawing.Size(32, 32);
            this.m_chkLien.TabIndex = 4;
            this.m_chkLien.CheckedChanged += new System.EventHandler(this.m_chkLien_CheckedChanged);
            // 
            // m_chkZoom
            // 
            this.m_chkZoom.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_chkZoom.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_chkZoom.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.m_chkZoom.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_chkZoom.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.m_chkZoom.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.m_chkZoom.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_chkZoom.Image = ((System.Drawing.Image)(resources.GetObject("m_chkZoom.Image")));
            this.m_chkZoom.Location = new System.Drawing.Point(35, 0);
            this.m_chkZoom.Name = "m_chkZoom";
            this.m_chkZoom.Size = new System.Drawing.Size(32, 32);
            this.m_chkZoom.TabIndex = 5;
            this.m_chkZoom.CheckedChanged += new System.EventHandler(this.m_chkZoom_CheckedChanged);
            // 
            // m_btnSelect
            // 
            this.m_btnSelect.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_btnSelect.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnSelect.Image = ((System.Drawing.Image)(resources.GetObject("m_btnSelect.Image")));
            this.m_btnSelect.Location = new System.Drawing.Point(0, 0);
            this.m_btnSelect.Name = "m_btnSelect";
            this.m_btnSelect.Size = new System.Drawing.Size(35, 32);
            this.m_btnSelect.TabIndex = 0;
            this.m_btnSelect.TabStop = true;
            this.m_btnSelect.UseVisualStyleBackColor = true;
            this.m_btnSelect.CheckedChanged += new System.EventHandler(this.m_btnSelect_CheckedChanged);
            // 
            // m_arbreFormules
            // 
            this.m_arbreFormules.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_arbreFormules.Location = new System.Drawing.Point(0, 0);
            this.m_arbreFormules.Name = "m_arbreFormules";
            this.m_arbreFormules.Size = new System.Drawing.Size(231, 470);
            this.m_arbreFormules.TabIndex = 1;
            this.m_arbreFormules.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.m_arbreFormules_ItemDrag);
            // 
            // m_panelDetailFormule
            // 
            this.m_panelDetailFormule.Controls.Add(this.m_editeurParametresStandard);
            this.m_panelDetailFormule.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_panelDetailFormule.Location = new System.Drawing.Point(231, 270);
            this.m_panelDetailFormule.Name = "m_panelDetailFormule";
            this.m_panelDetailFormule.Size = new System.Drawing.Size(534, 200);
            this.m_panelDetailFormule.TabIndex = 2;
            this.m_panelDetailFormule.Visible = false;
            // 
            // m_editeurParametresStandard
            // 
            this.m_editeurParametresStandard.AutoScroll = true;
            this.m_editeurParametresStandard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_editeurParametresStandard.Location = new System.Drawing.Point(0, 0);
            this.m_editeurParametresStandard.Name = "m_editeurParametresStandard";
            this.m_editeurParametresStandard.Size = new System.Drawing.Size(534, 200);
            this.m_editeurParametresStandard.TabIndex = 0;
            this.m_editeurParametresStandard.OnChangeDessin += new System.EventHandler(this.m_editeurParametres_OnChangeDessin);
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.Black;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(231, 267);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(534, 3);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // m_panelVariables
            // 
            this.m_panelVariables.Controls.Add(this.m_wndListeVariables);
            this.m_panelVariables.Controls.Add(this.panel1);
            this.m_panelVariables.Controls.Add(this.panel2);
            this.m_panelVariables.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_panelVariables.Location = new System.Drawing.Point(588, 32);
            this.m_panelVariables.Name = "m_panelVariables";
            this.m_panelVariables.Size = new System.Drawing.Size(177, 235);
            this.m_panelVariables.TabIndex = 1;
            // 
            // m_wndListeVariables
            // 
            this.m_wndListeVariables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.m_wndListeVariables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeVariables.Location = new System.Drawing.Point(0, 21);
            this.m_wndListeVariables.MultiSelect = false;
            this.m_wndListeVariables.Name = "m_wndListeVariables";
            this.m_wndListeVariables.Size = new System.Drawing.Size(177, 191);
            this.m_wndListeVariables.TabIndex = 0;
            this.m_wndListeVariables.UseCompatibleStateImageBehavior = false;
            this.m_wndListeVariables.View = System.Windows.Forms.View.Details;
            this.m_wndListeVariables.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.m_wndListeVariables_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Variables";
            this.columnHeader1.Width = 154;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_lnkRemoveVar);
            this.panel1.Controls.Add(this.m_lnkAddVar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(177, 21);
            this.panel1.TabIndex = 2;
            // 
            // m_lnkRemoveVar
            // 
            this.m_lnkRemoveVar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkRemoveVar.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkRemoveVar.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkRemoveVar.Location = new System.Drawing.Point(87, 0);
            this.m_lnkRemoveVar.Name = "m_lnkRemoveVar";
            this.m_lnkRemoveVar.Size = new System.Drawing.Size(87, 21);
            this.m_lnkRemoveVar.TabIndex = 2;
            this.m_lnkRemoveVar.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_lnkRemoveVar.LinkClicked += new System.EventHandler(this.m_lnkRemoveVar_LinkClicked);
            // 
            // m_lnkAddVar
            // 
            this.m_lnkAddVar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkAddVar.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkAddVar.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkAddVar.Location = new System.Drawing.Point(0, 0);
            this.m_lnkAddVar.Name = "m_lnkAddVar";
            this.m_lnkAddVar.Size = new System.Drawing.Size(87, 21);
            this.m_lnkAddVar.TabIndex = 1;
            this.m_lnkAddVar.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_lnkAddVar.LinkClicked += new System.EventHandler(this.m_lnkAddVar_LinkClicked);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_chkActivateDebug);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 212);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(177, 23);
            this.panel2.TabIndex = 3;
            // 
            // m_chkActivateDebug
            // 
            this.m_chkActivateDebug.AutoSize = true;
            this.m_chkActivateDebug.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_chkActivateDebug.Location = new System.Drawing.Point(0, 0);
            this.m_chkActivateDebug.Name = "m_chkActivateDebug";
            this.m_chkActivateDebug.Size = new System.Drawing.Size(150, 23);
            this.m_chkActivateDebug.TabIndex = 0;
            this.m_chkActivateDebug.Text = "Activate debugging|20014";
            this.m_chkActivateDebug.UseVisualStyleBackColor = true;
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.Color.Black;
            this.splitter2.Location = new System.Drawing.Point(231, 32);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 235);
            this.splitter2.TabIndex = 4;
            this.splitter2.TabStop = false;
            // 
            // splitter3
            // 
            this.splitter3.BackColor = System.Drawing.Color.Black;
            this.splitter3.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter3.Location = new System.Drawing.Point(585, 32);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(3, 235);
            this.splitter3.TabIndex = 5;
            this.splitter3.TabStop = false;
            // 
            // m_panelSaveLoadEtc
            // 
            this.m_panelSaveLoadEtc.Controls.Add(this.m_dragSetVar);
            this.m_panelSaveLoadEtc.Controls.Add(this.m_dragConstante);
            this.m_panelSaveLoadEtc.Controls.Add(this.m_btnDragIf);
            this.m_panelSaveLoadEtc.Controls.Add(this.m_DragAddNew);
            this.m_panelSaveLoadEtc.Controls.Add(this.m_btnCopy);
            this.m_panelSaveLoadEtc.Controls.Add(this.m_btnPaste);
            this.m_panelSaveLoadEtc.Controls.Add(this.label1);
            this.m_panelSaveLoadEtc.Controls.Add(this.m_btnSave);
            this.m_panelSaveLoadEtc.Controls.Add(this.m_btnLoad);
            this.m_panelSaveLoadEtc.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_panelSaveLoadEtc.Location = new System.Drawing.Point(234, 32);
            this.m_panelSaveLoadEtc.Name = "m_panelSaveLoadEtc";
            this.m_panelSaveLoadEtc.Size = new System.Drawing.Size(26, 235);
            this.m_panelSaveLoadEtc.TabIndex = 1;
            // 
            // m_dragSetVar
            // 
            this.m_dragSetVar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_dragSetVar.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_dragSetVar.Image = global::sc2i.win32.expression.Resource1.SetVar;
            this.m_dragSetVar.Location = new System.Drawing.Point(0, 63);
            this.m_dragSetVar.Name = "m_dragSetVar";
            this.m_dragSetVar.Size = new System.Drawing.Size(26, 21);
            this.m_dragSetVar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.m_dragSetVar.TabIndex = 38;
            this.m_dragSetVar.TabStop = false;
            this.m_tooltip.SetToolTip(this.m_dragSetVar, "Add an affectation|20012");
            this.m_dragSetVar.BeginDragDrop += new System.EventHandler(this.m_dragSetVar_BeginDragDrop);
            // 
            // m_dragConstante
            // 
            this.m_dragConstante.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_dragConstante.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_dragConstante.Image = global::sc2i.win32.expression.Resource1.Constante;
            this.m_dragConstante.Location = new System.Drawing.Point(0, 42);
            this.m_dragConstante.Name = "m_dragConstante";
            this.m_dragConstante.Size = new System.Drawing.Size(26, 21);
            this.m_dragConstante.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.m_dragConstante.TabIndex = 37;
            this.m_dragConstante.TabStop = false;
            this.m_tooltip.SetToolTip(this.m_dragConstante, "Add a constant|20011");
            this.m_dragConstante.BeginDragDrop += new System.EventHandler(this.m_dragConstante_BeginDragDrop);
            // 
            // m_btnDragIf
            // 
            this.m_btnDragIf.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_btnDragIf.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_btnDragIf.Image = global::sc2i.win32.expression.Resource1._if;
            this.m_btnDragIf.Location = new System.Drawing.Point(0, 21);
            this.m_btnDragIf.Name = "m_btnDragIf";
            this.m_btnDragIf.Size = new System.Drawing.Size(26, 21);
            this.m_btnDragIf.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.m_btnDragIf.TabIndex = 36;
            this.m_btnDragIf.TabStop = false;
            this.m_tooltip.SetToolTip(this.m_btnDragIf, "Add a condition|20013");
            this.m_btnDragIf.BeginDragDrop += new System.EventHandler(this.m_btnDragIf_BeginDragDrop);
            // 
            // m_DragAddNew
            // 
            this.m_DragAddNew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_DragAddNew.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_DragAddNew.Image = global::sc2i.win32.expression.Resource1.binary_tree;
            this.m_DragAddNew.Location = new System.Drawing.Point(0, 0);
            this.m_DragAddNew.Name = "m_DragAddNew";
            this.m_DragAddNew.Size = new System.Drawing.Size(26, 21);
            this.m_DragAddNew.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.m_DragAddNew.TabIndex = 35;
            this.m_DragAddNew.TabStop = false;
            this.m_tooltip.SetToolTip(this.m_DragAddNew, "Add a new bloc|20010");
            this.m_DragAddNew.BeginDragDrop += new System.EventHandler(this.m_DragAddNew_BeginDragDrop);
            this.m_DragAddNew.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_DragAddNew_MouseUp);
            // 
            // m_btnCopy
            // 
            this.m_btnCopy.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("m_btnCopy.Image")));
            this.m_btnCopy.Location = new System.Drawing.Point(0, 133);
            this.m_btnCopy.Name = "m_btnCopy";
            this.m_btnCopy.Size = new System.Drawing.Size(26, 23);
            this.m_btnCopy.TabIndex = 33;
            this.m_btnCopy.Click += new System.EventHandler(this.m_btnCopy_Click);
            // 
            // m_btnPaste
            // 
            this.m_btnPaste.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_btnPaste.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnPaste.Image = ((System.Drawing.Image)(resources.GetObject("m_btnPaste.Image")));
            this.m_btnPaste.Location = new System.Drawing.Point(0, 156);
            this.m_btnPaste.Name = "m_btnPaste";
            this.m_btnPaste.Size = new System.Drawing.Size(26, 23);
            this.m_btnPaste.TabIndex = 32;
            this.m_btnPaste.Click += new System.EventHandler(this.m_btnPaste_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(0, 179);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 10);
            this.label1.TabIndex = 34;
            // 
            // m_btnSave
            // 
            this.m_btnSave.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnSave.Image = ((System.Drawing.Image)(resources.GetObject("m_btnSave.Image")));
            this.m_btnSave.Location = new System.Drawing.Point(0, 189);
            this.m_btnSave.Name = "m_btnSave";
            this.m_btnSave.Size = new System.Drawing.Size(26, 23);
            this.m_btnSave.TabIndex = 31;
            this.m_btnSave.Click += new System.EventHandler(this.m_btnSave_Click);
            // 
            // m_btnLoad
            // 
            this.m_btnLoad.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnLoad.Image = ((System.Drawing.Image)(resources.GetObject("m_btnLoad.Image")));
            this.m_btnLoad.Location = new System.Drawing.Point(0, 212);
            this.m_btnLoad.Name = "m_btnLoad";
            this.m_btnLoad.Size = new System.Drawing.Size(26, 23);
            this.m_btnLoad.TabIndex = 30;
            this.m_btnLoad.Click += new System.EventHandler(this.m_btnLoad_Click);
            // 
            // m_menuAddFormule
            // 
            this.m_menuAddFormule.Name = "m_menuAddFormule";
            this.m_menuAddFormule.Size = new System.Drawing.Size(61, 4);
            // 
            // m_editeur
            // 
            this.m_editeur.AllowDrop = true;
            this.m_editeur.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.m_editeur.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_editeur.Echelle = 1F;
            this.m_editeur.EffetAjoutSuppression = false;
            this.m_editeur.EffetFonduMenu = true;
            this.m_editeur.EnDeplacement = false;
            this.m_editeur.FormesDesPoignees = sc2i.win32.common.EFormePoignee.Carre;
            cGrilleEditeurObjetGraphique1.Couleur = System.Drawing.Color.LightGray;
            cGrilleEditeurObjetGraphique1.HauteurCarreau = 20;
            cGrilleEditeurObjetGraphique1.LargeurCarreau = 20;
            cGrilleEditeurObjetGraphique1.Representation = sc2i.win32.common.ERepresentationGrille.LignesContinues;
            cGrilleEditeurObjetGraphique1.TailleCarreau = new System.Drawing.Size(20, 20);
            this.m_editeur.GrilleAlignement = cGrilleEditeurObjetGraphique1;
            this.m_editeur.HauteurMinimaleDesObjets = 10;
            this.m_editeur.HistorisationActive = true;
            this.m_editeur.LargeurMinimaleDesObjets = 10;
            this.m_editeur.Location = new System.Drawing.Point(260, 32);
            this.m_editeur.LockEdition = false;
            this.m_editeur.Marge = 10;
            this.m_editeur.ModeAffichageGrille = sc2i.win32.common.EModeAffichageGrille.AuDeplacement;
            this.m_editeur.ModeRepresentationGrille = sc2i.win32.common.ERepresentationGrille.LignesContinues;
            this.m_editeur.ModeSouris = sc2i.win32.common.CPanelEditionObjetGraphique.EModeSouris.Selection;
            this.m_editeur.ModeSourisCustom = sc2i.win32.expression.CPanelEditionRepresentationExpressionGraphique.EModeSourisCustom.LienSequence;
            this.m_editeur.Name = "m_editeur";
            this.m_editeur.NoClipboard = false;
            this.m_editeur.NoDelete = false;
            this.m_editeur.NoDoubleClick = false;
            this.m_editeur.NombreHistorisation = 10;
            this.m_editeur.NoMenu = false;
            this.m_editeur.ObjetAnalyse = null;
            this.m_editeur.ObjetEdite = null;
            cProfilEditeurObjetGraphique1.FormeDesPoignees = sc2i.win32.common.EFormePoignee.Carre;
            cProfilEditeurObjetGraphique1.Grille = cGrilleEditeurObjetGraphique1;
            cProfilEditeurObjetGraphique1.HistorisationActive = true;
            cProfilEditeurObjetGraphique1.Marge = 10;
            cProfilEditeurObjetGraphique1.ModeAffichageGrille = sc2i.win32.common.EModeAffichageGrille.AuDeplacement;
            cProfilEditeurObjetGraphique1.NombreHistorisation = 10;
            cProfilEditeurObjetGraphique1.ToujoursAlignerSurLaGrille = false;
            this.m_editeur.Profil = cProfilEditeurObjetGraphique1;
            this.m_editeur.RefreshSelectionChanged = true;
            this.m_editeur.SelectionVisible = true;
            this.m_editeur.Size = new System.Drawing.Size(325, 235);
            this.m_editeur.TabIndex = 0;
            this.m_editeur.ToujoursAlignerSelonLesControles = true;
            this.m_editeur.ToujoursAlignerSurLaGrille = false;
            this.m_editeur.SelectionChanged += new System.EventHandler(this.m_editeur_SelectionChanged);
            this.m_editeur.AskCreationVariable += new sc2i.win32.expression.AskCreationVariableEventHandler(this.m_editeur_AskCreationVariable);
            this.m_editeur.FormulesChanged += new System.EventHandler(this.m_editeur_FormulesChanged);
            // 
            // m_timerSelection
            // 
            this.m_timerSelection.Interval = 200;
            this.m_timerSelection.Tick += new System.EventHandler(this.m_timerSelection_Tick);
            // 
            // CEditeurExpressionGraphique
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_editeur);
            this.Controls.Add(this.m_panelSaveLoadEtc);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.splitter3);
            this.Controls.Add(this.m_panelVariables);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.m_panelDetailFormule);
            this.Controls.Add(this.m_btnLink);
            this.Controls.Add(this.m_arbreFormules);
            this.Name = "CEditeurExpressionGraphique";
            this.Size = new System.Drawing.Size(765, 470);
            this.m_btnLink.ResumeLayout(false);
            this.m_panelDetailFormule.ResumeLayout(false);
            this.m_panelVariables.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.m_panelSaveLoadEtc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_dragSetVar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dragConstante)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnDragIf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_DragAddNew)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CPanelEditionRepresentationExpressionGraphique m_editeur;
        private System.Windows.Forms.Panel m_btnLink;
        private System.Windows.Forms.RadioButton m_btnSelect;
        private System.Windows.Forms.TreeView m_arbreFormules;
        private System.Windows.Forms.Panel m_panelDetailFormule;
        private System.Windows.Forms.Splitter splitter1;
        private CEditeurParametresFormule m_editeurParametresStandard;
        private System.Windows.Forms.Panel m_panelVariables;
        private System.Windows.Forms.RadioButton m_chkLien;
        private System.Windows.Forms.RadioButton m_chkZoom;
        private System.Windows.Forms.ListView m_wndListeVariables;
        private System.Windows.Forms.Panel panel1;
        private sc2i.win32.common.CWndLinkStd m_lnkRemoveVar;
        private sc2i.win32.common.CWndLinkStd m_lnkAddVar;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.RadioButton m_btnLienParametre;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.Panel m_panelSaveLoadEtc;
        private System.Windows.Forms.Button m_btnCopy;
        private System.Windows.Forms.Button m_btnPaste;
        private System.Windows.Forms.Button m_btnSave;
        private System.Windows.Forms.Button m_btnLoad;
        private System.Windows.Forms.Label label1;
        private sc2i.win32.common.CPictureBoxDraggable m_DragAddNew;
        private sc2i.win32.common.CPictureBoxDraggable m_btnDragIf;
        private sc2i.win32.common.CPictureBoxDraggable m_dragConstante;
        private sc2i.win32.common.CPictureBoxDraggable m_dragSetVar;
        private sc2i.win32.common.CToolTipTraductible m_tooltip;
        private System.Windows.Forms.ContextMenuStrip m_menuAddFormule;
        private System.Windows.Forms.Timer m_timerSelection;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox m_chkActivateDebug;
    }
}
