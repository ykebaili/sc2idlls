using sc2i.common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace futurocom.easyquery.staticDataSet
{
    public class CODEQTableManuelle : CODEQBase
    {
        private DataTable m_table = new DataTable();

        //-----------------------------------------------------------
        public CODEQTableManuelle()
        {
            this.NomFinal = I.T("Manual table|20015");
        }

        //-----------------------------------------------------------
        public override string TypeName
        {
            get
            {
                return I.T("Manual table|20015");
            }
        }

        //-----------------------------------------------------------
        protected override CResultAErreur GetDatasHorsCalculees(CListeQuerySource sources)
        {
            CResultAErreur result = CResultAErreur.True;
            m_table.AcceptChanges();
            result.Data = m_table;
            return result;
        }

        //-----------------------------------------------------------
        public override IEnumerable<IColumnDeEasyQuery> GetColonnesFinales()
        {
            List<IColumnDeEasyQuery> lstRetour = new List<IColumnDeEasyQuery>();
            foreach (DataColumn col in m_table.Columns)
            {
                CColumnEQSimple cs = new CColumnEQSimple(Id +"~"+ col.ColumnName, col.ColumnName, col.DataType);
                lstRetour.Add(cs);
            }
            return lstRetour;
        }

        //-----------------------------------------------------------
        public DataTable Table
        {
            get
            {
                if (m_table == null)
                    m_table = new DataTable();
                return m_table;
            }
        }

        //-----------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //-----------------------------------------------------------
        public override CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if ( result )
                result = base.Serialize(serializer);
            if (result)
            {
                object obj = m_table;
                result = serializer.TraiteSerializable(ref obj);
                if (serializer.Mode == ModeSerialisation.Lecture)
                {
                    m_table = obj as DataTable;
                    if (m_table == null)
                        m_table = new DataTable();
                }
            }
            return result;
        }
    }
}
