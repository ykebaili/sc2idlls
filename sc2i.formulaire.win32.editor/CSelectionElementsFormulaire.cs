//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Windows.Forms;

//using sc2i.win32.common;

//namespace sc2i.formulaire.win32.editor
//{
//    /// <summary>
//    /// Description résumée de CSelectionElementsFormulaire.
//    /// </summary>
//    public class CSelectionElementsFormulaire : CSelectionElementsGraphiques
//    {
		
//        private CPanelEditionFormulaire m_controlParent = null;
//        private List<C2iWnd> m_selection = new List<C2iWnd>();

//        private CPoignee m_poigneeEnCours = null;

//        private bool m_bIsStartDragging = false;
//        private Point m_ptStartDrag;
//        private bool m_bIsDraggingPoignee = false;


//        private Rectangle m_rectangle = new Rectangle(0,0, 0,0);
		

//        private CPoignee[] m_poignees = new CPoignee[8];



//        /// /////////////////////////////////////////////////
//        public CSelectionElementsFormulaire( CPanelEditionFormulaire ctrlParent )
//            :base(ctrlParent)
//        {
//            m_controlParent = ctrlParent;
//            for ( int n = 0; n < 8; n++ )
//            {
//                m_poignees[n] = new CPoignee(this);
//                switch ( n )
//                {
//                    case 0 : 
//                        m_poignees[n].Alignement = ContentAlignment.TopLeft;
//                        break;
//                    case 1 :
//                        m_poignees[n].Alignement = ContentAlignment.TopCenter;
//                        break;
//                    case 2 :
//                        m_poignees[n].Alignement = ContentAlignment.TopRight;
//                        break;
//                    case 3 : 
//                        m_poignees[n].Alignement = ContentAlignment.MiddleLeft;
//                        break;
//                    case 4 : 
//                        m_poignees[n].Alignement = ContentAlignment.MiddleRight;
//                        break;
//                    case 5:
//                            m_poignees[n].Alignement = ContentAlignment.BottomLeft;
//                        break;
//                    case 6 :
//                        m_poignees[n].Alignement = ContentAlignment.BottomCenter;
//                        break;
//                    case 7 :
//                        m_poignees[n].Alignement = ContentAlignment.BottomRight;
//                        break;

//                }
//            }
//        }

//        /// /////////////////////////////////////////////////
//        public CPanelEditionFormulaire ControlParent
//        {
//            get
//            {
//                return m_controlParent;
//            }
//        }

//        // /////////////////////////////////////////////////
//        //public Rectangle RectangleEnglobant
//        //{
//        //    get
//        //    {
//        //        return m_rectangle;
//        //    }
//        //}


//        /// /////////////////////////////////////////////////
//        //public event EventHandler SelectionChanged;
//        /// /////////////////////////////////////////////////
//        public override void Clear ()
//        {
//            m_selection.Clear();
//            if ( SelectionChanged != null )
//                SelectionChanged ( this, new EventArgs() );
//            RecalcSize();
//        }
//        // /////////////////////////////////////////////////
//        public void Add ( C2iWnd wnd )
//        {
//            if ( wnd != null )
//            {
//                m_selection.Add(wnd);
//                if ( SelectionChanged != null )
//                    SelectionChanged ( this, new EventArgs() );
//                RecalcSize();
//            }
//        }
//        // /////////////////////////////////////////////////
//        public C2iWnd this[int n]
//        {
//            get
//            {
//                return m_selection[n];
//            }
//            set
//            {
//                m_selection[n] = value;
//                if ( SelectionChanged != null )
//                    SelectionChanged ( this, new EventArgs() );
//            }
//        }
//        // /////////////////////////////////////////////////
//        public override int Count 
//        {
//            get
//            {
//                return m_selection.Count;
//            }
//        }
//        // /////////////////////////////////////////////////
//        public override void RemoveAt(int n)
//        {
//            m_selection.RemoveAt ( n );
//            if ( SelectionChanged != null )
//                SelectionChanged ( this, new EventArgs() );
//        }
//        // /////////////////////////////////////////////////
//        private C2iWnd ControlSelectionneDeReference
//        {
//            get
//            {
//                return m_selection[m_selection.Count - 1];
//            }
//        }

		
//        // /////////////////////////////////////////////////
//        public override void MouseDown(Point pt)
//        {
//            m_bIsDraggingPoignee = false;
//            m_poigneeEnCours = null;
//            foreach ( CPoignee poignee in m_poignees )
//            {
//                if ( poignee.IsPointIn ( pt ) )
//                {
//                    m_bIsDraggingPoignee = false;
//                    m_poigneeEnCours = poignee;
//                    break;
//                }
//            }
//            //if ( m_poigneeEnCours == null )
//            //{
//            //    C2iWnd wnd = ControlParent.ElementEdite.WindowAtPoint(pt, false, null);
//            //    Clear();
//            //    Add ( wnd );
//            //    if ( SelectionChanged != null )
//            //        SelectionChanged ( this, new EventArgs() );
//            //}
			
