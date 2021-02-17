using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.memorydb
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    public class MemoryParentAttribute : Attribute
    {
        public string NomChampFils = "";
        public bool IsComposition = false;

        //-------------------------------------
        public MemoryParentAttribute(bool bIsComposition)
        {
            NomChampFils = "";
            IsComposition = bIsComposition;
        }

        //-------------------------------------
        public MemoryParentAttribute(string strNomChampFils, bool bIsComposition)
        {
            NomChampFils = strNomChampFils;
            IsComposition = bIsComposition;
        }

    }
}
