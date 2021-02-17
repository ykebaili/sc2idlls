using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.expression
{
    /// <summary>
    /// Une suite d'optimiseurs de GetValue pour obtenir une valeur à partir d'un objet
    /// </summary>
    public class COptimiseurGetValueDynamicMultiple : IOptimiseurGetValueDynamic
    {
        public List<IOptimiseurGetValueDynamic> m_listeOptimiseurs = new List<IOptimiseurGetValueDynamic>();
        
        public void AddOptimiseur ( IOptimiseurGetValueDynamic optimiseur )
        {
            m_listeOptimiseurs.Add(optimiseur);
        }
        
        public object GetValue(object objet)
        {
            object valeur = objet;
            foreach (IOptimiseurGetValueDynamic optimiseur in m_listeOptimiseurs)
            {
                valeur = optimiseur.GetValue(valeur);
                if (valeur == null)
                    return null;
            }
            return valeur;
        }

        public Type GetTypeRetourne()
        {
            if (m_listeOptimiseurs.Count() > 0)
            {
                IOptimiseurGetValueDynamic optimiseur = m_listeOptimiseurs[m_listeOptimiseurs.Count() - 1];
                if (optimiseur != null)
                    return optimiseur.GetTypeRetourne();
            }
            return null;
        }

        
    }
}
