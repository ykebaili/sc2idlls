using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

using sc2i.drawing;

namespace sc2i.win32.common
{

	/// <summary>
	/// Poignee de redimensionnement d'un objet I2iObjetGraphique
	/// Les poignées travaillent toujours dans le graphics de base, sans aucune transformation appliquée
    /// pour l'échelle ou les scrollbars
    /// Par contre, elles stockent leur données dans le repère logique
	/// </summary>
	public class CPoignee
	{

		public Point PositionDisplay
		{
			get
			{
                Point pt = Editeur.GetDisplayPointFromLogical(Position);
                switch (this.Alignement)
                {
                    case ContentAlignment.BottomCenter:
                        pt.Offset(-c_nTaille / 2, 0);
                        break;
                    case ContentAlignment.BottomLeft:
                        pt.Offset(-c_nTaille, 0);
                        break;
                    case ContentAlignment.MiddleCenter:
                        pt.Offset(-c_nTaille / 2, -c_nTaille / 2);
                        break;
                    case ContentAlignment.MiddleLeft:
                        pt.Offset(-c_nTaille, -c_nTaille / 2);
                        break;
                    case ContentAlignment.MiddleRight:
                        pt.Offset(0, -c_nTaille / 2);
                        break;
                    case ContentAlignment.TopCenter:
                        pt.Offset(-c_nTaille / 2, -c_nTaille);
                        break;
                    case ContentAlignment.TopLeft:
                        pt.Offset(-c_nTaille, -c_nTaille);
                        break;
                    case ContentAlignment.TopRight:
                        pt.Offset(0, -c_nTaille);
                        break;
                    default:
                        break;
                }
                return pt;
			}
		}


		public static int c_nTaille = 7;

        //Position de la poignée dans le repère logique
		protected Point m_position = new Point(0, 0);

		protected Point m_ptStartDrag;
		protected bool m_bIsDragging = false;
		protected CSelectionElementsGraphiques m_selection;
		protected Rectangle m_rectangleOrigineDrag; //Dans le repère logique et non physique
		private I2iObjetGraphique m_objLie;
		public I2iObjetGraphique ObjetLie
		{
			get
			{
				return m_objLie;
			}
		}
		protected ContentAlignment m_alignement = ContentAlignment.TopCenter;

		public CPoignee(CSelectionElementsGraphiques selection, I2iObjetGraphique obj, CPanelEditionObjetGraphique controlParentTmp)
		{
			m_selection = selection;
			m_editeur = controlParentTmp;
			m_objLie = obj;
		}

		///////////////////////////////////
		public ContentAlignment Alignement
		{
			get
			{
				return m_alignement;
			}
			set
			{
				m_alignement = value;
			}
		}

		///////////////////////////////////
		public Point Position
		{
			get
			{
				return m_position;
			}
			set
			{
				m_position = value;
			}
		}


		private EFormePoignee m_formePoignee = EFormePoignee.Carre;
		public EFormePoignee FormePoignee
		{
			get
			{
				return m_formePoignee;
			}
			set
			{
				m_formePoignee = value;
			}
		}

		private Color m_couleurPoignee = Color.Black;
		public Color CouleurPoignee
		{
			get
			{
				return m_couleurPoignee;
			}
			set
			{
				m_couleurPoignee = value;
			}
		}


		private Color m_couleurPoigneeSelectionnee = Color.FromArgb(50, Color.Red);
		public Color CouleurPoigneeSelectionnee
		{
			get
			{
				return m_couleurPoigneeSelectionnee;
			}
			set
			{
				m_couleurPoigneeSelectionnee = value;
			}
		}

		public Color CouleurBordure
		{
			get
			{
				return m_crayonBordure.Color;
			}
			set
			{
				m_crayonBordure.Color = value;
			}
		}

		private Pen m_crayonBordure = Pens.White;
		public Pen CrayonBordure
		{
			get
			{
				return m_crayonBordure;
			}
			set
			{
				m_crayonBordure = value;
			}
		}


