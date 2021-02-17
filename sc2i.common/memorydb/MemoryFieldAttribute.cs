using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.memorydb
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MemoryFieldAttribute : Attribute
    {
        public string NomChamp = "";

       //-------------------------------------
        public MemoryFieldAttribute(string strNomChamp)
        {
            NomChamp = strNomChamp;
        }
    }
}
