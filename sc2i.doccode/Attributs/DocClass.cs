using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{
	[global::System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public class DocClass : Attribute
	{
		#region :: Propriétés ::
		readonly string _docRessourceImageName;
		private string _path;
		private string _nom;

		private DocNameSpace _ns;
		#endregion

		#region ++ Constructeur ++
		public DocClass(string docRessourceImageName)
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
		public string Path
		{
			get
			{
				return _path;
			}
			set
			{
				_path = value;
			}
		}
		public string ID
		{
			get
			{
				return _ns.ID + "." + _nom;
			}
		}
		public string Nom
		{
			get
			{
				return _nom;
			}
			set
			{
				_nom = value;
			}
		}
		public DocNameSpace NameSpace
		{
			get
			{
				return _ns;
			}
			set
			{
				_ns = value;
			}
		}
		#endregion

		#region ~~ Méthodes ~~

		public DocClass Clone()
		{
			return new DocClass(_docRessourceImageName);
		}

		#endregion
	}

}
