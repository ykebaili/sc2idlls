using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET.WindowsForms.Markers;
using sc2i.common.sig;
using GMap.NET;
using sc2i.common;
using GMap.NET.WindowsForms;
using System.Drawing;
using sc2i.common.sig;

namespace sc2i.win32.common.VEarth
{
    [AutoExec("Autoexec")]
    public class CWin32MapItemPath : GMapRoute, IWin32MapItem
    {
        private CMapItemPath m_item = null;
        //------------------------------------------------------------------
        public CWin32MapItemPath(CMapItemPath item)
            : base(GetPointsLatLng ( item.Points ), item.ToolTip )
            
        {
            m_item = item;
            if (item.HasOnClick && item.Points.Count() >= 2)
            {
                IsHitTestVisible = true;
            }
        }

        //------------------------------------------------------------------
        private static List<PointLatLng> GetPointsLatLng ( IEnumerable<SLatLong> lst )
        {
            List<PointLatLng> lstF = new List<PointLatLng>();
            foreach (SLatLong pt in lst)
            {
                lstF.Add(new PointLatLng(pt.Latitude, pt.Longitude));
            }
            return lstF;
        }


        //------------------------------------------------------------------
        public static void Autoexec()
        {
            CAllocateurWin32MapItem.RegisterMarker(typeof(CMapItemPath), typeof(CWin32MapItemPath));
        }

        //------------------------------------------------------------------
        public IMapItem Item
        {
            get
            {
                return m_item;
            }
        }

        //------------------------------------------------------------------
        public void AddToLayer(GMapOverlay layer)
        {
            layer.Routes.Add(this);
        }

        

        //------------------------------------------------------------
        public override void OnRender(Graphics g)
        {
            Pen p = new Pen(m_item.LineColor, m_item.LineWidth);
            Pen oldPen = Stroke;
            Stroke = p;
            base.OnRender(g);
            Stroke = oldPen;
            p.Dispose();
        }

        //-----------------------------------------------------------
        public void DeleteItem()
        {
            if (Overlay != null)
                Overlay.Routes.Remove(this);
        }
    }
}

