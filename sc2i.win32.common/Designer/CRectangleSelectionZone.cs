using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace sc2i.win32.common
{
    /// <summary>
    /// Permet de dessiner un rectangle pendant une sélection.
    /// Le rectangle s'agrandit avec la fenêtre.
    /// Utilisation. Pour commencer, appeler StartSelection, avec la fenêtre dans laquelle on 
    /// fait la sélection et le point de départ.
    /// Pendant le mouvement, utiliser SetEndPoint pour indique le deuxième point de la selection
    /// En fin de mouvement, utiliser EndSelection.
    /// La propriété RectangleSelection retourne le rectangle sélectionné.
    /// </summary>
    public class CRectangleSelection
    {
        private Control m_wnd = null;
        Bitmap m_bitmap;

        private Point m_startPoint = new Point ( 0, 0);
        private Point m_endPoint = new Point(0, 0);

        public CRectangleSelection()
        {
        }

        public void StartSelection(Control wnd, Point startPoint)
        {
            if (m_bitmap != null)
                m_bitmap.Dispose();
            m_bitmap = null;
            m_bitmap = new Bitmap(wnd.Size.Width, wnd.Size.Height);
            wnd.DrawToBitmap(m_bitmap, new Rectangle(0, 0, wnd.Size.Width, wnd.Size.Height));
            m_wnd = wnd;
            m_startPoint = startPoint;
            m_endPoint = startPoint;
            m_wnd.Capture = true;
        }

        public void SetStartPoint(Point pt)
        {
            m_startPoint = pt;
        }

        public Rectangle RectangleSelection
        {
            get{
                Rectangle rct = new Rectangle ( 
                    Math.Min ( m_startPoint.X, m_endPoint.X ),
                    Math.Min ( m_startPoint.Y, m_endPoint.Y ),
                    Math.Abs ( m_startPoint.X - m_endPoint.X ),
                    Math.Abs ( m_startPoint.Y - m_endPoint.Y ));
                return rct;
            }
        }

        //-------------------------------
        private void DrawFond(Graphics g)
        {
            if (m_bitmap != null)
            {
                Point ptImage = m_wnd.PointToScreen(new Point(0, 0));
                Point ptControl;
                if ( m_wnd.Parent != null )
                    ptControl = m_wnd.Parent.PointToScreen ( m_wnd.Location );
                else
                    ptControl = m_wnd.Location;
                ptImage = new Point ( ptImage.X - ptControl.X, ptImage.Y - ptControl.Y );
                ptImage = new Point(-ptImage.X, -ptImage.Y);
                //Redessiner le fond
                g.DrawImage(m_bitmap, ptImage);
            }
        }

        public void SetEndPoint(Point pt)
        {
            if (m_wnd != null && m_bitmap != null)
            {
                Bitmap bmpComplet = new Bitmap(m_wnd.ClientSize.Width, m_wnd.ClientSize.Height);
                Graphics gImage = Graphics.FromImage(bmpComplet);
                DrawFond(gImage);
                m_endPoint = pt;
                Rectangle rct = RectangleSelection;
                if (rct.Width > 0 && rct.Height > 0)
                {
                    Brush brTrans = new SolidBrush(Color.FromArgb(50, Color.Green));
                    gImage.FillRectangle(brTrans, rct);
                    gImage.DrawRectangle(Pens.Blue, rct);
                    brTrans.Dispose();
                }
                gImage.Dispose();
                Graphics g = m_wnd.CreateGraphics();
                g.DrawImage ( bmpComplet, new Point ( 0, 0 ));
                g.Dispose();
            
            }
        }

        public void EndSelection()
        {
            if (m_bitmap != null)
            {
                Graphics g = m_wnd.CreateGraphics();
                DrawFond(g);
                g.Dispose();
                m_bitmap.Dispose();
                m_bitmap = null;
            }
            m_wnd.Capture = false;
        }
                        
                    
    }
}
