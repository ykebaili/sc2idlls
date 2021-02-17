using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.unites
{
    public interface IUnite
    {
        string LibelleLong { get; }
        string LibelleCourt { get; }

        string GlobalId { get; }

        IClasseUnite Classe { get; }

        /// <summary>
        /// Exemple : si l'unité est le km et l'unité de base est le m
        /// le facteur to base est 1000
        /// car xKm = 1000x m
        /// </summary>
        double FacteurVersBase { get; }

        /// <summary>
        /// Exemple : si l'unité est K° et l'unité de base est le C°
        /// l'offset to base est +273
        /// car x°K = (x+273)C°
        /// </summary>
        double OffsetVersBase { get; }

    }
}
