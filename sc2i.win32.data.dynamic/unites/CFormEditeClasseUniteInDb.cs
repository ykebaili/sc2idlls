using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.data.dynamic.unite;
using sc2i.data;
using sc2i.common.unites;
using sc2i.win32.common;
using sc2i.common;

namespace sc2i.win32.data.dynamic.unites
{
    public partial class CFormEditeClasseUniteInDb : Form
    {
        private CClasseUniteInDb m_classe = null;
        private bool m_bIsNewClasse = false;

        //--------------------------------------------------------------
        public CFormEditeClasseUniteInDb()
        {
            InitializeComponent();
        }

        //--------------------------------------------------------------
        public static bool EditeClasse(CClasseUniteInDb classe)
        {
            CFormEditeClasseUniteInDb form = new CFormEditeClasseUniteInDb();
            form.m_classe = classe;
            bool bResult = false;
            if (form.ShowDialog() == DialogResult.OK)
            {
                bResult = true;
                CGestionnaireUnites.Refresh();
            }
            form.Dispose();
            return bResult;
        }

        //--------------------------------------------------------------
        private void CFormEditeClasseUniteInDb_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate ( this );

            if (m_classe == null)
            {
                m_classe = new CClasseUniteInDb(CContexteDonneeSysteme.GetInstance());
                m_classe.CreateNew();
                m_bIsNewClasse = true;
            }
            else
            {
                m_classe.BeginEdit();
                m_bIsNewClasse = false;
            }

            m_txtLibelleClasse.Text = m_classe.Libelle;
            m_txtIdClasse.Text = m_classe.GlobalId;
            m_txtUniteDeBase.Text = m_classe.UniteBase;
       }

      
        //--------------------------------------------------------
        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            if (m_bIsNewClasse)
            {
                m_classe.CancelCreate();
            }
            else
                m_classe.CancelEdit();
            DialogResult = DialogResult.Cancel;
            Close();
        }

        //--------------------------------------------------------
        private void m_btnOk_Click(object sender, EventArgs e)
        {
            CResultAErreur result = CResultAErreur.True;
            m_classe.Libelle = m_txtLibelleClasse.Text;
            m_classe.GlobalId = m_txtIdClasse.Text;
            m_classe.UniteBase = m_txtUniteDeBase.Text;
            result = m_classe.CommitEdit();
            if ( !result )
            {
                CFormAlerte.Afficher ( result.Erreur );
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

       

    }
}
