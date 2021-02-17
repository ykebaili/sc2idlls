using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using sc2i.common;

namespace sc2i.win32.common
{
	public partial class CFormAlerte : Form
	{
        private int m_nSecondesMaxiAffichage = 0;
        private DateTime m_dateTimeOuverture;
        private Button m_boutonDecompte = null;
        private string m_strTexteOriginalBoutonDecompte = "";
        private DialogResult m_resultCloseAuto = DialogResult.OK;

		public CFormAlerte(string strMess)
		{
			InitializeComponent();
			Initialiser(strMess);
		}
		public CFormAlerte(string strMess, EFormAlerteType typeAlerte)
		{
			InitializeComponent();
			Initialiser(strMess, typeAlerte);
		}
		public CFormAlerte(string strMess, EFormAlerteBoutons boutons, EFormAlerteType icone)
		{
			InitializeComponent();
			Initialiser(strMess, boutons, icone);
		}
        public CFormAlerte(string strMess, EFormAlerteBoutons boutons, EFormAlerteType icone, int nSecondesMaxiAffichage)
        {
            InitializeComponent();
            Initialiser(strMess, boutons, icone);
            m_nSecondesMaxiAffichage = nSecondesMaxiAffichage;
        }
		public CFormAlerte(string strMess, EFormAlerteBoutons boutons, Bitmap icone)
		{
			InitializeComponent();
			Initialiser(strMess, boutons, icone);
		}

		public CFormAlerte(IErreur[] erreurs)
		{
			InitializeComponent();
			Initialiser(erreurs);
		}
		public CFormAlerte(IErreur[] erreurs, EModeAffichageErreurs mode)
		{
			InitializeComponent();
			Initialiser(erreurs, mode);
		}
		public CFormAlerte(IErreur[] erreurs, EFormAlerteBoutons boutons, EFormAlerteType icone)
		{
			InitializeComponent();
			Initialiser(erreurs, boutons, icone);
		}
		public CFormAlerte(IErreur[] erreurs, EFormAlerteBoutons boutons, Bitmap icone)
		{
			InitializeComponent();
			Initialiser(erreurs, boutons, icone);
		}
		public CFormAlerte(List<IErreur> erreurs)
		{
			InitializeComponent();
			Initialiser(erreurs);
		}
		public CFormAlerte(List<IErreur> erreurs, EModeAffichageErreurs mode)
		{
			InitializeComponent();
			Initialiser(erreurs, mode);
		}
		public CFormAlerte(List<IErreur> erreurs, EFormAlerteBoutons boutons, EFormAlerteType icone)
		{
			InitializeComponent();
			Initialiser(erreurs, boutons, icone);
		}
		public CFormAlerte(List<IErreur> erreurs, EFormAlerteBoutons boutons, Bitmap icone)
		{
			InitializeComponent();
			Initialiser(erreurs, boutons, icone);
		}


		public static DialogResult Afficher(string strMess)
		{
			CFormAlerte frm = new CFormAlerte(strMess);
			return frm.ShowDialog();
		}
		public static DialogResult Afficher(string strMess, EFormAlerteType typeAlerte)
		{
			CFormAlerte frm = new CFormAlerte(strMess, typeAlerte);
			return frm.ShowDialog();
		}
		public static DialogResult Afficher(string strMess, EFormAlerteBoutons boutons, EFormAlerteType icone)
		{
			CFormAlerte frm = new CFormAlerte(strMess, boutons, icone);
			return frm.ShowDialog();
		}
        public static DialogResult Afficher(string strMess, EFormAlerteBoutons boutons, EFormAlerteType icone, int nSecondesMaxAffichage)
        {
            return Afficher(strMess, boutons, icone, nSecondesMaxAffichage, null);
        }

        public static DialogResult Afficher(string strMess, EFormAlerteBoutons boutons, EFormAlerteType icone, int nSecondesMaxAffichage, Form owner)
        {
            CFormAlerte frm = new CFormAlerte(strMess, boutons, icone, nSecondesMaxAffichage);
            //frm.Owner = owner;
            return frm.ShowDialog(owner);
        }
		public static DialogResult Afficher(string strMess, EFormAlerteBoutons boutons, Bitmap icone)
		{
			CFormAlerte frm = new CFormAlerte(strMess, boutons, icone);
			return frm.ShowDialog();
		}

