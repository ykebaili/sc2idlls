using System;
using System.Collections.Generic;
using System.Text;
using sc2i.data.dynamic;
using System.Windows.Forms;
using sc2i.common;
using sc2i.win32.common;
using sc2i.formulaire;
using sc2i.data;
using System.Collections;
using sc2i.expression;
using sc2i.win32.data.dynamic.controlesFor2iWnd;

namespace sc2i.win32.data.dynamic
{
	public class CControleForVariableComboBox : IControleForVariable
	{
		public const string c_champElementSource = "Source element";
		private IVariableDynamique m_variable;
		private IElementAVariables m_elementAVariables = null;
		private bool m_bLockEdition = false;
        public event EventHandler ValueChanged;


        
		//Peut être un CComboBoxArbreObjetDonneesHierarchique si l'objet est un objet hiérarchique
		private Control m_combo = null;

        private CWndFor2iWndVariable m_wndFor2iVariable;
        public CWndFor2iWndVariable WndFor2iVariable
        {
            get
            {
                return m_wndFor2iVariable;
            }
            set
            {
                m_wndFor2iVariable = value;
            }
        }



        public object Value
        {
            get
            {
                if (m_combo is C2iComboBox)
				{
                    return ((C2iComboBox)m_combo).SelectedValue;
				}
                else if (m_combo is ISelectionneurElementListeObjetsDonnees)
                {
                    return ((ISelectionneurElementListeObjetsDonnees)m_combo).ElementSelectionne;
                }
                return null;
            }
            set
            {
                if (m_combo is C2iComboBox)
                {
                    ((C2iComboBox)m_combo).SelectedValue = value;
                }
                else if (m_combo is ISelectionneurElementListeObjetsDonnees)
                {
                    ((ISelectionneurElementListeObjetsDonnees)m_combo).ElementSelectionne =  value as CObjetDonnee;
                }
            }
        }


		public void SetVariable(IVariableDynamique variable)
		{
			m_variable = variable;
			AlloueCombo();
		}

		public void SetElementEdite(IElementAVariables element)
		{
			m_elementAVariables = element;
			AlloueCombo();
			if (m_variable != null && element != null)
			{
				object valeur = element.GetValeurChamp(m_variable.IdVariable);
                if (m_combo is C2iComboBox)
                {
                    if (valeur != null)
                        ((C2iComboBox)m_combo).SelectedValue = valeur;
                    else
                        ((C2iComboBox)m_combo).SelectedValue = "";
                    object obj = ((C2iComboBox)m_combo).SelectedValue;
                }
                else if (m_combo is ISelectionneurElementListeObjetsDonnees)
                {
                    CVariableDynamiqueSelectionObjetDonnee varSel = m_variable as CVariableDynamiqueSelectionObjetDonnee;
                    if (varSel != null && !(valeur is CObjetDonnee))
                    {
                        CReferenceObjetDonnee refObj = varSel.GetObjetFromValeurRetournee(valeur);
                        if (refObj != null)
                            valeur = refObj.GetObjet(CSc2iWin32DataClient.ContexteCourant);
                    }
					if (valeur is CObjetDonnee || valeur == null)
						((ISelectionneurElementListeObjetsDonnees)m_combo).ElementSelectionne = (CObjetDonnee)valeur;

				}
			}

				
		}

        //--------------------------------------------------
        public void SetElementEditeSansChangerLesValeursAffichees(IElementAVariables element)
        {
            m_elementAVariables = element;
        }

		//-----------------------------------------------------------
		public void FillFrom2iWnd(C2iWnd wnd)
		{
		}
			

