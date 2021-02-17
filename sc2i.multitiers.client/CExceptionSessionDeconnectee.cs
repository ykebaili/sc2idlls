using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;

namespace sc2i.multitiers.client
{
	[Serializable]
	public class DisconnectedSessionException : Exception
	{
		public override string Message
		{
			get
			{
				return I.T("Disconnected session|109");
			}
		}
	}
}
