using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.Data;

namespace sc2i.data.synchronisation
{
    /// <summary>
    /// Filtre qui se débrouille pour n'amener que les éléments nécéssaire
    /// d'une table dans la synchronisation
    /// </summary>
    public class CFiltreSynchronisationParentsNecessaires : IFiltreSynchronisation
    {
        private string m_strNomTable = "";
        private CFiltreData m_filtreDejaCalcule = null;

        //-----------------------------------------------------------------
        public CFiltreSynchronisationParentsNecessaires()
        {
        }

        //-----------------------------------------------------------------
        public CFiltreSynchronisationParentsNecessaires(string strNomTable)
        {
            m_strNomTable = strNomTable;
        }


        //-----------------------------------------------------------------
        public CFiltreData GetFiltreData(int nIdSession, CFiltresSynchronisation filtresSynchro)
        {
            if (m_filtreDejaCalcule != null)
                return m_filtreDejaCalcule;
            Type tp = CContexteDonnee.GetTypeForTable(m_strNomTable);
            if (tp == null)
                return null;
            CStructureTable structure = CStructureTable.GetStructure(tp);
            if (structure == null)
                return null;
            HashSet<int> lstIdsALire = new HashSet<int>();
            List<CInfoRelation> relationsFilles = new List<CInfoRelation>();
            foreach (Type tpTest in CContexteDonnee.GetAllTypes())
            {
                CStructureTable s = CStructureTable.GetStructure(tpTest);
                if (s != null)
                {
                    foreach (CInfoRelation relation in s.RelationsParentes)
                    {
                        if (relation.TableParente == m_strNomTable)
                            relationsFilles.Add(relation);
                    }
                }
            }

            //va chercher les dépendances filles qui auront besoin d'éléments de ce type
            foreach (CInfoRelation relation in relationsFilles)
            {
                Type tpFils = CContexteDonnee.GetTypeForTable(relation.TableFille);
                //Vérifie qu'il n'y a pas de filtre FilsDe ou FilsComposition sur la table
                bool bPrendreCetteTable = true;
                foreach ( IFiltreSynchronisation filtre in filtresSynchro.GetFiltresSynchroPourTable ( relation.TableFille ) )
                {
                    if ( relation.Composition && filtre is CFiltreSynchronisationFilsCompositions )
                    {
                        bPrendreCetteTable=  false;
                        break;
                    }
                    CFiltreSynchronisationFilsDe filsDe = filtre as CFiltreSynchronisationFilsDe;
                    if ( filsDe != null )
                    {
                        if ( filsDe.TablesParentes.Contains ( m_strNomTable ) )
                        {
                            bPrendreCetteTable=  false;
                            break;
                        }
                    }
                }
                if ( bPrendreCetteTable )
                {
                    
                    C2iRequeteAvancee requete = new C2iRequeteAvancee();
                    requete.TableInterrogee = relation.TableFille;
                    if ( relation.TableFille != m_strNomTable )
                        requete.FiltreAAppliquer = filtresSynchro.GetFiltreForTable(nIdSession, relation.TableFille);
                    requete.ListeChamps.Add ( 
                        new C2iChampDeRequete (relation.ChampsFille[0],
                            new CSourceDeChampDeRequete ( relation.ChampsFille[0] ),
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
                }
            }
            if (lstIdsALire.Count == 0)
            {
                m_filtreDejaCalcule = new CFiltreDataImpossible();
                return m_filtreDejaCalcule;
            }
            StringBuilder bl = new StringBuilder ();
            foreach ( int nId in lstIdsALire )
            {
                bl.Append(nId);
                bl.Append(',');
            }
            bl.Remove ( bl.Length-1, 1 );
            m_filtreDejaCalcule = new CFiltreData ( structure.ChampsId[0].NomChamp+" in ("+bl.ToString()+")" );
            return m_filtreDejaCalcule;
        }
    }
}
