using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{
	public class DocMenu : ADocElementAMenuItems
	{
		#region [[ Constantes ]]
		//ATTENTION > EN CAS DE MODIFICATION CONSERVER A ASSURER LA CORRESPONDANCE AVEC L'ASSESSEUR RELATIF
		public const string nomBalise = "DocMenu";
		public const string nomAttributID = "ID";

		public const string nomAttribut_NomMenu = "NomMenu";
		public const string nomAttribut_Description = "Description";
		public const string nomAttribut_Position = "Position";
		public const string nomAttribut_RessourcesLieesNames = "RessourcesLieesNames";
		public const string nomAttribut_DocRessourceImageName = "DocRessourceImageName";
		#endregion

		#region :: Propriétés ::
		readonly string _docRessourceImageName;
		readonly string[] _ressourcesLieesName;

		#endregion

		#region ++ Constructeurs ++

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id">sans espace et sans caractère * / \ { } [ ] ; . , ? : ! </param>
		/// <param name="nomMenu"></param>
		/// <param name="description"></param>
		/// <param name="position"></param>
		[DocConstructorAttribut]
		[DocConstructorAttributParameter("id", DocMenu.nomAttributID)]
		[DocConstructorAttributParameter("nomMenu", DocMenu.nomAttribut_NomMenu)]
		[DocConstructorAttributParameter("description", DocMenu.nomAttribut_Description)]
		[DocConstructorAttributParameter("position", DocMenu.nomAttribut_Position)]
		public DocMenu(string id, string nomMenu, string description, int position)
		{
			_enfants = new List<DocMenuItem>();
			_id = id;
			_nom = nomMenu;
			_descrip = description;
			_position = position;
		}
		public DocMenu()
		{
			_enfants = new List<DocMenuItem>();

		}
		#endregion

		#region >> Assesseurs <<
		//ATTENTION > EN CAS DE MODIFICATION VEILLEZ A CONSERVER LA CORRESPONDANCE AVEC LA CONSTANTE RELATIVE
		public string DocRessourceImageID
		{
			get
			{
				return _docRessourceImageName;
			}
		}
		public string NomMenu
		{
			get
			{
				return _nom;
			}
		}
		public string[] RessourcesLieesNames
		{
			get
			{
				return _ressourcesLieesName;
			}
		}

		/// <summary>
		/// Retourne la déclaration de cette instance
		/// </summary>
		/// 
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

			DocMenu clone = new DocMenu(_id, _nom, _descrip, _position);

			foreach (DocMenuItem itm in Enfants)
				clone._enfants.Add((DocMenuItem)itm.Clone());

			return clone;
		}

		#endregion
	}




}
