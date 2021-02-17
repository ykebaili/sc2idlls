using System;
using sc2i.common;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Description résumée de CServiceSurClientFermerApplication.
	/// </summary>
	public abstract class CServiceSurClientFermerApplication : CServiceSurClient
	{
		public static string c_idService = "CLOSE_APPLICATION";

		public CServiceSurClientFermerApplication()
		{
		}

		public override string IdService
		{
			get
			{
				return c_idService;
			}
		}
	}
}
