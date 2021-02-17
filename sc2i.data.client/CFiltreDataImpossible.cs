using System;

namespace sc2i.data
{
	/// <summary>
	/// CFiltreData qui ne correspond à aucune ligne
	/// </summary>
	[Serializable]
	public class CFiltreDataImpossible : CFiltreData
	{
		/// /////////////////////////////////////////////
		public CFiltreDataImpossible()
		{
			Filtre = "0=1";
		}

		/// /////////////////////////////////////////////
		
	}
}
