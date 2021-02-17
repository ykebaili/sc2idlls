using System;
using System.Collections;
using System.Data;

using sc2i.common;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Informations sur une relation entre des tables standard,
	/// C'est une relation qui se fait entre des champs de table
	/// relation classique SQL
	/// </summary>
	[Serializable]
	public class CInfoRelationAClefDefinissable : CInfoRelation
	{
		private string m_strKey;
		public CInfoRelationAClefDefinissable(
			string strKeyRelation,
			string strTableParente, 
			string strTableFille, 
			string[] strChampsParent, 
			string[] strChampsFille)
			:base(strTableParente,strTableFille,strChampsParent,strChampsFille,true,false)
		{
			m_strKey = strKeyRelation;
		}
		/// /////////////////////////////////////////////////
		public override string RelationKey
		{
			get
			{
				return m_strKey;
			}
		}
	}
}
