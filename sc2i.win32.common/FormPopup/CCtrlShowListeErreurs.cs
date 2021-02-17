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
    public partial class CCtrlShowListeErreurs : UserControl
    {
        public CCtrlShowListeErreurs()
        {
            InitializeComponent();
            
        }

        public List<IErreur> Erreurs
        {
            get
            {
                return m_erreurs;
            }
        }
        private List<IErreur> m_erreurs;
        public void Initialiser(IErreur[] erreurs)
        {
            List<IErreur> errs = new List<IErreur>();
            foreach (IErreur err in erreurs)
                errs.Add(err);
            Initialiser(errs);
        }
		private List<CCtrlShowErreur> m_controlesErreurs;

        public virtual void Initialiser(List<IErreur> erreurs)
        {
			Controls.Clear();
			m_controlesErreurs = new List<CCtrlShowErreur>();
            m_erreurs = erreurs;
			m_bOnlyValidationErrors = true;
			if(erreurs.Count > 0)
			{
				for(int n = Erreurs.Count; n > 0; n--)
				{
					IErreur err = Erreurs[n - 1];
					CCtrlShowErreur ctrl = null;
					if (err is CErreurValidation)
					{
						ctrl = new CCtrlShowErreurValidation();
						ctrl.Initialiser(err);
					}
					else if (err is CErreurSimple)
					{
						ctrl = new CCtrlShowErreur();
						ctrl.Initialiser(err);	
					}
					
					else if (err is CErreurException)
					{
						ctrl = new CCtrlShowErreur();
						ctrl.Initialiser(err);	
					}

					if (m_bOnlyValidationErrors && (!(err is CErreurValidation) || !((CErreurValidation)err).IsAvertissement))
						m_bOnlyValidationErrors = false;

					ctrl.Dock = DockStyle.Top;
					m_controlesErreurs.Add(ctrl);
					Controls.Add(ctrl);
				}
			}
        }

		private bool m_bOnlyValidationErrors = false;
		public bool OnlyValidationErrors
		{
			get
			{
				return m_bOnlyValidationErrors;
			}
		}

		public Size PerfectSize
		{
			get
			{
				int nWidth = 0;
				int nHeight = 0;
				foreach(CCtrlShowErreur ctrlErr in m_controlesErreurs)
				{
					if (nWidth < ctrlErr.PerfectSize.Width)
						nWidth = ctrlErr.PerfectSize.Width;
					nHeight += ctrlErr.PerfectSize.Height;
				}
				return new Size(nWidth +15, nHeight + 15);
			}
		}
		public Size GoodSizeUnextendedControls
		{
			get
			{
				int nWidth = 0;
				int nHeight = 0;
				foreach (CCtrlShowErreur ctrlErr in m_controlesErreurs)
				{
					if (nWidth < ctrlErr.GoodSizeUnextended.Width)
						nWidth = ctrlErr.GoodSizeUnextended.Width;
					nHeight += ctrlErr.GoodSizeUnextended.Height;
				}
				return new Size(nWidth, nHeight);
			}
		}
    }
}
