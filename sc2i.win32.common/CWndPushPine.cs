using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using sc2i.win32.common.Properties;

namespace sc2i.win32.common
{
    public class CWndPushPine : PictureBox
    {
        private bool m_bIsPined = false;

        public CWndPushPine()
            : base()
        {
            InitializeComponent();
            m_bIsPined = false;
            UpdateLook();
            Size = Resources.pusphpinOuverte.Size;
            SizeMode = PictureBoxSizeMode.AutoSize;
            Cursor = Cursors.Hand;
        }

        public bool Pined
        {
            get{
                return m_bIsPined;
            }
            set{
                if (value != m_bIsPined)
                {
                    m_bIsPined = value;
                    UpdateLook();
                    if (PineChanged != null)
                        PineChanged(this, new EventArgs());
                }
            }
        }

        private void UpdateLook()
        {
            if (m_bIsPined)
                Image = Resources.pushpinFermee;
            else
                Image = Resources.pusphpinOuverte;
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // CWndPushPine
            // 
            this.Click += new System.EventHandler(this.CWndPushPine_Click);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        private void CWndPushPine_Click(object sender, EventArgs e)
        {
            Pined = !Pined;
        }

        public event EventHandler PineChanged;
    }
}
