using System;
using System.Collections;
using sc2i.common;
using sc2i.multitiers.client;
using System.Data;
using System.Collections.Generic;

namespace sc2i.data
{
	/// <summary>
	/// Description r�sum�e de ISynchroniseurBdd.
	/// </summary>
	public interface ISynchroniseurBdd
	{
		int GetIdSynchroMain();
		int IncrementeIdSynchroMain();
		DataSet GetAddAndUpdateInTableMain ( string strNomTable, int nIdSyncDebut, int nIdSyncFin, string strCodeGroupeSynchronisation );
#if !PDA
		CResultAErreur PutSecondaryIntoMain ( string strConnexionSecondaire );
		
		//le data du result contient le dataset � appliquer � la base distante
		CResultAErreur PutSecondaryIntoMain ( CContexteDonneesSynchroSecondaire donneesSource);

		CResultAErreur PutMainIntoSecondary ( string strConnexionSecondaire, CFiltresSynchronisation filtres, string strCodeGroupeSynchronisation );
		
		//le data du result contient le dataset � appliquer � la base distante
		/// <summary>
		/// R�cup�re les modifs faite dans la base principale � envoyer � la base secondaire
		/// </summary>
		/// <param name="nIdSyncSessionFrom">Id de syncSession de la base principale � partir de laquelle on veut les modifs</param>
		/// <param name="nIdSyncSessionCourantSecondaire">Id de synchro de la base secondaire</param>
		/// <param name="strIdFiltres">Id des filtres � appliquer</param>
		/// <param name="nIdUtilisateur">Id de l'utilisateur (destinataire de la base secondaire)</param>
		/// <returns></returns>
		/// L'id des filtres � appliquer permet de retrouver les filtres � appliquer
		/// sur la base m�re pour la base secondaire
		CResultAErreur GetModifsInMain ( int nIdSyncSessionFrom, int nIdSyncSessionCourantSecondaire, string strCodeGroupeSynchronisation );


        /// <summary>
		/// V�rouille l'id de synchro � une valeur donn�e
		/// </summary>
		/// <param name="nIdSyncSession"></param>
		void LockSyncSessionLocalTo ( int nIdSyncSession );

		void UnlockSyncSessionLocale ();
#endif
	}

    

	public class CSynchroniseurBDD
	{
		private static ISynchroniseurBdd GetSynchroniseur( int nIdSession )
		{
			return (ISynchroniseurBdd)C2iFactory.GetNewObjetForSession ( "CSynchroniseurBddServeur", typeof(ISynchroniseurBdd), nIdSession );
		}
#if !PDA
		/// /////////////////////////////////////////////////////////
		public static CResultAErreur PutSecondaryIntoMain ( int nIdSession, string strConnexionSecondaire )
		{
			CResultAErreur result = CResultAErreur.True;
			ISynchroniseurBdd synchroniseur	= GetSynchroniseur ( nIdSession );
			try
			{
				result = synchroniseur.PutSecondaryIntoMain ( strConnexionSecondaire );
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
			}
			return result;
				
		}


		/// /////////////////////////////////////////////////////////
		public static CResultAErreur PutSecondaryIntoMain ( int nIdSession, CContexteDonneesSynchroSecondaire donneesSource)
		{
			CResultAErreur result = CResultAErreur.True;
			ISynchroniseurBdd synchroniseur	= GetSynchroniseur ( nIdSession );
			try
			{
				result = synchroniseur.PutSecondaryIntoMain ( donneesSource);
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
			}
			return result;
		}
		/// /////////////////////////////////////////////////////////
		public static CResultAErreur PutMainIntoSecondary ( int nIdSession, string strConnexionSecondaire, CFiltresSynchronisation filtres, string strCodeGroupeSynchronisation )
		{
			CResultAErreur result = CResultAErreur.True;
			ISynchroniseurBdd synchroniseur	= GetSynchroniseur ( nIdSession );
			try
			{
				result = synchroniseur.PutMainIntoSecondary ( strConnexionSecondaire, filtres, strCodeGroupeSynchronisation);
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
			}
			return result;
		}



		/// /////////////////////////////////////////////////////////
		public static CResultAErreur GetModifsInMain ( int nIdSession, int nIdSyncSessionFrom, int nIdSyncSessionCourantSecondaire, string strCodeGroupeSynchronisation )
		{
			CResultAErreur result = CResultAErreur.True;
			ISynchroniseurBdd synchroniseur	= GetSynchroniseur ( nIdSession );
			try
			{
				result = synchroniseur.GetModifsInMain ( nIdSyncSessionFrom, nIdSyncSessionFrom, strCodeGroupeSynchronisation);
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
			}
			return result;
		}
#endif
		/// /////////////////////////////////////////////////////////
		public static int GetIdSynchroMain( int nIdSession )
		{
			return GetSynchroniseur(nIdSession).GetIdSynchroMain();
		}

		/// /////////////////////////////////////////////////////////
		public static int IncrementIdSynchroMain(int nIdSession )
		{
			return GetSynchroniseur(nIdSession).IncrementeIdSynchroMain();
		}

		/// /////////////////////////////////////////////////////////
		public static DataSet GetAddAndUpdateInTable ( int nIdSession, string strNomTable, int nSessionSyncDebut, int nSessionSyncFin, string strCodeGroupeSynchronisation )
		{
			return GetSynchroniseur(nIdSession).GetAddAndUpdateInTableMain ( strNomTable, nSessionSyncDebut, nSessionSyncFin,strCodeGroupeSynchronisation );
		}

		/// /////////////////////////////////////////////////////////
		public static void LockSyncSessionLocalTo ( int nIdSession,int nIdSyncSession )
		{
			GetSynchroniseur(nIdSession).LockSyncSessionLocalTo ( nIdSyncSession );
		}

		/// /////////////////////////////////////////////////////////
		public static void UnlockSyncSessionLocal ( int nIdSession )
		{
			GetSynchroniseur ( nIdSession ).UnlockSyncSessionLocale();
		}
	

		/// /////////////////////////////////////////////////////////
		

	}

	
}
