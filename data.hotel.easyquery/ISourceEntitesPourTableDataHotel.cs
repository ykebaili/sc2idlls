using futurocom.easyquery;
using sc2i.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.easyquery
{
    public interface ISourceEntitesPourTableDataHotel : I2iSerializable
    {
        IEnumerable<string> GetListeIdsEntites(CEasyQuery query);
    }
}
