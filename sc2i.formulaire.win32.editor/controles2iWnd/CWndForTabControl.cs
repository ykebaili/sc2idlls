using System;
using System.Collections.Generic;
using System.Text;
using Crownwood.Magic.Controls;
using sc2i.win32.common;
using System.Windows.Forms;
using sc2i.expression;
using sc2i.common;
using sc2i.common.Restrictions;

namespace sc2i.formulaire.win32.controles2iWnd
{
	[AutoExec("Autoexec")]
	public class CWndForTabControl : C2iTabControl, IControleWndFor2iWnd, IControlALockEdition
	{
		private C2iWnd m_wndAssociee = null;
		private IControleWndFor2iWnd[] m_controlesFils = new IControleWndFor2iWnd[0];
		private object m_elementEdite = null;

        public ToolTip Tooltip { get; set; }

		public static void Autoexec()
		{
			CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndTabControl), typeof(CWndForTabControl));
		}

		#region IControleWndFor2iWnd Membres

		public void CreateControle(
			CCreateur2iFormulaireV2 createur,
			C2iWnd wnd,
			Control parent,
			IFournisseurProprietesDynamiques fournisseurProprietes)
		{
			C2iWndTabControl tabControl = wnd as C2iWndTabControl;
			if (tabControl == null)
				return;
			Ombre = false;
			CCreateur2iFormulaireV2.AffecteProprietesCommunes(tabControl, this);
			parent.Controls.Add(this);
			List<IControleWndFor2iWnd> ctrlsFils = new List<IControleWndFor2iWnd>();
			foreach ( C2iWndTabPage page in tabControl.TabPages )
			{
				IControleWndFor2iWnd fils = createur.CreateControle(page, this, fournisseurProprietes);
				ctrlsFils.Add(fils);
				if ( fils.Control != null )
					TabPages.Add((Crownwood.Magic.Controls.TabPage)fils.Control);
                fils.WndContainer = this;
			}
			m_controlesFils = ctrlsFils.ToArray();
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

        //-------------------------------------------------------------
		public void SetElementEdite(object element)
		{
            m_elementEdite = element;
			foreach (IControleWndFor2iWnd ctrl in m_controlesFils)
				ctrl.SetElementEdite(element);
		}

        public void SetElementEditeSansChangerLesValeursAffichees(object element)
        {
            m_elementEdite = element;
            foreach (IControleWndFor2iWnd ctrl in m_controlesFils)
                ctrl.SetElementEditeSansChangerLesValeursAffichees(element);
            CUtilControlesWnd.DeclencheEvenement(C2iWndFenetre.c_strIdEvenementOnInit, this);
        }

		public CResultAErreur MajChamps(bool bAvecVerification)
		{
			CResultAErreur result = CResultAErreur.True;
			foreach (IControleWndFor2iWnd ctrl in m_controlesFils)
			{
				result += ctrl.MajChamps(bAvecVerification);
			}
			return result;
		}

		public void UpdateValeursCalculees()
		{
			foreach (IControleWndFor2iWnd ctrl in m_controlesFils)
				ctrl.UpdateValeursCalculees();
		}

		

        //-------------------------------------
        public void OnChangeParentModeEdition(bool bModeEdition)
        {
            foreach (IControleWndFor2iWnd ctrl in m_controlesFils)
                ctrl.OnChangeParentModeEdition(bModeEdition);
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
            CRestrictionUtilisateurSurType restriction,
            CListeRestrictionsUtilisateurSurType listeRestriction,
            IGestionnaireReadOnlySysteme gestionnaireReadOnly)
		{
			foreach (IControleWndFor2iWnd ctrl in m_controlesFils)
				ctrl.AppliqueRestriction(
                    restriction,
                    listeRestriction,
                    gestionnaireReadOnly);
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



        #region IControlALockEdition Membres
        private bool m_bLockEdition = false;
        public bool LockEdition
        {
            get
            {
                return m_bLockEdition;
            }
            set
            {
                m_bLockEdition = value;
                //RecursiveLockEditionChilds(m_bLockEdition, this);
                if (OnChangeLockEdition != null)
                    OnChangeLockEdition(this, new EventArgs());
            }
        }

        //////////////////////////////////////////////////////////////
        private void RecursiveLockEditionChilds(bool b, Control ctrl)
        {
            foreach (Control fils in ctrl.Controls)
            {
                try
                {
                    if (fils is IControlALockEdition)
                        ((IControlALockEdition)fils).LockEdition = b;
                    else
                        fils.Enabled = !b;
                }
                catch { }
            }
        }

        public event EventHandler OnChangeLockEdition;

        #endregion
    }
}
