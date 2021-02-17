using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.sig
{
    public abstract class CMapItemPoint : IMapItem
    {
        private CMapLayer m_mapLayer = null;
        private string m_strTooltip = "";
        private bool m_bPermanentTooltip = false;
        private double m_fLatitude = 0;
        private double m_fLongitude = 0;
        private object m_tag = null;

        //------------------------------------
        public CMapItemPoint( CMapLayer layer )
        {
            m_mapLayer = layer;
            if ( layer != null )
                layer.AddItem(this);
        }

        //------------------------------------
        public CMapItemPoint(
            CMapLayer layer,
            double fLatitude, 
            double fLongitude)
            :this ( layer )
        {
            m_fLongitude = fLongitude;
            m_fLatitude = fLatitude;
        }

        //------------------------------------
        public CMapLayer Layer
        {
            get
            {
                return m_mapLayer;
            }
        }

        

        //------------------------------------
        public virtual string ToolTip
        {
            get
            {
                return m_strTooltip;
            }
            set
            {
                m_strTooltip = value;
            }
        }

        //------------------------------------
        public virtual bool PermanentToolTip
        {
            get
            {
                return m_bPermanentTooltip;
            }
            set
            {
                m_bPermanentTooltip = value;
            }
        }


        

        //------------------------------------
        public virtual double Latitude
        {
            get
            {
                return m_fLatitude;
            }
            set
            {
                m_fLatitude = value;
            }
        }

        //------------------------------------
        public virtual double Longitude
        {
            get
            {
                return m_fLongitude;
            }
            set
            {
                m_fLongitude = value;
            }
        }

        //------------------------------------
        public object Tag
        {
            get
            {
                return m_tag;
            }
            set
            {
                m_tag = value;
            }
        }

        //------------------------------------
        public event MapItemClickEventHandler MouseClicked;

        //------------------------------------
        public void OnClick()
        {
            if (MouseClicked != null)
                MouseClicked(this);
        }

       
        
    }
}
