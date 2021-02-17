using System;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de DynamicFieldAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
    public sealed class DynamicFieldAttribute : Attribute
	{
		

		public readonly string NomConvivial;
		public readonly string Rubrique;
		
		public DynamicFieldAttribute( string strNomConvivial)
		{
			NomConvivial = strNomConvivial.Replace(" ","_");
		}

		public DynamicFieldAttribute( string strNomConvivial, string strRubrique)
		{
			NomConvivial = strNomConvivial.Replace(" ","_");
			Rubrique = strRubrique;
		}
	}

	

}
