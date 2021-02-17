using System;

namespace sc2i.common
{
	/// <summary>
	/// Description r�sum�e de I2iObjetServer.
	/// </summary>
	public interface I2iMarshalObject
	{
		void RenouvelleBailParAppel();
        string UniqueId { get; }
	}

	public interface I2iMarshalObjectDeSession : I2iMarshalObject
	{
		int IdSession {get;set;}
	}
}
