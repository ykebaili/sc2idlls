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
	public partial class CDialogResultBouton : UserControl
	{

		public event ClicBoutonDialogResult ClicBouton;


		public CDialogResultBouton()
		{
			InitializeComponent();
			Actual();
		}

		private CDialogResultProviderForBouton Provider
		{
			get
			{
				return m_dialogResultProviderForBouton;
			}
		}
		public Button Bouton
		{
			get
			{
				return m_btn;
			}
		}
		public DialogResult ResultatAssocie
		{
			get
			{
				return Provider.Resultat;
			}
			set
			{
				Provider.Resultat = value;
				Actual();
			}
		}

		private string m_strTexteAffiche = null;
		/// <summary>
		/// Null pour passer en mode automatique
		/// </summary>
		public string TexteAffiche
		{
			get
			{
				return m_btn.Text;
			}
			set
			{
				m_strTexteAffiche = value;
				ActualTexte();
			}
		}

		private void Actual()
		{
			ActualTexte();
			ActualDesign();
		}
		private void ActualTexte()
		{
			string strTexte = "";
			if(m_strTexteAffiche == null)
				switch (ResultatAssocie)
				{
					case DialogResult.Abort:	strTexte = I.T("Abort|119");	break;
					case DialogResult.Cancel:	strTexte = I.T("Cancel|11");	break;
					case DialogResult.Ignore:	strTexte = I.T("Ignore|120");	break;
					case DialogResult.No:		strTexte = I.T("No|6");			break;
					case DialogResult.OK:		strTexte = I.T("Ok|10");		break;
					case DialogResult.Retry:	strTexte = I.T("Retry|118");	break;
					case DialogResult.Yes:		strTexte = I.T("Yes|5");		break;
					case DialogResult.None:
					default:					strTexte = "";					break;
				}
			else
				strTexte = m_strTexteAffiche;

			m_btn.Text = strTexte;
		}
		private void ActualDesign()
		{
		}

		private bool m_dialogResultProviderForBouton_ClicBouton()
		{
			if (ClicBouton != null)
				return ClicBouton();
			else 
				return true;
		}
	}
}
