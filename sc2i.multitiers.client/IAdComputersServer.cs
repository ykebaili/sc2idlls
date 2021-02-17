using System;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Description résumée de IAdUsers.
	/// </summary>
	public interface IAdComputersServer
	{
		CAdComputer[] GetComputers();
		CAdComputer GetComputer ( string strId );
	}
}