//            if ( Count > 0 && !this[0].IsLock)
//                m_bIsStartDragging = true;
//            m_ptStartDrag = pt;
//        }
//        private bool m_bIsDraggingSelection = false;
//        // /////////////////////////////////////////////////
//        public override void MouseMove(Point pt)
//        {
//            if ( m_bIsDraggingPoignee && m_poigneeEnCours != null )
//            {
//                m_poigneeEnCours.MouseMove ( pt );
//                return;
//            }
//            bool bStd = true;
//            if ( m_rectangle.Contains(pt) && Count > 0 && !this[0].IsLock)
//            {
//                Cursor.Current = Cursors.SizeAll;
//                bStd = false;
//            }
//            foreach ( CPoignee poignee in m_poignees )
//            {
//                if ( poignee.IsPointIn ( pt ) )
//                {
//                    Cursor.Current = poignee.Cursor;
//                    bStd = false;
//                    break;
//                }
//            }
//            if (bStd )
//                Cursor.Current = Cursors.Arrow;
//            if ( m_bIsStartDragging  && Count == 1   )
//            {
//                if ( Math.Abs(m_ptStartDrag.X-pt.X) > 3 ||
//                    Math.Abs(m_ptStartDrag.Y-pt.Y) > 3 )
//                {
//                    if ( m_poigneeEnCours != null )
//                    {
//                        m_bIsDraggingPoignee = true;
//                        m_poigneeEnCours.StartDrag(m_ptStartDrag);
//                    }
//                    else
//                    {
//                        Point ptOffset = new Point ( pt.X-m_rectangle.Left, pt.Y-m_rectangle.Top );
//                        CDonneeDragDropControl donnee = new CDonneeDragDropControl((C2iWnd)this[0], ptOffset);
//                        m_controlParent.DoDragDrop ( donnee, System.Windows.Forms.DragDropEffects.Move );
//                        m_bIsStartDragging = false;
//                    }
//                }
//            }
//        }
//        // /////////////////////////////////////////////////
//        public override void MouseUp(Point pt)
//        {
//            //if (m_bIsDraggingPoignee && m_poigneeEnCours != null)
//            //{
//            //    m_poigneeEnCours.MouseUp(pt);
//            //}
//            //m_bIsStartDragging = false;
//            //m_bIsDraggingPoignee = false;

//            C2iWnd ctrl = ControlParent.ElementEdite.WindowAtPoint(pt, false, null);

//            if (m_bIsDraggingSelection)
//            {
//                //ON SELECTIONNE LES ELEMENTS DANS LE RECTANGLE!

//                m_bIsDraggingSelection = false;
//            }
//            else
//            {
//                if (m_selection.Contains(ctrl))
//                {
//                    bool bRemove = m_selection.Contains(ctrl);
//                    bool bCtrlReference = ctrl == ControlSelectionneDeReference;

//                    if (Control.ModifierKeys == Keys.Control)
//                    {
//                        m_selection.Remove(ctrl);
//                        if (bCtrlReference)
//                            Add(ctrl);
//                        else if (SelectionChanged != null)
//                            SelectionChanged(this, new EventArgs());
//                    }
//                    else
//                    {
//                        Clear();
//                        if (!bRemove)
//                            Add(ctrl);
//                    }

//                }
//                else
//                {
//                    if (Control.ModifierKeys != Keys.Control)
//                        Clear();
//                    Add(ctrl);
//                }
//            }
//            m_bIsStartDragging = false;
//            m_bIsDraggingPoignee = false;
//            //m_bIsDraggingSelection = false;
//        }