		public static DialogResult Afficher(IErreur[] erreurs)
		{
			CFormAlerte frm = new CFormAlerte(erreurs);
			return frm.ShowDialog();
		}
		public static DialogResult Afficher(IErreur[] erreurs, EModeAffichageErreurs mode)
		{
			CFormAlerte frm = new CFormAlerte(erreurs, mode);
			return frm.ShowDialog();
		}
		public static DialogResult Afficher(IErreur[] erreurs, EFormAlerteBoutons boutons, EFormAlerteType icone)
		{
			CFormAlerte frm = new CFormAlerte(erreurs, boutons, icone);
			return frm.ShowDialog();
		}
		public static DialogResult Afficher(IErreur[] erreurs, EFormAlerteBoutons boutons, Bitmap icone)
		{
			CFormAlerte frm = new CFormAlerte(erreurs, boutons, icone);
			return frm.ShowDialog();
		}
		public static DialogResult Afficher(List<IErreur> erreurs)
		{
			CFormAlerte frm = new CFormAlerte(erreurs);
			return frm.ShowDialog();
		}
		public static DialogResult Afficher(List<IErreur> erreurs, EModeAffichageErreurs mode)
		{
			CFormAlerte frm = new CFormAlerte(erreurs, mode);
			return frm.ShowDialog();
		}
		public static DialogResult Afficher(List<IErreur> erreurs, EFormAlerteBoutons boutons, EFormAlerteType icone)
		{
			CFormAlerte frm = new CFormAlerte(erreurs, boutons, icone);
			return frm.ShowDialog();
		}
		public static DialogResult Afficher(List<IErreur> erreurs, EFormAlerteBoutons boutons, Bitmap icone)
		{
			CFormAlerte frm = new CFormAlerte(erreurs, boutons, icone);
			return frm.ShowDialog();
		}


		private void Initialiser(string strMess)
		{
			Initialiser(strMess, EFormAlerteType.Info);
		}
		private void Initialiser(string strMess, EFormAlerteType typeAlerte)
		{
			switch (typeAlerte)
			{
				case EFormAlerteType.Question:
					Initialiser(strMess, EFormAlerteBoutons.OuiNon, EFormAlerteType.Question);
					break;

				case EFormAlerteType.Erreur:
					Initialiser(strMess, EFormAlerteBoutons.Ok, EFormAlerteType.Erreur);
					break;

				case EFormAlerteType.Exclamation:
					Initialiser(strMess, EFormAlerteBoutons.Ok, EFormAlerteType.Exclamation);
					break;

				case EFormAlerteType.Info:
				default:
					Initialiser(strMess, EFormAlerteBoutons.Ok, EFormAlerteType.Info);
					break;
			}
		}
		private void Initialiser(string strMess, EFormAlerteBoutons boutons, EFormAlerteType icone)
		{
			m_icone = icone;
			m_bitmap = null;
			m_btns = boutons;
			Message = strMess;
			m_bIsErrorBox = false;
			ActualForme();
		}
		private void Initialiser(string strMess, EFormAlerteBoutons boutons, Bitmap icone)
		{
			if (icone == null)
				Initialiser(strMess, boutons, EFormAlerteType.Info);
			else
			{
				m_bitmap = icone;
				m_btns = boutons;
				Message = strMess;
				m_bIsErrorBox = false;
				ActualForme();
			}
		}

		private void Initialiser(IErreur[] erreurs)
		{
			Initialiser(erreurs, EModeAffichageErreurs.Automatique);
		}
		private void Initialiser(IErreur[] erreurs, EModeAffichageErreurs mode)
		{
			List<IErreur> lst = new List<IErreur>();
			foreach (IErreur err in erreurs)
				lst.Add(err);
			Initialiser(lst, mode);
		}
		private void Initialiser(IErreur[] erreurs, EFormAlerteBoutons boutons, EFormAlerteType icone)
		{
			List<IErreur> lst = new List<IErreur>();
			foreach (IErreur err in erreurs)
				lst.Add(err);
			Initialiser(lst, boutons, icone);
		}
		private void Initialiser(IErreur[] erreurs, EFormAlerteBoutons boutons, Bitmap icone)
		{
			List<IErreur> lst = new List<IErreur>();
			foreach (IErreur err in erreurs)
				lst.Add(err);
			Initialiser(lst, boutons, icone);
		}

