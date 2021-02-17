using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.common;
using sc2i.expression;
using sc2i.common.Restrictions;
using sc2i.expression.FonctionsDynamiques;

namespace sc2i.formulaire.win32.controles2iWnd
{
    public class CWndFor2iWndFormulaireEnSurimpression : CControlWndFor2iWnd, IElementAFonctionsDynamiques
    {
        Control m_controleAssocie = null;
        
        public CWndFor2iWndFormulaireEnSurimpression(Control controleAssocie)
        {
            m_controleAssocie = controleAssocie;
            IControleFormulaireExterne ctrlExterne = controleAssocie as IControleFormulaireExterne;
            if (ctrlExterne != null)
                ctrlExterne.AttacheToWndFor2iWnd(this);
        }

        protected override void OnChangeElementEdite(object element)
        {
        }

        protected override void MyCreateControle(CCreateur2iFormulaireV2 createur, C2iWnd wnd, Control parent, sc2i.expression.IFournisseurProprietesDynamiques fournisseur)
        {
            
        }

        public void AddChild(IRuntimeFor2iWnd child)
        {
            List<IRuntimeFor2iWnd> lst = new List<IRuntimeFor2iWnd>(Childs);
            lst.Add(child);
            SetControlesFils(lst.ToArray());
        }

        public override Control Control
        {
            get { return m_controleAssocie; }
        }

        protected override CResultAErreur MyMajChamps(bool bControlerLesValeursAvantValidation)
        {
            return CResultAErreur.True;
        }

        protected override void MyUpdateValeursCalculees()
        {
            
        }

        protected override void MyAppliqueRestriction(
            CRestrictionUtilisateurSurType restrictionSurObjetEdite,
            CListeRestrictionsUtilisateurSurType listeRestrictions,
            IGestionnaireReadOnlySysteme gestionnaireReadOnly)
        {
            
        }
        
        /*#region IElementAMethodeDynamiqueNommee Membres

        public bool HasMethodeNommee(string strNomMethode)
        {
            C2iWndFenetre fenetre = WndAssociee as C2iWndFenetre;
            if (fenetre != null)
            {
                foreach (CFonctionDynamique fonction in fenetre.Functions)
                    if (fonction.Nom == strNomMethode || fonction.IdFonction == strNomMethode)
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
                foreach (CFormuleNommee fonction in fenetre.Functions)
                {
                    if (fonction.Libelle == strMethode)
                    {
                        CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(new CEncaspuleurControleWndForFormules(this));
                        result = fonction.Formule.Eval(ctx);
                        return result;
                    }
                }
            }
            result.EmpileErreur(I.T("Method '@1' not found|30004", strMethode));
            return result;
        }

        #endregion*/

        #region IElementAFonctionsDynamiques Membres
        public IEnumerable<CFonctionDynamique> FonctionsDynamiques
        {
            get
            {
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
            return FonctionsDynamiques.FirstOrDefault(f => f.IdFonction == strIdFonction);
        }

        #endregion
    }
}