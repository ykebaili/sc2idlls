using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{
	/// <summary>
	/// Permet de spécifier à quelle propriété correspond le parametre
	/// </summary>
	[global::System.AttributeUsage(AttributeTargets.Constructor, Inherited = false, AllowMultiple = true)]
	public class DocConstructorAttributParameter : Attribute
	{
		#region :: Propriétés ::
		readonly string m_nomParametre;
		readonly string m_nomPropriete;
		#endregion

		#region >> Assesseurs <<
		public string NomParametre
		{ 
			get
			{
				return m_nomParametre;
			}
		}
		public string NomPropriete
		{
			get
			{
				return m_nomPropriete;
			}
		}
		#endregion

		#region ++ Constructeur ++
		public DocConstructorAttributParameter(string nomParametre, string nomPropriete)
		{
			m_nomParametre = nomParametre;
			m_nomPropriete = nomPropriete;
		}
		#endregion
	}




}
