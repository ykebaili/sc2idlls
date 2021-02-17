using System;
using System.Drawing;

namespace sc2i.drawing
{
	/// <summary>
	/// Description résumée de CSegment.
	/// </summary>
	public class CSegmentDroite
	{
		private Point m_pt1, m_pt2;
		
		/// //////////////////////////////////////////////////////////
		public CSegmentDroite(Point p1, Point p2)
		{
			m_pt1 = p1;
			m_pt2 = p2;
		}

        /// //////////////////////////////////////////////////////////
        public Point Point1
        {
            get
            {
                return m_pt1;
            }
            set
            {
                m_pt1 = value;
            }
        }

        /// //////////////////////////////////////////////////////////
        public Point Point2
        {
            get
            {
                return m_pt2;
            }
            set
            {
                m_pt2 = value;
            }
        }

        /// //////////////////////////////////////////////////////////
        public bool IsHorizontal
        {
            get
            {
                return Vecteur.Y == 0;
            }
        }

        /// //////////////////////////////////////////////////////////
        public bool IsVertical
        {
            get
            {
                return Vecteur.X == 0;
            }
        }
        

        /// //////////////////////////////////////////////////////////
        public Point Vecteur
        {
            get
            {
                return new Point(m_pt2.X - m_pt1.X, m_pt2.Y - m_pt1.Y);
            }
        }

        /// //////////////////////////////////////////////////////////
        public double Longueur
        {
            get
            {
                return Math.Sqrt(Math.Pow(m_pt2.X - m_pt1.X, 2) + Math.Pow(m_pt2.Y - m_pt1.Y, 2));
            }
        }

        /// //////////////////////////////////////////////////////////
        public int Left
        {
            get
            {
                return Math.Min(m_pt1.X, m_pt2.X);
            }
        }

        /// //////////////////////////////////////////////////////////
        public int Right
        {
            get
            {
                return Math.Max(m_pt1.X, m_pt2.X);
            }
        }

        /// //////////////////////////////////////////////////////////
        public int Top
        {
            get
            {
                return Math.Min(m_pt1.Y, m_pt2.Y);
            }
        }

        /// //////////////////////////////////////////////////////////
        public int Bottom
        {
            get
            {
                return Math.Max(m_pt1.Y, m_pt2.Y);
            }
        }

        /// //////////////////////////////////////////////////////////
        public Size SizeEnglobant
        {
            get
            {
                return new Size(Right - Left, Bottom - Top);
            }
        }

        /// //////////////////////////////////////////////////////////
        public Point Milieu
        {
            get
            {
                return new Point(m_pt1.X + (m_pt2.X - m_pt1.X) / 2, m_pt1.Y + (m_pt2.Y - m_pt1.Y) / 2);
            }
        }

		/// //////////////////////////////////////////////////////////
		public CSegmentDroite(int nX1, int nY1, int nX2, int nY2)
		{
			m_pt1 = new Point ( nX1, nY1 );
			m_pt2 = new Point ( nX2, nY2 );
		}

		/// //////////////////////////////////////////////////////////
		//Retourne les paramètres de l'équation de la droite
		//ax+by+c = 0
		public void GetEquationDroite ( ref double a, ref double b, ref double c )
		{
			if ( m_pt1.X - m_pt2.X == 0 )
			{
				b = 0;
				a = 1;
			}
			else
			{
				a = ((double)m_pt1.Y-(double)m_pt2.Y)/((double)m_pt1.X-(double)m_pt2.X);
				b = -1;
			}
			c = -a*(double)m_pt1.X-b*(double)m_pt1.Y;
		}

