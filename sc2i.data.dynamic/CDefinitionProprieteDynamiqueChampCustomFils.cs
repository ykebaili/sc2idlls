using System;

using sc2i.common;
using sc2i.expression;
using System.Collections.Generic;

namespace sc2i.data.dynamic
{
    /// <summary>
    /// Relation entre une entité et un champ custom dont cett entité est valeur
    /// (par exemple, si sur site il y a un champ custom qui renvoie un acteur, sur acteur, il y a 
    /// un champ qui renvoie une liste de sites. Ce champ est un CDefinitionProprietedynamiquechampCustomFils
    /// </summary>
    [Serializable]
    [AutoExec("Autoexec")]
    public class CDefinitionProprieteDynamiqueChampCustomFils : CDefinitionProprieteDynamique
    {
        public const string c_strCleType = "CF";

        //TESTDBKEYTODO : remplacer m_nIdChamp par un DbKeyChamp
        protected CDbKey m_dbKeyChamp = null;

        /// //////////////////////////////////////////////////////
        public CDefinitionProprieteDynamiqueChampCustomFils()
            : base()
        {
        }

        //-----------------------------------------------
        public static new void Autoexec()
        {
            CInterpreteurProprieteDynamique.RegisterTypeDefinition(c_strCleType, typeof(CInterperteurProprieteDynamiqueChampCustom));
        }

        /// //////////////////////////////////////////////////////
        private static string GetKeyChamp(CChampCustom champ)
        {
            if (champ.TypeDonneeChamp.TypeDonnee == sc2i.data.dynamic.TypeDonnee.tObjetDonneeAIdNumeriqueAuto)
                return CInfoRelationComposantFiltreChampToEntite.GetKeyChamp(champ, true) + "." +
                    CDefinitionProprieteDynamique.c_strCaractereStartCleType + c_strCleType +
                    CDefinitionProprieteDynamique.c_strCaractereEndCleType +
                CInfoRelationComposantFiltreEntiteToChampEntite.GetKeyChamp(champ, false);

            return champ.Nom.Replace(" ", "_");
        }

        /// //////////////////////////////////////////////////////
        public CDefinitionProprieteDynamiqueChampCustomFils(CChampCustom champ)
            : base(
            champ.LibellePourObjetParent.Replace(" ", "_"),
            GetKeyChamp(champ),
            new CTypeResultatExpression(champ.Role.TypeAssocie, true),
            true,
            true)
        {
            m_dbKeyChamp = champ.DbKey;
        }

        //-----------------------------------------------
        public override string CleType
        {
            get { return c_strCleType; }
        }

        /// //////////////////////////////////////////////////////
        public CDbKey KeyChamp
        {
            get
            {
                return m_dbKeyChamp;
            }
        }

        public override void CopyTo(CDefinitionProprieteDynamique def)
        {
            base.CopyTo(def);
            ((CDefinitionProprieteDynamiqueChampCustomFils)def).m_dbKeyChamp = m_dbKeyChamp;
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
            SetNomPropriete(CDefinitionProprieteDynamiqueChampCustom.ConvertNomProprieteFromIdToDbKey(NomPropriete));
        }
    }

    [AutoExec("Autoexec")]
    public class CFournisseurProprietesDynamiquesChampCustomFils : IFournisseurProprieteDynamiquesSimplifie
    {

        public static void Autoexec()
        {
            CFournisseurGeneriqueProprietesDynamiques.RegisterTypeFournisseur(new CFournisseurProprietesDynamiquesChampCustomFils());
        }


