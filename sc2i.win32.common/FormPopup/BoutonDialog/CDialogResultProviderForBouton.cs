using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace sc2i.win32.common
{
	public delegate bool ClicBoutonDialogResult();

	public partial class CDialogResultProviderForBouton : Component
	{
		public event ClicBoutonDialogResult ClicBouton;

		private Button m_btn;
		public Button Bouton
		{
			get
			{
				return m_btn;
			}
			set
			{
				if (value != m_btn)
				{
					if (m_btn != null)
						m_btn.Click -= m_btn_Click;

					m_btn = value;
					m_btn.Click += new EventHandler(m_btn_Click);
					ActualBehaviorBouton();
				}
			}
		}

		private void ActualBehaviorBouton()
		{
			if (ComportementAutomatique && Bouton != null)
			{
				Form frm = Bouton.FindForm();
				if (frm != null)
				{
					if (Resultat == DialogResult.Abort || Resultat == DialogResult.Cancel)
						frm.CancelButton = Bouton;
				}
			}
		}
		private bool m_bAutoBehavior = true;
		[DefaultValue(true)]
		public bool ComportementAutomatique
		{
			get
			{
				return m_bAutoBehavior;
			}
			set
			{
				if(value != m_bAutoBehavior && !value)
				{
					
					if (Bouton != null)
					{
						Form frm = Bouton.FindForm();
						if (frm != null)
						{
							if (Resultat == DialogResult.Abort || Resultat == DialogResult.Cancel)
								frm.CancelButton = Bouton;
						}
					}
				}
				m_bAutoBehavior = value;
			}
		}


		private DialogResult m_result = DialogResult.OK;
		[DefaultValue(DialogResult.OK)]
		public DialogResult Resultat
		{
			get
			{
				return m_result;
			}
			set
			{
				m_result = value;
				ActualBehaviorBouton();
			}
		}

		private bool m_bAutoClose = true;
		[DefaultValue(true)]
		public bool FermetureAutomatique
		{
			get
			{
				return m_bAutoClose;
			}
			set
			{
				m_bAutoClose = value;
			}
		}

		private void m_btn_Click(object sender, EventArgs e)
		{
			bool bClicValide = true;
			if (ClicBouton != null)
				bClicValide = ClicBouton();

			if (bClicValide)
			{
				Form frm = m_btn.FindForm();
				if (frm != null)
				{
					frm.DialogResult = Resultat;
					if (FermetureAutomatique)
						frm.Close();
				}
			}
		}

	}
}
