using System;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Description résumée de IServicePourSessionClient.
	/// Est un service executé sur le serveur pour une session cliente
	/// </summary>
	public interface IServicePourSessionClient
	{
	}

	public interface IFournisseurServicePourSessionClient
	{
		string TypeService{get;}
		IServicePourSessionClient GetService ( int nIdSession );
	}

	public interface IServicesClientManager
	{
		//void RegisterFournisseur ( IFournisseurServicePourSessionClient fournisseur );
		IFournisseurServicePourSessionClient GetFournisseur ( string strService );
		IServicePourSessionClient GetService ( string strService, int nIdSession );
	}


	public interface IServiceAvecInterface : IServicePourSessionClient
	{
		sc2i.common.CResultAErreur RunService ( object lastData );
	}

}
