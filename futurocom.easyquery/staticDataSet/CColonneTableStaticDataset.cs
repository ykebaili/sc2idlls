using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using futurocom.easyquery;
using sc2i.common;
using System.Data;

namespace futurocom.easyquery.staticDataSet
{
    public class CColonneTableStaticDataset : CColumnDefinitionSimple
    {
        
        public CColonneTableStaticDataset()
        {
        }

        

        private int GetNumVersion()
        {
            return 0;
        }

        //----------------------------------------------
        public static CColonneTableStaticDataset GetForDataCol(DataColumn col)
        {
            CColonneTableStaticDataset retour = new CColonneTableStaticDataset();
            retour.ColumnName = col.ColumnName;
            retour.DataType = col.DataType;
            return retour;
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
            return result;
        }
    }
}
