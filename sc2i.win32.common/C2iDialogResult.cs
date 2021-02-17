using System;
using System.Windows.Forms;

using sc2i.common;

namespace sc2i.win32.common
{
	/// <summary>
	/// Description résumée de C2iDialogResult.
	/// </summary>
	public class C2iDialogResult
	{
		public static E2iDialogResult Get2iDialogResultFromDialogResult ( DialogResult result )
		{
			switch ( result )
			{
				case DialogResult.Abort :
					return E2iDialogResult.Abort;
				case DialogResult.Cancel:
					return E2iDialogResult.Cancel;
				case DialogResult.Ignore :
					return E2iDialogResult.Ignore;
				case DialogResult.No :
					return E2iDialogResult.No;
				case DialogResult.None : 
					return E2iDialogResult.None;
				case DialogResult.OK :
					return E2iDialogResult.OK;
				case DialogResult.Retry :
					return E2iDialogResult.Retry;
				case DialogResult.Yes :
					return E2iDialogResult.Yes;
			}
			return E2iDialogResult.None;
		}
	}
}
