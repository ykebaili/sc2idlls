using sc2i.common;
using sc2i.expression;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.data.dynamic.StructureImport.SmartImport
{
    [Serializable]
    public class CValeursProprietes : I2iSerializable
    {
        private Dictionary<CDefinitionProprieteDynamique, object> m_dicValeursSimples = null;//new Dictionary<CDefinitionProprieteDynamique, object>();
        private string m_strLibelleObjet = "";
        private CDbKey m_dbKeyObjet = null;
        private Type m_typeObjet = null;

        //Liste des éléments parents
        private Dictionary<CDefinitionProprieteDynamique, CValeursProprietes> m_dicValeursParentes = null;//new Dictionary<CDefinitionProprieteDynamique,CValeursProprietes>();
        //Liste des éléments fils
        private Dictionary<CDefinitionProprieteDynamique, List<CValeursProprietes>> m_dicValeursFilles = null;//new Dictionary<CDefinitionProprieteDynamique,List<CValeursProprietes>>;

        //Non stocké, utilisé uniquement pendant le remplissage
        [NonSerialized]
        private CObjetDonnee m_objetAssocie = null;

        //Stocke les valeurs originales
        private CValeursProprietes m_valeursOriginales = null;
        private bool m_bAvecValeursOriginales = false;

        private DataRowVersion m_versionObjetStocke = DataRowVersion.Current;

        //---------------------------------------------------------------
        public CValeursProprietes()
        {
        }

        //---------------------------------------------------------------
        public CValeursProprietes(CObjetDonnee objet)
            :this(objet, false)
        {

        }

        //---------------------------------------------------------------
        public CValeursProprietes(CObjetDonnee objet, bool bAvecValeursOriginales)
        {
            m_objetAssocie = objet;
            m_strLibelleObjet = objet.DescriptionElement;
            m_dbKeyObjet = objet.DbKey;
            m_typeObjet = objet.GetType();
            m_bAvecValeursOriginales = bAvecValeursOriginales;
            m_versionObjetStocke = objet != null ? objet.VersionToReturn : DataRowVersion.Current;
        }

        //---------------------------------------------------------------
        public CObjetDonnee ObjetAssocie
        {
            get
            {
                return m_objetAssocie;
            }
        }

        //---------------------------------------------------------------
        public CValeursProprietes ValeursOriginales
        {
            get
            {
                return m_valeursOriginales;
            }
        }

        //---------------------------------------------------------------
        public string LibelleObjet
        {
            get
            {
                return m_strLibelleObjet;
            }
        }

        //---------------------------------------------------------------
        public Type TypeObjet
        {
            get
            {
                return m_typeObjet;
            }
        }

        //---------------------------------------------------------------
        public CDbKey DbKeyObjet
        {
            get
            {
                return m_dbKeyObjet;
            }
        }

        //---------------------------------------------------------------
        public bool IsCalculated
        {
            get
            {
                return m_dicValeursSimples != null;
            }
        }

        //---------------------------------------------------------------
        private void CalculeValeurs (  )
        {
            CalculeValeurs(new CFournisseurGeneriqueProprietesDynamiques().GetDefinitionsChamps(m_objetAssocie.GetType()));
        }

        //---------------------------------------------------------------
        private bool IsChampCompatibleImport ( Type typeObjet, CDefinitionProprieteDynamique def)
        {
            if (def is CDefinitionProprieteDynamiqueChampCustom)
                return true;
            if ( def is CDefinitionProprieteDynamiqueDotNet)
            {
                PropertyInfo info = typeObjet.GetProperty(def.NomProprieteSansCleTypeChamp);
                if ( info != null )
                {
                    TableFieldPropertyAttribute attField = info.GetCustomAttribute<TableFieldPropertyAttribute>(true);
                    if ( attField != null && attField.IsInDb)
                        return true;

                    RelationAttribute attRel = info.GetCustomAttribute<RelationAttribute>(true);
                    if ( attRel != null )
                        return true;
                    RelationFilleAttribute attFille = info.GetCustomAttribute<RelationFilleAttribute>(true);
                    if (attFille != null)
                        return true;
                }
            }
            if ( def is CDefinitionProprieteDynamiqueRelationTypeId )
                return true;
            if ( def is CDefinitionProprieteDynamiqueRelationTypeIdToParent)
                return true;
            if ( def is CDefinitionProprieteDynamiqueChampCustomFils)
                return true;
            return false;
        }

        //---------------------------------------------------------------
        private void CalculeValeurs(IEnumerable<CDefinitionProprieteDynamique> lstChamps)
        {
            m_dicValeursSimples = new Dictionary<CDefinitionProprieteDynamique, object>();
            m_dicValeursParentes = new Dictionary<CDefinitionProprieteDynamique, CValeursProprietes>();
            m_dicValeursFilles = new Dictionary<CDefinitionProprieteDynamique, List<CValeursProprietes>>();
            if ( m_objetAssocie == null )
                return;

            DataRowVersion oldVers = m_objetAssocie.VersionToReturn;
            m_objetAssocie.VersionToReturn = m_versionObjetStocke;

            m_strLibelleObjet = m_objetAssocie.DescriptionElement;


            HashSet<CDbKey> dicChampsAffectes = new HashSet<CDbKey>();
            //Optimisation des champs custom
            IElementAChamps eltAChamps = m_objetAssocie as IElementAChamps;
            if ( eltAChamps != null )
            {
                foreach (CRelationElementAChamp_ChampCustom rel in eltAChamps.RelationsChampsCustom)
                    dicChampsAffectes.Add(rel.ChampCustom.DbKey);
            }


            CResultAErreur res = CResultAErreur.True;
            
            foreach (CDefinitionProprieteDynamique def in lstChamps)
            {
                if (IsChampCompatibleImport(m_objetAssocie.GetType(), def))
                {
                    try
                    {
                        bool bInterprete = true;
                        CDefinitionProprieteDynamiqueChampCustom defCust = def as CDefinitionProprieteDynamiqueChampCustom;
                        if ( defCust != null )
                        {
                            bInterprete = dicChampsAffectes.Contains ( defCust.DbKeyChamp );
                        }
                        if (bInterprete)
                        {
                            DateTime dt = DateTime.Now;
                            res = CInterpreteurProprieteDynamique.GetValue(m_objetAssocie, def);
                            TimeSpan sp = DateTime.Now - dt;
                            if (sp.TotalMilliseconds > 400)
                                dt = DateTime.Now;
                            if (res)
                            {
                                object obj = res.Data;
                                if (obj is CObjetDonnee)
                                {
                                    CValeursProprietes val = new CValeursProprietes((CObjetDonnee)obj, m_bAvecValeursOriginales);
                                    m_dicValeursParentes[def] = val;

                                }
                                else if (obj is IEnumerable && !(obj is string))
                                {
                                    List<CValeursProprietes> lst = new List<CValeursProprietes>();
                                    foreach (object valDeListe in (IEnumerable)obj)
                                        if (valDeListe is CObjetDonnee)
                                            lst.Add(new CValeursProprietes((CObjetDonnee)valDeListe, m_bAvecValeursOriginales));
                                    if (lst.Count > 0)
                                    {
                                        m_dicValeursFilles[def] = lst;
                                    }

                                }
                                else if (obj != null && C2iSerializer.IsObjetSimple(obj))
                                    m_dicValeursSimples[def] = obj;
                            }
                        }
                    }

                    catch { }
                }

            }
            if ( m_bAvecValeursOriginales && m_objetAssocie.Row.RowState == DataRowState.Modified )
            {
                m_objetAssocie.VersionToReturn = DataRowVersion.Original;
                m_valeursOriginales = new CValeursProprietes(m_objetAssocie, false);
            }
            m_objetAssocie.VersionToReturn = oldVers;
            
        }

        //---------------------------------------------------------------
        private void AssureValeurs()
        {
            if (m_dicValeursSimples == null)
                CalculeValeurs();
        }

        //---------------------------------------------------------------
        public IEnumerable<CDefinitionProprieteDynamique> GetDefinitionsSimples()
        {
            AssureValeurs();
            return m_dicValeursSimples.Keys.ToArray();
        }
        
        //---------------------------------------------------------------
        public IEnumerable<CDefinitionProprieteDynamique> GetDefinitionsParentes()
        {
            AssureValeurs();
            return m_dicValeursParentes.Keys.ToArray();
        }

        //---------------------------------------------------------------
        public IEnumerable<CDefinitionProprieteDynamique> GetDefinitionsFilles()
        {
            AssureValeurs();
            return m_dicValeursFilles.Keys.ToArray();
        }

        //---------------------------------------------------------------
        public object GetValeurSimple ( CDefinitionProprieteDynamique def )
        {
            AssureValeurs();
            object val;
            if ( m_dicValeursSimples.TryGetValue(def, out val))
                return val;
            return null;
        }

        //---------------------------------------------------------------
        public CValeursProprietes GetValeurParente ( CDefinitionProprieteDynamique def)
        {
            AssureValeurs();
            CValeursProprietes val = null;
            if ( m_dicValeursParentes.TryGetValue(def, out val))
                return val;
            return null;
        }

        //---------------------------------------------------------------
        public IEnumerable<CValeursProprietes> GetValeursFilles ( CDefinitionProprieteDynamique def)
        {
            AssureValeurs();
            List<CValeursProprietes> lst = new List<CValeursProprietes>();
            if ( m_dicValeursFilles.TryGetValue(def, out lst ) )
                return lst.AsReadOnly();
            return new List<CValeursProprietes>();
        }

        //---------------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //---------------------------------------------------------------
        public CResultAErreur Serialize ( C2iSerializer serializer )
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            bool bIsCalcule = m_dicValeursSimples != null;
            serializer.TraiteBool ( ref bIsCalcule );
            if ( bIsCalcule)
            {
            int nNb = m_dicValeursSimples.Count;
            serializer.TraiteInt(ref nNb);
            switch (serializer.Mode)
            {
                case ModeSerialisation.Lecture:
                    m_dicValeursSimples = new Dictionary<CDefinitionProprieteDynamique,object>();
                    for (int n = 0; n < nNb; n++)
                    {
                        CDefinitionProprieteDynamique def = null;
                        result = serializer.TraiteObject<CDefinitionProprieteDynamique>(ref def);
                        if (!result)
                            return result;
                        object val = null;
                        result = serializer.TraiteObjetSimple(ref val);
                        if (!result)
                            return result;
                        m_dicValeursSimples[def] = val;
                    }
                    break;
                case ModeSerialisation.Ecriture:
                    foreach (KeyValuePair<CDefinitionProprieteDynamique, object> kv in m_dicValeursSimples)
                    {
                        CDefinitionProprieteDynamique def = kv.Key;
                        result = serializer.TraiteObject<CDefinitionProprieteDynamique>(ref def);
                        if (!result)
                            return result;
                        object val = kv.Value;
                        result = serializer.TraiteObjetSimple(ref val);
                        if (!result)
                            return result;
                    }
                    break;
            }

            nNb = m_dicValeursParentes.Count;
            serializer.TraiteInt(ref nNb);
            switch (serializer.Mode)
            {
                case ModeSerialisation.Lecture:
                    m_dicValeursParentes = new Dictionary<CDefinitionProprieteDynamique,CValeursProprietes>();
                    for (int n = 0; n < nNb; n++ )
                    {
                        CDefinitionProprieteDynamique def = null;
                        result = serializer.TraiteObject<CDefinitionProprieteDynamique>(ref def);
                        if (!result)
                            return result;
                        CValeursProprietes val = null;
                        result = serializer.TraiteObject<CValeursProprietes>(ref val);
                        if ( !result) 
                            return result;
                        m_dicValeursParentes[def] = val;
                    }
                        break;
                case ModeSerialisation.Ecriture:
                    foreach ( KeyValuePair<CDefinitionProprieteDynamique, CValeursProprietes> kv in m_dicValeursParentes)
                    {
                        CDefinitionProprieteDynamique def = kv.Key;
                        result = serializer.TraiteObject<CDefinitionProprieteDynamique>(ref def);
                        if ( !result )
                            return result;
                        CValeursProprietes val = kv.Value;
                        result = serializer.TraiteObject<CValeursProprietes>(ref val);
                        if ( !result )
                            return result;
                    }
                    break;
            }

                nNb = m_dicValeursFilles.Count;
                serializer.TraiteInt ( ref nNb );
                switch (serializer.Mode)
	{
		case ModeSerialisation.Lecture:
                        m_dicValeursFilles = new Dictionary<CDefinitionProprieteDynamique,List<CValeursProprietes>>();
                   for (int n = 0; n < nNb; n++ )
                    {
                        CDefinitionProprieteDynamique def = null;
                        result = serializer.TraiteObject<CDefinitionProprieteDynamique>(ref def);
                        if (!result)
                            return result;
                        
                       List<CValeursProprietes> lst = new List<CValeursProprietes>();
                       result = serializer.TraiteListe<CValeursProprietes>(lst);
                        if ( !result) 
                            return result;
                        m_dicValeursFilles[def] = lst;
                    }
                        break;
                case ModeSerialisation.Ecriture:
                    foreach ( KeyValuePair<CDefinitionProprieteDynamique, List<CValeursProprietes>> kv in m_dicValeursFilles)
                    {
                        CDefinitionProprieteDynamique def = kv.Key;
                        result = serializer.TraiteObject<CDefinitionProprieteDynamique>(ref def);
                        if ( !result )
                            return result;
                        List<CValeursProprietes> lst = kv.Value;
                        result = serializer.TraiteListe<CValeursProprietes>(lst);
                        if ( !result )
                            return result;
                    }
                    break;
                }
            }
            return result;
        }

        //---------------------------------------------------------
        public override string ToString()
        {
            return m_strLibelleObjet;
        }


            
        

    }
}
