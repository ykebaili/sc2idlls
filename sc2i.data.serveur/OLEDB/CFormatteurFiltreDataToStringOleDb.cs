using System;
using System.Collections;

using sc2i.common;
using sc2i.data;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Description résumée de CFormatteurFiltreDataToStringDataTable.
	/// </summary>
	public class CFormatteurFiltreDataToStringOleDb : IFormatteurFiltreDataToString
	{
		////////////////////////////////////////////////////////
		public CFormatteurFiltreDataToStringOleDb()
		{
		}

        ////////////////////////////////////////////////////////
        private static void ConvertFiltreForOleDb(CFiltreData filtre)
        {
            //Les paramètres doivent être dans l'ordre
            ArrayList lst = new ArrayList(filtre.Parametres);
            ArrayList newParams = new ArrayList();
            string strFiltre = filtre.Filtre;
            int nPos = strFiltre.IndexOf('@');
            int nParametre = 1;
            while (nPos >= 0)
            {
                int nStartParam = nPos + 1;
                int nEndParam = nPos + 1;
                while (nEndParam < strFiltre.Length && "0123456789".IndexOf(strFiltre[nEndParam]) >= 0)
                    nEndParam++;
                if (nEndParam > nStartParam)
                {
                    int nParam = Int32.Parse(strFiltre.Substring(nStartParam, nEndParam - nStartParam));
                    newParams.Add(lst[nParam - 1]);
                    string strNew = strFiltre.Substring(0, nStartParam);
                    strNew += nParametre;
                    strNew += strFiltre.Substring(nEndParam);
                    strFiltre = strNew;
                    nParametre++;
                }
                nPos = strFiltre.IndexOf('@', nStartParam + 1);
            }
            filtre.Parametres.Clear();
            filtre.Filtre = strFiltre;
            foreach (object obj in newParams)
                filtre.Parametres.Add(obj);
        }



		////////////////////////////////////////////////////////
		public string GetString ( CFiltreData filtre )
		{
            ConvertFiltreForOleDb(filtre);
			if ( filtre is CFiltreDataAvance )
				return GetStringAvance( (CFiltreDataAvance)filtre);
			string strFiltre = filtre.Filtre;
			for ( int nNumParametre = 1; nNumParametre <= filtre.Parametres.Count; nNumParametre++ )
			{
				string strReplace = "@PARAM"+nNumParametre;
					strFiltre = strFiltre.Replace("@"+nNumParametre.ToString(), strReplace );
			}
			strFiltre = strFiltre.Replace("§","");
			return strFiltre;
		}


		////////////////////////////////////////////////////////
		public string GetStringAvance ( CFiltreDataAvance filtre )
		{
			CComposantFiltre composant = filtre.ComposantPrincipal;
			if ( composant == null )
				return "";
			//remplace chaque champ par sa valeur
			for ( int nParametre = filtre.Parametres.Count; nParametre > 0; nParametre-- )
			{
				if ( filtre.Parametres[nParametre-1] is IList )
				{
					composant.RemplaceVariableParConstante ( "@"+nParametre,filtre.Parametres[nParametre-1]);
					filtre.Parametres[nParametre-1] = "";
				}
				composant.RenommeVariable("@"+nParametre, "@PARAM"+nParametre);
			}

			return GetStringExpression ( composant, filtre );
		}

		////////////////////////////////////////////////////////
		public string GetStringExpression ( IExpression expression, CFiltreData filtre )
		{
			string strRetour = "";
			if ( expression is CComposantFiltre )
				expression = ((CComposantFiltre)expression).GetComposantFiltreFinal( filtre);
			
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
				strRetour += GetStringExpression ( (IExpression)expression.Parametres[0], filtre );
				strRetour += " is null";
			}

			else if ( expression is CComposantFiltreHas )
			{
				strRetour += GetStringExpression ( (IExpression)expression.Parametres[0], filtre );
				strRetour += " is not null";
			}



			else if ( expression is CComposantFiltreListe )
			{
				CComposantFiltreListe liste = (CComposantFiltreListe)expression;
				strRetour = "";
				foreach ( IExpression expInListe in liste.Liste )
				{
					strRetour += GetStringExpression ( expInListe, filtre )+",";
				}
				if ( strRetour.Length > 0 )
					strRetour = strRetour.Substring(0, strRetour.Length-1);
				strRetour = "("+strRetour+")";
			}



            else if (expression is CComposantFiltreOperateur)
            {
                COperateurAnalysable operateur = ((CComposantFiltreOperateur)expression).Operateur;
                string strTexteOperateur = operateur.Texte;
                if (operateur.Texte.Length > 3 && operateur.Texte.Substring(0, 3).ToUpper() == "NOT")
                    strTexteOperateur = operateur.Texte.Substring(0, 3) + " " + operateur.Texte.Substring(3);
                if (operateur.Id == CComposantFiltreOperateur.c_IdOperateurContains)
                    strTexteOperateur = "like";
                if (operateur.Id == CComposantFiltreOperateur.c_IdOperateurWithout)
                    strTexteOperateur = "not like";
                if (operateur.Niveau > 0 && operateur.Niveau != 4)
                {
                    string str1, str2, str3;
                    str1 = GetStringExpression((IExpression)expression.Parametres[0], filtre);
                    str2 = strTexteOperateur;
                    str3 = GetStringExpression((IExpression)expression.Parametres[1], filtre);
                    string[] valeurs = new string[] { str3 };
                    if (operateur.Id == CComposantFiltreOperateur.c_IdOperateurContains ||
                         operateur.Id == CComposantFiltreOperateur.c_IdOperateurWithout)
                        str3 = "'%'+" + str3 + "+'%'";
                    if (operateur.Niveau >= 4)
                    {
                        str1 = "(" + str1 + ")";
                        str3 = "(" + str3 + ")";
                    }
                    bool bChercheVide = false;
                    if (strTexteOperateur.ToUpper() == "LIKE")
                    {
                        bChercheVide = str3 == "%%";
                        if (!bChercheVide && str3.Contains("@"))
                        {
                            try
                            {
                                int nParam = Int32.Parse(str3.Substring("@PARAM".Length));
                                if (filtre.Parametres[nParam - 1].ToString() == "%%")
                                    bChercheVide = true;
                            }
                            catch
                            {
                            }
                        }
                    }

                    if (bChercheVide)
                        strRetour += str3 + "=" + str3;
                    else
                    {
                        strRetour = str1 + " " + str2 + " " + str3;

                       /*if (operateur.Id == CComposantFiltreOperateur.c_IdOperateurLike ||
                            operateur.Id == CComposantFiltreOperateur.c_IdOperateurNotLike)
                            strRetour += " ESCAPE '\\'";*/
                    }
                }
                else
                {
                    strRetour = strTexteOperateur + "(";
                    foreach (IExpression exp in expression.Parametres)
                        strRetour += GetStringExpression(exp, filtre) + ",";
                    if (expression.Parametres.Count > 0)
                        strRetour = strRetour.Substring(0, strRetour.Length - 1);
                    strRetour += ")";
                }
            }
			return strRetour;

		}
		
	}
}
