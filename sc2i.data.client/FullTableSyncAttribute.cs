using System;

namespace sc2i.data
{
	/// <summary>
	/// Indique que lorsque la table est synchronis�e, toute la table est envoy�e. Par de filtre
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
	public class FullTableSyncAttribute : Attribute
	{
		public FullTableSyncAttribute()
		{
		}
	}
}
