using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.Referencement
{
    /// <summary>
    /// Indique si la Propriété est une référence vers une ou des Entités
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class ExternalReferencedEntityAttribute : Attribute
    {
        public Type EntityType { get; private set; }
        
        public ExternalReferencedEntityAttribute(Type tp)
        {
            EntityType = tp;
        }
        
    }
}
