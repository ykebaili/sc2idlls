using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using sc2i.formulaire;
using sc2i.common;
using sc2i.expression;
using sc2i.formulaire.win32;
using sc2i.formulaire.win32.controles2iWnd;
using sc2i.win32.common;
using sc2i.formulaire.win32.datagrid;
using sc2i.formulaire.datagrid;
using sc2i.common.Restrictions;

namespace sc2i.formulaire.win32
{
	[AutoExec("Autoexec")]
    public class CWndFor2iFormule : CControlWndFor2iWnd, IControleWndFor2iWnd, IControlIncluableDansDataGrid
	{
		private C2iLabel m_label = new C2iLabel();
        private C2iTextBox m_textBox = new C2iTextBox();

        private Control m_control;

		//--------------------------------------------
		public static void Autoexec()
		{
			CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndFormule), typeof(CWndFor2iFormule));
		}

		//--------------------------------------------
        public CWndFor2iFormule()
        {
            m_label.TextChanged+=new EventHandler(CWndFor2iFormule_TextChanged);
        }

        protected override void MyCreateControle(
            CCreateur2iFormulaireV2 createur,
            C2iWnd wnd,
            Control parent,
            IFournisseurProprietesDynamiques fournisseurProprietes)
        {
            C2iWndFormule wndExpression = wnd as C2iWndFormule;
            if (wndExpression == null)
                return;

            CCreateur2iFormulaireV2.AffecteProprietesCommunes(wndExpression, m_label);
            BorderStyle bstyle = BorderStyle.Fixed3D;
            switch (wndExpression.BorderStyle)
            {
                case C2iWndLabel.LabelBorderStyle._3D:
                    bstyle = BorderStyle.Fixed3D;
                    break;
                case C2iWndLabel.LabelBorderStyle.Aucun:
                    bstyle = BorderStyle.None;
                    break;
                case C2iWndLabel.LabelBorderStyle.Plein:
                    bstyle = BorderStyle.FixedSingle;
                    break;
            }

            //Dans une grille, on met une texte box pour pouvoir utiliser les touches
            if (wndExpression.Parent != null && wndExpression.Parent is C2iWndDataGridColumn)
            {
                m_control = m_textBox;
                m_textBox.LockEdition = true;
                m_textBox.Text = "";
                m_textBox.Multiline = true;
                m_textBox.BorderStyle = bstyle;
                switch (wndExpression.TextAlign)
                {
                    case System.Drawing.ContentAlignment.TopLeft:
                    case System.Drawing.ContentAlignment.MiddleLeft:
                    case System.Drawing.ContentAlignment.BottomLeft:
                        m_textBox.TextAlign = HorizontalAlignment.Left;
                        break;
                    case System.Drawing.ContentAlignment.BottomCenter:
                    case System.Drawing.ContentAlignment.MiddleCenter:
                    case System.Drawing.ContentAlignment.TopCenter:
                        m_textBox.TextAlign = HorizontalAlignment.Center;
                        break;
                    case System.Drawing.ContentAlignment.BottomRight:
                    case System.Drawing.ContentAlignment.MiddleRight:
                    case System.Drawing.ContentAlignment.TopRight:
                        m_textBox.TextAlign = HorizontalAlignment.Right;
                        break;
                        

                }
            }
            else
            {
                m_control = m_label;
                m_label.Text = "";
                m_label.TextAlign = wndExpression.TextAlign;
                m_label.BorderStyle = bstyle;
            }


            parent.Controls.Add(m_control);
        }

		private C2iWndFormule WndFormule
		{
			get
			{
				return WndAssociee as C2iWndFormule;
			}
		}


		//-------------------------------------
		public override Control Control
		{
			get
			{
				return m_control;
			}
		}

		//-------------------------------------
		protected override void  OnChangeElementEdite(object element)
		{
			UpdateValeursCalculees();
		}

		//-------------------------------------
		protected override CResultAErreur MyMajChamps(bool bControlerLesValeursAvantValidation)
		{
			return CResultAErreur.True;
		}

		//---------------------------------------------------------------
		protected override void MyUpdateValeursCalculees()
		{
			if (WndFormule == null || m_control == null )
				return;

            CContexteEvaluationExpression contexte = CUtilControlesWnd.GetContexteEval(this, EditedElement);
			CResultAErreur result = CResultAErreur.True;
			try
			{
				result = WndFormule.Formule.Eval(contexte);
			}
			catch
			{
				result.EmpileErreur("");
			}
            if (!result)
            {
                if (EditedElement != null)
                    m_control.Text = "ERROR";
                else
                    m_control.Text = "";
            }
            else if (result.Data != null)
                m_control.Text = result.Data.ToString();
            else
                m_control.Text = "";
		}

		

		//---------------------------------------------
        protected override void MyAppliqueRestriction(
            CRestrictionUtilisateurSurType restrictionSurObjetEdite,
            CListeRestrictionsUtilisateurSurType listeRestrictions,
            IGestionnaireReadOnlySysteme gestionnaireReadOnly)
		{
		}


		//---------------------------------------------
        private void CWndFor2iFormule_TextChanged(object sender, EventArgs e)
        {
            CUtilControlesWnd.DeclencheEvenement(C2iWndFormule.c_strIdEvenementTextChanged, this);
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
            return false;
        }

        //-------------------------------------------------
        public void SelectAll()
        {
        }

        #endregion
	}
}
