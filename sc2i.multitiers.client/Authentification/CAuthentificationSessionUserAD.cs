using System;
using System.Security;
using System.Threading;
using System.Security.Principal;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Description résumée de CAuthentificationSessionUserAD.
	/// </summary>
#if PDA
#else
	[Serializable]
#endif
	public class CAuthentificationSessionUserAD : IAuthentificationSession
	{
		string m_strId = "";


		/// ///////////////////////////////////////////////////////
		public CAuthentificationSessionUserAD()
		{
			/*WindowsIdentity principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
			Thread.CurrentPrincipal = principal;*/
			SetIdentity ( WindowsIdentity.GetCurrent() );
		}

		/// ///////////////////////////////////////////////////////
		public CAuthentificationSessionUserAD( IIdentity id )
		{
			SetIdentity ( id );
		}

		/// ///////////////////////////////////////////////////////
		private void SetIdentity ( IIdentity identity )
		{
			if ( identity.IsAuthenticated )
			{
				m_strId = identity.Name;
				//Supprime le domaine
				int nPos = m_strId.LastIndexOf('\\');
				if ( nPos > 0 )
					m_strId = m_strId.Substring(nPos+1);
			}
		}

		/// ///////////////////////////////////////////////////////
		public string UserId
		{
			get
			{
				return m_strId;
			}
		}
	}
}
