using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace futurocom.easyquery.staticDataSet
{
    public class CTableDefinitionStaticDataSet : CTableDefinitionBase
    {
        private string m_strId = "";
        private string m_strTableName = "";
        
        //---------------------------------------------------
        public CTableDefinitionStaticDataSet()
            :base()
        {
            m_strId = Guid.NewGuid().ToString();
        }

        //---------------------------------------------------
        public CTableDefinitionStaticDataSet(CEasyQuerySource laBase)
            : base(laBase)
        {
            m_strId = Guid.NewGuid().ToString();
        }

        //----------------------------------------
        public override string Id
        {
            get
            {
                return m_strId;
            }
        }

        //----------------------------------------
        public void SetId ( string strId )
        {
            if ( strId != null )
                m_strId = strId;
        }

        //----------------------------------------
        public override string TableName
        {
            get
            {
                return m_strTableName;
            }
            set
            {
                m_strTableName = value;
            }
        }


        //----------------------------------------
        public override CResultAErreur GetDatas(CEasyQuerySource source, params string[] strIdsColonnesSource)
        {
            CResultAErreur result = CResultAErreur.True;
            CStaticDataSetConnexion cnx = source.Connexion as CStaticDataSetConnexion;
            if (cnx != null)
            {
                System.Data.DataTable table = cnx.GetData(this, strIdsColonnesSource);
                if (table != null)
                    result.Data = table;
            }
            return result;
        }

        //----------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //----------------------------------------
        public override CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (result)
                result = base.Serialize(serializer);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strId);
            serializer.TraiteString(ref m_strTableName);
            if ( serializer.IsForClone)
                m_strId = Guid.NewGuid().ToString();
            return result;
        }
    }
}
