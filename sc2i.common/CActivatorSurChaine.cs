using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Globalization;

namespace sc2i.common
{
	/// <summary>
	/// Permet d'obtenir un type d'objet à partir de son nom (toString du type).
	/// </summary>
	public static class CActivatorSurChaine
	{
		/// <summary>
		/// contient la liste des noms de classe qui ont changé de nom
		/// et pour lesquels on doit conserver une trace de compatibilité.
		/// </summary>
		private static Dictionary<string, Type> m_dicTypesObsoletes = new Dictionary<string, Type>();

		private static Dictionary<string, Type> m_cache = new Dictionary<string,Type>();

		/// //////////////////////////////////////////////
		public static void RegisterTypeObsolete(string strType, Type typeRemplacant)
		{
			m_dicTypesObsoletes[strType] = typeRemplacant;
		}

		public static Type GetType ( string strTypeAdr )
		{
			return GetType( strTypeAdr, false);
		}

		/// <summary>
		/// Enregistre le type pour éviter d'avoir à le rechercher (optim)
		/// </summary>
		/// <param name="tp"></param>
		public static void RegisterType ( Type tp )
		{
			m_cache[tp.ToString()] = tp;
			string strNomConvivial = DynamicClassAttribute.GetNomConvivial ( tp );
			if ( strNomConvivial != tp.ToString() )
				m_cache[strNomConvivial] = tp;
		}

		/// <summary>
		/// Obtient le nom permettant de recharger le type en moins de deux.
		/// </summary>
		/// <param name="tp"></param>
		/// <returns></returns>
		public static string GetNomTypeAvecAssembly(Type tp)
		{
			if (tp == null)
				return "";
			return tp.Assembly.GetName().Name + "|" + tp.ToString();
		}

        //------------------------------------------------------------------
        public static Type GetType(string strTypeAdr, bool bAvecDynamicClass)
        {
            return GetType(strTypeAdr, bAvecDynamicClass, true);
        }

		/// <summary>
		/// Aloue un objet à partir de son type sous forme de texte
		/// </summary>
		/// <param name="strTypeURI"></param>
		/// <param name="parametresConstructeur"></param>
		/// <returns></returns>
		public static Type GetType ( string strTypeAdr, bool bAvecDynamicClass, bool bAvecTypeId )
		{
            
			if ( String.IsNullOrEmpty(strTypeAdr) )
				return null;
			string strAss = "";

            Type tp = null;
            string strTypeDemande = strTypeAdr;
            if (m_cache.TryGetValue(strTypeAdr, out tp))
                return tp;
            if (bAvecTypeId)
                tp = TypeIdAttribute.GetType(strTypeAdr);
            if (tp != null)
                return tp;
			int nPosPipe = strTypeAdr.IndexOf('|');
			if (nPosPipe > 0)//Si le nom est composé de assembly|nom de classe
			{
				strAss = strTypeAdr.Substring(0, nPosPipe);
				strTypeAdr = strTypeAdr.Substring(nPosPipe + 1);
			}
			string strTypeUpper = strTypeAdr.ToUpper(CultureInfo.InvariantCulture);
			tp = Type.GetType(strTypeAdr);
			if ( tp == null )
			{
				if ( !m_cache.TryGetValue ( strTypeAdr, out tp ) || tp == null)
				{
					if ( strAss.Length > 0 )
					{
						try
						{
							Assembly ass = Assembly.Load(strAss);
							if (ass != null)
								tp = ass.GetType(strTypeAdr);
						}
						catch
						{ }
					}
					if (tp == null)
					{
						//Tente d'abord en direct dans les assemblies
						foreach (System.Reflection.Assembly ass in CGestionnaireAssemblies.GetAssemblies())
						{
							tp = ass.GetType(strTypeAdr);
							if (tp != null)
								break;
						}
					}
					if (tp == null)
					{
						//Si on ne trouve pas en recherche directe, balaie tous les assemblies, et cherche à la main dedans
						foreach (System.Reflection.Assembly ass in CGestionnaireAssemblies.GetAssemblies())
						{
							foreach (Type type in ass.GetTypes())
							{
								if (type.FullName == strTypeAdr ||
									 type.Name == strTypeAdr ||
									 (bAvecDynamicClass && DynamicClassAttribute.GetNomConvivial(
                                     type).ToUpper(CultureInfo.InvariantCulture) == strTypeUpper))
								{
									tp = type;
									break;
								}
							}
							if (tp != null)
								break;
						}
					}
					if (tp == null)
					{
						//Recherche parmis les types obsolètes
						if (!m_dicTypesObsoletes.TryGetValue(strTypeAdr, out tp))
							tp = null;
					}
                    m_cache[strTypeAdr] = tp;
					
				}
			}
            m_cache[strTypeDemande] = tp;
            
			return tp;
		}

        //------------------------------------------------------------------
        public static string GetTypeId(Type tp)
        {
            if (tp == null)
                return "";
            string strId = TypeIdAttribute.GetTypeId(tp);
            if (strId == null)
                strId = tp.ToString();
            return strId;
        }

        //------------------------------------------------------------------
        public static string Sc2iTypeId(this Type tp)
        {
            return GetTypeId(tp);
        }
	}
}
