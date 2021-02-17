using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.drawing;
using sc2i.data;
using sc2i.common;
using System.Drawing;

namespace sc2i.process.workflow.dessin
{
    public class CWorkflowLienDessin : C2iObjetGraphique, IWorflowDessin
    {
        private CLienEtapesWorkflow m_lien = null;
        private string m_strIdUniverselLien = "";

        private string m_strIdUniverselSource = "";
        private string m_strIdUniverselDest = "";

        private bool m_bPositionInvalide = true;
        private CLienTracable m_lienTracable = null;

        private EModeSortieLien m_modeSortie = EModeSortieLien.Automatic;

        private Color m_couleurLien = Color.Black;
        private int m_nEpaisseurLien = 1;

        //------------------------------------------------------
        [System.ComponentModel.Browsable(false)]
        public string IdUniversel
        {
            get
            {
                return m_strIdUniverselLien;
            }
        }

        //------------------------------------------------------
        [System.ComponentModel.Browsable(false)]
        public string IdUniverselLien
        {
            get
            {
                return m_strIdUniverselLien;
            }
            set
            {
                m_strIdUniverselLien = value;
                m_lien = null;
                m_strIdUniverselSource = "";
                m_strIdUniverselDest = "";
            }
        }

        /// //////////////////////////////////////////////////////////////
        public EModeSortieLien OutStyle
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

        //------------------------------------------------------
        public Color LinkColor
        {
            get
            {
                return m_couleurLien;
            }
            set
            {
                m_couleurLien = value;
            }
        }

        /// //////////////////////////////////////////////////////////////
        public string ActivationCode
        {
            get
            {
                if (m_lien != null)
                    return m_lien.ActivationCode;
                return "";
            }
            set 
            {
                if (m_lien != null)
                    m_lien.ActivationCode = value.ToUpper() ;
            }

        }

        //------------------------------------------------------
        public int LinkWidth
        {
            get
            {
                return m_nEpaisseurLien;
            }
            set
            {
                m_nEpaisseurLien = value;
            }
        }

        //------------------------------------------------------
        [System.ComponentModel.Browsable(false)]
        public CLienEtapesWorkflow Lien
        {
            get
            {
                if (m_lien == null)
                {
                    CWorkflowDessin dessin = Parent as CWorkflowDessin;
                    if (dessin != null && dessin.TypeWorkflow != null)
                    {
                        CListeObjetsDonnees lst = dessin.TypeWorkflow.LiensEtapes;
                        lst.Filtre = new CFiltreData(CObjetDonnee.c_champIdUniversel + "=@1",
                            IdUniverselLien);
                        if (lst.Count > 0)
                            m_lien = lst[0] as CLienEtapesWorkflow;
                    }
                }
                return m_lien;
            }
            set
            {
                m_strIdUniverselDest = "";
                m_strIdUniverselSource = "";
                if (value != null)
                {
                    m_lien = value;
                    IdUniverselLien = m_lien.UniversalId;
                }
                else
                {
                    m_lien = null;
                    IdUniverselLien = "";
                }
            }
        }

        //------------------------------------------------------
        [System.ComponentModel.Browsable(false)]
        public IWorflowDessin ObjetSource
        {
            get
            {
                CWorkflowDessin wd = Parent as CWorkflowDessin;
                if (wd != null)
                    return wd.GetChild(IdUniverselSource);
                return null;
            }
        }

        //------------------------------------------------------
        [System.ComponentModel.Browsable(false)]
        public IWorflowDessin ObjetDest
        {
            get
            {
                CWorkflowDessin wd = Parent as CWorkflowDessin;
                if (wd != null)
                    return wd.GetChild(IdUniverselDest);
                return null;
            }
        }

        //-------------------------------------------------
        [System.ComponentModel.Browsable(false)]
        public string IdUniverselSource
        {
            get
            {
                if (m_strIdUniverselSource == "")
                {
                    CLienEtapesWorkflow lien = Lien;
                    if (lien != null && lien.EtapeSource != null)
                        m_strIdUniverselSource = lien.EtapeSource.IdUniversel;
                }
                return m_strIdUniverselSource;

            }
        }

        //-------------------------------------------------
        [System.ComponentModel.Browsable(false)]
        public string IdUniverselDest
        {
            get
            {
                if ( m_strIdUniverselDest == "" )
                {
                    CLienEtapesWorkflow lien = Lien;
                    m_strIdUniverselDest = lien != null && lien.EtapeDestination != null?lien.EtapeDestination.IdUniversel:"";
                }
                return m_strIdUniverselDest;
            }
        }

