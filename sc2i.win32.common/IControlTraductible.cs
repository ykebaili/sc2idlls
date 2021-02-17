using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.win32.common
{
	/// <summary>
	/// Permet d'indiquer qu'un contr�le peut �tre traduit. Le contr�le
	/// transmet les propri�t�s traductibles par GetListeProprietesATraduire.
	/// </summary>
	interface IControlTraductible
	{
		/// <summary>
		/// Retourne la liste des propri�t�s qu'il faut traduire
		/// </summary>
		/// <returns></returns>
		List<string> GetListeProprietesATraduire();

		/// <summary>
		/// Retourne la liste de propri�t�s retournant des objets de la classe 
		/// qu'il faut traduire.
		/// </summary>
		/// <returns></returns>
		List<string> GetListeProprietesSousObjetsATraduire();
	}
}
