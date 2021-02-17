using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using sc2i.expression;
using sc2i.common;
using System.Collections;
using sc2i.win32.common;
using sc2i.formulaire;
using sc2i.common.Restrictions;



namespace sc2i.formulaire.win32.controles2iWnd
{
	public partial class CWndFor2iZoneMultiple : UserControl, IControleWndFor2iWnd, IControlALockEdition
	{
		private C2iWndZoneMultiple m_zoneMultiple = null;
		private object m_elementEdite = null;
		private IFournisseurProprietesDynamiques m_fournisseurProprietes = null;
		private CCreateur2iFormulaireV2 m_createur = null;
        private object m_source = null;

        private bool m_bPreventAddFromRestriction = false;
        private bool m_bPreventDeleteFromRestriction = false;


        public ToolTip Tooltip { get; set; }
		
		//Stocke la liste des éléments que le contrôle a ajouté
		private ArrayList m_listeElementsAdd = new ArrayList();

        private CListeRestrictionsUtilisateurSurType m_listeRestrictionsAppliquées = null;
        private IGestionnaireReadOnlySysteme m_gestionnaireReadOnly = null;
        private CRestrictionUtilisateurSurType m_restrictionSurObjetEditeParent = null;


		public CWndFor2iZoneMultiple()
		{
			InitializeComponent();
		}

		#region IControleWndFor2iWnd Membres

		public void CreateControle(CCreateur2iFormulaireV2 createur, C2iWnd wnd, Control parent, sc2i.expression.IFournisseurProprietesDynamiques fournisseurProprietes)
		{
			C2iWndZoneMultiple zoneMultiple = wnd as C2iWndZoneMultiple;
			if (zoneMultiple == null)
				return;
			m_zoneMultiple = zoneMultiple;
			m_createur = createur;	
			CCreateur2iFormulaireV2.AffecteProprietesCommunes(wnd, this);
			m_fournisseurProprietes = fournisseurProprietes;
            //m_panelTop.Visible = zoneMultiple.HasAddButton;
			m_lnkAdd.Visible = zoneMultiple.HasAddButton && !m_bPreventAddFromRestriction;
            m_tooltip.SetToolTip(m_lnkDernierePage, I.T("Last page|10000"));
            m_tooltip.SetToolTip(m_lnkPremierePage, I.T("First page|10001"));
            m_tooltip.SetToolTip(m_lnkSuivant, I.T("Next page|10002"));
            m_tooltip.SetToolTip(m_lnkPrecedent, I.T("Previous page|10003"));
			parent.Controls.Add(this);
		}

		public C2iWnd WndAssociee
		{
			get { return m_zoneMultiple; }
		}

		public Control Control
		{
			get { return this; }
		}

		//-------------------------------------------------------
		public CResultAErreur OnDeleteElement(object element)
		{
			CResultAErreur result = CResultAErreur.True;
			if (m_zoneMultiple.DeleteConfirmMessage.Trim() != "")
			{
				if (CFormAlerte.Afficher(m_zoneMultiple.DeleteConfirmMessage, EFormAlerteType.Question) == DialogResult.No)
					return result;
			}
			IAllocateurSupprimeurElements supprimeur = m_elementEdite as IAllocateurSupprimeurElements;
			if ( supprimeur != null )
			{
				MajChamps(false);
                if (m_lstObjets != null)
                {
                    try
                    {
                        m_listeElementsAdd.Remove(element);
                        m_lstObjets.Remove(element);
                        if (Source is IList)
                            ((IList)Source).Remove(element);
                    }
                    catch
                    {
                    }
                }
				result = supprimeur.SupprimeElement(element);
                if (!result)
                {
                    m_listeElementsAdd.Add(element);
                    m_lstObjets.Add(element);
                    if (Source is IList)
                        ((IList)Source).Add(element);
                }
                else
                    UpdateFromSource();
			}
            CUtilControlesWnd.DeclencheEvenement(C2iWndZoneMultiple.c_strIdEvenementDeleteElement, this);

			return result;
		}

        private int m_nPageEnCours = 0;
        private int m_nbPages = 0;
        private int m_nbParPage = 0;
        ArrayList m_lstObjets = new ArrayList();

        //-------------------------------------------------------
        public void RefillControl()
        {
            SetElementEdite(EditedElement);
        }

