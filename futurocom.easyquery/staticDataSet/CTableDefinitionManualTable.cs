using sc2i.common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace futurocom.easyquery.staticDataSet
{
    public class CTableDefinitionManualTable : CTableDefinitionBase
    {
        //-------------------------------------------------------
        public override string Id
        {
            get { return "ID_MANUAL_TABLE"; }
        }

        //-------------------------------------------------------
        public override string TableName
        {
            get
            {
                return I.T(("Manual table|20015"));
            }
            set
            {
            }
        }

        //-------------------------------------------------------
        public override CResultAErreur GetDatas(CEasyQuerySource source, params string[] strIdsColonnesSource)
        {
            CResultAErreur result = CResultAErreur.True;
            result.Data = new DataTable();
            return result;
        }

        //-------------------------------------------------------
        public override IObjetDeEasyQuery GetObjetDeEasyQueryParDefaut()
        {
            return new CODEQTableManuelle();
        }
    }
}
