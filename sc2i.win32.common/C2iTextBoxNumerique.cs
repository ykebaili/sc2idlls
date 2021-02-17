using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

using sc2i.common;
using System.Text;
using System.Text.RegularExpressions;

namespace sc2i.win32.common
{
	/// <summary>
	/// Description résumée de C2iTextBoxNumerique.
	/// </summary>
    [AutoExec("Autoexec")]
	public class C2iTextBoxNumerique : TextBox, IControlALockEdition
	{
		private bool m_bLockEdition = false;
		private bool m_bDecimalAutorise = true;
		private bool m_bNullAutorise = false;
		private bool m_bSelectAllOnEnter = true;
		private int m_nArrondi = 0;
		private int m_nNbDecimalesMini = 0;
        private string m_strTexteSiVide = "";

        private string m_strSeparateurMilliers = "";

		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		public C2iTextBoxNumerique()
		{
			InitializeComponent();
            GereUserPaint();
		}

        public static void Autoexec()
        {
            CWin32Traducteur.AddProprieteATraduire(typeof(C2iTextBoxNumerique), "EmptyText");
        }

		#region Component Designer generated code
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// C2iTextBoxNumerique
			// 
			this.Validated += new System.EventHandler(this.C2iTextBoxNumerique_Validated);
			this.TextChanged += new System.EventHandler(this.C2iTextBoxNumerique_TextChanged);
			this.Enter += new System.EventHandler(this.C2iTextBoxNumerique_Enter);

		}
		#endregion
		
		public event EventHandler OnChangeLockEdition; 
		public bool LockEdition
		{
			get
			{
				return m_bLockEdition;
			}
			set
			{
				m_bLockEdition = value;
				Enabled = !m_bLockEdition;
                GereUserPaint();
				if (OnChangeLockEdition != null)
					OnChangeLockEdition(this, new EventArgs());
				Refresh();
			}
		}

        //-------------------------------------------
        private Font m_originalFont = null;
        protected void GereUserPaint()
        {
            if (DesignMode)
                return;
            if (LockEdition || (m_strTexteSiVide.Length > 0 && Text.Length == 0))
            {
                if (!GetStyle(ControlStyles.UserPaint))
                {
                    m_originalFont = Font;
                    SetStyle(ControlStyles.UserPaint, true);
                    Refresh();
                }
            }
            else if (GetStyle(ControlStyles.UserPaint))
            {

                SetStyle(ControlStyles.UserPaint, false);
                //Car le passage à UserPaint = false perd la fonte.
                //Je ne sais pas pourquoi
                if (m_originalFont != null)
                    Font = m_originalFont;
                Refresh();
            }
        }

        public string EmptyText
        {
            get
            {
                return m_strTexteSiVide;
            }
            set
            {
                m_strTexteSiVide = value;
                GereUserPaint();
            }
        }

        //-------------------------------------------
        public string SeparateurMilliers
        {
            get
            {
                return m_strSeparateurMilliers;
            }
            set
            {
                m_strSeparateurMilliers = value;
            }
        }

		/// ///////////////////////////////////////////////////
		public bool SelectAllOnEnter
		{
			get
			{
				return m_bSelectAllOnEnter;
			}
			set
			{
				m_bSelectAllOnEnter = value;
			}
		}

		/// ///////////////////////////////////////////////////
		public int Arrondi
		{
			get
			{
				return m_nArrondi;
			}
			set
			{
				m_nArrondi = value;
			}
		}

		
		/// ///////////////////////////////////////////////////
		[DefaultValue(0)]
		public int DecimalesMiniAffichage
		{
			get
			{
				return m_nNbDecimalesMini;
			}
			set
			{
				m_nNbDecimalesMini = value;
			}
		}

		

