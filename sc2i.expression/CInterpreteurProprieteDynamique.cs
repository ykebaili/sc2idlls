using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;
using System.Collections;
using System.Threading;

namespace sc2i.expression
{
	//----------------------------------------------------------
	public interface IInterpreteurProprieteDynamique
	{
		CResultAErreur GetValue(object objet, string strPropriete);
		CResultAErreur SetValue ( object objet, string strPropriete, object valeur );



		/// <summary>
		/// retourne vrai si la propriété doit être ignorée si elle est en fin de 
		/// nom de propriété de setValue
		/// </summary>
		/// <param name="strPropriete"></param>
		/// <returns></returns>
		bool ShouldIgnoreForSetValue(string strPropriete);

        IOptimiseurGetValueDynamic GetOptimiseur(Type tp, string strPropriete);

	}

    //----------------------------------------------------------
    public interface IOptimiseurGetValueDynamic
    {
        object GetValue ( object objet );

        Type GetTypeRetourne();
    }

    //----------------------------------------------------------
    public interface IInterpreteurProprieteDynamiqueAccedantADautresProprietes : IInterpreteurProprieteDynamique
    {
        void AddProprietesAccedees(CArbreDefinitionsDynamiques arbre, Type typeSource, string strProp);
    }

	//----------------------------------------------------------
	public static class CInterpreteurProprieteDynamique
	{
		private static Dictionary<string, Type> m_tableCleTypeToTypeInterpreteur = new Dictionary<string, Type>();

		private static IOldInterpreteurProprieteDynamique m_oldInterpreteur = null;

		private static CAssociationsObjetsSupplementairesPourChamps m_associationsObjetsSupplementaires = new CAssociationsObjetsSupplementairesPourChamps();

		public static CAssociationsObjetsSupplementairesPourChamps AssociationsSupplementaires
		{
			get
			{
				return m_associationsObjetsSupplementaires;
			}
		}


		//----------------------------------------------------------
		public static void SetOldInterpreteur(IOldInterpreteurProprieteDynamique oldInterpreteur)
		{
			m_oldInterpreteur = oldInterpreteur;
		}

		//----------------------------------------------------------
		/// <summary>
		/// Enregistre un nouvel interpreteur de propriete dynamique
		/// </summary>
		/// <param name="strCleType"></param>
		/// <param name="typeInterpreteur"></param>
		public static void RegisterTypeDefinition(string strCleType, Type typeInterpreteur)
		{
			Type tp = null;
			if (m_tableCleTypeToTypeInterpreteur.TryGetValue(strCleType, out tp))
				if (tp != typeInterpreteur)
					throw new Exception("Multiple Property key for " + strCleType);
			m_tableCleTypeToTypeInterpreteur[strCleType.ToUpper()] = typeInterpreteur;
		}

		//----------------------------------------------------------
		/// <summary>
		/// Le data du result contient la valeur
		/// </summary>
		/// <param name="objet"></param>
		/// <param name="strProprieteComplete"></param>
		/// <returns></returns>
		public static CResultAErreur GetValue(object objet, string strProprieteComplete)
		{
			return GetValue(objet, strProprieteComplete, null);
		}

		//----------------------------------------------------------
		public static CResultAErreur GetValue(object objet, CDefinitionProprieteDynamique defProp)
		{
			return GetValue(objet, defProp, null);
		}

		//----------------------------------------------------------
		public static CResultAErreur GetValue(object objet, CDefinitionProprieteDynamique defProp, CCacheValeursProprietes cache)
		{
			if (defProp != null)
			{
                CDefinitionMultiSourceForExpression source = objet as CDefinitionMultiSourceForExpression;
                if (source != null && !(defProp is CDefinitionProprieteDynamiqueSourceSupplementaire))
                    return GetValue(source.ObjetPrincipal, defProp);
				string strNomProp = defProp.NomPropriete;
				if (strNomProp.Length > 0 && strNomProp[0] != CDefinitionProprieteDynamique.c_strCaractereStartCleType &&
					m_oldInterpreteur != null)
				{
					CResultAErreur result = CResultAErreur.True;
					result.Data = m_oldInterpreteur.GetValue(objet, defProp);
					return result;
				}
				return GetValue(objet, strNomProp, cache);
			}
			return CResultAErreur.True;
		}


