using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.recherche
{
    /// <summary>
    /// Tout objet qui peut être cherché
    /// </summary>
    public interface IObjetCherchable
    {
        CRequeteRechercheObjet GetRequeteRecherche();
    }
}