//        //// /////////////////////////////////////////////////
//        //public void ResizeAndMove ( Rectangle newRect )
//        //{
//        //    try
//        //    {
//        //        double fRatioX, fRatioY;
//        //        fRatioX = (double)newRect.Width/(double)m_rectangle.Width;
//        //        fRatioY = (double)newRect.Height/(double)m_rectangle.Height;
//        //        foreach ( C2iWnd wnd in m_selection )
//        //        {
//        //            Size sz = new Size(0,0);
//        //            sz.Width = (int)(wnd.Size.Width * fRatioX);
//        //            sz.Height = (int)(wnd.Size.Height * fRatioY);
//        //            wnd.Size = sz;
//        //            Point pt = wnd.PositionAbsolue;
//        //            pt.X = (int)(newRect.Left+(pt.X-m_rectangle.Left)*fRatioX);
//        //            pt.Y = (int)(newRect.Top + (pt.Y-m_rectangle.Top)*fRatioY);
//        //            wnd.PositionAbsolue = pt;
//        //        }
//        //        RecalcSize();
				
//        //    }
//        //    catch
//        //    {
//        //    }
//        //    m_controlParent.Refresh();
//        //}
//        //// /////////////////////////////////////////////////
//        //public void RecalcSize()
//        //{
//        //    if (Count == 0)
//        //    {
//        //        m_rectangle = new Rectangle(0, 0, 0, 0);
//        //        return;
//        //    }
//        //    m_rectangle = new Rectangle((m_selection[0]).PositionAbsolue, (m_selection[0]).Size);
//        //    for (int nElt = 1; nElt < Count; nElt++)
//        //    {
//        //        C2iWnd wnd = m_selection[nElt];
//        //        if (wnd.PositionAbsolue.X < m_rectangle.Left)
//        //            m_rectangle.X = wnd.PositionAbsolue.Y;
//        //        if (wnd.PositionAbsolue.Y < m_rectangle.Top)
//        //            m_rectangle.Y = wnd.PositionAbsolue.Y;
//        //        if (wnd.PositionAbsolue.X + wnd.Size.Width > m_rectangle.Right)
//        //            m_rectangle.Width = wnd.PositionAbsolue.X + wnd.Size.Width - m_rectangle.Left;
//        //        if (wnd.PositionAbsolue.Y + wnd.Size.Height > m_rectangle.Bottom)
//        //            m_rectangle.Height = wnd.PositionAbsolue.Y + wnd.Size.Height - m_rectangle.Top;
//        //    }
//        //    foreach (CPoignee poignee in m_poignees)
//        //        poignee.RecalcPosition(m_rectangle);
//        //    /*
//        //    m_poignees[c_haute].Position = new Point ( m_rectangle.Left+m_rectangle.Width/2-CPoignee.c_nTaille/2, m_rectangle.Top-CPoignee.c_nTaille);
//        //    m_poignees[c_droite].Position = new Point ( m_rectangle.Right, m_rectangle.Top+m_rectangle.Height/2-CPoignee.c_nTaille/2 );
//        //    m_poignees[c_basse].Position = new Point ( m_rectangle.Left+m_rectangle.Width/2-CPoignee.c_nTaille/2, m_rectangle.Bottom );
//        //    m_poignees[c_gauche].Position = new Point ( m_rectangle.Left-CPoignee.c_nTaille, m_rectangle.Top+m_rectangle.Height/2-CPoignee.c_nTaille/2 );
//        //    */
//        //}


//        // /////////////////////////////////////////////////
//        public override void Draw(Graphics g, bool bAvecPoignees)
//        {
//            if (Count == 0)
//                return;

//            Pen pen = new Pen(Brushes.Black, 1);
//            Pen penRed = new Pen(Brushes.Red, 2);
//            Pen penBlue = new Pen(Brushes.Blue, 1);
//            pen = penRed;
//            foreach (C2iWnd wnd in m_selection)
//            {
//                g.DrawRectangle(pen, wnd.RectangleAbsolu);
//                pen = penBlue;
//            }


//            if (bAvecPoignees && !this[0].IsLock)
//            {
//                foreach (CPoignee poignee in m_poignees)
//                    poignee.Draw(g);
//            }
//        }
//    }
//}
