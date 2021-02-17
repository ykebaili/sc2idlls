using System;
using System.Collections;
using System.Text;
using System.Globalization;
using System.Data;
using System.Collections.Generic;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de CUtilTexte.
	/// </summary>
	public sealed class CUtilTexte
	{
		public const string c_strConstNull = "@NULL";

        private CUtilTexte() { }

		public static string GetInitiales ( string strTexte, bool bMajuscules )
		{
			string strRetour = "";
			string[] strVals = strTexte.Split(' ');
			foreach ( string strVal in strVals )
				if ( strVal.Length > 0 )
					strRetour += bMajuscules?strVal.ToUpper(CultureInfo.CurrentCulture)[0]:strVal[0];
			return strRetour;
		}

		/// <summary>
		/// Supprime les caractères qui ne sont pas numériques
		/// </summary>
		/// <param name="strValue"></param>
		/// <returns></returns>
		public static string RemoveNonChiffre ( string strValue )
		{
			string strNew = "";
			foreach( char cTmp in strValue )
				if ( Char.IsDigit(cTmp) )
					strNew += cTmp;
			return strNew;
		}
        
		/// <summary>
		/// Remplace les accents par leur équivalent sans accent
		/// </summary>
		/// <param name="strValue"></param>
		/// <returns></returns>
		public static string RemplaceAccentsParEquivalent ( string strValue )
		{
			char[] carsToReplace = {'à','â','ä','é','è','ê','ë','î','ï','ô','ö','û','ü','ç'};
			char[] carsRemplacan = {'a','a','a','e','e','e','e','i','i','o','o','u','u','c'};
			Hashtable tableReplace = new Hashtable();

			for ( int nTmp = 0; nTmp < carsToReplace.Length; nTmp++ )
			{
				tableReplace[carsToReplace[nTmp]] = carsRemplacan[nTmp];
			}

			StringBuilder builder = new StringBuilder( strValue.Length );
			foreach ( char cTmp in strValue )
			{
				object val = tableReplace[cTmp];
				if ( val != null )
					builder.Append ( (char)val );
				else
					builder.Append ( cTmp );
			}
			return builder.ToString();
		}
						
				/*strValue = strValue.Replace(carsToReplace[i], carsRemplacan[i]);
				strValue = strValue.Replace(Char.ToUpper(carsToReplace[i]), Char.ToUpper(carsRemplacan[i]) );
			}
			return strValue;
		}*/


		public static string TronqueLeMilieu ( string strTexte, int nNbCarsMax )
		{
			if ( strTexte.Length <= nNbCarsMax )
				return strTexte;
			int nEcart = Math.Max ( 3, strTexte.Length - nNbCarsMax );
			int nStart = strTexte.Length/2 - nEcart/2;
			string strRet = strTexte.Substring ( 0, nStart )+"..."+
				strTexte.Substring(nStart+nEcart );
			return strRet;
		}

		public static string ToUniversalString(object value)
		{
			if (value == null || value == DBNull.Value)
				return c_strConstNull;
			if (value is SByte)
			{
				return value.ToString();
			}
			else if (value is Int16)
			{
				return value.ToString();
			}
			else if (value is Int32)
			{
				return value.ToString();
			}
			else if (value is Int64)
			{
				return value.ToString();
			}
			else if (value is Byte)
			{
				return value.ToString();
			}
			else if (value is UInt16)
			{
				return value.ToString();
			}
			else if (value is UInt32)
			{
				return value.ToString();
			}
			else if (value is UInt64)
			{
				return value.ToString();
			}
			else if (value is Single)
			{
				return value.ToString().Replace(',','.');
			}
			else if (value is Double)
			{
				return value.ToString().Replace(',','.');
			}
			else if (value is Decimal)
			{
				return value.ToString().Replace(',','.');
			}
			else if (value is Boolean)
			{
				return ((bool)value)?"1":"0";
			}
			else if (value is Char)
			{
				return value.ToString();
			}
			else if (value is String)
			{
				if ( value.ToString() == c_strConstNull )
					return "@"+c_strConstNull;
				return value.ToString();
			}
            else if (value is DateTime)
            {
                return CUtilDate.GetUniversalString((DateTime)value);
            }
            else
            {
                CDateTimeEx dtex = (value as CDateTimeEx);
                if (dtex != null)
                {
                    return CUtilDate.GetUniversalString(dtex.DateTimeValue);
                }
            }
			throw new Exception(I.T("Unknown type for ToUniversalString |30107") + value.GetType().ToString());
		}

		public static object FromUniversalString ( string strValue, Type type )
		{
			if (strValue == c_strConstNull)
				return null;
			try
			{
				if (type == typeof(SByte) || type == typeof(SByte?))
				{
					return SByte.Parse(strValue, CultureInfo.CurrentCulture);
				}
				else if (type == typeof(Int16) || type == typeof(Int16?))
				{
                    return Int16.Parse(strValue, CultureInfo.CurrentCulture);
				}
				else if (type == typeof(Int32) || type == typeof(Int32?))
				{
                    return Int32.Parse(strValue, CultureInfo.CurrentCulture);
				}
				else if (type == typeof(Int64) || type == typeof(Int64?))
				{
                    return Int64.Parse(strValue, CultureInfo.CurrentCulture);
				}
				else if (type == typeof(Byte) || type == typeof(Byte?))
				{
                    return Byte.Parse(strValue, CultureInfo.CurrentCulture);
				}
				else if (type == typeof(UInt16) || type == typeof(UInt16?))
				{
                    return UInt16.Parse(strValue, CultureInfo.CurrentCulture);
				}
				else if (type == typeof(UInt32) || type == typeof(UInt32?))
				{
                    return UInt32.Parse(strValue, CultureInfo.CurrentCulture);
				}
				else if (type == typeof(UInt64) || type == typeof(UInt64?))
				{
                    return UInt64.Parse(strValue, CultureInfo.CurrentCulture);
				}
				else if (type == typeof(Single) || type == typeof(Single?))
				{
					try
					{
                        return Single.Parse(strValue, CultureInfo.CurrentCulture);
					}
					catch
					{
						if (strValue.IndexOf('.') >= 0)
							strValue = strValue.Replace('.', ',');
						else
							strValue = strValue.Replace(',', '.');
                        return Single.Parse(strValue, CultureInfo.CurrentCulture);
					}
				}
				else if (type == typeof(Double) || type == typeof(Double?))
				{
					return CUtilDouble.DoubleFromString(strValue);
				}
				else if (type == typeof(Decimal) || type == typeof(Decimal?))
				{
					try
					{
                        return Decimal.Parse(strValue, CultureInfo.CurrentCulture);
					}
					catch
					{
						if (strValue.IndexOf('.') >= 0)
							strValue = strValue.Replace('.', ',');
						else
							strValue = strValue.Replace(',', '.');
                        return Decimal.Parse(strValue, CultureInfo.CurrentCulture);
					}
				}
				else if (type == typeof(Boolean) || type == typeof(Boolean?))
				{
					return strValue == "1";
				}
				else if (type == typeof(Char) || type == typeof(Char?))
				{
					if (strValue.Length > 0)
						return strValue[0];
					return '\0';
				}
				else if (type == typeof(String))
				{
					if (strValue == "@" + c_strConstNull)
						return c_strConstNull;
					return strValue;
				}
				else if (type == typeof(DateTime) || type == typeof(DateTime?))
				{
					return CUtilDate.FromUniversalString(strValue);
				}
				else if (type == typeof(CDateTimeEx))
				{
					return new CDateTimeEx(CUtilDate.FromUniversalString(strValue));
				}
			}
			catch 
			{
				return null;
			}
			throw new Exception(I.T("Unknown type for ToUniversalString |30107") + type.ToString());
		}


        public static string[] GetExcelLine(string strChaine, char strCellSep, ref int nStartPos)
        {
            List<string> lstChaines = new List<string>();
            //Parcours la chaine jusqu'à trouver un retour ligne.
            //Les retour ligne dans une cellule sont encadrés de "
            //Les guillemets dans une chaine entourée par des " sont doublées
            //Les chaines non entourées de " peuvent contenir des guillemets
            bool bInGuillemet = false;
            int nPos = nStartPos;
            StringBuilder blCellule = new StringBuilder();
            while (nPos < strChaine.Length && (strChaine[nPos] != '\n' || bInGuillemet))
            {
                char c = strChaine[nPos];
                if (blCellule.Length == 0 && c == '"')
                    bInGuillemet = true;
                else if (c == strCellSep && !bInGuillemet)
                {
                    lstChaines.Add(blCellule.ToString());
                    blCellule = new StringBuilder();
                }
                else if (c == '"')
                {
                    if (bInGuillemet)
                    {
                        if (nPos + 1 < strChaine.Length && strChaine[nPos + 1] == '"')
                        {
                            blCellule.Append('"');
                            nPos++;
                        }
                        else
                            bInGuillemet = false;
                    }
                    else blCellule.Append('"');
                }
                else
                    blCellule.Append(c);
                nPos++;
            }
            lstChaines.Add(blCellule.ToString());
            nStartPos = nPos+1;
            return lstChaines.ToArray();
        }

        /// <summary>
        /// Lorsqu'on copie dans excel, si une cellule contient des retour
        /// ligne, le contenu de la cellule est encadré par des guillemets.
        /// </summary>
        /// <param name="strChaine"></param>
        /// <returns></returns>
        public static DataTable GetDataTableFromExcelPaste(string strChaine, bool bFirstLineIsHeader)
        {
            StringBuilder bl = new StringBuilder();
            int nStartPos = 0;
            DataTable table = new DataTable();
            table.BeginLoadData();
            if (bFirstLineIsHeader)
            {
                string[] strHeaders = GetExcelLine(strChaine, '\t', ref nStartPos);
                foreach (string strHeader in strHeaders)
                {
                    string strNomCol = "";
                    if (strHeader.Length == 0)
                        strNomCol = "Field " + (table.Columns.Count + 1);
                    else
                        strNomCol = strHeader.Trim();
                    DataColumn col = table.Columns.Add(strNomCol, typeof(string));
                    col.AllowDBNull = true;
                }
            }
            while (nStartPos < strChaine.Length)
            {
                string[] strDatas = GetExcelLine(strChaine, '\t', ref nStartPos);
                int nCol = 0;
                DataRow row = table.NewRow();
                foreach (string strData in strDatas)
                {
                    if (nCol >= table.Columns.Count)
                    {
                        DataColumn col = table.Columns.Add("Field " + (nCol + 1));
                        col.AllowDBNull = true;
                    }
                    row[nCol] = strData.Trim();
                    nCol++;
                }
                table.Rows.Add(row);
            }
            table.EndLoadData();
            return table;
        }


	}
}
