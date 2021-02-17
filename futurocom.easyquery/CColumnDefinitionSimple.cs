using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using sc2i.common;

namespace futurocom.easyquery
{
    [Serializable]
    public class CColumnDefinitionSimple : CColumnDefinitionBase
    {
        private string m_strName = "";
        private Type m_type = typeof(string);
        private bool m_bIsReadOnly = false;
        //------------------------------------------------
        public CColumnDefinitionSimple()
            :base()
        {
        }

        //------------------------------------------------
        public CColumnDefinitionSimple(string strName, Type dataType)
        {
            DataType = dataType;
            ColumnName = strName;
        }


        //------------------------------------------------
        public override string ColumnName
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
        public override Type DataType
        {
            get
            {
                return m_type;
            }
            set
            {
                m_type = value;
            }
        }

        //------------------------------------------------
        public override bool IsReadOnly
        {
            get
            {
                return m_bIsReadOnly;
            }
            set
            {
                m_bIsReadOnly = value;
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
            if (!result)
                return result;
            serializer.TraiteType(ref m_type);
            serializer.TraiteString(ref m_strName);
            return result;
        }

        //------------------------------------------------
        public static CColumnDefinitionSimple FromDataColumn(DataColumn col)
        {
            CColumnDefinitionSimple newCol = new CColumnDefinitionSimple();
            newCol.ColumnName = col.ColumnName;
            newCol.DataType = col.DataType;
            return newCol;
        }

    }
}