		//-------------------------------------------------------
		public void SetElementEdite(object element)
		{
			m_elementEdite = element;
            CContexteEvaluationExpression ctx = CUtilControlesWnd.GetContexteEval(this, element);
			
            if (m_zoneMultiple.SourceFormula != null)
			{
				CResultAErreur result = m_zoneMultiple.SourceFormula.Eval(ctx);
				if (result)
				{
                    //IEnumerable datas = result.Data as IEnumerable;
                    object datas = result.Data;
                    Source = datas;
                }
            }
            CUtilControlesWnd.DeclencheEvenement(C2iWndFenetre.c_strIdEvenementOnInit, this);
        }

        private void UpdateFromSource()
        {
            object datas = Source;
            m_lstObjets.Clear();
            m_nbPages = 0;
            m_nPageEnCours = 0;
            
			if (datas != null)
			{
                IEnumerable collection = datas as IEnumerable;
                if (collection != null)
                {
                    foreach (object obj in collection)
                        m_lstObjets.Add(obj);
                }
                else
                {
                    m_lstObjets.Add(datas);
                }
                foreach ( object newElt in m_listeElementsAdd )
                    if ( !m_lstObjets.Contains ( newElt ) )
                        m_lstObjets.Add(newElt);

                // Initialise le nombre de pages
                m_nbParPage = m_zoneMultiple.NumberOfElementsToDisplayPerPage;
                int nbElements = m_lstObjets.Count;
                if (nbElements == 0)
                {
                    m_nbPages = 0;
                    m_nPageEnCours = 0;
                }
                else
                {
                    m_nbPages = ((int)((nbElements - 1) / m_nbParPage)) + 1;
                    m_nPageEnCours = 0;
                }
			}
            m_panelNavigation.Visible = m_nbPages > 1;
            //m_panelTop.Visible = m_panelNavigation.Visible || m_lnkAdd.Visible;
            m_panelTop.Visible = m_nbPages > 1 || m_zoneMultiple.HasAddButton;
            AffichePage(0, false);
			CUtilControlesWnd.DeclencheEvenement(C2iWnd.c_strIdEvenementOnInit, this);
			
		}

        //---------------------------------------------------------------
        public void SetElementEditeSansChangerLesValeursAffichees(object elementEdite)
        {
            m_elementEdite = elementEdite;
            CResultAErreur result = CResultAErreur.True;
            foreach (Control ctrl in m_panelSousFormulaires.Controls)
            {
                CPanelChildElement sousFormulaire = ctrl as CPanelChildElement;
                if (sousFormulaire != null)
                {
                    sousFormulaire.SetElementEditeSansChangerLesValeursAffichees(elementEdite);

                }
            }
        }

        //-------------------------------------------------------------------------
        private bool AffichePage( int nNumPage, bool bMajChampsAvantAffichage )
        {
            if (m_gestionnaireModeEdition.ModeEdition && bMajChampsAvantAffichage)
            {
                CResultAErreur result = MajChamps(true);
                if (!result)
                {
                    CFormAlerte.Afficher(result.Erreur);
                    return false;
                }
            }
            this.SuspendDrawing();
            m_nPageEnCours = nNumPage;
            //m_panelSousFormulaires.SuspendDrawing();
            List<CPanelChildElement> reserveControles = new List<CPanelChildElement>();
            ArrayList lstControles = new ArrayList(m_panelSousFormulaires.Controls);
            foreach (Control ctrl in lstControles)
            {
                CPanelChildElement sousForm = ctrl as CPanelChildElement;
                if (sousForm != null)
                    reserveControles.Add(sousForm);
            }
            CPanelChildElement.DeleteElementDelegate delegueSuppression = null;
            if (m_elementEdite is IAllocateurSupprimeurElements &&
                m_zoneMultiple.HasDeleteButton && !m_bPreventDeleteFromRestriction)
                delegueSuppression = new CPanelChildElement.DeleteElementDelegate(OnDeleteElement);

            Hashtable tableDejaMis = new Hashtable();

            m_lblPageSurNbPages.Text = (m_nPageEnCours+1).ToString() + "/" + m_nbPages.ToString();
            for(int i = m_nPageEnCours * m_nbParPage; i < (m_nPageEnCours * m_nbParPage) + m_nbParPage; i++)
            {
                if (i < m_lstObjets.Count)
                {
                    object data = m_lstObjets[i];
                    try
                    {
                        if (!tableDejaMis.ContainsKey(data))
                        {
                            tableDejaMis[data] = true;
                            CPanelChildElement panel = null;
                            if (reserveControles.Count != 0)
                            {
                                panel = reserveControles[reserveControles.Count - 1];
                                reserveControles.RemoveAt(reserveControles.Count - 1);
                            }
                            else
                            {
                                panel = new CPanelChildElement();
                                panel.SizeChanged += new EventHandler(panelChild_SizeChanged);
                                if (m_gestionnaireReadOnly != null)
                                    m_gestionnaireReadOnly.AddControl(panel);
                            }
                            panel.Init(this, m_zoneMultiple.FormulaireFils, m_createur);
                            m_panelSousFormulaires.Controls.Add(panel);
                            if (m_zoneMultiple.Orientation == EOrientaion.Horizontal)
                            {
                                panel.Dock = DockStyle.Left;
                            }
                            else
                            {
                                panel.Dock = DockStyle.Top;
                            }

                            panel.Visible = true;
                            panel.LockEdition = !m_gestionnaireModeEdition.ModeEdition;
                            panel.OnChangeParentModeEdition(m_gestionnaireModeEdition.ModeEdition);
                            panel.SetElementEdite(data);
                            panel.BringToFront();
                            panel.DelegueSuppression = delegueSuppression;
                        }
                    }
                    catch { }
                }
            }
            //m_panelSousFormulaires.ResumeDrawing();

            //Supprime les contrôles qui ne servent plus
            foreach (Control ctrl in reserveControles.ToArray())
            {
                ctrl.Visible = false;
                m_panelSousFormulaires.Controls.Remove(ctrl);
                ctrl.Dispose();
            }
            if (m_listeRestrictionsAppliquées != null && EditedElement != null)
            {
                AppliqueRestriction(
                    m_restrictionSurObjetEditeParent,
                    m_listeRestrictionsAppliquées,
                    m_gestionnaireReadOnly);
            }
            RecalcAutoSize();
            this.ResumeDrawing();
            return true;
        }