        //-------------------------------------------------
        public override I2iObjetGraphique[] Childs
        {
            get 
            {
                return new I2iObjetGraphique[0];
            }
        }

        //-------------------------------------------------
        public override bool AcceptChilds
        {
            get
            {
                return false;
            }
        }

        //-------------------------------------------------
        public override bool AddChild(I2iObjetGraphique child)
        {
            return false;
        }

        //-------------------------------------------------
        public override bool ContainsChild(I2iObjetGraphique child)
        {
            return false;
        }

        //-------------------------------------------------
        public override void RemoveChild(I2iObjetGraphique child)
        {
            
        }

        //-------------------------------------------------
        public override void BringToFront(I2iObjetGraphique child)
        {
            
        }

        //-------------------------------------------------
        public override void FrontToBack(I2iObjetGraphique child)
        {
         
        }

        //-------------------------------------------------
        private int GetNumVersion()
        {
            return 1;
        }

        //-------------------------------------------------
        protected override CResultAErreur MySerialize(sc2i.common.C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strIdUniverselLien);
            if (serializer.Mode == ModeSerialisation.Lecture)
            {
                m_lien = null;
                m_strIdUniverselDest = "";
                m_strIdUniverselSource = "";
            }
            int nCol = m_couleurLien.ToArgb();
            serializer.TraiteInt(ref nCol);
            m_couleurLien = Color.FromArgb(nCol);

            serializer.TraiteInt(ref m_nEpaisseurLien);

            if (nVersion >= 1)
            {
                int nModeSortie = (int)m_modeSortie;
                serializer.TraiteInt(ref nModeSortie);
                m_modeSortie = (EModeSortieLien)nModeSortie;
            }

            return result;

        }

        /// //////////////////////////////////////////////////////////////
        protected virtual Pen GetNewPenCouleurCadre()
        {
            return new Pen(m_couleurLien, m_nEpaisseurLien);
        }

        /// //////////////////////////////////////////////////////////////
        protected override void MyDraw(CContextDessinObjetGraphique ctx)
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
            pt1 = lien.Points.ElementAt(lien.Points.Count() - 2);
            pt2 = lien.Points.ElementAt(lien.Points.Count() - 1);
            pen.Dispose();

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
                g.DrawLine(pen, pt2.X, pt2.Y, p[0].X, p[0].Y);
                g.DrawLine(pen, pt2.X, pt2.Y, p[1].X, p[1].Y);
                pen.Dispose();

