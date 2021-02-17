using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.common;
using sc2i.common.Restrictions;

namespace sc2i.formulaire.win32.controles2iWnd
{
    public class CWndFor2iControleExterne : CControlWndFor2iWnd, IElementAProprietesDynamiquesDeportees
    {
        IControleFormulaireExterne m_controleAssocie = null;

        public CWndFor2iControleExterne(IControleFormulaireExterne controleAssocie)
        {
            m_controleAssocie = controleAssocie;
        }

        protected override void OnChangeElementEdite(object element)
        {
        }

        protected override void MyCreateControle(CCreateur2iFormulaireV2 createur, C2iWnd wnd, Control parent, sc2i.expression.IFournisseurProprietesDynamiques fournisseur)
        {
            if (m_controleAssocie != null)
                m_controleAssocie.AttacheToWndFor2iWnd(this);
        }

        public void AddChild(IRuntimeFor2iWnd child)
        {
            List<IRuntimeFor2iWnd> lst = new List<IRuntimeFor2iWnd>(Childs);
            lst.Add(child);
            SetControlesFils(lst.ToArray());
        }

        public override Control Control
        {
            get { return m_controleAssocie.Control as Control; }
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





        #region IElementAProprietesDynamiquesDeportees Membres

        public object GetValeurDynamiqueDeportee(string strPropriete)
        {
            IElementAProprietesDynamiquesDeportees ctrl = m_controleAssocie as IElementAProprietesDynamiquesDeportees;
            if (ctrl != null)
                return ctrl.GetValeurDynamiqueDeportee(strPropriete);
            return null;
        }

        public void SetValeurDynamiqueDeportee(string strPropriete, object valeur)
        {
            IElementAProprietesDynamiquesDeportees ctrl = m_controleAssocie as IElementAProprietesDynamiquesDeportees;
            if (ctrl != null)
                ctrl.SetValeurDynamiqueDeportee(strPropriete, valeur);
        }

        #endregion
    }
}