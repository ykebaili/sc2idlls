using System;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de IFactory.
	/// </summary>
	public interface IFactory
	{
		MarshalByRefObject GetNewObject ( int nIdSession );
	}
}
