using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;

namespace sc2i.win32.common
{

	public class CProfilEffetFondu
	{
		public CProfilEffetFondu()
		{
		}

		public CProfilEffetFondu(bool bActif, int nNombreImage, int nInterval)
		{
			EffetActif = bActif;
			IntervalImages = nInterval;
			NombreImages = nNombreImage;
		}

		public bool EffetActif
		{
			get
			{
				return EffetFermeture || EffetOuverture;
			}
			set
			{
				if (!value)
				{
					EffetOuverture = false;
					EffetFermeture = false;
				}
				else if (!EffetActif)
				{
					EffetOuverture = true;
					EffetFermeture = true;
				}
			}
		}

		private bool m_bEffetOuverture = false;
		public bool EffetOuverture
		{
			get
			{
				return m_bEffetOuverture;
			}
			set
			{
				m_bEffetOuverture = value;
			}
		}
		private bool m_bEffetFermeture = false;
		public bool EffetFermeture
		{
			get
			{
				return m_bEffetFermeture;
			}
			set
			{
				m_bEffetFermeture = value;
			}
		}

		private int m_nNombreImages = 10;
		public int NombreImages
		{
			get
			{
				return m_nNombreImages;
			}
			set
			{
				if(value > 0)
					m_nNombreImages = value;
			}
		}

		private int m_nInterval = 10;
		public int IntervalImages
		{
			get
			{
				return m_nInterval;
			}
			set
			{
				if(value > 0)
					m_nInterval = value;
			}
		}
	}
}
