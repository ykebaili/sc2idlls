using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace sc2i.win32.common
{
	public partial class CReducteurControle : UserControl
	{
		public enum EModePositionnement
		{
			Haut = 0,
			Libre = 10
		}
		private Control m_controleReduit;
		private Control m_controlAgrandit;
		private Control m_controlAVoir;
		private int m_nMargeControle = 16;
		private int m_nHeightReduite= 32;
		private int m_nHeightAgrandie = 100;
		private bool m_bIsReduit = false;
		private EModePositionnement m_modePositionnement = EModePositionnement.Haut;
		public CReducteurControle()
		{
			InitializeComponent();
		}

		//----------------------------------------------------
		private void CReducteurControle_Resize(object sender, EventArgs e)
		{
			Size = m_btnChanger.Size;
		}

		//----------------------------------------------------
		public Control ControleReduit
		{
			get
			{
				return m_controleReduit;
			}
			set
			{
				m_controleReduit = value;
				m_controleReduit.Resize += new EventHandler(m_controleReduit_ChangePosition);
				m_controleReduit.Move += new EventHandler(m_controleReduit_ChangePosition);
				Repositionne();
			}
		}

		void  m_controleReduit_ChangePosition(object sender, EventArgs e)
		{
 			Repositionne();
		}

		//----------------------------------------------------
		public int MargeControle
		{
			get
			{
				return m_nMargeControle;
			}
			set
			{
				m_nMargeControle = value;
			}
		}

		//----------------------------------------------------
		private void Repositionne()
		{
			if (m_controleReduit == null)
				return;
			if ( ModePositionnement == EModePositionnement.Libre )
				return;
			switch ( ModePositionnement )
			{
				case EModePositionnement.Haut:
					try
					{
						Location = Parent.PointToClient(m_controleReduit.Parent.PointToScreen(new Point(m_controleReduit.Left + m_controleReduit.Width / 2 - Width / 2,
							m_controleReduit.Top - Height / 2)));
					}
					catch
					{
					}
					break;
			}
		}

		//----------------------------------------------------
		public Control ControleAgrandit
		{
			get
			{
				return m_controlAgrandit;
			}
			set
			{
				m_controlAgrandit = value;
			}
		}

		//----------------------------------------------------
		public Control ControleAVoir
		{
			get
			{
				return m_controlAVoir;
			}
			set
			{
				m_controlAVoir = value;
			}
		}

		//----------------------------------------------------
		[Browsable(false)]
		public int TailleReduite
		{
			get
			{
				return m_nHeightReduite;
			}
			set
			{
				m_nHeightReduite = value;
			}
		}

		//----------------------------------------------------
		[Browsable(false)]
		private int TailleAgrandie
		{
			get
			{
				return m_nHeightAgrandie;
			}
			set
			{
				m_nHeightAgrandie = value;
			}
		}

		//----------------------------------------------------
		private void m_btnChanger_Click(object sender, EventArgs e)
		{
			if (!m_bIsReduit)
			{
				if (ControleReduit != null)
				{
					m_nHeightReduite = m_nMargeControle;
					if (ControleAVoir != null)
					{
						m_nHeightReduite = ControleAVoir.Bottom + m_nMargeControle +
							ControleReduit.Size.Height - ControleReduit.ClientSize.Height;
					}
					m_nHeightAgrandie = ControleReduit.Height;
					ControleReduit.Height = m_nHeightReduite;
					if (ControleAgrandit != null)
					{
						int nEcart = m_nHeightAgrandie - m_nHeightReduite;
						ControleAgrandit.Location = new Point(ControleAgrandit.Location.X,
							ControleAgrandit.Location.Y - nEcart);
						ControleAgrandit.Size = new Size(ControleAgrandit.Size.Width, ControleAgrandit.Height + nEcart);
					}
				}
				m_bIsReduit = true;
			}
			else
			{
				if (ControleReduit != null)
				{
					ControleReduit.Height = m_nHeightAgrandie;
					int nEcart = m_nHeightAgrandie - m_nHeightReduite;
					ControleAgrandit.Location = new Point(ControleAgrandit.Location.X,
							ControleAgrandit.Location.Y + nEcart);
					ControleAgrandit.Size = new Size(ControleAgrandit.Size.Width,
						ControleAgrandit.Size.Height - nEcart);
				}
				m_bIsReduit = false;
			}
			m_btnChanger.Image = m_images.Images[m_bIsReduit ? 1 : 0];
		}

		//----------------------------------------------
		public EModePositionnement ModePositionnement
		{
			get
			{
				return m_modePositionnement;
			}
			set
			{
				m_modePositionnement = value;
			}
		}

		private void CReducteurControle_Move(object sender, EventArgs e)
		{
			Repositionne();
		}

	}
}
