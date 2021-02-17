using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


using sc2i.formulaire;
using sc2i.common;
using sc2i.expression;
using sc2i.win32.common;
using System.Drawing;
using sc2i.common.Restrictions;


namespace sc2i.formulaire.win32.controles2iWnd
{
	[AutoExec("Autoexec")]
	public class CWndFor2iSlidingPanel : CSlidingZone, IControleWndFor2iWnd
	{
		private object m_elementEdite = null;
		private C2iWndSlidingPanel m_slidingPanel = null;
		private IControleWndFor2iWnd[] m_controlesFils = new IControleWndFor2iWnd[0];
		private C2iPanel m_panelComposants;
		private IFournisseurProprietesDynamiques m_fournisseur = null;

        private CListeRestrictionsUtilisateurSurType m_listeRestrictions = null;
        private IGestionnaireReadOnlySysteme m_gestionnaireReadOnly = null;

        public ToolTip Tooltip { get; set; }

		private CCreateur2iFormulaireV2 m_createur = null;

		//Le Sliding Panel ne crée ses controles qu'une fois qu'il est ouvert
		private bool m_bControlesFilsCrees = false;
        private System.ComponentModel.IContainer components;

		//Le sliding Panel ne transmet l'élément lié que s'il est ouvert
		private bool m_bIsRempliAvecElement = false;

		public CWndFor2iSlidingPanel()
			: base()
		{
			InitializeComponent();
		}

