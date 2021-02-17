using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using sc2i.common;
using sc2i.data;
using sc2i.expression;
using sc2i.formulaire;
using sc2i.formulaire.win32;
using sc2i.win32.common;

using sc2i.data.dynamic;
using sc2i.formulaire.win32.controles2iWnd;
using sc2i.common.Restrictions;

namespace sc2i.win32.data.dynamic
{
    [AutoExec("Autoexec")]
    public partial class CWndFor2iWndSelectionValeurChamp : UserControl, IControleWndFor2iWnd, IControlALockEdition
    {
        private CValeurChampCustom m_valeurSelectionne = null;
        private List<CValeurChampCustom> m_listeValeurs = new List<CValeurChampCustom>();
        private C2iWndSelectionValeurChamp m_wndSelectionValeurChamp;
        private CCreateur2iFormulaireV2 m_createur = null;
        private object m_elementEdite = null;
        private IWndAContainer m_wndContainer = null;
        private CListeRestrictionsUtilisateurSurType m_listeRestrictions = null;
        private IGestionnaireReadOnlySysteme m_gestionnaireReadOnly = null;


        /// ///////////////////////////////////////////
        public CWndFor2iWndSelectionValeurChamp()
        {
            InitializeComponent();
        }

        public static void Autoexec()
        {
            CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndSelectionValeurChamp), typeof(CWndFor2iWndSelectionValeurChamp));
        }

        /// ///////////////////////////////////////////
        public ToolTip Tooltip { get; set; }

        /// ///////////////////////////////////////////
        public object EditedElement
        {
            get
            {
                return m_elementEdite;
            }
        }

        /// ///////////////////////////////////////////
        /// 
        /// ///////////////////////////////////////////
        public IRuntimeFor2iWnd[] Childs
        {
            get
            {
                return new IRuntimeFor2iWnd[0];
            }
        }


        #region IControleWndFor2iWnd Membres

        //---------------------------------------
        public Control Control
        {
            get { return this; }
        }

        //---------------------------------------
        public C2iWnd WndAssociee
        {
            get { return m_wndSelectionValeurChamp; }
        }

        //// ///////////////////////////////////////////
        /// Methode de création du control
        /// ///////////////////////////////////////////
        public void CreateControle(CCreateur2iFormulaireV2 createur, C2iWnd wnd, Control parent, IFournisseurProprietesDynamiques fournisseurProprietes)
        {
            C2iWndSelectionValeurChamp controlSelectionValeurChamp = wnd as C2iWndSelectionValeurChamp;
            if (controlSelectionValeurChamp == null)
                return;
            m_wndSelectionValeurChamp = controlSelectionValeurChamp;
            m_createur = createur;

            CCreateur2iFormulaireV2.AffecteProprietesCommunes(wnd, this);
            parent.Controls.Add(this);
            m_panorama.ButtonHeight = controlSelectionValeurChamp.ButtonHeight;
            m_panorama.ButtonWidth = controlSelectionValeurChamp.ButtonWidth;
            m_panorama.ButtonHorizontalMargin = controlSelectionValeurChamp.ButtonHorizontalMargin;
            m_panorama.ButtonVerticalMargin = controlSelectionValeurChamp.ButtonVerticalMargin;
            m_panorama.ButtonColor = controlSelectionValeurChamp.ButtonColor;
            m_panorama.MaxLineCount = m_wndSelectionValeurChamp.Size.Height / controlSelectionValeurChamp.ButtonHeight;

            m_listeValeurs.Clear();
            HashSet<string> setToAdd = null;
            if ( m_wndSelectionValeurChamp.LimitToValuesList != null && 
                m_wndSelectionValeurChamp.LimitToValuesList.Count() > 0 )
            {
                setToAdd = new HashSet<string>();
                foreach (string strValeur in m_wndSelectionValeurChamp.LimitToValuesList)
                    setToAdd.Add ( strValeur.ToUpper() );
            }
            foreach (CValeurChampCustom val in m_wndSelectionValeurChamp.ChampCustom.Valeurs)
            {
                if (setToAdd == null || setToAdd.Contains(val.Value.ToString()))
                    m_listeValeurs.Add(val);
            }

            List<object> lst = new List<object>();
            foreach (object obj in m_listeValeurs)
                lst.Add(obj);
            UpdateVisuelFromSelection();
            m_panorama.Init(lst, "");
        }

        #endregion


        #region IRuntimeFor2iWnd Membres

        //-------------------------------------------------------
        public void RefillControl()
        {
            SetElementEdite(EditedElement);
        }
        /// ///////////////////////////////////////////
        /// 
        /// ///////////////////////////////////////////
        public void SetElementEdite(object element)
        {
            m_elementEdite = element;
            IObjetDonneeAChamps elt = m_elementEdite as IObjetDonneeAChamps;
            
            if (elt != null && m_wndSelectionValeurChamp.ChampCustom != null)
            {
                object valeur = elt.GetValeurChamp(m_wndSelectionValeurChamp.ChampCustom.Id);
                if (valeur != null)
                {
                    foreach (CValeurChampCustom value in m_wndSelectionValeurChamp.ChampCustom.Valeurs)
                    {
                        if (valeur.ToString() == value.Value.ToString())
                        {
                            m_valeurSelectionne = value;
                        }
                    }
                }
                else
                    m_valeurSelectionne = null;
            }
            UpdateVisuelFromSelection();
        }

        [DynamicField("SelectedValue")]
        public object SelectedValue
        {
            get
            {
                if (m_valeurSelectionne == null)
                    return null;
                return m_valeurSelectionne.Value;
            }
            set
            {
                if (m_listeValeurs != null)
                {
                    foreach (CValeurChampCustom valeur in m_listeValeurs)
                    {
                        if (valeur.Value.Equals(value))
                        {
                            m_valeurSelectionne = valeur;
                            UpdateVisuelFromSelection();
                            CUtilControlesWnd.DeclencheEvenement(C2iWndSelectionValeurChamp.c_strIdEvenementValueChanged, this);
                            break;
                        }
                    }
                }
            }
        }
                


        /// ///////////////////////////////////////////
        /// 
        /// ///////////////////////////////////////////
        public void SetElementEditeSansChangerLesValeursAffichees(object element)
        {
            m_elementEdite = element;
        }

        /// ///////////////////////////////////////////
        /// 
        /// ///////////////////////////////////////////
        public void AppliqueRestriction(
                CRestrictionUtilisateurSurType restrictionSurElementEdite,
                CListeRestrictionsUtilisateurSurType listeRestriction,
                IGestionnaireReadOnlySysteme gestionnaire)
        {
            if (listeRestriction != null && EditedElement != null)
            {
                CChampCustom champ = m_wndSelectionValeurChamp.ChampCustom;
                if (champ != null && restrictionSurElementEdite != null)
                {
                    ERestriction rest = restrictionSurElementEdite.GetRestriction(champ.CleRestriction);
                    if ((rest & ERestriction.ReadOnly) == ERestriction.ReadOnly)
                        gestionnaire.SetReadOnly(Control, true);
                }
            }

        }


        /// ///////////////////////////////////////////
        /// 
        /// ///////////////////////////////////////////
        public bool IsRacineForEvenements
        {
            get { return false; }
        }

        /// ///////////////////////////////////////////
        /// 
        /// ///////////////////////////////////////////
        public void OnChangeParentModeEdition(bool bModeEdition)
        {
            bool bMyModeEdition = bModeEdition;
            if (m_wndSelectionValeurChamp != null && m_wndSelectionValeurChamp.LockMode == C2iWnd.ELockMode.DisableOnEdit)
                bMyModeEdition = !bModeEdition;
            if (WndAssociee != null && WndAssociee.LockMode != C2iWnd.ELockMode.Independant)
                LockEdition = !bMyModeEdition;
            foreach (IControleWndFor2iWnd ctrlFils in Childs)
                ctrlFils.OnChangeParentModeEdition(bMyModeEdition);
        }

        /// ///////////////////////////////////////////
        /// 
        /// ///////////////////////////////////////////
        public CResultAErreur MajChamps(bool bControlerLesValeursAvantValidation)
        {
            CResultAErreur result = CResultAErreur.False;
            IObjetDonneeAChamps elt = m_elementEdite as IObjetDonneeAChamps;
            if (elt != null && m_wndSelectionValeurChamp.ChampCustom != null)
            {
                result = elt.SetValeurChamp(m_wndSelectionValeurChamp.ChampCustom, m_valeurSelectionne == null ? null : m_valeurSelectionne.Value);
            }
            return result;
        }

        /// ///////////////////////////////////////////
        /// 
        /// ///////////////////////////////////////////
        public void UpdateValeursCalculees()
        {

        }

        /// ///////////////////////////////////////////
        /// 
        /// ///////////////////////////////////////////
        public CResultAErreur OnDeleteElement(object element)
        {
            CResultAErreur result = CResultAErreur.True;
            return result;
        }

        #endregion


        /// ///////////////////////////////////////////
        /// 
        /// ///////////////////////////////////////////
        public IElementAProprietesDynamiquesDeportees ConvertToElementAProprietesDynamiquesDeportees()
        {
            return null;
        }

        public IWndAContainer WndContainer
        {
            get
            {
                return m_wndContainer;
            }
            set
            {
                m_wndContainer = value;
            }
        }

        private void m_panorama_OnCalcButtonText(object sender, CalcButtonTextEventArgs args)
        {
            CValeurChampCustom valeur = args.Objet as CValeurChampCustom;
            if (valeur != null)
                args.Libelle = valeur.Display;
        }

        private void m_panorama_OnSelectObject(object sender, OnSelectObjectEventArgs args)
        {
            CValeurChampCustom valeur = args.ObjectSelectionne as CValeurChampCustom;
            if (valeur != null)
            {
                if (valeur.Equals(m_valeurSelectionne))
                {
                    m_valeurSelectionne = null;
                }
                else
                {
                    m_valeurSelectionne = valeur;
                }
                UpdateVisuelFromSelection();
                CUtilControlesWnd.DeclencheEvenement(C2iWndSelectionValeurChamp.c_strIdEvenementValueChanged, this);
            }
        }

        private void UpdateVisuelFromSelection()
        {
            if (m_valeurSelectionne == null)
            {
                m_panorama.ClearSpecificColors();
                m_panorama.ClearInvalidateValues();
            }
            else
            {
                foreach (CValeurChampCustom v in m_listeValeurs)
                {
                    m_panorama.InvalideValue(v, true);
                    m_panorama.SetSpecificColor(v, m_wndSelectionValeurChamp.ButtonInvalideColor);
                }
                m_panorama.InvalideValue(m_valeurSelectionne, false);
                m_panorama.SetSpecificColor(m_valeurSelectionne, m_wndSelectionValeurChamp.ButtonSelectedColor);
            }
        }

        #region IControlALockEdition Membres

        public bool LockEdition
        {
            get
            {
                return !m_gestionnaireModeEdition.ModeEdition;
            }
            set
            {
                m_gestionnaireModeEdition.ModeEdition =! value;
                if (OnChangeLockEdition != null)
                    OnChangeLockEdition(this, new EventArgs());
            }
        }

        public event EventHandler OnChangeLockEdition;

        #endregion
    }
}