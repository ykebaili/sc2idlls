using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET.WindowsForms.Markers;
using sc2i.common.sig;
using GMap.NET;
using sc2i.common.sig;
using GMap.NET.WindowsForms;
using sc2i.common;

namespace sc2i.win32.common.VEarth
{
    [AutoExec("Autoexec")]
    public class CWin32MapItemGoogleStd : GMarkerGoogle, IWin32MapItem
    {
        private IMapItem m_item = null;
        //------------------------------------------------------------------
        public CWin32MapItemGoogleStd(CMapItemSimple item)
            : base(new PointLatLng(item.Latitude, item.Longitude), 
            GetGoogleType(item.SimpleMarkerType))
        {
            m_item = item;
            
            ToolTipText = item.ToolTip;
            if (!item.PermanentToolTip)
                ToolTipMode = MarkerTooltipMode.OnMouseOver;
            else
                ToolTipMode = MarkerTooltipMode.Always;
        }

        //------------------------------------------------------------------
        public static void Autoexec()
        {
            CAllocateurWin32MapItem.RegisterMarker(typeof(CMapItemSimple), typeof(CWin32MapItemGoogleStd));
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
            layer.Markers.Add(this);
        }

        //------------------------------------------------------------------
        public static GMarkerGoogleType GetGoogleType(EMapMarkerType original)
        {
            switch (original)
            {
                case EMapMarkerType.none:
                    return GMarkerGoogleType.none;
                case EMapMarkerType.arrow:
                    return GMarkerGoogleType.arrow;

                case EMapMarkerType.blue:
                    return GMarkerGoogleType.blue;

                case EMapMarkerType.blue_small:
                    return GMarkerGoogleType.blue_small;

                case EMapMarkerType.blue_dot:
                    return GMarkerGoogleType.blue_dot;
                case EMapMarkerType.blue_pushpin:
                    return GMarkerGoogleType.blue_pushpin;
                case EMapMarkerType.brown_small:
                    return GMarkerGoogleType.brown_small;
                case EMapMarkerType.gray_small:
                    return GMarkerGoogleType.gray_small;
                case EMapMarkerType.green:
                    return GMarkerGoogleType.green;
                case EMapMarkerType.green_small:
                    return GMarkerGoogleType.green_small;
                case EMapMarkerType.green_dot:
                    return GMarkerGoogleType.green_dot;
                case EMapMarkerType.green_pushpin:
                    return GMarkerGoogleType.green_pushpin;
                case EMapMarkerType.green_big_go:
                    return GMarkerGoogleType.green_big_go;
                case EMapMarkerType.yellow:
                    return GMarkerGoogleType.yellow;
                case EMapMarkerType.yellow_small:
                    return GMarkerGoogleType.yellow_small;
                case EMapMarkerType.yellow_dot:
                    return GMarkerGoogleType.yellow_dot;
                case EMapMarkerType.yellow_big_pause:
                    return GMarkerGoogleType.yellow_big_pause;
                case EMapMarkerType.yellow_pushpin:
                    return GMarkerGoogleType.yellow_pushpin;
                case EMapMarkerType.lightblue:
                    return GMarkerGoogleType.lightblue;
                case EMapMarkerType.lightblue_dot:
                    return GMarkerGoogleType.lightblue_dot;
                case EMapMarkerType.lightblue_pushpin:
                    return GMarkerGoogleType.lightblue_pushpin;
                case EMapMarkerType.orange:
                    return GMarkerGoogleType.orange;
                case EMapMarkerType.orange_small:
                    return GMarkerGoogleType.orange_small;
                case EMapMarkerType.orange_dot:
                    return GMarkerGoogleType.orange_dot;
                case EMapMarkerType.pink:
                    return GMarkerGoogleType.pink;
                case EMapMarkerType.pink_dot:
                    return GMarkerGoogleType.pink_dot;
                case EMapMarkerType.pink_pushpin:
                    return GMarkerGoogleType.pink_pushpin;
                case EMapMarkerType.purple:
                    return GMarkerGoogleType.purple;
                case EMapMarkerType.purple_small:
                    return GMarkerGoogleType.purple_small;
                case EMapMarkerType.purple_dot:
                    return GMarkerGoogleType.purple_dot;
                case EMapMarkerType.purple_pushpin:
                    return GMarkerGoogleType.purple_pushpin;
                case EMapMarkerType.red:
                    return GMarkerGoogleType.red;
                case EMapMarkerType.red_small:
                    return GMarkerGoogleType.red_small;
                case EMapMarkerType.red_dot:
                    return GMarkerGoogleType.red_dot;
                case EMapMarkerType.red_pushpin:
                    return GMarkerGoogleType.red_pushpin;
                case EMapMarkerType.red_big_stop:
                    return GMarkerGoogleType.red_big_stop;
                case EMapMarkerType.black_small:
                    return GMarkerGoogleType.black_small;
                case EMapMarkerType.white_small:
                    return GMarkerGoogleType.white_small;
            }
            return GMarkerGoogleType.green;
        }

        //-----------------------------------------------------------
        public void DeleteItem()
        {
            if (Overlay != null)
                Overlay.Markers.Remove(this);
        }
    }
}

