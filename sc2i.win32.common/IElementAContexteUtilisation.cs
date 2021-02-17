using System;

namespace sc2i.win32.common
{
	/// <summary>
	/// Pour tout élément qui peut être utilisé dans différents contexte.
	/// Chaque element a contexte d'utilisation gère une chaine qui
	/// identifie dans quel contexte il s'execute
	/// </summary>
	public interface IElementAContexteUtilisation
	{
		string ContexteUtilisation {get;set;}
	}
}