        private Rectangle m_rectResize; // Dans le repère logique
        ///////////////////////////////////
        /// <summary>
        /// Retourne la dernière zone dans laquelle la poignée
        /// a dessiné une fonction de redimensionnement
        /// </summary>
        public Rectangle DerniereZoneDisplayPourResize
        {
            get
            {
                Rectangle rct = CUtilRect.Normalise(new Rectangle(
                    Editeur.GetDisplayPointFromLogical(m_rectResize.Location),
                    Editeur.GetDisplaySizeFromLogical(m_rectResize.Size)));

                rct.Inflate(2 * c_nTaille, 2 * c_nTaille);
                return rct;
            }
        }


		
		///////////////////////////////////
		public void Draw(Graphics g)
		{
			if (m_bIsDragging)
			{
				Pen pen = new Pen(Brushes.Black, 1);
				pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

                Rectangle rectDisplay = CUtilRect.Normalise(new Rectangle(Editeur.GetDisplayPointFromLogical(m_rectResize.Location),
                    Editeur.GetDisplaySizeFromLogical(m_rectResize.Size)));

			
                g.DrawRectangle(pen, rectDisplay);
              	
                
				pen.Dispose();
				m_bNewSizeCalculated = false;
			}

			Rectangle rect = new Rectangle(PositionDisplay, new Size(c_nTaille, c_nTaille));

			Brush bb;
           
			if (m_bIsDragging)
			{
				bb = new SolidBrush(CouleurPoigneeSelectionnee);
			}
			else
				bb = new SolidBrush(CouleurPoignee);
         
			
            g.FillRectangle(bb, rect);
			g.DrawRectangle(CrayonBordure, rect);

			bb.Dispose();
		}



        /*
        public void DrawInRepereReel(Graphics g, Rectangle zone)
        {
            if (m_bIsDragging)
            {
                Pen pen = new Pen(Brushes.Black, 1);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                m_rectResize = new Rectangle(
                    (int)(m_rectResize.Left *Editeur.Echelle),
                    (int)(m_rectResize.Top*Editeur.Echelle),
                    (int)(m_rectResize.Width*Editeur.Echelle),
                    (int)(m_rectResize.Height*Editeur.Echelle));
                if (m_bNewSizeCalculated)
                  m_rectResize.Offset((zone.Location));
               m_rectResize.Offset((int)(Editeur.AutoScrollPosition.X / Editeur.Echelle),
                    (int)(Editeur.AutoScrollPosition.Y / Editeur.Echelle));
                g.DrawRectangle(pen, m_rectResize);
                pen.Dispose();
                m_bNewSizeCalculated = false;
            }

            Rectangle rect = new Rectangle(PositionDisplay, new Size(c_nTaille, c_nTaille));

            Brush bb;
           // rect.Offset((zone.Location));

            if (m_bIsDragging)
            {
                bb = new SolidBrush(CouleurPoigneeSelectionnee);
            }
            else
                bb = new SolidBrush(CouleurPoignee);

            g.FillRectangle(bb, rect);
            g.DrawRectangle(CrayonBordure, rect);
            bb.Dispose();
        }*/

		///////////////////////////////////
		public bool IsPointIn(Point pt)
		{
            Point ptDisplay = Editeur.GetDisplayPointFromLogical(pt);
 			Rectangle rect = new Rectangle(PositionDisplay, new Size(c_nTaille, c_nTaille));
            return rect.Contains(ptDisplay);
		}
        /*
		///////////////////////////////////
		public Rectangle RectangleAbsolu
		{
			get
			{
				Rectangle rect = new Rectangle(PositionDisplay, new Size((int)(c_nTaille/Editeur.Echelle), (int)(c_nTaille/Editeur.Echelle)));
				return rect;                                                                            
			}
		}*/

		///////////////////////////////////
		public Cursor Cursor
		{
			get
			{
				switch (Alignement)
				{
					case ContentAlignment.TopCenter:
					case ContentAlignment.BottomCenter:
						return Cursors.SizeNS;
					case ContentAlignment.MiddleLeft:
					case ContentAlignment.MiddleRight:
						return Cursors.SizeWE;
					case ContentAlignment.TopLeft:
					case ContentAlignment.BottomRight:
						return Cursors.SizeNWSE;
					case ContentAlignment.BottomLeft:
					case ContentAlignment.TopRight:
						return Cursors.SizeNESW;
				}
				return Cursors.Arrow;
			}
		}