		private void Initialiser(List<IErreur> erreurs)
		{
			Initialiser(erreurs, EModeAffichageErreurs.Automatique);
		}
		private void Initialiser(List<IErreur> erreurs, EModeAffichageErreurs mode)
		{
			m_ctrlErreurs.Initialiser(erreurs);
			m_tt.SetToolTip(m_ctrlErreurs, m_ctrlErreurs.Erreurs.Count + " "+ I.T("Errors...|126"));
			switch (mode)
			{
				case EModeAffichageErreurs.Automatique:
					if (m_ctrlErreurs.OnlyValidationErrors)
					{
						m_icone = EFormAlerteType.Exclamation;
						m_btns = EFormAlerteBoutons.IgnorerCorriger;
					}
					else
					{
						m_icone = EFormAlerteType.Erreur;
						m_btns = EFormAlerteBoutons.Corriger;
					}

					break;
				case EModeAffichageErreurs.AvecIgnorer:
					m_icone = EFormAlerteType.Exclamation;
					m_btns = EFormAlerteBoutons.IgnorerCorriger;
					break;
				case EModeAffichageErreurs.SansIgnorer:
					m_icone = EFormAlerteType.Erreur;
					m_btns = EFormAlerteBoutons.Corriger;
					break;
				default:
					break;
			}
			m_bIsErrorBox = true;
			ActualForme();
		}
		private void Initialiser(List<IErreur> erreurs, EFormAlerteBoutons boutons, EFormAlerteType icone)
		{
			m_btns = boutons;
			m_icone = icone;
			m_ctrlErreurs.Initialiser(erreurs);
            m_tt.SetToolTip(m_ctrlErreurs, m_ctrlErreurs.Erreurs.Count + I.T("Errors...|126"));
			m_bIsErrorBox = true;
			ActualForme();
		}
		private void Initialiser(List<IErreur> erreurs, EFormAlerteBoutons boutons, Bitmap icone)
		{
			if (icone == null)
				Initialiser(erreurs, boutons, EFormAlerteType.Info);
			else
			{
				m_bitmap = icone;
				m_btns = boutons;
				m_ctrlErreurs.Initialiser(erreurs);
                m_tt.SetToolTip(m_ctrlErreurs, m_ctrlErreurs.Erreurs.Count + I.T("Errors...|126"));
				m_bIsErrorBox = true;
				ActualForme();
			}
		}

		private bool m_bIsErrorBox = false;
		private bool IsErrorsBox
		{
			get
			{
				return m_bIsErrorBox;
			}
		}

		//private string m_strCaption;
		//public string Titre
		//{
		//    get
		//    {
		//    }
		//    set
		//    {
		//    }
		//}

		public string Message
		{
			get
			{
				return m_txtMessage.Text;
			}
			set
			{
				m_txtMessage.Text = value;
			}
		}

		//Défini les boutons possibles
		private EFormAlerteBoutons m_btns;
		private EFormAlerteBoutons BoutonsAffiches
		{
			get
			{
				return m_btns;
			}
		}

		//Icone
		private Bitmap m_bitmap = null;
		private EFormAlerteType m_icone;
		public EFormAlerteType Icone
		{
			get
			{
				return m_icone;
			}
		}


