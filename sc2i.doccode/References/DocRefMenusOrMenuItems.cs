using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{
	[global::System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	public class DocRefMenusOrMenuItems : Attribute
	{
		#region [[ Constantes ]]
		//ATTENTION > EN CAS DE MODIFICATION CONSERVER A ASSURER LA CORRESPONDANCE AVEC L'ASSESSEUR RELATIF
		public const string nomBalise = "RefMenuOrMenuItem";
		public const string nomAttribut_IDMenuOrMenuItem = "IDMenuOrMenuItem";
		#endregion

		#region :: Propriétés ::
		readonly List<string> _IDsMenusOrMenuItems;
		#endregion

		#region ++ Constructeur ++
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id">sans espace et sans caractère * / \ { } [ ] ; . , ? : ! </param>
		/// <param name="nomMenu"></param>
		/// <param name="description"></param>
		/// <param name="position"></param>
		public DocRefMenusOrMenuItems(params string[] idsMenusOrMenuItems)
		{
			_IDsMenusOrMenuItems = new List<string>();
			foreach (string id in idsMenusOrMenuItems)
				_IDsMenusOrMenuItems.Add(id);
		}
		#endregion

		#region >> Assesseurs <<
		public List<string> IDsMenusOrMenuItems
		{
			get
			{
				return _IDsMenusOrMenuItems;
			}
		}
		#endregion

		#region ICloneable Membres

		public DocRefMenusOrMenuItems Clone()
		{
			DocRefMenusOrMenuItems clone = new DocRefMenusOrMenuItems(new string[] { });
			foreach (string itm in _IDsMenusOrMenuItems)
				clone._IDsMenusOrMenuItems.Add(itm);
			return clone;

		}

		#endregion
	}
}
