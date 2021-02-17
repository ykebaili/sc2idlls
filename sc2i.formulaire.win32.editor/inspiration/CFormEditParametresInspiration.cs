using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.win32.common;
using sc2i.common;
using sc2i.formulaire.inspiration;

namespace sc2i.formulaire.win32.inspiration
{
    public partial class CFormEditParametresInspiration : Form
    {
        private CListeParametresInspiration m_listeFinale = null;
        //-----------------------------------------------------------------------
        public CFormEditParametresInspiration()
        {
            InitializeComponent();
            CWin32Traducteur.Translate(this);
        }


        //-----------------------------------------------------------------------
        private void CFormEditParametresInspiration_Load(object sender, EventArgs e)
        {

        }

        //-----------------------------------------------------------------------
        public static CListeParametresInspiration EditeParametres(CListeParametresInspiration parametres)
        {
            if (parametres == null)
                parametres = new CListeParametresInspiration();
            CFormEditParametresInspiration form = new CFormEditParametresInspiration();
            form.m_panelParametres.Init(parametres);
            if (form.ShowDialog() == DialogResult.OK && form.m_listeFinale != null)
                parametres = form.m_listeFinale;
            form.Dispose();
            return parametres;
        }

        private void m_btnAnnulerModifications_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();

        }

        private void m_btnValiderModifications_Click(object sender, EventArgs e)
        {
            CResultAErreurType<CListeParametresInspiration> res = m_panelParametres.MajChamps();
            if (!res)
                CFormAlerte.Afficher(res.Erreur);
            else
            {
                m_listeFinale = res.DataType;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

    }

    [AutoExec("Autoexec")]
    public class CEditeurParametresInspiration : IEditeurParametresInspiration
    {
        public static void Autoexec()
        {
            CParametresInspriationEditor.SetTypeEditeur(typeof(CEditeurParametresInspiration));
        }
        public CListeParametresInspiration EditeParametres(CListeParametresInspiration parametres)
        {
            return CFormEditParametresInspiration.EditeParametres ( parametres );
        }

    }

}
