using System;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Description r�sum�e de IAdGroupsServeur.
	/// </summary>
	public interface IAdGroupsServeur
	{
		CAdGroup[] GetGroups();
		CAdGroup GetGroup( string strId );
	}
}
