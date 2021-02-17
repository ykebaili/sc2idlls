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
using sc2i.win32.common.customizableList;

namespace sc2i.formulaire.win32.controles2iWnd
{

	public partial class CPanelChildElementCustomList : CCustomizableListControl
	{
		public delegate CResultAErreur DeleteElementDelegate ( object element );

		private Timer m_timerDelete = new Timer();

		private DeleteElementDelegate m_delegueSuppression = null;

        private CDonneesSpecifiquesControleDansCustomList m_donneesAInitialisation = null;

        private CWndFor2iZoneMultipleCustomList m_wndZoneMultipleParent = null;

        private CRestrictionUtilisateurSurType m_restriction = null;
        private CListeRestrictionsUtilisateurSurType m_listeRestrictions = null;
        private IGestionnaireReadOnlySysteme m_gestionnaireReadonlySystem = null;

		//
		public CPanelChildElementCustomList()
		{
			InitializeComponent();
			m_timerDelete.Interval = 500;
			m_timerDelete.Tick += new EventHandler(m_timerDelete_Tick);
		}

        //---------------------------------------------------
        public override bool ShouldSaveControlsState
        {
            get
            {
                return true;
            }
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

        //---------------------------------------------------
        public override bool IsFixedSize
        {
            get
            {
                return false;
            }
        }

        //---------------------------------------------------
		public void Init(
            CWndFor2iZoneMultipleCustomList parent,
			C2iWndSousFormulaire sousFormulaire,
			CCreateur2iFormulaireV2 createur)
		{
            m_wndZoneMultipleParent = parent;
			if (sousFormulaire != null && !sousFormulaire.AdjustToContent)
				Height = sousFormulaire.Size.Height;
            m_panelSousFormulaire.Enabled = true;
			m_panelSousFormulaire.Init(m_wndZoneMultipleParent, sousFormulaire, createur);
		}

        //---------------------------------------------------
        public void SetElementEditeSansChangerLesValeursAffichees(object elementEdite)
        {
            if (m_panelSousFormulaire != null)
                m_panelSousFormulaire.SetElementEditeSansChangerLesValeursAffichees(elementEdite);
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
        protected override CResultAErreur MyInitChamps(CCustomizableListItem item)
        {
            CResultAErreur result = base.MyInitChamps(item);
            if (item != null && item.Tag != null)
            {
                m_panelSousFormulaire.SetElementEdite(item.Tag);

                if (m_restriction != null && !IsCreatingImage)
                {
                    CRestrictionUtilisateurSurType rest = m_restriction.Clone() as CRestrictionUtilisateurSurType;
                    rest.ApplyToObjet(item.Tag);
                    m_panelSousFormulaire.AppliqueRestrictions(
                        rest, 
                        m_listeRestrictions, 
                        m_gestionnaireReadonlySystem);
                }


                //Stock l'aspect du formulaire à l'initialisation
                if (m_donneesAInitialisation == null)
                {
                    m_donneesAInitialisation = new CDonneesSpecifiquesControleDansCustomList();
                    CUtilDonneesSpecifiquesDansCustomList.SaveDonneesControle(this, m_donneesAInitialisation);
                }
                if (item.DonneesControles== null || item.DonneesControles.IsEmpty)
                {
                    item.DonneesControles = m_donneesAInitialisation;
                }

            }
            if (IsCreatingImage)
                m_panelSousFormulaire.BackColor = ColorInactive;
            else
                m_panelSousFormulaire.BackColor = BackColor;
            return result;
        }

        //---------------------------------------------------
        protected override CResultAErreur MyMajChamps()
        {
            CResultAErreur result =  base.MyMajChamps();
            if (result && CurrentItem != null)
                result = m_panelSousFormulaire.MajChamps(true);
            return result;
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
            m_restriction = restrictionSurObjetEdite.Clone() as CRestrictionUtilisateurSurType;;
            m_listeRestrictions = listeRestrictions.Clone() as CListeRestrictionsUtilisateurSurType;
            m_gestionnaireReadonlySystem = gestionnaireReadOnly;
		}

		

		private void m_lnkDelete_LinkClicked(object sender, EventArgs e)
		{

			m_timerDelete.Enabled = true;
			if (m_delegueSuppression != null && CurrentItem != null && CurrentItem.Tag != null)
			{
				CResultAErreur result = m_delegueSuppression(CurrentItem.Tag);
				if (!result)
				{
					result.EmpileErreur(I.T("Error while deleting element|20002"));
					CFormAfficheErreur.Show(result.Erreur);
				}
                CancelEdit();
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

        public override bool HasChange
        {
            get
            {
                return true;
            }
            set
            {
                base.HasChange = value;
            }
        }

        public override bool LockEdition
        {
            get
            {
                return !m_gestionnaireModeEdition.ModeEdition;
            }
            set
            {
                base.LockEdition = value;
                m_gestionnaireModeEdition.ModeEdition = !value;
                if (!value && m_delegueSuppression != null)
                    m_panelDelete.Visible = true;
                else
                    m_panelDelete.Visible = false;
            }
        }

    }



    public class CItemChildZone : CCustomizableListItem
    {
    }
}
