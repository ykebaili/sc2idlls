using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic;
using sc2i.expression;
using sc2i.win32.common;
using sc2i.multitiers.client;
using sc2i.formulaire.win32;

namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Description résumée de CFormEditComposantFiltreSelectionStatique.
	/// </summary>
	public class CFormEditComposantFiltreSelectionStatique : System.Windows.Forms.Form
	{
		private CContexteDonnee m_contexte = null;
		private CDefinitionProprieteDynamique m_champ = null;
        private CFiltreData m_filtreRapide = null;

		private ArrayList m_listeSelectionnes = new ArrayList();

		private const string c_champLibelle = "LIBELLE";
		private const string c_champIdOperateur = "OPERATEUR";

		private CComposantFiltreDynamiqueSelectionStatique m_composant = null;
		private CFiltreDynamique m_filtre = null;

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button m_boutonDropList;
		private System.Windows.Forms.Label m_labelChamp;
		private System.Windows.Forms.Panel m_panelComboChamp;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button m_btnOk;
		private System.Windows.Forms.Button m_btnAnnuler;
		private System.Windows.Forms.MenuItem m_menuVariableSaisie;
		private System.Windows.Forms.ContextMenu m_menuNewVariable;
		private System.Windows.Forms.MenuItem m_menuVariableCalculée;
		private System.Windows.Forms.MenuItem m_menuVariableSelection;
		private sc2i.win32.common.CToolTipTraductible m_tooltip;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button m_btnAddOne;
		private System.Windows.Forms.Button m_btnRemoveOne;
		private sc2i.win32.common.ListViewAutoFilled m_wndListeSelectionnes;
		private sc2i.win32.common.ListViewAutoFilledColumn listViewAutoFilledColumn1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.RadioButton m_btnInclure;
		private System.Windows.Forms.RadioButton m_btnExclure;
		private sc2i.win32.common.C2iTabControl c2iTabControl1;
		private Crownwood.Magic.Controls.TabPage tabPage1;
		private Crownwood.Magic.Controls.TabPage tabPage2;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label4;
		private sc2i.win32.expression.CControleEditeFormule m_txtCondition;
		private sc2i.win32.expression.CControlAideFormule m_wndAide;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label m_lblFonctionNonDispo;
		private sc2i.win32.common.GlacialList m_wndListeDispos;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.RadioButton m_btnChamp;
		private System.Windows.Forms.RadioButton m_btnThis;
        private CExtStyle cExtStyle1;
        private Panel m_panelDispo;
        private Panel m_panelRecherche;
        private TextBox m_txtRecherche;
        private Label label5;
        private Button m_btnFiltrer;

		private sc2i.win32.expression.CControleEditeFormule m_lastTextBox = null;

		public CFormEditComposantFiltreSelectionStatique()
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();
			m_lastTextBox = m_txtCondition;
			m_txtCondition.BackColor = Color.LightGreen;
			m_contexte = new CContexteDonnee ( CSessionClient.GetSessionUnique().IdSession, true, true );

			//
			// TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
			//
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
				if ( m_contexte != null )
				{
					m_contexte.Dispose();
					m_contexte = null;
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditComposantFiltreSelectionStatique));
            sc2i.win32.common.GLColumn glColumn2 = new sc2i.win32.common.GLColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.m_boutonDropList = new System.Windows.Forms.Button();
            this.m_labelChamp = new System.Windows.Forms.Label();
            this.m_panelComboChamp = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.c2iTabControl1 = new sc2i.win32.common.C2iTabControl(this.components);
            this.tabPage1 = new Crownwood.Magic.Controls.TabPage();
            this.m_lblFonctionNonDispo = new System.Windows.Forms.Label();
            this.m_panelDispo = new System.Windows.Forms.Panel();
            this.m_wndListeDispos = new sc2i.win32.common.GlacialList();
            this.m_panelRecherche = new System.Windows.Forms.Panel();
            this.m_txtRecherche = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.m_btnFiltrer = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.m_btnRemoveOne = new System.Windows.Forms.Button();
            this.m_btnAddOne = new System.Windows.Forms.Button();
            this.m_wndListeSelectionnes = new sc2i.win32.common.ListViewAutoFilled();
            this.listViewAutoFilledColumn1 = new sc2i.win32.common.ListViewAutoFilledColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage2 = new Crownwood.Magic.Controls.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.m_txtCondition = new sc2i.win32.expression.CControleEditeFormule();
            this.label4 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.m_wndAide = new sc2i.win32.expression.CControlAideFormule();
            this.panel5 = new System.Windows.Forms.Panel();
            this.m_btnThis = new System.Windows.Forms.RadioButton();
            this.m_btnChamp = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.m_btnExclure = new System.Windows.Forms.RadioButton();
            this.m_btnInclure = new System.Windows.Forms.RadioButton();
            this.m_btnAnnuler = new System.Windows.Forms.Button();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_menuNewVariable = new System.Windows.Forms.ContextMenu();
            this.m_menuVariableSaisie = new System.Windows.Forms.MenuItem();
            this.m_menuVariableCalculée = new System.Windows.Forms.MenuItem();
            this.m_menuVariableSelection = new System.Windows.Forms.MenuItem();
            this.m_tooltip = new sc2i.win32.common.CToolTipTraductible(this.components);
            this.cExtStyle1 = new sc2i.win32.common.CExtStyle();
            this.m_panelComboChamp.SuspendLayout();
            this.panel1.SuspendLayout();
            this.c2iTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.m_panelDispo.SuspendLayout();
            this.m_panelRecherche.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 15);
            this.cExtStyle1.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 0;
            this.label1.Text = "Test field|112";
            // 
            // m_boutonDropList
            // 
            this.m_boutonDropList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_boutonDropList.BackColor = System.Drawing.SystemColors.Control;
            this.m_boutonDropList.Image = ((System.Drawing.Image)(resources.GetObject("m_boutonDropList.Image")));
            this.m_boutonDropList.Location = new System.Drawing.Point(596, 0);
            this.m_boutonDropList.Name = "m_boutonDropList";
            this.m_boutonDropList.Size = new System.Drawing.Size(17, 17);
            this.cExtStyle1.SetStyleBackColor(this.m_boutonDropList, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_boutonDropList, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_boutonDropList.TabIndex = 3;
            this.m_boutonDropList.TabStop = false;
            this.m_boutonDropList.UseVisualStyleBackColor = false;
            this.m_boutonDropList.Click += new System.EventHandler(this.m_boutonDropList_Click);
            // 
            // m_labelChamp
            // 
            this.m_labelChamp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_labelChamp.BackColor = System.Drawing.Color.White;
            this.m_labelChamp.Location = new System.Drawing.Point(0, 0);
            this.m_labelChamp.Name = "m_labelChamp";
            this.m_labelChamp.Size = new System.Drawing.Size(596, 17);
            this.cExtStyle1.SetStyleBackColor(this.m_labelChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_labelChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_labelChamp.TabIndex = 2;
            this.m_labelChamp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_tooltip.SetToolTip(this.m_labelChamp, "Indiquez ici le champ à tester|161");
            this.m_labelChamp.Click += new System.EventHandler(this.m_boutonDropList_Click);
            // 
            // m_panelComboChamp
            // 
            this.m_panelComboChamp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panelComboChamp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_panelComboChamp.Controls.Add(this.m_labelChamp);
            this.m_panelComboChamp.Controls.Add(this.m_boutonDropList);
            this.m_panelComboChamp.Location = new System.Drawing.Point(16, 0);
            this.m_panelComboChamp.Name = "m_panelComboChamp";
            this.m_panelComboChamp.Size = new System.Drawing.Size(616, 21);
            this.cExtStyle1.SetStyleBackColor(this.m_panelComboChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelComboChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelComboChamp.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.c2iTabControl1);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.m_btnAnnuler);
            this.panel1.Controls.Add(this.m_btnOk);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(712, 405);
            this.cExtStyle1.SetStyleBackColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel1.TabIndex = 8;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // c2iTabControl1
            // 
            this.c2iTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.c2iTabControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.c2iTabControl1.BoldSelectedPage = true;
            this.c2iTabControl1.ControlBottomOffset = 16;
            this.c2iTabControl1.ControlRightOffset = 16;
            this.c2iTabControl1.IDEPixelArea = false;
            this.c2iTabControl1.Location = new System.Drawing.Point(8, 72);
            this.c2iTabControl1.Name = "c2iTabControl1";
            this.c2iTabControl1.Ombre = true;
            this.c2iTabControl1.PositionTop = true;
            this.c2iTabControl1.SelectedIndex = 0;
            this.c2iTabControl1.SelectedTab = this.tabPage1;
            this.c2iTabControl1.Size = new System.Drawing.Size(696, 304);
            this.cExtStyle1.SetStyleBackColor(this.c2iTabControl1, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.c2iTabControl1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.c2iTabControl1.TabIndex = 23;
            this.c2iTabControl1.TabPages.AddRange(new Crownwood.Magic.Controls.TabPage[] {
            this.tabPage1,
            this.tabPage2});
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_lblFonctionNonDispo);
            this.tabPage1.Controls.Add(this.m_panelDispo);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.m_btnRemoveOne);
            this.tabPage1.Controls.Add(this.m_btnAddOne);
            this.tabPage1.Controls.Add(this.m_wndListeSelectionnes);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(0, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(680, 263);
            this.cExtStyle1.SetStyleBackColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.tabPage1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage1.TabIndex = 10;
            this.tabPage1.Title = "Entities";
            // 
            // m_lblFonctionNonDispo
            // 
            this.m_lblFonctionNonDispo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lblFonctionNonDispo.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblFonctionNonDispo.Location = new System.Drawing.Point(184, 59);
            this.m_lblFonctionNonDispo.Name = "m_lblFonctionNonDispo";
            this.m_lblFonctionNonDispo.Size = new System.Drawing.Size(312, 133);
            this.cExtStyle1.SetStyleBackColor(this.m_lblFonctionNonDispo, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_lblFonctionNonDispo, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lblFonctionNonDispo.TabIndex = 21;
            this.m_lblFonctionNonDispo.Text = "Selection opration impossible for this field|122";
            this.m_lblFonctionNonDispo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m_lblFonctionNonDispo.Visible = false;
            // 
            // m_panelDispo
            // 
            this.m_panelDispo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.m_panelDispo.Controls.Add(this.m_wndListeDispos);
            this.m_panelDispo.Controls.Add(this.m_panelRecherche);
            this.m_panelDispo.Location = new System.Drawing.Point(4, 24);
            this.m_panelDispo.Name = "m_panelDispo";
            this.m_panelDispo.Size = new System.Drawing.Size(308, 232);
            this.cExtStyle1.SetStyleBackColor(this.m_panelDispo, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_panelDispo, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelDispo.TabIndex = 26;
            // 
            // m_wndListeDispos
            // 
            this.m_wndListeDispos.AllowColumnResize = true;
            this.m_wndListeDispos.AllowMultiselect = false;
            this.m_wndListeDispos.AlternateBackground = System.Drawing.Color.DarkGreen;
            this.m_wndListeDispos.AlternatingColors = false;
            this.m_wndListeDispos.AutoHeight = true;
            this.m_wndListeDispos.AutoSort = true;
            this.m_wndListeDispos.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.m_wndListeDispos.CanChangeActivationCheckBoxes = false;
            this.m_wndListeDispos.CheckBoxes = false;
            this.m_wndListeDispos.CheckedItems = ((System.Collections.ArrayList)(resources.GetObject("m_wndListeDispos.CheckedItems")));
            glColumn2.ActiveControlItems = null;
            glColumn2.BackColor = System.Drawing.Color.Transparent;
            glColumn2.ControlType = sc2i.win32.common.ColumnControlTypes.None;
            glColumn2.ForColor = System.Drawing.Color.Black;
            glColumn2.ImageIndex = -1;
            glColumn2.IsCheckColumn = false;
            glColumn2.LastSortState = sc2i.win32.common.ColumnSortState.SortedUp;
            glColumn2.Name = "Column1";
            glColumn2.Propriete = "DescriptionElement";
            glColumn2.Text = "Description";
            glColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn2.Width = 300;
            this.m_wndListeDispos.Columns.AddRange(new sc2i.win32.common.GLColumn[] {
            glColumn2});
            this.m_wndListeDispos.ContexteUtilisation = "";
            this.m_wndListeDispos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeDispos.EnableCustomisation = true;
            this.m_wndListeDispos.FocusedItem = null;
            this.m_wndListeDispos.FullRowSelect = true;
            this.m_wndListeDispos.GLGridLines = sc2i.win32.common.GLGridStyles.gridNone;
            this.m_wndListeDispos.GridColor = System.Drawing.Color.Gray;
            this.m_wndListeDispos.HasImages = false;
            this.m_wndListeDispos.HeaderHeight = 22;
            this.m_wndListeDispos.HeaderStyle = sc2i.win32.common.GLHeaderStyles.Normal;
            this.m_wndListeDispos.HeaderTextColor = System.Drawing.Color.Black;
            this.m_wndListeDispos.HeaderTextFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.m_wndListeDispos.HeaderVisible = true;
            this.m_wndListeDispos.HeaderWordWrap = false;
            this.m_wndListeDispos.HotColumnIndex = -1;
            this.m_wndListeDispos.HotItemIndex = -1;
            this.m_wndListeDispos.HotTracking = false;
            this.m_wndListeDispos.HotTrackingColor = System.Drawing.Color.LightGray;
            this.m_wndListeDispos.ImageList = null;
            this.m_wndListeDispos.ItemHeight = 18;
            this.m_wndListeDispos.ItemWordWrap = false;
            this.m_wndListeDispos.ListeSource = null;
            this.m_wndListeDispos.Location = new System.Drawing.Point(0, 24);
            this.m_wndListeDispos.MaxHeight = 18;
            this.m_wndListeDispos.Name = "m_wndListeDispos";
            this.m_wndListeDispos.SelectedTextColor = System.Drawing.Color.White;
            this.m_wndListeDispos.SelectionColor = System.Drawing.Color.DarkBlue;
            this.m_wndListeDispos.ShowBorder = true;
            this.m_wndListeDispos.ShowFocusRect = false;
            this.m_wndListeDispos.Size = new System.Drawing.Size(308, 208);
            this.m_wndListeDispos.SortIndex = 0;
            this.cExtStyle1.SetStyleBackColor(this.m_wndListeDispos, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_wndListeDispos, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListeDispos.SuperFlatHeaderColor = System.Drawing.Color.White;
            this.m_wndListeDispos.TabIndex = 13;
            this.m_wndListeDispos.Text = "glacialList1";
            this.m_wndListeDispos.TrierAuClicSurEnteteColonne = true;
            this.m_wndListeDispos.DoubleClick += new System.EventHandler(this.m_wndListeDispos_DoubleClick);
            // 
            // m_panelRecherche
            // 
            this.m_panelRecherche.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_panelRecherche.Controls.Add(this.m_txtRecherche);
            this.m_panelRecherche.Controls.Add(this.label5);
            this.m_panelRecherche.Controls.Add(this.m_btnFiltrer);
            this.m_panelRecherche.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelRecherche.ForeColor = System.Drawing.Color.Black;
            this.m_panelRecherche.Location = new System.Drawing.Point(0, 0);
            this.m_panelRecherche.Name = "m_panelRecherche";
            this.m_panelRecherche.Size = new System.Drawing.Size(308, 24);
            this.cExtStyle1.SetStyleBackColor(this.m_panelRecherche, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorFondPanel);
            this.cExtStyle1.SetStyleForeColor(this.m_panelRecherche, sc2i.win32.common.CExtStyle.EnumCouleurs.ColorTexteFenetre);
            this.m_panelRecherche.TabIndex = 22;
            // 
            // m_txtRecherche
            // 
            this.m_txtRecherche.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtRecherche.Location = new System.Drawing.Point(105, 0);
            this.m_txtRecherche.Name = "m_txtRecherche";
            this.m_txtRecherche.Size = new System.Drawing.Size(203, 21);
            this.cExtStyle1.SetStyleBackColor(this.m_txtRecherche, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtRecherche, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtRecherche.TabIndex = 2;
            this.m_txtRecherche.KeyUp += new System.Windows.Forms.KeyEventHandler(this.m_txtRecherche_KeyUp);
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(24, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 24);
            this.cExtStyle1.SetStyleBackColor(this.label5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label5.TabIndex = 1;
            this.label5.Text = "Search|171";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_btnFiltrer
            // 
            this.m_btnFiltrer.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_btnFiltrer.Image = ((System.Drawing.Image)(resources.GetObject("m_btnFiltrer.Image")));
            this.m_btnFiltrer.Location = new System.Drawing.Point(0, 0);
            this.m_btnFiltrer.Name = "m_btnFiltrer";
            this.m_btnFiltrer.Size = new System.Drawing.Size(24, 24);
            this.cExtStyle1.SetStyleBackColor(this.m_btnFiltrer, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnFiltrer, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnFiltrer.TabIndex = 0;
            this.m_btnFiltrer.Click += new System.EventHandler(this.m_btnFiltrer_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(216, 24);
            this.cExtStyle1.SetStyleBackColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label2.TabIndex = 14;
            this.label2.Text = "Available entities|120";
            // 
            // m_btnRemoveOne
            // 
            this.m_btnRemoveOne.Location = new System.Drawing.Point(320, 120);
            this.m_btnRemoveOne.Name = "m_btnRemoveOne";
            this.m_btnRemoveOne.Size = new System.Drawing.Size(40, 24);
            this.cExtStyle1.SetStyleBackColor(this.m_btnRemoveOne, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnRemoveOne, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnRemoveOne.TabIndex = 17;
            this.m_btnRemoveOne.Text = "<";
            this.m_btnRemoveOne.Click += new System.EventHandler(this.m_btnRemoveOne_Click);
            // 
            // m_btnAddOne
            // 
            this.m_btnAddOne.Location = new System.Drawing.Point(320, 56);
            this.m_btnAddOne.Name = "m_btnAddOne";
            this.m_btnAddOne.Size = new System.Drawing.Size(40, 24);
            this.cExtStyle1.SetStyleBackColor(this.m_btnAddOne, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnAddOne, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAddOne.TabIndex = 16;
            this.m_btnAddOne.Text = ">";
            this.m_btnAddOne.Click += new System.EventHandler(this.m_btnAddOne_Click);
            // 
            // m_wndListeSelectionnes
            // 
            this.m_wndListeSelectionnes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.m_wndListeSelectionnes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.listViewAutoFilledColumn1});
            this.m_wndListeSelectionnes.EnableCustomisation = true;
            this.m_wndListeSelectionnes.FullRowSelect = true;
            this.m_wndListeSelectionnes.Location = new System.Drawing.Point(368, 24);
            this.m_wndListeSelectionnes.Name = "m_wndListeSelectionnes";
            this.m_wndListeSelectionnes.Size = new System.Drawing.Size(304, 232);
            this.cExtStyle1.SetStyleBackColor(this.m_wndListeSelectionnes, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_wndListeSelectionnes, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndListeSelectionnes.TabIndex = 19;
            this.m_wndListeSelectionnes.UseCompatibleStateImageBehavior = false;
            this.m_wndListeSelectionnes.View = System.Windows.Forms.View.Details;
            this.m_wndListeSelectionnes.DoubleClick += new System.EventHandler(this.m_wndListeSelectionnes_DoubleClick);
            // 
            // listViewAutoFilledColumn1
            // 
            this.listViewAutoFilledColumn1.Field = "DescriptionElement";
            this.listViewAutoFilledColumn1.PrecisionWidth = 0;
            this.listViewAutoFilledColumn1.ProportionnalSize = false;
            this.listViewAutoFilledColumn1.Text = "Description";
            this.listViewAutoFilledColumn1.Visible = true;
            this.listViewAutoFilledColumn1.Width = 271;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(384, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(255, 24);
            this.cExtStyle1.SetStyleBackColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label3.TabIndex = 20;
            this.label3.Text = "Selected entities|121";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Location = new System.Drawing.Point(0, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Selected = false;
            this.tabPage2.Size = new System.Drawing.Size(680, 263);
            this.cExtStyle1.SetStyleBackColor(this.tabPage2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.tabPage2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.tabPage2.TabIndex = 11;
            this.tabPage2.Title = "Condition";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.splitter1);
            this.panel2.Controls.Add(this.m_wndAide);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(680, 288);
            this.cExtStyle1.SetStyleBackColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel2.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.m_txtCondition);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(517, 288);
            this.cExtStyle1.SetStyleBackColor(this.panel3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel3.TabIndex = 19;
            // 
            // m_txtCondition
            // 
            this.m_txtCondition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtCondition.BackColor = System.Drawing.Color.White;
            this.m_txtCondition.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtCondition.Formule = null;
            this.m_txtCondition.Location = new System.Drawing.Point(8, 24);
            this.m_txtCondition.LockEdition = false;
            this.m_txtCondition.Name = "m_txtCondition";
            this.m_txtCondition.Size = new System.Drawing.Size(504, 256);
            this.cExtStyle1.SetStyleBackColor(this.m_txtCondition, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_txtCondition, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_txtCondition.TabIndex = 16;
            this.m_tooltip.SetToolTip(this.m_txtCondition, "Ce test ne sera integré au filtre que si l\'évaluation de la formule de condition " +
                    "vaut 1|160");
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 16);
            this.cExtStyle1.SetStyleBackColor(this.label4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.label4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label4.TabIndex = 17;
            this.label4.Text = "Enable condition|284";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(517, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 288);
            this.cExtStyle1.SetStyleBackColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.splitter1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.splitter1.TabIndex = 18;
            this.splitter1.TabStop = false;
            // 
            // m_wndAide
            // 
            this.m_wndAide.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_wndAide.FournisseurProprietes = null;
            this.m_wndAide.Location = new System.Drawing.Point(520, 0);
            this.m_wndAide.Name = "m_wndAide";
            this.m_wndAide.ObjetInterroge = null;
            this.m_wndAide.SendIdChamps = false;
            this.m_wndAide.Size = new System.Drawing.Size(160, 288);
            this.cExtStyle1.SetStyleBackColor(this.m_wndAide, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_wndAide, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_wndAide.TabIndex = 15;
            this.m_wndAide.OnSendCommande += new sc2i.win32.expression.SendCommande(this.m_wndAide_OnSendCommande);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.m_btnThis);
            this.panel5.Controls.Add(this.m_btnChamp);
            this.panel5.Controls.Add(this.m_panelComboChamp);
            this.panel5.Location = new System.Drawing.Point(80, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(632, 48);
            this.cExtStyle1.SetStyleBackColor(this.panel5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel5, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel5.TabIndex = 25;
            // 
            // m_btnThis
            // 
            this.m_btnThis.Location = new System.Drawing.Point(0, 24);
            this.m_btnThis.Name = "m_btnThis";
            this.m_btnThis.Size = new System.Drawing.Size(216, 24);
            this.cExtStyle1.SetStyleBackColor(this.m_btnThis, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnThis, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnThis.TabIndex = 6;
            this.m_btnThis.Text = "Direct selection|117";
            this.m_btnThis.CheckedChanged += new System.EventHandler(this.m_btnThis_CheckedChanged);
            // 
            // m_btnChamp
            // 
            this.m_btnChamp.Location = new System.Drawing.Point(0, 0);
            this.m_btnChamp.Name = "m_btnChamp";
            this.m_btnChamp.Size = new System.Drawing.Size(16, 24);
            this.cExtStyle1.SetStyleBackColor(this.m_btnChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnChamp, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnChamp.TabIndex = 5;
            this.m_btnChamp.Text = "radioButton1";
            this.m_btnChamp.CheckedChanged += new System.EventHandler(this.m_btnChamp_CheckedChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.m_btnExclure);
            this.panel4.Controls.Add(this.m_btnInclure);
            this.panel4.Location = new System.Drawing.Point(88, 56);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(600, 16);
            this.cExtStyle1.SetStyleBackColor(this.panel4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.panel4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.panel4.TabIndex = 24;
            // 
            // m_btnExclure
            // 
            this.m_btnExclure.Location = new System.Drawing.Point(216, 0);
            this.m_btnExclure.Name = "m_btnExclure";
            this.m_btnExclure.Size = new System.Drawing.Size(243, 16);
            this.cExtStyle1.SetStyleBackColor(this.m_btnExclure, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnExclure, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnExclure.TabIndex = 22;
            this.m_btnExclure.Text = "Exclude selected elements|119";
            // 
            // m_btnInclure
            // 
            this.m_btnInclure.Location = new System.Drawing.Point(8, 0);
            this.m_btnInclure.Name = "m_btnInclure";
            this.m_btnInclure.Size = new System.Drawing.Size(200, 16);
            this.cExtStyle1.SetStyleBackColor(this.m_btnInclure, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnInclure, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnInclure.TabIndex = 21;
            this.m_btnInclure.Text = "Include selected elements|118";
            // 
            // m_btnAnnuler
            // 
            this.m_btnAnnuler.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnAnnuler.BackColor = System.Drawing.SystemColors.Control;
            this.m_btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnuler.Location = new System.Drawing.Point(368, 375);
            this.m_btnAnnuler.Name = "m_btnAnnuler";
            this.m_btnAnnuler.Size = new System.Drawing.Size(80, 24);
            this.cExtStyle1.SetStyleBackColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnAnnuler, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAnnuler.TabIndex = 12;
            this.m_btnAnnuler.Text = "Cancel|11";
            this.m_btnAnnuler.UseVisualStyleBackColor = false;
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnOk.BackColor = System.Drawing.SystemColors.Control;
            this.m_btnOk.Location = new System.Drawing.Point(240, 375);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(80, 24);
            this.cExtStyle1.SetStyleBackColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this.m_btnOk, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnOk.TabIndex = 11;
            this.m_btnOk.Text = "Ok|10";
            this.m_btnOk.UseVisualStyleBackColor = false;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_menuNewVariable
            // 
            this.m_menuNewVariable.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuVariableSaisie,
            this.m_menuVariableCalculée,
            this.m_menuVariableSelection});
            // 
            // m_menuVariableSaisie
            // 
            this.m_menuVariableSaisie.Index = 0;
            this.m_menuVariableSaisie.Text = "Entered data|285";
            this.m_menuVariableSaisie.Click += new System.EventHandler(this.m_menuVariableSaisie_Click);
            // 
            // m_menuVariableCalculée
            // 
            this.m_menuVariableCalculée.Index = 1;
            this.m_menuVariableCalculée.Text = "Calculated field|286";
            this.m_menuVariableCalculée.Click += new System.EventHandler(this.m_menuVariableCalculée_Click);
            // 
            // m_menuVariableSelection
            // 
            this.m_menuVariableSelection.Index = 2;
            this.m_menuVariableSelection.Text = "Element Selection|287";
            this.m_menuVariableSelection.Click += new System.EventHandler(this.m_menuVariableSelection_Click);
            // 
            // CFormEditComposantFiltreSelectionStatique
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.CancelButton = this.m_btnAnnuler;
            this.ClientSize = new System.Drawing.Size(712, 405);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CFormEditComposantFiltreSelectionStatique";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.cExtStyle1.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.cExtStyle1.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.Text = "Selection|116";
            this.Load += new System.EventHandler(this.CFormEditComposantFiltreSelectionStatique_Load);
            this.m_panelComboChamp.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.c2iTabControl1.ResumeLayout(false);
            this.c2iTabControl1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.m_panelDispo.ResumeLayout(false);
            this.m_panelRecherche.ResumeLayout(false);
            this.m_panelRecherche.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// ////////////////////////////////////////////////////////////////////////////
		private bool FiltreChamp ( CDefinitionProprieteDynamique def )
		{
			if ( typeof(CObjetDonneeAIdNumerique).IsAssignableFrom ( def.TypeDonnee.TypeDotNetNatif ) )
				return true;
			return false;
		}
		/// <summary>
		/// ////////////////////////////////////////////////////////////////////////////
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_boutonDropList_Click(object sender, System.EventArgs e)
		{
			Rectangle rect = m_panelComboChamp.RectangleToScreen(new Rectangle ( 0, m_panelComboChamp.Height, m_panelComboChamp.Width, 230));
			bool bCancel = false;
			CDefinitionProprieteDynamique champ = CFormSelectChampPopup.SelectDefinitionChamp( 
				rect, 
				m_filtre.TypeElements , 
				new CFournisseurProprietesForFiltreDynamique(), 
				ref bCancel,
				new BeforeIntegrerChampEventHandler(FiltreChamp),
				null);
			if ( !bCancel )
			{
				m_champ = champ;
				m_labelChamp.Text = m_champ==null?I.T("[UNDEFINED]|30013"):m_champ.Nom;
				FillListeDispos();
			}
				
		}

		/// ////////////////////////////////////////////////////////////////////////////
		private void Init ( CComposantFiltreDynamiqueSelectionStatique composant, CFiltreDynamique filtre )
		{
			m_composant = composant;
			m_filtre = filtre;
			
		}

		/// ////////////////////////////////////////////////////////////////////////////
		public static bool EditeComposant ( 
			CComposantFiltreDynamiqueSelectionStatique composant,
			CFiltreDynamique filtre )
		{
			CFormEditComposantFiltreSelectionStatique form = new CFormEditComposantFiltreSelectionStatique();
			form.Init ( composant, filtre );
			bool bResult = form.ShowDialog() == DialogResult.OK;
			form.Dispose();
			return bResult;
		}


		/// ////////////////////////////////////////////////////////////////////////////
		private void CFormEditComposantFiltreSelectionStatique_Load(object sender, System.EventArgs e)
		{
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);
            
            InitDialog();
		}

		/// ////////////////////////////////////////////////////////////////////////////
		public void InitDialog()
		{
            m_labelChamp.Text = m_composant.Champ == null ? I.T("[UNDEFINED]|30013") : m_composant.Champ.Nom;
			m_champ = m_composant.Champ;
			m_wndAide.FournisseurProprietes = m_filtre;
			m_wndAide.ObjetInterroge = typeof(CFiltreDynamique);
			m_txtCondition.Init(m_wndAide.FournisseurProprietes, m_wndAide.ObjetInterroge);
			m_txtCondition.Text = m_composant.ConditionApplication.GetString();
			if ( m_composant.ExclureLesElementsSelectionnes )
				m_btnExclure.Checked = true;
			else
				m_btnExclure.Checked = false;
			m_btnInclure.Checked = !m_btnExclure.Checked;

			m_btnThis.Text = I.T("Selection of @1|30017",DynamicClassAttribute.GetNomConvivial ( m_filtre.TypeElements ));

			m_btnThis.Checked = m_composant.Champ is CDefinitionProprieteDynamiqueThis;
			m_btnChamp.Checked = !m_btnThis.Checked;

			FillListeDispos();
		}

		/// ////////////////////////////////////////////////////////////////////////////
		private CDefinitionProprieteDynamique m_lastChamp = null;
		private void FillListeDispos()
		{
			if ( m_champ != null && !m_champ.Equals(m_lastChamp))
			{
				m_lastChamp = m_champ;
				Type tp = m_champ.TypeDonnee.TypeDotNetNatif;
				if ( !typeof ( CObjetDonneeAIdNumerique ).IsAssignableFrom ( tp ) )
				{
					m_lblFonctionNonDispo.Visible = true;
					return;
				}
				m_lblFonctionNonDispo.Visible = false;
				CListeObjetsDonnees listeSource = new CListeObjetsDonnees ( m_contexte, tp );
                string strChampAffiche = "";
                strChampAffiche = DescriptionFieldAttribute.GetDescriptionField(tp, "Libelle", true);
                if (strChampAffiche == "")
                    strChampAffiche = "DescriptionElement";
                if ( m_wndListeDispos.Columns.Count > 0 )
                    m_wndListeDispos.Columns[0].Propriete = strChampAffiche;
                m_filtreRapide = CFournisseurFiltreRapide.GetFiltreRapideForType(tp);
                m_panelRecherche.Visible = m_filtreRapide != null;
                m_txtRecherche.Text = "";

				listeSource.RemplissageProgressif = true;
				m_wndListeDispos.ListeSource = listeSource;
				m_wndListeDispos.Refresh();

				m_wndListeSelectionnes.Items.Clear();
				m_listeSelectionnes = new ArrayList();
				if ( m_champ.Equals(m_composant.Champ) )
				{
					foreach ( CDbKey key in m_composant.ListeIdentifiants )
					{
						CObjetDonnee objet = (CObjetDonnee)Activator.CreateInstance ( tp, new object[]{m_contexte} );
						if ( objet.ReadIfExists ( key ) )
						{
							m_listeSelectionnes.Add ( objet );
						}
					}
					m_wndListeSelectionnes.Remplir ( m_listeSelectionnes, false );
				}
			}
			else if ( m_champ == null )
			{
				m_lblFonctionNonDispo.Visible = true;
				return;
			}
		}




		/// ////////////////////////////////////////////////////////////////////////////
		private void m_menuVariableSaisie_Click(object sender, System.EventArgs e)
		{
			CVariableDynamiqueSaisie variable = new CVariableDynamiqueSaisie ( m_filtre );
			if ( CFormEditVariableDynamiqueSaisie.EditeVariable ( variable, m_filtre ) )
			{
                m_filtre.AddVariable(variable);
				m_wndAide.FournisseurProprietes = m_filtre;
				m_wndAide.RefillChamps();
			}
		}

		private void panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		
		}

		private void m_wndAide_OnSendCommande(string strCommande, int nPosCurseur)
		{
			if ( m_lastTextBox != null )
				m_wndAide.InsereInTextBox ( m_lastTextBox, nPosCurseur, strCommande );
		}

		/// ////////////////////////////////////////////////////////////////////////////
		private void m_btnOk_Click(object sender, System.EventArgs e)
		{
			if ( m_champ == null )
			{
				CFormAlerte.Afficher(I.T("Select a field to test|30014"), EFormAlerteType.Exclamation);
				return;
			}
			CContexteAnalyse2iExpression ctx = new CContexteAnalyse2iExpression ( m_filtre, typeof(CFiltreDynamique) );
			CAnalyseurSyntaxiqueExpression analyseur = new CAnalyseurSyntaxiqueExpression(ctx);

			CResultAErreur result = analyseur.AnalyseChaine ( m_txtCondition.Text );
			if ( !result )
			{
				result.EmpileErreur(I.T("Error in condition formula|30016"));
				CFormAlerte.Afficher ( result);
				return;
			}
			C2iExpression expressionCondition = (C2iExpression)result.Data;
			
			m_composant.Champ = m_champ;
			m_composant.ConditionApplication = expressionCondition;
			m_composant.ExclureLesElementsSelectionnes = m_btnExclure.Checked;

			ArrayList lst = new ArrayList();
			foreach ( CObjetDonnee obj in m_listeSelectionnes )
				lst.Add ( obj.DbKey );
			m_composant.ListeIdentifiants = (CDbKey[])lst.ToArray(typeof(CDbKey));
			DialogResult = DialogResult.OK;
			Close();
		}

		/// ////////////////////////////////////////////////////////////////////////////
		private void m_menuVariableCalculée_Click(object sender, System.EventArgs e)
		{
			CVariableDynamiqueCalculee variable = new CVariableDynamiqueCalculee(m_filtre );
			if ( CFormEditVariableFiltreCalculee.EditeVariable(variable, m_filtre) )
			{
                m_filtre.AddVariable(variable);
				m_wndAide.RefillChamps();
			}
		}

		/// ////////////////////////////////////////////////////////////////////////////
		private void m_txtFormule_Enter(object sender, System.EventArgs e)
		{
			if ( !(sender is sc2i.win32.expression.CControleEditeFormule) )
				return;
			if ( m_lastTextBox != null )
			{
				m_lastTextBox.BackColor = Color.White;
			}
			m_lastTextBox = (sc2i.win32.expression.CControleEditeFormule)sender;
			m_lastTextBox.BackColor = Color.LightGreen;
		}

		/// ////////////////////////////////////////////////////////////////////////////
		private void m_menuVariableSelection_Click(object sender, System.EventArgs e)
		{
			CVariableDynamiqueSelectionObjetDonnee variable = new CVariableDynamiqueSelectionObjetDonnee(m_filtre );
			if ( CFormEditVariableDynamiqueSelectionObjetDonnee.EditeVariable(variable) )
			{
                m_filtre.AddVariable(variable);
				m_wndAide.RefillChamps();
			}
		}

		private void m_btnAddOne_Click(object sender, System.EventArgs e)
		{
			if ( m_wndListeDispos.SelectedItems.Count != 0 )
			{
				foreach ( CObjetDonnee objet in m_wndListeDispos.SelectedItems )
				{
					if ( !m_listeSelectionnes.Contains ( objet ) )
						m_listeSelectionnes.Add ( objet );
				}
				m_wndListeSelectionnes.Remplir ( m_listeSelectionnes );
				m_wndListeSelectionnes.EnsureVisible ( m_listeSelectionnes.Count-1 );
			}
		}

		private void m_btnRemoveOne_Click(object sender, System.EventArgs e)
		{
			if ( m_wndListeSelectionnes.SelectedItems.Count != 0 )
			{
				ArrayList lst = new ArrayList (m_wndListeSelectionnes.SelectedItems); 
				foreach ( ListViewItem item in lst )
				{
					m_listeSelectionnes.Remove ( item.Tag );
					m_wndListeSelectionnes.Items.Remove ( item );
				}
				
			}
		}

		private void m_wndListeDispos_DoubleClick(object sender, System.EventArgs e)
		{
			m_btnAddOne_Click ( m_wndListeDispos, new EventArgs( ) );
		}

		private void m_wndListeSelectionnes_DoubleClick(object sender, System.EventArgs e)
		{
			m_btnRemoveOne_Click ( m_wndListeSelectionnes, new EventArgs()) ;
		}

		private void m_btnChamp_CheckedChanged(object sender, System.EventArgs e)
		{
			UpdateDispoControles();
		}

		private void m_btnThis_CheckedChanged(object sender, System.EventArgs e)
		{
			UpdateDispoControles();
		}


		/// ////////////////////////////////////////////////////////////////////////////
		private void UpdateDispoControles()
		{
			m_panelComboChamp.Enabled = m_btnChamp.Checked;
			if ( m_btnThis.Checked )
			{
				m_champ = new CDefinitionProprieteDynamiqueThis ( 
					new CTypeResultatExpression ( m_filtre.TypeElements, false),
					true, true );
				m_labelChamp.Text = "";
			}
			FillListeDispos();
		}

        private void m_btnFiltrer_Click(object sender, EventArgs e)
        {
            CListeObjetsDonnees lst = m_wndListeDispos.ListeSource as CListeObjetsDonnees;
            if (lst != null)
            {
                if (m_filtreRapide != null && m_txtRecherche.Text.Trim() != "")
                {
                    if (m_filtreRapide.Parametres.Count < 1)
                        m_filtreRapide.Parametres.Add("%"+m_txtRecherche.Text+"%");
                    else
                        m_filtreRapide.Parametres[0] = "%"+m_txtRecherche.Text+"%";
                    lst.Filtre = m_filtreRapide;
                    m_wndListeDispos.ListeSource = lst;
                    m_wndListeDispos.Refresh();
                }
                else
                {
                    lst.Filtre = null;
                    m_wndListeDispos.Refresh();
                }
            }
        }

        private void m_txtRecherche_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                m_btnFiltrer_Click(m_txtRecherche, new EventArgs());
        }
		


	}
}