		///////////////////////////////////
		public void RecalcPosition(Rectangle rectLogique)
		{
			Point pt = new Point(0, 0);
			switch (Alignement)
			{
				case ContentAlignment.TopCenter:
				case ContentAlignment.TopLeft:
				case ContentAlignment.TopRight:
                    pt.Y = rectLogique.Top;
					break;
				case ContentAlignment.MiddleCenter:
				case ContentAlignment.MiddleLeft:
				case ContentAlignment.MiddleRight:
                    pt.Y = rectLogique.Top +rectLogique.Height / 2;
					break;
				case ContentAlignment.BottomCenter:
				case ContentAlignment.BottomRight:
				case ContentAlignment.BottomLeft:
					pt.Y = rectLogique.Bottom;
					break;
			}
			switch (Alignement)
			{
				case ContentAlignment.BottomLeft:
				case ContentAlignment.MiddleLeft:
				case ContentAlignment.TopLeft:
                    pt.X = rectLogique.Left;
                    break;
				case ContentAlignment.BottomCenter:
				case ContentAlignment.MiddleCenter:
				case ContentAlignment.TopCenter:
                    pt.X = rectLogique.Left + rectLogique.Width / 2;
					break;
				case ContentAlignment.BottomRight:
				case ContentAlignment.MiddleRight:
				case ContentAlignment.TopRight:
					pt.X = rectLogique.Right;
					break;

			}
			Position = pt;
		}


		///////////////////////////////////
		public void StartDrag(Point ptStart)
		{
			m_selection.Editeur.Capture = true;
			m_ptStartDrag = ptStart;
			m_bIsDragging = true;
			m_rectangleOrigineDrag = m_objLie.RectangleAbsolu;
		}
		public Point PointDebutDrag
		{
			get
			{
				return m_ptStartDrag;
			}
		}

		///////////////////////////////////
		private Rectangle CalcRectDragDrop(Point pt)
		{
			Rectangle rect = m_rectangleOrigineDrag;
			switch (Alignement)
			{
				case ContentAlignment.BottomCenter:
					rect.Height += pt.Y -m_rectangleOrigineDrag.Bottom;
					break;

				case ContentAlignment.BottomLeft:
					rect.Height += pt.Y - m_rectangleOrigineDrag.Bottom;
					rect.X += pt.X - m_rectangleOrigineDrag.X;
					rect.Width -= pt.X - m_rectangleOrigineDrag.Left;
					break;

				case ContentAlignment.BottomRight:
					rect.Height += pt.Y - m_rectangleOrigineDrag.Bottom;
					rect.Width += pt.X - m_rectangleOrigineDrag.Right;
					break;

				case ContentAlignment.MiddleLeft:
					rect.X += pt.X - m_rectangleOrigineDrag.X;
					rect.Width -= pt.X - m_rectangleOrigineDrag.Left;
					break;

				case ContentAlignment.MiddleRight:
					rect.Width += pt.X - m_rectangleOrigineDrag.Right;
					break;

				case ContentAlignment.TopCenter:
					rect.Y += pt.Y - m_rectangleOrigineDrag.Y;
					rect.Height -= pt.Y - m_rectangleOrigineDrag.Y;
					break;

				case ContentAlignment.TopLeft:
					rect.Y += pt.Y - m_rectangleOrigineDrag.Y;
					rect.Height -= pt.Y - m_rectangleOrigineDrag.Top;
					rect.X += pt.X - m_rectangleOrigineDrag.X;
					rect.Width -= pt.X - m_rectangleOrigineDrag.Left;
					break;
				case ContentAlignment.TopRight:
					rect.Y += pt.Y - m_rectangleOrigineDrag.Y;
					rect.Height -= pt.Y - m_rectangleOrigineDrag.Top;
					rect.Width += pt.X - m_rectangleOrigineDrag.Right;
					break;

				case ContentAlignment.MiddleCenter:
				default:
					break;
			}
            int nSgnY = Math.Sign(m_rectangleOrigineDrag.Height);
            int nSgnX = Math.Sign(m_rectangleOrigineDrag.Width);
            if (Math.Sign(rect.Height) != nSgnY || Math.Abs(rect.Height) < Editeur.HauteurMinimaleDesObjets)
                rect.Height = nSgnY * Editeur.HauteurMinimaleDesObjets;
            if (Math.Sign(rect.Width) != nSgnX || Math.Abs(rect.Width) < Editeur.LargeurMinimaleDesObjets)
                rect.Width = nSgnX * Editeur.LargeurMinimaleDesObjets;
			return rect;
		}

