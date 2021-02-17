using System;
using System.Collections;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Description résumée de CAdGroup.
	/// </summary>
#if PDA
#else
	[Serializable]
#endif
	public class CAdGroup : IComparable
	{
		private string m_strId;
		private string m_strNom;
		private ArrayList m_listeGroupes = new ArrayList();

		//////////////////////////////////////
		public CAdGroup( string strId, string strNom)
		{
			m_strId = strId;
			m_strNom = strNom;
		}

		//////////////////////////////////////
		public string Id
		{
			get
			{
				return m_strId;
			}
			set
			{
				m_strId = value;
				if ( m_strNom == "" )
					m_strNom = m_strId;
			}
		}

		//////////////////////////////////////
		public string Nom
		{
			get
			{
				return m_strNom;
			}
			set
			{
				m_strNom = value;
				if ( m_strNom == "" )
					m_strNom = m_strId;
			}

		}

		//////////////////////////////////////
		public ArrayList GroupesParents
		{
			get
			{
				return m_listeGroupes;
			}
		}

		//////////////////////////////////////
		public string[] HierarchieCodesGroupesParents
		{
			get
			{
				Hashtable table = new Hashtable();
				FillCodesGroupesParents ( table );
				string[] strCodes = new string[table.Count];
				int nCode = 0;
				foreach ( string strCode in table.Keys )
					strCodes[nCode++] = strCode;
				return strCodes;
			}
		}

		//////////////////////////////////////
		protected void FillCodesGroupesParents(Hashtable table )
		{
			if ( table[Id] != null )
				return;
			table[Id] = true;
			foreach ( CAdGroup groupe in GroupesParents )
				groupe.FillCodesGroupesParents(table);
		}

		//////////////////////////////////////
		public static CAdGroup[] GetGroups( int nIdSession )
		{
			IAdGroupsServeur adGroups = (IAdGroupsServeur)C2iFactory.GetNewObjetForSession("CAdGroupsServeur", typeof(IAdGroupsServeur), nIdSession );
			if ( adGroups != null )
				return adGroups.GetGroups();
			return null;
		}

		//////////////////////////////////////
		public static CAdGroup GetGroup( int nIdSession, string strId )
		{
			IAdGroupsServeur adGroups = (IAdGroupsServeur)C2iFactory.GetNewObjetForSession("CAdGroupsServeur", typeof(IAdGroupsServeur), nIdSession );
			if ( adGroups != null )
				return adGroups.GetGroup(strId);
			return null;
		}


		//////////////////////////////////////
		public int CompareTo ( object obj )
		{
			if ( !(obj is CAdUser) )
				return 1;
			CAdUser user = (CAdUser)obj;
			return Nom.CompareTo(user.Nom);
		}
		//////////////////////////////////////
		public override string ToString()
		{
			return Nom+" ("+Id+")";
		}
	}
}
