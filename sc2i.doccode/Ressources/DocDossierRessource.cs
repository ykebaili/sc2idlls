using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{
	[global::System.AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
	public class DocDossierRessource : Attribute
	{

		#region [[ Constantes ]]
		//ATTENTION > EN CAS DE MODIFICATION CONSERVER A ASSURER LA CORRESPONDANCE AVEC L'ASSESSEUR RELATIF
		public const string nomBalise = "DossierResx";
		public const string nomAttribut_TypeRessource = "TypeResx";
		public const string nomAttribut_Path = "Path";
		#endregion

		#region :: Propriétés ::
		private EDocRessourceType _type;
		private string _path;
		#endregion

		#region ++ Constructeurs ++
		/// <summary>
		/// 
		/// </summary>
		/// <param name="type">type de la ressource</param>
		/// <param name="path">chemin relatif du dossier contenant les ressources</param>
		[DocConstructorAttribut]
		[DocConstructorAttributParameter("type", DocDossierRessource.nomAttribut_TypeRessource)]
		[DocConstructorAttributParameter("path", DocDossierRessource.nomAttribut_Path)]
		public DocDossierRessource(EDocRessourceType type, string path)
		{
			_path = path;
			_type = type;
		}
		#endregion

		#region >> Assesseurs <<
		//ATTENTION > EN CAS DE MODIFICATION VEILLEZ A CONSERVER LA CORRESPONDANCE AVEC LA CONSTANTE RELATIVE
		public EDocRessourceType TypeResx
		{
			get
			{
				return _type;
			}
			set
			{
				_type = value;
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
		public DocDossierRessource Clone()
		{
			return new DocDossierRessource(_type, _path);
		}
		#endregion
	}
}
