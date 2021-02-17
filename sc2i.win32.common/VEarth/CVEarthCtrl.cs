using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GMap.NET.WindowsForms;
using GMap.NET;
using GMap.NET.WindowsForms.Markers;
using sc2i.common.sig;
using sc2i.win32.common.VEarth;
using sc2i.win32.common.Properties;
using sc2i.common.unites;

namespace sc2i.win32.common
{


    public partial class CVEarthCtrl : UserControl
    {
        private class CPointEarth
        {
            public double Longitude;
            public double Latitude;
            public string Libelle;
            public string Layer;
            public Bitmap Bitmap;

            public CPointEarth(
                double fLongitude,
                double fLatitude,
                string strLibelle,
                string strLayer)
            {
                Longitude = fLongitude;
                Latitude = fLatitude;
                Libelle = strLibelle;
                Layer = strLayer;
                Bitmap = null;
            }

            public CPointEarth(
                double fLongitude,
                double fLatitude,
                string strLibelle,
                string strLayer,
                Bitmap bmp)
            {
                Longitude = fLongitude;
                Latitude = fLatitude;
                Libelle = strLibelle;
                Layer = strLayer;
                Bitmap = bmp;
            }
        }

        private List<CPointEarth> m_listePointsMarques = new List<CPointEarth>();
        private GMapOverlay m_layerForDynamicsElements;
        private Dictionary<string, GMapOverlay> m_dicLayers = new Dictionary<string, GMapOverlay>();
        private double m_fCentreLat;
        private double m_fCentreLong;
        private double m_fZoom = 10;
        private EMapStyle m_mapStyle = EMapStyle.Road;

        public event EventHandler OnEarthChangeCenter;
        public event EventHandler OnEarthChangeZoom;

        public event EarthMouseEventHandler OnEarthMouseUp;
        public event EarthMouseEventHandler OnEarthMouseDown;
        public event EarthMouseEventHandler OnEarthMouseMove;

        private bool m_bShowMarkerOnClick = false;

        private GMarkerGoogle m_markerMouse = null;

        public const string c_strDefaultOverlay = "~~DYNAMIC_ELEMENTS";

        private List<IWin32MapItem> m_listeWin32Items = new List<IWin32MapItem>();

        private List<IWin32MapItem> m_listeElementsSelectionnes = new List<IWin32MapItem>();

        private bool m_bTraitementParDefautMouseUp = true;
        private bool m_bPreventClickOnItem = false;

        public CVEarthCtrl()
        {
            InitializeComponent();
            m_layerForDynamicsElements = new GMapOverlay(c_strDefaultOverlay);
            m_gMap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            m_gMap.Manager.Mode = AccessMode.ServerAndCache;
            m_gMap.Overlays.Add(m_layerForDynamicsElements);
            m_gMap.Position = new PointLatLng(43.75984, 1.738281);
            MapStyle = EMapStyle.Road;
            m_markerMouse = new GMarkerGoogle(new PointLatLng(0, 0), Resources.MarkerMouse);
            m_markerMouse.ToolTipMode = MarkerTooltipMode.OnMouseOver;
        }

        //------------------------------------
        public GMapControl MapControl
        {
            get
            {
                return m_gMap;
            }
        }


        //------------------------------------
        public bool ShowMarkerOnClick
        {
            get
            {
                return m_bShowMarkerOnClick;
            }
            set
            {
                m_bShowMarkerOnClick = value;
            }
        }

        //------------------------------------
        public double CenterLatitude
        {
            get
            {
                return m_fCentreLat;
            }
            set
            {
                m_fCentreLat = value;
                if (OnEarthChangeCenter != null)
                    OnEarthChangeCenter(this, null);
            }
        }

