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
    public class CDefinitionProprieteDynamiqueChampCustom : CDefinitionProprieteDynamique
    {
        public const string c_strCleType = "CC";
        // TESTDBKEYOK
        protected CDbKey m_dbKeyChamp = null;

        /// //////////////////////////////////////////////////////
        public CDefinitionProprieteDynamiqueChampCustom()
            : base()
        {
        }

        /// //////////////////////////////////////////////////////
        public CDefinitionProprieteDynamiqueChampCustom(CChampCustom champ)
            : this(champ, GetKeyChamp(champ), false)
        {
        }


        /// //////////////////////////////////////////////////////
        protected CDefinitionProprieteDynamiqueChampCustom(CChampCustom champ, string strKeyChamp, bool bValeurAffichee)
            : base(
            champ.Nom.Replace(" ", "_") + (bValeurAffichee ? "_display" : ""),
            strKeyChamp,
            new CTypeResultatExpression(
                champ.TypeDonneeChamp.TypeDonnee == sc2i.data.dynamic.TypeDonnee.tObjetDonneeAIdNumeriqueAuto ? champ.TypeObjetDonnee :
                bValeurAffichee && champ.IsChoixParmis() ?
                    typeof(string) :
                    champ.TypeDonneeChamp.TypeDotNetAssocie
                , false),
            champ.TypeDonneeChamp.TypeDonnee == sc2i.data.dynamic.TypeDonnee.tObjetDonneeAIdNumeriqueAuto ? true : false,

            false)
        {
            //TESTDBKEYOK
            m_dbKeyChamp = champ.DbKey;
        }

        //-----------------------------------------------
        public static new void Autoexec()
        {
            CInterpreteurProprieteDynamique.RegisterTypeDefinition(c_strCleType, typeof(CInterperteurProprieteDynamiqueChampCustom));
        }

        //-----------------------------------------------
        public override string CleType
        {
            get { return c_strCleType; }
        }

        /// //////////////////////////////////////////////////////
        private static string GetKeyChamp(CChampCustom champ)
        {
            //TESTDBKEYOK
            if (champ.TypeDonneeChamp.TypeDonnee == sc2i.data.dynamic.TypeDonnee.tObjetDonneeAIdNumeriqueAuto)
                return CInfoRelationComposantFiltreEntiteToChampEntite.GetKeyChamp(champ, true) + "." +
                    CDefinitionProprieteDynamique.c_strCaractereStartCleType + c_strCleType +
                    CDefinitionProprieteDynamique.c_strCaractereEndCleType +
                    CInfoRelationComposantFiltreChampToEntite.GetKeyChamp(champ, false);
            return CInfoRelationEntiteToValeurChampCustom.GetKeyChamp(champ);
        }

        /// //////////////////////////////////////////////////////
        public static string GetIdPropriete(string strProp)
        {
            //TESTDBKEYTODO : vérifier à quoi ça sert,c'est utilisé dans
            //l'interpreteur old propriétés dynamiques
            string strId = "";
            bool bFromEntiteToChamp = false;
            if (CInfoRelationComposantFiltreEntiteToChampEntite.DecomposeNomPropriete(strProp, ref strId, ref bFromEntiteToChamp))
                return strId;
            return null;
        }

        

        /// //////////////////////////////////////////////////////
        [ExternalReferencedEntityDbKey(typeof(CChampCustom))]
        public CDbKey DbKeyChamp
        {
            get
            {
                return m_dbKeyChamp;
            }
        }

        public override void CopyTo(CDefinitionProprieteDynamique def)
        {
            base.CopyTo(def);
            ((CDefinitionProprieteDynamiqueChampCustom)def).m_dbKeyChamp = m_dbKeyChamp;
        }

        /// ////////////////////////////////////////
        private int GetNumVersion()
        {
            //return 0;
            return 1; // Passage de Id Champ Custom à DbKey
        }

        /// ////////////////////////////////////////
        public override CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = base.Serialize(serializer);
            if (!result)
                return result;

            if (nVersion < 1)
            {
                //TESTDBKEYOK
                serializer.ReadDbKeyFromOldId(ref m_dbKeyChamp, typeof(CChampCustom));
                CChampCustom champ = new CChampCustom(CContexteDonneeSysteme.GetInstance());
                if (NomPropriete.IndexOf(".") > 0)
                    nVersion = 0;
                if (champ.ReadIfExists(m_dbKeyChamp))
                    //Convertit le nom de propriété avec les DbKey
                    //ne pas appeller SetNomProprieteSansCleTypeChamp(GetKeyChamp()) car pour les filtres,
                    //le nom de propriété peut contenir un chemin
                    ConvertNomProprieteFromIdToDbKey();
            }
            else
                //TESTDBKEYOK
                serializer.TraiteDbKey(ref m_dbKeyChamp);
            return result;
        }

        /// ////////////////////////////////////////////////////////////////
        public void ConvertNomProprieteFromIdToDbKey()
        {
            SetNomPropriete(ConvertNomProprieteFromIdToDbKey(NomPropriete));
        }

        /// ////////////////////////////////////////////////////////////////
        public static string ConvertNomProprieteFromIdToDbKey ( string strNomPropriete )
        {
            StringBuilder bl = new StringBuilder();
            string[] strProps = strNomPropriete.Split('.');
            foreach (string strPropAveCle in strProps)
            {
                string strPropKey = "";
                string[] strDefs = strPropAveCle.Split('|');
                if (strDefs.Length == 2)
                {
                    strPropKey = strDefs[0];
                    string strProp = strDefs[1];
                    bl.Append(strPropKey);
                    bl.Append('|');

                    string strCleChamp = "";
                    bool bFromEntiteToChamp = false;
                    if (CInfoRelationEntiteToValeurChampCustom.DecomposeNomPropriete(strProp, ref strCleChamp))
                    {
                        int nId = 0;
                        CChampCustom champ = new CChampCustom(CContexteDonneeSysteme.GetInstance());
                        if (Int32.TryParse(strCleChamp, out nId) && champ.ReadIfExists(nId))
                        {
                            bl.Append(CInfoRelationEntiteToValeurChampCustom.GetRelationKey(champ.DbKey.StringValue));
                        }
                        else
                            bl.Append(strProp);
                    }
                    else if (CInfoRelationComposantFiltreEntiteToChampEntite.DecomposeNomPropriete(
                        strProp, ref strCleChamp, ref bFromEntiteToChamp))
                    {
                        int nId = 0;
                        CChampCustom champ = new CChampCustom(CContexteDonneeSysteme.GetInstance());
                        if (Int32.TryParse(strCleChamp, out nId) && champ.ReadIfExists(nId))
                        {
                            bl.Append(CInfoRelationComposantFiltreEntiteToChampEntite.GetKeyChamp(champ, bFromEntiteToChamp));
                        }
                        else
                            bl.Append(strProp);
                    }
                    else
                        bl.Append(strProp);
                }
                else
                    bl.Append(strPropAveCle);
                bl.Append('.');
            }
            if ( bl.Length > 0 )
                bl.Remove ( bl.Length-1, 1);
            return bl.ToString();
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
                strRetour += CInfoRelationEntiteToValeurChampCustom.GetRelationKey(m_dbKeyChamp.StringValue) + ".";
                Type tpRetour = TypeDonnee.TypeDotNetNatif;
                if (tpRetour == C2iTypeDonnee.GetTypeDotNetFor(sc2i.data.dynamic.TypeDonnee.tEntier, null))
                    strRetour += CRelationElementAChamp_ChampCustom.c_champValeurInt;
                else if (tpRetour == C2iTypeDonnee.GetTypeDotNetFor(sc2i.data.dynamic.TypeDonnee.tDouble, null))
                    strRetour += CRelationElementAChamp_ChampCustom.c_champValeurDouble;
                else if (tpRetour == C2iTypeDonnee.GetTypeDotNetFor(sc2i.data.dynamic.TypeDonnee.tDate, null))
                    strRetour += CRelationElementAChamp_ChampCustom.c_champValeurDate;
                else if (tpRetour == C2iTypeDonnee.GetTypeDotNetFor(sc2i.data.dynamic.TypeDonnee.tBool, null))
                    strRetour += CRelationElementAChamp_ChampCustom.c_champValeurBool;
                else strRetour += CRelationElementAChamp_ChampCustom.c_champValeurString;
                return strRetour;
            }
        }

        /// //////////////////////////////////////////////////////
        public override ERestriction GetRestrictionAAppliquer(CRestrictionUtilisateurSurType rest)
        {
            //TESTDBKEYTODO
            return rest.GetRestriction(CChampCustom.GetCleRestriction(m_dbKeyChamp));
        }
    }

    /// ////////////////////////////////////////////////////////////////
    /// Relation d'une entité vers une valeur de champ custom qui n'est pas une entité
    [AutoExec("Autoexec")]
    [Serializable]
    public class CInfoRelationEntiteToValeurChampCustom : CInfoRelationComposantFiltre
    {
        private static string c_oldCleChamp = "ETYTOCUSTVAL";
        private static string c_cleChamp = "VAL";
        private CDbKey m_dbKeyChamp = null;
        private string m_strTableEntite = "";

        public static string GetKeyChamp(CChampCustom champ)
        {
            return GetRelationKey(champ.DbKey.StringValue);
        }

        /* TESTDBKEYOK (SC)*/
        public static string GetRelationKey(string strKeyChamp)
        {
            return c_cleChamp + "¤" + strKeyChamp + "¤";
        }

        public static bool DecomposeNomPropriete(string strNomPropriete, ref string strKeyChamp)
        {
            string strCleDef = "";
            string strPropDef = "";
            if (CDefinitionProprieteDynamique.DecomposeNomProprieteUnique(strNomPropriete, ref strCleDef, ref strPropDef))
            {
                if (strCleDef != CDefinitionProprieteDynamiqueChampCustom.c_strCleType &&
                    strCleDef != CDefinitionProprieteDynamiqueChampCustomFils.c_strCleType)
                    return false;
                strNomPropriete = strPropDef;
            }
            string[] strZones = strNomPropriete.Split('¤');
            if (strZones[0] != c_cleChamp && strZones[0] != c_oldCleChamp)
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
        public CInfoRelationEntiteToValeurChampCustom()
        {
        }

        /// ////////////////////////////////////////////////////////////////
        public CInfoRelationEntiteToValeurChampCustom(CDbKey dbKeyChamp, string strTableEntite)
        {
            m_dbKeyChamp = dbKeyChamp;
            m_strTableEntite = strTableEntite;
        }

        /// ////////////////////////////////////////////////////////////////
        public static void Autoexec()
        {
            CComposantFiltreChamp.FindRelationComplementaire += new FindRelationDelegate(FindRelation);
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
                int nTmp;
                //Si strKeyChamp est un int, il s'agit d'un id de champ custom
                //ce cas peut se présenter lors de la relecture d'un filtre sous forme de
                //test par exemple dans une fonction ObjectList
                if (int.TryParse(strKeyChamp, out nTmp))
                {
                    //TESTDBKEYTODO
                    strKeyChamp = CDbKey.GetNewDbKeyOnIdAUtiliserPourCeuxQuiNeGerentPasLeDbKey(nTmp).StringValue;
                }
                relationTrouvee = new CInfoRelationEntiteToValeurChampCustom(CDbKey.CreateFromStringValue(strKeyChamp), strTableEntite);
            }
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
            //return 0;
            return 1; // Passage de Id Champ à DbKey
        }

        /// ////////////////////////////////////////////////////////////////
        public override CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;

            //TESTDBKEYTODO
            if (nVersion < 1 )
                 serializer.ReadDbKeyFromOldId(ref m_dbKeyChamp, typeof(CChampCustom));
            else
                serializer.TraiteDbKey(ref m_dbKeyChamp);

            serializer.TraiteString(ref m_strTableEntite);
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
        internal CDbKey DbKeyChamp
        {
            get
            {
                return m_dbKeyChamp;
            }
        }

        private static Dictionary<string, CInfoRelation> m_cacheRelationsToChampsCustom = new Dictionary<string, CInfoRelation>();

        internal static CInfoRelation FindRelationToValeursChamps(string strTableEntite)
        {
            if (m_cacheRelationsToChampsCustom.ContainsKey(strTableEntite))
                return m_cacheRelationsToChampsCustom[strTableEntite];
            //Trouve le nom de la table qui contient les valeurs
            foreach (CInfoRelation relation in CContexteDonnee.GetListeRelationsTable(strTableEntite))
            {
                if (relation.TableParente == strTableEntite)
                {
                    Type tpAVerifier = CContexteDonnee.GetTypeForTable(relation.TableFille);
                    if (tpAVerifier != null && typeof(CRelationElementAChamp_ChampCustom).IsAssignableFrom(tpAVerifier))
                    {
                        m_cacheRelationsToChampsCustom[strTableEntite] = relation;
                        return relation;
                    }
                }
            }
            throw new Exception(I.T("Cannot find the link between @1 and custom fields|161", strTableEntite));
        }


        /// ////////////////////////////////////////////////////////////////
        public override string TableParente
        {
            get
            {
                return m_strTableEntite;
            }
        }

        /// ////////////////////////////////////////////////////////////////
        public override string TableFille
        {
            get
            {
                CInfoRelation relation = FindRelationToValeursChamps(m_strTableEntite);
                if (relation != null)
                    return relation.TableFille;
                return null;
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

            CInfoRelation relation = FindRelationToValeursChamps(m_strTableEntite);
            if (relation == null)
                throw new Exception(I.T("Cannot find the link between @1 and custom fields|161", m_strTableEntite));
            CStructureTable structure = CStructureTable.GetStructure(CContexteDonnee.GetTypeForTable(m_strTableEntite));
            string strAliasTableChamps = strAliasTableFille;
            string strAliasTableEntite = strAliasTableParente;
            string strSuffixeEntite = strSuffixeParent;
            string strSuffixeChamp = strSuffixeFils;
            string strJoin = "";
            //TESTDBKEYTODO
            int nIdChamp = CChampCustom.GetIdFromDbKey(m_dbKeyChamp);
            strJoin = strAliasTableEntite + "." + structure.ChampsId[0].NomChamp + strSuffixeEntite + "=" +
                strAliasTableChamps + "." + relation.ChampsFille[0] + strSuffixeChamp + " and " +
                strAliasTableChamps + "." + CChampCustom.c_champId + strSuffixeChamp + "=" +
                nIdChamp.ToString() +" and " + strAliasTableChamps + "." + CRelationElementAChamp_ChampCustom.c_champValeurNull + strSuffixeChamp + "=0";
            return strJoin;
        }
    }


    /// ////////////////////////////////////////////////////////////////
    /// <summary>
    /// Relation d'une entité vers une valeur de champ custom qui pointe sur une entité
    /// </summary>
    [AutoExec("Autoexec")]
    [Serializable]
    public class CInfoRelationComposantFiltreEntiteToChampEntite : CInfoRelationComposantFiltre
    {
        private const string c_oldCleChamp = "ETYTOCUSTETY";
        private const string c_cleChamp = "ETCH";
        // TESTDBKEYOK
        private CDbKey m_dbKeyChamp = null;
        private string m_strTableEntite = "";
        private bool m_bFromEntiteToChamp = true;

        public static string GetKeyChamp(CChampCustom champ, bool bFromEntiteToChamp)
        {
            return c_cleChamp + "¤" + champ.IdUniversel + "¤" + (bFromEntiteToChamp ? "1" : "0");
        }

        public static bool DecomposeNomPropriete(string strNomPropriete, ref string strKeyChamp, ref bool bFromEntiteToChamp)
        {
            string strCleDef = "";
            string strPropDef = "";
            if (CDefinitionProprieteDynamique.DecomposeNomProprieteUnique(strNomPropriete, ref strCleDef, ref strPropDef))
            {
                if (strCleDef != CDefinitionProprieteDynamiqueChampCustom.c_strCleType &&
                    strCleDef != CDefinitionProprieteDynamiqueChampCustomFils.c_strCleType && 
                    strCleDef != CDefinitionProprieteDynamiqueChampCustomDisplayValue.c_strCleType )
                    return false;
                strNomPropriete = strPropDef;
            }
            string[] strZones = strNomPropriete.Split('¤');
            if (strZones[0] != c_cleChamp && strZones[0] != c_oldCleChamp)
                return false;
            //Oui, c'en est une
            try
            {
                strKeyChamp = strZones[1];
                bFromEntiteToChamp = strZones[2] == "1";
                return true;
            }
            catch
            { }
            return false;
        }

        /// ////////////////////////////////////////////////////////////////
        public CInfoRelationComposantFiltreEntiteToChampEntite()
        {
        }

        /// ////////////////////////////////////////////////////////////////
        public CInfoRelationComposantFiltreEntiteToChampEntite(CDbKey dbKeyChamp, string strTableEntite, bool bFormEntiteToChamp)
        {
            m_dbKeyChamp = dbKeyChamp;
            m_strTableEntite = strTableEntite;
            m_bFromEntiteToChamp = bFormEntiteToChamp;
        }

        /// ////////////////////////////////////////////////////////////////
        public static void Autoexec()
        {
            CComposantFiltreChamp.FindRelationComplementaire += new FindRelationDelegate(FindRelation);
        }

        /// ////////////////////////////////////////////////////////////////
        public static void FindRelation(string strTable, Type type, ref CInfoRelationComposantFiltre relationTrouvee)
        {
            //TESTDBKEYOK (SC)
            if (relationTrouvee != null)
                return;
            string strKeyChamp = "";
            bool bFromEntiteToChamp = false;
            if (DecomposeNomPropriete(strTable, ref strKeyChamp, ref bFromEntiteToChamp))
            {
                string strTableEntite = "";
                if (!bFromEntiteToChamp)
                {
                    strTableEntite = FindRelationToValeursChamps(CContexteDonnee.GetNomTableForType(type)).TableParente;
                }
                else
                    strTableEntite = CContexteDonnee.GetNomTableForType(type);
                
                int nTmp;
                //Si strKeyChamp est un int, il s'agit d'un id de champ custom
                //ce cas peut se présenter lors de la relecture d'un filtre sous forme de
                //test par exemple dans une fonction ObjectList
                if (int.TryParse(strKeyChamp, out nTmp))
                {
                    //TESTDBKEYOK (SC)
                    strKeyChamp = CDbKey.GetNewDbKeyOnIdAUtiliserPourCeuxQuiNeGerentPasLeDbKey(nTmp).StringValue;
                }
                relationTrouvee = new CInfoRelationComposantFiltreEntiteToChampEntite(CDbKey.CreateFromStringValue(strKeyChamp), strTableEntite, bFromEntiteToChamp);
            }
        }

        /// ////////////////////////////////////////////////////////////////
        public override bool IsRelationFille
        {
            get
            {
                //SC 05/09/2016 : si de l'entité vers le champ, c'est une relation fille,
                //Sinon c'est une relation parente
                return m_bFromEntiteToChamp;
            }
        }

        /// ////////////////////////////////////////////////////////////////
        private int GetNumVersion()
        {
            //return 0;
            return 1; // Passage de Id Champ à DbKey
        }

        /// ////////////////////////////////////////////////////////////////
        public override CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            //TESTDBKEYTODO
            if (nVersion < 1)
                serializer.ReadDbKeyFromOldId(ref m_dbKeyChamp, typeof(CChampCustom));
            else
                serializer.TraiteDbKey(ref m_dbKeyChamp);
            serializer.TraiteString(ref m_strTableEntite);
            serializer.TraiteBool(ref m_bFromEntiteToChamp);

            return result;
        }


        /// ////////////////////////////////////////////////////////////////
        public override string RelationKey
        {
            get
            {
                return c_cleChamp + "¤" + (m_dbKeyChamp != null ? m_dbKeyChamp.StringValue : "") + "¤" + (m_bFromEntiteToChamp ? "1" : "0");
            }
        }

        private static Dictionary<string, CInfoRelation> m_cacheRelationsToChampsCustom = new Dictionary<string, CInfoRelation>();

        internal static CInfoRelation FindRelationToValeursChamps(string strTableEntiteOuValeurs)
        {
            if (m_cacheRelationsToChampsCustom.ContainsKey(strTableEntiteOuValeurs))
                return m_cacheRelationsToChampsCustom[strTableEntiteOuValeurs];
            //Trouve le nom de la table qui contient les valeurs
            foreach (CInfoRelation relation in CContexteDonnee.GetListeRelationsTable(strTableEntiteOuValeurs))
            {
                if (relation.TableParente == strTableEntiteOuValeurs || relation.TableFille == strTableEntiteOuValeurs)
                {
                    Type tpAVerifier = CContexteDonnee.GetTypeForTable(relation.TableFille);
                    if (tpAVerifier != null && typeof(CRelationElementAChamp_ChampCustom).IsAssignableFrom(tpAVerifier))
                    {
                        m_cacheRelationsToChampsCustom[strTableEntiteOuValeurs] = relation;
                        return relation;
                    }
                }
            }
            throw new Exception(I.T("Can not find link between @1 and custom fields|161", strTableEntiteOuValeurs));
        }


        /// ////////////////////////////////////////////////////////////////
        public override string TableParente
        {
            get
            {
                //SC 05/09/2016, la table parente est toujours la table entité,
                //C'est le fait que ce soit ou non un relation fille qui impacte
                //les requêtes
                return m_strTableEntite;
            }
        }

        /// ////////////////////////////////////////////////////////////////
        public override string TableFille
        {
            get
            {
                //SC 05/09/2016, la table fille est toujours la table de relations,
                //C'est le fait que ce soit ou non un relation fille qui impacte
                //les requêtes
                CInfoRelation relation = FindRelationToValeursChamps(m_strTableEntite);
                if (relation != null)
                    return relation.TableFille;
                return null;
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
            CInfoRelation relation = FindRelationToValeursChamps(m_strTableEntite);
            if (relation == null)
                throw new Exception(I.T("Cannot find the link between @1 and custom fields|161", m_strTableEntite));
            CStructureTable structure = CStructureTable.GetStructure(CContexteDonnee.GetTypeForTable(m_strTableEntite));
            string strAliasTableChamps = strAliasTableFille;
            string strAliasTableEntite = strAliasTableParente;
            string strSuffixeEntite = strSuffixeParent;
            string strSuffixeChamp = strSuffixeFils;
            string strJoin = "";
            //TESTDBKEYOK 
            //Testé sur reprise ancien DbKey avec Id
            //et sur filtre avec DbKey Universal ID
            int nIdChamp = CChampCustom.GetIdFromDbKey(m_dbKeyChamp);
            strJoin = strAliasTableEntite + "." + structure.ChampsId[0].NomChamp + strSuffixeEntite + "=" +
                 strAliasTableChamps + "." + relation.ChampsFille[0] + strSuffixeChamp + " and " +
                 strAliasTableChamps + "." + CChampCustom.c_champId + strSuffixeChamp + "=" +
                 nIdChamp.ToString();
            return strJoin;
        }

    }

    /// ////////////////////////////////////////////////////////////////
    /// <summary>
    /// Relation d'un champ vers un entite
    /// </summary>
    [AutoExec("Autoexec")]
    [Serializable]
    public class CInfoRelationComposantFiltreChampToEntite : CInfoRelationComposantFiltre
    {
        private const string c_oldCleChamp = "CUSTTOETY";
        private const string c_cleChamp = "CHET";

        public static string GetKeyChamp(CChampCustom champ, bool bFromEntiteToChamp)
        {
            Type tpSource = champ.Role.TypeAssocie;
            return c_cleChamp + "¤" +
                champ.TypeObjetDonnee.ToString().Replace(".", "@") + "¤" +
                tpSource.ToString().Replace(".", "@") + "¤" +
                (bFromEntiteToChamp ? "1" : "0");
        }


        private Type m_typeValeurChamp = null;
        private Type m_typeElementAChamps = null;
        private string m_strTableValeursChamps = "";
        private bool m_bFromEntiteToChamp = false;
        /// ////////////////////////////////////////////////////////////////
        public CInfoRelationComposantFiltreChampToEntite()
        {
        }

        /// ////////////////////////////////////////////////////////////////
        public CInfoRelationComposantFiltreChampToEntite(Type typeValeurChamps,
            Type typeElementAChamps,
            string strTableValeursChamps,
            bool bFromEntiteToChamp)
        {
            m_typeValeurChamp = typeValeurChamps;
            m_typeElementAChamps = typeElementAChamps;
            m_strTableValeursChamps = strTableValeursChamps;
            m_bFromEntiteToChamp = bFromEntiteToChamp;
        }

        /// ////////////////////////////////////////////////////////////////
        public static void Autoexec()
        {
            CComposantFiltreChamp.FindRelationComplementaire += new FindRelationDelegate(FindRelation);
        }

        /// ////////////////////////////////////////////////////////////////
        public static void FindRelation(string strTable, Type type, ref CInfoRelationComposantFiltre relationTrouvee)
        {
            if (relationTrouvee != null)
                return;
            string strPropDef = "";
            string strCleDef = "";
            if (CDefinitionProprieteDynamique.DecomposeNomProprieteUnique(strTable, ref strCleDef, ref strPropDef))
            {
                if (strCleDef != CDefinitionProprieteDynamiqueChampCustom.c_strCleType &&
                    strCleDef != CDefinitionProprieteDynamiqueChampCustomFils.c_strCleType)
                    return;
                strTable = strPropDef;
            }
            string[] strZones = strTable.Split('¤');
            if (strZones[0] != c_cleChamp && strZones[0] != c_oldCleChamp)
                return;
            //Oui, c'en est une
            try
            {
                Type tpValeurChamp = CActivatorSurChaine.GetType(strZones[1].Replace("@", "."));
                Type tpElementAChamp = CActivatorSurChaine.GetType(strZones[2].Replace("@", "."));
                bool bFromEntiteToChamp = strZones[3] == "1";
                string strTableChamps = "";
                if (bFromEntiteToChamp)
                {
                    CInfoRelation relation = CInfoRelationComposantFiltreEntiteToChampEntite.FindRelationToValeursChamps(CContexteDonnee.GetNomTableForType(tpElementAChamp));
                    strTableChamps = relation.TableFille;
                }
                else
                    strTableChamps = CContexteDonnee.GetNomTableForType(type);
                relationTrouvee = new CInfoRelationComposantFiltreChampToEntite(tpValeurChamp, tpElementAChamp, strTableChamps, bFromEntiteToChamp);
            }
            catch { }
        }

        /// ////////////////////////////////////////////////////////////////
        public override bool IsRelationFille
        {
            get
            {
                return m_bFromEntiteToChamp;
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

            serializer.TraiteType(ref m_typeValeurChamp);
            serializer.TraiteType(ref m_typeElementAChamps);
            serializer.TraiteString(ref m_strTableValeursChamps);
            serializer.TraiteBool(ref m_bFromEntiteToChamp);

            return result;
        }


        /// ////////////////////////////////////////////////////////////////
        public override string RelationKey
        {
            get
            {
                return c_cleChamp + "¤" +
                    m_typeValeurChamp.ToString().Replace(".", "@") + "¤" +
                    m_typeElementAChamps.ToString().Replace(".", "@") + "¤" +
                    (m_bFromEntiteToChamp ? "1" : "0");
            }
        }


        /// ////////////////////////////////////////////////////////////////
        public override string TableFille
        {
            get
            {
                return m_strTableValeursChamps;
            }
        }

        /// ////////////////////////////////////////////////////////////////
        public override string TableParente
        {
            get
            {
                return CContexteDonnee.GetNomTableForType(m_typeValeurChamp);
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
            //Identifie le type parent
            Type tpValeursChamps = CContexteDonnee.GetTypeForTable(m_strTableValeursChamps);
            if (!typeof(CRelationElementAChamp_ChampCustom).IsAssignableFrom(tpValeursChamps))
                throw new Exception(I.T("Incorrect link from a custom field|162"));
            CStructureTable structure = CStructureTable.GetStructure(m_typeValeurChamp);
            string strJoin = strAliasTableFille + "." + CRelationElementAChamp_ChampCustom.c_champValeurInt + strSuffixeFils + "=" +
                strAliasTableParente + "." + structure.ChampsId[0].NomChamp + strSuffixeParent;
            return strJoin;
        }

        /// ////////////////////////////////////////////////////////////////
        public static bool IsRelationFromChampToEntite(string strSuiteTmp)
        {
            if (strSuiteTmp.Length > c_cleChamp.Length &&
                (strSuiteTmp.Substring(0, c_cleChamp.Length) == c_cleChamp ||
                strSuiteTmp.Substring(0, c_oldCleChamp.Length) == c_oldCleChamp)
                &&
                strSuiteTmp.Split('¤').Length >= 4)
                return true;
            return false;
        }

    }

    /// ////////////////////////////////////////////////////////////////
    [AutoExec("Autoexec")]
    public class CInterperteurProprieteDynamiqueChampCustom :
        IInterpreteurProprieteDynamique,
        IDependanceListeObjetsDonneesReader
    {
        //------------------------------------------------------------
        public static void Autoexec()
        {
            CGestionnaireDependanceListeObjetsDonneesReader.RegisterReader(CDefinitionProprieteDynamiqueChampCustom.c_strCleType,
                typeof(CInterperteurProprieteDynamiqueChampCustom));
        }

        //------------------------------------------------------------
        public virtual bool ShouldIgnoreForSetValue(string strPropriete)
        {
            string strKeyChamp = "";
            if (!CInfoRelationEntiteToValeurChampCustom.DecomposeNomPropriete(strPropriete, ref strKeyChamp))
            {
                bool bFromEntiteToChamp = false;
                if (!CInfoRelationComposantFiltreEntiteToChampEntite.DecomposeNomPropriete(strPropriete, ref strKeyChamp, ref bFromEntiteToChamp))
                {
                    if (CInfoRelationComposantFiltreChampToEntite.IsRelationFromChampToEntite(strPropriete))
                    //Lien déjà traité par GetValeurChamp
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //------------------------------------------------------------
        public virtual CResultAErreur GetValue(object objet, string strPropriete)
        {
            CResultAErreur result = CResultAErreur.True;
            result.Data = null;

            string strKeyChamp = "";
            //TESTDBKEYOK (SC)
            if (!CInfoRelationEntiteToValeurChampCustom.DecomposeNomPropriete(strPropriete, ref strKeyChamp))
            {
                bool bFromEntiteToChamp = false;
                if (!CInfoRelationComposantFiltreEntiteToChampEntite.DecomposeNomPropriete(strPropriete, ref strKeyChamp, ref bFromEntiteToChamp))
                {
                    if (CInfoRelationComposantFiltreChampToEntite.IsRelationFromChampToEntite(strPropriete))
                    //Lien déjà traité par GetValeurChamp
                    {
                        result.Data = objet;
                        return result;
                    }
                    result.EmpileErreur(I.T("Bad custom field (@1)|20024", strPropriete));
                    return result;
                }
                else
                {
                    //C'est une relation vers un champ custom entité.
                    //Si relation du champ vers l'entité, c'est une liste !
                    if (!bFromEntiteToChamp)
                    {
                        CObjetDonnee objetAChamp = objet as CObjetDonnee;
                        if (objetAChamp == null)
                        {
                            result.EmpileErreur(I.T("Can not evaluate Custom field on @1|20033", DynamicClassAttribute.GetNomConvivial(objet.GetType())));
                            return result;
                        }
                        CContexteDonnee contexte = objetAChamp.ContexteDonnee;
                        CChampCustom champ = new CChampCustom(contexte);
                        if (!champ.ReadIfExists(CDbKey.CreateFromStringValue(strKeyChamp)))
                        {
                            result.EmpileErreur(I.T("Custom field @1 doesn't exists|20025", strPropriete));
                            return result;
                        }
                        Type tp = champ.Role.TypeAssocie;
                        try
                        {
                            IObjetDonneeAChamps elt = (IObjetDonneeAChamps)Activator.CreateInstance(tp, new object[] { contexte });
                            CRelationElementAChamp_ChampCustom rel = elt.GetNewRelationToChamp();
                            CListeObjetsDonnees liste = new CListeObjetsDonnees(contexte, tp);
                            liste.Filtre = new CFiltreDataAvance(
                                elt.GetNomTable(),
                                rel.GetNomTable() + "." +
                                CRelationElementAChamp_ChampCustom.c_champValeurString + "=@1 and " +
                                rel.GetNomTable() + "." +
                                CRelationElementAChamp_ChampCustom.c_champValeurInt + "=@2 and " +
                                rel.GetNomTable() + "." +
                                CChampCustom.c_champId + "=@3",
                                objetAChamp.GetType().ToString(),
                                ((CObjetDonneeAIdNumerique)objetAChamp).Id,
                                champ.Id);
                            result.Data = liste.ToArray(tp);
                        }
                        catch
                        {
                            result.Data = null;
                        }
                        return result;
                    }
                }
            }
            IElementAChamps eltAChamps = objet as IElementAChamps;
            if (eltAChamps == null)
                return result;
            //TESTDBKEYOK
            result.Data = eltAChamps.GetValeurChamp(strKeyChamp);
            return result;
        }

        /// //////////////////////////////////////////////////////
        public virtual CResultAErreur SetValue(object elementToModif, string strPropriete, object nouvelleValeur)
        {
            CResultAErreur result = CResultAErreur.True;
            //Si modif de champ custom

            //TESTDBKEYTODO
            string strCleChamp = "";
            if (!CInfoRelationEntiteToValeurChampCustom.DecomposeNomPropriete(strPropriete, ref strCleChamp))
            {
                bool bFromEntiteToChamp = false;
                if (!CInfoRelationComposantFiltreEntiteToChampEntite.DecomposeNomPropriete(strPropriete, ref strCleChamp, ref bFromEntiteToChamp))
                {
                    if (CInfoRelationComposantFiltreChampToEntite.IsRelationFromChampToEntite(strPropriete))
                    //Lien déjà traité par SetValeurChamp
                    {
                        return result;
                    }
                    result.EmpileErreur(I.T("Bad custom field (@1)|20024", strPropriete));
                    return result;
                }
                if (!bFromEntiteToChamp)
                {
                    result.EmpileErreur(I.T("Bad custom field (@1)|20024", strPropriete));
                    return result;
                }
            }
            if (!(elementToModif is IElementAChamps))
            {
                result.EmpileErreur(I.T("@1 : Incorrect custom field or invalid target|20019", strPropriete));
                return result;
            }
            result = ((IElementAChamps)elementToModif).SetValeurChamp(strCleChamp, nouvelleValeur);
            return result;
        }

        public class COptimiseurProprieteDynamiqueChampCustom : IOptimiseurGetValueDynamic
        {
            private bool m_bIsRelationFromChampToEntite = false;
            private bool m_bIsRelationFromEntiteToChamp = false;
            private int m_nIdChamp = -1;
            private CDbKey m_dbKeyChamp = null;
            Type m_typeElements = null;

            public COptimiseurProprieteDynamiqueChampCustom(
                CDbKey dbKeyChamp,
                Type typeElements,
                bool bFromChampToEntite,
                bool bFromEntiteToChamp)
            {
                m_typeElements = typeElements;
                //TESTDBKEYTODO
                m_nIdChamp = CChampCustom.GetIdFromDbKey(dbKeyChamp);
                m_dbKeyChamp = dbKeyChamp;
                m_bIsRelationFromChampToEntite = bFromChampToEntite;
                m_bIsRelationFromEntiteToChamp = bFromEntiteToChamp;
            }

            public object GetValue(object objet)
            {
                //TESTDBKEYTODO
                if (m_bIsRelationFromChampToEntite)//Déja traité par getValeurChamp
                    return objet;
                if (!m_bIsRelationFromEntiteToChamp)
                {
                    CObjetDonnee objetAChamp = objet as CObjetDonnee;
                    if (objetAChamp == null)
                    {
                        return null;
                    }
                    CContexteDonnee contexte = objetAChamp.ContexteDonnee;
                    CChampCustom champ = new CChampCustom(contexte);
                    try
                    {
                        IObjetDonneeAChamps elt = (IObjetDonneeAChamps)Activator.CreateInstance(m_typeElements, new object[] { contexte });
                        CRelationElementAChamp_ChampCustom rel = elt.GetNewRelationToChamp();
                        CListeObjetsDonnees liste = new CListeObjetsDonnees(contexte, m_typeElements);
                        liste.Filtre = new CFiltreDataAvance(
                            elt.GetNomTable(),
                            rel.GetNomTable() + "." +
                            CRelationElementAChamp_ChampCustom.c_champValeurString + "=@1 and " +
                            rel.GetNomTable() + "." +
                            CRelationElementAChamp_ChampCustom.c_champValeurInt + "=@2 and " +
                            rel.GetNomTable() + "." +
                            CChampCustom.c_champId + "=@3",
                            objetAChamp.GetType().ToString(),
                            ((CObjetDonneeAIdNumerique)objetAChamp).Id,
                            m_nIdChamp);
                        return liste.ToArray(m_typeElements);
                    }
                    catch
                    {
                    }
                    return null;
                }

                IElementAChamps eltAChamps = objet as IElementAChamps;
                if (eltAChamps == null)
                    return null;
                return eltAChamps.GetValeurChamp(m_dbKeyChamp.StringValue);
            }

            public Type GetTypeRetourne()
            {
                return m_typeElements;
            }
        }

        //---------------------------------------------------------------------------------------
        protected virtual IOptimiseurGetValueDynamic GetNewOptimiseur(
            CDbKey dbKeyChamp,
            Type tp,
            bool bFromChampToEntite,
            bool bFromEntiteToChamp)
        {
            return new COptimiseurProprieteDynamiqueChampCustom(dbKeyChamp, tp, bFromChampToEntite, bFromEntiteToChamp);
        }

        //---------------------------------------------------------------------------------------
        public IOptimiseurGetValueDynamic GetOptimiseur(Type tp, string strPropriete)
        {
            //TESTDBKEYTODO
            bool bFromChampToEntite = false;
            bool bFromEntiteToChamp = false;
            string strCleChamp = "";
            Type typeElements = null;
            if (!CInfoRelationEntiteToValeurChampCustom.DecomposeNomPropriete(strPropriete, ref strCleChamp))
            {
                if (!CInfoRelationComposantFiltreEntiteToChampEntite.DecomposeNomPropriete(strPropriete, ref strCleChamp, ref bFromEntiteToChamp))
                {
                    if (CInfoRelationComposantFiltreChampToEntite.IsRelationFromChampToEntite(strPropriete))
                    //Lien déjà traité par GetValeurChamp
                    {
                        return GetNewOptimiseur(
                            CDbKey.CreateFromStringValue(strCleChamp), tp, true, bFromEntiteToChamp);
                    }
                    return null;//Ce n'est pas normal
                }
                else
                {
                    CChampCustom champ = new CChampCustom(CContexteDonneeSysteme.GetInstance());
                    if (!champ.ReadIfExists(CDbKey.CreateFromStringValue(strCleChamp)))
                    {
                        strCleChamp = "";
                    }
                    else
                    {
                        //C'est une relation vers un champ custom entité.
                        //Si relation du champ vers l'entité, c'est une liste !
                        if (!bFromEntiteToChamp)
                        {
                            typeElements = champ.Role.TypeAssocie;
                        }
                        else
                            typeElements = champ.TypeDonnee.TypeDotNetNatif;
                    }
                }
            }
            else
            {
                bFromEntiteToChamp = true;
                CChampCustom champ = new CChampCustom(CContexteDonneeSysteme.GetInstance());
                if (!champ.ReadIfExists(CDbKey.CreateFromStringValue(strCleChamp)))
                {
                    strCleChamp = "";
                }
                else
                    typeElements = champ.TypeDonnee.TypeDotNetNatif;
            }

            return GetNewOptimiseur(
                CDbKey.CreateFromStringValue(strCleChamp), typeElements, bFromChampToEntite, bFromEntiteToChamp);
        }

        //---------------------------------------------------------------------------------------
        public void ReadArbre(CListeObjetsDonnees listeSource, CListeObjetsDonnees.CArbreProps arbre, List<string> lstPaquetsALire)
        {
            //TESTDBKEYOK
            string strKeyChamp = null;
            string strPropriete = "";
            string strCle = "";
            if (!CDefinitionProprieteDynamique.DecomposeNomProprieteUnique(arbre.ProprietePrincipale, ref strCle, ref strPropriete))
                return;
            if (listeSource.Count == 0)
                return;
            bool bFromEntiteToChamp = false;

            HashSet<string> lstIdsChampsALire = new HashSet<string>();
            bool bIsFirst = true;
            bool bIsValeurSimple = false;
            if (CInfoRelationEntiteToValeurChampCustom.DecomposeNomPropriete(strPropriete, ref strKeyChamp))//C'est une valeur de champ
            {
                if (arbre.ArbreParent != null)//Lit tous les champs fils (valeurs atomiques) d'un coup !
                {
                    foreach (CListeObjetsDonnees.CArbreProps arbreFils in arbre.ArbreParent.SousArbres)
                    {
                        if (CInfoRelationEntiteToValeurChampCustom.DecomposeNomPropriete(arbreFils.ProprietePrincipale, ref strKeyChamp))
                        {
                            if (arbreFils == arbre && !bIsFirst)//On ne lit que pour le premier sous arbre, 
                            {
                                lstIdsChampsALire.Clear();
                                //les autres sont lus par le premier
                                break;
                            }
                            bIsValeurSimple = true;

                            bIsFirst = false;
                            lstIdsChampsALire.Add(strKeyChamp);
                        }
                    }
                }
            }
            if (!CInfoRelationEntiteToValeurChampCustom.DecomposeNomPropriete(strPropriete, ref strKeyChamp))
            {
                if (!CInfoRelationComposantFiltreEntiteToChampEntite.DecomposeNomPropriete(strPropriete, ref strKeyChamp, ref bFromEntiteToChamp))
                {
                    if (CInfoRelationComposantFiltreChampToEntite.IsRelationFromChampToEntite(strPropriete))
                    {
                        //Lecture des données du champ vers l'entité
                        HashSet<int> idsTraites = new HashSet<int>();
                        List<string> lstPaquets = new List<string>();
                        CRelationElementAChamp_ChampCustom relExemple = listeSource[0] as CRelationElementAChamp_ChampCustom;
                        if (relExemple == null)
                            return;
                        Type typeElements = relExemple.ChampCustom.TypeDonnee.TypeDotNetNatif;
                        if (typeElements == null || !typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(typeElements))
                            return;
                        string strNomTable = CContexteDonnee.GetNomTableForType(typeElements);
                        if (strNomTable == null)
                            return;
                        DataTable table = listeSource.ContexteDonnee.GetTableSafe(strNomTable);
                        if (table == null)
                            return;
                        string strChampId = table.PrimaryKey[0].ColumnName;
                        StringBuilder strPaquet = new StringBuilder();
                        #region Création des paquets de lecture
                        foreach (CRelationElementAChamp_ChampCustom rel in listeSource)
                        {
                            if (rel.ValeurInt >= 0 && !idsTraites.Contains(rel.ValeurInt))
                            {
                                idsTraites.Add(rel.ValeurInt);
                                strPaquet.Append(rel.ValeurInt);
                                strPaquet.Append(',');
                                if (idsTraites.Count % CListeObjetsDonnees.c_nNbLectureParLotFils == 0)
                                {
                                    strPaquet.Remove(strPaquet.Length - 1, 1);
                                    lstPaquets.Add(strPaquet.ToString());
                                    strPaquet = new StringBuilder();
                                }
                            }
                        }
                        if (strPaquet.Length > 0)
                        {
                            strPaquet.Remove(strPaquet.Length - 1, 1);
                            lstPaquets.Add(strPaquet.ToString());
                        }
                        #endregion


                        foreach (string strPack in lstPaquets)
                        {
                            CListeObjetsDonnees lst = new CListeObjetsDonnees(listeSource.ContexteDonnee, typeElements);
                            lst.Filtre = new CFiltreData(strChampId + " in (" + strPack + ")");
                            lst.AssureLectureFaite();
                            lst.ReadDependances(arbre);
                        }
                        return;

                    }
                }
                else
                {
                    //C'est une relation vers un champ custom entité.
                    //Si relation du champ vers l'entité, c'est une liste !
                    if (!bFromEntiteToChamp)
                    {
                        //Ce n'est pas possible
                        return;
                    }
                    lstIdsChampsALire.Add(strKeyChamp);
                }
            }

            StringBuilder blIdsChamps = new StringBuilder();
            foreach (string strKey in lstIdsChampsALire)
            {
                int nId = CChampCustom.GetIdFromDbKey(CDbKey.CreateFromStringValue(strKey));
                if (nId >= 0)
                {
                    blIdsChamps.Append(nId);
                    blIdsChamps.Append(',');
                }
            }
            if (blIdsChamps.Length > 0)
            {
                blIdsChamps.Remove(blIdsChamps.Length - 1, 1);

                //Lecture des valeurs simples
                IObjetDonneeAChamps elt = listeSource[0] as IObjetDonneeAChamps;
                if (elt == null)
                    return;
                string strTableChamps = elt.GetNomTableRelationToChamps();
                Type typeValeurs = CContexteDonnee.GetTypeForTable(strTableChamps);
                if (typeValeurs == null)
                    return;

                CListeObjetsDonnees lstValsChamps = new CListeObjetsDonnees(listeSource.ContexteDonnee, typeValeurs);
                if (lstPaquetsALire == null)
                    lstPaquetsALire = listeSource.GetPaquetsPourLectureFils(elt.GetChampId(), null);
                foreach (string strPaquet in lstPaquetsALire)
                {
                    CListeObjetsDonnees lstValeurs = new CListeObjetsDonnees(listeSource.ContexteDonnee, typeValeurs);
                    lstValeurs.Filtre = new CFiltreData(CChampCustom.c_champId + " in (" + blIdsChamps.ToString() + ") and " +
                        elt.GetChampId() + " in " + strPaquet);
                    lstValeurs.ModeSansTri = true;
                    lstValeurs.AssureLectureFaite();
                    if (!bIsValeurSimple)
                        lstValeurs.ReadDependances(arbre);
                }
                List<int> lstIds = new List<int>();
                foreach (string cleALire in lstIdsChampsALire)
                {
                    CChampCustom champ = new CChampCustom(listeSource.ContexteDonnee);
                    if(champ.ReadIfExists(CDbKey.CreateFromStringValue(cleALire)))
                        lstIds.Add(champ.Id);
                }
                foreach (IObjetDonneeAChamps obj in listeSource)
                    CUtilElementAChamps.SetChampsLus(obj, lstIds.ToArray());
            }
        }
    }

    [AutoExec("Autoexec")]
    public class CFournisseurProprietesDynamiqueChampCustom :
        IFournisseurProprieteDynamiquesSimplifie
    {

        public static void Autoexec()
        {
            CFournisseurGeneriqueProprietesDynamiques.RegisterTypeFournisseur(new CFournisseurProprietesDynamiqueChampCustom());
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

                    CDefinitionProprieteDynamiqueChampCustom def = new CDefinitionProprieteDynamiqueChampCustom(champ);
                    if (champ.Categorie.Trim() != "")
                        def.Rubrique = champ.Categorie;
                    else
                        def.Rubrique = I.T("Complementary informations|59");
                    lstProps.Add(def);
                }
            }
            return lstProps.ToArray();

        }

    }
}
