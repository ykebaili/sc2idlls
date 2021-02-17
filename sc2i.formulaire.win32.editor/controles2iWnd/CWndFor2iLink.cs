using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


using sc2i.formulaire;
using sc2i.common;
using sc2i.expression;
using sc2i.win32.common;
using sc2i.formulaire.win32.datagrid;
using sc2i.common.Restrictions;


namespace sc2i.formulaire.win32.controles2iWnd
{
	[AutoExec("Autoexec")]
    public class CWndFor2iLink : CControlWndFor2iWnd, IControleWndFor2iWnd
	{
		private LinkLabel m_linkLabel = new LinkLabel();

		public static void Autoexec()
		{
			CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndLink), typeof(CWndFor2iLink));
		}

		protected override void MyCreateControle(
			CCreateur2iFormulaireV2 createur,
			C2iWnd wnd, 
			Control parent, 
			IFournisseurProprietesDynamiques fournisseurProprietes)
		{
            
            if (WndFor2iLink == null)
				return;
            CCreateur2iFormulaireV2.AffecteProprietesCommunes(WndFor2iLink, m_linkLabel);
            m_linkLabel.Text = WndFor2iLink.Text;
            m_linkLabel.TextAlign = WndFor2iLink.TextAlign;
			switch (WndFor2iLink.BorderStyle)
			{
				case C2iWndLabel.LabelBorderStyle._3D:
					m_linkLabel.BorderStyle = BorderStyle.Fixed3D;
					break;
				case C2iWndLabel.LabelBorderStyle.Aucun:
					m_linkLabel.BorderStyle = BorderStyle.None;
					break;
				case C2iWndLabel.LabelBorderStyle.Plein:
					m_linkLabel.BorderStyle = BorderStyle.FixedSingle;
					break;
			}
            // Comportement du lien
            switch (WndFor2iLink.LinkBehavior)
            {
                case C2iWndLink.C2iLinkBehavior.SystemDefault:
                    m_linkLabel.LinkBehavior = LinkBehavior.SystemDefault;
                    break;
                case C2iWndLink.C2iLinkBehavior.AlwaysUnderline:
                    m_linkLabel.LinkBehavior = LinkBehavior.AlwaysUnderline;
                    break;
                case C2iWndLink.C2iLinkBehavior.HoverUnderline:
                    m_linkLabel.LinkBehavior = LinkBehavior.HoverUnderline;
                    break;
                case C2iWndLink.C2iLinkBehavior.NeverUnderline:
                    m_linkLabel.LinkBehavior = LinkBehavior.NeverUnderline;
                    break;
            }

            m_linkLabel.LinkColor = WndFor2iLink.ForeColor;

            // Evenement clic
            m_linkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(CWndFor2iLink_LinkClicked);
            parent.Controls.Add(m_linkLabel);
		}

		private C2iWndLink WndFor2iLink
		{
			get
			{
				return WndAssociee as C2iWndLink;
			}
		}

        //------------------------------------------------------------------------------
        void CWndFor2iLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
			if (WndFor2iLink != null && WndFor2iLink.Action != null)
			{
				CResultAErreur result = CExecuteurActionSur2iLink.ExecuteAction(
                    Control, 
                    WndFor2iLink.Action, 
                    CUtilControlesWnd.GetObjetForEvalFormuleParametrage(this, EditedElement));
				if (!result)
					CFormAlerte.Afficher(result.Erreur);
			}
			CUtilControlesWnd.DeclencheEvenement(C2iWndLink.c_strIdEvenementLinkClicked, this);
        }

		//-------------------------------------------------------------------------
		public override Control Control
		{
			get
			{
				return m_linkLabel;
			}
		}

		//-------------------------------------------------------------------------
		protected override void OnChangeElementEdite(object element)
		{
		}

		//-------------------------------------------------------------------------
		protected override CResultAErreur MyMajChamps(bool bControlerLesValeursAvantValidation)
		{
			return CResultAErreur.True;
		}

		//-------------------------------------------------------------------------
		protected override void MyUpdateValeursCalculees()
		{
		}

		//-------------------------------------------------------------------------
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
                Control.Enabled = !value;
            }
        }



    }
}