        //------------------------------------
        public double CenterLongitude
        {
            get
            {
                return m_fCentreLong;
            }
            set
            {
                m_fCentreLong = value;
                if (OnEarthChangeCenter != null)
                    OnEarthChangeCenter(this, null);
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            try
            {
                base.OnMouseClick(e);
            }
            catch { }
        }

        //------------------------------------
        public void AutoZoomAndCenter()
        {
            double? fMinLat = null;
            double? fMinLong = null;
            double? fMaxLat = null;
            double? fMaxLong = null;
            foreach (GMapOverlay layer in m_dicLayers.Values)
            {
                foreach (GMapMarker marker in layer.Markers)
                {
                    if (fMinLat == null || marker.Position.Lat < fMinLat.Value)
                        fMinLat = marker.Position.Lat;
                    if (fMinLong == null || marker.Position.Lng < fMinLong.Value)
                        fMinLong = marker.Position.Lng;
                    if (fMaxLat == null || marker.Position.Lat > fMaxLat.Value)
                        fMaxLat = marker.Position.Lat;
                    if (fMaxLong == null || marker.Position.Lng > fMaxLong.Value)
                        fMaxLong = marker.Position.Lng;
                }
                foreach (GMapRoute route in layer.Routes)
                {
                    foreach (PointLatLng pt in route.Points)
                    {
                        if (fMinLat == null || pt.Lat < fMinLat.Value)
                            fMinLat = pt.Lat;
                        if (fMinLong == null || pt.Lng < fMinLong.Value)
                            fMinLong = pt.Lng;
                        if (fMaxLat == null || pt.Lat > fMaxLat.Value)
                            fMaxLat = pt.Lat;
                        if (fMaxLong == null || pt.Lng > fMaxLong.Value)
                            fMaxLong = pt.Lng;
                    }
                }
            }
            if (fMinLat != null && fMaxLat != null && fMinLong != null && fMaxLong != null)
            {
                using (GMapRoute route = new GMapRoute(new PointLatLng[]{
                    new PointLatLng(fMinLat.Value, fMinLong.Value),
                    new PointLatLng(fMinLat.Value, fMaxLong.Value),
                    new PointLatLng(fMaxLat.Value, fMaxLong.Value),
                    new PointLatLng(fMaxLat.Value, fMinLong.Value)}, ""))
                {
                    /* CenterLatitude = (fMinLat.Value + fMaxLat.Value) / 2;
                     CenterLongitude = (fMinLong.Value + fMaxLong.Value)/2;
                     Zoom = */
                    m_gMap.ZoomAndCenterRoute(route);
                    m_fCentreLat = m_gMap.Position.Lat;
                    m_fCentreLong = m_gMap.Position.Lng;
                    m_fZoom = m_gMap.Zoom;
                }
            }
        }

        //------------------------------------
        public GMapOverlay GetLayer(string strLayer)
        {
            return GetLayer(strLayer, false);
        }

        //------------------------------------
        public GMapOverlay GetLayer(string strLayer, bool bCreateIfNull)
        {
            if (strLayer == null)
                strLayer = "";
            GMapOverlay overlay = null;
            if (bCreateIfNull)
            {
                if (!m_dicLayers.TryGetValue(strLayer.ToUpper(), out overlay))
                {
                    overlay = new GMapOverlay(strLayer.ToUpper());
                    m_gMap.Overlays.Add(overlay);
                    m_dicLayers.Add(strLayer, overlay);
                }
            }
            return overlay;
        }

        //------------------------------------
        /// <summary>
        /// Obsolète, pour compatiblilté
        /// </summary>
        public void DisplayCarte()
        {
        }

        //------------------------------------
        public double Zoom
        {
            get
            {
                return m_fZoom;
            }
            set
            {
                //if (value != m_fZoom)
                {
                    m_fZoom = value;
                    m_gMap.Zoom = m_fZoom;
                    m_trackZoom.Value = (int)m_fZoom;
                    if (OnEarthChangeZoom != null)
                        OnEarthChangeZoom(this, null);
                }

            }
        }

        //------------------------------------
        public void RefreshPosition()
        {
            m_gMap.Position = new GMap.NET.PointLatLng(m_fCentreLat, m_fCentreLong);
            m_gMap.Zoom = m_fZoom;
        }

        //------------------------------------
        public event EventHandler OnEarthChangeMapStyle;
        public EMapStyle MapStyle
        {
            get
            {
                return m_mapStyle;
            }
            set
            {
                if (value != m_mapStyle)
                {
                    m_mapStyle = value;
                    switch (value)
                    {
                        case EMapStyle.Road:
                            m_gMap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
                            m_btnMap.Checked = true;
                            break;
                        case EMapStyle.Aerial:
                            m_gMap.MapProvider = GMap.NET.MapProviders.GoogleSatelliteMapProvider.Instance;
                            m_btnSat.Checked = true;
                            break;
                        case EMapStyle.Hybrid:
                            m_gMap.MapProvider = GMap.NET.MapProviders.GoogleHybridMapProvider.Instance;
                            m_btnHybrid.Checked = true;
                            break;
                        default:
                            break;
                    }
                    if (OnEarthChangeMapStyle != null)
                        OnEarthChangeMapStyle(this, null);
                }
            }
        }

        //------------------------------------
        public string Mark(double fLatitude, double fLongitude, string strLabel, string strLayer)
        {
            m_listePointsMarques.Add(new CPointEarth(
                fLongitude, fLatitude, strLabel, strLayer));

            GMapMarker marker = new GMarkerGoogle(new PointLatLng(fLatitude, fLongitude), GMarkerGoogleType.green);
            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            marker.ToolTipText = strLabel;
            GetLayer(c_strDefaultOverlay, true).Markers.Add(marker);
            return null;
        }

        //------------------------------------
        public string Mark(double fLatitude, double fLongitude, string strLabel, string strLayer, Bitmap image)
        {
            m_listePointsMarques.Add(new CPointEarth(
                fLongitude, fLatitude, strLabel, strLayer, image));

            GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(fLatitude, fLongitude), image);
            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            marker.ToolTipText = strLabel;
            GetLayer(c_strDefaultOverlay, true).Markers.Add(marker);
            return null;
        }



