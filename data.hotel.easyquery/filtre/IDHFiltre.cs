using data.hotel.client.query;
using futurocom.easyquery;
using sc2i.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.easyquery.filtre
{
    public interface IDHFiltre : I2iSerializable
    {
        ITestDataHotel GetTestFinal(object objetInterroge);

        string GetLibelle(IObjetDeEasyQuery table);
    }
}
