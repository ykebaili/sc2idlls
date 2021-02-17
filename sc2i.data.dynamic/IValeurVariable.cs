using System;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de IValeurVariable.
	/// </summary>
	public interface IValeurVariable
	{
		//Valeur stockée
		object Value{get;}

		//Valeur affichée
		string Display{get;}
	}
}
