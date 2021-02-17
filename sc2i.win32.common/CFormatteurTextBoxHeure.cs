using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace sc2i.win32.common
{
	public class CFormatteurTextBoxHeure : CFormatteurTextBox
	{
		private static string c_strSeparateursHeures = ":";
		public CFormatteurTextBoxHeure()
		{
		}

		public override object GetValue(string strTexte)
		{
			return strTexte;
		}

		public override string GetTextToDisplay(string strTexte, ref int nPosCurseur)
		{
			bool bInMin = false;

			string strRetour = "";
			string strSeps = c_strSeparateursHeures;
			string strNums = "0123456789";
			string strMin = "";
			foreach (char c in strTexte)
			{
				if (strSeps.IndexOf( c)  >= 0)
				{
					strRetour += c;
					bInMin = true;
				}
				else
				{
					if (strNums.IndexOf(c) >= 0)
					{
						if (!bInMin)
							strRetour += c;
						else
						{
							if (strMin.Length < 2)
							{
								strMin += c;
								strRetour += c;
							}
						}
					}
				}
			}
			return strRetour;

		}


	}
}
