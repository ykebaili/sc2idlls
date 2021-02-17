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
	[AutoExec ( "Autoexec")]
	public class CWndFor2iSplitter : CControlWndFor2iWnd, IControleWndFor2iWnd
	{
		private Splitter m_splitter = new Splitter();
		//--------------------------------------------
		public static void Autoexec()
		{
			CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndSplitter), typeof(CWndFor2iSplitter));
		}

		//--------------------------------------------
		protected override void MyCreateControle(
			CCreateur2iFormulaireV2 createur,
			C2iWnd wnd, 
			Control parent, 
			IFournisseurProprietesDynamiques fournisseurProprietes)
		{
            switch (wnd.DockStyle)
            {
                case EDockStyle.Top:
                    m_splitter.Dock = DockStyle.Top;
                    break;
                case EDockStyle.Bottom:
                    m_splitter.Dock = DockStyle.Bottom;
                    break;
                case EDockStyle.Left:
                    m_splitter.Dock = DockStyle.Left;
                    break;
                case EDockStyle.Right:
                    m_splitter.Dock = DockStyle.Right;
                    break;
            }
			CCreateur2iFormulaireV2.AffecteProprietesCommunes(wnd, m_splitter);
            parent.Controls.Add(m_splitter);
		}

		
		//--------------------------------------------
		public override Control Control
		{
			get
			{
                return m_splitter;
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

	}
}
