using System;
using System.Collections;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Description résumée de CConvertisseurObjetToSqlServeur.
	/// </summary>
	public class CConvertisseurObjetToMySql
	{
		public static string ConvertToMySql ( object valeur )
		{
			if ( valeur == null )
				return "null";
			if ( valeur is string )
			{
				string strVal = (string)valeur;
				return "'"+strVal.Replace("'", "\\'")+"'";
			}
			if ( valeur is DateTime )
			{
				DateTime dt = (DateTime)valeur;
				return dt.ToString ("'yyyy-MM-dd H:m:s'");
			}
			if ( valeur is bool )
				return ((bool)valeur)?"1":"0";
			if ( valeur is IList )
			{
				string strRetour = "(";
				foreach ( object elt in (IList)valeur )
					strRetour += ConvertToMySql ( elt )+",";
				if ( strRetour.Length > 1 )
					strRetour = strRetour.Substring(0, strRetour.Length-1 );
				strRetour += ")";
				return strRetour;
			}
				
														   

			return valeur.ToString();
		}
	}
}