        private bool m_bNoSizeEvent = false;
        void panelChild_SizeChanged(object sender, EventArgs e)
        {
            if (m_bNoSizeEvent)
                return;
            try
            {
                RecalcAutoSizeDelayed();
            }
            catch { }

        }

        private void RecalcAutoSizeDelayed()
        {
            m_timerRecalcSize.Stop();
            m_timerRecalcSize.Start();
        }

        //-----------------------------------------------------------------------------
        private void RecalcAutoSize()
        {
            m_timerRecalcSize.Stop();
            m_bNoSizeEvent = true;
            if (m_zoneMultiple.AutoSize )
            {
                int nAutoSize = 0;
                if ( m_zoneMultiple.Orientation == EOrientaion.Vertical ) 
                    nAutoSize = m_panelTop.Visible ? m_panelTop.Height : 0;
                foreach (Control ctrl in m_panelSousFormulaires.Controls)
                {
                    if (m_zoneMultiple.Orientation == EOrientaion.Vertical)
                        nAutoSize += ctrl.Height;
                    else
                        nAutoSize += ctrl.Width;
                }
                if (m_zoneMultiple.Orientation == EOrientaion.Vertical &&
                    m_zoneMultiple.DockStyle != EDockStyle.Fill &&
                    m_zoneMultiple.DockStyle != EDockStyle.Left &&
                    m_zoneMultiple.DockStyle != EDockStyle.Right)
                    Height = nAutoSize;
                if ( m_zoneMultiple.Orientation == EOrientaion.Horizontal &&
                    m_zoneMultiple.DockStyle != EDockStyle.Fill && 
                    m_zoneMultiple.DockStyle != EDockStyle.Top && 
                    m_zoneMultiple.DockStyle != EDockStyle.Bottom )
                    Width = nAutoSize;
            }
            m_bNoSizeEvent = false;
        }

		//-----------------------------------------------------------------------------
        public sc2i.common.CResultAErreur MajChamps(bool bControlerLesValeursAvantValidation)
		{
			CResultAErreur result = CResultAErreur.True;
			foreach (Control ctrl in m_panelSousFormulaires.Controls)
			{
				CPanelChildElement sousFormulaire = ctrl as CPanelChildElement;
				if (sousFormulaire != null)
				{
					CResultAErreur resTmp = sousFormulaire.MajChamps(bControlerLesValeursAvantValidation);
					if (!resTmp)
						result += resTmp;
				}
			}
			return result;
		}


        public bool UpdateEditedElement()
        {
            return MajChamps(true);
        }
            


