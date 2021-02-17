using System;

using sc2i.common;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Description r�sum�e de I2iObjetServeurAllocateur.
	/// </summary>
	public interface I2iObjetServeurFactory
	{
		//////////////////////////////////////////////////////
		I2iMarshalObjectDeSession GetNewObject ( string strClasse, int nIdSession );
	}
}
