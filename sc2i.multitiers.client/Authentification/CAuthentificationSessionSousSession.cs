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
	public class CAuthentificationSessionSousSession : IAuthentificationSession
	{
		int m_nIdSessionPrincipale = -1;


		/// ///////////////////////////////////////////////////////
		public CAuthentificationSessionSousSession( int nIdSessionPrincipale)
		{
			m_nIdSessionPrincipale = nIdSessionPrincipale;
		}

		/// ///////////////////////////////////////////////////////
		public int IdSessionPrincipale
		{
			get
			{
				return m_nIdSessionPrincipale;
			}
		}
	}
}
