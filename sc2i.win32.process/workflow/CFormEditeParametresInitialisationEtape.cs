using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.win32.common;
using sc2i.process.workflow;
using sc2i.common;
using sc2i.data.dynamic;
using sc2i.expression;

namespace sc2i.win32.process.workflow
{
    public partial class CFormEditeParametresInitialisationEtape : Form
    {
        private CParametresInitialisationEtape m_parametre = null;

        public CFormEditeParametresInitialisationEtape()
        {
            InitializeComponent();
        }

        public static CParametresInitialisationEtape EditeParametres(CParametresInitialisationEtape parametres)
        {
            CFormEditeParametresInitialisationEtape form = new CFormEditeParametresInitialisationEtape();
            form.m_parametre = CCloner2iSerializable.Clone(parametres) as CParametresInitialisationEtape;
            CParametresInitialisationEtape retour = parametres;
            if ( form.ShowDialog() == DialogResult.OK )
            {
                retour = form.m_parametre;
            }
            form.Dispose();
            return retour;
        }

        private void CFormEditeParametresAffectationEtape_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
            m_panelFormules.Init(m_parametre);
        }

        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void m_btnOk_Click(object sender, EventArgs e)
        {
            List<CFormuleNommee> lst = new List<CFormuleNommee>();
            CResultAErreurType<CParametresInitialisationEtape> result = m_panelFormules.MajChamps();
            if (!result)
            {
                CFormAlerte.Afficher(result.Erreur);
                return;
            }
            m_parametre = result.DataType;
            DialogResult = DialogResult.OK;
            Close();
        }
    }

    [AutoExec("Autoexec")]
    public class CEditeurParametresAffectationEtape : IEditeurParametresInitialisationEtape
    {
        #region IEditeurParametresAffectationProprietes Membres

        public CParametresInitialisationEtape EditeParametres(CParametresInitialisationEtape parametres)
        {
            return CFormEditeParametresInitialisationEtape.EditeParametres(parametres);
        }

        public static void Autoexec()
        {
            CParametresInitialisationEtapeEditor.SetTypeEditeur(typeof(CEditeurParametresAffectationEtape));
        }

        #endregion
    }
}
