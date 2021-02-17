using System;

using sc2i.common;
using sc2i.multitiers.client;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Tous les objets permettant de gérer les transactions sur le serveur
	/// L'objet session est le gestionnaire de transactions par excellence
	/// Dans une application multi-tiers
	/// </summary>

	public interface IServiceTransactions
	{
		CResultAErreur BeginTrans();
		CResultAErreur BeginTrans ( System.Data.IsolationLevel isolationLevel );
		CResultAErreur RollbackTrans();
		CResultAErreur CommitTrans();

		bool AccepteTransactionsImbriquees{get;}
		
		
	}

	public interface IFournisseurServiceTransactionPourSession
	{
		IServiceTransactions GetServiceTransaction ( int nIdSession );
		
		//Les transactions sont executées sur la priorité la plus grande en premier
		int PrioriteTransaction{get;}
	}

}
