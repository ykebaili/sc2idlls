using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.data;
using futurocom.easyquery;
using sc2i.common;

namespace sc2i.data.dynamic.easyquery
{
    [Serializable]
    public class CColumnDeEasyQueryChampDeRequete : 
        C2iChampDeRequete, 
        IColumnDeEasyQueryAGUID
    {
        private string m_strId = "";
        //---------------------------------------------------
        public CColumnDeEasyQueryChampDeRequete ()
        {
            m_strId = Guid.NewGuid().ToString();
        }

        //---------------------------------------------------
        public CColumnDeEasyQueryChampDeRequete ( 
			string strNomChampFinal,
			CSourceDeChampDeRequete source,
			Type typeDonneeAvantAgregation,
			OperationsAgregation operation,
			bool bGroupBy )
            :base ( strNomChampFinal,
            source,
            typeDonneeAvantAgregation,
            operation,
            bGroupBy )
        {
            m_strId = Guid.NewGuid().ToString();
        }

        //---------------------------------------------------
        public string ColumnName
        {
            get
            {
                return this.NomChamp;
            }
            set
            {
                NomChamp = value;
            }
        }

        //---------------------------------------------------
        public Type DataType
        {
            get
            {
                return TypeDonneeFinalForce == null?TypeDonneeAvantAgregation:TypeDonneeFinalForce;
            }
            set
            {
            }
        }

        //---------------------------------------------------
        public string Id
        {
            get { return m_strId; }
        }

        //---------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //---------------------------------------------------
        public override CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (result)
                result = base.Serialize(serializer);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strId);
            if ( serializer.IsForClone)
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
            m_strId = Guid.NewGuid().ToString();
        }

        //-------------------------------------------------------------
        public CResultAErreur OnRemplaceColSource(IColumnDeEasyQuery oldCol, IColumnDeEasyQuery newCol)
        {
            return CResultAErreur.True;
        }
    }
}
