using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;
using System.Windows.Forms;
using sc2i.expression;
using sc2i.common.Restrictions;

namespace sc2i.formulaire.win32.controles2iWnd
{
	[AutoExec("Autoexec")]
	public class CWndFor2iListe : CControlWndFor2iWnd
	{
		private CControleListePourFormulaires m_controleListe = null;
		
		public CWndFor2iListe()
		{
		}

		public static void Autoexec()
		{
			CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndListe), typeof(CWndFor2iListe));
		}

		protected override void MyCreateControle(
			CCreateur2iFormulaireV2 createur,
			C2iWnd wnd,
			Control parent,
			IFournisseurProprietesDynamiques fournisseur)
		{
			C2iWndListe liste2i = wnd as C2iWndListe;
			if (liste2i == null)
				return;
			m_controleListe = new CControleListePourFormulaires();
			m_controleListe.Init(this,liste2i);
			CCreateur2iFormulaireV2.AffecteProprietesCommunes(liste2i, m_controleListe);
			parent.Controls.Add(m_controleListe);
		}
		
		//------------------------------------------------------------
		public override Control Control
		{
			get { return m_controleListe; }
		}

		//------------------------------------------------------------
		protected override CResultAErreur MyMajChamps(bool bControlerLesValeursAvantValidation)
		{
			CResultAErreur result = CResultAErreur.True;
			return result;
		}

		//------------------------------------------------------------
		protected override void OnChangeElementEdite(object element)
		{
			if (m_controleListe != null)
				m_controleListe.SetElementEdite(element);
			MyUpdateValeursCalculees();
		}

		//------------------------------------------------------------
		protected override void MyUpdateValeursCalculees()
		{
			if (m_controleListe!=null)
				m_controleListe.UpdateValeursCalculees();
		}

		//------------------------------------------------------------
        protected override void MyAppliqueRestriction(
            CRestrictionUtilisateurSurType restrictionSurObjetEdite,
            CListeRestrictionsUtilisateurSurType listeRestrictions,
            IGestionnaireReadOnlySysteme gestionnaireReadOnly)
		{
		}
	}
}
