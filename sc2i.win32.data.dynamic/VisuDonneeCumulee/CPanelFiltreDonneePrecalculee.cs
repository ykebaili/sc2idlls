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
using sc2i.expression;
using sc2i.win32.common;

namespace sc2i.win32.data.dynamic
{
    public partial class CPanelFiltreDonneePrecalculee : UserControl
    {
        private CFiltreDonneePrecalculee m_filtre;
        private bool m_bAvecInterface = false;

        public CPanelFiltreDonneePrecalculee()
        {
            InitializeComponent();
        }

        public void Init(CFiltreDonneePrecalculee filtre, bool bAvecInterface, bool bAvecDescriptif)
        {
            m_filtre = filtre;
            UpdateVisuFiltre();
            m_txtLibelle.Text = m_filtre.Libelle;
            m_bAvecInterface = bAvecInterface;
            m_txtFormuleDesc.Init(m_filtre.Filtre, new CObjetPourSousProprietes( m_filtre.Filtre));
            m_txtFormuleDesc.Formule = m_filtre.FormuleDescription;
            if (bAvecDescriptif)
            {
                m_txtFormuleDesc.Visible = true;
            }
            else
                Height = m_panelTop.Height+1;
        }

        private void UpdateVisuFiltre()
        {
            if (m_filtre != null && m_filtre.Filtre.ComposantPrincipal != null)
                m_imageHasFiltre.Image = m_imageHasFiltreRef.Image;
            else
                m_imageHasFiltre.Image = null;
        }

        private void m_btnEditFiltre_Click(object sender, EventArgs e)
        {
            CFormEditFiltreDynamique.EditeFiltre(m_filtre.Filtre, m_bAvecInterface, false, null);
            UpdateVisuFiltre();
            m_txtFormuleDesc.Init(m_filtre.Filtre, new CObjetPourSousProprietes(m_filtre.Filtre));
        }

        private void m_txtLibelle_TextChanged(object sender, EventArgs e)
        {
            m_filtre.Libelle = m_txtLibelle.Text;
        }

        public void MajChamps()
        {
            m_filtre.FormuleDescription = m_txtFormuleDesc.Formule;
        }

        private void CPanelFiltreDonneePrecalculee_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
        }

    }
}
