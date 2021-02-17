using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace sc2i.win32.common
{
    public class CDegradeTitleBar : Label, IControlALockEdition
    {
        private Color m_backColor2 = Color.FromArgb ( 205, 210, 224);
        private ImageList m_imageList;
        private System.ComponentModel.IContainer components;
        private bool m_bIsCollapse = false;

        public CDegradeTitleBar()
            :base()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CDegradeTitleBar));
            this.m_imageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // m_imageList
            // 
            this.m_imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imageList.ImageStream")));
            this.m_imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imageList.Images.SetKeyName(0, "minimoins.png");
            this.m_imageList.Images.SetKeyName(1, "miniplus.png");
            // 
            // CDegradeTitleBar
            // 
            this.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CDegradeTitleBar_Paint);
            this.ResumeLayout(false);

        }

        //---------------------------------------
        public Color BackColorGradient
        {
            get
            {
                return m_backColor2;
            }
            set
            {
                m_backColor2 = value;
                Refresh();
            }
        }

        //---------------------------------------
        public bool IsCollapse
        {
            get
            {
                return m_bIsCollapse;
            }
            set
            {
                m_bIsCollapse = value;
                Refresh();
            }
        }

        private void CDegradeTitleBar_Paint(object sender, PaintEventArgs e)
        {
            Brush br = new LinearGradientBrush(new Point(0, 0),
                new Point(0, ClientSize.Height), BackColor, BackColorGradient);
            e.Graphics.FillRectangle(br, ClientRectangle);
            br.Dispose();

            
            int nIndex = m_bIsCollapse ? 1 : 0;
            Image bmpPlusMois = m_imageList.Images[nIndex];

            Rectangle rctImage = new Rectangle(0, 0, bmpPlusMois.Width, bmpPlusMois.Height);
            rctImage.Offset(2, ClientSize.Height / 2 - rctImage.Height / 2);
            e.Graphics.DrawImage(bmpPlusMois, rctImage);

            



            br = new SolidBrush(ForeColor);
            TextFormatFlags flags;
            switch ( TextAlign )
            {
                case ContentAlignment.BottomCenter:
                case ContentAlignment.BottomLeft:
                case ContentAlignment.BottomRight:
                    flags = TextFormatFlags.Bottom;
                    break;
                case ContentAlignment.TopCenter:
                case ContentAlignment.TopLeft:
                case ContentAlignment.TopRight :
                    flags = TextFormatFlags.Top;
                    break;
                default:
                    flags = TextFormatFlags.VerticalCenter;
                    break;
            }
            switch ( TextAlign )
            {
                case ContentAlignment.BottomLeft:
                case ContentAlignment.TopLeft:
                case ContentAlignment.MiddleLeft :
                    flags |= TextFormatFlags.Left;
                    break;
                case ContentAlignment.BottomRight:
                case ContentAlignment.TopRight:
                case ContentAlignment.MiddleRight :
                    flags |= TextFormatFlags.Right;
                    break;
                default:
                    flags |= TextFormatFlags.HorizontalCenter;
                    break;
            }
            flags |= TextFormatFlags.WordBreak;

            Rectangle rcText = new Rectangle(rctImage.Right + 3, ClientRectangle.Top,
                ClientRectangle.Width - rctImage.Right - 3, ClientRectangle.Height);
            TextRenderer.DrawText (e.Graphics, Text, Font, rcText, ForeColor, Color.Transparent, flags );
        }




        #region IControlALockEdition Membres

        public bool LockEdition
        {
            get
            {
                return false;
            }
            set
            {
                if (OnChangeLockEdition != null)
                    OnChangeLockEdition(this, new EventArgs());
            }
        }

        public event EventHandler OnChangeLockEdition;

        #endregion
    }
}
