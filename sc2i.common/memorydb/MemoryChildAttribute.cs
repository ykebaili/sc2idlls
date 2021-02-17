using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.memorydb
{
    /// <summary>
    /// Attention, une propriété fille doit obligatoirement
    /// être déclarée et retourner un IEnumerable<T>"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MemoryChildAttribute : Attribute
    {
        public readonly string NomChampForeignKey;

        public MemoryChildAttribute()
        {
            NomChampForeignKey = "";
        }

        public MemoryChildAttribute(string strNomChampForeignKey)
        {
            NomChampForeignKey = strNomChampForeignKey;
        }
    }
}
