using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using sc2i.expression;
using sc2i.common;
using System.Collections;
using sc2i.win32.common;
using sc2i.formulaire;
using sc2i.formulaire.win32;
using sc2i.data.dynamic;
using sc2i.data;
using sc2i.formulaire.win32.controles2iWnd;
using sc2i.common.Restrictions;


namespace sc2i.win32.data.dynamic
{
	[AutoExec("Autoexec")]
    public partial class CWndForDonneePrecalculee : CControlWndFor2iWnd, IControleWndFor2iWnd
	{
        CPanelVisuDonneePrecalculee m_panelVisu = null;
        private bool m_bIsInit = false;


        public CWndForDonneePrecalculee()
            : base()
		{
            m_panelVisu = new CPanelVisuDonneePrecalculee();
            LockEdition = false;
		}

		public static void Autoexec()
		{
			CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndDonneePrecalculee), typeof(CWndForDonneePrecalculee));
		}

		#region IControleWndFor2iWnd Membres

        

        //---------------------------------------------------------------------
		protected override void MyCreateControle(CCreateur2iFormulaireV2 createur, C2iWnd wnd, Control parent, IFournisseurProprietesDynamiques fournisseur)
		{
            C2iWndDonneePrecalculee listeStd = wnd as C2iWndDonneePrecalculee;
            if (listeStd == null)
				return;
            CCreateur2iFormulaireV2.AffecteProprietesCommunes(wnd, m_panelVisu);
            parent.Controls.Add(m_panelVisu);
		}

        

        //---------------------------------------------------------------------
        public override Control Control
		{
            get { return m_panelVisu; }
		}


		//---------------------------------------------------------------------
		protected override void OnChangeElementEdite(object element)
		{
            if (!m_bIsInit)
            {
                C2iWndDonneePrecalculee listeStd = WndAssociee as C2iWndDonneePrecalculee;
                m_panelVisu.Init(listeStd.Parametre, CSc2iWin32DataClient.ContexteCourant);
            }
            m_bIsInit = true;
		}

        //---------------------------------------------------------------------
		protected override CResultAErreur MyMajChamps(bool bControlerLesValeursAvantValidation)
        {
            CResultAErreur result = CResultAErreur.True;
            return result;
        }

        //---------------------------------------------------------------------
        protected override void MyUpdateValeursCalculees()
        {
        }


        //---------------------------------------------------------------------
        protected override void MyAppliqueRestriction(
            CRestrictionUtilisateurSurType restrictionSurObjetEdite,
            CListeRestrictionsUtilisateurSurType listeRestrictions,
            IGestionnaireReadOnlySysteme gestionnaireReadOnly)
		{
        }


		#endregion
        public override bool LockEdition
        {
            get
            {
                return false;
            }
            set
            {
                
            }
        }

        		
	}
}
