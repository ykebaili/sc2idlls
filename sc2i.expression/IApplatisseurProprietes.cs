using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.expression
{
	/// <summary>
	/// Tout élément qui gère à plat les propriétés de plusieurs éléments
	/// </summary>
	public interface IApplatisseurProprietes
	{
		object GetObjetPourPropriete(string strPropriete);

		object GetObjetParDefaut();
	}
}