		//MISE EN FORME
		private void ActualForme()
		{
			ActualApparence();
			ActualBoutons();
			ActualImagesEtIcones();
		}
		private void ActualBoutons()
		{
			ResultatBoutonGauche = null;
			m_btnGauche.TexteAffiche = null;
			m_btnGauche.TexteAffiche = TexteBoutonGauche;

			ResultatBoutonCentre = null;
			m_btnCentre.TexteAffiche = null;
			m_btnCentre.TexteAffiche = TexteBoutonCentre;

			ResultatBoutonDroit = null;
			m_btnDroit.TexteAffiche = null;
			m_btnDroit.TexteAffiche = TexteBoutonDroit;


			switch (BoutonsAffiches)
			{
				case EFormAlerteBoutons.Ok:
					ResultatBoutonCentre = DialogResult.OK;
					AcceptButton = BoutonCentre;
					CancelButton = BoutonCentre;
                    m_boutonDecompte = BoutonCentre;
                    m_resultCloseAuto = DialogResult.OK;
					break;

                case EFormAlerteBoutons.OkAbandonner:
                    ResultatBoutonGauche = DialogResult.OK;
                    ResultatBoutonDroit = DialogResult.Cancel;
                    AcceptButton = BoutonGauche;
                    CancelButton = BoutonDroit;
                    m_boutonDecompte = BoutonGauche;
                    m_resultCloseAuto = DialogResult.OK;
                    break;

				case EFormAlerteBoutons.IgnorerAnnuler:
					ResultatBoutonGauche = DialogResult.Ignore;
					ResultatBoutonDroit = DialogResult.Cancel;
                    m_boutonDecompte = BoutonGauche;
                    m_resultCloseAuto = DialogResult.Ignore;
                    break;
					break;

				case EFormAlerteBoutons.IgnorerCorriger:
					ResultatBoutonGauche = DialogResult.Ignore;
					ResultatBoutonDroit = DialogResult.Cancel;
					if (TexteBoutonDroit == null)
						m_btnDroit.TexteAffiche = I.T("Correct|13");
                    m_boutonDecompte = BoutonGauche;
                    m_resultCloseAuto = DialogResult.Ignore;
					break;

				case EFormAlerteBoutons.Corriger:
					ResultatBoutonCentre = DialogResult.Cancel;
					if (TexteBoutonCentre == null)
                        m_btnCentre.TexteAffiche = I.T("Correct|13");
					AcceptButton = BoutonCentre;
					CancelButton = BoutonCentre;
                    m_boutonDecompte = BoutonCentre;
                    m_resultCloseAuto = DialogResult.Cancel;
					break;

				case EFormAlerteBoutons.OuiNon:
					ResultatBoutonGauche = DialogResult.Yes;
                    ResultatBoutonDroit = DialogResult.No; 
                    m_boutonDecompte = BoutonGauche;
                    m_resultCloseAuto = DialogResult.Yes;
					break;

				case EFormAlerteBoutons.OuiNonCancel:
					ResultatBoutonGauche = DialogResult.Yes;
					ResultatBoutonCentre = DialogResult.No;
                    ResultatBoutonDroit = DialogResult.Cancel;
                    m_boutonDecompte = BoutonGauche;
                    m_resultCloseAuto = DialogResult.Yes;
					break;

				case EFormAlerteBoutons.ContinuerAnnuler:
					ResultatBoutonGauche = DialogResult.Ignore;
					if (TexteBoutonGauche == null)
						m_btnGauche.TexteAffiche = I.T("Continue|127");
                    ResultatBoutonDroit = DialogResult.Cancel;
                    m_boutonDecompte = BoutonGauche;
                    m_resultCloseAuto = DialogResult.Ignore;
					break;

				case EFormAlerteBoutons.IgnorerReessayerAbandonner:
					ResultatBoutonGauche = DialogResult.Ignore;
					ResultatBoutonCentre = DialogResult.Retry;
					ResultatBoutonDroit = DialogResult.Abort;
                    m_btnDroit.TexteAffiche = TexteBoutonDroit;
                    m_boutonDecompte = BoutonGauche;
                    m_resultCloseAuto = DialogResult.Ignore;
					break;
				default:
					break;
			}
		}
		private void ActualImagesEtIcones()
		{
			if (m_bitmap != null)
				m_panImageHaut.BackgroundImage = m_bitmap;
			else
				switch (Icone)
				{
					case EFormAlerteType.Exclamation:
						this.Icon = Properties.Resources.IcoExclam;
						m_panImageHaut.BackgroundImage = Properties.Resources.ImgExclam;
						break;
					case EFormAlerteType.Erreur:
						this.Icon = Properties.Resources.IcoErreur;
						m_panImageHaut.BackgroundImage = Properties.Resources.ImgErreur_old;
						break;
					case EFormAlerteType.Question:
						this.Icon = Properties.Resources.IcoQuestion;
						m_panImageHaut.BackgroundImage = Properties.Resources.ImgQuestion;
						break;
					case EFormAlerteType.Info:
						this.Icon = Properties.Resources.IcoInfo;
						m_panImageHaut.BackgroundImage = Properties.Resources.ImgInfo;
						break;
					default:
						break;
				}
		}
		private void ActualApparence()
		{
            int nTailleXmax = 800,
                nTailleXmin = 300,
                nTailleYmax = 600;

            int nHauteureInitiale = Height;
            int nLargeureInitiale = Width;
           
            m_ctrlErreurs.Visible = IsErrorsBox;
            m_txtMessage.Visible = !IsErrorsBox;

			//Taille
			if (!IsErrorsBox)
			{
                //Size s = TextRenderer.MeasureText(Message, m_txtMessage.Font, m_txtMessage.Size, TextFormatFlags.TextBoxControl);
                //s.Width = Math.Min(s.Width, nTailleXmax); // Taille max
                //s.Width = Math.Max(s.Width, nTailleXmin); // Taille min
                
                ////if (s.Width > Screen.GetWorkingArea(this).Width - 200)
                ////{
                ////    s.Width = Screen.GetWorkingArea(this).Width - 200;
                //s.Height = TextRenderer.MeasureText(Message, m_txtMessage.Font, s, TextFormatFlags.TextBoxControl | TextFormatFlags.WordBreak).Height;
                ////}

                //if (s.Height > Screen.GetWorkingArea(this).Height - 200)
                //{
                //    s.Height = Screen.GetWorkingArea(this).Height - 200;
                //    m_txtMessage.ScrollBars = ScrollBars.Vertical;
                //}
                ////if (s.Height > 600)
                ////{
                ////    s.Height = 600;
                ////    m_txtMessage.ScrollBars = ScrollBars.Vertical;
                ////}

                //Height = s.Height + m_panBoutons.Height + m_panImageHaut.Height + (m_txtMessage.Top - m_panImageHaut.Bottom) + (m_panBoutons.Top - m_txtMessage.Bottom) + 10;
                //Width = (Width - m_txtMessage.Right) + m_txtMessage.Left + s.Width;

                //if (Width < 300)
                //    Width = 300;

                // Nouvelle méthode qui marche sous Windows 7
                //Screen.GetWorkingArea(this); // 
                Size sCalculee = TextRenderer.MeasureText(Message, m_txtMessage.Font, m_txtMessage.Size, TextFormatFlags.TextBoxControl);
                sCalculee.Width = Math.Min(sCalculee.Width, nTailleXmax); // Taille max
                sCalculee.Width = Math.Max(sCalculee.Width, nTailleXmin); // Taille min
                // Calcul la hauteur
                sCalculee.Height = TextRenderer.MeasureText(Message, m_txtMessage.Font, sCalculee, TextFormatFlags.TextBoxControl | TextFormatFlags.WordBreak).Height;
                if (sCalculee.Height > nTailleYmax)
                {
                    sCalculee.Height = nTailleYmax;
                    m_txtMessage.ScrollBars = ScrollBars.Vertical;
                }

                Height = sCalculee.Height + (nHauteureInitiale - m_txtMessage.Height);
                Width = sCalculee.Width + (nLargeureInitiale - m_txtMessage.Width);
                
				FormBorderStyle = FormBorderStyle.FixedSingle;
			}
			else
			{

                Size goodSize = m_ctrlErreurs.PerfectSize;
                //if (goodSize.Height < 21 * m_ctrlErreurs.Erreurs.Count)
                //    goodSize.Height = 21 * m_ctrlErreurs.Erreurs.Count;
                //if (goodSize.Height > Screen.GetWorkingArea(this).Height - 200)
                //    goodSize.Height = Screen.GetWorkingArea(this).Height - 200;
                //if (goodSize.Width > Screen.GetWorkingArea(this).Width - 200)
                //    goodSize.Width = Screen.GetWorkingArea(this).Width - 200;

                //Height = goodSize.Height + m_panBoutons.Height + m_panImageHaut.Height + (m_txtMessage.Top - m_panImageHaut.Bottom) + (m_panBoutons.Top - m_txtMessage.Bottom) + 10;
                //Width = (Width - m_txtMessage.Right) + m_txtMessage.Left + goodSize.Width;

                //if (Width < 300)
                //    Width = 300;

                // Nouvelle méthode qui marche sous Windows 7
                goodSize.Height = Math.Max(goodSize.Height, m_ctrlErreurs.Erreurs.Count * 21);
                goodSize.Height = Math.Min(goodSize.Height, nTailleYmax);
                goodSize.Width = Math.Min(goodSize.Width, nTailleXmax); // Taille max
                goodSize.Width = Math.Max(goodSize.Width, nTailleXmin); // Taille min

                Height = goodSize.Height + (nHauteureInitiale - m_ctrlErreurs.Height);
                Width = goodSize.Width + (nLargeureInitiale - m_ctrlErreurs.Width);

				FormBorderStyle = FormBorderStyle.Sizable;
				//m_ctrlErreurs.AutoScrollPosition = new Point(0,200);

				//Text = "Erreurs...";
			}
		}


