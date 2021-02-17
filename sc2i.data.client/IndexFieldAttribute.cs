using System;

namespace sc2i.data
{
	/// <summary>
	/// Permet de définir qu'une propriété est indexée
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class IndexFieldAttribute : Attribute
	{
		public IndexFieldAttribute()
		{
		}
	}

    /// <summary>
    /// Permet d'indiquer plusieurs propriétés dans un index.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
    public class IndexAttribute : Attribute
    {
        public readonly string[] Champs;
        public bool IsCluster = false;

        public IndexAttribute(params string[] strChamps)
        {
            Champs = strChamps;
        }
    }
}
