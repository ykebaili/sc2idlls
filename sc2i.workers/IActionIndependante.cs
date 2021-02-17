using System;

using sc2i.common;
using sc2i.multitiers.client;

namespace sc2i.workers
{
	/// <summary>
	/// Description r�sum�e de IActionIndependance.
	/// </summary>
	public interface IActionIndependante : I2iMarshalObjectDeSession
	{
		CResultAErreur RunAction( object parametre);

		string IdAction{get;}
	}
}
