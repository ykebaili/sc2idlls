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

namespace futurocom.win32.chart.series
{
    public partial class CPanelEditeSeries : UserControl
    {
        private CChartSetup m_chartSetup = null;
        private CParametreSerieDeChart m_parametreEdite = null;

        //------------------------------------------------------------------------
        public CPanelEditeSeries()
        {
            InitializeComponent();
        }

        //------------------------------------------------------------------------
        private void m_wndListeSeries_SizeChanged(object sender, EventArgs e)
        {
            m_wndListeSeries.Columns[0].Width = m_wndListeSeries.Width - 20;
        }

        //------------------------------------------------------------------------
        public void Init(CChartSetup chart)
        {
            m_chartSetup = chart;
            EditeParametre(null);
            FillListeParametres();
        }

        //------------------------------------------------------------------------
        private void EditeParametre(CParametreSerieDeChart parametre)
        {
            if (m_parametreEdite != null)
                MajParametreEnCours();
            m_parametreEdite = null;
            if (parametre == null)
                m_panelDetailSerie.Visible = false;
            else
            {
                m_panelDetailSerie.Visible = true;
                FillDetail(parametre);
            }
        }

        //------------------------------------------------------------------------
        private void FillDetail(CParametreSerieDeChart parametre)
        {
            m_parametreEdite = parametre;
            m_gridProprietes.SelectedObject = parametre;
        }

        //------------------------------------------------------------------------
        private void MajParametreEnCours()
        {
        }

        //------------------------------------------------------------------------
        private void FillListeParametres()
        {
            m_wndListeSeries.BeginUpdate();
            m_wndListeSeries.Items.Clear();
            foreach (CParametreSerieDeChart p in m_chartSetup.Series)
            {
                ListViewItem item = new ListViewItem();
                FillItem(item, p);
                m_wndListeSeries.Items.Add(item);
            }
            m_wndListeSeries.EndUpdate();
        }

        //-----------------------------------------
        private void FillItem(ListViewItem item, CParametreSerieDeChart parametre)
        {
            item.Text = parametre.SerieName;
            item.Tag = parametre;
        }

        //------------------------------------------------------------------------
        private void m_lnkAddSerie_LinkClicked(object sender, EventArgs e)
        {
            CParametreSerieDeChart p = new CParametreSerieDeChart();
            p.SerieName = "S" + (m_wndListeSeries.Items.Count + 1);
            ListViewItem item = new ListViewItem();
            FillItem(item, p);
            m_wndListeSeries.Items.Add(item);
            item.Selected = true;
            MajChamps();
        }

        //------------------------------------------------------------------------
        private void m_wndListeSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_wndListeSeries.SelectedItems.Count > 0)
            {
                ListViewItem item = m_wndListeSeries.SelectedItems[0];
                CParametreSerieDeChart p = item.Tag as CParametreSerieDeChart;
                if (p != null)
                    EditeParametre(p);
            }
            else
                EditeParametre(null);
        }

        //------------------------------------------------------------------------
        public CResultAErreur MajChamps()
        {
            List<CParametreSerieDeChart> lstParametres = new List<CParametreSerieDeChart>();
            foreach (ListViewItem item in m_wndListeSeries.Items)
            {
                CParametreSerieDeChart p = item.Tag as CParametreSerieDeChart;
                if (p != null)
                    lstParametres.Add(p);
            }
            m_chartSetup.Series = lstParametres;
            return CResultAErreur.True;
        }

        //------------------------------------------------------------------------
        private void m_gridProprietes_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "SerieName")
            {
                if (m_wndListeSeries.SelectedItems.Count == 1)
                {
                    ListViewItem item = m_wndListeSeries.SelectedItems[0];
                    CParametreSerieDeChart p = item.Tag as CParametreSerieDeChart;
                    if (p == m_parametreEdite)
                        item.Text = p.SerieName;
                }
            }


            
        }

        private void m_lnkRemoveSerie_LinkClicked(object sender, EventArgs e)
        {
            if (m_wndListeSeries.SelectedItems.Count == 1)
            {
                ListViewItem item = m_wndListeSeries.SelectedItems[0];
                if (CFormAlerte.Afficher(I.T("Delete selected serie ?|20015"),
                    EFormAlerteBoutons.OuiNon,
                    EFormAlerteType.Question) == DialogResult.Yes)
                {
                    m_wndListeSeries.Items.Remove(item);
                    MajChamps();
                }
            }
        }

        //------------------------------------------------------
        private void m_btnUp_Click(object sender, EventArgs e)
        {
            if (m_wndListeSeries.SelectedItems.Count == 1)
            {
                ListViewItem item = m_wndListeSeries.SelectedItems[0];
                int nIndex = item.Index;
                if (nIndex > 0)
                {
                    m_wndListeSeries.Items.Remove(item);
                    m_wndListeSeries.Items.Insert(nIndex - 1, item);
                    MajChamps();
                }
            }
        }

        //------------------------------------------------------
        private void m_btnDown_Click(object sender, EventArgs e)
        {
            if (m_wndListeSeries.SelectedItems.Count == 1)
            {
                ListViewItem item = m_wndListeSeries.SelectedItems[0];
                int nIndex = item.Index;
                if (nIndex < m_wndListeSeries.Items.Count - 1)
                {
                    m_wndListeSeries.Items.Remove(item);
                    m_wndListeSeries.Items.Insert(nIndex + 1, item);
                }
                MajChamps();
            }
        }

        private void m_wndListeSeries_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
            {
                ListViewHitTestInfo info = m_wndListeSeries.HitTest(e.X, e.Y);
                m_menuCopieSerie.Visible = info != null && info.Item != null;
                m_menuSerie.Show(this, new Point(e.X, e.Y));
            }
        }

        //---------------------------------------------------------------------
        private void m_menuCopySeries_Click(object sender, EventArgs e)
        {
            if (m_wndListeSeries.SelectedItems.Count == 1)
            {
                ListViewItem item = m_wndListeSeries.SelectedItems[0];
                CParametreSerieDeChart serie = item.Tag as CParametreSerieDeChart;
                if (serie != null)
                    CSerializerObjetInClipBoard.Copy(serie, typeof(CParametreSerieDeChart).ToString());
            }
        }

        //---------------------------------------------------------------------
        private void m_menuPasteSeries_Click(object sender, EventArgs e)
        {
            I2iSerializable objet = null;
            if (CSerializerObjetInClipBoard.Paste(ref objet, typeof(CParametreSerieDeChart).ToString()))
            {
                CParametreSerieDeChart serie = objet as CParametreSerieDeChart;
                if (serie != null)
                {
                    serie.SerieName += " " + I.T("(copy)|20053");
                    ListViewItem item = new ListViewItem();
                    FillItem(item, serie);
                    m_wndListeSeries.Items.Add(item);
                    item.Selected = true;
                    MajChamps();
                }
            }
        }


    }
}
