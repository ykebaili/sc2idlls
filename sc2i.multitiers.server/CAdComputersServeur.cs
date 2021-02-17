using System;
using System.Collections;
using System.DirectoryServices;
using sc2i.multitiers.client;
using System.Text.RegularExpressions;


namespace sc2i.multitiers.server
{
	/// <summary>
	/// Description résumée de CAdComputersServeur.
	/// </summary>
	public class CAdComputersServeur : C2iObjetServeur, IAdComputersServer
			     
	{
		private static string c_champNom = "name";

		private static ArrayList m_listComputers = null;

		/// ///////////////////////////////////
		public CAdComputersServeur( int nIdSession )
			:base(nIdSession)
		{
			
		}

		/// /////////////////////////////////////
		private void AssureDonnees()
		{
			if ( m_listComputers != null )
				return;
			m_listComputers = new ArrayList();
			try
			{
				
				DirectoryEntry entry = CAdBase.RootEntry;//, m_strUser, m_strPassword);
				DirectorySearcher searcher = new DirectorySearcher(entry);
				SearchResultCollection results;
				searcher.Filter = "(objectCategory=computer)";
				searcher.PropertiesToLoad.Add(c_champNom);
				results = searcher.FindAll();
				Hashtable tableMembersOf = new Hashtable();
				foreach ( SearchResult result in results )
				{
					try
					{
						DirectoryEntry entryTrouvee = result.GetDirectoryEntry();
						CAdComputer Computer = new CAdComputer( 
							entryTrouvee.Properties[c_champNom].Value.ToString() );
						m_listComputers.Add ( Computer );
					}
					catch
					{
					}
				}
				m_listComputers.Sort();
			}
			catch
			{
				m_listComputers.Clear();
			}
		}

		/// /////////////////////////////////////
		public CAdComputer[] GetComputers()
		{
			AssureDonnees();
			return ( CAdComputer[] )m_listComputers.ToArray(typeof(CAdComputer));
		}

		/// /////////////////////////////////////
		public CAdComputer GetComputer( string strName )
		{
			AssureDonnees();
			if ( m_listComputers.Count == 0 )
			{
				CAdComputer Computer = new CAdComputer(strName);
				return Computer;
			}
			foreach ( CAdComputer Computer in m_listComputers )
				if ( Computer.Nom == strName )
					return Computer;
			return null;
		}

		/// /////////////////////////////////////
		public CAdComputer GetComputereFromMemberOfValue ( string strValue )
		{
			Regex ex = new Regex("^CN=[^,]*,");
			MatchCollection results = ex.Matches(strValue);
			if ( results != null && results.Count != 0 )
			{
				string strNom = results[0].Value.Substring(3, results[0].Value.Length-4);
				return GetComputer ( strNom );
			}
			return null;
		}
	}
}
