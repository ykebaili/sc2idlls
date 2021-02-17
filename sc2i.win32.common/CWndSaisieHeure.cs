using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace sc2i.win32.common
{
	public partial class CWndSaisieHeure : UserControl, IControlALockEdition
	{
		CFormatteurTextBoxHeure m_formatteur = new CFormatteurTextBoxHeure();

		private bool m_bIsDuree = true;
		private bool m_bNullAutorise = true;

		//----------------------------------------
		public CWndSaisieHeure()
		{
			InitializeComponent();
			m_formatteur.AttachTo(m_txtHeure);
		}

		//------------------------------------------
		public bool LockEdition
		{
			get
			{
				return m_txtHeure.LockEdition;
			}
			set
			{
				m_txtHeure.LockEdition = value;
				if ( OnChangeLockEdition != null )
					OnChangeLockEdition ( this, new EventArgs() );
			}
		}

		//-----------------------------------------
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

		//-----------------------------------------
		public bool SaisieDuree
		{
			get
			{
				return m_bIsDuree;
			}
			set
			{
				m_bIsDuree = value;
			}
		}


		public event EventHandler OnChangeLockEdition;


		//-----------------------------------------
		public double? ValeurHeure
		{
			get
			{
				double? fVal = VerifieSaisie();
				if (!m_bNullAutorise && fVal == null)
					return 0.0f;
				return fVal;
			}
			set
			{
				if (value == null && NullAutorise)
					Text = "";
				else
				{
					if (value == null)
						Text = "00:00";
					else
					{
						int nPos = m_txtHeure.SelectionStart;
						int nLength = m_txtHeure.SelectionLength;
						int nHeure = (int)value;
						int nMin = (int)((value - nHeure)*6000/100+.5 );
						Text = nHeure.ToString().PadLeft(2, '0') + ":" + nMin.ToString().PadLeft(2, '0');
					}
				}
			}
		}

		//-----------------------------------------
		public override string Text
		{
			get
			{
				return m_txtHeure.Text;
			}
			set
			{
				m_txtHeure.Text = value;
			}
		}


		//-----------------------------------------
		private Double? VerifieSaisie()
		{
			string strVal = Text.Trim();
			string[] strVals = strVal.Split(':');
			string strHeure = "0";
			string strMins = "0";
			if (strVal.Length == 0)
			{
				if (m_bNullAutorise)
					return null;
				return 0.0f;
			}
			if (strVals.Length == 1)
			{
				strHeure = strVals[0];
				strMins = "00";
			}
			else
			{
				strHeure = strVals[0];
				if (strHeure == "")
					strHeure = "0";
				strMins = strVals[1];
			}
			try
			{
				int nHeure = Int16.Parse(strHeure);
				int nMin = Int16.Parse(strMins);
				if (!m_bIsDuree && nHeure >= 24)
				{
					nHeure = 23;
					nMin = 59;
				}
				if (nMin > 59)
					nMin = 59;
				double fResult = (double)nHeure + ((double)nMin) / 60.0f;
				return fResult;
			}
			catch
			{
				return null;
			}
		}

		private void m_txtHeure_Validated(object sender, EventArgs e)
		{
			ValeurHeure = VerifieSaisie();
		}


				

	
	}
}
