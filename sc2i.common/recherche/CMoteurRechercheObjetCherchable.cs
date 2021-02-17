using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.common.recherche
{
    public static class CMoteurRechercheObjetCherchable
    {
        public static Dictionary<Type, bool> m_typesChercheursConnus = new Dictionary<Type, bool>();

        /// <summary>
        /// enregistre un nouveau chercheur d'objets
        /// </summary>
        /// <param name="tp"></param>
        public static void RegisterChercheur ( Type tp )
        {
            m_typesChercheursConnus[tp] = true;
        }


        //---------------------------------------------------------------------------------
        public static void ChercheObjet ( CRequeteRechercheObjet requete, CResultatRequeteRechercheObjet resultat )
        {
            foreach (Type tp in m_typesChercheursConnus.Keys)
            {
                IChercheurObjetsCherchables chercheur = (IChercheurObjetsCherchables)Activator.CreateInstance(tp);
                if( chercheur.CanFind ( requete.ObjetCherché ) )
                    chercheur.ChercheObjet( requete.ObjetCherché, resultat);
            }
        }
    }
}
