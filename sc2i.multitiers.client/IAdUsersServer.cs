using System;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Description résumée de IAdUsers.
	/// </summary>
	public interface IAdUsersServer
	{
		CAdUser[] GetUsers();
		CAdUser GetUser ( string strId );
	}
}
