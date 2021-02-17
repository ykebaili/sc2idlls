using System;
using System.Data;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Description r�sum�e de IAdapterBuilder.
	/// </summary>
	public interface IAdapterBuilder
	{
		IDataAdapter GetNewAdapter ( DataRowState etatsAPrendreEnCompte ,bool bDisableIdAuto, params string[]strChampsExclus);
	}
}