		/// //////////////////////////////////////////////////////////
		///retourne vrai si le point de la droite appartient au segment
		///si le point passé n'appartient pas à la droite, la fonction dit n'importe quoi
		private bool IsPointDeLaDroiteInSegment ( Point pt )
		{
			if ( pt.X < Math.Min ( m_pt1.X, m_pt2.X )-2 )
				return false;
			if ( pt.X > Math.Max ( m_pt1.X, m_pt2.X )+2 )
				return false;
			if ( pt.Y < Math.Min ( m_pt1.Y, m_pt2.Y )-2 )
				return false;
			if ( pt.Y > Math.Max ( m_pt1.Y, m_pt2.Y )+2 )
				return false;
			return true;
		}
		
		
		/// //////////////////////////////////////////////////////////
		public bool GetIntersectionPoint ( CSegmentDroite segment, ref Point pt )
		{
			double a1=0, b1=0, c1=0, a2=0, b2=0, c2=0;
			GetEquationDroite ( ref a1, ref b1, ref c1 );
			segment.GetEquationDroite ( ref a2, ref b2, ref c2 );
			if ( b2*a1-b1*a2 == 0 )
				return false;//Pas d'intersection
			double x, y;
			x = (b1*c2-b2*c1)/(b2*a1-b1*a2);
			y = (c2*a1-c1*a2)/(b1*a2-b2*a1);
            pt = new Point ( (int)x, (int)y );
			//Vérifie que le point trouvé est bien sur les deux segments
			if ( !IsPointDeLaDroiteInSegment ( pt ) || !segment.IsPointDeLaDroiteInSegment(pt))
				return false;
			return true;
		}

		/// //////////////////////////////////////////////////////////
		public bool GetIntersectionPoint ( Rectangle rect, ref Point pt )
		{
			if ( GetIntersectionPoint ( new CSegmentDroite ( rect.Left, rect.Top, rect.Right, rect.Top ), ref pt ))
				return true;

			if ( GetIntersectionPoint ( new CSegmentDroite ( rect.Right, rect.Top, rect.Right, rect.Bottom), ref pt ))
				return true;

			if ( GetIntersectionPoint ( new CSegmentDroite ( rect.Right, rect.Bottom, rect.Left, rect.Bottom), ref pt ))
				return true;

			if ( GetIntersectionPoint ( new CSegmentDroite ( rect.Left, rect.Bottom, rect.Left, rect.Top), ref pt ))
				return true;
			return false;
		}

		/// //////////////////////////////////////////////////////////
		public bool GetIntersectionPoint ( Point[] pts, ref Point pt )
		{
			for ( int nPoint = 0; nPoint < pts.Length; nPoint++ )
			{
				int nPointSuivant = nPoint+1;
				if ( nPointSuivant >= pts.Length )
					nPointSuivant = 0;
				if ( GetIntersectionPoint ( new CSegmentDroite ( pts[nPoint], pts[nPointSuivant] ), ref pt ))
					return true;
			}
			return false;
		}

        /// <summary>
        /// Retourne la distance entre un point et le segment
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public double GetDistance(Point pt)
        {
            double fA=0, fB=0, fC=0;
            GetEquationDroite(ref fA, ref fB, ref fC);
            double fSqrt = Math.Sqrt(fA * fA + fB * fB);
            if (fSqrt == 0)//La droite est un point
                return Math.Sqrt ( Math.Pow ( (double)m_pt1.X-(double)pt.X, 2.0)+Math.Pow ( (double)m_pt1.Y - (double)pt.Y, 2.0 ) );
            double fDistance = Math.Abs((fA * (double)pt.X + fB * (double)pt.Y + fC) / fSqrt);
            //S'il existe une projection perpendiculaire du point sur le segment, la distance du point
            //au segment est égale à la distance du point à la droite. Sinon, c'est le min de la distance
            //du point aux extremités.
            //pour savoir si le point P est sur le segment [AB], il faut que les vecteurs PA et PB soient
            //dans des sens opposés
            Point vectPA = new Point(pt.X - m_pt1.X, pt.Y - m_pt1.Y);
            Point vectPB = new Point(pt.X - m_pt2.X, pt.Y - m_pt2.Y);
            if (Math.Sign(vectPA.X) == Math.Sign(vectPB.X) &&
                Math.Sign(vectPA.Y) == Math.Sign(vectPB.Y))
            {
                //Le point n'a pas de projeté sur le segment
                double fDistA = Math.Sqrt(Math.Pow(pt.X - m_pt1.X, 2) + Math.Pow(pt.Y - m_pt1.Y, 2));
                double fDistB = Math.Sqrt(Math.Pow(pt.X - m_pt2.X, 2) + Math.Pow(pt.Y - m_pt2.Y, 2));
                return Math.Min(fDistA, fDistB);
            }
            return fDistance;
        }

