using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using sc2i.formulaire.win32.controles2iWnd;

namespace sc2i.formulaire.win32
{
    [AutoExec("Autoexec")]
    public class CWndAllocateurForZoneMultiple : IControleWndFor2iWnd
    {
        private IControleWndFor2iWnd m_controleAlloue = null;


        //------------------------------------------------------------------------
        public static void Autoexec()
        {
            CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndZoneMultiple), typeof(CWndAllocateurForZoneMultiple));
        }


        #region IControleWndFor2iWnd Membres
        //------------------------------------------------------------------------
        public void CreateControle(CCreateur2iFormulaireV2 createur, C2iWnd wnd, System.Windows.Forms.Control parent, sc2i.expression.IFournisseurProprietesDynamiques fournisseurProprietes)
        {
            C2iWndZoneMultiple zone = wnd as C2iWndZoneMultiple;
            if (zone != null)
            {
                if (zone.UseChildOptimization)
                    m_controleAlloue = new CWndFor2iZoneMultipleCustomList();
                else
                    m_controleAlloue = new CWndFor2iZoneMultiple();
                m_controleAlloue.CreateControle(createur, wnd, parent, fournisseurProprietes);
            }

        }

        //------------------------------------------------------------------------
        public System.Windows.Forms.Control Control
        {
            get
            {
                if (m_controleAlloue != null)
                    return m_controleAlloue.Control;
                return null;
            }
        }

        //------------------------------------------------------------------------
        public System.Windows.Forms.ToolTip Tooltip
        {
            get
            {
                if (m_controleAlloue != null)
                    return m_controleAlloue.Tooltip;
                return null;
            }
            set
            {
                if (m_controleAlloue != null)
                    m_controleAlloue.Tooltip = value;
            }
        }

        #endregion

        #region IRuntimeFor2iWnd Membres

        //------------------------------------------------------------------------
        public bool IsRacineForEvenements
        {
            get
            {
                if (m_controleAlloue != null)
                    return m_controleAlloue.IsRacineForEvenements;
                return false;
            }
        }

        //------------------------------------------------------------------------
        public C2iWnd WndAssociee
        {
            get
            {
                if (m_controleAlloue != null)
                    return m_controleAlloue.WndAssociee;
                return null;
            }
        }

        //-------------------------------------------------------
        public void RefillControl()
        {
            SetElementEdite(EditedElement);
        }

        //------------------------------------------------------------------------
        public void SetElementEdite(object element)
        {
            if (m_controleAlloue != null)
                m_controleAlloue.SetElementEdite(element);
        }

        //------------------------------------------------------------------------
        public void SetElementEditeSansChangerLesValeursAffichees(object element)
        {
            if (m_controleAlloue != null)
                m_controleAlloue.SetElementEditeSansChangerLesValeursAffichees(element);
        }

        //------------------------------------------------------------------------
        public CResultAErreur MajChamps(bool bControlerLesValeursAvantValidation)
        {
            if (m_controleAlloue != null)
                return m_controleAlloue.MajChamps(bControlerLesValeursAvantValidation);
            return CResultAErreur.True;
        }

        //------------------------------------------------------------------------
        public void UpdateValeursCalculees()
        {
            if (m_controleAlloue != null)
                m_controleAlloue.UpdateValeursCalculees();
        }

        //------------------------------------------------------------------------
        public IRuntimeFor2iWnd[] Childs
        {
            get
            {
                if (m_controleAlloue != null)
                    return m_controleAlloue.Childs;
                return new IRuntimeFor2iWnd[0];
            }
        }

        //------------------------------------------------------------------------
        public void AppliqueRestriction(CRestrictionUtilisateurSurType restrictionSurElementEdite, CListeRestrictionsUtilisateurSurType listeRestriction, sc2i.common.Restrictions.IGestionnaireReadOnlySysteme gestionnaire)
        {
            if (m_controleAlloue != null)
                m_controleAlloue.AppliqueRestriction(restrictionSurElementEdite,
                    listeRestriction, 
                    gestionnaire);
        }

        //------------------------------------------------------------------------
        public object EditedElement
        {
            get
            {
                if (m_controleAlloue != null)
                    return m_controleAlloue.EditedElement;
                return null;
            }
        }

        //------------------------------------------------------------------------
        public void OnChangeParentModeEdition(bool bModeEdition)
        {
            if (m_controleAlloue != null)
                m_controleAlloue.OnChangeParentModeEdition(bModeEdition);
        }

        #endregion

        #region IWndAContainer Membres

        //------------------------------------------------------------------------
        public IWndAContainer WndContainer
        {
            get
            {
                if (m_controleAlloue != null)
                    return m_controleAlloue.WndContainer;
                return null;
            }
            set
            {
                if (m_controleAlloue != null)
                    m_controleAlloue.WndContainer = value;

            }
        }

        #endregion

        #region IConvertibleEnIElementAProprietesDynamiquesDeportees Membres

        //------------------------------------------------------------------------
        public IElementAProprietesDynamiquesDeportees ConvertToElementAProprietesDynamiquesDeportees()
        {
            if (m_controleAlloue != null)
                return m_controleAlloue.ConvertToElementAProprietesDynamiquesDeportees();
            return null;
        }

        #endregion
    }
}
