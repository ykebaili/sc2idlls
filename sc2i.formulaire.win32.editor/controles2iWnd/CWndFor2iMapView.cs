using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


using sc2i.formulaire;
using sc2i.common;
using sc2i.expression;
using sc2i.win32.common;
using System.Drawing;
using sc2i.common.Restrictions;


namespace sc2i.formulaire.win32.controles2iWnd
{
	[AutoExec("Autoexec")]
	public class CWndFor2iMapView : CControlWndFor2iWnd, IControleWndFor2iWnd
	{
		private CVEarthCtrl m_vearthControl = new CVEarthCtrl();

		private double m_fLatitudeClick = 0;
		private double m_fLongitudeClick = 0;

		public static void Autoexec()
		{
			CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndMapView), typeof(CWndFor2iMapView));
		}

		public CWndFor2iMapView()
			: base()
		{
            try
            {
                m_vearthControl.OnEarthMouseUp += new EarthMouseEventHandler(m_vearthControl_OnEarthMouseUp);
            }
            catch { }
		}

		protected override void  MyCreateControle(
			CCreateur2iFormulaireV2 createur,
			C2iWnd wnd, 
			Control parent, 
			IFournisseurProprietesDynamiques fournisseurProprietes)
		{
			C2iWndMapView mapView = wnd as C2iWndMapView;
			if (mapView == null)
				return;
			CCreateur2iFormulaireV2.AffecteProprietesCommunes(mapView, m_vearthControl);
            try
            {
                parent.Controls.Add(m_vearthControl);
                m_vearthControl.DisplayCarte();
                m_vearthControl.Zoom = mapView.ZoomFactor;
                switch (mapView.MapMode)
                {
                    case EWndMapMode.Map:
                        m_vearthControl.MapStyle = EMapStyle.Road;
                        break;
                    case EWndMapMode.Aerial:
                        m_vearthControl.MapStyle = EMapStyle.Aerial;
                        break;
                }
            }
            catch { }
			
		}

		
		//----------------------------------------
		public override Control Control
		{
			get
			{
				return m_vearthControl;
			}
		}

		//----------------------------------------
		private C2iWndMapView WndMapView
		{
			get
			{
				return WndAssociee as C2iWndMapView;
			}
		}

		//----------------------------------------
		protected override void OnChangeElementEdite(object element)
		{
			UpdateValeursCalculees();
		}

		//----------------------------------------
		protected override CResultAErreur MyMajChamps(bool bControlerLesValeursAvantValidation)
		{
			return CResultAErreur.True;
		}

		//----------------------------------------
		protected override void MyUpdateValeursCalculees()
		{
            try
            {
                C2iWndMapView mapView = WndMapView;
                if (mapView != null && m_vearthControl != null)
                {
                    CResultAErreur result = CResultAErreur.True;
                    CContexteEvaluationExpression contexte = new CContexteEvaluationExpression(EditedElement);
                    if (mapView.LatitudeFormula != null)
                    {
                        result = mapView.LatitudeFormula.Eval(contexte);
                        if (result && result.Data is double || result.Data is int)
                            m_vearthControl.CenterLatitude = Convert.ToDouble(result.Data);

                    }
                    if (mapView.LongitudeFormula != null)
                    {
                        result = mapView.LongitudeFormula.Eval(contexte);
                        if (result && result.Data is double || result.Data is int)
                            m_vearthControl.CenterLongitude = Convert.ToDouble(result.Data);
                    }
                }
                if (m_vearthControl != null)
                {
                    m_vearthControl.RefreshPosition();
                }
            }
            catch { }

		}

		//----------------------------------------
        protected override void MyAppliqueRestriction(
            CRestrictionUtilisateurSurType restrictionSurObjetEdite,
            CListeRestrictionsUtilisateurSurType listeRestrictions,
            IGestionnaireReadOnlySysteme gestionnaireReadOnly)
		{
		}

		//----------------------------------------
        public override bool LockEdition
        {
            get
            {
                return false;
            }
            set
            {
                Control.Enabled = true;
            }
        }

		//----------------------------------------
		public double Latitude
		{
			get
			{
                try
                {
                    if (m_vearthControl != null)
                        return m_vearthControl.CenterLatitude;
                }
                catch { }
				return 0;
			}
			set
			{
                try
                {
                    if (m_vearthControl != null)
                    {
                        m_vearthControl.CenterLatitude = value;
                        m_vearthControl.RefreshPosition();
                    }
                }
                catch { }
			}
		}

		//----------------------------------------
		public double Longitude
		{
			get
			{
                try
                {
                    if (m_vearthControl != null)
                        return m_vearthControl.CenterLongitude;
                }
                catch { }
				return 0;
			}
			set
			{
                try
                {
                    if (m_vearthControl != null)
                    {
                        m_vearthControl.CenterLongitude = value;
                        m_vearthControl.RefreshPosition();
                    }
                }
                catch { }
			}
		}

		//----------------------------------------
		public int ZoomFactor
		{
			get
			{
                try
                {
                    if (m_vearthControl != null)
                        return (int)m_vearthControl.Zoom;
                }
                catch { }
				return 10;
			}
			set
			{
                try
                {
                    if (m_vearthControl != null)
                        m_vearthControl.Zoom = value;
                }
                catch { }
			}
		}

		//----------------------------------------
		public int MapMode
		{
			get
			{
                try
                {
                    if (m_vearthControl != null)
                    {
                        if (m_vearthControl.MapStyle == EMapStyle.Aerial)
                            return (int)EWndMapMode.Aerial;
                    }
                }
                catch { }
				return (int)EWndMapMode.Map;
			}
			set
			{
                try
                {
                    if (m_vearthControl != null)
                    {
                        if (value == (int)EWndMapMode.Aerial)
                            m_vearthControl.MapStyle = EMapStyle.Aerial;
                        else
                            m_vearthControl.MapStyle = EMapStyle.Road;
                    }
                }
                catch { }
			}
		}

		//----------------------------------------
		public void MarkPoint(double fLatitude, double fLongitude, string strLabel)
		{
            try
            {
                if (m_vearthControl != null)
                {
                    m_vearthControl.Mark(fLatitude, fLongitude, strLabel, "2I");
                }
            }
            catch { }
		}

		//----------------------------------------
		public void ClearPoints()
		{
            try
            {
                if (m_vearthControl != null)
                {
                    m_vearthControl.ClearLayer("2I");
                }
            }
            catch { }
		}

        //----------------------------------------
        public void ClearLines()
        {
            try
            {
                if (m_vearthControl != null)
                {
                    m_vearthControl.ClearLayer("2IL");
                }
            }
            catch { }
        }

        //----------------------------------------
        public void DrawLine(double fLat1, double fLong1,
            double fLat2, double fLong2,
            Color couleurLigne)
        {
            try
            {
                if (m_vearthControl != null)
                {
                    m_vearthControl.Line(fLat1,
                        fLong1,
                        fLat2,
                        fLong2,
                        couleurLigne,
                        "2IL");
                }
            }
            catch { }
        }


		//----------------------------------------
		public double LastLatitudeClick
		{
			get
			{
				return m_fLatitudeClick;
			}
		}

		//----------------------------------------
		public double LastLongitudeClick
		{
			get
			{
				return m_fLongitudeClick;
			}
		}

		//----------------------------------------
		void m_vearthControl_OnEarthMouseUp(object sender, EarthMouseEventArgs args)
		{
            try
            {
                m_fLatitudeClick = args.Latitude;
                m_fLongitudeClick = args.Longitude;
                CUtilControlesWnd.DeclencheEvenement(C2iWndMapView.c_strIdMouseUpOnMap, this);
            }
            catch { }
		} 

		

		
	}
}
