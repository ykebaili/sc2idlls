using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{
	[global::System.AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	public class DocMethod : Attribute
	{
		#region [[ Constantes ]]
		readonly string _docRessourceImageName;
		#endregion
		#region ++ Constructeurs ++
		public DocMethod(string docRessourceImageName)
		{
			_docRessourceImageName = docRessourceImageName;
		}
		#endregion
		#region >> Assesseurs <<
		public string DocRessourceImageName
		{
			get
			{
				return _docRessourceImageName;
			}
		}
		#endregion

		#region ~~ Méthodes ~~

		public DocMethod Clone()
		{
			return new DocMethod(_docRessourceImageName);
		}

		#endregion
	}

}
