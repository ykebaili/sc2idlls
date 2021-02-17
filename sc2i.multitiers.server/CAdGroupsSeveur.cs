using System;
using System.Collections;
using System.DirectoryServices;
using sc2i.multitiers.client;
using System.Text.RegularExpressions;


namespace sc2i.multitiers.server
{
	/// <summary>
	/// Description résumée de CAdGroupsSeveur.
	/// </summary>
	public class CAdGroupsServeur : C2iObjetServeur, IAdGroupsServeur
			     
	{
		private static string c_champId = "sAMAccountName";
		private static string c_champNom = "name";
		private static string c_champGroups = "memberOf";

		private static ArrayList m_listeGroups = null;

		/// ///////////////////////////////////
		public CAdGroupsServeur( int nIdSession )
			:base(nIdSession)
		{
			
		}

		/// /////////////////////////////////////
		private void AssureDonnees()
		{
			if ( m_listeGroups != null )
				return;
			m_listeGroups = new ArrayList();
			try
			{
				
				DirectoryEntry entry = CAdBase.RootEntry;//, m_strUser, m_strPassword);
				DirectorySearcher searcher = new DirectorySearcher(entry);
				SearchResultCollection results;
				searcher.Filter = "(objectCategory=group)";
				searcher.PropertiesToLoad.Add(c_champId);
				searcher.PropertiesToLoad.Add(c_champNom);
				searcher.PropertiesToLoad.Add(c_champGroups);
				results = searcher.FindAll();
				Hashtable tableMembersOf = new Hashtable();
				foreach ( SearchResult result in results )
				{
					try
					{
						DirectoryEntry entryTrouvee = result.GetDirectoryEntry();
						CAdGroup group = new CAdGroup( 
							entryTrouvee.Properties[c_champId].Value.ToString(), 
							entryTrouvee.Properties[c_champNom].Value.ToString() );
						tableMembersOf[group] = entryTrouvee.Properties[c_champGroups];
						m_listeGroups.Add ( group );
					}
					catch
					{
					}
				}
				//Récupère les groupes
				foreach ( CAdGroup group in m_listeGroups )
				{
					group.GroupesParents.Clear();
					PropertyValueCollection properties = (PropertyValueCollection)tableMembersOf[group];
					if ( properties != null )
					{
						foreach ( object obj in properties )
						{
							CAdGroup groupParent = GetGroupeFromMemberOfValue(obj.ToString());
							if ( groupParent != null )
								group.GroupesParents.Add ( groupParent );
						}

					}
				}
				m_listeGroups.Sort();
			}
			catch
			{
				m_listeGroups.Clear();
			}
		}

		/// /////////////////////////////////////
		public CAdGroup[] GetGroups()
		{
			AssureDonnees();
			return ( CAdGroup[] )m_listeGroups.ToArray(typeof(CAdGroup));
		}

		/// /////////////////////////////////////
		public CAdGroup GetGroup( string strId )
		{
			AssureDonnees();
			if ( m_listeGroups.Count == 0 )
			{
				CAdGroup group = new CAdGroup(strId, strId);
				return group;
			}
			foreach ( CAdGroup group in m_listeGroups )
				if ( group.Id == strId )
					return group;
			return null;
		}

		/// /////////////////////////////////////
		public CAdGroup GetGroupeFromMemberOfValue ( string strValue )
		{
			Regex ex = new Regex("^CN=[^,]*,");
			MatchCollection results = ex.Matches(strValue);
			if ( results != null && results.Count != 0 )
			{
				string strNom = results[0].Value.Substring(3, results[0].Value.Length-4);
				return GetGroup ( strNom );
			}
			return null;
		}
	}
}