		///////////////////////////////////
		public void MouseMove(Point pt)
		{
			if (!m_bIsDragging)
				return;
			else
				MethodeDeplacer(pt);
		}

		///////////////////////////////////
		public void MouseUp()
		{
			if (!m_bIsDragging)
				return;
			m_selection.Editeur.Capture = false;
			Cursor.Current = Cursor;
			Rectangle rect = CalcRectDragDrop(m_lastPtDraggPoignee);

            Rectangle rectAbsolu = m_objLie.RectangleAbsolu;

            int nDecalX = rect.X - rectAbsolu.X;
            int nDecalY = rect.Y - rectAbsolu.Y;
            int nWidth = rect.Width - rectAbsolu.Width;
            int nHeight = rect.Height - rectAbsolu.Height;


			int nLargeur = m_objLie.Size.Width + nWidth;
			if (nLargeur*Math.Sign(rectAbsolu.Width) < Editeur.LargeurMinimaleDesObjets)
				nLargeur = Editeur.LargeurMinimaleDesObjets;
			int nHauteur = m_objLie.Size.Height + nHeight;
			if (nHauteur*Math.Sign(rectAbsolu.Height) < Editeur.HauteurMinimaleDesObjets)
				nHauteur = Editeur.HauteurMinimaleDesObjets;
			m_objLie.Size = new Size(nLargeur, nHauteur);

			int nNewY = m_objLie.PositionAbsolue.Y + nDecalY;
			int nNewX = m_objLie.PositionAbsolue.X + nDecalX;
			m_objLie.PositionAbsolue = new Point(nNewX, nNewY);
			m_selection.RecalcPositionPoignees();

			m_bIsDragging = false;
		}

		#region Dragging
		public Rectangle RectangleOrigine
		{
			get
			{
				return m_rectangleOrigineDrag;
			}
		}