		#region Boutons

		//APPARENCE DES BOUTONS
		//private void m_btnDroit_MouseHover(object sender, EventArgs e)
		//{
		//    m_btnDroit2.BackColor = Color.PeachPuff;

		//}
		//private void m_btnDroit_MouseLeave(object sender, EventArgs e)
		//{
		//    m_btnDroit2.BackColor = Color.Transparent;
		//}

		//private void m_btnCentre_MouseLeave(object sender, EventArgs e)
		//{
		//    m_btnCentre2.BackColor = Color.Transparent;
		//}
		//private void m_btnCentre_MouseHover(object sender, EventArgs e)
		//{
		//    m_btnCentre2.BackColor = Color.PaleTurquoise;
		//}

		//private void m_btnGauche_MouseHover(object sender, EventArgs e)
		//{
		//    m_btnGauche2.BackColor = Color.PaleGreen;
		//}
		//private void m_btnGauche_MouseLeave(object sender, EventArgs e)
		//{
		//    m_btnGauche2.BackColor = Color.Transparent;
		//}


		//Boutons

		/// <summary>
		/// Null pour masquer le bouton
		/// </summary>
		private DialogResult? ResultatBoutonGauche
		{
			get
			{
				return m_btnGauche.ResultatAssocie;
			}
			set
			{
				m_btnGauche.Visible = value.HasValue;
				m_btnGauche.ResultatAssocie = value.HasValue ? value.Value : DialogResult.None;
			}
		}
		private DialogResult? ResultatBoutonCentre
		{
			get
			{
				return m_btnCentre.ResultatAssocie;
			}
			set
			{
				m_btnCentre.Visible = value != null;
				m_btnCentre.ResultatAssocie = value.HasValue ? value.Value : DialogResult.None;
			}
		}
		private DialogResult? ResultatBoutonDroit
		{
			get
			{
				return m_btnDroit.ResultatAssocie;
			}
			set
			{
				m_btnDroit.Visible = value.HasValue;
				m_btnDroit.ResultatAssocie = value.HasValue ? value.Value : DialogResult.None;
			}
		}

