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

	public static class CEffetFonduPourFormManager
	{
		private static CProfilEffetFondu m_profil;
		public static CProfilEffetFondu Profil
		{
			get
			{
				return m_profil;
			}
			set
			{
				m_profil = value;
			}
		}

		private static bool m_bEffetActif = true;
		public static bool EffetActif
		{
			get
			{
				return m_bEffetActif;
			}
			set
			{
				m_bEffetActif = value;
			}
		}
		

		private static int m_nNombreImages = 10;
		public static int NombreImages
		{
			get
			{
				return m_nNombreImages;
			}
			set
			{
				m_nNombreImages = value;
			}
		}

		private static int m_nInterval = 10;
		public static int IntervalImages
		{
			get
			{
				return m_nInterval;
			}
			set
			{
				m_nInterval = value;
			}
		}


	}

}
