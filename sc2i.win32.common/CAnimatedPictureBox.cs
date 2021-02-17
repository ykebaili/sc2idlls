using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace sc2i.win32.common
{
    public enum EAnimationOption
    {
        Loop = 0,
        Reverse,
        None
    }

    public class CAnimatedPictureBox : PictureBox
    {
        private int m_nCurrentFrame = 0;
        private int m_nSens = 1;
        private int m_nNbFrames = 1;
        private FrameDimension m_dimension = null;

        private Image m_imageToDraw = null;

        private Image m_imageAnimee = null;
        private System.ComponentModel.IContainer components;
        private Timer m_timer;

        public EAnimationOption m_options = EAnimationOption.Loop;
        

        private int m_nDelaiMs = 100;

        public CAnimatedPictureBox()
            : base()
        {
            InitializeComponent();
            m_timer.Interval = m_nDelaiMs;
            m_timer.Stop();
        }

        protected override void Dispose(bool disposing)
        {
            if (m_timer != null)
            {
                m_timer.Stop();
                m_timer.Dispose();
                m_timer = null;
            }
            if ( m_imageAnimee != null )
            {
                m_imageAnimee.Dispose();
                m_imageAnimee = null;
            }
            base.Dispose(disposing);
            
        }

        void m_timer_Tick(object sender, EventArgs e)
        {
            if (m_imageAnimee == null || m_dimension == null)
            {
                m_timer.Stop();
                return;
            }
            m_nCurrentFrame+= m_nSens;
            if (m_nCurrentFrame >= m_nNbFrames)
            {
                switch (m_options)
                {
                    case EAnimationOption.Loop:
                        m_nCurrentFrame = 0;
                        break;
                    case EAnimationOption.Reverse:
                        m_nSens = -1;
                        m_nCurrentFrame--;

                        break;
                    case EAnimationOption.None:
                        m_timer.Stop();
                        return;
                        break;
                    default:
                        break;
                }
            }
            if ( m_nCurrentFrame < 0 )
            {
                m_nCurrentFrame = 0;
                m_nSens = 1;
                if ( m_options == EAnimationOption.Reverse )
                    m_nCurrentFrame++;
            }
                    
            m_imageAnimee.SelectActiveFrame(m_dimension, m_nCurrentFrame);
            m_imageToDraw = m_imageAnimee;
        }

        public Image AnimatedImage
        {
            get
            {
                return m_imageAnimee;
            }
            set
            {
                m_imageAnimee = value;
                InitAnimation();
            }
        }

        private void InitAnimation()
        {
            m_timer.Stop();
            if (m_imageAnimee == null)
                return;
            m_dimension = new FrameDimension(m_imageAnimee.FrameDimensionsList[0]);
            if (m_dimension == null )
            {
                Image = m_imageAnimee;
                return;
            }
            m_nNbFrames = m_imageAnimee.GetFrameCount(m_dimension);
            m_nCurrentFrame = 0;
            m_timer.Interval = m_nDelaiMs;
            //m_timer.Start();
        }

        public int AnimationDelay
        {
            get
            {
                return m_nDelaiMs;
            }
            set
            {
                m_nDelaiMs = value;
                InitAnimation();
            }
        }

        public EAnimationOption AnimationOption
        {
            get{
                return m_options;
            }
            set{
                m_options = value;
                m_nSens = 1;
            }
        }
        

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.m_timer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // m_timer
            // 
            this.m_timer.Tick += new System.EventHandler(this.m_timer_Tick);
            // 
            // CAnimatedPictureBox
            // 
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CAnimatedPictureBox_Paint);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        private void CAnimatedPictureBox_Paint(object sender, PaintEventArgs e)
        {
           /* if ( m_imageToDraw != null )
                e.Graphics.DrawImageUnscaled(Image, new Point(0, 0))*/
        }



        



    }
}
