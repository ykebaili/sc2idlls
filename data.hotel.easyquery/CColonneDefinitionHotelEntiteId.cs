using futurocom.easyquery;
using sc2i.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.easyquery
{
    public class CColonneDefinitionHotelEntiteId : CColumnDefinitionSimple
    {
        public const string c_IdCol = "HOTEL_ENTITY";
        //-------------------------------------------------------------------------------------------
        public CColonneDefinitionHotelEntiteId()
            : base()
        {
            ForceId(c_IdCol);
        }

        //-------------------------------------------------------------------------------------------
        public CColonneDefinitionHotelEntiteId(string strName)
            : base(strName, typeof(string))
        {
            ForceId(c_IdCol);
        }

        //-------------------------------------------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //-------------------------------------------------------------------------------------------
        public override sc2i.common.CResultAErreur Serialize(sc2i.common.C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if ( result )
                result = base.Serialize(serializer);
            return result;
        }
    }
}
