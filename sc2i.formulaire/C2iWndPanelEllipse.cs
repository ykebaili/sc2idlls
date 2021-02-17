using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;

using sc2i.common;
using sc2i.drawing;
using sc2i.expression;

namespace sc2i.formulaire
{
    /// <summary>
    /// Description résumée de C2iPanelEllipse.
    /// </summary>
    [WndName("Panel Ellipse")]
    [Serializable]
    public class C2iWndPanelEllipse : C2iWndComposantFenetre
    {
       
        public enum PanelBorderStyle
        {
            Aucun,
            Plein
        }


		private PanelBorderStyle m_borderStyle = PanelBorderStyle.Plein;



        private Size m_gridSize = new Size(8, 8);

        public C2iWndPanelEllipse()
        {
        }

        /// ///////////////////////////////////////
        public override bool CanBeUseOnType(Type tp)
        {
            return true;
        }

        /// ///////////////////////
        public PanelBorderStyle BorderStyle
        {
            get
            {
                return m_borderStyle;
            }
            set
            {
                m_borderStyle = value;
           
            }
        }

        /// ///////////////////////
        [System.ComponentModel.Browsable(false)]
        public override bool AcceptChilds
        {
            get
            {
                return true;
            }
        }


        /// ///////////////////////////////////////
        public Size GridSize
        {
            get
            {
                return m_gridSize;
            }
            set
            {
                m_gridSize = value;
            }
        }

        /// ///////////////////////////////////////
        public override Point Magnetise(Point pt)
        {
            Point newPt = pt;
            if (m_gridSize.Width > 1)
                newPt.X = (int)(Math.Round((double)(newPt.X / m_gridSize.Width)) * m_gridSize.Width);
            if (m_gridSize.Height > 1)
                newPt.Y = (int)(Math.Round((double)(newPt.Y / m_gridSize.Height)) * m_gridSize.Height);
            return newPt;
        }

        /// ///////////////////////
        [System.ComponentModel.Browsable(false)]
        protected override Point OrigineCliente
        {
            get
            {
                if (BorderStyle == PanelBorderStyle.Aucun)
                    return new Point(0, 0);
                else
                    return new Point(1, 1);
            }
        }

   
        /// ///////////////////////////////////////
        [System.ComponentModel.Browsable(false)]
        protected override Size ClientSize
        {
            get
            {
                Size sz = Size;
                if (BorderStyle != PanelBorderStyle.Aucun)
                {
                    sz.Width -= 2;
                    sz.Height -= 2;
                }
        
                return sz;
            }
        }

#if PDA
#else
        /// ///////////////////////
        protected override void MyDraw(CContextDessinObjetGraphique ctx)
        {
            Graphics g = ctx.Graphic;
            Brush b = new SolidBrush(BackColor);
            Rectangle rect = new Rectangle(Position, Size);
            //rect = contexte.ConvertToAbsolute(rect);
            g.FillEllipse(b, rect);
            b.Dispose();
            DrawCadre(g);
            base.MyDraw(ctx);
        }

        /// /////////////////////////////////////////////////
        protected void DrawCadre(Graphics g)
        {
            Rectangle rect = new Rectangle(Position, Size);
            if (BorderStyle == PanelBorderStyle.Aucun)
            {
              
                return;
            }
            //rect = contexte.ConvertToAbsolute(rect);
            if (BorderStyle == PanelBorderStyle.Plein)
            {
                Pen pen = new Pen(ForeColor);
                g.DrawEllipse(pen, rect);
                pen.Dispose();
            }
       
        }
#endif
        /// ///////////////////////////////////////
        private int GetNumVersion()
        {
            return 0;
        }

        /// ///////////////////////////////////////
        protected override CResultAErreur MySerialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            int nTmp = m_gridSize.Width;
            serializer.TraiteInt(ref nTmp);
            m_gridSize.Width = nTmp;

            nTmp = m_gridSize.Height;
            serializer.TraiteInt(ref nTmp);
            m_gridSize.Height = nTmp;

            
            int nStyle = (int)m_borderStyle;
            serializer.TraiteInt(ref nStyle);
            m_borderStyle = (PanelBorderStyle)nStyle;
      
            return result;
        }



      

    }
}
