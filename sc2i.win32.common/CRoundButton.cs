using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using sc2i.win32.common;
using System.Media;
using System.Resources;
using System.IO;

namespace sc2i.win32.common
{
    public class CRoundButton : Button, IControlALockEdition
    {
        private int m_nMargeHorizontale = 0;
        private int m_nMargeVerticale = 0;
        private Color m_clickedColor = Color.LightBlue;
        private Color m_clickedTextColor = Color.LightBlue;
        private Color m_checkedColor = Color.LightBlue;
        private int m_nRayon = 10;
        private int m_nPctCouleur = 50;
        private float m_fCurrentPoliceSize = 20;
        private bool m_bAutoFitText = true;
        private GraphicsPath m_path = null;
        private bool m_bChecked = false;
        private bool m_bActivateSound = true;
        private CExtModeEdition m_gestionnaireModeEdition;

        private SoundPlayer m_player = null;

        

        public CRoundButton()
        {
            InitializeComponent();
            CalcTextSize();
            //MemoryStream stream = new MemoryStream(Resources.blip);
            //m_player = new SoundPlayer( Resources.blip);
        }


        //----------------------------------------
        public int MargeHorizontale
        {
            get
            {
                return m_nMargeHorizontale;
            }
            set
            {
                m_nMargeHorizontale = value;
                CalcPath();
                Refresh();
            }
        }

        //----------------------------------------
        public bool ActivateSound
        {
            get
            {
                return m_bActivateSound;
            }
            set
            {
                m_bActivateSound = value;
            }
        }

        //----------------------------------------
        public bool AutoFitText
        {
            get
            {
                return m_bAutoFitText;
            }
            set
            {
                m_bAutoFitText = value;
            }
        }

        //----------------------------------------
        public int MargeVerticale
        {
            get
            {
                return m_nMargeVerticale;
            }
            set
            {
                m_nMargeVerticale = value;
                CalcPath();
                Refresh();
            }
        }

        //----------------------------------------
        protected override void Dispose(bool disposing)
        {
            if (!disposing && m_path != null)
            {
                m_path.Dispose();
                m_path = null;
                if (m_player != null)
                    m_player.Dispose();
                m_player = null;
            }

            base.Dispose(disposing);
        }

        //----------------------------------------
        protected override void OnClick(EventArgs e)
        {
            if (!m_gestionnaireModeEdition.ModeEdition)
                return;

            if (m_bActivateSound && m_player != null)
                m_player.Play();
            Color c = BackColor;
            Color textOldColor = ForeColor;
            BackColor = ClickedColor;
            ForeColor = ClickedTextColor;
            Refresh();
            base.OnClick(e);
            
            if (BackColor == ClickedColor)
            {
                BackColor = c;
            }
            ForeColor = textOldColor;
        }

        //----------------------------------------
        public Color ClickedColor
        {
            get
            {
                return m_clickedColor;
            }
            set
            {
                m_clickedColor = value;
            }
        }

        //----------------------------------------
        public Color ClickedTextColor
        {
            get
            {
                return m_clickedTextColor;
            }
            set
            {
                m_clickedTextColor = value;
            }
        }

