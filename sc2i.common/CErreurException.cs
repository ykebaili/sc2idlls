using System;
using System.Reflection;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de CErreurException.
	/// </summary>
	[Serializable]
	public class CErreurException : IErreur
	{
		string m_strMessage="";
		string m_strStackTrace="";

		//////////////////////////////////////////////////////
		public CErreurException()
		{
		}

		//////////////////////////////////////////////////////
		public CErreurException( Exception e )
		{
			if ( e.Message != null )
				m_strMessage = e.Message;
			else
				m_strMessage = I.T("Unknown error|30044");
            Exception inner = e.InnerException;
            while (inner != null)
            {
                if (inner.Message != null)
                    m_strMessage += "\r\n" + inner.Message;
                inner = inner.InnerException;
            }
#if PDA
			m_strStackTrace = "";
#else
			if ( e.StackTrace != null )
				m_strStackTrace = e.StackTrace;
			else
				m_strStackTrace = "";
#endif
			if (e is TargetInvocationException)
			{
				TargetInvocationException targetEx = e as TargetInvocationException;
				if (targetEx != null)
				{
					m_strMessage += "\r\n";
					m_strMessage += targetEx.InnerException.Message;
					if (targetEx.InnerException.StackTrace != null)
					{
						m_strStackTrace += "\r\n";
						m_strMessage += targetEx.StackTrace;
					}
				}
			}
		}

		////////////////////////////////////////////////////////
		public string Message
		{
			get 
			{
				return m_strMessage+"\n"+m_strStackTrace;
			}
		}

        ////////////////////////////////////////////////////////
        public virtual bool IsAvertissement
        {
            get
            {
                return false;
            }
        }




	}
}
