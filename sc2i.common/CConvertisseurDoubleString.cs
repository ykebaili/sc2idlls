using System;
using System.Globalization;

namespace sc2i.common
{
	/// <summary>
	/// Convertit une chaine en double en s'affranchissant des pbs
	/// liés au point ou à la virgule
	/// </summary>
	public sealed class CConvertisseurDoubleString
	{
        private CConvertisseurDoubleString() { }

		public static string Convert ( double dValue )
		{
			return dValue.ToString(CultureInfo.CurrentCulture);
		}

		public static double Convert ( string strValue )
		{
			double dVal = 0;
			try
			{
				dVal = Double.Parse(strValue, CultureInfo.CurrentCulture);
			}
			catch
			{
				if ( strValue.IndexOf('.') >= 0 )
					strValue = strValue.Replace('.',',');
				else
					strValue = strValue.Replace(',','.');
				try
				{
					dVal = Double.Parse ( strValue, CultureInfo.CurrentCulture );
				}
				catch
				{
					dVal = 0;
				}
			}
			return dVal;
		}
	}
}
