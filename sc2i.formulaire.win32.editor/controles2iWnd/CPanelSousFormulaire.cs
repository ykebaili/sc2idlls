using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using sc2i.expression;
using sc2i.common;
using sc2i.win32.common;
using System.Drawing;
using sc2i.common.Restrictions;

namespace sc2i.formulaire.win32.controles2iWnd
{
	public class CPanelSousFormulaire : C2iPanel
	{
		private C2iWnd m_sousFormulaire = null;
		private CCreateur2iFormulaireV2 m_createur = null;
		private object m_elementEdite = null;
		private IControleWndFor2iWnd m_controlePrincipal = null;
        private bool m_bAdjustSizeToFormulaire = false;

		public CPanelSousFormulaire()
			: base()
		{
		}

		public bool Init(
            IControleWndFor2iWnd conteneurSousFormulaire,
			C2iWnd sousFormulaire, 
			CCreateur2iFormulaireV2 createur)
		{
			if (sousFormulaire == null)
			{
				return false;
			}
			if (sousFormulaire == m_sousFormulaire)
				return true;
			m_createur = createur;
			m_sousFormulaire = sousFormulaire;
            CreateSousFormulaire(conteneurSousFormulaire);
            m_controlePrincipal.OnChangeParentModeEdition(!LockEdition);
            m_createur.UpdateVisibilityEtEnable(m_controlePrincipal, m_elementEdite);
			return true;
		}

        public bool AdjustSizeToFormulaire
        {
            get
            {
                return m_bAdjustSizeToFormulaire;
            }
            set
            {
                m_bAdjustSizeToFormulaire = value;
            }
        }

        private void CreateSousFormulaire(IControleWndFor2iWnd conteneur)
		{
			this.SuspendDrawing();
            if ( m_bAdjustSizeToFormulaire )
			    Size = m_sousFormulaire.Size;
            
			//Supprime les anciens contrôles
			ArrayList lstControles = new ArrayList(Controls);
			foreach (Control ctrl in lstControles)
			{
				ctrl.Visible = false;
				Controls.Remove(ctrl);
				ctrl.Dispose();
			}
			m_controlePrincipal = m_createur.CreateControle(m_sousFormulaire, this, m_createur.FournisseurProprietes);
            m_controlePrincipal.WndContainer = conteneur;
            //if (m_controlePrincipal.Control != null)
            //    Enabled = m_controlePrincipal.Control.Enabled;
			if (m_controlePrincipal.Control != null)
                m_controlePrincipal.Control.Dock = m_bAdjustSizeToFormulaire?DockStyle.Top:DockStyle.Fill;
            if (m_controlePrincipal.Control != null)
            {
                m_controlePrincipal.Control.SizeChanged += new EventHandler(Control_SizeChanged);
                m_controlePrincipal.Control.VisibleChanged += new EventHandler(Control_SizeChanged);
            }
            if ( m_controlePrincipal.Control != null )
                m_controlePrincipal.Control.EnabledChanged += new EventHandler(Control_EnabledChanged);
            
			this.ResumeDrawing();
		}

        void Control_EnabledChanged(object sender, EventArgs e)
        {
            Enabled = m_controlePrincipal.Control.Enabled;
        }

		void Control_SizeChanged(object sender, EventArgs e)
		{
            if (m_controlePrincipal != null && m_controlePrincipal.Control != null)
            {
                int nDx = Size.Width - ClientSize.Width;
                int nDy = Size.Height - ClientSize.Height;
                Size = new Size(m_controlePrincipal.Control.Size.Width + nDx,
                    m_controlePrincipal.Control.Size.Height + nDy);
            }
		}

		//---------------------------------------------------
		public void SetElementEdite(object element)
		{
			m_elementEdite = element;
            if (m_controlePrincipal != null)
            {
                m_controlePrincipal.SetElementEdite(element);
                Enabled = m_controlePrincipal.Control.Enabled;
                // 14/11/2012 YK : il faut rappeller ici cette fonction pour mettre à jour la Visibilité des controles
                m_createur.UpdateVisibilityEtEnable(m_controlePrincipal, m_elementEdite);
            }
		}

        //---------------------------------------------------
        public void SetElementEditeSansChangerLesValeursAffichees(object element)
        {
            m_elementEdite = element;
            if (m_controlePrincipal != null)
                m_controlePrincipal.SetElementEditeSansChangerLesValeursAffichees(element);
        }

		//---------------------------------------------------
		public CResultAErreur MajChamps(bool bControlerValeurs)
		{
			CResultAErreur result = CResultAErreur.True;
			if (m_controlePrincipal != null)
				result = m_controlePrincipal.MajChamps(bControlerValeurs);
			return result;
		}

		//---------------------------------------------------
		public void UpdateValeursCalculees()
		{
			if (m_controlePrincipal != null)
				m_controlePrincipal.UpdateValeursCalculees();
		}


		//---------------------------------------------------
        public void AppliqueRestrictions(
            CRestrictionUtilisateurSurType restrictionSurObjetEdite,
            CListeRestrictionsUtilisateurSurType listeRestrictions,
            IGestionnaireReadOnlySysteme gestionnaireReadOnly)
		{
            if (m_controlePrincipal != null)
            {
                if (gestionnaireReadOnly != null)
                    gestionnaireReadOnly.AddControl(m_controlePrincipal);
                m_controlePrincipal.AppliqueRestriction(
                    restrictionSurObjetEdite,
                    listeRestrictions,
                    gestionnaireReadOnly);
            }
		}



        internal void OnChangeParentModeEdition(bool bModeEdition)
        {
            if (m_controlePrincipal != null)
                m_controlePrincipal.OnChangeParentModeEdition(bModeEdition);
        }
    }
}
