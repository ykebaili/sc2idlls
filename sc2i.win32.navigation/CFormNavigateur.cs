
using System.Collections;
using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using System.Collections.Generic;

using Crownwood.Magic.Controls;
using Crownwood.Magic.Menus;

using sc2i.win32.common;
using sc2i.common;
using sc2i.drawing;

namespace sc2i.win32.navigation
{
	
	/// <summary>
	/// Description résumée de CFormNavigateur.
	/// </summary>
	public class CFormNavigateur : Form
	{
		[DllImport("user32")] 
		public static extern short GetKeyState(int vKey);

		private const int c_nNbNiveauHistorique = 10;
		private System.ComponentModel.IContainer components = null;

        private Size m_tabIconSize = new Size(16, 16);

		//Stocke la première page affichée comme étant la page d'accueil.
		private IFormNavigable m_pageAccueil = null;

		private Crownwood.Magic.Controls.TabbedGroups m_tabs;

		private Hashtable m_tableTabPageToEntreeHistorique = new Hashtable();

		private bool m_bEnableTabs = true;
        private CExtStyle m_extStyle;
        public Panel m_panelForMainTabs;

		private Crownwood.Magic.Controls.TabPage m_pageRecepteurUniverselle = null;
		//---------------------------------------------------------------------------
		public CFormNavigateur()
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();
		}
		//---------------------------------------------------------------------------
		public CFormNavigateur ( IFormNavigable pageAccueil )
		{
			InitializeComponent();
			m_pageAccueil = pageAccueil;
		}


