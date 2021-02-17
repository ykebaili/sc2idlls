using data.hotel.client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.server
{
    public class COptimizedDataRoomTable
    {
        private string m_strTableId;
        private string m_strTableName;

        private Dictionary<string, string> m_dicFieldNameToFieldId = new Dictionary<string, string>();
        private Dictionary<string, string> m_dicFieldIdToFieldName = new Dictionary<string, string>();

        //-------------------------------------------------------
        public COptimizedDataRoomTable ( CDataHotelTable table )
        {
            m_strTableId = table.Id;
            m_strTableName = table.TableName;
            foreach ( CDataHotelField field in table.Fields )
            {
                m_dicFieldIdToFieldName[field.Id] = field.FieldName;
                m_dicFieldNameToFieldId[field.FieldName] = field.Id;
            }
        }

        //-------------------------------------------------------
        public string TableName
        {
            get
            {
                return m_strTableName;
            }
        }

        //-------------------------------------------------------
        public string TableId
        {
            get
            {
                return m_strTableId;
            }
        }

        //-------------------------------------------------------
        public string GetFieldId ( string strField )
        {
            if ( m_dicFieldIdToFieldName.ContainsKey(strField))
                return strField;
            string strId = null;
            if ( m_dicFieldNameToFieldId.TryGetValue (strField, out strId ))
                return strId;
            return null;
        }
    }
}