		//-----------------------------------------------------------
		public void AlloueCombo()
		{
			if (m_variable == null)
				return;
			Type typeElement = m_variable.TypeDonnee.TypeDotNetNatif;
			if (m_variable is CVariableDynamiqueSelectionObjetDonnee)
				typeElement = ((CVariableDynamiqueSelectionObjetDonnee)m_variable).FiltreSelection.TypeElements;
			//Combos hiérarchiques
			if (typeof(IObjetDonneeAutoReference).IsAssignableFrom(typeElement) &&
				typeof(IObjetHierarchiqueACodeHierarchique).IsAssignableFrom(typeElement))
			{
				try
				{
					if (!(m_combo is CComboBoxArbreObjetDonneesHierarchique) && m_combo != null)
					{
						m_combo.Visible = false;
						m_combo.Parent.Controls.Remove(m_combo);
						m_combo.Dispose();
						m_combo = null;
					}

					CComboBoxArbreObjetDonneesHierarchique arbre;
                    if (m_combo is CComboBoxArbreObjetDonneesHierarchique)
                        arbre = (CComboBoxArbreObjetDonneesHierarchique)m_combo;
                    else
                    {
                        arbre = new CComboBoxArbreObjetDonneesHierarchique();
                        arbre.ElementSelectionneChanged += new EventHandler(arbre_ElementSelectionneChanged);
                    }
					CFiltreData filtre = null;
					Type tp = m_variable.TypeDonnee.TypeDotNetNatif;
                    string strChamp = DescriptionFieldAttribute.GetDescriptionField(tp, "DescriptionElement", true);
                    string strTextNull = "";
                    if (m_variable is CVariableDynamiqueSelectionObjetDonnee)
					{
						CVariableDynamiqueSelectionObjetDonnee varSel = (CVariableDynamiqueSelectionObjetDonnee)m_variable;
						tp = ((CVariableDynamiqueSelectionObjetDonnee)m_variable).FiltreSelection.TypeElements;
						CFiltreDynamique filtreDyn = varSel.FiltreSelection;
						filtre = GetFiltre(filtreDyn);
                        strChamp = DescriptionFieldAttribute.GetDescriptionField(tp, "DescriptionElement", true);
                        strTextNull = varSel.TextNull;
					}
					if (m_variable is CChampCustom)
					{
						CFiltreDynamique filtreDyn = ((CChampCustom)m_variable).FiltreObjetDonnee;
						filtre = GetFiltre(filtreDyn);
					}

					using ( CContexteDonnee contexte = new CContexteDonnee ( 1, true, false ))
					{
						CObjetDonnee objet = (CObjetDonnee)Activator.CreateInstance(tp, new object[] { contexte });
						arbre.Init(tp,
							((IObjetDonneeAutoReference)objet).ProprieteListeFils,
							((IObjetDonneeAutoReference)objet).ChampParent,
							strChamp,
							filtre,
							null
							);
                        if (strTextNull != "")
                            arbre.TextNull = strTextNull;
					}
					m_combo = arbre;
					return;
				}
				catch { }
			}
			IList lst = null;
			lst = m_variable.Valeurs;
			if (m_variable is CChampCustom && ((CChampCustom)m_variable).TypeDonneeChamp.TypeDonnee == TypeDonnee.tObjetDonneeAIdNumeriqueAuto &&
				lst is CListeObjetsDonnees)
			{
				CFiltreData filtre = ((CListeObjetsDonnees)lst).Filtre;
				CFiltreDynamique filtreDyn = ((CChampCustom)m_variable).FiltreObjetDonnee;
				if (filtreDyn != null)
				{
					filtre = GetFiltre(filtreDyn);
				}

				((CListeObjetsDonnees)lst).Filtre = filtre;
			}
			else
				lst = m_variable.Valeurs;
			C2iComboBox combo = null;
			if (lst is CListeObjetsDonnees && ((CListeObjetsDonnees)lst).TypeObjets != typeof(CValeurChampCustom))
			{
				CComboBoxListeObjetsDonnees cmbListe = null;
				if (m_combo is CComboBoxListeObjetsDonnees)
					cmbListe = (CComboBoxListeObjetsDonnees)m_combo;
				else
				{
					if (m_combo != null)
					{
						m_combo.Visible = false;
						m_combo.Parent.Controls.Remove(m_combo);
						m_combo.Dispose();
						m_combo = null;
					}
					cmbListe = new CComboBoxListeObjetsDonnees();
                    cmbListe.SelectedValueChanged += new EventHandler(cmbListe_SelectedValueChanged);
				}
				cmbListe.NullAutorise = true;
				cmbListe.Init((CListeObjetsDonnees)lst,
					DescriptionFieldAttribute.GetDescriptionField(m_variable.TypeDonnee.TypeDotNetNatif, "DescriptionElement"),
					true);
				combo = cmbListe;
			}
			else
			{
				if ( m_combo != null && m_combo.GetType() == typeof(C2iComboBox ))
					combo = (C2iComboBox)m_combo;
				else
				{
					if (m_combo != null)
					{
						m_combo.Visible = false;
						m_combo.Parent.Controls.Remove(m_combo);
						m_combo.Dispose();
						m_combo = null;
					}
					combo = new C2iComboBox();
                    combo.SelectionChangeCommitted += new EventHandler(combo_SelectionChangeCommitted);
				}
				combo.DisplayMember = "Display";
				combo.ValueMember = "Value";
				combo.DataSource = lst;
			}
			combo.DropDownStyle = ComboBoxStyle.DropDownList;
			combo.Sorted = false;
			combo.IsLink = false;
			if (m_combo == null)
				combo.CreateControl();
			m_combo = combo;
		}

