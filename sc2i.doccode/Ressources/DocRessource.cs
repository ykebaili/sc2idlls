using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{
	[global::System.AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
	public class DocRessource : Attribute, IDocRessource
	{
		#region [[ Constantes ]]
		//ATTENTION > EN CAS DE MODIFICATION CONSERVER A ASSURER LA CORRESPONDANCE AVEC L'ASSESSEUR RELATIF
		public const string nomBalise = "DocRessource";
		public const string nomAttributID = "ID";

		public const string nomAttribut_Nom = "Nom";
		public const string nomAttribut_Description = "Description";
		public const string nomAttribut_Path = "Path";
		public const string nomAttribut_ImageID = "ImageID";
		public const string nomAttribut_TypeRessource = "TypeRessource";
		#endregion

		#region :: Propriétés ::
		private string _id;
		private string _nom;
		private string _description;
		private string _path;
		private string _imageID;
		private EDocRessourceType _typeRessource;
		#endregion

		#region ++ Constructeurs ++
		public DocRessource()
		{
		}
		[DocConstructorAttribut]
		[DocConstructorAttributParameter("id", DocRessource.nomAttributID)]
		[DocConstructorAttributParameter("nom", DocRessource.nomAttribut_Nom)]
		[DocConstructorAttributParameter("description", DocRessource.nomAttribut_Description)]
		[DocConstructorAttributParameter("path", DocRessource.nomAttribut_Path)]
		[DocConstructorAttributParameter("typeRessource", DocRessource.nomAttribut_TypeRessource)]
		public DocRessource(string id, string nom, string description, string path, EDocRessourceType typeRessource)
		{
			_id = id;
			_nom = nom;
			_description = description;
			_path = path;
			_typeRessource = typeRessource;

		}
		#endregion

		#region >> Accesseurs <<
		//ATTENTION > EN CAS DE MODIFICATION VEILLEZ A CONSERVER LA CORRESPONDANCE AVEC LA CONSTANTE RELATIVE
		public string ID
		{
			get
			{
				return _id;
			}
			set
			{
				_id = value;
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
		public string Description
		{
			get
			{
				return _description;
			}
			set
			{
				_description = value;
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

		public string ImageID
		{
			get
			{
				return _imageID;
			}
		}
		public virtual EDocRessourceType TypeRessource
		{
			get
			{
				return _typeRessource;
			}
			set 
			{
				_typeRessource = value;
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
		public void ChargerViaXML(XmlNode noeud)
		{
			_description = noeud.Attributes[DocRessource.nomAttribut_Description].Value;
			_nom = noeud.Attributes[DocRessource.nomAttribut_Nom].Value;
			_description = noeud.Attributes[DocRessource.nomAttribut_Path].Value;
			_typeRessource = (EDocRessourceType)int.Parse(noeud.Attributes[DocRessource.nomAttribut_TypeRessource].Value);
			_id = noeud.Attributes[DocRessource.nomAttributID].Value;

		}

		public DocRessource Clone()
		{
			DocRessource resx = new DocRessource(_id, _nom, _description, _path, _typeRessource);
			return resx;
		}

		#endregion

	}

}
