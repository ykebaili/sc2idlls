using System;
using System.Linq;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Décrit une 2iExpression. Utilisée pour l'analyse syntaxique notamment
	/// </summary>
	[Serializable]
	public class CInfo2iExpression
	{
		public const string c_categorieTexte = "Text";
		public const string c_categorieConversion = "Conversion";
		public const string c_categorieComparaison = "Comparaison";
		public const string c_categorieMathematiques = "Math";
		public const string c_categorieLogique = "Logical";
		public const string c_categorieDate = "Date";
		public const string c_categorieGroupe = "Grouping";
		public const string c_categorieDivers = "Misc.";
		public const string c_categorieConstantes = "Constants";

        private string m_strIdExpression = "";
		private string m_strTexte = "";
		private int m_nNiveau ;
		private CTypeResultatExpression m_TypeDonnee;
		private string m_strDescription="";
		private string m_strCategorie = "";
		private ArrayList m_listeDefinitionsParametres = new ArrayList();
		
		//Indique que l'expression peut être sélectionnée dans une liste de formule (pour interface)
		private bool m_bSelectionnable = true;

		/// ///////////////////////////////////////////////////////
		public CInfo2iExpression(int nNiveau, string strTexte, Type typeDonnee, string strDescription, string strCategorie )
		{
            Type tp = typeDonnee;
            bool bIsArray = tp.IsArray;
            if ( bIsArray )
                tp = tp.GetElementType();
			Init ( nNiveau, strTexte, strTexte, new CTypeResultatExpression(tp, bIsArray), strDescription, strCategorie);
		}
		
		/// ///////////////////////////////////////////////////////
		public CInfo2iExpression(int nNiveau, string strTexte, CTypeResultatExpression typeDonnee, string strDescription, string strCategorie )
		{
			Init ( nNiveau, strTexte, strTexte, typeDonnee, strDescription, strCategorie );
		}

        /// ///////////////////////////////////////////////////////
        public CInfo2iExpression(int nNiveau, string strIdExpression, string strTexte, CTypeResultatExpression typeDonnee, string strDescription, string strCategorie)
        {
            Init(nNiveau, strIdExpression, strTexte, typeDonnee, strDescription, strCategorie);
        }

        /// ///////////////////////////////////////////////////////
		protected void Init  (int nNiveau, string strIdExpression, string strTexte, CTypeResultatExpression typeDonnee, string strDescription, string strCategorie )
		{
			Niveau = nNiveau;
            IdExpression = strIdExpression;
			Texte = strTexte;
			TypeDonnee = typeDonnee;
			Description = strDescription;
			Categorie = strCategorie;
		}

        /// ///////////////////////////////////////////////////////
        public string IdExpression
        {
            get
            {
                return m_strIdExpression;
            }
            set
            {
                m_strIdExpression = value;
            }
        }

		/// ///////////////////////////////////////////////////////
		public string Texte
		{
			get
			{
				return m_strTexte;
			}
			set
			{
				m_strTexte = value;
			}
		}

		/// ///////////////////////////////////////////////////////
		public int Niveau
		{
			get
			{
				return m_nNiveau;
			}
			set
			{
				m_nNiveau = value;
			}
		}

		/// ///////////////////////////////////////////////////////
		public string Description
		{
			get
			{
				return m_strDescription;
			}
			set
			{
				m_strDescription = value;
			}
		}

		/// ///////////////////////////////////////////////////////
		public string Categorie
		{
			get
			{
				return m_strCategorie;
			}
			set
			{
				m_strCategorie = value;
			}
		}

		/// ///////////////////////////////////////////////////////
		public CTypeResultatExpression TypeDonnee
		{
			get
			{
				return m_TypeDonnee;
			}
			set
			{
				m_TypeDonnee = value;
			}
		}

		/// ///////////////////////////////////////////////////////
		public void AddDefinitionParametres ( CInfo2iDefinitionParametres info )
		{
			m_listeDefinitionsParametres.Add ( info );
		}

        /// ///////////////////////////////////////////////////////
        public void AddDefinitionParametres(params CInfoUnParametreExpression[] infos)
        {
            m_listeDefinitionsParametres.Add(new CInfo2iDefinitionParametres(infos));
        }


		/*/// ///////////////////////////////////////////////////////
		public int NbParametres
		{
			get
			{
				if ( m_listeDefinitionsParametres.Count == 0 )
					return 0;
				return ((CInfo2iDefinitionParametres)m_listeDefinitionsParametres[0]).TypesDonnees.Length;
			}
		}*/

		/// ///////////////////////////////////////////////////////
		public CInfo2iDefinitionParametres[] InfosParametres
		{
			get
			{
				return (CInfo2iDefinitionParametres[])m_listeDefinitionsParametres.ToArray(typeof(CInfo2iDefinitionParametres));
			}
		}

		/// ///////////////////////////////////////////////////////
		public bool Selectionnable
		{
			get
			{
				return m_bSelectionnable;
			}
			set
			{
				m_bSelectionnable = value;
			}
		}

        /// ///////////////////////////////////////////////////////////
        public string GetNomParametre(int nNumeroParametre)
        {
            string[] strNoms = GetNomsParametre(nNumeroParametre);
            StringBuilder bl = new StringBuilder();
            foreach (string strNom in strNoms)
            {
                string strTmp = strNom;
                if (strTmp.Length == 0)
                    strTmp = "[]";
                bl.Append(strTmp);
                bl.Append(",");
            }
            if (bl.Length > 0)
                bl.Remove(bl.Length - 1, 1);
            return bl.ToString().Trim();
        }

        /// ///////////////////////////////////////////////////////////
        public string[] GetNomsParametre(int nParametre)
        {
            List<string> lstNoms = new List<string>();
            string strLastNom = null;
            bool bNomsMultiples = false;
            foreach (CInfo2iDefinitionParametres infoParam in InfosParametres)
            {
                string strNom = "";
                if (nParametre >= 0 && nParametre < infoParam.InfosParametres.Count())
                {
                    strNom = infoParam.InfosParametres.ElementAt(nParametre).NomParametre;
                    if (strLastNom != null && strNom.ToUpper() != strLastNom.ToUpper())
                        bNomsMultiples = true;
                    strLastNom = strNom;
                }
                else if (infoParam.LastParametreIsMultiple)
                {
                    strNom = (strLastNom == null ?
                        I.T("Parameter|20081") :
                        strLastNom) + " "+(nParametre + 1).ToString();
                    bNomsMultiples = true;
                }
                else if (strLastNom == null)
                    strLastNom = "";
                lstNoms.Add(strNom);
            }
            if (bNomsMultiples)
                return lstNoms.ToArray();
            if (lstNoms.Count > 0 && lstNoms[0].Length > 0)
                return new string[] { lstNoms[0] };
            return new string[0];
        }
		
	}

    public static class CCacheInfosExpression 
    {
        private static Dictionary<Type, CInfo2iExpression> m_cache = new Dictionary<Type,CInfo2iExpression>();

        public static void SetInfo ( C2iExpression formule, CInfo2iExpression info )
        {
            if ( formule != null )
                m_cache[formule.GetType()] = info;
        }

        public static CInfo2iExpression GetCache ( C2iExpression formule )
        {
            if ( formule != null )
            {
                CInfo2iExpression info = null;
                m_cache.TryGetValue(formule.GetType(), out info );
                return info;
            }
            return null;
        }
    }
}
