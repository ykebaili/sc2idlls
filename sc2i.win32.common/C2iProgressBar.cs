using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using sc2i.common;
using System.Collections;

namespace sc2i.win32.common
{
	public partial class C2iProgressBar : UserControl, IIndicateurProgression
	{
		private bool m_bCancelRequest = false;
		private bool m_bCanCancel = true;

		private string m_strSeparateurLibelles = "/";
		private string m_strSeparateurLibellesInterne = "/";

		private CGestionnaireSegmentsProgression m_gestionnaireSegments = new CGestionnaireSegmentsProgression();
		private ArrayList m_listeLibelles = new ArrayList();

		//---------------------------------
		public C2iProgressBar()
		{
			InitializeComponent();
		}

		//---------------------------------
		public void RequestCancel()
		{
			m_bCancelRequest = true;
		}


		/// /////////////////////////////////////////////////////////////////
		public string SeparateurLibelles
		{
			get
			{
				return m_strSeparateurLibelles;
			}
			set
			{
				m_strSeparateurLibelles = value;
				m_strSeparateurLibellesInterne = m_strSeparateurLibelles;
				m_strSeparateurLibellesInterne = m_strSeparateurLibellesInterne.Replace("\\r", "\r");
				m_strSeparateurLibellesInterne = m_strSeparateurLibellesInterne.Replace("\\n", "\n");
				m_strSeparateurLibellesInterne = m_strSeparateurLibellesInterne.Replace("\\t", "\t");
			}
		}

		/// /////////////////////////////////////////////////////////////////
		public void PushSegment(int nMin, int nMax)
		{
			m_gestionnaireSegments.PushSegment(nMin, nMax);
		}

		/// /////////////////////////////////////////////////////////////////
		public void SetBornesSegment(int nMin, int nMax)
		{
			m_gestionnaireSegments.MinValue = nMin;
			m_gestionnaireSegments.MaxValue = nMax;
		}

		/// /////////////////////////////////////////////////////////////////
		public void PopSegment()
		{
			m_gestionnaireSegments.PopSegment();
		}

        private delegate void SetValueDelegate(int nValue);
		/// /////////////////////////////////////////////////////////////////
		public void SetValue(int nValue)
		{
            if(  IsHandleCreated )
                BeginInvoke(new SetValueDelegate(SetValuePrivate), nValue);
            else
                try
                {
                    SetValuePrivate(nValue);
                }
                catch { }
        }

        /// /////////////////////////////////////////////////////////////////
        private void SetValuePrivate ( int nValue )
        {
			m_progressBar.Value = m_gestionnaireSegments.GetValeurReelle(nValue);
			m_progressBar.Refresh();
		}

		/// /////////////////////////////////////////////////////////////////
		public void PushLibelle(string strLibelle)
		{
			m_listeLibelles.Add(strLibelle);
		}

		/// /////////////////////////////////////////////////////////////////
		public void PopLibelle()
		{
			if (m_listeLibelles.Count > 0)
				m_listeLibelles.RemoveAt(m_listeLibelles.Count - 1);
		}


		/// /////////////////////////////////////////////////////////////////
        private delegate void SetInfoDelegate(string strInfo);
        public void SetInfo(string strInfo)
		{
            if(  IsHandleCreated )
                BeginInvoke(new SetInfoDelegate(SetInfoPrivate), strInfo);
            else
                try
                {
                    SetInfoPrivate(strInfo);
                }
                catch { }
        }

        private void SetInfoPrivate ( string strInfo )
        {			
			StringBuilder bl = new StringBuilder();
			foreach (string strLib in m_listeLibelles)
			{
				bl.Append(strLib);
				bl.Append(m_strSeparateurLibellesInterne);
			}
			bl.Append(strInfo);
			m_labelText.Text = bl.ToString();
			m_labelText.Refresh();
		}

		/// /////////////////////////////////////////////////////////////////
		public bool CancelRequest
		{
			get
			{
				return m_bCancelRequest;
			}
		}

		/// /////////////////////////////////////////////////////////////////
		public bool CanCancel
		{
			get
			{
				return m_bCanCancel;
			}
			set
			{
				m_bCanCancel = value;
				if (OnChangeCanCancel != null)
					OnChangeCanCancel(this, new EventArgs());
			}
		}
		/// /////////////////////////////////////////////////////////////////
        private delegate void MasquerDelegate(bool bMasquer);
        public void Masquer(bool bMasquer)
		{
            if (IsHandleCreated)
                BeginInvoke(new MasquerDelegate(MasquerPrivate), bMasquer);
            else
                try
                {
                    MasquerPrivate(bMasquer);
                }
                catch { };
        }

        private void MasquerPrivate ( bool bMasquer )
        {
			Visible = !bMasquer;
		}

		public event EventHandler OnChangeCanCancel;

		/// /////////////////////////////////////////////////////////////////
        public ContentAlignment TextAlign
		{
			get
			{
				return m_labelText.TextAlign;
			}
			set
			{

				m_labelText.TextAlign = value;
			}
		}
	
	}
}
