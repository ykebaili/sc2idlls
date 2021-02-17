using System;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de CGenerateurStringUnique.
	/// </summary>
	public sealed class CGenerateurStringUnique
	{
		private static string m_strCars = "ABCDEFGHJKMNPQRSTUVWXYZ123456789";

        private CGenerateurStringUnique() { }

		public static string GetCodeFor(long lNum)
		{
			string strCode = "";
			long lLen = m_strCars.Length;
			long lVal = lNum/lLen;
			if (lVal>0)
				strCode+= GetCodeFor ( lVal );
			lVal = lNum % lLen;
			if (lVal<0)
				lVal = -lVal;
			strCode += m_strCars[(int)lVal];
			return strCode;
		}

		public static string GetNewNumero(int nNum)
		{
			string strCode = "";
			DateTime dtVal = DateTime.Now;
			long lVal = (dtVal.Year - 1950)*12*31;
			lVal += dtVal.Month*31 + dtVal.Day;
			strCode = GetCodeFor(lVal);

			lVal = dtVal.Hour * 100 * 60 * 60;
			lVal += dtVal.Minute * 100 * 60;
			lVal += dtVal.Second * 100;
			lVal += dtVal.Millisecond /10;

			strCode += GetCodeFor(lVal);
			strCode += GetCodeFor(nNum);
				
			return strCode;
  		}

		public static long GetNumFromCode ( string strCode )
		{
			long lVal = 0;
			int nPuissance = strCode.Length-1;
			foreach ( char cVal in strCode.ToCharArray() )
			{
				int nPos = m_strCars.IndexOf ( cVal );
				if ( nPos >= 0 )
					lVal += (long)Math.Pow ( m_strCars.Length, nPuissance )*nPos;
				nPuissance--;
			}
			return lVal;
		}

	}
}
