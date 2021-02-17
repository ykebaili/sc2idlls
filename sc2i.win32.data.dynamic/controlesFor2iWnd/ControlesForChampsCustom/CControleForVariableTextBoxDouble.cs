using System;
using System.Collections.Generic;
using System.Text;
using sc2i.data.dynamic;
using System.Windows.Forms;
using sc2i.common;
using sc2i.win32.common;
using sc2i.formulaire;
using sc2i.formulaire.win32.controles2iWnd;
using sc2i.win32.data.dynamic.controlesFor2iWnd;
using sc2i.expression;

namespace sc2i.win32.data.dynamic
{
	public class CControleForVariableTextBoxDouble : C2iTextBoxNumerique, IControleForVariable
	{
		private IVariableDynamique m_variable;
		private IElementAVariables m_elementAVariables = null;

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
                return (object)DoubleValue;
            }
            set
            {
                DoubleValue = (double?)value;
            }
        }

		//------------------------------------------------------
		public CControleForVariableTextBoxDouble()
		{
			DecimalAutorise = true;
			NullAutorise = true;
            InitializeComponent();
		}

		//------------------------------------------------------
		public void SetVariable(IVariableDynamique variable)
		{
			m_variable = variable;
			CChampCustom champ = variable as CChampCustom;
			if (champ != null )
				Arrondi = champ.Precision;
			else
				Arrondi = 10;
		}

		//------------------------------------------------------
		public void FillFrom2iWnd(C2iWnd wnd)
		{
			C2iWndVariable wndVariable = wnd as C2iWndVariable;
			if (wndVariable != null)
			{
				TextAlign = CUtilControlesVariables.GetHAlign(wndVariable.Alignement);
                SeparateurMilliers = wndVariable.SeparateurMilliers;
			}
		}


		//------------------------------------------------------
		public void SetElementEdite(IElementAVariables element)
		{
			m_elementAVariables = element;
			if (m_variable != null && element != null)
			{
				object valeur = element.GetValeurChamp(m_variable.IdVariable);
                if (valeur == null)
                    DoubleValue = null;
                else if (valeur is double)
					DoubleValue = (double)valeur;
				else
				{
					try
					{
						DoubleValue = Convert.ToDouble ( valeur );
					}
					catch
					{
						DoubleValue = null;
					}
				}
					
			}
		}

        //--------------------------------------------------
        public void SetElementEditeSansChangerLesValeursAffichees(IElementAVariables element)
        {
            m_elementAVariables = element;
        }

		//------------------------------------------------------
		public CResultAErreur MajChamps(bool bControlerValeur)
		{
			CResultAErreur result = CResultAErreur.True;
			if (m_elementAVariables != null && m_variable != null)
			{
				m_elementAVariables.SetValeurChamp(m_variable.IdVariable, DoubleValue);
				if (bControlerValeur)
				{
					result = m_variable.VerifieValeur(DoubleValue);
				}
			}
			return result;
		}

		//------------------------------------------------------
		public Control Control
		{
			get
			{
				return this;
			}
		}

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CControleForVariableTextBoxDouble
            // 
            this.TextChanged += new System.EventHandler(this.CControleForVariableTextBoxDouble_TextChanged);
            this.ResumeLayout(false);

        }

        private void CControleForVariableTextBoxDouble_TextChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(this, e);
        }

        public void SelectAll()
        {
        }

        public bool WantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch (keyData)
            {
                case Keys.Shift:
                    return true;
                case Keys.Right:
                case Keys.End:
                    if ((keyData & Keys.Shift) == Keys.Shift)
                        return true;
                    if (SelectionStart == Text.Length &&
                        SelectionLength == 0)
                        return false;
                    return true;
                case Keys.Left:
                case Keys.Home:
                    if ((keyData & Keys.Shift) == Keys.Shift)
                        return true;
                    if (SelectionStart == 0 && SelectionLength == 0)
                        return false;
                    return true;
            }
            return !dataGridViewWantsInputKey;
        }
	}
}
