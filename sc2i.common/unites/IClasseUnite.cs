using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.unites
{
    public interface IClasseUnite
    {
        string Libelle { get; }
        string GlobalId { get; }
        string UniteBase { get; }

    }
}
