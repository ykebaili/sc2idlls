using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


using sc2i.formulaire;
using sc2i.common;
using sc2i.expression;
using sc2i.common.Restrictions;
using sc2i.win32.common;


namespace sc2i.formulaire.win32.controles2iWnd
{
	[AutoExec("Autoexec")]
	public class CWndFor2iButton : CControlWndFor2iWnd, IControleWndFor2iWnd
	{
		private CDelayedButton m_bouton = null;

		public CWndFor2iButton()
		{
			m_bouton = new CDelayedButton();
			m_bouton.Click += new EventHandler(CWndFor2iButton_Click);
		}

		public void  CWndFor2iButton_Click(object sender, EventArgs e)
		{
            if (WndButton != null && WndButton.Action != null)
            {
                CResultAErreur result = CExecuteurActionSur2iLink.ExecuteAction(
                    Control, 
                    WndButton.Action, 
                    CUtilControlesWnd.GetObjetForEvalFormuleParametrage(this, EditedElement));
                if (!result)
                    CFormAlerte.Afficher(result.Erreur);
            }
			CUtilControlesWnd.DeclencheEvenement(C2iWndButton.c_strIdEvenementClick, this);
		} 
		
		public static void Autoexec()
		{
			CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndButton), typeof(CWndFor2iButton));
		}

        //-------------------------------------------
        public C2iWndButton WndButton
        {
            get
            {
                return WndAssociee as C2iWndButton;
            }
        }

		//-------------------------------------------
		protected override void MyCreateControle(
			CCreateur2iFormulaireV2 createur,
			C2iWnd wnd, 
			Control parent, 
			IFournisseurProprietesDynamiques fournisseurProprietes)
		{
			C2iWndButton bouton = wnd as C2iWndButton;
			if (bouton == null)
				return;
			CCreateur2iFormulaireV2.AffecteProprietesCommunes(bouton, m_bouton);
			m_bouton.Text = bouton.Text;
			m_bouton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            m_bouton.DelayInSeconds = bouton.AutoClickInSeconds;
			parent.Controls.Add(m_bouton);
		}


		//-------------------------------------------
		public override Control Control
		{
			get
			{
				return m_bouton;
			}
		}

		//-------------------------------------------
		protected override void  OnChangeElementEdite(object element)
		{
		}

		//-------------------------------------------
		protected override CResultAErreur MyMajChamps(bool bControlerLesValeursAvantValidation)
		{
			return CResultAErreur.True;
		}

		//-------------------------------------------
		protected override void MyUpdateValeursCalculees()
		{
		}
		
		//---------------------------------------------
        protected override void MyAppliqueRestriction(
            CRestrictionUtilisateurSurType restrictionSurObjetEdite,
            CListeRestrictionsUtilisateurSurType listeRestrictions,
            IGestionnaireReadOnlySysteme gestionnaireReadOnly)
		{
		}

	}
}
