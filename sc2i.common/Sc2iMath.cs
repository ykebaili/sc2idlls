using System;


namespace sc2i.common
{
	/// <summary>
	/// Description r�sum�e de Sc2iMath.
	/// </summary>
	public sealed class Sc2iMath
	{
        private Sc2iMath() { }

		/// <summary>
		/// Retourne l'arrondi � nb d�cimales pr�t
		/// </summary>
		/// <param name="fValeur"></param>
		/// <param name="nNbDecimales"></param>
		/// <returns></returns>
		public static double RoundUp ( double dValeur, int nNbDecimales )
		{
			double dTmp = dValeur * Math.Pow ( 10, nNbDecimales );
            if (nNbDecimales == int.MaxValue)
                throw new ArgumentOutOfRangeException("nNbDecimales", "le nombre de d�cimales doit �tre inf�rieur � Int32.MaxValue");
			if ( (dTmp - (int)dTmp ) >= 0.5 )
				return Math.Round(dValeur+Math.Pow(0.1,nNbDecimales+1), nNbDecimales );
			return Math.Round ( dValeur, nNbDecimales );
		}
	}
}