                //Dessin du libelle
                string strLibelle = ActivationCode;
                if (strLibelle.Trim().Length > 0)
                {
                    CSegmentDroite[] segments = lien.Segments;
                    Font ft = new Font("Arial", 6, FontStyle.Regular);
                    SizeF size = g.MeasureString(strLibelle, ft);
                    Rectangle rect = RectangleAbsolu;

                    Point pt = segments[0].Milieu;
                    if (segments[0].Vecteur.Y == 0)//horizontal
                        pt.Offset(-(int)(size.Width / 2), -(int)(size.Height + 3));
                    else
                        pt.Offset(3, (int)(-size.Height / 2));
                    Brush bBlack = new SolidBrush(Color.Black);
                    g.DrawString(strLibelle, ft, bBlack, pt.X, pt.Y);
                    bBlack.Dispose();
                    ft.Dispose();
                }
            }
            catch
            {
            }
            /*
            Image img = GetImage();
            if (img != null)
            {
                Point ptImage = lien.GetMilieu();
                ptImage.X -= img.Width / 2;
                ptImage.Y -= img.Height / 2;
                g.DrawImage(img, ptImage.X, ptImage.Y, img.Width, img.Height);
                img.Dispose();
            }*/

        }

        /// //////////////////////////////////////////////////////////////
        public override Point Position
        {
            get
            {
                AssurePositionOk();
                return new Point(
                    Math.Min(m_lastPointArrivee.X, m_lastPointDepart.X),
                    Math.Min(m_lastPointDepart.Y, m_lastPointArrivee.Y));
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
                return new Size(
                    Math.Abs(m_lastPointArrivee.X - m_lastPointDepart.X),
                    Math.Abs(m_lastPointArrivee.Y - m_lastPointDepart.Y));
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
        private Point GetCentreRect(Rectangle rect)
        {
            return new Point((rect.Left + rect.Right) / 2, (rect.Top + rect.Bottom) / 2);
        }

        /// //////////////////////////////////////////////////////////////
        public override void DrawSelected(Graphics g)
        {
            Pen pen = new Pen(Color.Yellow);
            pen.Width = 2;
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            GetLienTracable().Draw(g, pen);
            pen.Dispose();
        }

        /// ///////////////////////////////////////////////
        public override bool IsPointIn(Point pt)
        {
            return GetLienTracable().GetDistance(pt) < 6;
        }

        /// //////////////////////////////////////////////////////////////
        private Rectangle m_lastRectangleDepart = new Rectangle(0, 0, 0, 0);
        private Rectangle m_lastRectangleArrivee = new Rectangle(0, 0, 0, 0);
        private Point m_lastPointDepart = new Point(0, 0), m_lastPointArrivee = new Point(10, 10);
        private void AssurePositionOk()
        {
            IWorflowDessin objetSource = ObjetSource;
            IWorflowDessin objetDest = ObjetDest;
            if (!m_bPositionInvalide)//Vérifie que le départ et l'arrivé n'ont pas bougé
            {
                if (objetSource != null && objetSource.RectangleAbsolu != m_lastRectangleDepart ||
                     objetDest != null && objetDest.RectangleAbsolu != m_lastRectangleArrivee)
                {
                    m_bPositionInvalide = true;
                }
            }
            if (m_bPositionInvalide)
            {
                Point pt1;
                if (objetSource == null)
                    pt1 = m_lastPointDepart;
                else
                {
                    m_lastRectangleDepart = objetSource.RectangleAbsolu;
                    pt1 = GetCentreRect(m_lastRectangleDepart);
                }

                Point pt2;
                
                if (objetDest == null)
                    pt2 = m_lastPointArrivee;
                else
                {
                    m_lastRectangleArrivee = objetDest.RectangleAbsolu;
                    pt2 = GetCentreRect(m_lastRectangleArrivee);
                }


                m_lienTracable = CTraceurLienDroit.GetLien(pt1, pt2, OutStyle);

                if (objetSource != null)
                {
                    Point[] pts1 = objetSource.GetPolygoneDessin();

                    //Suppression des segments inutiles
                    int nLastIntersect = 0;
                    CSegmentDroite[] segments = m_lienTracable.Segments;
                    for (nLastIntersect = segments.Count() - 1; nLastIntersect > 0; nLastIntersect--)
                    {
                        Point dummy = new Point(0, 0);
                        if (segments[nLastIntersect].GetIntersectionPoint(pts1, ref dummy))
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
                if (objetDest != null)
                {
                    Point[] pts2 = objetDest.GetPolygoneDessin();

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
                        m_lienTracable.RemovePoint(m_lienTracable.Points.Count() - 1);

                    if (m_lienTracable.Points.Count() > 1)
                    {

                        CSegmentDroite segment = new CSegmentDroite(
                            m_lienTracable.Points.ElementAt(m_lienTracable.Points.Count() - 2),
                            m_lienTracable.Points.ElementAt(m_lienTracable.Points.Count() - 1));
                        if (segment.GetIntersectionPoint(pts2, ref pt2))
                            m_lienTracable.RemplacePoint(m_lienTracable.Points.Count() - 1, pt2);
                    }
                }
                m_lastPointDepart = m_lienTracable.Points.ElementAt(0); ;
                m_lastPointArrivee = m_lienTracable.Points.ElementAt(m_lienTracable.Points.Count() - 1);
                m_bPositionInvalide = false;
            }
        }

        //-----------------------------------------------------
        public CResultAErreur Delete()
        {
            CResultAErreur result = CResultAErreur.True;
            if (Lien != null && Lien.IsValide())
            {
                result = Lien.Delete(true);
            }
            return result;
        }

        //-----------------------------------------------------
        public string Text
        {
            get
            {
                if (Lien != null)
                    return Lien.Libelle;
                return "";
            }
            set
            {
                if (Lien != null)
                    Lien.Libelle = value;
            }
        }

        //-----------------------------------------------------
        public Point[] GetPolygoneDessin()
        {
            if (m_lienTracable != null)
                return m_lienTracable.Points.ToArray();
            return new Point[0];
        }


                
    }
}
