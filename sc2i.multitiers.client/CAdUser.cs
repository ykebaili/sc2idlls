using System;
using System.Collections;


namespace sc2i.multitiers.client
{
	/// <summary>
	/// Description résumée de CAdUser.
	/// </summary>
#if PDA
#else
	[Serializable]
#endif
	public class CAdUser : IComparable
	{
		private string m_strNom;
		private string m_strId;
		private ArrayList	m_listeGroupes;

		//////////////////////////////////////
		public CAdUser( string strId, string strNom)
		{
			m_strNom = strNom;
			m_strId = strId;
			if ( m_strNom == "" )
				m_strNom = m_strId;
			m_listeGroupes = new ArrayList();
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
		public static CAdUser[] GetUsers( int nIdSession )
		{
			IAdUsersServer adUsers = (IAdUsersServer)C2iFactory.GetNewObjetForSession("CAdUsersServeur", typeof(IAdUsersServer), nIdSession );
			if ( adUsers != null )
				return adUsers.GetUsers();
			return null;
		}

		//////////////////////////////////////
		public static CAdUser GetUser ( int nIdSession, string strId )
		{
			IAdUsersServer adUsers = (IAdUsersServer)C2iFactory.GetNewObjetForSession("CAdUsersServeur", typeof(IAdUsersServer), nIdSession );
			if ( adUsers != null )
				return adUsers.GetUser ( strId );
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

		//////////////////////////////////////
		public ArrayList Groups
		{
			get
			{
				return m_listeGroupes;
			}
		}

		
	}
}
