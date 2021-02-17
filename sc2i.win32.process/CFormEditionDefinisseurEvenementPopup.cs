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
using sc2i.process.workflow;

namespace sc2i.win32.process
{
    public partial class CFormEditionDefinisseurEvenementPopup : Form
    {
        private IDefinisseurEvenements m_definisseur;
        public CFormEditionDefinisseurEvenementPopup()
        {
            InitializeComponent();
            CWin32Traducteur.Translate(this);
        }



        //------------------------------------------------------------
        public static bool EditeDefinisseur(IDefinisseurEvenements definisseur)
        {
            CFormEditionDefinisseurEvenementPopup form = new CFormEditionDefinisseurEvenementPopup();
            form.m_definisseur = definisseur;
            bool bResult = form.ShowDialog() == DialogResult.OK;
            form.Dispose();
            return bResult;
        }

        //------------------------------------------------------------
        private void m_btnOk_Click(object sender, EventArgs e)
        {
            CResultAErreur result = m_panelEvenement.MAJ_Champs();
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
        private void CFormEditionDefinisseurEvenementPopup_Load(object sender, EventArgs e)
        {
            m_panelEvenement.InitChamps(m_definisseur);
        }
    }

    //------------------------------------------------------------
    [AutoExec("Autoexec")]
    public class CEditeurDefinisseurEvenements : IDefinisseurEvenementsEditor
    {

        //------------------------------------------------------------
        public static void Autoexec()
        {
            CDefinisseurEvenementEditor.SetTypeEditeur(typeof(CEditeurDefinisseurEvenements));
        }

        public void EditeDefinisseur(IDefinisseurEvenements definisseur)
        {
            CFormEditionDefinisseurEvenementPopup.EditeDefinisseur(definisseur);
        }

    }
}