        //---------------------------------------------------------------------------
        public Size TabsIconSize
        {
            get
            {
                return m_tabIconSize;
            }
            set
            {
                m_tabIconSize = value;
            }
        }
		//---------------------------------------------------------------------------
		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (m_pageAccueil != null )
					m_pageAccueil.Dispose();
				m_pageAccueil = null;

				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		//---------------------------------------------------------------------------
		#region Windows Form Designer generated code
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.m_tabs = new Crownwood.Magic.Controls.TabbedGroups();
            this.m_extStyle = new sc2i.win32.common.CExtStyle();
            this.m_panelForMainTabs = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.m_tabs)).BeginInit();
            this.m_panelForMainTabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_tabs
            // 
            this.m_tabs.AllowDrop = true;
            this.m_tabs.AtLeastOneLeaf = true;
            this.m_tabs.BackColor = System.Drawing.Color.LightBlue;
            this.m_tabs.CloseMenuText = "&Fermer";
            this.m_tabs.CloseShortcut = System.Windows.Forms.Shortcut.None;
            this.m_tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tabs.Location = new System.Drawing.Point(0, 0);
            this.m_tabs.MoveNextMenuText = "Entrer dans le groupe &Suivant";
            this.m_tabs.MovePreviousMenuText = "Entre dans le groupe &Précédent";
            this.m_tabs.Name = "m_tabs";
            this.m_tabs.NewHorizontalMenuText = "Nouveau groupe &Horizontal";
            this.m_tabs.NewVerticalMenuText = "Nouveau groupe &Vertical";
            this.m_tabs.PageCloseWhenEmpty = true;
            this.m_tabs.ProminentLeaf = null;
            this.m_tabs.ProminentMenuText = "&En avant";
            this.m_tabs.RebalanceMenuText = "&Reéquiliber les pages";
            this.m_tabs.ResizeBarColor = System.Drawing.Color.LightGray;
            this.m_tabs.Size = new System.Drawing.Size(360, 263);
            this.m_extStyle.SetStyleBackColor(this.m_tabs, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_tabs, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_tabs.TabIndex = 1;
            this.m_tabs.PageCloseRequest += new Crownwood.Magic.Controls.TabbedGroups.PageCloseRequestHandler(this.m_tabs_PageCloseRequest);
            this.m_tabs.PageContextMenu += new Crownwood.Magic.Controls.TabbedGroups.PageContextMenuHandler(this.m_tabs_PageContextMenu);
            this.m_tabs.ActiveLeafChanged += new System.EventHandler(this.m_tabs_ActiveLeafChanged);
            this.m_tabs.TabIndexChanged += new System.EventHandler(this.m_tabs_TabIndexChanged);
            // 
            // m_panelForMainTabs
            // 
            this.m_panelForMainTabs.Controls.Add(this.m_tabs);
            this.m_panelForMainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelForMainTabs.Location = new System.Drawing.Point(0, 0);
            this.m_panelForMainTabs.Name = "m_panelForMainTabs";
            this.m_panelForMainTabs.Size = new System.Drawing.Size(360, 263);
            this.m_extStyle.SetStyleBackColor(this.m_panelForMainTabs, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this.m_panelForMainTabs, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_panelForMainTabs.TabIndex = 2;
            // 
            // CFormNavigateur
            // 
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(360, 263);
            this.Controls.Add(this.m_panelForMainTabs);
            this.KeyPreview = true;
            this.Name = "CFormNavigateur";
            this.m_extStyle.SetStyleBackColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.m_extStyle.SetStyleForeColor(this, sc2i.win32.common.CExtStyle.EnumCouleurs.None);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CFormNavigateur_FormClosing);
            this.Load += new System.EventHandler(this.CFormNavigateur_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CFormNavigateur_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.m_tabs)).EndInit();
            this.m_panelForMainTabs.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		//---------------------------------------------------------------------------
		public IFormNavigable PageEnCours
		{
			get
			{
				if ( TabPageCible != null )
					return ( IFormNavigable )TabPageCible.Control;
				return null;
			}
			/*set
			{
				if ( ActiveTabPage != null )
					m_tableTabPageToPageEnCours[ActiveTabPage] = value;
			}*/
		}
		//---------------------------------------------------------------------------
		public CEntreeHistorique EntreeEnCours
		{
			get
			{
				if ( TabPageCible != null )
					return ( CEntreeHistorique)m_tableTabPageToEntreeHistorique[TabPageCible];
				return null;
			}
			set
			{
				if ( TabPageCible != null )
					m_tableTabPageToEntreeHistorique[TabPageCible] = value;
			}
		}


        //---------------------------------------------------------------------------
        public virtual CResultAErreur EditeElement(object objet, bool bNewOnglet, string strCodeFormulaire)
        {
            return CResultAErreur.True;
        }

		//---------------------------------------------------------------------------
		/// <summary>
		/// Stocke formNavigable dans l'historique
		/// </summary>
		private CEntreeHistorique HistorisePage(IFormNavigable formNavigable, bool bForcerHistorisation)
		{
			if (formNavigable==null)
				return null;
            CEntreeHistorique nouvelleEntree;

			IFormNavigable pageActive = PageEnCours;

			if (formNavigable == pageActive && !bForcerHistorisation && EntreeEnCours != null)
			{
				nouvelleEntree = EntreeEnCours;
			}
			else 
			{
                if (formNavigable == m_pageAccueil)
                    nouvelleEntree = new CEntreeHistoriqueAccueil(formNavigable, I.T("Home|110"));
                else
                    nouvelleEntree = new CEntreeHistorique(TitreFenetreEnCours);
				nouvelleEntree.EntreePrecedente = EntreeEnCours;
				if (EntreeEnCours!=null)
                    EntreeEnCours.EntreeSuivante = nouvelleEntree;
				int nNiveau = c_nNbNiveauHistorique;
				if ( EntreeEnCours != null )
				{
					CEntreeHistorique old = EntreeEnCours.EntreePrecedente;
					while ( nNiveau > 0 && old != null)
					{
						old = old.EntreePrecedente;
						nNiveau--;
					}
					if ( old != null )
					{
						old.EntreeSuivante.EntreePrecedente = null;
					}
				}

			}
			nouvelleEntree.Contexte = formNavigable.GetContexte();

			return nouvelleEntree;
		}

		//---------------------------------------------------------------------------
		protected virtual bool AffichePage(CEntreeHistorique entreeExistante)
		{
			if (entreeExistante==null)
				return false;

			CEntreeHistorique entreeEnCoursAvant = EntreeEnCours;

			bool bHistorisePageEnCours = entreeEnCoursAvant != null && (entreeEnCoursAvant.Contexte != null);

			bool result  = AffichePage(entreeExistante, false, bHistorisePageEnCours);
			if (!result)
				return result;
			if (entreeEnCoursAvant.Contexte == null)
			{
				if (entreeExistante ==  entreeEnCoursAvant.EntreePrecedente || entreeExistante ==  entreeEnCoursAvant.EntreeSuivante)
				{
					if (entreeEnCoursAvant.EntreePrecedente !=null)
						entreeEnCoursAvant.EntreePrecedente.EntreeSuivante = entreeEnCoursAvant.EntreeSuivante;
					if (entreeEnCoursAvant.EntreeSuivante !=null)
						entreeEnCoursAvant.EntreeSuivante.EntreePrecedente = entreeEnCoursAvant.EntreePrecedente;
				}
				else
				{
					if (entreeEnCoursAvant.EntreePrecedente !=null)
                        entreeEnCoursAvant.EntreePrecedente.EntreeSuivante = entreeExistante;
					entreeExistante.EntreePrecedente = EntreeEnCours.EntreePrecedente;
				}
			}
			EntreeEnCours = entreeExistante;
			return result;
		}
		//---------------------------------------------------------------------------
		protected virtual bool AffichePage(CEntreeHistorique entreeExistante, bool bHistoriseNewPage, bool bHistorisePageEnCours)
		{
			bool result;
			result = AffichePage(entreeExistante.GetPage(), bHistoriseNewPage, bHistorisePageEnCours, false);
			return result;
		}

		//---------------------------------------------------------------------------
		private Crownwood.Magic.Controls.TabPage TabPageActive
		{
			get
			{
				Crownwood.Magic.Controls.TabPage page = (( Crownwood.Magic.Controls.TabControl )m_tabs.ActiveLeaf.GroupControl).SelectedTab;
				if ( page == null )
					page = CreateOngletDefaut();
				return page;
			}
		}

		//---------------------------------------------------------------------------
		private Crownwood.Magic.Controls.TabPage TabPageCible
		{
			get
			{
				if ( m_pageRecepteurUniverselle != null )
					return m_pageRecepteurUniverselle;
				return TabPageActive;
			}
		}

        //---------------------------------------------------------------------------
        public virtual bool AffichePageDansNouvelOnglet(IFormNavigable formNavigable)
        {
            return AffichePage(formNavigable, true, true, true);
        }
        //---------------------------------------------------------------------------
        public virtual bool AffichePage(IFormNavigable formNavigable)
        {
            return AffichePage(formNavigable, true);
        }

        //---------------------------------------------------------------------------
        public bool AffichagePageSansHistorique(IFormNavigable formNavigable)
        {
            return AffichePage(formNavigable, false);
        }

        //---------------------------------------------------------------------------
        private bool AffichePage(IFormNavigable formNavigable, bool bHistoriseFormNavigable)
        {
            return AffichePage(formNavigable, bHistoriseFormNavigable, true, false);
        }
        //---------------------------------------------------------------------------
		protected bool AffichePage(IFormNavigable formNavigable, bool bHistoriseFormNavigable, bool bHistorisePageEnCours, bool bNouvelOnglet)
		{

            if (formNavigable == null)
                return false;
            

			try
			{
				//((Form)formNavigable).Visible = false;
				using ( CWaitCursor waiter = new CWaitCursor() )
				{
					if (((GetKeyState(0x10) & 0xF000)==0xF000 || bNouvelOnglet)&& m_bEnableTabs)
					{
						//Ouverture dans une nouvelle fenêtre
						Crownwood.Magic.Controls.TabPage page = new Crownwood.Magic.Controls.TabPage("Page");
						m_tabs.ActiveLeaf.TabPages.Add ( page );
						m_tableTabPageToEntreeHistorique[page] = null;
						(( Crownwood.Magic.Controls.TabControl )m_tabs.ActiveLeaf.GroupControl).SelectedTab = page;
                        page.VisibleChanged += new EventHandler(page_VisibleChanged);
					}
                    else
                    {

                    }

					if(PageEnCours != null)
					{
						if (!PageEnCours.QueryClose())
							return false;
					}

					if ( m_pageAccueil == null )
						m_pageAccueil = formNavigable;

					try
					{
						if (bHistorisePageEnCours)
							HistorisePage(PageEnCours, false);
					}
					catch { }

					IFormNavigable oldPageEnCours = PageEnCours;

					Form frm = (Form)formNavigable;
					frm.Size = TabPageCible.ClientSize;

                    
                    m_tabs.SuspendDrawing();
					formNavigable.Navigateur = this;
                    //TabPageCible.SuspendDrawing();
					TabPageCible.Control = (Form)formNavigable;
					//TabPageCible.ResumeDrawing();
                    m_tabs.ResumeDrawing();
                    
                    TabPageCible.Title = formNavigable.GetTitle();
                    SetImage(TabPageCible, formNavigable.GetImage());


					if ( oldPageEnCours != null )
					{
						if ( oldPageEnCours == m_pageAccueil )
							oldPageEnCours.Masquer();
						else
						{	
							//((Form)PageEnCours).Parent.Controls.Remove ( (Form)PageEnCours );
							((Form)oldPageEnCours).Parent = null;
							((Form)oldPageEnCours).Close();
							((Form)oldPageEnCours).Dispose();
						}
					}

					if (bHistoriseFormNavigable)
						EntreeEnCours = HistorisePage(formNavigable, true);

					//PageEnCours = formNavigable;

                    
				}
				((Form)formNavigable).Visible = true;
                ((Form)formNavigable).VisibleChanged += new EventHandler(page_VisibleChanged);
                if (ActivePageChanged != null)
                    ActivePageChanged(formNavigable, null);
                
			}
			catch ( Exception e )
			{
#if DEBUG
				throw (e );
#else
				return false;
#endif
			}
			finally
			{
				m_tabs.ResumeDrawing();
			}

			return true;
		}

        private void SetImage(Crownwood.Magic.Controls.TabPage tabPage, Image image)
        {
            if (tabPage == null)
                return;
            try
            {
                ImageList lst = tabPage.ImageList;
                if (lst != null)
                {
                    tabPage.ImageIndex = -1;
                    tabPage.ImageList = null;
                    lst.Dispose();
                }
                if (image != null)
                {
                    lst = new ImageList();
                    lst.ColorDepth = ColorDepth.Depth32Bit;
                    lst.ImageSize = new Size(16, 16);
                    Image copie = CUtilImage.CreateImageImageResizeAvecRatio ( 
                        image, lst.ImageSize, Color.FromArgb(0, 0, 0, 0));
                    lst.Images.Add(copie);
                    tabPage.ImageList = lst;
                    tabPage.ImageIndex = 0;
                }
            }
            catch 
            {
            }
        }

        void page_VisibleChanged(object sender, EventArgs e)
        {
            Control ctrl = sender as Control;
            if (ctrl != null && ctrl.Visible)
                m_tabs_TabIndexChanged(sender, e);
        }
		//---------------------------------------------------------------------------
		public bool AffichePagePrecedenteAvecDuplication()
		{
			if (EntreeEnCours==null || EntreeEnCours.EntreePrecedente == null )
				return false;

			bool result  = AffichePage(EntreeEnCours.EntreePrecedente,true,true);
			return result;
		}
		//---------------------------------------------------------------------------
		public bool AffichePagePrecedente()
		{
			if (EntreeEnCours==null)
				return false;
			bool result = AffichePage( EntreeEnCours.EntreePrecedente );
			return result;
		}
		//---------------------------------------------------------------------------
		public bool AffichePagePrecedenteSansHistorisation()
		{
            if (EntreeEnCours == null || EntreeEnCours.EntreePrecedente == null)
            {
                if (m_tabs.RootSequence.Count == 1)
                {
                    TabGroupLeaf leaf = m_tabs.FirstLeaf();
                    if (leaf.TabPages.Count == 1)
                    {
                        AffichePageAccueil();
                        return true;
                    }
                    else
                    {
                        if (TabPageCible != null)
                            return DeleteOnglet(TabPageCible);
                    }
                } 
                
                
            }

			bool result  = AffichePage(EntreeEnCours.EntreePrecedente,false,false);
			return result;
		}
        //---------------------------------------------------------------------------
		public bool AffichePageSuivante()
		{
			if (EntreeEnCours==null)
				return false;
			/*
			if (EntreeEnCours.EntreeSuivante==null)
				return false;
			*/
			AffichePage( EntreeEnCours.EntreeSuivante );
			return true;
		}


		//---------------------------------------------------------------------------
		public void AffichePageAccueil()
		{
			if ( m_pageAccueil != null )
			{
				AffichePage ( m_pageAccueil );
			}
		}

        //---------------------------------------------------------------------------
        private void CFormNavigateur_Load(object sender, System.EventArgs e)
		{
            // Lance la traduction du formulaire
            sc2i.win32.common.CWin32Traducteur.Translate(this);

            this.m_tabs.CloseMenuText = I.T("&Close|103");
            this.m_tabs.MoveNextMenuText = I.T("Enter in &Next group|104");
            this.m_tabs.MovePreviousMenuText = I.T("Enter in &Previous group|105");
            this.m_tabs.NewHorizontalMenuText = I.T("New &Horizontal group|106");
            this.m_tabs.NewVerticalMenuText = I.T("New &Vertical group|107");
            this.m_tabs.ProminentMenuText = I.T("In &Front|108");
            this.m_tabs.RebalanceMenuText = I.T("&Equilibrate pages|109");

            m_tabs.ActiveLeaf.GroupControl.BackColor = Color.FromArgb(189, 189, 255);
            
			if ( TabPageActive == null )
				CreateOngletDefaut();
		}

        void GroupControl_Paint(object sender, PaintEventArgs e)
        {
            
        }

        //---------------------------------------------------------------------------
        private Crownwood.Magic.Controls.TabPage CreateOngletDefaut()
		{
			Crownwood.Magic.Controls.TabPage page = new Crownwood.Magic.Controls.TabPage("Page");
			m_tableTabPageToEntreeHistorique[page] = null;
			m_tabs.FirstLeaf().TabPages.Add( page );
			m_tabs.BringToFront();
			return page;
		}
        //---------------------------------------------------------------------------
        private bool DeleteOnglet(Crownwood.Magic.Controls.TabPage pageASupprimer)
        {
            if (pageASupprimer != null && m_tabs.FirstLeaf().TabPages.Contains(pageASupprimer))
            {
				try
				{
					m_tabs.FirstLeaf().TabPages.Remove(pageASupprimer);
				}
				catch { }
                return true;
            }
            return false;
        }

        //---------------------------------------------------------------------------

		private Crownwood.Magic.Controls.TabPage m_pageContextMenu = null;
		private void m_tabs_PageContextMenu(Crownwood.Magic.Controls.TabbedGroups tg, Crownwood.Magic.Controls.TGContextMenuEventArgs e)
		{
			e.ContextMenu.MenuCommands.Add ( new Crownwood.Magic.Menus.MenuCommand (I.T("New tab|101"), new EventHandler(m_menuNouvelOnglet_Click) ) );
			m_pageContextMenu = e.TabPage;
			
			MenuCommand command = new Crownwood.Magic.Menus.MenuCommand (I.T("Direct new pages to this tab|102"),
				new EventHandler(OnOrienterNouveauxIci) );
			command.Checked = m_pageRecepteurUniverselle == m_pageContextMenu;
			e.ContextMenu.MenuCommands.Add ( command );
		}

		private void m_menuNouvelOnglet_Click(object sender, System.EventArgs e)
		{
			if (m_bEnableTabs )
			{
				Crownwood.Magic.Controls.TabPage page = new Crownwood.Magic.Controls.TabPage("Page");
				m_tabs.ActiveLeaf.TabPages.Add ( page );
				m_tableTabPageToEntreeHistorique[page] = null;
			
				CEntreeHistorique entree = null;
				if ( m_pageContextMenu != null )
					entree = (CEntreeHistorique)m_tableTabPageToEntreeHistorique[m_pageContextMenu];
				(( Crownwood.Magic.Controls.TabControl )m_tabs.ActiveLeaf.GroupControl).SelectedTab = page;
				if ( entree != null )
					AffichePage ( entree, true, false );
			}
		}

		private void OnOrienterNouveauxIci ( object sender, EventArgs args )
		{
			if ( m_pageRecepteurUniverselle != m_pageContextMenu )
				m_pageRecepteurUniverselle = m_pageContextMenu;
			else
				m_pageRecepteurUniverselle = null;
		}


		private void m_tabs_PageCloseRequest(Crownwood.Magic.Controls.TabbedGroups tg, Crownwood.Magic.Controls.TGCloseRequestEventArgs e)
		{
			if ( m_tabs.RootSequence.Count == 1 )
			{
				TabGroupLeaf leaf = m_tabs.FirstLeaf();
				if ( leaf.TabPages.Count == 1 )
				{
					CFormAlerte.Afficher(I.T("Impossible to close the unique thumbnail of the application|100"), EFormAlerteType.Erreur);
					e.Cancel = true;
				}
				else
					e.Cancel = false;
			}
		}

        public event EventHandler ActivePageChanged;

		bool m_bIsChangingTitrePrincipal = false;
        //---------------------------------------------------------------------------
        private void m_tabs_TabIndexChanged(object sender, System.EventArgs e)
		{
			m_bIsChangingTitrePrincipal = true;
			if ( TabPageActive != null )
				TitreFenetreEnCours = TabPageActive.Title;
			m_bIsChangingTitrePrincipal = false;
            if (ActivePageChanged != null)
                ActivePageChanged(TabPageActive.Control, null);
		}

        //---------------------------------------------------------------------------
        TabGroupLeaf m_lastLeaf = null;
        private void m_tabs_ActiveLeafChanged(object sender, System.EventArgs e)
		{
            //if (m_lastLeaf != null)
            //    m_lastLeaf.GroupControl.BackColor = Color.LightGray;
            //if (m_tabs.ActiveLeaf != null)
            //{
            //    m_tabs.ActiveLeaf.GroupControl.BackColor = Color.LightGray;
            //    m_lastLeaf = m_tabs.ActiveLeaf;
            //}
		}

        //---------------------------------------------------------------------------

		
		//---------------------------------------------------------------------------
		public virtual string TitreFenetreEnCours
		{
			get
			{
                if (TabPageCible != null)
                    return TabPageCible.Title;
				return "";
			}
			set
			{
				if ( !m_bIsChangingTitrePrincipal && TabPageCible != null )
					TabPageCible.Title = value;
			}
		}

		//---------------------------------------------------------------------------
		public IFormNavigable PageAccueil
		{
			get
			{
				return m_pageAccueil;
			}
		}

		//---------------------------------------------------------------------------
		public bool EnableTabs
		{
			get
			{
				return m_bEnableTabs;
			}
			set
			{
				m_bEnableTabs = value;
				if ( value )
					m_tabs.DisplayTabMode = Crownwood.Magic.Controls.TabbedGroups.DisplayTabModes.ShowAll;
				else
					m_tabs.DisplayTabMode = Crownwood.Magic.Controls.TabbedGroups.DisplayTabModes.HideAll;

			}
		}

		//---------------------------------------------------------------------------
		protected IFormNavigable[] FormesOuvertes
		{
			get
			{
				List<IFormNavigable> lst = new List<IFormNavigable>();
				foreach ( Crownwood.Magic.Controls.TabPage page in ((Crownwood.Magic.Controls.TabControl)m_tabs.ActiveLeaf.GroupControl).TabPages )
					if ( page.Control is IFormNavigable )
						lst.Add ((IFormNavigable)page.Control);
				return lst.ToArray();
			}
		}

		//---------------------------------------------------------------------------
		protected bool CloseForm(IFormNavigable form)
		{
			foreach (Crownwood.Magic.Controls.TabPage page in ((Crownwood.Magic.Controls.TabControl)m_tabs.ActiveLeaf.GroupControl).TabPages)
				if (page.Control == form)
				{
					if (form.QueryClose())
					{
						((Crownwood.Magic.Controls.TabControl)m_tabs.ActiveLeaf.GroupControl).TabPages.Remove(page);
						page.Dispose();
						return true;
					}
				}
			return false;
		}

        private void CFormNavigateur_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.Cancel)
                return;
            foreach (Crownwood.Magic.Controls.TabPage page in ((Crownwood.Magic.Controls.TabControl)m_tabs.ActiveLeaf.GroupControl).TabPages)
            {
                IFormNavigable form = page.Control as IFormNavigable;
                if (!form.QueryClose())
                {
                    e.Cancel = true;
                    return;
                }
            }

        }

        //-----------------------------------------------------------------
        public bool QueryClose()
        {
            ArrayList lst = new ArrayList(((Crownwood.Magic.Controls.TabControl)m_tabs.ActiveLeaf.GroupControl).TabPages);
            foreach (Crownwood.Magic.Controls.TabPage page in lst)
            {
                IFormNavigable form = page.Control as IFormNavigable;
                if (!form.QueryClose())
                    return false;
            }
            return true;
        }

        //-----------------------------------------------------------------
        public static CFormNavigateur FindNavigateur(Control ctrl)
        {
            if (ctrl == null)
                return null;
            if (ctrl is CFormNavigateur)
                return (CFormNavigateur)ctrl;
            return FindNavigateur(ctrl.Parent);
        }

        //-----------------------------------------------------------------
        public virtual void ToggleFullScreen()
        { }

        //-----------------------------------------------------------------
        private void CFormNavigateur_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
                ToggleFullScreen();
        }

	}
}
