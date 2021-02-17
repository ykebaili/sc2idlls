using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;

namespace sc2i.data.dynamic
{
    public interface IElementAVariablesDynamiquesAvecContexteDonnee : 
        IElementAVariablesDynamiques,
        IObjetAContexteDonnee
    {
        int IdSession { get; }

    }
}
