using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using sc2i.formulaire;
using sc2i.common;
using sc2i.win32.common;
using sc2i.expression;
using System.Drawing;
using sc2i.common.Restrictions;


namespace sc2i.formulaire.win32.controles2iWnd
{
	[AutoExec ( "Autoexec")]
	public class CWndFor2iPanel : CControlWndFor2iWnd
	{
        private CCreateur2iFormulaireV2 m_createur = null;
        private IFournisseurProprietesDynamiques m_fournisseur = null;

		//---------------------------------------------------------------
        public static void Autoexec()
        {
			CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndPanel), typeof(CWndFor2iPanel));
        }

		//---------------------------------------------------------------
		private C2iPanel m_panel = null;

		//---------------------------------------------------------------
		public CWndFor2iPanel()
		{
		}

		//---------------------------------------------------------------
		protected override void MyCreateControle(
			CCreateur2iFormulaireV2 createur,
			C2iWnd wnd,
			Control parent,
			IFournisseurProprietesDynamiques fournisseurProprietes)
		{
            m_createur = createur;
            m_fournisseur = fournisseurProprietes;
			C2iWndPanel panel = wnd as C2iWndPanel;
			if (panel == null)
				return;
			if (panel == null)
				return;

			if ( panel.Ombre )
				m_panel = new C2iPanelOmbre();
			else
				m_panel = new C2iPanel();

            switch (panel.BorderStyle)
            {
                case C2iWndPanel.PanelBorderStyle.Aucun:
                    m_panel.BorderStyle = BorderStyle.None;
                    break;
                case C2iWndPanel.PanelBorderStyle._3D:
                    m_panel.BorderStyle = BorderStyle.Fixed3D;
                    break;
                case C2iWndPanel.PanelBorderStyle.Plein:
                    m_panel.BorderStyle = BorderStyle.FixedSingle;
                    break;
                default:
                    break;
            }
			CCreateur2iFormulaireV2.AffecteProprietesCommunes(panel, m_panel);
            m_panel.AutoScroll = panel.AutoScroll;
            m_panel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            m_panel.AutoSize = panel.AutoSize;
			parent.Controls.Add(m_panel);
		}

		//---------------------------------------------------------------
		public override Control Control
		{
			get
			{
				return m_panel;
			}
		}

        //---------------------------------------------------------------
        public C2iWndPanel WndPanel
        {
            get
            {
                return WndAssociee as C2iWndPanel;
            }
        }
		//---------------------------------------------------------------
		protected override void OnChangeElementEdite(object element)
		{
		}

		//---------------------------------------------------------------
		protected override CResultAErreur MyMajChamps(bool bControlerLesValeursAvantValidation)
		{
			return CResultAErreur.True;
		}

		//---------------------------------------------------------------
		protected override void MyUpdateValeursCalculees()
		{
		}

		//---------------------------------------------------------------
        protected override void MyAppliqueRestriction(
            CRestrictionUtilisateurSurType restrictionSurObjetEdite,
            CListeRestrictionsUtilisateurSurType listeRestrictions,
            IGestionnaireReadOnlySysteme gestionnaireReadOnly)
		{
		}

        
	}
}
