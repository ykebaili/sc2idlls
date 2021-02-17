using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace futurocom.easyquery
{
    /// <summary>
    /// Colonne issue d'
    /// </summary>
    [Serializable]
    [ReplaceClass("futurocom.easyquery.CColonneFromSource")]
    public class CColumnEQFromSource :
        IColumnDeEasyQueryAGUID,
        I2iSerializable
    {
        private string m_strIdColonneSource;
        private string m_strNomColonne;
        private Type m_typeDonnee = typeof(string);
        private string m_strIdColonne;

        //-----------------------------------------
        public CColumnEQFromSource()
        {
            m_strIdColonne = Guid.NewGuid().ToString();
        }

        //-----------------------------------------
        public CColumnEQFromSource(string strIdColonneSource, string strColumnName, Type typeDonnee)
        {
            m_strIdColonne = Guid.NewGuid().ToString();
            m_strIdColonneSource = strIdColonneSource;
            m_strNomColonne = strColumnName;
            m_typeDonnee = typeDonnee;
        }

        //-----------------------------------------
        public CColumnEQFromSource(IColumnDefinition column)
        {
            m_strIdColonne = Guid.NewGuid().ToString();
            m_strNomColonne = column.ColumnName;
            m_strIdColonneSource = column.Id;
            m_typeDonnee = column.DataType;
        }

        //-----------------------------------------
        public CColumnEQFromSource(IColumnDeEasyQuery colSource)
        {
            m_strIdColonne = Guid.NewGuid().ToString();
            m_strNomColonne = colSource.ColumnName;
            m_strIdColonneSource = colSource.Id;
            m_typeDonnee = colSource.DataType;
        }

        //-----------------------------------------
        public string Id
        {
            get
            {
                return m_strIdColonne;
            }
        }

        //-----------------------------------------
        public string IdColumnSource
        {
            get
            {
                return m_strIdColonneSource;
            }
        }

        //-----------------------------------------
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

        //-----------------------------------------
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

        //-----------------------------------------
        public CResultAErreur OnRemplaceColSource(IColumnDeEasyQuery oldCol, IColumnDeEasyQuery newCol)
        {
            CResultAErreur result = CResultAErreur.True;
            if (oldCol.Id == IdColumnSource)
            {
                m_strIdColonneSource = newCol.Id;
                m_typeDonnee = oldCol.DataType;
            }
            return result;
        }


        //-----------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //-----------------------------------------
        public virtual CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strIdColonneSource);
            serializer.TraiteString(ref m_strNomColonne);
            serializer.TraiteType(ref m_typeDonnee);
            serializer.TraiteString(ref m_strIdColonne);
            if ( serializer.IsForClone)
                m_strIdColonne = Guid.NewGuid().ToString();
            return result;
        }




        //-------------------------------------------------------
        public void TraiteApresClonage(I2iSerializable source)
        {
            m_strIdColonne = Guid.NewGuid().ToString();
        }

        //-------------------------------------------------------------
        public void ForceId(string strId)
        {
            m_strIdColonne = Guid.NewGuid().ToString();
        }
    }


}
