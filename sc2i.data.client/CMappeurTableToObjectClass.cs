using System;
using System.Collections;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de CMAppeurTableToObjectClass.
	/// </summary>
	[Serializable]
	public class CMappeurTableToObjectClass
	{
		private Hashtable m_tableLoaderURI = new Hashtable();
		
		//Table Key : Nomtable, value = Type
		private Hashtable m_tableNomTableToType = new Hashtable();

		//Table Key = type, value = nomTable
		private Hashtable m_tableTypeToNomTable = new Hashtable();


		private Hashtable m_tableSynchronisable = new Hashtable();

		/// //////////////////
		public CMappeurTableToObjectClass()
		{
		}

		/// //////////////////
		public void SetTableMapping ( string strNomTable, 
			Type objetType, 
			string strLoaderURI, 
			bool bSynchronisable )
		{
			m_tableLoaderURI[strNomTable] = strLoaderURI;
			m_tableNomTableToType[strNomTable] = objetType;
			m_tableSynchronisable[strNomTable] = bSynchronisable;
			m_tableTypeToNomTable[objetType] = strNomTable;
		}

		/// //////////////////
		public string GetLoaderURIForTable( string strTable )
		{
			return (string)m_tableLoaderURI[strTable];
		}

		/// //////////////////
		public Type GetObjetTypeForTable ( string strTable )
		{
			return (Type)m_tableNomTableToType[strTable];
		}

		/// //////////////////
		public bool IsSynchronisable ( string strTable )
		{
			object obj = m_tableSynchronisable[strTable];
			if (obj != null )
				return (bool)obj;
			return false;
		}

		/// //////////////////
		public string[] GetListeTables()
		{
			ArrayList lst = new ArrayList();
			foreach ( object key in m_tableLoaderURI.Keys )
				lst.Add ( key.ToString() );
			return (string[])lst.ToArray(typeof(string));
		}
        
        /// //////////////////
        public Type[] GetListeTypes()
        {
            ArrayList lst = new ArrayList();
            foreach (object valeur in m_tableNomTableToType.Values)
                lst.Add(valeur);
            return (Type[])lst.ToArray(typeof(Type));
        }
		


		/// //////////////////
		public string GetNomTableForType ( Type tpCherche )
		{
			object obj = m_tableTypeToNomTable[tpCherche];
			if ( obj != null )
				return obj.ToString();
			return null;
		}

		
	}
}
