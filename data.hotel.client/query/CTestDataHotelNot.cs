﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.client.query
{
    [Serializable]
    public class CTestDataHotelNot : CTestDataHotelASousElements
    {
        //-------------------------------------------------------------------
        public CTestDataHotelNot()
        {

        }


        //-------------------------------------------------------------------
        public override bool IsInFilter(string strChamp, IDataRoomEntry record)
        {
            foreach (ITestDataHotel test in SousTests)
            {
                if (test != null && test.IsInFilter(strChamp, record))
                    return false;
            }
            return true;
        }
        
    }
}
