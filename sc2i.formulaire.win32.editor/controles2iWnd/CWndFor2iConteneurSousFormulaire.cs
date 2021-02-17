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
	public class CWndFor2iConteneurSousFormulaire : CControlWndFor2iWnd
	{
        private CCreateur2iFormulaireV2 m_createur = null;
        private IFournisseurProprietesDynamiques m_fournisseur = null;

        private CListeRestrictionsUtilisateurSurType m_listeRestrictions = null;
        private IGestionnaireReadOnlySysteme m_gestionnaireReadOnly = null;

        //private Panel m_panelPourBorder = null;

        CPanelSousFormulaire m_panelSousFormulaire = null;

        private C2iWnd m_subForm = null;


		//---------------------------------------------------------------
        public static void Autoexec()
        {
			CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndConteneurSousFormulaire), typeof(CWndFor2iConteneurSousFormulaire));
        }

		//---------------------------------------------------------------
		public CWndFor2iConteneurSousFormulaire()
		{
		}

        //---------------------------------------------------------------
        private C2iWndConteneurSousFormulaire WndSousFormulaire
        {
            get{
                return WndAssociee as C2iWndConteneurSousFormulaire;
            }
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
            C2iWndConteneurSousFormulaire conteneur = wnd as C2iWndConteneurSousFormulaire;
            if (conteneur == null)
                return;
            m_panelSousFormulaire = new CPanelSousFormulaire();

            //m_panelPourBorder = new Panel();
            //m_panelPourBorder.Controls.Add(m_panelSousFormulaire);
            //m_panelSousFormulaire.Dock = DockStyle.Fill;


            switch (conteneur.BorderStyle)
            {
                case C2iWndPanel.PanelBorderStyle.Aucun:

                    m_panelSousFormulaire.BorderStyle = BorderStyle.None;
                    break;
                case C2iWndPanel.PanelBorderStyle._3D:
                    m_panelSousFormulaire.BorderStyle = BorderStyle.Fixed3D;
                    break;
                case C2iWndPanel.PanelBorderStyle.Plein:
                    m_panelSousFormulaire.BorderStyle = BorderStyle.FixedSingle;
                    break;
                default:
                    break;
            }
            CCreateur2iFormulaireV2.AffecteProprietesCommunes(conteneur, m_panelSousFormulaire);
            m_panelSousFormulaire.AutoScroll = true;
            m_panelSousFormulaire.AdjustSizeToFormulaire = conteneur.Autosize;
			//parent.Controls.Add(m_panelPourBorder);
            parent.Controls.Add(m_panelSousFormulaire);
            if (conteneur != null && conteneur.SubFormReference != null)
            {
                C2iWnd frm = sc2i.formulaire.subform.C2iWndProvider.GetForm(conteneur.SubFormReference);
                if (frm != null)
                    SubForm = CCloner2iSerializable.Clone(frm) as C2iWnd;
            }

		}

		//---------------------------------------------------------------
		public override Control Control
		{
			get
			{
				return m_panelSousFormulaire;
			}
		}

        //---------------------------------------------------------------
        public C2iWnd SubForm
        {
            get
            {
                return m_subForm;
            }
            set
            {
                m_subForm = value;
                if (m_subForm is I2iWndAParametres && WndSousFormulaire != null)
                {
                    CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(EditedElement);
                    foreach (CFormuleNommee formule in WndSousFormulaire.SubFormParameters)
                    {
                        if (formule.Formule != null)
                        {
                            CResultAErreur res = formule.Formule.Eval(ctx);
                            if (res)
                                ((I2iWndAParametres)m_subForm).SetParameterValue(formule.Libelle, res.Data);
                        }
                    }
                }
                if (value == null)
                    m_panelSousFormulaire.Visible = false;
                else
                {
                    m_panelSousFormulaire.Init(this, value, m_createur);
                    m_panelSousFormulaire.SetElementEdite(this.EditedElement);
                    m_panelSousFormulaire.LockEdition = LockEdition;
                    m_panelSousFormulaire.Visible = true;
                    m_panelSousFormulaire.OnChangeParentModeEdition(!LockEdition);
                    if (m_listeRestrictions != null && EditedElement != null)
                    {
                        CRestrictionUtilisateurSurType rest = m_listeRestrictions.GetRestriction(EditedElement.GetType());
                        AppliqueRestriction(rest,
                            m_listeRestrictions,
                            m_gestionnaireReadOnly);
                    }

                }
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
            return m_panelSousFormulaire.MajChamps(bControlerLesValeursAvantValidation);
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
            if (listeRestrictions != null && gestionnaireReadOnly != null)
            {
                m_listeRestrictions = listeRestrictions.Clone() as CListeRestrictionsUtilisateurSurType;
                m_gestionnaireReadOnly = gestionnaireReadOnly;
                if (m_panelSousFormulaire != null)
                    m_panelSousFormulaire.AppliqueRestrictions(
                        restrictionSurObjetEdite,
                        listeRestrictions,
                        gestionnaireReadOnly);
            }
		}

        //---------------------------------------------------------------
        public override bool LockEdition
        {
            get
            {
                return base.LockEdition;
            }
            set
            {
                base.LockEdition = value;
                if (m_panelSousFormulaire != null)
                    m_panelSousFormulaire.LockEdition = value;
            }
        }

        public override void OnChangeParentModeEdition(bool bModeEdition)
        {
            base.OnChangeParentModeEdition(bModeEdition);
            m_panelSousFormulaire.OnChangeParentModeEdition(!LockEdition);
        }

        

        public override void SetElementEdite(object element)
        {
            base.SetElementEdite(element);
            C2iWndConteneurSousFormulaire cnt = WndSousFormulaire;
            if (cnt != null && cnt.EditedElement != null)
            {
                CContexteEvaluationExpression ctx = CUtilControlesWnd.GetContexteEval(this, element);
                CResultAErreur result = cnt.EditedElement.Eval(ctx);
                if (result)
                    element = result.Data;
                else
                    element = null;
            }
            if (m_panelSousFormulaire != null)
                m_panelSousFormulaire.SetElementEdite(element);
        }


        
	}
}
