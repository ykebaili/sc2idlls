using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace sc2i.common
{
    /// <summary>
    /// Permet facilement d'inclure un type dans une propertygrid, avec un libellé
    /// sympatique.
    /// Il suffit pour cela de dériver cette classe avec T et d'implémenter la fonction GetString
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CGenericObjectConverter<T> : ExpandableObjectConverter
    {
        //----------------------------------------------------------------------------------------
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (typeof(T).IsAssignableFrom(destinationType))
                return true;
            return base.CanConvertFrom(context, destinationType);
        }

        public abstract string GetString(T value);

        //-----------------------------------------------------------------
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is T)
                return GetString((T)value);
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
