using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using futurocom.easyquery;
using sc2i.data;
using sc2i.common;

namespace sc2i.data.dynamic.easyquery
{
    [Serializable]
    public class CColumnDefinitionChampDeTable : IColumnDefinition
    {
        private string m_strNomChamp = "";
        private string m_strNomColonne = "";
        private Type m_typeDonnee = typeof(string);
        private ITableDefinition m_tableDefinition = null;

        //-------------------------------------------------
        public CColumnDefinitionChampDeTable()
        {
        }

        //-------------------------------------------------
        public CColumnDefinitionChampDeTable(
            ITableDefinition tableDefinition,
            CInfoChampTable info)
            :this()
        {
            m_tableDefinition = tableDefinition;
            m_strNomChamp = info.NomChamp;
            m_strNomColonne = info.NomConvivial;
            if (m_strNomColonne.Trim().Length == 0)
                m_strNomColonne = info.NomChamp;
            m_typeDonnee = info.TypeDonnee;
        }

        //-------------------------------------------------
        public string ColumnName
        {
            get
            {
                return m_strNomColonne;
            }
            set
            {
                m_strNomColonne = value;
            }
        }

        //-------------------------------------------------
        public Type DataType
        {
            get
            {
                return m_typeDonnee;
            }
            set
            {
                m_typeDonnee = value;
            }
        }

        
           

        //-------------------------------------------------
        public string Id
        {
            get { return m_strNomColonne; }
        }

        //-------------------------------------------------
        public string ImageKey
        {
            get
            {
                return "";
            }
            set
            {
            }
        }

        //-------------------------------------------------
        public bool IsReadOnly
        {
            get
            {
                return true;
            }
            set
            {
            }
        }

        //-------------------------------------------------
        public ITableDefinition Table
        {
            get
            {
                return m_tableDefinition;
            }
            set
            {
                m_tableDefinition=  value;
            }
        }

        //-------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //-------------------------------------------------
        public CResultAErreur Serialize(sc2i.common.C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
            if ( !result )
                return result;
            serializer.TraiteString ( ref m_strNomChamp );
            serializer.TraiteString ( ref m_strNomColonne );
            serializer.TraiteType ( ref m_typeDonnee );
            return result;

        }
    }
}
