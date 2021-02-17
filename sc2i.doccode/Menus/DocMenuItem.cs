using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{
	public class DocMenuItem : ADocElementAMenuItems
	{

		#region [[ Constantes ]]
		//ATTENTION > EN CAS DE MODIFICATION CONSERVER A ASSURER LA CORRESPONDANCE AVEC L'ASSESSEUR RELATIF
		public const string nomBalise = "DocMenuItem";
		public const string nomAttributID = "ID";
		public const string nomAttribut_NomItem = "NomItem";
		public const string nomAttribut_Description = "Description";
		public const string nomAttribut_Position = "Position";
		public const string nomAttribut_RessourcesLieesNames = "RessourcesLieesNames";
		public const string nomAttribut_DocRessourceImageName = "DocRessourceImageName";
		public const string nomAttribut_ParentID = "ParentID";
		#endregion
		#region :: Propriétés ::
		
		readonly string _parentID;
		readonly string _docRessourceImageName;
		readonly string[] _ressourcesLieesNames;

		#endregion

		#region ++ Constructeurs ++
		public DocMenuItem()
		{
			_enfants = new List<DocMenuItem>();

		}
		[DocConstructorAttribut]
		[DocConstructorAttributParameter("id", DocMenuItem.nomAttributID)]
		[DocConstructorAttributParameter("nomItem", DocMenuItem.nomAttribut_NomItem)]
		[DocConstructorAttributParameter("description", DocMenuItem.nomAttribut_Description)]
		[DocConstructorAttributParameter("parentID", DocMenuItem.nomAttribut_ParentID)]
		[DocConstructorAttributParameter("position", DocMenuItem.nomAttribut_Position)]
		public DocMenuItem(string id, string nomItem, string description, string parentID, int position)
		{
			_enfants = new List<DocMenuItem>();
			_id = id;
			_nom = nomItem;
			_descrip = description;
			_parentID = parentID;
			_position = position;
		}
		#endregion

		#region >> Assesseurs <<
		//ATTENTION > EN CAS DE MODIFICATION VEILLEZ A CONSERVER LA CORRESPONDANCE AVEC LA CONSTANTE RELATIVE
		public string[] RessourcesLieesNames
		{
			get
			{
				return _ressourcesLieesNames;
			}
		}
		public string NomItem
		{
			get
			{
				return _nom;
			}
		}
		public string DocRessourceImageName
		{
			get
			{
				return _docRessourceImageName;
			}
		}
		public string ParentID
		{
			get
			{
				return _parentID;
			}
		}

		/// <summary>
		/// Retourne la déclaration de cette instance
		/// </summary>
		public string LigneDeclarationAttributAssemblage
		{
			get
			{
				return CDocLigneAttribut.GetLigneAttribut(this);
			}
		}
		#endregion

		#region ~~ Méthodes ~~


		public override IDocElementAMenuItems Clone()
		{
			DocMenuItem clone = new DocMenuItem(_id, _nom, _descrip, _parentID, _position);
			foreach (DocMenuItem itm in _enfants)
				clone._enfants.Add((DocMenuItem) itm.Clone());
			return clone;
		}


	
		#endregion
	}
}
