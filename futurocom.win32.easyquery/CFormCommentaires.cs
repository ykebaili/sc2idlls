using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.win32.common;

namespace futurocom.win32.easyquery
{
    public partial class CFormCommentaires : Form
    {
        public CFormCommentaires()
        {
            InitializeComponent();
        }

        private void CFormCommentaires_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
        }

        private void m_btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public static string GetTexte(string strText)
        {
            CFormCommentaires form = new CFormCommentaires();
            form.m_txtCommentaire.Text = strText;
            string strResult = strText;
            if (form.ShowDialog() == DialogResult.OK)
                strResult = form.m_txtCommentaire.Text;
            form.Dispose();
            return strResult;
        }
    }
}