		//----------------------------------------------------------
		/// <summary>
		/// Le data du result contient la valeur de la propriété pour l'objet
		/// </summary>
		/// <param name="objet"></param>
		/// <param name="strPropriete"></param>
		/// <returns></returns>
		public static CResultAErreur GetValue(object objet, string strProprieteComplete, CCacheValeursProprietes cache)
		{
			CResultAErreur result = CResultAErreur.True;
			IApplatisseurProprietes applatisseur = objet as IApplatisseurProprietes;
			if (applatisseur != null)
				objet = applatisseur.GetObjetPourPropriete(strProprieteComplete);
			if (cache != null && objet != null)
			{
				object valeur = cache.GetValeurCache(objet, strProprieteComplete);
				if (valeur != null)
				{
					result.Data = valeur;
					return result;
				}
			}
			string[] strProprietes = strProprieteComplete.Split('.');
			string strPropriete = strProprietes[0];
			string strSuitePropriete = "";
			if (strProprieteComplete.Length > strPropriete.Length)
				strSuitePropriete = strProprieteComplete.Substring(strPropriete.Length + 1);
			string strCleType = "";
			string strProprieteSansCle = "";
			if ( CDefinitionProprieteDynamique.DecomposeNomProprieteUnique ( strPropriete, ref strCleType, ref strProprieteSansCle ) )
			{
				object objetSource = null;
				if (cache != null)
					objetSource = cache.GetValeurCache(objet, strPropriete);
				if (objetSource == null)
				{
					strPropriete = strProprieteSansCle;
					Type tpInterpreteur = null;
					if (m_tableCleTypeToTypeInterpreteur.TryGetValue(strCleType.ToUpper(), out tpInterpreteur))
					{
						IInterpreteurProprieteDynamique interpreteur = Activator.CreateInstance(tpInterpreteur) as IInterpreteurProprieteDynamique;
						if (interpreteur != null)
						{
							result = interpreteur.GetValue(objet, strPropriete);
							if (!result)
								return result;
							objetSource = result.Data;
							if (result && cache != null)
								cache.StockeValeurEnCache(objet, strProprietes[0], objetSource);
						}
					}
					else
					{
						result.EmpileErreur(I.T("Can not find parser for @1|20001", strPropriete));
						return result;
					}
				}
				if (objetSource != null && strSuitePropriete.Length > 0)
				{
					result = GetValue(objetSource, strSuitePropriete);
					if (result && cache != null)
						cache.StockeValeurEnCache(objet, strProprieteComplete, result.Data);
				}
				return result;

			}
			if (m_oldInterpreteur != null)
			{
				result.Data = m_oldInterpreteur.GetValue(objet, strProprieteComplete);
				return result;
			}
			result.EmpileErreur(I.T("Bad property format (@1)|20000", strPropriete));
			return result;
		}

        //----------------------------------------------------------
        /// <summary>
        /// Le data du result contient la valeur de la propriété pour l'objet
        /// </summary>
        /// <param name="objet"></param>
        /// <param name="strPropriete"></param>
        /// <returns></returns>
        public static IOptimiseurGetValueDynamic GetOptimiseur(Type tp, string strProprieteComplete)
        {
            COptimiseurGetValueDynamicMultiple optimiseur = new COptimiseurGetValueDynamicMultiple();
            string strSuitePropriete = "";
            do
            {
                strSuitePropriete = "";
                string[] strProprietes = strProprieteComplete.Split('.');
                string strPropriete = strProprietes[0];
                if (strProprieteComplete.Length > strPropriete.Length)
                    strSuitePropriete = strProprieteComplete.Substring(strPropriete.Length + 1);
                string strCleType = "";
                string strProprieteSansCle = "";

                if (CDefinitionMethodeDynamique.DecomposeNomProprieteUnique(strPropriete, ref strCleType, ref strProprieteSansCle))
                {
                    strPropriete = strProprieteSansCle;
                    Type tpInterpreteur = null;
                    if (m_tableCleTypeToTypeInterpreteur.TryGetValue(strCleType.ToUpper(), out tpInterpreteur))
                    {
                        IInterpreteurProprieteDynamique interpreteur = Activator.CreateInstance(tpInterpreteur) as IInterpreteurProprieteDynamique;
                        if (interpreteur != null)
                        {
                            IOptimiseurGetValueDynamic sousOptim = interpreteur.GetOptimiseur(tp, strPropriete);
                            if (sousOptim == null)
                                return null;
                            optimiseur.AddOptimiseur(sousOptim);
                            tp = optimiseur.GetTypeRetourne();
                        }
                    }
                    else
                    {
                        return null;
                    }
                    strProprieteComplete = strSuitePropriete;
                }
                else
                    return null;
            }

            while (strSuitePropriete.Length > 0);
            
            return optimiseur;
        }

