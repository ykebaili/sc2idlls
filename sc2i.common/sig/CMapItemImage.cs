using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace sc2i.common.sig
{
    public class CMapItemImage : CMapItemPoint
    {
        private string m_strImageId = "";

        //------------------------------------
        public CMapItemImage( CMapLayer layer )
            :base ( layer )
        {
        }

        //------------------------------------
        public CMapItemImage(
            CMapLayer layer,
            double fLatitude, 
            double fLongitude,
            string strImageId)
            :base ( layer, fLatitude, fLongitude)
        {
            m_strImageId = strImageId;
        }

        

        //------------------------------------
        public string ImageId
        {
            get
            {
                return m_strImageId;
            }
            set
            {
                m_strImageId = value;
            }
        }

        //------------------------------------
        public Bitmap Image
        {
            get
            {
                if (Layer != null && Layer.Database != null)
                    return Layer.Database.GetImage(m_strImageId);
                return null;
            }
        }

        //------------------------------------
        public void OnClick()
        {
        }
    }
}
