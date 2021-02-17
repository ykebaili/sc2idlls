using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sc2i.formulaire.win32
{
    public partial class CDelayedButton : UserControl
    {
        private int m_nDelayInSeconds = 60;

        private DateTime m_lastDateStart;

        public CDelayedButton()
        {
            InitializeComponent();
            m_lastDateStart = DateTime.Now;
            RefreshProgress();
            m_timer.Enabled = Enabled;
            m_timer.Start();
        }

        //---------------------------------------------------
        public override string Text
        {
            get
            {
                return m_button.Text;
            }
            set
            {
                m_button.Text = value;
            }
        }

        //---------------------------------------------------
        public ContentAlignment TextAlign
        {
            get
            {
                return m_button.TextAlign;
            }
            set
            {
                m_button.TextAlign = value;
            }
        }

        //---------------------------------------------------
        public int DelayInSeconds
        {
            get
            {
                return m_nDelayInSeconds;
            }
            set
            {
                m_nDelayInSeconds = value;
                RefreshProgress();
            }
        }

        //---------------------------------------------------
        private void CDelayedButton_EnabledChanged(object sender, EventArgs e)
        {
            m_timer.Enabled = Enabled;

        }

        //---------------------------------------------------
        protected override void OnClick(EventArgs e)
        {
            m_timer.Stop();
            base.OnClick(e);
            m_lastDateStart = DateTime.Now;
            if (m_timer != null)
            //Si le timer est null, c'est qu'il a été disposé à cause
            //du click (par exemple,si le click affiche une fenêtre
            {
                m_timer.Start();
                RefreshProgress();
            }
        }

        //---------------------------------------------------
        private void m_timer_Tick(object sender, EventArgs e)
        {
            TimeSpan sp = DateTime.Now - m_lastDateStart;
            RefreshProgress();
            if (sp.TotalSeconds >= m_nDelayInSeconds && m_timer.Enabled)
            {

                if (m_nDelayInSeconds > 0)
                    OnClick(null);

            }
        }

        //---------------------------------------------------
        private void RefreshProgress()
        {
            if (m_nDelayInSeconds == 0)
                m_panelFondProgress.Visible = false;
            else
            {
                m_panelFondProgress.Visible = true;

                TimeSpan sp = DateTime.Now - m_lastDateStart;
                int nSec = (int)sp.TotalSeconds;
                if (m_nDelayInSeconds > 0)
                {
                    int nWidth = nSec * Width / m_nDelayInSeconds;
                    if (nWidth > 0)
                    {
                        m_lblProgress.Width = nWidth;
                        m_lblProgress.Visible = true;
                    }
                    else
                        m_lblProgress.Visible = false;
                }
            }
        }

        //---------------------------------------------------
        public void Restart()
        {
            m_lastDateStart = DateTime.Now;
            RefreshProgress();
        }

        //---------------------------------------------------
        private void m_button_Click(object sender, EventArgs e)
        {
            OnClick(null);
        }

        //----------------------------------------------------------------------------
        private void CDelayedButton_BackColorChanged(object sender, EventArgs e)
        {
            m_button.BackColor = BackColor;
        }

        //----------------------------------------------------------------------------
        private void CDelayedButton_FontChanged(object sender, EventArgs e)
        {
            m_button.Font = Font;
        }

        //----------------------------------------------------------------------------
        private void CDelayedButton_ForeColorChanged(object sender, EventArgs e)
        {
            m_button.ForeColor = ForeColor;
        }

        private void m_panelFondProgress_Click(object sender, EventArgs e)
        {
            if (m_nDelayInSeconds >= 0)
                m_timer.Enabled = !m_timer.Enabled;
        }

    }
}
