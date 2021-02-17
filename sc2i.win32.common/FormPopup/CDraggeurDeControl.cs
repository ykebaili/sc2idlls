using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace sc2i.win32.common
{
	public class CDraggeurDeControl : Component
	{
		private Control m_ctrl;
		public Control Controle
		{
			get
			{
				return m_ctrl;
			}
			set
			{
				if (m_ctrl != value)
				{

					if (value == null)
					{
						m_ctrl.MouseMove -= new MouseEventHandler(m_ctrl_MouseMove);
						m_ctrl.MouseDown -= new MouseEventHandler(m_ctrl_MouseDown);
						m_ctrl.MouseUp -= new MouseEventHandler(m_ctrl_MouseUp);
					}
					else
					{
						value.MouseMove += new MouseEventHandler(m_ctrl_MouseMove);
						value.MouseDown += new MouseEventHandler(m_ctrl_MouseDown);
						value.MouseUp += new MouseEventHandler(m_ctrl_MouseUp);

					}
					m_ctrl = value;

				}
			}
		}

		private bool m_bInDraggMode = false;
		private Point m_ptOffset = Point.Empty;
		private void m_ctrl_MouseMove(object sender, MouseEventArgs e)
		{
			if (m_bInDraggMode)
			{
				Point pt = Control.MousePosition;
				pt.Offset(m_ptOffset);
				m_ctrl.Location = pt;
			}
		}

		private void m_ctrl_MouseDown(object sender, MouseEventArgs e)
		{
			m_bInDraggMode = true;
			m_ptOffset = new Point(-e.X - 4, -e.Y - 4);
		}

		private void m_ctrl_MouseUp(object sender, MouseEventArgs e)
		{
			m_bInDraggMode = false;
		}

	}
}
