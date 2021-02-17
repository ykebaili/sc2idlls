using System;

namespace sc2i.data
{
	/// <summary>
	/// Stock des changements d'id d'objet à faire
	/// </summary>
	[Serializable]
	public class CConversionDataId
	{
		public readonly string NomTable;
		public readonly int OldId;
		public readonly int NewId;

		public CConversionDataId ( string strNomTable, int nOldId, int nNewId )
		{
			NomTable = strNomTable;
			OldId = nOldId;
			NewId = nNewId;
		}

	}
}