        //----------------------------------------
        public Color CheckedColor
        {
            get
            {
                return m_checkedColor;
            }
            set
            {
                m_checkedColor = value;
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (m_path != null && m_path.GetBounds().Contains(new Point(mevent.X, mevent.Y)))
                base.OnMouseUp(mevent);
        }


        [Browsable(false)]
        private int RayonApplique
        {
            get
            {
                return Math.Min(Math.Min(Rayon, (ClientSize.Width-MargeHorizontale*2) / 2), 
                    (ClientSize.Height-MargeVerticale*2) / 2);
            }
        }

        public int Rayon
        {
            get
            {
                return m_nRayon;
            }
            set
            {
                m_nRayon = value;
                CalcPath();
            }
        }

        public int PctColor
        {
            get
            {
                return m_nPctCouleur;
            }
            set
            {
                m_nPctCouleur = value;
                Refresh();
            }
        }

        public bool Checked
        {
            get
            {
                return m_bChecked;
            }
            set
            {
                m_bChecked = value;
                Refresh();
            }
        }

        private void InitializeComponent()
        {
            this.m_gestionnaireModeEdition = new sc2i.win32.common.CExtModeEdition();
            this.SuspendLayout();
            // 
            // m_gestionnaireModeEdition
            // 
            this.m_gestionnaireModeEdition.ModeEdition = true;
            // 
            // CRoundButton
            // 
            this.m_gestionnaireModeEdition.SetModeEdition(this, sc2i.win32.common.TypeModeEdition.Autonome);
            this.TextChanged += new System.EventHandler(this.CRoundButton_TextChanged);
            this.Move += new System.EventHandler(this.CRoundButton_Move);
            this.SizeChanged += new System.EventHandler(this.CRoundButton_SizeChanged);
            this.ResumeLayout(false);

        }

        private Color Eclaircir(Color color, int nPct)
        {
            byte nR = (byte)Math.Min(color.R + color.R * nPct / 100, 255);
            byte nG = (byte)Math.Min(color.G + color.G * nPct / 100, 255);
            byte nB = (byte)Math.Min(color.B + color.B * nPct / 100, 255);
            return Color.FromArgb(color.A, nR, nG, nB);

        }

        private Color Assombrir(Color color, int nPct)
        {
            byte nR = (byte)Math.Max(color.R - color.R * nPct / 100, 0);
            byte nG = (byte)Math.Max(color.G - color.G * nPct / 100, 0);
            byte nB = (byte)Math.Max(color.B - color.B * nPct / 100, 0);
            return Color.FromArgb(color.A, nR, nG, nB);
        }

        private GraphicsPath GetRoundRectPath(
            float fX,
            float fY,
            float fWidth,
            float fHeight,
            float fRadius)
        {
            GraphicsPath gp = new GraphicsPath();

            gp.AddLine(fX + fRadius, fY, fX + fWidth - (fRadius), fY); // Line
            if (fRadius > 0)
                gp.AddArc(fX + fWidth - (fRadius * 2), fY, fRadius * 2, fRadius * 2, 270, 90); // Corner
            gp.AddLine(fX + fWidth, fY + fRadius, fX + fWidth, fY + fHeight - (fRadius)); // Line
            if (fRadius > 0)
                gp.AddArc(fX + fWidth - (fRadius * 2), fY + fHeight - (fRadius * 2), fRadius * 2, fRadius * 2, 0, 90); // Corner
            gp.AddLine(fX + fWidth - (fRadius), fY + fHeight, fX + fRadius, fY + fHeight); // Line
            if (fRadius > 0)
                gp.AddArc(fX, fY + fHeight - (fRadius * 2), fRadius * 2, fRadius * 2, 90, 90); // Corner
            gp.AddLine(fX, fY + fHeight - (fRadius), fX, fY + fRadius); // Line
            if (fRadius > 0)
                gp.AddArc(fX, fY, fRadius * 2, fRadius * 2, 180, 90); // Corner
            gp.CloseFigure();
            return gp;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Rectangle rct = ClientRectangle;
            if (m_path == null)
                CalcPath();

            bool bClick = (Control.MouseButtons & MouseButtons.Left) == MouseButtons.Left;
            bool bMouseOver = false;
            Point pt = PointToClient(Cursor.Position);
            bMouseOver = m_path.GetBounds().Contains(pt);
            bClick &= bMouseOver;

            if (Parent != null)
            {
                Control parent = Parent;
                while ( parent != null && parent.BackColor.A == 0 )
                    parent = parent.Parent;
                if (parent != null)
                {
                    Brush br = new SolidBrush(parent.BackColor);
                    pevent.Graphics.FillRectangle(br, ClientRectangle);
                    br.Dispose();
                }
            }

            Color cFond = !m_bChecked ? BackColor : CheckedColor;
            cFond = bClick ? ClickedColor : cFond;

            Color fonce = Assombrir(cFond, m_nPctCouleur);
            //Color clair = Eclaircir(cFond, m_nPctCouleur);

            Rectangle rctFond = rct;

            Color tresFonce = Assombrir(cFond, 75);

            Brush brGradient;
            if (!m_bChecked)
                brGradient = new LinearGradientBrush(rct, cFond, fonce, 90);
            else
                brGradient = new LinearGradientBrush(rct, fonce, cFond, 90);


            pevent.Graphics.FillPath(brGradient, m_path);
            brGradient.Dispose();

            if(!m_bChecked)
            brGradient = new LinearGradientBrush(
                new Point(rct.Left, rct.Top),
                new Point(rct.Left, rct.Bottom),
                Color.FromArgb(128, cFond), Color.FromArgb(128, Color.White));
            else
                brGradient = new LinearGradientBrush(
                    new Point(rct.Left, rct.Top),
                    new Point(rct.Left, rct.Bottom),
                    Color.FromArgb(128, Color.White), Color.FromArgb(128, cFond));

            Pen p = new Pen(brGradient, 3);
            pevent.Graphics.DrawPath(p, m_path);
            brGradient.Dispose();
            p.Dispose();

            Rectangle rctText = rct;
            rctText.Inflate(-10, -10);
            Brush brTexte = new SolidBrush(m_bChecked? ClickedTextColor: ForeColor);
            StringFormat sf = new StringFormat();
            switch (TextAlign)
            {
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.BottomLeft:
                case ContentAlignment.TopLeft:
                    sf.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.BottomCenter:
                case ContentAlignment.TopCenter:
                    sf.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                case ContentAlignment.TopRight:
                    sf.Alignment = StringAlignment.Far;
                    break;
            }
            switch (TextAlign)
            {
                case ContentAlignment.BottomCenter:
                case ContentAlignment.BottomLeft:
                case ContentAlignment.BottomRight:
                    sf.LineAlignment = StringAlignment.Far;
                    break;
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleRight:
                    sf.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.TopCenter:
                case ContentAlignment.TopLeft:
                case ContentAlignment.TopRight:
                    sf.LineAlignment = StringAlignment.Near;
                    break;
            }
            CalcTextSize();
            Font ft = new Font(Font.FontFamily, m_fCurrentPoliceSize);
            Rectangle rectangleTexte = TextRectangle;
            if(m_bChecked)
                rectangleTexte.Offset(-10, 0);
            pevent.Graphics.DrawString(Text, ft, brTexte, rectangleTexte, sf);

            ft.Dispose();
            brTexte.Dispose();

            // Dessine une coche à droite si le bouton est checked
            if (m_bChecked)
            {
                Image imgCheck  = sc2i.win32.common.Properties.Resources.check_noir_simple_25x25;
                int nXpoint = TextRectangle.Right - imgCheck.Width - 10;
                int nYpoint = ClientRectangle.Y + ((ClientRectangle.Height - imgCheck.Height) / 2);
                pevent.Graphics.DrawImage(imgCheck, new Point(nXpoint, nYpoint));
            }

            if (Image != null)
            {
                int nX = (ClientRectangle.Width - Image.Width) / 2;
                int nY = (ClientRectangle.Height - Image.Height) / 2;
                pevent.Graphics.DrawImage(Image, new Point(nX, nY));
            }

        }


        private void CRoundButton_SizeChanged(object sender, EventArgs e)
        {
            CalcPath();
            CalcTextSize();
        }

        private void CRoundButton_TextChanged(object sender, EventArgs e)
        {
            CalcTextSize();
        }

        private Rectangle TextRectangle
        {
            get
            {
                Rectangle rct = ClientRectangle;
                rct.Inflate((int)(-RayonApplique / 8), (int)(-RayonApplique / 8));
                return rct;
            }
        }



        private void CalcTextSize()
        {
            Rectangle rctText = TextRectangle;
            Graphics g = CreateGraphics();
            float fSizeFont = Font.Size;
            Font ft = new Font(Font.FontFamily, fSizeFont);
            SizeF sz = g.MeasureString(Text, ft, rctText.Width);
            while ((sz.Width > rctText.Width || sz.Height > rctText.Height) && fSizeFont > 3)
            {
                fSizeFont -= 0.2f;
                ft.Dispose();
                ft = new Font(Font.FontFamily, fSizeFont);
                sz = g.MeasureString(Text, ft, rctText.Width);
            }
            m_fCurrentPoliceSize = fSizeFont;
            ft.Dispose();
            g.Dispose();
        }

        private void CRoundButton_Move(object sender, EventArgs e)
        {
            CalcPath();
        }

        private void CalcPath()
        {
            if (m_path != null)
                m_path.Dispose();
            m_path = null;
            Rectangle rct = ClientRectangle;
            m_path = GetRoundRectPath(
               rct.Left + MargeHorizontale,
               rct.Top + MargeVerticale,
               rct.Width - 1 - MargeHorizontale * 2, rct.Height - 1 - MargeVerticale * 2,
               RayonApplique);
        }

        #region IControlALockEdition Membres

        public bool LockEdition
        {
            get
            {
                return !m_gestionnaireModeEdition.ModeEdition;
            }
            set
            {
                m_gestionnaireModeEdition.ModeEdition = !value;
                if (OnChangeLockEdition != null)
                    OnChangeLockEdition(this, new EventArgs());
            }
        }

        public event EventHandler OnChangeLockEdition;

        #endregion
    }
}
