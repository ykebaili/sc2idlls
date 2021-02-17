using System;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de IErreur.
	/// </summary>
	public interface IErreur
	{
		string Message {get;}
        bool IsAvertissement { get; }
	}
}
