using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using futurocom.easyquery;
using sc2i.data.dynamic;
using sc2i.common;

namespace sc2i.data.dynamic.easyquery
{
    public class CColumnDefinitionChampCustom : IColumnDefinition
    {
        private ITableDefinition m_table = null;
        private CDbKey m_dbKeyChampCustom = null;
        private Type m_typeDonnee = typeof(string);
        private string m_strNomColonne = "";

        //---------------------------------------------------
        public CColumnDefinitionChampCustom()
        {
        }

        //---------------------------------------------------
        public CColumnDefinitionChampCustom(ITableDefinition table,
            CChampCustom champ)
        {
            m_table = table;
            //m_dbKeyChampCustom = champ != null ? champ.DbKey : null;
            if (champ != null)
            {
                m_dbKeyChampCustom = champ.DbKey;
                m_strNomColonne = champ.Nom;
                m_typeDonnee = champ.TypeDonneeChamp.TypeDotNetAssocie;
            }
        }

        //---------------------------------------------------
        [ExternalReferencedEntityDbKey(typeof(CChampCustom))]
        public CDbKey DbKeyChampCustom
        {
            get
            {
                return m_dbKeyChampCustom;
            }
        }

        //---------------------------------------------------
        public string ColumnName
        {
            get
            {
                return m_strNomColonne;
            }
            set
            {
            }
        }

        //---------------------------------------------------
        public Type DataType
        {
            get
            {
                return m_typeDonnee;
            }
            set
            {
            }
        }

        //---------------------------------------------------
        public string Id
        {
            get
            {
                return "#CHP_" + CChampCustom.GetIdFromDbKey(m_dbKeyChampCustom);
            }
        }

        //---------------------------------------------------
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

        //---------------------------------------------------
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
        //---------------------------------------------------

        public ITableDefinition Table
        {
            get
            {
                return m_table;
            }
            set
            {
                m_table = value;
            }
        }

        //---------------------------------------------------
        private int GetNumVersion()
        {
            //return 0;
            return 1; // Passagede Id Champ à DbKey
        }


        //---------------------------------------------------
        public CResultAErreur Serialize(sc2i.common.C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            //TESTDBKEYTODO : remplace m_nIDChampCustom par 
            if (nVersion < 1)
                serializer.ReadDbKeyFromOldId(ref m_dbKeyChampCustom, typeof(CChampCustom));
            else
                serializer.TraiteDbKey(ref m_dbKeyChampCustom);
            serializer.TraiteString(ref m_strNomColonne);
            serializer.TraiteType(ref m_typeDonnee);
            return result;
        }
    }
}
