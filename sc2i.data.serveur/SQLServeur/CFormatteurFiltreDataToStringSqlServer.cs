using System;
using System.Collections;

using sc2i.common;
using sc2i.data;
using System.Collections.Generic;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Description résumée de CFormatteurFiltreDataToStringDataTable.
	/// </summary>
	public class CFormatteurFiltreDataToStringSqlServer : IFormatteurFiltreDataToString
	{
        private CSqlDatabaseConnexion m_connexion = null;
        
        ////////////////////////////////////////////////////////
		public CFormatteurFiltreDataToStringSqlServer(CSqlDatabaseConnexion connexion)
		{
            m_connexion = connexion;
		}

		////////////////////////////////////////////////////////
		public string GetString ( CFiltreData filtre )
		{
			if ( filtre is CFiltreDataAvance )
				return GetStringAvance( (CFiltreDataAvance)filtre);
			string strFiltre = filtre.Filtre;
			for ( int nNumParametre = filtre.Parametres.Count; nNumParametre >=1; nNumParametre-- )
			{
				string strReplace = "@PARAM"+nNumParametre;
				/*if ( strFiltre.IndexOf("@"+nNumParametre.ToString()) >= 0 )
					strFiltre = strFiltre.Replace("@"+nNumParametre.ToString(), strReplace );
				else*/
					strFiltre = strFiltre.Replace("@"+nNumParametre.ToString(), strReplace );
			}
			strFiltre = strFiltre.Replace("§","");
			return strFiltre;
		}


		////////////////////////////////////////////////////////
		public string GetStringAvance ( CFiltreDataAvance filtre )
		{
			CComposantFiltre composant = filtre.ComposantPrincipal;
			if (composant == null)
				return "";
			//remplace chaque champ par sa valeur
			for (int nParametre = filtre.Parametres.Count; nParametre > 0; nParametre--)
			{
				if (filtre.Parametres[nParametre - 1] is IList)
				{
					composant.RemplaceVariableParConstante("@" + nParametre, filtre.Parametres[nParametre - 1]);
					filtre.Parametres[nParametre - 1] = "";
					//Renomme les variables suivantes (qui sont déjà renommées en @PARAM)
					for (int nSuivant = nParametre + 1; nSuivant <= filtre.Parametres.Count; nSuivant++)
						composant.RenommeVariable("@PARAM" + (nSuivant), "@PARAM" + (nSuivant - 1));
					filtre.Parametres.RemoveAt(nParametre - 1);
				}
				else
					composant.RenommeVariable("@" + nParametre, "@PARAM" + (nParametre));
			}

			return GetStringExpression ( composant, filtre);
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

            else if (expression is CComposantFiltreSousFiltre && m_connexion != null)
            {
                CComposantFiltreSousFiltre compo = (CComposantFiltreSousFiltre)expression;
                CResultAErreur result = compo.InterpreteParametres();
                if (result)
                {
                    string strWhere = "";
                    string strJoin = "";
                    string strPrefixFrom = "";
                    bool bDistinct = false;
                    compo.Filtre.Parametres.Clear();
                    compo.Filtre.AddChampAAjouterAArbreTable(compo.ChampSousFiltre);
                    foreach (object parametre in filtre.Parametres)
                        compo.Filtre.Parametres.Add(parametre);
                    CComposantFiltreOperateur compoEt = new CComposantFiltreOperateur(CComposantFiltreOperateur.c_IdOperateurEt);
                    compoEt.Parametres.Add(compo.Filtre.ComposantPrincipal);
                    CComposantFiltreHas compoNotNull = new CComposantFiltreHas();
                    compoNotNull.Parametres.Add(compo.ChampSousFiltre);
                    compoEt.Parametres.Add(compoNotNull);
                    compo.Filtre.ComposantPrincipal = compoEt;
                    IEnumerable<C2iDbDatabaseConnexion.CParametreRequeteDatabase> parametres = null;
                    result = m_connexion.PrepareRequeteFromFiltre(
                        compo.Filtre,
                        ref strWhere,
                        ref strJoin,
                        ref bDistinct,
                        ref strPrefixFrom,
                        ref parametres);
                    if (result)
                    {

                        strRetour += " ";
                        if (compo.ChampTeste.Alias != "")
                            strRetour += compo.ChampTeste.Alias + ".";
                        strRetour += compo.ChampTeste.NomChamp;
                        
                        if (compo is CComposantFiltreInSousFiltre)
                            strRetour += " in (";
                        else
                            strRetour += " not in (";
                        string strSelect = "select ";
                        if (compo.ChampSousFiltre.Alias != "")
                            strSelect += compo.ChampSousFiltre.Alias + ".";
                        strSelect += compo.ChampSousFiltre.NomChamp;
                        strSelect += " from " + CContexteDonnee.GetNomTableInDbForNomTable(compo.TableSousFiltre);
                        strRetour += m_connexion.GetSql(strSelect, strPrefixFrom, strJoin, strWhere);
                        strRetour += ")";
                    }
                }
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

                        if (operateur.Id == CComposantFiltreOperateur.c_IdOperateurLike ||
                            operateur.Id == CComposantFiltreOperateur.c_IdOperateurNotLike)
                            strRetour += " ESCAPE '\\'";
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
