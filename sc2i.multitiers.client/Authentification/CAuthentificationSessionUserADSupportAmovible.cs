using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using System.Threading;
using System.Security.Principal;

namespace sc2i.multitiers.client
{
#if PDA
#else
	[Serializable]
#endif
	public class CAuthentificationSessionUserADSupportAmovible : CAuthentificationSessionUserAD
	{
		private string m_strIdSupport;

		public CAuthentificationSessionUserADSupportAmovible()
		{
		}

		public CAuthentificationSessionUserADSupportAmovible(IIdentity id, string strIdSupport)
			: base(id)
		{
			m_strIdSupport = strIdSupport;

		}

		public CAuthentificationSessionUserADSupportAmovible(string strIdSupport)
			: base()
		{
			m_strIdSupport = strIdSupport;

		}

		public string IdSupportAmovible
		{
			get
			{
				return m_strIdSupport;
			}
			set
			{
				m_strIdSupport = value;
			}
		}
	}
}
