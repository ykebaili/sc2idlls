using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{
	/// <summary>
	/// Permet de mettre en relation avec plusieurs ressources
	/// </summary>
	[global::System.AttributeUsage(AttributeTargets.Class 
								| AttributeTargets.Constructor
								| AttributeTargets.Field
								| AttributeTargets.Method
								| AttributeTargets.Property
		, Inherited = false, AllowMultiple = true)]
	public class DocRefRessources : Attribute
	{

		#region :: Propriétés ::
		private List<string> _idsRessources;
		#endregion

		#region ++ Constructeur ++
		public DocRefRessources(params string[] idsRessources)
		{
			_idsRessources = new List<string>();
			foreach (string id in idsRessources)
				_idsRessources.Add(id);

		}
		#endregion

		#region >> Assesseurs <<
		public List<string> IDsRessources
		{
			get
			{
				return _idsRessources;
			}
		}
		#endregion

	}

}
