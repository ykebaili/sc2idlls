using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;


using sc2i.data.dynamic;
using sc2i.common;
using Crownwood.Magic.Controls;
using sc2i.formulaire;
using sc2i.win32.common;
using sc2i.multitiers.client;
using System.Text;
using sc2i.data;
using sc2i.formulaire.win32;
using sc2i.common.Restrictions;

namespace sc2i.win32.data.dynamic
{
	/// <summary>
	/// Description résumée de CPanelChampsCustom.
	/// </summary>
    public class CPanelChampsCustom : sc2i.win32.common.C2iTabControl, IControlALockEdition, IControleAGestionRestrictions
    {
        //Liste des pages initialisées avec l'élément en cours
        private Dictionary<Crownwood.Magic.Controls.TabPage, bool> m_tablePagesInit = new Dictionary<Crownwood.Magic.Controls.TabPage, bool>();

        /*//Liste des restrictions pour chaque page
        private Dictionary<Crownwood.Magic.Controls.TabPage, ERestriction> m_tablePageToRestriction = new Dictionary<Crownwood.Magic.Controls.TabPage, ERestriction>();*/
        private CListeRestrictionsUtilisateurSurType m_listeRestrictions = null;
        private IGestionnaireReadOnlySysteme m_gestionnaireReadOnly = null;

        //Liste des C2iWnd pour chaque page
        private Dictionary<Crownwood.Magic.Controls.TabPage, C2iWnd> m_tablePageTo2iWnd = new Dictionary<Crownwood.Magic.Controls.TabPage, C2iWnd>();

        private Dictionary<Crownwood.Magic.Controls.TabPage, int> m_tablePageToIdFormulaire = new Dictionary<Crownwood.Magic.Controls.TabPage, int>();

        private const string c_strNomTableChampsValeurs = "TableChampsValeurs";
        private const string c_strColChamp = "Champ";
        private const string c_strColChampNom = "Nom";
        private const string c_strColChampValeur = "Valeur";


        //Id de formulaire->TabPage
        private Dictionary<int, Crownwood.Magic.Controls.TabPage> m_tableIdFormulaireToTabPage = new Dictionary<int,Crownwood.Magic.Controls.TabPage>();

        //tabPage->CCreateur2iFormulaire
        private Dictionary<Crownwood.Magic.Controls.TabPage, CCreateur2iFormulaireObjetDonnee> m_tableCreateurs = new Dictionary<Crownwood.Magic.Controls.TabPage,CCreateur2iFormulaireObjetDonnee>();
        private bool m_bIsLock = false;
        private IObjetDonnee m_elementEdite;
        private List<CFormulaire> m_listeFormulaires = new List<CFormulaire>();
        private System.Windows.Forms.ToolTip m_champTooltip;
        private System.ComponentModel.IContainer components;

