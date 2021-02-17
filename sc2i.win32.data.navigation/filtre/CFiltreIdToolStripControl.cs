using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sc2i.win32.data.navigation.filtre
{
    public partial class CFiltreIdToolStripControl : UserControl
    {
        public CFiltreIdToolStripControl()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------
        public event EventHandler OnValideSaisie;
        private void m_btnValide_Click(object sender, EventArgs e)
        {
            ValideLaSaisie();
        }

        //-------------------------------------------------------------
        private void ValideLaSaisie()
        {
            if (m_txtId.IntValue != null && OnValideSaisie != null)
            {
                OnValideSaisie(this, null);
            }
        }

        //-------------------------------------------------------------
        public int? IdDemande
        {
            get
            {
                return m_txtId.IntValue;
            }
        }


        //-------------------------------------------------------------
        private void m_txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ValideLaSaisie();

        }
    }
}