		/// ///////////////////////////////////////////////////
		private string GetTexte()
		{
			if (PasswordChar!='\0')
			{
				string strRetour = "";
				for ( int n = 0; n < Text.Length; n++ )
					strRetour += PasswordChar;
				return strRetour;
			}
			return Text;
		}

		
		/// ///////////////////////////////////////////////////
		private bool m_bIsEnCoursDeNettoyage = false;
        private void C2iTextBoxNumerique_TextChanged(object sender, System.EventArgs e)
        {
            if (m_bIsEnCoursDeNettoyage)
                return;
            int nPos = ((TextBox)sender).SelectionStart;
            m_bIsEnCoursDeNettoyage = true;
            string strNew = "";
            string strOld = GetTexte();
            string strAutorises = "1234567890 -";
            if (m_bDecimalAutorise)
                strAutorises += ",.";

            //int nNbSeparateursAvant = 0;
            //Regex expressionSeparateur = new Regex(m_strSeparateurMilliers);
            //nNbSeparateursAvant = expressionSeparateur.Matches(strOld).Count;

            //if (m_strSeparateurMilliers != "")
            //    strOld = strOld.Replace(m_strSeparateurMilliers, "");
            
            int nIndex = 0;
            foreach (char c in strOld)
            {
                nIndex++;
                if (strAutorises.IndexOf(c) >= 0)
                {
                    strNew += c;
                    if ("1234567890".IndexOf(c) < 0)//Pas deux fois les caracteres autres que chiffres
                    {
                        strAutorises = strAutorises.Replace(c + "", "");
                        if (c == '.' || c == ',')
                        {
                            strAutorises = strAutorises.Replace(".", "");
                            strAutorises = strAutorises.Replace(",", "");
                        }
                    }
                }
                else
                    if (nIndex <= nPos)
                        nPos--;
                if (nIndex > 0)
                    strAutorises = strAutorises.Replace("-", "");
            }

            //string strFinale = AppliqueSeparateurMilliers(strNew);
            //if (m_strSeparateurMilliers != "")
            //{
            //    int nNbSeparateursApres = expressionSeparateur.Matches(strFinale).Count;
            //    nPos = nPos + ((nNbSeparateursApres - nNbSeparateursAvant) * m_strSeparateurMilliers.Length);
            //}

            Text = strNew;
            SelectionStart = Math.Max(nPos, 0);
            GereUserPaint();
            m_bIsEnCoursDeNettoyage = false;
        }

        private string AppliqueSeparateurMilliers(string strTexte)
        {
            if (m_strSeparateurMilliers != "")
                strTexte = strTexte.Replace(m_strSeparateurMilliers,"");
            else
                return strTexte;

            StringBuilder sbRetour = new StringBuilder();
            int nIndexSeparateurDec = strTexte.Length;
            if (strTexte.IndexOf('.') > 0)
            {
                nIndexSeparateurDec = strTexte.IndexOf('.');
                sbRetour.Append(".");
            }
            else
            {
                if (strTexte.IndexOf(',') > 0)
                {
                    nIndexSeparateurDec = strTexte.IndexOf(',');
                    sbRetour.Append(",");
                }
            }
            int nNbParGroupe = 3;
            for (int i = nIndexSeparateurDec - 1; i >= 0; i--)
            {
                sbRetour.Insert(0, strTexte[i]);
                if(nNbParGroupe == 0)
                {
                    sbRetour.Insert(1, m_strSeparateurMilliers);
                    nNbParGroupe = 3;
                }
                nNbParGroupe --;
            }
            nNbParGroupe = 3;
            for (int i = nIndexSeparateurDec +1 ; i < strTexte.Length; i++)
            {
                sbRetour.Append(strTexte[i]);
                if (nNbParGroupe == 0)
                {
                    sbRetour.Insert(sbRetour.Length - 1, m_strSeparateurMilliers);
                    nNbParGroupe = 3;
                }
                nNbParGroupe--;
            }

            return sbRetour.ToString(); ;
        }

		/// ///////////////////////////////////////////////////
		public bool IsSet()
		{
			return DoubleValue != null; 
		}