        public CPanelChampsCustom()
        {
            // Cet appel est requis par le Concepteur de formulaires Windows.Forms.
            InitializeComponent();

            // TODO : ajoutez les initialisations après l'appel à InitForm

        }

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
            this.m_champTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // CPanelChampsCustom
            // 
            this.Name = "CPanelChampsCustom";
            this.Size = new System.Drawing.Size(720, 408);
            this.SizeChanged += new System.EventHandler(this.CPanelChampsCustom_SizeChanged);
            this.SelectionChanged += new System.EventHandler(this.CPanelChampsCustom_SelectionChanged);
            this.BackColorChanged += new System.EventHandler(this.CPanelChampsCustom_BackColorChanged);
            this.Controls.SetChildIndex(this._rightArrow, 0);
            this.Controls.SetChildIndex(this._leftArrow, 0);
            this.Controls.SetChildIndex(this._closeButton, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// ///////////////////////////////////////////////////
        public IObjetDonnee ElementEdite
        {
            get
            {
                return m_elementEdite;
            }
            set
            {
                m_elementEdite = value;
                if (m_elementEdite is IElementAChamps)
                    m_listeFormulaires = new List<CFormulaire>(((IElementAChamps)m_elementEdite).GetFormulaires());
                UpdateOnglets();
            }
        }

        //----------------------------------------------------------------
        public void Init(IObjetDonnee elementEdite, CFormulaire[] listeFormulaires)
        {
            if (elementEdite != null)
            {
                m_elementEdite = elementEdite;
                m_listeFormulaires = new List<CFormulaire>(listeFormulaires);
                UpdateOnglets();
            }
        }

        public event EventHandler OnChangeLockEdition;
        /// ///////////////////////////////////////////////////
        public bool LockEdition
        {
            get
            {
                return m_bIsLock;
            }
            set
            {
                m_bIsLock = value;
                OnMyChangeLockEdition();
            }
        }

        /// ///////////////////////////////////////////////////
        public bool IsEmpty()
        {
            return TabPages.Count == 0;
        }

        /// ///////////////////////////////////////////////////
        public string Titre
        {
            get
            {
                if (TabPages.Count == 1)
                    return TabPages[0].Title;
                return (I.T("Additional information|30038"));
            }
        }


        /// ///////////////////////////////////////////////////
        private void OnMyChangeLockEdition()
        {
            foreach (Crownwood.Magic.Controls.TabPage page in TabPages)
            {
                CCreateur2iFormulaireObjetDonnee createur = null;
                if ( m_tableCreateurs.TryGetValue(page, out createur))
                {
                    createur.LockEdition = LockEdition;
                }
            }
            if (OnChangeLockEdition != null)
                OnChangeLockEdition(this, new EventArgs());
        }

        /// ///////////////////////////////////////////////////
        public CResultAErreur MAJ_Champs()
        {
            CResultAErreur result = CResultAErreur.True;
            if (m_bIsLock)
                return result;

            foreach ( KeyValuePair<Crownwood.Magic.Controls.TabPage, CCreateur2iFormulaireObjetDonnee> kv in m_tableCreateurs )
            {
                CCreateur2iFormulaireObjetDonnee createur = kv.Value;
                Crownwood.Magic.Controls.TabPage page = kv.Key;
                if (m_tablePagesInit.ContainsKey(page) &&
                    m_tablePagesInit[page])
                {
                    result = createur.MAJ_Champs();
                    if (!result)
                        return result;
                }
            }

            return result;
        }

        private class CSorterFormulaires : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x is CFormulaire && y is CFormulaire)
                {
                    return ((CFormulaire)x).NumeroOrdre.CompareTo(((CFormulaire)y).NumeroOrdre);
                }
                return 0;
            }

        }

        /// ///////////////////////////////////////////////////
        private bool m_bIsInitializing = false;
        public void UpdateOnglets()
        {
            this.SuspendDrawing();
            try
            {
                /*if (!LockEdition)
                {
                    //valide les modifs car on va tout supprimer
                    MAJ_Champs();
                }*/
                Crownwood.Magic.Controls.TabPage pageSelect = this.SelectedTab;
                m_bIsInitializing = true;
                Hashtable tablePagesToHide = new Hashtable();
                ArrayList pages = new ArrayList(TabPages);
                foreach (Crownwood.Magic.Controls.TabPage page in pages)
                {
                    m_tablePagesInit[page] = false;
                    tablePagesToHide[page] = true;
                    TabPages.Remove(page);
                }

                if (m_elementEdite == null)
                    return;

                /* YK : On obtient la session de l'objet en cours d'édition au lieur de la session par défaut */
                CSessionClient session = null;
                IObjetDonneeAIdNumerique objetDonnee = m_elementEdite as IObjetDonneeAIdNumerique;
                if (objetDonnee != null)
                    session = CSessionClient.GetSessionForIdSession(objetDonnee.ContexteDonnee.IdSession);
                else
                    session = CSessionClient.GetSessionUnique();

                ArrayList lstFormulaires = new ArrayList(m_listeFormulaires);
                lstFormulaires.Sort(new CSorterFormulaires());
                //Lit les champs liés et leurs valeurs
                StringBuilder bl = new StringBuilder();
                foreach (CFormulaire formulaire in lstFormulaires)
                {
                    bl.Append(formulaire.Id);
                    bl.Append(",");
                }
                if (bl.Length > 0)
                {
                    bl.Remove(bl.Length - 1, 1);
                    CListeObjetsDonnees lst = new CListeObjetsDonnees(((CFormulaire)lstFormulaires[0]).ContexteDonnee, typeof(CFormulaire));
                    lst.Filtre = new CFiltreData(CFormulaire.c_champId + " in (" + bl.ToString() + ")");
                    lst.AssureLectureFaite();
                    lst.ReadDependances("RelationsChamps", "RelationsChamps.Champ", "RelationsChamps.Champ.ListeValeurs");
                }


                foreach (CFormulaire formulaire in lstFormulaires)
                {

                    Crownwood.Magic.Controls.TabPage page = null;
                    m_tableIdFormulaireToTabPage.TryGetValue ( formulaire.Id, out page );
                    CCreateur2iFormulaireObjetDonnee createur = null;

                    if (page == null)
                    {
                        C2iWndFenetre wnd = formulaire.Formulaire;
                        if (wnd != null)
                        {
                            page = new Crownwood.Magic.Controls.TabPage(wnd.Text);
                            page.Name = formulaire.IdUniversel;
                            if (wnd.Text == "")
                                page.Title = formulaire.Libelle;
                            TabPages.Add(page);
                            if ( m_tableCreateurs.ContainsKey ( page ) )
                                m_tableCreateurs.Remove ( page );
                            m_tablePageTo2iWnd[page] = wnd;
                            m_tableIdFormulaireToTabPage[formulaire.Id] = page;
                            m_tablePageToIdFormulaire[page] = formulaire.Id;

                        }
                    }
                    else
                    {
                        m_tableCreateurs.TryGetValue ( page, out createur );
                    }

                    if (page != null)
                    {
                        if (!TabPages.Contains(page))
                            TabPages.Add(page);
                        tablePagesToHide.Remove(page);
                    }
                }
                

                /*CChampCustom[] champs = ElementEdite.GetChampsHorsFormulaire();
                if ( champs.Length != 0 )
                {
                    UpdatePageChamps( nRest );
                    if ( m_pageGrid != null )
                        tablePagesToHide.Remove(m_pageGrid);
                }*/
                foreach (Crownwood.Magic.Controls.TabPage pageToDel in tablePagesToHide.Keys)
                {
                    if (TabPages.Contains(pageToDel))
                        TabPages.Remove(pageToDel);
                }

                if (TabPages.Count == 1)
                {
                    this.HideTabsMode = Crownwood.Magic.Controls.TabControl.HideTabsModes.HideAlways;
                }
                else
                    this.HideTabsMode = Crownwood.Magic.Controls.TabControl.HideTabsModes.ShowAlways;
                if (TabPages.Contains(pageSelect))
                    SelectedTab = pageSelect;
                if (SelectedTab != null)
                    InitPageWithObjetCourant(SelectedTab);

            }
            finally
            {
                this.ResumeDrawing();
            }
            m_bIsInitializing = false;
        }

        /// ///////////////////////////////////////////////////
        private void CreateControles(Crownwood.Magic.Controls.TabPage page)
        {
            if ( m_tableCreateurs.ContainsKey(page) || ElementEdite == null )
                return;
            C2iWnd wnd = m_tablePageTo2iWnd[page];
            Panel panel = new Panel();
            panel.BackColor = wnd.BackColor;
            panel.ForeColor = wnd.ForeColor;
            panel.Font = wnd.Font;
            panel.Location = new Point(0, 0);
            panel.Size = wnd.Size;
            panel.AutoScroll = true;
            page.Controls.Add(panel);
            panel.SuspendDrawing();
            CCreateur2iFormulaireObjetDonnee createur;

            IObjetDonneeAIdNumerique objetDonnee = ElementEdite as IObjetDonneeAIdNumerique;
            if (objetDonnee != null)
                createur = new CCreateur2iFormulaireObjetDonnee(objetDonnee.ContexteDonnee.IdSession);
            else
                createur = new CCreateur2iFormulaireObjetDonnee(CSessionClient.GetSessionUnique().IdSession);

            createur.CreateControlePrincipalEtChilds(panel, wnd, new CFournisseurPropDynStd(true));
            panel.ResumeDrawing();
            panel.Size = page.ClientSize;
            m_tableCreateurs[page] = createur;
            page.ResumeDrawing();
            panel.Dock = DockStyle.Fill;
        }

        /// ///////////////////////////////////////////////////
        private void InitPageWithObjetCourant(Crownwood.Magic.Controls.TabPage page)
        {
            if (m_tablePagesInit.ContainsKey(page) &&
                m_tablePagesInit[page])
                return;

            if (!m_tableCreateurs.ContainsKey(page))
                CreateControles(page);

            CCreateur2iFormulaireObjetDonnee createur = null;
            m_tableCreateurs.TryGetValue ( page, out createur );
            createur.LockEdition = m_bIsLock;
            if (createur != null)
                createur.ElementEdite = m_elementEdite;
            m_tablePagesInit[page] = true;
            AppliqueRestrictions(m_listeRestrictions, m_gestionnaireReadOnly);
            

        }





        /// ///////////////////////////////////////////////////
        private void CPanelChampsCustom_BackColorChanged(object sender, System.EventArgs e)
        {

        }

        private void CPanelChampsCustom_SizeChanged(object sender, System.EventArgs e)
        {

        }

        /// ///////////////////////////////////////////////////
        private void CPanelChampsCustom_SelectionChanged(object sender, EventArgs e)
        {
            Crownwood.Magic.Controls.TabPage page = SelectedTab;
            if (!m_bIsInitializing && page != null)
                InitPageWithObjetCourant(page);
        }


        /// ///////////////////////////////////////////////////
        public void AppliqueRestrictions(CListeRestrictionsUtilisateurSurType restrictions, IGestionnaireReadOnlySysteme gestionnaireReadOnly)
        {
            if (restrictions == null || gestionnaireReadOnly == null)
                return;
            m_listeRestrictions = restrictions.Clone() as CListeRestrictionsUtilisateurSurType; 
            m_gestionnaireReadOnly = gestionnaireReadOnly;

            //Gère la visibilité des formulaires
            if (m_elementEdite != null)
            {
                CRestrictionUtilisateurSurType rest = m_listeRestrictions.GetRestriction(m_elementEdite.GetType());
                rest.ApplyToObjet(m_elementEdite);
                if (rest.HasRestrictions)
                {
                    foreach (KeyValuePair<Crownwood.Magic.Controls.TabPage, int> kv in m_tablePageToIdFormulaire)
                    {
                        CFormulaire formulaire = new CFormulaire(m_elementEdite.ContexteDonnee);
                        if (formulaire.ReadIfExists(kv.Value))
                        {
                            ERestriction r = rest.GetRestriction(formulaire.CleRestriction);
                            if ((r & ERestriction.Hide) == ERestriction.Hide)
                            {
                                if (TabPages.Contains(kv.Key))
                                {
                                    TabPages.Remove(kv.Key);
                                    m_tablePagesInit[kv.Key] = false;
                                }
                            }
                        }
                    }
                }
            }
            

            //Update les pages initialisées
            foreach (KeyValuePair<Crownwood.Magic.Controls.TabPage, bool> kv in m_tablePagesInit)
            {
                if (kv.Value)
                {
                    CCreateur2iFormulaireObjetDonnee createur = null;
                    if (m_tableCreateurs.TryGetValue(kv.Key, out createur))
                    {
                        CRestrictionUtilisateurSurType oldRest = null;

                        if (m_elementEdite != null)
                        {
                            CRestrictionUtilisateurSurType restriction = m_listeRestrictions.GetRestriction(m_elementEdite.GetType());
                            oldRest = m_listeRestrictions.GetRestriction(m_elementEdite.GetType());
                            restriction.ApplyToObjet(m_elementEdite);
                            int nIdFormulaire;
                            if (m_tablePageToIdFormulaire.TryGetValue(kv.Key, out nIdFormulaire))
                            {
                                CFormulaire formulaire = new CFormulaire(m_elementEdite.ContexteDonnee);
                                if (formulaire.ReadIfExists(nIdFormulaire))
                                {
                                    ERestriction rest = restriction.GetRestriction(formulaire.CleRestriction);
                                    if ((rest & ERestriction.ReadOnly) == ERestriction.ReadOnly)
                                    {
                                        restriction.AddRestrictionsHorsPriorite(new CRestrictionUtilisateurSurType(restriction.TypeAssocie, ERestriction.ReadOnly));
                                        //force readonly sur tous les contrôles
                                        restriction.RestrictionUtilisateur = ERestriction.ReadOnly;
                                        m_listeRestrictions.SetRestriction(restriction);
                                    }
                                }
                            }

                        }
                        createur.AppliqueRestrictions(m_listeRestrictions, m_gestionnaireReadOnly);
                        if (oldRest != null)
                            m_listeRestrictions.SetRestriction(oldRest);
                    }   
                }
            }
        }
    }
}
