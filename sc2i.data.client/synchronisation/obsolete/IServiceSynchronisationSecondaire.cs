using System;

using sc2i.common;
using sc2i.multitiers.client;

namespace sc2i.data
{
	/// <summary>
	/// Tous les objets permettant de gérer les transactions sur le serveur
	/// L'objet session est le gestionnaire de transactions par excellence
	/// Dans une application multi-tiers
	/// </summary>

	public interface IServiceSynchronisationSecondaire : I2iMarshalObject
	{
		CResultAErreur PutDataToMain ( IIndicateurProgression indicateurProgression );
		CResultAErreur GetDataFromMain ( IIndicateurProgression indicateurProgression, bool bModeRapide );
	}

}
