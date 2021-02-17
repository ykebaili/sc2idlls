using data.hotel.client.query;
using futurocom.easyquery;
using sc2i.common;
using sc2i.expression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.easyquery.filtre
{
    public class CDHFiltreOr : CDHFiltreASousElements
    {
        //--------------------------------------------------
        public CDHFiltreOr()
        {

        }

        //--------------------------------------------------
        public override string GetLibelle(IObjetDeEasyQuery table)
        {
            return I.T("Or|20008");
        }

        //--------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //--------------------------------------------------
        public override CResultAErreur Serialize ( C2iSerializer serializer )
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
            if ( result )
                result = base.Serialize ( serializer );
            if (!result)
                return result;
            return result;
        }


        //--------------------------------------------------
        protected override CTestDataHotelASousElements AlloueTestFinal()
        {
            return new CTestDataHotelOr();
        }
    }
}
