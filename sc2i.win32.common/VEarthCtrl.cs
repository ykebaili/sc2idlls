using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Resources;
using System.Reflection;

namespace sc2i.win32.common
{
    public enum EMapStyle
    {
        Road,
        Shaded,
        Aerial,
        Hybrid,
        Oblique,
        Birdseye,
        BirdseyeHybrid
    }

	public class EarthMouseEventArgs
	{
		public bool ControlKey;
		public bool AltKey;
		public bool ShiftKey;
		public string ElementId;
		public double Longitude;
		public double Latitude;
		public MouseButtons Buttons;
	}

	public delegate void EarthMouseEventHandler(object sender, EarthMouseEventArgs args); 
    

	
   

    [ComVisible(true)]
    public partial class CVEarthCtrl : UserControl
    {
        private double m_fCentreLat;
        private double m_fCentreLong;
        private double m_fZoom = 10;
        private EMapStyle m_mapStyle = EMapStyle.Road;

		private bool m_bIsInit = false;

		private static string c_strTextHTML = @"<HTML>
<script charset='UTF-8' type='text/javascript' src='http://dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.2&mkt=en-us'>
</script>
<body >
<div id='divMap'/>
<script charset='UTF-8' type='text/javascript' >
var initialPosition=new VELatLong(47.297127,-1.488608);
var initialZoom = 10;
var map = new VEMap('divMap');


map.LoadMap(initialPosition, initialZoom, VEMapStyle.Shaded, false, VEMapMode.Mode2D, true, 0);
window.external.AfterInitialUpdate();
map.AttachEvent('onchangeview', OnChangeView);
map.AttachEvent('onmouseup', OnMouseUp);
//map.AttacheEvent('onmousemove', OnMouseMove);
RefitMap();


function OnMouseMove ( e )
{
	//var position = map.PixelToLatLong ( new VEPixel ( e.mapX, e.mapY));
	//windows.external.ReceiveEarthMouseMove ( position.Latitude, position.Longitude );
}

function OnMouseUp ( e )
{
	var position = map.PixelToLatLong ( new VEPixel ( e.mapX, e.mapY));
	window.external.ReceiveEarthMouseUp ( 
		position.Latitude,
		position.Longitude,
		e.elementId,
		e.altKey,
		e.ctrlKey,
		e.shiftKey,
		e.leftMouseButton,
		e.rightMouseButton );
}

//redéfinit le zoom
function SetZoom ( fZoom )
{
initialZoom = fZoom;
map.SetZoomLevel ( fZoom )
}

//Se produit lors d'un changement de la vue active
function OnChangeView ( )
{
    window.external.ReceiveEarthChangeCenter(map.GetCenter().Latitude, map.GetCenter().Longitude);
    window.external.ReceiveEarthChangeZoom(map.GetZoomLevel());
    var strMapStyle = 'Road';
    switch ( map.GetMapStyle() )
    {
		case VEMapStyle.Road :
			strMapStyle = 'Road';
			break;
        case VEMapStyle.Shaded :
			strMapStyle = 'Shaded';
			break;
        case VEMapStyle.Aerial :
			strMapStyle = 'Aerial';
			break;
        case VEMapStyle.Hybrid :
			strMapStyle = 'Hybrid';
			break;
        case VEMapStyle.Oblique :
			strMapStyle = 'Oblique';
			break;
        case VEMapStyle.Birdseye :
			strMapStyle = 'Birdseye';
			break;
        case VEMapStyle.BirdseyeHybrid :
			strMapStyle = 'BirdseyeHybrid';
			break;
	}
    window.external.ReceiveEarthChangeMapStyle(strMapStyle);
}

//Redefinit le centre
function SetCenter ( long, lat )
{
    bw=new VELatLong(long,lat);
    map.SetCenter(bw);
}

//Redefinit le mapStyle
function SetMapStyle( s)
{
	var mapStyle = VEMapStyle.Aerial;
	switch ( s )
	{
	case 'Road':
		mapStyle = VEMapStyle.Road;
		break;
    case 'Aerial':
		mapStyle = VEMapStyle.Aerial;
		break;
     case 'Hybrid':
		mapStyle = VEMapStyle.Hybrid;
		break;
     case 'Oblique':
		mapStyle = VEMapStyle.Oblique;
		break;
     case 'Birdseye':
		mapStyle = VEMapStyle.Birdseye;
		break;
     case 'BirdseyeHybrid':
		mapStyle = VEMapStyle.BirdseyeHybrid;
		break;
    }
	map.SetMapStyle ( mapStyle );
}

function GetShapeLayer ( strLabel )
{
	var nbLayers = map.GetShapeLayerCount();
	for ( nLayer = 0; nLayer < nbLayers; nLayer++ )
	{
		var layer = map.GetShapeLayerByIndex(nLayer);
		if ( layer.GetTitle() == strLabel )
			return layer;
	}
	return null;
}

function Mark( fLat, fLong, strLabel, strLayer )
{
	var layer = GetShapeLayer ( strLayer );
	if(  layer == null )
	{
		layer = new VEShapeLayer()
		layer.SetTitle(strLayer);
		map.AddShapeLayer(layer);
	}
	lePoint = new VEShape(VEShapeType.Pushpin, new VELatLong ( fLat, fLong));
	if ( strLabel!='')
		lePoint.SetTitle(strLabel);
	layer.AddShape(lePoint);
	layer.Show();
	return lePoint.GetId();
}

function DrawLine ( fLat1, fLong1, fLat2, fLong2, nR, nG, nB,strLayer )
{

    var layer = GetShapeLayer ( strLayer );
	if(  layer == null )
	{
		layer = new VEShapeLayer();
		layer.SetTitle(strLayer);
		map.AddShapeLayer(layer);
	}
    points = [ new VELatLong(fLat1, fLong1),
                new VELatLong(fLat2, fLong2)];
    laLigne = new VEShape ( VEShapeType.Polyline, points);
    laLigne.SetLineColor ( new VEColor(nR,nG,nB,1.0));
    layer.AddShape ( laLigne );
    laLigne.HideIcon();
    laLigne.Show();
    layer.Show();
    
    return laLigne.GetId();
}



function ClearLayer ( strLayer )
{
	layer = GetShapeLayer ( strLayer );
	if ( layer != null )
		layer.DeleteAllShapes();
}
function SearchAddress ( strAdress )
{
	layer = map.GetShapeLayerByIndex(0);
	return map.Find ( null,
                     strAdress,
                                  null,
                                  layer,
                                  0,
                                  10,
                                  false,
                                  false,
                                  false,
                                  false,
                                  MoreResults);
}

function MoreResults(layer, resultsArray, places, hasMore, veErrorMessage)
{
	window.external.EarthStartReceiveResults();
	if ( places != null )
	{
		for  ( nPlace = 0; nPlace < places.length; nPlace++ )
			window.external.ReceiveEarthSearchResult ( places[nPlace].LatLong.Latitude, places[nPlace].LatLong.Longitude, places[nPlace].Name );
	}
	window.external.EarthEndReceiveResults();
   
}

//Redimensionne la carte pour coller à la taille du controle
function RefitMap(){ 
var frame = document.getElementById('divMap'); 
var htmlheight = document.body.parentNode.scrollHeight-40; 
var windowheight = window.innerHeight-40; 
var htmlWidth = document.body.parentNode.scrollWidth-40;
var windowWidth = window.innerWidth-40;
if ( htmlheight < windowheight ) 
{ 
    document.body.style.height = windowheight + 'px'; frame.style.height = windowheight + 'px'; 
    document.body.style.width = windowWidth + 'px'; frame.style.width = windowWidth + 'px'; 
} 
else 
{ 
    document.body.style.height = htmlheight + 'px'; frame.style.height = htmlheight + 'px'; 
    document.body.style.width = htmlWidth + 'px'; frame.style.width = htmlWidth + 'px'; 
} 
map.Resize();
} 


</script>
</body>
</html>";

