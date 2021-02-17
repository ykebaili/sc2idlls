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

namespace sc2i.win32.data.dynamic
{
	public class CPanelEditRequete : System.Windows.Forms.UserControl , IControlALockEdition
	{
		private string m_lastRequeteStructureGeneree = "";
		private DataTable m_tableExemple = null;
		private bool m_bIsLoad = false;
		private C2iRequete m_requete = null;
		private sc2i.win32.common.CExtModeEdition m_gestionnaireModeEdition;
		private sc2i.win32.common.C2iTabControl m_tab;
		private Crownwood.Magic.Controls.TabPage m_pageFiltre;
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
		private sc2i.win32.common.C2iTextBox m_txtRequete;
		private sc2i.win32.common.C2iPanel m_panelGaucheRequete;
		private sc2i.win32.common.C2iPanel m_panelDroiteRequete;
		private System.Windows.Forms.Splitter splitter2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button m_btnAjouterParametre;
		private Crownwood.Magic.Controls.TabPage m_pageTableCroisee;
		private System.Windows.Forms.CheckBox m_chkCroiserDonnees;
		private sc2i.win32.common.CPanelEditTableauCroise m_panelTableCroisee;
		private System.Windows.Forms.Button m_btnOpen;
		private System.Windows.Forms.Button m_btnSave;
		private System.Windows.Forms.Button m_btnPaste;
		private System.Windows.Forms.Button m_btnCopy;
        protected CExtStyle m_ExtStyle1;
		private System.ComponentModel.IContainer components;

		
		public CPanelEditRequete()
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
            sc2i.win32.common.CGrilleEditeurObjetGraphique cGrilleEditeurObjetGraphique3 = new sc2i.win32.common.CGrilleEditeurObjetGraphique();
            sc2i.win32.common.CProfilEditeurObjetGraphique cProfilEditeurObjetGraphique3 = new sc2i.win32.common.CProfilEditeurObjetGraphique();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPanelEditRequete));
            this.m_gestionnaireModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_tab = new sc2i.win32.common.C2iTabControl(this.components);
            this.m_pageFiltre = new Crownwood.Magic.Controls.TabPage();
            this.m_panelGaucheRequete = new sc2i.win32.common.C2iPanel(this.components);
            this.m_txtRequete = new sc2i.win32.common.C2iTextBox();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.m_panelDroiteRequete = new sc2i.win32.common.C2iPanel(this.components);
            this.m_btnAjouterParametre = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.m_btnAjouterChamp = new sc2i.win32.common.CWndLinkStd();
            this.m_btnSupprimerChamp = new sc2i.win32.common.CWndLinkStd();
            this.m_btnModifierChamp = new sc2i.win32.common.CWndLinkStd();
            this.m_wndListeVariables = new sc2i.win32.common.ListViewAutoFilled();
            this.colNomChamp = new sc2i.win32.common.ListViewAutoFilledColumn();
            this.m_colType = new sc2i.win32.common.ListViewAutoFilledColumn();
            this.m_pageFormulaire = new Crownwood.Magic.Controls.TabPage();
            this.m_panelEditionFormulaire = new sc2i.formulaire.win32.editor.CPanelEditionFormulaire();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.m_propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.m_listeControles = new sc2i.formulaire.win32.editor.CPanelListe2iWnd();
            this.m_pageTableCroisee = new Crownwood.Magic.Controls.TabPage();
            this.m_panelTableCroisee = new sc2i.win32.common.CPanelEditTableauCroise();
            this.m_chkCroiserDonnees = new System.Windows.Forms.CheckBox();
            this.m_btnOpen = new System.Windows.Forms.Button();
            this.m_btnSave = new System.Windows.Forms.Button();
            this.m_btnPaste = new System.Windows.Forms.Button();
            this.m_btnCopy = new System.Windows.Forms.Button();
            this.m_menuNewVariable = new System.Windows.Forms.ContextMenu();
            this.m_menuVariableSaisie = new System.Windows.Forms.MenuItem();
            this.m_menuNewVariableCalculée = new System.Windows.Forms.MenuItem();
            this.m_menuNewVariableSelection = new System.Windows.Forms.MenuItem();
            this.m_ExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.m_tab.SuspendLayout();
            this.m_pageFiltre.SuspendLayout();
            this.m_panelGaucheRequete.SuspendLayout();
            this.m_panelDroiteRequete.SuspendLayout();
            this.m_pageFormulaire.SuspendLayout();
            this.m_pageTableCroisee.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_tab
            // 
            this.m_tab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_tab.BoldSelectedPage = true;
            this.m_tab.ControlBottomOffset = 16;
            this.m_tab.ControlRightOffset = 16;
            this.m_tab.ForeColor = System.Drawing.Color.Black;
            this.m_tab.IDEPixelArea = false;
            this.m_tab.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_tab, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_tab.Name = "m_tab";
            this.m_tab.Ombre = true;
            this.m_tab.PositionTop = true;
            this.m_tab.SelectedIndex = 2;
            this.m_tab.SelectedTab = this.m_pageTableCroisee;
            this.m_tab.Size = new System.Drawing.Size(717, 325);
            this.m_ExtStyle1.SetStyleBackColor(this.m_tab, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_ExtStyle1.SetStyleForeColor(this.m_tab, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTextePanel);
            this.m_tab.TabIndex = 2;
            this.m_tab.TabPages.AddRange(new Crownwood.Magic.Controls.TabPage[] {
            this.m_pageFiltre,
            this.m_pageFormulaire,
            this.m_pageTableCroisee});
            this.m_tab.TextColor = System.Drawing.Color.Black;
            this.m_tab.SelectionChanged += new System.EventHandler(this.m_tab_SelectionChanged);
            // 
            // m_pageFiltre
            // 
            this.m_pageFiltre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pageFiltre.Controls.Add(this.m_panelGaucheRequete);
            this.m_pageFiltre.Controls.Add(this.splitter2);
            this.m_pageFiltre.Controls.Add(this.m_panelDroiteRequete);
            this.m_pageFiltre.Location = new System.Drawing.Point(0, 25);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_pageFiltre, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_pageFiltre.Name = "m_pageFiltre";
            this.m_pageFiltre.Selected = false;
            this.m_pageFiltre.Size = new System.Drawing.Size(701, 284);
            this.m_ExtStyle1.SetStyleBackColor(this.m_pageFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_pageFiltre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_pageFiltre.TabIndex = 0;
            this.m_pageFiltre.Title = "Request|193";
            // 
            // m_panelGaucheRequete
            // 
            this.m_panelGaucheRequete.Controls.Add(this.m_txtRequete);
            this.m_panelGaucheRequete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelGaucheRequete.Location = new System.Drawing.Point(0, 0);
            this.m_panelGaucheRequete.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelGaucheRequete, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelGaucheRequete.Name = "m_panelGaucheRequete";
            this.m_panelGaucheRequete.Size = new System.Drawing.Size(309, 284);
            this.m_ExtStyle1.SetStyleBackColor(this.m_panelGaucheRequete, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_panelGaucheRequete, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelGaucheRequete.TabIndex = 1;
            // 
            // m_txtRequete
            // 
            this.m_txtRequete.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtRequete.Location = new System.Drawing.Point(8, 8);
            this.m_txtRequete.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_txtRequete, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_txtRequete.Multiline = true;
            this.m_txtRequete.Name = "m_txtRequete";
            this.m_txtRequete.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.m_txtRequete.Size = new System.Drawing.Size(296, 269);
            this.m_ExtStyle1.SetStyleBackColor(this.m_txtRequete, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_txtRequete, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtRequete.TabIndex = 0;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(309, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.splitter2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 284);
            this.m_ExtStyle1.SetStyleBackColor(this.splitter2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.splitter2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.splitter2.TabIndex = 3;
            this.splitter2.TabStop = false;
            // 
            // m_panelDroiteRequete
            // 
            this.m_panelDroiteRequete.Controls.Add(this.m_btnAjouterParametre);
            this.m_panelDroiteRequete.Controls.Add(this.label1);
            this.m_panelDroiteRequete.Controls.Add(this.m_btnAjouterChamp);
            this.m_panelDroiteRequete.Controls.Add(this.m_btnSupprimerChamp);
            this.m_panelDroiteRequete.Controls.Add(this.m_btnModifierChamp);
            this.m_panelDroiteRequete.Controls.Add(this.m_wndListeVariables);
            this.m_panelDroiteRequete.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_panelDroiteRequete.Location = new System.Drawing.Point(312, 0);
            this.m_panelDroiteRequete.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelDroiteRequete, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelDroiteRequete.Name = "m_panelDroiteRequete";
            this.m_panelDroiteRequete.Size = new System.Drawing.Size(389, 284);
            this.m_ExtStyle1.SetStyleBackColor(this.m_panelDroiteRequete, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_panelDroiteRequete, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelDroiteRequete.TabIndex = 2;
            // 
            // m_btnAjouterParametre
            // 
            this.m_btnAjouterParametre.Location = new System.Drawing.Point(0, 152);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnAjouterParametre, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnAjouterParametre.Name = "m_btnAjouterParametre";
            this.m_btnAjouterParametre.Size = new System.Drawing.Size(24, 23);
            this.m_ExtStyle1.SetStyleBackColor(this.m_btnAjouterParametre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_btnAjouterParametre, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAjouterParametre.TabIndex = 6;
            this.m_btnAjouterParametre.Text = "<";
            this.m_btnAjouterParametre.Click += new System.EventHandler(this.m_btnAjouterParametre_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 5;
            this.label1.Text = "Fields|195";
            // 
            // m_btnAjouterChamp
            // 
            this.m_btnAjouterChamp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnAjouterChamp.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_btnAjouterChamp.Location = new System.Drawing.Point(8, 24);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnAjouterChamp, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnAjouterChamp.Name = "m_btnAjouterChamp";
            this.m_btnAjouterChamp.Size = new System.Drawing.Size(72, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.m_btnAjouterChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_btnAjouterChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAjouterChamp.TabIndex = 1;
            this.m_btnAjouterChamp.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_btnAjouterChamp.LinkClicked += new System.EventHandler(this.m_btnAjouterChamp_LinkClicked);
            // 
            // m_btnSupprimerChamp
            // 
            this.m_btnSupprimerChamp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnSupprimerChamp.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_btnSupprimerChamp.Location = new System.Drawing.Point(168, 24);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnSupprimerChamp, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnSupprimerChamp.Name = "m_btnSupprimerChamp";
            this.m_btnSupprimerChamp.Size = new System.Drawing.Size(72, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.m_btnSupprimerChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_btnSupprimerChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnSupprimerChamp.TabIndex = 3;
            this.m_btnSupprimerChamp.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_btnSupprimerChamp.LinkClicked += new System.EventHandler(this.m_btnSupprimerChamp_LinkClicked);
            // 
            // m_btnModifierChamp
            // 
            this.m_btnModifierChamp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnModifierChamp.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_btnModifierChamp.Location = new System.Drawing.Point(88, 24);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnModifierChamp, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnModifierChamp.Name = "m_btnModifierChamp";
            this.m_btnModifierChamp.Size = new System.Drawing.Size(72, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.m_btnModifierChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_btnModifierChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnModifierChamp.TabIndex = 2;
            this.m_btnModifierChamp.TypeLien = sc2i.win32.common.TypeLinkStd.Modification;
            this.m_btnModifierChamp.LinkClicked += new System.EventHandler(this.m_btnModifierChamp_LinkClicked);
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
            this.m_wndListeVariables.Location = new System.Drawing.Point(24, 40);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_wndListeVariables, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_wndListeVariables.Name = "m_wndListeVariables";
            this.m_wndListeVariables.Size = new System.Drawing.Size(357, 237);
            this.m_ExtStyle1.SetStyleBackColor(this.m_wndListeVariables, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_wndListeVariables, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListeVariables.TabIndex = 4;
            this.m_wndListeVariables.UseCompatibleStateImageBehavior = false;
            this.m_wndListeVariables.View = System.Windows.Forms.View.Details;
            this.m_wndListeVariables.DoubleClick += new System.EventHandler(this.m_wndListeVariables_DoubleClick);
            // 
            // colNomChamp
            // 
            this.colNomChamp.Field = "Nom";
            this.colNomChamp.PrecisionWidth = 0;
            this.colNomChamp.ProportionnalSize = false;
            this.colNomChamp.Text = "Name|253";
            this.colNomChamp.Visible = true;
            this.colNomChamp.Width = 250;
            // 
            // m_colType
            // 
            this.m_colType.Field = "LibelleType";
            this.m_colType.PrecisionWidth = 0;
            this.m_colType.ProportionnalSize = false;
            this.m_colType.Text = "Type|254";
            this.m_colType.Visible = true;
            this.m_colType.Width = 120;
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
            this.m_pageFormulaire.Size = new System.Drawing.Size(701, 284);
            this.m_ExtStyle1.SetStyleBackColor(this.m_pageFormulaire, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_pageFormulaire, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_pageFormulaire.TabIndex = 2;
            this.m_pageFormulaire.Title = "Form|190";
            // 
            // m_panelEditionFormulaire
            // 
            this.m_panelEditionFormulaire.AllowDrop = true;
            this.m_panelEditionFormulaire.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(247)))), ((int)(((byte)(184)))));
            this.m_panelEditionFormulaire.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelEditionFormulaire.Echelle = 1F;
            this.m_panelEditionFormulaire.EffetAjoutSuppression = false;
            this.m_panelEditionFormulaire.EffetFonduMenu = true;
            this.m_panelEditionFormulaire.EnDeplacement = false;
            this.m_panelEditionFormulaire.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.m_panelEditionFormulaire.FormesDesPoignees = sc2i.win32.common.EFormePoignee.Carre;
            cGrilleEditeurObjetGraphique3.Couleur = System.Drawing.Color.LightGray;
            cGrilleEditeurObjetGraphique3.HauteurCarreau = 20;
            cGrilleEditeurObjetGraphique3.LargeurCarreau = 20;
            cGrilleEditeurObjetGraphique3.Representation = sc2i.win32.common.ERepresentationGrille.LignesContinues;
            cGrilleEditeurObjetGraphique3.TailleCarreau = new System.Drawing.Size(20, 20);
            this.m_panelEditionFormulaire.GrilleAlignement = cGrilleEditeurObjetGraphique3;
            this.m_panelEditionFormulaire.HauteurMinimaleDesObjets = 10;
            this.m_panelEditionFormulaire.HistorisationActive = false;
            this.m_panelEditionFormulaire.LargeurMinimaleDesObjets = 10;
            this.m_panelEditionFormulaire.Location = new System.Drawing.Point(144, 0);
            this.m_panelEditionFormulaire.LockEdition = false;
            this.m_panelEditionFormulaire.Marge = 10;
            this.m_panelEditionFormulaire.ModeAffichageGrille = sc2i.win32.common.EModeAffichageGrille.AuDeplacement;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelEditionFormulaire, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_panelEditionFormulaire.ModeRepresentationGrille = sc2i.win32.common.ERepresentationGrille.LignesContinues;
            this.m_panelEditionFormulaire.Name = "m_panelEditionFormulaire";
            this.m_panelEditionFormulaire.NombreHistorisation = 10;
            this.m_panelEditionFormulaire.ObjetEdite = null;
            cProfilEditeurObjetGraphique3.FormeDesPoignees = sc2i.win32.common.EFormePoignee.Carre;
            cProfilEditeurObjetGraphique3.Grille = cGrilleEditeurObjetGraphique3;
            cProfilEditeurObjetGraphique3.HistorisationActive = false;
            cProfilEditeurObjetGraphique3.Marge = 10;
            cProfilEditeurObjetGraphique3.ModeAffichageGrille = sc2i.win32.common.EModeAffichageGrille.AuDeplacement;
            cProfilEditeurObjetGraphique3.NombreHistorisation = 10;
            cProfilEditeurObjetGraphique3.ToujoursAlignerSurLaGrille = false;
            this.m_panelEditionFormulaire.Profil = cProfilEditeurObjetGraphique3;
            this.m_panelEditionFormulaire.RefreshSelectionChanged = true;
            this.m_panelEditionFormulaire.Size = new System.Drawing.Size(378, 284);
            this.m_ExtStyle1.SetStyleBackColor(this.m_panelEditionFormulaire, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_panelEditionFormulaire, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelEditionFormulaire.TabIndex = 3;
            this.m_panelEditionFormulaire.ToujoursAlignerSelonLesControles = true;
            this.m_panelEditionFormulaire.ToujoursAlignerSurLaGrille = false;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(522, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.splitter1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 284);
            this.m_ExtStyle1.SetStyleBackColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
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
            this.m_propertyGrid.Location = new System.Drawing.Point(525, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_propertyGrid, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_propertyGrid.Name = "m_propertyGrid";
            this.m_propertyGrid.Size = new System.Drawing.Size(176, 284);
            this.m_ExtStyle1.SetStyleBackColor(this.m_propertyGrid, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_propertyGrid, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
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
            this.m_listeControles.Size = new System.Drawing.Size(144, 284);
            this.m_ExtStyle1.SetStyleBackColor(this.m_listeControles, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_ExtStyle1.SetStyleForeColor(this.m_listeControles, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_listeControles.TabIndex = 4;
            // 
            // m_pageTableCroisee
            // 
            this.m_pageTableCroisee.Controls.Add(this.m_panelTableCroisee);
            this.m_pageTableCroisee.Controls.Add(this.m_chkCroiserDonnees);
            this.m_pageTableCroisee.Location = new System.Drawing.Point(0, 25);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_pageTableCroisee, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_pageTableCroisee.Name = "m_pageTableCroisee";
            this.m_pageTableCroisee.Size = new System.Drawing.Size(701, 284);
            this.m_ExtStyle1.SetStyleBackColor(this.m_pageTableCroisee, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_pageTableCroisee, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_pageTableCroisee.TabIndex = 10;
            this.m_pageTableCroisee.Title = "Cross table|194";
            // 
            // m_panelTableCroisee
            // 
            this.m_panelTableCroisee.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelTableCroisee.Location = new System.Drawing.Point(0, 16);
            this.m_panelTableCroisee.LockEdition = true;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelTableCroisee, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_panelTableCroisee.Name = "m_panelTableCroisee";
            this.m_panelTableCroisee.Size = new System.Drawing.Size(698, 269);
            this.m_ExtStyle1.SetStyleBackColor(this.m_panelTableCroisee, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_panelTableCroisee, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelTableCroisee.TabIndex = 1;
            // 
            // m_chkCroiserDonnees
            // 
            this.m_chkCroiserDonnees.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_chkCroiserDonnees, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_chkCroiserDonnees.Name = "m_chkCroiserDonnees";
            this.m_chkCroiserDonnees.Size = new System.Drawing.Size(303, 21);
            this.m_ExtStyle1.SetStyleBackColor(this.m_chkCroiserDonnees, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_chkCroiserDonnees, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_chkCroiserDonnees.TabIndex = 0;
            this.m_chkCroiserDonnees.Text = "Use request to create a cross table|196";
            this.m_chkCroiserDonnees.CheckedChanged += new System.EventHandler(this.m_chkCroiserDonnees_CheckedChanged);
            // 
            // m_btnOpen
            // 
            this.m_btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOpen.Image")));
            this.m_btnOpen.Location = new System.Drawing.Point(680, 304);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnOpen, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnOpen.Name = "m_btnOpen";
            this.m_btnOpen.Size = new System.Drawing.Size(24, 23);
            this.m_ExtStyle1.SetStyleBackColor(this.m_btnOpen, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_btnOpen, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOpen.TabIndex = 28;
            // 
            // m_btnSave
            // 
            this.m_btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnSave.Image = ((System.Drawing.Image)(resources.GetObject("m_btnSave.Image")));
            this.m_btnSave.Location = new System.Drawing.Point(656, 304);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnSave, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnSave.Name = "m_btnSave";
            this.m_btnSave.Size = new System.Drawing.Size(24, 23);
            this.m_ExtStyle1.SetStyleBackColor(this.m_btnSave, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_btnSave, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnSave.TabIndex = 27;
            // 
            // m_btnPaste
            // 
            this.m_btnPaste.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnPaste.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnPaste.Image = ((System.Drawing.Image)(resources.GetObject("m_btnPaste.Image")));
            this.m_btnPaste.Location = new System.Drawing.Point(624, 304);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnPaste, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnPaste.Name = "m_btnPaste";
            this.m_btnPaste.Size = new System.Drawing.Size(24, 23);
            this.m_ExtStyle1.SetStyleBackColor(this.m_btnPaste, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_btnPaste, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnPaste.TabIndex = 26;
            this.m_btnPaste.Click += new System.EventHandler(this.m_btnPaste_Click);
            // 
            // m_btnCopy
            // 
            this.m_btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("m_btnCopy.Image")));
            this.m_btnCopy.Location = new System.Drawing.Point(600, 304);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnCopy, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnCopy.Name = "m_btnCopy";
            this.m_btnCopy.Size = new System.Drawing.Size(24, 23);
            this.m_ExtStyle1.SetStyleBackColor(this.m_btnCopy, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_btnCopy, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnCopy.TabIndex = 25;
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
            // CPanelEditRequete
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_btnOpen);
            this.Controls.Add(this.m_btnSave);
            this.Controls.Add(this.m_btnPaste);
            this.Controls.Add(this.m_btnCopy);
            this.Controls.Add(this.m_tab);
            this.m_gestionnaireModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CPanelEditRequete";
            this.Size = new System.Drawing.Size(720, 328);
            this.m_ExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.Enter += new System.EventHandler(this.CPanelEditRequete_Enter);
            this.Load += new System.EventHandler(this.CPanelEditRequete_Load);
            this.m_tab.ResumeLayout(false);
            this.m_tab.PerformLayout();
            this.m_pageFiltre.ResumeLayout(false);
            this.m_panelGaucheRequete.ResumeLayout(false);
            this.m_panelGaucheRequete.PerformLayout();
            this.m_panelDroiteRequete.ResumeLayout(false);
            this.m_pageFormulaire.ResumeLayout(false);
            this.m_pageTableCroisee.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		public void Init ( C2iRequete requete )
		{
			m_requete = requete;
			if ( m_bIsLoad )
				InitFenetre();
		}

		/// //////////////////////////////////////////
		private void CPanelEditRequete_Load(object sender, System.EventArgs e)
		{
			m_bIsLoad = true;
			if ( !DesignMode )
			{
				try//Sinon pbs au mode design
				{
					m_panelEditionFormulaire.Init(typeof(C2iRequete), m_requete, m_requete);
					m_panelEditionFormulaire.Selection.SelectionChanged += new EventHandler(SelectionChanged);
					m_listeControles.AddAllLoadedAssemblies();
					m_listeControles.SetTypeEdite(typeof(C2iRequete));
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
			m_panelEditionFormulaire.Selection.SelectionChanged += new EventHandler(SelectionChanged);
			if (m_requete != null)
			{
				m_panelEditionFormulaire.Init(typeof(C2iRequete), m_requete, m_requete);
				m_panelEditionFormulaire.ObjetEdite = m_requete.FormulaireEdition;
			}
			RefillListeVariables();
			
			//CProprieteVariableFiltreDynamiqueEditor.SetEditeur ( new CSelectionneurVariableAInterfaceUtilisateur(m_requete) );
		}

		
		//-------------------------------------------------------------------------
		public CResultAErreur InitChamps()
		{
			CResultAErreur result = CResultAErreur.True;
			if ( m_requete != null )
			{
				m_txtRequete.Text = m_requete.TexteRequete;
			}

			m_chkCroiserDonnees.Checked = m_requete.TableauCroise != null;
/*			if ( m_requete.TableauCroise != null )
				m_tableauCroise = (CTableauCroise)CCloner2iSerializable.Clone(m_requete.TableauCroise);
			else
				m_tableauCroise = null;*/
			UpdatePanelTable();
			InitFenetre();

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
			CVariableDynamiqueSaisie variable = new CVariableDynamiqueSaisie(m_requete);
			if ( EditeVariable ( variable ) )
			{
                m_requete.AddVariable(variable);
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
				bRetour = CFormEditVariableDynamiqueSaisie.EditeVariable ( (CVariableDynamiqueSaisie)variable, m_requete );
			else if ( variable is CVariableDynamiqueCalculee )
				bRetour = CFormEditVariableFiltreCalculee.EditeVariable ((CVariableDynamiqueCalculee)variable, m_requete) ;
			else if ( variable is CVariableDynamiqueSelectionObjetDonnee )
				bRetour = CFormEditVariableDynamiqueSelectionObjetDonnee.EditeVariable ( (CVariableDynamiqueSelectionObjetDonnee)variable);
			else
				bRetour = false;
			return bRetour;
		}

		//-------------------------------------------------------------------------
		private void RefillListeVariables()
		{
			if ( m_requete == null )
				return;
			int nItem = -1;
			if ( m_wndListeVariables.SelectedIndices.Count != 0 )
				nItem = m_wndListeVariables.SelectedIndices[0];
			m_wndListeVariables.Remplir ( m_requete.ListeVariables );
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
			CVariableDynamiqueCalculee variable = new CVariableDynamiqueCalculee(m_requete);
			if ( EditeVariable ( variable ) )
			{
                m_requete.AddVariable(variable);
				RefillListeVariables();
			}
		}

		//-------------------------------------------------------------------------
		private void m_propertyGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			m_panelEditionFormulaire.Refresh();
		}

		//-------------------------------------------------------------------------
        [Browsable(false)]
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
			if ( m_tab.SelectedTab == m_pageTableCroisee )
				UpdatePanelTable();
		}

		private void UpdatePanelTable()
		{
			if ( m_tableExemple == null || m_txtRequete.Text != m_lastRequeteStructureGeneree )
			{
				C2iRequete requete = (C2iRequete)CCloner2iSerializable.Clone ( m_requete );
				requete.TexteRequete = m_txtRequete.Text;
				requete.ElementAVariablesExterne = m_requete.ElementAVariablesExterne;
				CTableauCroise tableau = requete.TableauCroise;
				requete.TableauCroise = null;
				CResultAErreur result = requete.GetStructureResultat(CSc2iWin32DataClient.ContexteCourant.IdSession);
				if ( result && result.Data is DataTable )
				{
/*					if ( m_tableauCroise == null )
						m_tableauCroise = new CTableauCroise();*/
					m_tableExemple = (DataTable)result.Data;
					if ( tableau != null )
						tableau = (CTableauCroise)CCloner2iSerializable.Clone(tableau);
					else
						tableau = new CTableauCroise();
					m_panelTableCroisee.InitChamps ( m_tableExemple, tableau );
					m_lastRequeteStructureGeneree = m_txtRequete.Text;
				}
			}
		}

		//-------------------------------------------------------------------------
		private void m_btnSupprimerChamp_LinkClicked(object sender, System.EventArgs e)
		{
			CVariableDynamique variable = VariableSelectionnee;
			if ( m_requete.IsVariableUtilisee ( variable ) )
			{
				CFormAlerte.Afficher(I.T("Impossible to delete this variable because it is in use|264"), 
					EFormAlerteType.Erreur);
				return;
			}
			m_requete.RemoveVariable ( variable );
			RefillListeVariables();
		}

		//-------------------------------------------------------------------------
		[System.ComponentModel.Browsable(false)]
		public C2iRequete RequeteEditee
		{
			get
			{
				if (!DesignMode)
				{
					m_requete.TexteRequete = m_txtRequete.Text;
					if (m_chkCroiserDonnees.Checked)
						m_requete.TableauCroise = (CTableauCroise)CCloner2iSerializable.Clone(m_panelTableCroisee.TableauCroise);
					else
						m_requete.TableauCroise = null;
					return m_requete;
				}
				return null;
			}
			set
			{
				if (!DesignMode && value != null)
				{
					m_requete = value;
					InitChamps();
				}
			}
		}

		//-------------------------------------------------------------------------
		private void m_menuNewVariableSelection_Click(object sender, System.EventArgs e)
		{
			CVariableDynamiqueSelectionObjetDonnee variable = new CVariableDynamiqueSelectionObjetDonnee(m_requete);
			if ( EditeVariable ( variable ) )
			{
                m_requete.AddVariable(variable);
				RefillListeVariables();
			}
		}


		//-------------------------------------------------------------------------
		private void CPanelEditRequete_Enter(object sender, System.EventArgs e)
		{
			//CProprieteVariableFiltreDynamiqueEditor.SetEditeur ( new CSelectionneurVariableAInterfaceUtilisateur(m_requete) );
		}

		private void m_btnSave_Click(object sender, System.EventArgs e)
		{
			SaveFileDialog dlg = new SaveFileDialog ();
			dlg.Filter = I.T("Request (*.2iQuery)|*.2iQuery|All files (*.*)|*.*|30050");
			if ( dlg.ShowDialog()==DialogResult.OK )
			{
				string strNomFichier = dlg.FileName;
				CResultAErreur result = CSerializerObjetInFile.SaveToFile ( 
					m_requete,
					C2iRequete.c_idFichier,
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
            dlg.Filter = I.T("Request (*.2iQuery)|*.2iQuery|All files (*.*)|*.*|30050");
			if ( dlg.ShowDialog()==DialogResult.OK )
			{
				if ( CFormAlerte.Afficher(I.T("The data of the current request will be replaced, continue ?|30047"), 
					EFormAlerteType.Question) == DialogResult.No )
					return;
				C2iRequete newFiltre = new C2iRequete(m_requete.ContexteDonnee);
				CResultAErreur result = CSerializerObjetInFile.ReadFromFile ( newFiltre, C2iRequete.c_idFichier, dlg.FileName );
				if ( !result )
					CFormAlerte.Afficher(result);
				else
				{
					m_requete = newFiltre;
					Init ( m_requete );
				}
			}
		}

		//-------------------------------------------------------------------------
		private void m_btnAjouterParametre_Click(object sender, System.EventArgs e)
		{
			if ( m_wndListeVariables.SelectedItems.Count != 0 )
			{
				ListViewItem item = m_wndListeVariables.SelectedItems[0];
				if ( item.Tag is IVariableDynamique )
				{
					m_txtRequete.SelectedText = "[@"+((IVariableDynamique)item.Tag).Nom+"]";
				}
			}
		}

		private void m_chkCroiserDonnees_CheckedChanged(object sender, System.EventArgs e)
		{
			if ( m_chkCroiserDonnees.Checked )
			{
				m_gestionnaireModeEdition.SetModeEdition ( m_panelTableCroisee, TypeModeEdition.EnableSurEdition );
				m_panelTableCroisee.LockEdition = !m_gestionnaireModeEdition.ModeEdition;
			}
			else
			{
				m_gestionnaireModeEdition.SetModeEdition ( m_panelTableCroisee, TypeModeEdition.Autonome );
				m_panelTableCroisee.LockEdition = true;
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

		//-------------------------------------------------------------------------
		public void MasquerFormulaire( bool bMasquer )
		{
			if ( bMasquer && m_tab.TabPages.Contains ( m_pageFormulaire ) )
				m_tab.TabPages.Remove ( m_pageFormulaire );

			if ( !bMasquer && !m_tab.TabPages.Contains ( m_pageFormulaire ) )
				m_tab.TabPages.Add ( m_pageFormulaire );
		}

		private void m_btnCopy_Click(object sender, System.EventArgs e)
		{
			CResultAErreur result = CSerializerObjetInClipBoard.Copy ( m_requete, C2iRequete.c_idFichier );
			if ( !result) 
			{
				CFormAlerte.Afficher ( result);
			}
		}

		private void m_btnPaste_Click(object sender, System.EventArgs e)
		{
			I2iSerializable objet = null;
			CResultAErreur result = CSerializerObjetInClipBoard.Paste ( ref objet, C2iRequete.c_idFichier );
			if ( !result )
			{
				CFormAlerte.Afficher(result);
				return;
			}
			if ( CFormAlerte.Afficher(I.T("The data of the current request will be replaced, continue ?|30047"), 
				EFormAlerteType.Question) == DialogResult.No )
				return;
			m_requete = (C2iRequete)objet;
			Init ( m_requete );
		}
	}
}
