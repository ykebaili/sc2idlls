using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace sc2i.win32.common
{
	public class C2iLabel : Label, IControlALockEdition
	{
		#region IControlALockEdition Membres

		public bool LockEdition
		{
			get
			{
				return true;
			}
			set
			{
				if (OnChangeLockEdition != null)
					OnChangeLockEdition(this, new EventArgs());
			}
		}

		public event EventHandler OnChangeLockEdition;

		#endregion
	}
}
