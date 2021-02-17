using System;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de LoaderAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
	public class ObjetServeurURIAttribute : Attribute
	{
		/// <summary>
		/// Attribut indiquant l'URI du loader de cette classe
		/// </summary>
		/// <param name="strLoaderURI"></param>
		public ObjetServeurURIAttribute( string strObjetServerURI)
		{
			ObjetServeurURI = strObjetServerURI;
		}
		
		/// /////////////////////////////////////////////////////////////////
		public readonly string ObjetServeurURI;
	}
}
