using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace sc2i.drawing
{
    //Permet de dessiner des lignes brisées, en gérant des intersections
    public class CLienTracable
    {
        private List<Point> m_listePoints = new List<Point>();

        //----------------------------------------
        public CLienTracable()
        {

        }

        //----------------------------------------
        public CLienTracable(params Point[] pts)
        {
            foreach (Point pt in pts)
                m_listePoints.Add(pt);
        }

        //----------------------------------------
        public CLienTracable(CLienTracable lienSource)
        {
            m_listePoints.AddRange(lienSource.m_listePoints);
        }


        //----------------------------------------
        public IEnumerable<Point> Points
        {
            get
            {
                return m_listePoints.AsReadOnly();
            }
        }

        //----------------------------------------
        public void AddPoint(Point pt)
        {
            m_listePoints.Add(pt);
        }

        //----------------------------------------
        public void RemovePoint(Point pt)
        {
            m_listePoints.Remove(pt);
        }

        //----------------------------------------
        public void RemovePoint(int nPosition)
        {
            if (nPosition >= 0 && nPosition < m_listePoints.Count())
                m_listePoints.RemoveAt(nPosition);
        }

        //----------------------------------------
        public void ClearPoints()
        {
            m_listePoints.Clear();
        }

        
        //----------------------------------------
        public void Draw(Graphics g, Pen pen)
        {
            g.DrawLines(pen, m_listePoints.ToArray());
            
        }

        //----------------------------------------
        public CSegmentDroite[] Segments
        {
            get
            {
                List<CSegmentDroite> lst = new List<CSegmentDroite>();
                for (int nPoint = 1; nPoint < m_listePoints.Count(); nPoint++)
                {
                    lst.Add(new CSegmentDroite(m_listePoints[nPoint - 1], m_listePoints[nPoint]));
                }
                return lst.ToArray();
            }
        }

        //----------------------------------------
        public void RendVisibleAvecLesAutres(IEnumerable<CLienTracable> liensExistants)
        {
            //Commence par se décaller si confondu avec d'autres liens
            for (int nPoint = 1; nPoint < m_listePoints.Count; nPoint++)
            {
                Point ptOffset = Point.Empty;
                do
                {
                    CSegmentDroite segment = new CSegmentDroite(m_listePoints[nPoint - 1], m_listePoints[nPoint]);
                    ptOffset = Point.Empty;
                    //Vérifie la supperposition avec d'autres segments
                    foreach (CLienTracable lien in liensExistants)
                    {
                        IEnumerable<Point> ptsExistants = lien.Points;


                        for (int nPointExistant = 1; nPointExistant < ptsExistants.Count(); nPointExistant++)
                        {

                            CSegmentDroite segExiste = new CSegmentDroite(ptsExistants.ElementAt(nPointExistant - 1), ptsExistants.ElementAt(nPointExistant));
                            if (segment.IsHorizontal && segExiste.IsHorizontal && segment.Point1.Y == segExiste.Point1.Y &&
                                segExiste.Left < segment.Right && segExiste.Right > segment.Left)
                            {
                                //Décallage vers le bas
                                ptOffset = new Point(0, 5);
                                break;
                            }
                            if (segment.IsVertical && segExiste.IsVertical && segment.Point1.X == segExiste.Point1.X &&
                                segExiste.Top < segment.Bottom && segExiste.Bottom > segment.Top)
                            {
                                //Décallage vers la droite )
                                ptOffset = new Point(5, 0);
                                break;
                            }
                        }
                    }
                    if (ptOffset != Point.Empty)
                    {
                        Point pt = m_listePoints[nPoint - 1];
                        pt.Offset(ptOffset);
                        m_listePoints[nPoint - 1] = pt;
                        pt = m_listePoints[nPoint];
                        pt.Offset(ptOffset);
                        m_listePoints[nPoint] = pt;
                        
                    }
                } while (ptOffset != Point.Empty);
            }
            //Cherche les intersections
            //Création de la liste des segments dans les liens existants
            List<CSegmentDroite> segmentsExistants = new List<CSegmentDroite>();
            foreach (CLienTracable lien in liensExistants)
                segmentsExistants.AddRange(lien.Segments);
            
            CSegmentDroite[] mySegments = Segments;
            int nIndexStartSegment = 0;
            foreach (CSegmentDroite segTest in mySegments)
            {
                List<Point> lstInters = new List<Point>();
                foreach (CSegmentDroite seg in segmentsExistants)
                {
                    Point pt = Point.Empty;
                    if (segTest.GetIntersectionPoint(seg, ref pt))
                    {
                        lstInters.Add(pt);
                    }
                }
                if ( segTest.IsVertical )
                {
                    if ( segTest.Point1.Y < segTest.Point2.Y )
                        lstInters.Sort ( (p1, p2)=>p1.Y.CompareTo(p2.Y ));
                    else
                        lstInters.Sort ( (p1, p2)=>-p1.Y.CompareTo(p2.Y) );
                }
                if ( segTest.IsHorizontal )
                {
                    if ( segTest.Point1.X < segTest.Point2.X )
                        lstInters.Sort ( (p1, p2)=>p1.X.CompareTo(p2.X));
                    else
                        lstInters.Sort ( (p1, p2)=>-p1.X.CompareTo(p2.X) );
                }
                foreach (Point pt in lstInters)
                {
                    if (segTest.IsVertical)
                    {
                        int nInc = segTest.Point1.Y < segTest.Point2.Y ? -3 : +3;
                        m_listePoints.Insert(nIndexStartSegment + 1, new Point(pt.X, pt.Y + nInc));
                        m_listePoints.Insert(nIndexStartSegment + 2, new Point(pt.X + 3, pt.Y + nInc));
                        m_listePoints.Insert(nIndexStartSegment + 3, new Point(pt.X + 3, pt.Y - nInc));
                        m_listePoints.Insert(nIndexStartSegment + 4, new Point(pt.X, pt.Y - nInc));
                        nIndexStartSegment += 4;
                    }
                    if (segTest.IsHorizontal)
                    {
                        int nInc = segTest.Point1.X < segTest.Point2.X ? -3 : 3;
                        m_listePoints.Insert(nIndexStartSegment + 1, new Point(pt.X + nInc, pt.Y));
                        m_listePoints.Insert(nIndexStartSegment + 2, new Point(pt.X + nInc, pt.Y - 3));
                        m_listePoints.Insert(nIndexStartSegment + 3, new Point(pt.X - nInc, pt.Y - 3));
                        m_listePoints.Insert(nIndexStartSegment + 4, new Point(pt.X - nInc, pt.Y));
                        nIndexStartSegment += 4;
                    }

                }
                nIndexStartSegment++;
            }
            
        }

        //--------------------------------------
        public int GetDistance(Point pt)
        {
            int? nDistance = null;
            foreach (CSegmentDroite segment in Segments)
            {
                int n = (int)segment.GetDistance(pt);
                if (nDistance == null || n < nDistance.Value)
                    nDistance = n;
            }
            return nDistance.Value;
        }

        //--------------------------------------
        public Point GetMilieu()
        {
            if (m_listePoints.Count % 2 == 1)
                return m_listePoints[m_listePoints.Count / 2];
            
                int nMilieu = m_listePoints.Count / 2 - 1;
                CSegmentDroite segment = new CSegmentDroite(m_listePoints[nMilieu], m_listePoints[nMilieu+1]);
                return segment.Milieu;
            
                
        }

        public void RemplacePoint(int nPosition, Point pt1)
        {
            m_listePoints.Insert(nPosition, pt1);
            m_listePoints.RemoveAt(nPosition + 1);
        }
    }
}