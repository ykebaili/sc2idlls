using System;

namespace sc2i.win32.common
{
	/// <summary>
	/// Description r�sum�e de C2iControl.
	/// </summary>
	public interface IControlALockEdition 
	{
		bool LockEdition{get;set;}
		event EventHandler OnChangeLockEdition;
	}
}
