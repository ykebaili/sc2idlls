using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using sc2i.formulaire;
using sc2i.common;
using sc2i.win32.common;
using sc2i.expression;
using sc2i.formulaire.win32.datagrid;
using sc2i.common.Restrictions;
using sc2i.formulaire.inspiration;




namespace sc2i.formulaire.win32.controles2iWnd
{
	[AutoExec("Autoexec")]
    public class CWndFor2iTextBox : CControlWndFor2iWnd, IControleWndFor2iWnd, IControlIncluableDansDataGrid
	{
		private C2iTextBox m_textBox;

		public static void Autoexec()
		{
			CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndTextBox), typeof(CWndFor2iTextBox));
		}

		public CWndFor2iTextBox()
		{
		}

		//----------------------------------------------------------------
		void CWndFor2iTextBox_TextChanged(object sender, EventArgs e)
		{
            C2iWndTextBox txt = WndAssociee as C2iWndTextBox;
            if (txt != null && txt.AutoSetValue)
                MajChamps(false);
            if (m_gridView != null)
                m_gridView.NotifyCurrentCellDirty(true);
			CUtilControlesWnd.DeclencheEvenement(C2iWndTextBox.c_strIdEvenementTextChanged, this);
		}


		//----------------------------------------------------------------
		protected override void MyCreateControle(
			CCreateur2iFormulaireV2 createur,
			C2iWnd wnd,
			Control parent,
			IFournisseurProprietesDynamiques fournisseurProprietes)
		{
			C2iWndTextBox wndTextBox = wnd as C2iWndTextBox;
			if (wndTextBox != null)
			{
				m_textBox = new C2iTextBox();
				m_textBox.TextChanged += new EventHandler(CWndFor2iTextBox_TextChanged);
				m_textBox.Size = wndTextBox.Size;
				CCreateur2iFormulaireV2.AffecteProprietesCommunes(wndTextBox, m_textBox);
				m_textBox.Multiline = wndTextBox.Multiline;
                if(m_textBox.Multiline)
                    m_textBox.ScrollBars = ScrollBars.Vertical;
				parent.Controls.Add(m_textBox);
                if ( wndTextBox.Inspiration.Count() > 0 )
                    m_textBox.Enter += new EventHandler(m_textBox_Enter);
			}
		}

        //----------------------------------------------------------------
        private bool m_bInspirationIsCalc = false;
        void m_textBox_Enter(object sender, EventArgs e)
        {
            if (!m_bInspirationIsCalc)
            {
                m_bInspirationIsCalc = true;
                C2iWndTextBox wndTextBox = WndAssociee as C2iWndTextBox;
                C2iTextBox txtBox = Control as C2iTextBox;
                if (txtBox != null && wndTextBox != null && wndTextBox.Inspiration.Count() > 0)
                {
                    IEnumerable<string> lst = CFournisseurInspiration.GetInspiration(wndTextBox.Inspiration);
                    if (lst != null)
                    {
                        txtBox.AutoCompleteMode = AutoCompleteMode.Suggest;
                        txtBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        txtBox.AutoCompleteCustomSource.Clear();
                        txtBox.AutoCompleteCustomSource.AddRange(lst.ToArray());
                    }
                }
            }
        }

		//----------------------------------------------------------------
		private C2iWndTextBox WndTextBox
		{
			get
			{
				return WndAssociee as C2iWndTextBox;
			}
		}

		protected override void OnChangeElementEdite(object element)
		{
			if (WndTextBox != null && WndTextBox.Property != null)
			{
				string strValue = CInterpreteurProprieteDynamique.GetValue(element, WndTextBox.Property).Data as string;
				if (strValue != null)
					m_textBox.Text = strValue;
				else
					m_textBox.Text = "";
			}

		}

		protected override CResultAErreur MyMajChamps(bool bAvecVerification)
		{
			if (EditedElement != null && WndTextBox != null && WndTextBox.Property != null && !LockEdition)
			{
				return CInterpreteurProprieteDynamique.SetValue(EditedElement, WndTextBox.Property, m_textBox.Text);
			}
			return CResultAErreur.True;

		}
		

		//---------------------------------------------------------------
		public override Control Control
		{
			get
			{
				return m_textBox;
			}
		}


		protected override void MyUpdateValeursCalculees()
		{
		}



		//---------------------------------------------
        protected override void MyAppliqueRestriction(
            CRestrictionUtilisateurSurType restrictionSurObjetEdite,
            CListeRestrictionsUtilisateurSurType listeRestrictions,
            IGestionnaireReadOnlySysteme gestionnaireReadOnly)
		{
			if (EditedElement != null && m_textBox != null)
			{
                ERestriction rest = restrictionSurObjetEdite.RestrictionGlobale;
                C2iWndTextBox wndTxt = WndAssociee as C2iWndTextBox;
                if (wndTxt != null)
                {
                    CDefinitionProprieteDynamique def = wndTxt.Property;
                    if (def != null)
                        rest = def.GetRestrictionAAppliquer(restrictionSurObjetEdite);
                }
				switch (rest)
				{
					case ERestriction.ReadOnly:
                    case ERestriction.Hide :
						{
                            gestionnaireReadOnly.SetReadOnly(m_textBox, true);
							break;
						}
					default: break;
				}
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
            switch (keyData)
            {
                case Keys.Shift :
                    return true;
                case Keys.Right:
                case Keys.End:
                    if ((keyData & Keys.Shift)==Keys.Shift)
                        return true;
                    if (m_textBox != null && (m_textBox.SelectionStart == m_textBox.Text.Length &&
                        m_textBox.SelectionLength == 0 || m_textBox.SelectionLength == m_textBox.Text.Length))
                        return false;
                    return true;
                case Keys.Left:
                case Keys.Home:
                    if ((keyData & Keys.Shift) == Keys.Shift)
                        return true;
                    if (m_textBox != null && (m_textBox.SelectionStart == 0 && m_textBox.SelectionLength == 0 
                        || m_textBox.SelectionLength == m_textBox.Text.Length))
                        return false;
                    return true;
            }
            return !dataGridViewWantsInputKey;
        }

        //-------------------------------------------------
        public void SelectAll()
        {
            if (m_textBox != null)
                m_textBox.SelectAll();
        }
                    
        #endregion
    }
}
