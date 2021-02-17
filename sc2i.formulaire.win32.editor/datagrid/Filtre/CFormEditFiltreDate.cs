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
using sc2i.formulaire.datagrid;

namespace sc2i.formulaire.win32.datagrid.Filtre
{
    public partial class CFormEditFiltreDate : Form
    {
        private CGridFilterDateComparison m_filtre = null;
        public CFormEditFiltreDate()
        {
            InitializeComponent();
        }

        public static bool EditeFiltre(CGridFilterDateComparison filtre)
        {
            CFormEditFiltreDate form = new CFormEditFiltreDate();
            form.m_filtre = filtre;
            bool bResult = false;
            if (form.ShowDialog() == DialogResult.OK)
                bResult = true;
            form.Dispose();
            return bResult;
        }

        private void CFormEditFiltreDate_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
            m_lblTypeFiltre.Text = m_filtre.Label;
            m_dtPicker.Value = m_filtre.ReferenceValue == null?DateTime.Now:m_filtre.ReferenceValue.Value;
            CGridFilterDateEntre entre = m_filtre as CGridFilterDateEntre;
            m_panelForBetween.Visible = entre != null;
            if (entre != null)
                m_dtPicker2.Value = entre.DateFin == null ? DateTime.Now : entre.DateFin.Value; ;
        }

        private void m_btnOk_Click(object sender, EventArgs e)
        {
            m_filtre.ReferenceValue = m_dtPicker.Value;
            CGridFilterDateEntre entre = m_filtre as CGridFilterDateEntre;
            if (entre != null)
                entre.DateFin = m_dtPicker2.Value;
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
    public class CEditeurFiltreDate : IEditeurFiltreGrid
    {
        public static void Autoexec()
        {
            CGestionnaireEditeurFiltres.RegisterEditeur(typeof(CGridFilterDateDifferent), typeof(CEditeurFiltreDate));
            CGestionnaireEditeurFiltres.RegisterEditeur(typeof(CGridFilterDateEgal), typeof(CEditeurFiltreDate));
            CGestionnaireEditeurFiltres.RegisterEditeur(typeof(CGridFilterDateEntre), typeof(CEditeurFiltreDate));
            CGestionnaireEditeurFiltres.RegisterEditeur(typeof(CGridFilterDateInférieur), typeof(CEditeurFiltreDate));
            CGestionnaireEditeurFiltres.RegisterEditeur(typeof(CGridFilterDateInférieurOuEgal), typeof(CEditeurFiltreDate));
            CGestionnaireEditeurFiltres.RegisterEditeur(typeof(CGridFilterDateSuperieur), typeof(CEditeurFiltreDate));
            CGestionnaireEditeurFiltres.RegisterEditeur(typeof(CGridFilterDateSuperieurOuEgal), typeof(CEditeurFiltreDate));
        }

        public bool EditeFiltre(CGridFilterForWndDataGrid filtre)
        {
            CGridFilterDateComparison filtreText = filtre as CGridFilterDateComparison;
            if ( filtreText != null )
                return CFormEditFiltreDate.EditeFiltre ( filtreText );
            return false;
        }
    }
}
