using System;


namespace sc2i.common
{
	/// <summary>
	/// Description résumée de Sc2iMath.
	/// </summary>
	public sealed class Sc2iMath
	{
        private Sc2iMath() { }

		/// <summary>
		/// Retourne l'arrondi à nb décimales prêt
		/// </summary>
		/// <param name="fValeur"></param>
		/// <param name="nNbDecimales"></param>
		/// <returns></returns>
		public static double RoundUp ( double dValeur, int nNbDecimales )
		{
			double dTmp = dValeur * Math.Pow ( 10, nNbDecimales );
            if (nNbDecimales == int.MaxValue)
                throw new ArgumentOutOfRangeException("nNbDecimales", "le nombre de décimales doit être inférieur à Int32.MaxValue");
			if ( (dTmp - (int)dTmp ) >= 0.5 )
				return Math.Round(dValeur+Math.Pow(0.1,nNbDecimales+1), nNbDecimales );
			return Math.Round ( dValeur, nNbDecimales );
		}
	}
}
