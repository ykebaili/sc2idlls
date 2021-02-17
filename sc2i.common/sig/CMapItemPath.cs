using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace sc2i.common.sig
{
    public class CMapItemPath : IMapItem
    {
        private CMapLayer m_mapLayer = null;
        private string m_strTooltip = "";
        private bool m_bPermanentTooltip = false;
        private List<SLatLong> m_listePoints = new List<SLatLong>();
        private Color m_lineColor = Color.Green;
        private int m_lineWidth = 2;
        private object m_tag = null;
        private bool m_bDetectClick = false;

        private int m_nNumero = 0;

        private static int m_nCompteur = 0;

        //------------------------------------
        public CMapItemPath( CMapLayer layer )
        {
            m_mapLayer = layer;
            if ( layer != null )
                layer.AddItem(this);
            m_nNumero = m_nCompteur++;
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
        public IEnumerable<SLatLong> Points
        {
            get
            {
                return m_listePoints.AsReadOnly();
            }
            set
            {
                m_listePoints.Clear();
                if (value != null)
                {
                    foreach (SLatLong pt in value)
                        m_listePoints.Add(pt);
                }
            }
        }
        //------------------------------------
        public string ToolTip
        {
            get
            {
                return m_strTooltip;
            }
            set
            {
                m_strTooltip = "";
            }
        }

        //------------------------------------
        public bool PermanentToolTip
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
        public int LineWidth
        {
            get
            {
                return m_lineWidth;
            }
            set
            {
                m_lineWidth = value;
            }
        }

        //------------------------------------
        public Color LineColor
        {
            get
            {
                return m_lineColor;
            }
            set
            {
                m_lineColor = value;
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
        public bool EnableClick
        {
            get
            {
                return m_bDetectClick;
            }
            set
            {
                m_bDetectClick = value;
            }
        }

        //------------------------------------
        public bool HasOnClick
        {
            get
            {
                return MouseClicked != null || EnableClick;
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
