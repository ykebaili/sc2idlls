using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.common;
using sc2i.data.dynamic;
using sc2i.win32.common;

namespace sc2i.win32.data.navigation
{
    public partial class CFormCreerListeEntites : Form
    {
        private CListeEntites m_listeToEdit = null;
        public CFormCreerListeEntites()
        {
            InitializeComponent();
       
        }

        public static bool EditeListe ( CListeEntites liste )
        {
            CFormCreerListeEntites form = new CFormCreerListeEntites();
            form.m_listeToEdit = liste;
            bool bResult = false;
            if ( form.ShowDialog() == DialogResult.OK )
                bResult = true;
            form.Dispose();
            return bResult;
        }

        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void m_btnOk_Click(object sender, EventArgs e)
        {
            m_listeToEdit.Libelle = m_txtListName.Text;
            CResultAErreur result = m_listeToEdit.VerifieDonnees ( false );
            if ( !result )
            {
                CFormAlerte.Afficher ( result.Erreur );
                return;
            }
            DialogResult = DialogResult.OK;
            Close();            
        }

        private void CFormCreerListeEntites_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
            m_txtListName.Text = m_listeToEdit.Libelle;
        }
    }
}
