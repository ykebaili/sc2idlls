using System;

namespace data.hotel.client
{
    public interface IDataRoomEntry
    {
        DateTime Date { get; }
        double Value { get; }
    }

    [Serializable]
    public class CDataRoomEntry : IDataRoomEntry
    {
        private DateTime m_dateTime;
        private double m_fValue;

        //--------------------------------------------------------------------
        public CDataRoomEntry()
        {

        }

        //--------------------------------------------------------------------
        public CDataRoomEntry(DateTime dt, double fValue)
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
            set
            {
                m_dateTime = value;
            }
        }

        //--------------------------------------------------------------------
        public double Value
        {
            get
            {
                return m_fValue;
            }
            set
            {
                m_fValue = value;
            }
        }

    }
}
