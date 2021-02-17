using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.data
{
	/// <summary>
	/// Permet d'indiquer qu'une proprieté est optimisée en ayant lu appellé
	/// avant une autre propriété. Utilisé par CListeObjetsDonnees.ReadDependance
	/// qui lit les dépendances
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class OptimiseReadDependanceAttribute : Attribute
	{
		public readonly string ProprieteALire;

		public OptimiseReadDependanceAttribute(string strProprieteALire)
		{
			ProprieteALire = strProprieteALire;
		}
	}
}
