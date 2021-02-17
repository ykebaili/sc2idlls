using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace futurocom.easyquery
{
    [AutoExec("Autoexec")]
    [Serializable]
    public abstract class CColumnDefinitionBase : 
        IColumnDefinitionAGUID
    {
        public const string c_strImageKey = "STD_COLUMN";

        private string m_strId = "";
        private ITableDefinition m_table = null;
        private string m_strImageKey = c_strImageKey;

        //--------------------------------------
        public CColumnDefinitionBase()
        {
            m_strId = Guid.NewGuid().ToString();
        }

        //--------------------------------------
        public static void Autoexec()
        {
            CEasyQuerySource.RegisterImageForFolder(c_strImageKey, Resource1.Column16);
        }

        //--------------------------------------
        public string ImageKey
        {
            get
            {
                return m_strImageKey;
            }
            set
            {
                m_strImageKey = value;
            }
        }

        //--------------------------------------
        public string Id
        {
            get { return m_strId; }
        }

        //--------------------------------------
        public abstract string ColumnName { get; set; }

        //--------------------------------------
        public abstract Type DataType { get; set; }

        //--------------------------------------
        public abstract bool IsReadOnly { get; set; }

        //------------------------------------------------
        public ITableDefinition Table
        {
            get
            {
                return m_table;
            }
            set
            {
                if (Table != value)
                {
                    if (Table != null)
                        Table.RemoveColumn(this);
                    m_table = value;
                    if (m_table.GetColumn(Id) == null)
                        m_table.AddColumn(this);
                }
            }
        }

        //------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //------------------------------------------------
        public virtual CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strId);
            serializer.TraiteString(ref m_strImageKey);
            if (serializer.IsForClone)
                m_strId = Guid.NewGuid().ToString();
            return result;
        }


        //-------------------------------------------------------------
        public void TraiteApresClonage(I2iSerializable source)
        {
            m_strId = Guid.NewGuid().ToString();
        }

        //-------------------------------------------------------------
        public void ForceId(string strId)
        {
            m_strId = strId;
        }
    }

        
}
