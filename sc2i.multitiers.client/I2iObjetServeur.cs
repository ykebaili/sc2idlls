using System;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Description r�sum�e de I2iObjetServer.
	/// </summary>
	public interface I2iObjetServeur
	{
		int IdSession{get;set;}
		void RenouvelleBailParAppel();

	}
}
