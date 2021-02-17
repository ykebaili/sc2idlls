using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using sc2i.win32.common;
using sc2i.data.dynamic;

namespace sc2i.win32.data.dynamic
{
    public partial class CFormInfosNouveauModule : Form
    {
        public CFormInfosNouveauModule()
        {
            InitializeComponent();
        }

        private static CModuleParametrage m_moduleParametrage;

        public static bool EditModule(CModuleParametrage module)
        {
            m_moduleParametrage = module;

            CFormInfosNouveauModule form = new CFormInfosNouveauModule();
            if (form.ShowDialog() == DialogResult.OK)
                return true;

            return false;

        }

        private void m_btnAdd_Click(object sender, EventArgs e)
        {
            m_ctrlEditModuleParametrage.MAJ_champs();

            DialogResult = DialogResult.OK;
        }

        private void CFormInfosNouveauModule_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
            m_ctrlEditModuleParametrage.Init(m_moduleParametrage);
        }
    }
}
