using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Reflection;
using System.ComponentModel.Design;
using System.Drawing.Design;

using sc2i.win32.data;
using sc2i.data;
using sc2i.data.dynamic;
using sc2i.common;
using sc2i.win32.navigation;
using sc2i.win32.common;
using sc2i.win32.data.dynamic;
using sc2i.multitiers.client;
using System.Collections.Generic;
using System.Text;
using sc2i.expression;
using sc2i.common.Restrictions;
using sc2i.win32.common.customizableList;
using sc2i.win32.data.navigation.filtre;
using sc2i.formulaire;



namespace sc2i.win32.data.navigation
{
    public interface IControlDefinitionFiltre
    {
        event EventHandler OnAppliqueFiltre;
        CFiltreData Filtre { get; set; }
        int MinHeight { get; }
        void FillContexte(CContexteFormNavigable ctx);
        void InitFromContexte(CContexteFormNavigable ctx);
        bool ShouldShow();

        void AppliquerFiltre();

        //Affecte les valeurs que le filtre permet d'affecter à un nouvel objet donnee
        void AffecteValeursToNewObjet(CObjetDonnee objet);

        CResultAErreur SerializeFiltre(C2iSerializer serializer);
    }

    public delegate void OnNewObjetDonneeEventHandler(object sender, CObjetDonnee nouvelObjet, ref bool bCancel);

    /// <summary>
    /// Description résumée de CPanelListeSpeedStandard.
    /// </summary>
    [Serializable]
    public class CPanelListeSpeedStandard : System.Windows.Forms.UserControl, IControlALockEdition, IControleAGestionRestrictions
    {
        /// <summary>
        /// Permet de différencier les différentes utilisation d'une même liste
        /// </summary>
        private string m_strContexteUtilisation = "";
        private List<int> m_listeIdCheckedFromContexte = null;
        private bool? m_bPanelFiltreVisibleFromContexte = null;

        public bool TrierAuClicSurEnteteColonne
        {
            get
            {
                if (m_listView != null)
                    return m_listView.TrierAuClicSurEnteteColonne;
                return false;
            }
            set
            {
                if (m_listView != null)
                    m_listView.TrierAuClicSurEnteteColonne = value;
            }
        }

        private CControlPourListeDeListePanelSpeed m_controleListe = null;

        private bool m_bListeDeListeIsCollapse = true;
        private bool m_bListeDeListeIsInit = false;

        private CListeObjetsDonnees m_listeFiltresPossibles = null;

        //Filtre qui sera appliqué systèmatiquement en And sur le filtre data
        private CFiltreData m_filtreDeBase = null;

        //Indique si le filtre de base a été mis dans le filtre de la liste
        private bool m_bFiltreDeBaseAppliqueAListe = false;

        private CGestionnaireAjoutModifSuppObjetDonnee m_gestionnaireModifs = null;
        private bool m_bShowBoutonAjouter = true;
        private bool m_bShowBoutonModifier = true;
        private bool m_bShowBoutonSupprimer = true;
        private bool m_bShowBoutonExporter = true;
        private bool m_bShowBoutonFiltrer = true;
        private bool m_bShowListes = true;
        private bool m_bBoutonExtraiteVisible = true;

        [NonSerialized]
        List<CAffectationsProprietes> m_listeAffectationsInitiales = new List<CAffectationsProprietes>();
        [NonSerialized]
        private object m_objetReferencePourAffectationsInitiales = null;

        /// <summary>
        /// Indique si on doit stocker les colonnes et les relire du registre
        /// </summary>
        private bool m_bAlowSerializePreferences = true;

        private int m_nLargeurListeDeListe = 120;

        private CRestrictionUtilisateurSurType m_restrictionAppliquee = null;
        private CListeRestrictionsUtilisateurSurType m_listeRestrictions = null;
        private IGestionnaireReadOnlySysteme m_gestionnaireReadOnly = null;

        private bool m_bCheckBoxes = false;

        //Filtre à appliquer à la liste
        CFiltreData m_filtre = null;
        private string m_strQuickSearchText = "";

        private CListeEntites m_listeEntitesEnCours = null;


        private bool m_bAllowArbre = true;

        private bool m_bModeArbre = false;

        CFiltreData m_filtreRechercheRapide = null;

        private bool m_bModeQuickSearch = false;

        //Contrôle pour filtre built in (compilé)
        private IControlDefinitionFiltre m_controlFiltreStandard;

        //Filtre dynamique preferé (imposé à la création de la fenêtre)
        private CFiltreDynamique m_filtrePrefere = null;
        private ContextMenuStrip m_menuExport;
        private ToolStripMenuItem m_menuItemExportSimple;
        private ToolStripMenuItem m_menuItemExportAvance;
        private Panel m_panelBarreOutils;
        private PictureBox m_imgExport;
        private Splitter splitter1;
        private ToolStripMenuItem m_menuCopier;
        private ToolStripSeparator toolStripMenuItem1;
        private Panel m_panelListesEntites;
        private Panel m_panelListeOuArbre;
        private Splitter m_splitterListeListes;
        private Panel m_panelMargeListeDeListe;
        private PictureBox m_btnCollapseExpandListeDeListe;
        private Label label2;
        private sc2i.win32.common.customizableList.CCustomizableList m_wndListeListes;
        private Panel panel1;
        private PictureBox m_btnNoListe;
        private TextBox m_txtFiltreListe;
        private Button m_btnFiltreListes;
        private PictureBox m_btnExtractList;
        private Label label3;
        private PictureBox m_btnSaveList;
        private Label label4;
        private CWndLinkStd m_lnkEditListe;

        //Contrôle de filtre en cours
        private IControlDefinitionFiltre m_controlFiltreEnCours;

        public ColumnSortState? SensTrieColonneTriee
        {
            get
            {
                if (m_listView != null)
                    try
                    {
                        return m_listView.Columns[IndexColonneTriee].LastSortState;
                    }
                    catch
                    {
                    }
                return null;
            }
        }

        public int IndexColonneTriee
        {
            get
            {
                if (m_listView != null)
                    return m_listView.SortIndex;
                else
                    return -1;
            }
        }
        public void TrierSurLaColonne(int nCol)
        {
            if (m_listView != null)
                m_listView.SortColumn(nCol);
        }



        private CFiltreDynamique m_filtreTemporaire = null;



        private CListeObjetsDonnees m_listeObjets = null;
        protected sc2i.win32.common.CWndLinkStd m_lnkAjouter;
        protected sc2i.win32.common.CWndLinkStd m_lnkModifier;
        protected sc2i.win32.common.CWndLinkStd m_lnkSupprimer;
        private System.Windows.Forms.Panel m_panelListeEtFiltre;
        private System.Windows.Forms.Panel m_panelFiltre;
        private System.Windows.Forms.Splitter m_splitter;

        private bool m_bModeSelection = false;
        private CObjetDonneeAIdNumeriqueAuto m_objetDoubleClicked;
        private System.Windows.Forms.ContextMenuStrip m_menuFiltres;
        private System.Windows.Forms.Panel m_wndStatusBar;
        private System.Windows.Forms.Label m_statusText;
        protected sc2i.win32.data.CWndListeObjetsDonnees m_listView;
        protected sc2i.win32.data.navigation.CPanelFiltreFormListStd m_panelFiltreStd;
        protected sc2i.win32.common.CWndLinkStd m_lnkFiltrer;
        private System.Windows.Forms.Panel m_panelFiltreEtOutils;
        private System.Windows.Forms.Panel m_panelOutilsFiltre;
        private System.Windows.Forms.Button m_btnListeFiltres;
        private System.Windows.Forms.Button m_btnAppliquer;
        private System.Windows.Forms.Button m_btnPreference;
        private sc2i.win32.common.CToolTipTraductible m_tooltip;
        protected System.Windows.Forms.Label m_lblPageInterdite;
        private RadioButton m_chkVueListe;
        private RadioButton m_chkVueArbre;
        private CArbreObjetsDonneesHierarchiques m_arbre;
        private Panel m_panelChoixListeOuHierarchie;
        private Timer m_timerRemplir;
        private Panel m_panelDataInvalide;
        private Label label1;
        private PictureBox pictureBox4;
        private PictureBox pictureBox3;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private Label m_panelLoading;
        protected CExtModeEdition m_gestionnaireModeEdition;
        private System.ComponentModel.IContainer components;

        public event AfterCreateFormEditionEventHandler AfterCreateFormEdition;

        public event EventHandler AfterDeleteElement;
        //-------------------------------------------------------------------
        public CPanelListeSpeedStandard()
        {
            // Cet appel est requis par le Concepteur de formulaires Windows.Forms.
            InitializeComponent();

            //this.Load += new EventHandler( CPanelListeSpeedStandard_Load );
            m_controleListe = new CControlPourListeDeListePanelSpeed();
            m_controleListe.OnSelectList += new CControlPourListeDeListePanelSpeed.SelectListEventHandler(m_controleListe_OnSelectList);
            m_wndListeListes.ItemControl = m_controleListe;
            m_nLargeurListeDeListe = CSc2iWin32DataNavigationRegistre.LargeurListeDeListeDansPanelListeSpeed;
            CollapseListeDeListes();
            CWin32Traducteur.Translate(m_menuExport);
        }


