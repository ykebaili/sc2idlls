using System;
using System.Collections;
using System.Data;

using sc2i.common;
using sc2i.multitiers.client;
using sc2i.multitiers.server;
using sc2i.data;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Ajoute une fonction de cache au CObjetServeur
	/// </summary>
	public abstract class CObjetDonneeServeurAvecCache : CObjetServeur
	{

		/// <summary>
		/// //////////////////////////////////////////////////////
		/// </summary>
		public CObjetDonneeServeurAvecCache( int nIdSession )
			:base ( nIdSession )
		{
		}
	}
}
