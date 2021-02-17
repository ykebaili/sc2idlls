using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.process.workflow;
using sc2i.data.dynamic;
using sc2i.common;
using sc2i.win32.common;

namespace sc2i.win32.process.workflow
{
    public partial class CFormEditeModeleAffectationPopup : Form
    {
        private CModeleAffectationUtilisateurs m_modele = null;
        public CFormEditeModeleAffectationPopup()
        {
            InitializeComponent();
        }

        public static bool EditeModele ( CModeleAffectationUtilisateurs modele )
        {
            CFormEditeModeleAffectationPopup form = new CFormEditeModeleAffectationPopup();
            if ( !modele.IsNew() )
                modele.BeginEdit();
            form.m_modele = modele;
            bool bResult = form.ShowDialog() == DialogResult.OK;
            form.Dispose();
            return bResult;
        }

        public void Init (  )
        {
            m_txtLibelle.Text = m_modele.Libelle;
            m_panelFormules.Init ( m_modele.ParametresAffectation.Formules.ToArray(), typeof(CWorkflow), new CFournisseurPropDynStd() );
        }

        public CResultAErreur MajChamps()
        {
            CParametresAffectationEtape parametre = m_modele.ParametresAffectation;
            parametre.Formules = m_panelFormules.GetFormules();
            m_modele.ParametresAffectation = parametre;
            m_modele.Libelle = m_txtLibelle.Text;
            CResultAErreur result = m_modele.VerifieDonnees( true );
            if ( result )
                result = m_modele.CommitEdit();
            return result;
        }

        private void CFormEditeModeleAffectationPopup_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
            Init();
        }

        private void m_btnOk_Click(object sender, EventArgs e)
        {
            CResultAErreur result = MajChamps();
            if ( !result )
            {
                CFormAlerte.Afficher ( result.Erreur );
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            if (m_modele.IsNew() )
                m_modele.CancelCreate();
            else
                m_modele.CancelEdit();
            DialogResult = DialogResult.Cancel;
            Close();        
        }






    }
}
