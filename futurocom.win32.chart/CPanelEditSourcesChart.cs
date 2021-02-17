using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using futurocom.chart;
using sc2i.win32.common;
using futurocom.win32.chart.sources;
using sc2i.expression;
using sc2i.common;

namespace futurocom.win32.chart
{
    public partial class CPanelEditSourcesChart : UserControl
    {
        private CChartSetup m_chartSetup = null;
        private List<CParametreSourceChart> m_listeParametres = new List<CParametreSourceChart>();

        //--------------------------------------------------------
        public CPanelEditSourcesChart()
        {
            InitializeComponent();
        }

        //--------------------------------------------------------
        private void m_wndListeSources_SizeChanged(object sender, EventArgs e)
        {
            m_wndListeSources.Columns[0].Width = Math.Max(40, m_wndListeSources.Width - 20);
        }

        public event EventHandler SourcesChanged;

        //--------------------------------------------------------
        public void Init(
            CChartSetup chartSetup)
        {
            m_listeParametres.Clear();
            m_chartSetup = chartSetup;
            FillListeSources();
        }

        //--------------------------------------------------------
        public CResultAErreur MajChamps()
        {
            CResultAErreur result = CResultAErreur.True;
            return result;
        }

        //--------------------------------------------------------
        private void FillListeSources()
        {
            m_wndListeSources.BeginUpdate();
            m_wndListeSources.Items.Clear();
            foreach (CParametreSourceChart parametre in m_chartSetup.ParametresDonnees.ParametresSourceFV)
            {
                ListViewItem item = new ListViewItem(parametre.SourceName);
                item.Tag = parametre;
                m_wndListeSources.Items.Add(item);
            }
            m_wndListeSources.EndUpdate();
        }

        //--------------------------------------------------------
        private void AssureMenuTypesSource()
        {
            if (m_menuTypesSources.Items.Count > 0)
                return;
            foreach (Type tp in CParametreSourceChart.TypesSourcesConnus)
            {
                CParametreSourceChart parametre = (CParametreSourceChart)Activator.CreateInstance(tp, new object[]{m_chartSetup});
                ToolStripMenuItem itemNewParametre = new ToolStripMenuItem(parametre.SourceTypeName);
                itemNewParametre.Tag = tp;
                itemNewParametre.Click += new EventHandler(itemNewParametre_Click);
                m_menuTypesSources.Items.Add(itemNewParametre);
            }
        }

        //--------------------------------------------------------
        void itemNewParametre_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            Type tp = menuItem != null ? menuItem.Tag as Type : null;
            if (tp != null)
            {
                CParametreSourceChart parametre = (CParametreSourceChart)Activator.CreateInstance(tp, new object[]{m_chartSetup});
                ListViewItem item = new ListViewItem(parametre.SourceName);
                item.Tag = parametre;
                m_wndListeSources.Items.Add(item);
                if (!EditeParametre(ref parametre))
                {
                    m_wndListeSources.Items.Remove(item);
                }
            }
        }


        //--------------------------------------------------------
        private void m_lnkAddSource_LinkClicked(object sender, EventArgs e)
        {
            AssureMenuTypesSource();
            m_menuTypesSources.Show(m_lnkAddSource, new Point(0, m_lnkAddSource.Height));
        }

        //--------------------------------------------------------
        public bool EditeParametre(ref CParametreSourceChart parametre)
        {
            parametre = parametre.GetCloneAvecMemeId();
            Type tp = CEditeurSourceChart.GetTypeFormEditeur(parametre);
            if (tp != null)
            {
                Form frm = Activator.CreateInstance(tp) as Form;
                if (frm != null)
                {
                    ((IFormEditSourceChart)frm).InitChamps(
                        parametre,
                        new CFournisseurGeneriqueProprietesDynamiques(),
                        new CObjetPourSousProprietes(m_chartSetup));
                    DialogResult res = frm.ShowDialog();
                    frm.Dispose();
                    if (res == DialogResult.OK)
                    {
                        foreach (ListViewItem item in m_wndListeSources.Items)
                        {
                            if (((CParametreSourceChart)item.Tag).SourceId == parametre.SourceId)
                            {
                                item.Tag = parametre;
                                item.Text = parametre.SourceName;
                            }
                        }
                        m_chartSetup.ParametresDonnees.AddSourceFV(parametre);
                        if (SourcesChanged != null)
                            SourcesChanged(this, null);
                        return true;
                    }
                }
            }
            return false;
        }

        //-----------------------------------------------------------------------
        private void m_lnkDetailSource_LinkClicked(object sender, EventArgs e)
        {
            if (m_wndListeSources.SelectedItems.Count > 0)
            {
                ListViewItem item = m_wndListeSources.SelectedItems[0];
                CParametreSourceChart parametre = item.Tag as CParametreSourceChart;
                if (parametre != null)
                {
                    EditeParametre(ref parametre);
                }
            }
        }

        private void m_lnkDeleteSource_LinkClicked(object sender, EventArgs e)
        {
            if (m_wndListeSources.SelectedItems.Count > 0)
            {
                ListViewItem item = m_wndListeSources.SelectedItems[0];
                CParametreSourceChart parametre = item.Tag as CParametreSourceChart;
                if (parametre != null && CFormAlerte.Afficher(
                    I.T("Delete source '@1' ?|20035", parametre.SourceName),
                    EFormAlerteBoutons.OuiNon, EFormAlerteType.Question) ==
                    DialogResult.Yes)
                {
                    m_wndListeSources.Items.Remove(item);
                    m_chartSetup.ParametresDonnees.RemoveSourceFV(parametre);
                }
            }
        }

        





    }
}
