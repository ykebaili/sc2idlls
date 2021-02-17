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
using data.hotel.easyquery.filtre;
using data.hotel.easyquery;
using data.hotel.client.query;

namespace data.hotel.eastquery.win32.filtre
{
    public partial class CFormEditeComposantDHComparaison : Form
    {
        private CDHFiltreValeur m_comparaison = null;
        private IObjetDeEasyQuery m_table = null;
        private CEasyQuery m_query = null;

        //--------------------------------------------------------------
        public CFormEditeComposantDHComparaison()
        {
            InitializeComponent();
            CWin32Traducteur.Translate(this);
        }

        //--------------------------------------------------------------
        public static bool EditeComparaison(CEasyQuery query,
            IObjetDeEasyQuery table,
            CDHFiltreValeur itemComparaison)
        {
            CFormEditeComposantDHComparaison frm = new CFormEditeComposantDHComparaison();
            frm.Init(query, table, itemComparaison);
            DialogResult res = frm.ShowDialog();
            frm.Dispose();
            return res == DialogResult.OK;
        }

        //--------------------------------------------------------------
        public void Init(
            CEasyQuery query,
            IObjetDeEasyQuery table,
            CDHFiltreValeur comparaison)
        {
            m_query = query;
            m_table = table;
            if (comparaison == null)
                comparaison = new CDHFiltreValeur();
            m_comparaison = comparaison;
             List<COperateurComparaisonMassStorage> ops = new List<COperateurComparaisonMassStorage>();
            foreach (EOperateurComparaisonMassStorage op in COperateurComparaisonMassStorage.ValeursEnumPossibles)
                ops.Add(new COperateurComparaisonMassStorage(op));
            m_comboOperateur.ListDonnees = ops;
            m_comboOperateur.ProprieteAffichee = "Libelle";

            m_comboOperateur.SelectedValue = new COperateurComparaisonMassStorage(comparaison.Operateur);

            IColumnDeEasyQuery colSel = null;
            List<CColumnEQFromSource> cols = new List<CColumnEQFromSource>();
            foreach ( IColumnDeEasyQuery col in m_table.Columns )
            {
                CColumnEQFromSource cs = col as CColumnEQFromSource;
               if ( cs != null &&  col.DataType == typeof(double))
                {
                    cols.Add(cs);
                    if (cs.IdColumnSource == m_comparaison.ColumnHotelId)
                        colSel = col;
                }

            }

            m_comboChamp.ListDonnees = cols;
            m_comboChamp.ProprieteAffichee = "ColumnName";
            m_comboChamp.SelectedValue = colSel;

            m_txtFormuleValeur.Init(
                new CFournisseurGeneriqueProprietesDynamiques(),
                new CObjetPourSousProprietes(m_query));
            m_txtFormuleValeur.Formule = comparaison.FormuleValeur;

            m_txtFormuleCondition.Init(
                new CFournisseurGeneriqueProprietesDynamiques(),
                new CObjetPourSousProprietes(m_query));
            m_txtFormuleCondition.Formule = comparaison.FormuleApplication;
        }

        //--------------------------------------------------------------
        private CResultAErreur MajChamps()
        {
            CResultAErreur result = CResultAErreur.True;

            if (m_comboOperateur.SelectedValue as COperateurComparaisonMassStorage == null)
            {
                result.EmpileErreur(I.T("Select a comparison operator|20032"));
            }
            if (m_comboChamp.SelectedValue as CColumnEQFromSource == null)
            {
                result.EmpileErreur(I.T("Select a field|20033"));
            }
            if (m_txtFormuleValeur.Formule == null)
            {
                if (!m_txtFormuleValeur.ResultAnalyse)
                    result.EmpileErreur(m_txtFormuleValeur.ResultAnalyse.Erreur);
                result.EmpileErreur(I.T("Invalid value formula|20034"));
            }
            if (m_txtFormuleCondition.Formule == null)
            {
                if (!m_txtFormuleCondition.ResultAnalyse)
                    result.EmpileErreur(m_txtFormuleCondition.ResultAnalyse.Erreur);
                result.EmpileErreur(I.T("Invalid condition formula|20035"));
            }
            if (!result)
                return result;
            m_comparaison.FormuleApplication = m_txtFormuleCondition.Formule;
            m_comparaison.Operateur = (m_comboOperateur.SelectedValue as COperateurComparaisonMassStorage).Code;
            m_comparaison.ColumnHotelId = (m_comboChamp.SelectedValue as CColumnEQFromSource).IdColumnSource;
            m_comparaison.FormuleValeur = m_txtFormuleValeur.Formule;
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
