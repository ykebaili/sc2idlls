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
    public class CWndFor2iTextBoxDecimal : CControlWndFor2iWnd, IControleWndFor2iWnd, IControlIncluableDansDataGrid
    {
		private C2iTextBoxNumerique m_textBoxNumerique = new C2iTextBoxNumerique();

        public static void Autoexec()
        {
            CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndTextBoxDecimal), typeof(CWndFor2iTextBoxDecimal));
        }

        public CWndFor2iTextBoxDecimal()
        {
            m_textBoxNumerique.TextChanged += new EventHandler(CWndFor2iTextBoxDecimal_TextChanged);
        }


        protected override void MyCreateControle(
            CCreateur2iFormulaireV2 createur,
            C2iWnd wnd,
            Control parent,
            IFournisseurProprietesDynamiques fournisseurProprietes)
        {
            
            m_textBoxNumerique.DecimalAutorise = true;
            m_textBoxNumerique.NullAutorise = true;

			C2iWndTextBoxDecimal wndTextBoxDecimale =  wnd as C2iWndTextBoxDecimal;

            if (wndTextBoxDecimale != null)
            {
                m_textBoxNumerique.Size = wndTextBoxDecimale.Size;
                CCreateur2iFormulaireV2.AffecteProprietesCommunes(wndTextBoxDecimale, m_textBoxNumerique);
                m_textBoxNumerique.Arrondi = wndTextBoxDecimale.Precision;
                m_textBoxNumerique.SeparateurMilliers = wndTextBoxDecimale.SeparateurMilliers;
                parent.Controls.Add(m_textBoxNumerique);
            }      
            
        }

		//----------------------------------------------------------
		private C2iWndTextBoxDecimal WndTextBoxDecimal
		{
			get
			{
				return WndAssociee as C2iWndTextBoxDecimal;
			}
		}

		//----------------------------------------------------------
        protected override void OnChangeElementEdite(object element)
        {
            if ( element != null && 
				m_textBoxNumerique != null &&
				WndTextBoxDecimal != null &&
				WndTextBoxDecimal.Property != null )

            {
				double? fValeur = CInterpreteurProprieteDynamique.GetValue(EditedElement, WndTextBoxDecimal.Property).Data as double?;
				m_textBoxNumerique.DoubleValue = fValeur;
			}
		}
		//----------------------------------------------------------
        protected override CResultAErreur  MyMajChamps(bool bControlerLesValeursAvantValidation)
        {
            CResultAErreur result = CResultAErreur.True;
			if (WndTextBoxDecimal != null &&
				WndTextBoxDecimal.Property != null &&
				m_textBoxNumerique != null &&
				EditedElement != null && 
                !LockEdition)
			{
				try
				{
					double? dValeur = m_textBoxNumerique.DoubleValue;
					return CInterpreteurProprieteDynamique.SetValue(EditedElement, WndTextBoxDecimal.Property, dValeur);
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

        //---------------------------------------------
        protected override void MyAppliqueRestriction(
            CRestrictionUtilisateurSurType restrictionSurObjetEdite,
            CListeRestrictionsUtilisateurSurType listeRestrictions,
            IGestionnaireReadOnlySysteme gestionnaireReadOnly)
        {
            if (EditedElement != null && m_textBoxNumerique != null)
            {
                ERestriction rest = restrictionSurObjetEdite.RestrictionGlobale;
                C2iWndTextBoxDecimal wndDec = WndAssociee as C2iWndTextBoxDecimal;
                if (wndDec != null)
                {
                    CDefinitionProprieteDynamique def = wndDec.Property;
                    if (def != null)
                        rest = def.GetRestrictionAAppliquer(restrictionSurObjetEdite);
                }
                switch (rest)
                {
                    case ERestriction.ReadOnly:
                    case ERestriction.Hide :
                        {
                            gestionnaireReadOnly.SetReadOnly(m_textBoxNumerique, true);
                            break;
                        }
                    default: break;
                }

            }
        }

		

        private void CWndFor2iTextBoxDecimal_TextChanged(object sender, EventArgs e)
        {
            C2iWndTextBoxDecimal dec = WndAssociee as C2iWndTextBoxDecimal;
            if ( dec != null && dec.AutoSetValue )
                MajChamps ( false );
            if (m_gridView != null)
                m_gridView.NotifyCurrentCellDirty(true);
            CUtilControlesWnd.DeclencheEvenement(C2iWndTextBoxDecimal.c_strIdEvenementValueChanged, this);
        }

		protected override void MyUpdateValeursCalculees()
		{
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
