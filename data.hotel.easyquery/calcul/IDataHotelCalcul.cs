using data.hotel.client.query;
using sc2i.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.easyquery.calcul
{
    public interface IDataHotelCalcul : I2iSerializable
    {
        IChampHotelCalcule GetChampHotel(object objetInterroge);

        Type TypeFinal { get; }
    }
}
