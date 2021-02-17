using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.data.dynamic;
using sc2i.common;

namespace sc2i.win32.data.dynamic
{
    public partial class CControlEditModuleParametrage : UserControl
    {
        public CControlEditModuleParametrage()
        {
            InitializeComponent();
        }

        private static CModuleParametrage m_moduleParametrage;

        public void Init(CModuleParametrage module)
        {
            m_moduleParametrage = module;
            if (module != null)
            {
                string strLibelle = module.Libelle;
                if (strLibelle == "")
                {
                    m_txtLibelle.Text = I.T("New Module|10003");
                    m_txtLibelle.SelectAll();
                }
                else
                    m_txtLibelle.Text = strLibelle;
                m_txtDescription.Text = module.Description;
            }
        }

        public void MAJ_champs()
        {
            if (m_moduleParametrage != null && m_moduleParametrage.IsValide())
            {
                m_moduleParametrage.Libelle = m_txtLibelle.Text;
                m_moduleParametrage.Description = m_txtDescription.Text;
            }
        }
    }
}