        //-------------------------------------------------------------------
        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPanelListeSpeedStandard));
            this.m_panelListeEtFiltre = new System.Windows.Forms.Panel();
            this.m_panelListeOuArbre = new System.Windows.Forms.Panel();
            this.m_listView = new sc2i.win32.data.CWndListeObjetsDonnees();
            this.m_arbre = new sc2i.win32.data.CArbreObjetsDonneesHierarchiques();
            this.m_splitterListeListes = new System.Windows.Forms.Splitter();
            this.m_panelListesEntites = new System.Windows.Forms.Panel();
            this.m_wndListeListes = new sc2i.win32.common.customizableList.CCustomizableList();
            this.m_txtFiltreListe = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.m_btnFiltreListes = new System.Windows.Forms.Button();
            this.m_btnNoListe = new System.Windows.Forms.PictureBox();
            this.m_lnkEditListe = new sc2i.win32.common.CWndLinkStd();
            this.m_panelMargeListeDeListe = new System.Windows.Forms.Panel();
            this.m_btnCollapseExpandListeDeListe = new System.Windows.Forms.PictureBox();
            this.m_wndStatusBar = new System.Windows.Forms.Panel();
            this.m_statusText = new System.Windows.Forms.Label();
            this.m_splitter = new System.Windows.Forms.Splitter();
            this.m_panelFiltreEtOutils = new System.Windows.Forms.Panel();
            this.m_panelFiltre = new System.Windows.Forms.Panel();
            this.m_panelFiltreStd = new sc2i.win32.data.navigation.CPanelFiltreFormListStd();
            this.m_panelOutilsFiltre = new System.Windows.Forms.Panel();
            this.m_btnPreference = new System.Windows.Forms.Button();
            this.m_btnAppliquer = new System.Windows.Forms.Button();
            this.m_btnListeFiltres = new System.Windows.Forms.Button();
            this.m_menuFiltres = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_timerRemplir = new System.Windows.Forms.Timer(this.components);
            this.m_panelDataInvalide = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.m_panelLoading = new System.Windows.Forms.Label();
            this.m_menuExport = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_menuCopier = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_menuItemExportSimple = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuItemExportAvance = new System.Windows.Forms.ToolStripMenuItem();
            this.m_tooltip = new sc2i.win32.common.CToolTipTraductible(this.components);
            this.m_imgExport = new System.Windows.Forms.PictureBox();
            this.m_btnExtractList = new System.Windows.Forms.PictureBox();
            this.m_btnSaveList = new System.Windows.Forms.PictureBox();
            this.m_gestionnaireModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_lblPageInterdite = new System.Windows.Forms.Label();
            this.m_panelBarreOutils = new System.Windows.Forms.Panel();
            this.m_panelChoixListeOuHierarchie = new System.Windows.Forms.Panel();
            this.m_chkVueListe = new System.Windows.Forms.RadioButton();
            this.m_chkVueArbre = new System.Windows.Forms.RadioButton();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.m_lnkSupprimer = new sc2i.win32.common.CWndLinkStd();
            this.m_lnkModifier = new sc2i.win32.common.CWndLinkStd();
            this.m_lnkAjouter = new sc2i.win32.common.CWndLinkStd();
            this.m_lnkFiltrer = new sc2i.win32.common.CWndLinkStd();
            this.m_panelListeEtFiltre.SuspendLayout();
            this.m_panelListeOuArbre.SuspendLayout();
            this.m_panelListesEntites.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnNoListe)).BeginInit();
            this.m_panelMargeListeDeListe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnCollapseExpandListeDeListe)).BeginInit();
            this.m_wndStatusBar.SuspendLayout();
            this.m_panelFiltreEtOutils.SuspendLayout();
            this.m_panelFiltre.SuspendLayout();
            this.m_panelOutilsFiltre.SuspendLayout();
            this.m_panelDataInvalide.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.m_menuExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_imgExport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnExtractList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnSaveList)).BeginInit();
            this.m_panelBarreOutils.SuspendLayout();
            this.m_panelChoixListeOuHierarchie.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panelListeEtFiltre
            // 
            this.m_panelListeEtFiltre.Controls.Add(this.m_panelListeOuArbre);
            this.m_panelListeEtFiltre.Controls.Add(this.m_splitterListeListes);
            this.m_panelListeEtFiltre.Controls.Add(this.m_panelListesEntites);
            this.m_panelListeEtFiltre.Controls.Add(this.m_wndStatusBar);
            this.m_panelListeEtFiltre.Controls.Add(this.m_splitter);
            this.m_panelListeEtFiltre.Controls.Add(this.m_panelFiltreEtOutils);
            this.m_panelListeEtFiltre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelListeEtFiltre.Location = new System.Drawing.Point(0, 26);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelListeEtFiltre, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelListeEtFiltre.Name = "m_panelListeEtFiltre";
            this.m_panelListeEtFiltre.Size = new System.Drawing.Size(781, 407);
            this.m_panelListeEtFiltre.TabIndex = 7;
            // 
            // m_panelListeOuArbre
            // 
            this.m_panelListeOuArbre.Controls.Add(this.m_listView);
            this.m_panelListeOuArbre.Controls.Add(this.m_arbre);
            this.m_panelListeOuArbre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelListeOuArbre.Location = new System.Drawing.Point(161, 108);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelListeOuArbre, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelListeOuArbre.Name = "m_panelListeOuArbre";
            this.m_panelListeOuArbre.Size = new System.Drawing.Size(620, 283);
            this.m_panelListeOuArbre.TabIndex = 12;
            // 
            // m_listView
            // 
            this.m_listView.AllowColumnResize = true;
            this.m_listView.AllowMultiselect = false;
            this.m_listView.AlternateBackground = System.Drawing.Color.WhiteSmoke;
            this.m_listView.AlternatingColors = true;
            this.m_listView.AutoHeight = true;
            this.m_listView.AutoSort = true;
            this.m_listView.BackColor = System.Drawing.Color.White;
            this.m_listView.CanChangeActivationCheckBoxes = true;
            this.m_listView.CheckBoxes = false;
            this.m_listView.CheckedItems = ((System.Collections.ArrayList)(resources.GetObject("m_listView.CheckedItems")));
            this.m_listView.ContexteUtilisation = "";
            this.m_listView.EnableCustomisation = true;
            this.m_listView.FocusedItem = null;
            this.m_listView.FullRowSelect = true;
            this.m_listView.GLGridLines = sc2i.win32.common.GLGridStyles.gridSolid;
            this.m_listView.GridColor = System.Drawing.Color.Gray;
            this.m_listView.HasImages = false;
            this.m_listView.HeaderHeight = 22;
            this.m_listView.HeaderStyle = sc2i.win32.common.GLHeaderStyles.SuperFlat;
            this.m_listView.HeaderTextColor = System.Drawing.Color.Black;
            this.m_listView.HeaderTextFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_listView.HeaderVisible = true;
            this.m_listView.HeaderWordWrap = false;
            this.m_listView.HotColumnIndex = -1;
            this.m_listView.HotItemIndex = -1;
            this.m_listView.HotTracking = true;
            this.m_listView.HotTrackingColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_listView.ImageList = null;
            this.m_listView.ItemHeight = 18;
            this.m_listView.ItemWordWrap = false;
            this.m_listView.ListeSource = null;
            this.m_listView.Location = new System.Drawing.Point(146, 79);
            this.m_listView.MaxHeight = 0;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_listView, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_listView.Name = "m_listView";
            this.m_listView.SelectedTextColor = System.Drawing.Color.White;
            this.m_listView.SelectionColor = System.Drawing.Color.DarkBlue;
            this.m_listView.ShowBorder = true;
            this.m_listView.ShowFocusRect = true;
            this.m_listView.Size = new System.Drawing.Size(397, 152);
            this.m_listView.SortIndex = 0;
            this.m_listView.SuperFlatHeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.m_listView.TabIndex = 8;
            this.m_listView.TrierAuClicSurEnteteColonne = true;
            this.m_listView.Visible = false;
            this.m_listView.OnBeginDragItem += new sc2i.win32.common.GlacialList.DragItemEventHandler(this.m_listView_OnBeginDragItem);
            this.m_listView.OnGetImage += new sc2i.win32.common.GlacialListGetImageEventHandler(this.m_listView_OnGetImage);
            this.m_listView.OnChangeSelection += new System.EventHandler(this.m_listView_OnChangeSelection);
            this.m_listView.DoubleClick += new System.EventHandler(this.m_listView_DoubleClick);
            // 
            // m_arbre
            // 
            this.m_arbre.AddRootForAll = false;
            this.m_arbre.AutoriserFilsDesAutorises = true;
            this.m_arbre.CheckBoxes = true;
            this.m_arbre.ElementSelectionne = null;
            this.m_arbre.ElementsSelectionnes = new sc2i.data.CObjetDonnee[0];
            this.m_arbre.ForeColorNonSelectionnable = System.Drawing.Color.DarkGray;
            this.m_arbre.Location = new System.Drawing.Point(371, 108);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_arbre, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_arbre.Name = "m_arbre";
            this.m_arbre.RootLabel = "Root";
            this.m_arbre.Size = new System.Drawing.Size(306, 141);
            this.m_arbre.TabIndex = 10;
            this.m_arbre.Visible = false;
            this.m_arbre.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.m_arbre_AfterSelect);
            this.m_arbre.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.m_arbre_NodeMouseDoubleClick);
            // 
            // m_splitterListeListes
            // 
            this.m_splitterListeListes.Location = new System.Drawing.Point(158, 108);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_splitterListeListes, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_splitterListeListes.Name = "m_splitterListeListes";
            this.m_splitterListeListes.Size = new System.Drawing.Size(3, 283);
            this.m_splitterListeListes.TabIndex = 11;
            this.m_splitterListeListes.TabStop = false;
            // 
            // m_panelListesEntites
            // 
            this.m_panelListesEntites.Controls.Add(this.m_wndListeListes);
            this.m_panelListesEntites.Controls.Add(this.m_txtFiltreListe);
            this.m_panelListesEntites.Controls.Add(this.panel1);
            this.m_panelListesEntites.Controls.Add(this.m_panelMargeListeDeListe);
            this.m_panelListesEntites.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_panelListesEntites.Location = new System.Drawing.Point(0, 108);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelListesEntites, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelListesEntites.Name = "m_panelListesEntites";
            this.m_panelListesEntites.Size = new System.Drawing.Size(158, 283);
            this.m_panelListesEntites.TabIndex = 11;
            this.m_panelListesEntites.SizeChanged += new System.EventHandler(this.m_panelListesEntites_SizeChanged);
            // 
            // m_wndListeListes
            // 
            this.m_wndListeListes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(187)))), ((int)(((byte)(255)))));
            this.m_wndListeListes.CurrentItemIndex = null;
            this.m_wndListeListes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_wndListeListes.ItemControl = null;
            this.m_wndListeListes.Items = new sc2i.win32.common.customizableList.CCustomizableListItem[0];
            this.m_wndListeListes.Location = new System.Drawing.Point(0, 46);
            this.m_wndListeListes.LockEdition = false;
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_wndListeListes, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_wndListeListes.Name = "m_wndListeListes";
            this.m_wndListeListes.Size = new System.Drawing.Size(148, 237);
            this.m_wndListeListes.TabIndex = 8;
            // 
            // m_txtFiltreListe
            // 
            this.m_txtFiltreListe.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_txtFiltreListe.Location = new System.Drawing.Point(0, 26);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_txtFiltreListe, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_txtFiltreListe.Name = "m_txtFiltreListe";
            this.m_txtFiltreListe.Size = new System.Drawing.Size(148, 20);
            this.m_txtFiltreListe.TabIndex = 10;
            this.m_txtFiltreListe.Visible = false;
            this.m_txtFiltreListe.TextChanged += new System.EventHandler(this.m_txtFiltre_TextChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.m_btnFiltreListes);
            this.panel1.Controls.Add(this.m_btnNoListe);
            this.panel1.Controls.Add(this.m_lnkEditListe);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.panel1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(148, 26);
            this.panel1.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(17, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 26);
            this.label2.TabIndex = 7;
            this.label2.Text = "Lists|20004";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_btnFiltreListes
            // 
            this.m_btnFiltreListes.BackColor = System.Drawing.Color.Transparent;
            this.m_btnFiltreListes.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_btnFiltreListes.FlatAppearance.BorderSize = 0;
            this.m_btnFiltreListes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnFiltreListes.Image = global::sc2i.win32.data.navigation.Properties.Resources.filtre_fleche_bas2;
            this.m_btnFiltreListes.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnFiltreListes, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnFiltreListes.Name = "m_btnFiltreListes";
            this.m_btnFiltreListes.Size = new System.Drawing.Size(17, 26);
            this.m_btnFiltreListes.TabIndex = 9;
            this.m_btnFiltreListes.UseVisualStyleBackColor = false;
            this.m_btnFiltreListes.Click += new System.EventHandler(this.m_txtFiltreListe_Click);
            // 
            // m_btnNoListe
            // 
            this.m_btnNoListe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.m_btnNoListe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnNoListe.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnNoListe.Image = global::sc2i.win32.data.navigation.Properties.Resources.Annuler;
            this.m_btnNoListe.Location = new System.Drawing.Point(95, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnNoListe, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnNoListe.Name = "m_btnNoListe";
            this.m_btnNoListe.Size = new System.Drawing.Size(27, 26);
            this.m_btnNoListe.TabIndex = 8;
            this.m_btnNoListe.TabStop = false;
            this.m_btnNoListe.VisibleChanged += new System.EventHandler(this.m_btnNoListe_VisibleChanged);
            this.m_btnNoListe.Click += new System.EventHandler(this.m_btnNoListe_Click);
            // 
            // m_lnkEditListe
            // 
            this.m_lnkEditListe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkEditListe.CustomImage = ((System.Drawing.Image)(resources.GetObject("m_lnkEditListe.CustomImage")));
            this.m_lnkEditListe.CustomText = "Detail";
            this.m_lnkEditListe.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_lnkEditListe.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkEditListe.Location = new System.Drawing.Point(122, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkEditListe, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lnkEditListe.Name = "m_lnkEditListe";
            this.m_lnkEditListe.ShortMode = false;
            this.m_lnkEditListe.Size = new System.Drawing.Size(26, 26);
            this.m_lnkEditListe.TabIndex = 10;
            this.m_lnkEditListe.TypeLien = sc2i.win32.common.TypeLinkStd.Modification;
            this.m_lnkEditListe.LinkClicked += new System.EventHandler(this.m_lnkEditListe_LinkClicked);
            // 
            // m_panelMargeListeDeListe
            // 
            this.m_panelMargeListeDeListe.BackColor = System.Drawing.Color.Transparent;
            this.m_panelMargeListeDeListe.BackgroundImage = global::sc2i.win32.data.navigation.Properties.Resources.fond_boutons_filtre;
            this.m_panelMargeListeDeListe.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.m_panelMargeListeDeListe.Controls.Add(this.m_btnCollapseExpandListeDeListe);
            this.m_panelMargeListeDeListe.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_panelMargeListeDeListe.Location = new System.Drawing.Point(148, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelMargeListeDeListe, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelMargeListeDeListe.Name = "m_panelMargeListeDeListe";
            this.m_panelMargeListeDeListe.Size = new System.Drawing.Size(10, 283);
            this.m_panelMargeListeDeListe.TabIndex = 6;
            // 
            // m_btnCollapseExpandListeDeListe
            // 
            this.m_btnCollapseExpandListeDeListe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnCollapseExpandListeDeListe.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_btnCollapseExpandListeDeListe.Image = global::sc2i.win32.data.navigation.Properties.Resources.Onglet_vertical;
            this.m_btnCollapseExpandListeDeListe.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnCollapseExpandListeDeListe, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnCollapseExpandListeDeListe.Name = "m_btnCollapseExpandListeDeListe";
            this.m_btnCollapseExpandListeDeListe.Size = new System.Drawing.Size(10, 31);
            this.m_btnCollapseExpandListeDeListe.TabIndex = 0;
            this.m_btnCollapseExpandListeDeListe.TabStop = false;
            this.m_btnCollapseExpandListeDeListe.Click += new System.EventHandler(this.m_btnCollapseExpandListeDeListe_Click);
            // 
            // m_wndStatusBar
            // 
            this.m_wndStatusBar.Controls.Add(this.m_statusText);
            this.m_wndStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_wndStatusBar.Location = new System.Drawing.Point(0, 391);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_wndStatusBar, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_wndStatusBar.Name = "m_wndStatusBar";
            this.m_wndStatusBar.Size = new System.Drawing.Size(781, 16);
            this.m_wndStatusBar.TabIndex = 7;
            // 
            // m_statusText
            // 
            this.m_statusText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_statusText.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_statusText.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_statusText, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_statusText.Name = "m_statusText";
            this.m_statusText.Size = new System.Drawing.Size(781, 16);
            this.m_statusText.TabIndex = 0;
            // 
            // m_splitter
            // 
            this.m_splitter.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_splitter.Location = new System.Drawing.Point(0, 105);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_splitter, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_splitter.Name = "m_splitter";
            this.m_splitter.Size = new System.Drawing.Size(781, 3);
            this.m_splitter.TabIndex = 5;
            this.m_splitter.TabStop = false;
            // 
            // m_panelFiltreEtOutils
            // 
            this.m_panelFiltreEtOutils.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_panelFiltreEtOutils.Controls.Add(this.m_panelFiltre);
            this.m_panelFiltreEtOutils.Controls.Add(this.m_panelOutilsFiltre);
            this.m_panelFiltreEtOutils.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelFiltreEtOutils.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelFiltreEtOutils, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelFiltreEtOutils.Name = "m_panelFiltreEtOutils";
            this.m_panelFiltreEtOutils.Size = new System.Drawing.Size(781, 105);
            this.m_panelFiltreEtOutils.TabIndex = 9;
            this.m_panelFiltreEtOutils.Visible = false;
            // 
            // m_panelFiltre
            // 
            this.m_panelFiltre.AutoScroll = true;
            this.m_panelFiltre.Controls.Add(this.m_panelFiltreStd);
            this.m_panelFiltre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelFiltre.Location = new System.Drawing.Point(32, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelFiltre, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelFiltre.Name = "m_panelFiltre";
            this.m_panelFiltre.Size = new System.Drawing.Size(745, 101);
            this.m_panelFiltre.TabIndex = 4;
            // 
            // m_panelFiltreStd
            // 
            this.m_panelFiltreStd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelFiltreStd.Filtre = null;
            this.m_panelFiltreStd.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelFiltreStd, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelFiltreStd.Name = "m_panelFiltreStd";
            this.m_panelFiltreStd.Size = new System.Drawing.Size(745, 101);
            this.m_panelFiltreStd.TabIndex = 1;
            this.m_panelFiltreStd.OnChangeDesiredHeight += new sc2i.win32.data.navigation.CPanelFiltreFormListStd.OnChangeDesiredHeightDelegate(this.m_panelFiltreStd_OnChangeDesiredHeight);
            this.m_panelFiltreStd.OnAppliqueFiltre += new System.EventHandler(this.OnChangeFiltre);
            // 
            // m_panelOutilsFiltre
            // 
            this.m_panelOutilsFiltre.BackColor = System.Drawing.Color.Transparent;
            this.m_panelOutilsFiltre.BackgroundImage = global::sc2i.win32.data.navigation.Properties.Resources.fond_boutons_filtre;
            this.m_panelOutilsFiltre.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.m_panelOutilsFiltre.Controls.Add(this.m_btnPreference);
            this.m_panelOutilsFiltre.Controls.Add(this.m_btnAppliquer);
            this.m_panelOutilsFiltre.Controls.Add(this.m_btnListeFiltres);
            this.m_panelOutilsFiltre.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_panelOutilsFiltre.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelOutilsFiltre, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelOutilsFiltre.Name = "m_panelOutilsFiltre";
            this.m_panelOutilsFiltre.Size = new System.Drawing.Size(32, 101);
            this.m_panelOutilsFiltre.TabIndex = 5;
            // 
            // m_btnPreference
            // 
            this.m_btnPreference.BackColor = System.Drawing.Color.Transparent;
            this.m_btnPreference.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_btnPreference.FlatAppearance.BorderSize = 0;
            this.m_btnPreference.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnPreference.Image = global::sc2i.win32.data.navigation.Properties.Resources.Preference;
            this.m_btnPreference.Location = new System.Drawing.Point(0, 64);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnPreference, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnPreference.Name = "m_btnPreference";
            this.m_btnPreference.Size = new System.Drawing.Size(32, 32);
            this.m_btnPreference.TabIndex = 8;
            this.m_tooltip.SetToolTip(this.m_btnPreference, "Save as favorit Filter|127");
            this.m_btnPreference.UseVisualStyleBackColor = false;
            this.m_btnPreference.Click += new System.EventHandler(this.m_btnPreference_Click);
            this.m_btnPreference.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_btnPreference_MouseUp);
            // 
            // m_btnAppliquer
            // 
            this.m_btnAppliquer.BackColor = System.Drawing.Color.Transparent;
            this.m_btnAppliquer.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_btnAppliquer.FlatAppearance.BorderSize = 0;
            this.m_btnAppliquer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnAppliquer.Image = global::sc2i.win32.data.navigation.Properties.Resources.Appliquer_2;
            this.m_btnAppliquer.Location = new System.Drawing.Point(0, 32);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnAppliquer, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnAppliquer.Name = "m_btnAppliquer";
            this.m_btnAppliquer.Size = new System.Drawing.Size(32, 32);
            this.m_btnAppliquer.TabIndex = 7;
            this.m_tooltip.SetToolTip(this.m_btnAppliquer, "Apply Filter|129");
            this.m_btnAppliquer.UseVisualStyleBackColor = false;
            this.m_btnAppliquer.Click += new System.EventHandler(this.m_btnAppliquer_Click);
            this.m_btnAppliquer.Enter += new System.EventHandler(this.m_btnAppliquer_Enter);
            this.m_btnAppliquer.Leave += new System.EventHandler(this.m_btnAppliquer_Leave);
            // 
            // m_btnListeFiltres
            // 
            this.m_btnListeFiltres.BackColor = System.Drawing.Color.Transparent;
            this.m_btnListeFiltres.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_btnListeFiltres.FlatAppearance.BorderSize = 0;
            this.m_btnListeFiltres.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnListeFiltres.Image = global::sc2i.win32.data.navigation.Properties.Resources.filtre_fleche_bas2;
            this.m_btnListeFiltres.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnListeFiltres, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnListeFiltres.Name = "m_btnListeFiltres";
            this.m_btnListeFiltres.Size = new System.Drawing.Size(32, 32);
            this.m_btnListeFiltres.TabIndex = 6;
            this.m_tooltip.SetToolTip(this.m_btnListeFiltres, "Select Filter|128");
            this.m_btnListeFiltres.UseVisualStyleBackColor = false;
            this.m_btnListeFiltres.Click += new System.EventHandler(this.m_btnListeFiltres_Click);
            // 
            // m_menuFiltres
            // 
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_menuFiltres, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_menuFiltres.Name = "m_menuFiltres";
            this.m_menuFiltres.Size = new System.Drawing.Size(61, 4);
            // 
            // m_timerRemplir
            // 
            this.m_timerRemplir.Interval = 300;
            this.m_timerRemplir.Tick += new System.EventHandler(this.m_timerRemplir_Tick);
            // 
            // m_panelDataInvalide
            // 
            this.m_panelDataInvalide.Controls.Add(this.label1);
            this.m_panelDataInvalide.Controls.Add(this.pictureBox4);
            this.m_panelDataInvalide.Controls.Add(this.pictureBox3);
            this.m_panelDataInvalide.Controls.Add(this.pictureBox2);
            this.m_panelDataInvalide.Controls.Add(this.pictureBox1);
            this.m_panelDataInvalide.Location = new System.Drawing.Point(164, 155);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelDataInvalide, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelDataInvalide.Name = "m_panelDataInvalide";
            this.m_panelDataInvalide.Size = new System.Drawing.Size(424, 128);
            this.m_panelDataInvalide.TabIndex = 4003;
            this.m_panelDataInvalide.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(56, 32);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(320, 72);
            this.label1.TabIndex = 4002;
            this.label1.Text = "The data of this page are not available|101";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Visible = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(376, 24);
            this.m_gestionnaireModeEdition.SetModeEdition(this.pictureBox4, sc2i.win32.common.TypeModeEdition.Autonome);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(16, 16);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox4.TabIndex = 4006;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(376, 96);
            this.m_gestionnaireModeEdition.SetModeEdition(this.pictureBox3, sc2i.win32.common.TypeModeEdition.Autonome);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(16, 16);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 4005;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(40, 96);
            this.m_gestionnaireModeEdition.SetModeEdition(this.pictureBox2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 4004;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(36, 16);
            this.m_gestionnaireModeEdition.SetModeEdition(this.pictureBox1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 4003;
            this.pictureBox1.TabStop = false;
            // 
            // m_panelLoading
            // 
            this.m_panelLoading.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_panelLoading.BackColor = System.Drawing.Color.White;
            this.m_panelLoading.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_panelLoading.Location = new System.Drawing.Point(234, 201);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelLoading, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelLoading.Name = "m_panelLoading";
            this.m_panelLoading.Size = new System.Drawing.Size(320, 72);
            this.m_panelLoading.TabIndex = 4003;
            this.m_panelLoading.Text = "Loading...|130";
            this.m_panelLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_menuExport
            // 
            this.m_menuExport.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_menuCopier,
            this.toolStripMenuItem1,
            this.m_menuItemExportSimple,
            this.m_menuItemExportAvance});
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_menuExport, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_menuExport.Name = "m_menuExport";
            this.m_menuExport.ShowImageMargin = false;
            this.m_menuExport.Size = new System.Drawing.Size(145, 76);
            // 
            // m_menuCopier
            // 
            this.m_menuCopier.Name = "m_menuCopier";
            this.m_menuCopier.Size = new System.Drawing.Size(144, 22);
            this.m_menuCopier.Text = "Copy|20003";
            this.m_menuCopier.Click += new System.EventHandler(this.m_menuCopier_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(141, 6);
            // 
            // m_menuItemExportSimple
            // 
            this.m_menuItemExportSimple.Name = "m_menuItemExportSimple";
            this.m_menuItemExportSimple.Size = new System.Drawing.Size(144, 22);
            this.m_menuItemExportSimple.Text = "Simple...|10001";
            this.m_menuItemExportSimple.Click += new System.EventHandler(this.m_menuItemExportSimple_Click);
            // 
            // m_menuItemExportAvance
            // 
            this.m_menuItemExportAvance.Name = "m_menuItemExportAvance";
            this.m_menuItemExportAvance.Size = new System.Drawing.Size(144, 22);
            this.m_menuItemExportAvance.Text = "Advanced...|10002";
            this.m_menuItemExportAvance.Click += new System.EventHandler(this.m_menuItemExportAvance_Click);
            // 
            // m_imgExport
            // 
            this.m_imgExport.BackColor = System.Drawing.Color.Transparent;
            this.m_imgExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_imgExport.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_imgExport.Image = global::sc2i.win32.data.navigation.Properties.Resources.Export_List;
            this.m_imgExport.Location = new System.Drawing.Point(695, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_imgExport, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_imgExport.Name = "m_imgExport";
            this.m_imgExport.Size = new System.Drawing.Size(24, 26);
            this.m_imgExport.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.m_imgExport.TabIndex = 0;
            this.m_imgExport.TabStop = false;
            this.m_tooltip.SetToolTip(this.m_imgExport, "Export list|20008");
            this.m_imgExport.Click += new System.EventHandler(this.m_imgExport_Click);
            // 
            // m_btnExtractList
            // 
            this.m_btnExtractList.BackColor = System.Drawing.Color.Transparent;
            this.m_btnExtractList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnExtractList.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnExtractList.Image = global::sc2i.win32.data.navigation.Properties.Resources.Extract_List;
            this.m_btnExtractList.Location = new System.Drawing.Point(755, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnExtractList, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnExtractList.Name = "m_btnExtractList";
            this.m_btnExtractList.Size = new System.Drawing.Size(26, 26);
            this.m_btnExtractList.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.m_btnExtractList.TabIndex = 15;
            this.m_btnExtractList.TabStop = false;
            this.m_tooltip.SetToolTip(this.m_btnExtractList, "Extract list|20009");
            this.m_btnExtractList.Click += new System.EventHandler(this.m_btnExtractList_Click);
            // 
            // m_btnSaveList
            // 
            this.m_btnSaveList.BackColor = System.Drawing.Color.Transparent;
            this.m_btnSaveList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnSaveList.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnSaveList.Image = global::sc2i.win32.data.navigation.Properties.Resources.Save_list;
            this.m_btnSaveList.Location = new System.Drawing.Point(724, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnSaveList, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_btnSaveList.Name = "m_btnSaveList";
            this.m_btnSaveList.Size = new System.Drawing.Size(26, 26);
            this.m_btnSaveList.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.m_btnSaveList.TabIndex = 16;
            this.m_btnSaveList.TabStop = false;
            this.m_tooltip.SetToolTip(this.m_btnSaveList, "Save checked items to list|20010");
            this.m_btnSaveList.Click += new System.EventHandler(this.m_btnSaveList_Click);
            this.m_btnSaveList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_btnSaveList_MouseUp);
            // 
            // m_lblPageInterdite
            // 
            this.m_lblPageInterdite.BackColor = System.Drawing.Color.White;
            this.m_lblPageInterdite.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblPageInterdite.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblPageInterdite.Location = new System.Drawing.Point(0, 217);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lblPageInterdite, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblPageInterdite.Name = "m_lblPageInterdite";
            this.m_lblPageInterdite.Size = new System.Drawing.Size(250, 43);
            this.m_lblPageInterdite.TabIndex = 11;
            this.m_lblPageInterdite.Text = "You do not have the rights to display this page|103";
            this.m_lblPageInterdite.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m_lblPageInterdite.Visible = false;
            // 
            // m_panelBarreOutils
            // 
            this.m_panelBarreOutils.BackColor = System.Drawing.Color.Transparent;
            this.m_panelBarreOutils.BackgroundImage = global::sc2i.win32.data.navigation.Properties.Resources.fond_menu_liste;
            this.m_panelBarreOutils.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.m_panelBarreOutils.Controls.Add(this.m_panelChoixListeOuHierarchie);
            this.m_panelBarreOutils.Controls.Add(this.m_imgExport);
            this.m_panelBarreOutils.Controls.Add(this.label3);
            this.m_panelBarreOutils.Controls.Add(this.m_btnSaveList);
            this.m_panelBarreOutils.Controls.Add(this.label4);
            this.m_panelBarreOutils.Controls.Add(this.m_lnkSupprimer);
            this.m_panelBarreOutils.Controls.Add(this.m_lnkModifier);
            this.m_panelBarreOutils.Controls.Add(this.m_lnkAjouter);
            this.m_panelBarreOutils.Controls.Add(this.m_lnkFiltrer);
            this.m_panelBarreOutils.Controls.Add(this.m_btnExtractList);
            this.m_panelBarreOutils.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelBarreOutils.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelBarreOutils, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelBarreOutils.Name = "m_panelBarreOutils";
            this.m_panelBarreOutils.Size = new System.Drawing.Size(781, 26);
            this.m_panelBarreOutils.TabIndex = 4004;
            // 
            // m_panelChoixListeOuHierarchie
            // 
            this.m_panelChoixListeOuHierarchie.BackColor = System.Drawing.Color.Transparent;
            this.m_panelChoixListeOuHierarchie.Controls.Add(this.m_chkVueListe);
            this.m_panelChoixListeOuHierarchie.Controls.Add(this.m_chkVueArbre);
            this.m_panelChoixListeOuHierarchie.Controls.Add(this.splitter1);
            this.m_panelChoixListeOuHierarchie.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_panelChoixListeOuHierarchie.Location = new System.Drawing.Point(628, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelChoixListeOuHierarchie, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelChoixListeOuHierarchie.Name = "m_panelChoixListeOuHierarchie";
            this.m_panelChoixListeOuHierarchie.Size = new System.Drawing.Size(67, 26);
            this.m_panelChoixListeOuHierarchie.TabIndex = 14;
            // 
            // m_chkVueListe
            // 
            this.m_chkVueListe.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_chkVueListe.BackColor = System.Drawing.Color.Transparent;
            this.m_chkVueListe.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_chkVueListe.FlatAppearance.BorderSize = 0;
            this.m_chkVueListe.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.m_chkVueListe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_chkVueListe.Image = global::sc2i.win32.data.navigation.Properties.Resources.view_list;
            this.m_chkVueListe.Location = new System.Drawing.Point(5, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_chkVueListe, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_chkVueListe.Name = "m_chkVueListe";
            this.m_chkVueListe.Size = new System.Drawing.Size(30, 26);
            this.m_chkVueListe.TabIndex = 12;
            this.m_chkVueListe.UseVisualStyleBackColor = false;
            this.m_chkVueListe.CheckedChanged += new System.EventHandler(this.m_chkVueListe_CheckedChanged);
            // 
            // m_chkVueArbre
            // 
            this.m_chkVueArbre.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_chkVueArbre.BackColor = System.Drawing.Color.Transparent;
            this.m_chkVueArbre.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_chkVueArbre.FlatAppearance.BorderSize = 0;
            this.m_chkVueArbre.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.m_chkVueArbre.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_chkVueArbre.Image = global::sc2i.win32.data.navigation.Properties.Resources.View_tree;
            this.m_chkVueArbre.Location = new System.Drawing.Point(35, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_chkVueArbre, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_chkVueArbre.Name = "m_chkVueArbre";
            this.m_chkVueArbre.Size = new System.Drawing.Size(30, 26);
            this.m_chkVueArbre.TabIndex = 13;
            this.m_chkVueArbre.UseVisualStyleBackColor = false;
            this.m_chkVueArbre.CheckedChanged += new System.EventHandler(this.m_chkVueArbre_CheckedChanged);
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(65, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.splitter1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(2, 26);
            this.splitter1.TabIndex = 16;
            this.splitter1.TabStop = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Location = new System.Drawing.Point(719, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label3, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(5, 26);
            this.label3.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(750, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label4, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(5, 26);
            this.label4.TabIndex = 18;
            // 
            // m_lnkSupprimer
            // 
            this.m_lnkSupprimer.BackColor = System.Drawing.Color.Transparent;
            this.m_lnkSupprimer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkSupprimer.CustomImage = ((System.Drawing.Image)(resources.GetObject("m_lnkSupprimer.CustomImage")));
            this.m_lnkSupprimer.CustomText = "Remove";
            this.m_lnkSupprimer.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkSupprimer.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkSupprimer.Location = new System.Drawing.Point(240, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkSupprimer, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_lnkSupprimer.Name = "m_lnkSupprimer";
            this.m_lnkSupprimer.ShortMode = false;
            this.m_lnkSupprimer.Size = new System.Drawing.Size(93, 26);
            this.m_lnkSupprimer.TabIndex = 6;
            this.m_lnkSupprimer.TypeLien = sc2i.win32.common.TypeLinkStd.Suppression;
            this.m_lnkSupprimer.LinkClicked += new System.EventHandler(this.m_lnkSupprimer_LinkClicked);
            // 
            // m_lnkModifier
            // 
            this.m_lnkModifier.BackColor = System.Drawing.Color.Transparent;
            this.m_lnkModifier.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkModifier.CustomImage = ((System.Drawing.Image)(resources.GetObject("m_lnkModifier.CustomImage")));
            this.m_lnkModifier.CustomText = "Detail";
            this.m_lnkModifier.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkModifier.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkModifier.Location = new System.Drawing.Point(161, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkModifier, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_lnkModifier.Name = "m_lnkModifier";
            this.m_lnkModifier.ShortMode = false;
            this.m_lnkModifier.Size = new System.Drawing.Size(79, 26);
            this.m_lnkModifier.TabIndex = 5;
            this.m_lnkModifier.TypeLien = sc2i.win32.common.TypeLinkStd.Modification;
            this.m_lnkModifier.LinkClicked += new System.EventHandler(this.m_lnkModifier_LinkClicked);
            // 
            // m_lnkAjouter
            // 
            this.m_lnkAjouter.BackColor = System.Drawing.Color.Transparent;
            this.m_lnkAjouter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkAjouter.CustomImage = ((System.Drawing.Image)(resources.GetObject("m_lnkAjouter.CustomImage")));
            this.m_lnkAjouter.CustomText = "Add";
            this.m_lnkAjouter.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkAjouter.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkAjouter.Location = new System.Drawing.Point(78, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkAjouter, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_lnkAjouter.Name = "m_lnkAjouter";
            this.m_lnkAjouter.ShortMode = false;
            this.m_lnkAjouter.Size = new System.Drawing.Size(83, 26);
            this.m_lnkAjouter.TabIndex = 4;
            this.m_lnkAjouter.TypeLien = sc2i.win32.common.TypeLinkStd.Ajout;
            this.m_lnkAjouter.LinkClicked += new System.EventHandler(this.m_lnkAjouter_LinkClicked);
            // 
            // m_lnkFiltrer
            // 
            this.m_lnkFiltrer.BackColor = System.Drawing.Color.Transparent;
            this.m_lnkFiltrer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_lnkFiltrer.CustomImage = ((System.Drawing.Image)(resources.GetObject("m_lnkFiltrer.CustomImage")));
            this.m_lnkFiltrer.CustomText = "Filter";
            this.m_lnkFiltrer.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lnkFiltrer.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.m_lnkFiltrer.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lnkFiltrer, sc2i.win32.common.TypeModeEdition.EnableSurEdition);
            this.m_lnkFiltrer.Name = "m_lnkFiltrer";
            this.m_lnkFiltrer.ShortMode = false;
            this.m_lnkFiltrer.Size = new System.Drawing.Size(78, 26);
            this.m_lnkFiltrer.TabIndex = 10;
            this.m_lnkFiltrer.TypeLien = sc2i.win32.common.TypeLinkStd.Filtre;
            this.m_lnkFiltrer.LinkClicked += new System.EventHandler(this.m_lnkFiltrer_LinkClicked);
            // 
            // CPanelListeSpeedStandard
            // 
            this.Controls.Add(this.m_lblPageInterdite);
            this.Controls.Add(this.m_panelLoading);
            this.Controls.Add(this.m_panelDataInvalide);
            this.Controls.Add(this.m_panelListeEtFiltre);
            this.Controls.Add(this.m_panelBarreOutils);
            this.m_gestionnaireModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CPanelListeSpeedStandard";
            this.Size = new System.Drawing.Size(781, 433);
            this.Load += new System.EventHandler(this.CPanelListeSpeedStandard_Load);
            this.Enter += new System.EventHandler(this.m_btnAppliquer_Enter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CPanelListeSpeedStandard_KeyDown);
            this.Leave += new System.EventHandler(this.m_btnAppliquer_Leave);
            this.m_panelListeEtFiltre.ResumeLayout(false);
            this.m_panelListeOuArbre.ResumeLayout(false);
            this.m_panelListesEntites.ResumeLayout(false);
            this.m_panelListesEntites.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_btnNoListe)).EndInit();
            this.m_panelMargeListeDeListe.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_btnCollapseExpandListeDeListe)).EndInit();
            this.m_wndStatusBar.ResumeLayout(false);
            this.m_panelFiltreEtOutils.ResumeLayout(false);
            this.m_panelFiltre.ResumeLayout(false);
            this.m_panelOutilsFiltre.ResumeLayout(false);
            this.m_panelDataInvalide.ResumeLayout(false);
            this.m_panelDataInvalide.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.m_menuExport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_imgExport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnExtractList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnSaveList)).EndInit();
            this.m_panelBarreOutils.ResumeLayout(false);
            this.m_panelChoixListeOuHierarchie.ResumeLayout(false);
            this.ResumeLayout(false);

        }



        #endregion


        /// //////////////////////////////////////////////////////////////
        [Browsable(false)]
        public CFormNavigateur Navigateur
        {
            get
            {
                if (m_gestionnaireModifs != null)
                    return m_gestionnaireModifs.Navigateur;
                return null;
            }
            set
            {
                if (m_gestionnaireModifs != null)
                    m_gestionnaireModifs.Navigateur = value;
            }
        }


        [Browsable(false)]
        public IEnumerable<CAffectationsProprietes> AffectationsPourNouveauxElements
        {
            get
            {
                return m_listeAffectationsInitiales.AsReadOnly();
            }
            set
            {
                m_listeAffectationsInitiales.Clear();
                if (value != null)
                    m_listeAffectationsInitiales.AddRange(value);
            }
        }

        [Browsable(false)]
        public object ObjetReferencePourAffectationsInitiales
        {
            get
            {
                return m_objetReferencePourAffectationsInitiales;
            }
            set
            {
                m_objetReferencePourAffectationsInitiales = value;
            }
        }

        public bool AllowCustomisation
        {
            get
            {
                return m_listView.EnableCustomisation;
            }
            set
            {
                m_listView.EnableCustomisation = value;
            }
        }

        public bool AllowSerializePreferences
        {
            get
            {
                return m_bAlowSerializePreferences;
            }
            set
            {
                m_bAlowSerializePreferences = value;
            }
        }

        /// //////////////////////////////////////////////////////////////
        /// <summary>
        /// Lorsqu'on double click sur un objet ou sur le bouton détail, 
        /// l'objet est édité. Si cette propriété est différente de "",
        /// ce n'est pas l'objet qui est édité, mais l'objet retourné par l'appel de 
        /// ProprieteObjetAEditer sur l'objet.
        /// </summary>
        [Browsable(false)]
        public string ProprieteObjetAEditer
        {
            get
            {
                if (m_gestionnaireModifs != null)
                    return m_gestionnaireModifs.ProprieteObjetAEditer;
                return null;
            }
            set
            {
                if (m_gestionnaireModifs != null)
                    m_gestionnaireModifs.ProprieteObjetAEditer = value;
            }
        }

        //-------------------------------------------------------------------
        public bool UseCheckBoxes
        {
            get
            {
                return m_bCheckBoxes;
            }
            set
            {
                m_bCheckBoxes = value;
                if (m_listView != null)
                    m_listView.CheckBoxes = value;
            }
        }

        //-------------------------------------------------------------------
        public event ObjetDonneeEventHandler AfterValideCreateObjetDonnee;
        private void AfterValideCreateObjetDonneeFunc(object sender, CObjetDonneeEventArgs data)
        {
            if (AfterValideCreateObjetDonnee != null)
                AfterValideCreateObjetDonnee(sender, data);
        }

        //-------------------------------------------------------------------
        public bool AllowArbre
        {
            get
            {
                return m_bAllowArbre;
            }
            set
            {
                m_bAllowArbre = value;
            }
        }


        //-------------------------------------------------------------------
        public void InitFromListeObjets(
            CListeObjetsDonnees listeObjets,
            Type typeObjet,
            CObjetDonneeAIdNumeriqueAuto objetContainer,
            string strChampContainer)
        {

            InitFromListeObjets(
                listeObjets,
                typeObjet,
                null,   // Le type de Form est à null pour que le gestionnaireAjoutModifSupp utilise CFormFinder
                objetContainer,
                strChampContainer);
        }

        //-------------------------------------------------------------------
        public void InitFromListeObjets(
            CListeObjetsDonnees listeObjets,
            Type typeObjet,
            CReferenceTypeForm refTypeForm,
            CObjetDonneeAIdNumeriqueAuto objetContainer,
            string strChampContainer)
        {
            if (DesignMode)
                return;
            this.SuspendDrawing();
            try
            {
                m_panelChoixListeOuHierarchie.Visible = typeof(IObjetHierarchiqueACodeHierarchique).IsAssignableFrom(typeObjet) && m_bAllowArbre;
                listeObjets.AppliquerFiltreAffichage = true;
                m_gestionnaireModifs = new CGestionnaireAjoutModifSuppObjetDonnee(
                    typeObjet,
                    refTypeForm,
                    objetContainer,
                    strChampContainer);
                m_gestionnaireModifs.OnNewObjetDonnee += new OnNewObjetDonneeEventHandler(m_gestionnaire_OnNewObjetDonnee);
                m_gestionnaireModifs.AfterCreateFormEdition += new AfterCreateFormEditionEventHandler(OnAfterCreateFormEditionParGestionnaire);
                m_gestionnaireModifs.AfterValideCreateObjetDonnee += new ObjetDonneeEventHandler(AfterValideCreateObjetDonneeFunc);
                ListeObjets = listeObjets;

                if (m_listeRestrictions != null && m_gestionnaireReadOnly != null)
                    AppliqueRestrictions(m_listeRestrictions, m_gestionnaireReadOnly);

                //Si InterditLectureInDb est true, il ne faut pas utiliser le remplissage progressif
                m_listeObjets.RemplissageProgressif = !m_listeObjets.InterditLectureInDB;
                m_filtre = m_listeObjets.Filtre;
                InitMenuFiltres();
                MetEnPlaceFiltreInitial();
                if (m_bModeQuickSearch && m_filtreRechercheRapide != null)
                {
                    ControlFiltreStandard = new CPanelFiltreRapide(m_filtreRechercheRapide, m_strQuickSearchText);
                    m_filtre = ControlFiltreStandard.Filtre;
                }
                if (m_controlFiltreStandard == null)
                    ControlFiltreEnCours = m_panelFiltreStd;
                if (m_controlFiltreEnCours != null)
                    m_controlFiltreEnCours.Filtre = m_filtre;
                m_lnkFiltrer.Visible = m_menuFiltres.Items.Count > 0 && BoutonFiltrerVisible;
                m_btnListeFiltres.Visible = m_menuFiltres.Items.Count > 0;

                if (m_controlFiltreEnCours != null)
                {
                    m_filtre = m_controlFiltreEnCours.Filtre;
                    m_bFiltreDeBaseAppliqueAListe = false;
                }

                ReadPreferenceFromRegistre();

                ReadColonnes();

                RemplirGrille();

                m_bListeDeListeIsInit = false;
                if (CSc2iWin32DataNavigationRegistre.GetShowListeDeListeSpeed(
                    m_listeObjets.TypeObjets, m_strContexteUtilisation))
                    ExpandListeDeListes();
                else
                    CollapseListeDeListes();


                //quick search et 1 objet, on double click dessus tout de suite
                /*if ( m_bModeQuickSearch && m_listeObjets.Count == 1 )
                {
                    m_listView.SelectItem ( 0 );
                    m_listView_DoubleClick ( this, new EventArgs() );
                }*/
                //Trouve le premier filtre dynamique
                if (ControlFiltreEnCours == m_panelFiltreStd && m_panelFiltreStd.GetFiltreDynamique() == null)
                {
                    foreach (ToolStripItem item in m_menuFiltres.Items)
                    {
                        if (item is CMenuItemAFiltre)
                        {
                            m_panelFiltreStd.SetFiltreDynamique(((CMenuItemAFiltre)item).Filtre.Filtre);
                        }
                    }
                }
                /*
                CMenuItemAFiltre menuFiltre = m_menuFiltres.Items.FirstOrDefault(
                if (ControlFiltreEnCours == m_panelFiltreStd && m_menuFiltres.Items.Count > 3 && m_panelFiltreStd.GetFiltreDynamique() == null)
				{
					m_panelFiltreStd.SetFiltreDynamique(((CMenuItemAFiltre)m_menuFiltres.Items[3]).Filtre.Filtre);
				}*/

                /*if (m_bModeQuickSearch)
                {
                    m_btnListeFiltres.Visible = false;
                    m_lnkFiltrer.Visible = false;
                }*/

                //Applique les restricitions
                try
                {
                    CRestrictionUtilisateurSurType restriction = CSessionClient.GetSessionForIdSession(
                        CSc2iWin32DataClient.ContexteCourant.IdSession).
                        GetInfoUtilisateur().
                        GetRestrictionsSur(listeObjets.TypeObjets, listeObjets.ContexteDonnee.IdVersionDeTravail);
                    if (!restriction.CanShowType())
                    {
                        m_lblPageInterdite.Dock = DockStyle.Fill;
                        m_lblPageInterdite.Visible = true;
                        m_lblPageInterdite.BringToFront();
                    }
                    if (!restriction.CanCreateType())
                        BoutonAjouterVisible = false;
                    if (!restriction.CanDeleteType())
                        BoutonSupprimerVisible = false;
                    if (!restriction.CanShowType())
                        BoutonModifierVisible = false;
                    //Applique les restrictions sur les colonnes
                    int nIndex = 0;
                    if (restriction.HasRestrictions)
                    {
                        foreach (GLColumn col in new ArrayList(m_listView.Columns))
                        {
                            string strSuite = "";
                            CDefinitionProprieteDynamique defProp = CConvertisseurInfoStructureDynamiqueToDefinitionChamp.GetDefinitionProprieteDynamique(col.Propriete, ref strSuite);
                            string strProp = "";
                            if (defProp is CDefinitionProprieteDynamiqueChampCustom)
                            {
                                CChampCustom champ = new CChampCustom(CSc2iWin32DataClient.ContexteCourant);
                                if (champ.ReadIfExists(((CDefinitionProprieteDynamiqueChampCustom)defProp).DbKeyChamp))
                                    strProp = champ.CleRestriction;
                            }
                            else if (defProp != null)
                                strProp = defProp.NomProprieteSansCleTypeChamp;
                            if (strProp.Length > 0 && ((restriction.GetRestriction(strProp) & ERestriction.Hide) == ERestriction.Hide))
                            {
                                m_listView.Columns.Remove(nIndex);
                                nIndex--;
                            }
                            nIndex++;
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                }
                m_panelDataInvalide.Visible = false;
            }
            catch
            {
                m_arbre.Visible = false;
                m_listView.Visible = false;
                m_panelDataInvalide.Visible = true;
            }
            finally
            {
                this.ResumeDrawing();
            }
        }

        /// ////////////////////////////////////////////////////////////////////
        private void InitModeArbre()
        {
            CFiltreData filtreComplet = m_listeObjets.GetFiltreForRead();

            IObjetHierarchiqueACodeHierarchique objet = (IObjetHierarchiqueACodeHierarchique)Activator.CreateInstance(
                m_listeObjets.TypeObjets, new object[] { m_listeObjets.ContexteDonnee });

            if (m_gestionnaireModifs.ObjetContainer != null)
            {
                if (m_gestionnaireModifs.ObjetContainer.GetType() == m_listeObjets.TypeObjets)
                {
                    m_arbre.Init(m_gestionnaireModifs.ObjetContainer,
                        objet.ProprieteListeFils,
                        objet.ChampParent,
                        "Libelle",
                        filtreComplet);
                }
                else
                {
                    //Filtre la racine par l'objet conteneur
                    //Trouve le champ correspondant au lien parent
                    string strChampParent = "";
                    CStructureTable structure = CStructureTable.GetStructure(m_listeObjets.TypeObjets);
                    foreach (CInfoRelation rel in structure.RelationsParentes)
                    {
                        if (rel.Propriete == m_gestionnaireModifs.ChampLienParent)
                            strChampParent = rel.ChampsParent[0];
                    }
                    if (strChampParent == "")
                        return;

                    CFiltreData filtre = new CFiltreData(strChampParent + "=@1",
                        m_gestionnaireModifs.ObjetContainer.Id);
                    m_arbre.Init(objet.GetType(),
                    objet.ProprieteListeFils,
                    objet.ChampParent,
                    "Libelle",
                    filtreComplet,
                    filtre);
                }
            }
            else
                m_arbre.Init(objet.GetType(),
                    objet.ProprieteListeFils,
                    objet.ChampParent,
                    "Libelle",
                    filtreComplet,
                    null);

        }


        /// ////////////////////////////////////////////////////////////////////
        private void OnAfterCreateFormEditionParGestionnaire(object sender, CFormEditionStandard form)
        {
            if (AfterCreateFormEdition != null)
                AfterCreateFormEdition(sender, form);
        }

        /// ////////////////////////////////////////////////////////////////////
        private class CMenuItemAFiltre : ToolStripMenuItem
        {
            public readonly CFiltreDynamiqueInDb Filtre;

            public CMenuItemAFiltre(CFiltreDynamiqueInDb filtre)
                : base(filtre.Libelle)
            {
                Filtre = filtre;
            }
        }

        /// ////////////////////////////////////////////////////////////////////
        private class CMenuItemForFiltreStandard : ToolStripMenuItem
        {
            public CMenuItemForFiltreStandard()
                : base(I.T("Standard Filter|10000"))
            {
            }
        }

        /// ////////////////////////////////////////////////////////////////////
        private void OnMenuTemporaire(object sender, EventArgs args)
        {

            CFiltreDynamique filtre = null;
            if (m_filtreTemporaire == null)
                filtre = new CFiltreDynamique();
            else
                filtre = (CFiltreDynamique)CCloner2iSerializable.Clone(m_filtreTemporaire);
            filtre.TypeElements = m_listeObjets.TypeObjets;
            filtre.ContexteDonnee = m_listeObjets.ContexteDonnee;
            if (CFormEditFiltreDynamique.EditeFiltre(filtre, true, true, null))
            {
                m_filtreTemporaire = filtre;
                CResultAErreur result = filtre.GetFiltreData();
                if (!result)
                {
                    CFormAlerte.Afficher(result);
                    return;
                }
                m_filtre = (CFiltreData)result.Data;
                m_bFiltreDeBaseAppliqueAListe = false;
                RemplirGrille();
            }
        }


        /// ////////////////////////////////////////////////////////////////////
        private void InitMenuFiltres()
        {
            if (m_listeFiltresPossibles != null || m_listeObjets == null)
                return;
            m_listeFiltresPossibles = new CListeObjetsDonnees(CSc2iWin32DataClient.ContexteCourant, typeof(CFiltreDynamiqueInDb));
            m_listeFiltresPossibles.Filtre = new CFiltreData(CFiltreDynamiqueInDb.c_champTypeElements + "=@1", m_listeObjets.TypeObjets.ToString());
            m_menuFiltres.Items.Clear();
            ToolStripMenuItem menu = new ToolStripMenuItem(I.T("Temporary Filter|131"));
            menu.Click += new EventHandler(OnMenuTemporaire);
            m_menuFiltres.Items.Add(menu);
            CSessionClient session = CSessionClient.GetSessionForIdSession(CSc2iWin32DataClient.ContexteCourant.IdSession);
            if (session != null)
            {
                IInfoUtilisateur user = session.GetInfoUtilisateur();
                if (user != null)
                {
                    if (user.GetDonneeDroit(CDroitDeBaseSC2I.c_droitAdministration) != null)
                    {
                        CFiltreIdToolStrip menuFiltreId = new CFiltreIdToolStrip();
                        menuFiltreId.OnAskId += new CFiltreIdToolStrip.OnAskIdEventHandler(menuFiltreId_OnAskId);
                        m_menuFiltres.Items.Add(menuFiltreId);
                    }
                }
            }

            m_menuFiltres.Items.Add(new ToolStripSeparator());
            if (m_controlFiltreStandard != null)
            {
                CMenuItemForFiltreStandard item = new CMenuItemForFiltreStandard();
                item.Click += new EventHandler(OnSelectFiltre);
                m_menuFiltres.Items.Add(item);
            }
            foreach (CFiltreDynamiqueInDb filtre in m_listeFiltresPossibles)
            {
                CMenuItemAFiltre item = new CMenuItemAFiltre(filtre);
                item.Click += new EventHandler(OnSelectFiltre);
                m_menuFiltres.Items.Add(item);
            }
        }

        /// ////////////////////////////////////////////////////////////////////
        private void menuFiltreId_OnAskId(object sender, int? nIdDemande)
        {
            m_menuFiltres.Hide();
            CFiltreData filtre = new CFiltreData();
            if (nIdDemande != null && m_listeObjets != null && typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(m_listeObjets.TypeObjets))
            {
                CStructureTable structure = CStructureTable.GetStructure(m_listeObjets.TypeObjets);
                string strChampId = structure.ChampsId[0].NomChamp;
                filtre = new CFiltreData(strChampId + "=@1",
                    nIdDemande.Value);
            }
            m_filtre = filtre;
            m_bFiltreDeBaseAppliqueAListe = false;
            RemplirGrille();
        }

        private void OnSelectFiltre(object sender, EventArgs args)
        {
            if (sender is CMenuItemForFiltreStandard)
            {
                ControlFiltreEnCours = m_controlFiltreStandard;
            }
            else if (sender is CMenuItemAFiltre)
            {
                ControlFiltreEnCours = m_panelFiltreStd;
                m_panelFiltreStd.SetFiltreDynamique(((CMenuItemAFiltre)sender).Filtre.Filtre);
            }
        }


        public CGestionnaireAjoutModifSuppObjetDonnee GestionnaireAjoutModifSuppression
        {
            get
            {
                return m_gestionnaireModifs;
            }
        }
        /*/-------------------------------------------------------------------
            public CObjetDonneeAIdNumeriqueAuto ObjetContainer
            {
                get
                {
                    return m_objetContainer;
                }
                set
                {
                    m_objetContainer = value;
                }
            }*/
        //-------------------------------------------------------------------
        public CListeObjetsDonnees ListeObjets
        {
            get
            {
                return m_listeObjets;
            }
            set
            {
                m_listeObjets = value;
                if (m_listeObjets != null && !m_listeObjets.HasAfterLoadPageEventHandler &&
                    typeof(IElementAChamps).IsAssignableFrom(m_listeObjets.TypeObjets))
                {
                    m_listeObjets.AfterLoadPage += new CListeObjetsDonnees.AfterLoadPageEventHandler(m_listeObjets_AfterLoadPage);
                }
            }
        }

        public void m_listeObjets_AfterLoadPage(List<DataRow> rowsLues)
        {
            if (typeof(IElementAChamps).IsAssignableFrom(m_listeObjets.TypeObjets) && rowsLues.Count > 0)
            {
                string strKey = rowsLues[0].Table.PrimaryKey[0].ColumnName;
                StringBuilder bl = new StringBuilder();
                foreach (DataRow row in rowsLues)
                {
                    bl.Append(row[strKey]);
                    bl.Append(",");
                }
                bl.Remove(bl.Length - 1, 1);
                CFiltreData filtre = new CFiltreData(strKey + " in (" +
                    bl.ToString() + ")");
                CListeObjetsDonnees lst = new CListeObjetsDonnees(m_listeObjets.ContexteDonnee, m_listeObjets.TypeObjets);
                lst.Filtre = filtre;
                lst.InterditLectureInDB = true;
                if (typeof(IObjetDonneeAChamps).IsAssignableFrom(m_listeObjets.TypeObjets))
                {
                    int[] idsChamps = GetListeIdsChampsCustomALire();
                    if (idsChamps.Length > 0)
                        CUtilElementAChamps.ReadChampsCustom(lst, idsChamps);
                }
                else
                    lst.ReadDependances("RelationsChampsCustom");
            }
        }


        bool ShouldSerializeColumns()
        { return true; }

        public void ResetColums()
        { }

        //-------------------------------------------------------------------
        [
        Editor(typeof(CollectionEditor), typeof(UITypeEditor)),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content)
        ]
        public GLColumnCollection Columns
        {
            get
            {
                return m_listView.Columns;
            }
        }

        //-------------------------------------------------------------------
        public bool ShouldSerializeMultiSelect()
        {
            return true;
        }
        public void ResetMultiSelect()
        {
            MultiSelect = false;
        }
        public bool MultiSelect
        {
            get
            {
                return m_listView.AllowMultiselect;
            }
            set
            {
                m_listView.AllowMultiselect = value;
                m_arbre.CheckBoxes = value;
            }
        }
        //-------------------------------------------------------------------
        public void AddColonne(string strTitre, string strField, int nWidth)
        {
            m_listView.Columns.Add(strTitre, strField, nWidth);
        }

        //-------------------------------------------------------------------
        public void AddColonne(string strTitre, string strField)
        {
            int nWidth = CSc2iWin32DataNavigationRegistre.GetWidthValue(this.ParentForm.GetType().Name + "_" + this.Name + "_Colonne" + m_listView.Columns.Count.ToString() + "width");
            AddColonne(strTitre, strField, nWidth);
        }
        //-------------------------------------------------------------------
        public void RemplirGrille()
        {
            m_timerRemplir.Stop();
            m_timerRemplir.Start();
        }

        //-------------------------------------------------------------------
        private CFiltreData GetFiltreAAppliquer(bool bAvecListeEnCours)
        {
            CFiltreData filtre = m_filtre;
            if (FiltreDeBase != null)
                filtre = CFiltreData.GetAndFiltre(filtre, m_filtreDeBase);
            if (m_listeObjets != null)
            {
                CFiltreData filtreAdd = CFiltreInterfaceExterne.GetFiltreAdditionnel(m_listeObjets.TypeObjets);
                if (filtreAdd != null)
                    filtre = CFiltreData.GetAndFiltre(filtre, filtreAdd);
            }
            if (bAvecListeEnCours && m_listeEntitesEnCours != null)
            {
                filtre = CFiltreData.GetAndFiltre(filtre,
                    m_listeEntitesEnCours.GetFiltrePourListe());
            }

            return filtre;
        }

        //-------------------------------------------------------------------
        public void RemplirGrilleSansTimer()
        {
            this.SuspendDrawing();
            using (CWaitCursor waiter = new CWaitCursor())
            {
                try
                {
                    m_timerRemplir.Stop();
                    m_panelLoading.Visible = true;

                    m_btnNoListe.Visible = m_listeEntitesEnCours != null;
                    foreach (CCustomizableListItem item in m_wndListeListes.Items)
                    {
                        CControlPourListeDeListePanelSpeed.CItemListe il = item as CControlPourListeDeListePanelSpeed.CItemListe;
                        if (il != null)
                            il.IsSelected = il.ListeEntites.Equals(m_listeEntitesEnCours);
                        m_wndListeListes.RefreshItem(item);
                    }
                    m_wndListeListes.Refresh();

                    if (m_listeObjets != null)
                    {
                        if (!m_bFiltreDeBaseAppliqueAListe)
                        {
                            m_listeObjets.Filtre = GetFiltreAAppliquer(true);
                            m_bFiltreDeBaseAppliqueAListe = true;
                        }
                        if (m_bModeArbre)
                            InitModeArbre();
                        else
                        {
                            m_listView.ListeSource = m_listeObjets;
                            if ( m_listeIdCheckedFromContexte != null && m_listeIdCheckedFromContexte.Count > 0 )
                            {
                                if ( typeof(CObjetDonneeAIdNumerique).IsAssignableFrom ( m_listeObjets.TypeObjets));
                                ArrayList lstChecked = new ArrayList();
                                foreach ( int nId in m_listeIdCheckedFromContexte )
                                {
                                    CObjetDonneeAIdNumerique objet = (CObjetDonneeAIdNumerique)Activator.CreateInstance ( m_listeObjets.TypeObjets, new object[]{m_listeObjets.ContexteDonnee});
                                    if ( objet.ReadIfExists ( nId ) )
                                        lstChecked.Add ( objet );
                                }
                                m_listView.CheckedItems = lstChecked;
                            }
                            m_listeIdCheckedFromContexte = null;
                        }
                        m_statusText.Text = m_listeObjets.Count.ToString() + " " + I.T("element|132");
                        if (m_listeObjets.Count > 1)
                            m_statusText.Text += "s";
                    }
                    else
                        m_statusText.Text = "";
                    m_panelDataInvalide.Visible = false;
                    if (m_bModeArbre)
                        m_arbre.Dock = DockStyle.Fill;
                    else
                        m_listView.Dock = DockStyle.Fill;
                    if (m_arbre.Dock != DockStyle.Fill)
                        m_arbre.Visible = false;
                    if (m_listView.Dock != DockStyle.Fill)
                        m_listView.Visible = false;
                    m_arbre.Visible = m_bModeArbre;
                    m_listView.Visible = !m_bModeArbre;
                }
                catch
                {
                    m_arbre.Visible = false;
                    m_listView.Visible = false;
                    m_panelDataInvalide.Visible = true;
                }
                finally
                {
                    this.ResumeDrawing();
                }
                //Accede à la liste pour forcer sa lecture, et que le sablier soit en phase
                //avec la lecture
                if (m_listeObjets.Count > 0)
                {
                    object dummy = m_listeObjets[0];
                }
            }
            if (m_listView.Visible)
                m_listView.Refresh();
            m_panelLoading.Visible = false;

            this.ResumeDrawing();
        }
        //-------------------------------------------------------------------
        protected virtual void SupprimeTout(object sender, CObjetDonneeEventArgs args)
        {
            CSc2iWin32DataNavigation.Navigateur.AffichePagePrecedenteSansHistorisation();
            CSc2iWin32DataNavigation.Navigateur.EntreeEnCours.EntreeSuivante = null;
        }
        //---------------------------------------------------------------------------
        protected virtual void AnnuleTout(object sender, CObjetDonneeEventArgs args)
        {
            CSc2iWin32DataNavigation.Navigateur.AffichePagePrecedente();
            CSc2iWin32DataNavigation.Navigateur.EntreeEnCours.EntreeSuivante = null;
        }
        //------------------------------------------------------------------------
        protected void AfterModification(object sender, CObjetDonneeEventArgs args)
        {
            CSc2iWin32DataNavigation.Navigateur.AffichePagePrecedenteAvecDuplication();
        }
        //---------------------------------------------------------------------------
        /*private CFormEditionStandard NewCFormEdition(CObjetDonneeAIdNumeriqueAuto objet, CListeObjetsDonnees liste)
        {
            CFormEditionStandard frm = (CFormEditionStandard) Activator.CreateInstance( m_typeForm, new object[] {objet, liste} );

            frm.AfterSuppression += new ObjetDonneeEventHandler ( SupprimeTout );
            frm.AfterValideModification += new ObjetDonneeEventHandler ( AfterModification );
            frm.AfterAnnulationModification += new ObjetDonneeEventHandler ( AnnuleTout );

            return frm;
        }*/

        /// <summary>
        /// Evenement lancé après la création d'un nouvel objet. Il
        /// est alors possible au handler d'évenement de modifier l'élément ajouté
        /// </summary>
        public event OnNewObjetDonneeEventHandler OnNewObjetDonnee;

        private void m_gestionnaire_OnNewObjetDonnee(object sender, CObjetDonnee nouvelObjet, ref bool bCancel)
        {
            if (OnNewObjetDonnee != null)
            {
                OnNewObjetDonnee(this, nouvelObjet, ref bCancel);
            }
            CAffectationsProprietes affectation = null;
            if (m_listeAffectationsInitiales.Count() > 1)
            {
                CMenuModal menu = new CMenuModal();
                ToolStripMenuItem item = null;
                foreach (CAffectationsProprietes aff in m_listeAffectationsInitiales)
                {
                    item = new ToolStripMenuItem(aff.Libelle);
                    item.Tag = aff;
                    menu.Items.Add(item);
                }
                Point pt = m_lnkAjouter.PointToScreen(new Point(0, m_lnkAjouter.Height));
                item = menu.ShowMenu(pt) as ToolStripMenuItem;
                affectation = item != null ? item.Tag as CAffectationsProprietes : null;
                if (affectation == null)
                {
                    bCancel = true;
                    return;
                }
            }
            if (m_listeAffectationsInitiales.Count == 1)
                affectation = m_listeAffectationsInitiales[0];
            if (affectation != null)
                affectation.AffecteProprietes(nouvelObjet, m_objetReferencePourAffectationsInitiales, new CFournisseurPropDynStd());

            ControlFiltreEnCours.AffecteValeursToNewObjet(nouvelObjet);
        }

        public delegate CResultAErreur AjouterElementDelegate(CListeObjetsDonnees lst);
        public AjouterElementDelegate AjouterElement;

        //---------------------------------------------------------------------------
        private void m_lnkAjouter_LinkClicked(object sender, System.EventArgs e)
        {
            if (m_gestionnaireModifs != null && m_bShowBoutonAjouter)
            {
                CResultAErreur result = null;
                if (AjouterElement != null)
                    result = AjouterElement(m_listeObjets);
                else
                    result = m_gestionnaireModifs.Ajouter(m_listeObjets, AffectationsPourNouveauxElements);
                if (!result)
                    CFormAlerte.Afficher(result);
            }
        }


        //---------------------------------------------------------------------------
        private void m_lnkModifier_LinkClicked(object sender, System.EventArgs e)
        {
            SelectionElement();
        }
        //---------------------------------------------------------------------------
        public event EventHandler OnObjetDoubleClick;
        //---------------------------------------------------------------------------
        private void m_listView_DoubleClick(object sender, System.EventArgs e)
        {
            if (!m_bModeSelection)
                SelectionElement();
            else
                m_objetDoubleClicked = (CObjetDonneeAIdNumeriqueAuto)m_listView.SelectedItems[0];

            if (OnObjetDoubleClick != null)
                OnObjetDoubleClick(sender, e);
        }

        //---------------------------------------------------------------------------
        private void m_arbre_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!m_bModeSelection)
                SelectionElement();
            else
                m_objetDoubleClicked = (CObjetDonneeAIdNumeriqueAuto)m_arbre.ElementSelectionne;
            if (OnObjetDoubleClick != null)
                OnObjetDoubleClick(sender, e);
        }

        //---------------------------------------------------------------------------
        public bool ModeSelection
        {
            get
            {
                return m_bModeSelection;
            }
            set
            {
                m_bModeSelection = value;
            }
        }
        //---------------------------------------------------------------------------
        public CObjetDonneeAIdNumeriqueAuto ObjetDoubleClicked
        {
            get
            {
                if (m_bModeSelection)
                    return m_objetDoubleClicked;
                return null;
            }
        }

        public delegate CResultAErreur ModifierElementDelegate(CObjetDonnee objet);
        public ModifierElementDelegate TraiterModificationElement;
        //---------------------------------------------------------------------------
        private void SelectionElement()
        {
            if (m_bModeArbre)
            {
                if (m_arbre.ElementSelectionne == null)
                    return;
            }
            else
            {
                if (m_listView.SelectedItems.Count <= 0)
                    return;
            }

            CObjetDonneeAIdNumeriqueAuto objet = null;

            if (m_bModeArbre)
                objet = (CObjetDonneeAIdNumeriqueAuto)m_arbre.ElementSelectionne;
            else
                objet = (CObjetDonneeAIdNumeriqueAuto)m_listView.SelectedItems[0];

            CResultAErreur result = CResultAErreur.True;

            if (TraiterModificationElement != null && BoutonModifierVisible)
            {
                result = TraiterModificationElement(objet);
            }
            else if (m_gestionnaireModifs != null && BoutonModifierVisible)
            {
                result = m_gestionnaireModifs.Modifier(objet, m_listeObjets, m_listeAffectationsInitiales);

            }
            if (!result)
                CFormAlerte.Afficher(result);
        }
        //---------------------------------------------------------------------------
        private void m_lnkSupprimer_LinkClicked(object sender, System.EventArgs e)
        {
            if (!m_bShowBoutonSupprimer)
                return;
            if (!m_bModeArbre && m_listView.SelectedItems.Count <= 0)
                return;
            if (m_bModeArbre && m_arbre.ElementSelectionne == null)
                return;

            if (m_gestionnaireModifs != null)
            {
                ArrayList lstToDelete = new ArrayList();
                if (m_bModeArbre)
                {
                    lstToDelete.Add(m_arbre.ElementSelectionne);
                }
                else
                {
                    foreach (CObjetDonnee obj in m_listView.SelectedItems)
                        lstToDelete.Add(obj);
                }

                DialogResult reponse = new DialogResult();

                string strSuppression = I.T(" Do you want to remove definitely |133");
                if (lstToDelete.Count == 1)
                {
                    CObjetDonneeAIdNumeriqueAuto obj = (CObjetDonneeAIdNumeriqueAuto)(lstToDelete[0]);
                    strSuppression += obj.DescriptionElement;
                }
                else
                {
                    strSuppression += ":";
                    if (m_bModeArbre)
                    {
                        foreach (CObjetDonneeAIdNumeriqueAuto obj in m_arbre.ElementsSelectionnes)
                            strSuppression += "\r\n     -" + obj.DescriptionElement + ",";
                    }
                    else
                    {
                        foreach (CObjetDonnee obj in m_listView.SelectedItems)
                            strSuppression += "\r\n    - " + obj.DescriptionElement + ",";
                    }
                    strSuppression = strSuppression.Substring(0, strSuppression.Length - 1);
                }
                strSuppression += " ?";

                reponse = CFormAlerte.Afficher(strSuppression, EFormAlerteType.Question);
                //reponse = MessageBox.Show(
                //    CSc2iWin32DataNavigation.Navigateur, 
                //    strSuppression,
                //    I.T("Confirmation|125"), 
                //    MessageBoxButtons.YesNo, 
                //    MessageBoxIcon.Question, 
                //    MessageBoxDefaultButton.Button2);

                if (reponse == DialogResult.No)
                    return;

                CResultAErreur result = m_gestionnaireModifs.Supprimer(lstToDelete);
                if (!result)
                    CFormAlerte.Afficher(result);
                else
                {
                    if (AfterDeleteElement != null)
                        AfterDeleteElement(this, new EventArgs());
                    Refresh();
                    //RemplirGrille();
                }
            }



        }
        //---------------------------------------------------------------------------
        protected void CPanelListeSpeedStandard_Load(object sender, System.EventArgs e)
        {
            if (!DesignMode)
            {
                ReadColonnes();

                CreateControlFiltreStandard();

                MetEnPlaceFiltreInitial();
            }
        }

        //---------------------------------------------------------------------------
        private void ReadColonnes()
        {
            if (!DesignMode)
            {
                try
                {
                    if (AllowCustomisation && AllowSerializePreferences)
                    {
                        CSc2iWin32DataNavigationRegistre.ReadGlacialList(m_listView, ContexteUtilisation, this.Name);
                    }
                }
                catch { }
                //m_listView.ReadFromRegistre(new CSc2iWin32DataNavigationRegistre().GetKey("Preferences\\Panel_Listes\\" + this.ParentForm.GetType().Name + "_" + this.Name, true));

                if (this.ParentForm != null)
                    this.ParentForm.Closing += new CancelEventHandler(ParentForm_ClosingAttitude);
                else if (this.FindForm() != null)
                    this.FindForm().Closing += new CancelEventHandler(ParentForm_ClosingAttitude);
                else if (this.Parent != null)
                {
                    if (this.Parent.FindForm() != null)
                        this.Parent.FindForm().Closing += new CancelEventHandler(ParentForm_ClosingAttitude);
                }
                if (m_bCheckBoxes)
                    m_listView.CheckBoxes = true;
            }
        }

        private int GetVersionPreferenceFiltre()
        {
            return 0;
        }

        //---------------------------------------------------------------------------
        /// <summary>
        /// Chaine identifiant le contexte d'utilisation de la liste, ce qui
        /// conditionne les préférences de cette liste
        /// </summary>
        public string ContexteUtilisation
        {
            get
            {
                return m_strContexteUtilisation;
            }
            set
            {
                m_strContexteUtilisation = value;
            }
        }

        //---------------------------------------------------------------------------
        private bool ReadPreferenceFromRegistre()
        {
            try
            {
                if (!AllowSerializePreferences)
                    return false;
                m_bModeArbre = CSc2iWin32DataNavigationRegistre.GetPreferenceVueArbre(this, ContexteUtilisation);
                if (m_filtrePrefere != null)
                    return false;
                if (m_bModeQuickSearch)
                    return false;
                string strVal = CSc2iWin32DataNavigationRegistre.GetStringPreferenceFiltre(this, ContexteUtilisation);
                if (strVal == "")
                    return false;
                CStringSerializer serializer = new CStringSerializer(strVal, ModeSerialisation.Lecture);
                int nVersion = GetVersionPreferenceFiltre();
                CResultAErreur result = serializer.TraiteVersion(ref nVersion);
                if (!result)
                    return false;

                bool bIsStandard = false;
                serializer.TraiteBool(ref bIsStandard);
                if (bIsStandard)
                {
                    if (m_controlFiltreStandard == null)
                        return false;
                    ControlFiltreEnCours = m_controlFiltreStandard;
                    if (m_controlFiltreEnCours.SerializeFiltre(serializer))
                    {
                        m_controlFiltreEnCours.AppliquerFiltre();
                        m_panelFiltreEtOutils.Visible = true;
                    }
                    else
                        return false;
                }
                else
                {
                    ControlFiltreEnCours = m_panelFiltreStd;
                    if (m_panelFiltreStd.SerializeFiltre(serializer))
                    {
                        m_panelFiltreStd.AppliquerFiltre();
                        m_panelFiltreEtOutils.Visible = true;
                    }
                    else
                        return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        //---------------------------------------------------------------------------
        private void ParentForm_ClosingAttitude(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //m_listView.WriteToRegistre(new CSc2iWin32DataNavigationRegistre().GetKey("Preferences\\Panel_Listes\\" + this.ParentForm.GetType().Name + "_" + this.Name, true));
            if (AllowCustomisation && AllowSerializePreferences)
                CSc2iWin32DataNavigationRegistre.SaveGlacialList(m_listView, ContexteUtilisation, this.Name);
            CSc2iWin32DataNavigationRegistre.SetPreferenceVueArbre(this, ContexteUtilisation, m_bModeArbre);

        }



        //---------------------------------------------------------------------------
        public void AfficherPanelFiltre()
        {
            if (m_controlFiltreEnCours == null)
                return;
            m_panelFiltreEtOutils.Visible = true;
            m_splitter.Visible = true;
        }
        //---------------------------------------------------------------------------
        public IControlDefinitionFiltre ControlFiltreStandard
        {
            get
            {
                return m_controlFiltreStandard;
            }
            set
            {
                if (m_bModeQuickSearch && !(value is CPanelFiltreRapide))
                    return;
                if (m_controlFiltreStandard != null && IsHandleCreated && m_controlFiltreStandard is Control)
                    m_panelFiltre.Controls.Remove((Control)m_controlFiltreStandard);
                m_controlFiltreStandard = value;
                if (m_filtrePrefere == null)
                    ControlFiltreEnCours = m_controlFiltreStandard;
                if (IsHandleCreated)
                {
                    CreateControlFiltreStandard();
                }

                if (value != null)
                {
                    //Si on ne trouve pas de menu pour filtre standard, ajout
                    bool bTrouvé = false;
                    int? nIndexSeparateur = null;
                    int nIndex = 0;
                    foreach (ToolStripItem item in m_menuFiltres.Items)
                    {
                        if (item is CMenuItemForFiltreStandard)
                        {
                            bTrouvé = true;
                            break;
                        }
                        if (item is ToolStripSeparator)
                            nIndexSeparateur = nIndex;
                        nIndex++;
                    }
                    if (!bTrouvé)
                    {
                        CMenuItemForFiltreStandard item = new CMenuItemForFiltreStandard();
                        item.Click += new EventHandler(OnSelectFiltre);
                        if (nIndexSeparateur != null && nIndexSeparateur.Value + 1 < m_menuFiltres.Items.Count)
                            m_menuFiltres.Items.Insert(nIndexSeparateur.Value + 1, item);
                        else
                            m_menuFiltres.Items.Add(item);
                    }
                    m_lnkFiltrer.Visible = BoutonFiltrerVisible;
                    if (ControlFiltreEnCours == null)
                        ControlFiltreEnCours = m_controlFiltreStandard;
                }
            }
        }

        //---------------------------------------------------------------------------
        protected IControlDefinitionFiltre ControlFiltreEnCours
        {
            get
            {
                return m_controlFiltreEnCours;
            }
            set
            {
                m_controlFiltreEnCours = value;
                if (m_controlFiltreEnCours == m_panelFiltreStd)
                {
                    m_panelFiltreStd.Visible = true;
                    if (m_controlFiltreStandard is Control)
                    {
                        ((Control)m_controlFiltreStandard).Visible = false;
                    }
                }
                else
                {
                    m_panelFiltreStd.Visible = false;
                    if (m_controlFiltreStandard is Control)
                    {
                        ((Control)m_controlFiltreStandard).Visible = true;
                    }
                }

            }
        }

        //---------------------------------------------------------------------------
        private void CreateControlFiltreStandard()
        {
            if (m_controlFiltreStandard == null)
                return;
            m_panelFiltreEtOutils.Visible = false;
            m_splitter.Visible = false;
            if (m_controlFiltreStandard != null)
                if (m_controlFiltreStandard is Control)
                {
                    Control ctrlFiltre = (Control)m_controlFiltreStandard;
                    ctrlFiltre.Parent = m_panelFiltre;
                    ctrlFiltre.Dock = DockStyle.Fill;
                    ctrlFiltre.Visible = true;
                    m_controlFiltreStandard.OnAppliqueFiltre += new EventHandler(OnChangeFiltre);
                    m_splitter.MinSize = m_controlFiltreEnCours.MinHeight + m_panelFiltre.Size.Height - m_panelFiltre.ClientSize.Height;
                    m_panelFiltre.Height = m_splitter.MinSize;
                }
            MetEnPlaceFiltreInitial();
        }

        //---------------------------------------------------------------------------
        public void OnChangeFiltre(object sender, EventArgs e)
        {
            m_filtre = null;
            if (m_controlFiltreEnCours != null)
                m_filtre = m_controlFiltreEnCours.Filtre;
            if (m_listeObjets == null)
                return;
            m_bFiltreDeBaseAppliqueAListe = false;
            RemplirGrille();
        }

        //---------------------------------------------------------------------------
        public void FillContexte(CContexteFormNavigable ctx)
        {
            string strPrefix = Name;
            ctx[strPrefix + "QUICK_SEARCH"] = m_bModeQuickSearch;
            ctx[strPrefix + "QUICK_FILTRE"] = m_filtreRechercheRapide;
            ctx[strPrefix + "FILTRE_VISIBLE"] = m_panelFiltreEtOutils.Visible;
            ctx[strPrefix + "ACTION_DELEGATE"] = TraiterModificationElement;
            try
            {
                if (m_controlFiltreStandard != null)
                    m_controlFiltreStandard.FillContexte(ctx);
            }
            catch
            {
            }
            ctx[strPrefix + "FILTRE_STD"] = null;
            try
            {
                if (m_controlFiltreEnCours == m_panelFiltreStd)
                {
                    //Sauvegarde le filtre en cours
                    CFiltreDynamique filtre = m_panelFiltreStd.GetFiltreDynamique();
                    if (filtre != null)
                        ctx[strPrefix + "FILTRE_STD"] = filtre;
                }
            }
            catch
            {
            }
            ctx["AFFECTATIONS"] = m_listeAffectationsInitiales;

            List<int> lstIdsChecked = new List<int>();
            foreach (object obj in m_listView.CheckedItems)
            {
                CObjetDonneeAIdNumerique objId = obj as CObjetDonneeAIdNumerique;
                if (objId != null)
                    lstIdsChecked.Add(objId.Id);
            }
            ctx["CHECKED"] = lstIdsChecked.ToArray();


        }

        //---------------------------------------------------------------------------
        public void InitFromContexte(CContexteFormNavigable ctx)
        {
            string strPrefix = Name;
            m_bModeQuickSearch = (bool)ctx[strPrefix + "QUICK_SEARCH"];
            m_filtreRechercheRapide = (CFiltreData)ctx[strPrefix + "QUICK_FILTRE"];
            try
            {
                if (m_controlFiltreStandard != null)
                    m_controlFiltreStandard.InitFromContexte(ctx);
            }
            catch
            {
            }
            try
            {
                if (ctx[strPrefix + "FILTRE_STD"] != null)
                {
                    FiltrePrefere = (CFiltreDynamique)ctx[strPrefix + "FILTRE_STD"];
                    if (FiltrePrefere != null && FiltrePrefere.ComposantPrincipal == null)
                        FiltrePrefere = null;
                }
            }
            catch
            {
            }
            m_panelFiltreEtOutils.Visible = (bool)ctx[strPrefix + "FILTRE_VISIBLE"];
            m_bPanelFiltreVisibleFromContexte = (bool)ctx[strPrefix + "FILTRE_VISIBLE"];
            m_listeAffectationsInitiales = ctx["AFFECTATIONS"] as List<CAffectationsProprietes>;
            TraiterModificationElement = ctx[strPrefix + "ACTION_DELEGATE"] as ModifierElementDelegate;
            if (m_listeAffectationsInitiales == null)
                m_listeAffectationsInitiales = new List<CAffectationsProprietes>();
            int[] lstIdChecked = ctx["CHECKED"] as int[];
            if (lstIdChecked != null)
            {
                m_listeIdCheckedFromContexte = new List<int>();
                m_listeIdCheckedFromContexte.AddRange(lstIdChecked);
            }

        }

        //-------------------------------------------------------------------
        public bool EnableCustomisation
        {
            get
            {
                return m_listView.EnableCustomisation;
            }
            set
            {
                m_listView.EnableCustomisation = value;

            }
        }
        //-------------------------------------------------------------------
        public Control ControlAjout
        {
            get
            {
                return m_lnkAjouter;
            }
        }
        //-------------------------------------------------------------------
        public Control ControlModification
        {
            get
            {
                return m_lnkModifier;
            }
        }
        //-------------------------------------------------------------------
        public Control ControlSuppression
        {
            get
            {
                return m_lnkSupprimer;
            }
        }
        //-------------------------------------------------------------------
        [DefaultValue(true)]
        public bool BoutonAjouterVisible
        {
            get
            {
                return m_bShowBoutonAjouter;
            }
            set
            {
                m_bShowBoutonAjouter = value;
                m_lnkAjouter.Visible = value & (RestrictionActive.RestrictionGlobale & ERestriction.NoCreate) != ERestriction.NoCreate;
            }
        }

        //-------------------------------------------------------------------
        private CRestrictionUtilisateurSurType RestrictionActive
        {
            get
            {
                if (m_restrictionAppliquee == null)
                    return new CRestrictionUtilisateurSurType(m_listeObjets != null ? m_listeObjets.TypeObjets : typeof(string));
                return m_restrictionAppliquee;
            }
        }


        //-------------------------------------------------------------------
        [DefaultValue(true)]
        public bool BoutonModifierVisible
        {
            get
            {
                return m_bShowBoutonModifier;
            }
            set
            {
                m_bShowBoutonModifier = value;
                m_lnkModifier.Visible = value & (RestrictionActive.RestrictionGlobale & ERestriction.Hide) != ERestriction.Hide;
            }
        }

        //-------------------------------------------------------------------
        [DefaultValue(true)]
        public bool BoutonSupprimerVisible
        {
            get
            {
                return m_bShowBoutonSupprimer;
            }
            set
            {
                m_bShowBoutonSupprimer = value;
                m_lnkSupprimer.Visible = value & (RestrictionActive.RestrictionGlobale & ERestriction.NoDelete) != ERestriction.NoDelete;
            }
        }
        //-------------------------------------------------------------------
        [DefaultValue(true)]
        public bool BoutonExporterVisible
        {
            get
            {
                return m_bShowBoutonExporter;
            }
            set
            {
                m_bShowBoutonExporter = value;
                m_imgExport.Visible = value & (RestrictionActive.RestrictionGlobale & ERestriction.Hide) != ERestriction.Hide;
            }
        }

        //-------------------------------------------------------------------
        [DefaultValue(true)]
        public bool BoutonFiltrerVisible
        {
            get
            {
                return m_bShowBoutonFiltrer;
            }
            set
            {
                m_bShowBoutonFiltrer = value;
                m_lnkFiltrer.Visible = value;
            }
        }

        //-------------------------------------------------------------------
        [DefaultValue(true)]
        public bool BoutonListesVisible
        {
            get
            {
                return m_bShowListes;
            }
            set
            {
                m_bShowListes = value;
                m_panelListesEntites.Visible = value;
            }
        }

        //-------------------------------------------------------------------
        [DefaultValue(true)]
        public bool BoutonExtraireListeVisible
        {
            get
            {
                return m_bBoutonExtraiteVisible;
            }
            set
            {
                m_bBoutonExtraiteVisible = value;
                m_btnExtractList.Visible = value;
            }
        }

        //-------------------------------------------------------------------
        public CListeObjetsDonnees GetElementsCheckes()
        {
            CListeObjetsDonnees liste = new CListeObjetsDonnees(m_listeObjets.ContexteDonnee, m_listeObjets.TypeObjets);
            string strListe = "";
            if (m_bModeArbre)
            {
                foreach (CObjetDonneeAIdNumeriqueAuto objet in m_arbre.ElementsSelectionnes)
                    strListe += objet.Id.ToString() + ",";
            }
            else
            {
                foreach (CObjetDonneeAIdNumeriqueAuto objet in m_listView.CheckedItems)
                    strListe += objet.Id.ToString() + ",";
            }
            if (strListe.Length > 1)
            {
                strListe = strListe.Substring(0, strListe.Length - 1);
                liste.Filtre = new CFiltreData(((CObjetDonneeAIdNumeriqueAuto)m_listeObjets[0]).GetChampId() + " in (" +
                    strListe + ")");
            }
            else
                liste.Filtre = new CFiltreDataImpossible();

            return liste;
        }

        //-------------------------------------------------------------------
        public void SetElementsCheckes(List<CObjetDonnee> lstSels)
        {
            if (m_bModeArbre)
            {
                m_arbre.ElementsSelectionnes = lstSels.ToArray();
            }
            else
            {
                m_listView.CheckedItems = new ArrayList(lstSels);
            }
        }


        //-------------------------------------------------------------------
        public CListeObjetsDonnees GetElementsSelectionnes()
        {
            CListeObjetsDonnees liste = new CListeObjetsDonnees(m_listeObjets.ContexteDonnee, m_listeObjets.TypeObjets);
            string strListe = "";
            if (m_bModeArbre)
            {
                foreach (CObjetDonneeAIdNumeriqueAuto objet in m_arbre.ElementsSelectionnes)
                    strListe += objet.Id.ToString() + ",";
            }
            else
            {
                foreach (CObjetDonneeAIdNumeriqueAuto objet in m_listView.SelectedItems)
                    strListe += objet.Id.ToString() + ",";
            }
            if (strListe.Length > 1)
            {
                strListe = strListe.Substring(0, strListe.Length - 1);
                liste.Filtre = new CFiltreData(((CObjetDonneeAIdNumeriqueAuto)m_listeObjets[0]).GetChampId() + " in (" +
                    strListe + ")");
            }
            else
                liste.Filtre = new CFiltreDataImpossible();

            return liste;
        }


        //-------------------------------------------------------------------
        private void m_menuItemExportAvance_Click(object sender, EventArgs e)
        {
            CListeObjetsDonnees liste = AskListToExport();

            if (liste != null)
                CFormSelectStructure.Export(liste);

        }

        //-------------------------------------------------------------------
        private void m_menuItemExportSimple_Click(object sender, EventArgs e)
        {
            CListeObjetsDonnees liste = AskListToExport();

            if (liste != null)
            {

                IExporteurDataset exporteur = null;
                IDestinationExport destination = null;
                if (CFormOptionsExport.EditeOptions(ref destination, ref exporteur))
                {
                    if (exporteur != null)
                    {
                        DataSet ds = null;
                        DataTable dt = GetDataTableFromList(liste);
                        if (dt == null)
                        {
                            CFormAlerte.Afficher(I.T("Can not export that list|20002"), EFormAlerteType.Info);
                            return;
                        }

                        if (dt.DataSet == null)
                        {
                            ds = new DataSet();
                            ds.Tables.Add(dt);
                        }
                        CResultAErreur result = exporteur.Export(dt.DataSet, destination);
                        if (!result)
                        {
                            CFormAlerte.Afficher(result.Erreur);
                            return;
                        }
                        else
                        {
                            CFormAlerte.Afficher(I.T("Export completed|136"));
                        }
                    }
                }
            }
        }


        //-------------------------------------------------------------------
        private CListeObjetsDonnees AskListToExport()
        {
            CListeObjetsDonnees liste = m_listeObjets;

            if (m_listView.CheckBoxes && m_listView.CheckedItems.Count != 0 &&
                typeof(CObjetDonneeAIdNumeriqueAuto).IsAssignableFrom(m_listeObjets.TypeObjets))
            {
                DialogResult res = CFormAlerte.Afficher(I.T("Export all elements (Yes) or only checked elements (No)|134"),
                    EFormAlerteBoutons.OuiNonCancel, EFormAlerteType.Question);
                if (res == DialogResult.Cancel)
                    return null;
                if (res == DialogResult.No)
                    liste = GetElementsCheckes();
            }

            return liste;
        }

        //-------------------------------------------------------------------
        public void AddEventOnAfterSuppression(ObjetDonneeEventHandler evnt)
        {
            m_gestionnaireModifs.AfterSuppressionDansFormEdition += evnt;
        }
        //-------------------------------------------------------------------
        public void AddEventOnAfterAnnulation(ObjetDonneeEventHandler evnt)
        {
            m_gestionnaireModifs.AfterAnnulationDansFormEdition += evnt;
        }
        //-------------------------------------------------------------------
        public void AddEventOnAfterValidation(ObjetDonneeEventHandler evnt)
        {
            m_gestionnaireModifs.AfterValidationDansFormEdition += evnt;
        }
        //-------------------------------------------------------------------
        public void AddEventOnBeforeAfficheForm(EventHandler evnt)
        {
            m_gestionnaireModifs.BeforeAfficheForm += evnt;
        }

        //-------------------------------------------------------------------
        [EditorBrowsable(EditorBrowsableState.Never)]
        public CFiltreDynamique FiltrePrefere
        {
            get
            {
                return m_filtrePrefere;
            }
            set
            {
                m_filtrePrefere = value;
                if (value != null)
                {
                    ControlFiltreEnCours = m_panelFiltreStd;
                    m_panelFiltreStd.SetFiltreDynamique(m_filtrePrefere);
                }
            }
        }

        //-------------------------------------------------------------------
        //Sélectionne le filtre initial
        private void MetEnPlaceFiltreInitial()
        {

            if (m_filtrePrefere != null)
            {
                m_panelFiltreStd.SetFiltreDynamique(m_filtrePrefere);
                ControlFiltreEnCours = m_panelFiltreStd;
                m_panelFiltreStd.AppliquerFiltre();
            }
            else if (ReadPreferenceFromRegistre())
                return;
            else if (m_controlFiltreStandard != null)
            {
                ControlFiltreEnCours = m_controlFiltreStandard;
            }
            else
            {
                if (m_menuFiltres.Items.Count == 0)
                    InitMenuFiltres();
                if (!m_bModeQuickSearch)
                {
                    //Sélectionne le premier filtre trouvé
                    foreach (ToolStripItem item in m_menuFiltres.Items)
                    {
                        if (item is CMenuItemAFiltre)
                        {
                            m_panelFiltreStd.SetFiltreDynamique(((CMenuItemAFiltre)item).Filtre.Filtre);

                            ControlFiltreEnCours = m_panelFiltreStd;
                            break;
                        }
                    }
                }
            }
            if ((m_filtrePrefere != null && m_filtrePrefere.ComposantPrincipal != null) || m_bModeQuickSearch)
                AfficherPanelFiltre();
        }

        //-------------------------------------------------------------------
        private void m_lnkFiltrer_LinkClicked(object sender, System.EventArgs e)
        {
            if (m_controlFiltreEnCours == null)
            {
                MetEnPlaceFiltreInitial();
                if (m_controlFiltreEnCours == null)
                    return;
            }
            bool bFiltreAffiche = m_panelFiltreEtOutils.Visible;
            m_panelFiltreEtOutils.Visible = !bFiltreAffiche;
            m_splitter.Visible = !bFiltreAffiche;
        }

        private void m_btnListeFiltres_Click(object sender, System.EventArgs e)
        {
            m_menuFiltres.Show(m_btnListeFiltres, new Point(0, m_btnListeFiltres.Height));
        }

        //-------------------------------------------------------------------
        private void m_btnAppliquer_Click(object sender, System.EventArgs e)
        {
            if (m_controlFiltreEnCours != null)
                m_controlFiltreEnCours.AppliquerFiltre();
        }

        private void CPanelListeSpeedStandard_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (m_panelFiltre.Visible && e.KeyCode == Keys.Return && m_controlFiltreEnCours != null)
                m_controlFiltreEnCours.AppliquerFiltre();

        }

        //-------------------------------------------------------------------
        private void m_btnPreference_Click(object sender, System.EventArgs e)
        {
            if (CFormAlerte.Afficher(I.T("This Filter will be applied by default on openning this wondow, are you sure?|124"),
                EFormAlerteType.Question) == DialogResult.Yes)
            {
                m_controlFiltreEnCours.AppliquerFiltre();
                CStringSerializer serializer = new CStringSerializer(ModeSerialisation.Ecriture);
                int nVersion = GetVersionPreferenceFiltre();
                serializer.TraiteVersion(ref nVersion);

                bool bIsStandard = ControlFiltreEnCours == ControlFiltreStandard;
                serializer.TraiteBool(ref bIsStandard);

                ControlFiltreEnCours.SerializeFiltre(serializer);

                CSc2iWin32DataNavigationRegistre.SetStringPreferenceFiltre(this, ContexteUtilisation, serializer.String);
            }
        }

        //-------------------------------------------------------------------
        private void m_btnPreference_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (CFormAlerte.Afficher(I.T("Remove preferences|126"),
                    EFormAlerteType.Question) == DialogResult.Yes)
                {
                    CSc2iWin32DataNavigationRegistre.SetStringPreferenceFiltre(this, ContexteUtilisation, "");
                }
            }
        }

        //-------------------------------------------------------------------
        public bool m_bFiltreDeBaseEnAjout = false;

        public event EventHandler OnChangeSelection;

        private void m_listView_OnChangeSelection(object sender, System.EventArgs e)
        {
            if (OnChangeSelection != null)
                OnChangeSelection(this, e);
        }

        private void m_arbre_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (OnChangeSelection != null)
                OnChangeSelection(this, e);
        }

        private IButtonControl m_lastAcceptButton = null;
        private void m_btnAppliquer_Enter(object sender, System.EventArgs e)
        {
            Form frm = FindForm();
            if (frm != null)
            {
                try
                {
                    m_lastAcceptButton = frm.AcceptButton;
                    frm.AcceptButton = m_btnAppliquer;
                }
                catch { }
            }
        }

        private void m_btnAppliquer_Leave(object sender, System.EventArgs e)
        {
            Form frm = FindForm();
            if (frm != null)
            {
                try
                {
                    frm.AcceptButton = m_lastAcceptButton;
                }
                catch { }
            }
        }

        public bool FiltreDeBaseEnAjout
        {
            get
            {
                return m_bFiltreDeBaseEnAjout;
            }
            set
            {
                m_bFiltreDeBaseEnAjout = value;
            }
        }

        public CFiltreData FiltreDeBase
        {
            get
            {
                return m_filtreDeBase;
            }
            set
            {
                if (m_bFiltreDeBaseEnAjout)
                    m_filtreDeBase = CFiltreData.GetAndFiltre(m_filtreDeBase, value);
                else
                    m_filtreDeBase = value;
            }
        }

        //-------------------------------------------------------------------
        public bool ModeQuickSearch
        {
            get
            {
                return m_bModeQuickSearch;
            }
            set
            {
                m_bModeQuickSearch = value;
            }
        }

        //-------------------------------------------------------------------
        public CFiltreData FiltreRapide
        {
            get
            {
                return m_filtreRechercheRapide;
            }
            set
            {
                m_filtreRechercheRapide = value;
                if (m_controlFiltreStandard is CPanelFiltreRapide)
                {
                    ((CPanelFiltreRapide)m_controlFiltreStandard).FiltreRapide = m_filtreRechercheRapide;
                }
            }
        }

        //-------------------------------------------------------------------
        public string QuickSearchText
        {
            get
            {
                return m_strQuickSearchText;
            }
            set
            {
                m_strQuickSearchText = value;
                if (m_controlFiltreStandard is CPanelFiltreRapide)
                {
                    ((CPanelFiltreRapide)m_controlFiltreStandard).QuickSearchText = value; ;
                }
            }
        }

        //-------------------------------------------------------------------
        public CObjetDonnee[] ElementsSelectionnes
        {
            get
            {
                if (m_bModeArbre)
                {
                    CObjetDonnee obj = ElementSelectionne;
                    if (obj != null)
                        return new CObjetDonnee[] { obj };
                    return new CObjetDonnee[0];
                }
                else
                {
                    List<CObjetDonnee> lst = new List<CObjetDonnee>();
                    foreach (object obj in m_listView.SelectedItems)
                    {
                        CObjetDonnee o = obj as CObjetDonnee;
                        if (o != null)
                            lst.Add(o);
                    }
                    return lst.ToArray();
                }
            }
        }

        //-------------------------------------------------------------------
        public CObjetDonnee ElementSelectionne
        {
            get
            {
                if (m_bModeArbre)
                {
                    return m_arbre.ElementSelectionne;
                }
                else
                {
                    if (m_listView.SelectedItems.Count == 1)
                        return (CObjetDonnee)m_listView.SelectedItems[0];
                }
                return null;
            }
            set
            {
                if (m_arbre != null && m_bModeArbre)
                    m_arbre.ElementSelectionne = value;
                else if (m_listView != null && m_listView.ListeSource != null)
                {
                    if (value == null)
                    {
                        m_listView.SelectedItems.Clear();
                        m_listView.Refresh();
                    }
                    else
                    {
                        int nIdx = 0;
                        foreach (CObjetDonnee o in m_listView.ListeSource)
                        {
                            if (o == value)
                            {
                                m_listView.SelectItem(nIdx);
                                break;
                            }
                            nIdx++;
                        }
                    }
                }
            }
        }

        //-------------------------------------------------------------------
        public override void Refresh()
        {
            if (m_listeObjets != null)
                m_listeObjets.Refresh();
            RemplirGrille();
        }

        private void m_chkVueListe_CheckedChanged(object sender, EventArgs e)
        {
            m_bModeArbre = false;
            RemplirGrille();
        }

        private void m_chkVueArbre_CheckedChanged(object sender, EventArgs e)
        {
            m_bModeArbre = true;
            RemplirGrille();
        }

        private void m_timerRemplir_Tick(object sender, EventArgs e)
        {
            RemplirGrilleSansTimer();
        }

        //-----------------------------------------------------------------------
        private int[] GetListeIdsChampsCustomALire()
        {
            List<int> lstIds = new List<int>();
            string strKey = "#CC|VAL¤";
            foreach (GLColumn colonne in this.Columns)
            {
                int nIndexCC = colonne.Propriete.IndexOf(strKey);
                if (nIndexCC > 0)
                {
                    int nNext = colonne.Propriete.IndexOf("¤", nIndexCC + strKey.Length);
                    if (nNext >= 0)
                    {
                        string strId = colonne.Propriete.Substring(nIndexCC + strKey.Length, nNext - nIndexCC - strKey.Length);
                        try
                        {
                            int nId;
                            //TESTDBKEYOK (SC)
                            if (!int.TryParse(strId, out nId))
                            {
                                CDbKey key = CDbKey.CreateFromStringValue(strId);
                                nId = CChampCustom.GetIdFromDbKey(key);
                            }
                            lstIds.Add(nId);
                        }
                        catch { }
                    }
                }
            }
            return lstIds.ToArray();
        }



        //-----------------------------------------------------------------------
        private DataTable GetDataTableFromList(CListeObjetsDonnees listeSource)
        {
            DataTable dt = new DataTable("EXPORT");

            List<GLColumn> lst = new List<GLColumn>();
            foreach (GLColumn col in Columns)
                lst.Add(col);
            C2iStructureExport structure = CConvertisseurInfoStructureDynamiqueToDefinitionChamp.ConvertToStructureExport(listeSource.TypeObjets,
                lst.ToArray());
            if (structure == null)
            {
                return null;
            }
            structure.TraiterSurServeur = true;
            DataSet ds = null;
            CResultAErreur result = structure.Export(ListeObjets.ContexteDonnee.IdSession,
                ListeObjets,
                ref ds,
                null);
            if (!result)
                return null;
            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;

            /*
            //Préchargement de la liste et de ses dépendances
            List<string> lstDep = new List<string>();
            List<int> idsChampsCustoms = new List<int>();
            foreach (GLColumn colonne in this.Columns)
            {
                if (colonne.Propriete.IndexOf('.') > 0)
                    lstDep.Add(colonne.Propriete);
                
            }
            if (lstDep.Count > 0)
                listeSource.ReadDependances(lstDep.ToArray());
            if (typeof(IObjetDonneeAChamps).IsAssignableFrom(listeSource.TypeObjets))
            {
                int[] idsChamps = GetListeIdsChampsCustomALire();
                if (idsChamps.Length > 0)
                    CUtilElementAChamps.ReadChampsCustom(listeSource, idsChamps);
            }

            // Construction des colonnes
            foreach (GLColumn col in m_listView.Columns)
            {
                if (!dt.Columns.Contains(col.Text))
                    dt.Columns.Add(col.Text);
            }

            // Remplissage des lignes
            for (int nItem = 0; nItem < listeSource.Count; nItem++)
            {
                dt.Rows.Add(dt.NewRow());
                for (int nCol = 0; nCol < Columns.Count; nCol++)
                {
                    string strPropriete = Columns[nCol].Propriete;
                    object obj = null;
                    if (listeSource != null && nItem >= 0 && nItem < listeSource.Count)
                    {
                        obj = listeSource[nItem];
                        if (obj != null)
                        {
                            // Met la valeur de la propriété dans la cellule de le table
                            dt.Rows[nItem][nCol] = CInfoStructureDynamique.GetDonneeDynamiqueString(obj, strPropriete, "");

                        }
                    }

                }
            }

            // Supprime éventuellement la première colonne des check boxes
            if(dt.Columns.Contains("Column1"))
                dt.Columns.Remove("Column1");

            return dt;*/
        }


        private void m_imgExport_Click(object sender, EventArgs e)
        {
            m_menuExport.Show(m_imgExport, 0, m_imgExport.Height);

        }

        public bool HasImages
        {
            get
            {
                return m_listView.HasImages;
            }
            set
            {
                m_listView.HasImages = value;
            }
        }

        public event GlacialListGetImageEventHandler OnGetImage;

        private Image m_listView_OnGetImage(object obj)
        {
            if (OnGetImage != null)
                return OnGetImage(obj);
            return null;
        }

        void m_listView_OnBeginDragItem(object sender, object itemDrag)
        {
            CObjetDonnee[] objets = ElementsSelectionnes;
            if (objets != null && objets.Length > 0)
            {
                DoDragDrop(new CReferenceObjetDonneeDragDropData(objets), DragDropEffects.None | DragDropEffects.Move | DragDropEffects.Copy | DragDropEffects.Link);
            }

            /*
            if (ElementSelectionne != null)
            {
                CReferenceObjetDonnee reference = new CReferenceObjetDonnee(ElementSelectionne);
                if (reference != null)
                {
                    DoDragDrop(reference, DragDropEffects.None | DragDropEffects.Move | DragDropEffects.Copy | DragDropEffects.Link);
                }
            }
            CObjetDonnee[] objets = ElementsSelectionnes;
            if ( objets != null && objets.Length > 0 )
            {
                List<CReferenceObjetDonnee> lst = new List<CReferenceObjetDonnee>();
                foreach ( CObjetDonnee objet in objets )
                {
                    lst.Add ( new CReferenceObjetDonnee ( objet ));
                }
                if ( lst.Count > 0 )
                {
                    DoDragDrop ( lst.ToArray(), DragDropEffects.None | DragDropEffects.Move | DragDropEffects.Copy | DragDropEffects.Link );
                }
            }*/
        }


        #region IControlALockEdition Membres

        public bool LockEdition
        {
            get
            {
                return !m_gestionnaireModeEdition.ModeEdition;
            }
            set
            {
                m_gestionnaireModeEdition.ModeEdition = !value;
                this.ModeSelection = value;
                if (OnChangeLockEdition != null)
                    OnChangeLockEdition(this, new EventArgs());
            }
        }


        public event EventHandler OnChangeLockEdition;

        #endregion

        private void m_panelFiltreStd_OnChangeDesiredHeight(int nHeight)
        {
            m_panelFiltreEtOutils.Height = Math.Max(nHeight + 4, 104);
        }

        public CObjetDonnee GetObjetQuickSearchSiUnique()
        {
            if (m_listeObjets != null && FiltreRapide != null)
            {
                m_filtre = FiltreRapide;
                if (m_filtre != null && m_filtre.Parametres.Count > 0)
                    m_filtre.Parametres[0] = "%" + QuickSearchText + "%";
                m_listeObjets.Filtre = GetFiltreAAppliquer(true);
                if (m_listeObjets.Count == 1)
                    return m_listeObjets[0] as CObjetDonnee;
            }
            return null;
        }


        #region IControleAGestionRestrictions Membres

        public void AppliqueRestrictions(CListeRestrictionsUtilisateurSurType restrictions, IGestionnaireReadOnlySysteme gestionnaireReadOnly)
        {
            m_listeRestrictions = restrictions.Clone() as CListeRestrictionsUtilisateurSurType;
            m_gestionnaireReadOnly = gestionnaireReadOnly;
            if (m_listeObjets != null && restrictions != null && gestionnaireReadOnly != null)
            {
                m_restrictionAppliquee = restrictions.GetRestriction(m_listeObjets.TypeObjets);
                //force l'affichage des boutons en fonction des restrictions
                BoutonAjouterVisible = BoutonAjouterVisible;
                BoutonSupprimerVisible = BoutonSupprimerVisible;
                BoutonModifierVisible = BoutonModifierVisible;
                BoutonExporterVisible = BoutonExporterVisible;
            }
        }

        #endregion

        private void m_menuCopier_Click(object sender, EventArgs e)
        {
            CListeObjetsDonnees liste = AskListToExport();

            if (liste != null)
            {
                DataSet ds = null;
                DataTable dt = GetDataTableFromList(liste);
                if (dt == null)
                {
                    CFormAlerte.Afficher(I.T("Can not export that list|20002"), EFormAlerteType.Info);
                    return;
                }

                if (dt.DataSet == null)
                {
                    ds = new DataSet();
                    ds.Tables.Add(dt);
                }
                StringBuilder bl = new StringBuilder();
                foreach (DataColumn col in dt.Columns)
                {
                    bl.Append(col.ColumnName.Replace("\t", " "));
                    bl.Append("\t");
                }
                bl.Append(Environment.NewLine);
                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn col in dt.Columns)
                    {
                        object val = row[col];
                        if (val == null || val == DBNull.Value)
                            val = "";
                        bl.Append(val.ToString().Replace("\t", " "));
                        bl.Append("\t");
                    }
                    bl.Append(Environment.NewLine);
                }
                Clipboard.SetData(DataFormats.Text, bl.ToString());
            }
        }

        //------------------------------------------------
        private void FillListesDisponibles()
        {
            if (BoutonListesVisible)
            {
                List<CControlPourListeDeListePanelSpeed.CItemListe> lstItems = new List<CControlPourListeDeListePanelSpeed.CItemListe>();
                if (m_listeObjets != null)
                {
                    CListeObjetDonneeGenerique<CListeEntites> lstListes = new CListeObjetDonneeGenerique<CListeEntites>(
                        m_listeObjets.ContexteDonnee);
                    lstListes.Filtre = new CFiltreData(
                        CListeEntites.c_champTypeElements + "=@1",
                        m_listeObjets.TypeObjets.ToString());
                    CListeObjetsDonnees lstTmp = new CListeObjetsDonnees(m_listeObjets.ContexteDonnee, m_listeObjets.TypeObjets,
                            CFiltreData.GetAndFiltre(m_listeObjets.FiltrePrincipal,
                            m_filtreDeBase));
                    foreach (CListeEntites lstEntites in lstListes)
                    {
                        lstTmp.Filtre = lstEntites.GetFiltrePourListe();
                        int nNb = lstTmp.CountNoLoad;
                        if (nNb > 0)
                        {
                            CControlPourListeDeListePanelSpeed.CItemListe item = new CControlPourListeDeListePanelSpeed.CItemListe(
                                lstEntites, nNb);
                            lstItems.Add(item);
                        }
                    }
                }
                m_wndListeListes.Items = lstItems.ToArray();
                m_wndListeListes.Refresh();
                m_panelListesEntites.Visible = lstItems.Count > 0;
                m_bListeDeListeIsInit = true;
            }
            else
                m_panelListesEntites.Visible = false;
        }

        //------------------------------------------------
        void m_controleListe_OnSelectList(CListeEntites liste)
        {
            m_listeEntitesEnCours = liste;
            m_bFiltreDeBaseAppliqueAListe = false;
            RemplirGrilleSansTimer();
        }

        //------------------------------------------------
        private void CollapseListeDeListes()
        {
            m_bListeDeListeIsCollapse = true;
            this.SuspendDrawing();
            m_panelListesEntites.Width = m_panelMargeListeDeListe.Width;
            m_splitterListeListes.Visible = false;
            if (m_listeObjets != null)
                CSc2iWin32DataNavigationRegistre.SetShowListeDeListeSpeed(
                    m_listeObjets.TypeObjets, m_strContexteUtilisation, false);
            this.ResumeDrawing();
        }

        //------------------------------------------------
        private void ExpandListeDeListes()
        {
            this.SuspendDrawing();
            m_panelListesEntites.Width = m_nLargeurListeDeListe;
            m_splitterListeListes.Visible = true;
            m_bListeDeListeIsCollapse = false;
            if (!m_bListeDeListeIsInit)
                FillListesDisponibles();
            if (m_listeObjets != null)
                CSc2iWin32DataNavigationRegistre.SetShowListeDeListeSpeed(
                    m_listeObjets.TypeObjets, m_strContexteUtilisation, true);
            this.ResumeDrawing();
        }

        //------------------------------------------------
        private void m_panelListesEntites_SizeChanged(object sender, EventArgs e)
        {
            if (m_panelListesEntites.Width < 2 * m_panelMargeListeDeListe.Width)
                CollapseListeDeListes();
            else
            {
                m_nLargeurListeDeListe = m_panelListesEntites.Width;
                CSc2iWin32DataNavigationRegistre.LargeurListeDeListeDansPanelListeSpeed = m_panelListesEntites.Width;
            }
        }

        //------------------------------------------------
        private void m_btnCollapseExpandListeDeListe_Click(object sender, EventArgs e)
        {
            if (m_bListeDeListeIsCollapse)
                ExpandListeDeListes();
            else
                CollapseListeDeListes();
        }

        //------------------------------------------------
        private void m_btnNoListe_Click(object sender, EventArgs e)
        {
            m_listeEntitesEnCours = null;
            m_bFiltreDeBaseAppliqueAListe = false;
            RemplirGrilleSansTimer();
        }

        //------------------------------------------------
        private void m_txtFiltreListe_Click(object sender, EventArgs e)
        {
            m_txtFiltreListe.Visible = !m_txtFiltreListe.Visible;
            if (!m_txtFiltreListe.Visible)
                m_txtFiltreListe.Text = "";
        }

        //------------------------------------------------
        private void m_txtFiltre_TextChanged(object sender, EventArgs e)
        {
            string strText = m_txtFiltreListe.Text.Trim().ToUpper();
            m_wndListeListes.CurrentItemIndex = null;
            foreach (CCustomizableListItem item in m_wndListeListes.Items)
            {
                CControlPourListeDeListePanelSpeed.CItemListe il = item as CControlPourListeDeListePanelSpeed.CItemListe;
                if (il != null)
                {
                    if (strText.Length == 0 || il.ListeEntites.Libelle.ToUpper().Contains(strText))
                        il.IsMasque = false;
                    else
                        il.IsMasque = true;
                }
            }
            m_wndListeListes.Refill();
        }

        private void m_btnExtractList_Click(object sender, EventArgs e)
        {
            string strText = "";
            Form frm = FindForm();
            if (frm != null)
                strText = frm.Text;
            CFormListeStandard frmListe = frm as CFormListeStandard;
            if (frmListe != null)
                strText = frmListe.GetTitle();
            CFormEditionStandard frmEdit = frm as CFormEditionStandard;
            if (frmEdit != null)
            {
                //Si contenu dans un onglet, récupère le titre de l'onglet
                Control parent = Parent;
                while (parent != null && !(parent is Crownwood.Magic.Controls.TabPage))
                    parent = parent.Parent;
                Crownwood.Magic.Controls.TabPage page = parent as Crownwood.Magic.Controls.TabPage;
                if (page != null)
                {
                    strText = page.Title + "/" + frmEdit.GetTitle();
                }
            }
            CFormListeExtraite.ShowListeFromPanelListStd(strText, this);
        }

        public bool ShortIcons
        {
            get
            {
                return m_lnkAjouter.ShortMode;
            }
            set
            {
                m_lnkAjouter.ShortMode = value;
                m_lnkFiltrer.ShortMode = value;
                m_lnkSupprimer.ShortMode = value;
                m_lnkModifier.ShortMode = value;
            }
        }

        //-----------------------------------------------------------------------
        private void m_btnSaveList_Click(object sender, EventArgs e)
        {

        }

        //-----------------------------------------------------------------------
        void itemNewList_Click(object sender, EventArgs e)
        {
            using (CContexteDonnee ctx = new CContexteDonnee(ListeObjets.ContexteDonnee.IdSession, true, false))
            {
                CListeEntites liste = new CListeEntites(ctx);
                liste.CreateNewInCurrentContexte();
                liste.TypeElements = ListeObjets.TypeObjets;
                CSessionClient session = CSessionClient.GetSessionForIdSession(ctx.IdSession);
                IInfoUtilisateur info = session != null ? session.GetInfoUtilisateur() : null;
                if (info != null)
                    liste.Libelle = I.T("Liste created by @1 at @2|", info.NomUtilisateur,
                        DateTime.Now.ToString("d") + " " + DateTime.Now.ToString("t"));
                if (CFormCreerListeEntites.EditeListe(liste))
                {
                    int nNb = m_listView.CheckedItems.Count;
                    if (MessageBox.Show(I.T("Add @1 items to list '@2'|20013", nNb.ToString(), liste.Libelle),
                        I.T("Confirmation|125"),
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        return;
                    }
                    AddCheckedItemsToList(liste);
                    CResultAErreur result = ctx.SaveAll(true);
                    if (!result)
                        CFormAlerte.Afficher(result.Erreur);
                    else
                        FillListesDisponibles();
                }
            }



        }

        //-----------------------------------------------------------------------
        void itemAddToList_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            CListeEntites lst = item != null ? item.Tag as CListeEntites : null;
            if (lst != null)
            {
                int nNb = m_listView.CheckedItems.Count;
                if (MessageBox.Show(I.T("Add @1 items to list '@2'|20013", nNb.ToString(), lst.Libelle),
                    I.T("Confirmation|125"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
                using (CContexteDonnee ctx = new CContexteDonnee(ListeObjets.ContexteDonnee.IdSession, true, false))
                {
                    lst = lst.GetObjetInContexte(ctx) as CListeEntites;
                    AddCheckedItemsToList(lst);
                    CResultAErreur result = ctx.SaveAll(true);
                    if (!result)
                        CFormAlerte.Afficher(result.Erreur);
                    else
                        FillListesDisponibles();
                }
            }
        }

        //-----------------------------------------------------------------------
        private void AddCheckedItemsToList(CListeEntites liste)
        {
            foreach (object elt in m_listView.CheckedItems)
            {
                CObjetDonneeAIdNumerique obj = elt as CObjetDonneeAIdNumerique;
                if (obj != null)
                {
                    liste.AddElement(obj);
                }
            }
        }



        //-----------------------------------------------------------------------

        private void m_btnSaveList_MouseUp(object sender, MouseEventArgs e)
        {
            int nNb = m_listView.CheckedItems.Count;
            if (nNb == 0)
            {
                if ( e.Button == MouseButtons.Left )
                    MessageBox.Show(I.T("Check items to add first|20012"));
                else
                    MessageBox.Show(I.T("Check items to remove first|20014"));
                return;
            }
            ContextMenuStrip menu = new ContextMenuStrip();
            CListeObjetDonneeGenerique<CListeEntites> lstEntites = new CListeObjetDonneeGenerique<CListeEntites>(ListeObjets.ContexteDonnee);
            lstEntites.Filtre = new CFiltreData(CListeEntites.c_champTypeElements + "=@1",
                ListeObjets.TypeObjets.ToString());
            ToolStripMenuItem itemNewList = new ToolStripMenuItem(I.T("New list|20011"));
            itemNewList.Click += new EventHandler(itemNewList_Click);
            menu.Items.Add(itemNewList);
            menu.Items.Add(new ToolStripSeparator());
            foreach (CListeEntites liste in lstEntites)
            {
                if (liste.FiltreDynamique == null)
                {
                    ToolStripMenuItem itemList;
                    if ( e.Button == MouseButtons.Left )
                    {
                        itemList = new ToolStripMenuItem(
                            I.T("Add items to '@1'|20018", liste.Libelle));
                        itemList.Click += new EventHandler(itemAddToList_Click);
                    }
                    else
                    {
                        itemList = new ToolStripMenuItem(
                            I.T("Remove items from '@1'|20019", liste.Libelle ));
                        itemList.Image = sc2i.win32.data.navigation.Properties.Resources.delete;
                        itemList.Click += new EventHandler(itemRemoveFromList_Click);
                    }

                    itemList.Tag = liste;
                    
                    menu.Items.Add(itemList);
                }
            }
            menu.Show(m_btnSaveList, new Point(0, m_btnSaveList.Height));
        }

        //-----------------------------------------------------------------
        void itemRemoveFromList_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            CListeEntites lst = item != null ? item.Tag as CListeEntites : null;
            if (lst != null)
            {
                int nNb = m_listView.CheckedItems.Count;
                if (MessageBox.Show(I.T("Remove @1 items to list '@2'|20020", nNb.ToString(), lst.Libelle),
                    I.T("Confirmation|125"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
                using (CContexteDonnee ctx = new CContexteDonnee(ListeObjets.ContexteDonnee.IdSession, true, false))
                {
                    lst = lst.GetObjetInContexte(ctx) as CListeEntites;
                    foreach (object obj in m_listView.CheckedItems)
                    {
                        CObjetDonneeAIdNumerique od = obj as CObjetDonneeAIdNumerique;
                        if (od != null)
                            lst.RemoveElement(od);
                    }
                    CResultAErreur result = ctx.SaveAll(true);
                    if (!result)
                        CFormAlerte.Afficher(result.Erreur);
                    else
                        FillListesDisponibles();
                }
            }
        }

        private void m_btnNoListe_VisibleChanged(object sender, EventArgs e)
        {
            m_lnkEditListe.Visible = m_btnNoListe.Visible;
        }

        private void m_lnkEditListe_LinkClicked(object sender, EventArgs e)
        {
            if (m_listeEntitesEnCours != null)
            {
                IFormNavigable frm = FindForm() as IFormNavigable;
                if (frm != null && frm.Navigateur != null)
                    frm.Navigateur.EditeElement(m_listeEntitesEnCours, false, "");
            }
        }

    }
}
        