		public Button BoutonGauche
		{
			get
			{
				return m_btnGauche.Bouton;
			}
		}
		public Button BoutonCentre
		{
			get
			{
				return m_btnCentre.Bouton;
			}
		}
		public Button BoutonDroit
		{
			get
			{
				return m_btnDroit.Bouton;
			}
		}


		private string m_strTexteBoutonGauche;
		/// <summary>
		/// Null pour passer en nommage automatique
		/// </summary>
		public string TexteBoutonGauche
		{
			get
			{
				return m_strTexteBoutonGauche;
			}
			set
			{
				m_strTexteBoutonGauche = value;
			}
		}
		private string m_strTexteBoutonCentre;
		/// <summary>
		/// Null pour passer en nommage automatique
		/// </summary>
		public string TexteBoutonCentre
		{
			get
			{
				return m_strTexteBoutonCentre;
			}
			set
			{
				m_strTexteBoutonCentre = value;
			}
		}
		private string m_strTexteBoutonDroit;
		/// <summary>
		/// Null pour passer en nommage automatique
		/// </summary>
		public string TexteBoutonDroit
		{
			get
			{
				return m_strTexteBoutonDroit;
			}
			set
			{
				m_strTexteBoutonDroit = value;
			}
		}


		#endregion

        private void m_timerAutoClose_Tick(object sender, EventArgs e)
        {
            UpdateDecompte();
        }

        private void UpdateDecompte()
        {
            if (m_timerAutoClose.Enabled)
            {
                TimeSpan sp = DateTime.Now - m_dateTimeOuverture;
                int nSecondes = (int)(m_nSecondesMaxiAffichage - sp.TotalSeconds);
                if (m_strTexteOriginalBoutonDecompte == "")
                    m_strTexteOriginalBoutonDecompte = m_boutonDecompte.Text;
                string strText = m_strTexteOriginalBoutonDecompte + " (" + nSecondes + "s)";
                m_boutonDecompte.Text = strText;
                if (nSecondes <= 0)
                {
                    DialogResult = m_resultCloseAuto;
                    m_timerAutoClose.Enabled = false;
                    Close();
                }
            }
        }

        private void CFormAlerte_Load(object sender, EventArgs e)
        {
            if (m_nSecondesMaxiAffichage > 0)
                m_timerAutoClose.Enabled = true;
            else
                m_timerAutoClose.Enabled = false;
            m_dateTimeOuverture = DateTime.Now;
            UpdateDecompte();
        }



		//AFFICHAGE COMME MESSAGE BOX
		//public static DialogResult Show(string strText)
		//{
		//}
		//public static DialogResult Show(string strText, MessageBoxButtons msgButtons)
		//{
		//}
		//public string toto()
		//{
		//    MessageBoxIcon.
		//    System.Windows.Forms.MessageBox.Show(²"Test");
		//}
	}
	
	
}
