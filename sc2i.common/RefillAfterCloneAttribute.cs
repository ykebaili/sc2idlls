using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace sc2i.common
{
    /// <summary>
    /// Indique que le clonage d'une propriété ne peut pas
    /// se faire comme le clonage standard.
    /// Par exemple, c'est utilisé pour les liens entre deux
    /// éléments, dont le clonage est particulier
    /// </summary>
    public class RefillAfterCloneAttribute : Attribute
    {
        public RefillAfterCloneAttribute()
        {
        }

        
        
        
        /// <summary>
        /// Remplit toutes les propriétés notées comme RefillAfterClone
        /// avec les clones des valeurs trouvées dans dicClone.
        /// </summary>
        /// <example>
        /// Class CA 
        /// {
        ///     public IEnumerable<CB> valeurs{get;};
        /// }
        /// class CB
        /// {
        ///     [RefillAfterClone]
        ///     public CA ObjectA1{get;set;}
        ///     [RefillAfterClone]
        ///     public CA ObjectA2{get;set;}
        /// }
        /// Lorsqu'on clone la classe CA, ça clone la classe CB,
        /// mais CB a deux relations sur un objet CA. il se peut que dans le processus
        /// de clonage, on ait également cloné l'objet 2 attaché à l'objet B, 
        /// RefillAfterClone va regarder dans la liste des clonés s'il trouve l'objet 2
        /// en tant que clone et l'affecter à l'objet B après le clonage
        /// </example>
        /// <param name="obj"></param>
        /// <param name="dicClones"></param>
        public static void RefillAfterClone( object obj, Dictionary<object, object> dicClones )
        {
            foreach ( PropertyInfo info in from i in obj.GetType().GetProperties()
                                           where i.GetCustomAttributes(typeof(RefillAfterCloneAttribute), true).Length > 0 &&
                                           i.GetGetMethod() != null && i.GetSetMethod() != null
                                           select i )
            {
                object original = info.GetGetMethod().Invoke ( obj, new object[0] );
                object clone = null;
                if ( original != null && dicClones.TryGetValue ( original, out clone ) )
                {
                    info.GetSetMethod().Invoke ( obj, new object[] {clone} );
                }
            }
        }



    }

}
