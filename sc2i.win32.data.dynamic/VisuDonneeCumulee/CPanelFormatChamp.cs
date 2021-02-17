using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.expression;
using sc2i.data;
using sc2i.data.dynamic;
using sc2i.win32.expression;
using sc2i.formulaire;


namespace sc2i.win32.data.dynamic
{
    public partial class CPanelFormatChamp : UserControl
    {
        private CParametreVisuChampTableauCroise m_parametreVisuChamp = null;
        private CParametreVisuDonneePrecalculee m_parametreVisuDonnee = null;
        private CContexteDonnee m_contexteDonnee = null;

        public CPanelFormatChamp()
        {
            InitializeComponent();
        }

        private void m_lblFont_Click(object sender, EventArgs e)
        {

        }

        private void m_colorFore_Click(object sender, EventArgs e)
        {

        }

        private void CPanelFormatChamp_Load(object sender, EventArgs e)
        {

        }

        public void Init(
            CParametreVisuChampTableauCroise parametre,
            CParametreVisuDonneePrecalculee parametreVisuDonneePrecalculee,
            CContexteDonnee contexteDonnee)
        {
            if (m_parametreVisuChamp != null)
                MajChamps();
            m_contexteDonnee = contexteDonnee;
            m_parametreVisuDonnee = parametreVisuDonneePrecalculee;
            m_parametreVisuChamp = parametre;
            if (parametre == null)
            {
                Visible = false;
                return;
            }
            Visible = true;
            CTypeDonneeCumulee typeDonnee = m_parametreVisuDonnee.GetTypeDonneeCumulee(m_contexteDonnee);
            CParametreDonneeCumulee parametreDonnee = null;
            if (typeDonnee != null)
                parametreDonnee = typeDonnee.Parametre;

            m_lblNomChamp.Text = parametre.ChampFinal.NomChamp;
            m_panelFormatStandard.Init(m_parametreVisuChamp.FormatParDefaut, 
                parametre.GetObjetPourFormuleData(parametreDonnee), false);
            m_panelFormatHeader.Init(m_parametreVisuChamp.FormatHeader, null, true);

            
            CElementAVariablesDynamiques obj = m_parametreVisuChamp.GetObjetPourFormuleHeader(parametreDonnee);
            m_txtFormuleHeader.Init(obj,
                new CObjetPourSousProprietes(obj));

            obj = m_parametreVisuChamp.GetObjetPourFormuleData(parametreDonnee);
            m_txtFormuleData.Init(obj,
                new CObjetPourSousProprietes(obj));

            m_txtFormuleData.Formule = m_parametreVisuChamp.FormuleData;
            m_txtFormuleHeader.Formule = m_parametreVisuChamp.FormuleHeader;
            m_txtSortOrder.IntValue = m_parametreVisuChamp.SortOrder;
            m_chkDecroissant.Checked = m_parametreVisuChamp.TriDecroissant;

            m_imageLink.Visible = m_parametreVisuChamp.ActionSurClick != null;
        }

        public void MajChamps()
        {
            if (m_parametreVisuChamp != null)
            {
                m_panelFormatStandard.MajChamps();
                m_panelFormatHeader.MajChamps();
                m_parametreVisuChamp.FormuleHeader = m_txtFormuleHeader.Formule;
                m_parametreVisuChamp.FormuleData = m_txtFormuleData.Formule;
                m_parametreVisuChamp.SortOrder = m_txtSortOrder.IntValue;
                m_parametreVisuChamp.TriDecroissant = m_chkDecroissant.Checked;
            }
        }

        private void cSlidingZone1_Load(object sender, EventArgs e)
        {

        }

        private void cSlidingZone2_Load(object sender, EventArgs e)
        {

        }

        private void m_lnkAction_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CElementAVariablesDynamiques elt = m_parametreVisuChamp.GetObjetPourFormuleCellule(m_parametreVisuDonnee) as CElementAVariablesDynamiques;
            CActionSur2iLink action = m_parametreVisuChamp.ActionSurClick;
            CActionSur2iLinkEditor.EditeAction(ref action, new CObjetPourSousProprietes(elt));
            m_parametreVisuChamp.ActionSurClick = action;
            m_imageLink.Visible = m_parametreVisuChamp.ActionSurClick != null;
        }

        private void m_txtSortOrder_TextChanged(object sender, EventArgs e)
        {
            m_chkDecroissant.Visible = m_txtSortOrder.Text!="";
        }

       

       
    }
}
