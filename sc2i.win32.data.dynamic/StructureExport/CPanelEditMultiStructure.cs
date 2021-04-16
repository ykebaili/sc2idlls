using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Threading;

using sc2i.data.dynamic;
using sc2i.common;
using sc2i.formulaire;
using sc2i.win32.common;
using sc2i.formulaire.win32.editor;
using sc2i.expression;
using sc2i.data.dynamic.EasyQuery;
using sc2i.win32.data.dynamic.EasyQuery;

namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Description résumée de CPanelEditMultiStructure.
	/// </summary>
	public class CPanelEditMultiStructure : System.Windows.Forms.UserControl , IControlALockEdition
	{
		private bool m_bIsLoad = false;
        private bool m_bModeSansVariables = false;
		private CMultiStructureExport m_multiStructure = null;
		private sc2i.win32.common.CExtModeEdition m_gestionnaireModeEdition;
		private sc2i.win32.common.C2iTabControl m_tab;
		private Crownwood.Magic.Controls.TabPage m_pageFiltre;
		private Crownwood.Magic.Controls.TabPage m_pageChamps;
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
		private sc2i.win32.common.CWndLinkStd m_lnkAjouterStructure;
		private sc2i.win32.common.CWndLinkStd m_lnkModifierStructure;
		private sc2i.win32.common.CWndLinkStd m_lnkSupprimerStructure;
		private sc2i.win32.common.ListViewAutoFilled m_wndListeStructures;
		private sc2i.win32.common.ListViewAutoFilledColumn listViewAutoFilledColumn1;
		private System.Windows.Forms.ContextMenu m_menuNewStructure;
		private System.Windows.Forms.MenuItem m_menuAddStructureExport;
		private System.Windows.Forms.MenuItem m_menuAddRequete;
		private System.Windows.Forms.LinkLabel m_lnkVoirResultat;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Button m_btnOpen;
		private System.Windows.Forms.Button m_btnSave;
		private System.Windows.Forms.Button m_btnPaste;
		private System.Windows.Forms.Button m_btnCopy;
        protected CExtStyle cExtStyle1;
        private MenuItem m_menuAddEasyQuery;
		private System.ComponentModel.IContainer components;

		
		public CPanelEditMultiStructure()
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			// TODO : ajoutez les initialisations après l'appel à InitForm

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

		#region Component Designer generated code
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPanelEditMultiStructure));
            sc2i.win32.common.CGrilleEditeurObjetGraphique cGrilleEditeurObjetGraphique1 = new sc2i.win32.common.CGrilleEditeurObjetGraphique();
            sc2i.win32.common.CProfilEditeurObjetGraphique cProfilEditeurObjetGraphique1 = new sc2i.win32.common.CProfilEditeurObjetGraphique();
            this.m_gestionnaireModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_tab = new sc2i.win32.common.C2iTabControl(this.components);
            this.m_pageFiltre = new Crownwood.Magic.Controls.TabPage();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.m_lnkVoirResultat = new System.Windows.Forms.LinkLabel();
            this.m_wndListeStructures = new sc2i.win32.common.ListViewAutoFilled();
            this.listViewAutoFilledColumn1 = ((sc2i.win32.common.ListViewAutoFilledColumn)(new sc2i.win32.common.ListViewAutoFilledColumn()));
            this.m_lnkSupprimerStructure = new sc2i.win32.common.CWndLinkStd();
            this.m_lnkModifierStructure = new sc2i.win32.common.CWndLinkStd();
            this.m_lnkAjouterStructure = new sc2i.win32.common.CWndLinkStd();
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
            this.m_btnOpen = new System.Windows.Forms.Button();
            this.m_btnSave = new System.Windows.Forms.Button();
            this.m_btnPaste = new System.Windows.Forms.Button();
            this.m_btnCopy = new System.Windows.Forms.Button();
            this.m_menuNewVariable = new System.Windows.Forms.ContextMenu();
            this.m_menuVariableSaisie = new System.Windows.Forms.MenuItem();
            this.m_menuNewVariableCalculée = new System.Windows.Forms.MenuItem();
            this.m_menuNewVariableSelection = new System.Windows.Forms.MenuItem();
            this.m_menuNewStructure = new System.Windows.Forms.ContextMenu();
            this.m_menuAddStructureExport = new System.Windows.Forms.MenuItem();
            this.m_menuAddRequete = new System.Windows.Forms.MenuItem();
            this.m_menuAddEasyQuery = new System.Windows.Forms.MenuItem();
            this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.m_tab.SuspendLayout();
            this.m_pageFiltre.SuspendLayout();
            this.m_pageChamps.SuspendLayout();
            this.m_pageFormulaire.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_tab
            // 
            this.m_tab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_tab.BoldSelectedPage = true;
            this.m_tab.IDEPixelArea = false;
            this.m_tab.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_tab, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_tab.Name = "m_tab";
            this.m_tab.Ombre = false;
            this.m_tab.PositionTop = true;
            this.m_tab.SelectedIndex = 0;
            this.m_tab.SelectedTab = this.m_pageFiltre;
            this.m_tab.Size = new System.Drawing.Size(712, 348);
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
            this.m_pageFiltre.Controls.Add(this.linkLabel1);
            this.m_pageFiltre.Controls.Add(this.m_lnkVoirResultat);
            this.m_pageFiltre.Controls.Add(this.m_wndListeStructures);
            this.m_pageFiltre.Controls.Add(this.m_lnkSupprimerStructure);
            this.m_pageFiltre.Controls.Add(this.m_lnkModifierStructure);
            this.m_pageFiltre.Controls.Add(this.m_lnkAjouterStructure);
            this.m_pageFiltre.Location = new System.Drawing.Point(0, 25);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_pageFiltre, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_pageFiltre.Name = "m_pageFiltre";
            this.m_pageFiltre.Size = new System.Drawing.Size(712, 323);
            this.cExtStyle1.SetStyleBackColor(this.m_pageFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_pageFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_pageFiltre.TabIndex = 0;
            this.m_pageFiltre.Title = "Structures|188";
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.Location = new System.Drawing.Point(432, 8);
            this.m_gestionnaireModeEdition.SetModeEdition(this.linkLabel1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(153, 16);
            this.cExtStyle1.SetStyleBackColor(this.linkLabel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.linkLabel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Recalcultate test set|191";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // m_lnkVoirResultat
            // 
            this.m_lnkVoirResultat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lnkVoirResultat.Location = new System.Drawing.Point(608, 8);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkVoirResultat, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lnkVoirResultat.Name = "m_lnkVoirResultat";
            this.m_lnkVoirResultat.Size = new System.Drawing.Size(100, 16);
            this.cExtStyle1.SetStyleBackColor(this.m_lnkVoirResultat, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_lnkVoirResultat, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lnkVoirResultat.TabIndex = 6;
            this.m_lnkVoirResultat.TabStop = true;
            this.m_lnkVoirResultat.Text = "View result|192";
            this.m_lnkVoirResultat.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkVoirResultat_LinkClicked);
            // 
            // m_wndListeStructures
            // 
            this.m_wndListeStructures.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_wndListeStructures.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.listViewAutoFilledColumn1});
            this.m_wndListeStructures.EnableCustomisation = true;
            this.m_wndListeStructures.FullRowSelect = true;
            this.m_wndListeStructures.HideSelection = false;
            this.m_wndListeStructures.Location = new System.Drawing.Point(8, 32);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_wndListeStructures, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_wndListeStructures.MultiSelect = false;
            this.m_wndListeStructures.Name = "m_wndListeStructures";
            this.m_wndListeStructures.Size = new System.Drawing.Size(696, 284);
            this.cExtStyle1.SetStyleBackColor(this.m_wndListeStructures, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_wndListeStructures, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListeStructures.TabIndex = 5;
            this.m_wndListeStructures.UseCompatibleStateImageBehavior = false;
            this.m_wndListeStructures.View = System.Windows.Forms.View.Details;
            this.m_wndListeStructures.DoubleClick += new System.EventHandler(this.m_wndListeStructures_DoubleClick);
            // 
            // listViewAutoFilledColumn1
            // 
            this.listViewAutoFilledColumn1.Field = "Libelle";
            this.listViewAutoFilledColumn1.PrecisionWidth = 0D;
            this.listViewAutoFilledColumn1.ProportionnalSize = false;
            this.listViewAutoFilledColumn1.Text = "Label|50";
            this.listViewAutoFilledColumn1.Visible = true;
            this.listViewAutoFilledColumn1.Width = 421;
            // 
            // m_lnkSupprimerStructure
            // 
            this.m_lnkSupprimerStructure.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkSupprimerStructure.CustomImage = ((System.Drawing.Image)(resources.GetObject("m_lnkSupprimerStructure.CustomImage")));
            this.m_lnkSupprimerStructure.CustomText = "Remove";
            this.m_lnkSupprimerStructure.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkSupprimerStructure.Location = new System.Drawing.Point(192, 8);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkSupprimerStructure, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_lnkSupprimerStructure.Name = "m_lnkSupprimerStructure";
            this.m_lnkSupprimerStructure.ShortMode = false;
            this.m_lnkSupprimerStructure.Size = new System.Drawing.Size(88, 16);
            this.cExtStyle1.SetStyleBackColor(this.m_lnkSupprimerStructure, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_lnkSupprimerStructure, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lnkSupprimerStructure.TabIndex = 3;
            this.m_lnkSupprimerStructure.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_lnkSupprimerStructure.LinkClicked += new System.EventHandler(this.m_lnkSupprimerStructure_LinkClicked);
            // 
            // m_lnkModifierStructure
            // 
            this.m_lnkModifierStructure.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkModifierStructure.CustomImage = ((System.Drawing.Image)(resources.GetObject("m_lnkModifierStructure.CustomImage")));
            this.m_lnkModifierStructure.CustomText = "Detail";
            this.m_lnkModifierStructure.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkModifierStructure.Location = new System.Drawing.Point(104, 8);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkModifierStructure, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_lnkModifierStructure.Name = "m_lnkModifierStructure";
            this.m_lnkModifierStructure.ShortMode = false;
            this.m_lnkModifierStructure.Size = new System.Drawing.Size(88, 16);
            this.cExtStyle1.SetStyleBackColor(this.m_lnkModifierStructure, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_lnkModifierStructure, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lnkModifierStructure.TabIndex = 2;
            this.m_lnkModifierStructure.TypeLien = sc2i.win32.common.TypeLinkStd.Modification;
            this.m_lnkModifierStructure.LinkClicked += new System.EventHandler(this.m_lnkModifierStructure_LinkClicked);
            // 
            // m_lnkAjouterStructure
            // 
            this.m_lnkAjouterStructure.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkAjouterStructure.CustomImage = ((System.Drawing.Image)(resources.GetObject("m_lnkAjouterStructure.CustomImage")));
            this.m_lnkAjouterStructure.CustomText = "Add";
            this.m_lnkAjouterStructure.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkAjouterStructure.Location = new System.Drawing.Point(16, 8);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkAjouterStructure, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_lnkAjouterStructure.Name = "m_lnkAjouterStructure";
            this.m_lnkAjouterStructure.ShortMode = false;
            this.m_lnkAjouterStructure.Size = new System.Drawing.Size(88, 16);
            this.cExtStyle1.SetStyleBackColor(this.m_lnkAjouterStructure, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_lnkAjouterStructure, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lnkAjouterStructure.TabIndex = 1;
            this.m_lnkAjouterStructure.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_lnkAjouterStructure.LinkClicked += new System.EventHandler(this.m_lnkAjouterStructure_LinkClicked);
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
            this.m_pageChamps.Size = new System.Drawing.Size(712, 323);
            this.cExtStyle1.SetStyleBackColor(this.m_pageChamps, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_pageChamps, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_pageChamps.TabIndex = 1;
            this.m_pageChamps.Title = "Filter fields|189";
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
            this.m_wndListeVariables.Size = new System.Drawing.Size(696, 284);
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
            this.m_pageFormulaire.Size = new System.Drawing.Size(712, 323);
            this.cExtStyle1.SetStyleBackColor(this.m_pageFormulaire, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_pageFormulaire, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_pageFormulaire.TabIndex = 2;
            this.m_pageFormulaire.Title = "Form|190";
            // 
            // m_panelEditionFormulaire
            // 
            this.m_panelEditionFormulaire.AllowDrop = true;
            this.m_panelEditionFormulaire.AutoScroll = true;
            this.m_panelEditionFormulaire.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_panelEditionFormulaire.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.m_panelEditionFormulaire.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelEditionFormulaire.Echelle = 1F;
            this.m_panelEditionFormulaire.EffetAjoutSuppression = false;
            this.m_panelEditionFormulaire.EffetFonduMenu = true;
            this.m_panelEditionFormulaire.EnDeplacement = false;
            this.m_panelEditionFormulaire.EntiteEditee = null;
            this.m_panelEditionFormulaire.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.m_panelEditionFormulaire.ForeColor = System.Drawing.Color.Black;
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
            this.m_panelEditionFormulaire.Size = new System.Drawing.Size(389, 323);
            this.cExtStyle1.SetStyleBackColor(this.m_panelEditionFormulaire, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.m_panelEditionFormulaire, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.m_panelEditionFormulaire.TabIndex = 3;
            this.m_panelEditionFormulaire.ToujoursAlignerSelonLesControles = true;
            this.m_panelEditionFormulaire.ToujoursAlignerSurLaGrille = false;
            this.m_panelEditionFormulaire.TypeEdite = null;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(533, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.splitter1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 323);
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
            this.m_propertyGrid.Location = new System.Drawing.Point(536, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_propertyGrid, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_propertyGrid.Name = "m_propertyGrid";
            this.m_propertyGrid.Size = new System.Drawing.Size(176, 323);
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
            this.m_listeControles.Size = new System.Drawing.Size(144, 323);
            this.cExtStyle1.SetStyleBackColor(this.m_listeControles, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.m_listeControles, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_listeControles.TabIndex = 4;
            // 
            // m_btnOpen
            // 
            this.m_btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOpen.Image")));
            this.m_btnOpen.Location = new System.Drawing.Point(88, 354);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnOpen, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnOpen.Name = "m_btnOpen";
            this.m_btnOpen.Size = new System.Drawing.Size(24, 23);
            this.cExtStyle1.SetStyleBackColor(this.m_btnOpen, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnOpen, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOpen.TabIndex = 24;
            this.m_btnOpen.Click += new System.EventHandler(this.m_btnOpen_Click);
            // 
            // m_btnSave
            // 
            this.m_btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnSave.Image = ((System.Drawing.Image)(resources.GetObject("m_btnSave.Image")));
            this.m_btnSave.Location = new System.Drawing.Point(64, 354);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnSave, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnSave.Name = "m_btnSave";
            this.m_btnSave.Size = new System.Drawing.Size(24, 23);
            this.cExtStyle1.SetStyleBackColor(this.m_btnSave, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnSave, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnSave.TabIndex = 23;
            this.m_btnSave.Click += new System.EventHandler(this.m_btnSave_Click);
            // 
            // m_btnPaste
            // 
            this.m_btnPaste.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnPaste.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnPaste.Image = ((System.Drawing.Image)(resources.GetObject("m_btnPaste.Image")));
            this.m_btnPaste.Location = new System.Drawing.Point(32, 354);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnPaste, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnPaste.Name = "m_btnPaste";
            this.m_btnPaste.Size = new System.Drawing.Size(24, 23);
            this.cExtStyle1.SetStyleBackColor(this.m_btnPaste, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnPaste, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnPaste.TabIndex = 22;
            this.m_btnPaste.Click += new System.EventHandler(this.m_btnPaste_Click);
            // 
            // m_btnCopy
            // 
            this.m_btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("m_btnCopy.Image")));
            this.m_btnCopy.Location = new System.Drawing.Point(8, 354);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnCopy, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnCopy.Name = "m_btnCopy";
            this.m_btnCopy.Size = new System.Drawing.Size(24, 23);
            this.cExtStyle1.SetStyleBackColor(this.m_btnCopy, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnCopy, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnCopy.TabIndex = 21;
            this.m_btnCopy.Click += new System.EventHandler(this.m_btnCopy_Click);
            // 
            // m_menuNewVariable
            // 
            this.m_menuNewVariable.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuVariableSaisie,
            this.m_menuNewVariableCalculée,
            this.m_menuNewVariableSelection});
            // 
            // m_menuVariableSaisie
            // 
            this.m_menuVariableSaisie.Index = 0;
            this.m_menuVariableSaisie.Text = "Entered data|285";
            this.m_menuVariableSaisie.Click += new System.EventHandler(this.m_menuVariableSaisie_Click);
            // 
            // m_menuNewVariableCalculée
            // 
            this.m_menuNewVariableCalculée.Index = 1;
            this.m_menuNewVariableCalculée.Text = "Calculated|286";
            this.m_menuNewVariableCalculée.Click += new System.EventHandler(this.m_menuNewVariableCalculée_Click);
            // 
            // m_menuNewVariableSelection
            // 
            this.m_menuNewVariableSelection.Index = 2;
            this.m_menuNewVariableSelection.Text = "Elements Selection|287";
            this.m_menuNewVariableSelection.Click += new System.EventHandler(this.m_menuNewVariableSelection_Click);
            // 
            // m_menuNewStructure
            // 
            this.m_menuNewStructure.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuAddStructureExport,
            this.m_menuAddRequete,
            this.m_menuAddEasyQuery});
            // 
            // m_menuAddStructureExport
            // 
            this.m_menuAddStructureExport.Index = 0;
            this.m_menuAddStructureExport.Text = "Export structure|20008";
            this.m_menuAddStructureExport.Click += new System.EventHandler(this.m_menuAddStructureExport_Click);
            // 
            // m_menuAddRequete
            // 
            this.m_menuAddRequete.Index = 1;
            this.m_menuAddRequete.Text = "Query|20010";
            this.m_menuAddRequete.Click += new System.EventHandler(this.m_menuAddRequete_Click);
            // 
            // m_menuAddEasyQuery
            // 
            this.m_menuAddEasyQuery.Index = 2;
            this.m_menuAddEasyQuery.Text = "Easy query|20113";
            this.m_menuAddEasyQuery.Click += new System.EventHandler(this.m_menuAddEasyQuery_Click);
            // 
            // CPanelEditMultiStructure
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_tab);
            this.Controls.Add(this.m_btnOpen);
            this.Controls.Add(this.m_btnSave);
            this.Controls.Add(this.m_btnPaste);
            this.Controls.Add(this.m_btnCopy);
            this.m_gestionnaireModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CPanelEditMultiStructure";
            this.Size = new System.Drawing.Size(712, 391);
            this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.Load += new System.EventHandler(this.CPanelEditMultiStructure_Load);
            this.BackColorChanged += new System.EventHandler(this.CPanelEditMultiStructure_BackColorChanged);
            this.Enter += new System.EventHandler(this.CPanelEditMultiStructure_Enter);
            this.m_tab.ResumeLayout(false);
            this.m_tab.PerformLayout();
            this.m_pageFiltre.ResumeLayout(false);
            this.m_pageChamps.ResumeLayout(false);
            this.m_pageFormulaire.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		public void Init ( CMultiStructureExport multiStructure )
		{
			/*m_multiStructure = multiStructure;
			if ( m_bIsLoad )
				InitFenetre();*/
            Init(multiStructure, false);
		}

        public void Init(CMultiStructureExport multiStructure, bool bModeSansVariables)
        {
            m_multiStructure = multiStructure;
            m_bModeSansVariables = bModeSansVariables;
            if (m_bIsLoad)
                InitFenetre();
			m_jeuEssai = null;
        }

		public CMultiStructureExport MultiStructure
		{
			get
			{
				return m_multiStructure;
			}
		}

		private void CPanelEditMultiStructure_Load(object sender, System.EventArgs e)
		{
			m_bIsLoad = true;
			if ( !DesignMode )
			{
				try//Sinon pbs au mode design
				{
					m_panelEditionFormulaire.Init(typeof(CMultiStructureExport), m_multiStructure, m_multiStructure);
					m_panelEditionFormulaire.Selection.SelectionChanged += new EventHandler(SelectionChanged);
					m_listeControles.AddAllLoadedAssemblies();
					m_listeControles.SetTypeEdite(typeof(CMultiStructureExport));
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
            if (m_bModeSansVariables)
            {
                while (m_tab.TabPages.Count > 1)
                    m_tab.TabPages.RemoveAt(1);
            }
			m_panelEditionFormulaire.Selection.SelectionChanged += new EventHandler(SelectionChanged);
			if (m_multiStructure != null)
			{
				m_panelEditionFormulaire.ObjetEdite = m_multiStructure.Formulaire;
				m_panelEditionFormulaire.Init(typeof(CMultiStructureExport), m_multiStructure, m_multiStructure);
			}
			RefillListeVariables();
			m_wndListeStructures.Remplir(m_multiStructure.Definitions);
			
			//CProprieteVariableFiltreDynamiqueEditor.SetEditeur ( new CSelectionneurVariableAInterfaceUtilisateur(m_multiStructure) );
		}

	
		//-------------------------------------------------------------------------
		public CResultAErreur InitChamps()
		{
			CResultAErreur result = CResultAErreur.True;

			return result;
		}


		//-------------------------------------------------------------------------
		private void m_btnAjouterChamp_LinkClicked(object sender, System.EventArgs e)
		{
			m_menuNewVariable.Show ( m_btnAjouterChamp, new Point ( 0, m_btnAjouterChamp.Height ) );
		}

		//-------------------------------------------------------------------------
		private void m_menuVariableSaisie_Click(object sender, System.EventArgs e)
		{
			CVariableDynamiqueSaisie variable = new CVariableDynamiqueSaisie(m_multiStructure);
			if ( EditeVariable ( variable ) )
			{
                m_multiStructure.AddVariable(variable);
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
				bRetour = CFormEditVariableDynamiqueSaisie.EditeVariable ( (CVariableDynamiqueSaisie)variable, m_multiStructure );
			else if ( variable is CVariableDynamiqueCalculee )
				bRetour = CFormEditVariableFiltreCalculee.EditeVariable ((CVariableDynamiqueCalculee)variable, m_multiStructure) ;
			else if ( variable is CVariableDynamiqueSelectionObjetDonnee )
				bRetour = CFormEditVariableDynamiqueSelectionObjetDonnee.EditeVariable ( (CVariableDynamiqueSelectionObjetDonnee)variable);
			else
				bRetour = false;
			return bRetour;
		}

		//-------------------------------------------------------------------------
		private void RefillListeVariables()
		{
			if ( m_multiStructure == null )
				return;
			int nItem = -1;
			if ( m_wndListeVariables.SelectedIndices.Count != 0 )
				nItem = m_wndListeVariables.SelectedIndices[0];
			m_wndListeVariables.Remplir ( m_multiStructure.ListeVariables );
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
			CVariableDynamiqueCalculee variable = new CVariableDynamiqueCalculee(m_multiStructure);
			if ( EditeVariable ( variable ) )
			{
                m_multiStructure.AddVariable(variable);
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
			if ( m_multiStructure.IsVariableUtilisee ( variable ) )
			{
				CFormAlerte.Afficher(I.T("Impossible to remove this variable because it is in use|265"), EFormAlerteType.Erreur);
				return;
			}
			m_multiStructure.RemoveVariable ( variable );
			RefillListeVariables();
		}

		//-------------------------------------------------------------------------
		public CMultiStructureExport FiltreDynamique
		{
			get
			{
				return m_multiStructure;
			}
			set
			{
				m_multiStructure = value;
				InitChamps();
				m_jeuEssai = null;
			}
		}

		//-------------------------------------------------------------------------
		private void m_menuNewVariableSelection_Click(object sender, System.EventArgs e)
		{
			CVariableDynamiqueSelectionObjetDonnee variable = new CVariableDynamiqueSelectionObjetDonnee(m_multiStructure);
			if ( EditeVariable ( variable ) )
			{
                m_multiStructure.AddVariable(variable);
				RefillListeVariables();
			}
		
		}

		//-------------------------------------------------------------------------
		private void CPanelEditMultiStructure_Enter(object sender, System.EventArgs e)
		{
			//CProprieteVariableFiltreDynamiqueEditor.SetEditeur ( new CSelectionneurVariableAInterfaceUtilisateur(m_multiStructure) );
		}

		//-------------------------------------------------------------------------
		private void m_btnSave_Click(object sender, System.EventArgs e)
		{
			SaveFileDialog dlg = new SaveFileDialog ();
			dlg.Filter = I.T("Multiple structure(*.2imultiStruct)|*.2imultiStruct|All files(*.*)|*.*|30052");
			if ( dlg.ShowDialog()==DialogResult.OK )
			{
				string strNomFichier = dlg.FileName;
				CResultAErreur result = CSerializerObjetInFile.SaveToFile ( 
					m_multiStructure,
					CMultiStructureExport.c_idFichier,
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
            dlg.Filter = I.T("Multiple structure(*.2imultiStruct)|*.2imultiStruct|All files(*.*)|*.*|30052"); 
            if (dlg.ShowDialog() == DialogResult.OK)
			{
                if (CFormAlerte.Afficher(I.T("Current structure data will be replaced.  Continue?|259"),
					EFormAlerteType.Question) == DialogResult.No )
					return;
				CMultiStructureExport newFiltre = new CMultiStructureExport(m_multiStructure.ContexteDonnee);
				CResultAErreur result = CSerializerObjetInFile.ReadFromFile ( newFiltre, CMultiStructureExport.c_idFichier, dlg.FileName );
				if ( !result )
					CFormAlerte.Afficher(result);
				else
				{
					m_multiStructure = newFiltre;
					Init ( m_multiStructure );
				}
			}
		}

		//-------------------------------------------------------------------------
		private void m_lnkAjouterStructure_LinkClicked(object sender, System.EventArgs e)
		{
			m_menuNewStructure.Show ( m_lnkAjouterStructure, new Point ( 0, m_lnkAjouterStructure.Height ) );
		}

		//-------------------------------------------------------------------------
		private void m_menuAddStructureExport_Click(object sender, System.EventArgs e)
		{
			CElementMultiStructureExport element = new CElementMultiStructureExport();
			CStructureExportAvecFiltre structure = new CStructureExportAvecFiltre();
			element.DefinitionJeu = structure;
            element.Libelle = I.T("Export structure|20008");
			if ( m_multiStructure == null )
				m_multiStructure = new CMultiStructureExport(CSc2iWin32DataClient.ContexteCourant);
			m_multiStructure.AddDefinition ( element );
			if ( !EditeElementDeStructure ( element ))
			{
				m_multiStructure.RemoveDefinition ( element );
			}
		}

		//-------------------------------------------------------------------------
		public bool EditeElementDeStructure ( CElementMultiStructureExport element )
		{
            bool bResult = false;
            if (element.DefinitionJeu is CDefinitionJeuDonneesEasyQuery)
            {
                bResult = CFormEditJeuDeDonneesEasyQuery.EditeElementQuery(element);
            }
            else
            {
                bResult = CFormEditeElementDeMultiStructure.EditeElement(element, m_multiStructure);
            }
			if ( bResult )
			{
				RefillListeVariables();
				m_wndListeStructures.Remplir ( m_multiStructure.Definitions );
			}
			return bResult;
		}

		private void m_lnkModifierStructure_LinkClicked(object sender, System.EventArgs e)
		{
			EditeElementSelectionne();
		}

		private void m_wndListeStructures_DoubleClick(object sender, System.EventArgs e)
		{
			EditeElementSelectionne();
		}

		//-------------------------------------------------------------------------
		private void EditeElementSelectionne()
		{
			if ( m_wndListeStructures.SelectedItems.Count != 1 )
				return;
			ListViewItem item = m_wndListeStructures.SelectedItems[0];
			CElementMultiStructureExport element = (CElementMultiStructureExport)item.Tag;
			EditeElementDeStructure ( element );
		}

		//-------------------------------------------------------------------------
		private void m_menuAddRequete_Click(object sender, System.EventArgs e)
		{
			CElementMultiStructureExport element = new CElementMultiStructureExport();
			C2iRequete requete = new C2iRequete(m_multiStructure.ContexteDonnee);
			element.DefinitionJeu = requete;
            element.Libelle = I.T("Query|20010");
			requete.ElementAVariablesExterne = m_multiStructure;
			m_multiStructure.AddDefinition ( element );
			if ( !EditeElementDeStructure ( element ))
			{
				m_multiStructure.RemoveDefinition ( element );
			}
		}



		private void m_lnkSupprimerStructure_LinkClicked(object sender, System.EventArgs e)
		{
			if ( m_wndListeStructures.SelectedItems.Count != 1 )
				return;
			ListViewItem item = m_wndListeStructures.SelectedItems[0];
			CElementMultiStructureExport element = (CElementMultiStructureExport)item.Tag;
			if ( CFormAlerte.Afficher(I.T("Delete the structure |267")+element.Libelle+"' ?", 
				EFormAlerteType.Question) == DialogResult.Yes )
			{
				MultiStructure.RemoveDefinition ( element );
				m_wndListeStructures.Items.Remove ( item );
			}
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

		////////////////////////////////////////////////////////////
		private DataSet m_jeuEssai = null;
		public DataSet GetJeuEssai( IIndicateurProgression indicateurProgression )
		{
			if ( m_jeuEssai == null )
			{
				CResultAErreur result = RecalcJeuEssai ( indicateurProgression );
				if ( !result )
				{
					CFormAlerte.Afficher ( result);
					return null;
				}
			}
			return m_jeuEssai;
		}

		////////////////////////////////////////////////////////////
		public CResultAErreur RecalcJeuEssai ( IIndicateurProgression indicateur )
		{
			CResultAErreur result = CResultAErreur.True;
			CMultiStructureExport structure = MultiStructure;
			if ( structure.Formulaire != null && structure.Formulaire.Childs.Length > 0 )
			{
				if (indicateur != null)
					indicateur.Masquer(true);
				if ( !CFormFormulairePopup.EditeElement ( structure.Formulaire, structure, I.T("Report data |268") ))
				{
					result.EmpileErreur(I.T("Error or cancel in the form|269"));
					return result;
				}
				if (indicateur != null)
					indicateur.Masquer(false);
			}
			IIndicateurProgression myIndicateur = null;
			if ( indicateur == null )
			{
				myIndicateur = CFormProgression.GetNewIndicateurAndPopup(I.T("Test set calculation|270"));
				indicateur = myIndicateur;
			}
			m_jeuEssai = null;
			result = structure.GetDataSet ( false, null, indicateur );
			if ( myIndicateur != null )
			{
				CFormProgression.EndIndicateur ( myIndicateur );
				indicateur = null;
			}
			if ( !result )
			{
				return result;
			}
			m_jeuEssai = (DataSet)result.Data;
			result.Data = null;
			return result;
		}


		private void m_lnkVoirResultat_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if ( m_jeuEssai == null )
			{
				CResultAErreur result = RecalcJeuEssai(null);
				if ( !result )
				{
					CFormAlerte.Afficher ( result);
				}
			}
			if ( m_jeuEssai != null )
				CFormVisualisationDataSet.AfficheDonnees ( m_jeuEssai );

		}

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			CResultAErreur result = RecalcJeuEssai(null);
			if ( !result )
				CFormAlerte.Afficher ( result);
		}

		private void m_btnCopy_Click(object sender, System.EventArgs e)
		{
			CResultAErreur result = CSerializerObjetInClipBoard.Copy ( MultiStructure, CMultiStructureExport.c_idFichier );
			if ( !result) 
			{
				CFormAlerte.Afficher ( result);
			}
		}

		private void m_btnPaste_Click(object sender, System.EventArgs e)
		{
			I2iSerializable objet = null;
			CResultAErreur result = CSerializerObjetInClipBoard.Paste ( ref objet, CMultiStructureExport.c_idFichier );
			if ( !result )
			{
				CFormAlerte.Afficher(result);
				return;
			}
			if ( CFormAlerte.Afficher(I.T("Current structure data will be replaced. Continue?|259"), 
				EFormAlerteType.Question) == DialogResult.No )
				return;
			m_multiStructure = (CMultiStructureExport)objet;
			Init ( m_multiStructure );
		}

		private void CPanelEditMultiStructure_BackColorChanged(object sender, System.EventArgs e)
		{
		}	

		public Color TabColor
		{
			get
			{
				return m_tab.BackColor;
			}
			set
			{
				m_tab.BackColor = value;
			}
        }

        private void m_menuAddEasyQuery_Click(object sender, EventArgs e)
        {
            CElementMultiStructureExport element = new CElementMultiStructureExport();
            CDefinitionJeuDonneesEasyQuery def = new CDefinitionJeuDonneesEasyQuery();
            def.ContexteDonnee = m_multiStructure.ContexteDonnee;
            element.DefinitionJeu = def;
            element.Libelle = I.T("Easy Query|20113");
            def.ElementAVariablesExterne = m_multiStructure;
            m_multiStructure.AddDefinition(element);
            if (!EditeElementDeStructure(element))
            {
                m_multiStructure.RemoveDefinition(element);
            }
        }

        
	}
}