        //------------------------------------
        public string Line(
            double fLat1, double fLong1,
            double fLat2, double fLong2,
            Color couleur,
            string strLayer)
        {
            List<PointLatLng> lstPts = new List<PointLatLng>();
            lstPts.Add(new PointLatLng(fLat1, fLong1));
            lstPts.Add(new PointLatLng(fLat2, fLong2));
            GMapPolygon polygone = new GMapPolygon(lstPts, "");
            GetLayer(c_strDefaultOverlay, true).Polygons.Add(polygone);
            return "";
        }

        //------------------------------------
        public void ClearLayer(string strLayer)
        {
            m_listePointsMarques.Clear();
            GetLayer(strLayer, true).Polygons.Clear();
            GetLayer(strLayer, true).Markers.Clear();
        }


        //-----------------------------------------------------------
        private void m_gMap_MouseDown(object sender, MouseEventArgs e)
        {
            m_bTraitementParDefautMouseUp = true;
            m_bPreventClickOnItem = false;
            if (OnEarthMouseDown != null)
            {
                EarthMouseEventArgs args = new EarthMouseEventArgs(m_gMap, e);
                OnEarthMouseDown(sender, args);
                if (args.IsProcessed)
                {
                    m_bPreventClickOnItem = true;
                    return;
                }
            }

        }


        //-----------------------------------------------------------
        private void m_gMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (OnEarthMouseMove != null)
            {
                EarthMouseEventArgs args = new EarthMouseEventArgs(m_gMap, e);
                OnEarthMouseMove(sender, args);
                if (args.IsProcessed)
                    return;
            }
        }


        //-----------------------------------------------------------
        private void m_gMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (!m_bTraitementParDefautMouseUp)//On a demandé à ne pas traiter le mouseup
                return;

            PointLatLng pt = m_gMap.FromLocalToLatLng(e.X, e.Y);

            if (OnEarthMouseUp != null)
            {
                EarthMouseEventArgs args = new EarthMouseEventArgs(m_gMap, e);
                OnEarthMouseUp(this, args);
                if (args.IsProcessed)
                    return;
            }
            if (m_bShowMarkerOnClick && (e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                ShowMouseMarker(pt.Lat, pt.Lng);
                /*GMapOverlay ov = GetLayer(c_strDefaultOverlay, true);
                m_markerMouse.Position = new PointLatLng(pt.Lat, pt.Lng);
                m_markerMouse.ToolTipText = "Lat : " + pt.Lat.ToString() +
                    Environment.NewLine +
                    "Long : " + pt.Lng.ToString();
                if (!ov.Markers.Contains(m_markerMouse))
                {
                    ov.Markers.Add(m_markerMouse);
                }*/
            }

        }

