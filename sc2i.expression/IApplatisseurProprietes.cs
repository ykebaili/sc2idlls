using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.expression
{
	/// <summary>
	/// Tout �l�ment qui g�re � plat les propri�t�s de plusieurs �l�ments
	/// </summary>
	public interface IApplatisseurProprietes
	{
		object GetObjetPourPropriete(string strPropriete);

		object GetObjetParDefaut();
	}
}
