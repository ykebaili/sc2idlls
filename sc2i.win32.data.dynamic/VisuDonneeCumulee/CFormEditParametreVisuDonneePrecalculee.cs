using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using sc2i.data.dynamic;
using sc2i.common;

namespace sc2i.win32.data.dynamic
{
    [AutoExec("Autoexec")]
    public partial class CFormEditParametreVisuDonneePrecalculee : Form, IEditeurParametreVisuDonneePrecalculee
    {
        private CParametreVisuDonneePrecalculee m_parametre;

        public CFormEditParametreVisuDonneePrecalculee()
        {
            InitializeComponent();
        }

        public static void Autoexec()
        {
            CEditeurParametreVisuDonneePrecalculee.SetTypeEditeur(typeof(CFormEditParametreVisuDonneePrecalculee));
        }

        private void CFormEditParametreVisuDonneePrecalculee_Load(object sender, EventArgs e)
        {
            sc2i.win32.common.CWin32Traducteur.Translate(this);
            m_panelParametres.Init(m_parametre);
        }

        

        private void m_btnOk_Click(object sender, EventArgs e)
        {
            m_panelParametres.MajChamps();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }


        #region IEditeurParametreVisuDonneePrecalculee Membres

        public CParametreVisuDonneePrecalculee EditeParametre(CParametreVisuDonneePrecalculee parametre)
        {
            m_parametre = parametre;
            if (ShowDialog() == DialogResult.OK)
                return m_panelParametres.Parametre;
            return parametre;
        }

        #endregion

        private void m_lnkTester_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            m_panelParametres.MajChamps();
            CFormTestVisuTableauCroise.Teste(m_panelParametres.Parametre);
        }
    }
}
