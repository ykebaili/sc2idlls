using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace sc2i.win32.common
{
    public class CPictureBoxDraggable : PictureBox
    {
        public CPictureBoxDraggable()
            :base()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // CPictureBoxDraggable
            // 
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CPictureBoxDraggable_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CPictureBoxDraggable_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        private Point m_ptStartDragDrop = new Point(0, 0);
        private void CPictureBoxDraggable_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                m_ptStartDragDrop = new Point(e.X, e.Y);
        }

        private void CPictureBoxDraggable_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Math.Abs(m_ptStartDragDrop.X - e.X) > 3 ||
                    Math.Abs(m_ptStartDragDrop.Y - e.Y) > 3)
                {
                    if (BeginDragDrop != null)
                        BeginDragDrop(this, null);
                }
            }
        }

        public event EventHandler BeginDragDrop;
    }
}
