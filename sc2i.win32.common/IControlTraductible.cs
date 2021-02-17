using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.win32.common
{
	/// <summary>
	/// Permet d'indiquer qu'un contrôle peut être traduit. Le contrôle
	/// transmet les propriétés traductibles par GetListeProprietesATraduire.
	/// </summary>
	interface IControlTraductible
	{
		/// <summary>
		/// Retourne la liste des propriétés qu'il faut traduire
		/// </summary>
		/// <returns></returns>
		List<string> GetListeProprietesATraduire();

		/// <summary>
		/// Retourne la liste de propriétés retournant des objets de la classe 
		/// qu'il faut traduire.
		/// </summary>
		/// <returns></returns>
		List<string> GetListeProprietesSousObjetsATraduire();
	}
}
