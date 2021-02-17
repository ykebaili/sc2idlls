using System;
using System.Collections;

using sc2i.common;


using sc2i.expression;
using System.Collections.Generic;
using sc2i.multitiers.client;

namespace sc2i.data.dynamic
{
    /// <summary>
    /// Propriété dynamique créée par une données cumulée.
    /// La propriété contient l'id du type de donnée cumulée, | le numéro de clé
    /// correspondant à l'élément
    /// Format de la propriété : A|B
    /// où A est le type de donnée, et B le numéro de clé
    /// </summary>
    [Serializable]
    [AutoExec("Autoexec")]
    public class CDefinitionProprieteDynamiqueDonneeCumulee : CDefinitionProprieteDynamique//, ITraducteurNomChamps
    {
        private const string c_strCleType = "DC";

        //TESTDBKEYTODO : remplacer m_nIdTypeDonneeCumulee par un DbKeyChamp
        protected CDbKey m_dbKeyTypeDonneeCumulee = null;

        private Hashtable m_tableConversionNoms = null;

        /// //////////////////////////////////////////////////////
        public CDefinitionProprieteDynamiqueDonneeCumulee()
            : base()
        {
            Rubrique = I.T("Cumulated data|163");
        }
        /// //////////////////////////////////////////////////////
        public CDefinitionProprieteDynamiqueDonneeCumulee(CTypeDonneeCumulee td, int nNumeroCle)
            : base(td.Libelle.Replace(" ", "_"),
            td.Id + "|" + nNumeroCle,
                new CTypeResultatExpression(typeof(CDonneeCumulee), true), true, true)
        {
            m_dbKeyTypeDonneeCumulee = td.DbKey;
            Rubrique = I.T("Cumulated data|163");
        }

        /// //////////////////////////////////////////////////////
        public static new void Autoexec()
        {
            CInterpreteurProprieteDynamique.RegisterTypeDefinition(c_strCleType, typeof(CInterpreteurProprieteDynamiqueDonneeCumulee));
        }

        //-----------------------------------------------
        public override string CleType
        {
            get { return c_strCleType; }
        }

        /// //////////////////////////////////////////////////////
        [ExternalReferencedEntityDbKey(typeof(CTypeDonneeCumulee))]
        public CDbKey DbKeyTypeDonnee
        {
            get
            {
                return m_dbKeyTypeDonneeCumulee;
            }
        }

        public override void CopyTo(CDefinitionProprieteDynamique def)
        {
            base.CopyTo(def);
            ((CDefinitionProprieteDynamiqueDonneeCumulee)def).m_dbKeyTypeDonneeCumulee = m_dbKeyTypeDonneeCumulee;
        }

        /// ////////////////////////////////////////
        private int GetNumVersion()
        {
            //return 0;
            return 1; // Passage en Dbkey
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
                serializer.ReadDbKeyFromOldId(ref m_dbKeyTypeDonneeCumulee, typeof(CTypeDonneeCumulee));
            else
                serializer.TraiteDbKey(ref m_dbKeyTypeDonneeCumulee);

            return result;
        }

        /// ////////////////////////////////////////
        public CDefinitionProprieteDynamique GetDefinitionConvertie(CDefinitionProprieteDynamique propriete)
        {
            /*if ( !(propriete is CDefinitionProprieteDynamiqueDonneeCumulee) )
                return propriete;*/
            if (m_tableConversionNoms == null)
                CreateTableConversionNoms();

            //Isole la fin
            string[] strNoms = propriete.Nom.Split('.');
            string strNomProp = strNoms[strNoms.Length - 1];

            string strNewNom = (string)m_tableConversionNoms[strNomProp];
            if (strNewNom == null)
                return null;
            propriete.ChangeNom(propriete.Nom.Substring(0, propriete.Nom.Length - strNomProp.Length) + strNewNom);
            return propriete;
        }

        /// ////////////////////////////////////////
        private void CreateTableConversionNoms()
        {
            m_tableConversionNoms = new Hashtable();
            using (CContexteDonnee contexte = new CContexteDonnee(sc2i.multitiers.client.CSessionClient.GetSessionUnique().IdSession, true, false))
            {
                CTypeDonneeCumulee type = new CTypeDonneeCumulee(contexte);
                if (type.ReadIfExists(m_dbKeyTypeDonneeCumulee))
                {
                    CParametreDonneeCumulee parametre = type.Parametre;
                    foreach (CParametreDonneeCumulee.CNomChampCumule cle in parametre.Cles)
                    {
                        if (cle.NomChamp != "")
                            m_tableConversionNoms[I.T("Key|45") + cle.NumeroChamp.ToString()] = cle.NomChamp;
                    }
                    foreach (CParametreDonneeCumulee.CNomChampCumule valeur in parametre.NomChampsDecimaux)
                    {
                        if (valeur.NomChamp != "")
                            m_tableConversionNoms[I.T("Value|40") + valeur.NumeroChamp.ToString()] = valeur.NomChamp;
                    }
                    foreach (CParametreDonneeCumulee.CNomChampCumule valeur in parametre.NomChampsDates)
                    {
                        if (valeur.NomChamp != "")
                            m_tableConversionNoms[I.T("Date|245") + valeur.NumeroChamp.ToString()] = valeur.NomChamp;
                    }
                    foreach (CParametreDonneeCumulee.CNomChampCumule valeur in parametre.NomChampsTextes)
                    {
                        if (valeur.NomChamp != "")
                            m_tableConversionNoms[I.T("Text|246") + valeur.NumeroChamp.ToString()] = valeur.NomChamp;
                    }

                }
            }
        }

    }


