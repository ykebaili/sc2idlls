using System;

namespace sc2i.common
{
	/// <summary>
	/// Indique que la classe h�rite des restrictions de sa classe parente
	/// </summary>
	/// <remarks>
	/// Cette classe doit �tre utilis�e par toute classe qui va chercher
	/// ses restrictions sur une autre classe
	/// </remarks>
	[AttributeUsage(AttributeTargets.Class)]
	public class RestrictionHeriteeAttribute : Attribute
	{
		public RestrictionHeriteeAttribute (  )
		{
		}
	}
}