		public static void Autoexec()
		{
			CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndSlidingPanel), typeof(CWndFor2iSlidingPanel));
		}

		#region IControleWndFor2iWnd Membres

		public void CreateControle(
			CCreateur2iFormulaireV2 createur,
			C2iWnd wnd, 
			Control parent, 
			IFournisseurProprietesDynamiques fournisseurProprietes)
		{
			m_createur = createur;
			C2iWndSlidingPanel panel = wnd as C2iWndSlidingPanel;
			if ( panel == null )
				return;
			
			TitleAlignment = panel.TitleAlignement;
			TitleBackColor = panel.TitleBackColor;
			TitleBackColorGradient = panel.TitleGradientBackColor;
			TitleHeight = panel.TitleHeight;
            TitleFont = panel.TilteFont;
			
			bool bOldCollapse = panel.IsCollapsed;
			panel.IsCollapsed = false;
			CCreateur2iFormulaireV2.AffecteProprietesCommunes(panel, this);
			panel.IsCollapsed = bOldCollapse;
			IsCollapse = panel.IsCollapsed;
			parent.Controls.Add(this);
			m_controlesFils = new IControleWndFor2iWnd[0];
			m_bControlesFilsCrees = false;
			m_slidingPanel = panel;
			m_fournisseur = fournisseurProprietes;
			if (!IsCollapse)
			{
				AssureCreationControlesFils();
			}
			CalculeTitre();
            
          
		}


		private void AssureCreationControlesFils()
		{
			if (m_slidingPanel == null)
				return;
			if ( m_bControlesFilsCrees )
				return;
			m_controlesFils = m_createur.CreateChilds(m_slidingPanel, m_panelComposants, m_fournisseur);
            foreach (IControleWndFor2iWnd ctrl in Childs)
            {
                ctrl.WndContainer = this;
                if (m_slidingPanel.AdjustToContent && ctrl.Control != null)
                    ctrl.Control.SizeChanged += new EventHandler(Control_SizeChanged);
                if ( m_gestionnaireReadOnly != null && ctrl.Control != null)
                    m_gestionnaireReadOnly.AddControl ( ctrl.Control );
            }

                    
			m_bControlesFilsCrees = true;
            OnChangeParentModeEdition(!m_createur.LockEdition);
            foreach (IControleWndFor2iWnd ctrl in Childs)
                m_createur.UpdateVisibilityEtEnable(ctrl, EditedElement);
		}

		//-------------------------------------
        public void Control_SizeChanged(object sender, EventArgs e)
        {
            AdjustToContent();
        }

        //-------------------------------------
        public override void  Extend()
        {
            AdjustToContent();
 	        base.Extend();
        }

        //-------------------------------------
        private void AdjustToContent()
        {
            if (m_slidingPanel != null && m_slidingPanel.AdjustToContent)
            {
                if (!this.IsCollapse)
                {
                    int nMaxHeight = 20;
                    foreach (Control ctrl in m_panelComposants.Controls)
                    {
                        nMaxHeight = Math.Max(nMaxHeight, ctrl.Location.Y + ctrl.Size.Height);
                    }
                    ExtendedSize = Math.Min(nMaxHeight, m_slidingPanel.MaxAutosizeHeight)+TitleHeight+2;
                    Height = ExtendedSize;
                }
            }
        }

        //-------------------------------------
        public C2iWnd WndAssociee
		{
			get
			{
				return m_slidingPanel;
			}
		}

		//-------------------------------------
		public Control Control
		{
			get
			{
				return this;
			}
		}

		#endregion

        //-------------------------------------------------------
        public void RefillControl()
        {
            SetElementEdite(EditedElement);
        }
        //--------------------------------------------------
		public void SetElementEdite(object element)
		{
			m_elementEdite = element;
			CalculeTitre();
			if (IsCollapse)
			{
				m_bIsRempliAvecElement = false;
			}
			else
			{
				AssureCreationControlesFils();
				RemplitAvecElementEdite();
			}
            CUtilControlesWnd.DeclencheEvenement(C2iWndFenetre.c_strIdEvenementOnInit, this);
			
		}

        //--------------------------------------------------
        public void SetElementEditeSansChangerLesValeursAffichees(object element)
        {
            m_elementEdite = element;
            foreach (IControleWndFor2iWnd controleFils in m_controlesFils)
                controleFils.SetElementEditeSansChangerLesValeursAffichees(element);
            CUtilControlesWnd.DeclencheEvenement(C2iWndFenetre.c_strIdEvenementOnInit, this);
        }

		private void RemplitAvecElementEdite()
		{
            m_bIsRempliAvecElement = true; 
			foreach (IControleWndFor2iWnd controleFils in m_controlesFils)
				controleFils.SetElementEdite(m_elementEdite);
            foreach (IControleWndFor2iWnd ctrl in Childs)
                m_createur.UpdateVisibilityEtEnable(ctrl, EditedElement);

            if (EditedElement != null && m_listeRestrictions != null)
            {
                CRestrictionUtilisateurSurType rest = m_listeRestrictions.GetRestriction(EditedElement.GetType());
                AppliqueRestriction(rest,
                    m_listeRestrictions,
                    m_gestionnaireReadOnly);
            }
		}

		public CResultAErreur MajChamps(bool bAvecVerification)
		{
			CResultAErreur result = CResultAErreur.True;
			if (!m_bIsRempliAvecElement)
				return result;
			foreach (IControleWndFor2iWnd controleFils in m_controlesFils)
				result += controleFils.MajChamps(bAvecVerification);
			return result;
		}

		public void UpdateValeursCalculees()
		{
			CalculeTitre ( );
			foreach (IControleWndFor2iWnd controleFils in m_controlesFils)
				controleFils.UpdateValeursCalculees();
		}

		
        //-------------------------------------
        public void OnChangeParentModeEdition(bool bModeEdition)
        {
            bool bMyMode = bModeEdition;
            if (WndAssociee != null && WndAssociee.LockMode == C2iWnd.ELockMode.DisableOnEdit)
                bMyMode = !bModeEdition;
            foreach (IControleWndFor2iWnd fils in m_controlesFils)
                    fils.OnChangeParentModeEdition(bMyMode);
        }

		//---------------------------------------------
        public IRuntimeFor2iWnd[] Childs
		{
			get
			{
				return m_controlesFils;
			}
		}

		//---------------------------------------------
        public void AppliqueRestriction(
            CRestrictionUtilisateurSurType restrictionSurObjetEdite,
            CListeRestrictionsUtilisateurSurType listeRestrictions,
            IGestionnaireReadOnlySysteme gestionnaireReadOnly)
		{
            if (listeRestrictions != null)
                m_listeRestrictions = listeRestrictions.Clone() as CListeRestrictionsUtilisateurSurType; 
            if (gestionnaireReadOnly != null)
                m_gestionnaireReadOnly = gestionnaireReadOnly;
                
			foreach (IControleWndFor2iWnd child in Childs)
				child.AppliqueRestriction(restrictionSurObjetEdite,
                    listeRestrictions,
                    gestionnaireReadOnly);
		}

		//---------------------------------------------
		private void CalculeTitre()
		{
			if (m_fournisseur != null && m_slidingPanel != null &&
				m_slidingPanel.TitleFormula != null)
			{
				CContexteEvaluationExpression ctx = CUtilControlesWnd.GetContexteEval(this, m_elementEdite);
				CResultAErreur result = m_slidingPanel.TitleFormula.Eval(ctx);
				if (result && result.Data != null)
				{
					TitleText = result.Data.ToString();
					return;
				}
			}
			TitleText = "";
		}

		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.m_panelComposants = new sc2i.win32.common.C2iPanel(this.components);
            this.SuspendLayout();
            // 
            // m_panelComposants
            // 
            this.m_panelComposants.AutoSize = true;
            this.m_panelComposants.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelComposants.Location = new System.Drawing.Point(0, 18);
            this.m_panelComposants.LockEdition = false;
            this.m_panelComposants.Name = "m_panelComposants";
            this.m_panelComposants.Size = new System.Drawing.Size(237, 180);
            this.m_panelComposants.TabIndex = 1;
            // 
            // CWndFor2iSlidingPanel
            // 
            this.Controls.Add(this.m_panelComposants);
            this.Name = "CWndFor2iSlidingPanel";
            this.OnCollapseChange += new System.EventHandler(this.CWndFor2iSlidingPanel_OnCollapseChange);
            this.Controls.SetChildIndex(this.m_panelComposants, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		private IControleWndFor2iWnd m_ctrlWndParent = null;
        public IWndAContainer WndContainer
		{
			get
			{
				return m_ctrlWndParent;
			}
			set
			{
				m_ctrlWndParent = value as IControleWndFor2iWnd;
			}
		}

        

        private void CWndFor2iSlidingPanel_OnCollapseChange(object sender, EventArgs e)
        {
			if (m_slidingPanel == null)
				return;
			AssureCreationControlesFils();
			if (!m_bIsRempliAvecElement && !IsCollapse)
				RemplitAvecElementEdite();
            CUtilControlesWnd.DeclencheEvenement(C2iWndSlidingPanel.c_strIdEvenementChangeCollapse, this);
        }

		public object EditedElement
		{
			get
			{
				return m_elementEdite;
			}
		}

        //------------------------------------------------
        public bool IsRacineForEvenements
        {
            get
            {
                return false;
            }
        }

        public IElementAProprietesDynamiquesDeportees ConvertToElementAProprietesDynamiquesDeportees()
        {
            return new CEncaspuleurControleWndForFormules(this);
        }

	}
}
