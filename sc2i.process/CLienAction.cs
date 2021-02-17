using System;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using sc2i.common;
using sc2i.drawing;
using System.Collections.ObjectModel;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CLienAction.
	/// </summary>
    public class CLienAction : C2iObjetGraphiqueSansChilds, IObjetDeProcess, I2iCloneableAvecTraitementApresClonage
	{
		private CProcess m_process;
		private int m_nIdActionDepart = -1;
		private int m_nIdActionArrivee = -1;
		private Point m_lastPointDepart=new Point ( 0, 0), m_lastPointArrivee=new Point ( 10, 10);

		//Indique que m_lastPointDepart et m_lastPointArrivee ne sont pas corrects
		private bool m_bPositionInvalide = true;

        //private List<CSegmentDroite> m_listeSegments = new List<CSegmentDroite>();
        private CLienTracable m_lienTracable = null;

        /// </summary>
        private bool m_bLienIsRenduVisible = false;

		//Stocké pour optim
		private string m_strLibelle = I.T("Simple link|273");

        private EModeSortieLien m_modeSortie = EModeSortieLien.Automatic;

		/// //////////////////////////////////////////////////////////////
		public CLienAction(CProcess process)
		{
			m_process = process;
			m_nId = m_process.GetIdNouvelObjetDeProcess();
			Parent = process;
		}

		private int m_nId;
		public int IdObjetProcess
		{
			get
			{
				return m_nId;
			}
		}

		public override bool IsLock
		{
			get
			{
				return true;
			}
			set
			{
				base.IsLock = value;
			}
		}

		/// //////////////////////////////////////////////////////////////
		public CProcess Process
		{
			get
			{
				return m_process;
			}
		}

		/// //////////////////////////////////////////////////////////////
		
		public virtual string Libelle
		{
			get
			{
				return m_strLibelle;
			}
		}

		/// //////////////////////////////////////////////////////////////
		private Rectangle m_lastRectangleDepart = new Rectangle(0, 0, 0, 0);
		private Rectangle m_lastRectangleArrivee = new Rectangle(0, 0, 0, 0);
        private void AssurePositionOk( )
        {
            if (!m_bPositionInvalide)//Vérifie que le départ et l'arrivé n'ont pas bougé
            {
                if (ActionDepart != null && ActionDepart.RectangleAbsolu != m_lastRectangleDepart ||
                     ActionArrivee != null && ActionArrivee.RectangleAbsolu != m_lastRectangleArrivee)
                {
                    m_bPositionInvalide = true;
                }
            }
            if (m_bPositionInvalide)
            {
                Point pt1;
                CAction actionDepart = ActionDepart;
                if (actionDepart == null)
                    pt1 = m_lastPointDepart;
                else
                {
                    m_lastRectangleDepart = actionDepart.RectangleAbsolu;
                    pt1 = GetCentreRect(m_lastRectangleDepart);
                }

                Point pt2;
                CAction actionArrivee = ActionArrivee;
                if (actionArrivee == null)
                    pt2 = m_lastPointArrivee;
                else
                {
                    m_lastRectangleArrivee = actionArrivee.RectangleAbsolu;
                    pt2 = GetCentreRect(m_lastRectangleArrivee);
                }


                m_lienTracable = CTraceurLienDroit.GetLien(pt1, pt2, ModeSortieDuLien);

                if (actionDepart != null)
                {
                    Point[] pts1 = actionDepart.GetPolygoneDessin();

                    //Suppression des segments inutiles
                    int nLastIntersect = 0;
                    CSegmentDroite[] segments = m_lienTracable.Segments;
                    for ( nLastIntersect = segments.Count()-1; nLastIntersect > 0; nLastIntersect-- )
                    {
                        Point dummy = new Point(0, 0) ;
                        if ( segments[nLastIntersect].GetIntersectionPoint ( pts1, ref dummy ))
                            break;
                    }
                    for (int n = 0; n < nLastIntersect; n++)
                        m_lienTracable.RemovePoint(0);

                    if (m_lienTracable.Points.Count() > 1)
                    {
                        CSegmentDroite segment = new CSegmentDroite(m_lienTracable.Points.ElementAt(0), m_lienTracable.Points.ElementAt(1));
                        if (segment.GetIntersectionPoint(pts1, ref pt1))
                            m_lienTracable.RemplacePoint(0, pt1);
                    }

                }
                if (actionArrivee != null)
                {
                    Point[] pts2 = actionArrivee.GetPolygoneDessin();

                    //Suppression des segments inutiles
                    CSegmentDroite[] segments = m_lienTracable.Segments;
                    int nLastIntersect = segments.Count() - 1;
                    for (nLastIntersect = 0; nLastIntersect < segments.Count() - 1; nLastIntersect++)
                    {
                        Point dummy = new Point(0, 0);
                        if (segments[nLastIntersect].GetIntersectionPoint(pts2, ref dummy))
                            break;
                    }
                    for (int n = nLastIntersect + 1; n < segments.Count(); n++)
                        m_lienTracable.RemovePoint (m_lienTracable.Points.Count()-1);

                    if (m_lienTracable.Points.Count() > 1)
                    {

                        CSegmentDroite segment = new CSegmentDroite(
                            m_lienTracable.Points.ElementAt(m_lienTracable.Points.Count() - 2),
                            m_lienTracable.Points.ElementAt(m_lienTracable.Points.Count() - 1));
                        if (segment.GetIntersectionPoint(pts2, ref pt2))
                            m_lienTracable.RemplacePoint(m_lienTracable.Points.Count() - 1, pt2);
                    }
                }
                m_lastPointDepart = m_lienTracable.Points.ElementAt(0);;
                m_lastPointArrivee = m_lienTracable.Points.ElementAt(m_lienTracable.Points.Count()-1);
                m_bPositionInvalide = false;
            }
        }

        /// //////////////////////////////////////////////////////////////
        public EModeSortieLien ModeSortieDuLien
        {
            get
            {
                return m_modeSortie;
            }
            set
            {
                m_modeSortie = value;
                m_bPositionInvalide = true;
            }
        }
		

		/// //////////////////////////////////////////////////////////////
		public override Point Position
		{
			get
			{
				AssurePositionOk();
				return new Point ( 
					Math.Min ( m_lastPointArrivee.X, m_lastPointDepart.X ),
					Math.Min (m_lastPointDepart.Y, m_lastPointArrivee.Y ));
			}
			set
			{
			}
		}

		/// //////////////////////////////////////////////////////////////
		public override Size Size
		{
			get
			{
				AssurePositionOk();
				return new Size (
					Math.Abs ( m_lastPointArrivee.X - m_lastPointDepart.X ),
					Math.Abs ( m_lastPointArrivee.Y - m_lastPointDepart.Y ));
			}
			set
			{
			}
		}

        /// ///////////////////////////////////////////////
        public CLienTracable GetLienTracable()
        {
            AssurePositionOk();
            return m_lienTracable;
        }

        /// //////////////////////////////////////////////////////////////
        public override bool NoRectangleSelection
        {
            get
            {
                return true;
            }
        }

        /// //////////////////////////////////////////////////////////////
        public override bool NoPoignees
        {
            get
            {
                return true;
            }
        }

        /// //////////////////////////////////////////////////////////////
        public override bool NoMove
        {
            get
            {
                return true;
            }
        }
        
        /// //////////////////////////////////////////////////////////////
        public override void DrawSelected(Graphics g)
        {
            Pen pen = new Pen(Color.Yellow);
            pen.Width = 2;
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            GetLienTracable().Draw(g, pen);
            /*foreach (CSegmentDroite segment in GetSegments())
                g.DrawLine(pen, segment.Point1, segment.Point2);*/
            pen.Dispose();
        }


		/// ///////////////////////////////////////////////
		public override bool IsPointIn ( Point pt )
		{
            return GetLienTracable().GetDistance(pt) < 6;
		}


		/// //////////////////////////////////////////////////////////////
		public CAction ActionDepart
		{
			get
			{
				return m_process.GetActionFromId ( m_nIdActionDepart );
			}
			set
			{
				if ( value == null )
					m_nIdActionDepart = -1;
				else
					m_nIdActionDepart = value.IdObjetProcess;
				m_bPositionInvalide = true;
			}
		}

		/// //////////////////////////////////////////////////////////////
		public CAction ActionArrivee
		{
			get
			{
				return m_process.GetActionFromId ( m_nIdActionArrivee );
			}
			set
			{
				if ( value == null )
					m_nIdActionArrivee = -1;
				else
					m_nIdActionArrivee = value.IdObjetProcess;
				m_bPositionInvalide = true;
			}
		}

		/// //////////////////////////////////////////////////////////////
		public int IdActionArrivee
		{
			get
			{
				return m_nIdActionArrivee;
			}
			set
			{
				m_nIdActionArrivee = value;
				m_bPositionInvalide = true;
			}
		}

		/// //////////////////////////////////////////////////////////////
		public int IdActionDepart
		{
			get
			{
				return m_nIdActionDepart;
			}
			set
			{
				m_nIdActionDepart = value;
				m_bPositionInvalide = true;
			}
		}

		/// ////////////////////////////////////////////////////////
		public virtual Image GetImage()
		{
			try
			{
				//Nom d'image : le nom du type suivi de gif
				string[] strNoms = GetType().ToString().Split('.');
				string strNomImage = strNoms[strNoms.Length-1]+".bmp";
				Image img = new Bitmap ( GetType(), strNomImage );
				return img;
			}
			catch{}
			return null;
		}

		

		/// //////////////////////////////////////////////////////////////
		private Point GetCentreRect ( Rectangle rect )
		{
			return new Point ( (rect.Left+rect.Right)/2, (rect.Top+rect.Bottom)/2);
		}

		/// //////////////////////////////////////////////////////////////
		public virtual Pen GetNewPenCouleurCadre()
		{
			return new Pen(Color.Black, 2);
		}

		/// //////////////////////////////////////////////////////////////
		protected override void MyDraw ( CContextDessinObjetGraphique ctx )
		{
			Graphics g = ctx.Graphic;
        	AssurePositionOk();
			Point pt1 = m_lastPointDepart;
			Point pt2 = m_lastPointArrivee;

            CLienTracable lien = new CLienTracable(GetLienTracable());
            lien.RendVisibleAvecLesAutres(ctx.Liens);
            ctx.AddLien(lien);
			Pen pen = GetNewPenCouleurCadre();
            lien.Draw(ctx.Graphic, pen);
            pt1 = lien.Points.ElementAt(lien.Points.Count()-2);
            pt2 = lien.Points.ElementAt(lien.Points.Count() - 1);
			pen.Dispose();
		
			try
			{
				double fDim = 12;
				double fCosa = (double)Math.Abs(pt1.X - pt2.X) / (double)(Math.Sqrt((pt1.X - pt2.X)*(pt1.X - pt2.X) + (pt1.Y - pt2.Y)*(pt1.Y - pt2.Y)));
				double fSina = (double)Math.Abs(pt1.Y - pt2.Y) / (double)(Math.Sqrt((pt1.X - pt2.X)*(pt1.X - pt2.X) + (pt1.Y - pt2.Y)*(pt1.Y - pt2.Y)));
				Point m = new Point(0,0);
				Point[] p = new Point[2];
				p[0] = new Point(0,0);
				p[1] = new Point(0,0);

				if (pt1.X > pt2.X) 
				{
					m.X = (int)(pt2.X + (long)(fDim * Math.Sqrt(3) * fCosa / 2.0));
					p[0].X = (int)(m.X + (long)(fDim * fSina / 2.0));
					p[1].X = (int)(m.X - (long)(fDim * fSina / 2.0));
				}
				else 
				{
					m.X = (int)(pt2.X - (long)(fDim * Math.Sqrt(3) * fCosa / 2.0));
					p[0].X = (int)(m.X - (long)(fDim * fSina / 2.0));
					p[1].X = (int)(m.X + (long)(fDim * fSina / 2.0));
				}
				if (pt1.Y > pt2.Y) 
				{
					m.Y = (int)(pt2.Y + (long)(fDim * Math.Sqrt(3) * fSina / 2.0));
					p[0].Y = (int)(m.Y - (long)(fDim * fCosa / 2.0));
					p[1].Y = (int)(m.Y + (long)(fDim * fCosa / 2.0));
				}
				else 
				{
					m.Y = (int)(pt2.Y - (long)(fDim * Math.Sqrt(3) * fSina / 2.0));
					p[0].Y = (int)(m.Y + (long)(fDim * fCosa / 2.0));
					p[1].Y = (int)(m.Y - (long)(fDim * fCosa / 2.0));
				}	
				pen = GetNewPenCouleurCadre();
				g.DrawLine ( pen, pt2.X, pt2.Y, p[0].X, p[0].Y );
				g.DrawLine ( pen, pt2.X, pt2.Y, p[1].X, p[1].Y );
				pen.Dispose();
			}
			catch
			{
			}

			Image img = GetImage();
			if ( img != null )
			{
                Point ptImage = lien.GetMilieu();
				ptImage.X -= img.Width/2;
				ptImage.Y -= img.Height/2;
				g.DrawImage ( img, ptImage.X, ptImage.Y, img.Width, img.Height );
				img.Dispose();
			}
        
		}

		/// //////////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 2;
            //2 : ajoute le mode de sortie du lien
		}

		/// //////////////////////////////////////////////////////////////
		protected override sc2i.common.CResultAErreur MySerialize(sc2i.common.C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			serializer.TraiteInt ( ref m_nIdActionArrivee );
			serializer.TraiteInt ( ref m_nIdActionDepart );
			int nX, nY;
			AssurePositionOk();
			
			nX = m_lastPointArrivee.X;
			nY = m_lastPointArrivee.Y;
			serializer.TraiteInt ( ref nX );
			serializer.TraiteInt ( ref nY );
			m_lastPointArrivee.X = nX;
			m_lastPointArrivee.Y = nY;

			nX = m_lastPointDepart.X;
			nY = m_lastPointDepart.Y;
			serializer.TraiteInt ( ref nX );
			serializer.TraiteInt ( ref nY );
			m_lastPointDepart.X = nX;
			m_lastPointDepart.Y = nY;

			if (nVersion > 0)
			{
				serializer.TraiteInt(ref m_nId);
			}
			else
			{
				if (m_process != null)
					m_nId = m_process.GetIdNouvelObjetDeProcess();
				else
					m_nId = -1;
			}

            if (nVersion >= 2)
            {
                int nTmp = (int)m_modeSortie;
                serializer.TraiteInt(ref nTmp);
                m_modeSortie = (EModeSortieLien)nTmp;
            }
			if (serializer.Mode == ModeSerialisation.Lecture)
			{
				m_bPositionInvalide = true;
			}

			return result;
		}

		/// ////////////////////////////////////////////////////////
		public virtual CResultAErreur VerifieDonnees()
		{
			return CResultAErreur.True;
		}

        /// ////////////////////////////////////////////////////////
        public void TraiteApresClonage(I2iSerializable objetSource)
        {
            m_nId = Process.GetIdNouvelObjetDeProcess();
        }

		

	}
}
