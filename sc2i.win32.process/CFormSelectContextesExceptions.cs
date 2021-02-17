using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.common;
using sc2i.win32.common;

namespace sc2i.win32.process
{
    public partial class CFormSelectContextesExceptions : Form
    {
        private HashSet<string> m_contextes = new HashSet<string>();
        public CFormSelectContextesExceptions()
        {
            InitializeComponent();
        }

        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void m_btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            DataTable table = m_gridExceptions.DataSource as DataTable;
            m_contextes = new HashSet<string>();
            if (table != null)
            {
                
                foreach (DataRow row in table.Rows)
                    m_contextes.Add(row[0].ToString());
            }
            Close();
        }

        private void CFormSelectContextesExceptions_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
            DataTable table = new DataTable("EXCEPTIONS");
            table.Columns.Add("TEXT");
            foreach (string strException in m_contextes)
            {
                DataRow row = table.NewRow();
                row[0] = strException;
                table.Rows.Add(row);
            }
            if (m_gridExceptions.TableStyles[table.TableName] == null)
            {
                DataGridTableStyle tableStyle = new DataGridTableStyle();
                tableStyle.MappingName = table.TableName;

                DataGridTextBoxColumn colStyleValue = new DataGridTextBoxColumn();
                colStyleValue.MappingName = "TEXT";
                colStyleValue.HeaderText = I.T("Exceptions|20031");
                colStyleValue.Width = m_gridExceptions.Width - 30;
                colStyleValue.ReadOnly = false;

                tableStyle.RowHeadersVisible = true;

                tableStyle.GridColumnStyles.AddRange(new DataGridColumnStyle[] { colStyleValue });

                m_gridExceptions.TableStyles.Add(tableStyle);
            }

            table.DefaultView.AllowNew = true;
            table.DefaultView.AllowDelete = true;

            m_gridExceptions.DataSource = table;
        }

        public static HashSet<string> EditeContextes ( HashSet<string> contextes )
        {
            CFormSelectContextesExceptions form = new CFormSelectContextesExceptions();
            form.m_contextes = new HashSet<string>(contextes);
            if (form.ShowDialog() == DialogResult.OK)
                contextes = form.m_contextes;
            form.Dispose();
            return contextes;
        }
    }
}