using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using sc2i.data.dynamic;
using sc2i.common;
using sc2i.formulaire;
using sc2i.win32.common;
using sc2i.formulaire.win32.editor;
using sc2i.expression;
using sc2i.data;

namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Description résumée de CPanelEditFiltreDynamique.
	/// </summary>
	public delegate void ChangeTypeElementsEventHandler(object sender, Type typeSelectionne );

	public class CPanelEditFiltreDynamique : System.Windows.Forms.UserControl , IControlALockEdition
	{
        private IFournisseurProprietesDynamiques m_fournisseurProprietesFiltrees = null;
		private sc2i.expression.CDefinitionProprieteDynamique m_definitionRacinePourChampsFiltres = null;
		private bool m_bModeSansType = false;
		private bool m_bModeSansVariables = false;
		private bool m_bComboInitialized = false;
		private TreeNode m_lastNodeSelectionne = null;
		private bool m_bIsLoad = false;
		private CFiltreDynamique m_filtreDynamique = null;
		private System.Windows.Forms.Label label1;
		private sc2i.win32.common.C2iComboBox m_cmbTypeElements;
		private sc2i.win32.common.CExtModeEdition m_gestionnaireModeEdition;
		private sc2i.win32.common.C2iTabControl m_tab;
		private Crownwood.Magic.Controls.TabPage m_pageFiltre;
		private Crownwood.Magic.Controls.TabPage m_pageChamps;
		private System.Windows.Forms.TreeView m_arbreFiltre;
		private System.Windows.Forms.ContextMenu m_menuArbre;
		private System.Windows.Forms.MenuItem m_menuAddEt;
		private System.Windows.Forms.MenuItem m_menuAddOu;
		private System.Windows.Forms.MenuItem m_menuAjouter;
		private System.Windows.Forms.MenuItem m_menuAddCondition;
		private System.Windows.Forms.MenuItem m_menuInsererEt;
		private System.Windows.Forms.MenuItem m_menuInsererOu;
		private System.Windows.Forms.MenuItem m_menuSupprimer;
		private System.Windows.Forms.MenuItem m_menuInsert;
		private System.Windows.Forms.MenuItem m_menuSupprimerElementEtFils;
		private System.Windows.Forms.MenuItem m_menuDecalerFilsVersLeHaut;
		private System.Windows.Forms.MenuItem m_menuProprietes;
		private Crownwood.Magic.Controls.TabPage m_pageFormulaire;
		private sc2i.win32.common.CWndLinkStd m_btnAjouterChamp;
		private sc2i.win32.common.CWndLinkStd m_btnModifierChamp;
		private sc2i.win32.common.CWndLinkStd m_btnSupprimerChamp;
		private System.Windows.Forms.ContextMenu m_menuNewVariable;
		private System.Windows.Forms.MenuItem m_menuVariableSaisie;
		private sc2i.win32.common.ListViewAutoFilledColumn colNomChamp;
		private sc2i.win32.common.ListViewAutoFilledColumn m_colType;
		private sc2i.win32.common.ListViewAutoFilled m_wndListeVariables;
		private System.Windows.Forms.MenuItem m_menuNewVariableCalculée;
		private System.Windows.Forms.MenuItem m_menuNewVariableSelection;
		private System.Windows.Forms.PropertyGrid m_propertyGrid;
		private sc2i.formulaire.win32.editor.CPanelListe2iWnd m_listeControles;
		private sc2i.formulaire.win32.editor.CPanelEditionFormulaire m_panelEditionFormulaire;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.MenuItem m_menuEstNull;
		private System.Windows.Forms.MenuItem m_menuAjouterSelection;
		private System.Windows.Forms.Panel m_panelTypeElements;
		private System.Windows.Forms.Panel m_panelBas;
		private System.Windows.Forms.MenuItem m_menuNewListeObjets;
		private System.Windows.Forms.Button m_btnCopy;
		private System.Windows.Forms.Button m_btnPaste;
		private System.Windows.Forms.Button m_btnSave;
		private System.Windows.Forms.Button m_btnOpen;
		private System.Windows.Forms.MenuItem m_menuRechercheAvancee;
        protected CExtStyle cExtStyle1;
		private Button m_btnCopyText;
        private MenuItem m_menuCopierComposant;
        private MenuItem m_menuCollerComposant;
        private Panel m_panelMenuOption;
        private LinkLabel m_lnkOptions;
        private MenuItem m_menuSousFiltre;
		private System.ComponentModel.IContainer components;

		
		public CPanelEditFiltreDynamique()
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();
			// TODO : ajoutez les initialisations après l'appel à InitForm

            m_fournisseurProprietesFiltrees = new CFournisseurProprietesForFiltreDynamique();

		}

		/// <summary> 
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		//Definition racine des champs filtrés (pour traduction de champs)
		public CDefinitionProprieteDynamique DefinitionRacineDeChampsFiltres
		{
			get
			{
				return m_definitionRacinePourChampsFiltres;
			}
			set
			{
				m_definitionRacinePourChampsFiltres = value;
			}
		}

		#region Component Designer generated code
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPanelEditFiltreDynamique));
            sc2i.win32.common.CGrilleEditeurObjetGraphique cGrilleEditeurObjetGraphique1 = new sc2i.win32.common.CGrilleEditeurObjetGraphique();
            sc2i.win32.common.CProfilEditeurObjetGraphique cProfilEditeurObjetGraphique1 = new sc2i.win32.common.CProfilEditeurObjetGraphique();
            this.label1 = new System.Windows.Forms.Label();
            this.m_cmbTypeElements = new sc2i.win32.common.C2iComboBox();
            this.m_gestionnaireModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_tab = new sc2i.win32.common.C2iTabControl(this.components);
            this.m_pageFiltre = new Crownwood.Magic.Controls.TabPage();
            this.m_arbreFiltre = new System.Windows.Forms.TreeView();
            this.m_panelMenuOption = new System.Windows.Forms.Panel();
            this.m_lnkOptions = new System.Windows.Forms.LinkLabel();
            this.m_pageChamps = new Crownwood.Magic.Controls.TabPage();
            this.m_wndListeVariables = new sc2i.win32.common.ListViewAutoFilled();
            this.colNomChamp = ((sc2i.win32.common.ListViewAutoFilledColumn)(new sc2i.win32.common.ListViewAutoFilledColumn()));
            this.m_colType = ((sc2i.win32.common.ListViewAutoFilledColumn)(new sc2i.win32.common.ListViewAutoFilledColumn()));
            this.m_btnSupprimerChamp = new sc2i.win32.common.CWndLinkStd();
            this.m_btnModifierChamp = new sc2i.win32.common.CWndLinkStd();
            this.m_btnAjouterChamp = new sc2i.win32.common.CWndLinkStd();
            this.m_pageFormulaire = new Crownwood.Magic.Controls.TabPage();
            this.m_panelEditionFormulaire = new sc2i.formulaire.win32.editor.CPanelEditionFormulaire();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.m_propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.m_listeControles = new sc2i.formulaire.win32.editor.CPanelListe2iWnd();
            this.m_panelTypeElements = new System.Windows.Forms.Panel();
            this.m_panelBas = new System.Windows.Forms.Panel();
            this.m_btnCopyText = new System.Windows.Forms.Button();
            this.m_btnOpen = new System.Windows.Forms.Button();
            this.m_btnSave = new System.Windows.Forms.Button();
            this.m_btnPaste = new System.Windows.Forms.Button();
            this.m_btnCopy = new System.Windows.Forms.Button();
            this.m_menuArbre = new System.Windows.Forms.ContextMenu();
            this.m_menuAjouter = new System.Windows.Forms.MenuItem();
            this.m_menuAddEt = new System.Windows.Forms.MenuItem();
            this.m_menuAddOu = new System.Windows.Forms.MenuItem();
            this.m_menuAddCondition = new System.Windows.Forms.MenuItem();
            this.m_menuEstNull = new System.Windows.Forms.MenuItem();
            this.m_menuAjouterSelection = new System.Windows.Forms.MenuItem();
            this.m_menuRechercheAvancee = new System.Windows.Forms.MenuItem();
            this.m_menuSousFiltre = new System.Windows.Forms.MenuItem();
            this.m_menuInsert = new System.Windows.Forms.MenuItem();
            this.m_menuInsererEt = new System.Windows.Forms.MenuItem();
            this.m_menuInsererOu = new System.Windows.Forms.MenuItem();
            this.m_menuSupprimer = new System.Windows.Forms.MenuItem();
            this.m_menuSupprimerElementEtFils = new System.Windows.Forms.MenuItem();
            this.m_menuDecalerFilsVersLeHaut = new System.Windows.Forms.MenuItem();
            this.m_menuProprietes = new System.Windows.Forms.MenuItem();
            this.m_menuCopierComposant = new System.Windows.Forms.MenuItem();
            this.m_menuCollerComposant = new System.Windows.Forms.MenuItem();
            this.m_menuNewVariable = new System.Windows.Forms.ContextMenu();
            this.m_menuVariableSaisie = new System.Windows.Forms.MenuItem();
            this.m_menuNewVariableCalculée = new System.Windows.Forms.MenuItem();
            this.m_menuNewVariableSelection = new System.Windows.Forms.MenuItem();
            this.m_menuNewListeObjets = new System.Windows.Forms.MenuItem();
            this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.m_tab.SuspendLayout();
            this.m_pageFiltre.SuspendLayout();
            this.m_panelMenuOption.SuspendLayout();
            this.m_pageChamps.SuspendLayout();
            this.m_pageFormulaire.SuspendLayout();
            this.m_panelTypeElements.SuspendLayout();
            this.m_panelBas.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 16);
            this.cExtStyle1.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filtered elements|174";
            // 
            // m_cmbTypeElements
            // 
            this.m_cmbTypeElements.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbTypeElements.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbTypeElements.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_cmbTypeElements.IsLink = false;
            this.m_cmbTypeElements.Location = new System.Drawing.Point(126, 8);
            this.m_cmbTypeElements.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_cmbTypeElements, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_cmbTypeElements.Name = "m_cmbTypeElements";
            this.m_cmbTypeElements.Size = new System.Drawing.Size(570, 21);
            this.cExtStyle1.SetStyleBackColor(this.m_cmbTypeElements, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_cmbTypeElements, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_cmbTypeElements.TabIndex = 1;
            this.m_cmbTypeElements.SelectedIndexChanged += new System.EventHandler(this.m_cmbTypeElements_SelectedIndexChanged);
            this.m_cmbTypeElements.SelectedValueChanged += new System.EventHandler(this.m_cmbType_SelectedValueChanged);
            // 
            // m_tab
            // 
            this.m_tab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_tab.BoldSelectedPage = true;
            this.m_tab.ControlBottomOffset = 16;
            this.m_tab.ControlRightOffset = 16;
            this.m_tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tab.IDEPixelArea = false;
            this.m_tab.Location = new System.Drawing.Point(0, 32);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_tab, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_tab.Name = "m_tab";
            this.m_tab.Ombre = true;
            this.m_tab.PositionTop = true;
            this.m_tab.SelectedIndex = 0;
            this.m_tab.SelectedTab = this.m_pageFiltre;
            this.m_tab.Size = new System.Drawing.Size(712, 320);
            this.cExtStyle1.SetStyleBackColor(this.m_tab, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.m_tab, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_tab.TabIndex = 2;
            this.m_tab.TabPages.AddRange(new Crownwood.Magic.Controls.TabPage[] {
            this.m_pageFiltre,
            this.m_pageChamps,
            this.m_pageFormulaire});
            this.m_tab.SelectionChanged += new System.EventHandler(this.m_tab_SelectionChanged);
            // 
            // m_pageFiltre
            // 
            this.m_pageFiltre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pageFiltre.Controls.Add(this.m_arbreFiltre);
            this.m_pageFiltre.Controls.Add(this.m_panelMenuOption);
            this.m_pageFiltre.Location = new System.Drawing.Point(0, 25);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_pageFiltre, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_pageFiltre.Name = "m_pageFiltre";
            this.m_pageFiltre.Size = new System.Drawing.Size(696, 279);
            this.cExtStyle1.SetStyleBackColor(this.m_pageFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_pageFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_pageFiltre.TabIndex = 0;
            this.m_pageFiltre.Title = "Filter|175";
            // 
            // m_arbreFiltre
            // 
            this.m_arbreFiltre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_arbreFiltre.HideSelection = false;
            this.m_arbreFiltre.Location = new System.Drawing.Point(11, 27);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_arbreFiltre, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_arbreFiltre.Name = "m_arbreFiltre";
            this.m_arbreFiltre.Size = new System.Drawing.Size(680, 249);
            this.cExtStyle1.SetStyleBackColor(this.m_arbreFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_arbreFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_arbreFiltre.TabIndex = 0;
            this.m_arbreFiltre.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.m_arbreFiltre_NodeMouseDoubleClick);
            this.m_arbreFiltre.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_arbreFiltre_MouseUp);
            // 
            // m_panelMenuOption
            // 
            this.m_panelMenuOption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelMenuOption.Controls.Add(this.m_lnkOptions);
            this.m_panelMenuOption.Location = new System.Drawing.Point(585, 2);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelMenuOption, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_panelMenuOption.Name = "m_panelMenuOption";
            this.m_panelMenuOption.Size = new System.Drawing.Size(111, 25);
            this.cExtStyle1.SetStyleBackColor(this.m_panelMenuOption, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelMenuOption, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelMenuOption.TabIndex = 11;
            // 
            // m_lnkOptions
            // 
            this.m_lnkOptions.AutoSize = true;
            this.m_lnkOptions.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_lnkOptions.Location = new System.Drawing.Point(2, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkOptions, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lnkOptions.Name = "m_lnkOptions";
            this.m_lnkOptions.Size = new System.Drawing.Size(109, 15);
            this.cExtStyle1.SetStyleBackColor(this.m_lnkOptions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_lnkOptions, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lnkOptions.TabIndex = 12;
            this.m_lnkOptions.TabStop = true;
            this.m_lnkOptions.Text = "Filter options|20054";
            this.m_lnkOptions.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkOptions_LinkClicked);
            // 
            // m_pageChamps
            // 
            this.m_pageChamps.Controls.Add(this.m_wndListeVariables);
            this.m_pageChamps.Controls.Add(this.m_btnSupprimerChamp);
            this.m_pageChamps.Controls.Add(this.m_btnModifierChamp);
            this.m_pageChamps.Controls.Add(this.m_btnAjouterChamp);
            this.m_pageChamps.Location = new System.Drawing.Point(0, 25);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_pageChamps, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_pageChamps.Name = "m_pageChamps";
            this.m_pageChamps.Selected = false;
            this.m_pageChamps.Size = new System.Drawing.Size(696, 279);
            this.cExtStyle1.SetStyleBackColor(this.m_pageChamps, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_pageChamps, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_pageChamps.TabIndex = 1;
            this.m_pageChamps.Title = "Filter fields|176";
            // 
            // m_wndListeVariables
            // 
            this.m_wndListeVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_wndListeVariables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colNomChamp,
            this.m_colType});
            this.m_wndListeVariables.EnableCustomisation = true;
            this.m_wndListeVariables.FullRowSelect = true;
            this.m_wndListeVariables.Location = new System.Drawing.Point(8, 32);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_wndListeVariables, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_wndListeVariables.Name = "m_wndListeVariables";
            this.m_wndListeVariables.Size = new System.Drawing.Size(680, 240);
            this.cExtStyle1.SetStyleBackColor(this.m_wndListeVariables, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_wndListeVariables, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListeVariables.TabIndex = 4;
            this.m_wndListeVariables.UseCompatibleStateImageBehavior = false;
            this.m_wndListeVariables.View = System.Windows.Forms.View.Details;
            this.m_wndListeVariables.DoubleClick += new System.EventHandler(this.m_wndListeVariables_DoubleClick);
            // 
            // colNomChamp
            // 
            this.colNomChamp.Field = "Nom";
            this.colNomChamp.PrecisionWidth = 0D;
            this.colNomChamp.ProportionnalSize = false;
            this.colNomChamp.Text = "Name|253";
            this.colNomChamp.Visible = true;
            this.colNomChamp.Width = 250;
            // 
            // m_colType
            // 
            this.m_colType.Field = "LibelleType";
            this.m_colType.PrecisionWidth = 0D;
            this.m_colType.ProportionnalSize = false;
            this.m_colType.Text = "Type|254";
            this.m_colType.Visible = true;
            this.m_colType.Width = 120;
            // 
            // m_btnSupprimerChamp
            // 
            this.m_btnSupprimerChamp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnSupprimerChamp.CustomImage = ((System.Drawing.Image)(resources.GetObject("m_btnSupprimerChamp.CustomImage")));
            this.m_btnSupprimerChamp.CustomText = "Remove";
            this.m_btnSupprimerChamp.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_btnSupprimerChamp.Location = new System.Drawing.Point(168, 8);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnSupprimerChamp, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnSupprimerChamp.Name = "m_btnSupprimerChamp";
            this.m_btnSupprimerChamp.ShortMode = false;
            this.m_btnSupprimerChamp.Size = new System.Drawing.Size(72, 16);
            this.cExtStyle1.SetStyleBackColor(this.m_btnSupprimerChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnSupprimerChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnSupprimerChamp.TabIndex = 3;
            this.m_btnSupprimerChamp.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_btnSupprimerChamp.LinkClicked += new System.EventHandler(this.m_btnSupprimerChamp_LinkClicked);
            // 
            // m_btnModifierChamp
            // 
            this.m_btnModifierChamp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnModifierChamp.CustomImage = ((System.Drawing.Image)(resources.GetObject("m_btnModifierChamp.CustomImage")));
            this.m_btnModifierChamp.CustomText = "Detail";
            this.m_btnModifierChamp.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_btnModifierChamp.Location = new System.Drawing.Point(88, 8);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnModifierChamp, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnModifierChamp.Name = "m_btnModifierChamp";
            this.m_btnModifierChamp.ShortMode = false;
            this.m_btnModifierChamp.Size = new System.Drawing.Size(72, 16);
            this.cExtStyle1.SetStyleBackColor(this.m_btnModifierChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnModifierChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnModifierChamp.TabIndex = 2;
            this.m_btnModifierChamp.TypeLien = sc2i.win32.common.TypeLinkStd.Modification;
            this.m_btnModifierChamp.LinkClicked += new System.EventHandler(this.m_btnModifierChamp_LinkClicked);
            // 
            // m_btnAjouterChamp
            // 
            this.m_btnAjouterChamp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnAjouterChamp.CustomImage = ((System.Drawing.Image)(resources.GetObject("m_btnAjouterChamp.CustomImage")));
            this.m_btnAjouterChamp.CustomText = "Add";
            this.m_btnAjouterChamp.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_btnAjouterChamp.Location = new System.Drawing.Point(8, 8);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnAjouterChamp, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnAjouterChamp.Name = "m_btnAjouterChamp";
            this.m_btnAjouterChamp.ShortMode = false;
            this.m_btnAjouterChamp.Size = new System.Drawing.Size(72, 16);
            this.cExtStyle1.SetStyleBackColor(this.m_btnAjouterChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnAjouterChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAjouterChamp.TabIndex = 1;
            this.m_btnAjouterChamp.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_btnAjouterChamp.LinkClicked += new System.EventHandler(this.m_btnAjouterChamp_LinkClicked);
            // 
            // m_pageFormulaire
            // 
            this.m_pageFormulaire.Controls.Add(this.m_panelEditionFormulaire);
            this.m_pageFormulaire.Controls.Add(this.splitter1);
            this.m_pageFormulaire.Controls.Add(this.m_propertyGrid);
            this.m_pageFormulaire.Controls.Add(this.m_listeControles);
            this.m_pageFormulaire.Location = new System.Drawing.Point(0, 25);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_pageFormulaire, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_pageFormulaire.Name = "m_pageFormulaire";
            this.m_pageFormulaire.Selected = false;
            this.m_pageFormulaire.Size = new System.Drawing.Size(696, 279);
            this.cExtStyle1.SetStyleBackColor(this.m_pageFormulaire, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_pageFormulaire, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_pageFormulaire.TabIndex = 2;
            this.m_pageFormulaire.Title = "Form|177";
            // 
            // m_panelEditionFormulaire
            // 
            this.m_panelEditionFormulaire.AllowDrop = true;
            this.m_panelEditionFormulaire.AutoScroll = true;
            this.m_panelEditionFormulaire.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(247)))), ((int)(((byte)(184)))));
            this.m_panelEditionFormulaire.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.m_panelEditionFormulaire.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelEditionFormulaire.Echelle = 1F;
            this.m_panelEditionFormulaire.EffetAjoutSuppression = false;
            this.m_panelEditionFormulaire.EffetFonduMenu = true;
            this.m_panelEditionFormulaire.EnDeplacement = false;
            this.m_panelEditionFormulaire.EntiteEditee = null;
            this.m_panelEditionFormulaire.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.m_panelEditionFormulaire.FormesDesPoignees = sc2i.win32.common.EFormePoignee.Carre;
            this.m_panelEditionFormulaire.FournisseurProprietes = null;
            cGrilleEditeurObjetGraphique1.Couleur = System.Drawing.Color.LightGray;
            cGrilleEditeurObjetGraphique1.HauteurCarreau = 20;
            cGrilleEditeurObjetGraphique1.LargeurCarreau = 20;
            cGrilleEditeurObjetGraphique1.Representation = sc2i.win32.common.ERepresentationGrille.LignesContinues;
            cGrilleEditeurObjetGraphique1.TailleCarreau = new System.Drawing.Size(20, 20);
            this.m_panelEditionFormulaire.GrilleAlignement = cGrilleEditeurObjetGraphique1;
            this.m_panelEditionFormulaire.HauteurMinimaleDesObjets = 10;
            this.m_panelEditionFormulaire.HistorisationActive = false;
            this.m_panelEditionFormulaire.LargeurMinimaleDesObjets = 10;
            this.m_panelEditionFormulaire.Location = new System.Drawing.Point(144, 0);
            this.m_panelEditionFormulaire.LockEdition = false;
            this.m_panelEditionFormulaire.Marge = 10;
            this.m_panelEditionFormulaire.MaxZoom = 6F;
            this.m_panelEditionFormulaire.MinZoom = 0.2F;
            this.m_panelEditionFormulaire.ModeAffichageGrille = sc2i.win32.common.EModeAffichageGrille.AuDeplacement;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelEditionFormulaire, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_panelEditionFormulaire.ModeRepresentationGrille = sc2i.win32.common.ERepresentationGrille.LignesContinues;
            this.m_panelEditionFormulaire.ModeSouris = sc2i.win32.common.CPanelEditionObjetGraphique.EModeSouris.Selection;
            this.m_panelEditionFormulaire.Name = "m_panelEditionFormulaire";
            this.m_panelEditionFormulaire.NoClipboard = false;
            this.m_panelEditionFormulaire.NoDelete = false;
            this.m_panelEditionFormulaire.NoDoubleClick = false;
            this.m_panelEditionFormulaire.NombreHistorisation = 10;
            this.m_panelEditionFormulaire.NoMenu = false;
            this.m_panelEditionFormulaire.ObjetEdite = null;
            cProfilEditeurObjetGraphique1.FormeDesPoignees = sc2i.win32.common.EFormePoignee.Carre;
            cProfilEditeurObjetGraphique1.Grille = cGrilleEditeurObjetGraphique1;
            cProfilEditeurObjetGraphique1.HistorisationActive = false;
            cProfilEditeurObjetGraphique1.Marge = 10;
            cProfilEditeurObjetGraphique1.ModeAffichageGrille = sc2i.win32.common.EModeAffichageGrille.AuDeplacement;
            cProfilEditeurObjetGraphique1.NombreHistorisation = 10;
            cProfilEditeurObjetGraphique1.ToujoursAlignerSurLaGrille = false;
            this.m_panelEditionFormulaire.Profil = cProfilEditeurObjetGraphique1;
            this.m_panelEditionFormulaire.RefreshSelectionChanged = true;
            this.m_panelEditionFormulaire.SelectionVisible = true;
            this.m_panelEditionFormulaire.Size = new System.Drawing.Size(373, 279);
            this.cExtStyle1.SetStyleBackColor(this.m_panelEditionFormulaire, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelEditionFormulaire, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelEditionFormulaire.TabIndex = 3;
            this.m_panelEditionFormulaire.ToujoursAlignerSelonLesControles = true;
            this.m_panelEditionFormulaire.ToujoursAlignerSurLaGrille = false;
            this.m_panelEditionFormulaire.TypeEdite = null;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(517, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.splitter1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 279);
            this.cExtStyle1.SetStyleBackColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.splitter1.TabIndex = 6;
            this.splitter1.TabStop = false;
            // 
            // m_propertyGrid
            // 
            this.m_propertyGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(247)))), ((int)(((byte)(184)))));
            this.m_propertyGrid.CommandsBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(247)))), ((int)(((byte)(184)))));
            this.m_propertyGrid.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_propertyGrid.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.m_propertyGrid.HelpVisible = false;
            this.m_propertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.m_propertyGrid.Location = new System.Drawing.Point(520, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_propertyGrid, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_propertyGrid.Name = "m_propertyGrid";
            this.m_propertyGrid.Size = new System.Drawing.Size(176, 279);
            this.cExtStyle1.SetStyleBackColor(this.m_propertyGrid, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_propertyGrid, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_propertyGrid.TabIndex = 5;
            this.m_propertyGrid.ToolbarVisible = false;
            this.m_propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.m_propertyGrid_PropertyValueChanged);
            // 
            // m_listeControles
            // 
            this.m_listeControles.AutoScroll = true;
            this.m_listeControles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_listeControles.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_listeControles.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.m_listeControles.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_listeControles, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_listeControles.Name = "m_listeControles";
            this.m_listeControles.Size = new System.Drawing.Size(144, 279);
            this.cExtStyle1.SetStyleBackColor(this.m_listeControles, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.m_listeControles, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_listeControles.TabIndex = 4;
            // 
            // m_panelTypeElements
            // 
            this.m_panelTypeElements.BackColor = System.Drawing.Color.White;
            this.m_panelTypeElements.Controls.Add(this.label1);
            this.m_panelTypeElements.Controls.Add(this.m_cmbTypeElements);
            this.m_panelTypeElements.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelTypeElements.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelTypeElements, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelTypeElements.Name = "m_panelTypeElements";
            this.m_panelTypeElements.Size = new System.Drawing.Size(712, 32);
            this.cExtStyle1.SetStyleBackColor(this.m_panelTypeElements, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelTypeElements, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelTypeElements.TabIndex = 1;
            // 
            // m_panelBas
            // 
            this.m_panelBas.BackColor = System.Drawing.Color.White;
            this.m_panelBas.Controls.Add(this.m_btnCopyText);
            this.m_panelBas.Controls.Add(this.m_btnOpen);
            this.m_panelBas.Controls.Add(this.m_btnSave);
            this.m_panelBas.Controls.Add(this.m_btnPaste);
            this.m_panelBas.Controls.Add(this.m_btnCopy);
            this.m_panelBas.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_panelBas.Location = new System.Drawing.Point(0, 352);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelBas, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelBas.Name = "m_panelBas";
            this.m_panelBas.Size = new System.Drawing.Size(712, 24);
            this.cExtStyle1.SetStyleBackColor(this.m_panelBas, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelBas, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelBas.TabIndex = 11;
            // 
            // m_btnCopyText
            // 
            this.m_btnCopyText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCopyText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnCopyText.Image = ((System.Drawing.Image)(resources.GetObject("m_btnCopyText.Image")));
            this.m_btnCopyText.Location = new System.Drawing.Point(570, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnCopyText, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnCopyText.Name = "m_btnCopyText";
            this.m_btnCopyText.Size = new System.Drawing.Size(24, 23);
            this.cExtStyle1.SetStyleBackColor(this.m_btnCopyText, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnCopyText, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnCopyText.TabIndex = 21;
            this.m_btnCopyText.Click += new System.EventHandler(this.m_btnCopyText_Click);
            // 
            // m_btnOpen
            // 
            this.m_btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOpen.Image")));
            this.m_btnOpen.Location = new System.Drawing.Point(680, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnOpen, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnOpen.Name = "m_btnOpen";
            this.m_btnOpen.Size = new System.Drawing.Size(24, 23);
            this.cExtStyle1.SetStyleBackColor(this.m_btnOpen, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnOpen, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOpen.TabIndex = 20;
            this.m_btnOpen.Click += new System.EventHandler(this.m_btnOpen_Click);
            // 
            // m_btnSave
            // 
            this.m_btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnSave.Image = ((System.Drawing.Image)(resources.GetObject("m_btnSave.Image")));
            this.m_btnSave.Location = new System.Drawing.Point(656, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnSave, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnSave.Name = "m_btnSave";
            this.m_btnSave.Size = new System.Drawing.Size(24, 23);
            this.cExtStyle1.SetStyleBackColor(this.m_btnSave, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnSave, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnSave.TabIndex = 19;
            this.m_btnSave.Click += new System.EventHandler(this.m_btnSave_Click);
            // 
            // m_btnPaste
            // 
            this.m_btnPaste.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnPaste.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnPaste.Image = ((System.Drawing.Image)(resources.GetObject("m_btnPaste.Image")));
            this.m_btnPaste.Location = new System.Drawing.Point(624, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnPaste, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnPaste.Name = "m_btnPaste";
            this.m_btnPaste.Size = new System.Drawing.Size(24, 23);
            this.cExtStyle1.SetStyleBackColor(this.m_btnPaste, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnPaste, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnPaste.TabIndex = 18;
            this.m_btnPaste.Click += new System.EventHandler(this.m_btnPaste_Click);
            // 
            // m_btnCopy
            // 
            this.m_btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("m_btnCopy.Image")));
            this.m_btnCopy.Location = new System.Drawing.Point(600, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnCopy, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnCopy.Name = "m_btnCopy";
            this.m_btnCopy.Size = new System.Drawing.Size(24, 23);
            this.cExtStyle1.SetStyleBackColor(this.m_btnCopy, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnCopy, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnCopy.TabIndex = 17;
            this.m_btnCopy.Click += new System.EventHandler(this.m_btnCopy_Click);
            // 
            // m_menuArbre
            // 
            this.m_menuArbre.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuAjouter,
            this.m_menuInsert,
            this.m_menuSupprimer,
            this.m_menuProprietes,
            this.m_menuCopierComposant,
            this.m_menuCollerComposant});
            // 
            // m_menuAjouter
            // 
            this.m_menuAjouter.Index = 0;
            this.m_menuAjouter.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuAddEt,
            this.m_menuAddOu,
            this.m_menuAddCondition,
            this.m_menuEstNull,
            this.m_menuAjouterSelection,
            this.m_menuRechercheAvancee,
            this.m_menuSousFiltre});
            this.m_menuAjouter.Text = "Add";
            // 
            // m_menuAddEt
            // 
            this.m_menuAddEt.Index = 0;
            this.m_menuAddEt.Text = "AND group|271";
            this.m_menuAddEt.Click += new System.EventHandler(this.m_menuAddEt_Click);
            // 
            // m_menuAddOu
            // 
            this.m_menuAddOu.Index = 1;
            this.m_menuAddOu.Text = "OR group|272";
            this.m_menuAddOu.Click += new System.EventHandler(this.m_menuAddOu_Click);
            // 
            // m_menuAddCondition
            // 
            this.m_menuAddCondition.Index = 2;
            this.m_menuAddCondition.Text = "Simple condition|273";
            this.m_menuAddCondition.Click += new System.EventHandler(this.m_menuAddCondition_Click);
            // 
            // m_menuEstNull
            // 
            this.m_menuEstNull.Index = 3;
            this.m_menuEstNull.Text = "Nullity test|274";
            this.m_menuEstNull.Click += new System.EventHandler(this.m_menuEstNull_Click);
            // 
            // m_menuAjouterSelection
            // 
            this.m_menuAjouterSelection.Index = 4;
            this.m_menuAjouterSelection.Text = "Selection|275";
            this.m_menuAjouterSelection.Click += new System.EventHandler(this.m_menuAjouterSelection_Click);
            // 
            // m_menuRechercheAvancee
            // 
            this.m_menuRechercheAvancee.Index = 5;
            this.m_menuRechercheAvancee.Text = "Advanced search|111";
            this.m_menuRechercheAvancee.Visible = false;
            this.m_menuRechercheAvancee.Click += new System.EventHandler(this.m_menuRechercheAvancee_Click);
            // 
            // m_menuSousFiltre
            // 
            this.m_menuSousFiltre.Index = 6;
            this.m_menuSousFiltre.Text = "Sub filter|20098";
            this.m_menuSousFiltre.Click += new System.EventHandler(this.m_menuSousFiltre_Click);
            // 
            // m_menuInsert
            // 
            this.m_menuInsert.Index = 1;
            this.m_menuInsert.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuInsererEt,
            this.m_menuInsererOu});
            this.m_menuInsert.Text = "Insert|245";
            // 
            // m_menuInsererEt
            // 
            this.m_menuInsererEt.Index = 0;
            this.m_menuInsererEt.Text = "AND group|271";
            this.m_menuInsererEt.Click += new System.EventHandler(this.m_menuInsererEt_Click);
            // 
            // m_menuInsererOu
            // 
            this.m_menuInsererOu.Index = 1;
            this.m_menuInsererOu.Text = "OR group|272";
            this.m_menuInsererOu.Click += new System.EventHandler(this.m_menuInsererOu_Click);
            // 
            // m_menuSupprimer
            // 
            this.m_menuSupprimer.Index = 2;
            this.m_menuSupprimer.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuSupprimerElementEtFils,
            this.m_menuDecalerFilsVersLeHaut});
            this.m_menuSupprimer.Text = "Delete|276";
            // 
            // m_menuSupprimerElementEtFils
            // 
            this.m_menuSupprimerElementEtFils.Index = 0;
            this.m_menuSupprimerElementEtFils.Text = "The element and its children|277";
            this.m_menuSupprimerElementEtFils.Click += new System.EventHandler(this.m_menuSupprimerElementEtFils_Click);
            // 
            // m_menuDecalerFilsVersLeHaut
            // 
            this.m_menuDecalerFilsVersLeHaut.Index = 1;
            this.m_menuDecalerFilsVersLeHaut.Text = "Shift the children upwards|278";
            this.m_menuDecalerFilsVersLeHaut.Click += new System.EventHandler(this.m_menuDecalerFilsVersLeHaut_Click);
            // 
            // m_menuProprietes
            // 
            this.m_menuProprietes.Index = 3;
            this.m_menuProprietes.Text = "Properties|279";
            this.m_menuProprietes.Click += new System.EventHandler(this.m_menuProprietes_Click);
            // 
            // m_menuCopierComposant
            // 
            this.m_menuCopierComposant.Index = 4;
            this.m_menuCopierComposant.Text = "Copy|280";
            this.m_menuCopierComposant.Click += new System.EventHandler(this.m_menuCopierComposant_Click);
            // 
            // m_menuCollerComposant
            // 
            this.m_menuCollerComposant.Index = 5;
            this.m_menuCollerComposant.Text = "Paste|281";
            this.m_menuCollerComposant.Click += new System.EventHandler(this.m_menuCollerComposant_Click);
            // 
            // m_menuNewVariable
            // 
            this.m_menuNewVariable.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuVariableSaisie,
            this.m_menuNewVariableCalculée,
            this.m_menuNewVariableSelection,
            this.m_menuNewListeObjets});
            // 
            // m_menuVariableSaisie
            // 
            this.m_menuVariableSaisie.Index = 0;
            this.m_menuVariableSaisie.Text = "Entered|30010";
            this.m_menuVariableSaisie.Click += new System.EventHandler(this.m_menuVariableSaisie_Click);
            // 
            // m_menuNewVariableCalculée
            // 
            this.m_menuNewVariableCalculée.Index = 1;
            this.m_menuNewVariableCalculée.Text = "Computed|30011";
            this.m_menuNewVariableCalculée.Click += new System.EventHandler(this.m_menuNewVariableCalculée_Click);
            // 
            // m_menuNewVariableSelection
            // 
            this.m_menuNewVariableSelection.Index = 2;
            this.m_menuNewVariableSelection.Text = "Element selection|30012";
            this.m_menuNewVariableSelection.Click += new System.EventHandler(this.m_menuNewVariableSelection_Click);
            // 
            // m_menuNewListeObjets
            // 
            this.m_menuNewListeObjets.Index = 3;
            this.m_menuNewListeObjets.Text = "Object list|30054";
            this.m_menuNewListeObjets.Click += new System.EventHandler(this.m_menuNewListeObjets_Click);
            // 
            // CPanelEditFiltreDynamique
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_tab);
            this.Controls.Add(this.m_panelBas);
            this.Controls.Add(this.m_panelTypeElements);
            this.m_gestionnaireModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CPanelEditFiltreDynamique";
            this.Size = new System.Drawing.Size(712, 376);
            this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.Load += new System.EventHandler(this.CPanelEditFiltreDynamique_Load);
            this.BackColorChanged += new System.EventHandler(this.CPanelEditFiltreDynamique_BackColorChanged);
            this.Enter += new System.EventHandler(this.CPanelEditFiltreDynamique_Enter);
            this.m_tab.ResumeLayout(false);
            this.m_tab.PerformLayout();
            this.m_pageFiltre.ResumeLayout(false);
            this.m_panelMenuOption.ResumeLayout(false);
            this.m_panelMenuOption.PerformLayout();
            this.m_pageChamps.ResumeLayout(false);
            this.m_pageFormulaire.ResumeLayout(false);
            this.m_panelTypeElements.ResumeLayout(false);
            this.m_panelBas.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		public void Init ( CFiltreDynamique filtre )
		{
			m_filtreDynamique = filtre;
			if ( m_bIsLoad )
				InitFenetre();
		}

		public void InitSansVariables ( CFiltreDynamique filtre )
		{
			m_filtreDynamique = filtre;
			m_bModeSansVariables = true;
			if ( m_bIsLoad )
				InitFenetre();
		}

        /// <summary>
        /// Indique si c'est un filtre convertit en formule ou en filtre data
        /// </summary>
        public bool ModeFiltreExpression
        {
            get
            {
                return m_fournisseurProprietesFiltrees is CFournisseurPropDynStd;
            }
            set
            {
                if (value)
                    m_fournisseurProprietesFiltrees = new CFournisseurPropDynStd();
                else
                    m_fournisseurProprietesFiltrees = new CFournisseurProprietesForFiltreDynamique();
            }
        }

		public void MasquerFormulaire( bool bMasquer)
		{
			if ( bMasquer && m_tab.TabPages.Contains ( m_pageFormulaire ) )
				m_tab.TabPages.Remove ( m_pageFormulaire );
			if ( !bMasquer && !m_tab.TabPages.Contains ( m_pageFormulaire ) )
				m_tab.TabPages.Add ( m_pageFormulaire );
		}

		private void CPanelEditFiltreDynamique_Load(object sender, System.EventArgs e)
		{
			m_bIsLoad = true;
			if ( !DesignMode )
			{
				try//Sinon pbs au mode design
				{
					m_panelEditionFormulaire.Init(typeof(CFiltreDynamique), m_filtreDynamique, m_filtreDynamique);
					m_panelEditionFormulaire.Selection.SelectionChanged += new EventHandler(SelectionChanged);
					m_listeControles.AddAllLoadedAssemblies();
					m_listeControles.SetTypeEdite(typeof(CFiltreDynamique));
					m_listeControles.RefreshControls();
					InitFenetre();
				}
				catch{}
			}
		}

		/// <summary>
		/// /////////////////////////////////////////
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void InitFenetre()
		{
			if ( m_bModeSansVariables )
			{
				while ( m_tab.TabPages.Count > 1 )
					m_tab.TabPages.RemoveAt ( 1 );
			}
			//Remplit l'arbre
			FillArbre();
			InitComboBoxType();
			if (m_filtreDynamique != null && m_filtreDynamique.TypeElements != null)
			{
				m_cmbTypeElements.SelectedValue = m_filtreDynamique.TypeElements;
			}
			else
				m_cmbTypeElements.SelectedIndex = 0;
			
			m_panelEditionFormulaire.Selection.SelectionChanged += new EventHandler(SelectionChanged);
			if ( m_filtreDynamique != null )
				m_panelEditionFormulaire.ObjetEdite = m_filtreDynamique.FormulaireEdition;
			m_panelEditionFormulaire.Init(typeof(CFiltreDynamique), m_filtreDynamique, m_filtreDynamique);
			RefillListeVariables();
            
			
//			CProprieteVariableFiltreDynamiqueEditor.SetEditeur ( new CSelectionneurVariableAInterfaceUtilisateur(m_filtreDynamique) );
		}

       
		/// /////////////////////////////////////////
		protected void FillArbre()
		{
			m_arbreFiltre.Nodes.Clear();
			if(  m_filtreDynamique == null )
				return;
			CreateNode ( m_arbreFiltre.Nodes, m_filtreDynamique.ComposantPrincipal );
		}

		/// /////////////////////////////////////////
		protected void CreateNode ( TreeNodeCollection nodes, CComposantFiltreDynamique composant )
		{
			if ( composant == null )
				return;
			TreeNode node = nodes.Add ( composant.Description );
			UpdateNode ( node, composant );
		}

		/// /////////////////////////////////////////
		private void UpdateNode ( TreeNode node, CComposantFiltreDynamique composant )
		{
			node.Tag = composant;
			node.Text = composant.Description;
			node.Nodes.Clear();
			foreach ( CComposantFiltreDynamique fils in composant.ListeComposantsFils )
				CreateNode ( node.Nodes, fils );
			node.Expand();
		}


		/// /////////////////////////////////////////
		private CComposantFiltreDynamique GetComposantForNode ( TreeNode node )
		{
			if ( node == null )
				return null;
			return ( CComposantFiltreDynamique ) node.Tag;
		}

        //--------------------------------------------------------------------------------------------
        private void AddChild(Type typeComposant)
		{
		    CComposantFiltreDynamique composantFils = (CComposantFiltreDynamique)Activator.CreateInstance(typeComposant);
            AddChild(composantFils, true);
		}

        //--------------------------------------------------------------------------------------------
        private void AddChild(CComposantFiltreDynamique composantFils, bool bEditerAvantAjout)
        {
            CComposantFiltreDynamique composant = GetComposantForNode(m_lastNodeSelectionne);
            if (bEditerAvantAjout && !EditeElement(composantFils))
                return;
            if (composant != null)
            {
                if (!composant.AddComposantFils(composantFils))
                    CFormAlerte.Afficher(I.T("Impossible to add at this level|30039"), EFormAlerteType.Erreur);
            }
            else
            {
                if (m_filtreDynamique.ComposantPrincipal == null)
                    m_filtreDynamique.ComposantPrincipal = composantFils;
                else
                    CFormAlerte.Afficher(I.T("Impossible to add|30040"), EFormAlerteType.Erreur);
            }
            if (m_lastNodeSelectionne != null)
                UpdateNode(m_lastNodeSelectionne, composant);
            else
                FillArbre();
        }

		/// /////////////////////////////////////////
		private bool EditeElement ( CComposantFiltreDynamique composant )
		{
			bool bRetour = true;
			if ( composant is CComposantFiltreDynamiqueValeurChamp )
			{
				bRetour = CFormEditComposantFiltreValeurChamp.EditeComposantValeurChamp ( (CComposantFiltreDynamiqueValeurChamp)composant, m_filtreDynamique, !m_bModeSansVariables, m_definitionRacinePourChampsFiltres, m_fournisseurProprietesFiltrees );
			}
			if ( composant is CComposantFiltreDynamiqueTestNull )
			{
				bRetour = CFormEditComposantFiltreTestNull.EditeComposant ( (CComposantFiltreDynamiqueTestNull)composant, m_filtreDynamique, m_fournisseurProprietesFiltrees );
			}
			if ( composant is CComposantFiltreDynamiqueSelectionStatique )
			{
				bRetour = CFormEditComposantFiltreSelectionStatique.EditeComposant ( (CComposantFiltreDynamiqueSelectionStatique)composant, m_filtreDynamique );
			}
			if ( composant is CComposantFiltreDynamiqueRechercheAvancee )
			{
				bRetour = CFormEditComposantFiltreRechercheAvancee.EditeComposantRechercheAvancee ( (CComposantFiltreDynamiqueRechercheAvancee)composant, m_filtreDynamique, !m_bModeSansVariables, m_definitionRacinePourChampsFiltres );
			}
            if (composant is CComposantFiltreDynamiqueSousFiltre)
            {
                bRetour = CFormEditComposantFiltreSousFiltre.EditeComposantSousFiltre(
                    (CComposantFiltreDynamiqueSousFiltre)composant,
                    m_filtreDynamique, 
                    !m_bModeSansVariables, 
                    m_definitionRacinePourChampsFiltres,
                    m_fournisseurProprietesFiltrees);
            }
			RefillListeVariables();
			return bRetour;
		}

		/// /////////////////////////////////////////
		private void Insert ( Type typeComposant )
		{
			CComposantFiltreDynamique composant = GetComposantForNode ( m_lastNodeSelectionne );
			if ( composant == null )
				return;
			TreeNode nodeParent = m_lastNodeSelectionne.Parent;
			CComposantFiltreDynamique composantParent = GetComposantForNode ( nodeParent );
			CComposantFiltreDynamique composantInsere = (CComposantFiltreDynamique)Activator.CreateInstance(typeComposant);
			if ( !composantInsere.AddComposantFils (composant) )
			{
				CFormAlerte.Afficher ( I.T("Impossible to insert|30041") , EFormAlerteType.Erreur);
				return;
			}
			if ( composantParent != null )
			{
				int nIndex = composantParent.RemoveComposantFils ( composant );
				composantParent.InsertComposantFils( composantInsere, nIndex );
				UpdateNode ( nodeParent, composantParent );
			}
			else
			{
				m_filtreDynamique.ComposantPrincipal = composantInsere;
				FillArbre();
			}
		}

		/// /////////////////////////////////////////
		private void m_arbreFiltre_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( (e.Button & MouseButtons.Right) == MouseButtons.Right && m_gestionnaireModeEdition.ModeEdition)
			{
				TreeNode node = m_arbreFiltre.GetNodeAt(e.X, e.Y );
				m_menuInsert.Visible = node != null;
				m_menuSupprimer.Visible = node != null;
				m_menuAjouter.Visible = node != null || m_filtreDynamique.ComposantPrincipal==null;
                m_menuCopierComposant.Visible = node != null;
                m_menuCollerComposant.Visible = node != null || m_filtreDynamique.ComposantPrincipal == null;
                m_menuCollerComposant.Enabled = CSerializerObjetInClipBoard.IsObjetInClipboard(CComposantFiltreDynamique.c_signatureComposant);
                m_lastNodeSelectionne = node;
				if ( m_lastNodeSelectionne != null )
				{
					if ( !GetComposantForNode ( m_lastNodeSelectionne ).AccepteComposantsFils )
						m_menuAjouter.Visible = false;
				}
                m_menuProprietes.Visible =
                    GetComposantForNode(node) is CComposantFiltreDynamiqueValeurChamp ||
                    GetComposantForNode(node) is CComposantFiltreDynamiqueTestNull ||
                    GetComposantForNode(node) is CComposantFiltreDynamiqueSelectionStatique ||
                    GetComposantForNode(node) is CComposantFiltreDynamiqueRechercheAvancee ||
                    GetComposantForNode(node) is CComposantFiltreDynamiqueSousFiltre;
				m_menuArbre.Show(m_arbreFiltre, new Point ( e.X, e.Y ) );
			}
		}

		/// /////////////////////////////////////////
		private void m_menuAddEt_Click(object sender, System.EventArgs e)
		{
			AddChild ( typeof(CComposantFiltreDynamiqueEt) );
		}

		/// /////////////////////////////////////////
		private void m_menuAddOu_Click(object sender, System.EventArgs e)
		{
			AddChild ( typeof(CComposantFiltreDynamiqueOu) );
		}

		/// /////////////////////////////////////////
		private void m_menuAddCondition_Click(object sender, System.EventArgs e)
		{
			AddChild ( typeof(CComposantFiltreDynamiqueValeurChamp) );
		}

		/// /////////////////////////////////////////
		private void m_menuInsererEt_Click(object sender, System.EventArgs e)
		{
			Insert ( typeof(CComposantFiltreDynamiqueEt) );
		}

		/// /////////////////////////////////////////
		private void m_menuInsererOu_Click(object sender, System.EventArgs e)
		{
			Insert ( typeof(CComposantFiltreDynamiqueOu) );
		}

		/// /////////////////////////////////////////
		private void m_menuSupprimerElementEtFils_Click(object sender, System.EventArgs e)
		{
			CComposantFiltreDynamique composant = GetComposantForNode ( m_lastNodeSelectionne );
			if ( composant == null )
				return;
			if ( CFormAlerte.Afficher (I.T("Are you sure ?|30042"), EFormAlerteType.Question)==DialogResult.Yes )
			{
				CComposantFiltreDynamique composantParent = GetComposantForNode ( m_lastNodeSelectionne.Parent );
				if ( composantParent != null )
				{
					composantParent.RemoveComposantFils ( composant );
					UpdateNode ( m_lastNodeSelectionne.Parent, composantParent );
				}
				else
				{
					m_filtreDynamique.ComposantPrincipal = null;
					FillArbre();
				}
			}
		}

		/// /////////////////////////////////////////
		private void m_menuDecalerFilsVersLeHaut_Click(object sender, System.EventArgs e)
		{
            CComposantFiltreDynamique composant = GetComposantForNode ( m_lastNodeSelectionne );
			if ( composant == null )
				return;
			CComposantFiltreDynamique composantParent = GetComposantForNode ( m_lastNodeSelectionne.Parent );
			if ( composantParent == null && composant.ListeComposantsFils.Length > 1 )
			{
				CFormAlerte.Afficher(I.T("Impossible to shift several children upwards|30043"), EFormAlerteType.Erreur);
				return;
			}
			if ( composantParent !=  null && !composantParent.AccepteComposantsFils )
			{
				CFormAlerte.Afficher(I.T("Impossible to shift elements upwards|30044"), EFormAlerteType.Erreur);
				return;
			}
			if ( CFormAlerte.Afficher (I.T("Are you sure ?|30042"), EFormAlerteType.Question) != DialogResult.Yes )
				return;
			if ( composantParent == null )
			{
				if ( composant.ListeComposantsFils.Length == 1 )
					m_filtreDynamique.ComposantPrincipal = composant.ListeComposantsFils[0];
				else
					m_filtreDynamique.ComposantPrincipal =  null;
				FillArbre();
			}
			else
			{
				foreach ( CComposantFiltreDynamique composantFils in composant.ListeComposantsFils )
				{
					composantParent.AddComposantFils ( composantFils );
					composant.RemoveComposantFils ( composantFils );
				}
				composantParent.RemoveComposantFils ( composant );
				UpdateNode ( m_lastNodeSelectionne.Parent, composantParent );
			}

		}

		
		private CResultAErreur InitComboBoxType()
		{
			CResultAErreur result = CResultAErreur.True;
		
			if (m_bComboInitialized)
				return result;

			//CInfoClasseDynamique[] classes = DynamicClassAttribute.GetAllDynamicClass(typeof(sc2i.data.TableAttribute));
			ArrayList infosClasses = new ArrayList(DynamicClassAttribute.GetAllDynamicClass());
            infosClasses.Insert(0, new CInfoClasseDynamique(typeof(DBNull), I.T("None|30048")));
			m_cmbTypeElements.DataSource = null;
			m_cmbTypeElements.DataSource = infosClasses;
			m_cmbTypeElements.ValueMember = "Classe";
			m_cmbTypeElements.DisplayMember = "Nom";

			m_bComboInitialized = true;
			return result;
		}
		//-------------------------------------------------------------------------
		public CResultAErreur InitChamps()
		{
			CResultAErreur result = CResultAErreur.True;

			result = InitComboBoxType();
			if (!result)
				return result;

			return result;
		}

		//-------------------------------------------------------------------------
		private Type m_oldType = null;
		private void m_cmbType_SelectedValueChanged(object sender, System.EventArgs e)
		{
			if (!m_bComboInitialized)
				return;
			if (m_cmbTypeElements.SelectedValue == null || m_cmbTypeElements.SelectedValue == typeof(DBNull))
				return;
			m_oldType = (Type) m_cmbTypeElements.SelectedValue;
			if ( m_filtreDynamique != null )
				m_filtreDynamique.TypeElements = m_oldType;
            //UpdateVisuPanelOptions();
		}

		//-------------------------------------------------------------------------
		private void m_menuProprietes_Click(object sender, System.EventArgs e)
		{
            TreeNode node = m_lastNodeSelectionne; // m_arbreFiltre.SelectedNode;
            CComposantFiltreDynamique composant = GetComposantForNode(node);
			if ( composant == null )
				return;
			if ( EditeElement ( composant ) )
				node.Text = composant.Description;
		}


		//-------------------------------------------------------------------------
		private void m_btnAjouterChamp_LinkClicked(object sender, System.EventArgs e)
		{
			m_menuNewVariable.Show ( m_btnAjouterChamp, new Point ( 0, m_btnAjouterChamp.Height ) );
		}

		//-------------------------------------------------------------------------
		private void m_menuVariableSaisie_Click(object sender, System.EventArgs e)
		{
			CVariableDynamiqueSaisie variable = new CVariableDynamiqueSaisie(m_filtreDynamique);
			if ( EditeVariable ( variable ) )
			{
                m_filtreDynamique.AddVariable(variable);
				RefillListeVariables();
			}
		}

		//-------------------------------------------------------------------------
		private bool EditeVariable ( CVariableDynamique variable )
		{
			if ( variable == null )
				return false;
			bool bRetour = true;
			if ( variable is CVariableDynamiqueSaisie )
				bRetour = CFormEditVariableDynamiqueSaisie.EditeVariable ( (CVariableDynamiqueSaisie)variable, m_filtreDynamique );
			else if ( variable is CVariableDynamiqueCalculee )
				bRetour = CFormEditVariableFiltreCalculee.EditeVariable ((CVariableDynamiqueCalculee)variable, m_filtreDynamique) ;
			else if ( variable is CVariableDynamiqueSelectionObjetDonnee )
				bRetour = CFormEditVariableDynamiqueSelectionObjetDonnee.EditeVariable ( (CVariableDynamiqueSelectionObjetDonnee)variable);
			else if ( variable is CVariableDynamiqueListeObjets )
				bRetour = CFormEditVariableDynamiqueListeObjets.EditeVariable ( (CVariableDynamiqueListeObjets)variable, m_filtreDynamique );
			else
				bRetour = false;
			return bRetour;
		}

		//-------------------------------------------------------------------------
		private void RefillListeVariables()
		{
			if ( m_filtreDynamique == null )
				return;
			int nItem = -1;
			if ( m_wndListeVariables.SelectedIndices.Count != 0 )
				nItem = m_wndListeVariables.SelectedIndices[0];
			m_wndListeVariables.Remplir ( m_filtreDynamique.ListeVariables );
			try
			{
				m_wndListeVariables.Items[nItem].Selected = true;
			}
			catch{}
		}

		//-------------------------------------------------------------------------
		private void m_btnModifierChamp_LinkClicked(object sender, System.EventArgs e)
		{
			if ( EditeVariable ( VariableSelectionnee ) )
			{
				RefillListeVariables();
			}
		}

		//-------------------------------------------------------------------------
		private void m_wndListeVariables_DoubleClick(object sender, System.EventArgs e)
		{
			if ( EditeVariable ( VariableSelectionnee ) )
				RefillListeVariables();
		}

		//-------------------------------------------------------------------------
		private void m_menuNewVariableCalculée_Click(object sender, System.EventArgs e)
		{
			CVariableDynamiqueCalculee variable = new CVariableDynamiqueCalculee(m_filtreDynamique);
			if ( EditeVariable ( variable ) )
			{
                m_filtreDynamique.AddVariable(variable);
				RefillListeVariables();
			}
		}

		//-------------------------------------------------------------------------
		private void m_propertyGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			m_panelEditionFormulaire.Refresh();
		}

		//-------------------------------------------------------------------------
		private CVariableDynamique VariableSelectionnee
		{
			get
			{
				if ( m_wndListeVariables.SelectedItems.Count == 0 )
					return null;
				return (CVariableDynamique) m_wndListeVariables.SelectedItems[0].Tag;
			}
		}

		//-------------------------------------------------------------------------
		private void SelectionChanged ( object sender, EventArgs args )
		{
			if ( m_panelEditionFormulaire.Selection.Count == 1 )
				m_propertyGrid.SelectedObject = m_panelEditionFormulaire.Selection[0];
			else
				m_propertyGrid.SelectedObject = null;
		}

		//-------------------------------------------------------------------------
		private void m_tab_SelectionChanged(object sender, System.EventArgs e)
		{
		
		}

		//-------------------------------------------------------------------------
		private void m_btnSupprimerChamp_LinkClicked(object sender, System.EventArgs e)
		{
			CVariableDynamique variable = VariableSelectionnee;
			if ( m_filtreDynamique.IsVariableUtilisee ( variable ) )
			{
				CFormAlerte.Afficher(I.T("Impossible to delete this variabe because it is used|264"), EFormAlerteType.Erreur);
				return;
			}
			m_filtreDynamique.RemoveVariable ( variable );
			RefillListeVariables();
		}

		//-------------------------------------------------------------------------
		public CFiltreDynamique FiltreDynamique
		{
			get
			{
				return m_filtreDynamique;
			}
			set
			{
				m_filtreDynamique = value;
				InitChamps();
			}
		}

		//-------------------------------------------------------------------------
		public event ChangeTypeElementsEventHandler OnChangeTypeElements;
 
		//-------------------------------------------------------------------------
		private void m_cmbTypeElements_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if ( OnChangeTypeElements != null && m_cmbTypeElements.SelectedValue != typeof(DBNull))
			{
				OnChangeTypeElements ( m_cmbTypeElements, (Type)m_cmbTypeElements.SelectedValue );
			}
            //UpdateVisuPanelOptions();

		}

		//-------------------------------------------------------------------------
		private void m_menuNewVariableSelection_Click(object sender, System.EventArgs e)
		{
			CVariableDynamiqueSelectionObjetDonnee variable = new CVariableDynamiqueSelectionObjetDonnee(m_filtreDynamique);
			if ( EditeVariable ( variable ) )
			{
                m_filtreDynamique.AddVariable(variable);
				RefillListeVariables();
			}
		
		}

		//-------------------------------------------------------------------------
		private void CPanelEditFiltreDynamique_Enter(object sender, System.EventArgs e)
		{
			//CProprieteVariableFiltreDynamiqueEditor.SetEditeur ( new CSelectionneurVariableAInterfaceUtilisateur(m_filtreDynamique) );
		}

		private void m_btnSave_Click(object sender, System.EventArgs e)
		{
			SaveFileDialog dlg = new SaveFileDialog ();
			dlg.Filter = I.T("Dynamic filter (*.2iFilter)|*.2iFilter|All files (*.*)|*.*|30049");
			if ( dlg.ShowDialog()==DialogResult.OK )
			{
				string strNomFichier = dlg.FileName;
				CResultAErreur result = CSerializerObjetInFile.SaveToFile ( 
					m_filtreDynamique,
					CFiltreDynamique.c_idFichier,
					strNomFichier );
				if ( !result )
					CFormAlerte.Afficher ( result);
				else
					CFormAlerte.Afficher (I.T("Save successful|260"));
			}
		}

		private void m_btnOpen_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = I.T("Dynamic filter (*.2iFilter)|*.2iFilter|All files (*.*)|*.*|30049");
			if ( dlg.ShowDialog()==DialogResult.OK )
			{
				if ( CFormAlerte.Afficher(I.T("Current filter data will be replaced, continue ?|30045"), 
					EFormAlerteType.Question) == DialogResult.No )
					return;
				CFiltreDynamique newFiltre = new CFiltreDynamique(m_filtreDynamique.ContexteDonnee);
				CResultAErreur result = CSerializerObjetInFile.ReadFromFile ( newFiltre, CFiltreDynamique.c_idFichier, dlg.FileName );
				if ( !result )
					CFormAlerte.Afficher(result);
				else
				{
					CCloner2iSerializable.CopieTo ( newFiltre, m_filtreDynamique );
					Init ( m_filtreDynamique );
				}
			}
		}

		private void m_menuEstNull_Click(object sender, System.EventArgs e)
		{
			AddChild ( typeof(CComposantFiltreDynamiqueTestNull) );
		}

		private void m_menuAjouterSelection_Click(object sender, System.EventArgs e)
		{
			AddChild ( typeof ( CComposantFiltreDynamiqueSelectionStatique ) );
		}

		private void m_menuNewListeObjets_Click(object sender, System.EventArgs e)
		{
			CVariableDynamiqueListeObjets variable = new CVariableDynamiqueListeObjets(m_filtreDynamique);
			if ( EditeVariable ( variable ) )
			{
                m_filtreDynamique.AddVariable(variable);
				RefillListeVariables();
			}
		}

		private void m_btnCopy_Click(object sender, System.EventArgs e)
		{
			CResultAErreur result = CSerializerObjetInClipBoard.Copy ( FiltreDynamique, CFiltreDynamique.c_idFichier );
			if ( !result) 
			{
				CFormAlerte.Afficher ( result);
			}
		}

		private void m_btnPaste_Click(object sender, System.EventArgs e)
		{
			I2iSerializable objet = null;
			CResultAErreur result = CSerializerObjetInClipBoard.Paste ( ref objet, CFiltreDynamique.c_idFichier );
			if ( !result )
			{
				CFormAlerte.Afficher(result);
				return;
			}
            if (CFormAlerte.Afficher(I.T("Current filter data will be replaced, continue ?|30045"), 
				EFormAlerteType.Question) == DialogResult.No )
				return;
			CCloner2iSerializable.CopieTo ( objet, m_filtreDynamique );
			Init ( m_filtreDynamique );
		}

		private void m_panelBas_BackColorChanged(object sender, System.EventArgs e)
		{
			m_tab.BackColor = BackColor;
		}

		private void CPanelEditFiltreDynamique_BackColorChanged(object sender, System.EventArgs e)
		{

		}

		private void m_menuRechercheAvancee_Click(object sender, System.EventArgs e)
		{
			AddChild ( typeof ( CComposantFiltreDynamiqueRechercheAvancee ) );
		}

		//-------------------------------------------------------------------------
		public event EventHandler OnChangeLockEdition; 
		public bool LockEdition
		{
			get
			{
				return !m_gestionnaireModeEdition.ModeEdition;
			}
			set
			{
				m_gestionnaireModeEdition.ModeEdition = !value;
				if ( OnChangeLockEdition != null )
					OnChangeLockEdition(this, new EventArgs());
			}
		}

		//-------------------------------------------------------------------------
		public bool ModeSansType
		{
			get
			{
				return m_bModeSansType;
			}
			set
			{
				m_bModeSansType = value;
					m_panelTypeElements.Visible = !m_bModeSansType;
/*				if ( m_bModeSansType )
				{
					m_gestionnaireModeEdition.SetModeEdition ( m_cmbTypeElements, TypeModeEdition.Autonome);
					m_cmbTypeElements.LockEdition = true;
				}
				else
				{
					m_gestionnaireModeEdition.SetModeEdition ( m_cmbTypeElements, TypeModeEdition.EnableSurEdition);
					m_cmbTypeElements.LockEdition = !m_gestionnaireModeEdition.ModeEdition;
				}*/
			}
		}

		private void m_btnCopyText_Click(object sender, EventArgs e)
		{
			CResultAErreur result = FiltreDynamique.GetFiltreData();
			if (result)
				Clipboard.SetText(((CFiltreData)result.Data).Filtre);
			else
				Clipboard.SetText(I.T("No filter data|30046"));
        }


        //------------------------------------------------------------------------------
        private void m_menuCopierComposant_Click(object sender, EventArgs e)
        {
            CComposantFiltreDynamique composant = GetComposantForNode(m_lastNodeSelectionne);
            CResultAErreur result = CSerializerObjetInClipBoard.Copy(composant, CComposantFiltreDynamique.c_signatureComposant);
            if (!result)
            {
                CFormAlerte.Afficher(result);
            }

        }

        //------------------------------------------------------------------------------
        private void m_menuCollerComposant_Click(object sender, EventArgs e)
        {
            I2iSerializable objet = null;
            CResultAErreur result = CSerializerObjetInClipBoard.Paste(ref objet, CComposantFiltreDynamique.c_signatureComposant);
            if (!result)
            {
                CFormAlerte.Afficher(result);
                return;
            }

            CComposantFiltreDynamique composantAColler = objet as CComposantFiltreDynamique;
            if (composantAColler == null)
            {
                result.EmpileErreur(I.T("The object to paste is not a dynamic filter component|10000"));
                CFormAlerte.Afficher(result);
                return;
            }
            // Ajoute le composant à coller
            AddChild(composantAColler, false);

        }

        //------------------------------------------------------------------------------
        private void m_arbreFiltre_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Edite les proprietes au double clic
            TreeNode node = e.Node;
            if (!LockEdition && node != null)
            {
                CComposantFiltreDynamique composant = GetComposantForNode(node);
                if (composant == null)
                    return;
                if (EditeElement(composant))
                    node.Text = composant.Description;
            }
        }


        private void m_lnkOptions_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CFormOptionsFiltreDynamique.EditeOptions(m_filtreDynamique);
        }

        private void m_menuSousFiltre_Click(object sender, EventArgs e)
        {
            AddChild(typeof(CComposantFiltreDynamiqueSousFiltre));
        }

	}
}
