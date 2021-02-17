using System;
using System.Collections.Generic;
using System.Text;
using sc2i.formulaire.win32;
using sc2i.data.dynamic;
using sc2i.common;
using sc2i.data;
using sc2i.win32.common;
using sc2i.multitiers.client;
using System.Windows.Forms;
using sc2i.formulaire.win32.controles2iWnd;
using sc2i.formulaire.win32.datagrid;
using sc2i.common.Restrictions;
using sc2i.expression;
using System.Drawing;

namespace sc2i.win32.data.dynamic.controlesFor2iWnd
{
	[AutoExec("Autoexec")]
    public class CWndFor2iWndVariable : CControlWndFor2iWnd, IControlIncluableDansDataGrid
	{
		private IControleForVariable m_controleForVariable = null;
        private CVisualiseurReadOnly m_visualiseurReadOnly = null;

        private static Dictionary<Type, Type> m_dicTypesDeRemplacement = new Dictionary<Type, Type>();

        //-----------------------------------------------------------
        //Permet de remplacer un contrôle par un autre
        public static void RegisterTypeDeRemplacement(Type typeOriginal, Type typeRemplacant)
        {
            m_dicTypesDeRemplacement[typeOriginal] = typeRemplacant;
        }

        //-----------------------------------------------------------
        public static void UnRegisterTypeDeRemplacement(Type typeOriginal)
        {
            if (m_dicTypesDeRemplacement.ContainsKey(typeOriginal))
                m_dicTypesDeRemplacement.Remove(typeOriginal);
        }

