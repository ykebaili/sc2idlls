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
	public class CControleForVariableRechercheRapide : IControleForVariable
	{
		public const string c_champElementSource = "Source element";
		private IVariableDynamique m_variable;
		private IElementAVariables m_elementAVariables = null;
		private bool m_bLockEdition = false;

		private C2iTextBoxFiltreRapide m_control =null;

        public event EventHandler ValueChanged;


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
                if (m_control != null)
                {
                    return (object)m_control.ElementSelectionne;
                }
                else
                {
                    return (null);
                }
            }
            set
            {
                if (m_control != null)
                {
                    m_control.ElementSelectionne = value as CObjetDonnee;
                }

            }
        }


		public void SetVariable(IVariableDynamique variable)
		{
			m_variable = variable;
			AlloueControl();
		}

		//--------------------------------------------------------
		public void SetElementEdite(IElementAVariables element)
		{
			m_elementAVariables = element;
			AlloueControl();
			if (m_elementAVariables != null && element != null)
			{
				object selVal = ((IElementAVariables)m_elementAVariables).GetValeurChamp(m_variable.IdVariable);
				try
				{
                    CVariableDynamiqueSelectionObjetDonnee variableSel = m_variable as CVariableDynamiqueSelectionObjetDonnee;
                    if ( variableSel != null )
                    {
                        CReferenceObjetDonnee refObj = variableSel.GetObjetFromValeurRetournee(selVal);
                        if (refObj != null)
                            selVal = refObj.GetObjet(CSc2iWin32DataClient.ContexteCourant);
                        /*try
                        {
                            CObjetDonneeAIdNumerique obj = Activator.CreateInstance(variableSel.FiltreSelection.TypeElements,
                                new object[] { CSc2iWin32DataClient.ContexteCourant }) as CObjetDonneeAIdNumerique;
                            if (obj != null && obj.ReadIfExists ( (int)selVal))
                                selVal = obj;
                        }
                        catch
                        {
                        }*/
                    }
					if (m_control != null)
						m_control.ElementSelectionne = (CObjetDonnee)selVal;
				}
				catch { }
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
		public void AlloueControl()
		{
			if (m_variable == null)
				return;
			Type typeElements = null;
			string strPropAffichee = "";
			CFiltreData filtre = null;
			if (m_variable is CVariableDynamiqueSelectionObjetDonnee)
			{
				C2iExpression expression = ((CVariableDynamiqueSelectionObjetDonnee)m_variable).ExpressionAffichee;
				if (expression is C2iExpressionChamp)
					strPropAffichee = ((C2iExpressionChamp)expression).DefinitionPropriete.NomProprieteSansCleTypeChamp;
				CFiltreDynamique filtreDyn = ((CVariableDynamiqueSelectionObjetDonnee)m_variable).FiltreSelection;
				filtre = GetFiltre(filtreDyn);
				typeElements = filtreDyn.TypeElements;
			}
			else if (m_variable is CChampCustom)
			{
				CFiltreDynamique filtreDyn = ((CChampCustom)m_variable).FiltreObjetDonnee;
				if (filtreDyn != null)
				{
					filtre = GetFiltre(filtreDyn);
				}
				typeElements = ((CChampCustom)m_variable).TypeObjetDonnee;
			}

			if (m_control == null)
			{
				m_control = new C2iTextBoxFiltreRapide();
                m_control.ElementSelectionneChanged += new EventHandler(m_control_ElementSelectionneChanged);
			}
			if (strPropAffichee == "")
			{
				strPropAffichee = DescriptionFieldAttribute.GetDescriptionField(typeElements, "DescriptionElement");
			}
			m_control.InitAvecFiltreDeBase(typeElements,
				strPropAffichee,
				filtre, true);
			
		}

        void m_control_ElementSelectionneChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(this, e);
        }

		//-----------------------------------------------------------
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
				if (m_control is ISelectionneurElementListeObjetsDonnees)
				{
					valeur = ((ISelectionneurElementListeObjetsDonnees)m_control).ElementSelectionne;
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
				return m_control;
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
				if (m_control != null)
					m_control.LockEdition = value;
				if ( OnChangeLockEdition != null )
					OnChangeLockEdition ( this, new EventArgs() );
			}
		}
		public event EventHandler OnChangeLockEdition;

        public void SelectAll()
        {
            if ( m_control != null )
                m_control.SelectAll();
        }

        public bool WantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            if (m_control != null)
                return m_control.WantsInputKey(keyData, dataGridViewWantsInputKey);
            return false;
            
        }
	}




		
}
