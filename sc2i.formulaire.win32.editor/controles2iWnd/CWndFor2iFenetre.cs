using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.formulaire;
using sc2i.common;
using sc2i.expression;
using sc2i.win32.common;
using sc2i.common.Restrictions;
using sc2i.expression.FonctionsDynamiques;


namespace sc2i.formulaire.win32.controles2iWnd
{
	[AutoExec ( "Autoexec")]
	public class CWndFor2iFenetre : CControlWndFor2iWnd, IControleWndFor2iWnd, IWndErrorProvider,
        IElementAFonctionsDynamiques
	{
		private ErrorProvider m_errorProvider = new ErrorProvider();
		private Panel m_panel = new C2iPanel();

		public static void Autoexec()
		{
			CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndFenetre), typeof(CWndFor2iFenetre));
		}

		protected override void  MyCreateControle(
			CCreateur2iFormulaireV2 createur,
			C2iWnd wnd, 
			Control parent, 
			IFournisseurProprietesDynamiques fournisseurProprietes)
		{
			C2iWndFenetre fenetre = wnd as C2iWndFenetre;
			if (fenetre == null)
				return;
			CCreateur2iFormulaireV2.AffecteProprietesCommunes(fenetre, m_panel);
			m_panel.AutoScroll = true;
            if (fenetre.AutoSize)
            {
                m_panel.AutoSize = true;
                m_panel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            }
			parent.Controls.Add(m_panel);
			m_errorProvider.ContainerControl = m_panel.FindForm();

		}

		//-------------------------------------
		protected override void AfterCreateChilds()
		{
			base.AfterCreateChilds();
			//CUtilControlesWnd.DeclencheEvenement(C2iWndFenetre.c_strIdEvenementOnInit, this);
		}

		//-------------------------------------
		public ErrorProvider ErrorProvider
		{
			get
			{
				return m_errorProvider;
			}
		}

        //-------------------------------------
        public override bool  IsRacineForEvenements
        {
            get
            {
                return true;
            }
        }

		//-------------------------------------
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


       /* #region IElementAMethodeDynamiqueNommee Membres

        public bool HasMethodeNommee(string strNomMethode)
        {
            C2iWndFenetre fenetre = WndAssociee as C2iWndFenetre;
            if (fenetre != null)
            {
                foreach (CFonctionDynamique fonction in fenetre.Functions)
                    if (fonction.Nom == strNomMethode)
                        return true;
            }
            return false;
        }

       public CResultAErreur ExecuteMethode(string strMethode, object[] parametres)
        {
            C2iWndFenetre fenetre = WndAssociee as C2iWndFenetre;
            CResultAErreur result = CResultAErreur.True;
            if (fenetre != null)
            {
                foreach (CFonctionDynamique fonction in fenetre.Functions)
                {
                    if (fonction.Nom == strMethode)
                    {
                        CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(new CEncaspuleurControleWndForFormules(this));
                        result = fonction.Formule.Eval(ctx);
                        return result;
                    }
                }
            }
            result.EmpileErreur(I.T("Method '@1' not found|30004",strMethode));
            return result;
        }*/

        public bool MessageBox(string strMessage)
        {
            System.Windows.Forms.MessageBox.Show(strMessage);
            return true;
        }

        public bool YesNoBox(string strMessage)
        {
            return System.Windows.Forms.MessageBox.Show(strMessage, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        public bool OkCancelBox(string strMessage)
        {
            return System.Windows.Forms.MessageBox.Show(strMessage, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK;
        }


        #region IElementAFonctionsDynamiques Membres

        public IEnumerable<CFonctionDynamique> FonctionsDynamiques
        {
            get { 
                C2iWndFenetre fenetre = WndAssociee as C2iWndFenetre;
            if (fenetre != null)
            {
                return fenetre.FonctionsDynamiques;
            }
                return new CFonctionDynamique[0];
            }
        }

        public CFonctionDynamique GetFonctionDynamique(string strIdFonction)
        {
            return FonctionsDynamiques.FirstOrDefault(f=>f.IdFonction == strIdFonction );
        }

        #endregion
    }
}
