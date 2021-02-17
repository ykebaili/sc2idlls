using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace sc2i.common.memorydb
{
    [Serializable]
    public class CMemoryDataRow
    {
        private DataRow m_row = null;

        public CMemoryDataRow(DataRow row)
        {
            m_row = row;
        }

        public DataRow Row
        {
            get
            {
                return m_row;
            }
        }

        public DataTable Table
        {
            get
            {
                return m_row.Table;
            }
        }


        public object this[string strColonne]
        {
            get
            {
                object val = m_row[strColonne];
                if (val == DBNull.Value)
                    val = null;
                return val;
            }
            set
            {
                if (value == null)
                    m_row[strColonne] = DBNull.Value;
                else
                    m_row[strColonne] = value;
            }
        }

        public T Get<T>(string strColonne)
        {
            object v = this[strColonne];
            if (v == null)
                return default(T);
            return (T)v;
        }

        
    }
}
