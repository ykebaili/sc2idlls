using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{
	/// <summary>
	/// Cette classe représente une ligne de déclaration d'un attribut
	/// dans le fichier DocInfo.
	/// Des fonctions statiques sont disponibles pour générer la ligne de déclaration 
	/// selon un objet si cet objet est supporté
	/// </summary>
	public class CDocLigneAttribut
	{
		#region ++ Constructeur ++
		public CDocLigneAttribut(int numeroligne, string ligne)
		{
			m_ligne = ligne;
			m_nligne = numeroligne;

			if (DeterminationType())
			{
				ExtractionParametres();
				ConstruireObjet();

				m_valide = true;
			}
			else
				m_valide = false;

		}
		#endregion

		#region ~~ Méthodes ~~
		private bool DeterminationType()
		{
			string nom = CDocInfo.ExtraireNomAttribut(m_ligne);
			Type t;

			if (nom == typeof(DocMenu).Name)
				t = typeof(DocMenu);
			else if (nom == typeof(DocMenuItem).Name)
				t = typeof(DocMenuItem);
			else if (nom == typeof(DocRessource).Name)
				t = typeof(DocRessource);
			else if (nom == typeof(DocDossierRessource).Name)
				t = typeof(DocDossierRessource);
			else if (nom == typeof(DocLienRessourceMenu).Name)
				t = typeof(DocLienRessourceMenu);
			else if (nom == typeof(DocLienRessourceMenuItem).Name)
				t = typeof(DocLienRessourceMenuItem);
			else if (nom == typeof(DocLienRessourceRessource).Name)
				t = typeof(DocLienRessourceRessource);
			else
				return false;

			m_tattribut = t;
			return true;
		}
		private void ExtractionParametres()
		{
			m_parametres = new List<object>();

			//On récupère les valeurs en chaine de texte des parametres
			List<string> strparams = SpliterParametres();

			//Construction des parametres
			int nparam = strparams.Count;
			ConstructorInfo[] ctors = m_tattribut.GetConstructors();
			foreach (ConstructorInfo ctor in ctors)
			{
				if (ctor.GetParameters().Length == nparam)
				{
					try
					{
						ParameterInfo[] paramsinfo = ctor.GetParameters();
						int nparaminf = 0;
						foreach (ParameterInfo infp in paramsinfo)
						{
							object obj = null;
							if (infp.ParameterType.IsEnum)
								obj = Enum.Parse(infp.ParameterType, strparams[nparaminf].Split('.')[1]);
							else if (infp.ParameterType == typeof(string))
								obj = strparams[nparaminf].ToString().Trim();
							else if (infp.ParameterType == typeof(int))
								obj = int.Parse(strparams[nparaminf]);
							else if (infp.ParameterType == typeof(bool))
								obj = bool.Parse(strparams[nparaminf]);
							else if (infp.ParameterType == typeof(double))
								obj = double.Parse(strparams[nparaminf]);
							else if (infp.ParameterType == typeof(DateTime))
								obj = DateTime.Parse(strparams[nparaminf]);
							else
								continue;

							m_parametres.Add(obj);
							nparaminf++;
						}
						break;
					}
					catch (Exception e)
					{

						continue;
					}
				}

			}

		}
		private List<string> SpliterParametres()
		{
			string tmpligne = m_ligne.Substring(m_ligne.IndexOf('(') + 1);
			tmpligne = tmpligne.Substring(0, tmpligne.LastIndexOf(')'));

			char? lastchar = null;
			char[] lettres = tmpligne.ToCharArray();
			string strparam = "";
			bool zonetexte = false;
			List<string> strparams = new List<string>();

			foreach (char lettre in lettres)
			{
				if (lettre == '"')
				{
					if (!zonetexte)
					{
						strparam = "";
						zonetexte = true;
						continue;
					}
					else if (lastchar != '\\')
					{
						zonetexte = false;
						continue;
					}
				}
				else if (lettre == ',')
				{
					if (!zonetexte)
					{
						strparams.Add(strparam.Trim());
						strparam = "";
						lastchar = null;
						continue;
					}
				}
				lastchar = lettre;
				strparam += lettre;
			}

			if (strparam.Trim() != "")
				strparams.Add(strparam.Trim());

			return strparams;
		}

		private void ConstruireObjet()
		{
			try
			{
				m_obj = Activator.CreateInstance(m_tattribut, m_parametres.ToArray());
			}
			catch
			{
				m_valide = false;
			}
		}

		#endregion

		#region :: Propriétés ::
		private bool m_valide;
		private string m_ligne;
		private int m_nligne;
		private Type m_tattribut;
		private List<object> m_parametres;
		private object m_obj;
		#endregion

		#region >> Assesseurs <<
		public bool AttributValide
		{
			get
			{
				return m_valide;
			}
		}
		public string Ligne
		{
			get
			{
				return m_ligne;
			}
		}
		public List<object> Parametres
		{
			get
			{
				return m_parametres;
			}
		}
		public int NumeroLigne
		{
			get
			{
				return m_nligne;
			}

		}
		public Type TypeAttribut
		{
			get
			{
				return m_tattribut;
			}

		}
		public object ObjetAttribut
		{
			get
			{
				return m_obj;
			}
		}

		#endregion


		#region [[ Constantes ]]


		public const string debutDeclaration = "[assembly:";
		public const string finDeclaration = "]";
		#endregion

		#region :[ Propriétés Statiques ]:
		private static List<DocConstructorAttributParameter> m_cacheLstDocCtorPraInf;
		private static PropertyInfo[] m_cacheLstPropObjet;
		#endregion

		#region ~[ Méthodes Statiques ]~
		/// <summary>
		/// Créé la ligne de déclaration de l'attribut avec l'un des objets attributs suivant :
		///  - DocMenu
		///  - DocMenuItem
		///  - DocRessource
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static string GetLigneAttribut(object obj)
		{

			string ligne = debutDeclaration;

			ConstructorInfo ctor;
			Type t = null;

			//Identification du type de l'attribut
			if (obj is DocMenu)
				t = typeof(DocMenu);
			else if (obj is DocMenuItem)
				t = typeof(DocMenuItem);
			else if (obj is DocRessource)
				t = typeof(DocRessource);
			else if (obj is DocDossierRessource)
				t = typeof(DocDossierRessource);
			else if (obj is DocLienRessource)
				t = typeof(DocLienRessource);
			else if (obj is DocLienRessourceMenu)
				t = typeof(DocLienRessourceMenu);
			else if (obj is DocLienRessourceMenuItem)
				t = typeof(DocLienRessourceMenuItem);
			else if (obj is DocLienRessourceRessource)
				t = typeof(DocLienRessourceRessource);
			else
				return "";

			ligne += t.Name + "(";

			//Recuperation du constructeur
			ctor = GetConstructorAttribut(t);

			//Recuperation de la chaine des parametres
			ligne += GetConstructorParametersInLigne(obj, ctor);

			ligne += ")" + finDeclaration;

			return ligne;
		}

		/// <summary>
		/// Va chercher le constructeur à utiliser pour la déclaration dans le fichier
		/// DocInfo
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		private static ConstructorInfo GetConstructorAttribut(Type t)
		{
			ConstructorInfo[] ctors = t.GetConstructors();
			foreach (ConstructorInfo ctor in ctors)
				if (ctor.GetCustomAttributes(typeof(DocConstructorAttribut), false).Length == 1)
					return ctor;

			return null;
		}

		/// <summary>
		/// Retourne la chaine des valeurs des parametres de construction de l'objet
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="ctor"></param>
		/// <returns></returns>
		private static string GetConstructorParametersInLigne(object obj, ConstructorInfo ctor)
		{
			List<string> valsParams = GetConstructorParameters(obj, ctor);
			string retour = "";
			foreach (string val in valsParams)
				retour += val + ",";
			retour = retour.Substring(0, retour.Length - 1);

			return retour;
		}



		/// <summary>
		/// Retourne la liste des valeurs des parametres
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="ctor"></param>
		/// <returns></returns>
		private static List<string> GetConstructorParameters(object obj, ConstructorInfo ctor)
		{
			List<string> lstparams = new List<string>();
			ParameterInfo[] pars = ctor.GetParameters();

			//On trie les parametres par leurs position
			List<ParameterInfo> parsTries = new List<ParameterInfo>();
			foreach (ParameterInfo par in pars)
				parsTries.Add(par);

			ParameterInfoPositionComparer comparer = new ParameterInfoPositionComparer();
			parsTries.Sort(comparer);



			foreach (ParameterInfo par in pars)
				lstparams.Add(GetConstructorParameter(obj, par));

			return lstparams;
		}
		/// <summary>
		/// Retourne en string la valeur du parametre selon l'objet
		/// </summary>
		/// <param name="obj">l'objet avec les valeurs à inscrire</param>
		/// <param name="ff"></param>
		/// <returns></returns>
		private static string GetConstructorParameter(object obj, ParameterInfo par)
		{

			//recuperation du constructeur
			ConstructorInfo ctor = (ConstructorInfo)par.Member;

			//Si on est au premier attribut on RAZ les caches
			if (par.Position == 0)
			{
				m_cacheLstPropObjet = ctor.DeclaringType.GetProperties();
				m_cacheLstDocCtorPraInf = null;
			}

			//recuperation de l'attribut de relation Propriete / Parametre
			DocConstructorAttributParameter parinf = GetDocConstructorParameterInfo(ctor, par.Name);

			foreach (PropertyInfo prop in m_cacheLstPropObjet)
				if (prop.Name == parinf.NomPropriete)
				{

					//On récupère l'assesseur de la propriete et la valeur dans l'objet
					MethodInfo met = prop.GetGetMethod();
					object valprop = met.Invoke(obj, new object[] { });

					//Dans le cas d'une Enumeration on doit mettre l'identifieur de la classe enum
					if (prop.PropertyType.IsEnum)
					{
						//A TESTER
						return prop.PropertyType.Name + "." + valprop.ToString();
						//return prop.PropertyType.Name + "." + Activator.CreateInstance(
					}
					else if (prop.PropertyType == typeof(string))
						return "\"" + (string)valprop + "\"";
					else
						return valprop.ToString();
				}

			return "";
		}
		/// <summary>
		/// Retourne l'attribut d'information permettant de faire la relation
		/// entre un paramètre et la propriété de l'objet
		/// </summary>
		/// <param name="ctor"></param>
		/// <param name="nomParametre"></param>
		/// <returns></returns>
		private static DocConstructorAttributParameter GetDocConstructorParameterInfo(ConstructorInfo ctor, string nomParametre)
		{
			//Initialisation
			if (m_cacheLstDocCtorPraInf == null)
			{
				m_cacheLstDocCtorPraInf = new List<DocConstructorAttributParameter>();
				object[] attsDoc = ctor.GetCustomAttributes(typeof(DocConstructorAttributParameter), false);
				List<DocConstructorAttributParameter> lstInfoPars = new List<DocConstructorAttributParameter>();
				foreach (object obj in attsDoc)
					m_cacheLstDocCtorPraInf.Add((DocConstructorAttributParameter)obj);
			}

			foreach (DocConstructorAttributParameter inf in m_cacheLstDocCtorPraInf)
				if (inf.NomParametre == nomParametre)
					return inf;
			return null;
		}
		#endregion

	}
}
