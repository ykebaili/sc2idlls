using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using futurocom.easyquery;
using sc2i.common;
using futurocom.easyquery.CAML;
using sc2i.expression;

namespace data.hotel.easyquery
{
    public class CTableDefinitionDataHotel : CTableDefinitionBase
    {
        private string m_strTableId = "";
        private string m_strNomTable;

        

        //----------------------------------------------
        public CTableDefinitionDataHotel()
        {
        }

        //----------------------------------------------
        public CTableDefinitionDataHotel(
            string strId,
            string strNomTable)
        {
            m_strNomTable = strNomTable;
            m_strTableId = strId;
        }

        //----------------------------------------------
        public override string Id
        {
            get { return m_strTableId; }
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
            serializer.TraiteString(ref m_strTableId);

            return result;
        }

        //----------------------------------------------
        public override CResultAErreur GetDatas(CEasyQuerySource source, params string[] strIdsColonnesSource)
        {
            CResultAErreur result = CResultAErreur.True;

            CDataHotelConnexion filler = source.Connexion as CDataHotelConnexion;
            if (filler != null)
            {
                List<string> strColsHotel = new List<string>();
                foreach ( IColumnDefinition col in Columns )
                {
                    if ( strIdsColonnesSource.Contains ( col.Id ) )
                    {
                        CColonneDefinitionDataHotel colHotel = col as CColonneDefinitionDataHotel;
                        if ( colHotel != null )
                            strColsHotel.Add ( colHotel.Id );
                    }
                }
            }
            return result;
        }

        //----------------------------------------------
        public override IObjetDeEasyQuery GetObjetDeEasyQueryParDefaut()
        {
            return new CODEQTableFromDataHotel(this);
        }

        
        
        
    }
}
