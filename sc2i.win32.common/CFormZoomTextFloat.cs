using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace sc2i.win32.common
{
	public partial class CFormZoomTextFloat : CFloatingFormBase
	{
		private bool m_bLockEdition = false;
		public CFormZoomTextFloat()
		{
			InitializeComponent();
		}

		public static string Show(string strText, int nWidth, bool bLockEdition, Color backColor)
		{
			CFormZoomTextFloat form = new CFormZoomTextFloat();
			Graphics g = form.CreateGraphics();
			SizeF sz = g.MeasureString(strText, form.Font, nWidth-5);
			form.Size = new Size(Math.Max(nWidth,50), Math.Max((int)sz.Height+10, 21));
			form.m_txtBox.Text = strText;
            form.m_txtBox.BackColor = backColor;
			form.m_txtBox.SelectionLength = 0;
			form.m_txtBox.SelectionStart = 0;
			form.m_bLockEdition = bLockEdition;
			form.Show();
			return form.m_txtBox.Text;
		}

		private void CFormZoomTextFloat_Load(object sender, EventArgs e)
		{

		}

		private void m_txtBox_KeyDown(object sender, KeyEventArgs e)
		{
				if (m_bLockEdition &&
					e.KeyCode != Keys.Up &&
					e.KeyCode != Keys.Down &&
					e.KeyCode != Keys.Right &&
					e.KeyCode != Keys.Left &&
					e.KeyCode != Keys.PageDown &&
					e.KeyCode != Keys.PageUp &&
					e.KeyCode != Keys.End &&
					e.KeyCode != Keys.Home)
				e.Handled = true;
		}

		private void m_txtBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (m_bLockEdition)
				e.Handled = true;
		}
	}
}