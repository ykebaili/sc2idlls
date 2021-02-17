using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using sc2i.common;
using sc2i.data;
using sc2i.win32.common;

namespace sc2i.win32.data.navigation
{
	public partial class CExtendeurFormEditionStandardTabPage : UserControl, IExtendeurFormEditionStandard
	{
		private CFormEditionStandard m_form = null;

		private Crownwood.Magic.Controls.TabPage m_page = null;

		private bool m_bIsInit = false;

		private string m_strTitle = "";

		public CExtendeurFormEditionStandardTabPage()
		{
			InitializeComponent();
		}

		//---------------------------------------------
		/// <summary>
		/// A surcharger absolument
		/// </summary>
		public virtual Type TypeObjetEtendu {
			get
			{
				return null;
			}
		}

        //---------------------------------------------
        protected CFormEditionStandard FormEdition
        {
            get
            {
                return m_form;
            }
        }

		//---------------------------------------------
		protected virtual string Title
		{
			get
			{
				return m_strTitle;
			}
			set
			{
				m_strTitle = value;
				if (m_page != null)
					m_page.Title = m_strTitle;
			}
		}

		//---------------------------------------------
		protected void HidePage()
		{
			if (m_form != null && m_form.TabControl != null &&
				m_page != null &&
				m_form.TabControl.TabPages.Contains(m_page))
				m_form.TabControl.TabPages.Remove(m_page);
		}

		//---------------------------------------------
		protected void ShowPage()
		{
			if (m_form != null && m_form.TabControl != null &&
				m_page != null &&
				!m_form.TabControl.TabPages.Contains(m_page))
				m_form.TabControl.TabPages.Add(m_page);
		}


		//---------------------------------------------
		public virtual void CreateInForm(CFormEditionStandard form)
		{
			if (form.TabControl != null)
			{
				m_page = new Crownwood.Magic.Controls.TabPage();
				m_page.Title = Title;
				m_page.Control = this;
				form.TabControl.TabPages.Add(m_page);
				form.TabControl.SelectionChanged += new EventHandler(TabControl_SelectionChanged);
				m_form = form;
                m_form.OnChangementSurObjet += new EventOnChangementDonnee(m_form_OnChangementSurObjet);
                CWin32Traducteur.Translate(m_page);
			}
		}

        public event EventOnChangementDonnee OnChangementSurObjet;

        void m_form_OnChangementSurObjet(string strNomChamp)
        {
            if (OnChangementSurObjet != null)
                OnChangementSurObjet(strNomChamp);
        }

		//---------------------------------------------
		void TabControl_SelectionChanged(object sender, EventArgs e)
		{
			if (m_form != null && m_form.TabControl != null && m_page != null &&
				m_page == m_form.TabControl.SelectedTab && !m_bIsInit)
				InitChamps();
		}

		//---------------------------------------------
		public CObjetDonnee ObjetEdite
		{
			get
			{
				if (m_form != null)
					return m_form.GetObjetEdite();
				return null;
			}
		}

		//---------------------------------------------
		public virtual CResultAErreur MyInitChamps()
		{
			CResultAErreur result = m_extLinkField.FillDialogFromObjet(ObjetEdite);
			return result;
		}

		
		//---------------------------------------------
		public CResultAErreur InitChamps()
		{
			CResultAErreur result = CResultAErreur.True;
			m_bIsInit = false;
			if (m_page != null && m_form != null &&
				m_form.TabControl != null &&
				m_page == m_form.TabControl.SelectedTab)
			{
				result = MyInitChamps();
				m_bIsInit = result;
			}
			return result;
		}
		//---------------------------------------------
		public virtual CResultAErreur MyMajChamps()
		{
			CResultAErreur result = m_extLinkField.FillObjetFromDialog(ObjetEdite);
			return result;
		}

		//---------------------------------------------
		public virtual CResultAErreur MajChamps()
		{
			if (m_bIsInit)
				return MyMajChamps();
			return CResultAErreur.True;
		}

		public bool LockEdition
		{
			get
			{
				return !m_extModeEdition.ModeEdition;
			}
			set
			{
				m_extModeEdition.ModeEdition = !value;
				if ( OnChangeLockEdition != null )
					OnChangeLockEdition ( this, new EventArgs() );
			}
		}

		public event EventHandler OnChangeLockEdition;

		private void CExtendeurFormEditionStandardTabPage_Load(object sender, EventArgs e)
		{
			CWin32Traducteur.Translate(this);
		}
	}
}
