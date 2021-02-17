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
using futurocom.chart.ChartArea;
using sc2i.win32.common;

namespace futurocom.win32.chart.Areas
{
    public partial class CPanelEditeAreas : UserControl
    {
        private CChartSetup m_chartSetup = null;
        private CChartArea m_editedArea = null;

        //------------------------------------------------------------------------
        public CPanelEditeAreas()
        {
            InitializeComponent();
        }

        //------------------------------------------------------------------------
        private void m_wndListeAreas_SizeChanged(object sender, EventArgs e)
        {
            m_wndListeAreas.Columns[0].Width = m_wndListeAreas.Width - 20;
        }

        //------------------------------------------------------------------------
        public void Init(CChartSetup chart)
        {
            m_chartSetup = chart;
            EditeArea(null);
            FillListeParametres();
        }

        //------------------------------------------------------------------------
        private void EditeArea(CChartArea parametre)
        {
            if (m_editedArea != null)
                MajParametreEnCours();
            m_editedArea = null;
            if (parametre == null)
                m_panelDetailSerie.Visible = false;
            else
            {
                m_panelDetailSerie.Visible = true;
                FillDetail(parametre);
            }
        }

        //------------------------------------------------------------------------
        private void FillDetail(CChartArea parametre)
        {
            m_editedArea = parametre;
            m_gridProprietes.SelectedObject = parametre;
        }

        //------------------------------------------------------------------------
        private void MajParametreEnCours()
        {
        }

        //------------------------------------------------------------------------
        private void FillListeParametres()
        {
            m_wndListeAreas.BeginUpdate();
            m_wndListeAreas.Items.Clear();
            foreach (CChartArea p in m_chartSetup.Areas)
            {
                ListViewItem item = new ListViewItem();
                FillItem(item, p);
                m_wndListeAreas.Items.Add(item);
            }
            m_wndListeAreas.EndUpdate();
        }

        //-----------------------------------------
        private void FillItem(ListViewItem item, CChartArea parametre)
        {
            item.Text = parametre.AreaName;
            item.Tag = parametre;
        }

        //------------------------------------------------------------------------
        private void m_lnkAddSerie_LinkClicked(object sender, EventArgs e)
        {
            CChartArea p = new CChartArea();
            p.AreaName= "Area " + (m_wndListeAreas.Items.Count + 1);
            ListViewItem item = new ListViewItem();
            FillItem(item, p);
            m_wndListeAreas.Items.Add(item);
            item.Selected = true;
            MajChamps();
        }

        //------------------------------------------------------------------------
        private void m_wndListeAreas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_wndListeAreas.SelectedItems.Count > 0)
            {
                ListViewItem item = m_wndListeAreas.SelectedItems[0];
                CChartArea p = item.Tag as CChartArea;
                if (p != null)
                    EditeArea(p);
            }
            else
                EditeArea(null);
        }

        //------------------------------------------------------------------------
        public CResultAErreur MajChamps()
        {
            List<CChartArea> lstParametres = new List<CChartArea>();
            foreach (ListViewItem item in m_wndListeAreas.Items)
            {
                CChartArea p = item.Tag as CChartArea;
                if (p != null)
                    lstParametres.Add(p);
            }
            m_chartSetup.Areas = lstParametres;
            return CResultAErreur.True;
        }

        private void m_gridProprietes_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "AreaName" && m_wndListeAreas.SelectedItems.Count > 0)
            {
                ListViewItem item = m_wndListeAreas.SelectedItems[0];
                if (item.Tag == m_editedArea)
                    item.Text = m_editedArea.AreaName;
            }
                
        }

        private void m_lnkRemoveSerie_LinkClicked(object sender, EventArgs e)
        {
            if (m_wndListeAreas.SelectedItems.Count == 1)
            {
                ListViewItem item = m_wndListeAreas.SelectedItems[0];
                if (CFormAlerte.Afficher(I.T("Delete selected area ?|20016"),
                    EFormAlerteBoutons.OuiNon,
                    EFormAlerteType.Question) == DialogResult.Yes)
                {
                    m_wndListeAreas.Items.Remove(item);
                    MajChamps();
                }
            }
        }

        private void m_wndListeAreas_MouseUp(object sender, MouseEventArgs e)
        {
            if ( (e.Button & MouseButtons.Right)== MouseButtons.Right)
            {
                ListViewHitTestInfo info = m_wndListeAreas.HitTest (e.X, e.Y);
                m_menuCopyArea.Visible = info != null && info.Item!= null;
                m_menuArea.Show ( this, new Point ( e.X, e.Y ));
            }
        }

        //---------------------------------------------------------------------
        private void m_menuCopyArea_Click(object sender, EventArgs e)
        {
            if ( m_wndListeAreas.SelectedItems.Count  == 1 )
            {
                ListViewItem item = m_wndListeAreas.SelectedItems[0];
                CChartArea area = item.Tag as CChartArea;
                if (area != null)
                    CSerializerObjetInClipBoard.Copy(area, typeof(CChartArea).ToString());
            }
        }

        //---------------------------------------------------------------------
        private void m_menuPasteArea_Click(object sender, EventArgs e)
        {
            I2iSerializable objet = null;
            if ( CSerializerObjetInClipBoard.Paste ( ref objet, typeof(CChartArea).ToString()) )
            {
                CChartArea area = objet as CChartArea;
                if (area != null)
                {
                    area.AreaName += " " + I.T("(copy)|20053");
                    ListViewItem item = new ListViewItem();
                    FillItem(item, area);
                    m_wndListeAreas.Items.Add(item);
                    item.Selected = true;
                    MajChamps();
                }
            }
        }


    }
}
