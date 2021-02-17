using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.Data;

namespace sc2i.data.synchronisation
{
    public class CIdentifiantElement
    {
        private string m_strNomTable = "";
        private int m_nIdElement;

        public CIdentifiantElement()
        {
        }

        public CIdentifiantElement(string strTable, int nIdElement)
        {
            m_strNomTable = strTable;
            m_nIdElement = nIdElement;
        }

        public string TableName
        {
            get
            {
                return m_strNomTable;
            }
        }

        public int IdElement
        {
            get
            {
                return m_nIdElement;
            }
        }

        //-------------------------------------------
        public override bool  Equals(object obj)
        {
            CIdentifiantElement id = obj as CIdentifiantElement;
            if ( id != null )
            {
                return id.TableName == TableName && id.IdElement == IdElement;
            }
            return false;
        }

        //-------------------------------------------
        public override int  GetHashCode()
        {
 	        return (TableName+"_"+IdElement).GetHashCode();
        }
    }

    //-------------------------------------------
    public class CMapOldIdToNewId
    {
        private const string c_nomTable = "MAPOLDIDTONEWID";
        private const string c_champNomTable = "MAP_TABLENAME";
        private const string c_champOldId = "MAP_OLD_ID";
        private const string c_champNewId = "MAP_NEW_ID";
        private Dictionary<CIdentifiantElement, int> m_mapElementToNewId = new Dictionary<CIdentifiantElement, int>();

        //-------------------------------------------
        public CMapOldIdToNewId()
        {
        }

        //-------------------------------------------
        public CMapOldIdToNewId(DataSet dataSetDeTransport)
        {
            DataTable tbl = dataSetDeTransport.Tables[c_nomTable];
            foreach ( DataRow row in tbl.Rows )
            {
                SetNewIdForElement ( (string)row[c_champNomTable],
                    (int)row[c_champOldId],
                    (int)row[c_champNewId] );
            }
        }

        //-------------------------------------------
        public DataSet GetDataSetPourTransport()
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable(c_nomTable);
            ds.Tables.Add(table);
            table.Columns.Add(c_champNomTable, typeof(string));
            table.Columns.Add(c_champOldId, typeof(int));
            table.Columns.Add(c_champNewId, typeof(int));
            foreach (KeyValuePair<CIdentifiantElement, int> kv in m_mapElementToNewId)
            {
                DataRow row = table.NewRow();
                row[c_champNomTable] = kv.Key.TableName;
                row[c_champOldId] = kv.Key.IdElement;
                row[c_champNewId] = kv.Value;
                table.Rows.Add(row);
            }
            return ds;
        }

        //-------------------------------------------
        public void SetNewIdForElement(string strNomTable, int nOldId, int nNewId)
        {
            CIdentifiantElement id = new CIdentifiantElement(strNomTable, nOldId);
            m_mapElementToNewId[id] = nNewId;
        }

        //-------------------------------------------
        public IEnumerable<KeyValuePair<CIdentifiantElement, int>> ListeMappages
        {
            get
            {
                return m_mapElementToNewId.AsEnumerable();
            }
        }
    }
}
