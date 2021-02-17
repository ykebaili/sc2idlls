using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{
	/// <summary>
	/// Fonctionnalité ATTRIBUT à supprimer > Ne sert strictement à rien
	/// </summary>
	[global::System.AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
	public class DocLienRessource : Attribute
	{
		#region [[ Constantes ]]
		//ATTENTION > EN CAS DE MODIFICATION CONSERVER A ASSURER LA CORRESPONDANCE AVEC L'ASSESSEUR RELATIF
		public const string nomBalise = "RefResx";
		public const string nomAttribut_IDResx = "IDResx";

		public const string nomAttribut_Description = "Description";
		public const string nomAttribut_Position = "Position";
		#endregion

		#region :: Propriétés ::
		private int _pos;
		private string _descrip;
		readonly string _IDObj;
		private string _IDRessource;
		#endregion

		#region ++ Constructeurs ++
		public DocLienRessource()
		{
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id">sans espace et sans caractère * / \ { } [ ] ; . , ? : ! </param>
		/// <param name="nomMenu"></param>
		/// <param name="description"></param>
		/// <param name="position"></param>
		public DocLienRessource(string idObj, string idRessource)
		{
			_IDObj = idObj;
			_IDRessource = idRessource;
		}
		public DocLienRessource(string idObj, string idRessource, string description, int position)
		{
			_pos = position;
			_descrip = description;
			_IDObj = idObj;
			_IDRessource = idRessource;
		}
		#endregion

		#region >> Assesseurs <<
		//ATTENTION > EN CAS DE MODIFICATION VEILLEZ A CONSERVER LA CORRESPONDANCE AVEC LA CONSTANTE RELATIVE
		public string IDObjet
		{
			get
			{
				return _IDObj;
			}
		}
		public string IDRessource
		{
			get
			{
				return _IDRessource;
			}
			set
			{
				_IDRessource = value;
			}
		}
		public string Description
		{
			get
			{
				return _descrip;
			}
			set
			{
				_descrip = value;
			}
		}
		public int Position
		{
			get
			{
				return _pos;
			}
			set
			{
				_pos = value;
			}
		}
		#endregion

	}




}
