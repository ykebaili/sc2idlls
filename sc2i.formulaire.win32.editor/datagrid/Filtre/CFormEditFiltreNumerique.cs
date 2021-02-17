using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.formulaire.datagrid.Filters;
using sc2i.win32.common;
using sc2i.common;

namespace sc2i.formulaire.win32.datagrid.Filtre
{
    public partial class CFormEditFiltreNumerique : Form
    {
        private CGridFilterNumericComparison m_filtre = null;
        public CFormEditFiltreNumerique()
        {
            InitializeComponent();
        }

        public static bool EditeFiltre(CGridFilterNumericComparison filtre)
        {
            CFormEditFiltreNumerique form = new CFormEditFiltreNumerique();
            form.m_filtre = filtre;
            bool bResult = false;
            if (form.ShowDialog() == DialogResult.OK)
                bResult = true;
            form.Dispose();
            return bResult;
        }

        private void CFormEditFiltreNumerique_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
            m_lblTypeFiltre.Text = m_filtre.Label;
            m_txtValeurNumerique.DoubleValue = m_filtre.ReferenceValue;
            CGridFilterNumericEntre entre = m_filtre as CGridFilterNumericEntre;
            m_panelForBetween.Visible = entre != null;
            if (entre != null)
                m_valeur2.DoubleValue = entre.ValeurFin;
        }

        private void m_btnOk_Click(object sender, EventArgs e)
        {
            m_filtre.ReferenceValue = m_txtValeurNumerique.DoubleValue;
            CGridFilterNumericEntre entre = m_filtre as CGridFilterNumericEntre;
            if (entre != null)
                entre.ValeurFin = m_valeur2.DoubleValue;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        
    }

    [AutoExec("Autoexec")]
    public class CEditeurFiltreNumerique : IEditeurFiltreGrid
    {
        public static void Autoexec()
        {
            CGestionnaireEditeurFiltres.RegisterEditeur(typeof(CGridFilterNumericEgal), typeof(CEditeurFiltreNumerique));
            CGestionnaireEditeurFiltres.RegisterEditeur(typeof(CGridFilterNumericInferieur), typeof(CEditeurFiltreNumerique));
            CGestionnaireEditeurFiltres.RegisterEditeur(typeof(CGridFilterNumericInferieurOuEgal), typeof(CEditeurFiltreNumerique));
            CGestionnaireEditeurFiltres.RegisterEditeur(typeof(CGridFilterNumericSuperieur), typeof(CEditeurFiltreNumerique));
            CGestionnaireEditeurFiltres.RegisterEditeur(typeof(CGridFilterNumericSuperieurOuEgal), typeof(CEditeurFiltreNumerique));
            CGestionnaireEditeurFiltres.RegisterEditeur(typeof(CGridFilterNumericEntre), typeof(CEditeurFiltreNumerique));
        }

        public bool EditeFiltre(sc2i.formulaire.datagrid.CGridFilterForWndDataGrid filtre)
        {
            CGridFilterNumericComparison filtreText = filtre as CGridFilterNumericComparison;
            if ( filtreText != null )
                return CFormEditFiltreNumerique.EditeFiltre ( filtreText );
            return false;
        }
    }
}
