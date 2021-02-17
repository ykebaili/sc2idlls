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
    public partial class CFormEditeListeFormules : Form
    {
        public CFormEditeListeFormules()
        {
            InitializeComponent();
        }

        private void CFormEditeListeFormules_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
        }

        public static bool EditeFormules(ref C2iExpression[] lstExpressions, CObjetPourSousProprietes objetPourSousProprietes)
        {
            CFormEditeListeFormules form = new CFormEditeListeFormules();
            List<CFormuleNommee> lstFormules = new List<CFormuleNommee>();
            foreach (C2iExpression exp in lstExpressions)
            {
                if (exp != null)
                    lstFormules.Add(new CFormuleNommee("", exp));
            }
            form.m_wndListeFormules.Init(lstFormules.ToArray(), objetPourSousProprietes, null);
            bool bResult = false;
            if (form.ShowDialog() == DialogResult.OK)
            {
                lstFormules = new List<CFormuleNommee>(form.m_wndListeFormules.GetFormules());
                List<C2iExpression> lstExp = new List<C2iExpression>();
                foreach (CFormuleNommee f in lstFormules)
                    if (f.Formule != null)
                        lstExp.Add(f.Formule);
                lstExpressions = lstExp.ToArray();
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
    public class CEditeurListeFormules : IEditeurListeFormules
    {
        public static void Autoexec()
        {
            CListeFormulesEditor.SetTypeEditeur(typeof(CEditeurListeFormules));
        }

        private CObjetPourSousProprietes m_objetPourSousProprietes = null;
        #region IEditeurListeFonctions Membres

        public C2iExpression[] EditeFormules(C2iExpression[] formulesParam)
        {
            C2iExpression[] formules = formulesParam;
            CFormEditeListeFormules.EditeFormules(ref formules, m_objetPourSousProprietes);
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