using System;

namespace sc2i.data
{
	/// <summary>
	/// Indique que lorsque la table est synchronisée, toute la table est envoyée. Par de filtre
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
	public class FullTableSyncAttribute : Attribute
	{
		public FullTableSyncAttribute()
		{
		}
	}
}
