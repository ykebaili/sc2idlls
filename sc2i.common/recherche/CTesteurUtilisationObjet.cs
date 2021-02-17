using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.recherche
{
    /// <summary>
    /// Un objet cablage de tester l'utilisation d'un type d'objet particulier
    /// dans certains objets.
    /// Par exemple, il en existe un qui teste l'utilisation des CDefinitionProprieteDynamique
    /// dans les C2iExpression
    /// </summary>
    public interface ITesteurUtilisationObjet
    {
        /// <summary>
        /// Retourne true si l'objet utilisateur utilise l'objet cherché
        /// </summary>
        /// <param name="objetUtilisateur"></param>
        /// <param name="objetCherche"></param>
        /// <returns></returns>
        bool DoesUse(object objetUtilisateur, object objetCherche);

    }


    /// <summary>
    /// Vérifie si un objet utilise directement un autre objet
    /// </summary>
    public static class CTesteurUtilisationObjet
    {
        /// <summary>
        /// Liste des testeurs connus
        /// </summary>
        private static Dictionary<Type, ITesteurUtilisationObjet> m_tableTesteurs = new Dictionary<Type, ITesteurUtilisationObjet>();


        public static void RegisterTesteur(ITesteurUtilisationObjet testeur)
        {
            if ( !m_tableTesteurs.ContainsKey(testeur.GetType()) )
                m_tableTesteurs[testeur.GetType()] = testeur;
        }

        public static bool DoesUse(object objetUtilisateur, object objetCherche)
        {
            foreach (ITesteurUtilisationObjet testeur in m_tableTesteurs.Values)
            {
                if (testeur.DoesUse(objetUtilisateur, objetCherche))
                    return true;
            }
            return false;
        }

    }
}
