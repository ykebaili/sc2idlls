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
    public partial class CFormEditeUniteInDb : Form
    {
        private CUniteInDb m_unite = null;
        private bool m_bIsNewUnite = false;

        //--------------------------------------------------------------
        public CFormEditeUniteInDb()
        {
            InitializeComponent();
        }

        //--------------------------------------------------------------
        public static bool EditeUnite(CUniteInDb unite)
        {
            CFormEditeUniteInDb form = new CFormEditeUniteInDb();
            form.m_unite = unite;
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
        private void CFormEditeUniteInDb_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate ( this );

            if (m_unite == null)
            {
                m_unite = new CUniteInDb(CContexteDonneeSysteme.GetInstance());
                m_unite.CreateNew();
                m_bIsNewUnite = true;
            }
            else
            {
                m_unite.BeginEdit();
                m_bIsNewUnite = false;
            }

            m_cmbClasse.ListDonnees = CGestionnaireUnites.Classes;
            m_cmbClasse.ProprieteAffichee = "Libelle";
            m_cmbClasse.SelectedValue = m_unite.Classe;

            m_txtIdUnite.Text = m_unite.GlobalId;
            m_txtLibellCourtUnite.Text = m_unite.Libelle;
            m_txtLibelleLongUnite.Text = m_unite.LibelleLong;
            m_txtFacteurConversion.DoubleValue = m_unite.FacteurVersBase;
            m_txtOffsetConversion.DoubleValue = m_unite.OffsetVersBase;
            RefreshFormule();
        }

        //------------------------------------------------------------
        private void RefreshFormule()
        {
            IClasseUnite classe = m_cmbClasse.SelectedValue as IClasseUnite;
            if (classe != null)
            {
                m_lblConversion.Text = 1 + m_txtLibellCourtUnite.Text + " = " +
                        "A" + classe.Libelle + "+B";
                return;
            }
            m_lblConversion.Text = "";
        }

        //--------------------------------------------------------
        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            if (m_bIsNewUnite)
            {
                m_unite.CancelCreate();
            }
            else
                m_unite.CancelEdit();
            DialogResult = DialogResult.Cancel;
            Close();
        }

        //--------------------------------------------------------
        private void m_btnOk_Click(object sender, EventArgs e)
        {
            IClasseUnite classe = m_cmbClasse.SelectedValue as IClasseUnite;
            CResultAErreur result = CResultAErreur.True;
            if (classe == null)
            {
                result.EmpileErreur("Select a unity class|20073");
            }
            if ( result )
            {
                m_unite.Classe = classe;
                m_unite.Libelle = m_txtLibellCourtUnite.Text;
                m_unite.LibelleLong = m_txtLibelleLongUnite.Text;
                m_unite.GlobalId = m_txtIdUnite.Text;
                m_unite.FacteurVersBase = m_txtFacteurConversion.DoubleValue.Value;
                m_unite.OffsetVersBase = m_txtOffsetConversion.DoubleValue.Value;
                result = m_unite.CommitEdit();
            }
            if ( !result )
            {
                CFormAlerte.Afficher ( result.Erreur );
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void m_cmbClasse_SelectionChangeCommitted(object sender, EventArgs e)
        {
            RefreshFormule();
        }      

    }
}
