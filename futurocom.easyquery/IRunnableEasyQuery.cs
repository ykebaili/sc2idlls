using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;
using sc2i.expression.datatable;

namespace futurocom.easyquery
{
    public interface IRunnableEasyQuery : IElementAVariableInstance,
        IElementADataTableDynamique
    {
        //-----------------------------------------------
        IEnumerable<CEasyQuerySource> Sources { get; }

        //-----------------------------------------------
        string Libelle{get;}
        
    }
}
