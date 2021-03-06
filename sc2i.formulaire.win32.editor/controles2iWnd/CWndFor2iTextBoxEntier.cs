using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using sc2i.formulaire;
using sc2i.common;
using sc2i.win32.common;
using sc2i.expression;
using sc2i.formulaire.win32.datagrid;
using sc2i.common.Restrictions;



namespace sc2i.formulaire.win32.controles2iWnd
{
    [AutoExec("Autoexec")]
    public class CWndFor2iTextBoxEntier : CControlWndFor2iWnd, IControleWndFor2iWnd, IControlIncluableDansDataGrid
    {
		private C2iTextBoxNumerique m_textBoxNumerique = new C2iTextBoxNumerique();


        public static void Autoexec()
        {
            CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndTextBoxEntier), typeof(CWndFor2iTextBoxEntier));
        }

        public CWndFor2iTextBoxEntier()
        {
			m_textBoxNumerique.TextChanged += new EventHandler(CWndFor2iTextBoxEntier_TextChanged);
        }


        protected override void MyCreateControle(
            CCreateur2iFormulaireV2 createur,
            C2iWnd wnd,
            Control parent,
            IFournisseurProprietesDynamiques fournisseurProprietes)
        {
            if (WndTextBox != null)
            {
				m_textBoxNumerique.DecimalAutorise = false;
                m_textBoxNumerique.Size = WndTextBox.Size;
                m_textBoxNumerique.SeparateurMilliers = WndTextBox.SeparateurMilliers;
                CCreateur2iFormulaireV2.AffecteProprietesCommunes(WndTextBox, m_textBoxNumerique);
                parent.Controls.Add(m_textBoxNumerique);
            }
        }

		private C2iWndTextBoxEntier WndTextBox
		{
			get
			{
				return WndAssociee as C2iWndTextBoxEntier;
			}
		}

        protected override void  OnChangeElementEdite(object element)
        {
            if (element != null &&
				WndTextBox!=null && 
				WndTextBox.Property != null && 
				m_textBoxNumerique != null)
			{
				int? nValue = CInterpreteurProprieteDynamique.GetValue(element, WndTextBox.Property).Data as int?;
				m_textBoxNumerique.IntValue = nValue;
            }
        }



        protected override CResultAErreur  MyMajChamps(bool bControlerLesValeursAvantValidation)
        {
            CResultAErreur result = CResultAErreur.True;
			if (EditedElement != null &&
				WndTextBox != null &&
				WndTextBox.Property != null &&
				m_textBoxNumerique != null &&
                !LockEdition)
			{
				try
				{
					int? nValue = m_textBoxNumerique.IntValue;
					return CInterpreteurProprieteDynamique.SetValue(EditedElement, WndTextBox.Property, nValue);
				}
				catch 
				{
					result.EmpileErreur(I.T("Incorrect numerical value|30000"));
				}
			}
            return result;
        }
       
        //---------------------------------------------------------------
        public override Control Control
        {
            get
            {
                return m_textBoxNumerique;
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
            if (EditedElement != null && m_textBoxNumerique != null)
            {
                ERestriction rest = restrictionSurObjetEdite.RestrictionGlobale;
                C2iWndTextBoxEntier wndTxt = WndAssociee as C2iWndTextBoxEntier;
                if (wndTxt != null)
                {
                    CDefinitionProprieteDynamique def = wndTxt.Property;
                    if (def != null)
                        rest = def.GetRestrictionAAppliquer(restrictionSurObjetEdite);
                }

                switch (rest)
                {
                    case ERestriction.ReadOnly:
                    case ERestriction.Hide:
                        {
                            gestionnaireReadOnly.SetReadOnly(m_textBoxNumerique, true);
                            break;
                        }
                    default: break;
                }
            }

        }

		
		private void CWndFor2iTextBoxEntier_TextChanged(object sender, EventArgs e)
        {
            C2iWndTextBoxEntier txt = WndAssociee as C2iWndTextBoxEntier;
            if (txt != null && txt.AutoSetValue)
                MajChamps(false);
            if (m_gridView != null)
                m_gridView.NotifyCurrentCellDirty(true);
            CUtilControlesWnd.DeclencheEvenement(C2iWndTextBoxEntier.c_strIdEvenementValueChanged, this);
        }



        #region IControlIncluableDansDataGrid Membres
        private DataGridView m_gridView = null;
        //-------------------------------------------------
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

        //-------------------------------------------------
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
                    if (m_textBoxNumerique != null && m_textBoxNumerique.SelectionStart == m_textBoxNumerique.Text.Length &&
                        m_textBoxNumerique.SelectionLength == 0)
                        return false;
                    return true;
                case Keys.Left:
                case Keys.Home:
                    if ((keyData & Keys.Shift) == Keys.Shift)
                        return true;
                    if (m_textBoxNumerique != null && m_textBoxNumerique.SelectionStart == 0 && m_textBoxNumerique.SelectionLength == 0)
                        return false;
                    return true;
            }
            return !dataGridViewWantsInputKey;
        }

        //-------------------------------------------------
        public void SelectAll()
        {
            if (m_textBoxNumerique != null)
                m_textBoxNumerique.SelectAll();
        }
        #endregion
    }
}