		public void UpdateValeursCalculees()
		{
			foreach (Control ctrl in m_panelSousFormulaires.Controls)
			{
				CPanelChildElement sousFormulaire = ctrl as CPanelChildElement;
				if (sousFormulaire != null)
				{
					sousFormulaire.UpdateValeursCalculees();
				}
			}
		}

		public IRuntimeFor2iWnd[] Childs
		{
			get
			{
                return new IRuntimeFor2iWnd[0];
			}
		}

		public void AppliqueRestriction(
            CRestrictionUtilisateurSurType restrictionSurObjetEdite,
            CListeRestrictionsUtilisateurSurType listeRestriction,
                IGestionnaireReadOnlySysteme gestionnaireReadOnly)
		{
            if (listeRestriction == null || gestionnaireReadOnly == null)
                return;
            m_listeRestrictionsAppliquées = listeRestriction.Clone() as CListeRestrictionsUtilisateurSurType;
            m_restrictionSurObjetEditeParent = restrictionSurObjetEdite;
            m_gestionnaireReadOnly = gestionnaireReadOnly;
            bool bAllReadOnly = false;
            if ((restrictionSurObjetEdite.RestrictionGlobale & ERestriction.ReadOnly) == ERestriction.ReadOnly)
            {
                gestionnaireReadOnly.SetReadOnly(this, true);
                bAllReadOnly = true;
            }
                //Récupère la restriction sur le type des éléments
            if (m_zoneMultiple != null && m_zoneMultiple.SourceFormula != null)
            {
                C2iExpression source = m_zoneMultiple.SourceFormula;
                Type tp = source.TypeDonnee.TypeDotNetNatif;
                if (tp != null)
                {
                    //Chope la restriction et l'envoie à tous ses fils
                    CRestrictionUtilisateurSurType rest = listeRestriction.GetRestriction(tp);
                    if (bAllReadOnly)//Si readonly forcé sur toute la zone
                        rest.RestrictionUtilisateur |= ERestriction.ReadOnly;
                    m_bPreventAddFromRestriction = (rest.RestrictionGlobale & ERestriction.NoCreate) == ERestriction.NoCreate;
                    m_bPreventDeleteFromRestriction = (rest.RestrictionGlobale & ERestriction.NoDelete) == ERestriction.NoDelete;
                    m_lnkAdd.Visible = !m_bPreventAddFromRestriction && m_zoneMultiple.HasAddButton;
                    m_panelTop.Visible = m_nbPages > 1 || m_zoneMultiple.HasAddButton;
                    foreach (Control ctrl in m_panelSousFormulaires.Controls)
                    {
                        CPanelChildElement sousFormulaire = ctrl as CPanelChildElement;
                        if (sousFormulaire != null)
                        {
                            sousFormulaire.AppliqueRestrictions(rest,
                                listeRestriction,
                                gestionnaireReadOnly);
                            if (m_bPreventDeleteFromRestriction)
                                sousFormulaire.DelegueSuppression = null;
                        }
                    }
                }
            }
		}

		#endregion

		#region IControlALockEdition Membres

		public bool LockEdition
		{
			get
			{
				return !m_gestionnaireModeEdition.ModeEdition;
			}
			set
			{
				if ( value == m_gestionnaireModeEdition.ModeEdition )
					m_listeElementsAdd.Clear();
				m_gestionnaireModeEdition.ModeEdition = !value;
				if (OnChangeLockEdition != null)
					OnChangeLockEdition(this, new EventArgs());
			}
		}

        //-------------------------------------
        public void OnChangeParentModeEdition(bool bModeEdition)
        {
            switch (m_zoneMultiple.LockMode)
            {
                case C2iWnd.ELockMode.EnableOnEdit:
                    LockEdition = !bModeEdition;

                    break;
                case C2iWnd.ELockMode.DisableOnEdit:
                    LockEdition = bModeEdition;
                    break;
            }
            foreach (Control ctrl in m_panelSousFormulaires.Controls)
            {
                CPanelChildElement sousFormulaire = ctrl as CPanelChildElement;
                if (sousFormulaire != null)
                    sousFormulaire.OnChangeParentModeEdition(!LockEdition);
            }
            
        }

		public event EventHandler OnChangeLockEdition;

		#endregion