        public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet, CDefinitionProprieteDynamique defParente)
        {
            List<CDefinitionProprieteDynamique> lstProps = new List<CDefinitionProprieteDynamique>();
            if (objet == null)
                return lstProps.ToArray();
            Type tp = objet.TypeAnalyse;
            if (tp == null)
                return lstProps.ToArray();
            if (!sc2i.multitiers.client.C2iFactory.IsInit())
                return lstProps.ToArray();

            CContexteDonnee contexte = CContexteDonneeSysteme.GetInstance();
            //Liens sur champs custom
            CListeObjetsDonnees listeChamps = new CListeObjetsDonnees(contexte, typeof(CChampCustom));
            listeChamps.Filtre = new CFiltreData(
                CChampCustom.c_champTypeObjetDonnee + "=@1",
                tp.ToString());
            foreach (CChampCustom champ in listeChamps)
            {
                CDefinitionProprieteDynamiqueChampCustomFils def = new CDefinitionProprieteDynamiqueChampCustomFils(
                    champ);
                if (champ.Categorie.Trim() != "")
                    def.Rubrique = champ.Categorie;
                else
                    def.Rubrique = I.T("Complementary informations|59");
                lstProps.Add(def);
            }
            return lstProps.ToArray();
        }

    }

    //------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------
    [AutoExec("Autoexec")]
    public class CDependanceListeChampCustomFilsReader : IDependanceListeObjetsDonneesReader
    {
        //------------------------------------------------------------------------------------------
        public static void Autoexec()
        {
            CGestionnaireDependanceListeObjetsDonneesReader.RegisterReader(CDefinitionProprieteDynamiqueChampCustomFils.c_strCleType,
                typeof(CDependanceListeChampCustomFilsReader));
        }

        //------------------------------------------------------------------------------------------
        public void ReadArbre(CListeObjetsDonnees listeSource, CListeObjetsDonnees.CArbreProps arbre, List<string> lstPaquetsALire)
        {
            string strIdChamp = "";
            string strPropriete = "";
            string strCle = "";
            if (listeSource.Count == 0)
                return;
            bool bFromEntiteToChamp = false;
            if (!CDefinitionProprieteDynamique.DecomposeNomProprieteUnique(arbre.ProprietePrincipale, ref strCle, ref strPropriete))
                return;
            if (!CInfoRelationComposantFiltreEntiteToChampEntite.DecomposeNomPropriete(strPropriete, ref strIdChamp, ref bFromEntiteToChamp))
            {
                //c'est une relation vers des valeurs de champ qui pointent sur cette valeur.
                //Ignore, et traite tout dans le composant suivant qui est un Entité to champ à l'envers
                listeSource.ReadDependances(arbre);
                return;
            }
            //On a affaire à une liste source qui contient des valeurs qui sont pointées
            //par des valeurs de champs, et on veut les éléments qui pointent sur ces valeurs de champs

            CChampCustom champ = new CChampCustom(listeSource.ContexteDonnee);
            CDbKey dbKeyChamp = CDbKey.CreateFromStringValue(strIdChamp);
            if (!champ.ReadIfExists(dbKeyChamp))
                return;
            //Vérifie que la liste source est bien du type de données du champ custom
            if (champ.TypeDonnee.TypeDotNetNatif != listeSource.TypeObjets)
                return;
            IObjetDonneeAIdNumerique obj = listeSource[0] as IObjetDonneeAIdNumerique;
            if ( obj == null )
                return;
            string strChampId = obj.GetChampId();
            if ( lstPaquetsALire == null )
                lstPaquetsALire = listeSource.GetPaquetsPourLectureFils(strChampId, null);
            
            //Trouve le type des éléments à champs
            Type typeElementsFinaux = champ.Role.TypeAssocie;
            //Trouve le type des relations aux elementAchamp_ChampCustom
            IObjetDonneeAChamps elt = Activator.CreateInstance(typeElementsFinaux, new object[] { listeSource.ContexteDonnee }) as IObjetDonneeAChamps;
            if ( elt == null )
                return;
            string strNomTableRelToChamp = elt.GetNomTableRelationToChamps();
            Type tpRelToChamp = CContexteDonnee.GetTypeForTable(strNomTableRelToChamp);
            if (tpRelToChamp == null)
                return;
            foreach (string strPaquet in lstPaquetsALire)
            {
                CListeObjetsDonnees lst = new CListeObjetsDonnees(listeSource.ContexteDonnee, typeElementsFinaux);
                
                //TESTDBKEYTODO 
                string strChampCustomId = dbKeyChamp.IsNumericalId() ? CChampCustom.c_champId : CObjetDonnee.c_champIdUniversel;

                lst.Filtre = new CFiltreDataAvance(
                    CContexteDonnee.GetNomTableForType(typeElementsFinaux),
                    strNomTableRelToChamp + "." + strChampCustomId + "=@1 and " +
                    strNomTableRelToChamp + "." + CRelationElementAChamp_ChampCustom.c_champValeurInt + " in " +
                    strPaquet,
                    dbKeyChamp.GetValeurInDb());
                lst.AssureLectureFaite();
                lst.ReadDependances(arbre);
            }
        }
    }

    [AutoExec("Autoexec")]
    public class CFournisseurPropInverseChampCustomFils : IFournisseurProprieteDynamiqueInverse
    {
        //----------------------------------------------------------------
        public static void Autoexec()
        {
            CFournisseurProprieteDynamiqueInverse.RegisterFournisseur(typeof(CDefinitionProprieteDynamiqueChampCustomFils), typeof(CFournisseurPropInverseChampCustomFils));
        }

        //----------------------------------------------------------------
        public CDefinitionProprieteDynamique GetProprieteInverse(Type typePortantLaPropriete, CDefinitionProprieteDynamique def)
        {
            CDefinitionProprieteDynamiqueChampCustomFils defFils = def as CDefinitionProprieteDynamiqueChampCustomFils;
            if (defFils == null)
                return null;
            CChampCustom champ = new CChampCustom(CContexteDonneeSysteme.GetInstance());
            if (champ.ReadIfExists(defFils.KeyChamp))
                return new CDefinitionProprieteDynamiqueChampCustom(champ);
            return null;
        }
    }

}