using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;
using System.Windows.Forms;
using sc2i.expression;
using sc2i.common.Restrictions;
using System.Collections;

namespace sc2i.formulaire.win32.controles2iWnd
{
	[AutoExec("Autoexec")]
	public class CWndFor2iMultiSelect : CControlWndFor2iWnd
	{

        

		private ListView m_controleListe = null;
		
		public CWndFor2iMultiSelect()
		{
		}

		public static void Autoexec()
		{
			CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndMultiSelect), typeof(CWndFor2iMultiSelect));
		}

		protected override void MyCreateControle(
			CCreateur2iFormulaireV2 createur,
			C2iWnd wnd,
			Control parent,
			IFournisseurProprietesDynamiques fournisseur)
		{
            C2iWndMultiSelect multiSel = wnd as C2iWndMultiSelect;
			if (multiSel == null)
				return;
			m_controleListe = new ListView();
            m_controleListe.CheckBoxes = true;
            m_controleListe.ItemChecked += new ItemCheckedEventHandler(m_controleListe_ItemChecked);
            C2iWndMultiSelect.CConfigMultiSelect config = multiSel.Setup;
            foreach (C2iWndMultiSelect.CColonneMultiSelect col in config.Colonnes)
            {
                ColumnHeader head = new ColumnHeader();
                head.Text = col.Nom;
                head.Width = col.Largeur;
                m_controleListe.Columns.Add(head);
            }
            m_controleListe.View = View.Details;
            CCreateur2iFormulaireV2.AffecteProprietesCommunes(multiSel, m_controleListe);
			parent.Controls.Add(m_controleListe);
		}

        void m_controleListe_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if ( !m_bIsFilling )
                CUtilControlesWnd.DeclencheEvenement(C2iWndMultiSelect.c_strIdEvenementCheckChanged, this);
        }


        //------------------------------------------------------------
        private C2iWndMultiSelect MultiSel
        {
            get
            {
                return WndAssociee as C2iWndMultiSelect;
            }
        }
		
		//------------------------------------------------------------
		public override Control Control
		{
			get { return m_controleListe; }
		}

        //------------------------------------------------------------
        public IEnumerable Checked_Items
        {
            get
            {
                ArrayList lstObjects = new ArrayList();
                C2iExpression formule = MultiSel.Setup.FormuleSelectedValue;
                if (m_controleListe != null && formule != null)
                {
                    foreach (ListViewItem item in m_controleListe.CheckedItems)
                    {
                        CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(item.Tag);
                        CResultAErreur result = formule.Eval(ctx);
                        if (result && result.Data != null)
                            lstObjects.Add(result.Data);
                    }
                }
                return lstObjects.ToArray();
            }
            set 
            {
                m_bIsFilling = true;
                if (m_controleListe != null && MultiSel != null)
                {
                    m_controleListe.BeginUpdate();
                    HashSet<object> set = new HashSet<object>();
                    if (value != null)
                        foreach (object obj in value)
                            set.Add(obj);

                    C2iExpression formule = MultiSel.Setup.FormuleSelectedValue;
                    foreach (ListViewItem item in m_controleListe.Items)
                    {
                        if (value == null)
                            item.Checked = false;
                        else
                        {
                            CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(item.Tag);
                            CResultAErreur result = formule.Eval(ctx);
                            if (result && result.Data != null)
                            {
                                item.Checked = set.Contains(result.Data);
                            }
                        }
                    }
                    m_controleListe.EndUpdate();
                    m_bIsFilling = false;
                }
            }
        }

		//------------------------------------------------------------
		protected override CResultAErreur MyMajChamps(bool bControlerLesValeursAvantValidation)
		{
			CResultAErreur result = CResultAErreur.True;
			return result;
		}

		//------------------------------------------------------------
		protected override void OnChangeElementEdite(object element)
		{
			MyUpdateValeursCalculees();
		}

		//------------------------------------------------------------
        private bool m_bIsFilling = false;
        protected override void MyUpdateValeursCalculees()
		{
            m_bIsFilling = true;
            if ( m_controleListe != null )
            {
                m_controleListe.BeginUpdate();
                m_controleListe.Items.Clear();
            }
            if (m_controleListe != null && MultiSel != null && MultiSel.SourceFormula != null)
            {
                
                CContexteEvaluationExpression ctx = CUtilControlesWnd.GetContexteEval(this, EditedElement);
                CResultAErreur result = MultiSel.SourceFormula.Eval(ctx);
                if (result && result.Data is IEnumerable)
                {

                    IEnumerable lst = (IEnumerable)result.Data;
                    foreach (object obj in lst)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Tag = obj;
                        ctx = new CContexteEvaluationExpression(obj);
                        int nItem = 0;
                        C2iWndMultiSelect.CConfigMultiSelect config = MultiSel.Setup;
                        foreach (C2iWndMultiSelect.CColonneMultiSelect col in config.Colonnes)
                        {
                            string strText = "";
                            if (col.Formule != null)
                            {
                                result = col.Formule.Eval(ctx);
                                if (result && result.Data != null)
                                    strText = result.Data.ToString();
                            }
                            if (nItem == 0)
                                item.Text = strText;
                            else
                                item.SubItems.Add(strText);
                            nItem++;
                        }
                        m_controleListe.Items.Add(item);
                    }
                }
            }
            if (m_controleListe != null)
                m_controleListe.EndUpdate();
            m_bIsFilling = false;
			
		}

		//------------------------------------------------------------
        protected override void MyAppliqueRestriction(
            CRestrictionUtilisateurSurType restrictionSurObjetEdite,
            CListeRestrictionsUtilisateurSurType listeRestrictions,
            IGestionnaireReadOnlySysteme gestionnaireReadOnly)
		{
		}
	}
}
