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
    public class CWndFor2iDateTime : CControlWndFor2iWnd, IControleWndFor2iWnd, IControlIncluableDansDataGrid
    {
		private C2iDateTimeExPicker m_dateTimeEx;

		//---------------------------------------------------------
        public static void Autoexec()
        {
            CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndDateTime), typeof(CWndFor2iDateTime));
        }

		//---------------------------------------------------------
        public CWndFor2iDateTime()
        {
			m_dateTimeEx = new C2iDateTimeExPicker();
			m_dateTimeEx.OnValueChange += new EventHandler(CWndFor2iDateTime_OnValueChange);
        }


        protected override void MyCreateControle(
            CCreateur2iFormulaireV2 createur,
            C2iWnd wnd,
            Control parent,
            IFournisseurProprietesDynamiques fournisseurProprietes)
        {

			C2iWndDateTime wndDateTime = wnd as C2iWndDateTime;

			if (wndDateTime != null)
            {
                m_dateTimeEx.Visible = true;
				m_dateTimeEx.Format = DateTimePickerFormat.Custom;
                m_dateTimeEx.CustomFormat = CUtilDate.gFormat;//dd/MM/yyyy HH:mm";
				CCreateur2iFormulaireV2.AffecteProprietesCommunes(WndDateTime, m_dateTimeEx);
				parent.Controls.Add(m_dateTimeEx);
            }

        }

		//----------------------------------------------------------
		private C2iWndDateTime WndDateTime
		{
			get
			{
				return WndAssociee as C2iWndDateTime;
			}
		}

		//----------------------------------------------------------
		protected override void OnChangeElementEdite(object element)
		{
			if (element != null && WndDateTime != null &&
				WndDateTime.Property != null && 
				m_dateTimeEx != null)
            {
                    DateTime? valeur = CInterpreteurProprieteDynamique.GetValue(element, WndDateTime.Property).Data as DateTime?;

                    m_dateTimeEx.Value = valeur;
            }
        }


		//----------------------------------------------------------
		protected override CResultAErreur MyMajChamps(bool bControlerLesValeursAvantValidation)
        {
			DateTime? dateValue = m_dateTimeEx.Value;
			if ( WndDateTime != null && WndDateTime.Property != null &&
				m_dateTimeEx != null && EditedElement != null && !LockEdition )
			{
				return CInterpreteurProprieteDynamique.SetValue(EditedElement, WndDateTime.Property, dateValue);
			}
            return CResultAErreur.True;
        }

		//---------------------------------------------------------------
        public override Control Control
        {
            get
            {
                return m_dateTimeEx;
            }
        }

		//---------------------------------------------------------------
		protected override void MyUpdateValeursCalculees()
		{
		}


        //---------------------------------------------
        protected override void MyAppliqueRestriction(
            CRestrictionUtilisateurSurType restrictionSurObjetEdite,
            CListeRestrictionsUtilisateurSurType listeRestrictions,
            IGestionnaireReadOnlySysteme gestionnaireReadOnly)
        {
            if (EditedElement != null && m_dateTimeEx != null)
            {
                ERestriction rest = restrictionSurObjetEdite.RestrictionGlobale;
                C2iWndDateTime wndDate = WndAssociee as C2iWndDateTime;
                if (wndDate != null)
                {
                    CDefinitionProprieteDynamique def = wndDate.Property;
                    if (def != null)
                        rest = def.GetRestrictionAAppliquer(restrictionSurObjetEdite);
                }
                switch (rest)
                {
                    case ERestriction.ReadOnly:
                    case ERestriction.Hide:
                        {
                            gestionnaireReadOnly.SetReadOnly(m_dateTimeEx, true);
                            break;
                        }
                    default: break;
                }
            }
        }

        private void CWndFor2iDateTime_OnValueChange(object sender, EventArgs e)
        {
            C2iWndDateTime dt = WndAssociee as C2iWndDateTime;
            if (dt != null && dt.AutoSetValue)
                MajChamps(false);
            if (m_gridView != null)
                m_gridView.NotifyCurrentCellDirty(true);
            CUtilControlesWnd.DeclencheEvenement(C2iWndDateTime.c_strIdEvenementValueChanged, this);
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

        //-------------------------------------------------
        public void SelectAll()
        {

        }
        #endregion
	

    }
}
