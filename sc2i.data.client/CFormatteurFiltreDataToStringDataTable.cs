using System;

using System.Text.RegularExpressions;
using sc2i.common;

namespace sc2i.data
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
			if ( filtre is CFiltreDataAvance )
				return GetStringAvance ( (CFiltreDataAvance)filtre);
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
					strReplace = "#"+dt.ToString("MM/dd/yyyy HH:mm:ss")+"#";
				}
				if ( obj is bool )
					strReplace = ((bool)obj)?"1":"0";
				/*if ( strFiltre.IndexOf("@"+nNumParametre.ToString()) >= 0 )
					strFiltre = strFiltre.Replace("@"+nNumParametre.ToString(), strReplace );
				else*/
				Regex ex = new Regex ( "(@"+nNumParametre.ToString()+")(?<SUITE>[^0123456789]{1})" );
                //stef 29/05/2012, les $ sont mals gérés dans une chaine de remplacement, et
                //ça met le bronx dans le replace.
                //On remplace donc les $ par une chaine improbables pour qu'il n'y ait
                //Plus de $ dans la chaine de replace
                strReplace = strReplace.Replace("$", "#~é&,è§£");
			    strFiltre = ex.Replace ( strFiltre+" ", strReplace+"${SUITE}" );
                strFiltre = strFiltre.Replace("#~é&,è§£","$");
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

		////////////////////////////////////////////////////////
		public string GetStringAvance ( CFiltreDataAvance filtre )
		{
			CComposantFiltre composant = filtre.ComposantPrincipal;
			if ( composant == null )
				return "";
			//remplace chaque champ par sa valeur
			for ( int nParametre = 1; nParametre <= filtre.Parametres.Count; nParametre++ )
				(composant).RemplaceVariableParConstante("@"+nParametre, filtre.Parametres[nParametre-1]);

			return GetStringExpression ( composant, filtre );
		}

		////////////////////////////////////////////////////////
		public string GetStringExpression ( IExpression expression, CFiltreData filtre )
		{
			string strRetour = "";

			if ( expression is CComposantFiltre )
				expression = ((CComposantFiltre)expression).GetComposantFiltreFinal(filtre);

			if ( expression is CComposantFiltreChamp )
			{
				CComposantFiltreChamp champ = (CComposantFiltreChamp)expression;
				foreach ( CInfoRelationComposantFiltre info in champ.Relations )
					strRetour += "Parent("+ info.RelationKey+").";
				strRetour += champ.NomChamp;
			}


			else if ( expression is CComposantFiltreConstante )
			{
				CComposantFiltreConstante constante = (CComposantFiltreConstante)expression;
				if ( constante.Valeur is String )
					strRetour = "'"+constante.Valeur.ToString().Replace("'","''")+"'";
				else if ( constante.Valeur is DateTime )
					strRetour += ((DateTime)constante.Valeur).ToShortDateString();
				else
					strRetour += constante.Valeur.ToString();
			}


			else if ( expression is CComposantFiltreListe )
			{
				CComposantFiltreListe liste = (CComposantFiltreListe)expression;
				strRetour = "";
				foreach ( IExpression expDeListe in liste.Liste )
				{
					strRetour += GetStringExpression ( expDeListe, filtre )+",";
				}
				if ( strRetour.Length > 0 )
					strRetour = strRetour.Substring(0, strRetour.Length-1);
				strRetour = "("+strRetour+")";
			}


			else if ( expression is CComposantFiltreOperateur )
			{
				
				COperateurAnalysable operateur = ((CComposantFiltreOperateur)expression).Operateur;
				string strTexteOperateur = operateur.Texte;
				if ( operateur.Niveau > 0 && operateur.Niveau != 4 )
				{
					if ( operateur.Texte.Length > 3 && operateur.Texte.Substring(0,3).ToUpper()=="NOT" )
						strTexteOperateur = operateur.Texte.Substring(0,3)+" "+operateur.Texte.Substring(3);

					strRetour = GetStringExpression ( (IExpression)expression.Parametres[0], filtre )+
						" "+strTexteOperateur+
						" "+GetStringExpression ( (IExpression)expression.Parametres[1], filtre );
				}
				else
				{
					strRetour = strTexteOperateur+"(";
					foreach ( IExpression exp in expression.Parametres )
						strRetour += GetStringExpression ( exp, filtre )+",";
					if ( expression.Parametres.Count > 0 )
						strRetour = strRetour.Substring(0, strRetour.Length-1);
					strRetour +=")";
				}
			}

			return strRetour;

		}
		
	}
}
