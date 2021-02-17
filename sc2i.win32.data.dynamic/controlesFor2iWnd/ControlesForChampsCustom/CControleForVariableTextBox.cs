using System;
using System.Collections.Generic;
using System.Text;
using sc2i.data.dynamic;
using System.Linq;
using System.Windows.Forms;
using sc2i.common;
using sc2i.win32.common;
using sc2i.formulaire;
using sc2i.formulaire.win32.controles2iWnd;
using sc2i.win32.data.dynamic.controlesFor2iWnd;
using sc2i.formulaire.inspiration;
using sc2i.expression;

namespace sc2i.win32.data.dynamic
{
	public class CControleForVariableTextBox : C2iTextBox, IControleForVariable
	{
		private IVariableDynamique m_variable;
		private IElementAVariables m_elementAVariables = null;

        private CWndFor2iWndVariable m_wndFor2iVariable;
        private C2iWnd m_wndSource = null;
        private CListeParametresInspiration m_parametresInspiration = null;

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


        public CControleForVariableTextBox()
        {
            InitializeComponent();

        }

        public object Value
        {
            get
            {
                return (object)Text;
            }
            set
            {
                Text = value!=null?value.ToString():"";
            }
        }


		//------------------------------------------------------
		public void SetVariable(IVariableDynamique variable)
		{
			m_variable = variable;
			CChampCustom champ = variable as CChampCustom;
			if (champ != null && champ.Precision >= 1)
				MaxLength = champ.Precision;
		}

		//------------------------------------------------------
		public void FillFrom2iWnd(C2iWnd wnd)
		{
			C2iWndVariable wndVariable = wnd as C2iWndVariable;
			if (wndVariable != null)
			{
                if (wndVariable.EditMask.Trim() != "")
				{
                    CFormatteurTextBoxMasque formatteur = new CFormatteurTextBoxMasque(wndVariable.EditMask);
					formatteur.AttachTo(this);
				}
				TextAlign = CUtilControlesVariables.GetHAlign(wndVariable.Alignement);
				Multiline = wndVariable.MultiLine;
                if (Multiline)
                    ScrollBars = ScrollBars.Vertical;
                m_parametresInspiration = wndVariable.Inspiration;
			}
		}

        private IEnumerable<string> m_valeursInspiration = null;
        //------------------------------------------------------


		//------------------------------------------------------
		public void SetElementEdite(IElementAVariables element)
		{
			m_elementAVariables = element;
			if (m_variable != null && element != null)
			{
				object valeur = element.GetValeurChamp(m_variable.IdVariable);
                
				if (valeur != null)
					Text = valeur.ToString();
				else
					Text = "";
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
				m_elementAVariables.SetValeurChamp(m_variable.IdVariable, Text);
				if (bControlerValeur)
				{
					result = m_variable.VerifieValeur(Text);
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
            // CControleForVariableTextBox
            // 
            this.Validated += new System.EventHandler(this.CControleForVariableTextBox_TextChanged);
            this.Enter += new System.EventHandler(this.CControleForVariableTextBox_Enter);
            this.ResumeLayout(false);

        }

        public event EventHandler ValueChanged;

        private void CControleForVariableTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged ( this, e);
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


        //----------------------------------------------------------------------------------------
        private void CControleForVariableTextBox_Enter(object sender, EventArgs e)
        {
            if (m_valeursInspiration == null && m_parametresInspiration != null &&
                m_parametresInspiration.Count > 0)
            {
                m_valeursInspiration = CFournisseurInspiration.GetInspiration(m_parametresInspiration);
                AutoCompleteMode = AutoCompleteMode.Suggest;
                AutoCompleteSource = AutoCompleteSource.CustomSource;
                AutoCompleteCustomSource.Clear();
                AutoCompleteCustomSource.AddRange(m_valeursInspiration.ToArray());
            }
        }


         
	}
}
