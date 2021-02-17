using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.common.recherche
{
    /// <summary>
    /// Tout �l�ment permettant de trouver des objets cherchables
    /// </summary>
    public interface IChercheurObjetsCherchables
    {

        /// <summary>
        /// Retourne true si ce chercheur peut trouver des objets du type demand�
        /// </summary>
        /// <param name="objetCherche"></param>
        /// <returns></returns>
        bool CanFind(object objetCherche);

        //Remplit le r�sultat de la recherche
        void ChercheObjet( object objetCherche, CResultatRequeteRechercheObjet resultat );
    }
}
