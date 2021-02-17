using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using futurocom.chart;
using sc2i.common;
using sc2i.win32.common;
using futurocom.chart.LegendArea;

namespace futurocom.win32.chart.legends
{
    public partial class CPanelEditeLegends : UserControl
    {
        private CChartSetup m_chartSetup = null;
        private CLegendArea m_editedLegend = null;

        //------------------------------------------------------------------------
        public CPanelEditeLegends()
        {
            InitializeComponent();
        }

        //------------------------------------------------------------------------
        private void m_wndListeLegends_SizeChanged(object sender, EventArgs e)
        {
            m_wndListeLegends.Columns[0].Width = m_wndListeLegends.Width - 20;
        }

        //------------------------------------------------------------------------
        public void Init(CChartSetup chart)
        {
            m_chartSetup = chart;
            EditeLegend(null);
            FillListeParametres();
        }

        //------------------------------------------------------------------------
        private void EditeLegend(CLegendArea parametre)
        {
            if (m_editedLegend != null)
                MajParametreEnCours();
            m_editedLegend = null;
            if (parametre == null)
                m_panelDetailSerie.Visible = false;
            else
            {
                m_panelDetailSerie.Visible = true;
                FillDetail(parametre);
            }
        }

        //------------------------------------------------------------------------
        private void FillDetail(CLegendArea parametre)
        {
            m_editedLegend = parametre;
            m_gridProprietes.SelectedObject = parametre;
        }

        //------------------------------------------------------------------------
        private void MajParametreEnCours()
        {
        }

        //------------------------------------------------------------------------
        private void FillListeParametres()
        {
            m_wndListeLegends.BeginUpdate();
            m_wndListeLegends.Items.Clear();
            foreach (CLegendArea p in m_chartSetup.Legends)
            {
                ListViewItem item = new ListViewItem();
                FillItem(item, p);
                m_wndListeLegends.Items.Add(item);
            }
            m_wndListeLegends.EndUpdate();
        }

        //-----------------------------------------
        private void FillItem(ListViewItem item, CLegendArea parametre)
        {
            item.Text = parametre.LegendName;
            item.Tag = parametre;
        }

        //------------------------------------------------------------------------
        private void m_lnkAddSerie_LinkClicked(object sender, EventArgs e)
        {
            CLegendArea p = new CLegendArea();
            p.LegendName= "Legend " + (m_wndListeLegends.Items.Count + 1);
            ListViewItem item = new ListViewItem();
            FillItem(item, p);
            m_wndListeLegends.Items.Add(item);
            item.Selected = true;
            MajChamps();
        }

        //------------------------------------------------------------------------
        private void m_wndListeLegends_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_wndListeLegends.SelectedItems.Count > 0)
            {
                ListViewItem item = m_wndListeLegends.SelectedItems[0];
                CLegendArea p = item.Tag as CLegendArea;
                if (p != null)
                    EditeLegend(p);
            }
            else
                EditeLegend(null);
        }

        //------------------------------------------------------------------------
        public CResultAErreur MajChamps()
        {
            List<CLegendArea> lstParametres = new List<CLegendArea>();
            foreach (ListViewItem item in m_wndListeLegends.Items)
            {
                CLegendArea p = item.Tag as CLegendArea;
                if (p != null)
                    lstParametres.Add(p);
            }
            m_chartSetup.Legends = lstParametres;
            return CResultAErreur.True;
        }

        private void m_gridProprietes_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "LegendName" && m_wndListeLegends.SelectedItems.Count > 0)
            {
                ListViewItem item = m_wndListeLegends.SelectedItems[0];
                if (item.Tag == m_editedLegend)
                    item.Text = m_editedLegend.LegendName;
            }
                
        }

        private void m_lnkRemoveSerie_LinkClicked(object sender, EventArgs e)
        {
            if (m_wndListeLegends.SelectedItems.Count == 1)
            {
                ListViewItem item = m_wndListeLegends.SelectedItems[0];
                if (CFormAlerte.Afficher(I.T("Delete selected Legend ?|20016"),
                    EFormAlerteBoutons.OuiNon,
                    EFormAlerteType.Question) == DialogResult.Yes)
                {
                    m_wndListeLegends.Items.Remove(item);
                    MajChamps();
                }
            }
        }


    }
}
