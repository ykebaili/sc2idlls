using System;

namespace sc2i.common
{
	/// <summary>
	/// Description r�sum�e de IErreur.
	/// </summary>
	public interface IErreur
	{
		string Message {get;}
        bool IsAvertissement { get; }
	}
}
