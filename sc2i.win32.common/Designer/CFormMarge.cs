using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace sc2i.win32.common
{
    public partial class CFormMarge : CFormPopUpEditeur
    {
        public CFormMarge(CPanelEditionObjetGraphique editeur)
        {
            InitializeComponent();
            m_editeur = editeur;
            m_txtMarge.IntValue = editeur.Marge;
            CWin32Traducteur.Translate(this);
        }
        private CPanelEditionObjetGraphique m_editeur;
        internal static void AfficherDialog(CPanelEditionObjetGraphique editeur)
        {
            CFormMarge frm = new CFormMarge(editeur);
            frm.ShowDialog();
        }

        private void CFormMarge_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!m_txtMarge.IntValue.HasValue)
            {
            }
            else if (m_txtMarge.IntValue.Value < 0)
            {
            }
            else
                return;

            e.Cancel = true;
        }

        private void CFormMarge_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_editeur.Marge = m_txtMarge.IntValue.Value;
        }
    }
}
