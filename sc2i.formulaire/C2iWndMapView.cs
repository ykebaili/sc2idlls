using System;
using System.Drawing;
using System.IO;
using System.Drawing.Design;
using System.Collections.Generic;


using sc2i.expression;
using sc2i.common;
using sc2i.drawing;


namespace sc2i.formulaire
{
	public enum EWndMapMode
	{
		Map = 0,
		Aerial = 1
	}
	/// <summary>
	/// Description résumée de C2iLabel.
	/// </summary>
	[WndName("Map view")]
	[Serializable]
	public class C2iWndMapView : C2iWndComposantFenetre, IDisposable
	{
		public const string c_strIdMouseUpOnMap = "MAP_MOUSEUP";
		private C2iExpression m_formuleLatitude = null;
		private C2iExpression m_formuleLongitude = null;

		private int m_nZoomFactor = 10;
		private EWndMapMode m_mapMode = EWndMapMode.Map;

		Image m_imageDesignTime = null;

		/// ///////////////////////
		public C2iWndMapView()
		{
			Size = new Size(100, 60);
            LockMode = ELockMode.Independant;
		}

		/// ///////////////////////
		public void Dispose()
		{
			if ( m_imageDesignTime != null )
				m_imageDesignTime.Dispose();
			m_imageDesignTime = null;
		}

		/// ///////////////////////////////////////
		public override bool CanBeUseOnType(Type tp)
		{
			return true;
		}

		

		/// ///////////////////////
		[System.ComponentModel.Editor(typeof(CProprieteExpressionEditor), typeof(UITypeEditor))]
		public C2iExpression LatitudeFormula
		{
			get
			{
				return m_formuleLatitude;
			}
			set
			{
				m_formuleLatitude = value;
			}
		}

		/// ///////////////////////
		[System.ComponentModel.Editor(typeof(CProprieteExpressionEditor), typeof(UITypeEditor))]
		public C2iExpression LongitudeFormula
		{
			get
			{
				return m_formuleLongitude;
			}
			set
			{
				m_formuleLongitude = value;
			}
		}

		/// ///////////////////////
		public int ZoomFactor
		{
			get
			{
				return m_nZoomFactor;
			}
			set
			{
				if (value >= 2 && value <= 19)
					m_nZoomFactor = value;
			}
		}

		/////////////////////////////////////
		public EWndMapMode MapMode
		{
			get
			{
				return m_mapMode;
			}
			set
			{
				m_mapMode = value;
			}
		}
		
		

#if PDA
#else
		/// ///////////////////////
		protected override void MyDraw( CContextDessinObjetGraphique ctx )
		{
            Graphics g = ctx.Graphic;
			Rectangle rect = new Rectangle ( Position , Size );
			if ( m_imageDesignTime == null )
			{
				string strNomImage = "MapViewDesignTime.bmp";
				m_imageDesignTime = new Bitmap(GetType(), strNomImage);
			}
			if ( m_imageDesignTime != null )
				g.DrawImage ( m_imageDesignTime, rect );
			else
			{
				Brush br = new SolidBrush ( BackColor );
				g.FillRectangle ( br, rect);
				br.Dispose();
				Pen pen = new Pen(ForeColor);
				g.DrawRectangle(pen, rect);
				pen.Dispose();
				StringFormat sf = new StringFormat();
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment = StringAlignment.Center;
				br = new SolidBrush(ForeColor);
				g.DrawString("Map view", Font, br, rect, sf);
				br.Dispose();
			}
		}


		
#endif
		/// ///////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// ///////////////////////////////////////
		protected override CResultAErreur MySerialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion(ref nVersion);
			if (!result)
				return result;

			serializer.TraiteObject<C2iExpression>(ref m_formuleLatitude);
			serializer.TraiteObject<C2iExpression>(ref m_formuleLongitude);
			int nVal = (int)MapMode;
			serializer.TraiteInt(ref nVal);
			MapMode = (EWndMapMode)nVal;
			serializer.TraiteInt(ref m_nZoomFactor);
			return result;
		}

		//-------------------------------------------------------------
		public override CDefinitionProprieteDynamique[] GetProprietesInstance()
		{
			List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
			lst.AddRange(base.GetProprietesInstance());
			
			lst.Add ( new CDefinitionProprieteDynamiqueDeportee (
				"Latitude", "Latitude", 
				new CTypeResultatExpression(typeof(double), false ),
				false,
				false,
				""));
			lst.Add ( new CDefinitionProprieteDynamiqueDeportee (
				"Longitude", "Longitude", 
				new CTypeResultatExpression(typeof(double), false ),
				false,
				false,
				""));
			lst.Add ( new CDefinitionProprieteDynamiqueDeportee (
				"ZoomFactor", "ZoomFactor", 
				new CTypeResultatExpression(typeof(int), false ),
				false,
				false,
				""));
			lst.Add ( new CDefinitionProprieteDynamiqueDeportee(
				"MapMode", "MapMode",
				new CTypeResultatExpression(typeof(int), false ),
				false,
				false,
				""));

			lst.Add(new CDefinitionProprieteDynamiqueDeportee(
				"Last Latitude click", "LastLatitudeClick",
				new CTypeResultatExpression ( typeof(double), false ),
				false,
				true,
				""));
			lst.Add(new CDefinitionProprieteDynamiqueDeportee(
				"Last Longitude click", "LastLongitudeClick",
				new CTypeResultatExpression ( typeof(double), false ),
				false,
				true,
				""));

			lst.Add(new CDefinitionMethodeDynamique(
				"MarkPoint", "MarkPoint",
				new CTypeResultatExpression(typeof(void), false),
				false,
				I.T("Mark a point on the map|20008"),
				new string[]{
					I.T("Latitude|20009"),
					I.T("Longitude|20010"),
					I.T("Point label|20021")
				}
				)
				);
			lst.Add(new CDefinitionMethodeDynamique(
				"ClearPoints", "ClearPoints",
				new CTypeResultatExpression(typeof(void), false),
				false,
				I.T("Clear marked points|20011"),
				new string[0]));

            lst.Add(new CDefinitionMethodeDynamique(
                "DrawLine", "DrawLine",
                new CTypeResultatExpression(typeof(void), false),
                false,
                I.T("Draw a line on the map|20014"),
                new string[]{
                    I.T("Point 1 latitude|20015"),
                    I.T("Point 1 longitude|20016"),
                    I.T("Point 2 latitude|20017"),
                    I.T("Point 2 longitude|20018"),
                    I.T("Line color|20019")}));
            lst.Add(new CDefinitionMethodeDynamique(
                "ClearLines", "ClearLines",
                new CTypeResultatExpression(typeof(void), false),
                false,
                I.T("Clear lines on map|20020"),
                new string[0]));

			return lst.ToArray();
		}

		//-------------------------------------------------------------
		public override CDescriptionEvenementParFormule[] GetDescriptionsEvenements()
		{
			List<CDescriptionEvenementParFormule> lst = new List<CDescriptionEvenementParFormule>(base.GetDescriptionsEvenements());
			lst.Add(new CDescriptionEvenementParFormule(
				c_strIdMouseUpOnMap, "OnClick",
				I.T("Occurs when user click on the map|20012")));
			return lst.ToArray();
		}


      
	}
}
