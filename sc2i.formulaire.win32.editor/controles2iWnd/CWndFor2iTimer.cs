using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;
using System.Windows.Forms;
using sc2i.common.Restrictions;


namespace sc2i.formulaire.win32.controles2iWnd
{
	[AutoExec("Autoexec")]
	public class CWndFor2iTimer : CControlWndFor2iWnd
	{
		private Timer m_timer = new Timer();

		public CWndFor2iTimer()
		{
			m_timer.Enabled = false;
			m_timer.Tick += new EventHandler(m_timer_Tick);
            m_timer.Disposed += new EventHandler(m_timer_Disposed);
		}

        void m_timer_Disposed(object sender, EventArgs e)
        {
            if (m_timer != null)
                m_timer.Stop();
            m_timer = null;
        }


		//-----------------------------------------------------
		public static void Autoexec()
		{
			CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndTimer), typeof(CWndFor2iTimer));
		}


		//-----------------------------------------------------
		protected override void OnChangeElementEdite(object element)
		{
		}

		//-----------------------------------------------------
		protected override void MyCreateControle(CCreateur2iFormulaireV2 createur, C2iWnd wnd, System.Windows.Forms.Control parent, sc2i.expression.IFournisseurProprietesDynamiques fournisseur)
		{
			C2iWndTimer timer = wnd as C2iWndTimer;
			if (timer != null)
			{
				Interval = timer.Period;
				parent.Disposed += new EventHandler(parent_Disposed);
			}
		}

		void parent_Disposed(object sender, EventArgs e)
		{
			if (m_timer != null)
			{
				m_timer.Enabled = false;
				m_timer.Dispose();
			}
		}

		//-----------------------------------------------------
		public int Interval
		{
			get
			{
				if (m_timer != null)
					return (int)m_timer.Interval;
				return 0;
			}
			set
			{
				if (m_timer != null)
				{
					if (value <= 0)
						m_timer.Enabled = false;
					else
					{
						m_timer.Interval = value;
						m_timer.Enabled = true;
					}
				}
			}
		}


		void m_timer_Tick(object sender, EventArgs e)
		{
			CUtilControlesWnd.DeclencheEvenement ( 
				C2iWndTimer.c_strIdEventOnTick,
				this );
		}

        public void Start()
        {
            if (m_timer != null)
                m_timer.Start();
        }

        public void Stop()
        {
            if (m_timer != null)
                m_timer.Stop();
        }

		public override System.Windows.Forms.Control Control
		{
			get { return null; }
		}

		protected override sc2i.common.CResultAErreur MyMajChamps(bool bControlerLesValeursAvantValidation)
		{
			return CResultAErreur.True;
		}

		protected override void MyUpdateValeursCalculees()
		{
		}

        protected override void MyAppliqueRestriction(
            CRestrictionUtilisateurSurType restrictionSurObjetEdite,
            CListeRestrictionsUtilisateurSurType listeRestrictions,
            IGestionnaireReadOnlySysteme gestionnaireReadOnly)
		{
		}
	}
}
