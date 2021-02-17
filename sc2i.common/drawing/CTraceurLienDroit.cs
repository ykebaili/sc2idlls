using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.drawing;
using System.Drawing;
using sc2i.common;

namespace sc2i.drawing
{
    public enum EModeSortieLien
    {
        Automatic,
        Horizontal,
        Vertical,
        Straight
    }

    public class CModeSortieLien : CEnumALibelle<EModeSortieLien>
    {
        public CModeSortieLien(EModeSortieLien mode) :
            base(mode)
        {
        }

        public override string Libelle
        {
            get
            {
                switch (Code)
                {
                    case EModeSortieLien.Automatic:
                        return I.T("Automatic|20172");
                    case EModeSortieLien.Vertical:
                        return I.T("Vertical|20173");
                    case EModeSortieLien.Horizontal:
                        return I.T("Horiztonal|20174");
                    case EModeSortieLien.Straight:
                        return I.T("Straight|20175");
                }
                return "";
            }
        }
    }

    public class CTraceurLienDroit
    {
        public static CLienTracable GetLienPourLier(Rectangle rct1, Rectangle rct2, EModeSortieLien mode)
        {
            CLienTracable lien = GetLien ( 
                new Point(rct1.Left + rct1.Width / 2, rct1.Top + rct1.Height / 2),
                new Point(rct2.Left + rct2.Width / 2, rct2.Top + rct2.Height / 2),
                mode);
            List<CSegmentDroite> segments = new List<CSegmentDroite>();
            IEnumerable<Point> pts = lien.Points;
            for (int n = 1; n < pts.Count(); n++)
                segments.Add(new CSegmentDroite(pts.ElementAt(n - 1), pts.ElementAt(n)));

            int nRect = 0;
            while (true)
            {
                if (segments.Count == 0)
                    return lien;
                bool bIntersect = false;
                Rectangle rct = nRect == 0 ? rct1 : rct2;
                CSegmentDroite segment = nRect == 0 ? segments[0] : segments[segments.Count() - 1];
                Point[] ptsDeRect = new Point[]{
                    new Point ( rct.Left, rct.Top ),
                    new Point ( rct.Right, rct.Top ),
                    new Point ( rct.Right, rct.Bottom ),
                    new Point ( rct.Left, rct.Bottom )};
                for (int n = 0; n < 4; n++)
                {
                    CSegmentDroite seg = new CSegmentDroite(ptsDeRect[n],
                        ptsDeRect[(n + 1) % 4]);
                    Point pt = new Point(0, 0);
                    if (segment.GetIntersectionPoint(seg, ref pt))
                    {
                        if (nRect == 0)
                            segment.Point1 = pt;
                        else
                            segment.Point2 = pt;
                        bIntersect = true;
                        nRect++;
                        break;
                    }
                }
                if (nRect == 1 && !bIntersect)
                {
                    //Pas d'intersection
                    List<CSegmentDroite> lst = new List<CSegmentDroite>(segments);
                    lst.RemoveAt(lst.Count - 1);
                    segments = new List<CSegmentDroite>(lst);
                }
                if (nRect > 1)
                    break;
                if (nRect == 0)
                    break;
            }
            lien = new CLienTracable();
            lien.AddPoint(segments[0].Point1);
            for (int n = 0; n < segments.Count(); n++)
            {
                lien.AddPoint(segments[n].Point2);
            }
            return lien;
        }


        public static CLienTracable GetLien(Point pt1, Point pt2, EModeSortieLien modeSortie)
        {
            if (modeSortie == EModeSortieLien.Straight)
                return new CLienTracable(pt1, pt2);
            List<CSegmentDroite> lstSegments = new List<CSegmentDroite>();
            double fAngle = Math.PI / 2;
            if (pt1.X == pt2.X)
                fAngle = Math.PI / 2 * Math.Sign(pt2.Y * pt1.Y);
            else
                fAngle = Math.Atan((double)((pt2.Y - pt1.Y)) / (double)((pt2.X - pt1.X)));
            if (modeSortie == EModeSortieLien.Automatic)
            {
                if (Math.Abs(fAngle) < Math.PI / 4)
                    modeSortie = EModeSortieLien.Horizontal;
                else
                    modeSortie = EModeSortieLien.Vertical;
            }
            if (modeSortie == EModeSortieLien.Horizontal)
            {
                lstSegments.Add(new CSegmentDroite(pt1, new Point(pt2.X, pt1.Y)));
                if (pt2.Y != pt1.Y)
                    lstSegments.Add(new CSegmentDroite(new Point(pt2.X, pt1.Y), pt2));
            }
            else
            {
                lstSegments.Add(new CSegmentDroite(pt1, new Point(pt1.X, pt2.Y)));
                if (pt2.X != pt1.X)
                    lstSegments.Add(new CSegmentDroite(new Point(pt1.X, pt2.Y), pt2));
            }
            CLienTracable lien = new CLienTracable();
            lien.AddPoint ( lstSegments[0].Point1 );
            foreach (CSegmentDroite segment in lstSegments)
            {
                lien.AddPoint(segment.Point2);
            }

            return lien;
        }

        

    }
}
