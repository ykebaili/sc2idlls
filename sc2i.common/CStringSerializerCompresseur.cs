using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de CStringSerializerCompresseur.
	/// </summary>
	public class CStringSerializerCompresseur
	{
		private const int c_nLongueurMinDico = 4;

		private const string c_strCaracteresCodeDico = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz&é'(-è_çà)=+°0@ù%*µ!§:/;.,?";

		private const char c_cCaractereDico = '¤';
		private const string c_strIdZip = "Z~IP";


		private class CDico
		{
			public string m_strValeur;
			public int m_nNombreRencontre;
			public string m_strCode;
		}


		/// ///////////////////////// ///////////////////////// //////////////////////
		public CStringSerializerCompresseur()
		{
		}

		/// ///////////////////////// ///////////////////////// //////////////////////
		private string GetCodeDico ( int nDico )
		{
			int nSuite = (int)(nDico/c_strCaracteresCodeDico.Length);
			int nVal = nDico % c_strCaracteresCodeDico.Length;
			
			string strVal = c_strCaracteresCodeDico[nVal]+"";
			if ( nSuite > 0 )
				strVal +=GetCodeDico(nSuite);
			return strVal;
		}

		/// ///////////////////////// ///////////////////////// //////////////////////
		/*private int GetNumDico ( string strCodeDico )
		{
			return GetNumFromCode ( strCodeDico, 0 );
		}*/

		/// ///////////////////////// ///////////////////////// //////////////////////
		/*private int GetNumFromCode ( string strCode, int nPosition )
		{
			if ( strCode.Length  == 0 )
				return 0;
			int nVal = strCode[0]*(m_strCaracteresCodeDico.Length^nPosition)+GetNumFromCode ( strCode.Substring(1), nPosition+1);
			return nVal;
		}*/

		/// ///////////////////////// ///////////////////////// //////////////////////
		private string GetNumVersion()
		{
			return GetCodeDico(0);
		}

		/// ///////////////////////// ///////////////////////// //////////////////////
		private string ZipCompress(string strDataOriginal)
		{
			byte[] buffer = Encoding.UTF8.GetBytes(strDataOriginal);
			MemoryStream ms = new MemoryStream();
			using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true))
			{
				zip.Write(buffer, 0, buffer.Length);
			}

			ms.Position = 0;

			byte[] compressed = new byte[ms.Length];
			ms.Read(compressed, 0, compressed.Length);

			byte[] gzBuffer = new byte[compressed.Length + 4];
			System.Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
			System.Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);
			string strCompress = Convert.ToBase64String(gzBuffer);
			strCompress = c_strIdZip + strCompress;
			if (strCompress.Length < strDataOriginal.Length)
				return strCompress;
			return strDataOriginal;
		}

		/// ///////////////////////// ///////////////////////// //////////////////////
		private string ZipUncompress(string strDataCompresse)
		{
			if (strDataCompresse.Length < c_strIdZip.Length)
				return strDataCompresse;
			if (strDataCompresse.Substring(0, c_strIdZip.Length) != c_strIdZip)
				return strDataCompresse;
			strDataCompresse = strDataCompresse.Substring(c_strIdZip.Length);
			byte[] gzBuffer = Convert.FromBase64String(strDataCompresse);
			using (MemoryStream ms = new MemoryStream())
			{
				int nMsgLength = BitConverter.ToInt32(gzBuffer, 0);
				ms.Write(gzBuffer, 4, gzBuffer.Length - 4);

				byte[] buffer = new byte[nMsgLength];

				ms.Position = 0;
				using (GZipStream zip = new GZipStream(ms, CompressionMode.Decompress))
				{
					zip.Read(buffer, 0, buffer.Length);
				}

				return Encoding.UTF8.GetString(buffer);
			}
		}


		/// <summary>
		/// ///////////////////////// ///////////////////////// //////////////////////
		/// </summary>
		/// <param name="strData"></param>
		/// <param name="nNbPaquets"></param>
		/// <returns></returns>
		public string Compress(string strDataOriginal)
		{
			//SC le 14/05/2008, utilise la compression zip
			string strZip = ZipCompress(strDataOriginal);
			//Remplace tous les ¤ par des \¤
			string strData = strDataOriginal.Replace("¤","\\¤");
			Hashtable tableDico = new Hashtable();
			ArrayList lstValeurs = new ArrayList();
			int nTotal = strData.Length;
			string strChaine = "";
			bool bSlashAvant = false;
			int nPos;
			for ( nPos = 0; nPos < nTotal; nPos++ )
			{
				if ( strData[nPos] == '~' && !bSlashAvant )
				{
					lstValeurs.Add ( strChaine );
					strChaine = "";
				}
				else 
					strChaine += strData[nPos];
				if ( strData[nPos] == '\\' )
					bSlashAvant = true;
				else
					bSlashAvant = false;
			}
			lstValeurs.Add ( strChaine );

			//Créee la table dico de base
			nTotal = lstValeurs.Count;
			ArrayList lstDicosMultiples = new ArrayList();

			for ( int nZone = 0; nZone < nTotal; nZone++ )
			{
				string strBase = (string)lstValeurs[nZone];
				if ( strBase.Length > c_nLongueurMinDico ) 
				{
					CDico dico = (CDico)tableDico[strBase];
					if ( dico != null )
					{
						dico.m_nNombreRencontre++;
						if ( dico.m_nNombreRencontre >= 2 )
							lstDicosMultiples.Add ( dico );
					}
					else
					{
						dico = new CDico();
						dico.m_strValeur = strBase;
						dico.m_nNombreRencontre = 1;
						tableDico[strBase] = dico;
					}
				}
			}
			tableDico.Clear();
			nPos = 0;
			string strDico = "";
			foreach ( CDico dico in lstDicosMultiples )
			{
				if ( tableDico[dico.m_strValeur] == null )
				{
					dico.m_strCode = GetCodeDico ( nPos );
					tableDico[dico.m_strValeur] = dico;
					strDico += c_cCaractereDico+dico.m_strValeur;
					nPos++;
				}
			}


			string strComp = strDico+c_cCaractereDico+c_cCaractereDico;
			
			foreach ( string strValeur in lstValeurs )
			{
				CDico dico = (CDico)tableDico[strValeur];
				if ( dico != null )
					strComp += c_cCaractereDico+dico.m_strCode;
				else
					strComp += strValeur+"~";
			}
			if ( strData[strData.Length-1] == '~' )
				//Supprime le dernier ~ qui est doublé
				strComp = strComp.Substring(0, strComp.Length-1 );

			strComp = c_cCaractereDico+GetNumVersion()+strComp;
			if (strZip.Length < strData.Length)
			{
				if (strZip.Length < strComp.Length)
					return strZip;
			}
			if ( strComp.Length > strDataOriginal.Length )
				return strDataOriginal;
			return strComp;
		}

		/// <summary>
		/// //////////////////////
		/// </summary>
		/// <param name="strData"></param>
		/// <param name="nNbPaquets"></param>
		/// <returns></returns>
		public string UnCompress ( string strTexte )
		{
			if ( strTexte.Length == 0 )
				return "";
			if ( strTexte[0] != c_cCaractereDico )
				return ZipUncompress(strTexte) ;
			//Lecture du dictionnaire
			int nPos = 2;
			int nDico = 0;
			Hashtable tableDico = new Hashtable();
			bool bStop = nPos >= strTexte.Length-1 || strTexte[nPos] != c_cCaractereDico;
			while ( !bStop )
			{
				//On s'arrête quand on a deux ¤¤
				if ( strTexte[nPos+1] == c_cCaractereDico )
				{
					bStop = true;
					nPos += 2;
				}
				else
				{
					//Trouve l'entrée de dico
					int nPosFin = strTexte.IndexOf(c_cCaractereDico, nPos+1)-1;
					while (strTexte[nPosFin] == '\\') //c'est un \¤ on ignore
						nPosFin = strTexte.IndexOf(c_cCaractereDico, nPosFin + 2) - 1;
					string strEntree = strTexte.Substring(nPos+1, nPosFin-nPos);
					string strCodeDico = GetCodeDico ( nDico );
					nDico++;
					tableDico[strCodeDico] = strEntree;
					nPos = nPosFin;
					nPos++;
					if ( nPos < strTexte.Length-1 && strTexte[nPos] != c_cCaractereDico )
						throw new Exception(I.T("Uncompression error|30065"));
					bStop = bStop || nPos >= strTexte.Length-1;
				}
			}
			string strUncomp = strTexte.Substring ( nPos );
			foreach ( string strCodeDico in tableDico.Keys )
			{
                int nPosDic = strUncomp.IndexOf('¤' + strCodeDico, 0);
                while (nPosDic >= 0)
                {
                    if (nPosDic >= 0)
                    {
                        if (nPosDic == 0 || strUncomp[nPosDic - 1] != '\\')
                            strUncomp = strUncomp.Substring(0, nPosDic) + (string)tableDico[strCodeDico]+"~" + strUncomp.Substring(nPosDic+strCodeDico.Length + 1);
                    }
                    nPosDic = strUncomp.IndexOf('¤' + strCodeDico, nPosDic + 1);
                }

				/*Regex ex = new Regex("[^\\\\]+¤"+strCodeDico);
				strUncomp = ex.Replace(strUncomp, (string)tableDico[strCodeDico] + "~");*/
			}
			strUncomp = strUncomp.Replace("\\¤","¤");
			return strUncomp;
		}
	}
}
