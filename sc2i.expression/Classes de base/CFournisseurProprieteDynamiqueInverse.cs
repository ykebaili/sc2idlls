using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.expression
{
    /// <summary>
    /// A partir d'une propriété dynamique, retourne la propriété inverse à celle ci
    /// exemple
    /// Class CA
    /// {
    ///     public IEnumerable<CB> Fils{get;} //Retourne la liste des B qui ont A comme parent
    /// }
    /// Class CB
    /// {
    ///     public CA Parent {get;}
    /// }
    /// La propriété inverse de CA.Fils est CB.Parent et vis et versa
    /// </summary>

    public interface IFournisseurProprieteDynamiqueInverse
    {
        CDefinitionProprieteDynamique GetProprieteInverse(Type typePortantLaPropriete, CDefinitionProprieteDynamique def);
    }

    public static class CFournisseurProprieteDynamiqueInverse
    {
        private static Dictionary<Type, List<Type>> m_dicTypeDefToFournisseursInverses = new Dictionary<Type, List<Type>>();

        public static void RegisterFournisseur(Type typeDefinition, Type typeFournisseur)
        {
            List<Type> lstFournisseurs = null;
            if (!m_dicTypeDefToFournisseursInverses.TryGetValue(typeDefinition, out lstFournisseurs))
            {
                lstFournisseurs = new List<Type>();
                m_dicTypeDefToFournisseursInverses[typeDefinition] = lstFournisseurs;
            }
            if ( !lstFournisseurs.Contains ( typeFournisseur ) )
                lstFournisseurs.Add(typeFournisseur);
        }

        public static CDefinitionProprieteDynamique GetProprieteInverse ( Type typePortantLaPropriete, CDefinitionProprieteDynamique def )
        {
            CDefinitionProprieteDynamique defInverse = null;
            List<Type> lstFournisseurs = null;
            if ( m_dicTypeDefToFournisseursInverses.TryGetValue(def.GetType(), out lstFournisseurs ))
            {
                foreach ( Type tp in lstFournisseurs )
                {
                    IFournisseurProprieteDynamiqueInverse f = Activator.CreateInstance ( tp ) as IFournisseurProprieteDynamiqueInverse;
                    if ( f != null )
                    {
                        defInverse = f.GetProprieteInverse ( typePortantLaPropriete, def );
                        if ( defInverse != null )
                            return defInverse;
                    }
                }
            }
            return null;
        }
                    
    }
}
