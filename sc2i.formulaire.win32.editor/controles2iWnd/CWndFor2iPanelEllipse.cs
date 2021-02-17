using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using sc2i.formulaire;
using sc2i.common;
using sc2i.win32.common;
using sc2i.expression;
using sc2i.common.Restrictions;


namespace sc2i.formulaire.win32.controles2iWnd
{
    [AutoExec("Autoexec")]
    public class CWndFor2iPanelEllipse :CControlWndFor2iWnd, IControleWndFor2iWnd
    {
        //---------------------------------------------------------------
        public static void Autoexec()
        {
            CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndPanelEllipse), typeof(CWndFor2iPanelEllipse));
        }

		//---------------------------------------------------------------
		private CPanelEllipse m_panel = new CPanelEllipse();

		//---------------------------------------------------------------
		public CWndFor2iPanelEllipse()
		{
		}

		//---------------------------------------------------------------
		protected override void MyCreateControle(
			CCreateur2iFormulaireV2 createur,
			C2iWnd wnd,
			Control parent,
			IFournisseurProprietesDynamiques fournisseurProprietes)
		{
			C2iWndPanelEllipse ellipse = wnd as C2iWndPanelEllipse;
			if (ellipse == null)
				return;

			CCreateur2iFormulaireV2.AffecteProprietesCommunes(ellipse, m_panel);
			m_panel.AutoScroll = false;
			m_panel.DrawBorder =ellipse.BorderStyle == C2iWndPanelEllipse.PanelBorderStyle.Plein;
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
