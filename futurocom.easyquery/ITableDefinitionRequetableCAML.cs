using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using futurocom.easyquery.CAML;

namespace futurocom.easyquery
{
    public interface ITableDefinitionRequetableCAML : ITableDefinition
    {
        IEnumerable<CCAMLItemField> CAMLFields { get; }

        CResultAErreur GetDatasWithCAML(CEasyQuerySource source, 
            CEasyQuery easyQuery,
            CCAMLQuery CAMLquery, 
            params string[] strIdsColonnesSources);
    }
}
