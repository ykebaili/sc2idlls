using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;

using sc2i.drawing;
using sc2i.common;

namespace sc2i.win32.common
{
    public class CRectangleZoom
    {

         public CRectangleZoom(CPanelEditionObjetGraphique ctrlParent)
        {
            m_editeur = ctrlParent;
        }

        public event EventHandler ElementsMovedOrResized;

        /// /////////////////////////////////////////////////
        private CPanelEditionObjetGraphique m_editeur = null;
        public CPanelEditionObjetGraphique Editeur
        {
            get
            {
                return m_editeur;
            }
        }

        public bool EnDeplacement
        {
            get
            {
                return m_bIsStartDragging;
            }
        }

        private bool m_bIsStartDragging = false;
        private Point m_ptStartDrag;


        private Point m_ptStartSelection;
        private Rectangle GetRectangleSelection(Point pt)
        {
            int nPosX = pt.X > m_ptStartSelection.X ? m_ptStartSelection.X : pt.X;
            int nPosY = pt.Y > m_ptStartSelection.Y ? m_ptStartSelection.Y : pt.Y;
            int nWidth = Math.Abs(pt.X - m_ptStartSelection.X);
            int nHeight = Math.Abs(pt.Y - m_ptStartSelection.Y);
            return new Rectangle(nPosX, nPosY, nWidth, nHeight);
        }


        DateTime m_dtLastMouseDown = DateTime.Now;

        //est vrai si un mousedown a eu lieu dans cette fenêtre
        private bool m_bMouseDownDansCetteFenetre = false;
        public virtual void MouseDown(Point pt)
        {
            m_dtLastMouseDown = DateTime.Now;
            m_ptStartDrag = pt;
           // m_waitSelection = null;
            m_bMouseDownDansCetteFenetre = true;
            //REDIMENTIONNEMENT
            m_bmp = null;
           
           
            
        }

        // /////////////////////////////////////////////////
        private Bitmap m_bmp;
        private Bitmap CacheScreenShot
        {
            get
            {
                if (m_bmp == null)
                {
                   
                    m_bmp = Editeur.DernierApercu;
                }
                return m_bmp;
            }
        }




    }
}
