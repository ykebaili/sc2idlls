using System;
using System.Globalization;

namespace sc2i.common
{
	/// <summary>
	/// Fournit un algorithme de cryptographie basique mais efficace !
	/// </summary>
	public sealed class C2iCrypto
	{
		/// ////////////////////////////////////
		private C2iCrypto()
		{
		}


		/// ////////////////////////////////////
		public static string Crypte( string strChaine )
		{
			if ( String.IsNullOrEmpty(strChaine) )
				return "";
			byte[] cRetour = new byte[strChaine.Length+3];
			byte btCheckSum=0;
			byte btCarn, btCarMoinsUn;
			int nTmp;
			nTmp = strChaine.Length<<4;
			cRetour[0] = (byte)(nTmp & 0xFF);
			cRetour[1] = (byte)((nTmp &0xFF00)>>8);
			foreach ( char cTmp in strChaine )
				btCheckSum += (byte)cTmp;
			cRetour[2]=btCheckSum;
			btCarn = (byte)((byte)strChaine[0] + btCheckSum) ;
			cRetour[3] = btCarn;
			btCarMoinsUn = btCarn;
			for (int nCar = 1; nCar < strChaine.Length; nCar++)
			{
				nTmp = strChaine[nCar] + btCarMoinsUn - btCheckSum;
				btCarn = (byte)nTmp;
				cRetour[nCar+3]=btCarn;
				btCarMoinsUn = btCarn	;
			}
			string strRetour = "";

			//Reconvertit le tout en hexa
			foreach ( byte btTmp in cRetour )
			{
				strRetour += btTmp.ToString("X", CultureInfo.InvariantCulture).PadLeft(2,'0');
			}
			return strRetour;
		}

		/// ////////////////////////////////////
		public static string Decrypte ( string strChaine )
		{
			if ( String.IsNullOrEmpty(strChaine) )
				return "";
			//Convertit de l'hexa en table de bytes
			byte[] cData = new byte[strChaine.Length/2];
			for ( int nCar = 0; nCar < strChaine.Length; nCar+= 2 )
			{
				string strHex = strChaine.Substring(nCar, 2 );
				cData[nCar/2] = Convert.ToByte ( strHex, 16 );
			}

			int nTaille = cData[0]+(cData[1]<<8);
			nTaille = nTaille >> 4;

			string strRetour = "";
			byte btCheckSum = 0;
			byte btCar;

			btCheckSum = cData[2];
            btCar = (byte)(cData[3] - btCheckSum);
            strRetour += (char)btCar;
	
			for (int nCar = 4; nCar < cData.Length; nCar++)
			{
                btCar = (byte)(cData[nCar] - cData[nCar - 1] + btCheckSum);
                strRetour += (char)btCar;
			}
			return strRetour;
		}

	}
}
