using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using futurocom.easyquery.CAML;
using futurocom.easyquery;
using sc2i.expression;
using sc2i.win32.common;
using sc2i.common;

namespace futurocom.win32.easyquery.CAML
{
    public partial class CFormEditeComposantCAMLNull : Form
    {
        private CCAMLItemNull m_itemNull = null;
        private CEasyQuery m_query = null;

        //--------------------------------------------------------------
        public CFormEditeComposantCAMLNull()
        {
            InitializeComponent();
            CWin32Traducteur.Translate(this);
        }

        //--------------------------------------------------------------
        public static bool EditeNull(
            CEasyQuery query,
            IEnumerable<CCAMLItemField> fields,
            CCAMLItemNull itemComparaison)
        {
            CFormEditeComposantCAMLNull frm = new CFormEditeComposantCAMLNull();
            frm.Init(fields, itemComparaison);
            frm.m_query = query;
            DialogResult res = frm.ShowDialog();
            frm.Dispose();
            return res == DialogResult.OK;
        }

        //--------------------------------------------------------------
        public void Init(IEnumerable<CCAMLItemField> fields, CCAMLItemNull itemNull)
        {
            m_itemNull = itemNull;

            m_comboChamp.ListDonnees = fields;
            m_comboChamp.ProprieteAffichee = "Libelle";
            m_comboChamp.SelectedValue = itemNull.Field;

            m_rbtnEstNull.Checked = m_itemNull.IsNull;

            m_txtFormuleCondition.Init(new CFournisseurGeneriqueProprietesDynamiques(),
                new CObjetPourSousProprietes (m_query));
            m_txtFormuleCondition.Formule = itemNull.Condition;
        }

        //--------------------------------------------------------------
        private CResultAErreur MajChamps()
        {
            CResultAErreur result = CResultAErreur.True;

            if (m_comboChamp.SelectedValue as CCAMLItemField == null)
            {
                result.EmpileErreur(I.T("Select a field|20041"));
            }
            if (m_txtFormuleCondition.Formule == null)
            {
                if (!m_txtFormuleCondition.ResultAnalyse)
                    result.EmpileErreur(m_txtFormuleCondition.ResultAnalyse.Erreur);
                result.EmpileErreur(I.T("Invalid condition formula|20042"));
            }
            if (!result)
                return result;
            m_itemNull.Condition = m_txtFormuleCondition.Formule;
            m_itemNull.Field = m_comboChamp.SelectedValue as CCAMLItemField;
            m_itemNull.IsNull = m_rbtnEstNull.Checked;
            return result;

        }

        //--------------------------------------------------------------
        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        //--------------------------------------------------------------
        private void m_btnOk_Click(object sender, EventArgs e)
        {
            CResultAErreur result = MajChamps();
            if (!result)
            {
                CFormAlerte.Afficher(result.Erreur);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        
    }
}
