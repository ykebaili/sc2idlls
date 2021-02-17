using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using sc2i.common;
using sc2i.win32.common;
using sc2i.common.Restrictions;

namespace sc2i.formulaire.win32.controles2iWnd
{

	public partial class CPanelChildElement : UserControl, IControlALockEdition
	{
		public delegate CResultAErreur DeleteElementDelegate ( object element );

		private Timer m_timerDelete = new Timer();

		private DeleteElementDelegate m_delegueSuppression = null;
		private object m_elementEdite = null;

        private CWndFor2iZoneMultiple m_wndZoneMultipleParent = null;
		

		//
		public CPanelChildElement()
		{
			InitializeComponent();
			m_timerDelete.Interval = 500;
			m_timerDelete.Tick += new EventHandler(m_timerDelete_Tick);
		}

		//---------------------------------------------------
		void m_timerDelete_Tick(object sender, EventArgs e)
		{
			try
			{
				m_lnkDelete.Visible = !m_lnkDelete.Visible;
				m_panelSousFormulaire.Visible = !m_panelSousFormulaire.Visible;
			}
			catch
			{ }
		}

		public void Init(
            CWndFor2iZoneMultiple parent,
			C2iWndSousFormulaire sousFormulaire,
			CCreateur2iFormulaireV2 createur)
		{
            m_wndZoneMultipleParent = parent;
			if (sousFormulaire != null && !sousFormulaire.AdjustToContent)
				Height = sousFormulaire.Size.Height;
            m_panelSousFormulaire.Enabled = true;
			m_panelSousFormulaire.Init(m_wndZoneMultipleParent, sousFormulaire, createur);
            m_panelSousFormulaire.AutoScroll = sousFormulaire.AutoScroll;
		}

		//---------------------------------------------------
		public DeleteElementDelegate DelegueSuppression
		{
			set
			{
				m_delegueSuppression = value;
				m_panelDelete.Visible = m_gestionnaireModeEdition.ModeEdition && m_delegueSuppression != null;
			}
		}

		//---------------------------------------------------
		public void SetElementEdite(object element)
		{
			m_elementEdite = element;
			m_panelSousFormulaire.SetElementEdite(element);
		}

        public void SetElementEditeSansChangerLesValeursAffichees(object elementEdite)
        {
            m_elementEdite = elementEdite;
            m_panelSousFormulaire.SetElementEditeSansChangerLesValeursAffichees(elementEdite);
        }

		//---------------------------------------------------
		public CResultAErreur MajChamps(bool bControlerValeurs)
		{
			return m_panelSousFormulaire.MajChamps(bControlerValeurs);
		}

		//---------------------------------------------------
		public void UpdateValeursCalculees()
		{
			m_panelSousFormulaire.UpdateValeursCalculees();
		}


		//---------------------------------------------------
        public void AppliqueRestrictions(
            CRestrictionUtilisateurSurType restrictionSurObjetEdite,
            CListeRestrictionsUtilisateurSurType listeRestrictions,
            IGestionnaireReadOnlySysteme gestionnaireReadOnly)
		{
			m_panelSousFormulaire.AppliqueRestrictions(
                restrictionSurObjetEdite,
                listeRestrictions,
                gestionnaireReadOnly);
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
				if (!value && m_delegueSuppression != null)
					m_panelDelete.Visible = true;
				else
					m_panelDelete.Visible = false;
				if (OnChangeLockEdition != null)
					OnChangeLockEdition(this, new EventArgs());
			}
		}

		public event EventHandler OnChangeLockEdition;

		#endregion

		private void m_lnkDelete_LinkClicked(object sender, EventArgs e)
		{

			m_timerDelete.Enabled = true;
			if (m_delegueSuppression != null)
			{
				CResultAErreur result = m_delegueSuppression(m_elementEdite);
				if (!result)
				{
					result.EmpileErreur(I.T("Error while deleting element|20002"));
					CFormAfficheErreur.Show(result.Erreur);
				}
			}
			try
			{
				m_timerDelete.Enabled = false;
				m_lnkDelete.Visible = true;
				m_panelSousFormulaire.Visible = true;
			}
			catch
			{
			}
		}

		private void m_panelSousFormulaire_SizeChanged(object sender, EventArgs e)
		{
			Size = new Size(m_panelSousFormulaire.Width +
				(m_panelDelete.Visible ? m_panelDelete.Width : 0),
				Math.Max(m_panelSousFormulaire.Height, m_lnkDelete.Height));
		}



        internal void OnChangeParentModeEdition(bool bModeEdition)
        {
            m_panelSousFormulaire.OnChangeParentModeEdition(bModeEdition);
        }

        private void m_panelSousFormulaire_EnabledChanged(object sender, EventArgs e)
        {
            m_lnkDelete.Enabled = m_panelSousFormulaire.Enabled;
        }
    }
}
