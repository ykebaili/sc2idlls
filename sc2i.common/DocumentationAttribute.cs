using System;

namespace sc2i.common
{
	/// <summary>
	/// Indique que la classe ne sert que pour la génération de documentation
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
    public sealed class DocumentationAttribute : Attribute
	{
		//Nom de la rubrique dans l'arbre
		public readonly string NomConvivial;
		
		//Type de la rubrique parente dans l'arbre
		//Si null, sera sous le namespace dans le doc content
		public readonly Type TypeDocumentationParent;
		public DocumentationAttribute( string strNomConvivial )
		{
			NomConvivial = strNomConvivial;
		}

		public DocumentationAttribute ( string strNomConvivial, Type type )
		{
			TypeDocumentationParent = type;
			NomConvivial = strNomConvivial;
		}
	}
}