        //-------------------------------------------------
        public void Offset(Point ptOffset)
        {
            m_pt1.Offset(ptOffset);
            m_pt2.Offset(ptOffset);
        }

        //-------------------------------------------------
        public void DrawFlechePt1(Graphics g, Pen pen, int nLongueur)
        {
            DrawFleche(g, pen, false, nLongueur);
        }

        //-------------------------------------------------
        public void DrawFlechePt2(Graphics g, Pen pen, int nLongueur)
        {
            DrawFleche(g, pen, true, nLongueur);
        }

        //-------------------------------------------------
        private void DrawFleche(Graphics g, Pen pen, bool bFin, int nLongueur)
        {
            Point pt1, pt2;
            if (bFin)
            {
                pt1 = m_pt1;
                pt2 = m_pt2;
            }
            else
            {
                pt1 = m_pt2;
                pt2 = m_pt1;
            }
            try
            {
                double fDim = 12;
                double fCosa = (double)Math.Abs(pt1.X - pt2.X) / (double)(Math.Sqrt((pt1.X - pt2.X) * (pt1.X - pt2.X) + (pt1.Y - pt2.Y) * (pt1.Y - pt2.Y)));
                double fSina = (double)Math.Abs(pt1.Y - pt2.Y) / (double)(Math.Sqrt((pt1.X - pt2.X) * (pt1.X - pt2.X) + (pt1.Y - pt2.Y) * (pt1.Y - pt2.Y)));
                Point m = new Point(0, 0);
                Point[] p = new Point[2];
                p[0] = new Point(0, 0);
                p[1] = new Point(0, 0);

                if (pt1.X > pt2.X)
                {
                    m.X = (int)(pt2.X + (long)(fDim * Math.Sqrt(nLongueur) * fCosa / 2.0));
                    p[0].X = (int)(m.X + (long)(fDim * fSina / 2.0));
                    p[1].X = (int)(m.X - (long)(fDim * fSina / 2.0));
                }
                else
                {
                    m.X = (int)(pt2.X - (long)(fDim * Math.Sqrt(nLongueur) * fCosa / 2.0));
                    p[0].X = (int)(m.X - (long)(fDim * fSina / 2.0));
                    p[1].X = (int)(m.X + (long)(fDim * fSina / 2.0));
                }
                if (pt1.Y > pt2.Y)
                {
                    m.Y = (int)(pt2.Y + (long)(fDim * Math.Sqrt(nLongueur) * fSina / 2.0));
                    p[0].Y = (int)(m.Y - (long)(fDim * fCosa / 2.0));
                    p[1].Y = (int)(m.Y + (long)(fDim * fCosa / 2.0));
                }
                else
                {
                    m.Y = (int)(pt2.Y - (long)(fDim * Math.Sqrt(nLongueur) * fSina / 2.0));
                    p[0].Y = (int)(m.Y + (long)(fDim * fCosa / 2.0));
                    p[1].Y = (int)(m.Y - (long)(fDim * fCosa / 2.0));
                }
                g.DrawLine(pen, pt2.X, pt2.Y, p[0].X, p[0].Y);
                g.DrawLine(pen, pt2.X, pt2.Y, p[1].X, p[1].Y);
            }
            catch
            {
            }
        }
    }
}