        //------------------------------------
        public void ShowMouseMarker(double fLatitude, double fLongitude)
        {
            GMapOverlay ov = GetLayer(c_strDefaultOverlay, true);
            m_markerMouse.Position = new PointLatLng(fLatitude, fLongitude);
            m_markerMouse.ToolTipText = "Lat : " +
                (new CValeurUnite(fLatitude, "°").ToString("° ' ''")) +
                Environment.NewLine +
                "Long : " +
                (new CValeurUnite(fLongitude, "°").ToString("° ' ''"));
            if (!ov.Markers.Contains(m_markerMouse))
            {
                ov.Markers.Add(m_markerMouse);
            }
        }

        //------------------------------------
        public void HideMouseMarker()
        {
            GMapOverlay ov = GetLayer(c_strDefaultOverlay, true);
            if (ov.Markers.Contains(m_markerMouse))
            {
                ov.Markers.Remove(m_markerMouse);
            }
        }

        //------------------------------------
        private void m_trackZoom_ValueChanged(object sender, EventArgs e)
        {
            if (m_gMap.Zoom != m_trackZoom.Value)
                m_gMap.Zoom = m_trackZoom.Value;
        }

        //------------------------------------
        private void m_gMap_OnMapZoomChanged()
        {
            if (m_trackZoom.Value != m_gMap.Zoom)
                m_trackZoom.Value = (int)m_gMap.Zoom;
        }

        /*private void m_gMap_OnMapTypeChanged(MapType type)
        {
            switch (type)
            {
                case MapType.GoogleHybrid:
                    MapStyle = EMapStyle.Hybrid;
                    break;
                case MapType.GoogleMap:
                    MapStyle = EMapStyle.Road;
                    break;
                case MapType.GoogleSatellite:
                    MapStyle = EMapStyle.Aerial;
                    break;
                
            }
        }*/

        //------------------------------------
        private void m_btnMap_CheckedChanged(object sender, EventArgs e)
        {
            if (m_btnMap.Checked && MapStyle != EMapStyle.Road)
                MapStyle = EMapStyle.Road;
        }

        //------------------------------------
        private void m_btnSat_CheckedChanged(object sender, EventArgs e)
        {
            if (m_btnSat.Checked && MapStyle != EMapStyle.Aerial)
                MapStyle = EMapStyle.Aerial;
        }

        //------------------------------------
        private void m_btnHybrid_CheckedChanged(object sender, EventArgs e)
        {
            if (m_btnHybrid.Checked && MapStyle != EMapStyle.Hybrid)
                MapStyle = EMapStyle.Hybrid;
        }

        //------------------------------------
        private void ClearDatabaseObjects()
        {
            GMapOverlay[] layers = m_gMap.Overlays.ToArray();
            foreach (GMapOverlay layer in layers)
            {
                if (layer != m_layerForDynamicsElements)
                    m_gMap.Overlays.Remove(layer);
            }
            m_dicLayers.Clear();
            m_gMap.Refresh();
        }

        //------------------------------------
        public void SetMapDatabase(CMapDatabase database)
        {
            m_gMap.SuspendDrawing();
            ClearDatabaseObjects();
            foreach (CMapLayer layer in database.Layers)
            {
                if (layer.IsVisible)
                {
                    foreach (IMapItem item in layer.Items)
                        AddMapItem(item);
                }
            }
            m_gMap.ResumeDrawing();
        }

        //------------------------------------
        public IWin32MapItem GetGMapItemFromIMapItem(IMapItem item)
        {
            //Trouve l'object qui représente cet item
            foreach (GMapOverlay overlay in m_dicLayers.Values)
            {
                foreach (IWin32MapItem w32Item in overlay.Markers.OfType<IWin32MapItem>())
                    if (w32Item.Item == item)
                    {
                        return w32Item;
                    }
                foreach (IWin32MapItem w32Item in overlay.Routes.OfType<IWin32MapItem>())
                {
                    if (w32Item.Item == item)
                    {
                        return w32Item;
                    }
                }
                foreach (IWin32MapItem w32Item in overlay.Polygons.OfType<IWin32MapItem>())
                {
                    if (w32Item.Item == item)
                    {
                        return w32Item;
                    }
                }
            }
            return null;
        }

        //------------------------------------
        public void RefreshItem(IMapItem item)
        {
            IWin32MapItem w32Item = GetGMapItemFromIMapItem(item);
            if (w32Item != null)
            {
                w32Item.DeleteItem();
            }
            AddMapItem(item);
        }

