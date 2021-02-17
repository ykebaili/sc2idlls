using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using sc2i.common;

namespace sc2i.expression.datatable
{
    [Serializable]
    [DynamicClass("DataTable content")]
    public class CDynamicDataTable
    {
        private DataTable m_table = null;

        public CDynamicDataTable(DataTable table)
        {
            m_table = table;
        }


        public DataTable GetTable()
        {
            return m_table;
        }

        [DynamicField("Rows")]
        public CDynamicDataTableRow[] Rows
        {
            get
            {
                List<CDynamicDataTableRow> lst = new List<CDynamicDataTableRow>();
                if (m_table != null)
                    for (int n = 0; n < m_table.Rows.Count; n++)
                        lst.Add(new CDynamicDataTableRow(m_table, n));
                return lst.ToArray();
            }
        }

        [DynamicMethod("Filter rows", "Filter (RowFilter syntax)")]
        public CDynamicDataTableRow[] SelectRows(string strFilter)
        {
            List<CDynamicDataTableRow> lst = new List<CDynamicDataTableRow>();
            if (m_table != null)
            {
                try
                {
                    DataRow[] rows = m_table.Select(strFilter);
                    foreach (DataRow row in rows)
                    {
                        lst.Add(new CDynamicDataTableRow(row));
                    }
                }
                catch { }
            }
            return lst.ToArray();
        }


      
    }
}
