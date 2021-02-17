using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;

namespace sc2i.common
{
	/// <summary>
	/// Un élement qui est capable d'en allouer d'autres
	/// </summary>
	public interface IAllocateurSupprimeurElements
	{
		//Le data du result contient l'objet alloué
		CResultAErreur AlloueElement(Type tp);

		CResultAErreur SupprimeElement(object elt);
	}
}
