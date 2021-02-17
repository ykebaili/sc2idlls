using System;
using System.Collections;

namespace sc2i.win32.common
{
	public class CMot
	{
		private string m_strMot;
		private string m_strMotPourRecherche;
		private string m_strValeurStockee="";

		public CMot ( string strMot, string strValeurStockee )
		{
			m_strMotPourRecherche = CArbreVocabulaire.ConvertToRecherche ( strMot );
			m_strMot = strMot;
			m_strValeurStockee = strValeurStockee;
		}

		public string Mot
		{
			get
			{
				return m_strMot;
			}
		}

		public string MotPourRecherche
		{
			get
			{
				return m_strMotPourRecherche;
			}
		}

		public string ValeurStockee
		{
			get
			{
				return m_strValeurStockee;
			}
		}

		public override int GetHashCode()
		{
			return m_strMotPourRecherche.GetHashCode();
		}

		public override string ToString()
		{
			return m_strMot;
		}

		public override bool Equals(object obj)
		{
			if ( !(obj is CMot ))
				return false;
			return ((CMot)obj).MotPourRecherche.Equals ( MotPourRecherche );
		}


	}

	/// <summary>
	/// Description résumée de CArbreVocabulaire.
	/// </summary>
	public class CArbreVocabulaire
	{
		//Indique que toute la liste est renvoyée en cas de chaine vide 
		private bool m_bTouteLaListeSurChaineVide = false;
		
		
		//Profondeur d'index total
		private int m_nIndexDeep = 3;
		
		//Position de l'arbre dans l'index général
		private int m_nPositionArbre = 0;

		private string m_strIndex = "";
		
		//Index->Sous arbre
		Hashtable m_tableSousArbres = new Hashtable();

		private ArrayList m_listeMots = new ArrayList();

		public CArbreVocabulaire( int nIndexDeep, int nPositionArbre, string strIndex )
		{
			m_nIndexDeep = nIndexDeep;
			m_nPositionArbre = nPositionArbre;
			m_strIndex = strIndex;
		}

		public static string ConvertToRecherche ( string strMot )
		{
			char[] carsToReplace = {'à','â','ä','é','è','ê','ë','î','ï','ô','ö','û','ü','ç'};
			char[] carsRemplacan = {'a','a','a','e','e','e','e','i','i','o','o','u','u','c'};

			for ( int i = 0; i < carsToReplace.Length; i++ )
			{
				strMot = strMot.Replace(carsToReplace[i], carsRemplacan[i]);
				strMot = strMot.Replace(Char.ToUpper(carsToReplace[i]), Char.ToUpper(carsRemplacan[i]) );
			}
			return strMot.ToUpper();
		}

		/// <summary>
		/// Stocke un mot dans le dictionnaire
		/// </summary>
		/// <param name="strMot"></param>
		public void StockeMot ( string strMot, string strValeurStockee )
		{
			StockeMot ( new CMot ( strMot, strValeurStockee ) );
			if ( strValeurStockee != "" )
				StockeMot ( new CMot ( strValeurStockee, strValeurStockee ) );
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="mot"></param>
		private void StockeMot ( CMot mot )
		{
			string strUpper = mot.MotPourRecherche;
			
			if ( m_strIndex != "" &&
				strUpper == m_strIndex )
				if ( !m_listeMots.Contains ( mot ) )
					m_listeMots.Add ( mot );
			
			if ( strUpper.Length > m_nPositionArbre)
			{
				string strIndex = strUpper.Substring(0, m_nPositionArbre+1);
				if ( m_nIndexDeep == 0 && !m_listeMots.Contains ( mot ) )
					m_listeMots.Add ( mot );
				else
				{
					CArbreVocabulaire sousArbre = (CArbreVocabulaire)m_tableSousArbres[strIndex];
					if ( sousArbre == null )
					{
						sousArbre = new CArbreVocabulaire ( m_nIndexDeep-1, m_nPositionArbre+1, strIndex );
						m_tableSousArbres[strIndex] = sousArbre;
					}
					sousArbre.StockeMot ( mot );
				}
			}
		}

		public ArrayList GetTousLesMots()
		{
			ArrayList lst = new ArrayList(m_listeMots);
			foreach ( CArbreVocabulaire sousArbre in m_tableSousArbres.Values )
				lst.AddRange ( sousArbre.GetTousLesMots() );
			return lst;
		}

		//------------------------------------------------
		public CMot GetMot ( string strMot )
		{
			return GetMotProtected ( ConvertToRecherche ( strMot ) );
		}			

		//------------------------------------------------
		protected CMot GetMotProtected ( string strMot )
		{
			if ( strMot.Trim() == "" )
				return null;
			if ( strMot.Length > m_nPositionArbre && m_nIndexDeep > 0 )
			{
				string strIndex = strMot.Substring(0, m_nPositionArbre+1 );
				CArbreVocabulaire sousArbre = (CArbreVocabulaire)m_tableSousArbres[strIndex];
				if ( sousArbre == null )
					return null;
				return sousArbre.GetMot ( strMot);
			}
			
			ArrayList arrToReturn = new ArrayList();
			foreach ( CMot mot in GetTousLesMots() )
				if ( mot.MotPourRecherche ==strMot )
					return mot;
			return null;
		}

		public bool ExisteMot ( string strMot )
		{
			return GetMot (strMot )!= null;
		}
			/*if ( strMot.Trim() == "" )
				return false;
			strMot = ConvertToRecherche ( strMot );
			if ( strMot.Length > m_nPositionArbre && m_nIndexDeep > 0 )
			{
				string strIndex = strMot.Substring(0, m_nPositionArbre+1 );
				CArbreVocabulaire sousArbre = (CArbreVocabulaire)m_tableSousArbres[strIndex];
				if ( sousArbre == null )
					return false;
				return sousArbre.ExisteMot ( strMot);
			}

			ArrayList arrToReturn = new ArrayList();
			foreach ( CMot mot in GetTousLesMots() )
				if ( mot.MotPourRecherche ==strMot )
					return true;
			return false;
		}*/

		//--------------------------------------------
		/// <summary>
		/// Indique que la fonction GetMots retourne tous les mots de la
		/// liste en cas de chaine vide
		/// </summary>
		public bool TouteLaListeSurChaineVide
		{
			get
			{
				return m_bTouteLaListeSurChaineVide;
			}
			set
			{
				m_bTouteLaListeSurChaineVide = value;
			}
		}

		public ArrayList GetMots ( string strDebut )
		{
			if (strDebut.Trim() == "")
			{
				if (m_bTouteLaListeSurChaineVide)
				{
					ArrayList lstMots = new ArrayList();
					foreach (CMot mot in GetTousLesMots())
						lstMots.Add(mot.Mot);
					return lstMots;
				}
				return new ArrayList();
			}
			strDebut = ConvertToRecherche ( strDebut );
			if ( strDebut.Length > m_nPositionArbre && m_nIndexDeep > 0 )
			{
				string strIndex = strDebut.Substring(0, m_nPositionArbre+1 );
				CArbreVocabulaire sousArbre = (CArbreVocabulaire)m_tableSousArbres[strIndex];
				if ( sousArbre == null )
					return new ArrayList();
				return sousArbre.GetMots ( strDebut );
			}

			ArrayList arrToReturn = new ArrayList();
			foreach ( CMot mot in GetTousLesMots() )
				if ( mot.MotPourRecherche.Length >= strDebut.Length &&  mot.MotPourRecherche.Substring(0, strDebut.Length) == strDebut )
					arrToReturn.Add ( mot.Mot );
			arrToReturn.Sort();
			return arrToReturn;
		}
				
	}
}
