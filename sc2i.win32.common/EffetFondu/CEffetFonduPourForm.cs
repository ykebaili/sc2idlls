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
	public class CEffetFonduPourForm : Component
	{
		public CEffetFonduPourForm()
		{
			m_timerAlpha = new System.Windows.Forms.Timer();
			m_timerAlpha.Tick += new EventHandler(TimerAlpha_Tick);
			m_timerAlpha.Interval = m_nIntervalTick;
            //FinEffetFermeture += new EventHandler(CEffetFonduPourForm_FinEffetFermeture);
		}

        //private Thread m_thread;
        //private void CEffetFonduPourForm_FinEffetFermeture(object sender, EventArgs e)
        //{
        //    Thread th = m_thread;
        //    m_thread = null;
        //    th.Resume();
        //}


		private CProfilEffetFondu m_profil;
		public CProfilEffetFondu Profil
		{
			get
			{
				if(ModeFonctionnement == EModeFonctionnementEffetFonduPourForm.Autonome)
					return new CProfilEffetFondu(m_formulaire != null, NombreImage, IntervalImages);
				return new CProfilEffetFondu(CEffetFonduPourFormManager.EffetActif, NombreImage, IntervalImages);
			}
			set
			{
				m_profil = value;
			}
		}

		private EModeFonctionnementEffetFonduPourForm m_modeFonctionnement = EModeFonctionnementEffetFonduPourForm.Manager;
		[DefaultValue(EModeFonctionnementEffetFonduPourForm.Manager)]
		public EModeFonctionnementEffetFonduPourForm ModeFonctionnement
		{
			get
			{
				return m_modeFonctionnement;
			}
			set
			{
				m_modeFonctionnement = value;

			}
		}

		private bool m_bOnTheTop = true;
		public bool AuDessusDesAutresFenetres
		{
			get
			{
				return m_bOnTheTop;
			}
			set
			{
				m_bOnTheTop = value;
			}
		}

		private bool m_bInitialise = false;
		private System.Windows.Forms.Timer m_timerAlpha;
		private void TimerAlpha_Tick(object sender, EventArgs e)
		{
			if (m_bInitialise)
			{
				Formulaire.Opacity -= Pas;
				if (Formulaire.Opacity <= 0)
				{
					m_timerAlpha.Stop();
					m_bInitialise = false;
					if (FinEffetFermeture != null)
                        FinEffetFermeture(this, new EventArgs());
				}
			}
			else
			{
				Formulaire.Opacity += Pas;
                Formulaire.Refresh();
				if (Formulaire.Opacity >= 1)
				{
					m_timerAlpha.Stop();
					m_bInitialise = true;
					if (FinEffetOuverture != null)
						FinEffetOuverture(this, new EventArgs());
				}
				if(m_bOnTheTop)
					pInvoke.SetWindowPos(Formulaire.Handle, pInvoke.HWND_TOPMOST, 0, 0, 0, 0, pInvoke.SWP_NOMOVE | pInvoke.SWP_NOSIZE);
			}
		}
		private double Pas
		{
			get
			{
				return ((double)100 / (double)NombreImage) / (double)100;
			}
		}
		private int m_nIntervalTick = 10;
		[DefaultValue(10)]
		public int IntervalImages
		{
			get
			{
				return m_nIntervalTick;
			}
			set
			{
				m_nIntervalTick = value;
			}
		}
		private int m_nbImages = 10;
		[DefaultValue(10)]
		public int NombreImage
		{
			get
			{
				return m_nbImages;
			}
			set
			{
				m_nbImages = value;
			}
		}

		private double m_dbOriginalOpacity;
		private Form m_formulaire;
		public Form Formulaire
		{
			get
			{
				return m_formulaire;
			}
			set
			{

				if (value != null && value != m_formulaire)
				{
					m_formulaire = value;
					m_formulaire.Load += new EventHandler(Formulaire_Load);
					m_formulaire.FormClosing += new FormClosingEventHandler(Formulaire_Closing);
					m_bInitialise = false;
					m_dbOriginalOpacity = value.Opacity;
					if (EffetFonduOuverture)
						value.Opacity = 0;
					m_formulaire = value;
				}
			}
		}

		private void Formulaire_Closing(object sender, FormClosingEventArgs e)
		{
			if (EffetFonduFermeture && m_bInitialise && !e.Cancel)
			{
				if (DebutEffetFermeture != null)
                    DebutEffetFermeture(this, new EventArgs());


                for (int n = 0; n < NombreImage; n++)
                {
                    Formulaire.Opacity -= Pas;
                    Formulaire.Refresh();
                    if (Formulaire.Opacity <= 0)
                        break;
                    Thread.Sleep(IntervalImages);
                }
                m_bInitialise = false;
                if (FinEffetFermeture != null)
                    FinEffetFermeture(this, new EventArgs());
                //Control.CheckForIllegalCrossThreadCalls = false;
                //m_thread = Thread.CurrentThread;
                //Thread thEffet = new Thread(new ThreadStart(LaugthEffet));
                //thEffet.Start();
                //if(m_thread != null)
                //    m_thread.Join();
                
			}
		}
		private void Formulaire_Load(object sender, EventArgs e)
		{
			if (EffetFonduOuverture)
			{
				if (DebutEffetOuverture != null)
					DebutEffetOuverture(this, new EventArgs());
				m_timerAlpha.Start();
			}
			else
			{
				Formulaire.Opacity = m_dbOriginalOpacity;
				m_bInitialise = true;
			}
		}



        private void LaugthEffet()
        {
            m_timerAlpha.Start();            
        }


		public bool EffetActif
		{
			get
			{
				return Profil.EffetActif;
			}
		}

		private bool m_bEffetFonduFermeture;
		public bool EffetFonduFermeture
		{
			get
			{
				if (ModeFonctionnement == EModeFonctionnementEffetFonduPourForm.Manager && !CEffetFonduPourFormManager.EffetActif)
					return false;
				return m_bEffetFonduFermeture;
			}
			set
			{
				m_bEffetFonduFermeture = value;
			}
		}
		private bool m_bEffetFonduOuverture;
		public bool EffetFonduOuverture
		{
			get
			{
				if (ModeFonctionnement == EModeFonctionnementEffetFonduPourForm.Manager && !CEffetFonduPourFormManager.EffetActif)
					return false;
				return m_bEffetFonduOuverture;
			}
			set
			{
				m_bEffetFonduOuverture = value;
			}
		}

		public event EventHandler DebutEffetOuverture;
		public event EventHandler FinEffetOuverture;
		public event EventHandler DebutEffetFermeture;
		public event EventHandler FinEffetFermeture;

		public class pInvoke
		{
			private pInvoke() { }

			public delegate bool EnumWindowsProc(IntPtr hwnd, int lParam);
			public const int SWP_NOSIZE = 0x1;
			public const int SWP_NOMOVE = 0x2;
			public const int HWND_TOPMOST = -1;
			public const int HWND_NOTOPMOST = -2;

			[DllImport("user32")]
			public static extern int EnumWindows(EnumWindowsProc lpEnumFunc, int lParam);
			[DllImport("user32.dll")]
			public static extern int GetWindowText(IntPtr hWnd, [Out] StringBuilder lpString, int nMaxCount);
			[DllImport("user32.dll")]
			public static extern int GetWindowTextLength(IntPtr hWnd);
			[DllImport("user32.dll")]
			public static extern bool IsWindowVisible(IntPtr hWnd);
			[DllImport("user32.dll")]
			public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

			public static string GetWindowText(IntPtr hWnd)
			{
				StringBuilder sb = new StringBuilder(GetWindowTextLength(hWnd) + 1);
				GetWindowText(hWnd, sb, sb.Capacity);
				return sb.ToString();
			}
		}

        private void InitializeComponent()
        {


        }


	}

}
