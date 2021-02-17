using System;
using System.Collections;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de CElementRechercheIntuitive.
	/// </summary>
	public abstract class CElementRechercheIntuitive
	{
		private ArrayList m_parametres = new ArrayList(); 

		//Mot qui a été utilisé pour utiliser cet élément dans l'analyse syntaxique (par exemple, et, ou, pas, ni, ...)
		private string m_strMotUtilise = "";
		
		
		public CElementRechercheIntuitive()
		{
			
		}

		public CElementRechercheIntuitive[] Parametres
		{
			get
			{
				return (CElementRechercheIntuitive[])m_parametres.ToArray(typeof(CElementRechercheIntuitive) );
			}
		}

		public ArrayList ListeParametres
		{
			get
			{
				return m_parametres;
			}
		}

		public string MotUtilise
		{
			get
			{
				return m_strMotUtilise;
			}
			set
			{
				m_strMotUtilise = value;
			}
		}

		public abstract string GetString();

		/// <summary>
		/// Mots qui peuvent définir cet élément
		/// </summary>
		public abstract CMotRecherche[] MotsPossibles{get;}

		public static string GetStringSysteme ( CElementRechercheIntuitive elt )
		{
			string strTexte = elt.MotUtilise;
			if ( elt.Parametres.Length != 0 )
			{
				strTexte += "(";
				foreach ( CElementRechercheIntuitive par in elt.ListeParametres )
				{
					strTexte += GetStringSysteme ( par )+";";
				}
				strTexte = strTexte.Substring(0, strTexte.Length-1 )+")";
			}
			return strTexte;
		}

		public abstract CComposantFiltre GetComposantFiltre( CComposantFiltreChamp champ );
	}
}
