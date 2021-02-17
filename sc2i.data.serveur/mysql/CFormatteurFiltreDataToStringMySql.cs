using System;
using System.Collections;
using System.Text.RegularExpressions;

using sc2i.common;
using sc2i.data;
using System.Collections.Generic;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Description résumée de CFormatteurFiltreDataToStringDataTable.
	/// </summary>
	public class CFormatteurFiltreDataToStringMySql : IFormatteurFiltreDataToString
	{
        private const string c_null = "LAVARIABLEVAUTNULL";

		////////////////////////////////////////////////////////
		public CFormatteurFiltreDataToStringMySql()
		{
		}

		private string ToUpper(Match mtch)
		{
			Group p1 = mtch.Groups["P1"];
			Group p2 = mtch.Groups["P2"];
			string strStart = mtch.Value.Substring(0, p1.Index - mtch.Index);
			string strOp = mtch.Value.Substring(p1.Index + p1.Length - mtch.Index, p2.Index - p1.Index - p1.Length);
			string strEnd = mtch.Value.Substring(p2.Index + p2.Length - mtch.Index);
			string strFinal = strStart + "Upper(" + p1.Value + ")" + strOp + "Upper(" + p2.Value + ")" + strEnd;
			return strFinal;

		}

		////////////////////////////////////////////////////////
		public string GetString ( CFiltreData filtre )
		{
			if ( filtre is CFiltreDataAvance )
				return GetStringAvance( (CFiltreDataAvance)filtre);
			string strFiltre = filtre.Filtre;
			for ( int nNumParametre = filtre.Parametres.Count; nNumParametre >= 1 ; nNumParametre-- )
			{
				string strReplace = ":PARAM"+nNumParametre;

				/*if ( strFiltre.IndexOf("@"+nNumParametre.ToString()) >= 0 )
					strFiltre = strFiltre.Replace("@"+nNumParametre.ToString(), strReplace );
				else*/
					strFiltre = strFiltre.Replace("@"+nNumParametre.ToString(), strReplace );
			}
			strFiltre = strFiltre.Replace("§","");

			if (strFiltre.ToUpper().IndexOf("LIKE") > 0)
			{
				Regex r = new Regex("[(]* *(?<P1>[^ (]+) *[)]* +(?<OP>like|not like) +[(]* *(?<P2>[^ (]+) *[)]*", RegexOptions.IgnoreCase);
				strFiltre = r.Replace(strFiltre, new MatchEvaluator(ToUpper));
			}

			return strFiltre;
		}


		////////////////////////////////////////////////////////
		public string GetStringAvance ( CFiltreDataAvance filtre )
		{
			CComposantFiltre composant = filtre.ComposantPrincipal;
			if ( composant == null )
				return "";
            Dictionary<string, object> valeursParametres = new Dictionary<string, object>();
			//remplace chaque champ par sa valeur
			for ( int nParametre = filtre.Parametres.Count; nParametre > 0; nParametre-- )
			{
                if (filtre.Parametres[nParametre - 1] is IList)
                {
                    composant.RemplaceVariableParConstante("@" + nParametre, filtre.Parametres[nParametre - 1]);
                    filtre.Parametres[nParametre - 1] = "";
                    //Renomme les variables suivantes (qui sont déjà renommées en :PARAM)
                    for (int nSuivant = nParametre + 1; nSuivant <= filtre.Parametres.Count; nSuivant++)
                        composant.RenommeVariable(":PARAM" + (nSuivant), ":PARAM" + (nSuivant - 1));
                    filtre.Parametres.RemoveAt(nParametre - 1);
                }
                else
                {
                    composant.RenommeVariable("@" + nParametre, ":PARAM" + (nParametre));
                    if (filtre.Parametres[nParametre - 1] == null)
                        filtre.Parametres[nParametre - 1] = c_null;
                    valeursParametres[":PARAM" + nParametre] = filtre.Parametres[nParametre-1];

                }
			}

            return GetStringExpression(composant, valeursParametres);
		}

		////////////////////////////////////////////////////////
		protected string GetStringExpression ( IExpression expression, Dictionary<string, object> valeursParametres )
		{
			string strRetour = "";

			if ( expression is CComposantFiltreChamp )
			{
				CComposantFiltreChamp champ = (CComposantFiltreChamp)expression;
				if ( champ.Alias != "" )
					strRetour += champ.Alias+".";
				strRetour += champ.NomChamp;
				/*if ( champ.Relations.Length > 0 )
				{
					if (champ.Relations[champ.Relations.Length-1].IsRelationFille)
						strRetour = champ.Relations[champ.Relations.Length-1].TableFille+".";
					else
						strRetour = champ.Relations[champ.Relations.Length-1].TableParente+".";
				}
				strRetour += champ.NomChamp;*/
			}


			else if( expression is CComposantFiltreVariable )
				strRetour = ((CComposantFiltreVariable)expression).GetString();
			
			
			
			else if ( expression is CComposantFiltreConstante )
			{
				CComposantFiltreConstante constante = (CComposantFiltreConstante)expression;
				strRetour = CConvertisseurObjetToSqlServeur.ConvertToSql ( constante.Valeur );
			}



			else if ( expression is CComposantFiltreHasNo )
			{
				strRetour += GetStringExpression ( (IExpression)expression.Parametres[0], valeursParametres );
				strRetour += " is null";
			}

			else if ( expression is CComposantFiltreHas )
			{
				strRetour += GetStringExpression ( (IExpression)expression.Parametres[0], valeursParametres );
				strRetour += " is not null";
			}



			else if ( expression is CComposantFiltreListe )
			{
				CComposantFiltreListe liste = (CComposantFiltreListe)expression;
				strRetour = "";
				foreach ( IExpression expInListe in liste.Liste )
				{
					strRetour += GetStringExpression ( expInListe, valeursParametres )+",";
				}
				if ( strRetour.Length > 0 )
					strRetour = strRetour.Substring(0, strRetour.Length-1);
				strRetour = "("+strRetour+")";
			}



			else if ( expression is CComposantFiltreOperateur )
			{
				COperateurAnalysable operateur = ((CComposantFiltreOperateur)expression).Operateur;
				string strTexteOperateur = operateur.Texte;
				//Remplacement du not par not espace operateur
				bool bToUpper = false;
				if ( operateur.Texte.Length > 3 && operateur.Texte.Substring(0,3).ToUpper()=="NOT" )
					strTexteOperateur = operateur.Texte.Substring(0,3)+" "+operateur.Texte.Substring(3);
                if (operateur.Id == CComposantFiltreOperateur.c_IdOperateurContains)
                    strTexteOperateur = "like";
                if (operateur.Id == CComposantFiltreOperateur.c_IdOperateurWithout)
                    strTexteOperateur = "not like";
				if (operateur.Id == CComposantFiltreOperateur.c_IdOperateurLike ||
					 operateur.Id == CComposantFiltreOperateur.c_IdOperateurNotLike ||
                    operateur.Id == CComposantFiltreOperateur.c_IdOperateurContains ||
                    operateur.Id == CComposantFiltreOperateur.c_IdOperateurWithout)
					bToUpper = true;

                
                if (operateur.Id == CComposantFiltreOperateur.c_IdOperateurEtBinaire)
                {
                    string str1, str2;
                    str1 = GetStringExpression ( (IExpression)expression.Parametres[0], valeursParametres );
                    str2 = GetStringExpression ( (IExpression)expression.Parametres[1],valeursParametres );
                    strRetour = "BitAnd("+str1+","+str2+")";
                }
                else if (operateur.Id == CComposantFiltreOperateur.c_IdOperateurOuBinaire)
                {
                    string str1, str2;
                    str1 = GetStringExpression((IExpression)expression.Parametres[0], valeursParametres);
                    str2 = GetStringExpression((IExpression)expression.Parametres[1], valeursParametres);
                    strRetour = "BitOr(" + str1 + "," + str2 + ")";
                }

                else if ( operateur.Niveau > 0 && operateur.Niveau != 4 )
				{
					string str1,str2,str3;
					str1 = GetStringExpression ( (IExpression)expression.Parametres[0], valeursParametres );
					if (bToUpper)
						str1 = "Upper(" + str1 + ")";
					str2 = strTexteOperateur;
					str3=GetStringExpression ( (IExpression)expression.Parametres[1], valeursParametres );

                    object val = null;

                    if (operateur.Id == CComposantFiltreOperateur.c_IdOperateurEgal &&
                        valeursParametres != null &&
                        valeursParametres.TryGetValue(str3, out val) &&
                        val == c_null)
                    {
                        //Obligé d'ajouter une référence au nom de la variable, sinon, MySql fait la gueule
                        //d'où or str3 is null qui ne sert à rien
                        strRetour = "("+str1 + " is null or "+str3+" is null)";
                    }
                    else if (operateur.Id == CComposantFiltreOperateur.c_IdOperateurDifferent &&
                        valeursParametres != null &&
                        valeursParametres.TryGetValue(str3, out val) &&
                        val == c_null)
                    {
                        //Obligé d'ajouter une référence au nom de la variable, sinon, MySql fait la gueule
                        //d'où or str3 is null qui ne sert à rien
                        strRetour = "(" + str1 + " is not null or " + str3 + " is null)";
                    }
                    else
                    {

                        if (operateur.Id == CComposantFiltreOperateur.c_IdOperateurContains ||
                             operateur.Id == CComposantFiltreOperateur.c_IdOperateurWithout)
                            str3 = "Concat('%',concat(" + str3 + ",'%'))";
                        if (bToUpper)
                            str3 = "Upper(" + str3 + ")";

                        if (operateur.Niveau >= 4)
                        {
                            str1 = "(" + str1 + ")";
                            str3 = "(" + str3 + ")";
                        }
                        strRetour = str1 + " " + str2 + " " + str3;
                    }
				}
				else
				{
					strRetour = strTexteOperateur+"(";
					foreach ( IExpression exp in expression.Parametres )
						strRetour += GetStringExpression ( exp, valeursParametres )+",";
					if ( expression.Parametres.Count > 0 )
						strRetour = strRetour.Substring(0, strRetour.Length-1);
					strRetour +=")";
				}
			}
			return strRetour;

		}
		
	}
}
