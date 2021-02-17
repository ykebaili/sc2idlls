using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.expression.FonctionsDynamiques
{
    /// <summary>
    /// Un élément qui possède ses propres définitions de fonctions dynamiques
    /// </summary>
    public interface IElementAFonctionsDynamiques
    {
        IEnumerable<CFonctionDynamique> FonctionsDynamiques { get; }
        CFonctionDynamique GetFonctionDynamique(string strIdFonction);
    }
}
