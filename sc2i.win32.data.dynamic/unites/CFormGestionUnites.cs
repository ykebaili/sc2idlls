using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.win32.common;

namespace sc2i.win32.data.dynamic.unites
{
    public partial class CFormGestionUnites : Form
    {
        public CFormGestionUnites()
        {
            InitializeComponent();
        }

        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CFormGestionUnites_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
            m_panelGestion.Init();
        }

        public static void GererLesUnites()
        {
            CFormGestionUnites form = new CFormGestionUnites();
            form.ShowDialog();
            form.Dispose();
        }
    }
}
