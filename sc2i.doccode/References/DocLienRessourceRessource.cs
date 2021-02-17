using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{
	[global::System.AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
	public class DocLienRessourceRessource : Attribute
	{
		#region [[ Constantes ]]
		//ATTENTION > EN CAS DE MODIFICATION CONSERVER A ASSURER LA CORRESPONDANCE AVEC L'ASSESSEUR RELATIF
		public const string nomBalise = "DocLienRessourceRessource";
		public const string nomAttribut_IDRessource1 = "IDRessource1";
		public const string nomAttribut_IDRessource2 = "IDRessource2";

		readonly string _IDRessource1;
		readonly string _IDRessource2;
		#endregion

		#region ++ Constructeurs ++
		[DocConstructorAttribut]
		[DocConstructorAttributParameter("idRessource1", DocLienRessourceRessource.nomAttribut_IDRessource1)]
		[DocConstructorAttributParameter("idRessource2", DocLienRessourceRessource.nomAttribut_IDRessource2)]
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id">sans espace et sans caractère * / \ { } [ ] ; . , ? : ! </param>
		/// <param name="nomMenu"></param>
		/// <param name="description"></param>
		/// <param name="position"></param>
		public DocLienRessourceRessource(string idRessource1, string idRessource2)
		{
			_IDRessource1 = idRessource1;
			_IDRessource2 = idRessource2;

		}
		#endregion

		#region >> Assesseurs <<
		//ATTENTION > EN CAS DE MODIFICATION VEILLEZ A CONSERVER LA CORRESPONDANCE AVEC LA CONSTANTE RELATIVE
		public string IDRessource1
		{
			get
			{
				return _IDRessource1;
			}
		}
		public string IDRessource2
		{
			get
			{
				return _IDRessource2;
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

		public DocLienRessourceRessource Clone()
		{
			return new DocLienRessourceRessource(_IDRessource1, _IDRessource2);
		}

		#endregion
	}




}
