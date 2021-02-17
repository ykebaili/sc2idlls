using sc2i.win32.data;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

using System.Windows.Forms;
using sc2i.win32.common;
using sc2i.win32.navigation;
using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic;
using sc2i.win32.data.navigation.Properties;
using System.Drawing.Imaging;
using System.Collections.Generic;
using sc2i.formulaire; 


namespace sc2i.win32.data.navigation
{
	public class CFormListeStandard : CFormMaxiSansMenu, 
		IFormNavigable, 
		IElementAContexteUtilisation,
		IFormAContexteDonnee
	{
        private Image m_imageOnglet = null;
		private string m_strTitreForce = "";
		protected CListeObjetsDonnees m_listeObjets = null;
		protected CFiltreData m_filtre = new CFiltreData();	
		protected CContexteFormNavigable m_contexte;
		protected sc2i.win32.data.navigation.CPanelListeSpeedStandard m_panelListe;
		private System.ComponentModel.IContainer components = null;
		private bool m_bModeQuickSearch = false;
		private System.Windows.Forms.Panel m_panelTotal;
		protected System.Windows.Forms.Panel m_panelGauche;
		protected System.Windows.Forms.Panel m_panelDroit;
		protected System.Windows.Forms.Panel m_panelBas;
		protected System.Windows.Forms.Panel m_panelHaut;
		protected System.Windows.Forms.Panel m_panelMilieu;

		private bool m_bModeSelection = false;

		//-------------------------------------------------------------------
		public CFormListeStandard()
		{
			// Cet appel est requis par le Concepteur Windows Form.
			InitializeComponent();
		}
		//-------------------------------------------------------------------
		public CFormListeStandard(Type typeObjet)
		{
			// Cet appel est requis par le Concepteur Windows Form.
			InitializeComponent();

			m_listeObjets = new CListeObjetsDonnees( CSc2iWin32DataClient.ContexteCourant, typeObjet, false );
			m_listeObjets.AppliquerFiltreAffichage = true;
            m_listeObjets.RemplissageProgressif = true;
			object[] attribs = typeObjet.GetCustomAttributes( typeof(DynamicClassAttribute), false);
			m_panelListe.BoutonExporterVisible = ( attribs != null && attribs.Length == 1 );
		}
		//-------------------------------------------------------------------
		public CFormListeStandard(CListeObjetsDonnees listeObjets)
		{
			// Cet appel est requis par le Concepteur Windows Form.
			InitializeComponent();

			m_listeObjets = listeObjets;
			m_listeObjets.AppliquerFiltreAffichage = true;
			object[] attribs = listeObjets.TypeObjets.GetCustomAttributes( typeof(DynamicClassAttribute), false);
			m_panelListe.BoutonExporterVisible = ( attribs != null && attribs.Length == 1 );
		}


        public void SetModifierElementDelegate(CPanelListeSpeedStandard.ModifierElementDelegate action)
        {
            m_panelListe.TraiterModificationElement = action;
        }

		
		//-------------------------------------------------------------------
		public CListeObjetsDonnees ListeObjets
		{
			get
			{
				return m_listeObjets;
			}
		}
		//-------------------------------------------------------------------
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
                    if ( m_imageOnglet != null )
                        m_imageOnglet.Dispose();
                    m_imageOnglet = null;
				}
			}
			base.Dispose( disposing );
		}
		//-------------------------------------------------------------------
		#region Designer generated code
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormListeStandard));
            this.m_panelTotal = new System.Windows.Forms.Panel();
            this.m_panelMilieu = new System.Windows.Forms.Panel();
            this.m_panelListe = new sc2i.win32.data.navigation.CPanelListeSpeedStandard();
            this.m_panelGauche = new System.Windows.Forms.Panel();
            this.m_panelDroit = new System.Windows.Forms.Panel();
            this.m_panelBas = new System.Windows.Forms.Panel();
            this.m_panelHaut = new System.Windows.Forms.Panel();
            this.m_panelTotal.SuspendLayout();
            this.m_panelMilieu.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_panelTotal
            // 
            this.m_panelTotal.Controls.Add(this.m_panelMilieu);
            this.m_panelTotal.Controls.Add(this.m_panelGauche);
            this.m_panelTotal.Controls.Add(this.m_panelDroit);
            this.m_panelTotal.Controls.Add(this.m_panelBas);
            this.m_panelTotal.Controls.Add(this.m_panelHaut);
            this.m_panelTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelTotal.Location = new System.Drawing.Point(0, 0);
            this.m_panelTotal.Name = "m_panelTotal";
            this.m_panelTotal.Size = new System.Drawing.Size(598, 420);
            this.m_extStyle.SetStyleBackColor(this.m_panelTotal, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelTotal, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelTotal.TabIndex = 2;
            // 
            // m_panelMilieu
            // 
            this.m_panelMilieu.Controls.Add(this.m_panelListe);
            this.m_panelMilieu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelMilieu.Location = new System.Drawing.Point(0, 0);
            this.m_panelMilieu.Name = "m_panelMilieu";
            this.m_panelMilieu.Size = new System.Drawing.Size(598, 420);
            this.m_extStyle.SetStyleBackColor(this.m_panelMilieu, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelMilieu, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelMilieu.TabIndex = 3;
            // 
            // m_panelListe
            // 
            this.m_panelListe.AffectationsPourNouveauxElements = ((System.Collections.Generic.IEnumerable<sc2i.formulaire.CAffectationsProprietes>)(resources.GetObject("m_panelListe.AffectationsPourNouveauxElements")));
            this.m_panelListe.AllowArbre = true;
            this.m_panelListe.AllowCustomisation = true;
            this.m_panelListe.AllowSerializePreferences = true;
            this.m_panelListe.BackColor = System.Drawing.Color.White;
            this.m_panelListe.ContexteUtilisation = "";
            this.m_panelListe.ControlFiltreStandard = null;
            this.m_panelListe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelListe.ElementSelectionne = null;
            this.m_panelListe.EnableCustomisation = true;
            this.m_panelListe.FiltreDeBase = null;
            this.m_panelListe.FiltreDeBaseEnAjout = false;
            this.m_panelListe.FiltrePrefere = null;
            this.m_panelListe.FiltreRapide = null;
            this.m_panelListe.HasImages = false;
            this.m_panelListe.ListeObjets = null;
            this.m_panelListe.Location = new System.Drawing.Point(0, 0);
            this.m_panelListe.LockEdition = false;
            this.m_panelListe.ModeQuickSearch = false;
            this.m_panelListe.ModeSelection = false;
            this.m_panelListe.MultiSelect = true;
            this.m_panelListe.Name = "m_panelListe";
            this.m_panelListe.Navigateur = null;
            this.m_panelListe.ObjetReferencePourAffectationsInitiales = null;
            this.m_panelListe.ProprieteObjetAEditer = null;
            this.m_panelListe.QuickSearchText = "";
            this.m_panelListe.ShortIcons = false;
            this.m_panelListe.Size = new System.Drawing.Size(598, 420);
            this.m_extStyle.SetStyleBackColor(this.m_panelListe, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelListe, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelListe.TabIndex = 1;
            this.m_panelListe.TrierAuClicSurEnteteColonne = true;
            this.m_panelListe.UseCheckBoxes = false;
            this.m_panelListe.AfterValideCreateObjetDonnee += new sc2i.data.ObjetDonneeEventHandler(this.m_panelListe_AfterValideCreateObjetDonnee);
            this.m_panelListe.OnNewObjetDonnee += new sc2i.win32.data.navigation.OnNewObjetDonneeEventHandler(this.m_panelListe_OnNewObjetDonnee);
            this.m_panelListe.OnObjetDoubleClick += new System.EventHandler(this.m_panelListe_OnObjetDoubleClick);
            this.m_panelListe.Load += new System.EventHandler(this.m_panelListe_Load);
            // 
            // m_panelGauche
            // 
            this.m_panelGauche.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_panelGauche.Location = new System.Drawing.Point(0, 0);
            this.m_panelGauche.Name = "m_panelGauche";
            this.m_panelGauche.Size = new System.Drawing.Size(0, 420);
            this.m_extStyle.SetStyleBackColor(this.m_panelGauche, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelGauche, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelGauche.TabIndex = 1;
            // 
            // m_panelDroit
            // 
            this.m_panelDroit.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_panelDroit.Location = new System.Drawing.Point(598, 0);
            this.m_panelDroit.Name = "m_panelDroit";
            this.m_panelDroit.Size = new System.Drawing.Size(0, 420);
            this.m_extStyle.SetStyleBackColor(this.m_panelDroit, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelDroit, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelDroit.TabIndex = 2;
            // 
            // m_panelBas
            // 
            this.m_panelBas.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_panelBas.Location = new System.Drawing.Point(0, 420);
            this.m_panelBas.Name = "m_panelBas";
            this.m_panelBas.Size = new System.Drawing.Size(598, 0);
            this.m_extStyle.SetStyleBackColor(this.m_panelBas, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelBas, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelBas.TabIndex = 2;
            // 
            // m_panelHaut
            // 
            this.m_panelHaut.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_panelHaut.Location = new System.Drawing.Point(0, 0);
            this.m_panelHaut.Name = "m_panelHaut";
            this.m_panelHaut.Size = new System.Drawing.Size(598, 0);
            this.m_extStyle.SetStyleBackColor(this.m_panelHaut, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelHaut, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelHaut.TabIndex = 0;
            // 
            // CFormListeStandard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(598, 420);
            this.Controls.Add(this.m_panelTotal);
            this.Name = "CFormListeStandard";
            this.m_extStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.Load += new System.EventHandler(this.CFormListeStandard_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CFormListeStandard_KeyDown);
            this.m_panelTotal.ResumeLayout(false);
            this.m_panelMilieu.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		//-------------------------------------------------------------------
		public int NbreElements
		{
			get
			{
				if (m_listeObjets == null)
					return 0;
				CListeObjetsDonnees liste = m_listeObjets;
				liste.Filtre = m_filtre;
				return liste.Count;
			}
		}
		//-------------------------------------------------------------------
		public bool ModeSelection
		{
			get
			{
				return m_bModeSelection;
			}
			set
			{
				m_bModeSelection = value;
				m_panelListe.ModeSelection = value;
				/*this.BoutonAjouterVisible = !value;
				this.BoutonModifierVisible = !value;
				this.BoutonSupprimerVisible = !value;
				if (value)
					this.FormBorderStyle = FormBorderStyle.FixedDialog;*/
			}
		}

        //-------------------------------------------------------------------
        [Browsable(false)]
        public IEnumerable<CAffectationsProprietes> AffectationsPourNouveauxElements
        {
            get
            {
                return m_panelListe.AffectationsPourNouveauxElements;
            }
            set
            {
                m_panelListe.AffectationsPourNouveauxElements = value;
            }
        }

        //-------------------------------------------------------------------
        [Browsable(false)]
        public object ObjetReferencePourAffectationsInitiales
        {
            get
            {
                return m_panelListe.ObjetReferencePourAffectationsInitiales;
            }
            set
            {
                m_panelListe.ObjetReferencePourAffectationsInitiales = value;
            }
        }

		//---------------------------------------------------------------------------
		public CObjetDonneeAIdNumerique ObjetDoubleClicked
		{
			get
			{
				if(m_bModeSelection)
				{
					if (m_listeObjets.Count==1)
						return (CObjetDonneeAIdNumerique) m_listeObjets[0];
                    
					CObjetDonneeAIdNumerique obj = m_panelListe.ObjetDoubleClicked;
					return obj;
				}
				return null;
			}
		}

		//-------------------------------------------------------------------
		//Stocke le filtre de base
		private bool m_bFiltreDeBaseStock = false;
		private CFiltreData m_filtreDeBaseAvantClasseDerivee = null;
		private void PrivateInitPanel()
		{
			if ( !m_bFiltreDeBaseStock )
				m_filtreDeBaseAvantClasseDerivee = m_panelListe.FiltreDeBase;
			else
				m_panelListe.FiltreDeBase = m_filtreDeBaseAvantClasseDerivee;
			m_bFiltreDeBaseStock = true;
			m_panelListe.FiltreDeBaseEnAjout = true;
			InitPanel();
			m_panelListe.FiltreDeBaseEnAjout = false;
		}
			
		//-------------------------------------------------------------------
		protected virtual void InitPanel()
		{
		}
		//-------------------------------------------------------------------
		protected virtual void CFormListeStandard_Load(object sender, System.EventArgs e)
		{
            //La traduction est lancée par CFormMaxiSansMenu
            
            
            if (DesignMode)
				return;
			PrivateInitPanel();

			if ( m_strTitreForce.Trim() != "" )
				CSc2iWin32DataNavigation.Navigateur.TitreFenetreEnCours = m_strTitreForce;
			/*else
				AffecterTitre();*/
		}

        public event ContexteFormEventHandler AfterGetContexte;
        public event ContexteFormEventHandler AfterInitFromContexte;

		//-------------------------------------------------------------------
		public virtual CContexteFormNavigable GetContexte()
		{
			CContexteFormNavigable ctx = new CContexteFormNavigable(GetType() );
			//ctx["ListeObjets"] = CStockeurObjetPersistant.GetPersistantData(m_listeObjets);
			CStringSerializer serializer = new CStringSerializer(ModeSerialisation.Ecriture);
			I2iSerializable obj = m_listeObjets;
			serializer.TraiteObject ( ref obj );
			ctx["ListeObjets"] = serializer.String;
			ctx["CONTEXTE_UTILISATION"] = ContexteUtilisation;
			m_panelListe.FillContexte ( ctx );
            ctx["FILTRE_DE_BASE"] = m_filtreDeBaseAvantClasseDerivee;
			if ( AfterGetContexte != null )
                AfterGetContexte ( this,ctx );
			return ctx;
		}
		//-------------------------------------------------------------------
		public virtual CResultAErreur InitFromContexte(CContexteFormNavigable ctx)
		{
			CResultAErreur result = CResultAErreur.True;
			m_contexte = ctx;
			if ( ctx["CONTEXTE_UTILISATION"] != null )
				ContexteUtilisation = ctx["CONTEXTE_UTILISATION"].ToString();
			/*m_listeObjets = (CListeObjetsDonnees)CStockeurObjetPersistant.AlloueFromPersistantData(
				(byte[])ctx["ListeObjets"], CSc2iWin32DataClient.ContexteCourant );*/
			CStringSerializer serializer = new CStringSerializer((string)ctx["ListeObjets"], ModeSerialisation.Lecture);
			I2iSerializable obj = null;
			result = serializer.TraiteObject ( ref obj,CSc2iWin32DataClient.ContexteCourant );
			if ( !result )
			{
				System.Console.WriteLine (I.T("Erreur de chargement de la liste|30005")+result.Erreur.ToString());
				m_listeObjets = null;
			}
			else
				m_listeObjets = (CListeObjetsDonnees)obj;
            m_filtreDeBaseAvantClasseDerivee = ctx["FILTRE_DE_BASE"] as CFiltreData;
            m_bFiltreDeBaseStock = true;
			m_panelListe.InitFromContexte ( ctx );
            if (AfterInitFromContexte != null)
                AfterInitFromContexte(this, ctx);

			return result;
		}

        //------------------------------------------------------------------
        public CResultAErreur Actualiser()
        {
            if (m_listeObjets != null)
                m_listeObjets.Refresh();
            InitPanel();
            return CResultAErreur.True;
        }

		//-------------------------------------------------------------------
		public bool QueryClose()
		{
			return true;
		}
		
        /*//-------------------------------------------------------------------
        private string m_strTitreAffecté = "";
		protected virtual void AffecterTitre()
		{
			CSc2iWin32DataNavigation.Navigateur.TitreFenetreEnCours = "";

		}

        //-------------------------------------------------------------------
        protected virtual void AffecterTitre(string strTitre)
        {
            m_strTitreAffecté = strTitre;
            CSc2iWin32DataNavigation.Navigateur.TitreFenetreEnCours = strTitre;
        }*/

        public event EventHandler OnObjetDoubleClicked;
		public event ObjetDonneeEventHandler AfterValideCreationObjet;
		public event OnNewObjetDonneeEventHandler OnNewObjetDonnee;
		//-------------------------------------------------------------------
		private void m_panelListe_OnObjetDoubleClick(object sender, System.EventArgs e)
		{
			if ( OnObjetDoubleClicked != null )
				OnObjetDoubleClicked ( sender, e );
			/*if (this.ModeSelection)
			{
				this.DialogResult = DialogResult.OK;
				this.Close();
			}*/
		}
		//-------------------------------------------------------------------
		public bool BoutonAjouterVisible
		{
			get
			{
				return m_panelListe.BoutonAjouterVisible;
			}
			set
			{
				m_panelListe.BoutonAjouterVisible = value;
			}
		}

		//-------------------------------------------------------------------
		public bool BoutonModifierVisible
		{
			get
			{
				return m_panelListe.BoutonModifierVisible;
			}
			set
			{
				m_panelListe.BoutonModifierVisible = value;
			}
		}
		//-------------------------------------------------------------------
		public bool BoutonSupprimerVisible
		{
			get
			{
				return m_panelListe.BoutonSupprimerVisible;
			}
			set
			{
				m_panelListe.BoutonSupprimerVisible = value;
			}
		}
		//-------------------------------------------------------------------
		public void AfficherPanelFiltre()
		{
			m_panelListe.AfficherPanelFiltre();
		}


		//-------------------------------------------------------------------
		public void RafraichirListe()
		{
			m_panelListe.ListeObjets.Refresh();
			m_panelListe.RemplirGrille();
		}


		//-------------------------------------------------------------------
		public CFiltreDynamique FiltrePrefere
		{
			get
			{
				return m_panelListe.FiltrePrefere;
			}
			set
			{
				m_panelListe.FiltrePrefere = value;
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
				m_panelListe.ModeQuickSearch = value;
			}
		}

		//-------------------------------------------------------------------
		public CFiltreData FiltreRapide
		{
			get
			{
				return m_panelListe.FiltreRapide;
			}
			set
			{
				m_panelListe.FiltreRapide = value;
			}
		}

		//-------------------------------------------------------------------
		public string QuickSearchText
		{
			get
			{
				return m_panelListe.QuickSearchText;
			}
			set
			{
				m_panelListe.QuickSearchText = value;
			}
		}

		//-------------------------------------------------------------------
		public CFiltreData FiltreDeBase
		{
			get
			{
				return m_panelListe.FiltreDeBase;
			}
			set
			{
				m_panelListe.FiltreDeBase = value;
			}
		}

		//-------------------------------------------------------------------
		/// <summary>
		/// Chaine identifiant le mode d'utilisation de la fenêtre. Cette
		/// châine conditionne les préférences de l'utilisation sur la liste.
		/// </summary>
		public string ContexteUtilisation
		{
			get
			{
				return m_panelListe.ContexteUtilisation;
			}
			set
			{
				m_panelListe.ContexteUtilisation = value;
			}
		}

		//-------------------------------------------------------------------
		public string TitreForce
		{
			get
			{
				return m_strTitreForce;
			}
			set
			{
				m_strTitreForce = value;
			}
		}

		//-------------------------------------------------------------------------
		public CObjetDonnee ElementSelectionne
		{
			get
			{
				return m_panelListe.ElementSelectionne;
			}
		}

		//-------------------------------------------------------------------------
		public CObjetDonnee[] ElementsCoches
		{
			get
			{
				return (CObjetDonnee[])m_panelListe.GetElementsCheckes().ToArray ( typeof ( CObjetDonnee) );
			}
		}

		//-------------------------------------------------------------------------
		public Panel PanelHaut
		{
			get
			{
				return m_panelHaut;
			}
		}

		//-------------------------------------------------------------------------
		public Panel PanelGauche
		{
			get
			{
				return m_panelGauche;
			}
		}

		//-------------------------------------------------------------------------
		public Panel PanelDroit
		{
			get
			{
				return m_panelDroit;
			}
		}

		//-------------------------------------------------------------------------
		public Panel PanelBas
		{
			get
			{
				return m_panelBas;
			}
		}

		//-------------------------------------------------------------------------
		public bool AffichePanelHaut
		{
			get
			{
				return PanelHaut.Height > 0;
			}
			set
			{
				if ( value && PanelHaut.Height == 0 )
					PanelHaut.Height =16;
				if ( !value )
					PanelHaut.Height = 0;
			}
		}

		//-------------------------------------------------------------------------
		public bool AfficherPanelBas
		{
			get
			{
				return PanelBas.Height > 0;
			}
			set
			{
				if ( value && PanelBas.Height == 0 )
					PanelBas.Height = 16;
				if ( !value )
					PanelBas.Height = 0;
			}
		}

		//-------------------------------------------------------------------------
		public bool AffichePanelGauche
		{
			get
			{
				return PanelGauche.Width > 0;
			}
			set
			{
				if ( value && PanelGauche.Width== 0 )
					PanelGauche.Width = 16;
				if ( !value )
					PanelGauche.Width = 0;
			}
		}

		//-------------------------------------------------------------------------
		public bool AfficherPanelDroit
		{
			get
			{
				return PanelDroit.Width > 0;
			}
			set
			{
				if ( value && PanelDroit.Width== 0 )
					PanelDroit.Width = 16;
				if ( !value )
					PanelDroit.Width = 0;
			}
		}

		private void m_panelListe_OnNewObjetDonnee(object sender, CObjetDonnee nouvelObjet, ref bool bCancel)
		{
			if ( OnNewObjetDonnee != null )
				OnNewObjetDonnee ( sender, nouvelObjet, ref bCancel );
		}

		private void m_panelListe_AfterValideCreateObjetDonnee ( object sender, CObjetDonneeEventArgs args )
		{
			if ( AfterValideCreationObjet != null )
				AfterValideCreationObjet ( sender, args );
		}

        private void m_panelListe_Load(object sender, EventArgs e)
        {
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);

        }

        public CObjetDonnee GetObjetQuickSearchSiUnique()
        {
            m_panelListe.ListeObjets = m_listeObjets;
            return m_panelListe.GetObjetQuickSearchSiUnique();            
        }

        //------------------------------------------------
        public virtual Image GetImage()
        {
            if (m_listeObjets != null)
            {
                m_imageOnglet = CalculeNewImage(m_listeObjets.TypeObjets);
                return m_imageOnglet;
            }
            return Resources.view_list;
        }

        //------------------------------------------------
        public static Image CalculeNewImage(Type tp)
        {
            if (tp != null)
            {
                Image img = DynamicClassAttribute.GetImage(tp);
                if (img != null)
                {
                    Bitmap bmp = new Bitmap(16, 16);
                    Graphics g = Graphics.FromImage(bmp);

                    ColorMatrix cm = new ColorMatrix();
                    cm.Matrix33 = 0.75f;
                    ImageAttributes ia = new ImageAttributes();
                    ia.SetColorMatrix(cm);
                    Image imgListe = Resources.view_list;
                    g.DrawImage(Resources.view_list, new Rectangle(0, 0, 16, 16), 0, 0, imgListe.Width, imgListe.Height,
                        GraphicsUnit.Pixel, ia);
                    ia.Dispose();
                    g.DrawImage(img, 4, 4, 12, 12);
                    g.Dispose();
                    return bmp;
                }
            }
            return Resources.view_list;
        }

        
        //------------------------------------------------
        public virtual string GetTitle()
        {
            return I.T("List|20005");
        }




		#region IFormAContexteDonnee Membres

		public CContexteDonnee ContexteDonnee
		{
			get
			{
				if (m_listeObjets != null)
					return m_listeObjets.ContexteDonnee;
				return null;
			}
		}

		#endregion

        private void CFormListeStandard_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F11:
                    Navigateur.ToggleFullScreen();
                    break;
            }
        }
	}
}

