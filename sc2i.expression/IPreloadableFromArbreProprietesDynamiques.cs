using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.expression
{
    /// <summary>
    /// Un objet qui peut se précharger à partir d'un arbre de propriétés dynamiques
    /// </summary>
    public interface IPreloadableFromArbreProprietesDynamiques
    {
        void Preload(CArbreDefinitionsDynamiques arbre);
    }
}
