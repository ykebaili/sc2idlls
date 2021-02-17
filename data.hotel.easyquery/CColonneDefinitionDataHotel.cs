using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using futurocom.easyquery;
using sc2i.common;

namespace data.hotel.easyquery
{
    public class CColonneDefinitionDataHotel : CColumnDefinitionSimple
    {
        //égale à L'id de la colonne conservée pour compatiblité
        private string m_strHotelColumnId = "";

        //----------------------------------------------------------
        public CColonneDefinitionDataHotel()
            :base()
        {
        }

        //----------------------------------------------------------
        public CColonneDefinitionDataHotel(string strHotelColumnId, string strName)
            : base(strName, typeof(double))
        {
            m_strHotelColumnId = strHotelColumnId;
            ForceId(m_strHotelColumnId);
        }

        //----------------------------------------------------------
        public string HotelColumnId
        {
            get
            {
                return m_strHotelColumnId;
            }
            set
            {
                m_strHotelColumnId = value;
                ForceId(m_strHotelColumnId);
            }
        }

        

        //----------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //----------------------------------------------
        public override sc2i.common.CResultAErreur Serialize(sc2i.common.C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
            if (!result )
                return result;
            result = base.Serialize(serializer);
            if (!result)
                return result;
            serializer.TraiteString(ref m_strHotelColumnId);
            return result;
        }
    }
}
