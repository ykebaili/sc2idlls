using sc2i.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;

namespace sc2i.data.Package
{
    //-----------------------------------------------------------------------
    public interface IFournisseurDependancesObjetDonnee
    {
        List<CReferenceObjetDependant> GetDependances(
            CEntitiesManager manager,
            CObjetDonnee objet);
    }

    //-----------------------------------------------------------------------
    public class CReferenceObjetDependant : CReferenceObjetDonnee
    {
        private string m_strNomPropriete = "";

        //--------------------------------------------------------------------------
        public CReferenceObjetDependant(string strNomPropriete, Type tp, CDbKey keyObjet)
            : base(tp, keyObjet)
        {
            m_strNomPropriete = strNomPropriete;
        }

        //--------------------------------------------------------------------------
        public CReferenceObjetDependant(string strNomPropriete, Type tp, object[] clesObjet)
            : base(tp, clesObjet)
        {
            m_strNomPropriete = strNomPropriete;
        }

        //--------------------------------------------------------------------------
        public CReferenceObjetDependant(string strNomPropriete, CObjetDonnee objet)
            : base(objet)
        {
            m_strNomPropriete = strNomPropriete;
        }

        //--------------------------------------------------------------------------
        public string NomPropriete
        {
            get
            {
                return m_strNomPropriete;
            }
        }

        //--------------------------------------------------------------------------
        public override int GetHashCode()
        {
            StringBuilder bl = new StringBuilder();
            bl.Append(base.GetHashCode());
            bl.Append("_");
            bl.Append(m_strNomPropriete);
            return bl.ToString().GetHashCode();
        }


    }
    /// <summary>
    /// Classe capable de connaitre toutes les entités existantes dans une base de données
    /// </summary>
    public class CEntitiesManager
    {
        private static List<Type> m_listeFournisseurs = new List<Type>();

        private CConfigurationRechercheEntites m_configuration = new CConfigurationRechercheEntites();


        public static void RegisterFournisseurDependances ( Type typeFournisseur )
        {
            if (!m_listeFournisseurs.Contains(typeFournisseur))
                m_listeFournisseurs.Add(typeFournisseur);
        }

        private int m_nIdSession = -1;
        private Dictionary<Type, List<string>> m_dicTypeToKeys = new Dictionary<Type, List<string>>();
        private Dictionary<string, Type> m_dicKeyToTypes = new Dictionary<string, Type>();
        
        //Liste des sites qui ont été ignorés automatiquement
        private HashSet<Type> m_setTypesIgnoresAutomatiquement = new HashSet<Type>();

        private bool m_bHasAllDbKeys = false;

        private CConteneurIndicateurProgression m_indicateurProgression = CConteneurIndicateurProgression.GetConteneur(null);

        //--------------------------------------------------------------------------
        public CEntitiesManager(int nIdSession )
        {
            m_nIdSession = nIdSession;
        }

        //--------------------------------------------------------------------------
        public IIndicateurProgression IndicateurProgression
        {
            set
            {
                m_indicateurProgression = CConteneurIndicateurProgression.GetConteneur(value);
            }
        }
        //--------------------------------------------------------------------------
        public CConfigurationRechercheEntites ConfigurationRecherche
        {
            get
            {
                if (m_configuration == null)
                    m_configuration = new CConfigurationRechercheEntites();
                return m_configuration;
            }
            set
            {
                if (value != null)
                    m_configuration = value;
            }
        }

        //--------------------------------------------------------------------------
        public CResultAErreur ReadAllDbKeys()
        {
            m_indicateurProgression.PushLibelle(I.T("Reading references|20015"));
            foreach (Type tp in CContexteDonnee.GetAllTypes())
            {
                ReadRefsForType(tp);
            }
            m_indicateurProgression.PopLibelle();
            
            m_bHasAllDbKeys = true;
            return CResultAErreur.True;
        }

        //--------------------------------------------------------------------------
        private void EnsureAllDbKeys()
        {
            if (m_dicKeyToTypes.Count == 0 || !m_bHasAllDbKeys)
                ReadAllDbKeys();
        }

        //--------------------------------------------------------------------------
        public Type GetTypeForUniversalId( string strUniversalId)
        {
            EnsureAllDbKeys();
            Type tp = null;
            m_dicKeyToTypes.TryGetValue(strUniversalId, out tp);
            return tp;
        }

