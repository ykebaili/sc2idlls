using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace futurocom.easyquery
{
    /// <summary>
    /// Colonne issue d'
    /// </summary>
    public interface IColumnDeEasyQuery : I2iSerializable
    {
        string Id { get; }
        string ColumnName { get; set; }
        Type DataType { get; set; }

        CResultAErreur OnRemplaceColSource(IColumnDeEasyQuery oldCol, IColumnDeEasyQuery newCol);
    }

    public interface IColumnDeEasyQueryAGUID : IColumnDeEasyQuery,
        I2iCloneableAvecTraitementApresClonage
    {
        void ForceId(string strId);
    }
}
