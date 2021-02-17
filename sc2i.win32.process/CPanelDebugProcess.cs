using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using sc2i.multitiers.client;
using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic;
using sc2i.process;
using sc2i.win32.data;
using sc2i.win32.common;
using sc2i.expression;

namespace sc2i.win32.process
{
	/// <summary>
	/// Description résumée de CPanelDebugProcess.
	/// </summary>
	
	public class CPanelDebugProcess : System.Windows.Forms.UserControl
	{
		public delegate void ShowObjetDelegate ( object objet );

		private CBrancheProcess m_brancheProcess = null;
		private CProcessEnExecutionInDb m_processEnExecution =  null;

		private object m_valeurVariableEnCours = null;

		private sc2i.win32.common.CExtModeEdition m_extModeEdition;
		private sc2i.win32.common.C2iPanel m_panelDroite;
		private System.Windows.Forms.Splitter m_splitterDroite;
		private sc2i.win32.common.C2iPanel m_panelBas;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.ListView m_wndListeVariables;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListView m_wndPileAppels;
		private System.Windows.Forms.ColumnHeader m_colAction;
		private sc2i.win32.common.C2iTabControl m_tabControl;
		private Crownwood.Magic.Controls.TabPage m_pageVariables;
		private Crownwood.Magic.Controls.TabPage m_pageResume;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label m_lblDateCreation;
		private System.Windows.Forms.Label m_lblUtilisateur;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label m_lblDateModif;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label m_lblCible;
		private System.Windows.Forms.Label m_lblEtat;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ColumnHeader m_colNomVariable;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.Splitter splitter2;
		private System.Windows.Forms.Panel m_panelValeur1Variable;
		private System.Windows.Forms.Label m_lblValeurVariableDetailSiPasListe;
		private sc2i.win32.process.CControlEditionProcess m_viewProcess;
		private sc2i.win32.common.C2iPanel m_panelListeVariables;
		private System.Windows.Forms.ListView m_wndValeursVariableSiListe;
		private System.Windows.Forms.LinkLabel m_btnDetailValeurVariable;
		private System.Windows.Forms.LinkLabel m_btnDetailCible;
		private System.Windows.Forms.Label m_lblNomVariableDetail;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Label m_lblInfo;
		private Crownwood.Magic.Controls.TabPage m_pageEvenements;
		private sc2i.win32.common.GlacialList m_wndListeEvenements;
        private System.Windows.Forms.LinkLabel m_btnDetailEvenement;
        protected sc2i.win32.common.CExtStyle m_ExtStyle1;
		private Label m_lblPasDeDetail;
		private System.ComponentModel.IContainer components;

		public CPanelDebugProcess()
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			// TODO : ajoutez les initialisations après l'appel à InitializeComponent

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

