using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;

using sc2i.common;
using sc2i.drawing;
using sc2i.expression;
using System.ComponentModel;
using sc2i.formulaire.web;

namespace sc2i.formulaire
{
    /// <summary>
    /// Description résumée de C2iPanel.
    /// </summary>
    [WndName("Panel")]
    [AWndIcone("ico_panel")]
    [Serializable]
    public class C2iWndPanel : C2iWndComposantFenetre, I2iWebControl
    {
        private const int c_tailleOmbre = 16;
        public enum PanelBorderStyle
        {
            Aucun,
            _3D,
            Plein
        }


        private PanelBorderStyle m_borderStyle = PanelBorderStyle.Aucun;

        private bool m_bOmbre = false;

        private Size m_gridSize = new Size(8, 8);

        private bool m_bAutoScroll = false;
        private bool m_bAutoSize = false;

        private int m_nNumOrdreWeb = 0;
        private string m_strLibelleWeb = "";

        public C2iWndPanel()
        {
            LockMode = ELockMode.Independant;
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
                if (value != PanelBorderStyle.Aucun)
                    Ombre = false;
            }
        }

        /// ///////////////////////
        public bool AutoScroll
        {
            get
            {
                return m_bAutoScroll;
            }
            set
            {
                m_bAutoScroll = value;
            }
        }

        /// ///////////////////////
        public bool AutoSize
        {
            get
            {
                return m_bAutoSize;
            }
            set
            {
                m_bAutoSize = value;
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
        [Browsable(false)]
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
        public bool Ombre
        {
            get
            {
                return m_bOmbre;
            }
            set
            {
                m_bOmbre = value;
                if (value)
                    BorderStyle = PanelBorderStyle.Aucun;
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
                if (m_bOmbre)
                {
                    sz.Width -= c_tailleOmbre;
                    sz.Height -= c_tailleOmbre;
                }
                return sz;
            }
        }

        /// //////////////////////////////////////////////////
        public string WebLabel
        {
            get
            {
                return m_strLibelleWeb;
            }
            set
            {
                m_strLibelleWeb = value;
            }
        }

        /// //////////////////////////////////////////////////
        public int WebNumOrder
        {
            get
            {
                return m_nNumOrdreWeb;
            }
            set
            {
                m_nNumOrdreWeb = value;
            }
        }


        /// ///////////////////////
        protected override void MyDraw(CContextDessinObjetGraphique ctx)
        {
            Graphics g = ctx.Graphic;
            Brush b = new SolidBrush(BackColor);
            Rectangle rect = new Rectangle(Position, Size);
            //rect = contexte.ConvertToAbsolute(rect);
            g.FillRectangle(b, rect);
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
                if (Ombre)
                {
                    Bitmap bmp;
                    bmp = new Bitmap(GetType(), "Bas.bmp");
                    Rectangle rectImg = new Rectangle(rect.Left, rect.Bottom - c_tailleOmbre,
                        rect.Width - c_tailleOmbre, c_tailleOmbre);
                    g.DrawImage(bmp, rectImg, 0, 0, rectImg.Width, rectImg.Height, GraphicsUnit.Pixel);
                    bmp.Dispose();
                    bmp = new Bitmap(GetType(), "Droite.bmp");
                    rectImg = new Rectangle(rect.Right - c_tailleOmbre, rect.Top,
                        c_tailleOmbre, rect.Height - c_tailleOmbre);
                    g.DrawImage(bmp, rectImg, 0, 0, rectImg.Width, rectImg.Height, GraphicsUnit.Pixel);
                    bmp.Dispose();
                    bmp = new Bitmap(GetType(), "BasDroite.bmp");
                    rectImg = new Rectangle(rect.Right - c_tailleOmbre, rect.Bottom - c_tailleOmbre,
                        c_tailleOmbre, c_tailleOmbre);
                    g.DrawImage(bmp, rectImg, 0, 0, c_tailleOmbre, c_tailleOmbre, GraphicsUnit.Pixel);
                    bmp.Dispose();
                }
                return;
            }
            //rect = contexte.ConvertToAbsolute(rect);
            if (BorderStyle == PanelBorderStyle.Plein)
            {
                Pen pen = new Pen(ForeColor);
                g.DrawRectangle(pen, rect);
                pen.Dispose();
            }
            if (BorderStyle == PanelBorderStyle._3D)
            {
                Pen pen = SystemPens.ControlDark;
                g.DrawRectangle(pen, rect);
                pen = SystemPens.ControlLight;
                g.DrawLine(pen, new Point(rect.Left, rect.Bottom), new Point(rect.Right, rect.Bottom));
                g.DrawLine(pen, new Point(rect.Right, rect.Bottom), new Point(rect.Right, rect.Top));
            }
        }

        /// ///////////////////////////////////////
        private int GetNumVersion()
        {

            return 4;
            // 2 : ajout de autoscroll
            // 3 : ajout de autosize
            // 4 : ajout de propriétés pour le web
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

            if (nVersion >= 1)
            {
                int nStyle = (int)m_borderStyle;
                serializer.TraiteInt(ref nStyle);
                m_borderStyle = (PanelBorderStyle)nStyle;
                serializer.TraiteBool(ref m_bOmbre);
            }
            if (nVersion >= 2)
                serializer.TraiteBool(ref m_bAutoScroll);
            if (nVersion >= 3)
                serializer.TraiteBool(ref m_bAutoSize);

            // Ajout des propriétés pour le web
            if (nVersion >= 4)
            {
                serializer.TraiteString(ref m_strLibelleWeb);
                serializer.TraiteInt(ref m_nNumOrdreWeb);
            }

            return result;
        }

    }
}
