using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.memorydb
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
    public class MemoryTableAttribute : Attribute
    {
        public string NomTable;
        public string ChampId;

        public MemoryTableAttribute(string strNomTable, string strChampId)
        {
            NomTable = strNomTable;
            ChampId = strChampId;
        }
    }
}
