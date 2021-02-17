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
    public partial class CFormEditFiltreTexte : Form
    {
        private CGridFilterTextComparison m_filtre = null;
        public CFormEditFiltreTexte()
        {
            InitializeComponent();
        }

        public static bool EditeFiltre(CGridFilterTextComparison filtre)
        {
            CFormEditFiltreTexte form = new CFormEditFiltreTexte();
            form.m_filtre = filtre;
            bool bResult = false;
            if (form.ShowDialog() == DialogResult.OK)
                bResult = true;
            form.Dispose();
            return bResult;
        }

        private void CFormEditFiltreTexte_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
            m_lblTypeFiltre.Text = m_filtre.Label;
            m_txtText.Text = m_filtre.ReferenceValue;
        }

        private void m_btnOk_Click(object sender, EventArgs e)
        {
            m_filtre.ReferenceValue = m_txtText.Text;
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
    public class CEditeurFiltreTexte : IEditeurFiltreGrid
    {
        public static void Autoexec()
        {
            CGestionnaireEditeurFiltres.RegisterEditeur ( typeof(CGridFilterTextCommencePar), typeof(CEditeurFiltreTexte));
            CGestionnaireEditeurFiltres.RegisterEditeur ( typeof(CGridFilterTextContient), typeof(CEditeurFiltreTexte));
            CGestionnaireEditeurFiltres.RegisterEditeur ( typeof(CGridFilterTextDifferent), typeof(CEditeurFiltreTexte));
            CGestionnaireEditeurFiltres.RegisterEditeur ( typeof(CGridFilterTextEgal), typeof(CEditeurFiltreTexte));
            CGestionnaireEditeurFiltres.RegisterEditeur ( typeof(CGridFilterTextNeCommencePasPar), typeof(CEditeurFiltreTexte));
            CGestionnaireEditeurFiltres.RegisterEditeur ( typeof(CGridFilterTextNeContientPas), typeof(CEditeurFiltreTexte));
            CGestionnaireEditeurFiltres.RegisterEditeur ( typeof(CGridFilterTextNeTerminePasPar), typeof(CEditeurFiltreTexte));
            CGestionnaireEditeurFiltres.RegisterEditeur ( typeof(CGridFilterTextTerminePar), typeof(CEditeurFiltreTexte));
        }

        public bool EditeFiltre(sc2i.formulaire.datagrid.CGridFilterForWndDataGrid filtre)
        {
            CGridFilterTextComparison filtreText = filtre as CGridFilterTextComparison;
            if ( filtreText != null )
                return CFormEditFiltreTexte.EditeFiltre ( filtreText );
            return false;
        }
    }
}
