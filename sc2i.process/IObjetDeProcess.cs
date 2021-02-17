using System;

using sc2i.common;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de IObjetDeProcess.
	/// </summary>
	public interface IObjetDeProcess : I2iSerializable
	{
		CProcess Process{get;}
		int IdObjetProcess { get;}
		CResultAErreur VerifieDonnees();


	}
}
