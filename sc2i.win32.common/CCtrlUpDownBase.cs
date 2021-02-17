using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace sc2i.win32.common
{
	public abstract partial class CCtrlUpDownBase : UserControl
	{
		public event EventHandler AuClic;
		public void OnClic()
		{
			if (AuClic != null)
				AuClic(this, new EventArgs());
		}
		public event EventHandler AuClicPourMonter;
		public event EventHandler AuClicPourDescendre;
		public void OnClicPourDescendre()
		{
			if (AuClicPourDescendre != null)
				AuClicPourDescendre(this, new EventArgs());
		}
		public void OnClicPourMonter()
		{
			if (AuClicPourMonter != null)
				AuClicPourMonter(this, new EventArgs());
		}

		public CCtrlUpDownBase()
		{
			InitializeComponent();
		}

		private void m_btnBas_Click(object sender, EventArgs e)
		{
			Descendre();
		}
		public abstract void Descendre();

		//----------------------------------------------------------
		private void m_btnHaut_Click(object sender, EventArgs e)
		{
			Monter();
		}
		public abstract void Monter();


		//----------------------------------------------------------
		private bool m_bLockEdition = false;
		public bool LockEdition
		{
			get { return m_bLockEdition; }
			set
			{
				m_bLockEdition = value;
				if (!DesignMode)
					Visible = !value;
				if (OnChangeLockEdition != null)
					OnChangeLockEdition(this, new EventArgs());
			}
		}

		public event EventHandler OnChangeLockEdition;

		public enum ESensAction
		{
			Monter,
			Descendre
		}
		private enum EModeBouton
		{
			Survol,
			Clic,
			Base
		}
		private enum EModeBoutonDetaille
		{
			UpSurvol,
			UpClic,
			UpBase,
			DownSurvol,
			DownClic,
			DownBase

		}
		private Bitmap GetBitmap(EModeBoutonDetaille mode)
		{
			switch (mode)
			{
				case EModeBoutonDetaille.UpSurvol:
					return Properties.Resources.up_vert;
				case EModeBoutonDetaille.UpClic:
					return Properties.Resources.up_red;
				case EModeBoutonDetaille.UpBase:
					return Properties.Resources.up_blue;
				case EModeBoutonDetaille.DownSurvol:
					return Properties.Resources.down_vert;
				case EModeBoutonDetaille.DownClic:
					return Properties.Resources.down_red;
				case EModeBoutonDetaille.DownBase:
					return Properties.Resources.down_blue;
				default:
					return Properties.Resources.Supprimer;
			}
		}
		private void SetImage(Button btn, EModeBouton mode)
		{
			EModeBoutonDetaille modeDetaille = EModeBoutonDetaille.DownBase;
			if (btn == m_btnBas)
			{
				switch (mode)
				{
					case EModeBouton.Survol:
						modeDetaille = EModeBoutonDetaille.DownSurvol;
						break;
					case EModeBouton.Clic:
						modeDetaille = EModeBoutonDetaille.DownClic;
						break;
					case EModeBouton.Base:
						modeDetaille = EModeBoutonDetaille.DownBase;
						break;
				}
			}
			else
			{
				switch (mode)
				{
					case EModeBouton.Survol:
						modeDetaille = EModeBoutonDetaille.UpSurvol;
						break;
					case EModeBouton.Clic:
						modeDetaille = EModeBoutonDetaille.UpClic;
						break;
					case EModeBouton.Base:
						modeDetaille = EModeBoutonDetaille.UpBase;
						break;
				}
			}
			btn.BackgroundImage = GetBitmap(modeDetaille);
		}

		private void Btns_MouseDown(object sender, MouseEventArgs e)
		{
			if (Control.MouseButtons == MouseButtons.Left)
				SetImage((Button)sender, EModeBouton.Clic);	
		}
		private void Btns_MouseUp(object sender, MouseEventArgs e)
		{
			if (Control.MouseButtons == MouseButtons.Left)
				SetImage((Button)sender, EModeBouton.Base);	
		}
		private void Btns_MouseHover(object sender, EventArgs e)
		{
			SetImage((Button)sender, EModeBouton.Survol);	
		}
		private void Btns_MouseLeave(object sender, EventArgs e)
		{
			SetImage((Button)sender, EModeBouton.Base);	
		}
		private void Btns_MouseMove(object sender, MouseEventArgs e)
		{
			if(Control.MouseButtons == MouseButtons.Left)
				SetImage((Button)sender, EModeBouton.Survol);	

		}
		private void Ctrl_MouseLeave(object sender, EventArgs e)
		{
			SetImage(m_btnBas, EModeBouton.Base);
			SetImage(m_btnHaut, EModeBouton.Base);	
		}

	}
}
