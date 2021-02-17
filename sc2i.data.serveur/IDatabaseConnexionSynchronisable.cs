using System;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Description résumée de IDatabaseConnexionSynchronisable.
	/// </summary>
	public interface IDatabaseConnexionSynchronisable : IDatabaseConnexion
	{
		//Active ou désactive l'enregistrement de log de synchronisation
		bool EnableLogSynchronisation{get;set;}

		//Id de session de synchronisation en cours
		int IdSyncSession{get;}

		//Force l'id de synchronisation en cours à une valeur, jusqu'à Unlock
		void LockSyncSessionLocalTo ( int nId );

		void UnlockSyncSessionLocal();

		//Augmente l'id de session de synchronisation
		void IncrementeSyncSession();

		void SetSyncSession ( int nVersion );
			

		//Dernière version mise dans la base principale
		int LastSyncIdPutInBasePrincipale{get;set;}

		//Dernière version vue de la base principale
		int LastSyncIdVueDeBasePrincipale{get;set;}
	}
}
