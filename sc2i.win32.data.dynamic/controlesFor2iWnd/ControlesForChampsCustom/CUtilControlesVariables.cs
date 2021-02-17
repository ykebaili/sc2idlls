using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using sc2i.data.dynamic;
using System.Drawing;

namespace sc2i.win32.data.dynamic
{
	public class CUtilControlesVariables
	{
		public static HorizontalAlignment GetHAlign(sc2i.data.dynamic.C2iWndVariable.TypeAlignement alignement)
		{
			HorizontalAlignment hAlignement = HorizontalAlignment.Left;
			switch (alignement)
			{
				case C2iWndVariable.TypeAlignement.Centre:
					hAlignement = HorizontalAlignment.Center;
					break;
				case C2iWndVariable.TypeAlignement.Droite:
					hAlignement = HorizontalAlignment.Right;
					break;
				case C2iWndVariable.TypeAlignement.Gauche:
					hAlignement = HorizontalAlignment.Left;
					break;
			}
			return hAlignement;
		}

		public static ContentAlignment GetContentAlign(sc2i.data.dynamic.C2iWndVariable.TypeAlignement alignement)
		{
			ContentAlignment cAlignement = ContentAlignment.TopLeft;
			switch (alignement)
			{
				case C2iWndVariable.TypeAlignement.Centre:
					cAlignement = ContentAlignment.TopCenter;
					break;
				case C2iWndVariable.TypeAlignement.Droite:
					cAlignement = ContentAlignment.TopRight;
					break;
				case C2iWndVariable.TypeAlignement.Gauche:
					cAlignement = ContentAlignment.TopLeft;
					break;
			}
			return cAlignement;
		}

	}
}
