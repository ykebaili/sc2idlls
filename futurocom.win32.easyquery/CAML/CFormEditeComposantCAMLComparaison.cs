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
    public partial class CFormEditeComposantCAMLComparaison : Form
    {
        private CCAMLItemComparaison m_comparaison = null;
        private CEasyQuery m_query = null;

        //--------------------------------------------------------------
        public CFormEditeComposantCAMLComparaison()
        {
            InitializeComponent();
            CWin32Traducteur.Translate(this);
        }

        //--------------------------------------------------------------
        public static bool EditeComparaison(CEasyQuery query,
            IEnumerable<CCAMLItemField> fields,
            CCAMLItemComparaison itemComparaison)
        {
            CFormEditeComposantCAMLComparaison frm = new CFormEditeComposantCAMLComparaison();
            frm.Init(query, fields, itemComparaison);
            DialogResult res = frm.ShowDialog();
            frm.Dispose();
            return res == DialogResult.OK;
        }

        //--------------------------------------------------------------
        public void Init(
            CEasyQuery query,
            IEnumerable<CCAMLItemField> fields, 
            CCAMLItemComparaison comparaison)
        {
            m_query = query;
            m_comparaison = comparaison;
            List<CCAMLItemComparaison> itemsComparaison = new List<CCAMLItemComparaison>();
            List<CCAMLOperateurComparaison> ops = new List<CCAMLOperateurComparaison>();
            foreach (ECAMLComparaison op in CCAMLOperateurComparaison.ValeursEnumPossibles)
                ops.Add(new CCAMLOperateurComparaison(op));
            m_comboOperateur.ListDonnees = ops;
            m_comboOperateur.ProprieteAffichee = "Libelle";

            m_comboOperateur.SelectedValue = new CCAMLOperateurComparaison(comparaison.Operateur);

            m_comboChamp.ListDonnees = fields;
            m_comboChamp.ProprieteAffichee = "Libelle";
            m_comboChamp.SelectedValue = comparaison.Field;

            m_txtFormuleValeur.Init(
                new CFournisseurGeneriqueProprietesDynamiques(),
                new CObjetPourSousProprietes(m_query));
            m_txtFormuleValeur.Formule = comparaison.Valeur;

            m_txtFormuleCondition.Init(
                new CFournisseurGeneriqueProprietesDynamiques(),
                new CObjetPourSousProprietes(m_query));
            m_txtFormuleCondition.Formule = comparaison.Condition;
        }

        //--------------------------------------------------------------
        private CResultAErreur MajChamps()
        {
            CResultAErreur result = CResultAErreur.True;

            if (m_comboOperateur.SelectedValue as CCAMLOperateurComparaison == null)
            {
                result.EmpileErreur(I.T("Select a comparison operator|20040"));
            }
            if (m_comboChamp.SelectedValue as CCAMLItemField == null)
            {
                result.EmpileErreur(I.T("Select a field|20041"));
            }
            if (m_txtFormuleValeur.Formule == null)
            {
                if (!m_txtFormuleValeur.ResultAnalyse)
                    result.EmpileErreur(m_txtFormuleValeur.ResultAnalyse.Erreur);
                result.EmpileErreur(I.T("Invalid value formula|20043"));
            }
            if (m_txtFormuleCondition.Formule == null)
            {
                if (!m_txtFormuleCondition.ResultAnalyse)
                    result.EmpileErreur(m_txtFormuleCondition.ResultAnalyse.Erreur);
                result.EmpileErreur(I.T("Invalid condition formula|20042"));
            }
            if (!result)
                return result;
            m_comparaison.Condition = m_txtFormuleCondition.Formule;
            m_comparaison.Operateur = m_comboOperateur.SelectedValue as CCAMLOperateurComparaison;
            m_comparaison.Field = m_comboChamp.SelectedValue as CCAMLItemField;
            m_comparaison.Valeur = m_txtFormuleValeur.Formule;
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
