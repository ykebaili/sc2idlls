using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.formulaire.datagrid;

namespace sc2i.formulaire.win32.datagrid
{
    
    public partial class CFormFiltreDataGrid : sc2i.win32.common.CFloatingFormBase
    {
        private int m_nColumnIndex = -1;
        public delegate void OnFiltrerDataGridEventHandler(int nCol, string strFiltre);
        public CFormFiltreDataGrid()
        {
            InitializeComponent();
        }

        private OnFiltrerDataGridEventHandler m_handler;

        private void CFormFiltreDataGrid_Load(object sender, EventArgs e)
        {
            panel1.Height = m_txtFiltre.Height + m_panelTop.Height + 2;
            Height = panel1.Height;
            m_txtFiltre.Focus();
        }

        public static void Show(
            C2iWndDataGridColumn col, 
            int nColumnIndex,
            string strText, 
            int nWidth,
            OnFiltrerDataGridEventHandler handler)
        {
            CFormFiltreDataGrid form = new CFormFiltreDataGrid();
            form.m_txtFiltre.Text = strText;
            form.m_lblNomCol.BackColor = col.BackColor;
            form.m_lblNomCol.Text = col.Text;
            form.m_handler = handler;
            form.m_nColumnIndex = nColumnIndex;
            form.Show();
        }



        private void m_txtFiltre_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if (m_handler != null)
                {
                    m_handler(m_nColumnIndex, m_txtFiltre.Text);
                    e.Handled = true;
                    Hide();
                }
        }

    }
}
