using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using sc2i.common;

namespace futurocom.easyquery
{
    /*public class CTableDefinitionSimple : CTableDefinitionBase
    {
        private string m_strName = "";
        private string m_strId = "";

        //------------------------------------------------
        public CTableDefinitionSimple()
            :base()
        {
            m_strId = Guid.NewGuid().ToString();
        }

        //------------------------------------------------
        public CTableDefinitionSimple(CEasyQuerySource laBase)
            :base ( laBase )
        {
            m_strId = Guid.NewGuid().ToString();
        }

        //------------------------------------------------
        public override string Id
        {
            get
            {
                return m_strId;
            }
        }
        
        //------------------------------------------------
        public override string TableName
        {
            get
            {
                return m_strName;
            }
            set
            {
                m_strName = value;
            }
        }

        //------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //------------------------------------------------
        public override CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (result)
                result = base.Serialize(serializer);
            if ( !result )
                return result;

            serializer.TraiteString(ref m_strId);
            serializer.TraiteString(ref m_strName);

            return result;
        }

        //------------------------------------------------
        public static CTableDefinitionSimple FromDataTable ( CEasyQuerySource laBase, DataTable table )
        {
            CTableDefinitionSimple newTable = new CTableDefinitionSimple( laBase );
            newTable.TableName = table.TableName;
            foreach ( DataColumn col in table.Columns )
            {
                newTable.AddColumn ( CColumnDefinitionSimple.FromDataColumn ( col ));
            }
            laBase.AddTable(newTable);
            return newTable;
        }
    

    } */
}
