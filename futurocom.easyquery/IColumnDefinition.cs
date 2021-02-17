using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace futurocom.easyquery
{
    public interface IColumnDefinition : I2iSerializable, IElementDeQuerySource
    {
        string Id { get; }

        string ColumnName { get; set; }

        Type DataType { get; set; }

        ITableDefinition Table { get; set; }

        string ImageKey { get; set; }

        bool IsReadOnly { get; set; }
    }

    public interface IColumnDefinitionAGUID : IColumnDefinition, I2iCloneableAvecTraitementApresClonage
    {
        void ForceId(string strId);
    }
}
