using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace futurocom.easyquery
{
    public interface ITableFiller
    {
        void ClearCache(ITableDefinition table);

        bool CanFill(ITableDefinition tableDefinition);

        DataTable GetData(ITableDefinition tableDefinition, params string[] strIdsColonnesSource);
    }
}
