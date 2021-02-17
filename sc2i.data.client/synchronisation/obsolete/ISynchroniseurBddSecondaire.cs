using System;
using System.Collections;
using sc2i.common;
using sc2i.multitiers.client;
using System.Data;

namespace sc2i.data.synchronisation
{
	/// <summary>
	/// PErmet de récuperer les modifs faites sur la base primaire et les mettre
	/// dans la base secondaire
	/// </summary>
	public interface ISynchroniseurBddSecondaire : I2iMarshalObject
	{
		#region Synchronisation Main->Secondaire
		//Retourne un id de process de synchronisation
		int StartProcessSynchronisationMainToSecondaire ( 
			IAuthentificationSession authentification,
			int nIdLastIdSyncMain,
			string strCodeGroupeSynchro,
			int nVersionParametreSynchronisation);

		//Retourne l'id de la base vue par le process de synchro
		int GetVersionVueDeBaseMain ( int nIdProcessSynchro );

		//Retourne vrai s'il reste des données d'ajout ou update à synchronisation
		bool HasDataToAddOrUpdateMainToSecondaire ( int nIdProcessSynchro );

		//Retourne un dataset (partiel) avec des données à ajouter ou à modifier 
		DataSet GetDataToAddOrUpdateMainToSecondaire ( int nIdProcessSynchro, IIndicateurProgression indicateur );

		//Retourne la liste des tables concernées par un update ou un insert
		string[] GetListeTablesConcerneesParAddOrUpdateMainToSecondaire ( int nIdProcessSynchro );

		//Retourne les éléments à ajouter ou updater dans une table
		//Tant que la table retournée possède des enregistrements,
		//il faut rappeler cette fonction car elle envoie la table par paquets
		DataTable GetDataToAddOrUpdateMainToSecondaire ( int nIdProcessSynchro,string strNomTable,int[] listeIdsPresentsInSecondaire );

		byte[] GetBlob ( int nIdProcessSynchro, string strNomTable, string strNomChampBlob, int nIdElement );

		//Retourne la liste des tables concernées par un delete 
		string[] GetListeTablesConcerneesParDeleteSynchronisationMainToSecondaire ( int nIdProcessSynchro );
		
		//Retourne la liste des ids à supprimer dans la table spécifiée. La liste d'ids passée
		//en paramètre contient la liste des ids connues par le secondaire.
		int[] GetListeIdsToDeleteInTable ( int nIdProcessSynchro, string strNomTable, int[] listeIdsOnSecondaire );

		//Permet de suivre l'avancée lors d'un appel asynchrone
		//Retourne le nom de la table en cours de lecture
		string GetNomTableAddOrUpdateEnCoursMainToSecondaire ( int nIdProcessSynchro );

		//Permet de suivre l'avancée lors d'un appel asynchrone
		//Retourne le taux d'avancement du add entre 0 et 100
		int GetPourcentageAddOrUpdateEnCoursMainToSecondaire ( int nIdProcessSynchro );

		void EndProcessSynchronisationMainToSecondaire ( int nIdProcessSynchro );

		/// ////////////////////////////////////////////////////////
		/// Renouvelle le bail de vie du process de nNbMinutes
		void RenouvelleBailMainToSecondaire ( int nProcess, int nNbMinutes );
		#endregion

		#region Synchronisation secondaire->Main

		//Commence un process de synchronisation de secondaire vers main et retourne son id
		int StartProcessSynchronisationSecondaireToMain ( 
			IAuthentificationSession authentification,
			int nIdLastVersionVueDeMainParSecondaire );

		/// ////////////////////////////////////////////////////////
		//Retourne l'id de sync session sous laquelle les modifs seront écrites
		int GetIdSyncSessionAlloueeInBaseMain ( int nIdProcess );

		/// ////////////////////////////////////////////////////////
		/// Retourne un dataset avec la table de mappage des champs id nouveaux
		/// le dataset peut également contenir des données à ajouter à la base
		/// secondaire si des suppressions ont été refusées
		DataSet SendAddsUpdateAndDelete ( int nIdProcess, DataSet ds, IIndicateurProgression indicateur );

