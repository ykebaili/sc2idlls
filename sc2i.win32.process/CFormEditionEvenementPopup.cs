using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.process;
using sc2i.win32.common;
using sc2i.common;

namespace sc2i.win32.process
{
    public partial class CFormEditionEvenementPopup : Form
    {
        private CEvenement m_evenement;
        public CFormEditionEvenementPopup()
        {
            InitializeComponent();
            CWin32Traducteur.Translate(this);
        }

        //------------------------------------------------------------
        public static bool EditeEvenement(CEvenement evenement)
        {
            CFormEditionEvenementPopup form = new CFormEditionEvenementPopup();
            form.m_evenement = evenement;
            bool bResult = form.ShowDialog() == DialogResult.OK;
            form.Dispose();
            return bResult;
        }

        //------------------------------------------------------------
        private void m_btnOk_Click(object sender, EventArgs e)
        {
            CResultAErreur result = m_panelEvenement.MAJ_Champs();
            if (result)
                result = m_evenement.VerifieDonnees(false);
            if (!result)
            {
                CFormAlerte.Afficher(result.Erreur);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();

        }

        //------------------------------------------------------------
        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }


        //------------------------------------------------------------
        private void CFormEditionEvenementPopup_Load(object sender, EventArgs e)
        {
            m_panelEvenement.InitChamps(m_evenement);
        }
    }
}
