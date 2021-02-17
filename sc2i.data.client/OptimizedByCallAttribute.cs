using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.data
{
	/// <summary>
	/// Permet d'indiquer qu'une propriet� est optimis�e en ayant lu appell�
	/// avant une autre propri�t�. Utilis� par CListeObjetsDonnees.ReadDependance
	/// qui lit les d�pendances
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
