using System;
using System.Text;

namespace sc2i.common
{
	/// <summary>
	/// Liste des restrictions possible
	/// Binaire combinables
	/// </summary>
	[Flags]
	public enum ERestriction
	{
		Aucune = 0,
		NoCreate = 1,
		NoDelete = 2,
		ReadOnly = 7,
		Hide = 15
	}

}
