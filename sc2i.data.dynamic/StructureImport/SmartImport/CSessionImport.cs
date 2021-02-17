using sc2i.common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.data.dynamic.StructureImport.SmartImport
{
    [Serializable]
    public class CSessionImport : I2iSerializable
    {
        private DataTable m_tableSource;
        private DataTable m_tableLignesImportees;
        private DataTable m_tableLignesNonImportees;
        private DataTable m_tableLignesMarquées;
        private CConfigMappagesSmartImport m_configMappage;
        private List<CLigneLogImport> m_logImport = new List<CLigneLogImport>();
        private CElementsAjoutesPendantImport m_elementsAjoutes = new CElementsAjoutesPendantImport();
        private List<CInfoImportLigne> m_listeElementsParLigne = new List<CInfoImportLigne>();

        public const string c_colNumLigneOriginal = "SC2I_ORIG_LINE_NUM";

        //-------------------------------------------------------------------
        public CSessionImport()
        {
        }

        //-------------------------------------------------------------------
        public DataTable TableSource
        {
            get
            {
                return m_tableSource;
            }
            set { m_tableSource = value; }
        }

        //-------------------------------------------------------------------
        public DataTable TableLignesImportees
        {
            get
            {
                return m_tableLignesImportees;
            }
        }

        //-------------------------------------------------------------------
        public DataTable TableLignesNonImportees
        {
            get
            {
                return m_tableLignesNonImportees;
            }
        }

        //-------------------------------------------------------
        public DataTable TableLignesMarquées
        {
            get
            {
                if (m_tableLignesMarquées == null &&
                    m_tableSource != null)
                    m_tableLignesMarquées = m_tableSource.Clone();
                if (m_tableLignesMarquées.Columns[c_colNumLigneOriginal] == null)
                    m_tableLignesMarquées.Columns.Add(c_colNumLigneOriginal, typeof(int));
                return m_tableLignesMarquées;
            }
        }

        //-------------------------------------------------------
        public CElementsAjoutesPendantImport ElementsAjoutes
        {
            get
            {
                return m_elementsAjoutes;
            }
            set
            {
                m_elementsAjoutes = value;
            }
        }

        //-------------------------------------------------------
        public IEnumerable<CInfoImportLigne> ElementsRootDeLignes
        {
            get
            {
                return m_listeElementsParLigne;
            }
            set
            {
                m_listeElementsParLigne = new List<CInfoImportLigne>();
                if (value != null)
                    m_listeElementsParLigne.AddRange(value);
            }
        }

        //-------------------------------------------------------------------
        public CConfigMappagesSmartImport ConfigMappage
        {
            get
            {
                return m_configMappage;
            }
            set
            {
                m_configMappage = value;
            }
        }

        //-------------------------------------------------------------------
        public IEnumerable<CLigneLogImport> Logs
        {
            get
            {
                return m_logImport.AsReadOnly();
            }
            set
            {
                List<CLigneLogImport> lignes = new List<CLigneLogImport>();
                if (value != null)
                    lignes.AddRange(value);
                m_logImport = lignes;
            }
        }

        //-------------------------------------------------------------------
        public void AddLogs(IEnumerable<CLigneLogImport> lignes)
        {
            if ( lignes != null )
                m_logImport.AddRange(lignes);
        }

        //-------------------------------------------------------------------
        public void AddLog(CLigneLogImport ligne)
        {
            if (ligne != null)
                m_logImport.Add(ligne);
        }

        //-------------------------------------------------------------------
        public IEnumerable<CLigneLogImport> GetLogsForLine ( int nLigne )
        {
            return from l in Logs where l.RowIndex != null && 
                   (l.RowIndex.Value == nLigne || (l.EndRowIndex != null &&
                   l.RowIndex.Value <= nLigne && l.EndRowIndex.Value >= nLigne))
                   select l;
        }
        //-------------------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //-------------------------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            object tmp = m_tableSource;
            bool bHasObject = tmp != null;
            serializer.TraiteBool(ref bHasObject);
            if ( bHasObject )
                result = serializer.TraiteSerializable(ref tmp);
            if (result)
            {
                m_tableSource = (DataTable)tmp;
                tmp = m_tableLignesImportees;
                bHasObject = tmp != null;
                serializer.TraiteBool(ref bHasObject);
                if ( bHasObject )
                    result = serializer.TraiteSerializable(ref tmp);
            }
            if (result)
            {
                m_tableLignesImportees = (DataTable)tmp;
                tmp = m_tableLignesNonImportees;
                bHasObject = tmp != null;
                serializer.TraiteBool(ref bHasObject);
                if (bHasObject)
                    result = serializer.TraiteSerializable(ref tmp);
            }
            if (result)
            {
                m_tableLignesNonImportees = (DataTable)tmp;
                tmp = m_tableLignesMarquées;
                bHasObject = tmp != null;
                serializer.TraiteBool(ref bHasObject);
                if (bHasObject)
                    result = serializer.TraiteSerializable(ref tmp);
            }
            if ( result )
            {
                m_tableLignesMarquées = (DataTable)tmp;
                result = serializer.TraiteListe<CLigneLogImport>(m_logImport);
            }
            if (result)
                result = serializer.TraiteObject<CConfigMappagesSmartImport>(ref m_configMappage);
            if (result)
                result = serializer.TraiteObject<CElementsAjoutesPendantImport>(ref m_elementsAjoutes);
            if (result)
                result = serializer.TraiteListe<CInfoImportLigne>(m_listeElementsParLigne);
            return result;
        }

        //-------------------------------------------------------
        public void SetImportees ( int nStartLigne, int nEndLigne )
        {
            if (m_tableLignesImportees == null)
                m_tableLignesImportees = m_tableSource.Clone();
            CopieLignesTo(m_tableLignesImportees, nStartLigne, nEndLigne);
        }

        //-------------------------------------------------------
        public void SetNonImportees ( int nStartLigne, int nEndLigne )
        {
            if (m_tableLignesNonImportees == null)
                m_tableLignesNonImportees = m_tableSource.Clone();
            CopieLignesTo(m_tableLignesNonImportees, nStartLigne, nEndLigne);
        }

        //-------------------------------------------------------
        private void CopieLignesTo(
            DataTable tableDest,
            int nStartLigne,
            int nEndLigne)
        {
            if (tableDest.Columns[c_colNumLigneOriginal] == null)
                tableDest.Columns.Add(c_colNumLigneOriginal, typeof(int));
            tableDest.BeginLoadData();
            for (int nLigne = nStartLigne; nLigne <= nEndLigne; nLigne++)
            {
                if (nLigne >= 0 && nLigne < TableSource.Rows.Count)
                {
                    tableDest.ImportRow(TableSource.Rows[nLigne]);
                    tableDest.Rows[tableDest.Rows.Count - 1][c_colNumLigneOriginal] = nLigne;
                }
            }
            tableDest.EndLoadData();
        }

        //-------------------------------------------------------
        private DataRow AddLigneToReimport ( DataRow row )
        {
            if (row == null)
                return null;
            if (row.Table.Columns[c_colNumLigneOriginal] == null)
                return null;
            int nVal = (int)row[c_colNumLigneOriginal];
            DataRow[] rows = TableLignesMarquées.Select(c_colNumLigneOriginal + "=" + nVal);
            if (rows.Length == 0)
            {
                TableLignesMarquées.ImportRow(row);
                return TableLignesMarquées.Rows[TableLignesMarquées.Rows.Count - 1];
            }
            return rows[0];
        }



        //-----------------------------------------------------------------
        public void ReplaceSourceParMarkedRecords()
        {
            if ( TableLignesMarquées != null )
            {
                m_tableSource = TableLignesMarquées.Clone();
                m_tableSource.Columns.Remove(c_colNumLigneOriginal);
                foreach (DataRow row in TableLignesMarquées.Rows)
                    m_tableSource.ImportRow(row);
            }
        }
    }
}
