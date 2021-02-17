using System;
using System.Collections.Generic;
using sc2i.data;
using sc2i.common;
using System.Collections;
using sc2i.multitiers.client;
using System.Timers;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CContexteExecutionAction.
	/// </summary>
	public class CContexteExecutionAction
	{
		private int c_nDelayRefreshIndicateur = 500;

		private CContexteDonnee m_contexteDonnee = null;
		private CProcessEnExecutionInDb m_processEnExecution = null;
		private object m_objetCible = null;
		private CBrancheProcess m_branche;
		private bool m_bSauvegardeContexteExterne = false;
		private bool m_bHasSessionPropre = false;
		private Timer m_timerRefreshIndicateur = new Timer();

        public class CParametreServiceALancerALaFin
        {
            public readonly CServiceSurClient Service;
            public readonly object Parametre;
            private bool m_bHasRun = false;

            public CParametreServiceALancerALaFin ( CServiceSurClient service, object parametre )
            {
                Service = service;
                Parametre = parametre;
            }

            //--------------------------------------------------------
            public void Run()
            {
                try
                {
                    m_bHasRun = true;
                    Service.RunService(Parametre);
                }
                catch { }
            }
        }

        [NonSerialized]
        private List<CParametreServiceALancerALaFin> m_listeServiceALancerEnFin = new List<CParametreServiceALancerALaFin>();

		private IIndicateurProgression m_indicateurProgression = null;

		ArrayList m_pileInfoElementPourInfoProgress = new ArrayList();

		public CContexteExecutionAction( 
			CProcessEnExecutionInDb processEnExecution, 
			CBrancheProcess branche,
			object objetCible,
			CContexteDonnee contexteDonnee,
			IIndicateurProgression indicateur)
		{
			m_processEnExecution = processEnExecution;
			m_contexteDonnee = contexteDonnee;
			m_branche = branche;
			m_objetCible = objetCible;
			PushElementInfoProgress ( m_objetCible );
			m_indicateurProgression = indicateur;
			m_timerRefreshIndicateur.Elapsed += new ElapsedEventHandler(m_timerRefreshIndicateur_Elapsed);
			m_timerRefreshIndicateur.Interval = c_nDelayRefreshIndicateur;
			
		}

		
		//-------------------------------------------------------------------
		public void PushElementInfoProgress ( object obj )
		{
            try
            {
                if (obj != null && obj is CObjetDonnee)
                    m_pileInfoElementPourInfoProgress.Add(((CObjetDonnee)obj).DescriptionElement);
                else
                    m_pileInfoElementPourInfoProgress.Add("");
            }
            catch { }
		}

		//-------------------------------------------------------------------
		public void PopElementInfoProgress()
		{
			if ( m_pileInfoElementPourInfoProgress.Count > 0 )
				m_pileInfoElementPourInfoProgress.RemoveAt ( m_pileInfoElementPourInfoProgress.Count-1 );
		}

        //-------------------------------------------------------------------
        public void AddServiceALancerALaFin ( CParametreServiceALancerALaFin parametreLancement )
        {
            m_listeServiceALancerEnFin.Add(parametreLancement);
        }

        //-------------------------------------------------------------------
        public void DoTraitementApresExecution()
        {
            foreach (CParametreServiceALancerALaFin service in ServicesALancerALaFin)
            {
                service.Run();
            }
        }


        //-------------------------------------------------------------------
        public IEnumerable<CParametreServiceALancerALaFin> ServicesALancerALaFin
        {
            get
            {
                return m_listeServiceALancerEnFin.AsReadOnly();
            }
        }

        //-------------------------------------------------------------------
        public void ClearServicesALancerALaFin()
        {
            m_listeServiceALancerEnFin.Clear();
        }

		//-------------------------------------------------------------------
		public object ObjetCible
		{
			get
			{
				return m_objetCible;
			}
			set
			{
				m_objetCible = value;
			}
		}


		//-------------------------------------------------------------------
		/// <summary>
		/// Indique qu'en fin de process, le contexte est sauvegarde à l'exterieur
		/// </summary>
		public bool SauvegardeContexteExterne
		{
			get
			{
				return (this.m_bSauvegardeContexteExterne);
			}
			set
			{
				this.m_bSauvegardeContexteExterne = value;
			}
		}

		//-------------------------------------------------------------------
		public IIndicateurProgression IndicateurProgression
		{
			get
			{
				return m_indicateurProgression;
			}
			set
			{
				m_indicateurProgression = value;
			}
		}

		//-------------------------------------------------------------------
		/// <summary>
		/// Affiche les infos de progression
		/// </summary>
		/// <param name="strInfo"></param>
		/// Modif stef 18/11/2008 : n'envoie les infos que toutes les 0,5 secondes maxi
		private DateTime m_lastDateInfo = DateTime.Now;
		private string m_strLastInfo = "";
		public void SetInfoProgression ( string strInfo )
		{
			m_timerRefreshIndicateur.Stop();
			if ( m_indicateurProgression != null )
			{
				TimeSpan sp = DateTime.Now - m_lastDateInfo;
				if (sp.TotalMilliseconds >= c_nDelayRefreshIndicateur)
				{
					if (m_pileInfoElementPourInfoProgress.Count > 0)
						strInfo = strInfo + "\r\n" + m_pileInfoElementPourInfoProgress[m_pileInfoElementPourInfoProgress.Count - 1].ToString();
					try
					{
						m_indicateurProgression.SetInfo(strInfo);
					}
					catch { }
					m_lastDateInfo = DateTime.Now;
				}
				else
				{
					m_strLastInfo = strInfo;
					if (m_pileInfoElementPourInfoProgress.Count > 0)
						m_strLastInfo = strInfo + "\r\n" + m_pileInfoElementPourInfoProgress[m_pileInfoElementPourInfoProgress.Count - 1].ToString();
					m_timerRefreshIndicateur.Start();
				}
			}
		}

		//-------------------------------------------------------------------
		void m_timerRefreshIndicateur_Elapsed(object sender, ElapsedEventArgs e)
		{
			m_timerRefreshIndicateur.Stop();
			if (m_indicateurProgression != null)
			{
				try
				{
					m_indicateurProgression.SetInfo(m_strLastInfo);
				}
				catch { }
			}
		}





		//-------------------------------------------------------------------
		/// <summary>
		/// retourne le contexte de donnée d'execution de l'action
		/// </summary>
		public CContexteDonnee ContexteDonnee
		{
			get
			{
				return m_contexteDonnee;
			}
			set
			{
				m_contexteDonnee = value;
			}
		}

		//-------------------------------------------------------------------
		public CProcessEnExecutionInDb ProcessEnExecution
		{
			get
			{
				return m_processEnExecution;
			}
			set
			{
				m_processEnExecution = value;
			}
		}

		//-------------------------------------------------------------------
		public void OnEndProcess()
		{
            DoTraitementApresExecution();
			if (m_bHasSessionPropre)
			{
				CSessionClient session = CSessionClient.GetSessionForIdSession(IdSession);
				if (session != null)
				{
					try
					{
						session.CloseSession();
					}
					catch { }
				}
			}
            
		}

		//-------------------------------------------------------------------
		public CBrancheProcess Branche
		{
			get
			{
				return m_branche;
			}
		}

		//-------------------------------------------------------------------
		public int IdSession
		{
			get
			{
				return m_contexteDonnee.IdSession;
			}
		}

		//-------------------------------------------------------------------
		public void ChangeIdSession ( int nNewIdSession )
		{
			ContexteDonnee.IdSession = nNewIdSession;
		}

		//-------------------------------------------------------------------
		public bool HasSessionPropre
		{
			get
			{
				return m_bHasSessionPropre;
			}
			set
			{
				m_bHasSessionPropre = value;
			}
		}

		//-------------------------------------------------------------------
		public string LastErreur
		{
			get
			{
				if ( m_branche != null && m_branche.Process != null )
					return m_branche.Process.LastErreur;
				return "";
			}
			set
			{
				if ( m_branche != null && m_branche.Process != null )
					m_branche.Process.LastErreur = value;
			}
		}
	}
}
