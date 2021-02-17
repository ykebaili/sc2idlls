using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.sig
{
    public class CMapItemSimple : CMapItemPoint
    {
        private EMapMarkerType m_markerType = EMapMarkerType.green;

        //------------------------------------
        public CMapItemSimple( CMapLayer layer )
            :base ( layer )
        {
        }

        //------------------------------------
        public CMapItemSimple(
            CMapLayer layer,
            double fLatitude, 
            double fLongitude, 
            EMapMarkerType type)
            :base ( layer, fLatitude, fLongitude)
        {
            m_markerType = type;
        }

        //------------------------------------
        public EMapMarkerType SimpleMarkerType
        {
            get
            {
                return m_markerType;
            }
            set
            {
                m_markerType = value;
            }
        }

        //------------------------------------
        public void OnClick()
        {
        }
    }
}
