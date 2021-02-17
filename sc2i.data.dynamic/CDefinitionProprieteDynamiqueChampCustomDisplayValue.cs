using System;
using System.Collections.Generic;
using sc2i.common;
using sc2i.expression;
using System.Text;
using System.Data;
using sc2i.multitiers.client;
using System.Collections;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de CDefinitionProprieteDynamiqueChampCustom.
	/// </summary>
	[Serializable]
	[AutoExec("Autoexec")]
	public class CDefinitionProprieteDynamiqueChampCustomDisplayValue : CDefinitionProprieteDynamiqueChampCustom
	{
		public const string c_strCleType = "CDV";

		/// //////////////////////////////////////////////////////
		public CDefinitionProprieteDynamiqueChampCustomDisplayValue()
			:base()
		{
		}

		//-----------------------------------------------
		public static new void Autoexec()
		{
			CInterpreteurProprieteDynamique.RegisterTypeDefinition ( c_strCleType, typeof(CInterperteurProprieteDynamiqueChampCustomDisplayValue) );
		}

		//-----------------------------------------------
		public override string CleType
		{
			get { return c_strCleType; }
		}

		


		/// //////////////////////////////////////////////////////
		public CDefinitionProprieteDynamiqueChampCustomDisplayValue( CChampCustom champ )
            :base( champ, GetKeyChamp(champ), true )
		{
		}

        /// //////////////////////////////////////////////////////
        private static string GetKeyChamp(CChampCustom champ)
        {
            //TESTDBKEYOK
            return CInfoRelationComposantFiltreEntiteToChampEntite.GetKeyChamp(champ, true) + "." +
                    CDefinitionProprieteDynamique.c_strCaractereStartCleType + c_strCleType +
                    CDefinitionProprieteDynamique.c_strCaractereEndCleType +
                    CInfoRelationEValeurChampCustomToDisplay.GetKeyChamp(champ);
        }

        /// //////////////////////////////////////////////////////
		public override void CopyTo(CDefinitionProprieteDynamique def)
		{
			base.CopyTo(def);
			((CDefinitionProprieteDynamiqueChampCustomDisplayValue)def).m_dbKeyChamp = m_dbKeyChamp;
		}

		/// ////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// ////////////////////////////////////////
		public override CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			result = base.Serialize ( serializer );
			if ( !result )
				return result;
			return result;
		}

        /// ////////////////////////////////////////
        public override string NomChampCompatibleCComposantFiltreChamp
        {
            get
            {
                //TESTDBKEYTODO
                //Insère le chemin indiqué devant la propriété
                string[] strChemins = NomPropriete.Split('.');
                string strRetour = "";
                for (int nChemin = 0; nChemin < strChemins.Length - 1; nChemin++)
                    strRetour += strChemins[nChemin] + ".";
                strRetour += CInfoRelationEValeurChampCustomToDisplay.GetRelationKey(m_dbKeyChamp.StringValue) + ".";
                strRetour += CValeurChampCustom.c_champDisplay;
                return strRetour;
            }
        }

        

	}

	

	/// ////////////////////////////////////////////////////////////////
	[AutoExec("AutoexecDisplayValue")]
    public class CInterperteurProprieteDynamiqueChampCustomDisplayValue : 
        CInterperteurProprieteDynamiqueChampCustom

	{
        //------------------------------------------------------------
        public static void AutoexecDisplayValue()
        {
            CGestionnaireDependanceListeObjetsDonneesReader.RegisterReader ( CDefinitionProprieteDynamiqueChampCustomDisplayValue.c_strCleType, 
                typeof(CInterperteurProprieteDynamiqueChampCustomDisplayValue ));
        }

        //------------------------------------------------------------
        public override bool ShouldIgnoreForSetValue(string strPropriete)
        {
            return true;
        }

		//------------------------------------------------------------
		public override CResultAErreur GetValue(object objet, string strPropriete)
		{
            CResultAErreur result = CResultAErreur.True;
			result.Data = null;
            string strIdChamp = null;
            bool bEntiteToChamp = false;
 
            if (
                /*Pour compatibilité, avant modifs de décembre 2015, le nom du champ était en fait le même qu'une propriété champ custom.
                 * En décembre 2015, ajout de la possiblité de retourner les valeurs display de champ custom dans les requêtes,
                 * ce qui a changé le nom de la propriétés
                 * */
                CInfoRelationEntiteToValeurChampCustom.DecomposeNomPropriete(strPropriete, ref strIdChamp) ||
                /*fin pour compatiblité*/
                CInfoRelationComposantFiltreEntiteToChampEntite.DecomposeNomPropriete ( strPropriete, ref strIdChamp, ref bEntiteToChamp ))
            {
                IElementAChamps eltAChamps = objet as IElementAChamps;
                if (eltAChamps == null)
                    return result;
                result.Data = eltAChamps.GetValeurChamp(strIdChamp);
                CChampCustom champ = new CChampCustom(CContexteDonneeSysteme.GetInstance());
                if (result.Data != null && champ.ReadIfExists(CDbKey.CreateFromStringValue(strIdChamp)))
                {
                    if (!champ.IsChoixParmis())
                        result.Data = result.Data.ToString();
                    else
                    {
                        foreach (CValeurChampCustom valeur in champ.ListeValeurs)
                        {
                            if (valeur.Value.Equals(result.Data))
                            {
                                result.Data = valeur.Display;
                                return result;
                            }
                        }
                    }
                }
            }
            else if ( CInfoRelationEValeurChampCustomToDisplay.DecomposeNomPropriete ( strPropriete, ref strIdChamp ))
            {
                //Rien de spécial, il s'agit de la navigation vers la valeur du champ qui a déjà été reprise par le composant filtre to champ entité
                //car la propriété complète d'un definitionDisplayValue est donnée par GetKeyChamp qui ajoute un composant Element->ValeurChamp + ValeurChamp->Display value
                result.Data = objet;
            }
            
			return result;
		}

		/// //////////////////////////////////////////////////////
        public override CResultAErreur SetValue(object elementToModif, string strPropriete, object nouvelleValeur)
        {
            CResultAErreur result = CResultAErreur.True;
            return result;
        }

        /// //////////////////////////////////////////////////////
        protected override IOptimiseurGetValueDynamic GetNewOptimiseur(CDbKey dbKeyChamp, Type tp, bool bFromChampToEntite, bool bFromEntiteToChamp)
        {
            return new COptimiseurProprieteDynamiqueChampCustomDisplayValue(dbKeyChamp);
        }
    }

    /// //////////////////////////////////////////////////////
    public class COptimiseurProprieteDynamiqueChampCustomDisplayValue : IOptimiseurGetValueDynamic
    {
        private CDbKey m_dbKeyChamp = null;
        Dictionary<object, string> m_dicValeurs = new Dictionary<object, string>();

        public COptimiseurProprieteDynamiqueChampCustomDisplayValue(CDbKey keyChamp)
        {
            m_dbKeyChamp = keyChamp;
            CChampCustom champ = new CChampCustom(CContexteDonneeSysteme.GetInstance());
            if (champ.ReadIfExists(keyChamp))
            {
                if (champ.IsChoixParmis())
                {
                    foreach (CValeurChampCustom valeur in champ.ListeValeurs)
                    {
                        m_dicValeurs[valeur.Value] = valeur.Display;
                    }
                }
            }
        }

        public object GetValue(object objet)
        {
            IElementAChamps eltAChamps = objet as IElementAChamps;
            if (eltAChamps == null)
                return null;
            object valeur = eltAChamps.GetValeurChamp(m_dbKeyChamp.StringValue);
            if (valeur == null)
                return valeur;
            string strDisplay = valeur.ToString();
            m_dicValeurs.TryGetValue(valeur, out strDisplay);
            return strDisplay;
        }

        public Type GetTypeRetourne()
        {
            return typeof(string);
        }
    }
        
      

	[AutoExec("Autoexec")]
	public class CFournisseurProprietesDynamiqueChampCustomDisplayValue : 
        IFournisseurProprieteDynamiquesSimplifie
	{

		public static void Autoexec()
		{
			CFournisseurGeneriqueProprietesDynamiques.RegisterTypeFournisseur(new CFournisseurProprietesDynamiqueChampCustomDisplayValue());
		}

		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet, CDefinitionProprieteDynamique defParente)
		{
			List<CDefinitionProprieteDynamique> lstProps = new List<CDefinitionProprieteDynamique>();
			if (objet == null)
				return lstProps.ToArray();

			Type tp = objet.TypeAnalyse;
			if (tp == null)
				return lstProps.ToArray();
            if (!C2iFactory.IsInit())
                return lstProps.ToArray();
			//Champs custom
			CRoleChampCustom role = CRoleChampCustom.GetRoleForType(tp);
            CRestrictionUtilisateurSurType rest = null;
            CSessionClient session = CSessionClient.GetSessionUnique();
            if (session != null)
            {
                IInfoUtilisateur info = session.GetInfoUtilisateur();
                if (info != null)
                    rest = info.GetRestrictionsSur(tp, null);
            }
			if (role != null)
			{
				CContexteDonnee contexte = CContexteDonneeSysteme.GetInstance();
                CListeObjetsDonnees listeChamps = CChampCustom.GetListeChampsForRole(contexte, role.CodeRole);
				foreach (CChampCustom champ in listeChamps)
				{
                    if (rest != null)
                    {
                        ERestriction restChamp = rest.GetRestriction(champ.CleRestriction);
                        if ((restChamp & ERestriction.Hide) == ERestriction.Hide)
                            continue;
                    }
                    if (champ.ListeValeurs.Count > 0)
                    {
                        CDefinitionProprieteDynamiqueChampCustomDisplayValue def = new CDefinitionProprieteDynamiqueChampCustomDisplayValue(champ);
                        if (champ.Categorie.Trim() != "")
                            def.Rubrique = champ.Categorie;
                        else
                            def.Rubrique = I.T("Complementary informations|59");
                        lstProps.Add(def);
                    }
				}
			}
			return lstProps.ToArray();

		}

	}


    /// ////////////////////////////////////////////////////////////////
    /// Relation d'une entité vers une valeur de champ custom qui n'est pas une entité
    [AutoExec("Autoexec")]
    [Serializable]
    public class CInfoRelationEValeurChampCustomToDisplay : CInfoRelationComposantFiltre
    {
        private static string c_cleChamp = "VALDIS";
        private CDbKey m_dbKeyChamp = null;
        private string m_strTableParente = "";


        /// ////////////////////////////////////////////////////////////////
        public CInfoRelationEValeurChampCustomToDisplay()
        {
        }

        /// ////////////////////////////////////////////////////////////////
        public CInfoRelationEValeurChampCustomToDisplay(CDbKey dbKeyChamp, string strTableParente)
        {
            m_dbKeyChamp = dbKeyChamp;
            m_strTableParente = strTableParente;
        }

        /// ////////////////////////////////////////////////////////////////
        public static string GetKeyChamp(CChampCustom champ)
        {
            return GetRelationKey(champ.DbKey.StringValue);
        }

        public static string GetRelationKey(string strKeyChamp)
        {
            return c_cleChamp + "¤" + strKeyChamp + "¤";
        }

        /// ////////////////////////////////////////////////////////////////
        public static void Autoexec()
        {
            CComposantFiltreChamp.FindRelationComplementaire += new FindRelationDelegate(FindRelation);
        }

        public static bool DecomposeNomPropriete(string strNomPropriete, ref string strKeyChamp)
        {
            string strCleDef = "";
            string strPropDef = "";
            if (CDefinitionProprieteDynamique.DecomposeNomProprieteUnique(strNomPropriete, ref strCleDef, ref strPropDef))
            {
                if (strCleDef != CDefinitionProprieteDynamiqueChampCustomDisplayValue.c_strCleType )
                    return false;
                strNomPropriete = strPropDef;
            }
            string[] strZones = strNomPropriete.Split('¤');
            if (strZones[0] != c_cleChamp )
                return false;
            //Oui, c'en est une
            try
            {
                strKeyChamp = strZones[1];
                return true;
            }
            catch
            { }
            return false;
        }

        /// ////////////////////////////////////////////////////////////////
        public static void FindRelation(string strTable, Type type, ref CInfoRelationComposantFiltre relationTrouvee)
        {
            //TESTDBKEYTODO
            if (relationTrouvee != null)
                return;
            string strKeyChamp = "";
            if (DecomposeNomPropriete(strTable, ref strKeyChamp))
            {
                string strTableEntite = CContexteDonnee.GetNomTableForType(type);
                //Si strKeyChamp est un int, il s'agit d'un id de champ custom
                //ce cas peut se présenter lors de la relecture d'un filtre sous forme de
                //test par exemple dans une fonction ObjectList
                relationTrouvee = new CInfoRelationEValeurChampCustomToDisplay(CDbKey.CreateFromStringValue(strKeyChamp), strTableEntite);
            }
            //relationTrouvee = new CInfoRelationEValeurChampCustomToDisplay(relToChamp.DbKeyChamp, relToChamp.TableFille);
        }
        /// ////////////////////////////////////////////////////////////////
        public override bool IsRelationFille
        {
            get
            {
                return true;
            }
        }

        /// ////////////////////////////////////////////////////////////////
        private int GetNumVersion()
        {
            return 0;
        }

        /// ////////////////////////////////////////////////////////////////
        public override CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;

            serializer.TraiteDbKey(ref m_dbKeyChamp);
            serializer.TraiteString(ref m_strTableParente);

            return result;
        }



        /// ////////////////////////////////////////////////////////////////
        public override string RelationKey
        {
            get
            {
                return GetRelationKey(m_dbKeyChamp.StringValue);
            }
        }


        /// ////////////////////////////////////////////////////////////////
        public override string TableParente
        {
            get
            {
                return m_strTableParente;
            }
        }

        /// ////////////////////////////////////////////////////////////////
        public override string TableFille
        {
            get
            {
                return CValeurChampCustom.c_nomTable;
            }
        }

        /// ////////////////////////////////////////////////////////////////
        //Retourne le texte de la clause join à mettre dans SQL
        public override string GetJoinClause(
            string strAliasTableParente,
            string strSuffixeParent,
            string strAliasTableFille,
            string strSuffixeFils)
        {

            string strAliasTableValeursChampCustom = strAliasTableFille;
            string strAliasTableValeurDuChamp = strAliasTableParente;
            string strSuffixeValeursChampsCustom = strSuffixeFils;
            string strSuffixeValeurDuChamp = strSuffixeParent;
            
            string strJoin = "";
            int nIdChamp = CChampCustom.GetIdFromDbKey(m_dbKeyChamp);
            string strChampValeur = CRelationElementAChamp_ChampCustom.c_champValeurString;
            strJoin = strAliasTableParente + "." + strChampValeur + strSuffixeValeurDuChamp + "=" +
                strAliasTableValeursChampCustom + "." + CValeurChampCustom.c_champValue + strSuffixeValeursChampsCustom + " and " +
                strAliasTableValeursChampCustom + "." + CChampCustom.c_champId + strSuffixeValeursChampsCustom + "=" + nIdChamp.ToString();
            return strJoin;
        
        }
    }
}
