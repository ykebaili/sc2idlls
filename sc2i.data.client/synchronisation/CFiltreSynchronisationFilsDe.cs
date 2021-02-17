using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.Data;

namespace sc2i.data.synchronisation
{
    public class CFiltreSynchronisationFilsDe : IFiltreSynchronisation
    {
        private string m_strNomTableFille = "";
        private List<string> m_listeTablesParentes = new List<string>();
        CFiltreData m_filtreDejaCalcule = null;

        //--------------------------------------------------------
        public CFiltreSynchronisationFilsDe()
        {
        }

        //--------------------------------------------------------
        public CFiltreSynchronisationFilsDe(
            string strNomTableFille,
            params string[] strTablesParentes)
        {
            m_strNomTableFille = strNomTableFille;
            m_listeTablesParentes.AddRange(strTablesParentes);
        }

        //--------------------------------------------------------
        public IEnumerable<string> TablesParentes
        {
            get
            {
                return m_listeTablesParentes.AsReadOnly();
            }
        }

        //--------------------------------------------------------
        public string NomTableFille
        {
            get
            {
                return m_strNomTableFille;
            }
        }

        //--------------------------------------------------------
        public CFiltreData GetFiltreData(int nIdSession, CFiltresSynchronisation filtresSynchro)
        {
            if (m_filtreDejaCalcule != null)
                return m_filtreDejaCalcule;
            Type tp = CContexteDonnee.GetTypeForTable(m_strNomTableFille);
            if (tp == null)
                return null;
            CStructureTable structure = CStructureTable.GetStructure(tp);
            if (structure == null)
                return null;
            CFiltreData filtre = null;
            foreach (CInfoRelation relationParente in structure.RelationsParentes)
            {
                if ( m_listeTablesParentes.Contains ( relationParente.TableParente ) )
                {
                    Type tpParent = CContexteDonnee.GetTypeForTable(relationParente.TableParente);
                    CStructureTable structParente = CStructureTable.GetStructure(tpParent);
                    if (structParente != null)
                    {
                        HashSet<int> lstIdsALire = new HashSet<int>();
                        C2iRequeteAvancee requete = new C2iRequeteAvancee();
                        requete.TableInterrogee = relationParente.TableParente;
                        requete.FiltreAAppliquer = filtresSynchro.GetFiltreForTable(nIdSession, relationParente.TableParente);
                        requete.ListeChamps.Add(
                        new C2iChampDeRequete(structParente.ChampsId[0].NomChamp,
                            new CSourceDeChampDeRequete(structParente.ChampsId[0].NomChamp),
                            typeof(int),
                            OperationsAgregation.None,
                            true));
                        CResultAErreur result = requete.ExecuteRequete(nIdSession);
                        if (result)
                        {
                            DataTable table = result.Data as DataTable;
                            if (table != null)
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    int? nVal = row[0] as int?;
                                    if (nVal != null)
                                        lstIdsALire.Add(nVal.Value);
                                }
                            }
                        }
                        if (lstIdsALire.Count > 0)
                        {
                            StringBuilder bl = new StringBuilder();
                            foreach (int nId in lstIdsALire)
                            {
                                bl.Append(nId.ToString());
                                bl.Append(',');
                            }
                            bl.Remove ( bl.Length-1, 1 );
                            filtre = CFiltreData.GetOrFiltre ( filtre,
                                new CFiltreData ( relationParente.ChampsFille[0]+" in ("+bl.ToString()+")") );
                        }
                    }
                }
            }
            if (filtre == null)
                filtre = new CFiltreDataImpossible();
            m_filtreDejaCalcule = filtre;
            return m_filtreDejaCalcule;
        }
    }
}
