using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.server
{
    public class CEntityLastValue
    {
        private string m_strIdEntite;
        private DateTime m_date;
        private double m_fLastValue;

        //---------------------------------------------
        public CEntityLastValue()
        {

        }

        //---------------------------------------------
        public string IdEntite
        {
            get
            {
                return m_strIdEntite;
            }
            set
            {
                m_strIdEntite = value;
            }
        }

        //---------------------------------------------
        public DateTime Date
        {
            get
            {
                return m_date;
            }
            set
            {
                m_date = value;
            }
        }

        //---------------------------------------------
        public double Value
        {
            get
            {
                return m_fLastValue;
            }
            set
            {
                m_fLastValue = value;
            }
        }

        //---------------------------------------------
    }
    public class CEntityLastValues
    {
        
    }
}
