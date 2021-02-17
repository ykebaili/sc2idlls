using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using sc2i.common;

namespace futurocom.easyquery
{
    public class CDataRowForChampCalculeODEQ : IElementARunnableEasyQueryDynamique
    {
        private DataRow m_currentRow = null;
        private DataRow m_previousRow = null;
        private IRunnableEasyQuery m_query = null;

        //---------------------------------------------------------------------------
        public CDataRowForChampCalculeODEQ(DataRow current, DataRow previous, IRunnableEasyQuery query)
        {
            m_currentRow = current;
            m_previousRow = previous;
            m_query = query;
        }

        //---------------------------------------------------------------------------
        public DataRow CurrentRow
        {
            get
            {
                return m_currentRow;
            }
        }

        //---------------------------------------------------------------------------
        [DynamicField("PreviousRow")]
        public DataRow PreviousRow
        {
            get
            {
                return m_previousRow;
            }
        }

        //---------------------------------------------------------------------------
        public static implicit operator DataRow ( CDataRowForChampCalculeODEQ dr )
        {
            return dr.CurrentRow;
        }

        //---------------------------------------------------------------------------
        public bool CanConvertTo(Type tp)
        {
            return tp == typeof(DataRow);            
        }

        //---------------------------------------------------------------------------
        public object ConvertTo(Type tp)
        {
            if (tp == typeof(DataRow))
                return CurrentRow;
            return null;
        }

        public IRunnableEasyQuery GetQuery(string strLibelle)
        {
            return m_query;
        }
    }
}
