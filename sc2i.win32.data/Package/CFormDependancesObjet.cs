using sc2i.data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sc2i.win32.data.Package
{
    public partial class CFormDependancesObjet : Form
    {
        //------------------------------------------------------------
        public CFormDependancesObjet()
        {
            InitializeComponent();
        }

        //------------------------------------------------------------
        public static void ShowDependances ( CObjetDonnee objet )
        {
            using ( CFormDependancesObjet frm = new CFormDependancesObjet())
            {
                using (CContexteDonnee contexte = new CContexteDonnee(objet.ContexteDonnee.IdSession, true, false))
                {
                    objet = objet.GetObjetInContexte(contexte);
                    frm.m_arbre.Init(objet);
                    frm.ShowDialog();
                }
            }
        }

        //------------------------------------------------------------
        private void m_arbre_Load(object sender, EventArgs e)
        {

        }
    }
}
