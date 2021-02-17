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
	public class CControleForVariableDateTimePicker : C2iDateTimeExPicker, IControleForVariable
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

		public new object Value
		{
			get
			{
				return base.Value;
			}
			set
			{
				DateTime ? valeur = value as DateTime ?;
				if ( value is CDateTimeEx )
					valeur = ((CDateTimeEx)value).DateTimeValue;
				base.Value = valeur;
			}
		}


        

		public CControleForVariableDateTimePicker()
		{
			Format = DateTimePickerFormat.Custom;
            CustomFormat = CUtilDate.gFormat; //"g";// "dd/MM/yyyy HH:mm";
            OnValueChange+=new EventHandler(CControleForVariableDateTimePicker_OnValueChange);
		}

		public void SetVariable(IVariableDynamique variable)
		{
			m_variable = variable;
		}

		public void SetElementEdite(IElementAVariables element)
		{
			m_elementAVariables = element;
			if (m_variable != null && element != null)
			{
				object valeur = element.GetValeurChamp(m_variable.IdVariable);
				if (valeur is DateTime || valeur is CDateTimeEx)
					Value = (DateTime)valeur;
				else
					Value = null;
                valeur = Value;
			}
		}

        //--------------------------------------------------
        public void SetElementEditeSansChangerLesValeursAffichees(IElementAVariables element)
        {
            m_elementAVariables = element;
        }

		//------------------------------------------------------
		public void FillFrom2iWnd(C2iWnd wnd)
		{
			C2iWndVariable wndVariable = wnd as C2iWndVariable;
		}

		//------------------------------------------------------
		public CResultAErreur MajChamps(bool bControlerValeur)
		{
			CResultAErreur result = CResultAErreur.True;
			if (m_elementAVariables != null && m_variable != null)
			{
				object value = Value;
				if (value is CDateTimeEx)
					value = ((CDateTimeEx)value).DateTimeValue;
				else if (value == null || !typeof(DateTime).IsAssignableFrom(value.GetType()))
					value = null;
				m_elementAVariables.SetValeurChamp(m_variable.IdVariable, value);
				if (bControlerValeur)
				{
					result = m_variable.VerifieValeur(value);
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
            // CControleForVariableDateTimePicker
            // 
            this.Name = "CControleForVariableDateTimePicker";
			this.Value = DateTime.Now;
            this.OnValueChange += new System.EventHandler(this.CControleForVariableDateTimePicker_OnValueChange);
            this.ResumeLayout(false);

        }

        private void CControleForVariableDateTimePicker_OnValueChange(object sender, EventArgs e)
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
                    return true;
                case Keys.Left:
                case Keys.Home:
                    return true;
            }
            return !dataGridViewWantsInputKey;
        }

		
	}
}
