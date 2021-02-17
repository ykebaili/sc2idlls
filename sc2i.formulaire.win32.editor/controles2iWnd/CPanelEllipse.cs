using System;
using System.Collections.Generic;
using System.Text;
using sc2i.win32.common;
using System.Drawing;
using System.Windows.Forms;

namespace sc2i.formulaire.win32.controles2iWnd
{
	public class CPanelEllipse : C2iPanel
	{
		private bool m_bDrawBorder = true;
		//-------------------------------
		public CPanelEllipse()
		{
			BorderStyle = BorderStyle.None;
		}

		//-------------------------------
		public bool DrawBorder
		{
			get
			{
				return m_bDrawBorder;
			}
			set
			{
				m_bDrawBorder = value;
			}
		}

		//-------------------------------
		protected override CreateParams CreateParams
		{

			get
			{

				CreateParams cp = base.CreateParams;

				cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT

				return cp;

			}

		}

		//-------------------------------
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			Graphics g = e.Graphics;

			Brush b = new SolidBrush(BackColor);

			Rectangle rect = ClientRectangle;
			rect.Width = rect.Width - 1;
			rect.Height = rect.Height - 1;
			g.FillEllipse(b, rect);
			if ( DrawBorder )
			{
				Pen p = new Pen(ForeColor);
				g.DrawEllipse(p, rect);
				p.Dispose();
			}
			b.Dispose();
		}

		//-------------------------------
		protected override void OnPaintBackground(PaintEventArgs e)
		{
		}
	}
}
