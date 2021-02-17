using System;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description r�sum�e de IValeurVariable.
	/// </summary>
	public interface IValeurVariable
	{
		//Valeur stock�e
		object Value{get;}

		//Valeur affich�e
		string Display{get;}
	}
}
