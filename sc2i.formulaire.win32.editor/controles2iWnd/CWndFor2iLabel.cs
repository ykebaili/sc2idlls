using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


using sc2i.formulaire;
using sc2i.common;
using sc2i.expression;
using sc2i.win32.common;
using sc2i.common.Restrictions;


namespace sc2i.formulaire.win32.controles2iWnd
{
	[AutoExec("Autoexec")]
	public class CWndFor2iLabel : CControlWndFor2iWnd, IControleWndFor2iWnd
	{
		private C2iLabel m_label = new C2iLabel();

		public static void Autoexec()
		{
			CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndLabel), typeof(CWndFor2iLabel));
		}

		protected override void  MyCreateControle(
			CCreateur2iFormulaireV2 createur,
			C2iWnd wnd, 
			Control parent, 
			IFournisseurProprietesDynamiques fournisseurProprietes)
		{
			C2iWndLabel label = wnd as C2iWndLabel;
			if ( label == null )
				return;
			CCreateur2iFormulaireV2.AffecteProprietesCommunes(label, m_label);
            m_label.Text = label.Text;
			m_label.TextAlign = label.TextAlign;
			switch (label.BorderStyle)
			{
				case C2iWndLabel.LabelBorderStyle._3D:
					m_label.BorderStyle = BorderStyle.Fixed3D;
					break;
				case C2iWndLabel.LabelBorderStyle.Aucun:
					m_label.BorderStyle = BorderStyle.None;
					break;
				case C2iWndLabel.LabelBorderStyle.Plein:
					m_label.BorderStyle = BorderStyle.FixedSingle;
					break;
			}
			parent.Controls.Add(m_label);
		}

		
		//----------------------------------------
		public override Control Control
		{
			get
			{
				return m_label;
			}
		}

		//----------------------------------------
		protected override void OnChangeElementEdite(object element)
		{
		}

		//----------------------------------------
		protected override CResultAErreur MyMajChamps(bool bControlerLesValeursAvantValidation)
		{
			return CResultAErreur.True;
		}

		//----------------------------------------
		protected override void MyUpdateValeursCalculees()
		{
		}

		//----------------------------------------
        protected override void MyAppliqueRestriction(
            CRestrictionUtilisateurSurType restrictionSurObjetEdite,
            CListeRestrictionsUtilisateurSurType listeRestrictions,
            IGestionnaireReadOnlySysteme gestionnaireReadOnly)
		{
		}

        public override bool LockEdition
        {
            get
            {
                return false;
            }
            set
            {
                Control.Enabled = true;
            }
        }
	}
}
