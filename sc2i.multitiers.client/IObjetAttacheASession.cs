using System;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Description r�sum�e de IObjetAttacheASession.
	/// </summary>
	public interface IObjetAttacheASession
	{
		string DescriptifObjetAttacheASession{get;}
		void OnCloseSession ( );
	}
}
