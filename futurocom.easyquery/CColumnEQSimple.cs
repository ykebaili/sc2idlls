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
    public class CColumnEQSimple : IColumnDeEasyQueryAGUID, I2iSerializable, I2iCloneableAvecTraitementApresClonage
    {
        private string m_strNomColonne;
        private Type m_typeDonnee = typeof(string);
        private string m_strIdColonne;

        //-----------------------------------------
        public CColumnEQSimple()
        {
            m_strIdColonne = Guid.NewGuid().ToString();
        }

        //-----------------------------------------
        public CColumnEQSimple(string strId, string strColumnName, Type typeDonnee)
        {
            m_strIdColonne = strId;
            m_strNomColonne = strColumnName;
            m_typeDonnee = typeDonnee;
        }

       

        //-----------------------------------------
        public string Id
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
            return CResultAErreur.True;
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
            serializer.TraiteString(ref m_strNomColonne);
            serializer.TraiteType(ref m_typeDonnee);
            serializer.TraiteString(ref m_strIdColonne);
            if ( serializer.IsForClone)
                m_strIdColonne = Guid.NewGuid().ToString();
            return result;
        }




        //-------------------------------------------------------------
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
