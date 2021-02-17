using System;
using System.DirectoryServices;

namespace sc2i.multitiers.server
{
	/// <summary>
	/// Description résumée de CAdBase.
	/// </summary>
	public class CAdBase
	{
		private static string m_strBasePath = "";
		private static string m_strUser = "";
		private static string m_strPassword = "";
		
		/// <summary>
		//////////////////////////////////////////////////////////////
		/// </summary>
		/// <param name="strBasePath"></param>
		/// <param name="strUser"></param>
		/// <param name="strPassword"></param>
		public static void Init ( string strBasePath, string strUser, string strPassword )
		{
			m_strBasePath = strBasePath;
			m_strUser = strUser;
			m_strPassword = strPassword;
		}

		//////////////////////////////////////////////////////////////
		public static DirectoryEntry RootEntry
		{
			get
			{
				if ( m_strUser == "" )
					return new DirectoryEntry ( m_strBasePath );
				else
					return new DirectoryEntry ( m_strBasePath, m_strUser, m_strPassword );
			}
		}

	}
}
