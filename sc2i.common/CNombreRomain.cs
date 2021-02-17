using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.common
{
	public sealed class CNombreRomain
	{
		private const string c_strSymboles = "IVXLCDM";

        private CNombreRomain() { }

		public static string RomainFromInt(int nVal)
		{
			if (nVal > 10000)
				return I.T("Impossible Roman conversion|30057");
			string strRomain = "";
			int nIndex = 0;
			while (nVal != 0)
			{
				string strUniteRomaine = "";
				if (nIndex == c_strSymboles.Length - 1)
				{
					for (int nTmp = 0; nTmp < nVal; nTmp++)
					{
						strUniteRomaine += c_strSymboles[nIndex];
					}
					nVal = 0;
				}
				else
				{
					int nUnite = nVal % 10;
					int nDiz = nVal / 10;
					if (nUnite >= 4 && nUnite < 9)
						strUniteRomaine = c_strSymboles[nIndex + 1] + "";
					if (nUnite == 9)
						strUniteRomaine = "" + c_strSymboles[nIndex] + c_strSymboles[nIndex + 2];
					if (nUnite == 4)
						strUniteRomaine = c_strSymboles[nIndex] + strUniteRomaine;
					nUnite = nUnite - nUnite / 5 * 5;
					if (nUnite < 4)
						for (int nTmp = 0; nTmp < nUnite; nTmp++)
						{
							strUniteRomaine += c_strSymboles[nIndex];
						}
					nVal = nDiz;
					nIndex += 2;
				}
				strRomain = strUniteRomaine + strRomain;
			}
			return strRomain;
		}

		public static int IntFromRomain(string strRomain)
		{
			int nVal = 0;
			int nPos = 0;
			int nLastSymbMore = 0;
			int nNbPareils = 0;
			while (strRomain.Length > 0)
			{
				char cVal = strRomain[strRomain.Length - 1];
				strRomain = strRomain.Substring(0, strRomain.Length - 1);
				int nSymb = c_strSymboles.IndexOf(cVal);
				if (nSymb < 0)
					throw new Exception(I.T("Conversion error : the character @1 is not valid for a Roman number|30058",cVal.ToString()));  
                      
				int nValSymb = (nSymb % 2) == 0 ? (int)Math.Pow(10, (nSymb / 2)) : 5 * (int)Math.Pow(10, (nSymb / 2));
				if (nSymb < nPos)
				{
					if (nSymb % 2 != 0 || nPos - nSymb > 2)
						throw new Exception(I.T("Incorrect Roman number|30059"));
					if (nLastSymbMore - strRomain.Length != 1)
                        throw new Exception(I.T("Incorrect Roman number|30059"));
					nVal -= nValSymb;
				}
				else
				{
					nVal += nValSymb;
					if (nPos == nSymb)
					{
						nNbPareils++;
						if (nNbPareils > 3 && nPos != c_strSymboles.Length - 1 || nSymb % 2 != 0)
                            throw new Exception(I.T("Incorrect Roman number|30059"));
					}
					else
					{
						nPos = nSymb;
						nNbPareils = 1;
					}
					nLastSymbMore = strRomain.Length;
				}
			}
			return nVal;
		}

	}
}