		private class CPointEarth 
	{
		public double Longitude;
		public double Latitude;
		public string Libelle;
		public string Layer;

		public CPointEarth ( 
			double fLongitude,
			double fLatitude,
			string strLibelle,
			string strLayer )
		{
			Longitude = fLongitude;
			Latitude = fLatitude;
			Libelle = strLibelle;
			Layer = strLayer;
		}		
	}

		private List<CPointEarth> m_listePointsMarques = new List<CPointEarth>();

        public CVEarthCtrl()
        {
            InitializeComponent();
        }

        public event EventHandler OnEarthChangeCenter;

		private bool m_bIsPageLoaded = false;



        //------------------------------------
        public double CenterLatitude
        {
            get
            {
                return m_fCentreLat;
            }
            set
            {
                //if (value != m_fCentreLat)
                {
                    m_fCentreLat = value;
                    if (OnEarthChangeCenter != null)
                        OnEarthChangeCenter(this, null);
                }
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
                //if (value != m_fCentreLong)
                {
                    m_fCentreLong = value;
                    if (OnEarthChangeCenter != null)
                        OnEarthChangeCenter(this, null);
                }
            }
        }

        public event EventHandler OnEarthChangeZoom;
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
                    if (m_browser.Document != null)
                    {
                        m_browser.Document.InvokeScript("SetZoom", new object[] { m_fZoom });
                    }
                    if (OnEarthChangeZoom != null)
                        OnEarthChangeZoom(this, null);
                }
              
            }
        }

        public event EventHandler OnEarthChangeMapStyle;
        public EMapStyle MapStyle
        {
            get
            {
                return m_mapStyle;
            }
            set
            {
                //if (value != m_mapStyle)
                {
                    m_mapStyle = value;
                    if (m_browser.Document != null)
                        m_browser.Document.InvokeScript("SetMapStyle", new object[] { value.ToString() });
                    if (OnEarthChangeMapStyle != null)
                        OnEarthChangeMapStyle(this, null);
                }
            }
        }

        public void ReceiveEarthChangeCenter(double fX, double fY)
        {
            CenterLatitude = fX;
            CenterLongitude= fY;
        }

        public void ReceiveEarthChangeZoom(double fZoom)
        {
            Zoom = fZoom;
        }

        public void ReceiveEarthChangeMapStyle(string strStyle)
        {
            foreach ( object val in Enum.GetValues ( typeof(EMapStyle)))
            {
                if ( val.ToString() == strStyle )
                    MapStyle = (EMapStyle)val;
            }
        }

        

        

        //------------------------------------
        private string CreateHTML()
        {
            string resourceValue = string.Empty;
            try
            {
				string result = c_strTextHTML;
                

                return result;
            }
            catch (Exception e )
            {
                return e.ToString();
            }
        }

        //------------------------------------
		public void DisplayCarte()
		{
			m_browser.AllowNavigation = false;
			m_browser.DocumentText = CreateHTML();
			m_browser.ObjectForScripting = this;
		}

        private void m_browser_SizeChanged(object sender, EventArgs e)
        {
            if (m_browser.Document != null)
                m_browser.Document.InvokeScript("RefitMap");
        }

        public void RefreshPosition()
        {
            if (m_browser.Document != null)
            {
                m_browser.Document.InvokeScript("SetCenter", new object[] { m_fCentreLat, m_fCentreLong });
				m_browser.Document.InvokeScript("SetZoom", new object[] { m_fZoom });

            }
        }

        public string Mark(double fLatitude, double fLongitude, string strLabel, string strShape)
        {
			m_listePointsMarques.Add(new CPointEarth(
				fLongitude, fLatitude, strLabel, strShape));
            if (m_browser.Document != null)
            {
                return m_browser.Document.InvokeScript("Mark", new object[]{
                    fLatitude, fLongitude, strLabel, strShape}) as string;
            }
            return null;
        }


        public string Line(
            double fLat1, double fLong1,
            double fLat2, double fLong2,
            Color couleur,
            string strLayer)
        {
            return m_browser.Document.InvokeScript("DrawLine", new object[]{
                    fLat1, fLong1, fLat2, fLong2, couleur.R, couleur.G, couleur.B, strLayer}) as string;
        }

        public object SearchAddress(string strAddress)
        {
            if (m_browser.Document != null)
                return m_browser.Document.InvokeScript("SearchAddress", new object[] { strAddress });
            return null;
        }

        public void ClearLayer(string strLayer)
        {
            if (m_browser.Document != null)
                m_browser.Document.InvokeScript("ClearLayer", new object[] { strLayer });
			foreach (CPointEarth pt in m_listePointsMarques.ToArray())
			{
				if (pt.Layer == strLayer)
					m_listePointsMarques.Remove(pt);
			}
        }

		public event EarthMouseEventHandler OnEarthMouseUp;
		public void ReceiveEarthMouseUp(
			double fLatitude,
			double fLongitude,
			string strElementId,
			bool bAltKey,
			bool bControlKey,
			bool bShiftKey,
			bool bLeftButton,
			bool bRightButton
			)
		{
			if (OnEarthMouseUp != null)
			{
				EarthMouseEventArgs args = new EarthMouseEventArgs();
				args.AltKey = bAltKey;
				args.ShiftKey = bShiftKey;
				args.ControlKey = bControlKey;
				args.Longitude = fLongitude;
				args.Latitude = fLatitude;
				args.ElementId = strElementId;
				args.Buttons = MouseButtons.None;
				if (bLeftButton)
					args.Buttons |= MouseButtons.Left;
				if (bRightButton)
					args.Buttons |= MouseButtons.Right;
				OnEarthMouseUp(this, args);
			}
		}

		public void AfterInitialUpdate()
		{
			CenterLatitude = m_fCentreLat;
			CenterLongitude = m_fCentreLong;
			MapStyle = m_mapStyle;
			Zoom = m_fZoom;
			RefreshPosition();
			m_bIsInit = true;
			List<CPointEarth> oldList = new List<CPointEarth>(m_listePointsMarques);
			m_listePointsMarques.Clear();
			foreach (CPointEarth pt in oldList)
				Mark(pt.Latitude, pt.Longitude, pt.Libelle, pt.Layer);
		}

        

       
    }
}
