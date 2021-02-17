using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.common;
using sc2i.win32.common;

namespace sc2i.win32.data.dynamic.formulesglobale
{
    public partial class CFormEditeFormulesGlobales : Form
    {
        public CFormEditeFormulesGlobales()
        {
            InitializeComponent();
            CWin32Traducteur.Translate(this);
        }

        //---------------------------------------------------------
        private void m_btnOk_Click(object sender, EventArgs e)
        {
            CResultAErreur result = m_panelFormules.MajChammps();
            if (!result)
            {
                CFormAlerte.Afficher(result.Erreur);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        //---------------------------------------------------------
        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        //---------------------------------------------------------
        public static void EditeFormulesGlobales()
        {
            CFormEditeFormulesGlobales form = new CFormEditeFormulesGlobales();
            form.ShowDialog();
            form.Dispose();
        }


    }
}
