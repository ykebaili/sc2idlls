using System;
using System.Collections;
using System.DirectoryServices;
using sc2i.multitiers.client;
using System.Text.RegularExpressions;


namespace sc2i.multitiers.server
{
	/// <summary>
	/// Description résumée de CAdUsersServer.
	/// </summary>
	public class CAdUsersServeur : C2iObjetServeur, IAdUsersServer
	{
		private static string c_champId = "sAMAccountName";
		private static string c_champNom = "name";
		private static string c_champGroups = "memberOf";

		private static ArrayList m_listeUsers = null;
		/// <summary>
		/// /////////////////////////////////////
		/// </summary>
		public CAdUsersServeur( int nIdSession)
			:base(nIdSession)
		{
			
		}

		/// /////////////////////////////////////
		private void AssureDonnees()
		{
			if ( m_listeUsers != null )
				return;
			m_listeUsers = new ArrayList();
			try
			{
				DirectoryEntry entry = CAdBase.RootEntry;//, m_strUser, m_strPassword);
				DirectorySearcher searcher = new DirectorySearcher(entry);
				SearchResultCollection results;
				searcher.Filter = "(objectCategory=person)";
				searcher.PropertiesToLoad.Add(c_champId);
				searcher.PropertiesToLoad.Add(c_champNom);
				searcher.PropertiesToLoad.Add(c_champGroups);
				results = searcher.FindAll();
				CAdGroupsServeur groupeServeur = new CAdGroupsServeur(IdSession);
				foreach ( SearchResult result in results )
				{
					try
					{
						DirectoryEntry entryTrouvee = result.GetDirectoryEntry();
						CAdUser user = new CAdUser( 
							entryTrouvee.Properties[c_champId].Value.ToString(), 
							entryTrouvee.Properties[c_champNom].Value.ToString() );
						if ( entryTrouvee.Properties[c_champGroups] != null )
							foreach ( object obj in entryTrouvee.Properties[c_champGroups] )
							{
								CAdGroup group = groupeServeur.GetGroupeFromMemberOfValue(obj.ToString());
								if ( group != null )
									user.Groups.Add ( group );
							}							
						m_listeUsers.Add ( user );
					}
					catch
					{
					}
				}
				m_listeUsers.Sort();
			}
			catch
			{
				m_listeUsers.Clear();
			}
		}



		/// /////////////////////////////////////
		public CAdUser[] GetUsers()
		{
			AssureDonnees();
			return ( CAdUser[] )m_listeUsers.ToArray(typeof(CAdUser));
		}

		/// /////////////////////////////////////
		public CAdUser GetUser( string strId )
		{
			AssureDonnees();
			if ( m_listeUsers.Count == 0 )
			{
				CAdUser user = new CAdUser ( strId, strId);
				return user;
			}
			foreach ( CAdUser user in m_listeUsers )
				if ( user.Id == strId )
					return user;
			return null;
		}
	}
}
