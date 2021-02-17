using System;
using System.Collections.Generic;
using System.Text;
using sc2i.data.dynamic;
using System.Windows.Forms;
using sc2i.common;
using sc2i.formulaire;
using sc2i.formulaire.win32;
using sc2i.formulaire.win32.controles2iWnd;
using sc2i.win32.data.dynamic.controlesFor2iWnd;
using sc2i.expression;

namespace sc2i.win32.data.dynamic
{
    public class CControleForVariableCheckBox : CheckBox, IControleForVariable
	{
		private IVariableDynamique m_variable;
		private IElementAVariables m_elementAVariables = null;

        
        public object Value
        {
            get
            {
                return (object)Checked;
            }
            set
            {
                Checked = (bool)value;
            }
        }


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
        public CControleForVariableCheckBox()
        {
            CheckedChanged+=new EventHandler(CControleForVariableCheckBox_CheckedChanged);
        }


		//------------------------------------------------------
		public void SetVariable(IVariableDynamique variable)
		{
			m_variable = variable;
            
		}

		//------------------------------------------------------
		public void FillFrom2iWnd(C2iWnd wnd)
		{
			C2iWndVariable wndVariable = wnd as C2iWndVariable;
			if (wndVariable != null)
			{
				TextAlign = CUtilControlesVariables.GetContentAlign(wndVariable.Alignement);
			}
		}

		//------------------------------------------------------
		public void SetElementEdite(IElementAVariables element)
		{
			m_elementAVariables = element;
			if (m_variable != null && element != null)
			{
				object valeur = element.GetValeurChamp(m_variable.IdVariable);
                if (valeur is bool)
					Checked = (bool)valeur;
				else
					Checked = false;
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
				m_elementAVariables.SetValeurChamp(m_variable.IdVariable, Checked);
				if (bControlerValeur)
				{
					result = m_variable.VerifieValeur(Checked);
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

		//------------------------------------------------------
		public bool LockEdition
		{
			get
			{
				return !Enabled;
			}
			set
			{
				Enabled = !value;
				if (OnChangeLockEdition != null)
					OnChangeLockEdition(this, new EventArgs());
			}
		}
		public event EventHandler OnChangeLockEdition;

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CControleForVariableCheckBox
            // 
            this.CheckedChanged += new System.EventHandler(this.CControleForVariableCheckBox_CheckedChanged);
            this.ResumeLayout(false);

        }

        public event EventHandler ValueChanged;

        private void CControleForVariableCheckBox_CheckedChanged(object sender, EventArgs e)
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
                case Keys.Space:
                    return true;
            }
            return false ;
        }

    
	}
}
