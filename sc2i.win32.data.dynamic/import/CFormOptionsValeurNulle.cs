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
    public partial class CFormOptionsValeursNulle : Form
    {
        public CFormOptionsValeursNulle()
        {
            InitializeComponent();
            CWin32Traducteur.Translate(this);
        }

        public static bool EditeOptions(ref COptionsValeursNulles option)
        {
            if ( option == null)
                option = new COptionsValeursNulles();
            using (CFormOptionsValeursNulle frm = new CFormOptionsValeursNulle())
            {
                frm.m_chkNullSiErreurConversion.Checked = option.NullOnConversionError;
                StringBuilder bl = new StringBuilder();
                foreach ( string strValeur in option.ValeursNulles )
                {
                    bl.Append(strValeur);
                    bl.Append(Environment.NewLine);
                }
                frm.m_txtValeursNulles.Text = bl.ToString();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    option.NullOnConversionError = frm.m_chkNullSiErreurConversion.Checked;
                    if (frm.m_txtValeursNulles.Text.Length > 0)
                    {
                        string[] strVals = frm.m_txtValeursNulles.Text.Split('\n');
                        option.ValeursNulles = strVals;
                    }
                    else
                        option.ValeursNulles = null;
                    return true;
                }
                return false;
            }
        }

        private void CFormOptionsValeursNulle_Load(object sender, EventArgs e)
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
