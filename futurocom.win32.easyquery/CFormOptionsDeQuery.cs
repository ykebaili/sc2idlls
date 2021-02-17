using futurocom.easyquery;
using sc2i.win32.common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace futurocom.win32.easyquery
{
    public partial class CFormOptionsDeQuery : Form
    {
        private CEasyQuery m_query;
        public CFormOptionsDeQuery()
        {
            InitializeComponent();
        }

        private void CFormOptionsDeQuery_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
            if (m_query != null)
                m_chkActiverCompatiblité4013.Checked = m_query.ModeCompatibilteTimos4_0_1_3;
        }

        public static void EditeOptions(CEasyQuery query)
        {
            using (CFormOptionsDeQuery frm = new CFormOptionsDeQuery())
            {
                frm.m_query = query;
                frm.ShowDialog();
            }
        }

        private void m_btnOk_Click(object sender, EventArgs e)
        {
            if (m_query != null)
                m_query.ModeCompatibilteTimos4_0_1_3 = m_chkActiverCompatiblité4013.Checked;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
