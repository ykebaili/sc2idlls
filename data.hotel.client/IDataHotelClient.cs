using System;
namespace data.hotel.client
{
    public interface IDataHotelClient
    {
        IDataRoomServer GetRoomServer();
        IDataRoomServer GetRoomServerProxy();
        void Init(string strHotelURL);
    }

    public interface IDataHotelClientAllocator
    {
        IDataHotelClient GetNewClient();
    }

    public class CDataHotelClientAllocator : MarshalByRefObject, IDataHotelClientAllocator
    {
        public IDataHotelClient GetNewClient()
        {
            return new CDataHotelClient();
        }
    }
}
