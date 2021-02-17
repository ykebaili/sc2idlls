using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.sig
{
    public struct SLatLong
    {
        public double Latitude;
        public double Longitude;

        public SLatLong(double fLatitude, double fLongitude)
        {
            Latitude = fLatitude;
            Longitude = fLongitude;
        }
    }
}
