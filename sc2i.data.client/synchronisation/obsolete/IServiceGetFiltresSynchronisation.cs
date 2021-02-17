using System;

using sc2i.common;
using sc2i.multitiers.client;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de IServiceGetFiltresSynchronisation.
	/// </summary>
	public interface IServiceGetFiltresSynchronisation : IServicePourSessionClient
	{
		//Le data du result contient un CFiltresSynchronisation
		CResultAErreur GetFiltresSynchronisation ( string strCodeGroupeSynchronisation );
	}
}
