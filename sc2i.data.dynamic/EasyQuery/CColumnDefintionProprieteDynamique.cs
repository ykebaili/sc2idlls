using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using futurocom.easyquery;
using sc2i.expression;
using sc2i.common;

namespace sc2i.data.dynamic.easyquery
{
    public class CColumnDefintionProprieteDynamique : IColumnDefinition
    {
        private CDefinitionProprieteDynamique m_definition = null;
        private CTableDefinitionFramework m_table = null;


        //-------------------------------------------------------------------------
        public CColumnDefintionProprieteDynamique()
        {
        }

        //-------------------------------------------------------------------------
        public CColumnDefintionProprieteDynamique(
            ITableDefinition table,
            CDefinitionProprieteDynamique def)
        {
            m_definition = def;
        }

        #region IColumnDefinition Membres
        //-------------------------------------------------------------------------
        public string ColumnName
        {
            get
            {
                if (m_definition != null)
                    return m_definition.Nom;
                return "?";
            }
            set
            {
            }
        }

        //-------------------------------------------------------------------------
        public Type DataType
        {
            get
            {
                if (m_definition != null)
                    return m_definition.TypeDonnee.TypeDotNetNatif;
                return typeof(string);
            }
            set
            {
            }
        }

        //-------------------------------------------------------------------------
        public string Id
        {
            get { return m_definition.NomPropriete; }
        }

        //-------------------------------------------------------------------------
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

        //-------------------------------------------------------------------------
        public bool IsReadOnly
        {
            get
            {
                if (m_definition != null)
                    return m_definition.IsReadOnly;
                return true;
            }
            set
            {
            }
        }

        //-------------------------------------------------------------------------
        public ITableDefinition Table
        {
            get
            {
                return m_table;
            }
            set
            {
                m_table = value as CTableDefinitionFramework;
            }
        }

        #endregion

        //-----------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //-----------------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            result = serializer.TraiteObject<CDefinitionProprieteDynamique>(ref m_definition);
            if (!result)
                return result;
            return result;
        }
    }
}
