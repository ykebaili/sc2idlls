using System;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de DynamicChildsAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
    public sealed class DynamicChildsAttribute : Attribute
	{
		public readonly string NomConvivial;
		public readonly Type TypeFils;
		public string Rubrique;
		
		public DynamicChildsAttribute( string strNomConvivial, Type typeFils)
		{
			NomConvivial = strNomConvivial.Replace(" ","_");
			TypeFils = typeFils;
		}

		public DynamicChildsAttribute( string strNomConvivial, Type typeFils, string strRubrique)
		{
			NomConvivial = strNomConvivial.Replace(" ","_");
			TypeFils = typeFils;
			Rubrique = strRubrique;
		}

	}
}
