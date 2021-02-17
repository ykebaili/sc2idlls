using System;

namespace sc2i.win32.common
{
	/// <summary>
	/// Pour tout �l�ment qui peut �tre utilis� dans diff�rents contexte.
	/// Chaque element a contexte d'utilisation g�re une chaine qui
	/// identifie dans quel contexte il s'execute
	/// </summary>
	public interface IElementAContexteUtilisation
	{
		string ContexteUtilisation {get;set;}
	}
}
