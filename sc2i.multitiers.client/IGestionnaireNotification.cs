using System;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Description r�sum�e de IGestionnaireNotification.
	/// </summary>
	public interface IGestionnaireNotification
	{
		//Envoie une notification � tous les r�cepteurs inscrits
		void EnvoieNotifications ( IDonneeNotification[] donnees );

        //Relai d'une notification de serveur � serveur
        void RelaieNotifications(IDonneeNotification[] donnee);

		//Transactions
		void BeginTrans(  );
		void CommitTrans(  );
		void RollbackTrans(  );
	}
}
