using System;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Description r�sum�e de IAdUsers.
	/// </summary>
	public interface IAdComputersServer
	{
		CAdComputer[] GetComputers();
		CAdComputer GetComputer ( string strId );
	}
}
