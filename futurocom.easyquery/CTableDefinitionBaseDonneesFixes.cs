using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.Data;

namespace futurocom.easyquery
{
    /// <summary>
    /// Une définition de table qui contient ses propres données, il s'agit d'une classe
    /// de base, qui doit être héritée pour implémenter la partie ID et nom
    /// </summary>
    [Serializable]
    public abstract class CTableDefinitionBaseDonneesFixes : CTableDefinitionBase
    {
        private DataTable m_table = null;

        //-----------------------------------------
        protected void AssureTable()
        {
            if (m_table == null)
            {
                m_table = new DataTable(TableName);
                foreach (IColumnDefinition col in Columns)
                {
                    DataColumn dataCol = new DataColumn(col.ColumnName, col.DataType);
                    m_table.Columns.Add(dataCol);
                }
            }
        }

        //-----------------------------------------
        public DataTable TableContenu
        {
            get
            {
                AssureTable();
                return m_table;
            }
        }

        //-----------------------------------------
        public override CResultAErreur GetDatas(CEasyQuerySource source, params string[] strIdsColonnesSources)
        {
            CResultAErreur result = CResultAErreur.True;
            DataTable table = TableContenu.Clone();
            foreach (DataRow row in TableContenu.Rows)
                table.ImportRow(row);
            result.Data = table;
            return result;
        }

        //-----------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //-----------------------------------------
        public override CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (result)
                result = base.Serialize(serializer);
            if (!result)
                return result;
            AssureTable();
            object obj = m_table;
            result = serializer.TraiteSerializable(ref obj);
            if (!result)
                return result;
            m_table = obj as DataTable;
            
            return result;        
        }
    }
}
