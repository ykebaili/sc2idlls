using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace sc2i.win32.common
{
	public class CSizerDeControl : Component
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

		private int m_precision = 2;
		[DefaultValue(2)]
		public int Precision
		{
			get
			{
				return m_precision;
			}
			set
			{
				m_precision = value;
			}
		}
		private void m_ctrl_MouseMove(object sender, MouseEventArgs e)
		{
			int nWidth = m_ctrl.ClientRectangle.Width;
			int nHeight = m_ctrl.ClientRectangle.Height;

			bool bLeft = (e.X >= 0 - Precision) && (e.X <= 0 + Precision);
			bool bRight = (e.X >= nWidth - Precision) && (e.X <= nWidth + Precision);
			bool bTop = (e.Y >= 0 - Precision) && (e.Y <= 0 + Precision);
			bool bBottom = (e.Y >= nHeight - Precision) && (e.Y <= nHeight + Precision);

			//Angle Haut Gauche
			if (bTop && bLeft)
			{
				m_ctrl.Cursor = Cursors.SizeNWSE;
			}
			//Angle Bas Gauche
			else if (bBottom && bLeft)
			{
				m_ctrl.Cursor = Cursors.SizeNESW;
			}
			//Angle Haut Droit
			else if (bTop && bRight)
			{
				m_ctrl.Cursor = Cursors.SizeNESW;
			}
			//Angle Bas Droit
			else if (bBottom && bRight)
			{
				m_ctrl.Cursor = Cursors.SizeNWSE;
			}
			//Bord Gauche
			else if (bLeft)
			{
				m_ctrl.Cursor = Cursors.SizeWE;
			}
			//Bord Droit
			else if (bRight)
			{
				m_ctrl.Cursor = Cursors.SizeWE;
			}
			//Bord Haut
			else if (bTop)
			{
				m_ctrl.Cursor = Cursors.SizeNS;
			}
			//Bord Bas
			else if (bBottom)
			{
				m_ctrl.Cursor = Cursors.SizeNS;
			}
			else
				m_ctrl.Cursor = Cursors.Arrow;
		}

		private void m_ctrl_MouseDown(object sender, MouseEventArgs e)
		{
		}

		private void m_ctrl_MouseUp(object sender, MouseEventArgs e)
		{
		}
	}
}
