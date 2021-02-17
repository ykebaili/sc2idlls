using sc2i.common;
using sc2i.data.dynamic.StructureImport.SmartImport;
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
    public partial class CFormOptionsImport : Form
    {
        public CFormOptionsImport()
        {
            InitializeComponent();
            CWin32Traducteur.Translate(this);
        }

        public static bool EditeOptions(COptionExecutionSmartImport option)
        {
            using (CFormOptionsImport frm = new CFormOptionsImport())
            {
                frm.m_txtStartLine.IntValue = option.StartLine;
                frm.m_txtNbLines.IntValue = option.NbLineToImport;
                frm.m_chkBestEffort.Checked = option.BestEffort;
                frm.m_txtTaillePaquets.IntValue = option.TaillePaquets;
                frm.m_txtSaveFile.Text = option.NomFichierSauvegarde;
                frm.m_chkCreerVersionDonnees.Checked = option.UtiliserVersionDonnee;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    option.StartLine = frm.m_txtStartLine.IntValue;
                    option.NbLineToImport = frm.m_txtNbLines.IntValue;
                    option.BestEffort = frm.m_chkBestEffort.Checked;
                    option.NomFichierSauvegarde = frm.m_txtSaveFile.Text;
                    option.TaillePaquets = frm.m_txtTaillePaquets.IntValue;
                    option.UtiliserVersionDonnee = frm.m_chkCreerVersionDonnees.Checked;
                    return true;
                }
                return false;
            }
        }

        private void CFormOptionsImport_Load(object sender, EventArgs e)
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

        private void m_btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = I.T("Import session(*.futimpses)|*.futimpses|All files|*.*|20148");
            if (dlg.ShowDialog() == DialogResult.OK)
                m_txtSaveFile.Text = dlg.FileName;
        }
    }
}
