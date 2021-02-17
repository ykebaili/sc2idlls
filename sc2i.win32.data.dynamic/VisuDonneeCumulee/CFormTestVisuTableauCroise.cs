using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using sc2i.data.dynamic;
using sc2i.win32.data;
using sc2i.common;

namespace sc2i.win32.data.dynamic
{
    public partial class CFormTestVisuTableauCroise : Form
    {
        private CParametreVisuDonneePrecalculee m_parametre = null;
        public CFormTestVisuTableauCroise()
        {
            InitializeComponent();
        }

        public static void Teste(CParametreVisuDonneePrecalculee parametreVisu)
        {
            CResultAErreur result = parametreVisu.GetDataTable(CSc2iWin32DataClient.ContexteCourant);
            if (result)
            {
                CFormTestVisuTableauCroise form = new CFormTestVisuTableauCroise();
                form.m_parametre = parametreVisu;
                form.ShowDialog();
                form.Dispose();
            }

        }

        private void CFormTest_Load(object sender, EventArgs e)
        {
            sc2i.win32.common.CWin32Traducteur.Translate(this);
            m_grid.Init(m_parametre, CSc2iWin32DataClient.ContexteCourant);
        }
            
    }
}
