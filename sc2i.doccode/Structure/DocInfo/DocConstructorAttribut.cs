using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{
	/// <summary>
	/// Permet de spécifier quel constructeur est à utiliser pour la déclaration d'assemblage 
	/// dans le fichier DocInfo
	/// </summary>
	[global::System.AttributeUsage(AttributeTargets.Constructor, Inherited = false, AllowMultiple = false)]
	public class DocConstructorAttribut : Attribute
	{
		#region ++ Constructeur ++
		public DocConstructorAttribut()
		{
		}
		#endregion
	}




}
