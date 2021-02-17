using System;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Description résumée de IAdGroupsServeur.
	/// </summary>
	public interface IAdGroupsServeur
	{
		CAdGroup[] GetGroups();
		CAdGroup GetGroup( string strId );
	}
}