		private void m_lnkAdd_LinkClicked(object sender, EventArgs e)
		{
			CResultAErreur result = CResultAErreur.True;
			IAllocateurSupprimeurElements allocateur = m_elementEdite as IAllocateurSupprimeurElements;
			if ( m_elementEdite != null  )
			{
				if ( m_zoneMultiple.SourceFormula != null )
				{
					Type tp = m_zoneMultiple.SourceFormula.TypeDonnee.TypeDotNetNatif;
                    CAffectationsProprietes affectations = SelectAffectation();
					if ( tp != null && affectations != null)
					{
						object obj = null;
						try
						{
							if ( allocateur != null )
							{
								result = allocateur.AlloueElement(tp);
								if (result)
									obj = result.Data;
							}
							else
								obj = Activator.CreateInstance (tp);
						}
						catch ( Exception ex )
						{
							result.EmpileErreur(new CErreurException(ex));
						}
						if (obj == null | !result)
						{
							result.EmpileErreur(I.T("Error while allocating element|20003"));
							CFormAfficheErreur.Show(result.Erreur);
							return;
						}
						result = affectations.AffecteProprietes ( obj, m_elementEdite, m_fournisseurProprietes );
						if (!result)
						{
							result.EmpileErreur(I.T("Some values cannot be assigned|20004"));
							CFormAfficheErreur.Show(result.Erreur);
						}
						MajChamps(false);
						if ( !m_listeElementsAdd.Contains ( obj ))
                        {
						    m_listeElementsAdd.Add ( obj );
                            m_lastAddedelement = obj;
                            CUtilControlesWnd.DeclencheEvenement(C2iWndZoneMultiple.c_strIdEvenementAddElement, this);

                        }
                        UpdateFromSource();
					}
				}
			}

        }

        private class CMenuItemAvecAffectation : ToolStripMenuItem
        {
            private CAffectationsProprietes m_affectation = null;

            public CMenuItemAvecAffectation(CAffectationsProprietes affectation)
                : base(affectation.Libelle)
            {
                m_affectation = affectation;
            }

            public CAffectationsProprietes Affectation
            {
                get
                {
                    return m_affectation;
                }
            }
        }
        //---------------------------------------------------------------------
        private CAffectationsProprietes SelectAffectation()
        {
            if (m_elementEdite == null)
                return null;
            List<CAffectationsProprietes> lst = new List<CAffectationsProprietes>();
            CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(m_elementEdite);
            foreach (CAffectationsProprietes aff in m_zoneMultiple.Affectations)
            {
                CResultAErreur result = CResultAErreur.True;
                result.Data = true;
                if (aff.FormuleCondition != null)
                {
                    result = aff.FormuleCondition.Eval(ctx);
                }
                if (result && result.Data is bool && (bool)result.Data)
                    lst.Add(aff);
            }
            if (lst.Count == 1)
                return lst[0];
            if (lst.Count == 0)
                return null;


            CMenuModal menu = new CMenuModal();
            foreach (CAffectationsProprietes affectation in lst)
            {
                CMenuItemAvecAffectation item = new CMenuItemAvecAffectation(affectation);
                menu.Items.Add(item);
            }
            CMenuItemAvecAffectation menuAff = menu.ShowMenu(PointToScreen(new Point(m_lnkAdd.Left, m_lnkAdd.Bottom))) as CMenuItemAvecAffectation;
            if (menuAff != null)
                return menuAff.Affectation;
            return null;
        }

        //---------------------------------------------------------------------
        private object m_lastAddedelement = null;
        public object LastAddedElement
        {
            get
            {
                return m_lastAddedelement;
            }
        }

		//---------------------------------------------------------------------
        public object EditedElement
		{
			get
			{
				return m_elementEdite;
			}
		}

        //---------------------------------------
        private void m_lnkPremierePage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (m_nPageEnCours < 1)
                return;
            AffichePage( 0, true );

        }

        //---------------------------------------
        private void m_lnkPrecedent_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (m_nPageEnCours < 1)
                return;
            AffichePage(m_nPageEnCours - 1, true);

        }

        //---------------------------------------
        private void m_lnkSuivant_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (m_nPageEnCours >= m_nbPages - 1)
                return;
            AffichePage(m_nPageEnCours + 1, true);
        }

        //---------------------------------------
        private void m_lnkDernierePage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (m_nPageEnCours >= m_nbPages - 1)
                return;
            AffichePage(m_nbPages - 1, true);

        }

        //---------------------------------------
        public object Source
        {
            get
            {
                return m_source;
            }
            set
            {
                m_source = value;
                UpdateFromSource();
            }
        }

        private void m_timerRecalcSize_Tick(object sender, EventArgs e)
        {
            m_bNoSizeEvent = true;
            RecalcAutoSize();
            m_bNoSizeEvent = false;
        }

	}
}
