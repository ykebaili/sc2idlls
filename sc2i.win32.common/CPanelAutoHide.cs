using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using sc2i.common;

namespace sc2i.win32.common
{
    public class CPanelAutoHide : Panel
    {
        private Size m_sizeDeployee;
        private bool m_bIsHidden = false;
        private Timer m_timerHide;
        private System.ComponentModel.IContainer components;
        private bool m_bIsAutoHide = false;
        private Timer m_timerAnime;
        private int m_nHiddenSize = 20;

        private int m_nIncAnim = 50;

        //Activé au premier passage de souris pour éviter de sauver les préférences qui sont en dur dans le code
        private bool m_bEnableSavePreference = false;

        private Size m_sizeDestAnim;

        public CPanelAutoHide()
            :base()
        {
            InitializeComponent();
            m_sizeDeployee = Size;
            m_timerAnime.Stop();

        }

        private string RegistreKey
        {
            get
            {
                StringBuilder bl = new StringBuilder();
                Control parent = this;
                while (parent != null)
                {
                    bl.Append(parent.Name);
                    parent = parent.Parent;
                }
                return bl.ToString();
            }
        }


        //---------------------------------
        public void ReadPreference()
        {
            string strVal = C2iRegistre.GetValueInRegistreApplication("Preferences\\AutoHide", RegistreKey, "NONE");
            if ( strVal != "NONE" )
                AutoHide = strVal == "1";
        }

        //---------------------------------
        public void SavePreference()
        {
            C2iRegistre.SetValueInRegistreApplication("Preferences\\AutoHide", RegistreKey, AutoHide ? "1" : "0");
        }

        //---------------------------------
        public bool AutoHide
        {
            get
            {
                return m_bIsAutoHide;
            }
            set
            {
                m_bIsAutoHide = value;
                m_timerHide.Stop();
                if (!DesignMode)
                {
                    if (value)
                        m_timerHide.Start();
                }
                else
                    SetNotHidden(false);
                if (m_bEnableSavePreference )
                    SavePreference();
                if (AutoHideChanged != null)
                    AutoHideChanged(this, new EventArgs());
            }
        }

        public event EventHandler AutoHideChanged;

        //---------------------------------
        public bool IsHidden
        {
            get
            {
                return m_bIsHidden;
            }
        }

        //---------------------------------
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.m_timerHide = new System.Windows.Forms.Timer(this.components);
            this.m_timerAnime = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // m_timerHide
            // 
            this.m_timerHide.Interval = 200;
            this.m_timerHide.Tick += new System.EventHandler(this.m_timerHide_Tick);
            // 
            // m_timerAnime
            // 
            this.m_timerAnime.Interval = 25;
            this.m_timerAnime.Tick += new System.EventHandler(this.m_timerAnime_Tick);
            // 
            // CPanelAutoHide
            // 
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CPanelAutoHide_MouseMove);
            this.SizeChanged += new System.EventHandler(this.CPanelAutoHide_SizeChanged);
            this.ResumeLayout(false);

        }

        //---------------------------------
        private DateTime m_dateDemandeAffichage;
        private bool m_bISInDelaiAffichage = false;
        private void m_timerHide_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!m_timerAnime.Enabled)
                {
                    Point pt = Cursor.Position;
                    Rectangle rct = ClientRectangle;
                    rct = new Rectangle(PointToScreen(rct.Location), rct.Size);
                    rct.Inflate(new Size(5, 5));

                    bool bAgir = false;
                    bAgir = rct.Contains(pt) && IsHidden ||
                            !rct.Contains(pt) && !IsHidden;
                    if (bAgir)
                    {
                        if (m_bISInDelaiAffichage)
                        {
                            m_bEnableSavePreference = true;
                            if (((TimeSpan)(DateTime.Now - m_dateDemandeAffichage)).TotalMilliseconds > 300)
                            {
                                if (IsHidden)
                                    SetNotHidden(true);
                                else
                                    SetHidden(true);
                            }
                        }
                        else
                        {
                            m_dateDemandeAffichage = DateTime.Now;
                            m_bISInDelaiAffichage = true;
                        }
                    }
                    else
                        m_bISInDelaiAffichage = false;
                }
            }
            catch { }
        }

        //---------------------------------
        public void SetNotHidden(bool bAnimate)
        {
            if (IsHidden)
            {
                m_bIsHidden = false;
                if (HiddenChanged != null)
                    HiddenChanged(this, new EventArgs());
                if (bAnimate)
                    SizeToAnim = m_sizeDeployee;
                else
                    Size = m_sizeDeployee;
                m_timerHide.Stop();
                m_timerHide.Start();
            }
        }

        //---------------------------------
        private Size SizeToAnim
        {
            set
            {
                m_sizeDestAnim = value;
                m_timerAnime.Start();
            }
        }

        //---------------------------------
        public void SetHidden( bool bAnimate )
        {
            if (!IsHidden)
            {
                m_bIsHidden = true;
                if (HiddenChanged != null)
                    HiddenChanged(this, new EventArgs());
                m_sizeDeployee = Size;
                Size newSize = Size;
                switch (Dock)
                {
                    case DockStyle.Bottom:
                    case DockStyle.Top:
                        newSize = new Size(Size.Width, m_nHiddenSize);
                        break;
                    case DockStyle.Left:
                    case DockStyle.Right:
                        newSize = new Size(m_nHiddenSize, Size.Height);
                        break;
                }
                if (bAnimate)
                    SizeToAnim = newSize;
                else
                    Size = newSize;
                m_timerHide.Stop();
                m_timerHide.Start();
            }
        }

        //---------------------------------------------------
        private void CPanelAutoHide_SizeChanged(object sender, EventArgs e)
        {
            if (!IsHidden && !m_timerAnime.Enabled)
                m_sizeDeployee = Size;
        }

        //---------------------------------------------------
        public int HiddenSize
        {
            get
            {
                return m_nHiddenSize;
            }
            set
            {
                m_nHiddenSize = value;
            }
        }

        public event EventHandler HiddenChanged;

        private void m_timerAnime_Tick(object sender, EventArgs e)
        {
            switch (Dock)
            {
                case DockStyle.Bottom:
                case DockStyle.Top:
                    if (m_sizeDestAnim.Height < Size.Height)
                        Size = new Size ( Size.Width, Math.Max ( Size.Height - m_nIncAnim, m_sizeDestAnim.Height ));
                    else
                        Size = new Size ( Size.Width, Math.Min ( Size.Height + m_nIncAnim, m_sizeDestAnim.Height ));
                    if ( Size.Height == m_sizeDestAnim.Height )
                        m_timerAnime.Stop();
                    break;
                case DockStyle.Left:
                case DockStyle.Right:
                    if (m_sizeDestAnim.Width < Size.Width)
                        Size = new Size ( Math.Max ( Size.Width - m_nIncAnim, m_sizeDestAnim.Width), Size.Height);
                    else
                        Size = new Size ( Math.Min ( Size.Width + m_nIncAnim, m_sizeDestAnim.Width), Size.Height);
                    if (Size.Width == m_sizeDestAnim.Width)
                        m_timerAnime.Stop();
                    break;
                default:
                    break;
            }
        }

        private void CPanelAutoHide_MouseMove(object sender, MouseEventArgs e)
        {
            m_bEnableSavePreference = true;
        }

        




    }
}
