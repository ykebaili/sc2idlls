using System;

namespace sc2i.common
{
	/// <summary>
	/// Description r�sum�e de IFactory.
	/// </summary>
	public interface IFactory
	{
		MarshalByRefObject GetNewObject ( int nIdSession );
	}
}
