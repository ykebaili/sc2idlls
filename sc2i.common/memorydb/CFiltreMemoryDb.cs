using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Data;

using sc2i.common;
using System.Text;

namespace sc2i.common.memorydb
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
    public class CFiltreMemoryDb : I2iSerializable
	{
		protected string m_strFiltre;
		private ArrayList	m_listeParametres = new ArrayList();

		//////////////////////////////////////////////////
        public CFiltreMemoryDb(  )
		{
			m_strFiltre = "";
		}

		//////////////////////////////////////////////////
		public CFiltreMemoryDb (string strFiltre, params object[] parametres )
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
		protected void CopyTo ( CFiltreMemoryDb filtre )
		{
			filtre.Filtre = m_strFiltre;
			foreach ( object param in Parametres )
				filtre.Parametres.Add ( param );
		}

		
		//////////////////////////////////////////////////
		public virtual CFiltreMemoryDb GetClone()
		{
			CFiltreMemoryDb filtre = new CFiltreMemoryDb();
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
		public virtual bool HasFiltre
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
        protected static CFiltreMemoryDb CombineFiltres(CFiltreMemoryDb filtre1, CFiltreMemoryDb filtre2, string strOperateur)
        {
            if ((filtre1 == null || !filtre1.HasFiltre) && (filtre2 == null || !filtre2.HasFiltre))
                return null;
            if (filtre1 == null || !filtre1.HasFiltre)
                return filtre2.GetClone();
            if (filtre2 == null || !filtre2.HasFiltre)
                return filtre1.GetClone();
            if (filtre1 is CFiltreMemoryDBImpossible || filtre2 is CFiltreMemoryDBImpossible)
                return new CFiltreMemoryDBImpossible();
            CFiltreMemoryDb filtreResult = null;

            if (filtre1 is CFiltreMemoryDbAvance)
                filtreResult = new CFiltreMemoryDbAvance ( ((CFiltreMemoryDbAvance) filtre1).TypePrincipal);
            else if ( filtre2 is CFiltreMemoryDbAvance)
                filtreResult = new CFiltreMemoryDbAvance ( ((CFiltreMemoryDbAvance)filtre2).TypePrincipal);
            else
                filtreResult = new CFiltreMemoryDb();
            filtreResult.Filtre = "(" + filtre1.Filtre + ")";
            CFiltreMemoryDb copie = filtre2.GetClone();
            copie.RenumerotteParameters(filtre1.Parametres.Count + 1);
            filtreResult.Filtre += " " + strOperateur + " (" + copie.Filtre + ")";
            foreach (object parametre in filtre1.Parametres)
                filtreResult.Parametres.Add(parametre);
            foreach (object parametre in filtre2.Parametres)
                filtreResult.Parametres.Add(parametre);

            return filtreResult;
        }


		///////////////////////////////////////////////////
		public static CFiltreMemoryDb GetAndFiltre(CFiltreMemoryDb filtre1, CFiltreMemoryDb filtre2)
		{
			return CombineFiltres(filtre1, filtre2, "AND");
		}

		///////////////////////////////////////////////////
		public static CFiltreMemoryDb GetOrFiltre(CFiltreMemoryDb filtre1, CFiltreMemoryDb filtre2)
		{
			return CombineFiltres(filtre1, filtre2, "OR");
		}

			

		//////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
			
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
				strNewFiltre = Regex.Replace (strNewFiltre, "("+strOldParam+")(?<suite>[^0-9]?)", strNewParam+"${suite}");
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
			return result;
		}

        //-------------------------------------------------
        public virtual string GetFiltreDataTable( )
        {
            string strFiltre = Filtre;

            int nNumParametre = 1;
            foreach (object obj in Parametres)
            {
                string strReplace = obj.ToString();
                if (obj is String)
                {
                    strReplace = strReplace.Replace("'", "''");
                    strReplace = "'" + strReplace + "'";
                }
                if (obj is DateTime)
                {
                    DateTime dt = (DateTime)obj;
                    strReplace = "#" + dt.ToString("MM/dd/yyyy HH:mm") + "#";
                }
                if (obj is bool)
                    strReplace = ((bool)obj) ? "1" : "0";
                Regex ex = new Regex("(@" + nNumParametre.ToString() + ")(?<SUITE>[^0123456789]{1})");
                strFiltre = ex.Replace(strFiltre + " ", strReplace + "${SUITE}");
                nNumParametre++;
            }
            return strFiltre;
        }

        ////////////////////////////////////////////////////////
        protected string GetStringFor(object obj)
        {
            string strReplace = obj.ToString();
            if (obj is String)
            {
                strReplace = strReplace.Replace("'", "\'");
                strReplace = "'" + strReplace + "'";
            }
            if (obj is DateTime)
            {
                DateTime dt = (DateTime)obj;
                strReplace = "#" + dt.Month.ToString() + "/" + dt.Day.ToString() + "/" + dt.Year.ToString() + "#";
            }
            if (obj is bool)
                strReplace = ((bool)obj) ? "1" : "0";
            return strReplace;
        }

		

	}
}

