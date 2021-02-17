using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.common;

namespace futurocom.win32.easyquery
{
    public partial class CFormVisuTable : Form
    {
        private DataTable m_table = null;
        public CFormVisuTable()
        {
            InitializeComponent();
        }

        public static void ShowTable(DataTable table)
        {
            CFormVisuTable form = new CFormVisuTable();
            form.m_grid.DataSource = table;
            form.m_table = table;
            form.ShowDialog();
            form.Dispose();
        }

        private void CFormVisuTable_Load(object sender, EventArgs e)
        {
            sc2i.win32.common.CWin32Traducteur.Translate(this);
            if (m_table != null)
            {
                m_lblNbRecs.Text = I.T("@1 records|20057", m_table.Rows.Count.ToString());
            }
        }

        private void m_btnCopy_Click(object sender, EventArgs e)
        {
            StringBuilder bl = new StringBuilder();
            foreach ( DataColumn col in m_table.Columns )
            {
                bl.Append(col.ColumnName);
                bl.Append("\t");
            }
            bl.Append(Environment.NewLine);
            foreach (DataRow row in m_table.Rows)
            {
                foreach (DataColumn col in m_table.Columns)
                {
                    object val = row[col];
                    if (val != DBNull.Value)
                        bl.Append(val.ToString());
                    else
                        bl.Append("");
                    bl.Append("\t");
                }
                bl.Append(Environment.NewLine);
            }
            Clipboard.SetText(bl.ToString());
        }
    }
}
