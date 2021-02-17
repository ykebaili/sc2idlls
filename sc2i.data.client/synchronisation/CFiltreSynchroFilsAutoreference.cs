using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.Data;

namespace sc2i.data.synchronisation
{
    public class CFiltreSynchroFilsAutoreference : IFiltreSynchronisation
    {
        private string m_strNomChampParentId = "";

        //------------------------------------------------------------------------------------
        public CFiltreSynchroFilsAutoreference ( string strNomChampParentId )
        {
            m_strNomChampParentId = strNomChampParentId;
        }

        //------------------------------------------------------------------------------------
        public CFiltreData GetFiltreData(int nIdSession, CFiltresSynchronisation filtresSynchro)
        {
            return null;
        }

        

        //------------------------------------------------------------------------------------
        public CFiltreData GetFiltreFinal(int nIdSession, string strNomTable, CFiltreData filtreNonAutoRef)
        {
            Type tp = CContexteDonnee.GetTypeForTable(strNomTable);
            if (tp == null)
                return null;
            CStructureTable structure = CStructureTable.GetStructure(tp);
            if (structure == null)
                return null;

            //Va lire les ids non autoref dans la base
            HashSet<int> lstIdsALire = new HashSet<int>();
            C2iRequeteAvancee requete = new C2iRequeteAvancee();
            requete.TableInterrogee = strNomTable;
            requete.FiltreAAppliquer = filtreNonAutoRef;
            requete.ListeChamps.Add(
            new C2iChampDeRequete(structure.ChampsId[0].NomChamp,
                new CSourceDeChampDeRequete(structure.ChampsId[0].NomChamp),
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
            CFiltreData filtre = new CFiltreDataImpossible();
            if (lstIdsALire.Count > 0)
            {
                StringBuilder bl = new StringBuilder();
                foreach (int nId in lstIdsALire)
                {
                    bl.Append(nId.ToString());
                    bl.Append(',');
                }
                do
                {
                    bl.Remove(bl.Length - 1, 1);
                    filtre = new CFiltreData(m_strNomChampParentId + " in (" + bl.ToString() + ")");
                    //Va lire les ids des fils, jusqu'à ce qu'on ne trouve plus de fils
                    HashSet<int> lstNewIds = new HashSet<int>();
                    requete.FiltreAAppliquer = filtre;
                    result = requete.ExecuteRequete(nIdSession);
                    if (!result)
                        return new CFiltreDataImpossible();
                    DataTable table = result.Data as DataTable;
                    bl = new StringBuilder();
                    if (table != null)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            int? nVal = row[0] as int?;
                            if (nVal != null)
                            {
                                lstIdsALire.Add(nVal.Value);
                                bl.Append(nVal.Value);
                                bl.Append(',');
                            }
                        }
                    }
                }
                while (bl.Length > 0);
                bl = new StringBuilder();
                foreach (int nId in lstIdsALire)
                {
                    bl.Append(nId);
                    bl.Append(',');
                }
                if (bl.Length > 0)
                {
                    bl.Remove(bl.Length - 1, 1);
                    filtre = new CFiltreData(structure.ChampsId[0].NomChamp + " in (" +
                        bl.ToString() + ")");
                }
            }
            return filtre;
        }
    }
}
