using System;
using System.Collections.Generic;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de CSc2iDataConst.
	/// </summary>
	public class CSc2iDataConst
	{
		public const string c_champIdSynchro = "SC2I_SYNC_SESSION";

		public const string c_champIdVersion = "SC2I_VERSION";
		public const string c_champOriginalId = "SC2I_ORIGINAL_ID";
		public const string c_champIsDeleted = "SC2I_DELETED";

		public const string c_ServiceFiltresSynchronisation = "SYNC_FILTER_SERVICE";
		public const string c_ServiceSynchronisationSecondaire = "SYNC_SECONDAIRE_SERVICE";

		public static List<string> GetNomsChampsSysteme()
		{
			List<string> lst = new List<string>();
			lst.Add(c_champIdSynchro);
			lst.Add(c_champIdVersion);
			lst.Add(c_champIsDeleted);
			lst.Add(c_champOriginalId);
			return lst;
		}
	}
}
