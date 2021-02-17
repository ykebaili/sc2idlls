using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using sc2i.formulaire;
using sc2i.common;
using sc2i.win32.common;
using sc2i.expression;
using Crownwood.Magic.Controls;
using sc2i.common.Restrictions;


namespace sc2i.formulaire.win32.controles2iWnd
{
	[AutoExec ( "Autoexec")]
	public class CWndFor2iTabPage : Crownwood.Magic.Controls.TabPage,IControleWndFor2iWnd
	{
		private IControleWndFor2iWnd[] m_controlesFils = new IControleWndFor2iWnd[0];
		private bool m_bLockEdition = false;
		private C2iWnd m_wndAssociee = null;
		private object m_elementEdite = null;

        public ToolTip Tooltip { get; set; }

		//---------------------------------------------------------------
		public static void Autoexec()
		{
			CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndTabPage), typeof(CWndFor2iTabPage));
		}

		//---------------------------------------------------------------
		public void CreateControle(
			CCreateur2iFormulaireV2 createur,
			C2iWnd wnd, 
			Control parent, 
			IFournisseurProprietesDynamiques fournisseurProprietes)
		{
			C2iWndTabPage tabPage = wnd as C2iWndTabPage;
			CCreateur2iFormulaireV2.AffecteProprietesCommunes(tabPage, this);

            this.Title = tabPage.Text;
            this.Name = tabPage.Text;
			m_controlesFils = createur.CreateChilds(tabPage, this, fournisseurProprietes);
			foreach (IControleWndFor2iWnd ctrl in Childs)
				ctrl.WndContainer = this;
			m_wndAssociee = wnd;
		}

		//-------------------------------------
		public C2iWnd WndAssociee
		{
			get
			{
				return m_wndAssociee;
			}
		}

		//---------------------------------------------------------------
		public new Control Control
		{
			get
			{
				return this;
			}
		}

        //-------------------------------------------------------
        public void RefillControl()
        {
            SetElementEdite(EditedElement);
        }

		//---------------------------------------------------------------
		public void SetElementEdite(object elementEdite)
		{
            m_elementEdite = elementEdite;

            foreach (IControleWndFor2iWnd controleFils in m_controlesFils)
                controleFils.SetElementEdite(elementEdite);
            CUtilControlesWnd.DeclencheEvenement(C2iWndFenetre.c_strIdEvenementOnInit, this);
		}

        //---------------------------------------------------------------
        public void SetElementEditeSansChangerLesValeursAffichees(object elementEdite)
        {
            m_elementEdite = elementEdite;
            foreach (IControleWndFor2iWnd controleFils in m_controlesFils)
                controleFils.SetElementEdite(elementEdite);
        }

		//---------------------------------------------------------------
		public CResultAErreur MajChamps ( bool bAvecVerificationDeDonnees )
		{
			CResultAErreur result = CResultAErreur.True;
            
                foreach (IControleWndFor2iWnd controleFils in m_controlesFils)
                    result += controleFils.MajChamps(bAvecVerificationDeDonnees);
            
			return result;
		}

		//-------------------------------------
		public void UpdateValeursCalculees()
		{
			foreach (IControleWndFor2iWnd controleFils in m_controlesFils)
				controleFils.UpdateValeursCalculees();
		}

		//-------------------------------------
		public bool LockEdition
		{
			get
			{
				return m_bLockEdition;
			}
			set
			{
				m_bLockEdition = value;
				
				if (OnChangeLockEdition != null)
					OnChangeLockEdition(this, new EventArgs());
			}
		}

        public void OnChangeParentModeEdition(bool bModeEdition)
        {
            foreach (IControleWndFor2iWnd ctrl in Childs)
                ctrl.OnChangeParentModeEdition(bModeEdition);
        }


		public event EventHandler OnChangeLockEdition;

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
			foreach (IControleWndFor2iWnd child in Childs)
				child.AppliqueRestriction(restrictionSurObjetEdite, 
                    listeRestrictions,
                    gestionnaireReadOnly);
		}

		private IControleWndFor2iWnd m_ctrlWndParent = null;
        //---------------------------------------------
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

        //---------------------------------------------
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
