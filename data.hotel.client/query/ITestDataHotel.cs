using sc2i.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.client.query
{
    public interface ITestDataHotel
    {
        bool IsInFilter(string strChamp, IDataRoomEntry record);

        void ReplaceColumnId(string strOldId, string strNewId);
    }
}
