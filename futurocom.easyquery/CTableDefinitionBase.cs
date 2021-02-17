using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using sc2i.common;
using System.Drawing;

namespace futurocom.easyquery
{
    [AutoExec("Autoexec")]
    [Serializable]
    public abstract class CTableDefinitionBase : ITableDefinition
    {
        private const string c_strImageKey = "STD_TABLE";

        private CEasyQuerySource m_base = null;
        private string m_strSourceId = "";
        private string m_strFolderId = "";
        private List<IColumnDefinition> m_listeColonnes = new List<IColumnDefinition>();

        private string m_strImageKey = c_strImageKey;


        //------------------------------------------------
        public CTableDefinitionBase()
        {
        }

        //------------------------------------------------
        public CTableDefinitionBase(CEasyQuerySource laBase)
        {
            m_base = laBase;
        }

        //------------------------------------------------
        public string SourceId
        {
            get
            {
                return m_strSourceId;
            }
            set
            {
                m_strSourceId = value;
            }
        }

       
        //------------------------------------------------
        public static void Autoexec()
        {
            CEasyQuerySource.RegisterImageForFolder(c_strImageKey, Resource1.table16);
        }

        //------------------------------------------------
        public CEasyQuerySource Base
        {
            get
            {
                return m_base;
            }
            set
            {
                m_base = value;
            }
        }

        //------------------------------------------------
        public string FolderId
        {
            get
            {
                return m_strFolderId;
            }
            set
            {
                m_strFolderId = value;
            }
        }


        //------------------------------------------------
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


        //------------------------------------------------
        public abstract string Id{get;}

        //------------------------------------------------
        public abstract string TableName { get;set;}
      
        //------------------------------------------------
        public virtual IEnumerable<IColumnDefinition> Columns
        {
            get { return m_listeColonnes.AsReadOnly(); }
        }

        //------------------------------------------------
        public void AddColumn(IColumnDefinition column)
        {
            if (GetColumn(column.Id) == null)
            {
                m_listeColonnes.Add(column);
                if ( column.Table != this )
                    column.Table = this;
            }
        }

        //------------------------------------------------
        public void RemoveColumn(IColumnDefinition column)
        {
            m_listeColonnes.Remove ( column );
        }

        //------------------------------------------------
        public IColumnDefinition GetColumn(string strIdOrName)
        {
            return m_listeColonnes.FirstOrDefault ( c=>c.Id == strIdOrName || c.ColumnName.ToUpper() == strIdOrName.ToUpper() );
        }



        //------------------------------------------------
        public abstract CResultAErreur GetDatas( CEasyQuerySource source, params string[] strIdsColonnesSource );

        //------------------------------------------------
        private int GetNumVersion()
        {
            return 1;
            //1 : ajout de ConnexionId
        }

        //------------------------------------------------
        public virtual CResultAErreur Serialize(sc2i.common.C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
            if ( !result )
                return result;
            serializer.TraiteString(ref m_strImageKey);
            result = serializer.TraiteListe<IColumnDefinition>(m_listeColonnes);
            if ( !result )
                return result;
            foreach ( IColumnDefinition col in m_listeColonnes )
                col.Table = this;
            if (nVersion >= 1)
                serializer.TraiteString(ref m_strSourceId);

            return result;
        }

        //------------------------------------------------
        public virtual IObjetDeEasyQuery GetObjetDeEasyQueryParDefaut()
        {
            return new CODEQTableFromBase(this);
        }


    }
}
