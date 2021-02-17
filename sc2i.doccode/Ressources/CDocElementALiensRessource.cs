using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{

	public class CDocElementALiensRessource : IDocElementALiensRessource
	{
		#region [[ Constantes ]]
		public const string nomAttributID = "ID";

		public const string nomAttribut_Description = "Description";
		public const string nomAttribut_Position = "Position";

		#endregion

		#region ++ Constructeur ++
		public CDocElementALiensRessource()
		{

		}
		#endregion

		#region :: Propriétés ::
		private string m_id;
		private string m_descrip;
		private int m_pos;
		private List<DocLienRessource> m_ressources;
		#endregion

		#region >> Accesseurs <<
		public string ID
		{
			get
			{
				return m_id;
			}
			set
			{
				m_id = value;
			}
		}
		public string Description
		{
			get
			{
				return m_descrip;
			}
			set
			{
				m_descrip = value;
			}
		}
		public List<DocLienRessource> Ressources
		{
			get
			{
				return m_ressources;
			}
			set
			{
				m_ressources = value;
			}
		}
		public int Position
		{
			get
			{
				return m_pos;
			}
			set
			{
				m_pos = value;
			}
		}
		#endregion


	}

}
