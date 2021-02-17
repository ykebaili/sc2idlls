using System;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Description r�sum�e de IDatabaseConnexionSynchronisable.
	/// </summary>
	public interface IDatabaseConnexionSynchronisable : IDatabaseConnexion
	{
		//Active ou d�sactive l'enregistrement de log de synchronisation
		bool EnableLogSynchronisation{get;set;}

		//Id de session de synchronisation en cours
		int IdSyncSession{get;}

		//Force l'id de synchronisation en cours � une valeur, jusqu'� Unlock
		void LockSyncSessionLocalTo ( int nId );

		void UnlockSyncSessionLocal();

		//Augmente l'id de session de synchronisation
		void IncrementeSyncSession();

		void SetSyncSession ( int nVersion );
			

		//Derni�re version mise dans la base principale
		int LastSyncIdPutInBasePrincipale{get;set;}

		//Derni�re version vue de la base principale
		int LastSyncIdVueDeBasePrincipale{get;set;}
	}
}
