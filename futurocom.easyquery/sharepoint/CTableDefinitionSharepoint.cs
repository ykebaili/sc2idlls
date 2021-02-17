using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using futurocom.easyquery;
using sc2i.common;
using futurocom.easyquery.CAML;

namespace futurocom.easyquery.sharepoint30
{
    public class CTableDefinitionSharepoint : CTableDefinitionBase, ITableDefinitionRequetableCAML
    {

        private string m_strGUId;
        private string m_strNomTable;
        private string m_strDescription;

        //----------------------------------------------
        public CTableDefinitionSharepoint()
        {
        }

        //----------------------------------------------
        public CTableDefinitionSharepoint(
            string strGUID,
            string strNomTable,
            string strDescription)
        {
            m_strGUId = strGUID;
            m_strNomTable = strNomTable;
            m_strDescription = strDescription;
        }

        //----------------------------------------------
        public override string Id
        {
            get { return m_strGUId; }
        }

        //----------------------------------------------
        public override string TableName
        {
            get
            {
                return m_strNomTable;
            }
            set
            {
                m_strNomTable = value;
            }
        }

        //----------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //----------------------------------------------
        public override CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = base.Serialize(serializer);
            if ( !result )
                return result;
            serializer.TraiteString(ref m_strNomTable);
            serializer.TraiteString(ref m_strGUId);
            serializer.TraiteString(ref m_strDescription);
            return result;
        }

        //----------------------------------------------
        public override CResultAErreur GetDatas(CEasyQuerySource source, params string[] strIdsColonnesSource)
        {
            return GetDatasWithCAML(source, null, null, strIdsColonnesSource);
        }

        //----------------------------------------------
        public CResultAErreur GetDatasWithCAML ( 
            CEasyQuerySource source, 
            CEasyQuery easyQuery,
            CCAMLQuery camlquery, 
            params string[] strIdsColonnesSource)
        {
            CResultAErreur result = CResultAErreur.True;

            CSharepointConnexion filler = source.Connexion as CSharepointConnexion;
            if (filler != null)
            {
                List<string> strColsShp = new List<string>();
                foreach ( IColumnDefinition col in Columns )
                {
                    if ( strIdsColonnesSource.Contains ( col.Id ) )
                    {
                        CColonneDefinitionSharepoint colShp = col as CColonneDefinitionSharepoint;
                        if ( colShp != null )
                            strColsShp.Add ( colShp.SharepointId );
                    }
                }
                string strCAMLQuery = "";
                if (camlquery != null && easyQuery != null)
                    strCAMLQuery = camlquery.GetXmlText(easyQuery);
                System.Data.DataTable table = filler.GetDataWithCAML(
                    this, 
                    strCAMLQuery,
                    strColsShp.ToArray());
                result.Data = table;
            }
            return result;
        }

        //-----------------------------------------------------
        public IEnumerable<CCAMLItemField> CAMLFields
        {
            get
            {
                List<CCAMLItemField> fields = new List<CCAMLItemField>();
                foreach (IColumnDefinition colonne in Columns)
                {
                    CColonneDefinitionSharepoint col = colonne as CColonneDefinitionSharepoint;
                    if (col != null)
                    {
                        CCAMLItemField field = new CCAMLItemField(
                            col.ColumnName,
                            "Name",
                            col.SharepointId);
                        fields.Add(field);
                    }
                }
                return fields.AsReadOnly();
            }
        }

        
        
    }
}