        //--------------------------------------------------------------------------
        public CResultAErreur ReadRefsForType( Type tp )
        {
            CResultAErreur result = CResultAErreur.True;
            if (tp == null)
                return result;
            List<string> lst = new List<string>();
            m_dicTypeToKeys[tp] = lst;
            if (m_configuration.IsIgnore(tp))
                return result;
            NoIdUniverselAttribute att = tp.GetCustomAttribute<NoIdUniverselAttribute>(true);
            if (att != null)
                return result;
            m_indicateurProgression.SetInfo(DynamicClassAttribute.GetNomConvivial(tp));
            string strNomTable = CContexteDonnee.GetNomTableForType(tp);
            //Compte les éléments dans la table
            using ( CContexteDonnee ctx = new CContexteDonnee(m_nIdSession, true, false))
            {
                CListeObjetsDonnees lstCount = new CListeObjetsDonnees(ctx, tp);
                int nNb = lstCount.CountNoLoad;
                if (nNb > m_configuration.LimiteNbPourRechercheReference)
                {
                    m_setTypesIgnoresAutomatiquement.Add(tp);
                    return result;
                }
            }
            C2iRequeteAvancee r = new C2iRequeteAvancee(null);
            r.TableInterrogee = strNomTable;
            r.ListeChamps.Add(new C2iChampDeRequete(CObjetDonnee.c_champIdUniversel,
                new CSourceDeChampDeRequete(CObjetDonnee.c_champIdUniversel),
                typeof(string),
                OperationsAgregation.None,
                true));
            result = r.ExecuteRequete(m_nIdSession);
            if (result)
            {
                DataTable table = result.Data as DataTable;
                if (table != null)
                    foreach (DataRow row in table.Rows)
                    {
                        lst.Add((string)row[0]);
                        m_dicKeyToTypes[(string)row[0]] = tp;
                    }
            }

            return result;
        }

        public delegate void OnFournisseurTermine(HashSet<CReferenceObjetDependant> set);

       
        //-----------------------------------------------------------------------------------
        public List<CReferenceObjetDependant> GetAllDependances(IEnumerable<CObjetDonnee> lstObjets)
        {
            return GetAllDependances(lstObjets, null);
        }

        //-----------------------------------------------------------------------------------
        public List<CReferenceObjetDependant> GetAllDependances(IEnumerable<CObjetDonnee> lstObjets, OnFournisseurTermine funcOnEachFournisseur)
        {
            HashSet<CReferenceObjetDependant> setDependances = new HashSet<CReferenceObjetDependant>();

            HashSet<CReferenceObjetDonnee> setExplores = new HashSet<CReferenceObjetDonnee>();

            HashSet<CReferenceObjetDonnee> objetsToExplore = new HashSet<CReferenceObjetDonnee>();
            foreach (CObjetDonnee objet in lstObjets)
                objetsToExplore.Add(new CReferenceObjetDonnee(objet));


            while (objetsToExplore.Count > 0)
            {
                HashSet<CReferenceObjetDonnee> newToDo = new HashSet<CReferenceObjetDonnee>();
                foreach (CReferenceObjetDonnee refToDo in objetsToExplore)
                {
                    if (!setExplores.Contains(refToDo.GetCloneReferenceObjetDonnee()))
                    {
                        setExplores.Add(refToDo.GetCloneReferenceObjetDonnee());
                        CObjetDonnee objetToExplore = refToDo.GetObjet(lstObjets.ElementAt(0).ContexteDonnee);
                        m_indicateurProgression.SetInfo(I.T("Exploring @1|20016", objetToExplore.DescriptionElement));
                        foreach (Type tpFournisseur in m_listeFournisseurs)
                        {
                            IFournisseurDependancesObjetDonnee fournisseur = Activator.CreateInstance(tpFournisseur) as IFournisseurDependancesObjetDonnee;
                            HashSet<CReferenceObjetDependant> setDeFournisseur = new HashSet<CReferenceObjetDependant>();
                            foreach (CReferenceObjetDependant reference in fournisseur.GetDependances(this, objetToExplore))
                            {
                                CReferenceObjetDonnee rf = reference.GetCloneReferenceObjetDonnee();
                                if (!setExplores.Contains(rf))
                                {
                                    COptionRechercheType option = ConfigurationRecherche.GetOption(rf.TypeObjet);
                                    if (option != null && option.RecursiveSearch)
                                        newToDo.Add(rf);
                                }
                                if (!setDependances.Contains(reference))
                                {
                                    setDependances.Add(reference);
                                    setDeFournisseur.Add(reference);
                                }
                            }
                            if (funcOnEachFournisseur != null)
                                funcOnEachFournisseur(setDeFournisseur);
                        }
                    }
                }
                objetsToExplore = newToDo;
            }


            List<CReferenceObjetDependant> lst = new List<CReferenceObjetDependant>();
            foreach (CReferenceObjetDependant reference in setDependances)
                lst.Add(reference);
            return lst;
        }
       
    }

    
}
