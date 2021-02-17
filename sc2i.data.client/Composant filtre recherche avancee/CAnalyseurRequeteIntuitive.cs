using System;
using System.Collections;
using System.Text.RegularExpressions;

using sc2i.common;


namespace sc2i.data
{
	///Niveau 1 : opérateur unaires
	///Niveau 0 : chaines de caractères
	/// <summary>
	/// Description résumée de CAnalyseurRequeteIntuitive.
	/// </summary>
	public class CAnalyseurRequeteIntuitive
	{
		//Niveau->ArrayList de string
		private Hashtable m_tableNiveauToMots = new Hashtable();

		//Mot->Type element de recherche
		private Hashtable m_tableMotsToTypeElement = new Hashtable();
		
		
		//-----------------------------------------------------------------
		public CAnalyseurRequeteIntuitive()
		{
			foreach ( Type tp in CAllocateurElementRecherche.TypesElements )
			{
				CElementRechercheIntuitive elt = (CElementRechercheIntuitive)Activator.CreateInstance ( tp );
				ArrayList lst = null;
		
				
				foreach ( CMotRecherche mot in elt.MotsPossibles )
				{
					lst = (ArrayList)m_tableNiveauToMots[mot.Niveau];
					if ( lst == null )
					{
						lst = new ArrayList();
						m_tableNiveauToMots[mot.Niveau] = lst;
					}
					lst.Add ( mot.Mot );
					m_tableMotsToTypeElement[mot.Mot.ToLower()] = tp;
				}
			}
		}

		//-----------------------------------------------------------------
		public CResultAErreur AnalyseChaine ( string strChaine )
		{
			return GetElementNiveau ( strChaine, 10 );
		}

		//-----------------------------------------------------------------
		public CResultAErreur GetElementNiveau ( string strChaine, int nNiveau )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( nNiveau == 1 )
				return GetElement1( strChaine );
			ArrayList lst = (ArrayList)m_tableNiveauToMots[nNiveau];
			if ( lst != null )
			{
				string strRegEx = "";
				foreach ( string strMot in lst )
					strRegEx += "("+strMot+")"+"|";
				strRegEx = strRegEx.Substring(0, strRegEx.Length-1);
				Regex ex = new Regex ( strRegEx, RegexOptions.IgnoreCase );
				MatchCollection matches = ex.Matches ( strChaine );
				if ( matches.Count > 0 )
				{
					Match mt = matches[matches.Count-1];
					string strVal = mt.Value.ToLower();
					Type tp = (Type)m_tableMotsToTypeElement[strVal];
					if ( tp != null )
					{
						//On a trouvé. Récupère le texte avant et après
						int nPos = strChaine.LastIndexOf ( mt.Value );
						string strGauche, strDroite;
						strGauche = strChaine.Substring ( 0, nPos ).Trim();
						strDroite = strChaine.Substring ( nPos + mt.Value.Length ).Trim();
						result = GetElementNiveau ( strGauche, nNiveau );
						if ( result )
						{
							CElementRechercheIntuitive eltGauche = (CElementRechercheIntuitive)result.Data;
							result = GetElementNiveau ( strDroite, nNiveau );
							if ( result )
							{
								CElementRechercheIntuitive eltDroite = (CElementRechercheIntuitive)result.Data;
								CElementRechercheIntuitive newElt = (CElementRechercheIntuitive)Activator.CreateInstance ( tp );
								newElt.MotUtilise = mt.Value;
								if ( eltGauche != null )
									newElt.ListeParametres.Add ( eltGauche );
								if ( eltDroite != null )
									newElt.ListeParametres.Add ( eltDroite );
								result.Data = newElt;
								return result;
							}
						}
					}
				}
			}
			return GetElementNiveau ( strChaine, nNiveau-1 );
		}

		//-----------------------------------------------------------------
		public CResultAErreur GetElement1 ( string strChaine )
		{
			CResultAErreur result = CResultAErreur.True;
			
			//Recherche un opérateur de niveau 1
			ArrayList lst = (ArrayList)m_tableNiveauToMots[1];
			if (lst != null )
			{
				foreach ( string strMot in lst )
				{
					if ( strChaine.Length >= strMot.Length && 
						strChaine.Substring(0, strMot.Length ) == strMot )
					{
						Type tp = (Type)m_tableMotsToTypeElement[strMot];
						if ( tp != null )
						{
							CElementRechercheIntuitive elt = (CElementRechercheIntuitive)Activator.CreateInstance ( tp );
							elt.MotUtilise = strChaine.Substring(0, strMot.Length );
							string strChaineDroite = strChaine.Substring ( strMot.Length ).Trim();
							result = GetElement0 ( strChaineDroite );
							if ( result )
							{
								if ( result.Data != null )
									elt.ListeParametres.Add ( result.Data );
								result.Data = elt;
								return result;
							}
						}
					}
				}
			}
			return GetElement0 ( strChaine );
		}

		//-----------------------------------------------------------------
		public CResultAErreur GetElement0 ( string strChaine )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( strChaine.Trim() == "" )
				return result;

			CElementRechercheTexte elementGauche = null;

			int nPos = 0;
			if ( strChaine.Length >= 1 && strChaine.Substring ( 0,1) == "\"") //ahaha c'est une "
			{
				strChaine = strChaine.Substring(1);
				//Cherche la " fermante qui suit : 
				nPos = strChaine.IndexOf('"' );
				if ( nPos < 0 )
					nPos = strChaine.Length;
			}
			else
			{
				//Cherche le mot
				nPos = strChaine.IndexOf(' ');
				if ( nPos < 0 )
					nPos = strChaine.Length;
			}
			string strText = strChaine.Substring ( 0, nPos );
			strText = strText.Trim();
			if ( strText.Length > 0 )
			{
				elementGauche = new CElementRechercheTexte ( );
				elementGauche.MotUtilise = strText;
			}
			if ( nPos+1 < strChaine.Length )
				strChaine = strChaine.Substring ( nPos+1);
			else
				strChaine = "";
			strChaine = strChaine.Trim();
			if ( strChaine.Length > 0 )
			{
				//Il reste de trucs, on applique donc un Et entre ces trucs
				result = GetElement1 ( strChaine );
				if ( result )
				{
					CElementRechercheEt et = new CElementRechercheEt ( );
					if ( elementGauche != null )
						et.ListeParametres.Add ( elementGauche );
					if ( result.Data != null )
						et.ListeParametres.Add ( result.Data );
					result.Data = et;
					return result;
				}
			}
			result.Data = elementGauche;
			return result;
		}
	
	}
}
