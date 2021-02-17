using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using sc2i.common;

namespace sc2i.win32.common
{
	public partial class CCtrlShowErreur : UserControl
	{
		public CCtrlShowErreur()
		{
			InitializeComponent();
			SizeChanged += new EventHandler(CCtrlShowErreur_SizeChanged);
			DoubleBuffered = true;
		}
		private bool m_bExtended = false;

		private void CCtrlShowErreur_SizeChanged(object sender, EventArgs e)
		{
			m_timer.Stop();
			m_timer.Start();
			//ActualiserAffichage();	
		}

		private void ActualiserAffichage()
		{
			bool bBoutonVisible = m_bMessageTronque || (m_txtBoxErr.Width + m_panRight.Width <= TextRenderer.MeasureText(Erreur.Message, m_txtBoxErr.Font).Width);
			if (m_panRight.Visible != bBoutonVisible)
				m_panRight.Visible = bBoutonVisible;
			if (m_txtBoxErrSingle.Visible != !Extended)
				m_txtBoxErrSingle.Visible = !Extended;
			Size s = TextRenderer.MeasureText(Erreur.Message, m_txtBoxErr.Font, m_txtBoxErr.Size, TextFormatFlags.TextBoxControl | TextFormatFlags.WordBreak);
			if (Extended)
			{
				int nHeight = s.Height + 5;
				if (Height != nHeight)
					Height = nHeight;
				m_btnElargir.Text = "/\\";

			}
			else
			{
				m_btnElargir.Text = "\\/";
				if (Height != 20)
					Height = 20;
			}


		}

		private IErreur m_err;
		public IErreur Erreur
		{
			get
			{
				return m_err;
			}
		}
		public virtual void Initialiser(IErreur err)
		{
			m_err = err;
			m_txtBoxErr.Text = Erreur.Message;

			int nIdxMax = -1;
			int nIdxR = err.Message.IndexOf("\r");
			int nIdxN = err.Message.IndexOf("\n");
			nIdxMax = Math.Min(nIdxN, nIdxR);
			m_bMessageTronque = nIdxMax != -1;
			if (m_bMessageTronque)
				m_txtBoxErrSingle.Text = err.Message.Substring(0, nIdxMax) + "(...)";
			else
				m_txtBoxErrSingle.Text = err.Message;
			ActualiserAffichage();
		}
		private bool m_bMessageTronque = false;

		public bool Extended
		{
			get
			{
				return m_bExtended;
			}
			set
			{
				m_bExtended = value;
				ActualiserAffichage();

			}
		}
		private void m_btnElargir_Click(object sender, EventArgs e)
		{
			Extended = !Extended;
		}

		private void m_txtBoxErr_MouseLeave(object sender, EventArgs e)
		{
			TextBox txtBox = Extended ? m_txtBoxErr : m_txtBoxErrSingle;
			txtBox.BackColor = Color.White;
			//txtBox.ForeColor = Color.Black;
		}
		private void m_txtBoxErr_MouseEnter(object sender, EventArgs e)
		{
			TextBox txtBox = Extended ? m_txtBoxErr : m_txtBoxErrSingle;
			txtBox.BackColor = Color.LightGray;
			//txtBox.ForeColor = Color.White;
		}

		private void m_btnElargir_MouseEnter(object sender, EventArgs e)
		{
			m_btnElargir.BackColor = Color.LightGray;
			//m_btnElargir.ForeColor = Color.White;
		}
		private void m_btnElargir_MouseLeave(object sender, EventArgs e)
		{
			m_btnElargir.BackColor = Color.White;
			//m_btnElargir.ForeColor = Color.Black;
		}


		public Size PerfectSize
		{
			get
			{
				Size szTextBox = TextRenderer.MeasureText(Erreur.Message, m_txtBoxErr.Font, m_txtBoxErr.Size, TextFormatFlags.TextBoxControl);
				return new Size(szTextBox.Width + m_panLeft.Width + m_panRight.Width, szTextBox.Height);
			}
		}
		public Size GoodSizeUnextended
		{
			get
			{
				Size szTextBox = TextRenderer.MeasureText(Erreur.Message, m_txtBoxErr.Font, m_txtBoxErr.Size, TextFormatFlags.TextBoxControl | TextFormatFlags.WordBreak);
				return new Size(szTextBox.Width + m_panLeft.Width + m_panRight.Width, szTextBox.Height);
			}
		}

		private void m_timer_Tick(object sender, EventArgs e)
		{
			ActualiserAffichage();
			m_timer.Stop();
		}
	}
}
