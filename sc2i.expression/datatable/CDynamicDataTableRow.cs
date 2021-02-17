using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection.Emit;
using sc2i.common;

namespace sc2i.expression.datatable
{
    [Serializable]
    [DynamicClass("Datatable content row")]
    public class CDynamicDataTableRow
    {
        private DataTable m_table = null;
        private int m_nIndex = 0;
        private DataRow m_row = null;

        public CDynamicDataTableRow(DataTable table, int nIndex)
        {
            m_table = table;
            m_nIndex = nIndex;
        }

        public CDynamicDataTableRow ( DataRow row )
        {
            m_row = row;
            if (row != null)
                m_table = row.Table;
        }


        public DataTable Table
        {
            get
            {
                if (m_row != null)
                    return m_row.Table;
                return m_table;
            }
        }

        public DataRow Row
        {
            get
            {
                DataRow row = null;
                if (m_row != null)
                    row = m_row;
                else
                {
                    if (m_table != null && m_table.Rows.Count > m_nIndex && m_nIndex >= 0 )
                    {
                        row = m_table.Rows[m_nIndex];
                    }
                }
                return row;
            }
        }

        [DynamicMethod("Return desired value", "column name")]
        public object GetValue(string strColonne)
        {
            if (m_table == null)
                return "";
            DataRow row = Row;
            if ( row != null && row.Table != null && row.Table.Columns.Contains ( strColonne ))
            {
                object val = row[strColonne];
                return val == DBNull.Value ? null : val;
            }
            return null;
        }

        [DynamicMethod("Return desired value as string", "column name")]
        public string GetStringValue(string strColonne)
        {
            object val = GetValue(strColonne);
            return val == null ? "" : val.ToString();
        }

        [DynamicMethod("Set value", "column name", "value to set")]
        public object SetValue(string strColonne, object valeur)
        {
            if (m_table == null)
                return null;
            DataRow row = Row;
            if ( row != null && row.Table != null && row.Table.Columns.Contains (strColonne))
            {
                try
                {
                    row[strColonne] = valeur;
                }
                catch { }
            }
            return valeur;
        }
            
    }
}
