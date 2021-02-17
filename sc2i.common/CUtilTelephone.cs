using System;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de CUtilTelephone.
	/// </summary>
	public sealed class CUtilTelephone
	{
        private CUtilTelephone() { }

		/// <summary>
		/// //////////////////////////////////////////////////////////////
		/// </summary>
		/// <param name="strTelephone"></param>
		/// <returns></returns>
		public static string GetValeurStockage ( string strTelephone )
		{
			//Supprime tous les .-
			strTelephone = strTelephone.Replace(".","");
			strTelephone = strTelephone.Replace("-","");
			strTelephone = strTelephone.Replace(" ","");
			return strTelephone;
		}

		/// //////////////////////////////////////////////////////////////
		public static string GetValeurAffichage ( string strValeurStockee )
		{
			strValeurStockee = GetValeurStockage ( strValeurStockee );
			
			string strNumber = "01234567890";
			int nNbDigit = 0;
			for ( int nTmp = 0; nTmp < strValeurStockee.Length; nTmp++ )
				if ( strNumber.IndexOf(strValeurStockee[nTmp]) >= 0 )
					nNbDigit++;
			int nCountNumber = 2;
			if ( (nNbDigit % 2) != 0 )
				nCountNumber = 1;
			string strRetour = "";
			string strFermants = "])";
			for ( int nI = 0; nI < strValeurStockee.Length; nI++ )
			{
				if ( nCountNumber == 0 && strFermants.IndexOf(strValeurStockee[nI]) <0)
				{
					strRetour += " ";
					nCountNumber = 2;
				}
				strRetour += strValeurStockee[nI];
				if ( strNumber.IndexOf(strValeurStockee[nI])>= 0 )
					nCountNumber--;
				
			}
			return strRetour;
		}

	}
}
