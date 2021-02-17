using System;
using System.Collections;
using System.Threading;


using sc2i.common;
using sc2i.multitiers.client;
using sc2i.multitiers.server;
using sc2i.data;

namespace sc2i.data.dynamic.loader
{
	/// <summary>
	/// Description résumée de CFournisseurFiltresForSynchronisation.
	/// </summary>
	public class CFournisseurFiltresForSynchronisation : MarshalByRefObject, IFournisseurServicePourSessionClient
	{
		/// ///////////////////////////////////////////////////////
		public CFournisseurFiltresForSynchronisation()
		{
		}

		/// ///////////////////////////////////////////////////////
		public string TypeService
		{
			get
			{
				return CSc2iDataConst.c_ServiceFiltresSynchronisation;
			}
		}

		/// ///////////////////////////////////////////////////////
		public IServicePourSessionClient GetService ( int nIdSession )
		{
			return new CServiceGetFiltresSynchronisation ( nIdSession );
		}
	}

	/// ///////////////////////////////////////////////////////
	public class CServiceGetFiltresSynchronisation : C2iObjetServeur, IServiceGetFiltresSynchronisation
	{
		/*private class CCacheFiltres
		{
			int m_nIdSession;
			int m_nIdUtilisateur;
			string m_strTypeSynchro = "";
			CFiltresSynchronisation m_filtres = null;
			DateTime m_dtLastUse = DateTime.Now;

			public CCacheFiltres ( int nIdSession, int nIdUtilisateur, string strTypeSynchro, CFiltresSynchronisation filtres )
			{
				m_nIdSession = nIdSession;
				m_nIdUtilisateur = nIdUtilisateur;
				m_filtres = filtres;
				m_strTypeSynchro = strTypeSynchro;
			}

			public int IdSession
			{
				get
				{
					return m_nIdSession;
				}
			}

			public int IdUtilisateur
			{
				get
				{
					return m_nIdUtilisateur;
				}
			}

			public string TypeSynchro
			{
				get
				{
					return m_strTypeSynchro;
				}
			}

			public void RenouvelleLastUse()
			{
				m_dtLastUse = DateTime.Now;
			}

			public CFiltresSynchronisation Filtres
			{
				get
				{
					return m_filtres;
				}
			}

			public DateTime LastDateUse
			{
				get
				{
					return m_dtLastUse;
				}
			}
		}*/


		//private static ArrayList m_cacheFiltres = new ArrayList();

		//Vérifie périodiquement les éléments en cache à supprimer
		//private static Timer m_timer = null;

		/// /////////////////////////////////////////////////////////////
		public CServiceGetFiltresSynchronisation ( int nIdSession )
			:base ( nIdSession )
		{
			//if ( m_timer == null )
				//m_timer = new Timer ( new TimerCallback(OnNettoyage), null, TimeSpan.FromMinutes(10),TimeSpan.FromMinutes(10) );
		}

		/*/// /////////////////////////////////////////////////////////////
		private static void OnNettoyage ( object state )
		{
			lock ( m_cacheFiltres )
			{
				for ( int nCache = m_cacheFiltres.Count-1; nCache >= 0; nCache-- )
				{
					CCacheFiltres cache = (CCacheFiltres)m_cacheFiltres[nCache];
					TimeSpan sp = DateTime.Now - cache.LastDateUse;
					if ( sp.TotalMinutes > 10 )
						m_cacheFiltres.RemoveAt ( nCache );
				}
			}
		}

		/// /////////////////////////////////////////////////////////////
		private static CCacheFiltres GetCache ( int nIdSession, int nIdUtilisateur, string strTypeSynchro )
		{
			lock ( m_cacheFiltres )
			{
				foreach ( CCacheFiltres cache in m_cacheFiltres )
				{
					if ( cache.IdSession == nIdSession && 
						cache.IdUtilisateur == nIdUtilisateur && 
						cache.TypeSynchro == strTypeSynchro )
					{
						return cache;
					}
				}
				return null;
			}
		}

		/// /////////////////////////////////////////////////////////////
		private static void OnUseFiltre ( 
			int nIdSession, 
			int nIdUtilisateur, 
			string strTypeSynchro, 
			CFiltresSynchronisation filtres )
		{
			CCacheFiltres cache = GetCache ( nIdSession, nIdUtilisateur, strTypeSynchro );
			if ( cache != null )
				cache.RenouvelleLastUse();
			else
			{
				cache = new CCacheFiltres ( nIdSession, nIdUtilisateur, strTypeSynchro, filtres );
				lock ( m_cacheFiltres )
				{
					m_cacheFiltres.Add ( cache );
				}
			}
		}*/
						
		/// /////////////////////////////////////////////////////////////
		public CResultAErreur GetFiltresSynchronisation ( string strCodeGroupeSynchronisation )
		{
			CResultAErreur result = CResultAErreur.True;
			/*/CCacheFiltres cache = GetCache ( IdSession, nIdUtilisateur, strTypeSynchronisation );
			if ( cache != null )
			{
				result.Data = cache.Filtres;
				return result;
			}
			*/
			using ( CContexteDonnee contexte = new CContexteDonnee ( IdSession, true, false ) )
			{
				//Cherche le groupe de synchronisation
				CGroupeUtilisateursSynchronisation groupe = new CGroupeUtilisateursSynchronisation ( contexte );
				if ( !groupe.ReadIfExists ( new CFiltreData ( CGroupeUtilisateursSynchronisation.c_champCode+"=@1 or "+
					CGroupeUtilisateursSynchronisation.c_champIdMachine+"=@1",
					strCodeGroupeSynchronisation ) ) )
				{
					result.EmpileErreur(I.T("The group '@1' doesn't exist|121",strCodeGroupeSynchronisation));
					return result;
				}
				CParametreSynchronisationInDb parametreInDb = groupe.ParametreSynchronisation;
				CParametreSynchronisation parametre = parametreInDb.Parametre;
				if ( parametre != null )
				{
					result = parametre.GetFiltresFinaux ( IdSession, groupe );
					if ( result )
					{
						CFiltresSynchronisation filtres = (CFiltresSynchronisation)result.Data;
						//OnUseFiltre ( IdSession, nIdUtilisateur, strTypeSynchronisation, filtres );
						result.Data = filtres;
						return result;
					}
				}
			}
			return result;
		}
	}
}
