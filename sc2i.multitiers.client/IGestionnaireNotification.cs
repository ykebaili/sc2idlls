using System;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Description résumée de IGestionnaireNotification.
	/// </summary>
	public interface IGestionnaireNotification
	{
		//Envoie une notification à tous les récepteurs inscrits
		void EnvoieNotifications ( IDonneeNotification[] donnees );

        //Relai d'une notification de serveur à serveur
        void RelaieNotifications(IDonneeNotification[] donnee);

		//Transactions
		void BeginTrans(  );
		void CommitTrans(  );
		void RollbackTrans(  );
	}
}
