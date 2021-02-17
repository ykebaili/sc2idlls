using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{
	[global::System.AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
	public class DocLienRessourceMenu : Attribute
	{
		#region [[ Constantes ]]
		//ATTENTION > EN CAS DE MODIFICATION CONSERVER A ASSURER LA CORRESPONDANCE AVEC L'ASSESSEUR RELATIF
		public const string nomBalise = "DocLienRessourceMenu";
		public const string nomAttribut_IDMenu = "IDMenu";
		public const string nomAttribut_IDRessource = "IDRessource";

		readonly string _IDMenu;
		readonly string _IDRessource;
		#endregion

		#region ++ Constructeurs ++
		[DocConstructorAttribut]
		[DocConstructorAttributParameter("idMenu", DocLienRessourceMenu.nomAttribut_IDMenu)]
		[DocConstructorAttributParameter("idRessource", DocLienRessourceMenu.nomAttribut_IDRessource)]
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id">sans espace et sans caractère * / \ { } [ ] ; . , ? : ! </param>
		/// <param name="nomMenu"></param>
		/// <param name="description"></param>
		/// <param name="position"></param>
		public DocLienRessourceMenu(string idMenu, string idRessource)
		{
			_IDMenu = idMenu;
			_IDRessource = idRessource;
		}
		#endregion

		#region >> Assesseurs <<
		//ATTENTION > EN CAS DE MODIFICATION VEILLEZ A CONSERVER LA CORRESPONDANCE AVEC LA CONSTANTE RELATIVE
		public string IDMenu
		{
			get
			{
				return _IDMenu;
			}
		}
		public string IDRessource
		{
			get
			{
				return _IDRessource;
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


		#region ICloneable Membres

		public DocLienRessourceMenu Clone()
		{
			return new DocLienRessourceMenu(_IDMenu, _IDRessource);
		}

		#endregion
	}




}
