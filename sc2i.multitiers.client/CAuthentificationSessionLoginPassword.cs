using System;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Description résumée de CAuthentificationSessionUserPassword.
	/// </summary>
	[Serializable]
	public class CAuthentificationSessionLoginPassword : IAuthentificationSession
	{
		private string m_strLogin = "";
		private string m_strPassword = "";

		/// //////////////////////////////////////////////////////////////////////////
		public CAuthentificationSessionLoginPassword()
		{
		}

		/// //////////////////////////////////////////////////////////////////////////
		public CAuthentificationSessionLoginPassword(string strLogin, string strPassword)
		{
			m_strLogin = strLogin;
			m_strPassword = strPassword;
		}

		/// //////////////////////////////////////////////////////////////////////////
		public string Login
		{
			get
			{
				return m_strLogin;
			}
			set
			{
				m_strLogin = value;
			}
		}

		/// //////////////////////////////////////////////////////////////////////////
		public string Password
		{
			get
			{
				return m_strPassword;
			}
			set
			{
				m_strPassword = value;
			}
		}
	}
}
