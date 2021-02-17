using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.client.query
{
    [Serializable]
    public abstract class CTestDataHotelASousElements : ITestDataHotel
    {
        private List<ITestDataHotel> m_listeSousTests = new List<ITestDataHotel>();

        //-------------------------------------------------------------------------
        public CTestDataHotelASousElements()
        {
        }

        //-------------------------------------------------------------------
        public List<ITestDataHotel> SousTests
        {
            get
            {
                return m_listeSousTests;
            }
            set
            {
                List<ITestDataHotel> lst = new List<ITestDataHotel>();
                if (value != null)
                    lst.AddRange(value);
                m_listeSousTests = lst;
            }
        }

        public abstract bool IsInFilter(string strChamp, IDataRoomEntry record);


        //-------------------------------------------------------------------
        public void ReplaceColumnId(string strOldId, string strNewId)
        {
            foreach (ITestDataHotel test in SousTests)
                test.ReplaceColumnId(strOldId, strNewId);
        }
    }
}
