using System;

namespace sc2i.formulaire
{
	/// <summary>
	/// Description résumée de WndNameAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class WndNameAttribute : Attribute
	{
		public readonly string Name;
		public WndNameAttribute( string strName )
		{
			Name = strName;
		}
	}
}