        void cmbListe_SelectedValueChanged(object sender, EventArgs e)
        {
            OnChangeValue();
        }

        void combo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            OnChangeValue();
        }

        void arbre_ElementSelectionneChanged(object sender, EventArgs e)
        {
            OnChangeValue();
        }

        private void OnChangeValue()
        {
            if (ValueChanged != null)
                ValueChanged(this, new EventArgs());
        }

		private CFiltreData GetFiltre(CFiltreDynamique filtre)
		{
			if (filtre == null )
				return null;
			if (m_elementAVariables != null)
			{
				IVariableDynamique variable = AssureVariableElementCible(filtre, m_elementAVariables.GetType());
				filtre.SetValeurChamp(variable.IdVariable, m_elementAVariables);
			}
			CResultAErreur result = filtre.GetFiltreData();
			if (result)
				return (CFiltreData)result.Data;
			return null;
		}

		public static IVariableDynamique AssureVariableElementCible(CFiltreDynamique filtre, Type typeElement)
		{
			IVariableDynamique variableASupprimer = null;
			foreach (IVariableDynamique variable in filtre.ListeVariables)
			{
				if (variable.Nom == c_champElementSource)
				{
					if (variable.TypeDonnee.TypeDotNetNatif != typeElement)
						variableASupprimer = variable;
					else
						return variable;
				}
			}
			if (variableASupprimer != null)
				filtre.RemoveVariable(variableASupprimer);
			CVariableDynamiqueSysteme newVariable = new CVariableDynamiqueSysteme(filtre);
			newVariable.Nom = c_champElementSource;
			newVariable.SetTypeDonnee(new sc2i.expression.CTypeResultatExpression(typeElement, false));
			filtre.AddVariablePropreAuFiltre(newVariable);
			return newVariable;
		}


		//------------------------------------------------------
		public CResultAErreur MajChamps(bool bControlerValeur)
		{
			CResultAErreur result = CResultAErreur.True;
			if (m_elementAVariables != null && m_variable != null)
			{
				object valeur = null;
				if (m_combo is C2iComboBox)
					valeur = ((C2iComboBox)m_combo).SelectedValue;
				else if (m_combo is ISelectionneurElementListeObjetsDonnees)
				{
					valeur = ((ISelectionneurElementListeObjetsDonnees)m_combo).ElementSelectionne;
					if (m_variable is CVariableDynamiqueSelectionObjetDonnee)
					{
						CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(valeur);
						CResultAErreur resTmp = ((CVariableDynamiqueSelectionObjetDonnee)m_variable).ExpressionRetournee.Eval(ctx);
						if (resTmp)
							valeur = resTmp.Data;
					}
				}
				m_elementAVariables.SetValeurChamp(m_variable.IdVariable, valeur);
				if (bControlerValeur)
					result = m_variable.VerifieValeur(valeur);
			}
			return result;
		}

		//-----------------------------------------------------------
		public Control Control
		{
			get
			{
				return m_combo;
			}
		}

		//-----------------------------------------------------------
		public bool LockEdition
		{
			get
			{
				return m_bLockEdition;
			}
			set
			{
				m_bLockEdition = value;
				IControlALockEdition ctrlLock = m_combo as IControlALockEdition;
				if (ctrlLock != null)
					ctrlLock.LockEdition = value;
				else if ( m_combo != null )
					m_combo.Enabled = !value;
				if ( OnChangeLockEdition != null )
					OnChangeLockEdition ( this, new EventArgs() );
			}
		}
		public event EventHandler OnChangeLockEdition;

        //-----------------------------------------------------------
        public void SelectAll()
        {
        }

        public bool WantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            return !dataGridViewWantsInputKey;
        }


	}


		
}
