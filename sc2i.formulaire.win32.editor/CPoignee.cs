//using System;
//using System.Drawing;
//using System.Windows.Forms;

//namespace sc2i.formulaire.win32.editor
//{
//    /// <summary>
//    /// Description résumée de CPoignee.
//    /// </summary>
//    public class CPoignee
//    {
//        public static int c_nTaille = 7;
//        protected Point m_position = new Point(0,0);

//        protected Point m_ptStartDrag;
//        protected bool m_bIsDragging = false;
//        protected CSelectionElementsFormulaire m_selection;
//        protected Rectangle m_rectangleOrigineDrag;

//        protected ContentAlignment m_alignement = ContentAlignment.TopCenter;

//        public CPoignee( CSelectionElementsFormulaire selection)
//        {
//            m_selection = selection;
//        }

//        ///////////////////////////////////
//        public ContentAlignment Alignement
//        {
//            get
//            {
//                return m_alignement;
//            }
//            set
//            {
//                m_alignement = value;
//            }
//        }

//        ///////////////////////////////////
//        public Point Position
//        {
//            get
//            {
//                return m_position;
//            }
//            set
//            {
//                m_position = value;
//            }
//        }

//        ///////////////////////////////////
//        public void Draw ( Graphics g )
//        {
//            Rectangle rect = new Rectangle ( Position, new Size ( c_nTaille, c_nTaille ) );
//            g.FillRectangle ( Brushes.Black, rect );
//        }

//        ///////////////////////////////////
//        public bool IsPointIn ( Point pt )
//        {
//            Rectangle rect = new Rectangle(Position, new Size ( c_nTaille, c_nTaille ));
//            return rect.Contains(pt);
//        }

//        ///////////////////////////////////
//        public Cursor Cursor
//        {
//            get
//            {
//                switch ( Alignement )
//                {
//                    case ContentAlignment.TopCenter :
//                    case ContentAlignment.BottomCenter :
//                        return Cursors.SizeNS;
//                    case ContentAlignment.MiddleLeft :
//                    case ContentAlignment.MiddleRight :
//                        return Cursors.SizeWE;
//                    case ContentAlignment.TopLeft :
//                    case ContentAlignment.BottomRight :
//                        return Cursors.SizeNWSE;
//                    case ContentAlignment.BottomLeft :
//                    case ContentAlignment.TopRight :
//                        return Cursors.SizeNESW;
//                }
//                return Cursors.Arrow;
//            }
//        }

//        ///////////////////////////////////
//        public void RecalcPosition ( Rectangle rect )
//        {
//            Point pt = new Point(0,0);
//            switch ( Alignement )
//            {
//                case ContentAlignment.TopCenter :
//                case ContentAlignment.TopLeft :
//                case ContentAlignment.TopRight :
//                    pt.Y = rect.Top-c_nTaille;
//                    break;
//                case ContentAlignment.MiddleCenter :
//                case ContentAlignment.MiddleLeft :
//                case ContentAlignment.MiddleRight :
//                    pt.Y = rect.Top + rect.Height/2 - c_nTaille/2;
//                    break;
//                case ContentAlignment.BottomCenter : 
//                case ContentAlignment.BottomRight : 
//                case ContentAlignment.BottomLeft : 
//                    pt.Y = rect.Bottom;
//                    break;
//            }
//            switch ( Alignement )
//            {
//                case ContentAlignment.BottomLeft :
//                case ContentAlignment.MiddleLeft :
//                case ContentAlignment.TopLeft :
//                    pt.X = rect.Left-CPoignee.c_nTaille;
//                    break;
//                case ContentAlignment.BottomCenter :
//                case ContentAlignment.MiddleCenter :
//                case ContentAlignment.TopCenter :
//                    pt.X = rect.Left+rect.Width/2 - c_nTaille/2;
//                    break;
//                case ContentAlignment.BottomRight :
//                case ContentAlignment.MiddleRight :
//                case ContentAlignment.TopRight : 
//                    pt.X = rect.Right;
//                    break;

//            }
//            Position = pt;
//        }


//        ///////////////////////////////////
//        public void StartDrag ( Point ptStart )
//        {
//            m_selection.ControlParent.Capture = true;
//            m_ptStartDrag = ptStart;	
//            m_bIsDragging = true;
//            m_rectangleOrigineDrag = m_selection.RectangleEnglobant;
//        }

//        ///////////////////////////////////
//        private Rectangle CalcRectDragDrop ( Point pt )
//        {
//            Rectangle rect = m_rectangleOrigineDrag;
//            switch ( Alignement )
//            {
//                case ContentAlignment.BottomCenter :
//                case ContentAlignment.BottomLeft :
//                case ContentAlignment.BottomRight :
//                    rect.Height = m_rectangleOrigineDrag.Height+ pt.Y - m_ptStartDrag.Y;
//                    break;
//                case ContentAlignment.TopCenter :
//                case ContentAlignment.TopLeft :
//                case ContentAlignment.TopRight :
//                    int nBottom = rect.Bottom;
//                    rect.Height = m_rectangleOrigineDrag.Height-pt.Y + m_ptStartDrag.Y;
//                    rect.Y = nBottom - rect.Height;
//                    break;
//            }
//            switch ( Alignement )
//            {
//                case ContentAlignment.BottomLeft :
//                case ContentAlignment.MiddleLeft :
//                case ContentAlignment.TopLeft :
//                    int nRight = rect.Right;
//                    rect.Width = m_rectangleOrigineDrag.Width - pt.X + m_ptStartDrag.X;
//                    rect.X = nRight - rect.Width;
//                    break;
//                case ContentAlignment.BottomRight :
//                case ContentAlignment.MiddleRight : 
//                case ContentAlignment.TopRight :
//                    rect.Width = m_rectangleOrigineDrag.Width + pt.X - m_ptStartDrag.X;
//                    break;

//            }
//            return rect;
//        }
		
//        ///////////////////////////////////
//        public void MouseMove ( Point pt )
//        {
//            if ( !m_bIsDragging )
//                return;
			
//            Cursor.Current = Cursor;
//            Rectangle rect = CalcRectDragDrop(pt);
//            Graphics g = m_selection.ControlParent.CreateGraphics();
//            m_selection.ControlParent.DrawPage(g);
//            Pen pen = new Pen ( Brushes.Black, 1 );
//            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
//            g.DrawRectangle(pen, rect);
//            //br.Dispose();
//            pen.Dispose();

//            RecalcPosition ( rect );
//            Draw(g);
//        }

//        ///////////////////////////////////
//        public void MouseUp ( Point pt )
//        {
//            if ( !m_bIsDragging )
//                return;
//            m_selection.ControlParent.Capture = false;
//            Cursor.Current = Cursor;
//            Rectangle rect = CalcRectDragDrop(pt);
//            m_selection.ResizeAndMove ( rect );
//        }
		
//    }
//}