    public class CInterpreteurProprieteDynamiqueDonneeCumulee : IInterpreteurProprieteDynamique
    {
        //------------------------------------------------------------
        public bool ShouldIgnoreForSetValue(string strPropriete)
        {
            return false;
        }

        //------------------------------------------------------------
        public CResultAErreur GetValue(object objet, string strPropriete)
        {
            CResultAErreur result = CResultAErreur.True;
            CObjetDonneeAIdNumerique objetDonnee = objet as CObjetDonneeAIdNumerique;
            if (objetDonnee == null)
                return result;
            //traduit la propriété : Id du type | Numéro de clé
            int nIdType = -1;
            int nNumCle = -1;
            try
            {
                string[] strProps = strPropriete.Split('|');
                nIdType = Int32.Parse(strProps[0]);
                nNumCle = Int32.Parse(strProps[1]);
            }
            catch
            {
                result.EmpileErreur(I.T("Bad cumulated data field @1|20026", strPropriete));
                return result;
            }
            CTypeDonneeCumulee type = new CTypeDonneeCumulee(objetDonnee.ContexteDonnee);
            if (!type.ReadIfExists(nIdType))
            {
                result.EmpileErreur(I.T("Cumulated data type @1 doesn't exists|20027", nIdType.ToString()));
                return result;
            }
            result.Data = type.GetDonneesCumuleesForObjet(objetDonnee);
            return result;
        }

        public CResultAErreur SetValue(object objet, string strPropriete, object valeur)
        {
            CResultAErreur result = CResultAErreur.True;
            result.EmpileErreur(I.T("Forbidden affectation|20034"));
            return result;
        }

        public class COptimiseurProprieteDynamiqueDonneeCumulee : IOptimiseurGetValueDynamic
        {
            private int m_nIdTypeDonnee = -1;

            public COptimiseurProprieteDynamiqueDonneeCumulee(int nIdTypeDonnee)
            {
                m_nIdTypeDonnee = nIdTypeDonnee;
            }

            public object GetValue(object objet)
            {
                CObjetDonneeAIdNumerique objetDonnee = objet as CObjetDonneeAIdNumerique;
                if (objetDonnee == null)
                    return null;

                CTypeDonneeCumulee type = new CTypeDonneeCumulee(objetDonnee.ContexteDonnee);
                if (type.ReadIfExists(m_nIdTypeDonnee))
                {
                    return type.GetDonneesCumuleesForObjet(objetDonnee);
                }
                return null;
            }

            public Type GetTypeRetourne()
            {
                return typeof(CDonneeCumulee);
            }
        }


        public IOptimiseurGetValueDynamic GetOptimiseur(Type tp, string strPropriete)
        {
            int nIdType = -1;
            int nNumCle = -1;
            try
            {
                string[] strProps = strPropriete.Split('|');
                nIdType = Int32.Parse(strProps[0]);
                nNumCle = Int32.Parse(strProps[1]);
            }
            catch
            {
            }
            return new COptimiseurProprieteDynamiqueDonneeCumulee(nIdType);
        }

    }

    [AutoExec("Autoexec")]
    public class CFournisseurProprieteDynamiqueDonneeCumulee : IFournisseurProprieteDynamiquesSimplifie
    {
        private static Dictionary<Type, List<CTypeDonneeCumulee>> m_tableTypeToDonneesCumulees = new Dictionary<Type, List<CTypeDonneeCumulee>>();
        private static CRecepteurNotification m_recepteurNotificationModifTypeDonneeCumulee = null;
        private static CRecepteurNotification m_recepteurNotificationAjoutTypeDonneeCumulee = null;


        public static void Autoexec()
        {
            CFournisseurGeneriqueProprietesDynamiques.RegisterTypeFournisseur(new CFournisseurProprieteDynamiqueDonneeCumulee());
        }

