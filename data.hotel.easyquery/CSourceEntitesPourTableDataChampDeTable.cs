using futurocom.easyquery;
using sc2i.common;
using sc2i.expression;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.easyquery
{
    public class CSourceEntitesPourTableDataChampDeTable : ISourceEntitesPourTableDataHotel
    {
        private string m_strIdTable = "";
        private string m_strIdColonne = "";

        //------------------------------------------------------
        public CSourceEntitesPourTableDataChampDeTable()
        {
        }

        //------------------------------------------------------
        public string IdTable
        {
            get
            {
                return m_strIdTable;
            }
            set
            {
                m_strIdTable = value;
            }
        }

        //------------------------------------------------------
        public string IdColonne
        {
            get
            {
                return m_strIdColonne;
            }
            set
            {
                m_strIdColonne = value;
            }
        }


        //------------------------------------------------------
        public IEnumerable<string> GetListeIdsEntites(CEasyQuery query)
        {
            HashSet<string> lst = new HashSet<string>();
            if ( query != null )
            {
                CODEQBase table = query.GetObjet(m_strIdTable) as CODEQBase;
                IColumnDeEasyQuery colSel = null;
                if ( table != null )
                {
                    foreach (IColumnDeEasyQuery col in table.Columns)
                        if (col.Id == m_strIdColonne)
                            colSel = col;
                }
                if ( colSel != null )
                {
                    CResultAErreur result = table.GetDatas(new CListeQuerySource(query.Sources));
                    if ( result && result.Data is DataTable )
                    {
                        DataTable tableRes = result.Data as DataTable;
                        if (tableRes.Columns[colSel.ColumnName] != null)
                        {
                            foreach ( DataRow row in tableRes.Rows )
                            {
                                object val = row[colSel.ColumnName];
                                if (val != DBNull.Value && val != null)
                                    lst.Add(val.ToString());
                            }
                        }
                    }
                }
            }
            return lst;
        }

        //------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //------------------------------------------------------
        public CResultAErreur Serialize ( C2iSerializer serializer )
        {
            int  nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if ( !result )
                return result;
            serializer.TraiteString ( ref m_strIdTable );
            serializer.TraiteString ( ref m_strIdColonne );
            return result;
        }

        
    }
}
