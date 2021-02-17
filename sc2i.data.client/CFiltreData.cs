using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Data;

using sc2i.common;
using System.Text;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de CFiltreData.
	/// </summary>
	/*
	La chaine m_strFiltre contient le filtre sous forme de texte
	La liste de paramètre contient la liste des objets paramètres du filtre.
	Chaque paramètre est indiqué dans le filtre en utilisant "@n" où n 
	est les numéro du paramétre.
	Par exemple si strFiltre est NUMERO=@1
	et le premier paramètre est un entier 12,
	le filtre équivaut à NUMERO=12.
	Utiliser les IFormatteurFiltreData pour formatter la chaine 
	dans un format particulier.

	La syntaxe du filtre reprend la syntaxe du filtre DataTable (expression datatable dans l'aide)
	*/
	[Serializable]
    public class CFiltreData : I2iSerializable
	{
		protected string m_strFiltre;
		private ArrayList	m_listeParametres = new ArrayList();
		private string m_strSortOrder="";
		private bool m_bIgnorerVersionDeContexte = false;
		private bool m_bIntegrerLesElementsSupprimes = false;
        private bool m_bIntegrerParentsHierarchiques = false;
        private bool m_bIntegrerFilsHierarchiques = false;
        private bool m_bNeConserverQueLesRacines = false;
		
		//Null ou vide si lecture dans le référentiel, sinon, contient les ids
		//des versions à lire
		private int[] m_listeIdsVersionsALire = null;

		//////////////////////////////////////////////////
        public CFiltreData(  )
		{
			m_strFiltre = "";
		}

		//////////////////////////////////////////////////
		public CFiltreData (string strFiltre, params object[] parametres )
		{
			Init ( strFiltre, parametres );
		}

        //////////////////////////////////////////////////
        /// <summary>
        /// Vérifie que les paramètres ne sont pas nuls
        /// </summary>
        /// <returns></returns>
        public bool AreParametresValides()
        {
            foreach (object parametre in Parametres)
                if (parametre == null)
                    return false;
            return true;
        }


		//////////////////////////////////////////////////
		protected void CopyTo ( CFiltreData filtre )
		{
			filtre.Filtre = m_strFiltre;
			foreach ( object param in Parametres )
				filtre.Parametres.Add ( param );
			filtre.m_strSortOrder = m_strSortOrder;
			filtre.IgnorerVersionDeContexte = IgnorerVersionDeContexte;
			filtre.IntegrerLesElementsSupprimes = IntegrerLesElementsSupprimes;
			filtre.IdsDeVersionsALire = IdsDeVersionsALire;
            filtre.IntergerParentsHierarchiques = IntergerParentsHierarchiques;
            filtre.IntegrerFilsHierarchiques = IntegrerFilsHierarchiques;
            filtre.NeConserverQueLesRacines = NeConserverQueLesRacines;
		}

		//////////////////////////////////////////////////
		/// <summary>
		/// Indique si la lecture via ce filtre gère les version de données
		/// </summary>
		public bool IgnorerVersionDeContexte
		{
			get
			{
				return m_bIgnorerVersionDeContexte;
			}
			set
			{
				m_bIgnorerVersionDeContexte = value;
			}
		}

        //////////////////////////////////////////////////
        public bool IntergerParentsHierarchiques
        {
            get
            {
                return m_bIntegrerParentsHierarchiques;
            }
            set
            {
                m_bIntegrerParentsHierarchiques = value;
            }
        }

        ///////////////////////////////////////////////
        public bool IntegrerFilsHierarchiques
        {
            get
            {
                return m_bIntegrerFilsHierarchiques;
            }
            set
            {
                m_bIntegrerFilsHierarchiques = value;
            }
        }

        ///////////////////////////////////////////////
        public bool NeConserverQueLesRacines
        {
            get
            {
                return m_bNeConserverQueLesRacines;
            }
            set
            {
                m_bNeConserverQueLesRacines = value;
            }
        }

		/// <summary>
		/// si true, les éléments supprimés ne sont pas lus
		/// </summary>
		public bool IntegrerLesElementsSupprimes
		{
			get
			{
				return m_bIntegrerLesElementsSupprimes;
			}
			set
			{
				m_bIntegrerLesElementsSupprimes = value;
			}
		}

		//////////////////////////////////////////////////
		/// <summary>
		/// Retourne la liste des ids de version à lire dans la base
		/// ou null
		/// </summary>
		public int[] IdsDeVersionsALire
		{
			get
			{
				return m_listeIdsVersionsALire;
			}
			set
			{
				m_listeIdsVersionsALire = value;
			}
		}

		//////////////////////////////////////////////////
		/// <summary>
		/// Retourne la liste des ids à lire dans la base sous forme de texte séparés
		/// par des strSeparateur
		/// ou bien null
		/// </summary>
		/// <param name="strSeparateur"></param>
		/// <returns></returns>
		public string GetStringListeIdsVersionsALire(char strSeparateur)
		{
			if (m_listeIdsVersionsALire != null && m_listeIdsVersionsALire.Length > 0)
			{
				StringBuilder bl = new StringBuilder();
				foreach (int nId in m_listeIdsVersionsALire)
				{
					bl.Append(nId);
					bl.Append(strSeparateur);
				}
				bl.Remove(bl.Length - 1, 1);
				return bl.ToString();
			}
			return null;
		}

		//////////////////////////////////////////////////
		public string SortOrder
		{
			get
			{
				return m_strSortOrder;
			}
			set
			{
				m_strSortOrder = value;
			}
		}

		//////////////////////////////////////////////////
		public virtual CFiltreData GetClone()
		{
			CFiltreData filtre = new CFiltreData();
			CopyTo ( filtre );
			return filtre;
		}

		//////////////////////////////////////////////////
		public void Init( string strFiltre, params object[] parametres )
		{
			m_strFiltre = strFiltre;
			if ( parametres != null )
				foreach ( object obj in parametres )
					m_listeParametres.Add ( obj );
		}

		//////////////////////////////////////////////////
		/// <summary>
		/// Crée un filtre and à partir des données d'une DataRow
		/// </summary>
		/// <param name="strChamps">
		/// Liste des champs
		/// </param>
		/// <param name="row">
		/// Ligne contenant les données
		/// </param>
		/// <returns></returns>
		public static CFiltreData CreateFiltreAndSurRow ( string[] strChamps, DataRow row, DataRowVersion version )
		{
			CFiltreData filtre = new CFiltreData ( );
			int nIndex = 1;
			foreach( string strChamp in strChamps )
			{
				filtre.Filtre+=strChamp+"=@"+nIndex+" and ";
				filtre.Parametres.Add ( row[strChamp, version] );
				nIndex++;
			}
			filtre.Filtre = filtre.Filtre.Substring(0, filtre.Filtre.Length-5);
			return filtre;
		}

		//////////////////////////////////////////////////
		public static CFiltreData CreateFiltreAndSurRow ( string[] strChamps, DataRow row )
		{
			return CreateFiltreAndSurRow ( strChamps, row, DataRowVersion.Default );
		}

		//////////////////////////////////////////////////
		public static CFiltreData CreateFiltreAndSurRow ( DataColumn[] cols, DataRow row, DataRowVersion version )
		{
			string[] lstChamps = new string[cols.Length];
			int nIndex = 0;
			foreach ( DataColumn col in cols )
				lstChamps[nIndex++] = col.ColumnName;
			return CreateFiltreAndSurRow ( lstChamps, row, version );
		}

		//////////////////////////////////////////////////
		public static CFiltreData CreateFiltreAndSurRow ( DataColumn[] cols, DataRow row )
		{
			return CreateFiltreAndSurRow ( cols, row, DataRowVersion.Default );
		}
				

		//////////////////////////////////////////////////
		/// <summary>
		/// Crée un filtre and à partir des données d'une DataRow
		/// </summary>
		/// <param name="strChamps">
		/// Liste des champs
		/// </param>
		/// <param name="row">
		/// Ligne contenant les données
		/// </param>
		/// <returns></returns>
		public static CFiltreData CreateFiltreAndSurValeurs ( string[] strChamps, object[] valeurs )
		{
			CFiltreData filtre = new CFiltreData ( );
			int nIndex = 1;
			foreach( string strChamp in strChamps )
			{
				filtre.Filtre+=strChamp+"=@"+nIndex+" and ";
				filtre.Parametres.Add ( valeurs[nIndex-1] );
				nIndex++;
			}
			filtre.Filtre = filtre.Filtre.Substring(0, filtre.Filtre.Length-5);
			return filtre;
		}

		//////////////////////////////////////////////////
		public static CFiltreData CreateFiltreAndSurValeurs ( DataColumn[] cols, object[] valeurs )
		{
			string[] strChamps = new string[cols.Length];
			for ( int n = 0; n < cols.Length; n++ )
				strChamps[n] = cols[n].ColumnName;
			return CreateFiltreAndSurValeurs ( strChamps, valeurs );
		}
		

		//////////////////////////////////////////////////
		public bool HasFiltre
		{
			get
			{
				return m_strFiltre.Trim() != "";
			}
		}

		//////////////////////////////////////////////////
		public virtual string Filtre
		{
			get
			{
				return m_strFiltre;
			}
			set
			{
				m_strFiltre = value;
			}
		}

		//////////////////////////////////////////////////
		public ArrayList Parametres
		{
			get
			{
				return m_listeParametres;
			}
			set
			{
				m_listeParametres = value;
			}
		}



		///////////////////////////////////////////////////
		/// <summary>
		/// Retourne le filtre formatté à la syntaxe SC2I
		/// </summary>
		/// <returns></returns>
		public string Get2iString()
		{
			string strFiltre = Filtre;
			int nNumParametre = 1;
			foreach ( object obj in Parametres )
			{
				string strReplace = obj.ToString();
				strReplace.Replace("\""," ");
				if ( obj is String )
					strReplace = "\""+strReplace+"\"";
				if ( obj is DateTime )
				{
					DateTime dt = (DateTime)obj;
					strReplace = "#"+dt.Year.ToString()+"/"+dt.Month.ToString()+"/"+dt.Day.ToString()+"#";
				}
				if ( obj is bool )
				{
					strReplace = ((bool)obj)?"1":"0";
				}
				strFiltre = strFiltre.Replace("@"+nNumParametre.ToString(), strReplace );
				nNumParametre++;
			}
			return strFiltre;
		}

		/*///////////////////////////////////////////////////
		public void AddAndFiltre ( CFiltreData filtre )
		{
			if ( filtre == null || !filtre.HasFiltre )
				return;
			if ( m_strFiltre.Trim() != "" )
				m_strFiltre = "("+m_strFiltre+") and ";
			string strNewFiltre = filtre.Filtre+" ";
			int nNumNewParam = Parametres.Count +1;
			for ( int nParam = 0; nParam < filtre.Parametres.Count; nParam++ )
			{
				strNewFiltre = strNewFiltre.Replace("@"+(nParam+1).ToString()+" ", "@"+nNumNewParam.ToString()+" ");
				Parametres.Add ( filtre.Parametres[nParam] );
				nNumNewParam++;
			}
			m_strFiltre += strNewFiltre;
		}*/

		///////////////////////////////////////////////////
		protected static CFiltreData CombineFiltres(CFiltreData filtre1, CFiltreData filtre2, string strOperateur)
		{
			if ( (filtre1 == null || !filtre1.HasFiltre )&& (filtre2 == null || !filtre2.HasFiltre) )
				return null;
			if ( filtre1 == null || !filtre1.HasFiltre)
				return filtre2.GetClone();
			if ( filtre2 == null || !filtre2.HasFiltre)
				return filtre1.GetClone();
			if ( filtre1 is CFiltreDataImpossible || filtre2 is CFiltreDataImpossible )
				return new CFiltreDataImpossible();
			CFiltreData filtreResult = null;
			if ( filtre1 is CFiltreDataAvance || filtre2 is CFiltreDataAvance )
			{

				string strTable;
				if ( filtre1 is CFiltreDataAvance )
				{
					strTable = ((CFiltreDataAvance)filtre1).TablePrincipale;
					if ( filtre2 is CFiltreDataAvance && 
						((CFiltreDataAvance)filtre2).TablePrincipale != strTable )
						throw new Exception(I.T("Cannot combine @1 filter on different tables|133",strOperateur));
				}
				else
					strTable = ((CFiltreDataAvance)filtre2).TablePrincipale;

				
				filtreResult = new CFiltreDataAvance ( strTable, "");


				if (!(filtre1 is CFiltreDataAvance))
					filtre1 = CFiltreDataAvance.ConvertFiltreToFiltreAvance(strTable, filtre1);
				if (!(filtre2 is CFiltreDataAvance))
					filtre2 = CFiltreDataAvance.ConvertFiltreToFiltreAvance(strTable, filtre2);


                CResultAErreur result = CResultAErreur.True;
                CFiltreDataAvance filtre1Avance = filtre1 as CFiltreDataAvance;
                CComposantFiltre composant1 = filtre1Avance.ComposantPrincipal;
                if (composant1 == null)
                {
                    result = CAnalyseurSyntaxiqueFiltre.AnalyseFormule(filtre1.Filtre, strTable);
                    if (!result)
                    {
                        result.EmpileErreur(I.T("Error while analyzing filter @1|134", filtre1.Filtre));
                        throw new CExceptionErreur(result.Erreur);
                    }
                    composant1 = result.Data as CComposantFiltre;
                }
			
				CFiltreDataAvance copie = filtre2.GetClone() as CFiltreDataAvance;
				copie.RenumerotteParameters ( filtre1.Parametres.Count+1 );

                CComposantFiltre composant2 = copie.ComposantPrincipal;
                if (composant2 == null)
                {
                    result = CAnalyseurSyntaxiqueFiltre.AnalyseFormule(copie.Filtre, strTable);
                    if (!result)
                    {
                        result.EmpileErreur(I.T("Error while analyzing filter @1|134", copie.Filtre));
                        throw new CExceptionErreur(result.Erreur);
                    }
                    composant2 = result.Data as CComposantFiltre;
                }
                CComposantFiltre composantPrincipal = null;
                if (strOperateur.ToUpper() == "OR")
                    composantPrincipal = new CComposantFiltreOperateur(CComposantFiltreOperateur.c_IdOperateurOu);
                if (strOperateur.ToUpper() == "AND")
                    composantPrincipal = new CComposantFiltreOperateur(CComposantFiltreOperateur.c_IdOperateurEt);
                if (composantPrincipal != null)
                {
                    composantPrincipal.Parametres.Add(composant1);
                    composantPrincipal.Parametres.Add(composant2);
                    filtreResult = new CFiltreDataAvance(strTable, composantPrincipal);
                }
                else
                    filtreResult.Filtre = "(" + composant1.GetString() + ") " + strOperateur + " (" +
                        composant2.GetString() + ")";				
			}
			else
			{
				filtreResult = new CFiltreData();
				filtreResult.Filtre = "("+filtre1.Filtre+")";
				CFiltreData copie = filtre2.GetClone();
				copie.RenumerotteParameters ( filtre1.Parametres.Count+1 );
				filtreResult.Filtre += " "+strOperateur+" ("+copie.Filtre+")";
			}
			foreach ( object parametre in filtre1.Parametres )
				filtreResult.Parametres.Add ( parametre );
			foreach ( object parametre in filtre2.Parametres )
				filtreResult.Parametres.Add ( parametre );
			

			if ( filtre1 != null && filtre1.SortOrder != "" )
				filtreResult.SortOrder = filtre1.SortOrder;
			if ( filtre2 != null && filtre2.SortOrder != "" )
			{
				if ( filtreResult.SortOrder != "" )
					filtreResult.SortOrder += ",";
				filtreResult.SortOrder += filtre2.SortOrder;
			}

			if (filtre1 != null)
			{
				filtreResult.IntegrerLesElementsSupprimes |= filtre1.IntegrerLesElementsSupprimes;
				filtreResult.IgnorerVersionDeContexte |= filtre1.IgnorerVersionDeContexte;
				filtreResult.IdsDeVersionsALire = filtre1.IdsDeVersionsALire;
                filtreResult.IntergerParentsHierarchiques |= filtre1.IntergerParentsHierarchiques;
                filtreResult.IntegrerFilsHierarchiques |= filtre1.IntegrerFilsHierarchiques;
                filtreResult.NeConserverQueLesRacines |= filtre1.NeConserverQueLesRacines;
			}

			if (filtre2 != null)
			{
				filtreResult.IntegrerLesElementsSupprimes |= filtre2.IntegrerLesElementsSupprimes;
				filtreResult.IgnorerVersionDeContexte |= filtre2.IgnorerVersionDeContexte;
                filtreResult.IntergerParentsHierarchiques |= filtre2.IntergerParentsHierarchiques;
                filtreResult.IntegrerFilsHierarchiques |= filtre2.IntegrerFilsHierarchiques;
                filtreResult.NeConserverQueLesRacines |= filtre2.NeConserverQueLesRacines;
				if (filtre2.IdsDeVersionsALire != null)
				{
					if (filtreResult.IdsDeVersionsALire != null)
					{
						Hashtable tblIds = new Hashtable();
						foreach (int nId in filtreResult.IdsDeVersionsALire)
							tblIds[nId] = true;
						foreach (int nId in filtre2.IdsDeVersionsALire)
							tblIds[nId] = true;
						ArrayList lst = new ArrayList();
						foreach (int nId in tblIds.Keys)
							lst.Add(nId);
						filtreResult.IdsDeVersionsALire = (int[])lst.ToArray(typeof(int));
					}
					else
						filtreResult.IdsDeVersionsALire = filtre2.IdsDeVersionsALire;
				}
			}

			return filtreResult;
		}


		///////////////////////////////////////////////////
		public static CFiltreData GetAndFiltre(CFiltreData filtre1, CFiltreData filtre2)
		{
			return CombineFiltres(filtre1, filtre2, "AND");
		}

		///////////////////////////////////////////////////
		public static CFiltreData GetOrFiltre(CFiltreData filtre1, CFiltreData filtre2)
		{
			return CombineFiltres(filtre1, filtre2, "OR");
		}

			

		//////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 5;
			//Version 2 : Ajout de "IgnorerVersion"
			//Version 3 : Ignorer les supprimés
			//Version 4 : Ajout de la liste des ids de versions à lire
            // Version 5 : Ajout du traitement des listes d'objets simples
		}

		//////////////////////////////////////////////////
		///Modifie les numéros de parametres. Le 0 devient le nNumDebut, 
		///le 1 devient nNumDebut+1
		///...
		public virtual void RenumerotteParameters ( int nNumDebut )
		{
			string strNewFiltre = Filtre+" ";
			nNumDebut += Parametres.Count-1;
			for ( int nParam = Parametres.Count-1; nParam >=0 ; nParam-- )
			{
				string strOldParam = "@"+(nParam+1).ToString();
				string strNewParam = "@"+nNumDebut.ToString();
				strNewFiltre = Regex.Replace (strNewFiltre+" ", "("+strOldParam+")(?<suite>[^0-9]+?)", strNewParam+"${suite}");
                strNewFiltre = strNewFiltre.Trim();
				nNumDebut--;
			}
			Filtre = strNewFiltre;
		}

		//////////////////////////////////////////////////
		public virtual CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			serializer.TraiteString ( ref m_strFiltre );
			int nNbParametres = m_listeParametres.Count;
			serializer.TraiteInt ( ref nNbParametres );
			switch ( serializer.Mode )
			{
				case ModeSerialisation.Ecriture :
					foreach ( object obj in m_listeParametres )
					{
                        bool bIsArray = obj.GetType().IsArray;
                        serializer.TraiteBool(ref bIsArray);
                        if (bIsArray)
                        {
                            // Le paramètre est une liste, donc on le cast en Collection, et on écrit une liste d'objets simples
                            ICollection listObj = obj as ICollection;
                            IList lstObjectsSimples = new ArrayList();
                            if (listObj != null)
                                ((ArrayList)lstObjectsSimples).AddRange(listObj);
                            result = serializer.TraiteListeObjetsSimples(ref lstObjectsSimples);
                        }
                        else
                        {
                            object copie = obj;
                            result = serializer.TraiteObjetSimple(ref copie);
                        }
						if ( !result )
							return result;
					}
					break;
				case ModeSerialisation.Lecture :
					m_listeParametres.Clear();
					for ( int nParametre = 0; nParametre < nNbParametres; nParametre++ )
					{
						object obj = null;
                        bool bIsArray = false;
                        if (nVersion >= 5)
                            serializer.TraiteBool(ref bIsArray);
                        if (bIsArray)
                        {
                            // Le paramètre est une liste de valeurs, donc on lit une liste d'objets
                            IList lstObjectsSimples = new ArrayList();
                            result = serializer.TraiteListeObjetsSimples(ref lstObjectsSimples);
                            obj = ((ArrayList)lstObjectsSimples).ToArray();

                        }
                        else
                        {
                            result = serializer.TraiteObjetSimple(ref obj);
                        }
						if ( !result )
							return result;
						m_listeParametres.Add ( obj );
					}
					break;
			}
			if ( nVersion >= 1 )
				serializer.TraiteString(ref m_strSortOrder );
			if (nVersion >= 2)
				serializer.TraiteBool(ref m_bIgnorerVersionDeContexte);
			if (nVersion >= 3)
				serializer.TraiteBool(ref m_bIntegrerLesElementsSupprimes);
			else
				m_bIntegrerLesElementsSupprimes = false;
			if (nVersion >= 4)
			{
				IList lstIds = new ArrayList();
				if (m_listeIdsVersionsALire != null)
					((ArrayList)lstIds).AddRange(m_listeIdsVersionsALire);
				result = serializer.TraiteListeObjetsSimples(ref lstIds);
				if (serializer.Mode == ModeSerialisation.Lecture)
				{
					ArrayList lst = new ArrayList(lstIds);
					m_listeIdsVersionsALire = (int[])lst.ToArray(typeof(int));
					if (m_listeIdsVersionsALire.Length == 0)
						m_listeIdsVersionsALire = null;
				}
			}
			return result;
		}
		

	}
}

