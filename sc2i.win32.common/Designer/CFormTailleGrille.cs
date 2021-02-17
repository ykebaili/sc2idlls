using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace sc2i.win32.common
{
    public partial class CFormTailleGrille : CFormPopUpEditeur
    {
        public CFormTailleGrille(CGrilleEditeurObjetGraphique grille)
        {
            InitializeComponent();
            
            m_grille = grille;
            m_txtLargeur.IntValue = grille.LargeurCarreau;
            m_txtHauteur.IntValue = grille.HauteurCarreau;
            CWin32Traducteur.Translate(this);
        }
        private CGrilleEditeurObjetGraphique m_grille;

        private void CFormTailleGrille_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_grille.TailleCarreau = new Size(m_txtLargeur.IntValue.Value, m_txtHauteur.IntValue.Value);
        }

        private void CFormTailleGrille_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!m_txtLargeur.IntValue.HasValue)
            {
            }
            else if (!m_txtHauteur.IntValue.HasValue)
            {
            }
            else if (m_txtHauteur.IntValue.Value == 0)
            {
            }
            else if (m_txtLargeur.IntValue.Value == 0)
            {
            }
            else if (m_txtHauteur.IntValue.Value < 0)
            {
            }
            else if (m_txtLargeur.IntValue.Value < 0)
            {
            }
            else
            {
                return;
            }
            e.Cancel = true;
        }

        internal static void AfficherDialog(CGrilleEditeurObjetGraphique grille)
        {
            CFormTailleGrille frm = new CFormTailleGrille(grille);
            frm.ShowDialog();
        }
    }
}