        //----------------------------------------------------------
        /// <summary>
        /// Le data du result contient la valeur de la propriété pour l'objet
        /// </summary>
        /// <param name="objet"></param>
        /// <param name="strPropriete"></param>
        /// <returns></returns>
        public static string[] GetProprietesAccedees(Type typeSource, string strProprieteComplete)
        {
            string strSuitePropriete = "";
            List<string> lstProps = new List<string>();
            string strProprieteCompleteOriginale = strProprieteComplete;
            string strChemin = "";
            do
            {
                strSuitePropriete = "";
                string[] strProprietes = strProprieteComplete.Split('.');
                string strPropriete = strProprietes[0];
                if (strProprieteComplete.Length > strPropriete.Length)
                    strSuitePropriete = strProprieteComplete.Substring(strPropriete.Length + 1);
                string strCleType = "";
                string strProprieteSansCle = "";
                if (CDefinitionMethodeDynamique.DecomposeNomProprieteUnique(strPropriete, ref strCleType, ref strProprieteSansCle))
                {
                    Type tpInterpreteur = null;
                    if (m_tableCleTypeToTypeInterpreteur.TryGetValue(strCleType.ToUpper(), out tpInterpreteur))
                    {
                        IInterpreteurProprieteDynamiqueAccedantADautresProprietes interpreteur = Activator.CreateInstance(tpInterpreteur) as IInterpreteurProprieteDynamiqueAccedantADautresProprietes;
                        if (interpreteur != null)
                        {
                            CArbreDefinitionsDynamiques arbreProps = new CArbreDefinitionsDynamiques(null);
                            interpreteur.AddProprietesAccedees(arbreProps, typeSource, strProprieteSansCle);
                            foreach (string strProp in arbreProps.GetListeProprietesAccedees())
                                lstProps.Add(strChemin + strProp);
                        }
                        else
                        {
                            lstProps.Add(strChemin + strPropriete);
                        }
                        strChemin += strPropriete + ".";
                    }
                    else
                    {
                        strChemin += strPropriete + ".";
                    }
                    strProprieteComplete = strSuitePropriete;
                }
                else return lstProps.ToArray();
            }
            while (strSuitePropriete.Length > 0);

            return lstProps.ToArray();
        }

		public static CResultAErreur SetValue(object objet, CDefinitionProprieteDynamique defProp, object valeur)
		{
			CResultAErreur result = CResultAErreur.True;
			if (defProp != null)
				return SetValue(objet, defProp.NomPropriete, valeur);
			return result;
		}

		private static string NettoieProprieteSetValue(string strPropriete)
		{
			string strPropFinale = strPropriete;
			while (true)
			{
				int nPos = strPropFinale.LastIndexOf('.');
				if (nPos > 0)
				{
					string strPropFin = strPropFinale.Substring(nPos + 1);
					string strCle = "";
					string strPropNue = "";

					if (!CDefinitionProprieteDynamique.DecomposeNomProprieteUnique(strPropFin, ref strCle, ref strPropNue))
						return strPropFinale;
					Type tp = null;
					m_tableCleTypeToTypeInterpreteur.TryGetValue(strCle, out tp);
					if (tp == null)
						return strPropFinale;
					IInterpreteurProprieteDynamique interpreteur = Activator.CreateInstance(tp) as IInterpreteurProprieteDynamique;
					if (interpreteur == null)
						return strPropFin;
					if (interpreteur.ShouldIgnoreForSetValue(strPropNue))
						strPropFinale = strPropFinale.Substring(0, nPos);
					else
						return strPropFinale;
				}
				else
					return strPropFinale;
			}


		}

		public static CResultAErreur SetValue(object objet, string strPropriete, object valeur)
		{
			CResultAErreur result = CResultAErreur.True;
			//Nettoie la propriété pour enlever les éléments de fin qui ne sont pas
			//à prendre en compte lors d'un SetValue
			strPropriete = NettoieProprieteSetValue(strPropriete);
			//Récupère la valeur source
			int nPos = strPropriete.LastIndexOf('.');
			string strLastProp = strPropriete;
			if (nPos > 0)
			{
				strLastProp = strPropriete.Substring(nPos + 1);
				string strPropDebut = strPropriete.Substring(0, nPos);
				result = GetValue(objet, strPropDebut);
				if (!result)
					return result;
				objet = result.Data;
			}
			if ( objet == null )
			{
				result.EmpileErreur(I.T("Trying to affect a value to a null object |20002"));
				return result;
			}
			string strCleType = "";
			string strProprieteSansCle = "";
			if ( CDefinitionMethodeDynamique.DecomposeNomProprieteUnique ( strLastProp, ref strCleType, ref strProprieteSansCle ) )
			{
				Type tpInterpreteur = null;
				if (m_tableCleTypeToTypeInterpreteur.TryGetValue(strCleType.ToUpper(), out tpInterpreteur))
				{
					IInterpreteurProprieteDynamique interpreteur = Activator.CreateInstance(tpInterpreteur) as IInterpreteurProprieteDynamique;
					if (interpreteur != null)
					{
						return interpreteur.SetValue(objet, strProprieteSansCle, valeur);
					}
				}
			}
			result.EmpileErreur(I.T("Bad property format (@1)|20000", strPropriete));
			return result;
		}
	}

	
}