        //------------------------------------
        public void DeleteItem(IMapItem item)
        {
            IWin32MapItem w32Item = GetGMapItemFromIMapItem(item);
            if (w32Item != null)
            {
                w32Item.DeleteItem();
            }
        }


        //------------------------------------
        public void AddMapItem(IMapItem item)
        {
            IWin32MapItem win32Item = CAllocateurWin32MapItem.GetNewMapItem(item);
            if (win32Item != null)
            {
                m_listeWin32Items.Add(win32Item);
                win32Item.AddToLayer(GetLayer(item.Layer.LayerName, true));
            }
        }

        //------------------------------------
        public void AddMapsItems(IEnumerable<IMapItem> items)
        {
            foreach (IMapItem item in items)
            {
                AddMapItem(item);
            }
        }

        //------------------------------------
        public void HideLayer(string strLayer)
        {
            GMapOverlay layer = GetLayer(strLayer);
            if (layer != null)
                layer.IsVisibile = false;
        }

        //------------------------------------
        public void ShowLayer(string strLayer)
        {
            GMapOverlay layer = GetLayer(strLayer);
            if (layer != null)
                layer.IsVisibile = true;
        }

        //------------------------------------
        public event MapItemClickEventHandler MapItemClicked;

        //------------------------------------
        private void m_gMap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if (m_bPreventClickOnItem)
                return;
            IWin32MapItem win32Item = item as IWin32MapItem;
            m_bTraitementParDefautMouseUp = false;
            if (item == m_markerMouse)
            {
                HideMouseMarker();

            }
            if (win32Item != null && win32Item.Item != null)
            {
                MapItemClickEventArgs mapArgs = new MapItemClickEventArgs(win32Item.Item, e);
                if (MapItemClicked != null)
                    MapItemClicked(this, mapArgs);
                if (win32Item.Item != null && !mapArgs.IsProcessed)
                    win32Item.Item.OnClick();
            }
        }

        //------------------------------------
        private void m_gMap_OnRouteClick(GMapRoute item, MouseEventArgs e)
        {
            if (m_bPreventClickOnItem)
                return;
            IWin32MapItem win32Item = item as IWin32MapItem;
            m_bTraitementParDefautMouseUp = false;
            if (win32Item != null && win32Item.Item != null)
            {
                MapItemClickEventArgs mapArgs = new MapItemClickEventArgs(win32Item.Item, e);
                if (MapItemClicked != null)
                    MapItemClicked(this, mapArgs);
                if (win32Item.Item != null && !mapArgs.IsProcessed)
                    win32Item.Item.OnClick();
            }
        }


        //-------------------------------------------------------------
        public bool CaptureOnMapWindow
        {
            get
            {
                return m_gMap.Capture;
            }
            set
            {
                m_gMap.Capture = value;
            }
        }


        //-------------------------------------------------------------
        public Image ToImage()
        {
            return this.m_gMap.ToImage();
        }


    }

    public enum EMapStyle
    {
        Road,
        Aerial,
        Hybrid
    }

    public class EarthMouseEventArgs
    {
        public double Longitude;
        public double Latitude;
        public MouseButtons Buttons;
        public bool IsProcessed = false;
        public Point MousePoint;

        public EarthMouseEventArgs()
        {
        }

        public EarthMouseEventArgs (GMapControl map, MouseEventArgs args)
        {

            PointLatLng pt = map.FromLocalToLatLng(args.X, args.Y);
            MousePoint = new Point(args.X, args.Y);
            Buttons = args.Button;
            Latitude = pt.Lat;
            Longitude = pt.Lng;
            IsProcessed = false;
        }
    }

    public delegate void EarthMouseEventHandler(object sender, EarthMouseEventArgs args);

    public class MapItemClickEventArgs
    {
        public IMapItem Item;
        public MouseEventArgs MouseArgs;
        public bool IsProcessed = false;

        public MapItemClickEventArgs(IMapItem item, MouseEventArgs mouseArgs)
        {
            Item = item;
            MouseArgs = mouseArgs;
        }
    }

    public delegate void MapItemClickEventHandler(CVEarthCtrl ctrl, MapItemClickEventArgs args);

    

}
