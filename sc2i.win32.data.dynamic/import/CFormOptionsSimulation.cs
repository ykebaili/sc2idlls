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

namespace sc2i.win32.data.dynamic.import
{
    public partial class CFormOptionsSimulation : Form
    {
        public CFormOptionsSimulation()
        {
            InitializeComponent();
            CWin32Traducteur.Translate(this);
        }

        public static bool EditeOptions(COptionSimulationSmartImport option)
        {
            using (CFormOptionsSimulation frm = new CFormOptionsSimulation())
            {
                frm.m_txtStartLine.IntValue = option.StartLine;

                frm.m_txtNbLines.IntValue = option.NbLineToImport;
                frm.m_chkSimulateWriting.Checked = option.TestDbWriting;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    option.StartLine = frm.m_txtStartLine.IntValue;
                    option.NbLineToImport = frm.m_txtNbLines.IntValue;
                    option.TestDbWriting = frm.m_chkSimulateWriting.Checked;
                    return true;
                }
                return false;
            }
        }

        private void CFormOptionsSimulation_Load(object sender, EventArgs e)
        {

        }

        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void m_btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