	#	region Code généré par le Concepteur de composants
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            sc2i.win32.common.GLColumn glColumn2 = new sc2i.win32.common.GLColumn();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPanelDebugProcess));
            sc2i.win32.common.CGrilleEditeurObjetGraphique cGrilleEditeurObjetGraphique4 = new sc2i.win32.common.CGrilleEditeurObjetGraphique();
            sc2i.win32.common.CProfilEditeurObjetGraphique cProfilEditeurObjetGraphique4 = new sc2i.win32.common.CProfilEditeurObjetGraphique();
            this.m_extModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_panelDroite = new sc2i.win32.common.C2iPanel(this.components);
            this.m_tabControl = new sc2i.win32.common.C2iTabControl(this.components);
            this.m_pageEvenements = new Crownwood.Magic.Controls.TabPage();
            this.m_btnDetailEvenement = new System.Windows.Forms.LinkLabel();
            this.m_wndListeEvenements = new sc2i.win32.common.GlacialList();
            this.m_pageResume = new Crownwood.Magic.Controls.TabPage();
            this.m_btnDetailCible = new System.Windows.Forms.LinkLabel();
            this.m_lblInfo = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.m_lblEtat = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.m_lblCible = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.m_lblDateModif = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.m_lblUtilisateur = new System.Windows.Forms.Label();
            this.m_lblDateCreation = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_pageVariables = new Crownwood.Magic.Controls.TabPage();
            this.m_panelListeVariables = new sc2i.win32.common.C2iPanel(this.components);
            this.m_wndListeVariables = new System.Windows.Forms.ListView();
            this.m_colNomVariable = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.m_panelValeur1Variable = new System.Windows.Forms.Panel();
            this.m_btnDetailValeurVariable = new System.Windows.Forms.LinkLabel();
            this.m_lblValeurVariableDetailSiPasListe = new System.Windows.Forms.Label();
            this.m_lblNomVariableDetail = new System.Windows.Forms.Label();
            this.m_wndValeursVariableSiListe = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.m_splitterDroite = new System.Windows.Forms.Splitter();
            this.m_panelBas = new sc2i.win32.common.C2iPanel(this.components);
            this.m_wndPileAppels = new System.Windows.Forms.ListView();
            this.m_colAction = new System.Windows.Forms.ColumnHeader();
            this.label2 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.m_lblPasDeDetail = new System.Windows.Forms.Label();
            this.m_viewProcess = new sc2i.win32.process.CControlEditionProcess();
            this.m_ExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.m_panelDroite.SuspendLayout();
            this.m_tabControl.SuspendLayout();
            this.m_pageEvenements.SuspendLayout();
            this.m_pageResume.SuspendLayout();
            this.m_pageVariables.SuspendLayout();
            this.m_panelListeVariables.SuspendLayout();
            this.m_panelValeur1Variable.SuspendLayout();
            this.m_panelBas.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panelDroite
            // 
            this.m_panelDroite.Controls.Add(this.m_tabControl);
            this.m_panelDroite.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_panelDroite.Location = new System.Drawing.Point(424, 0);
            this.m_panelDroite.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_panelDroite, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelDroite.Name = "m_panelDroite";
            this.m_panelDroite.Size = new System.Drawing.Size(224, 392);
            this.m_ExtStyle1.SetStyleBackColor(this.m_panelDroite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_panelDroite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelDroite.TabIndex = 1;
            // 
            // m_tabControl
            // 
            this.m_tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tabControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_tabControl.BoldSelectedPage = true;
            this.m_tabControl.IDEPixelArea = false;
            this.m_tabControl.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_tabControl, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_tabControl.Name = "m_tabControl";
            this.m_tabControl.Ombre = false;
            this.m_tabControl.PositionTop = true;
            this.m_tabControl.SelectedIndex = 1;
            this.m_tabControl.SelectedTab = this.m_pageVariables;
            this.m_tabControl.Size = new System.Drawing.Size(224, 392);
            this.m_ExtStyle1.SetStyleBackColor(this.m_tabControl, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.m_ExtStyle1.SetStyleForeColor(this.m_tabControl, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_tabControl.TabIndex = 2;
            this.m_tabControl.TabPages.AddRange(new Crownwood.Magic.Controls.TabPage[] {
            this.m_pageResume,
            this.m_pageVariables,
            this.m_pageEvenements});
            // 
            // m_pageEvenements
            // 
            this.m_pageEvenements.Controls.Add(this.m_btnDetailEvenement);
            this.m_pageEvenements.Controls.Add(this.m_wndListeEvenements);
            this.m_pageEvenements.Location = new System.Drawing.Point(0, 25);
            this.m_extModeEdition.SetModeEdition(this.m_pageEvenements, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_pageEvenements.Name = "m_pageEvenements";
            this.m_pageEvenements.Selected = false;
            this.m_pageEvenements.Size = new System.Drawing.Size(224, 367);
            this.m_ExtStyle1.SetStyleBackColor(this.m_pageEvenements, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_pageEvenements, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_pageEvenements.TabIndex = 12;
            this.m_pageEvenements.Title = "Events|173";
            // 
            // m_btnDetailEvenement
            // 
            this.m_btnDetailEvenement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnDetailEvenement.Location = new System.Drawing.Point(0, 352);
            this.m_extModeEdition.SetModeEdition(this.m_btnDetailEvenement, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnDetailEvenement.Name = "m_btnDetailEvenement";
            this.m_btnDetailEvenement.Size = new System.Drawing.Size(144, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.m_btnDetailEvenement, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_btnDetailEvenement, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnDetailEvenement.TabIndex = 1;
            this.m_btnDetailEvenement.TabStop = true;
            this.m_btnDetailEvenement.Text = "Detail|179";
            this.m_btnDetailEvenement.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_btnDetailEvenement_LinkClicked);
            // 
            // m_wndListeEvenements
            // 
            this.m_wndListeEvenements.AllowColumnResize = true;
            this.m_wndListeEvenements.AllowMultiselect = false;
            this.m_wndListeEvenements.AlternateBackground = System.Drawing.Color.DarkGreen;
            this.m_wndListeEvenements.AlternatingColors = false;
            this.m_wndListeEvenements.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_wndListeEvenements.AutoHeight = true;
            this.m_wndListeEvenements.AutoSort = true;
            this.m_wndListeEvenements.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.m_wndListeEvenements.CanChangeActivationCheckBoxes = false;
            this.m_wndListeEvenements.CheckBoxes = false;
            glColumn2.ActiveControlItems = ((System.Collections.ArrayList)(resources.GetObject("glColumn2.ActiveControlItems")));
            glColumn2.BackColor = System.Drawing.Color.Transparent;
            glColumn2.ControlType = sc2i.win32.common.ColumnControlTypes.None;
            glColumn2.ForColor = System.Drawing.Color.Black;
            glColumn2.ImageIndex = -1;
            glColumn2.IsCheckColumn = false;
            glColumn2.LastSortState = sc2i.win32.common.ColumnSortState.SortedUp;
            glColumn2.Name = "Column1";
            glColumn2.Propriete = "Libelle";
            glColumn2.Text = "Label|200";
            glColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn2.Width = 250;
            this.m_wndListeEvenements.Columns.AddRange(new sc2i.win32.common.GLColumn[] {
            glColumn2});
            this.m_wndListeEvenements.ContexteUtilisation = "";
            this.m_wndListeEvenements.EnableCustomisation = true;
            this.m_wndListeEvenements.FocusedItem = null;
            this.m_wndListeEvenements.FullRowSelect = true;
            this.m_wndListeEvenements.GLGridLines = sc2i.win32.common.GLGridStyles.gridSolid;
            this.m_wndListeEvenements.GridColor = System.Drawing.Color.Gray;
            this.m_wndListeEvenements.HeaderHeight = 22;
            this.m_wndListeEvenements.HeaderStyle = sc2i.win32.common.GLHeaderStyles.Normal;
            this.m_wndListeEvenements.HeaderTextColor = System.Drawing.Color.Black;
            this.m_wndListeEvenements.HeaderTextFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.m_wndListeEvenements.HeaderVisible = true;
            this.m_wndListeEvenements.HeaderWordWrap = false;
            this.m_wndListeEvenements.HotColumnIndex = -1;
            this.m_wndListeEvenements.HotItemIndex = -1;
            this.m_wndListeEvenements.HotTracking = false;
            this.m_wndListeEvenements.HotTrackingColor = System.Drawing.Color.LightGray;
            this.m_wndListeEvenements.ImageList = null;
            this.m_wndListeEvenements.ItemHeight = 18;
            this.m_wndListeEvenements.ItemWordWrap = false;
            this.m_wndListeEvenements.ListeSource = null;
            this.m_wndListeEvenements.Location = new System.Drawing.Point(0, 0);
            this.m_wndListeEvenements.MaxHeight = 18;
            this.m_extModeEdition.SetModeEdition(this.m_wndListeEvenements, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_wndListeEvenements.Name = "m_wndListeEvenements";
            this.m_wndListeEvenements.SelectedTextColor = System.Drawing.Color.White;
            this.m_wndListeEvenements.SelectionColor = System.Drawing.Color.DarkBlue;
            this.m_wndListeEvenements.ShowBorder = true;
            this.m_wndListeEvenements.ShowFocusRect = false;
            this.m_wndListeEvenements.Size = new System.Drawing.Size(224, 352);
            this.m_wndListeEvenements.SortIndex = 0;
            this.m_ExtStyle1.SetStyleBackColor(this.m_wndListeEvenements, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_wndListeEvenements, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListeEvenements.SuperFlatHeaderColor = System.Drawing.Color.White;
            this.m_wndListeEvenements.TabIndex = 0;
            this.m_wndListeEvenements.Text = "glacialList1";
            this.m_wndListeEvenements.TrierAuClicSurEnteteColonne = true;
            // 
            // m_pageResume
            // 
            this.m_pageResume.Controls.Add(this.m_btnDetailCible);
            this.m_pageResume.Controls.Add(this.m_lblInfo);
            this.m_pageResume.Controls.Add(this.label8);
            this.m_pageResume.Controls.Add(this.m_lblEtat);
            this.m_pageResume.Controls.Add(this.label9);
            this.m_pageResume.Controls.Add(this.m_lblCible);
            this.m_pageResume.Controls.Add(this.label5);
            this.m_pageResume.Controls.Add(this.m_lblDateModif);
            this.m_pageResume.Controls.Add(this.label6);
            this.m_pageResume.Controls.Add(this.m_lblUtilisateur);
            this.m_pageResume.Controls.Add(this.m_lblDateCreation);
            this.m_pageResume.Controls.Add(this.label4);
            this.m_pageResume.Controls.Add(this.label3);
            this.m_pageResume.Location = new System.Drawing.Point(0, 25);
            this.m_extModeEdition.SetModeEdition(this.m_pageResume, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_pageResume.Name = "m_pageResume";
            this.m_pageResume.Selected = false;
            this.m_pageResume.Size = new System.Drawing.Size(224, 367);
            this.m_ExtStyle1.SetStyleBackColor(this.m_pageResume, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_pageResume, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_pageResume.TabIndex = 11;
            this.m_pageResume.Title = "Summary|171";
            // 
            // m_btnDetailCible
            // 
            this.m_btnDetailCible.Location = new System.Drawing.Point(8, 184);
            this.m_extModeEdition.SetModeEdition(this.m_btnDetailCible, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnDetailCible.Name = "m_btnDetailCible";
            this.m_btnDetailCible.Size = new System.Drawing.Size(96, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.m_btnDetailCible, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_btnDetailCible, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnDetailCible.TabIndex = 15;
            this.m_btnDetailCible.TabStop = true;
            this.m_btnDetailCible.Text = "Detail|179";
            this.m_btnDetailCible.Visible = false;
            this.m_btnDetailCible.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_btnDetailCible_LinkClicked);
            // 
            // m_lblInfo
            // 
            this.m_lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblInfo.BackColor = System.Drawing.Color.White;
            this.m_lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblInfo.Location = new System.Drawing.Point(8, 216);
            this.m_extModeEdition.SetModeEdition(this.m_lblInfo, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblInfo.Name = "m_lblInfo";
            this.m_lblInfo.Size = new System.Drawing.Size(208, 144);
            this.m_ExtStyle1.SetStyleBackColor(this.m_lblInfo, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_lblInfo, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lblInfo.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(8, 200);
            this.m_extModeEdition.SetModeEdition(this.label8, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(120, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.label8, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.label8, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label8.TabIndex = 10;
            this.label8.Text = "Information |180";
            // 
            // m_lblEtat
            // 
            this.m_lblEtat.BackColor = System.Drawing.Color.White;
            this.m_lblEtat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblEtat.Location = new System.Drawing.Point(64, 56);
            this.m_extModeEdition.SetModeEdition(this.m_lblEtat, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblEtat.Name = "m_lblEtat";
            this.m_lblEtat.Size = new System.Drawing.Size(152, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.m_lblEtat, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_lblEtat, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lblEtat.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(8, 56);
            this.m_extModeEdition.SetModeEdition(this.label9, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.label9, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.label9, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label9.TabIndex = 8;
            this.label9.Text = "State|176";
            // 
            // m_lblCible
            // 
            this.m_lblCible.BackColor = System.Drawing.Color.White;
            this.m_lblCible.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblCible.Location = new System.Drawing.Point(8, 152);
            this.m_extModeEdition.SetModeEdition(this.m_lblCible, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblCible.Name = "m_lblCible";
            this.m_lblCible.Size = new System.Drawing.Size(208, 32);
            this.m_ExtStyle1.SetStyleBackColor(this.m_lblCible, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_lblCible, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lblCible.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 136);
            this.m_extModeEdition.SetModeEdition(this.label5, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.label5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.label5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label5.TabIndex = 6;
            this.label5.Text = "Target|178";
            // 
            // m_lblDateModif
            // 
            this.m_lblDateModif.BackColor = System.Drawing.Color.White;
            this.m_lblDateModif.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblDateModif.Location = new System.Drawing.Point(64, 32);
            this.m_extModeEdition.SetModeEdition(this.m_lblDateModif, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblDateModif.Name = "m_lblDateModif";
            this.m_lblDateModif.Size = new System.Drawing.Size(152, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.m_lblDateModif, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_lblDateModif, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lblDateModif.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 88);
            this.m_extModeEdition.SetModeEdition(this.label6, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.label6, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.label6, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label6.TabIndex = 4;
            this.label6.Text = "User|177";
            // 
            // m_lblUtilisateur
            // 
            this.m_lblUtilisateur.BackColor = System.Drawing.Color.White;
            this.m_lblUtilisateur.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblUtilisateur.Location = new System.Drawing.Point(8, 104);
            this.m_extModeEdition.SetModeEdition(this.m_lblUtilisateur, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblUtilisateur.Name = "m_lblUtilisateur";
            this.m_lblUtilisateur.Size = new System.Drawing.Size(208, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.m_lblUtilisateur, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_lblUtilisateur, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lblUtilisateur.TabIndex = 3;
            // 
            // m_lblDateCreation
            // 
            this.m_lblDateCreation.BackColor = System.Drawing.Color.White;
            this.m_lblDateCreation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblDateCreation.Location = new System.Drawing.Point(64, 8);
            this.m_extModeEdition.SetModeEdition(this.m_lblDateCreation, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblDateCreation.Name = "m_lblDateCreation";
            this.m_lblDateCreation.Size = new System.Drawing.Size(152, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.m_lblDateCreation, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_lblDateCreation, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lblDateCreation.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 32);
            this.m_extModeEdition.SetModeEdition(this.label4, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.label4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.label4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label4.TabIndex = 1;
            this.label4.Text = "Modification|175";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 8);
            this.m_extModeEdition.SetModeEdition(this.label3, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label3.TabIndex = 0;
            this.label3.Text = "Creation174";
            // 
            // m_pageVariables
            // 
            this.m_pageVariables.Controls.Add(this.m_panelListeVariables);
            this.m_pageVariables.Controls.Add(this.splitter2);
            this.m_pageVariables.Controls.Add(this.m_panelValeur1Variable);
            this.m_pageVariables.Location = new System.Drawing.Point(0, 25);
            this.m_extModeEdition.SetModeEdition(this.m_pageVariables, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_pageVariables.Name = "m_pageVariables";
            this.m_pageVariables.Size = new System.Drawing.Size(224, 367);
            this.m_ExtStyle1.SetStyleBackColor(this.m_pageVariables, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_pageVariables, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_pageVariables.TabIndex = 10;
            this.m_pageVariables.Title = "Variables|172";
            // 
            // m_panelListeVariables
            // 
            this.m_panelListeVariables.Controls.Add(this.m_wndListeVariables);
            this.m_panelListeVariables.Controls.Add(this.label1);
            this.m_panelListeVariables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelListeVariables.Location = new System.Drawing.Point(0, 0);
            this.m_panelListeVariables.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_panelListeVariables, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelListeVariables.Name = "m_panelListeVariables";
            this.m_panelListeVariables.Size = new System.Drawing.Size(224, 228);
            this.m_ExtStyle1.SetStyleBackColor(this.m_panelListeVariables, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_panelListeVariables, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelListeVariables.TabIndex = 4;
            // 
            // m_wndListeVariables
            // 
            this.m_wndListeVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_wndListeVariables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_colNomVariable,
            this.columnHeader1});
            this.m_wndListeVariables.Location = new System.Drawing.Point(0, 16);
            this.m_extModeEdition.SetModeEdition(this.m_wndListeVariables, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_wndListeVariables.MultiSelect = false;
            this.m_wndListeVariables.Name = "m_wndListeVariables";
            this.m_wndListeVariables.Size = new System.Drawing.Size(224, 204);
            this.m_ExtStyle1.SetStyleBackColor(this.m_wndListeVariables, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_wndListeVariables, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListeVariables.TabIndex = 0;
            this.m_wndListeVariables.UseCompatibleStateImageBehavior = false;
            this.m_wndListeVariables.View = System.Windows.Forms.View.Details;
            this.m_wndListeVariables.SelectedIndexChanged += new System.EventHandler(this.m_wndListeVariables_SelectedIndexChanged);
            // 
            // m_colNomVariable
            // 
            this.m_colNomVariable.Text = "Name|164";
            this.m_colNomVariable.Width = 96;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Value|16";
            this.columnHeader1.Width = 112;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.label1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 1;
            this.label1.Text = "Variables|136";
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Location = new System.Drawing.Point(0, 228);
            this.m_extModeEdition.SetModeEdition(this.splitter2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(224, 3);
            this.m_ExtStyle1.SetStyleBackColor(this.splitter2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.splitter2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.splitter2.TabIndex = 2;
            this.splitter2.TabStop = false;
            // 
            // m_panelValeur1Variable
            // 
            this.m_panelValeur1Variable.BackColor = System.Drawing.SystemColors.Control;
            this.m_panelValeur1Variable.Controls.Add(this.m_btnDetailValeurVariable);
            this.m_panelValeur1Variable.Controls.Add(this.m_lblValeurVariableDetailSiPasListe);
            this.m_panelValeur1Variable.Controls.Add(this.m_lblNomVariableDetail);
            this.m_panelValeur1Variable.Controls.Add(this.m_wndValeursVariableSiListe);
            this.m_panelValeur1Variable.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_panelValeur1Variable.Location = new System.Drawing.Point(0, 231);
            this.m_extModeEdition.SetModeEdition(this.m_panelValeur1Variable, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelValeur1Variable.Name = "m_panelValeur1Variable";
            this.m_panelValeur1Variable.Size = new System.Drawing.Size(224, 136);
            this.m_ExtStyle1.SetStyleBackColor(this.m_panelValeur1Variable, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_panelValeur1Variable, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelValeur1Variable.TabIndex = 3;
            // 
            // m_btnDetailValeurVariable
            // 
            this.m_btnDetailValeurVariable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnDetailValeurVariable.Location = new System.Drawing.Point(8, 120);
            this.m_extModeEdition.SetModeEdition(this.m_btnDetailValeurVariable, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnDetailValeurVariable.Name = "m_btnDetailValeurVariable";
            this.m_btnDetailValeurVariable.Size = new System.Drawing.Size(96, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.m_btnDetailValeurVariable, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_btnDetailValeurVariable, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnDetailValeurVariable.TabIndex = 14;
            this.m_btnDetailValeurVariable.TabStop = true;
            this.m_btnDetailValeurVariable.Text = "Detail|179";
            this.m_btnDetailValeurVariable.Visible = false;
            this.m_btnDetailValeurVariable.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_btnDetailValeurVariable_LinkClicked);
            // 
            // m_lblValeurVariableDetailSiPasListe
            // 
            this.m_lblValeurVariableDetailSiPasListe.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblValeurVariableDetailSiPasListe.BackColor = System.Drawing.Color.White;
            this.m_lblValeurVariableDetailSiPasListe.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblValeurVariableDetailSiPasListe.Location = new System.Drawing.Point(8, 16);
            this.m_extModeEdition.SetModeEdition(this.m_lblValeurVariableDetailSiPasListe, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblValeurVariableDetailSiPasListe.Name = "m_lblValeurVariableDetailSiPasListe";
            this.m_lblValeurVariableDetailSiPasListe.Size = new System.Drawing.Size(208, 104);
            this.m_ExtStyle1.SetStyleBackColor(this.m_lblValeurVariableDetailSiPasListe, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_lblValeurVariableDetailSiPasListe, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lblValeurVariableDetailSiPasListe.TabIndex = 12;
            // 
            // m_lblNomVariableDetail
            // 
            this.m_lblNomVariableDetail.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblNomVariableDetail.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.m_lblNomVariableDetail, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblNomVariableDetail.Name = "m_lblNomVariableDetail";
            this.m_lblNomVariableDetail.Size = new System.Drawing.Size(224, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.m_lblNomVariableDetail, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_lblNomVariableDetail, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lblNomVariableDetail.TabIndex = 0;
            this.m_lblNomVariableDetail.Text = "m_labelNomVariableDetail";
            // 
            // m_wndValeursVariableSiListe
            // 
            this.m_wndValeursVariableSiListe.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_wndValeursVariableSiListe.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.m_wndValeursVariableSiListe.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_wndValeursVariableSiListe.Location = new System.Drawing.Point(0, 16);
            this.m_extModeEdition.SetModeEdition(this.m_wndValeursVariableSiListe, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_wndValeursVariableSiListe.Name = "m_wndValeursVariableSiListe";
            this.m_wndValeursVariableSiListe.Size = new System.Drawing.Size(224, 104);
            this.m_ExtStyle1.SetStyleBackColor(this.m_wndValeursVariableSiListe, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_wndValeursVariableSiListe, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndValeursVariableSiListe.TabIndex = 13;
            this.m_wndValeursVariableSiListe.UseCompatibleStateImageBehavior = false;
            this.m_wndValeursVariableSiListe.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "";
            this.columnHeader2.Width = 3000;
            // 
            // m_splitterDroite
            // 
            this.m_splitterDroite.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_splitterDroite.Location = new System.Drawing.Point(421, 0);
            this.m_extModeEdition.SetModeEdition(this.m_splitterDroite, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_splitterDroite.Name = "m_splitterDroite";
            this.m_splitterDroite.Size = new System.Drawing.Size(3, 392);
            this.m_ExtStyle1.SetStyleBackColor(this.m_splitterDroite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_splitterDroite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_splitterDroite.TabIndex = 2;
            this.m_splitterDroite.TabStop = false;
            // 
            // m_panelBas
            // 
            this.m_panelBas.Controls.Add(this.m_wndPileAppels);
            this.m_panelBas.Controls.Add(this.label2);
            this.m_panelBas.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_panelBas.Location = new System.Drawing.Point(0, 240);
            this.m_panelBas.LockEdition = false;
            this.m_extModeEdition.SetModeEdition(this.m_panelBas, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelBas.Name = "m_panelBas";
            this.m_panelBas.Size = new System.Drawing.Size(421, 152);
            this.m_ExtStyle1.SetStyleBackColor(this.m_panelBas, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_panelBas, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelBas.TabIndex = 3;
            // 
            // m_wndPileAppels
            // 
            this.m_wndPileAppels.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_wndPileAppels.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_colAction});
            this.m_wndPileAppels.Location = new System.Drawing.Point(0, 16);
            this.m_extModeEdition.SetModeEdition(this.m_wndPileAppels, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_wndPileAppels.MultiSelect = false;
            this.m_wndPileAppels.Name = "m_wndPileAppels";
            this.m_wndPileAppels.Size = new System.Drawing.Size(424, 136);
            this.m_ExtStyle1.SetStyleBackColor(this.m_wndPileAppels, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_wndPileAppels, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndPileAppels.TabIndex = 3;
            this.m_wndPileAppels.UseCompatibleStateImageBehavior = false;
            this.m_wndPileAppels.View = System.Windows.Forms.View.Details;
            this.m_wndPileAppels.SelectedIndexChanged += new System.EventHandler(this.m_wndPileAppels_SelectedIndexChanged);
            // 
            // m_colAction
            // 
            this.m_colAction.Text = "Action label|30024";
            this.m_colAction.Width = 372;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.m_extModeEdition.SetModeEdition(this.label2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(424, 16);
            this.m_ExtStyle1.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 2;
            this.label2.Text = "Call stack|170";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 237);
            this.m_extModeEdition.SetModeEdition(this.splitter1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(421, 3);
            this.m_ExtStyle1.SetStyleBackColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // m_lblPasDeDetail
            // 
            this.m_lblPasDeDetail.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_lblPasDeDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblPasDeDetail.Location = new System.Drawing.Point(85, 79);
            this.m_extModeEdition.SetModeEdition(this.m_lblPasDeDetail, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblPasDeDetail.Name = "m_lblPasDeDetail";
            this.m_lblPasDeDetail.Size = new System.Drawing.Size(222, 82);
            this.m_ExtStyle1.SetStyleBackColor(this.m_lblPasDeDetail, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_lblPasDeDetail, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lblPasDeDetail.TabIndex = 16;
            this.m_lblPasDeDetail.Text = "Execution detail is not available|20002";
            this.m_lblPasDeDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m_lblPasDeDetail.Visible = false;
            // 
            // m_viewProcess
            // 
            this.m_viewProcess.AllowDrop = true;
            this.m_viewProcess.AutoScroll = true;
            this.m_viewProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_viewProcess.Echelle = 1F;
            this.m_viewProcess.EffetAjoutSuppression = false;
            this.m_viewProcess.EffetFonduMenu = true;
            this.m_viewProcess.EnDeplacement = false;
            this.m_viewProcess.FormesDesPoignees = sc2i.win32.common.EFormePoignee.Carre;
            cGrilleEditeurObjetGraphique4.Couleur = System.Drawing.Color.LightGray;
            cGrilleEditeurObjetGraphique4.HauteurCarreau = 20;
            cGrilleEditeurObjetGraphique4.LargeurCarreau = 20;
            cGrilleEditeurObjetGraphique4.Representation = sc2i.win32.common.ERepresentationGrille.LignesContinues;
            cGrilleEditeurObjetGraphique4.TailleCarreau = new System.Drawing.Size(20, 20);
            this.m_viewProcess.GrilleAlignement = cGrilleEditeurObjetGraphique4;
            this.m_viewProcess.HauteurMinimaleDesObjets = 10;
            this.m_viewProcess.HistorisationActive = false;
            this.m_viewProcess.InfoActionACree = null;
            this.m_viewProcess.LargeurMinimaleDesObjets = 10;
            this.m_viewProcess.Location = new System.Drawing.Point(0, 0);
            this.m_viewProcess.LockEdition = false;
            this.m_viewProcess.Marge = 10;
            this.m_viewProcess.ModeAffichageGrille = sc2i.win32.common.EModeAffichageGrille.AuDeplacement;
            this.m_extModeEdition.SetModeEdition(this.m_viewProcess, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_viewProcess.ModeEdition = sc2i.win32.process.EModeEditeurProcess.Selection;
            this.m_viewProcess.ModeRepresentationGrille = sc2i.win32.common.ERepresentationGrille.LignesContinues;
            this.m_viewProcess.Name = "m_viewProcess";
            this.m_viewProcess.NombreHistorisation = 10;
            this.m_viewProcess.ObjetEdite = null;
            this.m_viewProcess.ProcessEdite = null;
            cProfilEditeurObjetGraphique4.FormeDesPoignees = sc2i.win32.common.EFormePoignee.Carre;
            cProfilEditeurObjetGraphique4.Grille = cGrilleEditeurObjetGraphique4;
            cProfilEditeurObjetGraphique4.HistorisationActive = false;
            cProfilEditeurObjetGraphique4.Marge = 10;
            cProfilEditeurObjetGraphique4.ModeAffichageGrille = sc2i.win32.common.EModeAffichageGrille.AuDeplacement;
            cProfilEditeurObjetGraphique4.NombreHistorisation = 10;
            cProfilEditeurObjetGraphique4.ToujoursAlignerSurLaGrille = false;
            this.m_viewProcess.Profil = cProfilEditeurObjetGraphique4;
            this.m_viewProcess.RefreshSelectionChanged = true;
            this.m_viewProcess.Size = new System.Drawing.Size(421, 237);
            this.m_ExtStyle1.SetStyleBackColor(this.m_viewProcess, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this.m_viewProcess, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_viewProcess.TabIndex = 5;
            this.m_viewProcess.ToujoursAlignerSelonLesControles = true;
            this.m_viewProcess.ToujoursAlignerSurLaGrille = false;
            // 
            // CPanelDebugProcess
            // 
            this.Controls.Add(this.m_lblPasDeDetail);
            this.Controls.Add(this.m_viewProcess);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.m_panelBas);
            this.Controls.Add(this.m_splitterDroite);
            this.Controls.Add(this.m_panelDroite);
            this.m_extModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CPanelDebugProcess";
            this.Size = new System.Drawing.Size(648, 392);
            this.m_ExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_ExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.Load += new System.EventHandler(this.CPanelDebugProcess_Load);
            this.m_panelDroite.ResumeLayout(false);
            this.m_tabControl.ResumeLayout(false);
            this.m_tabControl.PerformLayout();
            this.m_pageEvenements.ResumeLayout(false);
            this.m_pageResume.ResumeLayout(false);
            this.m_pageVariables.ResumeLayout(false);
            this.m_panelListeVariables.ResumeLayout(false);
            this.m_panelValeur1Variable.ResumeLayout(false);
            this.m_panelBas.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// //////////////////////////////////////////////////////
		public void Init ( CProcessEnExecutionInDb process )
		{
			m_processEnExecution = process;

			m_lblDateCreation.Text = m_processEnExecution.DateCreation.ToString(CUtilDate.gFormat);//dd/MM/yyyy HH:mm:ss");
            m_lblDateModif.Text = m_processEnExecution.DateModification.ToString(CUtilDate.gFormat);//"dd/MM/yyyy HH:mm:ss");

			m_lblEtat.Text = m_processEnExecution.Etat.ToString();

			
			CObjetDonneeAIdNumerique objetLie = m_processEnExecution.ElementLie;
			if ( objetLie == null )
				m_lblCible.Text = I.T("None|19");
			else
				m_lblCible.Text = objetLie.DescriptionElement;

			m_lblInfo.Text = process.InfoEtat;

			m_brancheProcess = process.BrancheEnCours;

			if (m_brancheProcess != null)
			{
				string strNomUtilisateur = CSessionClient.GestionnaireSessions.GetNomUtilisateurFromKeyUtilisateur(m_brancheProcess.KeyUtilisateur);
				m_lblUtilisateur.Text = strNomUtilisateur;
			}

			FillPileAppels();
			FillVariables();
			FillEvenements();
			
			if (m_brancheProcess == null || m_brancheProcess.Process == null)
			{
				m_lblPasDeDetail.Visible = true;
				m_viewProcess.Visible = false;
				return;
			}
			m_lblPasDeDetail.Visible = false;
			m_viewProcess.Visible = true;

			m_viewProcess.ProcessEdite = m_brancheProcess.Process;

			
			
		}

		/// //////////////////////////////////////////////////////
		private ShowObjetDelegate m_delShowObjet = null;
		public ShowObjetDelegate OnShowObject
		{
			set
			{
				m_delShowObjet += value;
				m_btnDetailCible.Visible = m_delShowObjet != null;
				m_btnDetailValeurVariable.Visible = m_delShowObjet != null;
				m_btnDetailEvenement.Visible = m_delShowObjet != null;
			}
		}

		/// //////////////////////////////////////////////////////
		private void FillPileAppels( )
		{
			m_wndPileAppels.Items.Clear();
			if (m_brancheProcess == null || m_brancheProcess.Process == null)
				return;
			CBrancheProcess.CInfoPileExecution[] pile = m_brancheProcess.PileExecution;
			for ( int nElt = pile.Length-1; nElt >= 0; nElt--)

			{
				CBrancheProcess.CInfoPileExecution infoPile = pile[nElt];
				ListViewItem item = new ListViewItem();
				CAction action = m_brancheProcess.Process.GetActionFromId ( infoPile.IdAction );
				if ( action == null )
					item.Text = "???";
				else
					item.Text = action.Libelle;
				item.Tag = action;
				m_wndPileAppels.Items.Add ( item );
			}
		}

		/// //////////////////////////////////////////////////////
		public void FillEvenements()
		{
			m_wndListeEvenements.ListeSource = m_processEnExecution.EvenementsLies;
			m_wndListeEvenements.Refresh();
		}

		/// //////////////////////////////////////////////////////
		private void FillVariables (  )
		{
			m_wndListeVariables.Items.Clear();
			if (m_brancheProcess == null || m_brancheProcess.Process == null)
				return;
			foreach ( IVariableDynamique variable in m_brancheProcess.Process.ListeVariables )
			{
                object valeur = m_brancheProcess.Process.GetValeurChamp(variable.IdVariable);
				string strValeur = "null";
				if ( valeur != null )
				{
					if ( valeur is IList )
						strValeur = "Liste";
					else if ( valeur is CObjetDonneeAIdNumerique )
						strValeur = "Objet";
					else
						strValeur = valeur.ToString();
				}
				ListViewItem item = new ListViewItem();
				item.Text = variable.Nom;
				item.SubItems.Add ( strValeur );
				item.Tag = variable;
				m_wndListeVariables.Items.Add ( item );
			}
		}

		/// //////////////////////////////////////////////////////
		private void m_wndListeVariables_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (m_brancheProcess == null || m_brancheProcess.Process == null)
				return;
			if ( m_wndListeVariables.SelectedItems.Count == 0 )
			{
				m_panelValeur1Variable.Visible = false;
				return;
			}
			m_panelValeur1Variable.Visible = true;
			IVariableDynamique variable = (IVariableDynamique)m_wndListeVariables.SelectedItems[0].Tag;
			m_lblNomVariableDetail.Text = variable.Nom;
            object valeur = m_brancheProcess.Process.GetValeurChamp(variable.IdVariable);
			m_valeurVariableEnCours = valeur;
			m_wndValeursVariableSiListe.Visible =  valeur is IList;
			m_lblValeurVariableDetailSiPasListe.Visible = !m_wndValeursVariableSiListe.Visible;
			if ( valeur == null )
				m_lblValeurVariableDetailSiPasListe.Text = "null";
			else
			{
				if ( valeur is IList )
				{
					foreach ( object obj in (IList)valeur )
					{
						ListViewItem item = new ListViewItem();
						if ( obj is CObjetDonnee )
							item.Text = ((CObjetDonnee)obj).DescriptionElement;
						else if (obj==null)
							item.Text = "null";
						else
							item.Text = obj.ToString();
						item.Tag = obj;
						m_wndValeursVariableSiListe.Items.Add ( item );
					}
				}
				else if (valeur is CObjetDonnee )
					m_lblValeurVariableDetailSiPasListe.Text = ((CObjetDonnee)valeur).DescriptionElement;
				else
					m_lblValeurVariableDetailSiPasListe.Text = valeur.ToString();
			}
		}

		private void m_btnDetailValeurVariable_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if ( m_valeurVariableEnCours == null || m_delShowObjet == null )
				CFormAlerte.Afficher(I.T("Not avaliable|30025"), EFormAlerteType.Erreur);
			else
			{
				if ( ! (m_valeurVariableEnCours  is IList ) )
					m_delShowObjet ( m_valeurVariableEnCours );
				else
				{
					if ( m_wndValeursVariableSiListe.SelectedItems.Count == 1 )
						m_delShowObjet ( m_wndValeursVariableSiListe.SelectedItems[0].Tag );
				}
			}
		}

		private void m_btnDetailCible_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			CObjetDonneeAIdNumerique objet = m_processEnExecution.ElementLie;
			if ( objet == null || m_delShowObjet == null )
				CFormAlerte.Afficher(I.T("Not avaliable|30025"), EFormAlerteType.Erreur);
			else
				m_delShowObjet ( objet );
		}

		private void m_wndPileAppels_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			m_viewProcess.Selection.Clear();
			if ( m_wndPileAppels.SelectedItems.Count == 1 )
			{
				CAction action = ((CAction)m_wndPileAppels.SelectedItems[0].Tag);
				m_viewProcess.Selection.Add (  action );
				m_viewProcess.Refresh();
			}

		}

		private void m_btnDetailEvenement_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if ( m_wndListeEvenements.SelectedItems.Count == 0 )
				return;
			if ( m_delShowObjet == null )
                CFormAlerte.Afficher(I.T("Not avaliable|30025"), EFormAlerteType.Erreur);
			else
			{
				if ( m_wndListeEvenements.SelectedItems[0] != null  )
					m_delShowObjet ( m_wndListeEvenements.SelectedItems[0] );
			}
		}

        private void CPanelDebugProcess_Load(object sender, EventArgs e)
        {
 
        }


	}
}
