using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Drawing;
using System.Windows.Forms;

using sc2i.data;
using sc2i.common;
using sc2i.win32.common;
using sc2i.win32.navigation;
using sc2i.win32.data;
using sc2i.multitiers.client;
using sc2i.win32.data.dynamic;
using System.Data;
using sc2i.common.recherche;
using sc2i.win32.common.recherche;
using sc2i.expression;
using sc2i.data.dynamic;
using sc2i.data.dynamic.NommageEntite;
using sc2i.formulaire;
using sc2i.win32.data.dynamic.import;
using sc2i.win32.data.Package;



namespace sc2i.win32.data.navigation
{

	public delegate CResultAErreur EventOnPageHandler ( object page );
    public delegate void EventOnChangementDonnee ( string strNomChamp );

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Fonctionnement des droits :
    /// En fin de InitChamps, l'objet CGestionnaireReadOnlySysteme stocke
    /// l'état des contrôles et va suivre les demandes faites par le code
    /// sur ces contrôles
    /// On applique les restrictions (lockEdition et Enabled sont gerés).
    /// Si après application des restrictions, si le code tente de faire passe 
    /// un controle readonly par restriction en notreadonly, le gestionnaireReadonly
    /// empeche la modif (par l'évenement), par contre, il stocke la demande.
    /// Ainsi, quand on réapplique les restrictions, le gestionnaire readonly
    /// applique ce que le code avait demandé si le contrôle en question n'a
    /// plus la restriction readonly.
    /// Si on ajoute dynamiquement des contrôles, il faut penser à l'indiquer
    /// au gestionnaire readonly
    /// </remarks>
    /// 
	public class CFormEditionStandard : 
		CFormMaxiSansMenu, 
		IFormNavigable, 
		IFormEditObjetDonnee, 
		IElementAContexteUtilisation,
		IFormAContexteDonnee
	{
		/// <summary>
		/// Indique pour chaque page si elle est initialisée ou pas
		/// </summary>
		private Dictionary<object, bool> m_pagesInitialisees = new Dictionary<object, bool>();


		private string m_strContexteUtilisation = "";
		private bool m_bAjouterVisible = true;
		private bool m_bSupprimerVisible = true;
		private bool m_bSansEdition = false;

        /// <summary>
        /// restrictions en plus des restrictions utilisateur
        /// </summary>
        private Dictionary<string, CListeRestrictionsUtilisateurSurType> m_dicRestrictionsComplementaires = new Dictionary<string,CListeRestrictionsUtilisateurSurType>();
        //private CListeRestrictionsUtilisateurSurType m_restrictionsComplementaires = null;

		private bool m_bEtatEdition = true;
		private CObjetDonneeAIdNumeriqueAuto m_objetEdite;

		//Tables indiquant les champs liés à chaque controle
		private Hashtable m_tableControlToChampAssocie = new Hashtable();
		private Control m_controlEnCoursDeFiltre = null;

		private bool m_bConsultationOnly = false;

		private Crownwood.Magic.Controls.TabControl m_tabControl = null;

		private CGestionnaireExtendeurFormEditionStandard m_extendeurForm = null;

        private CGestionnaireReadOnlySysteme m_gestionnaireReadOnly = new CGestionnaireReadOnlySysteme();

        private List<CAffectationsProprietes> m_listeAffectationsPourNouvelElement = new List<CAffectationsProprietes>();

        //Exception de modification appliquée au contexte lorsque l'élément passe en édition
        private string m_strContexteModification = "";

		protected CContexteFormNavigable m_contexte;

		protected CListeObjetsDonnees m_listeObjets = null;
		protected IEnumeratorBiSens m_enumerator;
		protected System.Windows.Forms.Button m_btnAnnulerModifications;
		protected System.Windows.Forms.Button m_btnValiderModifications;
		protected System.Windows.Forms.Button m_btnSupprimerObjet;
		protected System.Windows.Forms.Button m_btnEditerObjet;
		private System.Windows.Forms.ToolTip m_tipFormEditionObjetDonnee;
		protected sc2i.win32.common.CExtModeEdition m_gestionnaireModeEdition;
		public sc2i.win32.common.CExtLinkField m_extLinkField;
		protected System.Windows.Forms.Panel m_panelNavigation;
        protected System.Windows.Forms.Label m_lblNbListes;
        protected System.Windows.Forms.Button m_btnPrecedent;
        protected System.Windows.Forms.Button m_btnSuivant;
		private System.Windows.Forms.ContextMenu m_menuFiltreDynamique;
		private System.Windows.Forms.MenuItem m_menuFiltreSur;
		private System.Windows.Forms.MenuItem m_menuFiltreSurSelection;
		private System.Windows.Forms.MenuItem m_menuFiltreHorsSelection;
		private System.Windows.Forms.MenuItem m_menuAnnulerFiltres;
        protected System.Windows.Forms.Button m_btnAjout;
        protected System.Windows.Forms.Label m_lblId;
        protected System.Windows.Forms.Panel m_panelCle;
		private System.Windows.Forms.ContextMenu m_menuNouveau;
		private System.Windows.Forms.MenuItem m_menuNouvelElement;
		private System.Windows.Forms.MenuItem m_menuCopie;
        private System.Windows.Forms.Label m_lblPageInterdite;
        protected Panel m_panelMenu;
		private System.Windows.Forms.Panel m_panelDataInvalide;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.PictureBox pictureBox3;
		private System.Windows.Forms.PictureBox pictureBox4;
		protected PictureBox m_btnHistorique;
        protected PictureBox m_imageCle;
        protected PictureBox m_btnChercherObjet;
        private PictureBox m_btnExtractList;
        private CTabControlFullScreenerPanel m_TabControlZoomer;
		private System.ComponentModel.IContainer components = null;
		//-------------------------------------------------------------------------
		public CFormEditionStandard()
		{
			// Cet appel est requis par le Concepteur Windows Form.
			InitializeComponent();

			m_extendeurForm = new CGestionnaireExtendeurFormEditionStandard(this);

			m_objetEdite = null;
		}
		//-------------------------------------------------------------------------
		public CFormEditionStandard( CObjetDonneeAIdNumeriqueAuto objet )
		{
			InitializeComponent();

			m_objetEdite = objet;
			m_extendeurForm = new CGestionnaireExtendeurFormEditionStandard(this);
		}

		//-------------------------------------------------------------------------
		public CFormEditionStandard(CObjetDonneeAIdNumeriqueAuto objet, CListeObjetsDonnees liste)
		{
			// Cet appel est requis par le Concepteur Windows Form.
			InitializeComponent();

			if (DesignMode)
				return;

			m_listeObjets = liste;
			m_objetEdite = objet;
			if (m_listeObjets != null)
			{

				m_enumerator = m_listeObjets.GetEnumeratorBiSens();

				m_enumerator.CurrentIndex = m_listeObjets.GetIndex(m_objetEdite);
			}

			m_extendeurForm = new CGestionnaireExtendeurFormEditionStandard(this);
		}

		//-------------------------------------------------------------------------
		public CObjetDonnee GetObjetEdite()
		{
			return m_objetEdite;
		}


        //-------------------------------------------------------------------------
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public IEnumerable<CAffectationsProprietes> AffectationsPourNouvelElement
        {
            get
            {
                return m_listeAffectationsPourNouvelElement.AsReadOnly();
            }
            set
            {
                m_listeAffectationsPourNouvelElement.Clear();
                if (value != null)
                    m_listeAffectationsPourNouvelElement.AddRange(value);
            }
        }

		//-------------------------------------------------------------------------
		public Crownwood.Magic.Controls.TabControl TabControl
		{
			get
			{
				return m_tabControl;
			}
			set
			{
				m_tabControl = value;
				if (m_tabControl != null)
				{
					m_tabControl.SelectionChanged += new EventHandler(m_tabControl_SelectionChanged);
				}
                if (value is C2iTabControl)
                    m_TabControlZoomer.TabControl = value as C2iTabControl;
			}
		}

        //-------------------------------------------------------------------------
        public void InvalidePage(Crownwood.Magic.Controls.TabPage page)
        {
            if (m_pagesInitialisees.ContainsKey(page))
                m_pagesInitialisees.Remove(page);
        }

		//-------------------------------------------------------------------------
		void  m_tabControl_SelectionChanged(object sender, EventArgs e)
		{
			if ( DesignMode )
				return;
			if ( m_tabControl == null )
				return;
			if (!m_pagesInitialisees.ContainsKey(m_tabControl.SelectedTab) ||
				!m_pagesInitialisees[m_tabControl.SelectedTab])
			{
				CResultAErreur result = InitPage(m_tabControl.SelectedTab);
				if (!result)
					CFormAlerte.Afficher(result);
			}
		}

		//---------------------------------------------------
		public CResultAErreur InitPage(object page)
		{
			CResultAErreur result = CResultAErreur.True;
			if (OnInitPage != null && page != null)
			{

				result = OnInitPage(page);
				if (result)
				{
					m_pagesInitialisees[page] = true;
				}
				else
				{
					m_pagesInitialisees[page] = false;
				}
			}
			return result;
		}

		//-------------------------------------------------------------------------
		/// <summary>
		/// Se déclenche pour lancer l'initialisation d'une page
		/// </summary>
		public event EventOnPageHandler OnInitPage;

		/// <summary>
		/// Se déclenche pour lancer la mise à jour d'une page
		/// </summary>
		public event EventOnPageHandler OnMajChampsPage;

		
		//-------------------------------------------------------------------------
		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormEditionStandard));
            this.m_panelMenu = new System.Windows.Forms.Panel();
            this.m_panelCle = new System.Windows.Forms.Panel();
            this.m_btnChercherObjet = new System.Windows.Forms.PictureBox();
            this.m_btnHistorique = new System.Windows.Forms.PictureBox();
            this.m_lblId = new System.Windows.Forms.Label();
            this.m_imageCle = new System.Windows.Forms.PictureBox();
            this.m_btnAjout = new System.Windows.Forms.Button();
            this.m_btnEditerObjet = new System.Windows.Forms.Button();
            this.m_btnSupprimerObjet = new System.Windows.Forms.Button();
            this.m_btnValiderModifications = new System.Windows.Forms.Button();
            this.m_btnAnnulerModifications = new System.Windows.Forms.Button();
            this.m_panelNavigation = new System.Windows.Forms.Panel();
            this.m_btnSuivant = new System.Windows.Forms.Button();
            this.m_lblNbListes = new System.Windows.Forms.Label();
            this.m_btnPrecedent = new System.Windows.Forms.Button();
            this.m_btnExtractList = new System.Windows.Forms.PictureBox();
            this.m_tipFormEditionObjetDonnee = new System.Windows.Forms.ToolTip(this.components);
            this.m_gestionnaireModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.m_lblPageInterdite = new System.Windows.Forms.Label();
            this.m_panelDataInvalide = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.m_TabControlZoomer = new sc2i.win32.common.CTabControlFullScreenerPanel();
            this.m_extLinkField = new sc2i.win32.common.CExtLinkField();
            this.m_menuFiltreDynamique = new System.Windows.Forms.ContextMenu();
            this.m_menuFiltreSur = new System.Windows.Forms.MenuItem();
            this.m_menuFiltreSurSelection = new System.Windows.Forms.MenuItem();
            this.m_menuFiltreHorsSelection = new System.Windows.Forms.MenuItem();
            this.m_menuAnnulerFiltres = new System.Windows.Forms.MenuItem();
            this.m_menuNouveau = new System.Windows.Forms.ContextMenu();
            this.m_menuNouvelElement = new System.Windows.Forms.MenuItem();
            this.m_menuCopie = new System.Windows.Forms.MenuItem();
            this.m_panelMenu.SuspendLayout();
            this.m_panelCle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnChercherObjet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnHistorique)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_imageCle)).BeginInit();
            this.m_panelNavigation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnExtractList)).BeginInit();
            this.m_panelDataInvalide.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // m_panelMenu
            // 
            this.m_panelMenu.BackColor = System.Drawing.Color.White;
            this.m_panelMenu.Controls.Add(this.m_panelCle);
            this.m_panelMenu.Controls.Add(this.m_btnAjout);
            this.m_panelMenu.Controls.Add(this.m_btnEditerObjet);
            this.m_panelMenu.Controls.Add(this.m_btnSupprimerObjet);
            this.m_panelMenu.Controls.Add(this.m_btnValiderModifications);
            this.m_panelMenu.Controls.Add(this.m_btnAnnulerModifications);
            this.m_panelMenu.Controls.Add(this.m_panelNavigation);
            this.m_panelMenu.Controls.Add(this.m_btnExtractList);
            this.m_panelMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_extLinkField.SetLinkField(this.m_panelMenu, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.m_panelMenu, false);
            this.m_panelMenu.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelMenu, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelMenu.Name = "m_panelMenu";
            this.m_panelMenu.Size = new System.Drawing.Size(720, 32);
            this.m_extStyle.SetStyleBackColor(this.m_panelMenu, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelMenu, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelMenu.TabIndex = 4000;
            // 
            // m_panelCle
            // 
            this.m_panelCle.Controls.Add(this.m_btnChercherObjet);
            this.m_panelCle.Controls.Add(this.m_btnHistorique);
            this.m_panelCle.Controls.Add(this.m_lblId);
            this.m_panelCle.Controls.Add(this.m_imageCle);
            this.m_panelCle.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_extLinkField.SetLinkField(this.m_panelCle, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.m_panelCle, false);
            this.m_panelCle.Location = new System.Drawing.Point(405, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelCle, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelCle.Name = "m_panelCle";
            this.m_panelCle.Size = new System.Drawing.Size(108, 32);
            this.m_extStyle.SetStyleBackColor(this.m_panelCle, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelCle, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelCle.TabIndex = 6;
            // 
            // m_btnChercherObjet
            // 
            this.m_btnChercherObjet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnChercherObjet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnChercherObjet.Image = global::sc2i.win32.data.navigation.Properties.Resources.search;
            this.m_extLinkField.SetLinkField(this.m_btnChercherObjet, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.m_btnChercherObjet, false);
            this.m_btnChercherObjet.Location = new System.Drawing.Point(2, 9);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnChercherObjet, sc2i.win32.common.TypeModeEdition.DisableSurEdition);
            this.m_btnChercherObjet.Name = "m_btnChercherObjet";
            this.m_btnChercherObjet.Size = new System.Drawing.Size(16, 16);
            this.m_btnChercherObjet.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.m_extStyle.SetStyleBackColor(this.m_btnChercherObjet, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnChercherObjet, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnChercherObjet.TabIndex = 4003;
            this.m_btnChercherObjet.TabStop = false;
            this.m_tipFormEditionObjetDonnee.SetToolTip(this.m_btnChercherObjet, "History");
            this.m_btnChercherObjet.Visible = false;
            this.m_btnChercherObjet.Click += new System.EventHandler(this.m_btnChercherObjet_Click);
            // 
            // m_btnHistorique
            // 
            this.m_btnHistorique.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnHistorique.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnHistorique.Image = global::sc2i.win32.data.navigation.Properties.Resources.histo_2;
            this.m_extLinkField.SetLinkField(this.m_btnHistorique, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.m_btnHistorique, false);
            this.m_btnHistorique.Location = new System.Drawing.Point(21, 6);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnHistorique, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnHistorique.Name = "m_btnHistorique";
            this.m_btnHistorique.Size = new System.Drawing.Size(22, 22);
            this.m_btnHistorique.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.m_extStyle.SetStyleBackColor(this.m_btnHistorique, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnHistorique, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnHistorique.TabIndex = 4002;
            this.m_btnHistorique.TabStop = false;
            this.m_tipFormEditionObjetDonnee.SetToolTip(this.m_btnHistorique, "History");
            this.m_btnHistorique.Click += new System.EventHandler(this.m_btnHistorique_Click);
            // 
            // m_lblId
            // 
            this.m_lblId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblId.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_extLinkField.SetLinkField(this.m_lblId, "Id");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.m_lblId, true);
            this.m_lblId.Location = new System.Drawing.Point(66, 9);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lblId, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblId.Name = "m_lblId";
            this.m_lblId.Size = new System.Drawing.Size(36, 16);
            this.m_extStyle.SetStyleBackColor(this.m_lblId, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_lblId, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lblId.TabIndex = 0;
            this.m_lblId.Text = "[Id]";
            this.m_lblId.Visible = false;
            // 
            // m_imageCle
            // 
            this.m_imageCle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_imageCle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_imageCle.Image = global::sc2i.win32.data.navigation.Properties.Resources.key1;
            this.m_extLinkField.SetLinkField(this.m_imageCle, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.m_imageCle, false);
            this.m_imageCle.Location = new System.Drawing.Point(44, 9);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_imageCle, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_imageCle.Name = "m_imageCle";
            this.m_imageCle.Size = new System.Drawing.Size(16, 16);
            this.m_imageCle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.m_extStyle.SetStyleBackColor(this.m_imageCle, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_imageCle, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_imageCle.TabIndex = 4001;
            this.m_imageCle.TabStop = false;
            this.m_tipFormEditionObjetDonnee.SetToolTip(this.m_imageCle, "Key");
            this.m_imageCle.Click += new System.EventHandler(this.m_imageCle_Click);
            this.m_imageCle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_imageCle_MouseDown);
            this.m_imageCle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_imageCle_MouseUp);
            // 
            // m_btnAjout
            // 
            this.m_btnAjout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnAjout.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAjout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnAjout.ForeColor = System.Drawing.Color.White;
            this.m_btnAjout.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAjout.Image")));
            this.m_extLinkField.SetLinkField(this.m_btnAjout, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.m_btnAjout, false);
            this.m_btnAjout.Location = new System.Drawing.Point(160, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnAjout, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnAjout.Name = "m_btnAjout";
            this.m_btnAjout.Size = new System.Drawing.Size(32, 32);
            this.m_extStyle.SetStyleBackColor(this.m_btnAjout, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnAjout, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAjout.TabIndex = 5;
            this.m_btnAjout.TabStop = false;
            this.m_tipFormEditionObjetDonnee.SetToolTip(this.m_btnAjout, "Add a new element (F4, Ctrl-F4 to clone this element)");
            this.m_btnAjout.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_btnAjout_MouseUp);
            // 
            // m_btnEditerObjet
            // 
            this.m_btnEditerObjet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnEditerObjet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnEditerObjet.ForeColor = System.Drawing.Color.White;
            this.m_btnEditerObjet.Image = global::sc2i.win32.data.navigation.Properties.Resources.edit;
            this.m_extLinkField.SetLinkField(this.m_btnEditerObjet, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.m_btnEditerObjet, false);
            this.m_btnEditerObjet.Location = new System.Drawing.Point(0, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnEditerObjet, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnEditerObjet.Name = "m_btnEditerObjet";
            this.m_btnEditerObjet.Size = new System.Drawing.Size(32, 32);
            this.m_extStyle.SetStyleBackColor(this.m_btnEditerObjet, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnEditerObjet, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnEditerObjet.TabIndex = 0;
            this.m_btnEditerObjet.TabStop = false;
            this.m_tipFormEditionObjetDonnee.SetToolTip(this.m_btnEditerObjet, "Modify this element (F8)");
            this.m_btnEditerObjet.Click += new System.EventHandler(this.m_btnEditerObjet_Click);
            // 
            // m_btnSupprimerObjet
            // 
            this.m_btnSupprimerObjet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnSupprimerObjet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnSupprimerObjet.ForeColor = System.Drawing.Color.White;
            this.m_btnSupprimerObjet.Image = ((System.Drawing.Image)(resources.GetObject("m_btnSupprimerObjet.Image")));
            this.m_extLinkField.SetLinkField(this.m_btnSupprimerObjet, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.m_btnSupprimerObjet, false);
            this.m_btnSupprimerObjet.Location = new System.Drawing.Point(40, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnSupprimerObjet, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnSupprimerObjet.Name = "m_btnSupprimerObjet";
            this.m_btnSupprimerObjet.Size = new System.Drawing.Size(32, 32);
            this.m_extStyle.SetStyleBackColor(this.m_btnSupprimerObjet, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnSupprimerObjet, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnSupprimerObjet.TabIndex = 1;
            this.m_btnSupprimerObjet.TabStop = false;
            this.m_tipFormEditionObjetDonnee.SetToolTip(this.m_btnSupprimerObjet, "Delete this element");
            this.m_btnSupprimerObjet.Click += new System.EventHandler(this.m_btnSupprimerObjet_Click);
            // 
            // m_btnValiderModifications
            // 
            this.m_btnValiderModifications.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnValiderModifications.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnValiderModifications.ForeColor = System.Drawing.Color.White;
            this.m_btnValiderModifications.Image = ((System.Drawing.Image)(resources.GetObject("m_btnValiderModifications.Image")));
            this.m_extLinkField.SetLinkField(this.m_btnValiderModifications, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.m_btnValiderModifications, false);
            this.m_btnValiderModifications.Location = new System.Drawing.Point(80, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnValiderModifications, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnValiderModifications.Name = "m_btnValiderModifications";
            this.m_btnValiderModifications.Size = new System.Drawing.Size(32, 32);
            this.m_extStyle.SetStyleBackColor(this.m_btnValiderModifications, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnValiderModifications, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnValiderModifications.TabIndex = 2;
            this.m_btnValiderModifications.TabStop = false;
            this.m_tipFormEditionObjetDonnee.SetToolTip(this.m_btnValiderModifications, "Accept modifications (F10)");
            this.m_btnValiderModifications.Click += new System.EventHandler(this.m_btnValiderModifications_Click);
            // 
            // m_btnAnnulerModifications
            // 
            this.m_btnAnnulerModifications.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnAnnulerModifications.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAnnulerModifications.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnAnnulerModifications.ForeColor = System.Drawing.Color.White;
            this.m_btnAnnulerModifications.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAnnulerModifications.Image")));
            this.m_extLinkField.SetLinkField(this.m_btnAnnulerModifications, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.m_btnAnnulerModifications, false);
            this.m_btnAnnulerModifications.Location = new System.Drawing.Point(120, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnAnnulerModifications, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnAnnulerModifications.Name = "m_btnAnnulerModifications";
            this.m_btnAnnulerModifications.Size = new System.Drawing.Size(32, 32);
            this.m_extStyle.SetStyleBackColor(this.m_btnAnnulerModifications, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnAnnulerModifications, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnAnnulerModifications.TabIndex = 3;
            this.m_btnAnnulerModifications.TabStop = false;
            this.m_tipFormEditionObjetDonnee.SetToolTip(this.m_btnAnnulerModifications, "Cancel modifications");
            this.m_btnAnnulerModifications.Click += new System.EventHandler(this.m_btnAnnulerModifications_Click);
            // 
            // m_panelNavigation
            // 
            this.m_panelNavigation.BackColor = System.Drawing.Color.White;
            this.m_panelNavigation.Controls.Add(this.m_btnSuivant);
            this.m_panelNavigation.Controls.Add(this.m_lblNbListes);
            this.m_panelNavigation.Controls.Add(this.m_btnPrecedent);
            this.m_panelNavigation.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_extLinkField.SetLinkField(this.m_panelNavigation, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.m_panelNavigation, false);
            this.m_panelNavigation.Location = new System.Drawing.Point(513, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelNavigation, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelNavigation.Name = "m_panelNavigation";
            this.m_panelNavigation.Size = new System.Drawing.Size(175, 32);
            this.m_extStyle.SetStyleBackColor(this.m_panelNavigation, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelNavigation, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelNavigation.TabIndex = 4;
            // 
            // m_btnSuivant
            // 
            this.m_btnSuivant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnSuivant.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnSuivant.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnSuivant.ForeColor = System.Drawing.Color.White;
            this.m_btnSuivant.Image = ((System.Drawing.Image)(resources.GetObject("m_btnSuivant.Image")));
            this.m_extLinkField.SetLinkField(this.m_btnSuivant, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.m_btnSuivant, false);
            this.m_btnSuivant.Location = new System.Drawing.Point(35, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnSuivant, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnSuivant.Name = "m_btnSuivant";
            this.m_btnSuivant.Size = new System.Drawing.Size(32, 32);
            this.m_extStyle.SetStyleBackColor(this.m_btnSuivant, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnSuivant, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnSuivant.TabIndex = 1;
            this.m_btnSuivant.TabStop = false;
            this.m_tipFormEditionObjetDonnee.SetToolTip(this.m_btnSuivant, "Next element");
            this.m_btnSuivant.Click += new System.EventHandler(this.m_btnSuivant_Click);
            // 
            // m_lblNbListes
            // 
            this.m_lblNbListes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblNbListes.BackColor = System.Drawing.Color.Transparent;
            this.m_lblNbListes.ForeColor = System.Drawing.Color.Black;
            this.m_extLinkField.SetLinkField(this.m_lblNbListes, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.m_lblNbListes, false);
            this.m_lblNbListes.Location = new System.Drawing.Point(67, 10);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lblNbListes, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblNbListes.Name = "m_lblNbListes";
            this.m_lblNbListes.Size = new System.Drawing.Size(56, 13);
            this.m_extStyle.SetStyleBackColor(this.m_lblNbListes, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_lblNbListes, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lblNbListes.TabIndex = 2;
            this.m_lblNbListes.Text = "Nb / Nb";
            this.m_lblNbListes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_btnPrecedent
            // 
            this.m_btnPrecedent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnPrecedent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnPrecedent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnPrecedent.ForeColor = System.Drawing.Color.White;
            this.m_btnPrecedent.Image = ((System.Drawing.Image)(resources.GetObject("m_btnPrecedent.Image")));
            this.m_extLinkField.SetLinkField(this.m_btnPrecedent, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.m_btnPrecedent, false);
            this.m_btnPrecedent.Location = new System.Drawing.Point(3, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnPrecedent, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnPrecedent.Name = "m_btnPrecedent";
            this.m_btnPrecedent.Size = new System.Drawing.Size(32, 32);
            this.m_extStyle.SetStyleBackColor(this.m_btnPrecedent, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnPrecedent, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnPrecedent.TabIndex = 0;
            this.m_btnPrecedent.TabStop = false;
            this.m_tipFormEditionObjetDonnee.SetToolTip(this.m_btnPrecedent, "Previous element");
            this.m_btnPrecedent.Click += new System.EventHandler(this.m_btnPrecedent_Click);
            // 
            // m_btnExtractList
            // 
            this.m_btnExtractList.BackColor = System.Drawing.Color.Transparent;
            this.m_btnExtractList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnExtractList.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_btnExtractList.Image = global::sc2i.win32.data.navigation.Properties.Resources.Extract_List;
            this.m_extLinkField.SetLinkField(this.m_btnExtractList, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.m_btnExtractList, false);
            this.m_btnExtractList.Location = new System.Drawing.Point(688, 0);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_btnExtractList, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_btnExtractList.Name = "m_btnExtractList";
            this.m_btnExtractList.Size = new System.Drawing.Size(32, 32);
            this.m_btnExtractList.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.m_extStyle.SetStyleBackColor(this.m_btnExtractList, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_btnExtractList, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_btnExtractList.TabIndex = 16;
            this.m_btnExtractList.TabStop = false;
            this.m_btnExtractList.Click += new System.EventHandler(this.m_btnExtractList_Click);
            // 
            // m_gestionnaireModeEdition
            // 
            this.m_gestionnaireModeEdition.ModeEditionChanged += new System.EventHandler(this.m_gestionnaireModeEdition_ModeEditionChanged);
            // 
            // m_lblPageInterdite
            // 
            this.m_lblPageInterdite.BackColor = System.Drawing.Color.White;
            this.m_lblPageInterdite.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_lblPageInterdite.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_extLinkField.SetLinkField(this.m_lblPageInterdite, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.m_lblPageInterdite, false);
            this.m_lblPageInterdite.Location = new System.Drawing.Point(140, 84);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_lblPageInterdite, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_lblPageInterdite.Name = "m_lblPageInterdite";
            this.m_lblPageInterdite.Size = new System.Drawing.Size(256, 32);
            this.m_extStyle.SetStyleBackColor(this.m_lblPageInterdite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_lblPageInterdite, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_lblPageInterdite.TabIndex = 4001;
            this.m_lblPageInterdite.Text = "You do not have the rights to display this page|30024";
            this.m_lblPageInterdite.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m_lblPageInterdite.Visible = false;
            // 
            // m_panelDataInvalide
            // 
            this.m_panelDataInvalide.Controls.Add(this.label1);
            this.m_panelDataInvalide.Controls.Add(this.pictureBox4);
            this.m_panelDataInvalide.Controls.Add(this.pictureBox3);
            this.m_panelDataInvalide.Controls.Add(this.pictureBox2);
            this.m_panelDataInvalide.Controls.Add(this.pictureBox1);
            this.m_extLinkField.SetLinkField(this.m_panelDataInvalide, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.m_panelDataInvalide, false);
            this.m_panelDataInvalide.Location = new System.Drawing.Point(48, 40);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_panelDataInvalide, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_panelDataInvalide.Name = "m_panelDataInvalide";
            this.m_panelDataInvalide.Size = new System.Drawing.Size(407, 158);
            this.m_extStyle.SetStyleBackColor(this.m_panelDataInvalide, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelDataInvalide, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelDataInvalide.TabIndex = 4002;
            this.m_panelDataInvalide.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_extLinkField.SetLinkField(this.label1, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.label1, false);
            this.label1.Location = new System.Drawing.Point(47, 47);
            this.m_gestionnaireModeEdition.SetModeEdition(this.label1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(320, 72);
            this.m_extStyle.SetStyleBackColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.label1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.label1.TabIndex = 4002;
            this.label1.Text = "Data from this page are not available|101";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Visible = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.m_extLinkField.SetLinkField(this.pictureBox4, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.pictureBox4, false);
            this.pictureBox4.Location = new System.Drawing.Point(367, 39);
            this.m_gestionnaireModeEdition.SetModeEdition(this.pictureBox4, sc2i.win32.common.TypeModeEdition.Autonome);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(16, 16);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.m_extStyle.SetStyleBackColor(this.pictureBox4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.pictureBox4, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.pictureBox4.TabIndex = 4006;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.m_extLinkField.SetLinkField(this.pictureBox3, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.pictureBox3, false);
            this.pictureBox3.Location = new System.Drawing.Point(367, 111);
            this.m_gestionnaireModeEdition.SetModeEdition(this.pictureBox3, sc2i.win32.common.TypeModeEdition.Autonome);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(16, 16);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.m_extStyle.SetStyleBackColor(this.pictureBox3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.pictureBox3, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.pictureBox3.TabIndex = 4005;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.m_extLinkField.SetLinkField(this.pictureBox2, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.pictureBox2, false);
            this.pictureBox2.Location = new System.Drawing.Point(31, 111);
            this.m_gestionnaireModeEdition.SetModeEdition(this.pictureBox2, sc2i.win32.common.TypeModeEdition.Autonome);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.m_extStyle.SetStyleBackColor(this.pictureBox2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.pictureBox2, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.pictureBox2.TabIndex = 4004;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.m_extLinkField.SetLinkField(this.pictureBox1, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.pictureBox1, false);
            this.pictureBox1.Location = new System.Drawing.Point(27, 31);
            this.m_gestionnaireModeEdition.SetModeEdition(this.pictureBox1, sc2i.win32.common.TypeModeEdition.Autonome);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.m_extStyle.SetStyleBackColor(this.pictureBox1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.pictureBox1, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.pictureBox1.TabIndex = 4003;
            this.pictureBox1.TabStop = false;
            // 
            // m_TabControlZoomer
            // 
            this.m_TabControlZoomer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_extLinkField.SetLinkField(this.m_TabControlZoomer, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this.m_TabControlZoomer, false);
            this.m_TabControlZoomer.Location = new System.Drawing.Point(2, 33);
            this.m_gestionnaireModeEdition.SetModeEdition(this.m_TabControlZoomer, sc2i.win32.common.TypeModeEdition.Autonome);
            this.m_TabControlZoomer.Name = "m_TabControlZoomer";
            this.m_TabControlZoomer.Size = new System.Drawing.Size(718, 268);
            this.m_extStyle.SetStyleBackColor(this.m_TabControlZoomer, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_TabControlZoomer, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_TabControlZoomer.TabControl = null;
            this.m_TabControlZoomer.TabIndex = 4003;
            this.m_TabControlZoomer.TitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.m_TabControlZoomer.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_TabControlZoomer.TitleForeColor = System.Drawing.Color.White;
            this.m_TabControlZoomer.Visible = false;
            this.m_TabControlZoomer.VisibleChanged += new System.EventHandler(this.m_TabControlZoomer_VisibleChanged);
            // 
            // m_extLinkField
            // 
            this.m_extLinkField.SourceTypeString = "";
            // 
            // m_menuFiltreDynamique
            // 
            this.m_menuFiltreDynamique.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuFiltreSur,
            this.m_menuFiltreSurSelection,
            this.m_menuFiltreHorsSelection,
            this.m_menuAnnulerFiltres});
            // 
            // m_menuFiltreSur
            // 
            this.m_menuFiltreSur.Index = 0;
            this.m_menuFiltreSur.Text = "Filter on ...|30025";
            this.m_menuFiltreSur.Click += new System.EventHandler(this.m_menuRechercher_Click);
            // 
            // m_menuFiltreSurSelection
            // 
            this.m_menuFiltreSurSelection.Index = 1;
            this.m_menuFiltreSurSelection.Text = "Filter on selection|30026";
            // 
            // m_menuFiltreHorsSelection
            // 
            this.m_menuFiltreHorsSelection.Index = 2;
            this.m_menuFiltreHorsSelection.Text = "Filter out of selection|30027";
            // 
            // m_menuAnnulerFiltres
            // 
            this.m_menuAnnulerFiltres.Index = 3;
            this.m_menuAnnulerFiltres.Text = "Cancel filter|30028";
            this.m_menuAnnulerFiltres.Click += new System.EventHandler(this.m_menuAnnulerFiltre_Click);
            // 
            // m_menuNouveau
            // 
            this.m_menuNouveau.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuNouvelElement,
            this.m_menuCopie});
            // 
            // m_menuNouvelElement
            // 
            this.m_menuNouvelElement.Index = 0;
            this.m_menuNouvelElement.Text = "Create a new element|30029";
            this.m_menuNouvelElement.Click += new System.EventHandler(this.m_menuNouvelElement_Click);
            // 
            // m_menuCopie
            // 
            this.m_menuCopie.Index = 1;
            this.m_menuCopie.Text = "Create a copy|30030";
            this.m_menuCopie.Click += new System.EventHandler(this.m_menuCopie_Click);
            // 
            // CFormEditionStandard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(720, 300);
            this.Controls.Add(this.m_panelMenu);
            this.Controls.Add(this.m_panelDataInvalide);
            this.Controls.Add(this.m_lblPageInterdite);
            this.Controls.Add(this.m_TabControlZoomer);
            this.KeyPreview = true;
            this.m_extLinkField.SetLinkField(this, "");
            this.m_extLinkField.SetLinkFieldAutoUpdate(this, false);
            this.m_gestionnaireModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.Name = "CFormEditionStandard";
            this.m_extStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CFormEditionStandard_FormClosing);
            this.Load += new System.EventHandler(this.CFormEditionStandard_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CFormEditionStandard_KeyDown);
            this.m_panelMenu.ResumeLayout(false);
            this.m_panelCle.ResumeLayout(false);
            this.m_panelCle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnChercherObjet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_btnHistorique)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_imageCle)).EndInit();
            this.m_panelNavigation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_btnExtractList)).EndInit();
            this.m_panelDataInvalide.ResumeLayout(false);
            this.m_panelDataInvalide.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion
		//-------------------------------------------------------------------------
		// Events Handler Definition
		public event ObjetDonneeCancelEventHandler BeforeSuppression;
		public event ObjetDonneeEventHandler AfterSuppression;

		public event ObjetDonneeCancelEventHandler BeforePassageEnEdition;
		public event ObjetDonneeEventHandler AfterPassageEnEdition;

		public event ObjetDonneeCancelEventHandler BeforeValideModification;
		public event ObjetDonneeEventHandler AfterValideModification;

		public event ObjetDonneeCancelEventHandler BeforeAnnulationModification;
		public event ObjetDonneeEventHandler AfterAnnulationModification;

		public event ResultEventHandler OnMajChamps;
		public event ResultEventHandler OnInitChamps;
		//-------------------------------------------------------------------------
		private void UpdateBoutonsEdition()
		{
			m_btnEditerObjet.Enabled = !EtatEdition;
			m_btnSupprimerObjet.Enabled = !EtatEdition;
			m_btnValiderModifications.Enabled = EtatEdition;
			m_btnAnnulerModifications.Enabled = EtatEdition;

			m_gestionnaireModeEdition.ModeEdition = EtatEdition;
		}

		//-------------------------------------------------------------------------
		public bool ModeEdition
		{
			get
			{
				return m_gestionnaireModeEdition.ModeEdition;
			}
			set
			{
				m_gestionnaireModeEdition.ModeEdition = value;
			}
		}

        //-------------------------------------------------------------------------
        /// <summary>
        /// Indique le contexte de modification (exception de restriction) qui 
        /// sera passée au contexte lorsque l'objet passe en modification.
        /// </summary>
        public string ContexteModification
        {
            get{
                return m_strContexteModification;
            }
            set{m_strContexteModification = value;
            }
        }

		//-------------------------------------------------------------------------
		public void OnClickModifier()
		{
			OnClickModifier (false);
		}
		//-------------------------------------------------------------------------
		public void OnClickModifier( bool bSansBeginEdit)
		{
			if ( m_bConsultationOnly || m_bSansEdition || !m_btnEditerObjet.Visible )
			{
				CFormAlerte.Afficher(I.T("Unauthorized function|30001"), EFormAlerteType.Erreur);
				return;
			}
			CObjetDonneeCancelEventArgs cancelArgs = new CObjetDonneeCancelEventArgs(m_objetEdite);
			//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			if (BeforePassageEnEdition != null)
			{
				BeforePassageEnEdition ( this, cancelArgs);
				if ( cancelArgs.Cancel )
					return;
			}
			//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            if (!ObjetEdite.IsNewInThisContexte() && !bSansBeginEdit)
            {
                m_objetEdite.BeginEdit();
                if (ContexteModification.Length > 0)
                {
                    m_objetEdite.ContexteDonnee.ContexteModification = ContexteModification;
                    CContexteDonnee.ChangeRowSansDetectionModification(
                        m_objetEdite.Row, CObjetDonnee.c_champContexteModification, ContexteModification);
                }
            }
			m_bEtatEdition = true;
			UpdateBoutonsEdition();
			UpdateControlesOnEtatEdition();
			if ( AfterPassageEnEdition != null )
				AfterPassageEnEdition ( this, new CObjetDonneeCancelEventArgs(m_objetEdite) );

			//Appel de init champs pour que tous les éléments passent en édition
			CResultAErreur result = CResultAErreur.True;
			
			Control ctrlMinTabValue = GetMinTabValueControl(this.Controls);
			if (ctrlMinTabValue!=null)
				ctrlMinTabValue.Focus();

			try
			{
				result = InitChamps();
			}
			catch ( Exception e )
			{
				result.EmpileErreur( new CErreurException(e));
				CFormAlerte.Afficher ( e.ToString()+"\r\n"+e.StackTrace.ToString() , EFormAlerteType.Erreur);
			}
			if ( !result )
				AffichageDataInvalides();
			else
				MasqueDataInvalides();

			

			return;
		}
		//-------------------------------------------------------------------------
		private Control GetMinTabValueControl(System.Windows.Forms.Control.ControlCollection controls)
		{
			int nMinValue = -1;
			Control tempCtrl = null;
			foreach(Control ctrl in controls)
			{
				if ((ctrl.CanFocus && ctrl.CanSelect) || ctrl.Controls.Count>0)
				{
					if (nMinValue == -1 || ctrl.TabIndex < nMinValue)
					{
						nMinValue = ctrl.TabIndex;
						tempCtrl = ctrl;
					}
				}
			}
			if (tempCtrl == null)
				return null;
			if (tempCtrl.Controls.Count <1)
				return tempCtrl;
			else
				return GetMinTabValueControl(tempCtrl.Controls);
		}
		//-------------------------------------------------------------------------
		public void OnClickSupprimer()
		{
			if ( m_bConsultationOnly || !m_btnSupprimerObjet.Visible)
			{
				CFormAlerte.Afficher(I.T("Unauthorized function|30001"), EFormAlerteType.Erreur);
				return;
			}
			CObjetDonneeCancelEventArgs cancelArgs = new CObjetDonneeCancelEventArgs(m_objetEdite);
			//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			DialogResult reponse = CFormAlerte.Afficher(I.T(" Do you want to remove defintively  @1 ?|30002", m_objetEdite.DescriptionElement),
				EFormAlerteType.Question);

			//reponse = MessageBox.Show(
			//    CSc2iWin32DataNavigation.Navigateur, 
			//    " Voulez-vous supprimer définitivement " + m_objetEdite.DescriptionElement + " ?",
			//    "Confirmation", 
			//    MessageBoxButtons.YesNo, 
			//    MessageBoxIcon.Question, 
			//    MessageBoxDefaultButton.Button2);

			if (reponse == DialogResult.No)
				return;

			if (BeforeSuppression != null)
			{
				BeforeSuppression ( this, cancelArgs);
				if ( cancelArgs.Cancel )
					return;
			}
			//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			CResultAErreur result = m_objetEdite.Delete();
			if ( !result )
			{
				CFormAlerte.Afficher( result);
				return;
			}
			//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			if ( AfterSuppression != null )
				AfterSuppression ( this, new CObjetDonneeCancelEventArgs(m_objetEdite) );

			if (CSc2iWin32DataNavigation.Navigateur!=null)
				if (CSc2iWin32DataNavigation.Navigateur.Visible)
					CSc2iWin32DataNavigation.Navigateur.AffichePagePrecedenteSansHistorisation();

			return;
		}
		//-------------------------------------------------------------------------
		public virtual bool OnClickValider()
		{
			return ValidationElement(ObjetEdite);
		}
		//-------------------------------------------------------------------------
		public Control ControlAjout
		{
			get
			{
				return m_btnAjout;
			}
		}
		//-------------------------------------------------------------------------
		public virtual CObjetDonneeAIdNumeriqueAuto NewObject()
		{
			CObjetDonneeAIdNumeriqueAuto objet = (CObjetDonneeAIdNumeriqueAuto) Activator.CreateInstance( m_objetEdite.GetType() , new object[] {CSc2iWin32DataClient.ContexteCourant} );
			objet.CreateNew();
			if ( m_listeObjets != null && m_listeObjets is CListeObjetsDonneesContenus )
			{
				CObjetDonnee container = ((CListeObjetsDonneesContenus)m_listeObjets).ObjetConteneur;
				if ( container != null )
				{
					//Tente de trouver la propriété qui renvoie sur cet objet dans la classe fille
					Type tp = m_listeObjets.TypeObjets;
					foreach ( PropertyInfo prop in tp.GetProperties() )
					{
						if ( prop.PropertyType == container.GetType() )
						{
							object[] attribs = prop.GetCustomAttributes ( typeof(RelationAttribute), true );
							if ( attribs.Length != 0 )
							{
								RelationAttribute relAttrib = (RelationAttribute)attribs[0];
								if ( relAttrib.TableMere == container.GetNomTable() )
								{
									MethodInfo method = prop.GetSetMethod();
									if ( method != null )
									{
										try
										{
											method.Invoke ( objet, new object[]{container} );
											break;
										}
										catch
										{
										}
									}
								}
							}
						}
					}
				}
			}
			return objet;
		}
		//-------------------------------------------------------------------------
        private bool m_bCloneAutorise = true;
		public virtual void OnClickAjouter( bool bClone )
		{
			if ( m_bConsultationOnly || !m_btnAjout.Visible )
			{
				CFormAlerte.Afficher(I.T("Unauthorized function|30001"), EFormAlerteType.Erreur);
				return;
			}
			if ( EtatEdition )
			{
				if ( !OnClickValider() )
					return;
			}
			if (!QueryClose())
				return;
            CAffectationsProprietes affectations = null;
            if (!bClone)
            {
                if (m_listeAffectationsPourNouvelElement.Count == 1)
                    affectations = m_listeAffectationsPourNouvelElement[0];
                if (m_listeAffectationsPourNouvelElement.Count > 1)
                {
                    CMenuModal menu = new CMenuModal();
                    ToolStripMenuItem item = null;
                    foreach (CAffectationsProprietes affectation in m_listeAffectationsPourNouvelElement)
                    {
                        item = new ToolStripMenuItem(affectation.Libelle);
                        item.Tag = affectation;
                        menu.Items.Add(item);
                    }
                    Point pt = m_btnAjout.PointToScreen(new Point(0, m_btnAjout.Height));
                    item = menu.ShowMenu(pt) as ToolStripMenuItem;
                    if (item == null)
                        return;
                    affectations = item.Tag as CAffectationsProprietes;
                }
            }
			CObjetDonneeAIdNumeriqueAuto objet;
			if ( bClone && ObjetEdite != null )
			{
				try
				{
                    if(m_bCloneAutorise)
					    objet = (CObjetDonneeAIdNumeriqueAuto)ObjetEdite.Clone(true);
                    else
                    {
                        CFormAlerte.Afficher(I.T("Impossible to copy this object|30003"), EFormAlerteType.Erreur);
                        return;
                    }

				}
				catch
				{
					CFormAlerte.Afficher(I.T("Impossible to copy this object|30003"), EFormAlerteType.Erreur);
					return;
				}
			}
			else
				objet= NewObject();
            if (affectations != null)
            {
                CResultAErreur result = affectations.AffecteProprietes(objet, null, new CFournisseurPropDynStd());
                if (!result)
                {
                    CFormAlerte.Afficher(result.Erreur);
                }
            }
			ObjetEdite = objet;
			UpdateForm(true);
			this.OnClickModifier();
		}

		//-------------------------------------------------------------------------
		public void OnClickAjouter()
		{
			OnClickAjouter ( false );
		}
		//-------------------------------------------------------------------------
		public void OnClickAnnuler()
		{
			CObjetDonneeCancelEventArgs cancelArgs = new CObjetDonneeCancelEventArgs(m_objetEdite);
			//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			if (BeforeAnnulationModification != null)
			{
				BeforeAnnulationModification ( this, cancelArgs);
				if ( cancelArgs.Cancel )
					return;
			}
			//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

			if ( ObjetEdite.IsNewInThisContexte() )
			{
                if (AfterAnnulationModification != null)
                    AfterAnnulationModification(this, new CObjetDonneeCancelEventArgs(m_objetEdite));
				m_objetEdite.CancelCreate();
				m_objetEdite = null;
				m_bEtatEdition = false;
				if (this.Navigateur!=null)
					if (this.Navigateur.Visible)
						CSc2iWin32DataNavigation.Navigateur.AffichePagePrecedenteSansHistorisation();

				return;
			}
			else
				m_objetEdite.CancelEdit();
			//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			m_bEtatEdition = false;
			UpdateBoutonsEdition();
			UpdateControlesOnEtatEdition();
			if ( AfterAnnulationModification != null )
				AfterAnnulationModification ( this, new CObjetDonneeCancelEventArgs(m_objetEdite) );

			if ( m_objetEdite != null )
			{
				if ( !InitChamps() )
					AffichageDataInvalides();
				else
					MasqueDataInvalides();
			}
			return;
		}
		//-------------------------------------------------------------------------
        private bool m_bIsLoaded = false;
        private void CFormEditionStandard_Load(object sender, System.EventArgs e)
		{
            //La traduction est assurée par CFormMaxiSansMenu
            m_bIsLoaded = true;
            
            if (DesignMode)
				return;
			
			m_bEtatEdition = ObjetEdite.IsNewInThisContexte();

			if ( m_bEtatEdition )
				OnClickModifier();
			UpdateBoutonsEdition();

			UpdateForm(!ObjetEdite.IsNewInThisContexte()); //Cette fonction appelle InitChamps();

			//Ajoute les fonctions de filtre dynamique
			m_tableControlToChampAssocie.Clear();
			InitControlesFiltrables( this );
            CCustomiseurControlesSysteme.AppliqueToForm(this);
		}

		//-------------------------------------------------------------------------
		//gère les contrôles auquel il est possible d'affecter un filtre direct
		private void InitControlesFiltrables( Control controlParent )
		{
			if ( ObjetEdite == null )
				return;
			foreach ( Control fils in controlParent.Controls )
			{
				string strProp = m_extLinkField.GetLinkField(fils);
				if ( strProp != null && strProp.IndexOf('.') < 0)
				{
					//Regarde si la propriété est associée à un champ
					MemberInfo[] info = ObjetEdite.GetType().GetMember(strProp);
					if ( info != null && info.Length==1  )
					{
						object[] attribs = info[0].GetCustomAttributes(typeof(TableFieldPropertyAttribute), true);
						if ( attribs.Length == 1 )
						{
							m_tableControlToChampAssocie[fils] = ((TableFieldPropertyAttribute)attribs[0]).NomChamp;
							fils.MouseUp += new MouseEventHandler(OnMouseUpSurControlFiltrable);
						}
					}
				}
				InitControlesFiltrables ( fils );
			}
		}
		//-------------------------------------------------------------------------
		public void OnMouseUpSurControlFiltrable ( object sender, MouseEventArgs args )
		{
			if ( args.Button == MouseButtons.Right && sender is Control && !m_bEtatEdition)
			{
				string strField = (string)m_tableControlToChampAssocie[sender];
				if ( strField == null )
					return;
				m_controlEnCoursDeFiltre = (Control)sender;
				Point pt = new Point(0,m_controlEnCoursDeFiltre.Height);
				pt = m_controlEnCoursDeFiltre.PointToScreen(pt);
				pt = PointToClient(pt);
				m_menuFiltreDynamique.Show ( this, pt );
			}
		}
		
		//-------------------------------------------------------------------------
		private void m_menuRechercher_Click(object sender, System.EventArgs e)
		{
			if ( m_listeObjets == null )
				return;
			if ( m_controlEnCoursDeFiltre == null )
				return;
			string strChamp = (string)m_tableControlToChampAssocie[m_controlEnCoursDeFiltre];
			if ( strChamp == null )
				return;
			Point position = new Point(0,m_controlEnCoursDeFiltre.Height);
			position = m_controlEnCoursDeFiltre.PointToScreen(position);
			string strFiltre = CFormRechercheRapide.GetTexteFiltre(position);
			if ( strFiltre == null )
				return;
			CFiltreData filtre = new CFiltreData(strChamp+" like @1");
			filtre.Parametres.Add ( "%"+strFiltre+"%" );

			/*CFiltreData oldFiltre = m_listeObjets.Filtre;

			CFiltreData newFiltre = CFiltreData.GetAndFiltre ( oldFiltre, filtre );*/
			CFiltreData newFiltre = filtre;

			AppliqueFiltreToListe( newFiltre );

			
		}

		/// <summary>
		/// Applique un filtre à la liste d'objets et affiche le
		/// premier élement de la liste
		/// </summary>
		/// <param name="filtre"></param>
		protected void AppliqueFiltreToListe ( CFiltreData filtre )
		{
			CFiltreData oldFiltre = m_listeObjets.Filtre;
			m_listeObjets.Filtre = filtre;
			if ( m_listeObjets.Count == 0 )
			{
				m_listeObjets.Filtre = oldFiltre;
				CFormAlerte.Afficher(I.T("No corresponding record for this filter |30004"), EFormAlerteType.Erreur);
			}
			else
			{
				ObjetEdite = (CObjetDonneeAIdNumeriqueAuto) m_listeObjets[0];
				m_enumerator = m_listeObjets.GetEnumeratorBiSens();
				m_enumerator.CurrentIndex = m_listeObjets.GetIndex(m_objetEdite);
				UpdateForm(false);
			}
		}

		//-------------------------------------------------------------------------
		private void m_menuAnnulerFiltre_Click(object sender, System.EventArgs e)
		{
			if ( m_listeObjets == null )
				return;
			m_listeObjets.Filtre = null;
			if ( m_listeObjets.Count  != 0 )
			{
				ObjetEdite = (CObjetDonneeAIdNumeriqueAuto) m_listeObjets[0];
				UpdateForm(false);
			}
		}
		//-------------------------------------------------------------------------
		public bool EtatEdition
		{
			get 
			{   
				return m_bEtatEdition; 
			}
		}
		//-------------------------------------------------------------------------
		public bool IsEnEditionPourNouvelElement
		{
			get
			{
				return ObjetEdite != null && ObjetEdite.IsNewInThisContexte();
			}
		}

        //-------------------------------------------------------------------------
        public virtual CReferenceTypeForm GetReferenceTypeForm()
        {
            return new CReferenceTypeFormBuiltIn(GetType());
        }

		//-------------------------------------------------------------------------
		public event EventHandler ObjetEditeChanged;

		protected CObjetDonneeAIdNumeriqueAuto ObjetEdite
		{
			get
			{
				return m_objetEdite;
			}
			set
			{
				if (DesignMode)
					return;
                if (value != null)
                {
                    CReferenceTypeForm refType = CFormFinder.GetRefFormToEdit(value.GetType());
                    if (refType != null)
                        refType = refType.GetFinalRefTypeForm(value);
                    if (refType != null && !refType.Equals(GetReferenceTypeForm()) && Navigateur != null)
                    {
                        IFormNavigable frm = refType.GetForm(value, m_listeObjets) as IFormNavigable;
                        if (frm != null && Navigateur.AffichagePageSansHistorique(frm))
                            return;
                    }
                }
				m_objetEdite = value;
				if ( m_bIsLoaded && !InitChamps() )
					AffichageDataInvalides();
				else
					MasqueDataInvalides();
				if (ObjetEditeChanged != null) 
					ObjetEditeChanged(this, null);
			}
		}
		//-------------------------------------------------------------------------
		protected void UpdateControlesOnEtatEdition()
		{
			m_gestionnaireModeEdition.ModeEdition = EtatEdition;
		}

        //-------------------------------------------------------------------------
        /// <summary>
        /// Remet à jour la dialogue avec le contenu de l'objet
        /// </summary>
        public void RefillDialog()
        {
            if (!m_gestionnaireModeEdition.ModeEdition)
                ObjetEdite.Refresh();
            InitChamps();
        }

        //-------------------------------------------------------------------------
        private bool m_bGestionnaireROIsInit = false;
        protected CResultAErreur InitChamps()
        {
            using (CWaitCursor waiter = new CWaitCursor())
            {
                CResultAErreur result = CResultAErreur.True;
                try
                {
                    this.SuspendDrawing();

                    ReadTabControlState();

                    m_btnExtractList.Visible = ObjetEdite != null && ObjetEdite.ContexteDonnee.ContextePrincipal == null;

                    m_gestionnaireReadOnly.Clean();

                    result = MyInitChamps();

                    m_gestionnaireReadOnly.Init(this);
                    m_bGestionnaireROIsInit = true;
                    UpdateDisponibiliteControles();
                }
                finally
                {
                    this.ResumeDrawing();
                }

                return result;
            }
        }

		//-------------------------------------------------------------------------
		protected virtual CResultAErreur MyInitChamps() 
		{
			if (DesignMode)
				return CResultAErreur.True;

			if (!ObjetEdite.IsValide())
			{
				if (!Navigateur.AffichePagePrecedente())
					Navigateur.AffichePageAccueil();
				return CResultAErreur.False ;
			}
			CResultAErreur result =  m_extLinkField.FillDialogFromObjet(ObjetEdite);
			if (!result)
				return result;
            if (ObjetEdite != null)
            {
                m_lblId.Text = ObjetEdite.Id < 0 ? "-" : ObjetEdite.Id.ToString();
                SetToolTipImageCle();
                // Vérifie si l'objet est clonable
                Type tpObjetEdite = ObjetEdite.GetType();
                object[] attributes = tpObjetEdite.GetCustomAttributes(typeof(NonClonableObjectAttribute), false);
                m_bCloneAutorise = m_menuCopie.Visible = (attributes.Length == 0);

            }
			m_pagesInitialisees.Clear();
			if (m_tabControl != null && m_tabControl.SelectedTab != null)
				result = InitPage(m_tabControl.SelectedTab);

			if ( OnInitChamps != null && result)
				OnInitChamps ( this, ref result );
            m_btnChercherObjet.Visible = ObjetEdite is IObjetCherchable;
			return result;
		}
		//-------------------------------------------------------------------------
		protected virtual CResultAErreur MAJ_Champs() 
		{
			//Attention : risque de ne pas fonctionner avec des Champs non-texte...
			CResultAErreur result = m_extLinkField.FillObjetFromDialog(ObjetEdite);
			if (!result)
				return result;
			if (OnMajChampsPage != null)
			{
				foreach (KeyValuePair<object, bool> infoPage in m_pagesInitialisees)
					if (infoPage.Value)
					{
						result = OnMajChampsPage(infoPage.Key);
						if (!result)
							return result;
					}
			}
			if (OnMajChamps != null && result)
			{
				OnMajChamps(this, ref result);
			}

            SetToolTipImageCle();

			return result;
		}
		//-------------------------------------------------------------------------
        private bool m_bIsQueryClosing = false;
        public virtual bool QueryClose()
		{
			if (!EtatEdition || m_bIsQueryClosing)
			{
				return true;
			}
            try
            {
                m_bIsQueryClosing = true;

                DialogResult reponse = CFormAlerte.Afficher(m_objetEdite.DescriptionElement + I.T(" is being edited.\r\nDo you want to save changes ?|30005"),
                    EFormAlerteBoutons.OuiNonCancel, EFormAlerteType.Question);

                //reponse = MessageBox.Show(
                //    CSc2iWin32DataNavigation.Navigateur, 
                //    m_objetEdite.DescriptionElement + " est en cours d'édition.\nVoulez-vous enregistrer les modifications ?",
                //    "Confirmation", 
                //    MessageBoxButtons.YesNoCancel, 
                //    MessageBoxIcon.Question, 
                //    MessageBoxDefaultButton.Button3);

                if (reponse == DialogResult.Cancel)
                    return false;
                else if (reponse == DialogResult.No)
                {
                    OnClickAnnuler();
                    return true;
                }
                else
                {
                    MAJ_Champs();
                    return (ValidationElement(m_objetEdite));
                }
            }
            finally
            {
                m_bIsQueryClosing = false;
            }
		}
		//-------------------------------------------------------------------------
		private bool ValidationElement(CObjetDonnee obj)
		{
			if ( m_bConsultationOnly || m_bSansEdition || !m_btnValiderModifications.Visible)
			{
				CFormAlerte.Afficher(I.T("Unauthorized function|30001"), EFormAlerteType.Erreur);
				return false;
			}
			using (CWaitCursor waiter = new CWaitCursor())
			{
				CObjetDonneeCancelEventArgs cancelArgs = new CObjetDonneeCancelEventArgs(m_objetEdite);
				//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
				if (BeforeValideModification != null)
				{
					BeforeValideModification(this, cancelArgs);
					if (cancelArgs.Cancel)
						return false;
				}

				CResultAErreur result = MAJ_Champs();
				if (result)
				{
					//On vérifie sur le serveur si le contexte de l'élement
					//est un contexte d'édition sur un contexte d'édition
					bool bVerifImmediate =
						m_objetEdite.ContexteDonnee.ContextePrincipal != null &&
						m_objetEdite.ContexteDonnee.ContextePrincipal.IsEnEdition;
					result = m_objetEdite.VerifieDonnees(!bVerifImmediate);
				}
				
				if (!result)
				{
					if (CFormAlerte.Afficher(result) == DialogResult.Cancel)
						return false;
					result = CResultAErreur.True;
				}

				//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
				result = m_objetEdite.CommitEdit();
				
				if (!result)
				{
					CFormAlerte.Afficher(result);
					return false;
				}
				//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
				m_bEtatEdition = false;
				UpdateBoutonsEdition();
				UpdateControlesOnEtatEdition();
				if (AfterValideModification != null)
					AfterValideModification(this, new CObjetDonneeCancelEventArgs(m_objetEdite));

				if (CSc2iWin32DataNavigation.Navigateur == null)
					return false;

				if (!CSc2iWin32DataNavigation.Navigateur.Visible)
					return false;

				if (IsHandleCreated)
				{
					if (!InitChamps())
						AffichageDataInvalides();
					else
						MasqueDataInvalides();
				}
				return true;
			}
		}
		/// /////////////////////////////////////////
		private void m_btnAjout_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( e.Button == MouseButtons.Right )
				m_menuNouveau.Show  ( m_btnAjout, new Point ( 0, m_btnAjout.Height ) );
			else
				OnClickAjouter();
		}
		//-------------------------------------------------------------------------

		//-------------------------------------------------------------------------
		private void m_btnEditerObjet_Click(object sender, System.EventArgs e)
		{
				OnClickModifier();
		}
		//-------------------------------------------------------------------------
		private void m_btnSupprimerObjet_Click(object sender, System.EventArgs e)
		{ 
				OnClickSupprimer();
		}
		//-------------------------------------------------------------------------
		private void m_btnValiderModifications_Click(object sender, System.EventArgs e)
		{			
				OnClickValider();
		}
		//-------------------------------------------------------------------------
		private void m_btnAnnulerModifications_Click(object sender, System.EventArgs e)
		{
				OnClickAnnuler();
		}
		//-------------------------------------------------------------------------
		private void DetermineIndexEnCours()
		{
			m_lblNbListes.Text = (m_enumerator.CurrentIndex+1).ToString() + "/" + m_listeObjets.Count.ToString();
		}
		//-------------------------------------------------------------------------
		private void m_btnPrecedent_Click(object sender, System.EventArgs e)
		{
			//Cf commentaires du m_btnSuivant
			if ( m_enumerator.CurrentIndex <= 0 )
				return;
			if (!QueryClose())
				return;

			m_bEtatEdition = false;
			int nCurrentId = ObjetEdite.Id;
			bool bResult = m_enumerator.MovePrev();
			
			if (!bResult)
				return;
			ObjetEdite = (CObjetDonneeAIdNumeriqueAuto)m_enumerator.Current;
			if (nCurrentId != ObjetEdite.Id)
			{
				UpdateForm(false);
			}
		}
		//-------------------------------------------------------------------------
		private void m_btnSuivant_Click(object sender, System.EventArgs e)
		{
			///SC : Modif du 25/09
			///lorsqu'on appelle la fonction 'ObjetEdite set', on appelle UpdateForm,
			///Il n'est donc pas nécéssaire de le rappeler après!
			if ( m_listeObjets==null ||  m_enumerator.CurrentIndex >= m_listeObjets.Count-1 )
				return;
			if (!QueryClose())
				return;
			//Déplacé 25/09
			m_bEtatEdition = false;
			m_gestionnaireModeEdition.ModeEdition = m_bEtatEdition;
			int nCurrentId = ObjetEdite.Id;
			bool bResult = m_enumerator.MoveNext();
			if (!bResult)
				return;
			ObjetEdite = (CObjetDonneeAIdNumeriqueAuto)m_enumerator.Current;
			if (nCurrentId != ObjetEdite.Id)
			{
				UpdateForm(false);
			}
		}
		//-------------------------------------------------------------------------
		public void UpdateForm( bool bInitChamps )
		{
			UpdateBoutonsEdition();
			UpdateControlesOnEtatEdition();
			CResultAErreur result = CResultAErreur.True;
			if ( bInitChamps )
			{
				if (!InitChamps())
					AffichageDataInvalides();
				else
					MasqueDataInvalides();
			}
			if(m_listeObjets==null)
			{
				m_panelNavigation.Hide();
				return;
			}
			m_panelNavigation.Show();
			DetermineIndexEnCours();
		}

        public event ContexteFormEventHandler AfterGetContexte;
        public event ContexteFormEventHandler AfterInitFromContexte;

		//-------------------------------------------------------------------
		public virtual CContexteFormNavigable GetContexte()
		{
			if (IsEnEditionPourNouvelElement || m_panelDataInvalide.Visible )
				return null;

			CContexteFormNavigable ctx = new CContexteFormNavigable(GetType() );
			CStringSerializer serializer = new CStringSerializer(ModeSerialisation.Ecriture);
			I2iSerializable obj = m_listeObjets;
			serializer.TraiteObject ( ref obj );
			ctx["ListeObjets"] = serializer.String;
			if ( ObjetEdite != null )
			{
				ctx["CHAMP_ID"] = ObjetEdite.Id;
				ctx["TypeObjet"] = ObjetEdite.GetType();
			}
			else
			{
				ctx["CHAMP_ID"] = null;
				ctx["TypeObjet"] = null;
			}
            ctx["AFFECTATIONS"] = m_listeAffectationsPourNouvelElement;
			StockEtatControles ( this, ctx );
            ctx["CONTEXTE_UTILISATION"] = m_strContexteUtilisation;
            ctx["CONTEXTE_MODIFICATION"] = m_strContexteModification;
            if (AfterGetContexte != null)
                AfterGetContexte(this, ctx);
            if (m_enumerator != null)
                ctx["ITEM_INDEX_IN_LISTE"] = m_enumerator.CurrentIndex;
			return ctx;
		}
		//-------------------------------------------------------------------------
		protected virtual void StockEtatControles ( Control ctrl, CContexteFormNavigable contexte )
		{
			foreach (Control ctrlFils in ctrl.Controls )
			{
				if ( ctrlFils is System.Windows.Forms.TabControl )
				{
					contexte["TAB_"+ctrlFils.Name] = ((TabControl)ctrlFils).SelectedIndex;
					contexte["TABNAME_"+ctrlFils.Name] = ((TabControl)ctrlFils).SelectedTab.Text;
				}
				if ( ctrlFils is sc2i.win32.common.C2iTabControl )
				{
					contexte["TAB_"+ctrlFils.Name] = ((C2iTabControl)ctrlFils).SelectedIndex;
					contexte["TABNAME_"+ctrlFils.Name] = ((C2iTabControl)ctrlFils).SelectedTab.Title;
				}
			}

		}
		//-------------------------------------------------------------------------
		public virtual CResultAErreur InitFromContexte(CContexteFormNavigable ctx)
		{
			CResultAErreur result = CResultAErreur.True;
			m_contexte = ctx;
            m_strContexteUtilisation = ctx["CONTEXTE_UTILISATION"] is string ? (string)ctx["CONTEXTE_UTILISATION"] : ""; ;
            m_strContexteModification = ctx["CONTEXTE_MODIFICATION"] is string ? (string)ctx["CONTEXTE_MODIFICATION"] : "";
			CStringSerializer serializer = new CStringSerializer((string)ctx["ListeObjets"], ModeSerialisation.Lecture);
			I2iSerializable obj = null;
			if ( serializer.TraiteObject ( ref obj, CSc2iWin32DataClient.ContexteCourant ))
				m_listeObjets = (CListeObjetsDonnees)obj;
			else
				m_listeObjets = null;

			Type tp = (Type)ctx["TypeObjet"];
			CObjetDonneeAIdNumeriqueAuto tempObject = (CObjetDonneeAIdNumeriqueAuto) Activator.CreateInstance(tp, new object[] {CSc2iWin32DataClient.ContexteCourant} );

			tempObject.Id = (int)ctx["CHAMP_ID"];
			ObjetEdite = tempObject;	
		
			if(m_listeObjets!=null)
			{
				if (m_enumerator==null)
				{
					m_enumerator = m_listeObjets.GetEnumeratorBiSens();
                    //S'assure que la page demandée est lue dans la liste
                    if (m_listeObjets.RemplissageProgressif && ctx["ITEM_INDEX_IN_LISTE"] is int)
                    {
                        int nIndex = (int)ctx["ITEM_INDEX_IN_LISTE"];
                        tempObject = m_listeObjets[nIndex] as CObjetDonneeAIdNumeriqueAuto;
                    }
					m_enumerator.CurrentIndex = m_listeObjets.GetIndex(ObjetEdite);
				}
			}
            m_listeAffectationsPourNouvelElement = ctx["AFFECTATIONS"] as List<CAffectationsProprietes>;
            if ( m_listeAffectationsPourNouvelElement == null )
                m_listeAffectationsPourNouvelElement = new List<CAffectationsProprietes>();
			RemetEtatControles ( this, ctx );
            if (AfterInitFromContexte != null)
                AfterInitFromContexte(this, ctx);
			return result;
		}

		//-------------------------------------------------------------------------
		protected virtual void RemetEtatControles ( Control ctrl, CContexteFormNavigable contexte )
		{
			foreach (Control ctrlFils in ctrl.Controls )
			{
				if ( ctrlFils is System.Windows.Forms.TabControl )
				{
					try
					{
						((TabControl)ctrlFils).SelectedIndex = Int32.Parse(contexte["TAB_"+ctrlFils.Name].ToString());
					}
					catch
					{
					}
					
				}
				if ( ctrlFils is sc2i.win32.common.C2iTabControl )
				{
					try
					{
						//((C2iTabControl)ctrlFils).SelectedIndex = Int32.Parse(contexte["TAB_"+ctrlFils.Name].ToString());
						((C2iTabControl)ctrlFils).SelectedTab = ((C2iTabControl)ctrlFils).TabPages[ contexte["TABNAME_"+ctrlFils.Name].ToString() ];
					}
					catch
					{
					}
				}
			}
		}
		//-------------------------------------------------------------------------
		protected void AffecterTitre(string strTitre)
		{
			CSc2iWin32DataNavigation.Navigateur.TitreFenetreEnCours = strTitre;
		}


		//-------------------------------------------------------------------------
		private void m_menuNouvelElement_Click(object sender, System.EventArgs e)
		{
			OnClickAjouter ();
		}

		//-------------------------------------------------------------------------
		private void m_menuCopie_Click(object sender, System.EventArgs e)
		{
			OnClickAjouter ( true );
		}

		//-------------------------------------------------------------------------
		[DefaultValue(false)]
		public bool ConsultationOnly
		{
			get
			{
				return m_bConsultationOnly;
			}
			set
			{
				m_bConsultationOnly = value;
				m_btnAjout.Visible = !m_bConsultationOnly;
				m_btnEditerObjet.Visible = !m_bConsultationOnly;
				m_btnSupprimerObjet.Visible = !m_bConsultationOnly;
				m_btnValiderModifications.Visible = !m_bConsultationOnly;
				m_btnAnnulerModifications.Visible = !m_bConsultationOnly;
				m_bAjouterVisible = !m_bConsultationOnly;
				m_bSupprimerVisible = !m_bConsultationOnly;
				UpdateDisponibiliteControles();
			}
		}

		//-------------------------------------------------------------------------
		[DefaultValue(false)]
		public bool SansEdition
		{
			get
			{
				return m_bSansEdition;
			}
			set
			{
				m_bSansEdition = value;
				m_btnSupprimerObjet.Visible = true;
				m_btnAjout.Visible = true;
				m_btnEditerObjet.Visible = !m_bSansEdition;
				m_btnValiderModifications.Visible = !m_bSansEdition;
				m_btnAnnulerModifications.Visible = !m_bSansEdition;
				UpdateDisponibiliteControles();
			}
		}

        //-------------------------------------------------------------------------
        public void AddRestrictionComplementaire(string strTag, CListeRestrictionsUtilisateurSurType restrictions, bool bApplicationImmediate)
        {
            if (restrictions != null)
                m_dicRestrictionsComplementaires[strTag] = restrictions;
            else if (m_dicRestrictionsComplementaires.ContainsKey(strTag))
                m_dicRestrictionsComplementaires.Remove(strTag);
            
            /*m_restrictionsComplementaires = new CListeRestrictionsUtilisateurSurType();
            foreach (CListeRestrictionsUtilisateurSurType restriction in m_dicRestrictionsComplementaires.Values)
                if (restriction != null)
                    m_restrictionsComplementaires.Combine(restriction);
            */
            if (bApplicationImmediate)
                UpdateDisponibiliteControles();
        }


        private class CLockerDispoControles { }

		//-------------------------------------------------------------------------
		/// <summary>
		/// Utilise les restrictions de l'utilisateur et le ExLinkField
		/// Pour activer ou désactiver les contrôles
		/// </summary>
		protected void UpdateDisponibiliteControles( )
		{
			if (DesignMode)
				return;
            lock (typeof(CLockerDispoControles))
            {
                try
                {
                    List<CListeRestrictionsUtilisateurSurType> listeRestrictionsACombiner = new List<CListeRestrictionsUtilisateurSurType>();
                    
                    //CSessionClient session = CSessionClient.GetSessionForIdSession(CSc2iWin32DataClient.ContexteCourant.IdSession);
                    CSessionClient session = CSessionClient.GetSessionForIdSession(ObjetEdite.ContexteDonnee.IdSession);
                    CListeRestrictionsUtilisateurSurType restrictionsSysteme = session.GetInfoUtilisateur().GetListeRestrictions(ObjetEdite.ContexteDonnee.IdVersionDeTravail).Clone() as CListeRestrictionsUtilisateurSurType;
                    
                    string strOldContexteModif = ObjetEdite.ContexteDeModification;
                    //S'il y a un contexte de modification, l'applique à l'objet
                    //Pour éventuellement débloquer le mode d'édition
                    if (ContexteModification.Length > 0)
                    {
                        CContexteDonnee.ChangeRowSansDetectionModification(
                            ObjetEdite.Row,
                            CObjetDonnee.c_champContexteModification,
                            ContexteModification);
                    }

                    // Adapte toutes les restrictions à l'objet en cours
                    restrictionsSysteme.ApplyToObjet(ObjetEdite);
                    foreach (CListeRestrictionsUtilisateurSurType lstRest in m_dicRestrictionsComplementaires.Values)
                    {
                        CListeRestrictionsUtilisateurSurType lstRestClone = lstRest.Clone() as CListeRestrictionsUtilisateurSurType;
                        CRestrictionUtilisateurSurType restTemp = lstRestClone.GetRestriction(ObjetEdite.GetType()).Clone() as CRestrictionUtilisateurSurType;
                        restTemp.ConvertToRestrictionSansPriorite();
                        restTemp.ApplyToObjet(ObjetEdite);
                        if (restTemp.HasRestrictions)
                            lstRestClone.SetRestriction(restTemp);
                        else
                            lstRestClone.SetRestriction(new CRestrictionUtilisateurSurType(ObjetEdite.GetType(), ERestriction.Aucune));
                        listeRestrictionsACombiner.Add(lstRestClone);
                    }
                    //S'il y a un contexte de modification, remet l'objet dans 
                    //son état original
                    if (ContexteModification.Length > 0)
                        CContexteDonnee.ChangeRowSansDetectionModification(
                            ObjetEdite.Row,
                            CObjetDonnee.c_champContexteModification,
                            strOldContexteModif);
                    
                    //Combine les restrictions de la liste
                    foreach (CListeRestrictionsUtilisateurSurType restrictionsACombiner in listeRestrictionsACombiner)
                    {
                        restrictionsSysteme.Combine(restrictionsACombiner);
                    }

                    CRestrictionUtilisateurSurType rest = restrictionsSysteme.GetRestriction(ObjetEdite.GetType()).Clone() as CRestrictionUtilisateurSurType;
                    if (!rest.CanShowType())
                    {
                        m_panelMenu.Visible = false;
                        m_lblPageInterdite.Dock = DockStyle.Fill;
                        m_lblPageInterdite.Visible = true;
                        m_lblPageInterdite.BringToFront();
                    }
                    else
                    {
                        m_panelMenu.Visible = true;
                        m_lblPageInterdite.Visible = false;
                        m_lblPageInterdite.Dock = DockStyle.None;
                    }
                    m_btnAjout.Visible = rest.CanCreateType() && !m_bConsultationOnly & m_bAjouterVisible;
                    m_btnSupprimerObjet.Visible = rest.CanDeleteType() && !m_bConsultationOnly & m_bSupprimerVisible;
                    m_btnEditerObjet.Visible = rest.CanModifyType() && !m_bConsultationOnly && !m_bSansEdition;
                    m_btnAnnulerModifications.Visible = rest.CanModifyType() && !m_bConsultationOnly && !m_bSansEdition;
                    m_btnValiderModifications.Visible = rest.CanModifyType() && !m_bConsultationOnly && !m_bSansEdition;

                    m_extLinkField.SourceType = ObjetEdite.GetType();
                    CApplicateurRestrictions.AppliqueRestrictions(this, restrictionsSysteme, m_gestionnaireReadOnly);
                    
                    
                }
                catch
                {
                }
            }
		}

		//-------------------------------------------------------------------------
		/// <summary>
		/// Utilise les restrictions de l'utilisateur et le ExLinkField
		/// Pour activer ou désactiver les contrôles
		/// </summary>
		protected void UpdateDisponibiliteControles(Control control, CRestrictionUtilisateurSurType restriction )
		{
            if (restriction.IsContexteException(ContexteModification))
                return;

            string strProp = m_extLinkField.GetLinkField(control);
			if ( strProp != null && strProp != "" )
				AppliqueRestrictionSurControle ( control, restriction.GetRestriction(strProp));
			foreach ( Control controlFils in control.Controls )
				UpdateDisponibiliteControles ( controlFils, restriction );
		}

		protected void AppliqueRestrictionSurControle ( Control control, ERestriction rest )
		{
			if ( !CRestrictionUtilisateurSurType.CanModify ( rest ) )
			{
				m_gestionnaireModeEdition.SetModeEdition ( control, TypeModeEdition.Autonome );
				if ( control is IControlALockEdition )
					((IControlALockEdition)control).LockEdition = true;
				else
					control.Enabled = false;
			}
			
			if ( !CRestrictionUtilisateurSurType.CanShow ( rest ) )
			{
				m_extLinkField.SetLinkField(control, "ValeurMasquee");
				control.ForeColor = control.BackColor;
			}
		}

		//-------------------------------------------------------------------------
		public bool ShouldSerializeBoutonAjouterVisible()
		{
			return m_bAjouterVisible == false;;
		}

		/// //////////////////////////////////////
		public bool BoutonAjouterVisible
		{
			get
			{
				return m_bAjouterVisible;
			}
			set
			{
				m_bAjouterVisible = value;
				UpdateDisponibiliteControles();
			}
		}

		/// //////////////////////////////////////
		public bool ShouldSerializeBoutonSupprimerVisible()
		{
			return m_bSupprimerVisible == false;
		}
		
		/// //////////////////////////////////////
		public bool BoutonSupprimerVisible
		{
			get
			{
				return m_bSupprimerVisible;
			}
			set
			{
				m_bSupprimerVisible = value;
				m_btnSupprimerObjet.Visible = value;
				UpdateDisponibiliteControles();
			}
		}
		
		/// //////////////////////////////////////
		/// A appeler quand les informations sont invalides
		protected void AffichageDataInvalides()
		{
			m_panelMenu.Visible = false;
			m_panelDataInvalide.Dock = DockStyle.Fill;
			m_panelDataInvalide.Visible = true;
			m_panelDataInvalide.BringToFront();
			m_bEtatEdition = false;
			m_gestionnaireModeEdition.ModeEdition = false;
		}

		/// //////////////////////////////////////
		/// A appeler quand les informations sont invalides
		protected void MasqueDataInvalides()
		{
			m_panelMenu.Visible = true;
			m_panelDataInvalide.Dock = DockStyle.None;
			m_panelDataInvalide.Visible = false;
			m_panelDataInvalide.SendToBack();

		}

        private void CFormEditionStandard_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
               case Keys.F12:
                    CSessionClient session = CSessionClient.GetSessionUnique();
                    if (session != null && session.GetInfoUtilisateur().GetDonneeDroit(
                                CDroitDeBaseSC2I.c_droitAdministration) != null)
                    {
                        e.Handled = true;
                        CFormSetupSmartImport.ShowValeurs(ObjetEdite);
                    }
                    
                    break;
                case Keys.F10:
                    if (m_bEtatEdition)
                    {
                        if (!m_btnValiderModifications.Focus())
                        {
                            CFormAlerte.Afficher(I.T("Correct errors before validation|135"), EFormAlerteType.Exclamation);
                            e.Handled = true;
                            return;
                        }
                        OnClickValider();
                        e.Handled = true;
                    }
                    break;
                case Keys.F8:
                    if (!m_bEtatEdition)
                    {
                        m_btnEditerObjet.Focus();
                        OnClickModifier();
                        e.Handled = true;
                    }
                    break;
                case Keys.F4:
                    m_btnAjout.Focus();
                    OnClickAjouter((e.Modifiers & Keys.Control) == Keys.Control);
                    e.Handled = true;
                    break;
                case Keys.F5:
                    if (!m_bEtatEdition)
                    {
                        Actualiser();
                        e.Handled = true;
                    }
                    break;
                case Keys.V:
                    if (e.Alt && e.Control)
                    {
                        CFormVisualisationDataSet.AfficheDonnees(CSc2iWin32DataClient.ContexteCourant);
                    }
                    break;
                case Keys.D:
                    if ( e.Alt && e.Control && !ModeEdition)
                    {
                        CFormDependancesObjet.ShowDependances(ObjetEdite);
                    }
                    break;
            }

        }

        //------------------------------------------------------------------
        public CResultAErreur Actualiser()
        {
            if(!ObjetEdite.Refresh())
                return CResultAErreur.False;
            return InitChamps();
        }

        /// <summary>
        /// Envoyé par certaines fenêtre pour indiquer qu'une donnée de l'objet édité a été modifiée
        /// </summary>
        public event EventOnChangementDonnee OnChangementSurObjet;

        protected void DeclencheEvenementChangementDonnee(string strNomChamp)
        {
            if (OnChangementSurObjet != null)
                OnChangementSurObjet(strNomChamp);
        }

		#region Membres de IElementAContexteUtilisation

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

		#endregion

		private void m_btnHistorique_Click(object sender, EventArgs e)
		{
			if (this.ObjetEdite is CObjetDonneeAIdNumeriqueAuto)
			{
                if (this.ObjetEdite.ContexteDonnee.IdVersionDeTravail == null)
                {
                    CFormListeArchives.ShowArchives((CObjetDonneeAIdNumeriqueAuto)this.ObjetEdite);
                    Refresh();
                }
                else
                {
                    CVersionDonnees version = new CVersionDonnees(ObjetEdite.ContexteDonnee);
                    if (version.ReadIfExists((int)ObjetEdite.ContexteDonnee.IdVersionDeTravail))
                        CFormDetailVersion.ShowDetail((CObjetDonneeAIdNumeriqueAuto)this.ObjetEdite, version);
                }
			}
		}

		#region IFormAContexteDonnee Membres

		public CContexteDonnee ContexteDonnee
		{
			get
			{
				if ( ObjetEdite != null )
					return ObjetEdite.ContexteDonnee;
				return null;
			}
		}

		#endregion

        private void m_btnChercherObjet_Click(object sender, EventArgs e)
        {
            IObjetCherchable objet = ObjetEdite as IObjetCherchable;
            if (objet != null)
            {
                CRequeteRechercheObjet requete = objet.GetRequeteRecherche();
                CFormResultatRechercheObjet.RechercheInThread(requete, requete.Libelle);
            }
        }
        
        //-------------------------------------------------------------------------
        private void m_imageCle_Click(object sender, System.EventArgs e)
        {
            //m_lblId.Visible = !m_lblId.Visible;
        }

        void m_imageCle_MouseDown(object sender, MouseEventArgs e)
        {
            //m_lblId.Visible = !m_lblId.Visible;
            if (ObjetEdite != null && e.Button == MouseButtons.Left)
            {
                CReferenceObjetDonnee reference = new CReferenceObjetDonnee(ObjetEdite);
                if (reference != null)
                    DoDragDrop(reference, DragDropEffects.None | DragDropEffects.Move | DragDropEffects.Link | DragDropEffects.Copy);
            }
        }


        public event EventHandler OnChangeModeEdition;


        private void m_gestionnaireModeEdition_ModeEditionChanged(object sender, EventArgs e)
        {
            if (OnChangeModeEdition != null)
                OnChangeModeEdition(this, new EventArgs());
        }

        private void m_imageCle_MouseUp(object sender, MouseEventArgs e)
        {
            if (ObjetEdite != null && e.Button == MouseButtons.Right)
            {
                ContextMenuStrip menu = new ContextMenuStrip();
                if (ObjetEdite.ManageIdUniversel)
                {
                    // Copie une chaine Formule GetEntity("Type", Id)
                    ToolStripMenuItem itemGetEntity = new ToolStripMenuItem(I.T("Copy universal id to Clipboard|10110"));
                    itemGetEntity.Click += new EventHandler(itemGetEntity_Click);
                    menu.Items.Add(itemGetEntity);
                }

                // Nommer l'entité
                CSessionClient session = CSessionClient.GetSessionForIdSession(ObjetEdite.ContexteDonnee.IdSession);
                IInfoUtilisateur infoUser = session.GetInfoUtilisateur();
                // Interdit quand l'objet est nouveau à cause de son Id négatif qui sera associé aux CNommageEntite
                if (infoUser != null && !ObjetEdite.IsNew())
                {
                    int? nIdVersion = ObjetEdite.ContexteDonnee.IdVersionDeTravail;
                    CListeRestrictionsUtilisateurSurType restrictions = infoUser.GetListeRestrictions(nIdVersion).Clone() as CListeRestrictionsUtilisateurSurType;
                    if (restrictions != null)
                    {
                        CRestrictionUtilisateurSurType rest = restrictions.GetRestriction(typeof(CNommageEntite));
                        if (rest != null && rest.CanShowType())
                        {
                            m_bCanModifyNommageEntite = rest.CanModifyType();
                            ToolStripMenuItem itemNommerEntiter = new ToolStripMenuItem(I.T("Name the Entity|10111"));
                            itemNommerEntiter.Click += new EventHandler(itemNommerEntiter_Click);
                            menu.Items.Add(itemNommerEntiter);
                        }
                    }
                }

                menu.Show(m_imageCle, new Point(0, m_imageCle.Height));
            }

        }

        void itemGetEntity_Click(object sender, EventArgs e)
        {
            if (ObjetEdite.ManageIdUniversel)
                Clipboard.SetText(ObjetEdite.IdUniversel);
            /*
            C2iExpressionGetEntite formuleACopier = new C2iExpressionGetEntite();
            formuleACopier.Parametres.Add(new C2iExpressionConstante(DynamicClassAttribute.GetNomConvivial(ObjetEdite.GetType())));
            formuleACopier.Parametres.Add(new C2iExpressionConstante(ObjetEdite.Id));

            Clipboard.SetText(formuleACopier.GetString());*/
        }

        private bool m_bCanModifyNommageEntite = false;
        void itemNommerEntiter_Click(object sender, EventArgs e)
        {
            if (m_bCanModifyNommageEntite)
                CFormNommageEntite.NommerEntite(ObjetEdite);
            else
            {
                // Affiche les Noms donnés
                // Recherche la liste des CNommageEntite existants sur cet élément
                CListeObjetsDonnees lstNommages = new CListeObjetsDonnees(ObjetEdite.ContexteDonnee, typeof(CNommageEntite));
                //TESTDBKEYOK
                if (ObjetEdite.DbKey != null)
                    lstNommages.Filtre = new CFiltreData(
                        CNommageEntite.c_champTypeEntite + " = @1 and " +
                        CNommageEntite.c_champCleEntite + " = @2",
                        ObjetEdite.TypeString,
                        ObjetEdite.DbKey.StringValue);
                else
                    lstNommages.Filtre = new CFiltreDataImpossible();

                string strNoms = "";
                foreach (CNommageEntite nom in lstNommages)
                {
                    strNoms += Environment.NewLine + nom.NomFort;
                }

                CFormAlerte.Afficher(I.T("You don't have the rights to modify Entity Strong Names|10116") + Environment.NewLine + strNoms);
            }
            SetToolTipImageCle();
        }

        //----------------------------------------------------------------------------------------
        private void SetToolTipImageCle()
        {
            CListeObjetsDonnees lstNommages = new CListeObjetsDonnees(ObjetEdite.ContexteDonnee, typeof(CNommageEntite));
            //TESTDBKEYOK
            if (ObjetEdite.DbKey != null)
                lstNommages.Filtre = new CFiltreData(
                    CNommageEntite.c_champTypeEntite + " = @1 and " +
                    CNommageEntite.c_champCleEntite + " = @2",
                    ObjetEdite.TypeString,
                    ObjetEdite.DbKey.StringValue);
            else
                lstNommages.Filtre = new CFiltreDataImpossible();

            string strBulle = "ID="+ObjetEdite.Id.ToString();
            IObjetHierarchiqueACodeHierarchique h = ObjetEdite as IObjetHierarchiqueACodeHierarchique;
            if (h != null)
            {
                strBulle += " (" + h.CodeSystemeComplet + ")";
            }
            if (ObjetEdite.ManageIdUniversel)
                strBulle += Environment.NewLine + "UID="+ObjetEdite.IdUniversel;

            foreach (CNommageEntite nom in lstNommages)
            {
                strBulle += Environment.NewLine + nom.NomFort;
            }

            m_tipFormEditionObjetDonnee.SetToolTip(m_imageCle, strBulle);
        }

        //----------------------------------------------------------------------------------------
        public virtual string GetTitle()
        {
            if (ObjetEdite != null)
                return ObjetEdite.DescriptionElement;
            return "";
        }

        //----------------------------------------------------------------------------------------
        public virtual Image GetImage()
        {
            if (ObjetEdite != null)
            {
                Image img = DynamicClassAttribute.GetImage(ObjetEdite.GetType());
                if (img != null)
                    return img;
            }
            return sc2i.win32.data.navigation.Properties.Resources.view_edit;
        }

        //----------------------------------------------------------------------------------------
        public bool ValiderModifications()
        {
            return OnClickValider();
        }

        //----------------------------------------------------------------------------------------
        public void AnnulerModifications()
        {
            OnClickAnnuler();
        }

        //----------------------------------------------------------------------------------------
        private void m_btnExtractList_Click(object sender, EventArgs e)
        {
            CReferenceTypeForm refTypForm = CFormFinder.GetRefFormToEdit(m_objetEdite.GetType());
            if (refTypForm != null)
            {
                CFormEditionStandard frm = (CFormEditionStandard)refTypForm.GetForm(m_objetEdite, m_listeObjets);
                if (frm != null)
                {
                    CFormNavigateurPopup.ShowNonModale(frm, FormWindowState.Normal);
                }
            }
        }
        

        //----------------------------------------------------------------------------------------
        public void HideBtnExtractList()
        {
            m_btnExtractList.Visible = false;
        }

        //----------------------------------------------------------------------------------------
        private void m_TabControlZoomer_VisibleChanged(object sender, EventArgs e)
        {
            if (m_TabControlZoomer.Visible)
            {
                m_TabControlZoomer.SuspendDrawing();
                m_TabControlZoomer.Location = new Point(0, m_panelMenu.Bottom + 1);
                m_TabControlZoomer.Size = new Size(ClientSize.Width, ClientSize.Height - m_panelMenu.Height);
                m_TabControlZoomer.ResumeDrawing();
            }
        }


        //----------------------------------------------------------------------------------------
        private void CFormEditionStandard_FormClosing(object sender, FormClosingEventArgs e)
        {
             try
             {
                 SaveTabControlState();
             }
            catch{}
        }


        //----------------------------------------------------------------------------------------
        protected void SaveTabControlState()
        {
            if (C2iRegistre.RegistreApplication != null)
            {
                List<CTabControlState> lstStates = CEtatTabControls.GetTabsControlsStates(this);
                if (lstStates.Count > 0)
                {
                    CStringSerializer ser = new CStringSerializer(ModeSerialisation.Ecriture);
                    CResultAErreur res = ser.TraiteListe<CTabControlState>(lstStates);
                    if (res)
                        C2iRegistre.RegistreApplication.SetValue("Preferences", "TABSTATE"+GetType().ToString() + "/" + ContexteUtilisation, ser.String);
                }
            }
        }


        private bool m_bTabStateApplied = false;
        //----------------------------------------------------------------------------------------
        protected void ReadTabControlState()
        {
            if ( C2iRegistre.RegistreApplication != null && !m_bTabStateApplied )
            {
                m_bTabStateApplied = true;
                string strVal = C2iRegistre.RegistreApplication.GetValue("Preferences", "TABSTATE"+GetType().ToString() + "/" + ContexteUtilisation, "");
                if ( strVal.Length > 0 )
                {
                    CStringSerializer ser = new CStringSerializer(strVal, ModeSerialisation.Lecture);
                    List<CTabControlState> lstStates = new List<CTabControlState>();
                    if ( ser.TraiteListe<CTabControlState>(lstStates ) )
                    {
                        CEtatTabControls.ApplyTabsControlsStates(this, lstStates);
                    }
                }
            }
        }

	}
}