		//-----------------------------------------------------------
		public static void Autoexec()
		{
			CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndChampCustom), typeof(CWndFor2iWndVariable));
			CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndVariableFiltreDyamique), typeof(CWndFor2iWndVariable));
		}

        //-----------------------------------------------------------
        private CVisualiseurReadOnly VisualiseurReadOnly
        {
            get
            {
                if (m_visualiseurReadOnly == null && m_controleForVariable != null && m_controleForVariable.Control != null)
                {
                    m_visualiseurReadOnly = new CVisualiseurReadOnly();
                    m_visualiseurReadOnly.ControlAssocie = m_controleForVariable.Control;
                    m_visualiseurReadOnly.Visible = false;
                }
                if (m_visualiseurReadOnly != null &&
                    m_visualiseurReadOnly.ControlAssocie != m_controleForVariable.Control)
                    m_visualiseurReadOnly.ControlAssocie = m_controleForVariable.Control;
                return m_visualiseurReadOnly;
            }
        }

        //-----------------------------------------------------------
        public IControleForVariable GetControle<T>()
            where T : class, IControleForVariable
        {
            Type tp = null;
            if (!m_dicTypesDeRemplacement.TryGetValue(typeof(T), out tp))
                tp = typeof(T);
            return (IControleForVariable)Activator.CreateInstance(tp, new object[0]);
        }

        //-----------------------------------------------------------
        public event EventHandler OnValueChanged;

		//-----------------------------------------------------------
        public object Value
        {
            get
            {
                if (m_controleForVariable != null)
                    return m_controleForVariable.Value;
                return null;
            }
            set
            {
                if (m_controleForVariable != null)
                {
                    m_controleForVariable.Value = value;
                    CUtilControlesWnd.DeclencheEvenement(C2iWndVariable.c_strIdEvenementValueChanged,
                        this);
                    if (OnValueChanged != null)
                        OnValueChanged(this, new EventArgs());
                }                    
            }
        }

        //-----------------------------------------------------------
        public override bool LockEdition
        {
            get
            {
                return base.LockEdition;
            }
            set
            {
                base.LockEdition = value;
            }
        }

        
		//-----------------------------------------------------------
		protected override void MyCreateControle(CCreateur2iFormulaireV2 createur, sc2i.formulaire.C2iWnd wnd, System.Windows.Forms.Control parent, sc2i.expression.IFournisseurProprietesDynamiques fournisseurProprietes)
		{
			if (WndVariable == null | WndVariable.Variable == null)
				return;
			IVariableDynamique variable = WndVariable.Variable;
			if (variable.IsChoixParmis())
			{
				if ((variable is CVariableDynamiqueSelectionObjetDonnee &&
					((CVariableDynamiqueSelectionObjetDonnee)variable).UtiliserRechercheRapide) ||
					(variable is CChampCustom &&
					typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(variable.TypeDonnee.TypeDotNetNatif) &&
					WndVariable is C2iWndChampCustom &&
					!((C2iWndChampCustom)WndVariable).UtiliserUneCombo)
					)
					m_controleForVariable = GetControle<CControleForVariableRechercheRapide>();
				else
                    m_controleForVariable = GetControle<CControleForVariableComboBox>();
			}
			else if (variable.TypeDonnee.TypeDotNetNatif == typeof(bool))
				m_controleForVariable = GetControle<CControleForVariableCheckBox>();
            else if (variable.TypeDonnee.TypeDotNetNatif == typeof(string))
            {
                m_controleForVariable = GetControle<CControleForVariableTextBox>();
            }
            else if (variable.TypeDonnee.TypeDotNetNatif == typeof(int))
                m_controleForVariable = GetControle<CControleForVariableTextBoxEntier>();
            else if (variable.TypeDonnee.TypeDotNetNatif == typeof(double))
            {
                if (variable.ClasseUnite != null)
                {
                    m_controleForVariable = GetControle<CControleForVariableTextBoxAvecUnite>();
                }
                else
                    m_controleForVariable = GetControle<CControleForVariableTextBoxDouble>();
            }
            else if (variable.TypeDonnee.TypeDotNetNatif == typeof(DateTime))
            {
                m_controleForVariable = GetControle<CControleForVariableDateTimePicker>();
            }
			if (m_controleForVariable != null)
			{
				m_controleForVariable.SetVariable(variable);
				System.Windows.Forms.Control ctrl = m_controleForVariable.Control;
                m_controleForVariable.WndFor2iVariable = this;
				if (ctrl != null)
				{
					parent.Controls.Add(ctrl);
                    m_controleForVariable.FillFrom2iWnd(wnd);
					CCreateur2iFormulaireV2.AffecteProprietesCommunes(WndVariable, ctrl);
					m_controleForVariable.LockEdition = LockEdition;
				}
                m_controleForVariable.ValueChanged += new EventHandler(m_controleForVariable_ValueChanged);
			}
		}

        //-------------------------------------------------------------
        void m_controleForVariable_ValueChanged(object sender, EventArgs e)
        {
            C2iWndChampCustom wndChamp = WndAssociee as C2iWndChampCustom;
            if (wndChamp != null && wndChamp.AutoSetValue)
                MajChamps(false);
            if (m_gridView != null)
                m_gridView.NotifyCurrentCellDirty(true);
            CUtilControlesWnd.DeclencheEvenement(C2iWndVariable.c_strIdEvenementValueChanged, this);
            if (OnValueChanged != null)
                OnValueChanged(this, new EventArgs());
        }

       

		//-----------------------------------------------------------
		private C2iWndVariable WndVariable
		{
			get
			{
				return WndAssociee as C2iWndVariable;
			}
		}

		//-----------------------------------------------------------
		public override Control Control
		{
			get
			{
				if (m_controleForVariable != null)
					return m_controleForVariable.Control;
				return null;
			}
		}

		//-----------------------------------------------------------
		protected override void OnChangeElementEdite(object element)
		{
			if (m_controleForVariable != null)
			{
				IElementAVariables eltAVariables = element as IElementAVariables;
				m_controleForVariable.SetElementEdite(eltAVariables);
			}
		}

        //-----------------------------------------------------------
        public override void SetElementEditeSansChangerLesValeursAffichees(object element)
        {
            if (m_controleForVariable != null)
                m_controleForVariable.SetElementEditeSansChangerLesValeursAffichees(element as IElementAVariables);
        }

		//-----------------------------------------------------------
		protected override CResultAErreur MyMajChamps(bool bControlerLesValeursAvantValidation)
		{
			CResultAErreur result = CResultAErreur.True;
			if (m_controleForVariable != null && !LockEdition)
				result = m_controleForVariable.MajChamps(bControlerLesValeursAvantValidation);
			return result;
		}

		//-----------------------------------------------------------
		protected override void MyUpdateValeursCalculees()
		{
		}

		//---------------------------------------------
        protected override void MyAppliqueRestriction(
            CRestrictionUtilisateurSurType restrictionSurObjetEdite,
            CListeRestrictionsUtilisateurSurType listeRestrictions,
            IGestionnaireReadOnlySysteme gestionnaireReadOnly)
		{
            bool bShowPictureRestriction = false;
			if (listeRestrictions != null && EditedElement != null)
			{
				CChampCustom champ = WndVariable.Variable as CChampCustom;
                if (champ != null && restrictionSurObjetEdite != null && m_controleForVariable.Control != null)
				{
                    ERestriction rest = restrictionSurObjetEdite.GetRestriction(champ.CleRestriction);
                    if ((rest & ERestriction.ReadOnly) == ERestriction.ReadOnly)
                    {
                        gestionnaireReadOnly.SetReadOnly(m_controleForVariable.Control, true);
                        bShowPictureRestriction = true;
                    }
				}
			}
            if (VisualiseurReadOnly != null)
            {
                if (bShowPictureRestriction)
                {
                    VisualiseurReadOnly.Visible = true;
                }
                else
                    VisualiseurReadOnly.Visible = false;
            }
		}

        #region IControlIncluableDansDataGrid Membres

        private DataGridView m_gridView = null;
        public DataGridView DataGrid
        {
            get
            {
                return m_gridView;
            }
            set
            {
                m_gridView = value;
            }
        }

        public bool WantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            if (m_controleForVariable != null)
                return m_controleForVariable.WantsInputKey(keyData, dataGridViewWantsInputKey);
            return false;
        }

        public void SelectAll()
        {
            if (m_controleForVariable != null)
                m_controleForVariable.SelectAll();
        }

        #endregion
    }
}
