using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.server
{
    public class CDataRoomRecord : data.hotel.client.IDataRoomEntry
    {
        private DateTime m_dateTime;
        private double m_fValue;

        //--------------------------------------------------------------------
        public CDataRoomRecord()
        {

        }

        //--------------------------------------------------------------------
        public CDataRoomRecord( DateTime dt, double fValue )
        {
            m_dateTime = dt;
            m_fValue = fValue;
        }

        //--------------------------------------------------------------------
        public DateTime Date
        {
            get
            {
                return m_dateTime;
            }
        }

        //--------------------------------------------------------------------
        public double Value
        {
            get
            {
                return m_fValue;
            }
        }

        //--------------------------------------------------------------------
        public void Write ( BinaryWriter writer )
        {
            byte[] bts = new byte[]{
                (byte)m_dateTime.Hour,
                (byte)m_dateTime.Minute,
                (byte)m_dateTime.Second};
            writer.Write(bts,0, 3);
            writer.Write(m_fValue);
        }

        //--------------------------------------------------------------------
        public void Read ( BinaryReader reader, DateTime dt )
        {
            byte[] bts = new byte[3];
            reader.Read(bts, 0, 3);
            m_dateTime = new DateTime(dt.Year, dt.Month, dt.Day, (int)bts[0], (int)bts[1], (int)bts[2]);
            m_fValue = reader.ReadDouble();
        }
    }
}
