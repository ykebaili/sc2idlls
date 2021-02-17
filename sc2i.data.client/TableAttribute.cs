using System;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de LienTableAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
	public class TableAttribute : Attribute
	{
		/// ///////////////////////////////////////////////////////////////////
		public TableAttribute( string strNomTable, string strChampId, bool bSynchronizable )
		{
			NomTable = strNomTable;
			NomTableInDb = strNomTable;
			ChampsId = new string[] {strChampId};
			Synchronizable = bSynchronizable;
			FillStrChampsId();
		}

		/// ///////////////////////////////////////////////////////////////////
		public TableAttribute(string strNomTable, string strNomTableInDb, string[] strChampsId)
		{
			NomTable = strNomTable;
			NomTableInDb = strNomTableInDb;
			Synchronizable = false;//Avec plusieurs champs id, l'élement ne peut pas être synchronisable
			ChampsId = strChampsId;
			FillStrChampsId();
		}

		/// ///////////////////////////////////////////////////////////////////
		public TableAttribute( string strNomTable, string[] strChampsId )
		{
			NomTable = strNomTable;
			NomTableInDb = strNomTable;
			Synchronizable = false;//Avec plusieurs champs id, l'élement ne peut pas être synchronisable
			ChampsId = strChampsId;
			FillStrChampsId();
		}

		// ///////////////////////////////////////////////////////////////////
		public TableAttribute(string strNomTable, string strNomTableInDb, string strChampId, bool bSynchronizable)
		{
			NomTable = strNomTable;
			NomTableInDb = strNomTableInDb;
			ChampsId = new string[] { strChampId };
			Synchronizable = bSynchronizable;
			FillStrChampsId();
		}

		/// ///////////////////////////////////////////////////////////////////
		private void FillStrChampsId()
		{
			m_strListeChampsIds = "";
			foreach ( string strChamp in ChampsId )
				m_strListeChampsIds += strChamp+",";
			if ( m_strListeChampsIds.Length > 0 )
				m_strListeChampsIds = m_strListeChampsIds.Substring(0, m_strListeChampsIds.Length-1 );
		}


		public readonly string NomTable;
		public readonly string NomTableInDb;
		public readonly string[] ChampsId;
		public readonly bool Synchronizable;
		public bool IsGrandVolume;
        public bool ModifiedByTrigger = false;
		
		//Champs id séparés par des ,
		//Utile pour la documentation
		public string m_strListeChampsIds;
	}
}
