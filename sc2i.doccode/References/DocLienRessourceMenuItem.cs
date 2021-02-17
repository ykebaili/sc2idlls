using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{
	[global::System.AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
	public class DocLienRessourceMenuItem : Attribute
	{
		#region [[ Constantes ]]
		//ATTENTION > EN CAS DE MODIFICATION CONSERVER A ASSURER LA CORRESPONDANCE AVEC L'ASSESSEUR RELATIF
		public const string nomBalise = "DocLienRessourceMenuItem";
		public const string nomAttribut_IDMenuItem = "IDMenuItem";
		public const string nomAttribut_IDRessource = "IDRessource";

		readonly string _IDMenuItem;
		readonly string _IDRessource;
		#endregion

		#region ++ Constructeurs ++
		[DocConstructorAttribut]
		[DocConstructorAttributParameter("idMenuItem", DocLienRessourceMenuItem.nomAttribut_IDMenuItem)]
		[DocConstructorAttributParameter("idRessource", DocLienRessourceMenuItem.nomAttribut_IDRessource)]
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id">sans espace et sans caractère * / \ { } [ ] ; . , ? : ! </param>
		/// <param name="nomMenu"></param>
		/// <param name="description"></param>
		/// <param name="position"></param>
		public DocLienRessourceMenuItem(string idMenuItem, string idRessource)
		{
			_IDMenuItem = idMenuItem;
			_IDRessource = idRessource;
		}
		#endregion

		#region >> Assesseurs <<
		//ATTENTION > EN CAS DE MODIFICATION VEILLEZ A CONSERVER LA CORRESPONDANCE AVEC LA CONSTANTE RELATIVE
		public string IDMenuItem
		{
			get
			{
				return _IDMenuItem;
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

		public DocLienRessourceMenuItem Clone()
		{
			return new DocLienRessourceMenuItem(_IDMenuItem, _IDRessource);
		}

		#endregion
	}




}
