using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using sc2i.common;
using sc2i.expression;
using sc2i.win32.common;

namespace sc2i.formulaire.win32
{
    public partial class CFormEditeListeFormulesNommees : Form
    {
        public CFormEditeListeFormulesNommees()
        {
            InitializeComponent();
        }

        private void CFormEditeListeFormulesNommees_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
        }

        public static bool EditeFormules(ref CFormuleNommee[] formules, CObjetPourSousProprietes objetPourSousProprietes)
        {
            CFormEditeListeFormulesNommees form = new CFormEditeListeFormulesNommees();
            form.m_wndListeFormules.Init(formules, objetPourSousProprietes, null);
            bool bResult = false;
            if (form.ShowDialog() == DialogResult.OK)
            {
                formules = form.m_wndListeFormules.GetFormules();
                bResult = true;
            }
            form.Dispose();
            return bResult;
        }

        private void m_btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }

    [AutoExec("Autoexec")]
    public class CEditeurListeNommees : IEditeurListeFonctions
    {
        public static void Autoexec()
        {
            CListeFonctionsEditor.SetTypeEditeur(typeof(CEditeurListeNommees));
        }

        private CObjetPourSousProprietes m_objetPourSousProprietes = null;
        #region IEditeurListeFonctions Membres

        public CFormuleNommee[] EditeFonctions(sc2i.expression.CFormuleNommee[] formulesNommees)
        {
            CFormuleNommee[] formules = formulesNommees;
            CFormEditeListeFormulesNommees.EditeFormules(ref formules, m_objetPourSousProprietes);
            return formules;
        }

        public CObjetPourSousProprietes ObjetPourSousProprietes
        {
            get
            {
                return m_objetPourSousProprietes;
            }
            set
            {
                m_objetPourSousProprietes = value;
            }
        }

        #endregion
    }
}