		/// ////////////////////////////////////////////////////////
		bool SendBlob ( int nIdProcess, string strNomTable, string strNomChampBlob, int nIdElement, byte[] data );

		//Permet de suivre l'avancée lors d'un appel asynchrone
		//Retourne le nom de la table en cours de lecture
		string GetNomTableAddOrUpdateEnCoursSecondaireToMain ( int nIdProcessSynchro );

		//Permet de suivre l'avancée lors d'un appel asynchrone
		//Retourne le taux d'avancement du add entre 0 et 100
		int GetPourcentageAddOrUpdateEnCoursSecondaireToMain ( int nIdProcessSynchro );

		/// ////////////////////////////////////////////////////////
		/// Termine la synchronisation sec->Main
		void CommitProcessSynchronisationSecondaireToMain ( int nIdProcess );

		/// ////////////////////////////////////////////////////////
		/// Termine la synchronisation sec->Main
		void CancelProcessSynchronisationSecondaireToMain ( int nIdProcess );

		/// ////////////////////////////////////////////////////////
		/// Renouvelle le bail de vie du process de nNbMinutes
		void RenouvelleBailSecondaireToMain ( int nProcess, int nNbMinutes );
		#endregion
	}

/*	public class CSynchroniseurBddSecondaire
	{


		private static ISynchroniseurBddSecondaire GetSynchroniseur( int nIdSession )
		{
			return (ISynchroniseurBddSecondaire)C2iFactory.GetNewObjetForSession ( "CSynchroniseurBddSecondaireServeur", typeof(ISynchroniseurBdd), nIdSession );
		}

		#region Synchronisation Main->Secondaire

		/// /////////////////////////////////////////////////////////
		public static int StartProcessSynchronisationMainToSecondaire (
			int nIdSessionClient,
			int nIdLastIdSyncMain,
			string strTypeSynchro,
			int nIdUtilisateur,
			int nVersionParametreSynchronisation,
			bool bCreateSession)
		{
			return GetSynchroniseur ( nIdSessionClient ).StartProcessSynchronisationMainToSecondaire(
				nIdLastIdSyncMain,
				strTypeSynchro,
				nIdUtilisateur,
				nVersionParametreSynchronisation );
		}

		/// /////////////////////////////////////////////////////////
		public static int GetVersionVueDeBaseMain ( int nIdSession, int nIdProcessSynchro )
		{
			return GetSynchroniseur ( nIdSession ).GetVersionVueDeBaseMain( nIdProcessSynchro );
		}

		/// /////////////////////////////////////////////////////////
		public static bool HasDataToAddOrUpdateMainToSecondaire ( int nIdSession, int nIdProcessSynchro )
		{
			return GetSynchroniseur ( nIdSession ).HasDataToAddOrUpdateMainToSecondaire ( nIdProcessSynchro );
		}


		//Retourne un dataset (partiel) avec des données à ajouter ou à modifier 
		/// /////////////////////////////////////////////////////////
		public static DataSet GetDataToAddOrUpdateMainToSecondaire ( int nIdSession, int nIdProcessSynchro, IIndicateurProgression indicateur )
		{
			return GetSynchroniseur ( nIdSession ).GetDataToAddOrUpdateMainToSecondaire ( nIdProcessSynchro, indicateur );
		}

		//Retourne un dataset contenant la table d'entrées log synchro
		/// /////////////////////////////////////////////////////////
		public static DataSet GetDeletesMainToSecondaire ( int nIdSession, int nIdProcessSynchro, IIndicateurProgression indicateur )
		{
		{
			return GetSynchroniseur ( nIdSession ).GetDeletesMainToSecondaire ( nIdProcessSynchro, indicateur );
		}
		}

		/// /////////////////////////////////////////////////////////
		public static void EndProcessSynchronisationMainToSecondaire ( int nIdSession, int nIdProcessSynchro )
		{
			GetSynchroniseur ( nIdSession ).EndProcessSynchronisationMainToSecondaire ( nIdProcessSynchro );

		}

		/// /////////////////////////////////////////////////////////
		public static string GetNomTableAddOrUpdateEnCoursMainToSecondaire ( int nIdSession, int nIdProcessSynchro )
		{
			return GetSynchroniseur ( nIdSession ).GetNomTableAddOrUpdateEnCoursMainToSecondaire ( nIdProcessSynchro );
		}

		/// /////////////////////////////////////////////////////////
		public static int GetPourcentageAddOrUpdateEnCoursMainToSecondaire ( int nIdSession, int nIdProcessSynchro )
		{
			return GetSynchroniseur ( nIdSession ).GetPourcentageAddOrUpdateEnCoursMainToSecondaire ( nIdProcessSynchro );
		}

		/// ////////////////////////////////////////////////////////
		/// Renouvelle le bail de vie du process de nNbMinutes
		public static void RenouvelleBailMainToSecondaire (  int nIdSession, int nProcess, int nNbMinutes )
		{
			GetSynchroniseur ( nIdSession ).RenouvelleBailMainToSecondaire ( nProcess, nNbMinutes );
		}

		#endregion

		#region Synchronisation secondaire->Main

		/// ////////////////////////////////////////////////////////
		//Commence un process de synchronisation de secondaire vers main et retourne son id
		public static int StartProcessSynchronisationSecondaireToMain ( int nIdSession, int nIdLastVersionVueDeMainParSecondaire )
		{
			return GetSynchroniseur ( nIdSession ).StartProcessSynchronisationSecondaireToMain ( nIdLastVersionVueDeMainParSecondaire );
		}

		/// ////////////////////////////////////////////////////////
		//Retourne l'id de sync session sous laquelle les modifs seront écrites
		public static int GetIdSyncSessionAlloueeInBaseMain ( int nIdSession, int nIdProcess )
		{
			return GetSynchroniseur ( nIdSession ).GetIdSyncSessionAlloueeInBaseMain ( nIdProcess );
		}

		/// ////////////////////////////////////////////////////////
		/// Retourne un dataset avec la table de mappage des champs id nouveaux
		public static DataSet SendAddsUpdateAndDelete ( int nIdSession, int nIdProcess, DataSet ds, IIndicateurProgression indicateur )
		{
			return GetSynchroniseur ( nIdSession ).SendAddsUpdateAndDelete ( nIdProcess, ds, indicateur );
		}
		/// /////////////////////////////////////////////////////////
		public static string GetNomTableAddOrUpdateEnCoursSecondaireToMain ( int nIdSession, int nIdProcessSynchro )
		{
			return GetSynchroniseur ( nIdSession ).GetNomTableAddOrUpdateEnCoursSecondaireToMain ( nIdProcessSynchro );
		}

		/// /////////////////////////////////////////////////////////
		public static int GetPourcentageAddOrUpdateEnCoursSecondaireToMain ( int nIdSession, int nIdProcessSynchro )
		{
			return GetSynchroniseur ( nIdSession ).GetPourcentageAddOrUpdateEnCoursSecondaireToMain ( nIdProcessSynchro );
		}

		/// ////////////////////////////////////////////////////////
		/// Termine la synchronisation sec->Main
		public static void CommitProcessSynchronisationSecondaireToMain ( int nIdSession, int nIdProcess )
		{
			GetSynchroniseur ( nIdSession ).CommitProcessSynchronisationSecondaireToMain ( nIdProcess );
		}

		/// ////////////////////////////////////////////////////////
		/// Termine la synchronisation sec->Main
		public static void CancelProcessSynchronisationSecondaireToMain (  int nIdSession, int nIdProcess )
		{
			GetSynchroniseur ( nIdSession ).CancelProcessSynchronisationSecondaireToMain ( nIdProcess );
		}

		/// ////////////////////////////////////////////////////////
		/// Renouvelle le bail de vie du process de nNbMinutes
		public static void RenouvelleBailSecondaireToMain (  int nIdSession, int nProcess, int nNbMinutes )
		{
			GetSynchroniseur ( nIdSession ).RenouvelleBailSecondaireToMain ( nProcess, nNbMinutes );
		}
		#endregion
	}*/

	
}