        public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet, CDefinitionProprieteDynamique defParente)
        {
            List<CDefinitionProprieteDynamique> lstProps = new List<CDefinitionProprieteDynamique>();
            if (objet == null)
                return lstProps.ToArray();
            Type tp = objet.TypeAnalyse;
            if (tp == null)
                return lstProps.ToArray();

            //Données ucmulées
            CTypeDonneeCumulee[] donnees = GetTypesDonneesPourType(tp);
            foreach (CTypeDonneeCumulee typeDonnee in donnees)
            {
                int nCle = 0;
                foreach (CCleDonneeCumulee cle in typeDonnee.Parametre.ChampsCle)
                {
                    if (cle.TypeLie == tp)
                    {
                        CDefinitionProprieteDynamiqueDonneeCumulee def =
                            new CDefinitionProprieteDynamiqueDonneeCumulee(
                            typeDonnee,
                            nCle);
                        lstProps.Add(def);
                        break;
                    }
                }
            }
            return lstProps.ToArray();
        }

        /// ///////////////////////////////////////////////////////////
        private static void UpdateTableDonneesCumulees()
        {
            int nIdSession = 0;
            try
            {
                nIdSession = CSessionClient.GetSessionUnique().IdSession;
            }
            catch
            { }
            if (m_recepteurNotificationModifTypeDonneeCumulee == null)
            {
                m_recepteurNotificationModifTypeDonneeCumulee = new CRecepteurNotification(nIdSession, typeof(CDonneeNotificationModificationContexteDonnee));
                m_recepteurNotificationModifTypeDonneeCumulee.OnReceiveNotification += new NotificationEventHandler(m_recepteurNotificationTypeDonneeCumulee_OnReceiveNotification);
            }
            if (m_recepteurNotificationAjoutTypeDonneeCumulee == null)
            {
                m_recepteurNotificationAjoutTypeDonneeCumulee = new CRecepteurNotification(nIdSession, typeof(CDonneeNotificationAjoutEnregistrement));
                m_recepteurNotificationAjoutTypeDonneeCumulee.OnReceiveNotification += new NotificationEventHandler(m_recepteurNotificationTypeDonneeCumulee_OnReceiveNotification);
            }
            if (m_tableTypeToDonneesCumulees != null)
                m_tableTypeToDonneesCumulees.Clear();
            else
                m_tableTypeToDonneesCumulees = new Dictionary<Type, List<CTypeDonneeCumulee>>();
            CContexteDonnee contexte = CContexteDonneeSysteme.GetInstance();
            CListeObjetsDonnees liste = new CListeObjetsDonnees(contexte, typeof(CTypeDonneeCumulee));
            foreach (CTypeDonneeCumulee type in liste)
            {
                CParametreDonneeCumulee parametre = type.Parametre;
                foreach (CCleDonneeCumulee cle in parametre.ChampsCle)
                {
                    if (cle.TypeLie != null)
                    {
                        List<CTypeDonneeCumulee> lst = null;
                        if (!m_tableTypeToDonneesCumulees.TryGetValue(cle.TypeLie, out lst))
                        {
                            lst = new List<CTypeDonneeCumulee>();
                            m_tableTypeToDonneesCumulees[cle.TypeLie] = lst;
                        }
                        lst.Add(type);
                    }
                }
            }
        }

        /// ///////////////////////////////////////////////////////////
        public static CTypeDonneeCumulee[] GetTypesDonneesPourType(Type tp)
        {
            if (m_tableTypeToDonneesCumulees == null)
                UpdateTableDonneesCumulees();
            List<CTypeDonneeCumulee> lst = null;
            if (!m_tableTypeToDonneesCumulees.TryGetValue(tp, out lst))
                return new CTypeDonneeCumulee[0];
            return lst.ToArray();
        }

        /// <summary>
        /// </summary>
        /// <param name="donnee"></param>
        private static void m_recepteurNotificationTypeDonneeCumulee_OnReceiveNotification(IDonneeNotification donnee)
        {
            lock (typeof(CFournisseurProprieteDynamiqueDonneeCumulee))
            {
                if (donnee is CDonneeNotificationAjoutEnregistrement)
                {
                    if (((CDonneeNotificationAjoutEnregistrement)donnee).NomTable == CTypeDonneeCumulee.c_nomTable)
                        m_tableTypeToDonneesCumulees = null;
                }
                else if (donnee is CDonneeNotificationModificationContexteDonnee)
                {
                    foreach (CDonneeNotificationModificationContexteDonnee.CInfoEnregistrementModifie info in
                        ((CDonneeNotificationModificationContexteDonnee)donnee).ListeModifications)
                        if (info.NomTable == CTypeDonneeCumulee.c_nomTable)
                        {
                            m_tableTypeToDonneesCumulees = null;
                            return;
                        }
                }
            }
        }




    }
}
