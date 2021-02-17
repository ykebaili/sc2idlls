using System;
using System.Collections;


using sc2i.common;
using System.Text;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Une session correspondant à un traitement isolé sur un serveur
	/// A utiliser quand on veut créer un process sur 
	/// le serveur.
	/// La session est un indicateur de progressio, on peut
	/// donc l'interroger pour savoir où elle en est !
	/// </summary>
	public class CSessionProcessServeurSuivi : CSessionClient, IIndicateurProgression
	{
		private string m_strNomUtilisateurSource = "";

		//Gère son propre indicateur de progression
		CGestionnaireSegmentsProgression m_gestionnaireProgression = new CGestionnaireSegmentsProgression();

		//Liste des indicateurs de progression externes attachés
		ArrayList m_listeIndicateursProgression = new ArrayList();

		private double m_fValeurProgression = 0;
		private string m_strInfoProgression = "";
		private bool m_bCanCancel = true;
		private ArrayList m_listeLibelles = new ArrayList();

		private CSessionClient m_sessionParente = null;

		private C2iSponsor m_sponsor = new C2iSponsor();

		public CSessionProcessServeurSuivi()
		{
			m_gestionnaireProgression.MaxValue = 100;
			m_gestionnaireProgression.MinValue = 0;
			m_gestionnaireProgression.PushSegment ( 0, 100 );
		}

		/// //////////////////////////////////////////
		public override void CloseSession()
		{
			base.CloseSession ();
			m_sponsor.UnregisterAll();
		}


		/// //////////////////////////////////////////
		public CResultAErreur OpenSession ( 
			IAuthentificationSession authentification,
			string strDescription,
			CSessionClient sessionParente )
		{
			CResultAErreur result = CResultAErreur.True;
            if (sessionParente != null)
            {
                m_sessionParente = sessionParente;
                m_sponsor.Register(m_sessionParente);
                IInfoUtilisateur info = sessionParente.GetInfoUtilisateur();
                if (info != null)
                    m_strNomUtilisateurSource = info.NomUtilisateur;
                ConfigurationsImpression = sessionParente.ConfigurationsImpression;
            }
            else
                m_strNomUtilisateurSource = I.T("Unknown|10000");
			///////////////////////////////////////////////
			
			return base.OpenSession ( authentification, 
				strDescription,
				ETypeApplicationCliente.Process );
		}

		/// //////////////////////////////////////////
		public string NomUtilisateurCreateur
		{
			get
			{
				return m_strNomUtilisateurSource;
			}
		}

		#region Membres de IIndicateurProgression

		/// //////////////////////////////////////////
		public void PushSegment(int nMin, int nMax)
		{
			m_gestionnaireProgression.PushSegment ( nMin, nMax );
			foreach ( IIndicateurProgression indicateur in m_listeIndicateursProgression.ToArray() )
			{
				try
				{
					indicateur.PushSegment ( nMin, nMax );
				}
				catch{}
			}
		}

		/// //////////////////////////////////////////
		public void PushLibelle(string strLibelle)
		{
			m_listeLibelles.Add(strLibelle);
		}

		/// //////////////////////////////////////////
		public void PopLibelle()
		{
			if (m_listeLibelles.Count > 0)
				m_listeLibelles.RemoveAt(m_listeLibelles.Count - 1);
		}

		/// //////////////////////////////////////////
		public void SetValue(int nValue)
		{
			m_fValeurProgression = m_gestionnaireProgression.GetValeurReelle ( nValue );
			foreach ( IIndicateurProgression indicateur in m_listeIndicateursProgression.ToArray() )
			{
				try
				{
					indicateur.SetValue ( nValue );
				}
				catch{}
			}

		}

		/// //////////////////////////////////////////
		public bool CancelRequest
		{
			get
			{
				foreach ( IIndicateurProgression indicateur in m_listeIndicateursProgression.ToArray() )
				{
					try
					{
						if ( indicateur.CancelRequest )
							return true;
					}
					catch
					{	
					}
				}
				return false;
			}
		}

		/// //////////////////////////////////////////
		public void PopSegment()
		{
			m_gestionnaireProgression.PopSegment();
			foreach ( IIndicateurProgression indicateur in m_listeIndicateursProgression.ToArray() )
			{
				try
				{
					indicateur.PopSegment();
				}
				catch
				{	
				}
			}
		}

		/// //////////////////////////////////////////
		public void SetInfo(string strInfo)
		{
			m_strInfoProgression = strInfo;
			StringBuilder bl = new StringBuilder();
			foreach (string strLibelle in m_listeLibelles)
				bl.Append(strLibelle + "/");
			bl.Append(strInfo);
			foreach ( IIndicateurProgression indicateur in m_listeIndicateursProgression.ToArray() )
			{
				try
				{
					indicateur.SetInfo(bl.ToString());
				}
				catch
				{	
				}
			}
		}

		/// //////////////////////////////////////////
		public bool CanCancel
		{
			get
			{
				return CanCancel;
			}
			set
			{
				m_bCanCancel = value;
				foreach ( IIndicateurProgression indicateur in m_listeIndicateursProgression.ToArray() )
				{
					try
					{
						indicateur.CanCancel = value;
					}
					catch
					{	
					}
				}
			}
		}


		/// //////////////////////////////////////////
		public void SetBornesSegment(int nMin, int nMax)
		{
			m_gestionnaireProgression.MaxValue = nMax;
			m_gestionnaireProgression.MinValue = nMin;

			foreach ( IIndicateurProgression indicateur in m_listeIndicateursProgression.ToArray() )
			{
				try
				{
					indicateur.SetBornesSegment ( nMin, nMax );
				}
				catch
				{	
				}
			}
		}

		public void Masquer(bool bMasquer)
		{
		}

		#endregion

		/// //////////////////////////////////////////
		public void AttacheIndicateur ( IIndicateurProgression indicateur )
		{
			try
			{
				lock( m_listeIndicateursProgression )
				{
					indicateur.SetBornesSegment ( m_gestionnaireProgression.MinValue, m_gestionnaireProgression.MaxValue );
					indicateur.SetInfo ( m_strInfoProgression );
					indicateur.CanCancel = m_bCanCancel;
					m_listeIndicateursProgression.Add ( indicateur );
				}
			}
			catch
			{
			}
		}

		/// //////////////////////////////////////////
		public override IInfoUtilisateur GetInfoUtilisateur()
		{
			try
			{
				return m_sessionParente.GetInfoUtilisateur();
			}
			catch
			{
				return base.GetInfoUtilisateur();
			}
		}

		
					
	}
}
