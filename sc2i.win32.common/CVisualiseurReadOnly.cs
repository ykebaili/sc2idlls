using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Drawing;
using System.Drawing.Imaging;
using sc2i.win32.common.Properties;

namespace sc2i.win32.common
{
    public class CVisualiseurReadOnly : Control
    {
        private Control m_controlAssocie = null;

        private EventHandler m_onControlResize;
        private EventHandler m_onControlMove;


        //----------------------------------------------------------------------------
        public CVisualiseurReadOnly()
        {
            m_onControlResize = new EventHandler(m_controlAssocie_Resize);
            m_onControlMove = new EventHandler(m_controlAssocie_Move);
            Visible = false;
            VisibleChanged += new EventHandler(CVisualiseurReadOnly_VisibleChanged);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();
            DoubleBuffered = true;

        }

        //----------------------------------------------------------------------------
        private bool m_bInVisibleChange = false;
        void CVisualiseurReadOnly_VisibleChanged(object sender, EventArgs e)
        {
            if (m_bInVisibleChange)
                return;
            m_bInVisibleChange = true;
            if (m_controlAssocie != null /*&& m_controlAssocie.Width > 32*/)
            {
                if (Visible)
                    BringToFront();
            }
            else
            {
                if (Visible)
                    Visible = false;
            }
            m_bInVisibleChange = false;
        }


        //----------------------------------------------------------------------------
        public Control ControlAssocie
        {
            get
            {
                return m_controlAssocie;
            }
            set
            {
                if (value != m_controlAssocie)
                {
                    if (m_controlAssocie != null)
                    {
                        m_controlAssocie.Resize -= m_onControlResize;
                        m_controlAssocie.Move -= m_onControlMove;
                        if (Parent != null)
                            Parent.Controls.Remove(this);
                    }
                    m_controlAssocie = value;
                    if (value != null)
                    {
                        m_controlAssocie.Resize += m_onControlResize;
                        m_controlAssocie.Move += m_onControlMove;
                        if (m_controlAssocie.Parent != null)
                        {
                            m_controlAssocie.Parent.Controls.Add(this);
                        }
                    }
                    AjustePosition();
                }
            }
        }

        //----------------------------------------------------------------------------
        void m_controlAssocie_Move(object sender, EventArgs e)
        {
            AjustePosition();
        }

        //----------------------------------------------------------------------------
        void m_controlAssocie_Resize(object sender, EventArgs e)
        {
            AjustePosition();
        }

        //----------------------------------------------------------------------------
        private void AjustePosition()
        {
            if (m_controlAssocie != null)
            {
                Location = new Point(
                    m_controlAssocie.Left,
                    m_controlAssocie.Top);
                Size = new Size(16,16);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CVisualiseurReadOnly
            // 
            this.BackColor = System.Drawing.Color.Black;
            this.TabStop = false;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CVisualiseurReadOnly_Paint);
            this.ResumeLayout(false);

        }

        //------------------------------------------------------------------------
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //base.OnPaintBackground(pevent);
        }


        //------------------------------------------------------------------------
        private void CVisualiseurReadOnly_Paint(object sender, PaintEventArgs e)
        {
            if (Visible && ControlAssocie != null)
            {
                Bitmap bmp = new Bitmap(ControlAssocie.Width, ControlAssocie.Height);
                ControlAssocie.DrawToBitmap(bmp, ControlAssocie.ClientRectangle);

                int nX = ControlAssocie.Location.X - Location.X;
                int nY = ControlAssocie.Location.Y - Location.Y;
                e.Graphics.DrawImage(bmp, new Point(nX, nY));
                bmp.Dispose();

                ColorMatrix cm = new ColorMatrix();
                cm.Matrix33 = m_controlAssocie.Width < 32?0.2f:0.375f;
                ImageAttributes ia = new ImageAttributes();
                ia.SetColorMatrix(cm);
                Image img = Resources.LockEnCoin;
                e.Graphics.DrawImage(img,
                    new Rectangle(0, 0, img.Width, img.Height),
                    0, 0, img.Width, img.Height,
                    GraphicsUnit.Pixel, ia);
                ia.Dispose();
                /*
                List<Point> lst = new List<Point>();
                lst.Add(new Point(0, 0));
                lst.Add(new Point(16, 0));
                lst.Add(new Point(0, 16));
                lst.Add(new Point(0, 0));
                Brush br = new SolidBrush(Color.FromArgb(128, 255, 0, 0));
                e.Graphics.FillPolygon(br, lst.ToArray());
                br.Dispose();*/
            }
        }

    }
}
