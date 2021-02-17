using System;
using System.Collections;

using sc2i.common;
using System.Collections.Generic;
using sc2i.data.synchronisation;

namespace sc2i.data
{
    public interface IFiltreSynchronisation
    {
        CFiltreData GetFiltreData( int nIdSession, CFiltresSynchronisation filtresSynchro);
    }

	/// <summary>
	/// Description résumée de CFiltresSynchronisation.
	/// </summary>
	/// <summary>
	/// Gère les filtres particuliers pour la synchronisation des données
	/// </summary>
	[Serializable]
	public class CFiltresSynchronisation
	{
		//Table NomTable->FiltreData à ajouter au filtre std
        private Dictionary<string, List<IFiltreSynchronisation>> m_dicTableToFiltre = new Dictionary<string, List<IFiltreSynchronisation>>();
		
		public CFiltresSynchronisation()
		{
		}

        //-----------------------------------------------------------------
        public void ClearFiltresSynchroPourTable(string strTable)
        {
            if (m_dicTableToFiltre.ContainsKey(strTable))
                m_dicTableToFiltre.Remove(strTable);
        }

        //-----------------------------------------------------------------
		public void AddFiltreForTable(string strTable, CFiltreData filtre, bool bAvecCompositions )
		{
            AddFiltreSynchroPourTable ( strTable, new CFiltreSynchronisationFiltreData ( filtre ), bAvecCompositions);
		}

        //-----------------------------------------------------------------
        public void AddFiltreSynchroPourTable ( string strTable, IFiltreSynchronisation filtre, bool bAvecCompositions )
        {
            List<IFiltreSynchronisation> lstFiltres = null;
            if (!m_dicTableToFiltre.TryGetValue(strTable, out lstFiltres))
            {
                lstFiltres = new List<IFiltreSynchronisation>();
                m_dicTableToFiltre[strTable] = lstFiltres;
            }
            lstFiltres.Add(filtre);
            if (bAvecCompositions)
            {
                //trouve les compositions de la table et les filtre !
                foreach (Type tp in CContexteDonnee.GetAllTypes())
                {
                    CStructureTable structure = CStructureTable.GetStructure(tp);
                    if (structure != null)
                    {
                        foreach (CInfoRelation relParente in structure.RelationsParentes)
                        {
                            Type tpFils = CContexteDonnee.GetTypeForTable(relParente.TableFille);
                            if (tpFils == null || tpFils.GetCustomAttributes(typeof(FullTableSyncAttribute), true).Length == 0)
                            {
                                if (relParente.Composition && relParente.TableParente == strTable && relParente.TableFille != strTable)
                                {
                                    AddFiltreSynchroPourTable(structure.NomTable, new CFiltreSynchronisationFilsCompositions(structure.NomTable, strTable), true);
                                }
                                else if( relParente.TableFille == strTable && relParente.TableParente == strTable )
                                    AddFiltreSynchroPourTable ( structure.NomTable, new CFiltreSynchroFilsAutoreference(relParente.ChampsFille[0]), false );
                            }
                        }
                    }
                }
            }
        }

        //-----------------------------------------------------------------
        public IEnumerable<IFiltreSynchronisation> GetFiltresSynchroPourTable(string strTable)
        {
            List<IFiltreSynchronisation> lstFiltres = null;
            m_dicTableToFiltre.TryGetValue ( strTable, out lstFiltres);
            if (lstFiltres == null)
                return new List<IFiltreSynchronisation>();
            return lstFiltres;
        }

        //-----------------------------------------------------------------
		public CFiltreData GetFiltreForTable ( int nIdSession, string strTable )
		{
            IEnumerable<IFiltreSynchronisation> filtres = GetFiltresSynchroPourTable(strTable);
            CFiltreSynchroFilsAutoreference filtreAutoRef = null;
            if(  filtres != null )
            {
                CFiltreData filtreFinal = null;
                foreach ( IFiltreSynchronisation filtreSynchro in filtres )
                {
                    filtreFinal = CFiltreData.GetOrFiltre ( filtreFinal, 
                        filtreSynchro.GetFiltreData ( nIdSession, this ));
                    if ( filtreSynchro is CFiltreSynchroFilsAutoreference )
                        filtreAutoRef = filtreSynchro as CFiltreSynchroFilsAutoreference;
                }
                if (filtreAutoRef != null)
                    filtreFinal = filtreAutoRef.GetFiltreFinal(nIdSession, strTable, filtreFinal);
                return filtreFinal;
            }
            return null;
		}

        //-----------------------------------------------------------------
        public string[] TablesDefinies
        {
            get
            {
                List<string> lst = new List<string>();
                foreach (string strKey in m_dicTableToFiltre.Keys)
                    lst.Add(strKey);
                return lst.ToArray();
            }
        }
	}
}
