using System;

using System.Text.RegularExpressions;
using sc2i.common;

namespace sc2i.common.memorydb
{
	/// <summary>
	/// Description résumée de CFormatteurFiltreDataToStringDataTable.
	/// </summary>
	public class CFormatteurFiltreDataToStringDataTable : IFormatteurFiltreDataToString
	{

		////////////////////////////////////////////////////////
		public CFormatteurFiltreDataToStringDataTable( )
		{
		}

		////////////////////////////////////////////////////////
		public string GetString ( CFiltreData filtre )
		{
			string strFiltre = filtre.Filtre;

			//Remplace le & (operateur binaire) qui n'est pas implémenté
			strFiltre = RemplaceEtOuBinaire ( strFiltre, 0 );

			int nNumParametre = 1;
			foreach ( object obj in filtre.Parametres )
			{
				string strReplace = obj.ToString();
				if ( obj is String )
				{
					strReplace = strReplace.Replace("'","''");
					strReplace = "'"+strReplace+"'";
				}
				if ( obj is DateTime )
				{
					DateTime dt = (DateTime)obj;
					strReplace = "#"+dt.ToString("MM/dd/yyyy HH:mm")+"#";
				}
				if ( obj is bool )
					strReplace = ((bool)obj)?"1":"0";
				/*if ( strFiltre.IndexOf("@"+nNumParametre.ToString()) >= 0 )
					strFiltre = strFiltre.Replace("@"+nNumParametre.ToString(), strReplace );
				else*/
				Regex ex = new Regex ( "(@"+nNumParametre.ToString()+")(?<SUITE>[^0123456789]{1})" );
				strFiltre = ex.Replace ( strFiltre+" ", strReplace+"${SUITE}" );
				//strFiltre = strFiltre.Replace("@"+nNumParametre.ToString(), strReplace );
				nNumParametre++;
			}
			return strFiltre;
		}
		
		////////////////////////////////////////////////////////
		public static string GetStringFor ( object obj )
		{
			string strReplace = obj.ToString();
			if ( obj is String )
			{
				strReplace = strReplace.Replace("'","\'");
				strReplace = "'"+strReplace+"'";
			}
			if ( obj is DateTime )
			{
				DateTime dt = (DateTime)obj;
				strReplace = "#"+dt.Month.ToString()+"/"+dt.Day.ToString()+"/"+dt.Year.ToString()+"#";
			}
			if ( obj is bool )
				strReplace = ((bool)obj)?"1":"0";
			/*if ( strFiltre.IndexOf("@"+nNumParametre.ToString()) >= 0 )
					strFiltre = strFiltre.Replace("@"+nNumParametre.ToString(), strReplace );
				else*/
			return strReplace;
		}

		////////////////////////////////////////////////////////
		///LE & et le | binaire ne sont pas pris en charge,
		///fabrication à la main !!!
		private string RemplaceEtOuBinaire ( string strFiltre, int nPosStart )
		{
			return strFiltre;
			/*bool bSlash = false;
			bool bGuillemetSimple = false;
			bool bGuillemetDouble = false;
			for ( int n = nPosStart; n < strFiltre.Length; n++ )
			{
				char c = strFiltre[n];
				if ( c == "\\" )
					bSlash = true;
				if ( c=='"' && !bSlash )
					bGuillemetDouble = !bGuillemetDouble;
				if ( c=='\'' && !bSlash )
					bGuillemetSimple = !bGuillemetSimple;
				if ( c == '&' && !bSlash && !bGuillemetDouble && !bGuillemetSimple )
					

					if ( strFiltre[n] != "\\" )
						bSlash = false;
			}
			return strFiltre;*/
			

		}
		
	}
}
