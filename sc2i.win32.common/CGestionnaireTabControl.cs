using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using sc2i.common;

namespace sc2i.win32.common
{
	public delegate CResultAErreur EventOnPageHandler(object page);

	public class CGestionnaireTabControl : Component
	{
		private Dictionary<object, bool> m_pagesInitialisees = new Dictionary<object, bool>();

		//-------------------------------------------------------------------------
		/// <summary>
		/// Se déclenche pour lancer l'initialisation d'une page
		/// </summary>
		public event EventOnPageHandler OnInitPage;

		/// <summary>
		/// Se déclenche pour lancer la mise à jour d'une page
		/// </summary>
		public event EventOnPageHandler OnMajChampsPage;


		public void ForceInitPageActive()
		{
			if(m_tabControl != null)
				InitPage(m_tabControl.SelectedTab);
		}

		//-------------------------------------------------------------------------
		private Crownwood.Magic.Controls.TabControl m_tabControl = null;
		public Crownwood.Magic.Controls.TabControl TabControl
		{
			get
			{
				return m_tabControl;
			}
			set
			{
				m_tabControl = value;
				if (m_tabControl != null)
				{
					m_tabControl.SelectionChanged += new EventHandler(m_tabControl_SelectionChanged);
				}
			}
		}


		//-------------------------------------------------------------------------
		void m_tabControl_SelectionChanged(object sender, EventArgs e)
		{
			if (DesignMode)
				return;
			if (m_tabControl == null)
				return;
			if (!m_pagesInitialisees.ContainsKey(m_tabControl.SelectedTab) ||
				!m_pagesInitialisees[m_tabControl.SelectedTab])
			{
				CResultAErreur result = InitPage(m_tabControl.SelectedTab);
				if (!result)
					CFormAlerte.Afficher(result);
			}
		}

		//---------------------------------------------------
		public CResultAErreur InitPage(object page)
		{
			CResultAErreur result = CResultAErreur.True;
			if (OnInitPage != null && page != null)
			{

				result = OnInitPage(page);
				if (result)
				{
					m_pagesInitialisees[page] = true;
				}
				else
				{
					m_pagesInitialisees[page] = false;
				}
			}
			return result;
		}

		//---------------------------------------
		public CResultAErreur MajPage(object page)
		{
			if (OnMajChampsPage != null)
				return OnMajChampsPage(page);
			return CResultAErreur.True;
		}

		//---------------------------------------
		public CResultAErreur MAJPages()
		{
			CResultAErreur result = CResultAErreur.True;
			if (OnMajChampsPage != null)
			{
				foreach (KeyValuePair<object, bool> infoPage in m_pagesInitialisees)
					if (infoPage.Value)
					{
						result = OnMajChampsPage(infoPage.Key);
						if (!result)
							return result;
					}
			}
			return result;
		}


		public void Reset()
		{
			m_pagesInitialisees.Clear();
		}
	}
}
