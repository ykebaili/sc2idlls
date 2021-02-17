using data.hotel.client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.server
{
    public class CDataRoomOptimizedDataBase
    {
        private Dictionary<string, COptimizedDataRoomTable> m_dicNomTableToTable = new Dictionary<string, COptimizedDataRoomTable>();
        private Dictionary<string, COptimizedDataRoomTable> m_dicIdTableToTable = new Dictionary<string, COptimizedDataRoomTable>();

        //---------------------------------------------------------------
        public CDataRoomOptimizedDataBase ( CDataHotel dataHotel )
        {
            if (dataHotel != null)
            {
                foreach (CDataHotelTable table in dataHotel.Tables)
                {
                    COptimizedDataRoomTable ot = new COptimizedDataRoomTable(table);
                    m_dicNomTableToTable[ot.TableName] = ot;
                    m_dicIdTableToTable[ot.TableId] = ot;
                }
            }
        }

        //---------------------------------------------------------------
        public COptimizedDataRoomTable GetTable ( string strTable )
        {
            COptimizedDataRoomTable table = null;
            if (!m_dicIdTableToTable.TryGetValue(strTable, out table))
                m_dicNomTableToTable.TryGetValue(strTable, out table);
            return table;
        }
    }
}
