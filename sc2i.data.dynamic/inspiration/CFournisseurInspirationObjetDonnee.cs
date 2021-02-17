using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.Data;
using sc2i.expression;
using sc2i.formulaire.inspiration;

namespace sc2i.data.dynamic.inspiration
{
    [AutoExec("Autoexec")]
    public class CFournisseurInspirationObjetDonnee : IFournisseurInspiration
    {
        private static int? m_nIdSession = null;

        public static void Init(int? nIdSession)
        {
            m_nIdSession = nIdSession;
        }

        private static CFournisseurInspirationObjetDonnee m_instance = null;
        private static CFournisseurInspirationObjetDonnee Instance
        {
            get{
                if ( m_instance == null )
                    m_instance = new CFournisseurInspirationObjetDonnee();
                return m_instance;
            }
        }


        private static Dictionary<IParametreInspiration, CInspiration> m_dicInspirations = new Dictionary<IParametreInspiration, CInspiration>();

        public static void Autoexec()
        {
            CFournisseurInspiration.RegisterFournisseur(Instance, typeof(CParametreInspirationProprieteDeType));
        }

        private class CInspiration
        {
            private static Dictionary<CParametreInspirationProprieteDeType, C2iRequeteAvancee> m_dicRequetes = null;
            private List<string> m_listeInspirations = null;
            private DateTime m_dateLastRefresh = DateTime.Now;

            private CParametreInspirationProprieteDeType m_parametre = null;

            //-----------------------------------------------------------
            public CInspiration(CParametreInspirationProprieteDeType parametre)
            {
                m_parametre = parametre;
            }

            //-----------------------------------------------------------
            public IEnumerable<string> GetInspiration()
            {
                if (m_listeInspirations == null)
                {
                    m_listeInspirations = new List<string>();
                    m_dateLastRefresh = DateTime.Now.AddYears(-1) ;
                }
                if ((DateTime.Now - m_dateLastRefresh).TotalMinutes > 10 && m_nIdSession != null )
                    FillInspirations(m_nIdSession.Value);
                return m_listeInspirations.AsReadOnly();
            }

            //-----------------------------------------------------------
            private class CLockerInspiration { }

            //-----------------------------------------------------------
            private void FillInspirations(int nIdSession)
            {
                lock (typeof(CLockerInspiration))
                {
                    this.m_dateLastRefresh = DateTime.Now;
                    m_listeInspirations = new List<string>();
                    HashSet<string> set = new HashSet<string>();
                    if (m_dicRequetes == null)
                        m_dicRequetes = new Dictionary<CParametreInspirationProprieteDeType, C2iRequeteAvancee>();
                    C2iRequeteAvancee requete = GetRequete(m_parametre);
                    if (requete != null)
                    {
                        CResultAErreur result = requete.ExecuteRequete(nIdSession);
                        if (result && result.Data is DataTable)
                        {
                            DataTable table = result.Data as DataTable;
                            if (table != null)
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    string strVal = row[0] as string;
                                    if (strVal != null)
                                    {
                                        strVal = strVal.Trim();
                                        int nIndex;
                                        if (strVal.Length > 0)
                                        {
                                            do
                                            {
                                                set.Add(strVal);
                                                nIndex = strVal.IndexOf(' ');
                                                if (nIndex >= 0)
                                                    strVal = strVal.Substring(nIndex + 1).Trim()
                                                        ;
                                            } while (nIndex >= 0);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    m_listeInspirations.AddRange(set.ToArray());
                }
            }

            //-----------------------------------------------------------
            private C2iRequeteAvancee GetRequete(CParametreInspirationProprieteDeType parametre)
            {
                C2iRequeteAvancee requete = null;
                if (m_dicRequetes.TryGetValue(parametre, out requete))
                    return requete;
                if (parametre.Type == null)
                    return null;
                CDefinitionProprieteDynamiqueChampCustom defCustom = parametre.Champ as CDefinitionProprieteDynamiqueChampCustom;
                if (defCustom != null)
                {
                    requete = GetRequeteChampCustom(parametre);
                }
                CDefinitionProprieteDynamiqueDotNet defDotNet = parametre.Champ as CDefinitionProprieteDynamiqueDotNet;
                if (defDotNet != null)
                    requete = GetRequeteChampDotNet(parametre);
                m_dicRequetes[parametre] = requete;
                return requete;
            }

            //-----------------------------------------------------------
            private C2iRequeteAvancee GetRequeteChampDotNet(CParametreInspirationProprieteDeType parametre)
            {
                CStructureTable structure = CStructureTable.GetStructure(parametre.Type);
                string strNomProp = parametre.Champ.NomProprieteSansCleTypeChamp;
                foreach (CInfoChampTable info in structure.Champs)
                {
                    if (info.Propriete == strNomProp)
                    {
                        
                        C2iRequeteAvancee requete = new C2iRequeteAvancee();
                        requete.TableInterrogee = CContexteDonnee.GetNomTableForType(parametre.Type);
                        requete.ListeChamps.Add(new C2iChampDeRequete("LABEL",
                            new CSourceDeChampDeRequete(info.NomChamp),
                            typeof(string),
                            OperationsAgregation.None,
                            true));
                        return requete;
                    }
                }
                return null;
            }

            //-----------------------------------------------------------
            private C2iRequeteAvancee GetRequeteChampCustom(CParametreInspirationProprieteDeType parametre)
            {
                CDefinitionProprieteDynamiqueChampCustom defChampCustom = parametre.Champ as CDefinitionProprieteDynamiqueChampCustom;
                if (defChampCustom == null || parametre.Type == null || defChampCustom.DbKeyChamp == null)
                    return null;

                DataTable table = new DataTable();
                DataRow row = table.NewRow();
                IObjetDonneeAChamps objet = Activator.CreateInstance(parametre.Type, new object[] { row }) as IObjetDonneeAChamps;
                if (objet != null)
                {
                    string strTableValeurs = objet.GetNomTableRelationToChamps();
                    C2iRequeteAvancee requete = new C2iRequeteAvancee();
                    requete.TableInterrogee = strTableValeurs;
                    requete.ListeChamps.Add(new C2iChampDeRequete("LABEL",
                        new CSourceDeChampDeRequete(CRelationElementAChamp_ChampCustom.c_champValeurString),
                        typeof(string),
                        OperationsAgregation.None,
                        true));
                    int nIdChamp = -1;
                    //TESTDBKEYOK SC 31/03/2014
                    if (defChampCustom.DbKeyChamp.IsNumericalId())
                        nIdChamp = (int)defChampCustom.DbKeyChamp.GetValeurInDb();
                    else
                        nIdChamp = CChampCustom.GetIdFromDbKey(defChampCustom.DbKeyChamp);
                    requete.FiltreAAppliquer = new CFiltreData(CChampCustom.c_champId + "=@1", nIdChamp);
                    return requete;
                }
                return null;
            }

        }

        public IEnumerable<string> GetInspiration(IParametreInspiration parametre)
        {
            IEnumerable<string> lstRetour = new List<string>();
            CInspiration inspiration = null;
            if (!m_dicInspirations.TryGetValue(parametre, out inspiration))
            {
                inspiration = new CInspiration(CCloner2iSerializable.Clone(parametre) as CParametreInspirationProprieteDeType);
                m_dicInspirations[parametre] = inspiration;
            }
            if (inspiration != null)
            {
                lstRetour = inspiration.GetInspiration();
            }

            return lstRetour;
        }

    }
}
