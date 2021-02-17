using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{
	[global::System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
	public class DocChamp : Attribute
	{
		#region [[ Constantes ]]
		readonly string _docRessourceImageID;
		readonly bool _obligatoire;
		#endregion

		#region ++ Constructeurs ++
		public DocChamp(bool obligatoire, string docRessourceImageID)
		{
			_obligatoire = obligatoire;
			_docRessourceImageID = docRessourceImageID;
		}
		#endregion

		#region >> Assesseur <<
		public bool Obligatoire
		{
			get
			{
				return _obligatoire;
			}
		}
		public string DocRessourceImageNameID
		{
			get
			{
				return _docRessourceImageID;
			}
		}
		#endregion

		#region ~~ Méthodes ~~
		public DocChamp Clone()
		{
			return new DocChamp(_obligatoire, _docRessourceImageID);
		}
		#endregion
	}

}