		public void MethodeDeplacer(Point pt)
		{
            Rectangle rct = RectangleOrigine;
			int nWidth = rct.Width;
            int nHeight = rct.Height;
            int nX = rct.X;
            int nY = rct.Y;
			int nXMax = nX + nWidth;
			int nYMax = nY + nHeight;

			if (Editeur.ModeAlignement)
			{
				Point ptInParent = pt;
				switch (Alignement)
				{
					case ContentAlignment.BottomCenter:
						nYMax = Editeur.GetThePlusProche(ptInParent.Y, EDimentionDessin.Y);
						pt = new Point(ptInParent.X, nYMax);
						break;
					case ContentAlignment.BottomLeft:
						nX = Editeur.GetThePlusProche(ptInParent.X, EDimentionDessin.X);
						nYMax = Editeur.GetThePlusProche(ptInParent.Y, EDimentionDessin.Y);
						pt = new Point(nX, nYMax);
						break;
					case ContentAlignment.BottomRight:
						nXMax = Editeur.GetThePlusProche(ptInParent.X, EDimentionDessin.X);
						nYMax = Editeur.GetThePlusProche(ptInParent.Y, EDimentionDessin.Y);
						pt = new Point(nXMax, nYMax);
						break;

					case ContentAlignment.MiddleLeft:
						nX = Editeur.GetThePlusProche(ptInParent.X, EDimentionDessin.X);
						pt = new Point(nX, ptInParent.Y);
						break;
					case ContentAlignment.MiddleRight:
						nXMax = Editeur.GetThePlusProche(ptInParent.X, EDimentionDessin.X);
						pt = new Point(nXMax, ptInParent.Y);
						break;

					case ContentAlignment.TopCenter:
						nY = Editeur.GetThePlusProche(ptInParent.Y, EDimentionDessin.Y);
						pt = new Point(ptInParent.X, nY);
						break;
					case ContentAlignment.TopLeft:
						nX = Editeur.GetThePlusProche(ptInParent.X, EDimentionDessin.X);
						nY = Editeur.GetThePlusProche(ptInParent.Y, EDimentionDessin.Y);
						pt = new Point(nX, nY);
						break;
					case ContentAlignment.TopRight:
						nY = Editeur.GetThePlusProche(ptInParent.Y, EDimentionDessin.Y);
						nXMax = Editeur.GetThePlusProche(ptInParent.X, EDimentionDessin.X);
						pt = new Point(nXMax, nY);
						break;
					default:
					case ContentAlignment.MiddleCenter:
						break;
				}
			}


			int nHMin = Editeur.HauteurMinimaleDesObjets;
			int nLMin = Editeur.LargeurMinimaleDesObjets;

			/*bool bCompressionBasVersHaut = nY + nHMin <= pt.Y;
			bool bCompressionHautVersBas = nYMax - nHMin >= pt.Y;
			bool bCompressionDroiteVersGauche = nX + nLMin <= pt.X;
			bool bCompressionGaucheVersDroite = nXMax - nLMin >= pt.X;
			switch (Alignement)
			{
				case ContentAlignment.BottomCenter:
					if (!bCompressionBasVersHaut)
					{
						nX = m_lastPtDraggPoignee.X;
						pt = new Point(nX, nY + nHMin);
					}
					break;

				case ContentAlignment.BottomLeft:
					if (!bCompressionBasVersHaut && !bCompressionGaucheVersDroite)
						pt = new Point(nXMax - nLMin, nY + nHMin);
					else if (!bCompressionBasVersHaut)
						pt = new Point(pt.X, nY + nHMin);
					else if (!bCompressionGaucheVersDroite)
						pt = new Point(nXMax - nLMin, pt.Y);
					break;

				case ContentAlignment.BottomRight:
					if (!bCompressionBasVersHaut && !bCompressionDroiteVersGauche)
						pt = new Point(nX + nLMin, nY + nHMin);
					else if (!bCompressionBasVersHaut)
						pt = new Point(pt.X, nY + nHMin);
					else if (!bCompressionDroiteVersGauche)
						pt = new Point(nX + nLMin, pt.Y);
					break;

				case ContentAlignment.MiddleLeft:
					if (!bCompressionGaucheVersDroite)
					{
						nY = m_lastPtDraggPoignee.Y;
						pt = new Point(nXMax - nLMin, nY);
					}
					break;

				case ContentAlignment.MiddleRight:
					if (!bCompressionDroiteVersGauche)
					{
						nY = m_lastPtDraggPoignee.Y;
						pt = new Point(nX + nLMin, nY);
					}
					break;

				case ContentAlignment.TopCenter:
					if (!bCompressionHautVersBas)
					{
						nX = m_lastPtDraggPoignee.X;
						pt = new Point(nX, nYMax - nHMin);
					}
					break;

				case ContentAlignment.TopLeft:
					if (!bCompressionHautVersBas && !bCompressionGaucheVersDroite)
						pt = new Point(nXMax - nLMin, nYMax - nHMin);
					else if (!bCompressionHautVersBas)
						pt = new Point(pt.X, nYMax - nHMin);
					else if (!bCompressionGaucheVersDroite)
						pt = new Point(nXMax - nLMin, pt.Y);
					break;

				case ContentAlignment.TopRight:
					if (!bCompressionHautVersBas && !bCompressionDroiteVersGauche)
						pt = new Point(nX + nLMin, nYMax - nHMin);
					else if (!bCompressionHautVersBas)
						pt = new Point(pt.X, nYMax - nHMin);
					else if (!bCompressionDroiteVersGauche)
						pt = new Point(nX + nLMin, pt.Y);
					break;

				case ContentAlignment.MiddleCenter:
				default:
					break;
			}*/
			m_bNewSizeCalculated = m_lastPtDraggPoignee != pt;
			if (m_bNewSizeCalculated)
			{
				m_rectResize = CalcRectDragDrop(pt);
				RecalcPosition(m_rectResize);
				Cursor.Current = Cursor;
			}

			m_lastPtDraggPoignee = pt;
		}
		private bool m_bNewSizeCalculated = false;
		public bool NewSizeCalculated
		{
			get
			{
				return m_bNewSizeCalculated;
			}
		}
		private Point m_lastPtDraggPoignee;
		#endregion

		private CPanelEditionObjetGraphique m_editeur;
		public CPanelEditionObjetGraphique Editeur
		{
			get
			{
				return m_editeur;
			}
		}
	}
}