		/// ///////////////////////////////////////////////////
		[DefaultValue(0.0)]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
		public Double? DoubleValue
		{
			get
			{
				string strText = Text;
                if (strText.Trim().Length == 0)
                {
                    if (m_bNullAutorise)
                        return null;
                    else
                        return (double)0;
                }
				try
				{
					double dbVal = Double.Parse ( Text );
					return dbVal;
				}
				catch
				{
					if ( strText.IndexOf('.') >= 0 )
						strText = strText.Replace('.',',');
					else
						strText = strText.Replace(',','.');
					try
					{
						double dbVal = Double.Parse ( strText );
						return dbVal;
					}
					catch
					{
						if (m_bNullAutorise)
							return null;
						else
							return (double)0;
					}
				}
			}
			set
			{
				if (value == null)
					Text = "";
				else if (value is Double)
				{
					if (Double.IsNaN((double)value))
						Text = "";
					else
						Text = GetString((double)value);
				}
				else
					Text = value.ToString();
			}
		}

		/// ///////////////////////////////////////////////////
		private string GetString ( double? fValeur )
		{
			if (fValeur == null)
				return "";
			double fTmp = (double)fValeur;
			if(  m_nArrondi >= 0 )
				fTmp = Sc2iMath.RoundUp(fTmp, m_nArrondi);
			if ( m_nArrondi > 0 )
			{
				string strFormat = "0.0000000000000000000000000000000000".Substring(0, m_nNbDecimalesMini+2);
				if ( m_nArrondi > m_nNbDecimalesMini )
					strFormat += "#############################".Substring(0, m_nArrondi-m_nNbDecimalesMini);
				return fTmp.ToString(strFormat);
			}
			return fTmp.ToString("0");
		}



		/// ///////////////////////////////////////////////////
		public int? IntValue
		{
			get
			{
				try
				{
					return Int32.Parse ( Text );
				}
				catch{}
				if ( NullAutorise )
					return null;
				return 0;
			}
			set
			{
				if (value == null)
					Text = "";
				else
					Text = value.ToString();
			}
		}

		/// ///////////////////////////////////////////////////
		private void C2iTextBoxNumerique_Validated(object sender, System.EventArgs e)
		{
			if ( IsSet() && Text != "")
			{
				if ( DecimalAutorise )
					Text = GetString (DoubleValue);
				else
					Text = IntValue.ToString();
			}
			else
				Text = "";
		}

		private void C2iTextBoxNumerique_Enter(object sender, System.EventArgs e)
		{
			if(m_bSelectAllOnEnter)
				SelectAll();
		}

		

		/// ///////////////////////////////////////////////////
		public bool DecimalAutorise
		{
			get
			{
				return m_bDecimalAutorise;
			}
			set
			{
				m_bDecimalAutorise = value;
			}
		}

		/// ///////////////////////////////////////////////////
		public bool NullAutorise
		{
			get
			{
				return m_bNullAutorise;
			}
			set
			{
				m_bNullAutorise = value;
			}
		}

		/// ///////////////////////////////////////////////////
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			Rectangle rct = ClientRectangle;
			Brush br = new SolidBrush(BackColor);
			pevent.Graphics.FillRectangle(br, rct);

			br.Dispose();
		}
		/// ///////////////////////////////////////////////////
		protected override void OnPaint(PaintEventArgs e)
		{
			TextFormatFlags flags = TextFormatFlags.Default;
			switch (TextAlign)
			{
				case HorizontalAlignment.Left:
					flags = TextFormatFlags.Left;
					break;
				case HorizontalAlignment.Center:
					flags = TextFormatFlags.HorizontalCenter;
					break;
				case HorizontalAlignment.Right:
					flags = TextFormatFlags.Right;
					break;
			}
			if (WordWrap)
				flags |= TextFormatFlags.WordBreak;
			flags |= TextFormatFlags.HidePrefix;
            string strAffiche = GetTexte();
            strAffiche = AppliqueSeparateurMilliers(strAffiche);
            Color c = ForeColor;
            if (strAffiche.Length == 0)
            {
                strAffiche = m_strTexteSiVide;
                c = Color.LightGray;
            }
			TextRenderer.DrawText(e.Graphics, strAffiche, Font, ClientRectangle, c, BackColor, flags);
		}
	}
}
