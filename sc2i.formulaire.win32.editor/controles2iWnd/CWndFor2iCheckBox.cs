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
    public class CWndFor2iCheckBox : CControlWndFor2iWnd, IControleWndFor2iWnd, IControlIncluableDansDataGrid
    {

		private CheckBox m_checkBox = null;

		public static void Autoexec()
        {
            CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndCheckBox), typeof(CWndFor2iCheckBox));
        }

        public CWndFor2iCheckBox()
        {
			m_checkBox = new CheckBox();
			m_checkBox.CheckedChanged += new EventHandler(CWndFor2iCheckBox_CheckedChanged);
        }

		void CWndFor2iCheckBox_CheckedChanged(object sender, EventArgs e)
		{
            C2iWndCheckBox chk = WndAssociee as C2iWndCheckBox;
            if (chk != null && chk.AutoSetValue)
                MajChamps(false);
            if (m_gridView != null)
                m_gridView.NotifyCurrentCellDirty(true);
			CUtilControlesWnd.DeclencheEvenement(C2iWndCheckBox.c_strIdEvenementCheckChanged, this);
		}


        protected override void MyCreateControle(
			CCreateur2iFormulaireV2 createur,
			C2iWnd wnd, 
			Control parent,                                              
			IFournisseurProprietesDynamiques fournisseurProprietes)
		{

            C2iWndCheckBox wndCheckBox = wnd as C2iWndCheckBox;

            if (wndCheckBox != null)
            {
				CCreateur2iFormulaireV2.AffecteProprietesCommunes(wndCheckBox, m_checkBox);
                parent.Controls.Add(m_checkBox);
				m_checkBox.Text = wndCheckBox.Text;
            }
	
		}

		private C2iWndCheckBox WndCheckBox
		{
			get
			{
				return WndAssociee as C2iWndCheckBox;
			}
		}
			

        protected override void  OnChangeElementEdite(object element)
        {
            if (
				EditedElement != null && 
				WndCheckBox != null && 
				WndCheckBox.Property != null &&
				m_checkBox != null)
            {
                object valeur = CInterpreteurProprieteDynamique.GetValue(EditedElement, WndCheckBox.Property).Data;
				if (valeur is bool)
					m_checkBox.Checked = (bool)valeur;
            }
        }

        protected override CResultAErreur MyMajChamps(bool bAvecVerification)
        {
			if ( !LockEdition && 
				EditedElement != null && 
				WndCheckBox != null && 
				WndCheckBox.Property != null && 
				m_checkBox != null )
			{
				return CInterpreteurProprieteDynamique.SetValue(EditedElement, WndCheckBox.Property, m_checkBox.Checked);
            }

            return CResultAErreur.True;
        }

        //---------------------------------------------------------------
        public override Control Control
        {
            get
            {
                return m_checkBox;
            }
        }

		//---------------------------------------------------------------
        protected override void  MyUpdateValeursCalculees()
		{
        }

        //---------------------------------------------
        protected override void MyAppliqueRestriction(
            CRestrictionUtilisateurSurType restrictionSurObjetEdite,
            CListeRestrictionsUtilisateurSurType listeRestrictions,
            IGestionnaireReadOnlySysteme gestionnaireReadOnly)
        {
            if (EditedElement != null && m_checkBox != null)
            {
                //Chope la restriction sur le champ concerné
                C2iWndCheckBox chk = WndAssociee as C2iWndCheckBox;
                ERestriction rest = restrictionSurObjetEdite.RestrictionGlobale;
                if (chk != null)
                {
                    CDefinitionProprieteDynamique def = chk.Property;
                    if (def != null)
                        rest = def.GetRestrictionAAppliquer(restrictionSurObjetEdite);
                }
                switch (rest)
                {
                    case ERestriction.ReadOnly:
                        {
                            gestionnaireReadOnly.SetReadOnly(m_checkBox, true);
                            break;
                        }

                    case ERestriction.Hide:
                        {
                            gestionnaireReadOnly.SetReadOnly(m_checkBox, true);
                            m_checkBox.Hide();
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

        //----------------------------------------------------------------------
        public bool WantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            if (keyData == Keys.Space)
                return true;
            return false;
        }

        //----------------------------------------------------------------------
        public void SelectAll()
        {
        }

        #endregion
    }
}
