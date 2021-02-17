using data.hotel.client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.server
{
    public static class CAddServerFunctionsOnRoomEntry
    {
        //--------------------------------------------------------------------
        public static void Write ( this CDataRoomEntry entry, BinaryWriter writer )
        {
            DateTime dt = entry.Date;
            byte[] bts = new byte[]{
                (byte)dt.Hour,
                (byte)dt.Minute,
                (byte)dt.Second};
            writer.Write(bts,0, 3);
            writer.Write(entry.Value);
        }

        
        //--------------------------------------------------------------------
        public static void Read(this CDataRoomEntry entry, BinaryReader reader, DateTime dt)
        {
            byte[] bts = new byte[3];
            reader.Read(bts, 0, 3);
            entry.Date =new DateTime(dt.Year, dt.Month, dt.Day, (int)bts[0], (int)bts[1], (int)bts[2]);
            entry.Value = reader.ReadDouble();
        }
    }
}
